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

namespace Viewer.Common.View {

    /// <summary>
    /// AccelerationChart testing view
    /// </summary>
    public partial class AccelerationChartView : UserControl {

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

        private static void TrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
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

        private static void PositionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            AccelerationChartView view = (AccelerationChartView)obj;
            TrackPoint point = e.NewValue as TrackPoint;
            view.RefreshPoints(point);
        }

        #endregion dependency properties


        #region fields

        private ObservableCollection<TrackPoint> m_points;

        #endregion // fields

        
        public AccelerationChartView() {
            InitializeComponent();

            m_points = new ObservableCollection<TrackPoint>();
        }


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
            chart.Clear();

            if (Track != null && Position != null) {
                foreach (TrackPoint p in Track.Points) {
                    m_points.Add(p);

                    chart.AddValue(p.AccelerationX, p.AccelerationY, p.AccelerationZ);

                    if (p == current)
                        break;
                }
            }
        }

        #endregion // internal methods
    }
}
