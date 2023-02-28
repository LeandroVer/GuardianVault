using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public List<WebsiteItem> filteredList = null;
        private void Click_SearchBar(object sender, RoutedEventArgs e)
        {
            string searchString = TextBox_SearchBar.Text.ToLower();

            if (string.IsNullOrEmpty(searchString))
            {
                // Si la recherche est vide, afficher tous les éléments
                filteredList = null!;
                DataGridWebsiteList.ItemsSource = WebsiteList;
            }
            else
            {
                // Sinon, filtrer la liste en supprimant tous les éléments qui ne correspondent pas à la recherche
                filteredList = WebsiteList.Where(item =>
                    (item.nom.ToLower().Contains(searchString))).ToList();

                // supprimer tous les éléments de la DataGrid sauf ceux ayant une correspondance avec la recherche effectuée
                DataGridWebsiteList.ItemsSource = filteredList;
                if (filteredList.Any())
                {
                    int index = WebsiteList.IndexOf(filteredList.First());
                    DataGridWebsiteList.SelectedItem = DataGridWebsiteList.Items[0];
                    DataGridWebsiteList.ScrollIntoView(DataGridWebsiteList.Items[0]);
                    Update_Details_WebSiteItem(index);

                }
            }
        }



    }
}
