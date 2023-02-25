using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MinHeight = MinWidth / (1920 / 985.0);
            SizeChanged += MainWindow_SizeChanged;
            HashFileExists();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) //Conserver le bon ratio de l'écran
        {
            if ((e.NewSize.Width / e.NewSize.Height) != (985 / 1920.0))
            {
                Height = Width / (1920 / 985.0);
            }
            else
            {
                Width = Height * (1920 / 985.0);
            }
        }
        //Les autres parties du code C# sont dans des fichiers séparés (CS_FrontEnd et CS_BackEnd)
        //Les fichiers qui doivent intéragir avec l'interface graphique (ex : ButtonClick) sont dans des fichiers séparés mais dans appartiennent à cette même classe (partial MainWindow)
    }
}
