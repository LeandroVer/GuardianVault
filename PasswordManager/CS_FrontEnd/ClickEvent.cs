using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Click_premiere_connexion(object sender, RoutedEventArgs e) //Event du bouton OK de la fenêtre de 1ere connexion
        {
            if (TextBoxSetMasterPass.Password == TextBoxConfirmMasterPass.Password)
            {
                
                if (FirstConnection.IsPasswordSecure(TextBoxSetMasterPass.Password))
                {
                    FirstConnection.GenerateFiles(TextBoxSetMasterPass.Password); //Genere le fichier contenant le hash du mot de passe maitre
                    Connection(sender, e, TextBoxSetMasterPass.Password);
                }
                else
                {
                    MessageBox.Show("Le mot de passe doit contenir au moins 6 caractères, dont au moins une lettre et un chiffre");
                }
                
            }else
            {
                MessageBox.Show("Le mot de passe et confirmation doivent être identique");
            }
        }

        private void Click_connexion(object sender, RoutedEventArgs e) ///Event du bouton OK de la fenêtre de connexion
        {
            if (TextBoxMasterPass.Password != "")
            {
                
                if (PasswordVerifier.VerifyPassword(TextBoxMasterPass.Password))
                {
                    Connection(sender, e, TextBoxMasterPass.Password);
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un mot de passe");
            }
        }

        private void Click_deconnexion(object sender, RoutedEventArgs e) //Event du bouton "Se déconnecter"
        {
            Page_connexion(sender, e);
        }

        private void Click_delete_account(object sender, RoutedEventArgs e) //Event du bouton "Supprimer le compte"
        {
            DeleteDatabaseEnc();
            DeleteHash();
            Page_premiere_connexion(sender, e);
        }

        private void Click_edit_master_password(object sender, RoutedEventArgs e) ///Event du bouton "Modifier le mot de passe maître"
        {
            DatabaseEncryption.DecryptFile();
            DeleteHash();
            Page_premiere_connexion(sender, e);
        }

        private void Click_parametres(object sender, RoutedEventArgs e) //Event du bouton Paramètres (écrou)
        {            
            if (GridSecondaire.Visibility == Visibility.Visible) //Affichage de l'une ou l'autre colonne de droite (Paramètres ou Ajout de site)
            {
                Page_parametres(sender, e);
            }
            else
            {
                Page_principale(sender, e);
            }
        }

        private void Click_DataGridWebsiteList(object sender, SelectionChangedEventArgs e) //Event lorsqu'un des sites web est cliqué dans la liste
        {
            Update_Details_WebSiteItem(DataGridWebsiteList.SelectedIndex); //Met à jour les détails du site web cliqué au centre de l'écran
        }
        public void DatabaseDisplay(object sender, RoutedEventArgs e)
        {
            Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
            DataGridWebsiteList.SelectedIndex = 0;
            Update_Details_WebSiteItem(0);
        }

        public void Connection(object sender, RoutedEventArgs e, string pwd)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string databasePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "database.gv");
            DatabaseEncryption.SetPwd(pwd);
            if (File.Exists(databasePath))
            {
                MessageBox.Show("Changement du mot de passe maître effectué");
                DatabaseEncryption.EncryptFile(); //Chiffre la base de données avec le nouveau mot de passe maitre (changement de mot de passe maitre)
            }
            DatabaseEncryption.DecryptFile(); //Déchiffre la base de données
            DatabaseDisplay(sender, e); //Affiche la liste des sites web dans la data grid
            DeleteDatabase(); //Supprime la base de données non-chiffrée
            Page_principale(sender, e);
        }
    }
}

