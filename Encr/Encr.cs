using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encr
{
    public class Encr
    {
        public byte[] EncryptString(string plainString, string keyPath, string ivPath)
        {
            byte[] result;

            using (SymmetricAlgorithm aes = new AesCryptoServiceProvider())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                using (var encrypter = aes.CreateEncryptor())
                {
                    var output = new MemoryStream();
                    var stream = new CryptoStream(output, encrypter, CryptoStreamMode.Write);
                    var writer = new StreamWriter(stream);

                    writer.Write(plainString);
                    result = output.ToArray();

                    writer.Close();
                    stream.Close();
                    output.Close();
                }

                WriteItemToFile(keyPath, aes.Key);
                WriteItemToFile(ivPath, aes.IV);
            }

            return result;
        }

        public string DecryptString(byte[] encryptedString, string keyPath, string ivPath)
        {
            byte[] key = ReadItemFromFile(keyPath);
            byte[] iv = ReadItemFromFile(ivPath);

            byte[] result;

            using (SymmetricAlgorithm aes = new AesCryptoServiceProvider())
            {
                using (var decrypter = aes.CreateDecryptor(key, iv))
                {
                    var output = new MemoryStream();
                    var stream = new CryptoStream(output, decrypter, CryptoStreamMode.Write);

                    stream.Write(encryptedString, 0, encryptedString.Length);
                    result = output.ToArray();

                    stream.Close();
                    output.Close();
                }
            }

            return Encoding.UTF8.GetString(result);
        }

        private byte[] ReadItemFromFile(string filePath)
        {
            byte[] buffer;

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[file.Length];

                file.Read(buffer, 0, (int) file.Length);
            }

            return buffer;
        }

        private void WriteItemToFile(string filePath, byte[] buffer)
        {
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                file.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
