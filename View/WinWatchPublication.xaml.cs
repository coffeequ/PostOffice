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
    /// Логика взаимодействия для WinWatchPublication.xaml
    /// </summary>
    public partial class WinWatchPublication : Window
    {
        User user;
        public WinWatchPublication(User user)
        {
            InitializeComponent();

            this.user = user;

            mainFrame.NavigationService.Navigate(new View.PageWatchPublicationOperator(user));
        }
    }
}
