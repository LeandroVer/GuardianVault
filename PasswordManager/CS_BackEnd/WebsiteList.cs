using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;


namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        List<WebsiteItem> WebsiteList = new List<WebsiteItem>(); //Liste qui va contenir tous les détails des sites web
       
        public void PasswordExctractor() //Déchiffrage des mots de passe et inscription dans la liste des sites web
        {
            WebsiteList.Clear();
            string[] lines = System.IO.File.ReadAllLines(@"CS_BackEnd\\password_list_temp.txt"); //Temporaire : lecture des combos dans un txt
            for (int i = 0; i < 11; i++) //Boucle 11 fois, dans le futur selon le nombre de ligne de la database
            {
                WebsiteList.Add(new WebsiteItem() //Ajoute un WebsiteItem à la liste
                {
                    logo = "/Assets/img_temp.png",
                    nom = lines[6*i+0],
                    url = lines[6*i+1],
                    email = lines[6*i+2],
                    password = lines[6*i+3],
                    note = lines[6*i+4]
                });
            }
        }
    }
}
