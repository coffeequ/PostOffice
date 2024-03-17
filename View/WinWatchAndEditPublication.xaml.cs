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
using System.Windows.Shapes;

namespace PostOffice.View
{
    /// <summary>
    /// Логика взаимодействия для WinWatchAndEditPublication.xaml
    /// </summary>
    public partial class WinWatchAndEditPublication : Window
    {
        Publication publication;

        Model.DataBasePostOffice dataBasePostOffice;

        public WinWatchAndEditPublication(Publication publication)
        {
            InitializeComponent();

            this.publication = publication;

            DataContext = publication;

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        private void isLoaded(object sender, RoutedEventArgs e)
        {
            var listFeedBack = dataBasePostOffice.postOfficeEntities.Feedback.Where(item => item.Publication.id_Publication == publication.id_Publication).ToList();
            DataGridTableReview.ItemsSource = listFeedBack;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
