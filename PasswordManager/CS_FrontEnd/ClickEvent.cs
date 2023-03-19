using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        //------------- Fenetre de création du mot de passe maitre ---------------
        private void Click_premiere_connexion(object sender, RoutedEventArgs e) //Event du bouton OK de la fenêtre de 1ere connexion
        {
            if (FirstConnection.First_connection(TextBoxSetMasterPass.Password, TextBoxConfirmMasterPass.Password)){
                LoadMainPage(TextBoxSetMasterPass.Password);
            }
        }

        //------------- Fenetre de connextion ---------------
        private void Click_connexion(object sender, RoutedEventArgs e) ///Event du bouton OK de la fenêtre de connexion
        {
            if (TextBoxMasterPass.Password != ""){
                if (PasswordVerifier.VerifyPassword(TextBoxMasterPass.Password)){
                    LoadMainPage(TextBoxMasterPass.Password);
                }else{
                    MessageBox.Show("Mot de passe incorrect");
                }
            }else{
                MessageBox.Show("Veuillez entrer un mot de passe");
            }
        }

        //------------- Fenetre principale ---------------
        private void Click_DataGridWebsiteList(object sender, SelectionChangedEventArgs e) //Event lorsqu'un des sites web est cliqué dans la liste
        {
            ResetAutoLockTimer();
            Disable_modification();
            Update_Details_WebSiteItem(DataGridWebsiteList.SelectedIndex); //Met à jour les détails du site web cliqué au centre de l'écran
        }
        public void DatafileDisplay(object sender, RoutedEventArgs e)
        {
            DatagridDisplay();
        }

        private void Click_SearchBar(object sender, RoutedEventArgs e) //Event lorsqu'on clique sur le bouton de recherche
        {
            Disable_modification();
            SearchBar();
        }

        private void Click_AddPwd(object sender, RoutedEventArgs e)//Event lors de l'ajout d'un mot de passe
        {
            if (Add_NomSite.Text == "" | Add_ID.Text == "" | Add_URL.Text == "" | Add_MDP.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                Disable_modification();
                // Transformer par exemple https://www.google.com/search en www.google.com
                Uri uri = new UriBuilder(Add_URL.Text).Uri;
                string baseUrl = uri.GetLeftPart(UriPartial.Authority);
                baseUrl = baseUrl.Replace("http://", "").Replace("https://", "");

                // Récupération de l'url de l'icon
                string url_img = Grab_icon(baseUrl);
                if (url_img == "NONE") //Si il y a un problème avec le lien du favicon
                {
                    AddPwd("/Assets/icon_placeholder.png", Add_NomSite.Text, baseUrl, Add_ID.Text, Add_MDP.Text); //Associe le lien du placeholder
                }
                else
                {
                    AddPwd(url_img, Add_NomSite.Text, baseUrl, Add_ID.Text, Add_MDP.Text);
                }
            }      
        }

        private void Click_copy_id(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(EmailWebSiteItem.Text);
        }

        private void Click_open_website(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = URLWebSiteItem.Text.ToString(), UseShellExecute = true });
        }

        private void Click_copy_mdp(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(PassordWebSiteItem.Text);
        }

        private void Click_Modifier(object sender, RoutedEventArgs e) //Event du bouton Supprimer
        {
            if (NoteWebSiteItem.IsReadOnly == true)
            {
                Enable_modification();
            }
            else
            {
                Disable_modification();
            }
            
        }

        private void Click_Supprimer(object sender, RoutedEventArgs e) //Event du bouton Supprimer
        {
            Disable_modification();
            Supprimer();
        }
        private void Click_Generate(object sender, RoutedEventArgs e)
        {
            Disable_modification();
            Generate(int.Parse(TextBoxLongueur.Text), Checkbox_Minuscule.IsChecked == true, Checkbox_Majuscule.IsChecked == true, Checkbox_Nombre.IsChecked == true, Checkbox_Symbole.IsChecked == true);
        }
        private void Click_Effacer(object sender, RoutedEventArgs e)
        {
            Disable_modification();
            Effacer();
        }
        //------------- Fenetre paramètres ---------------
        private void Click_parametres(object sender, RoutedEventArgs e) //Event du bouton Paramètres (écrou)
        {
            ResetAutoLockTimer();
            if (GridSecondaire.Visibility == Visibility.Visible) //Affichage de l'une ou l'autre colonne de droite (Paramètres ou Ajout de site)
            {
                Visibility_parametres();
            }
            else
            {
                Visibility_principale();
            }
        }
        private void Click_deconnexion(object sender, RoutedEventArgs e) //Event du bouton "Se déconnecter"
        {
            StopAutoLockTimer();
            Visibility_connexion();
        }

        private void Click_delete_account(object sender, RoutedEventArgs e) //Event du bouton "Supprimer le compte"
        {
            Delete_account();
        }

        private void Click_edit_master_password(object sender, RoutedEventArgs e) ///Event du bouton "Modifier le mot de passe maître"
        {
            Edit_master_password();
        }

        private void Click_exporter_coffre(object sender, EventArgs e)
        {
            Exporter_coffre();
        }

        private void Click_importer_coffre(object sender, EventArgs e)
        {
            Importer_coffre();
        }



        //------------- Detection touche "entrée" ---------------
        private void TextBoxConfirmMasterPass_KeyDown(object sender, KeyEventArgs e) //Event lorsqu'on appuie sur la touche entrée dans la fenêtre de 1ere connexion
        {
            if (e.Key == Key.Return)
            {
                if (FirstConnection.First_connection(TextBoxSetMasterPass.Password, TextBoxConfirmMasterPass.Password))
                {
                    LoadMainPage(TextBoxSetMasterPass.Password);
                }
            }
        }
        private void TextBoxMasterPass_KeyDown(object sender, KeyEventArgs e) //Event lorsqu'on appuie sur la touche entrée dans la fenêtre de connexion
        {
            if (e.Key == Key.Return)
            {
                if (TextBoxMasterPass.Password != "")
                {
                    if (PasswordVerifier.VerifyPassword(TextBoxMasterPass.Password))
                    {
                        LoadMainPage(TextBoxMasterPass.Password);
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
        }
        private void Click_SearchBar_KeyDown(object sender, KeyEventArgs e) //Event lorsqu'on appuie sur la touche entrée dans la barre de recherche
        {
            Disable_modification();
            if (e.Key == Key.Return)
            {
                SearchBar();
            }
        }
    }
}

