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
        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public WinEntrance()
        {
            InitializeComponent();

            View.PageLogin.closeWin += PageLogin_closeWin;

            View.PageLogin.closeWin1 += PageLogin_closeWin1;

            frameLog.NavigationService.Navigate(new View.PageLogin());
        }

        private void PageLogin_closeWin1()
        {
            Close();
        }

        private void PageLogin_closeWin()
        {
            closeWin();
            Close();
        }
    }
}
