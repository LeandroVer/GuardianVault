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

        

        private void Click_DataGridWebsiteList(object sender, SelectionChangedEventArgs e) //Event lorsqu'un des sites web est cliqué dans la liste
        {
            ResetAutoLockTimer();
            Update_Details_WebSiteItem(DataGridWebsiteList.SelectedIndex); //Met à jour les détails du site web cliqué au centre de l'écran
        }
        public void DatafileDisplay(object sender, RoutedEventArgs e)
        {
            Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
            DataGridWebsiteList.SelectedIndex = 0;
            Update_Details_WebSiteItem(0);
        }

        public void Connection(object sender, RoutedEventArgs e, string pwd)
        {
            StartAutoLockTimer();
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string DatafilePath = System.IO.Path.Combine(appDataFolder, "GuardianVault", "Datafile.gv");
            DatafileEncryption.SetPwd(pwd);
            if (File.Exists(DatafilePath))
            {
                MessageBox.Show("Changement du mot de passe maître effectué");
                DatafileEncryption.EncryptFile(); //Chiffre la base de données avec le nouveau mot de passe maitre (changement de mot de passe maitre)
            }
            DatafileEncryption.DecryptFile(); //Déchiffre la base de données
            DatafileDisplay(sender, e); //Affiche la liste des sites web dans la data grid
            DeleteDatafile(); //Supprime la base de données non-chiffrée
            Page_principale(sender, e);
        }
    }
}

