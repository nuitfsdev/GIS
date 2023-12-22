using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services
{
    public static class ServicesInjection
    {
        public static void Excecute(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<INotification, NotificationService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPrismService, PrismService>();
            services.AddScoped<IDamageReportService, DamageReportService>();
            services.AddScoped<IBodyMaterialService, BodyMaterialService>();
        }
    }
}
