////////////////////////////////////////////////////////////////////////////////
// BingMapView.cs
// 2012.03.07, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
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
using Viewer.Common.Model;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class BingMapView : UserControl {

        #region dependency properties

        /// <summary>
        /// Active Track
        /// </summary>
        public static readonly DependencyProperty ActiveTrackProperty =
            DependencyProperty.Register(
                "ActiveTrack",
                typeof(Track),
                typeof(BingMapView),
                new PropertyMetadata(ActiveTrackPropertyChanged));

        private static void ActiveTrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            BingMapView view = (BingMapView)obj;
            Track track = e.NewValue as Track;
            view.SetActive(track);
        }

        /// <summary>
        /// Track List
        /// </summary>
        public static readonly DependencyProperty TracksProperty =
            DependencyProperty.Register(
                "Tracks",
                typeof(IEnumerable),
                typeof(BingMapView),
                new PropertyMetadata(TracksPropertyChanged));

        private static void TracksPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            BingMapView view = (BingMapView)obj;
            view.ResetTracks(e.OldValue as ObservableCollection<Track>, e.NewValue as ObservableCollection<Track>);
        }

        private void ResetTracks(ObservableCollection<Track> oldTracks, ObservableCollection<Track> tracks) {
            if (oldTracks != null) {
                oldTracks.CollectionChanged -= new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }

            if (tracks != null) {
                tracks.CollectionChanged += new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }
        }

        private void tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
            case NotifyCollectionChangedAction.Add:
                foreach (Track track in e.NewItems) {
                    AddTrack(track);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                foreach (Track track in e.OldItems) {
                    RemoveTrack(track);
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                Clear();
                foreach (Track track in e.NewItems) {
                    AddTrack(track);
                }
                break;
            }
        }

        #endregion dependency properties

        
        #region fields

        private List<Track> m_tracks;
        private Track m_activeTrack;
        private MapPolygon m_region;
        //private MapPolyline m_route;
        private List<Location> m_locations;
        
        #endregion // fields


        #region constructor

        public BingMapView() {
            InitializeComponent();

            m_tracks = new List<Track>();
            CreateRegion();
            CreateRoutes();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// 현재 재생 중인 트랙.
        /// </summary>
        public Track ActiveTrack {
            get { return (Track)GetValue(ActiveTrackProperty); }
            set { SetValue(ActiveTrackProperty, value); }
        }

        /// <summary>
        /// map에 표시할 Track 정보들.
        /// </summary>
        public IEnumerable Tracks {
            get { return (IEnumerable)GetValue(TracksProperty); }
            set { SetValue(TracksProperty, value); }
        }
        
        #endregion // properties


        #region methods

        public bool AddTrack(Track track) {
            if (track != null && !m_tracks.Contains(track)) {
                m_tracks.Add(track);
                
                //RefreshLocations(track);
                //RefreshRegion(track);
                //RefreshRoutes(track);
                RefreshPins();

                return true;
            }
            return false;
        }

        public void RemoveTrack(Track track) {
            if (track != null && m_tracks.Contains(track)) {
                m_tracks.Remove(track);

                RefreshPins();
            }
        }

        public void Clear() {
            foreach (Track track in m_tracks) {
                RemoveTrack(track);
            }
        }

        #endregion // methods


        #region internal methods

        private void SetActive(Track track) {
            if (track != m_activeTrack) {
                ClearActive();
                m_activeTrack = track;
                if (AddTrack(m_activeTrack)) {
                }
            }
        }

        private void ClearActive() {
            if (m_activeTrack != null) {
            }
        }

        private void CreateRegion() {
            m_region = new MapPolygon();
            m_region.Locations = new LocationCollection();
            m_region.Fill = new SolidColorBrush(Colors.Blue);
            m_region.Stroke = new SolidColorBrush(Colors.Green);
            m_region.StrokeThickness = 1;
            m_region.Opacity = 0.1;
            m_region.FillRule = FillRule.Nonzero;

            trackLayer.Children.Add(m_region);
        }

        private void CreateRoutes() {
        }

        private void RefreshLocations(Track track) {
            if (m_locations == null) {
                m_locations = new List<Location>();
            } else {
                m_locations.Clear();
            }

            if (track != null) {
                foreach (TrackPoint p in track.Points) {
                    Location loc = new Location(p.Lattitude, p.Longitude);
                    m_locations.Add(loc);
                }
            }
        }

        private void RefreshRegion(Track track) {
        }

        private void RefreshRoutes(Track track) {
        }

        private void RefreshPins() {
            pinLayer.Children.Clear();
            /*
            foreach (Location loc in m_locations) {
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pinLayer.Children.Add(pin);
            }
             */
            foreach (Track track in m_tracks) {
                if (track.PointCount > 0) {
                    TrackPoint p = track[0];
                    Pushpin pin = new Pushpin();
                    pin.Location = new Location(p.Lattitude, p.Longitude);
                    pinLayer.Children.Add(pin);
                }
            }
        }

        #endregion // internal methods


        #region event handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Location loc = new Location(37.6, 127);
            mapView.Center = loc;
        }

        private void mapView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            e.Handled = true;

            Point p = e.GetPosition(this);
            Location loc = mapView.ViewportPointToLocation(p);

            /*
            m_region.Locations.Add(loc);

            Rectangle r = new Rectangle();
            r.Width = 8;
            r.Height = 8;
            r.Fill = new SolidColorBrush(Colors.Red);
            r.Stroke = new SolidColorBrush(Colors.Yellow);
            r.StrokeThickness = 1;

            trackLayer.AddChild(r, loc);
             */
        }

        #endregion // event handlers
    }
}
