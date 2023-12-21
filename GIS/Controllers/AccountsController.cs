using GIS.Models;
using GIS.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<Account> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("AddRoleAdmin/{id}")]
        public async Task<IActionResult> AddRoleAdmin(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.AddToRoleAsync(user, "ADMIN");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] SignUp signUp)
        {
            Account account = new()
            {
                UserName = signUp.Email,
                Email = signUp.Email,
                Name = signUp.Name,
                Phone = signUp.Phone
            };

            IdentityResult result = await _userManager.CreateAsync(account, signUp.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var addUserToRoleResult = await _userManager.AddToRoleAsync(account, "USER");
            if (!addUserToRoleResult.Succeeded)
            {
                return BadRequest(addUserToRoleResult.Errors);
            }

            return Ok(new AccountResponse
            {
                Id = account.Id.ToString(),
                Email = account.Email,
                Name = account.Name,
                Phone = account.Phone,
            });

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignIn signIn)
        {
            var user = await _userManager.FindByEmailAsync(signIn.Email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.CheckPasswordAsync(user, signIn.Password);
            if (!result)
            {
                return BadRequest();
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nguyenngocnam2108@gmail.com"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(60);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: expires,
                signingCredentials: creds,
                notBefore: DateTime.Now
                );
            return Ok(new AccountResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }

        [HttpPost("AddRole")]
        [Authorize(Roles = "ROOT")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            Role newRole = new()
            {
                Name = role
            };
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new AccountResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone

            });
        }

        [HttpGet("GetAllUser")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userManager.GetUsersInRoleAsync("USER");
            if (users == null)
            {
                return NotFound();
            }
            List<AccountResponse> accountResponses = new();
            foreach (var user in users)
            {
                accountResponses.Add(new AccountResponse
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone
                });
            }
            return Ok(accountResponses);
        }

        [HttpPut("UpdateUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateAccount updateUser)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var checkEmail = await _userManager.FindByEmailAsync(updateUser.Email);
            if (checkEmail != null && user.Email != updateUser.Email)
            {
                return BadRequest("Email đã tồn tại!");
            }
            user.Name = updateUser.Name;
            user.Phone = updateUser.Phone;
            user.Email = updateUser.Email;
            user.UserName = updateUser.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new AccountResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone
            });
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("GetMe")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new AccountResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone
            });
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("UpdateProfileMe")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileMe([FromBody] UpdateAccount updateProfileMe)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var checkEmail = await _userManager.FindByEmailAsync(updateProfileMe.Email);
            if(checkEmail != null && user.Email != updateProfileMe.Email)
            {
                return BadRequest("Email đã tồn tại!");
            }
            user.Email = updateProfileMe.Email;
            user.UserName = updateProfileMe.Email;
            user.Name = updateProfileMe.Name;
            user.Phone = updateProfileMe.Phone;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new AccountResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone
            });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
            {
                return NotFound();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callback = Url.Action("ResetPassword", "Accounts", new { token, email = user.Email }, Request.Scheme);
            bool isSendEmail = SendEmailForgotPassword(user, token);
            if(!isSendEmail)
            {
                return BadRequest("Đã xảy ra lỗi khi gửi Email!");
            }
            return Ok();


        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        private bool SendEmailForgotPassword(Account user, string token)
        {
            try
            {
                string fromMail = _configuration.GetSection("EmailAccount:Email").Value;
                string fromPassword = _configuration.GetSection("EmailAccount:Password").Value;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "GIS SYSTEM: Quên mật khẩu";
                message.To.Add(new MailAddress($"{user.Email}"));
                message.Body = $"Vui lòng truy cập link này trong vòng 60 phút để thiết lập lại mật khẩu:  \n http://localhost:3000/resetpass/{user.Email}/{token}";
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }



    }
}