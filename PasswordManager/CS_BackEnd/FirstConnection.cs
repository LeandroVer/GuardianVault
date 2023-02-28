using System;
using System.IO;
using System.Windows;

namespace PasswordManager
{
    internal class FirstConnection
    {
        public static bool First_connection(string pwd_set,string pwd_confirm)
        {
            if (pwd_set == pwd_confirm)
            {

                if (IsPasswordSecure(pwd_set))
                {
                    GenerateFile(pwd_set); //Genere le fichier contenant le hash du mot de passe maitre
                    return true;
                }
                else
                {
                    MessageBox.Show("Le mot de passe doit contenir au moins 6 caractères, dont au moins une lettre et un chiffre");
                }

            }
            else
            {
                MessageBox.Show("Le mot de passe et confirmation doivent être identique");
            }
            return false;
        }
        public static void GenerateFile(string password)
        {

            string hash = PasswordVerifier.HashPassword(password);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "GuardianVault");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            
            string hashFile = Path.Combine(path, "hash.gv");

            using (StreamWriter sw = new StreamWriter(hashFile))
            {
                sw.Write(hash); //Créer un fichier contenant le hash du mot de passe
            }
        }
        public static bool IsPasswordSecure(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            bool hasLetter = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            return hasLetter && hasDigit;
        }

    }
}


