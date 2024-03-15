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
    /// Логика взаимодействия для WinEntrance.xaml
    /// </summary>
    public partial class WinEntrance : Window
    {
        Model.DataBasePostOffice DataBasePostOffice;

        public WinEntrance()
        {
            InitializeComponent();

            try
            {
                DataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        private void btnEntrance(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit(object sender, RoutedEventArgs e)
        {

        }
    }
}
