using GIS.Models;
using GIS.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AccountsController(UserManager<Account> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
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
            return Ok( new AccountResponse
            {
                Id = account.Id.ToString(),
                Email = account.Email,
                Name = account.Name,
                Phone = account.Phone
            });
        }
        [HttpPost("addrole")]
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
    }
}
