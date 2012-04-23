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
using Viewer.Common.UI;
using Viewer.Common.Model;
using System.Collections.ObjectModel;

namespace Viewer.Common.View
{
    /// <summary>
    /// AccelerationChart testing view
    /// </summary>
    public partial class AccelerationChartView : UserControl
    {
        #region dependency properties

        /// <summary>
        /// Track
        /// </summary>
        public static readonly DependencyProperty TrackProperty =
            DependencyProperty.Register(
                "Track",
                typeof(Track),
                typeof(AccelerationChartView),
                new PropertyMetadata(TrackPropertyChanged));

        private static void TrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AccelerationChartView view = (AccelerationChartView)obj;
            Track track = e.NewValue as Track;
            view.RefreshPoints(null);
        }

        /// <summary>
        /// Position
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(
                "Position",
                typeof(TrackPoint),
                typeof(AccelerationChartView),
                new PropertyMetadata(PositionPropertyChanged));

        private static void PositionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AccelerationChartView view = (AccelerationChartView)obj;
            TrackPoint point = e.NewValue as TrackPoint;
            view.RefreshPoints(point);
        }

        #endregion dependency properties


        #region fields
        #endregion // fields


        public AccelerationChartView()
        {
            InitializeComponent();
        }


        #region properties

        /// <summary>
        /// chart에 표시할 Track 정보.
        /// </summary>
        public Track Track
        {
            get { return (Track)GetValue(TrackProperty); }
            set { SetValue(TrackProperty, value); }
        }

        /// <summary>
        /// 재생 포인트
        /// </summary>
        public TrackPoint Position
        {
            get { return (TrackPoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        #endregion // properties


        #region internal methods

        private void RefreshPoints(TrackPoint current)
        {
            chart.Clear();
            chart.Title = "";

            if (Track != null && Track.PointCount > 0 && Position != null) {
                foreach (TrackPoint p in Track.Points) {
                    chart.AddValue(p.PointTime, p.AccelerationX, p.AccelerationY, p.AccelerationZ);
                    if (p == current)
                        break;
                }

                chart.Title = Track.First.PointTime.ToString("yyyy-MM-dd hh:mm:ss") + " ~ " +
                    Track.Last.PointTime.ToString("yyyy-MM-dd hh:mm:ss");
            } else {
                chart.Title = "";
                chart.Clear();
            }
        }

        #endregion // internal methods
    }
}
