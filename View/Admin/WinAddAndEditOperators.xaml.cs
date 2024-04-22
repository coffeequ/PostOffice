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
    /// Логика взаимодействия для WinAddAndEditOperators.xaml
    /// </summary>
    public partial class WinAddAndEditOperators : Window
    {
        OperatorPostOffice operatorPostOffice;

        List<User> users;

        public WinAddAndEditOperators(OperatorPostOffice operatorPostOffice)
        {
            InitializeComponent();

            DataContext = operatorPostOffice;

            this.operatorPostOffice = operatorPostOffice;

            users = MainWindow.postOfficeEntity.User.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (operatorPostOffice.id_Operator == 0)
            {
                User user = new User();

                user.Login = tbLogin.Text;

                user.Password = tbPassword.Text;

                user.id_User = users[users.Count() - 1].id_User + 1;

                user.id_Role = 2;

                user.OperatorPostOffice = operatorPostOffice;

                MainWindow.postOfficeEntity.User.Add(user);

                operatorPostOffice.User = user;

                MainWindow.postOfficeEntity.OperatorPostOffice.Add(operatorPostOffice);
            }

            MainWindow.postOfficeEntity.SaveChanges();

            MessageBox.Show("Успешное сохранение");
        }
    }
}
