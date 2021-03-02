using Microsoft.EntityFrameworkCore;
using SCG.Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Scheme
{
    public static class EntityBuilder
    {
        public static ModelBuilder EntityConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoCategoriaEntity>(entity =>
            {
                entity.HasData(SeedData.GetTipoCategoriaEntities());
            });

            modelBuilder.Entity<CategoriaEntity>(entity =>
            {
                entity.HasData(SeedData.GetCategoriaEntities());
            });

            modelBuilder.Entity<BalanceEntity>(entity =>
            {
                entity.Property(b => b.Fecha).HasColumnType("date");
                // entity.HasData(SeedData.GetBalanceEntities());
            });

            return modelBuilder;
        }
    }
}
