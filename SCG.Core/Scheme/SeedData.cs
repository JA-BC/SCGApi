using Microsoft.EntityFrameworkCore;
using SCG.Core.Database;
using SCG.Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCG.Core.Scheme
{
    public static class SeedData
    {
        public static TipoCategoriaEntity[] GetTipoCategoriaEntities()
        {
            return new TipoCategoriaEntity[]
            {
                new TipoCategoriaEntity() { Id = 1, Descripcion = "Ingreso" },
                new TipoCategoriaEntity() { Id = 2, Descripcion = "Gasto" }
            };
        }

        public static CategoriaEntity[] GetCategoriaEntities()
        {
            return new CategoriaEntity[]
            {
                new CategoriaEntity() { Id = 1, Nombre = "Sueldo", TipoCategoriaId = 1 },
                new CategoriaEntity() { Id = 2, Nombre = "Gasto Mensuales", TipoCategoriaId = 2 },
                new CategoriaEntity() { Id = 3, Nombre = "Gasto Diarios", TipoCategoriaId = 2 }
            };
        }

        public static BalanceEntity[] GetBalanceEntities()
        {
            return new BalanceEntity[]
            {
                new BalanceEntity()
                {
                    Id = 1,
                    Costo = 85_000,
                    Descripcion = "Primera Quincena",
                    CategoriaId = 1,
                    Fecha = DateTime.Now
                },
                new BalanceEntity()
                {
                    Id = 2,
                    Costo = 9_000,
                    Descripcion = "Pago de Reparaciones",
                    CategoriaId = 2,
                    Fecha = DateTime.Now.AddDays(5)
                }
            };
        }
    }
}
