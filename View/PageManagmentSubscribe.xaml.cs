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
    /// Логика взаимодействия для PageManagmentSubscribe.xaml
    /// </summary>
    public partial class PageManagmentSubscribe : Page
    {
        SubscriberOfThePostOffice subscriberOfThePostOffice;

        Model.DataBasePostOffice dataBasePostOffice;

        List<Publication> publicationsSelected;

        List<Publication> activePublication;

        List<Subscribe> subscribes;

        User user;

        Random rnd;

        int yearEnd = 0;

        public PageManagmentSubscribe(SubscriberOfThePostOffice subscriberOfThePostOffice, User user)
        {
            InitializeComponent();

            this.subscriberOfThePostOffice = subscriberOfThePostOffice;

            publicationsSelected = new List<Publication>();

            activePublication = new List<Publication>();

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.user = user;

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            subscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            lvSelectedPublication.Items.Clear();

            rnd = new Random();
        }

        private void Grid_LoadedMonth(object sender, RoutedEventArgs e)
        {
            tbDayStart.IsEnabled = false;

            tbDayStart.Text = DateTime.Now.Day.ToString();
            
            tbMonthStart.IsEnabled = false;
            
            tbMonthStart.Text = DateTime.Now.Month.ToString();
            
            tbYearStart.IsEnabled = false;
            
            tbYearStart.Text = DateTime.Now.Year.ToString();

            tbDayEnd.IsEnabled = false;

            tbDayEnd.Text = DateTime.Now.Day.ToString();

            tbYearEnd.IsEnabled = false;

            tbYearEnd.Text = DateTime.Now.Year.ToString();

            for (int i = 0; i < subscribes.Count(); i++)
            {
                if (subscribes[i].id_Subscriber == subscriberOfThePostOffice.id_Subscriber)
                {
                    if (subscribes[i].StatusActive == 1)
                    {
                        activePublication.Add(subscribes[i].Publication);
                    }
                }
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (publicationsSelected.Count > 0)
            {
                try
                {
                    yearEnd = int.Parse(tbMonthEnd.Text);

                    if (string.IsNullOrWhiteSpace(yearEnd.ToString()))
                    {
                        throw new Exception($"Введите правильный месяц");
                    }

                    if (string.IsNullOrWhiteSpace(tbAdressDelivery.Text))
                    {
                        throw new Exception($"Введите адресс доставки");
                    }

                    if (int.Parse(tbMonthEnd.Text) < int.Parse(tbMonthStart.Text))
                    {
                        throw new Exception($"Дата окончания не может быть меньше чем дата начала");
                    }

                    if (yearEnd >= 13)
                    {
                       throw new Exception("Введите корректный месяц!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                Subscribe subscribe;

                int numberSubscribeGeneration = GetHashCode();

                for (int i = 0; i < publicationsSelected.Count(); i++)
                {
                    subscribe = new Subscribe();

                    subscribe.Publication = publicationsSelected[i];

                    subscribe.DateRegistration = DateTime.Now;

                    subscribe.EntryTime = DateTime.Now;

                    subscribe.StatusActive = 1;

                    DateTime dateTime = new DateTime(int.Parse(tbYearEnd.Text), yearEnd, int.Parse(tbDayEnd.Text));

                    subscribe.EndTime = dateTime;

                    subscribe.id_Subscriber = subscriberOfThePostOffice.id_Subscriber;

                    subscribe.NumberSubscribe = numberSubscribeGeneration.ToString();

                    dataBasePostOffice.postOfficeEntities.Subscribe.Add(subscribe);

                    dataBasePostOffice.postOfficeEntities.SaveChanges();
                }

                MessageBox.Show($"Успешно были добавлены {publicationsSelected.Count} публикаций в подписку!");

                MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вывести чек?",
                    "Внимание!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                }
                NavigationService.Navigate(new View.PageMoreDetailsSubscriber(subscriberOfThePostOffice, user));
            }
            else
            {
                MessageBox.Show("Выберите подписки");
            }
        }

        private void tbMonthEnd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgPublication_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as DataGrid;
            var itemSelected = item.SelectedItem as Publication;

            try
            {
                if (publicationsSelected.Contains(itemSelected))
                {
                    throw new Exception ($"Издание {itemSelected.Name} уже находится в списке добавленных");
                }

                if (activePublication.Contains(itemSelected))
                {
                    throw new Exception($"Подписка на издание: {itemSelected.Name}, уже активна");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show($"Была успешно добавлена публикация");

            publicationsSelected.Add(itemSelected);

            lvSelectedPublication.ItemsSource = publicationsSelected;

            lvSelectedPublication.Items.Refresh();
        }

        private void button_delete(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as Publication;

            publicationsSelected.Remove(selectedItem);

            lvSelectedPublication.ItemsSource = publicationsSelected;

            lvSelectedPublication.Items.Refresh();
        }
    }
}
