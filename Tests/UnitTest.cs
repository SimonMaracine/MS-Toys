using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encr;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestEncryption()
        {
            string message = "Hello, world! My name is Simon.";

            var cipher = Encryption.EncryptString(message, "KEY.dat", "IV.dat");
            var original = Encryption.DecryptString(cipher, "KEY.dat", "IV.dat");

            Assert.AreEqual(message, original);
        }
    }
}
