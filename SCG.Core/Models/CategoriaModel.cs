using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoCategoriaId { get; set; }
    }
}
