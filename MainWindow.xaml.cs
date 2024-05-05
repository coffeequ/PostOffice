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

        public MainWindow()
        {
            InitializeComponent();

            postOfficeEntity = new PostOfficeEntities();

            mainFrame.NavigationService.Navigate(new View.PageWatchPublicationNoReg());

            View.WinEntrance.closeWin += WinEntrance_closeWin;

            List<SubscriberOfThePostOffice> subscriberOfThePostOffices = postOfficeEntity.SubscriberOfThePostOffice.ToList();

            List<Subscribe> activeSubsribes = new List<Subscribe>();

            for (int i = 0; i < subscriberOfThePostOffices.Count; i++)
            {
                for (int j = 0; j < subscriberOfThePostOffices[i].Subscribe.Count(); j++)
                {
                    if (subscriberOfThePostOffices[i].Subscribe.ToList()[j].EndTime <= DateTime.Now)
                    {
                        subscriberOfThePostOffices[i].Subscribe.ToList()[j].StatusActive = 0;
                    }
                }
            }

            postOfficeEntity.SaveChanges();

        }

        private void WinEntrance_closeWin()
        {
            Close();
        }

        public void PageWatchPublicationNoReg_closePage()
        {

        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void F1Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ProcHelp procHelp = new Model.ProcHelp("Справка.chm");

            procHelp.CallHelp();
        }
    }
}
