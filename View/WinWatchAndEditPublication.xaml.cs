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

        List<Feedback> feedbacks;

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

            feedbacks = dataBasePostOffice.postOfficeEntities.Feedback.ToList();
        }

        private List<Feedback> UpdateInfo()
        {
            return dataBasePostOffice.postOfficeEntities.Feedback.Where(item => item.Publication.id_Publication == publication.id_Publication).ToList();
        }

        private void isLoaded(object sender, RoutedEventArgs e)
        {
            DataGridTableReview.ItemsSource = UpdateInfo();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KeyDownFeedback(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Feedback feedback = new Feedback();

                feedback.id_Feedback = feedbacks.Count() + 1;

                feedback.Feedback1 = CommentTextBox.Text;

                feedback.id_Publication = publication.id_Publication;

                dataBasePostOffice.postOfficeEntities.Feedback.Add(feedback);

                MessageBox.Show("Отзыв был успешно добавлен!");

                dataBasePostOffice.postOfficeEntities.SaveChanges();

                DataGridTableReview.ItemsSource = null;

                DataGridTableReview.ItemsSource = UpdateInfo();

                CommentTextBox.Clear();
            }
        }
    }
}
