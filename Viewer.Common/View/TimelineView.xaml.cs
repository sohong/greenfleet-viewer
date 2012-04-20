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

        public static readonly DependencyProperty TracksProperty = DependencyProperty.Register(
            "Tracks", typeof(TrackCollection), typeof(TimelineView),
            new FrameworkPropertyMetadata(null, OnTracksChanged));
        private static void OnTracksChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineView)d).UnregisterTracksEvents(a.OldValue);
            ((TimelineView)d).RefreshView();
            ((TimelineView)d).RegisterTracksEvents(a.NewValue);
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

        #endregion // properties


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

            bar.RefreshData(values, labels); 
        }

        #endregion // internal methods
    }
}
