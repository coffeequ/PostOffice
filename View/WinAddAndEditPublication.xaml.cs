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
        Dictionary<string, int> keyValuePairsType;

        Dictionary<string, int> keyValuePairsTypeView;

        List<Publication> allPublication;

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

            keyValuePairsType = dataBasePostOffice.postOfficeEntities.TypePublication.ToDictionary(value => value.Name, item => item.id_TypePublication);

            keyValuePairsTypeView = dataBasePostOffice.postOfficeEntities.TypeViewPublication.ToDictionary(value => value.Name, item => item.id_TypeViewPublication);

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();
            //Вывод в ComboBox не работает
            if (Publication.TypePublication == null)
            {
                cbTypePublication.ItemsSource = keyValuePairsType.Keys;
            }
            else
            {
                cbTypePublication.ItemsSource = keyValuePairsType.Keys;

                cbTypePublication.SelectedItem = Publication.TypePublication;
            }

            if (Publication.TypeViewPublication == null)
            {
                cbTypeViewPublication.ItemsSource = keyValuePairsTypeView.Keys;
            }
            else
            {
                cbTypeViewPublication.ItemsSource = keyValuePairsTypeView.Keys;

                cbTypeViewPublication.SelectedItem = Publication.TypeViewPublication;
            }

            DataContext = Publication;

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            decimal pricePerMonthCheck = 0;

            int numberIssuePerMonthCheck = 0;

            try
            {
                pricePerMonthCheck = decimal.Parse(tbPriceMonth.Text);

                numberIssuePerMonthCheck = int.Parse(tbNumberIssuesPerMonth.Text);

                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    throw new Exception("Наименование издания не может быть пустым");
                }

                if (cbTypePublication.SelectedItem == null)
                {
                    throw new Exception("Тип издания не может быть пустым");
                }

                if (string.IsNullOrWhiteSpace(cbTypeViewPublication.SelectedItem.ToString()))
                {
                    throw new Exception("Вид издания не может быть пустым");
                }

                if (string.IsNullOrWhiteSpace(tbPathToPhoto.Text))
                {
                    throw new Exception("Не указан путь до фото");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return;
            }

            Publication.Cover = tbPathToPhoto.Text;

            Publication.PricePerMonth = pricePerMonthCheck;

            Publication.NumberIssuesPerMonth = numberIssuePerMonthCheck;

            Publication.id_Publication = allPublication.Count() + 1;

            Publication.Name = tbName.Text;

            Publication.id_TypePublication = keyValuePairsType[cbTypePublication.SelectedItem.ToString()];

            Publication.id_TypeViewPublication = keyValuePairsTypeView[cbTypeViewPublication.SelectedItem.ToString()];

            dataBasePostOffice.postOfficeEntities.Publication.Add(Publication);

            MessageBox.Show("Издание было успешно добавлено!");

            dataBasePostOffice.postOfficeEntities.SaveChanges();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
