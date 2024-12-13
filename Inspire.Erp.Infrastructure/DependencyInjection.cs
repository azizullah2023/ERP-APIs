using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Inspire.Erp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
            //user can able to get appsetting informations using dependency injection-- IOptions<ApplicationSettings>
            //services.Configure<ApplicationSettings>(settingsSection);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<InspireErpDBContext>(options => options.UseSqlServer(settings.ConnectionString));
            return services;
        }
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            //var secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            var settings = config.GetSection("ApplicationSettings").Get<ApplicationSettings>();

            var key = Encoding.ASCII.GetBytes(settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = settings.Issuer,
                    //ValidAudience = settings.Audience
                };
            });

            return services;
        }


    }
}

