using System;
using System.Windows;
using System.IO;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Page_principale(object sender, RoutedEventArgs e) //Navigation vers la page contenant la liste des mots de passe à gauche et l'ajout d'un nouveau mot de passe à droite
        {
            Visibility_principale();
        }

        public void Visibility_principale()
        {
            FenetrePremiereConnexion.Visibility = Visibility.Hidden;
            FenetreConnexion.Visibility = Visibility.Hidden;
            FenetrePrincipale.Visibility = Visibility.Visible;
            ColorSeparation.Visibility = Visibility.Visible;
            TextBoxSetMasterPass.Password = "";
            TextBoxConfirmMasterPass.Password = "";
            TextBoxMasterPass.Password = "";
            GridSecondaire.Visibility = Visibility.Visible;
            GridParametres.Visibility = Visibility.Hidden;
            CheckboxParametres.IsChecked = false;
            Disable_modification();
        }

        public void Visibility_parametres() //Navigation vers la page contenant la liste des mots de passe à gauche et les paramètres à droite
        {
            GridSecondaire.Visibility = Visibility.Hidden;
            GridParametres.Visibility = Visibility.Visible;
            Disable_modification();
        }


        private void Page_connexion(object sender, RoutedEventArgs e) //Navigation vers la page de connexion (mot de passe à entrer)
        {
            Visibility_connexion();
        }

        public void Visibility_connexion() 
        {
            FenetrePrincipale.Visibility = Visibility.Hidden;
            ColorSeparation.Visibility = Visibility.Hidden;
            FenetreConnexion.Visibility = Visibility.Visible;
            FenetrePremiereConnexion.Visibility = Visibility.Hidden;
            Disable_modification();
        }
        private void Page_premiere_connexion(object sender, RoutedEventArgs e) //Navigation vers la page de première connexion (mot de passe à entrer et à confirmer)
        {
            Visibility_premiere_connexion();
        }

        public void Visibility_premiere_connexion()
        {
            FenetrePrincipale.Visibility = Visibility.Hidden;
            ColorSeparation.Visibility = Visibility.Hidden;
            FenetrePremiereConnexion.Visibility = Visibility.Visible;
            Disable_modification();
        }
        
        public void HashFileExists()
        {
            string path = appDataFolder;
            path = Path.Combine(path, "GuardianVault", "hash.gv");

            if (File.Exists(path))
            {
                Visibility_connexion();
            }
            else
            {
                Visibility_premiere_connexion();
            }
        }
    }
}

