using System;
using System.IO;
using System.Windows;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public void Delete_account() //Event du bouton "Supprimer le compte"
        {
            StopAutoLockTimer();
            DeleteDatafileEnc();
            DeleteDatafile();
            DeleteHash();
            Visibility_premiere_connexion();
        }

        public void Edit_master_password() ///Event du bouton "Modifier le mot de passe maître"
        {
            StopAutoLockTimer();
            DatafileEncryption.DecryptFile();
            DeleteHash();
            Visibility_premiere_connexion();
        }

        public void Exporter_coffre()
        {
            ResetAutoLockTimer();
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
                    string appDataPath = appDataFolder;
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

        public void Importer_coffre()
        {
            ResetAutoLockTimer();
            DeleteFilteredList();
            // Création de la boîte de dialogue
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // Configuration des options de la boîte de dialogue
            openFileDialog.Title = "Importer un fichier";
            openFileDialog.Filter = "GuardianVault files (.gv)|*.gv";

            // Affichage de la boîte de dialogue et récupération du résultat
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Récupération du chemin du fichier sélectionné
            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                string appDataPath = appDataFolder;
                string destinationFilePath = Path.Combine(appDataPath, "GuardianVault", "datafile.gv");
                DeleteDatafile();
                File.Copy(filePath, destinationFilePath, true);
                DatafileEncryption.EncryptFile();
                DatagridDisplay();
                DeleteDatafile();
            }

        }



    }
}
