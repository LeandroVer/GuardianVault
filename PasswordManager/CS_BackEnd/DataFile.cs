using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public void LoadMainPage(string pwd) //Charge la page principale
        {
            StartAutoLockTimer();
            string DatafilePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "Datafile.gv");
            DatafileEncryption.SetPwd(pwd);
            if (File.Exists(DatafilePath))
            {
                Open_pop_up("Mot de passe maître valide");
                DatafileEncryption.EncryptFile(); //Chiffre la base de données avec le nouveau mot de passe maitre (changement de mot de passe maitre)
            }
            DatafileEncryption.DecryptFile(); //Déchiffre la base de données
            DatagridDisplay(); //Affiche la liste des sites web dans la data grid
            DeleteDatafile(); //Supprime la base de données non-chiffrée
            Visibility_principale();
        }
        public void AddPwd(string logo, string nom, string url, string identifiant, string motDePasse)
        {
            
            DeleteFilteredList();
            ResetAutoLockTimer();
            string note = ""; // Note vide par défaut

            // Définir le chemin d'accès complet pour le fichier de base de données
            string datafilePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "datafile.gv");
            string datafileEncPath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "datafile.gv.enc");


            // Vérifier si le fichier de base de données existe déjà
            if (File.Exists(datafileEncPath))
            {
                DatafileEncryption.DecryptFile(); //Déchiffre la base de donnée

                // Ajouter la nouvelle entrée à la liste des sites web
                List<WebsiteItem> websites = new List<WebsiteItem>();
                using (StreamReader reader = new StreamReader(datafilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()!) != null)
                    {
                        string[] parts = line.Split('|');
                        WebsiteItem website = new WebsiteItem(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                        websites.Add(website);
                    }
                }
                websites.Add(new WebsiteItem(logo, nom, url, identifiant, motDePasse, note));

                // Écrire la liste mise à jour dans le fichier de base de données
                using (StreamWriter writer = new StreamWriter(datafilePath))
                {
                    foreach (WebsiteItem website in websites)
                    {
                        writer.WriteLine($"{website.url_logo}|{website.nom}|{website.url}|{website.email}|{website.password}|{website.note}");
                    }
                }
            }
            else
            {
                // Si le fichier n'existe pas, créer un nouveau fichier et ajouter la nouvelle entrée
                using (StreamWriter writer = new StreamWriter(datafilePath))
                {
                    writer.WriteLine($"{logo}|{nom}|{url}|{identifiant}|{motDePasse}|{note}");
                }
            }

            // Effacer les champs de texte pour permettre l'ajout de nouveaux mots de passe
            Add_NomSite.Text = "";
            Add_ID.Text = "";
            Add_URL.Text = "";
            Add_MDP.Text = "";

            DatafileEncryption.EncryptFile(); //Chiffre la base de donnée
            DatagridDisplay(); //Actualise la liste des sites web dans la data grid
            DeleteDatafile(); //Supprime la base de donnée non chiffrée
        }

        public void DeleteDatafile()
        {
            string path = System.IO.Path.Combine(appDataFolder, "GuardianVault", "datafile.gv");
            if (File.Exists(path)) {
                File.Delete(path); //Supprime la base de donnée non chiffrée
            } 
        }
        public void DeleteDatafileEnc()
        {
            string path = System.IO.Path.Combine(appDataFolder, "GuardianVault", "datafile.gv.enc");
            if (File.Exists(path)) {
                File.Delete(path); //Supprime la base de donnée chiffrée
            }
        }
        public void DeleteHash()
        {
            string path = System.IO.Path.Combine(appDataFolder, "GuardianVault", "hash.gv");
            if (File.Exists(path)) {
                File.Delete(path); //Supprime le hash
            }
        }

        public void Supprimer() //Event du bouton Supprimer
        {
            ResetAutoLockTimer();
            DeleteFilteredList();

            if (DataGridWebsiteList.SelectedItem is WebsiteItem selectedItem)
            {
                //Enleve l'élément sélectionné de la DataGrid
                WebsiteList.Remove(selectedItem);

                //Chiffre la base de donnée en enlevant l'élément sélectionné
                DatafileEncryption.DecryptFile();
                RemoveWebsiteItem(selectedItem);
                DatafileEncryption.EncryptFile();

                DatagridDisplay();
                DeleteDatafile();
            }
            else
            {
                Open_pop_up("Veuillez sélectionner un site web à supprimer.");
            }
        }

        public static void RemoveWebsiteItem(WebsiteItem selectedItem)
        {
            string websiteItemString = selectedItem.ConvertWebsiteItemToString(selectedItem);
            string datafileFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "datafile.gv");
            string[] datafileLines = File.ReadAllLines(datafileFilePath);

            for (int i = 0; i < datafileLines.Length; i++)
            {
                if (datafileLines[i] == websiteItemString)
                {
                    datafileLines = datafileLines.Where((val, idx) => idx != i).ToArray();
                    break;
                }
            }

            File.WriteAllLines(datafileFilePath, datafileLines);
        }
        public void Save_into_datafile(string identifiant, string url, string motDePasse, string note)
        {
            ResetAutoLockTimer();
            DeleteFilteredList();

            if (DataGridWebsiteList.SelectedItem is WebsiteItem selectedItem)
            {
                DatafileEncryption.DecryptFile();

                //Modifie la ligne correspondante à la modification avec les nouvelles informations saisies
                string DatafilePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "Datafile.gv");
                string selectedItem_string = selectedItem.url_logo + "|" + selectedItem.nom + "|" + selectedItem.url + "|" + selectedItem.email + "|" + selectedItem.password + "|" + selectedItem.note;
                string modified_string = selectedItem.url_logo + "|" + selectedItem.nom + "|" + url + "|" + identifiant + "|" + motDePasse + "|" + note;
                string[] lines = File.ReadAllLines(DatafilePath);

                // Rechercher la ligne correspondant à selectedItem_string et la remplacer par modified_string
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Equals(selectedItem_string))
                    {
                        lines[i] = modified_string;
                        break;
                    }
                }

                // Écrire les nouvelles lignes dans le fichier
                File.WriteAllLines(DatafilePath, lines);

                DatafileEncryption.EncryptFile();
                DatagridDisplay();
                DeleteDatafile();
            }
            else
            {
                Open_pop_up("Veuillez sélectionner un site web à modifier.");
            }
        }
        public void DatagridDisplay()
        {
            Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
            DataGridWebsiteList.SelectedIndex = 0;
            Update_Details_WebSiteItem(0);
        }
    }
}