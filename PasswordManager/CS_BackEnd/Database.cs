using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Click_AddPwd(object sender, RoutedEventArgs e)
        {
            // Récupérer les valeurs des champs de texte
            string nom = Add_NomSite.Text;
            string identifiant = Add_ID.Text;
            string url = Add_URL.Text;
            string motDePasse = Add_MDP.Text;
            string note = ""; // Note vide par défaut

            // Définir le chemin d'accès complet pour le fichier de base de données
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string databasePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "database.gv");
            string databaseEncPath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "database.gv.enc");

            // Vérifier si le fichier de base de données existe déjà
            if (File.Exists(databaseEncPath))
            {
                DatabaseEncryption.DecryptFile(); //Déchiffre la base de donnée

                // Ajouter la nouvelle entrée à la liste des sites web
                List<WebsiteItem> websites = new List<WebsiteItem>();
                using (StreamReader reader = new StreamReader(databasePath))
                {
                    string line;
                    while ((line = reader.ReadLine()!) != null)
                    {
                        string[] parts = line.Split('|');
                        WebsiteItem website = new WebsiteItem("/Assets/img_temp.png",parts[0], parts[1], parts[2], parts[3], parts[4]);
                        websites.Add(website);
                    }
                }
                websites.Add(new WebsiteItem("", nom, url, identifiant, motDePasse, note));

                // Écrire la liste mise à jour dans le fichier de base de données
                using (StreamWriter writer = new StreamWriter(databasePath))
                {
                    foreach (WebsiteItem website in websites)
                    {
                        writer.WriteLine($"{website.nom}|{website.email}|{website.url}|{website.password}|{website.note}");
                    }
                }
            }
            else
            {
                // Si le fichier n'existe pas, créer un nouveau fichier et ajouter la nouvelle entrée
                using (StreamWriter writer = new StreamWriter(databasePath))
                {
                    writer.WriteLine($"{nom}|{identifiant}|{url}|{motDePasse}|{note}");
                }
            }

            // Effacer les champs de texte pour permettre l'ajout de nouveaux mots de passe
            Add_NomSite.Text = "";
            Add_ID.Text = "";
            Add_URL.Text = "";
            Add_MDP.Text = "";

            DatabaseEncryption.EncryptFile(); //Chiffre la base de donnée
            DatabaseDisplay(sender, e); //Actualise la liste des sites web dans la data grid
            DeleteDatabase(); //Supprime la base de donnée non chiffrée
        }




        public void DeleteDatabase()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "database.gv");
            File.Delete(path); //Supprime la base de donnée non chiffrée
        }
        public void DeleteDatabaseEnc()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "database.gv.enc");
            File.Delete(path); //Supprime la base de donnée chiffrée
        }
        public void DeleteHash()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "hash.gv");
            File.Delete(path); //Supprime le hash
        }

        private void Click_Supprimer(object sender, RoutedEventArgs e) //Event du bouton Supprimer
        {

            if (DataGridWebsiteList.SelectedItem is WebsiteItem selectedItem)
            {
                //Enleve l'élément sélectionné de la DataGrid
                WebsiteList.Remove(selectedItem);

                //Chiffre la base de donnée en enlevant l'élément sélectionné
                DatabaseEncryption.DecryptFile();
                RemoveWebsiteItem(selectedItem);
                DatabaseEncryption.EncryptFile();

                DatabaseDisplay(sender, e);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un site web à supprimer.");
            }
        }

        public static void RemoveWebsiteItem(WebsiteItem selectedItem)
        {
            string websiteItemString = selectedItem.ConvertWebsiteItemToString(selectedItem);
            string databaseFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "database.gv");
            string[] databaseLines = File.ReadAllLines(databaseFilePath);

            for (int i = 0; i < databaseLines.Length; i++)
            {
                if (databaseLines[i] == websiteItemString)
                {
                    databaseLines = databaseLines.Where((val, idx) => idx != i).ToArray();
                    break;
                }
            }

            File.WriteAllLines(databaseFilePath, databaseLines);
        }
    }
}