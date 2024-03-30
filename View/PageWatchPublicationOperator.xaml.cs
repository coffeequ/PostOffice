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
    /// Логика взаимодействия для PageWatchPublicationOperator.xaml
    /// </summary>
    public partial class PageWatchPublicationOperator : Page
    {
        User user;

        Model.DataBasePostOffice dataBasePostOffice;

        public PageWatchPublicationOperator(User user)
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            this.user = user;
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            lbLogin.Content = user.Login;
            Image image = new Image()
            { 
                Height = 40, 
                Width = 40
            };

            spLogin.Children.Add(image);
        }

        private void btnEntrance(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выход или редактириование личных данных");
        }

        private void btnWatch(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as Publication;

            WinWatchAndEditPublication win = new WinWatchAndEditPublication(selectedItem);

            win.Show();
        }

        private void ButtonSubsribes(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.PageSubscribers());
        }

        private void ButtonPublication(object sender, RoutedEventArgs e)
        {
            frameToAction.NavigationService.Navigate(new View.PagePublication());
        }
    }
}
