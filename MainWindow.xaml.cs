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

namespace PostOffice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static PostOfficeEntities postOfficeEntity;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public MainWindow()
        {
            InitializeComponent();

            postOfficeEntity = new PostOfficeEntities();

            View.PageWatchPublicationNoReg.closePage += PageWatchPublicationNoReg_closePage;

            mainFrame.NavigationService.Navigate(new View.PageWatchPublicationNoReg());
        }

        public void PageWatchPublicationNoReg_closePage()
        {
            Close();
        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
