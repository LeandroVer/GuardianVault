using System;
using System.Text;
using System.IO;
using System.Windows;
using System.Security.Cryptography;

namespace PasswordManager
{
    internal class DatafileEncryption
    {
        private static readonly byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        private static readonly string _ivFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "iv.gv");
        private static readonly string _datafilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "datafile.gv");
        private static readonly string _datafileEncPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "datafile.gv.enc");
        private static string _pwd = "";
        public static void EncryptFile()
        {
            string plainText = File.ReadAllText(_datafilePath);
            if (string.IsNullOrEmpty(plainText))
            {
                File.Create(_datafileEncPath).Close();
                using (var aes = Aes.Create())
                {
                    aes.GenerateIV(); // Générer un IV aléatoire
                    File.WriteAllBytes(_ivFilePath, aes.IV); // Sauvegarder l'IV dans un fichier
                }
                return;
            }
            if (string.IsNullOrEmpty(_pwd))
            {
                MainWindow.Open_pop_up("Encrypt - Erreur MDP");
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
            File.WriteAllText(_datafileEncPath, res);
        }

        public static void DecryptFile()
        {
            //Créer un fichier en sortie vide si le fichier d'entrée n'existe pas
            if (!File.Exists(_datafileEncPath))
            {
                File.Create(_datafilePath).Close();
                return;
            }

            string cipherText = File.ReadAllText(_datafileEncPath);
            if (string.IsNullOrEmpty(_pwd))
            {
                MainWindow.Open_pop_up("Decrypt - Erreur MDP");
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
            File.WriteAllText(_datafilePath, plaintext);
        }
        public static void SetPwd(string password_entered)
        {
            _pwd = password_entered;
        }
    }
}
