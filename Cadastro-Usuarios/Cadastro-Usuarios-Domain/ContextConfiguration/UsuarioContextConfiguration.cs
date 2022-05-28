using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cadastro_Usuarios_Domain.ContextConfiguration
{
    public static class UsuarioContextConfiguration
    {
        public static void ConfigureDocumentalDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("ConnectionStrings.DefaultConnection não encontrado no appsettings");

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<UsuarioDbContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                                            sqlServerOptionsAction: sqlOptions =>
                                            {
                                                sqlOptions.MigrationsAssembly(typeof(UsuarioContextConfiguration).GetTypeInfo().Assembly.GetName().Name);
                                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                            });
                });
        }
    }
}
