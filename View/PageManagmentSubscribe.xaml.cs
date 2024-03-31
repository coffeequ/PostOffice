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

        int yearEnd = 0;

        public PageManagmentSubscribe(SubscriberOfThePostOffice subscriberOfThePostOffice)
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

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            subscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            lvSelectedPublication.Items.Clear();
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
            Subscribe subscribe;

            for (int i = 0; i < publicationsSelected.Count(); i++)
            {
                subscribe = new Subscribe();

                subscribe.Publication = publicationsSelected[i];

                subscribe.DateRegistration = DateTime.Now;

                subscribe.EntryTime = DateTime.Now;

                subscribe.StatusActive = 1;

                try
                {
                    if (string.IsNullOrWhiteSpace(yearEnd.ToString()))
                    {
                        throw new Exception($"Введите правильный месяц");
                    }
                    if (string.IsNullOrWhiteSpace(tbAdressDelivery.Text))
                    {
                        throw new Exception($"Введите адресс доставки");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                DateTime dateTime = new DateTime(int.Parse(tbYearEnd.Text), yearEnd, int.Parse(tbDayEnd.Text));

                subscribe.EndTime = dateTime;

                subscribe.id_Subscriber = subscriberOfThePostOffice.id_Subscriber;

                dataBasePostOffice.postOfficeEntities.Subscribe.Add(subscribe);

                dataBasePostOffice.postOfficeEntities.SaveChanges();
            }
            MessageBox.Show($"Успешно были добавлены {publicationsSelected.Count} публикации в подписку!");

            MessageBoxResult messageBoxResult = MessageBox.Show(
                "Вывести чек?",
                "Внимание!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

            }
            
        }

        private void tbMonthEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                yearEnd = int.Parse(tbMonthEnd.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $". Введите корректный месяц!");
                tbYearEnd.Clear();
            }

            if (yearEnd >= 13)
            {
                MessageBox.Show("Введите корректный месяц!");
                tbYearEnd.Clear();
            }
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
