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

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class BingMapView : UserControl {

        #region fields

        private MapPolygon m_region;
        private MapPolyline m_route;
        private List<Location> m_locations;
        
        #endregion // fields


        #region constructor

        public BingMapView() {
            InitializeComponent();

            CreateRegion();
            CreateRoutes();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// chart에 표시할 Track 정보.
        /// </summary>
        public Track Track {
            set {
                if (value != m_track) {
                    m_track = value;
                    RefreshLocations();
                    RefreshRegion();
                    RefreshRoutes();
                    RefreshPins();
                }
            }
        }
        private Track m_track;
        
        #endregion // properties


        #region internal methods

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

        private void RefreshLocations() {
            if (m_locations == null) {
                m_locations = new List<Location>();
            } else {
                m_locations.Clear();
            }

            if (m_track != null) {
                foreach (TrackPoint p in m_track.Points) {
                    Location loc = new Location(p.Lattitude, p.Longitude);
                    m_locations.Add(loc);
                }
            }
        }

        private void RefreshRegion() {
        }

        private void RefreshRoutes() {
        }

        private void RefreshPins() {
            pinLayer.Children.Clear();
            foreach (Location loc in m_locations) {
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pinLayer.Children.Add(pin);
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
