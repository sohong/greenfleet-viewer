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
using Viewer.Common.Model;
using Viewer.Common.Loader;

namespace Viewer.Common.Tester {

    /// <summary>
    /// </summary>
    public partial class MainWindow : Window {

        #region constructors 

        public MainWindow() {
            InitializeComponent();
        }

        #endregion // constructors


        #region internal methods

        private Track LoadTrack() {
            string source = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"sample\all_2012_03_11_20_37_31.inc");
            Track track = new LocalTrackLoader().Load(source, true);
            return track;
        }
        
        #endregion // internal methods


        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            LogUtil.InitLog4Net("Viewer.Common.Tester");
            Track track = LoadTrack();

            // video
            videoView.Track = track;

            // bing map
            bingView.Track = track;

            // google map

            // chart
            accelView.Track = track;
        }

        #endregion // event handlers
    }
}
