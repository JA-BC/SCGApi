using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCG.Core.Database.Entities
{
    [Table("TipoCategoria")]
    public class TipoCategoriaEntity
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        #region ForeignKeys
        public ICollection<CategoriaEntity> Categorias { get; set; }
        #endregion

    }
}
