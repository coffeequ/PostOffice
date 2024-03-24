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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        Model.DataBasePostOffice DataBasePostOffice;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public PageLogin()
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
            string inputLogin = tbLogin.Text.Trim();

            string inputPassword = tbPassword.Password.Trim();

            var operatorPostOffice = DataBasePostOffice.postOfficeEntities.User.ToList().Where(personal => personal.Login == inputLogin).ToList();

            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Введите логин");
            }
            else if (string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                MessageBox.Show("Введите пароль");
            }
            else if (operatorPostOffice.Count() != 0)
            {
                if (operatorPostOffice[0].Password == inputPassword)
                {
                    View.WinWatchPublication win = new WinWatchPublication(operatorPostOffice[0]);
                    win.Show();
                    closeWin();
                }
                else
                {
                    MessageBox.Show("Пароль был введен не правильно");
                }
            }
            else
            {
                MessageBox.Show($"Оператора с таким {inputLogin} не существует");
            }

        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            closeWin();
        }

        private void btnPasswordRecovery(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Восстановление пароля");
        }
    }
}
