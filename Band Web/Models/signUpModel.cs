using System;
using System.ComponentModel;

namespace Band_Web.Models
{
    public class SignUpModel
    {
        public int UserId { get; set; }

        [DisplayName("Account")]
        public string UserAccount { get; set; }

        [DisplayName("Password")]
        public string UserPassword { get; set; }

        [DisplayName("Email")]
        public string UserEmail { get; set; }

        [DisplayName("Sexual")]
        public string UserSexual { get; set; }

        [DisplayName("Birthday")]
        public DateTime UserBirthday { get; set; }

        [DisplayName("Address")]
        public string UserAddress { get; set; }

        [DisplayName("Phone NUmber")]
        public string UserPhone { get; set; }

        [DisplayName("Country")]
        public string UserCountry { get; set; }

        public DateTime LastEditDate { get { return DateTime.Now; } set { } }
    }
}