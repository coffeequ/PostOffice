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

        List<Publication> allPublication;

        List<Publication> sortPublication;

        List<Publication> temps;

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

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            sortPublication = new List<Publication>();

            temps = new List<Publication>();

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            comboBoxTypePublication(cbTypePublication, dataBasePostOffice.postOfficeEntities.TypePublication.ToList());

            comboBoxTypeViewPubication(cbCatergoriaPublication, dataBasePostOffice.postOfficeEntities.TypeViewPublication.ToList());
        }

        private void comboBoxTypePublication(ComboBox comboBox, List<TypePublication> list)
        {
            List<string> temp = new List<string>();

            temp.Add("Все издания");

            foreach (var item in list)
            {
                temp.Add(item.Name);
            }

            comboBox.ItemsSource = temp;

            comboBox.SelectedItem = temp[0];
        }

        private void comboBoxTypeViewPubication(ComboBox comboBox, List<TypeViewPublication> list)
        {
            List<string> temp = new List<string>();

            temp.Add("Все категории изданий");

            foreach (var item in list)
            {
                temp.Add(item.Name);
            }

            comboBox.ItemsSource = temp;

            comboBox.SelectedItem = temp[0];
        }

        private void TextChangedSearch(object sender, TextChangedEventArgs e)
        {
            SearchFilter();
            ApplyComboBoxFiltres();
        }

        private void SearchFilter()
        {
            sortPublication = allPublication.Where(item => item.Name.StartsWith(tbSearch.Text)).ToList();
        }

        private void ApplyComboBoxFiltres()
        {
            string selectItem = cbCatergoriaPublication.SelectedItem as string;

            string selectItem2 = cbTypePublication.SelectedItem as string;

            if (selectItem != "Все категории изданий")
            {
                temps = allPublication.Where(item => item.TypeViewPublication.Name == selectItem & item.Name.StartsWith(tbSearch.Text)).ToList();
                if (selectItem2 != "Все издания")
                {
                    sortPublication = temps.Where(item => item.TypePublication.Name == selectItem2 & item.Name.StartsWith(tbSearch.Text)).ToList();
                }
                else
                {
                    sortPublication = temps;
                }
            }

            if (selectItem2 != "Все издания")
            {
                temps = allPublication.Where(item => item.TypePublication.Name == selectItem2 & item.Name.StartsWith(tbSearch.Text)).ToList();
                if (selectItem != "Все категории изданий")
                {
                    sortPublication = temps.Where(item => item.TypeViewPublication.Name == selectItem & item.Name.StartsWith(tbSearch.Text)).ToList();
                }
                else
                {
                    sortPublication = temps;
                }
            }
            dgPublication.ItemsSource = sortPublication;
        }

        private void cbCategoriaChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchFilter();
            ApplyComboBoxFiltres();
        }

        private void cbTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchFilter();
            ApplyComboBoxFiltres();
        }
    }
}
