////////////////////////////////////////////////////////////////////////////////
// MainWindow.cs
// 2012.03.12, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Viewer.Personal.ViewModel;
using Viewer.Personal.Model;

namespace Viewer.Personal.Tester {

    /// <summary>
    /// </summary>
    public partial class MainWindow : Window {
    
        public MainWindow() {
            InitializeComponent();

            PersonalDomain.Domain.Start();

            Width = SystemParameters.PrimaryScreenWidth * 2 / 3;
            Height = SystemParameters.PrimaryScreenHeight * 3 / 4;
            Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;

            repoView.DataContext = new RepositoryViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
        }
    }
}
