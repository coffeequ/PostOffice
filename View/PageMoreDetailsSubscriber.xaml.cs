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

        List<SubscriberOfThePostOffice> allSubscribers;

        List<Subscribe> allSubscribes;

        Model.DataBasePostOffice dataBasePostOffice;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        User user;

        public PageMoreDetailsSubscriber(SubscriberOfThePostOffice subscriberOfThePostOffice, User user)
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

            this.user = user;

            DataContext = subscriberOfThePostOffice;

            allSubscribes = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            allSubscribers = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            var publicationSubscriber = dataBasePostOffice.postOfficeEntities.Subscribe.Where(persona => persona.id_Subscriber == subscriberOfThePostOffice.id_Subscriber).ToList();

            dgSubsriberPublication.ItemsSource = publicationSubscriber;
        }

        private void SaveSubscrubers()
        {
            if (subscriberOfThePostOffice.id_Subscriber == 0)
            {
                var allOperator = dataBasePostOffice.postOfficeEntities.OperatorPostOffice.ToList();

                OperatorPostOffice tempOperator = new OperatorPostOffice();

                foreach (var item in allOperator)
                {
                    if (item.id_Operator == user.id_User)
                    {
                        tempOperator = item;
                    }
                }
                subscriberOfThePostOffice.id_Subscriber = allSubscribers[allSubscribers.Count() - 1].id_Subscriber + 1;
                subscriberOfThePostOffice.OperatorPostOffice = tempOperator;
                dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.Add(subscriberOfThePostOffice);
            }

            subscriberOfThePostOffice.Birthday = (DateTime)tbBrithday.SelectedDate;

            dataBasePostOffice.postOfficeEntities.SaveChanges();
        }

        private void Button_Add_Publication(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbSurname.Text))
                {
                    throw new Exception("Введите фамилию");
                }

                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    throw new Exception("Введите имя");
                }

                if (string.IsNullOrWhiteSpace(tbMiddleName.Text))
                {
                    throw new Exception("Введите отчество");
                }

                if (string.IsNullOrWhiteSpace(tbPhone.Text))
                {
                    throw new Exception("Введите номер телефона");
                }

                if (string.IsNullOrWhiteSpace(tbBrithday.Text))
                {
                    throw new Exception("Введите дату рождения");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            SaveSubscrubers();
            NavigationService.Navigate(new View.PageManagmentSubscribe(subscriberOfThePostOffice, user));
        }

        private void Button_saveData(object sender, RoutedEventArgs e)
        {
            SaveSubscrubers();
            MessageBox.Show("Данные сохранились");

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
