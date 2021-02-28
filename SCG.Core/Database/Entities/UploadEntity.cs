using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCG.Core.Database.Entities
{
    [Table("Upload")]
    public class UploadEntity
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        #region Foreign Key
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        #endregion

    }
}
