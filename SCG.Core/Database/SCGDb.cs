using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCG.Core.Database.Entities;
using SCG.Core.Scheme;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Database
{
    public class SCGDb: DbContext
    {
        private readonly IConfiguration _configuration;

        public SCGDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["Data:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EntityConfig();
        }

        public DbSet<BalanceEntity> Balances { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<TipoCategoriaEntity> TipoCategorias { get; set; }
    }
}
