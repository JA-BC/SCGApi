using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string NewPassword { get; set; }
    }
}
