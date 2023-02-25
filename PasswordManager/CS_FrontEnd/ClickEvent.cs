using System;
using System.Collections.Generic;
using System.Linq;
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
                    FirstConnection.GenerateHashFile(TextBoxSetMasterPass.Password); //Genere le fichier contenant le hash du mot de passe maitre
                    Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
                    DataGridWebsiteList.SelectedIndex = 0;
                    Update_Details_WebSiteItem(0);
                    Page_principale(sender, e);
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
                    Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
                    DataGridWebsiteList.SelectedIndex = 0;
                    Update_Details_WebSiteItem(0);
                    Page_principale(sender, e);
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
            Page_premiere_connexion(sender, e);
        }

        private void Click_edit_master_password(object sender, RoutedEventArgs e) ///Event du bouton "Modifier le mot de passe maître"
        {
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
    }
}

