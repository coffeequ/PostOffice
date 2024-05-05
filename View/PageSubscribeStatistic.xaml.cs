using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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

        List<SubscriberOfThePostOffice> subscriberOfThePostOffices;

        List<Subscribe> allSubscribes;

        //public SeriesCollection sc { get; set; }

        public PageSubscribeStatistic()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            subscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            allSubscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            ChartValues<int> vs = new ChartValues<int>();

            for (int i = 0; i < subscriberOfThePostOffices.Count; i++)
            {
                vs.Add(subscriberOfThePostOffices[i].CountSubscribe);
            }

            SeriesCollection seriesViews = new SeriesCollection();

            for (int i = 0; i < subscriberOfThePostOffices.Count; i++)
            {
                seriesViews.Add(new PieSeries()
                {
                    Title = $"{subscriberOfThePostOffices[i].Surname} {subscriberOfThePostOffices[i].Name} {subscriberOfThePostOffices[i].MiddleName}",
                    Values = new ChartValues<int> { vs[i] },
                    PushOut = 15,
                    DataLabels = true
                });
            }

            myChart.Series = seriesViews;

            myChart.LegendLocation = LegendLocation.Right;

            myChart.FontSize = 14;

            DataContext = this;
        }

        private void dgPublication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgPublication.SelectedItem as Publication;

            List<SubscriberOfThePostOffice> temp = new List<SubscriberOfThePostOffice>();

            for (int i = 0; i < subscriberOfThePostOffices.Count(); i++)
            {
                for (int j = 0; j < subscriberOfThePostOffices[i].Subscribe.Count(); j++)
                {
                    if (subscriberOfThePostOffices[i].Subscribe.ToList()[j].id_Publication == item.id_Publication)
                    {
                        temp.Add(subscriberOfThePostOffices[i]);
                    }
                }
            }

            dgSubscribers.ItemsSource = temp;
        }

        private void Button_statisticToExcel(object sender, RoutedEventArgs e)
        {
            Model.IOExcel iOExcel = new Model.IOExcel(dataBasePostOffice);
            iOExcel.DBToExcelTablePubAndSub();
        }
    }
}
