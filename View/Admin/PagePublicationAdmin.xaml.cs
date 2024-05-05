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
    /// Логика взаимодействия для PagePublicationAdmin.xaml
    /// </summary>
    public partial class PagePublicationAdmin : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        List<Subscribe> allSubscribers;

        List<Subscribe> activeSubscribers;

        List<Publication> allPublication;

        List<Publication> sortPublication;

        List<Publication> temps;

        public PagePublicationAdmin()
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

            allSubscribers = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            sortPublication = new List<Publication>();

            temps = new List<Publication>();

            dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            comboBoxTypePublication(cbTypePublication, dataBasePostOffice.postOfficeEntities.TypePublication.ToList());

            comboBoxTypeViewPubication(cbCatergoriaPublication, dataBasePostOffice.postOfficeEntities.TypeViewPublication.ToList());

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            activeSubscribers = new List<Subscribe>();

            var item = sender as Button;

            var selectedItem = item.DataContext as Publication;

            if (selectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    "Вы точно хотите удалить запись?",
                    "Внимание",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error
                    );

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var itemSubsribers in allSubscribers)
                    {
                        if (itemSubsribers.id_Publication == selectedItem.id_Publication)
                        {
                            activeSubscribers.Add(itemSubsribers);
                        }
                    }

                    if (activeSubscribers.Count() == 0)
                    {
                        dataBasePostOffice.postOfficeEntities.Publication.Remove(selectedItem);

                        dataBasePostOffice.postOfficeEntities.SaveChanges();
                        
                        dgPublication.ItemsSource = null;
                        
                        dgPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.Publication.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Удаление предотвращено! Вы не можете удалить это издание, так как на него есть активные подписки");
                    }
                }
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var itemSelected = item.DataContext as Publication;

            new WinAddAndEditPublication(itemSelected).ShowDialog();

            SearchFilter();

            ComboBoxFiltres();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Publication publication = new Publication();

            new WinAddAndEditPublication(publication).ShowDialog();

            SearchFilter();

            ComboBoxFiltres();
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
            ComboBoxFiltres();
        }

        private void SearchFilter()
        {
            sortPublication = allPublication.Where(item => item.Name.StartsWith(tbSearch.Text)).ToList();
        }

        private void ComboBoxFiltres()
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
            ComboBoxFiltres();
        }

        private void cbTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchFilter();
            ComboBoxFiltres();
        }

    }
}
