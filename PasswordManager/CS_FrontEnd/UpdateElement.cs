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
        private List<WebsiteItem> filteredList = null;
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
            string link_icon="!";

            if (filteredList != null && indice >= 0 && indice < filteredList.Count)
            {
                //Si il y a une recherche est en cours
                NomWebSiteItem.Content = filteredList.ElementAt(indice).nom;
                URLWebSiteItem.Text = filteredList.ElementAt(indice).url;
                EmailWebSiteItem.Text = filteredList.ElementAt(indice).email;
                PassordWebSiteItem.Text = filteredList.ElementAt(indice).password;
                NoteWebSiteItem.Text = filteredList.ElementAt(indice).note.Replace("§", "\n");
                Details_Column.Visibility = Visibility.Visible;

                link_icon = filteredList.ElementAt(indice).url_logo;
            }
            else
            {
                //S'il n'y a pas de recherche en cours
                if (WebsiteList == null || WebsiteList.Count == 0 || indice < 0 || indice >= WebsiteList.Count)
                {
                    // Si la base de données est vide, on affiche des champs vides
                    NomWebSiteItem.Content = "";
                    URLWebSiteItem.Text = "";
                    EmailWebSiteItem.Text = "";
                    PassordWebSiteItem.Text = "";
                    NoteWebSiteItem.Text = "";
                    Details_Column.Visibility = Visibility.Hidden;

                    link_icon = "/Assets/icon_placeholder.png";
                }
                else
                {
                    // Sinon, on affiche les détails des sites web correspondant à l'indice
                    NomWebSiteItem.Content = WebsiteList[indice].nom;
                    URLWebSiteItem.Text = WebsiteList[indice].url;
                    EmailWebSiteItem.Text = WebsiteList[indice].email;
                    PassordWebSiteItem.Text = WebsiteList[indice].password;
                    NoteWebSiteItem.Text = WebsiteList[indice].note.Replace("§", "\n"); 
                    Details_Column.Visibility = Visibility.Visible;

                    link_icon = WebsiteList[indice].url_logo; 
                }
            }
            
            // Affichage de l'icon du site web
            var image = new Image();
            BitmapImage bitmap;
            if (link_icon == "/Assets/icon_placeholder.png") //Pas un  lien URL juste un lien relatif
            {
                bitmap = new BitmapImage(new Uri(link_icon, UriKind.Relative));
            }
            else //Lien URL
            {
                bitmap = new BitmapImage(new Uri(link_icon, UriKind.Absolute));

            }
            LogoWebSiteItem.Source = bitmap;
        }


        private void Update_DataGrid_Items() //Met à jour la liste des site web affiché
        {
            PasswordExtractor(); //Extraction des mots de passe
            DataGridWebsiteList.ItemsSource = null; //Dissocie la liste d'item de la datagrid
            DataGridWebsiteList.ItemsSource = WebsiteList; //Associe la liste "WebsiteList" à la liste d'item de la datagrid
        }

        //SearchBar
        public void SearchBar()
        {
            ResetAutoLockTimer();
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

        private void DeleteFilteredList()
        {
            TextBox_SearchBar.Text = "";
            filteredList = null!;
        }
        
        
    }
}
