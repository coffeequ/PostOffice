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
    /// Логика взаимодействия для PageWatchPublicationNoReg.xaml
    /// </summary>
    public partial class PageWatchPublicationNoReg : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        List<Publication> _publications;

        List<Button> buttonsMenu;

        private int _allCountPublicationEntity;

        private int _countPublication = 6;

        private int _maxPages;

        private int _currentPage = 1;

        public PageWatchPublicationNoReg()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            buttonsMenu = new List<Button>();

            _publications = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            _allCountPublicationEntity = dataBasePostOffice.postOfficeEntities.Publication.Count();
        }

        private void Refresh()
        {
            var publications = _publications.ToList();

            _maxPages = (int)Math.Ceiling(_allCountPublicationEntity * 1.0 / _countPublication);

            var publicationPages = publications.Skip((_currentPage - 1) * _countPublication).Take(_countPublication).ToList();

            MyLv.ItemsSource = publicationPages.ToList();
        }

        private void ListViewSelectedChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
