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
    /// Логика взаимодействия для WinMoreDetailsSubscriber.xaml
    /// </summary>
    public partial class WinMoreDetailsSubscriber : Window
    {
        SubscriberOfThePostOffice subscriberOfThePostOffice;

        Model.DataBasePostOffice dataBasePostOffice;

        public WinMoreDetailsSubscriber(SubscriberOfThePostOffice subscriberOfThePostOffice)
        {
            InitializeComponent();

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);

            this.subscriberOfThePostOffice = subscriberOfThePostOffice;
            
            DataContext = subscriberOfThePostOffice;

            var publicationSubscriber = dataBasePostOffice.postOfficeEntities.Subscribe.Where(persona => persona.id_Subscriber == subscriberOfThePostOffice.id_Subscriber).ToList();

            dgSubsriberPublication.ItemsSource = publicationSubscriber;
        }
    }
}
