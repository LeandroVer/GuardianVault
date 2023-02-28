using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private void Update_Slider_Longueur(object sender, RoutedEventArgs e) //Ajuster la valeur du slider de la longueur du mot de passe selon la valeur inscrite dans la TextBox
        {
            SliderLongueur.Value = Convert.ToDouble(TextBoxLongueur.Text);
        }

        private void Update_TextBox_Longueur(object sender, RoutedPropertyChangedEventArgs<double> e) //Ajuster la valeur inscrite dans la TextBox de la longueur du mot de passe selon la valeur du slider
        {
            TextBoxLongueur.Text = ((int) SliderLongueur.Value).ToString();
        }

        public void Update_Details_WebSiteItem(int indice)
        {
            if (filteredList != null)
            {

                NomWebSiteItem.Content = filteredList.ElementAt(indice).nom;
                URLWebSiteItem.Content = filteredList.ElementAt(indice).url;
                EmailWebSiteItem.Content = filteredList.ElementAt(indice).email;
                PassordWebSiteItem.Content = filteredList.ElementAt(indice).password;
                NoteWebSiteItem.Text = filteredList.ElementAt(indice).note;
                Details_Column.Visibility = Visibility.Visible;
            }
            else
            { 
                if (WebsiteList == null || WebsiteList.Count == 0 || indice < 0 || indice >= WebsiteList.Count)
                {
                    // Si la base de données est vide, on affiche des champs vides
                    NomWebSiteItem.Content = "";
                    URLWebSiteItem.Content = "";
                    EmailWebSiteItem.Content = "";
                    PassordWebSiteItem.Content = "";
                    NoteWebSiteItem.Text = "";
                    Details_Column.Visibility = Visibility.Hidden;
                }
                else
                {
                    // Sinon, on affiche les détails des sites web correspondant à l'indice
                    NomWebSiteItem.Content = WebsiteList[indice].nom;
                    URLWebSiteItem.Content = WebsiteList[indice].url;
                    EmailWebSiteItem.Content = WebsiteList[indice].email;
                    PassordWebSiteItem.Content = WebsiteList[indice].password;
                    NoteWebSiteItem.Text = WebsiteList[indice].note;
                    Details_Column.Visibility = Visibility.Visible;
                }
            }
        }


        private void Update_DataGrid_Items() //Met à jour la liste des site web affiché
        {
            PasswordExtractor(); //Extraction des mots de passe
            DataGridWebsiteList.ItemsSource = null; //Dissocie la liste d'item de la datagrid
            DataGridWebsiteList.ItemsSource = WebsiteList; //Associe la liste "WebsiteList" à la liste d'item de la datagrid
        }
    }
}
