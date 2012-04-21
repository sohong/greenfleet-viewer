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
using System.Collections.Specialized;
using Viewer.Common.UI.Timeline;

namespace Viewer.Common.View
{
    /// <summary>
    /// Interaction logic for TimelineView.xaml
    /// </summary>
    public partial class TimelineView : UserControl
    {
        #region dependency properties

        /// <summary>
        /// Tracks
        /// </summary>
        public static readonly DependencyProperty TracksProperty = DependencyProperty.Register(
            "Tracks", typeof(TrackCollection), typeof(TimelineView),
            new FrameworkPropertyMetadata(null, OnTracksChanged));
        private static void OnTracksChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).UnregisterTracksEvents(a.OldValue);
            ((TimelineView)d).RefreshView();
            ((TimelineView)d).RegisterTracksEvents(a.NewValue);
        }

        /// <summary>
        /// All range background
        /// </summary>
        public static readonly DependencyProperty AllBackgroundProperty = DependencyProperty.Register(
            "AllBackground", typeof(Brush), typeof(TimelineView),
            new FrameworkPropertyMetadata(Brushes.Blue, OnAllBackgroundChanged));
        private static void OnAllBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).RefreshView();
        }

        /// <summary>
        /// Event range background
        /// </summary>
        public static readonly DependencyProperty EventBackgroundProperty = DependencyProperty.Register(
            "EventBackground", typeof(Brush), typeof(TimelineView),
            new FrameworkPropertyMetadata(Brushes.Red, OnEventBackgroundChanged));
        private static void OnEventBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).RefreshView();
        }

        /// <summary>
        /// Current track
        /// </summary>
        public static readonly DependencyProperty CurrentTrackProperty = DependencyProperty.Register(
            "CurrentTrack", typeof(Track), typeof(TimelineView),
            new FrameworkPropertyMetadata(null, OnCurrentTrackChanged));
        private static void OnCurrentTrackChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).ResetTracker();
        }

        /// <summary>
        /// Current track point
        /// </summary>
        public static readonly DependencyProperty CurrentPointProperty = DependencyProperty.Register(
            "CurrentPoint", typeof(TrackPoint), typeof(TimelineView),
            new FrameworkPropertyMetadata(null, OnCurrentPointChanged));
        private static void OnCurrentPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).ResetTracker();
        }

        #endregion // dependency properties


        #region construcotr

        public TimelineView()
        {
            InitializeComponent();
        }

        #endregion // construcotr


        #region properties

        public TrackCollection Tracks
        {
            get { return (TrackCollection)GetValue(TracksProperty); }
            set { SetValue(TracksProperty, value); }
        }

        public Brush AllBackground
        {
            get { return (Brush)GetValue(AllBackgroundProperty); }
            set { SetValue(AllBackgroundProperty, value); }
        }

        public Brush EventBackground
        {
            get { return (Brush)GetValue(EventBackgroundProperty); }
            set { SetValue(EventBackgroundProperty, value); }
        }

        public Track CurrentTrack
        {
            get { return (Track)GetValue(CurrentTrackProperty); }
            set { SetValue(CurrentTrackProperty, value); }
        }

        public TrackPoint CurrentPoint
        {
            get { return (TrackPoint)GetValue(CurrentPointProperty); }
            set { SetValue(CurrentPointProperty, value); }
        }

        #endregion // properties


        #region methods

        public void SetPosition(Track track, TrackPoint point)
        {
            bar.SetPosition(track, point);
        }

        #endregion // methods


        #region internal methods

        private void tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshView();
        }

        private void RegisterTracksEvents(object source)
        {
            INotifyCollectionChanged coll = source as INotifyCollectionChanged;
            if (coll != null) {
                coll.CollectionChanged += new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }
        }

        private void UnregisterTracksEvents(object source)
        {
            INotifyCollectionChanged coll = source as INotifyCollectionChanged;
            if (coll != null) {
                coll.CollectionChanged -= new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }
        }

        private void RefreshView()
        {
            if (Tracks == null) return;

            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (Tracks.First != null) {
                start = Tracks.First.StartTime;
            }
            if (Tracks.Last != null) {
                end = Tracks.Last.EndTime;
            }

            AxisLabelProvider labels = new AxisLabelProvider();
            labels.BuildLabels(start, end);

            TimelineValueCollection values = new TimelineValueCollection(start, end);
            values.Build(Tracks);

            bar.AllBackground = this.AllBackground;
            bar.EventBackground = this.EventBackground;
            bar.RefreshBar(values, labels);
        }

        private void ResetTracker()
        {
            bar.SetPosition(CurrentTrack, CurrentPoint);
        }

        #endregion // internal methods
    }
}
