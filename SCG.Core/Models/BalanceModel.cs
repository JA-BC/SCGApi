using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Models
{
    public class BalanceModel
    {
        public int Id { get; set; }
        public int Costo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public int CategoriaTipoCategoriaId { get; set; }
    }
}
