﻿using System;
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
    /// Логика взаимодействия для PageAboutUser.xaml
    /// </summary>
    public partial class PageAboutUser : Page
    {
        public delegate void CloseWin();

        public static event CloseWin closeWin;

        public PageAboutUser(User user)
        {
            InitializeComponent();

            DataContext = user;

            tbPassword.Password = user.Password;
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            closeWin();
        }
    }
}