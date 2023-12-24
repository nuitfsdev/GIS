using GIS.Models;
using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;
using GIS.ViewModels.BodyMaterial;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace GIS.Services.BackgroundServices
{
    public class DailyCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DailyCheckService> _logger; // Thêm logger vào service
        private readonly INotification _notificationService;

        public DailyCheckService(IServiceScopeFactory scopeFactory, ILogger<DailyCheckService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _scopeFactory = scopeFactory;
            _serviceScopeFactory = serviceScopeFactory;
            //_notificationService = notificationService;
            _logger = logger; // Gán logger thông qua DI (Dependency Injection)
        }
        
        public async Task<List<MaterialInfo>> GetMaterialInfoAsync()
        {
            var materialInfoList= new List<MaterialInfo>();
            var connectionString = "Host=dpg-cm19mrun7f5s73e5oesg-a.singapore-postgres.render.com;Port=5432;Database=GIS_Development;Username=gis_wong_user;Password=r4kEJ0mHli1lOdkocwFfaniRPd6NYLVA";

            using (var connection = new NpgsqlConnection(connectionString)) // Hoặc NpgsqlConnection nếu bạn sử dụng PostgreSQL
            {
                await connection.OpenAsync();
                var query = @"
            SELECT 
                a.""Name"" as BodyName, 
                c.""AgeStartTime"", 
                b.""Name"" as MaterialName, 
                b.""Age"" as MaterialAge 
            FROM 
                public.""BodyMaterial"" c 
                INNER JOIN public.""Body"" a ON a.""Id"" = c.""BodyId"" 
                INNER JOIN public.""Materials"" b ON b.""Id"" = c.""MaterialId"";";

                // Truy vấn và lưu kết quả vào một mảng
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var materialInfo = new MaterialInfo
                            {
                                BodyName = reader["BodyName"].ToString(),
                                AgeStartTime = (DateTime?)reader["AgeStartTime"],
                                MaterialName = reader["MaterialName"].ToString(),
                                MaterialAge = (int)reader["MaterialAge"]
                            };
                            materialInfoList.Add(materialInfo);
                        }
                    }
                }
            }

            return materialInfoList;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var waitTime = TimeSpan.FromHours(12);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    //var notificationService = scope.ServiceProvider.GetRequiredService<INotification>();
                    var today = DateTime.UtcNow;
                    var _notificationService = scope.ServiceProvider.GetRequiredService<INotification>();
                    var materialInfoList = await GetMaterialInfoAsync();
                    foreach (var materialInfo in materialInfoList)
                    {
                        // Kiểm tra liệu AgeStartTime có được đặt không
                        if (materialInfo.AgeStartTime.HasValue)
                        {
                            // Tính toán số ngày từ AgeStartTime đến nay
                            var timeElapsed = today - materialInfo.AgeStartTime.Value;

                            // So sánh với độ tuổi ('age') của vật liệu
                            if (timeElapsed.TotalDays >= materialInfo.MaterialAge)
                            {
                                // Nếu thời gian hiện tại kể từ AgeStartTime lớn hơn hoặc 
                                // bằng độ tuổi cần kiểm tra, log thông báo
                                Notification nt = new()
                                {
                                    Type="Overdue",
                                    Message = ($"Thông báo: Vật liệu '{materialInfo.MaterialName}' " +
                                                  $"trong '{materialInfo.BodyName}' cần được thay thế. Tuổi vật liệu là {materialInfo.MaterialAge} ngày."),

                                };
                                await _notificationService.CreateAsync(nt);
                                _logger.LogInformation(($"Thông báo: Vật liệu '{materialInfo.MaterialName}' " +
                                                  $"trong '{materialInfo.BodyName}' cần được thay thế. Tuổi vật liệu là {materialInfo.MaterialAge} ngày."));

                            }
                        }
                    }
                   ;

                    if (true)
                    _logger.LogInformation("today adawkjfn eoufu");
                }

                // Đợi một khoảng thời gian cố định trước khi thực hiện loop tiếp theo
                await Task.Delay(waitTime, stoppingToken);
            }
        }
    }
}
