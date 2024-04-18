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

namespace PostOffice.View
{
    /// <summary>
    /// Логика взаимодействия для WinAboutUser.xaml
    /// </summary>
    public partial class WinAboutUser : Window
    {
        public WinAboutUser(User user)
        {
            InitializeComponent();

            framePage.NavigationService.Navigate(new View.PageAboutUser(user));

            View.PageAboutUser.closeWin += PageAboutUser_closeWin;
        }

        private void PageAboutUser_closeWin()
        {
            Close();
        }
    }
}
