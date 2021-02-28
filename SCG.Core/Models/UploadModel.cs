using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Models
{
    public class UploadModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public IFormFile File { get; set; }

        public string UserId { get; set; }

    }
}
