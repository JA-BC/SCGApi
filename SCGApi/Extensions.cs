using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SCG.Core.Database;
using SCG.Core.Database.Entities;
using SCG.Core.Interfaces;
using SCG.Core.Models;
using SCG.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGApi
{
    public static class Extensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            var mapping = new MapperConfiguration(cfg =>
                cfg.AddProfile(new EntityProfile()));

            services.AddSingleton(mapping.CreateMapper());

            services.AddDbContext<SCGDb>();

            services.AddIdentity<UserEntity, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<SCGDb>();

            services.AddScoped<BalanceService>();
            services.AddScoped<CategoriaService>();

            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Secret-key"])),
                    ClockSkew = TimeSpan.Zero
                });

            return services;
        }

    }
}
