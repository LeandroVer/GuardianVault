using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using PasswordManager.CS_BackEnd;

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
                using (StreamWriter file = new StreamWriter(databasePath, true))
                {
                    file.WriteLine($"{nom},{identifiant},{url},{motDePasse},{note}");
                }
            }
            else
            {
                // Si le fichier n'existe pas, créer un nouveau fichier et ajouter la nouvelle entrée
                using (StreamWriter file = new StreamWriter(databasePath))
                {
                    file.WriteLine($"{nom},{identifiant},{url},{motDePasse},{note}");
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
        private void DeleteDatabaseEnc()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GuardianVault", "database.gv.enc");
            File.Delete(path); //Supprime la base de donnée non chiffrée
        }
    }
}