////////////////////////////////////////////////////////////////////////////////
// GoogleMapView.cs
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
using Viewer.Common.Model;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Viewer.Common.View {

    /// <summary>
    /// Interaction logic for GoogleMapView.xaml
    /// </summary>
    public partial class GoogleMapView : UserControl {

        #region dependency properties

        /// <summary>
        /// Track
        /// </summary>
        public static readonly DependencyProperty ActiveTrackProperty =
            DependencyProperty.Register(
                "ActiveTrack",
                typeof(Track),
                typeof(GoogleMapView),
                new PropertyMetadata(ActiveTrackPropertyChanged));

        private static void ActiveTrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            GoogleMapView view = (GoogleMapView)obj;
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
                typeof(GoogleMapView),
                new PropertyMetadata(TracksPropertyChanged));

        private static void TracksPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            GoogleMapView view = (GoogleMapView)obj;
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
        //private MapPolygon m_region;
        //private MapPolyline m_route;
        //private List<Location> m_locations;

        #endregion // fields


        #region events

        public event Action<object, Track> TrackClicked;
        public event Action<object, Track> TrackDoubleClicked;

        #endregion // events


        #region constructor

        public GoogleMapView() {
            InitializeComponent();

            m_tracks = new List<Track>();

            MapScriptHelper helper = new MapScriptHelper(this);
            browser.ObjectForScripting = helper;

            if (!DesignerProperties.GetIsInDesignMode(this)) {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
                browser.Navigate(path);
            }
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// map에 표시할 Track 정보.
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
            /*
            m_region = new MapPolygon();
            m_region.Locations = new LocationCollection();
            m_region.Fill = new SolidColorBrush(Colors.Blue);
            m_region.Stroke = new SolidColorBrush(Colors.Green);
            m_region.StrokeThickness = 1;
            m_region.Opacity = 0.1;
            m_region.FillRule = FillRule.Nonzero;

            trackLayer.Children.Add(m_region);
             */
        }

        private void CreateRoutes() {
        }

        private void RefreshLocations(Track track) {
            /*
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
             */
        }

        private void RefreshRegion(Track track) {
        }

        private void RefreshRoutes(Track track) {
        }

        private void RefreshPins() {
            ClearPins();
            /*
            pinLayer.Children.Clear();
            foreach (Location loc in m_locations) {
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pinLayer.Children.Add(pin);
            }
             */
            foreach (Track track in m_tracks) {
                AddPin(track);
            }
        }

        private void AddPin(Track track) {
            if (track.Points.Count > 0) {
                TrackPoint p = track.Points[0];
                browser.InvokeScript("addMarker", track.Id, p.Lattitude, p.Longitude);
            }
        }

        private void ClearPins() {
            browser.InvokeScript("clearMarkers");
        }

        private void MarkerClicked(string trackId) {
            Track track = FindTrack(trackId);
            if (track != null) {
                MessageBox.Show("marker clicked at " + track.Id, "map");
                Action<object, Track> eh = TrackClicked;
                if (eh != null) {
                    eh(this, track);
                }
            }
        }

        private void MarkerDoubleClicked(string trackId) {
            Track track = FindTrack(trackId);
            if (track != null) {
                MessageBox.Show("marker double clicked at " + track.Id, "map");
                Action<object, Track> eh = TrackDoubleClicked;
                if (eh != null) {
                    eh(this, track);
                }
            }
        }

        private Track FindTrack(string trackId) {
            Track track = m_tracks.First((t) => {
                return t.Id.Equals(trackId);
            });
            return track;
        }

        #endregion // internal methods


        #region event handlers 

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        }

        #endregion // event handlers


        /// <summary>
        /// GoogleMap javascript helper
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        [ComVisible(true)]
        public class MapScriptHelper {

            #region fields

            private GoogleMapView m_view;

            #endregion // fields


            #region constructors

            public MapScriptHelper(GoogleMapView view) {
                m_view = view;
            }

            #endregion // constructors


            #region javascript functions 

            public void MarkerDblClicked(string track) {
                m_view.MarkerDoubleClicked(track);
            }
        
            #endregion javascript functions
        }
    }
}
