using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Click_deconnexion(object sender, RoutedEventArgs e) //Event du bouton "Se déconnecter"
        {
            Page_connexion(sender, e);
        }

        private void Click_delete_account(object sender, RoutedEventArgs e) //Event du bouton "Supprimer le compte"
        {
            DeleteDatafileEnc();
            DeleteDatafile();
            DeleteHash();
            Page_premiere_connexion(sender, e);
        }

        private void Click_edit_master_password(object sender, RoutedEventArgs e) ///Event du bouton "Modifier le mot de passe maître"
        {
            DatafileEncryption.DecryptFile();
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

        private void Click_exporter_coffre(object sender, EventArgs e)
        {
            DatafileEncryption.DecryptFile();
            // Afficher la boîte de dialogue de sauvegarde de fichier
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "datafile_export"; // Nom de fichier par défaut
            dlg.DefaultExt = ".gv"; // Extension de fichier par défaut
            dlg.Filter = "GuardianVault files (.gv)|*.gv"; // Filtre de fichiers
            Nullable<bool> result = dlg.ShowDialog();

            // Si l'utilisateur a sélectionné un emplacement de sauvegarde
            if (result == true)
            {
                try
                {
                    // Copier le fichier de données dans l'emplacement spécifié
                    string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string sourceFilePath = Path.Combine(appDataPath, "GuardianVault", "datafile.gv");
                    string destinationFilePath = dlg.FileName;
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    
                }
                catch (Exception ex)
                {
                    // Afficher un message d'erreur
                    MessageBox.Show("Une erreur s'est produite lors de l'enregistrement du fichier : " + ex.Message);
                }
            }
            DeleteDatafile();
        }

        
        

    }
}
