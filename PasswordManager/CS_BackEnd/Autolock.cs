using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer autoLockTimer = new DispatcherTimer();
        private bool isLocked = false;
        public void StartAutoLockTimer()
        {
            // Récupérer la durée d'inactivité avant le verrouillage automatique sélectionnée dans la liste déroulante
            isLocked = false;
            int autoLockDelaySeconds = RecoverInactivityTime();
            if (autoLockDelaySeconds == 0)
            {
                autoLockTimer.IsEnabled = false;
                return;
            }

            // Créer un nouveau DispatcherTimer avec une intervalle de temps de la durée d'inactivité avant le verrouillage automatique
            autoLockTimer.Interval = TimeSpan.FromSeconds(autoLockDelaySeconds);
            autoLockTimer.Tick += AutoLockTimer_Tick!;

            // Démarrer le DispatcherTimer
            autoLockTimer.IsEnabled = true;
        }

        public void ResetAutoLockTimer()
        {
            // Arrêter et redémarrer le DispatcherTimer pour réinitialiser le compte à rebours
            if (autoLockTimer.IsEnabled)
            {
                autoLockTimer.IsEnabled = false;
                autoLockTimer.IsEnabled = true;
            }
        }

        public void StopAutoLockTimer()
        {
            // Arrêter le DispatcherTimer
            autoLockTimer.IsEnabled = false;
        }

        public void AutoLockTimer_Tick(object sender, EventArgs e)
        {
            // Verrouiller l'application en utilisant la fonction de déconnexion
            if (!isLocked) {
                Visibility_connexion();
                StopAutoLockTimer();
                Open_pop_up("Vous avez été déconnecté pour cause d'inactivité");
                isLocked = true;
            }
        }

        public int RecoverInactivityTime()
        {
            // Récupérer la durée d'inactivité avant le verrouillage automatique sélectionnée dans la liste déroulante
            ComboBoxItem selectedItem = (ComboBoxItem)DDL_Autolock.SelectedItem;

            int autoLockDelaySeconds = 0;
            if (selectedItem != null)
            {
                
                switch (selectedItem.Content.ToString())
                {
                    case "Jamais":
                        return 0;
                    case "10 sec.":
                        autoLockDelaySeconds = 10; // 10 sec
                        break;
                    case "5 min.":
                        autoLockDelaySeconds = 5 * 60; // 5 minutes
                        break;
                    case "15 min.":
                        autoLockDelaySeconds = 15 * 60; // 15 minutes
                        break;
                    case "1 heure":
                        autoLockDelaySeconds = 60 * 60; // 1 heure
                        break;
                }
            }
            else
            {
                return 0;
            }

            return autoLockDelaySeconds;
        }
        private void DDL_Autolock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vérifier que autoLockTimer n'est pas null
            if (autoLockTimer != null)
            {
                // Arrêter le DispatcherTimer pour pouvoir le redémarrer avec la nouvelle durée d'inactivité sélectionnée
                autoLockTimer.Stop();
            }

            // Redémarrer le DispatcherTimer avec la nouvelle durée de timer
            StartAutoLockTimer();
        }


    }
}
