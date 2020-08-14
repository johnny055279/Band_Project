using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Band_Web.Models
{
    public class SignUpModel
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public byte[] UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserSexual { get; set; }
        public DateTime UserBirthday { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserCountry { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}