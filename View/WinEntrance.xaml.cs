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
        Model.DataBasePostOffice dataBasePostOffice;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        private List<OperatorPostOffice> AllOperatorPostOffices;

        public LogIO logIO;

        private List<LogIO> allLog;

        public WinEntrance()
        {
            InitializeComponent();

            try
            {
                dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

            AllOperatorPostOffices = dataBasePostOffice.postOfficeEntities.OperatorPostOffice.ToList();

            allLog = dataBasePostOffice.postOfficeEntities.LogIO.ToList();
        }


        private void btnEntrance(object sender, RoutedEventArgs e)
        {
            string inputLogin = tbLogin.Text.Trim();

            string inputPassword = tbPassword.Password.Trim();

            var operatorPostOffice = dataBasePostOffice.postOfficeEntities.User.ToList().Where(personal => personal.Login == inputLogin).ToList();

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

                    logIO = new LogIO()
                    {
                        id_User = operatorPostOffice[0].id_User,
                        EntryTime = DateTime.Now
                    };
                    dataBasePostOffice.postOfficeEntities.LogIO.Add(logIO);
                    dataBasePostOffice.postOfficeEntities.SaveChanges();
                    win.Show();
                    closeWin();
                    Close();
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

        private void btnPasswordRecovery(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Восстановление пароля");
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
