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

namespace PostOffice.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для WinEditAndAddTypePublication.xaml
    /// </summary>
    public partial class WinEditAndAddTypePublication : Window
    {
        TypePublication typePublication;

        Model.DataBasePostOffice dataBasePostOffice;

        public WinEditAndAddTypePublication(TypePublication typePublication)
        {
            InitializeComponent();

            this.typePublication = typePublication;

            DataContext = typePublication;

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (typePublication.id_TypePublication == 0)
            {
                typePublication.id_TypePublication = dataBasePostOffice.postOfficeEntities.TypePublication.Count() + 1;
                dataBasePostOffice.postOfficeEntities.TypePublication.Add(typePublication);
                dataBasePostOffice.postOfficeEntities.SaveChanges();
            }
            MessageBox.Show("Данные сохранились");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
