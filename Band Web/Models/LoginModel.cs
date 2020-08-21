using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Band_Web.Models
{
    public class LoginModel
    {
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public byte[] UserSalt { get; set; }
    }
}