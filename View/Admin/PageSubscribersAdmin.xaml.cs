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

        List<SubscriberOfThePostOffice> sortSubscriberPostOffice;

        User user;

        public PageSubscribersAdmin(User user)
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

            this.user = user;

            subscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            subscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            dgSubscribers.ItemsSource = subscriberOfThePostOffices;

            sortSubscriberPostOffice = new List<SubscriberOfThePostOffice>();
        }

        private void Button_More(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            new WinMoreDetailsSubscriber(selectedItem, user).ShowDialog();

            dgSubscribers.ItemsSource = null;

            ApplySearch();

            ApplyDataPicker();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            SubscriberOfThePostOffice subscriberOfThePostOffice = new SubscriberOfThePostOffice();

            new WinMoreDetailsSubscriber(subscriberOfThePostOffice, user).ShowDialog();

            dgSubscribers.ItemsSource = null;

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            var activePublication = selectedItem.Subscribe.ToList().Where(item2 => item2.StatusActive == 1);

            if (activePublication.Count() <= 0)
            {
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
            else
            {
                MessageBox.Show("Нельзя удалить подписчика, так как у него есть активные подписки");
                return;
            }

        }

        private void ApplySearch()
        {
            sortSubscriberPostOffice = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.Where(item => item.Surname.StartsWith(tbSearch.Text) || item.Name.StartsWith(tbSearch.Text) || item.MiddleName.StartsWith(tbSearch.Text)).ToList();
        }

        private void ApplyDataPicker()
        {
            var temp = new List<SubscriberOfThePostOffice>();
            try
            {
                DateTime dateTime = (DateTime)tbBrithday.SelectedDate;
                temp = sortSubscriberPostOffice.Where(item => item.Birthday == dateTime).ToList();
            }
            catch (Exception)
            {
                temp = sortSubscriberPostOffice.ToList();
            }

            dgSubscribers.ItemsSource = temp;
        }

        private void TextChangedSearch(object sender, TextChangedEventArgs e)
        {
            ApplySearch();
            ApplyDataPicker();
        }

        private void tbBrithday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySearch();
            ApplyDataPicker();
        }
    }
}
