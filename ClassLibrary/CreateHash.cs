using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Band_Web.Models
{
    public class CreateHash
    {
        public byte[] passWordeEncryption(string password, byte[] salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            return pbkdf2.GetBytes(24);
        }

        public byte[] getSalt()
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[24];
            rNGCryptoServiceProvider.GetBytes(salt);
            return salt;
        }
    }
}