////////////////////////////////////////////////////////////////////////////////
// ImpactView.cs
// 2012.03.13, created by sohong
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
using Viewer.Common.Model;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class AccelerationView : UserControl {

        #region static members

        private static readonly List<TrackPoint> EMPTY_POINTS = new List<TrackPoint>();

        #endregion // static members


        #region dependency properties

        /// <summary>
        /// Track
        /// </summary>
        public static readonly DependencyProperty TrackProperty =
            DependencyProperty.Register(
                "Track",
                typeof(Track),
                typeof(AccelerationView),
                new PropertyMetadata(TrackPropertyChanged));

        private static void TrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            AccelerationView view = (AccelerationView)obj;
            Track track = e.NewValue as Track;
            view.chartMain.DataContext = (track != null) ? (IEnumerable<TrackPoint>)track.Points : EMPTY_POINTS;
        }

        #endregion dependency properties

        
        #region constructor

        public AccelerationView() {
            InitializeComponent();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// chart에 표시할 Track 정보.
        /// </summary>
        public Track Track {
            get { return (Track)GetValue(TrackProperty); }
            set { SetValue(TrackProperty, value); }
        }

        #endregion // properties


        #region event handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            if (chartMain.DataContext == null) {
                chartMain.DataContext = EMPTY_POINTS;
            }
        }

        #endregion // event handlers
    }
}
