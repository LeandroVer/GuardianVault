using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager
{
    public class WebsiteItem //Classe représentant tous les détails d'un site web ajouté par l'utilisateur
    {
        public string url_logo { get; set; }
        public string nom { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string note { get; set; }

        public WebsiteItem()
        {
            url_logo = "";
            nom = "";
            url = "";
            email = "";
            password = "";
            note = "";
        }
        public WebsiteItem(string path, string name, string link, string mail, string pass, string text)
        {
            url_logo = path;
            nom = name;
            url = link;
            email = mail;
            password = pass;
            note = text;
        }
        public string ConvertWebsiteItemToString(WebsiteItem websiteItem)
        {
            string websiteInfo = $"{websiteItem.url_logo}|{websiteItem.nom}|{websiteItem.url}|{websiteItem.email}|{websiteItem.password}|{websiteItem.note}";
            return websiteInfo;
        }
    }
}
