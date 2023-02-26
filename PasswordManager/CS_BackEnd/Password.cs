using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class PasswordEntry
    {
        public string NomSite { get; set; }
        public string Identifiant { get; set; }
        public string URL { get; set; }
        public string MotDePasse { get; set; }
        public string Note { get; set; }

        public PasswordEntry(string nomSite, string identifiant, string url, string motDePasse, string note)
        {
            NomSite = nomSite;
            Identifiant = identifiant;
            URL = url;
            MotDePasse = motDePasse;
            Note = note;
        }
    }

}
