using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        List<WebsiteItem> WebsiteList = new List<WebsiteItem>(); //Liste qui va contenir tous les détails des sites web
       
        public void PasswordExtractor() //Extrait les mdp de la base de donnée non chiffrée
        {
            WebsiteList.Clear();
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "database.gv");

            if (!File.Exists(path))
            {
                return; // Quitte la fonction si le fichier n'existe pas
            }

            string[] lines = System.IO.File.ReadAllLines(path);
            int lineCount = lines.Length;

            for (int i = 0; i < lineCount; i++)
            {
                string[] fields = lines[i].Split(',');
                WebsiteList.Add(new WebsiteItem()
                {
                    logo = "/Assets/img_temp.png",
                    nom = fields[0],
                    email = fields[1],
                    url = fields[2],
                    password = fields[3],
                    note = fields[4]
                });
            }

            
        }
    }
}
