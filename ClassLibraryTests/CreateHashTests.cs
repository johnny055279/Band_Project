using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Band_Web.Models.Tests
{
    [TestClass()]
    public class CreateHashTests
    {
        [TestMethod()]
        public void passWordeEncryptionTest()
        {
            CreateHash createHash = new CreateHash();
            string password = "123";
            byte[] actual = createHash.passWordeEncryption(password, getSaltTest());
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public byte[] getSaltTest()
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[24];
            rNGCryptoServiceProvider.GetBytes(salt);
            return salt;
        }
    }
}