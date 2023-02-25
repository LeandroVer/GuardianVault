using System;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager
{
    internal class DatabaseEncryption
    {
        static void EncryptFile( string key) {
            string inputFile = "database.gv";
            string outputFile = "database.gv.enc";
            byte[] keyBytes = HexStringToByteArray(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                using (FileStream encryptedFileStream = new FileStream(outputFile, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(encryptedFileStream, encryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFile(string key) {
            string inputFile = "database.gv";
            string outputFile = "database.gv.enc";
            byte[] keyBytes = HexStringToByteArray(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                using (FileStream decryptedFileStream = new FileStream(outputFile, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        decryptedFileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static byte[] HexStringToByteArray(string hexString) {
            int numBytes = hexString.Length / 2;
            byte[] bytes = new byte[numBytes];

            for (int i = 0; i < numBytes; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }
    }
}
