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
    /// Логика взаимодействия для PageTypeViewPublication.xaml
    /// </summary>
    public partial class PageTypeViewPublication : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        public PageTypeViewPublication()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            dgTypeViewPublication.ItemsSource = dataBasePostOffice.postOfficeEntities.TypeViewPublication.ToList();
        }
    }
}
