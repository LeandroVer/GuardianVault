using System.Windows;

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
    }
}
