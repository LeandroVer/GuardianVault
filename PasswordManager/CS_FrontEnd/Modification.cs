using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls; //Ne pas supprimer


namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public void Enable_modification()
        {
            EmailWebSiteItem.IsReadOnly = false;
            URLWebSiteItem.IsReadOnly = false;
            PassordWebSiteItem.IsReadOnly = false;
            NoteWebSiteItem.IsReadOnly = false;
            BoutonCopierId.Visibility = Visibility.Hidden;
            BoutonOuvrirURL.Visibility = Visibility.Hidden;
            BoutonCopierMdp.Visibility = Visibility.Hidden;
            BoutonModifier.Content = "Sauvegarder";
        }

        public void Disable_modification()
        {
            EmailWebSiteItem.IsReadOnly = true;
            URLWebSiteItem.IsReadOnly = true;
            PassordWebSiteItem.IsReadOnly = true;
            NoteWebSiteItem.IsReadOnly = true;
            BoutonCopierId.Visibility = Visibility.Visible;
            BoutonOuvrirURL.Visibility = Visibility.Visible;
            BoutonCopierMdp.Visibility = Visibility.Visible;
            Update_Details_WebSiteItem(DataGridWebsiteList.SelectedIndex);
            BoutonModifier.Content = "Modifier";
        }

        public void Save_modification()
        {
            //Ici il faut mettre l'appel de la fonction qui va remplacer les nouvelles infos modifiés
            Disable_modification();
        }
    }
}