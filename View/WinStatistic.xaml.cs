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
    /// Логика взаимодействия для WinStatistic.xaml
    /// </summary>
    public partial class WinStatistic : Window
    {
        public WinStatistic(User user)
        {
            InitializeComponent();

            frameStatistic.NavigationService.Navigate(new View.PageStatistic(user));
        }
    }
}
