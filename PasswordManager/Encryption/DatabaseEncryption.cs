using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Security.Cryptography;

namespace PasswordManager
{
    internal class DatabaseEncryption
    {
        private static readonly byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        private static readonly string _ivFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "iv.gv");
        private static string _pwd = "";
        public static void EncryptFile()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string inputPath = Path.Combine(appDataFolder, "GuardianVault", "database.gv");
            string outputPath = Path.Combine(appDataFolder, "GuardianVault", "database.gv.enc");

            string plainText = File.ReadAllText(inputPath);
            if (string.IsNullOrEmpty(plainText))
            {
                File.Create(outputPath).Close();
                using (var aes = Aes.Create())
                {
                    aes.GenerateIV(); // Générer un IV aléatoire
                    File.WriteAllBytes(_ivFilePath, aes.IV); // Sauvegarder l'IV dans un fichier
                }
                return;
            }
            if (string.IsNullOrEmpty(_pwd))
            {
                MessageBox.Show("Encrypt - Erreur MDP");
            }

            byte[] encrypted;
            using (var aes = Aes.Create())
            {
                var key = new System.Security.Cryptography.Rfc2898DeriveBytes(_pwd, _salt);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.GenerateIV(); // Générer un IV aléatoire
                File.WriteAllBytes(_ivFilePath, aes.IV); // Sauvegarder l'IV dans un fichier

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            //Ecrit la base de donnée chiffrée dans un fichier
            string res = Convert.ToBase64String(encrypted);
            File.WriteAllText(outputPath, res);
        }

        public static void DecryptFile()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string inputPath = Path.Combine(appDataFolder, "GuardianVault", "database.gv.enc");
            string outputPath = Path.Combine(appDataFolder, "GuardianVault", "database.gv");

            //Créer un fichier en sortie vide si le fichier d'entrée n'existe pas
            if (!File.Exists(inputPath))
            {
                File.Create(outputPath).Close();
                return;
            }

            string cipherText = File.ReadAllText(inputPath);
            if (string.IsNullOrEmpty(_pwd))
            {
                MessageBox.Show("Decrypt - Erreur MDP");
            }
            string plaintext = null ?? "";
            using (var aes = Aes.Create())
            {
                var key = new System.Security.Cryptography.Rfc2898DeriveBytes(_pwd, _salt);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = File.ReadAllBytes(_ivFilePath); // Lire l'IV depuis le fichier

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            //Ecrit la base de donnée déchiffrée dans un fichier
            if (!File.Exists(outputPath))
            {
                File.Create(outputPath).Close();
            }
            File.WriteAllText(outputPath, plaintext);
        }
        public static void SetPwd(string password_entered)
        {
            _pwd = password_entered;
        }
    }
}
