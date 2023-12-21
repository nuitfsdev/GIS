using GIS.Services.ImplementServices;
using GIS.Services.InterfaceServices;

namespace GIS.Services
{
    public static class ServicesInjection
    {
        public static void Excecute(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPrismService, PrismService>();
        }
    }
}
