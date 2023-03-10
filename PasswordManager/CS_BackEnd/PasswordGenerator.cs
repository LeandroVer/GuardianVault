using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public void Generate(int length, bool useLowercase, bool useUppercase, bool useNumbers, bool useSymbols) 
        {
            if (useLowercase == false && useUppercase == false && useNumbers == false && useSymbols == false)
            {
                MessageBox.Show("Veuillez cocher au moins une case pour générer votre mot de passe");
            }
            else
            {
                Add_MDP.Text = GeneratePassword(length, useLowercase, useUppercase, useNumbers, useSymbols);
            }
        }
        private static string GeneratePassword(int length, bool useLowercase, bool useUppercase, bool useNumbers, bool useSymbols)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string symbolChars = "!@#$%^&*()_+-=[]{};:./<>?";
            StringBuilder passwordBuilder = new StringBuilder(length);
            Random random = new Random();

            // Ajouter des caractères de chaque type si la CheckBox correspondante est cochée
            if (useLowercase)
            {
                passwordBuilder.Append(lowercaseChars[random.Next(lowercaseChars.Length)]);
            }
            if (useUppercase)
            {
                passwordBuilder.Append(uppercaseChars[random.Next(uppercaseChars.Length)]);
            }
            if (useNumbers)
            {
                passwordBuilder.Append(numberChars[random.Next(numberChars.Length)]);
            }
            if (useSymbols)
            {
                passwordBuilder.Append(symbolChars[random.Next(symbolChars.Length)]);
            }

            // Ajouter des caractères aléatoires jusqu'à atteindre la longueur souhaitée
            while (passwordBuilder.Length < length)
            {
                string availableChars = "";
                if (useLowercase)
                {
                    availableChars += lowercaseChars;
                }
                if (useUppercase)
                {
                    availableChars += uppercaseChars;
                }
                if (useNumbers)
                {
                    availableChars += numberChars;
                }
                if (useSymbols)
                {
                    availableChars += symbolChars;
                }
                passwordBuilder.Append(availableChars[random.Next(availableChars.Length)]);
            }

            return passwordBuilder.ToString();
            
        }
        public void Effacer()
        {
            Add_NomSite.Text = "";
            Add_ID.Text = "";
            Add_URL.Text = "";
            Add_MDP.Text = "";
        }
    }
}
