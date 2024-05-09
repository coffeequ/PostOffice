using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        List<Publication> allPublication;

        public SeriesCollection seriesViews2 { get; set; }

        public Func<double, string> Formatter { get; set; }

        public PageSubscribeStatistic()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            dgSubscribers.ItemsSource = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            subscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
 
            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            allSubscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            ChartValues<decimal> vs = new ChartValues<decimal>();

            decimal resultPrice = 0;

            decimal allPrice = 0;

            for (int i = 0; i < allPublication.Count; i++)
            {
                resultPrice = 0;

                for (int j = 0; j < allPublication[i].Subscribe.Count(); j++)
                {
                    if (allPublication[i].Subscribe.ToList()[j].EntryTime.Year == DateTime.Now.Year & allPublication[i].Subscribe.ToList()[j].EndTime.Year == DateTime.Now.Year)
                    {
                        resultPrice += allPublication[i].Subscribe.ToList()[j].ResultPrice;
                        allPrice += resultPrice;
                    }
                }

                vs.Add(resultPrice);
            }

            SeriesCollection seriesViews = new SeriesCollection();

            for (int k = 0; k < allPublication.Count; k++)
            {
                seriesViews.Add(new PieSeries()
                {
                    Title = $"{allPublication[k].Name}",
                    Values = new ChartValues<decimal> { vs[k] },
                    PushOut = 15,
                    DataLabels = true
                });
            }

            myChart.Series = seriesViews;

            myChart.LegendLocation = LegendLocation.Right;

            myChart.FontSize = 14;

            ChartValues<int> vs2 = new ChartValues<int>();

            for (int i = 0; i < allPublication.Count(); i++)
            {
                vs2.Add(allPublication[i].CountSubscribeLastYear);
            }

            seriesViews2 = new SeriesCollection();

            for (int p = 0; p < allPublication.Count; p++)
            {
                seriesViews2.Add(new ColumnSeries()
                {
                    Title = $"{allPublication[p].Name}",
                    Values = new ChartValues<int> { vs2[p] },

                });
            }

            Formatter = value => value.ToString("N");

            myChart2.Series = seriesViews2;

            myChart2.LegendLocation = LegendLocation.Right;

            myChart2.FontSize = 14;

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
