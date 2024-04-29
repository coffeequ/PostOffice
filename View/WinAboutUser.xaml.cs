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
    /// Логика взаимодействия для WinAboutUser.xaml
    /// </summary>
    public partial class WinAboutUser : Window
    {
        Model.DataBasePostOffice dataBasePostOffice;

        User User;

        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public WinAboutUser(User user)
        {
            InitializeComponent();

            DataContext = user;

            User = user;

            tbPassword.Password = user.Password;

            dataBasePostOffice = new Model.DataBasePostOffice(MainWindow.postOfficeEntity);
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();

            var list = dataBasePostOffice.postOfficeEntities.LogIO.ToList();

            if (list.Count() <= 0)
            {
                LogIO logIO = new LogIO();

                logIO.ExitTime = DateTime.Now;

                logIO.id_User = User.id_User;

                dataBasePostOffice.postOfficeEntities.LogIO.Add(logIO);

                dataBasePostOffice.postOfficeEntities.SaveChanges();

                closeWin();

                Close();

                return;
            }

            list[list.Count() - 1].ExitTime = DateTime.Now;

            dataBasePostOffice.postOfficeEntities.SaveChanges();
            
            closeWin();

            Close();
        }
    }
}
