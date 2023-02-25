using System;
using System.IO;

namespace PasswordManager
{
    internal class FirstConnection
    {
        public static void GenerateHashFile(string password)
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
                sw.Write(hash);
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


