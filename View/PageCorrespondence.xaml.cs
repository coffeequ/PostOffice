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
    /// Логика взаимодействия для PageCorrespondence.xaml
    /// </summary>
    public partial class PageCorrespondence : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        public PageCorrespondence()
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

            dgCorrespondence.ItemsSource = dataBasePostOffice.postOfficeEntities.Correspondence.OrderByDescending(item => item.id_Correspondence).ToList();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as Correspondence;

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

                    dataBasePostOffice.postOfficeEntities.Correspondence.Remove(selectedItem);

                    dataBasePostOffice.postOfficeEntities.SaveChanges();

                    MessageBox.Show("Запись удалена!");

                    dgCorrespondence.ItemsSource = null;

                    dgCorrespondence.ItemsSource = dataBasePostOffice.postOfficeEntities.Correspondence.OrderByDescending(item2 => item2.id_Correspondence).ToList();
                }
            }
        }

    }
}
