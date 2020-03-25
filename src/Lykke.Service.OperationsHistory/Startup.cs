using System;
using AutoMapper;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.OperationsHistory.MappingProfiles;
using Lykke.Service.OperationsHistory.Middleware;
using Lykke.Service.OperationsHistory.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.OperationsHistory
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "OperationsHistory API", ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "OperationsHistoryLog";
                    logs.AzureTableConnectionStringResolver =
                        settings => settings.OperationsHistoryService.Db.LogsConnString;
                };

                options.Extend = (sc, rm) => { sc.AddAutoMapper(typeof(AutoMapperProfile)); };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.WithMiddleware = x => { x.UseMiddleware<BadRequestExceptionMiddleware>(); };
            });
        }
    }
}
