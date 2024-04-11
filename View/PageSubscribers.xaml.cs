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
    /// Логика взаимодействия для PageSubscribers.xaml
    /// </summary>
    public partial class PageSubscribers : Page
    {
        List<SubscriberOfThePostOffice> subscriberOfThePostOffices;

        List<Subscribe> subscribes;

        Model.DataBasePostOffice dataBasePostOffice;

        User user;

        public PageSubscribers(User user)
        {
            InitializeComponent();

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            subscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            subscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            dgSubscribers.ItemsSource = subscriberOfThePostOffices;

            this.user = user;
        }

        private void Button_More(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            new WinMoreDetailsSubscriber(selectedItem, null).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = subscriberOfThePostOffices;

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            SubscriberOfThePostOffice subscriberOfThePostOffice = new SubscriberOfThePostOffice();

            new WinMoreDetailsSubscriber(subscriberOfThePostOffice, user).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
        }
    }
}
