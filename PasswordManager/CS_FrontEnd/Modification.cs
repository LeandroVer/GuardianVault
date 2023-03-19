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
            BoutonModifier.Content = "Sauvegarder";
        }

        public void Disable_modification()
        {
            EmailWebSiteItem.IsReadOnly = true;
            URLWebSiteItem.IsReadOnly = true;
            PassordWebSiteItem.IsReadOnly = true;
            NoteWebSiteItem.IsReadOnly = true;
            BoutonModifier.Content = "Modifier";
        }

        public void Save_modification()
        {
            Disable_modification();
        }
    }
}