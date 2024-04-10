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
    /// Логика взаимодействия для PageSubscribersAdmin.xaml
    /// </summary>
    public partial class PageSubscribersAdmin : Page
    {
        List<SubscriberOfThePostOffice> subscriberOfThePostOffices;

        List<Subscribe> subscribes;

        Model.DataBasePostOffice dataBasePostOffice;

        public PageSubscribersAdmin()
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

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            SubscriberOfThePostOffice subscriberOfThePostOffice = new SubscriberOfThePostOffice();

            new WinMoreDetailsSubscriber(subscriberOfThePostOffice).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            if (selectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вы точно хотите удалить запись",
                    "Внимание!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.Remove(selectedItem);

                    var allSubs = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

                    var allActiveSub = new List<Subscribe>();

                    for (int i = 0; i < allSubs.Count(); i++)
                    {
                        if (allSubs[i].id_Subscriber == selectedItem.id_Subscriber)
                        {
                            allActiveSub.Add(allSubs[i]);
                            dataBasePostOffice.postOfficeEntities.Subscribe.Remove(allSubs[i]);
                        }
                    }

                    for (int i = 0; i < allActiveSub.Count(); i++)
                    {
                        allActiveSub[i].Correspondence.Clear();
                    }

                    dataBasePostOffice.postOfficeEntities.SaveChanges();

                    MessageBox.Show("Пользователь удален!");

                    dgSubscribers.ItemsSource = null;

                    dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
                }
            }
        }
    }
}
