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
using Microsoft.Practices.Prism.Events;
using Viewer.Common.Util;

namespace Viewer.Personal.Tester {

    /// <summary>
    /// </summary>
    public partial class MainWindow : Window {

        #region constructors

        public MainWindow() {
            InitializeComponent();

            PersonalDomain.Domain.EventAggregator = new EventAggregator();
            PersonalDomain.Domain.Start();

            Width = SystemParameters.PrimaryScreenWidth * 2 / 3;
            Height = SystemParameters.PrimaryScreenHeight * 3 / 4;
            Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;

            this.DataContext = new PersonalViewModel();

            deviceView.DataContext = new DeviceRepositoryViewModel();
            repoView.DataContext = new RepositoryViewModel();
        }

        #endregion // constructors


        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Logger.InitLog4Net("Viewer.Personal.Tester");
            Logger.Debug("Starting tester...");
        }

        #endregion // event handlers
    }
}
