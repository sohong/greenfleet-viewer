////////////////////////////////////////////////////////////////////////////////
// DashboardView.cs
// 2012.04.05, created by sohong
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

namespace Viewer.Common.View
{
    /// <summary>
    /// 날짜, 시간, 위치, 속도 등을 표시한다.
    /// </summary>
    public partial class DashboardView : UserControl
    {
        #region dependency properties

        /// <summary>
        /// DateTime
        /// </summary>
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register(
                "DateTime",
                typeof(DateTime),
                typeof(DashboardView),
                new PropertyMetadata(DateTimePropertyChanged));

        private static void DateTimePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DashboardView view = (DashboardView)obj;
            DateTime d = (DateTime)e.NewValue;
            view.RefreshDateTime();
        }

        /// <summary>
        /// 속도
        /// </summary>
        public static readonly DependencyProperty VelocityProperty =
            DependencyProperty.Register(
                "Velocity",
                typeof(double),
                typeof(DashboardView),
                new PropertyMetadata(VelocityPropertyChanged));

        private static void VelocityPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DashboardView view = (DashboardView)obj;
            double v = (double)e.NewValue;
            view.RefreshVelocity();
        }

        /// <summary>
        /// 위도
        /// </summary>
        public static readonly DependencyProperty LatitudeProperty =
            DependencyProperty.Register(
                "Latitude",
                typeof(double),
                typeof(DashboardView),
                new PropertyMetadata(LatitudePropertyChanged));

        private static void LatitudePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DashboardView view = (DashboardView)obj;
            double v = (double)e.NewValue;
            view.RefreshLocation();
        }

        /// <summary>
        /// 경도
        /// </summary>
        public static readonly DependencyProperty LongitudeProperty =
            DependencyProperty.Register(
                "Longitude",
                typeof(double),
                typeof(DashboardView),
                new PropertyMetadata(LongitudePropertyChanged));

        private static void LongitudePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DashboardView view = (DashboardView)obj;
            double v = (double)e.NewValue;
            view.RefreshLocation();
        }

        /// <summary>
        /// 진행 방향을 0 ~ 360으로.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(
                "Direction",
                typeof(double),
                typeof(DashboardView),
                new PropertyMetadata(DirectionPropertyChanged));

        private static void DirectionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DashboardView view = (DashboardView)obj;
            double v = (double)e.NewValue;
            view.RefreshDirection();
        }

        #endregion // dependency properties


        #region fields
        #endregion // fields


        #region constructor

        public DashboardView()
        {
            InitializeComponent();

            DateTime = DateTime.Today;
            Direction = 0;
            Velocity = 0;
            Latitude = 37.1;
            Longitude = 112.1;
        }

        #endregion // constructor


        #region properties

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public double Velocity
        {
            get { return (double)GetValue(VelocityProperty); }
            set { SetValue(VelocityProperty, value); }
        }

        public double Latitude
        {
            get { return (double)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }

        public double Longitude
        {
            get { return (double)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }

        public double Direction
        {
            get { return (double)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        #endregion // properties


        #region internal methods

        private void RefreshDateTime()
        {
            txtDate.Content = DateTime.ToString("yyyy-MM-dd");
            txtTime.Content = DateTime.ToString("hh:mm:ss tt");
        }

        private void RefreshLocation()
        {
            txtLatitude.Content = this.Latitude.ToString("0000.0000 N");
            txtLongitude.Content = this.Longitude.ToString("0000.0000 E");
        }

        private void RefreshDirection()
        {
            txtDirection.Content = this.Direction.ToString("000.00");
        }

        private void RefreshVelocity()
        {
            txtVelocity.Content = this.Velocity.ToString("000");
            speedometer.Value = this.Velocity;
        }

        #endregion // internal methods
    }
}
