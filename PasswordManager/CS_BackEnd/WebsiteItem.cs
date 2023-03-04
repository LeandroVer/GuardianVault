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
        public string logo { get; set; }
        public string nom { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string note { get; set; }

        public WebsiteItem()
        {
            logo = "";
            nom = "";
            url = "";
            email = "";
            password = "";
            note = "";
        }
        public WebsiteItem(string path, string name, string link, string mail, string pass, string text)
        {
            logo = path;
            nom = name;
            url = link;
            email = mail;
            password = pass;
            note = text;
        }
        public string ConvertWebsiteItemToString(WebsiteItem websiteItem)
        {
            string websiteInfo = $"{websiteItem.nom}|{websiteItem.email}|{websiteItem.url}|{websiteItem.password}|{websiteItem.note}";
            return websiteInfo;
        }
    }
}
