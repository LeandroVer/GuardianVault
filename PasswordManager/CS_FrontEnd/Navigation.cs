﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Page_principale(object sender, RoutedEventArgs e) //Navigation vers la page contenant la liste des mots de passe à gauche et l'ajout d'un nouveau mot de passe à droite
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
        }

        private void Page_parametres(object sender, RoutedEventArgs e) //Navigation vers la page contenant la liste des mots de passe à gauche et les paramètres à droite
        {
            GridSecondaire.Visibility = Visibility.Hidden;
            GridParametres.Visibility = Visibility.Visible;
        }


        private void Page_connexion(object sender, RoutedEventArgs e) //Navigation vers la page de connexion (mot de passe à entrer)
        {
            FenetrePrincipale.Visibility = Visibility.Hidden;
            ColorSeparation.Visibility = Visibility.Hidden;
            FenetreConnexion.Visibility = Visibility.Visible;
            FenetrePremiereConnexion.Visibility = Visibility.Hidden;
        }

        private void Page_premiere_connexion(object sender, RoutedEventArgs e) //Navigation vers la page de première connexion (mot de passe à entrer et à confirmer)
        {
            FenetrePrincipale.Visibility = Visibility.Hidden;
            ColorSeparation.Visibility = Visibility.Hidden;
            FenetrePremiereConnexion.Visibility = Visibility.Visible;
        }
        public void HashFileExists()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "GuardianVault", "hash.gv");

            if (File.Exists(path))
            {
                Page_connexion(this, new RoutedEventArgs());
            }
            else
            {
                Page_premiere_connexion(this, new RoutedEventArgs());
            }
        }
    }
}

