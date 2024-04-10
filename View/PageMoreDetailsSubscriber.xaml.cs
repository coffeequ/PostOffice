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
    /// Логика взаимодействия для PageMoreDetailsSubscriber.xaml
    /// </summary>
    public partial class PageMoreDetailsSubscriber : Page
    {
        SubscriberOfThePostOffice subscriberOfThePostOffice;

        List<Subscribe> allSubscribes;

        Model.DataBasePostOffice dataBasePostOffice;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public PageMoreDetailsSubscriber(SubscriberOfThePostOffice subscriberOfThePostOffice)
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

            this.subscriberOfThePostOffice = subscriberOfThePostOffice;

            DataContext = subscriberOfThePostOffice;

            allSubscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            var publicationSubscriber = dataBasePostOffice.postOfficeEntities.Subscribe.Where(persona => persona.id_Subscriber == subscriberOfThePostOffice.id_Subscriber).ToList();

            dgSubsriberPublication.ItemsSource = publicationSubscriber;
        }

        private void Button_Add_Publication(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.PageManagmentSubscribe(subscriberOfThePostOffice));
        }

        private void Button_saveData(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Данные сохранены");
            closeWin();
        }

        private void Button_wordCheck(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вывод чека об оплате");
        }

        private void Button_Delete_Publication(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            
            var selectedItem = item.DataContext as Subscribe;

            if (selectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вы точно хотите удалить запись",
                    "Внимание!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    dataBasePostOffice.postOfficeEntities.Subscribe.Remove(selectedItem);
                    dataBasePostOffice.postOfficeEntities.SaveChanges();
                    MessageBox.Show("Запись удалена!");

                    dgSubsriberPublication.ItemsSource = null;

                    var publicationSubscriber = dataBasePostOffice.postOfficeEntities.Subscribe.Where(persona => persona.id_Subscriber == subscriberOfThePostOffice.id_Subscriber).ToList();

                    dgSubsriberPublication.ItemsSource = publicationSubscriber;
                }
            }
        }
    }
}
