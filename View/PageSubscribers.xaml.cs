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

        public PageSubscribers()
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
        }

        private void Button_More(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            new WinMoreDetailsSubscriber(selectedItem).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = subscriberOfThePostOffices;

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
               "Удалить выбранную запись??",
               "Внимание!",
               MessageBoxButton.YesNo,
               MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var item = sender as Button;

                var selectedItem = item.DataContext as SubscriberOfThePostOffice;

                dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.Remove(selectedItem);

                List<Subscribe> allActiveSubs = new List<Subscribe>();

                for (int i = 0; i < subscribes.Count(); i++)
                {
                    if (selectedItem.id_Subscriber == subscribes[i].id_Subscriber)
                    {
                        allActiveSubs.Add(subscribes[i]);
                    }
                }

                for (int i = 0; i < allActiveSubs.Count(); i++)
                {
                    dataBasePostOffice.postOfficeEntities.Subscribe.Remove(allActiveSubs[i]);
                }

                dataBasePostOffice.postOfficeEntities.SaveChanges();

                dgSubscribers.ItemsSource = null;

                dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            SubscriberOfThePostOffice subscriberOfThePostOffice = new SubscriberOfThePostOffice();

            new WinMoreDetailsSubscriber(subscriberOfThePostOffice).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
        }
    }
}
