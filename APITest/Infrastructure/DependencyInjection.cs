using APITest.Application.Services.Interfaces;
using APITest.Domain.Interfaces;
using APITest.Infrastructure.Authentication;
using APITest.Infrastructure.Authentication.OptionsSetup;
using APITest.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Data;
using System.Data.SqlClient;

namespace APITest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            services.ConfigureOptions<JwtSettingsOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IRepository, DapperRepository>();
            services.AddScoped<IReadOnlyRepository, DapperReadOnlyRepository>();

            return services;
        }
    }
}
