

using Cadastro_Usuarios_Domain.ContextConfiguration;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;

namespace Cadastro_Usuarios_WebApi.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDocumentalDbContext(configuration);

            return services;
        }
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            //services.AddTransient<PresencaDeCargaRespondedEventHandler>();

            return services;
        }
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
           // services.AddScoped<IRequestHandler<RealizarPresencaDeCargaCommand, Unit>, RealizarPresencaDeCargaCommandHandler>();
           
            return services;
        }
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            //services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IIntegracaoN4, IntegracaoN4Service>();

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cadastro de Usuários - HTTP API",
                    Version = "v1",
                    Description = "Api de cadastro de usuários"
                });
            });

            return services;
        }

        public static IServiceCollection AddHttpClientsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicy = HttpPolicyExtensions
                           .HandleTransientHttpError()
                           .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            services.AddHttpClient("IntegracaoRpaClient", config =>
            {
                var configBaseAddresPath = Convert.ToBoolean(configuration["UiPathConfig:Cloud"]) ? "UiPathConfig:CloudConfig:Address" : "UiPathConfig:OnPremisesConfig:Address";

                config.BaseAddress = new Uri("http://localhost:3000/");
                config.Timeout = new TimeSpan(0, 1, 0);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            }).AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}
