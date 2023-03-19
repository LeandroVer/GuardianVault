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
            NoteWebSiteItem.IsReadOnly = false;
            BoutonModifier.Content = "Sauvegarder";
        }

        public void Disable_modification()
        {
            NoteWebSiteItem.IsReadOnly = true;
            BoutonModifier.Content = "Modifier";
        }

        public void Save_modification()
        {
            Disable_modification();
        }
    }
}