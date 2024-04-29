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
    /// Логика взаимодействия для PageCorrespondenceStatistic.xaml
    /// </summary>
    public partial class PageCorrespondenceStatistic : Page
    {
        Model.DataBasePostOffice dataBasePostOffice;

        Model.IOExcel iOExcel;

        public PageCorrespondenceStatistic()
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            dgCorrespondence.ItemsSource = dataBasePostOffice.postOfficeEntities.Correspondence.ToList();


        }

        private void Button_statisticToExcel(object sender, RoutedEventArgs e)
        {
            iOExcel = new Model.IOExcel(dataBasePostOffice);
            iOExcel.DBToExcelTableCorrespondence();
        }
    }
}
