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

        Model.DataBasePostOffice dataBasePostOffice;

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
        }

        private void Button_wordCheck(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вывод чека об оплате");
        }
    }
}
