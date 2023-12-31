﻿using GIS.Services.ImplementServices;
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
            services.AddScoped<IBodyCompService, BodyCompService>();
            services.AddScoped<IBodyService, BodyService>();
            services.AddScoped<IPrismService, PrismService>();
            services.AddScoped<IDamageReportService, DamageReportService>();
            services.AddScoped<IBodyMaterialService, BodyMaterialService>();
            services.AddScoped<INodeService, NodeService>();
            services.AddScoped<IFaceService, FaceService>();
            services.AddScoped<IFaceNodeService, FaceNodeService>();
            services.AddScoped<IBodyRepairStatusService, BodyRepairStatusService>();
        }
    }
}
