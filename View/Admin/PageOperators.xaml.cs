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

namespace PostOffice.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageOperators.xaml
    /// </summary>
    public partial class PageOperators : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        List<User> users;

        List<LogIO> logIOs;

        public PageOperators()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            logIOs = dataBasePostOffice.postOfficeEntities.LogIO.ToList();

            dgOperators.ItemsSource = MainWindow.postOfficeEntity.OperatorPostOffice.ToList();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            OperatorPostOffice operatorPostOffice = new OperatorPostOffice();

            new WinAddAndEditOperators(operatorPostOffice).ShowDialog();

            dgOperators.ItemsSource = null;

            dgOperators.ItemsSource = MainWindow.postOfficeEntity.OperatorPostOffice.ToList();

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            users = dataBasePostOffice.postOfficeEntities.User.ToList();

            var item = sender as Button;

            var selectedItem = item.DataContext as OperatorPostOffice;

            if (selectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вы точно хотите удалить запись",
                    "Внимание!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    User user = new User();

                    foreach (var itemCollection in users)
                    {
                        if (itemCollection.id_User == selectedItem.id_Operator)
                        {
                            user = itemCollection;
                            MainWindow.postOfficeEntity.User.Remove(itemCollection);
                        }
                    }

                    dataBasePostOffice.postOfficeEntities.OperatorPostOffice.Remove(selectedItem);

                    for (int i = 0; i < logIOs.Count(); i++)
                    {
                        if (logIOs[i].id_User == user.id_User)
                        {
                            MainWindow.postOfficeEntity.LogIO.Remove(logIOs[i]);
                        }
                    }

                    dataBasePostOffice.postOfficeEntities.SaveChanges();

                    MessageBox.Show("Запись удалена!");

                    dgOperators.ItemsSource = null;

                    dgOperators.ItemsSource = MainWindow.postOfficeEntity.OperatorPostOffice.ToList();
                }
            }
        }

        private void Button_edit(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as OperatorPostOffice;

            new WinAddAndEditOperators(selectedItem).ShowDialog();

            dgOperators.ItemsSource = null;

            dgOperators.ItemsSource = MainWindow.postOfficeEntity.OperatorPostOffice.ToList();
        }
    }
}
