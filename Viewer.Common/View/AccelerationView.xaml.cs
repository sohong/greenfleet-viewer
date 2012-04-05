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
using System.Collections.ObjectModel;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class AccelerationView : UserControl {

        #region static members
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
            view.m_points.Clear();
        }

        /// <summary>
        /// Position
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(
                "Position",
                typeof(TrackPoint),
                typeof(AccelerationView),
                new PropertyMetadata(PositionPropertyChanged));

        private static void PositionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            AccelerationView view = (AccelerationView)obj;
            TrackPoint point = e.NewValue as TrackPoint;
            view.RefreshPoints(point);
        }

        #endregion dependency properties


        #region fields

        private ObservableCollection<TrackPoint> m_points;
        
        #endregion // fields


        #region constructor

        public AccelerationView() {
            InitializeComponent();
            m_points = new ObservableCollection<TrackPoint>();
            chartMain.DataContext = m_points;
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

        /// <summary>
        /// 재생 포인트
        /// </summary>
        public TrackPoint Position {
            get { return (TrackPoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        #endregion // properties


        #region internal methods

        private void RefreshPoints(TrackPoint current) {
            m_points.Clear();
            if (Track != null) {
                foreach (TrackPoint p in Track.Points) {
                    m_points.Add(p);
                    if (p == current)
                        break;
                }

                for (int i = m_points.Count; i < Track.PointCount; i++) {
                    TrackPoint p = new TrackPoint();
                    p.PointTime = Track[i].PointTime;
                    m_points.Add(p);
                }
            }
        }

        #endregion // internal methods


        #region event handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        }

        #endregion // event handlers
    }
}
