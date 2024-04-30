using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для WinAddAndEditPublication.xaml
    /// </summary>
    public partial class WinAddAndEditPublication : Window
    {
        List<Publication> allPublication;

        List<TypePublication> allTypePublication;

        List<TypeViewPublication> allTypeViewPublication;

        Model.DataBasePostOffice dataBasePostOffice;

        Publication Publication;

        public WinAddAndEditPublication(Publication Publication)
        {
            this.Publication = Publication;

            InitializeComponent();

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataContext = Publication;

            allTypePublication = dataBasePostOffice.postOfficeEntities.TypePublication.ToList();

            cbTypePublication.ItemsSource = MainWindow.postOfficeEntity.TypePublication.ToList();

            cbTypeViewPublication.ItemsSource = MainWindow.postOfficeEntity.TypeViewPublication.ToList();

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            //Комментарии
            List<Feedback> allFeedBacks = dataBasePostOffice.postOfficeEntities.Feedback.ToList();

            var temp = new List<Feedback>();

            for (int i = 0; i < allFeedBacks.Count(); i++)
            {
                if (allFeedBacks[i].id_Publication == Publication.id_Publication)
                {
                    temp.Add(allFeedBacks[i]);
                }
            }

            DataGridTableReview.ItemsSource = temp.ToList();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (Publication.id_Publication == 0)
            {
                Publication.id_Publication = allPublication[allPublication.Count() - 1].id_Publication + 1;

                dataBasePostOffice.postOfficeEntities.Publication.Add(Publication);
            }

            dataBasePostOffice.postOfficeEntities.SaveChanges();

            MessageBox.Show("Издание было успешно сохранены");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as Feedback;

            if (selectedItem == null)
            {
                MessageBox.Show("Не возможно удалить данное поле");
                return;
            }

            dataBasePostOffice.postOfficeEntities.Feedback.Remove(selectedItem);

            dataBasePostOffice.postOfficeEntities.SaveChanges();

            MessageBox.Show("Комментарий удален!");

            List<Feedback> allFeedBacks = dataBasePostOffice.postOfficeEntities.Feedback.ToList();

            var temp = new List<Feedback>();

            for (int i = 0; i < allFeedBacks.Count(); i++)
            {
                if (allFeedBacks[i].id_Publication == Publication.id_Publication)
                {
                    temp.Add(allFeedBacks[i]);
                }
            }

            DataGridTableReview.ItemsSource = temp.ToList();

        }

        private void Button_add_cover(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filename = openFileDialog.FileName;
                var x = System.IO.File.ReadAllBytes(filename);
                Publication.Cover = x;
            }
            
        }
    }
}
