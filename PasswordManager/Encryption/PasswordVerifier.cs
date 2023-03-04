using System;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager
{
    internal class PasswordVerifier
    {

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static bool VerifyPassword(string userInput)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "GuardianVault", "hash.gv");

            string hash;
            // Lit le hash stocké dans le fichier
            using (StreamReader sr = new StreamReader(path))
            {
                hash = sr.ReadLine() ?? ""; // si sr.ReadLine() est null, affecte une chaîne vide ("") à hash
            }

            string userInputHash = HashPassword(userInput);

            return (hash == userInputHash);
        }

    }
}
