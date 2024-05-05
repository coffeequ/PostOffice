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
using System.Windows.Shapes;

namespace PostOffice.View
{
    /// <summary>
    /// Логика взаимодействия для WinMoreDetailsSubscriber.xaml
    /// </summary>
    public partial class WinMoreDetailsSubscriber : Window
    {
        User user;

        public WinMoreDetailsSubscriber(SubscriberOfThePostOffice subscriberOfThePostOffice, User user)
        {
            InitializeComponent();

            View.PageMoreDetailsSubscriber.closeWin += PageMoreDetailsSubscriber_closeWin;

            MyFrame.NavigationService.Navigate(new View.PageMoreDetailsSubscriber(subscriberOfThePostOffice, user));
        }

        private void PageMoreDetailsSubscriber_closeWin()
        {
            Close();
        }

        private void F1Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ProcHelp procHelp = new Model.ProcHelp("Справка.chm");

            procHelp.CallHelp();
        }
    }
}
