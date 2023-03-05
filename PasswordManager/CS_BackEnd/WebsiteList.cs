using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        List<WebsiteItem> WebsiteList = new List<WebsiteItem>(); //Liste qui va contenir tous les détails des sites web
       
        public void PasswordExtractor() //Extrait les mdp de la base de donnée non chiffrée
        {
            WebsiteList.Clear();
            string path = System.IO.Path.Combine(appDataFolder, "GuardianVault", "datafile.gv");

            if (!File.Exists(path))
            {
                return; // Quitte la fonction si le fichier n'existe pas
            }

            string[] lines = System.IO.File.ReadAllLines(path);
            int lineCount = lines.Length;

            for (int i = 0; i < lineCount; i++)
            {
                string[] fields = lines[i].Split('|');
                WebsiteList.Add(new WebsiteItem()
                {
                    url_logo = fields[0],
                    nom = fields[1],
                    url = fields[2],
                    email = fields[3],               
                    password = fields[4],
                    note = fields[5]
                });
            }

            
        }

    }
}
