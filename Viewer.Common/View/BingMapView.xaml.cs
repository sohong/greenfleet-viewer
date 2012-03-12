////////////////////////////////////////////////////////////////////////////////
// BingMapView.cs
// 2012.03.07, created by sohong
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
using Microsoft.Maps.MapControl.WPF;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class BingMapView : UserControl {

        #region constructor

        public BingMapView() {
            InitializeComponent();
        }

        #endregion // constructor


        #region event handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Location loc = new Location(37.6, 127);
            mapView.Center = loc;
        }

        #endregion // event handlers
    }
}
