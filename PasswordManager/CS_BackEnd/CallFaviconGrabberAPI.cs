using System;
using System.Windows;
using System.IO;

//Important !!
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private string Grab_icon(string url)
        {
            string icon_url;
            string url_brute = url;
            if (url_brute.EndsWith("/"))
            {
                url_brute = url_brute[..^1]; //Enlève le dernier caractère /
            }
            if ((url_brute != null) & (url_brute.EndsWith(".com") | url_brute.EndsWith(".fr") | url_brute.EndsWith(".org") | url_brute.EndsWith(".co") | url_brute.EndsWith(".net") | url_brute.EndsWith(".io") | url_brute.EndsWith(".eu") | url_brute.EndsWith(".info") | url_brute.EndsWith(".site") | url_brute.EndsWith(".tv")))
            {
                if (url_brute.StartsWith("http"))
                {
                    if (url_brute.StartsWith("https"))
                    {
                        icon_url = "https://favicongrabber.com/api/grab/" + url_brute[8..]; //Enlève https://
                    }
                    else
                    {
                        icon_url = "https://favicongrabber.com/api/grab/" + url_brute[7..]; //Enlève http://
                    }
                }
                else
                {
                    icon_url = "https://favicongrabber.com/api/grab/" + url_brute;
                }

                var client = new RestClient(icon_url);
                var request = new RestRequest();
                var response = client.Execute(request);

                if (response.IsSuccessStatusCode & response.Content != null)
                {
                    GroupIcons list_icons = JsonConvert.DeserializeObject<GroupIcons>(response.Content!)!;
                    int nombre_icons = list_icons.icons.Count;
                    if (nombre_icons > 0)
                    {
                        int indice_icon = nombre_icons - 1;


                        while (indice_icon > 0 & (list_icons.icons[indice_icon].type == "image/svg+xml" | list_icons.icons[indice_icon].type == "image/x-icon"))
                        { // Parcours descendant des icons donné par l'API jusqu'à trouver une icon qui n'est pas .sgv ou .icon 
                            indice_icon--;
                        } // Au pire l'indice arrive à zero et ça prendra un .icon après

                        return list_icons.icons[indice_icon].src;
                    }
                    else //Si le site n'a pas de favicon
                    {
                        return "NONE";
                    }
                }
                else //Si il y a un problème avec l'API
                {
                    return "NONE";
                }
            }
            else //Si l'URL n'est pas bon
            {
                return "NONE";
            }
        }

        public class GroupIcons
        {
            public string domain { get; set; }
            public List<Favicon> icons { get; set; }
        }

        public class Favicon
        {
            public string src { get; set; }
            public string type { get; set; }
            public string sizes { get; set; }
        }

        

    }
}
