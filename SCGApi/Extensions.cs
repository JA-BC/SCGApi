using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SCG.Core.Database;
using SCG.Core.Interfaces;
using SCG.Core.Models;
using SCG.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddScoped<BalanceService>();
            services.AddScoped<CategoriaService>();

            return services;
        }
    }
}
