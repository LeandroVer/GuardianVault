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
using System.Windows.Input;
using System.Security.Policy;

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
            Update_Details_WebSiteItem(DataGridWebsiteList.SelectedIndex); //Met à jour les détails du site web cliqué au centre de l'écran
        }
        public void DatafileDisplay(object sender, RoutedEventArgs e)
        {
            Update_DataGrid_Items(); //Ajout de la liste des site web dans la data grid
            DataGridWebsiteList.SelectedIndex = 0;
            Update_Details_WebSiteItem(0);
        }

        private void Click_SearchBar(object sender, RoutedEventArgs e) //Event lorsqu'on clique sur le bouton de recherche
        {
            SearchBar();
        }

        private void Click_AddPwd(object sender, RoutedEventArgs e)//Event lors de l'ajout d'un mot de passe
        {
            AddPwd(Add_NomSite.Text, Add_ID.Text, Add_URL.Text, Add_MDP.Text);
        }
        private void Click_Supprimer(object sender, RoutedEventArgs e) //Event du bouton Supprimer
        {
            Supprimer();
        }
        private void Click_Generate(object sender, RoutedEventArgs e)
        {
            Generate(int.Parse(TextBoxLongueur.Text), Checkbox_Minuscule.IsChecked == true, Checkbox_Majuscule.IsChecked == true, Checkbox_Nombre.IsChecked == true, Checkbox_Symbole.IsChecked == true);
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
            if (e.Key == Key.Return)
            {
                SearchBar();
            }
        }
    }
}

