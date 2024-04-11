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

        public PageWachPublicationAdmin(User user)
        {
            InitializeComponent();

            this.user = user;

            lbLogin.Content = user.Login;
        }

        private void btnEntrance(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выход или редактириование личных данных");
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
            new View.WinStatistic(user).Show();
        }

        private void ButtonTypePublication(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.PageViewPublication());
        }

        private void ButtonTypeViewPublication(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonLoginIO(object sender, RoutedEventArgs e)
        {

        }

    }
}
