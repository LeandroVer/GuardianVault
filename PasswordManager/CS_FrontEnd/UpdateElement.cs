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

        private void Update_Details_WebSiteItem(int indice) //Met à jour les détails du site web cliqué au centre de l'écran
        {
            NomWebSiteItem.Content = WebsiteList[indice].nom;
            URLWebSiteItem.Content = WebsiteList[indice].url;
            EmailWebSiteItem.Content = WebsiteList[indice].email;
            PassordWebSiteItem.Content = WebsiteList[indice].password;
            NoteWebSiteItem.Text = WebsiteList[indice].note;
        }

        private void Update_DataGrid_Items() //Met à jour la liste des site web affiché
        {
            PasswordExctractor(); //Exctraction des mots de passe
            DataGridWebsiteList.ItemsSource = WebsiteList; //Associe la liste "WebsiteList" à la liste d'item de la datagrid
        }
    }
}
