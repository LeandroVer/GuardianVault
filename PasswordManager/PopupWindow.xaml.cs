using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PasswordManager
{
    public partial class PopupWindow : Window
    {
        public PopupWindow(string message)
        {
            InitializeComponent();
            TextBlockMessage.Content = message;
            KeyDown += pop_up_KeyDown;
        }

        private void Click_pop_up(object sender, RoutedEventArgs e)
        {
            Close_pop_up();
        }

        private void pop_up_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                Close_pop_up(); 
            }
        }
        
        private void Close_pop_up()
        {
            this.DialogResult = true;
        }
    }
}
