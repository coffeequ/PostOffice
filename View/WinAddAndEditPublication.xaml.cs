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
                try
                {
                    Publication.TypePublication = (TypePublication)cbTypePublication.SelectedItem;

                    Publication.TypeViewPublication = (TypeViewPublication)cbTypeViewPublication.SelectedItem;

                    if (string.IsNullOrEmpty(tbName.Text))
                    {
                        throw new Exception("Поле \"Наименование издания\" не может быть пустым");
                    }

                    if (tbName.Text.Length > 50)
                    {
                        throw new Exception("Наименование издания не может превышать 50 символов");
                    }

                    if (allPublication.Where(item => item.Name == tbName.Text).Count() >= 1)
                    {
                        throw new Exception("Издание с таким именем уже существует");
                    }

                    if (string.IsNullOrWhiteSpace(tbNumberIssuesPerMonth.Text) || int.Parse(tbNumberIssuesPerMonth.Text) == 0)
                    {
                        throw new Exception("Поле \"Количество выпусков в месяц\" не может быть пустым");
                    }

                    if (string.IsNullOrEmpty(tbPriceMonth.Text) || int.Parse(tbPriceMonth.Text) == 0)
                    {
                        throw new Exception("Поле \"Цена за месяц\" не может быть пустым");
                    }

                    if (Publication.Cover == null)
                    {
                        throw new Exception("Выберете обложку для публикации");
                    }

                    if (Publication.TypePublication == null)
                    {
                        throw new Exception("Выберете тип издания");
                    }

                    if (Publication.TypeViewPublication == null)
                    {
                        throw new Exception("Выберете вид издания");
                    }

                    Publication.NumberIssuesPerMonth = int.Parse(tbNumberIssuesPerMonth.Text);

                    Publication.PricePerMonth = int.Parse(tbPriceMonth.Text);

                    if (Publication.NumberIssuesPerMonth > 4)
                    {
                        throw new Exception("Количество выпусков не может превышать 4 единиц в месяц");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                Publication.id_Publication = allPublication[allPublication.Count() - 1].id_Publication + 1;
                dataBasePostOffice.postOfficeEntities.Publication.Add(Publication);
            }

            dataBasePostOffice.postOfficeEntities.SaveChanges();

            MessageBox.Show("Издание было успешно сохранено");
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

            MessageBox.Show("Комментарий удалён!");

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
        private void F1Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ProcHelp procHelp = new Model.ProcHelp("Справка.chm");

            procHelp.CallHelp();
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as Feedback;

            if (selectedItem.Status != 1)
            {
                if (selectedItem == null)
                {
                    MessageBox.Show("Ошибочное поле");
                    return;
                }

                selectedItem.Status = 1;

                dataBasePostOffice.postOfficeEntities.SaveChanges();

                MessageBox.Show("Комментарий был опубликован!");
            }
            else
            {
                MessageBox.Show("Комментарий уже опубликован!");
            }
        }
    }
}
