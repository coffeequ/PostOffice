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
    /// Логика взаимодействия для PageSubscribeStatistic.xaml
    /// </summary>
    public partial class PageSubscribeStatistic : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        List<Subscribe> subscribes;

        List<Subscribe> corrSubs;

        public PageSubscribeStatistic()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            subscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();
        }

        private void dgPublication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgPublication.SelectedItem as Publication;

            corrSubs = new List<Subscribe>();

            for (int i = 0; i < subscribes.Count(); i++)
            {
                if (item.id_Publication == subscribes[i].id_Publication)
                {
                    corrSubs.Add(subscribes[i]);
                }
            }
            dgSubscribers.ItemsSource = corrSubs;
        }

        private void Button_statisticToExcel(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
