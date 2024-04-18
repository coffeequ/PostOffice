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

namespace PostOffice.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageWachPublicationAdmin.xaml
    /// </summary>
    public partial class PageWachPublicationAdmin : Page
    {
        User user;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public PageWachPublicationAdmin(User user)
        {
            InitializeComponent();

            this.user = user;

            lbLogin.Content = user.Login;

            View.PageAboutUser.closeWin += PageAboutUser_closeWin;
        }

        private void PageAboutUser_closeWin()
        {
            closeWin();
        }

        private void btnEntrance(object sender, RoutedEventArgs e)
        {
            new WinAboutUser(user).ShowDialog();
        }

        private void ButtonSubsribes(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.Admin.PageSubscribersAdmin(user));
        }

        private void ButtonPublication(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.Admin.PagePublicationAdmin());
        }

        private void ButtonWatchStatis(object sender, RoutedEventArgs e)
        {
            new View.WinStatistic(user).ShowDialog();
        }

        private void ButtonTypePublication(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.Admin.PageTypePublication());
        }

        private void ButtonTypeViewPublication(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.Admin.PageTypeViewPublication());
        }

        private void ButtonLoginIO(object sender, RoutedEventArgs e)
        {

        }

    }
}
