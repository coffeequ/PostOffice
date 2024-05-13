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
    //*****************************************************************//
    //* Название программы: "PostOffice"                               //
    //*                                                                //
    //* Назначение программы: учет подписчиков периодических изданий и //
    //* движения корреспонденции в почтовом отделении.                 //
    //* Разработчик: студент группы ПР-330/б Пугач С.Е.                //
    //*                                                                //
    //* Версия 1.0                                                     //
    //*****************************************************************//
    /// <summary>
    /// Логика взаимодействия для PageManagmentSubscribe.xaml
    /// </summary>
    public partial class PageManagmentSubscribe : Page
    {
        //Подписчик почтового отделения
        SubscriberOfThePostOffice subscriberOfThePostOffice;

        //БД
        Model.DataBasePostOffice dataBasePostOffice;

        //Лист публикаций
        List<Publication> publicationsSelected;

        //Лист активных публикаций у пользователя
        List<Publication> activePublication;

        //Лист всех подписок
        List<Subscribe> subscribes;

        //Лист всех корреспонденций
        List<Correspondence> allCorrespondence;

        //Пользователь 
        User user;

        //"Рандомное" поле
        Random rnd;
        
        //Год окончания подписки
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

        /// <summary>
        /// Метод загрузки грид формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Метод для добавления подписки и корреспонденции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (publicationsSelected.Count > 0)
            {
                try
                {
                    yearMonthEnd = int.Parse(tbMonthEnd.Text);

                    int yearMonthStart = int.Parse(tbMonthStart.Text);

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

                    if ((yearMonthEnd - yearMonthStart) == 0)
                    {
                        throw new Exception("Не возможно оформить подписку менее чем на 1 месяц");
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
                    decimal price = 0;

                    subscribe = new Subscribe();

                    int lastIndexCorrespondence = allCorrespondence[allCorrespondence.Count() - 1].id_Correspondence + 1;

                    subscribe.Publication = publicationsSelected[i];

                    subscribe.DateRegistration = DateTime.Now;

                    subscribe.EntryTime = DateTime.Now;

                    subscribe.StatusActive = 1;

                    DateTime dateTime = new DateTime(int.Parse(tbYearEnd.Text), yearMonthEnd, int.Parse(tbDayEnd.Text));

                    int countPublication = monthEnd - int.Parse(tbMonthStart.Text);

                    price = publicationsSelected[i].PricePerMonth * countPublication;

                    subscribe.ResultPrice = price;

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

                    try
                    {
                        dataBasePostOffice.postOfficeEntities.SaveChanges();
                    }
                    catch (Exception)
                    {
                        //Починить ошибку при, после которой не возможно взаимодейстовать с базой
                        MessageBox.Show("Не возможно оформить подписку не менее чем на 1 месяц");
                    }
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

                        decimal priceWord = countPublication * publicationsSelected[i].PricePerMonth;

                        priceItog += priceWord;

                        cellRange.Text = priceWord.ToString("F2");
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

        /// <summary>
        /// Метод для добавления подписки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Метод удаления выбранной подписки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
