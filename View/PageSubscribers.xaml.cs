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

            subscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            dgSubscribers.ItemsSource = subscriberOfThePostOffices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as SubscriberOfThePostOffice;

            new WinMoreDetailsSubscriber(selectedItem).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
