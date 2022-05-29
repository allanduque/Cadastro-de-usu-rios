using Cadastro_Usuarios_WebApi.Extensions;
using MediatR;

namespace Cadastro_Usuarios_WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IWebHostEnvironment env, IServiceCollection services)
        {
            services.AddControllers();

            services.AddLogging();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCustomDbContext(Configuration)
            .AddRepositories()
            .AddCommands()
            .AddSwagger(Configuration)
            .AddHttpClientsConfig(Configuration);
            services.AddMediatR(typeof(Startup));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceCollection services)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
