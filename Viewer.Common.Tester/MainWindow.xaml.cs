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
using Viewer.Common.Util;

namespace Viewer.Common.Tester {

    /// <summary>
    /// </summary>
    public partial class MainWindow : Window {

        #region constructors 

        public MainWindow() {
            InitializeComponent();
        }

        #endregion // constructors


        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            LogUtil.InitLog4Net("Viewer.Common.Tester");

            //videoView.Source = new Uri(@".\v.mp4", UriKind.RelativeOrAbsolute);
            videoView.Source = new Uri(@"w.wmv", UriKind.RelativeOrAbsolute);
        }

        #endregion // event handlers
    }
}
