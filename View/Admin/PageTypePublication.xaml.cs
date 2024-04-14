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
    /// Логика взаимодействия для PageTypePublication.xaml
    /// </summary>
    public partial class PageTypePublication : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        public PageTypePublication()
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

            var AllTypePublication = dataBasePostOffice.postOfficeEntities.TypePublication.ToList();

            DataContext = AllTypePublication;

            dgTypePublication.ItemsSource = AllTypePublication;
        }

        private void UpdateInfo()
        {
            dgTypePublication.ItemsSource = null;

            dgTypePublication.ItemsSource = dataBasePostOffice.postOfficeEntities.TypePublication.ToList();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            var selectedItem = item.DataContext as TypePublication;

            new View.Admin.WinEditAndAddTypePublication(selectedItem).ShowDialog();

            UpdateInfo();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            var allDeletePublication = new List<Publication>();

            var allSubscribers = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            var allDeleteSubscribers = new List<Subscribe>();

            var allCorrespondence = dataBasePostOffice.postOfficeEntities.Correspondence.ToList();

            var item = sender as Button;

            var selectedItem = item.DataContext as TypePublication;

            for (int i = 0; i < allPublication.Count; i++)
            {
                if (allPublication[i].TypePublication == selectedItem)
                {
                    allDeletePublication.Add(allPublication[i]);
                    dataBasePostOffice.postOfficeEntities.Publication.Remove(allPublication[i]);
                }
            }

            for (int i = 0; i < allDeletePublication.Count; i++)
            {
                for (int j = 0; j < allSubscribers.Count; j++)
                {
                    if (allDeletePublication[i].id_Publication == allSubscribers[j].id_Publication)
                    {
                        allDeleteSubscribers.Add(allSubscribers[j]);
                        dataBasePostOffice.postOfficeEntities.Subscribe.Remove(allDeleteSubscribers[j]);
                    }
                }
            }

            for (int i = 0; i < allDeleteSubscribers.Count; i++)
            {
                for (int j = 0; j < allCorrespondence.Count; j++)
                {
                    if (allDeleteSubscribers[i].id_Subscribe == allCorrespondence[j].id_Subscribe)
                    {
                        dataBasePostOffice.postOfficeEntities.Correspondence.Remove(allCorrespondence[j]);
                    }
                }
            }
            dataBasePostOffice.postOfficeEntities.TypePublication.Remove(selectedItem);

            dataBasePostOffice.postOfficeEntities.SaveChanges();

            MessageBox.Show("Запись была удалена!");

            UpdateInfo();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            TypePublication typePublication = new TypePublication();

            new View.Admin.WinEditAndAddTypePublication(typePublication).ShowDialog();

            UpdateInfo();
        }
    }
}
