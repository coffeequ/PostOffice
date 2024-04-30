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
using Word = Microsoft.Office.Interop.Word;

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

        List<Correspondence> allCorrespondence;

        User user;

        Random rnd;

        int yearMonthEnd = 0;

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

            allCorrespondence = dataBasePostOffice.postOfficeEntities.Correspondence.ToList();

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
                    yearMonthEnd = int.Parse(tbMonthEnd.Text);

                    if (string.IsNullOrWhiteSpace(yearMonthEnd.ToString()))
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

                    if (yearMonthEnd >= 13)
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

                Correspondence correspondence;

                int numberSubscribeGeneration = GetHashCode();

                int deliveryBreak = 0;

                int monthStart = int.Parse(tbMonthStart.Text);

                int monthEnd = int.Parse(tbMonthEnd.Text);

                int dayStart = int.Parse(tbDayStart.Text);

                for (int i = 0; i < publicationsSelected.Count(); i++)
                {
                    subscribe = new Subscribe();

                    int lastIndexCorrespondence = allCorrespondence[allCorrespondence.Count() - 1].id_Correspondence + 1;

                    subscribe.Publication = publicationsSelected[i];

                    subscribe.DateRegistration = DateTime.Now;

                    subscribe.EntryTime = DateTime.Now;

                    subscribe.StatusActive = 1;

                    DateTime dateTime = new DateTime(int.Parse(tbYearEnd.Text), yearMonthEnd, int.Parse(tbDayEnd.Text));

                    subscribe.EndTime = dateTime;

                    subscribe.id_Subscriber = subscriberOfThePostOffice.id_Subscriber;

                    subscribe.NumberSubscribe = numberSubscribeGeneration.ToString();

                    dataBasePostOffice.postOfficeEntities.Subscribe.Add(subscribe);

                    for (int month = monthStart; month < monthEnd; month++)
                    {
                        for (int k = 0; k < publicationsSelected[i].NumberIssuesPerMonth; k++)
                        {
                            correspondence = new Correspondence();

                            correspondence.DeliveryAddres = tbAdressDelivery.Text;

                            dayStart += 2;

                            if (dayStart >= 25)
                            {
                                monthStart += 1;

                                dayStart = 1;
                            }

                            correspondence.DateOfDispatch = new DateTime(int.Parse(tbYearEnd.Text), monthStart, dayStart);

                            dayStart += 4;

                            if (dayStart >= 25)
                            {
                                monthStart += 1;

                                dayStart = 1;
                            }

                            correspondence.DateOfDelivery = new DateTime(int.Parse(tbYearEnd.Text), monthStart, dayStart);

                            correspondence.Subscribe = subscribe;

                            correspondence.id_Correspondence = lastIndexCorrespondence;

                            dataBasePostOffice.postOfficeEntities.Correspondence.Add(correspondence);
                        }
                    }

                    deliveryBreak = 4;

                    deliveryBreak += 3;

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
                    Word.Application wordApp = new Word.Application();

                    Word.Document wordDocument = wordApp.Documents.Add(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CheckPay.docx"));

                    wordDocument.Bookmarks["NumberSubscribeWord"].Range.Text = "N " + numberSubscribeGeneration.ToString();

                    wordDocument.Bookmarks["DateNow"].Range.Text = DateTime.Now.ToString();

                    Word.Table table = wordDocument.Tables[2];

                    Word.Range cellRange;

                    int countPublication = monthEnd - int.Parse(tbMonthStart.Text);

                    decimal priceItog = 0;

                    for (int i = 0; i < publicationsSelected.Count; i++)
                    {
                        table.Rows.Add();

                        cellRange = table.Cell(i + 4, 1).Range;

                        cellRange.Text = (i + 1).ToString();

                        cellRange = table.Cell(i + 4, 2).Range;

                        cellRange.Text = publicationsSelected[i].Name;

                        cellRange = table.Cell(i + 4, 3).Range;

                        cellRange.Text = publicationsSelected[i].PricePerMonthRounded;

                        cellRange = table.Cell(i + 4, 4).Range;

                        cellRange.Text = countPublication.ToString();

                        cellRange = table.Cell(i + 4, 5).Range;

                        cellRange.Text = "НДС 20%";

                        cellRange = table.Cell(i + 4, 6).Range;

                        decimal price = countPublication * publicationsSelected[i].PricePerMonth;

                        priceItog += price;

                        cellRange.Text = price.ToString("F2");
                    }

                    wordDocument.Bookmarks["TotalPrice"].Range.Text = priceItog.ToString("F2");

                    wordDocument.Bookmarks["TotalPrice2"].Range.Text = priceItog.ToString("F2");

                    double nds = (double)priceItog * 0.20;

                    wordDocument.Bookmarks["Nds"].Range.Text = nds.ToString("F2");

                    wordApp.Visible = true;

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
