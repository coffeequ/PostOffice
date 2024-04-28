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
    /// Логика взаимодействия для PageLogIO.xaml
    /// </summary>
    public partial class PageLogIO : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        public PageLogIO()
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

            dgLogIO.ItemsSource = dataBasePostOffice.postOfficeEntities.LogIO.OrderByDescending(item => item.id_Journal).ToList();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вы точно хотите очистить записи",
                    "Внимание!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var item in MainWindow.postOfficeEntity.LogIO)
                {
                    MainWindow.postOfficeEntity.LogIO.Remove(item);
                }

                MainWindow.postOfficeEntity.SaveChanges();

                MainWindow.postOfficeEntity.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('LogIO', RESEED, 0);");

                MessageBox.Show("История была успешно очищина");

                dgLogIO.ItemsSource = MainWindow.postOfficeEntity.LogIO.ToList();

            }
        }
    }
}

