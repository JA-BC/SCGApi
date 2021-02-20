using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCG.Core.Database.Entities
{
    [Table("Categoria")]
    public class CategoriaEntity
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        #region ForeignKeys
        public int TipoCategoriaId { get; set; }
        public TipoCategoriaEntity TipoCategoria { get; set; }
        public ICollection<BalanceEntity> Balances { get; set; }
        #endregion
    }
}
