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
    /// Логика взаимодействия для PageMoreDetailsSubscriber.xaml
    /// </summary>
    public partial class PageMoreDetailsSubscriber : Page
    {
        //Подписчик почтового отделения
        SubscriberOfThePostOffice subscriberOfThePostOffice;

        //Лист со всеми клиентами почтового отделения
        List<SubscriberOfThePostOffice> allSubscriberOfThePostOffice;

        //Лист со всеми подписчиками почтового отделения
        List<SubscriberOfThePostOffice> allSubscribers;

        //Лист со всеми подписками
        List<Subscribe> allSubscribes;

        //БД
        Model.DataBasePostOffice dataBasePostOffice;

        //Метод для закрытия окна
        public delegate void CloseWin();

        //Объект метода для закрытия окна
        public static event CloseWin closeWin;

        //Поле польователь
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

            allSubscriberOfThePostOffice = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            var publicationSubscriber = dataBasePostOffice.postOfficeEntities.Subscribe.Where(persona => persona.id_Subscriber == subscriberOfThePostOffice.id_Subscriber).ToList();

            dgSubsriberPublication.ItemsSource = publicationSubscriber;
        }

        /// <summary>
        /// Метод для сохранения подписчика
        /// </summary>
        /// <returns></returns>
        private int SaveSubscrubers()
        {
            if (subscriberOfThePostOffice.id_Subscriber == 0)
            {
                var existsSubscribers = allSubscriberOfThePostOffice.Where(item => $"{item.Surname} {item.Name} {item.MiddleName}" == $"{subscriberOfThePostOffice.Surname} {subscriberOfThePostOffice.Name} {subscriberOfThePostOffice.MiddleName}");

                try
                {
                    if (existsSubscribers.Count() != 0)
                    {
                        throw new Exception("Клиент почтового отделения уже существует");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }

                List<OperatorPostOffice> allOperator = dataBasePostOffice.postOfficeEntities.OperatorPostOffice.ToList();

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

            return 1;

        }

        /// <summary>
        /// Кнопка для добавления публикации и перехода на страницу для добавления подписки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (string.IsNullOrWhiteSpace(tbPhone.Text))
                {
                    throw new Exception("Введите номер телефона");
                }

                if (string.IsNullOrWhiteSpace(tbBrithday.Text))
                {
                    throw new Exception("Введите дату рождения");
                }

                if (DateTime.Now.AddYears(-18) <= tbBrithday.SelectedDate.Value)
                {
                    throw new Exception("Пользователь должен быть страше 18 лет");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (SaveSubscrubers() == 1)
            {
                NavigationService.Navigate(new View.PageManagmentSubscribe(subscriberOfThePostOffice, user));
            }
        }

        /// <summary>
        /// Метод для сохранения подписчика 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_saveData(object sender, RoutedEventArgs e)
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

                if (string.IsNullOrWhiteSpace(tbPhone.Text))
                {
                    throw new Exception("Введите номер телефона");
                }

                if (string.IsNullOrWhiteSpace(tbBrithday.Text))
                {
                    throw new Exception("Введите дату рождения");
                }

                if (DateTime.Now.AddYears(-18) <= tbBrithday.SelectedDate.Value)
                {
                    throw new Exception("Пользователь должен быть страше 18 лет");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (SaveSubscrubers() == 1)
            {
                MessageBox.Show("Данные сохранились");
            }
        }

        /// <summary>
        /// Метод для удаления подписки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
