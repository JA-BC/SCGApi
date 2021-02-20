using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCG.Core.Database.Entities
{
    [Table("Balance")]
    public class BalanceEntity
    {
        [Key]
        public int Id { get; set; }

        public int Costo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        #region ForeignKeys
        public int CategoriaId { get; set; }
        public CategoriaEntity Categoria { get; set; }
        #endregion
    }
}
