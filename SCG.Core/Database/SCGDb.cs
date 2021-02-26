using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCG.Core.Database.Entities;
using SCG.Core.Scheme;

namespace SCG.Core.Database
{
    public class SCGDb: IdentityDbContext<UserEntity>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.EntityConfig();
        }

        public DbSet<BalanceEntity> Balances { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<TipoCategoriaEntity> TipoCategorias { get; set; }
    }
}
