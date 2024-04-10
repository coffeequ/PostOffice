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
    /// Логика взаимодействия для PagePublication.xaml
    /// </summary>
    public partial class PagePublication : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        public PagePublication()
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

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();
        }


        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var itemSelected = item.DataContext as Publication;

            new WinAddAndEditPublication(itemSelected).ShowDialog();

            dgPublication.ItemsSource = null;

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

        }
    }
}
