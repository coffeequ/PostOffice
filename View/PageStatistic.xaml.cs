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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostOffice.View
{
    /// <summary>
    /// Логика взаимодействия для PageStatistic.xaml
    /// </summary>
    public partial class PageStatistic : Page
    {
        public delegate void closeWin();

        public static event closeWin closewin;

        public PageStatistic(User user)
        {
            InitializeComponent();

            lbLogin.Content = user.Login;
        }

        private void ButtonSubsribesStatic(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.PageSubscribeStatistic());
        }

        private void ButtonCorrespondenceStatic(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.PageCorrespondenceStatistic());
        }

        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            closewin();
        }

        private void btnEntrance(object sender, RoutedEventArgs e)
        {
  
        }
    }
}
