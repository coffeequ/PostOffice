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

            cbTypePublication.ItemsSource = keyValuePairsType.Keys;

            cbTypeViewPublication.ItemsSource = keyValuePairsTypeView.Keys;

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Publication.id_Publication = allPublication.Count() + 1;

            Publication.Name = tbName.Text;

            Publication.id_TypePublication = keyValuePairsType[cbTypePublication.SelectedItem.ToString()];

            Publication.id_TypeViewPublication = keyValuePairsTypeView[cbTypeViewPublication.SelectedItem.ToString()];

            Publication.PricePerMonth = decimal.Parse(tbPriceMonth.Text);

            Publication.NumberIssuesPerMonth = int.Parse(tbNumberIssuesPerMonth.Text);

            dataBasePostOffice.postOfficeEntities.Publication.Add(Publication);

            MessageBox.Show("Издание было успешно добавлено!");

            dataBasePostOffice.postOfficeEntities.SaveChanges();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

        }
    }
}
