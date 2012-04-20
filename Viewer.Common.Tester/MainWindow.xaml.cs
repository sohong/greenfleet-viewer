////////////////////////////////////////////////////////////////////////////////
// MainWindow.cs
// 2012.03.12, created by sohong
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
using Viewer.Common.Util;
using Viewer.Common.Model;
using Viewer.Common.Loader;

namespace Viewer.Common.Tester
{
    /// <summary>
    /// MainWindow for Viewer.Common.Tester Application.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion // constructors


        #region internal methods

        private Track LoadTrack()
        {
            string source = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"sample\all_2012_03_11_20_37_31.inc");
            Track track = new LocalTrackLoader().Load(source, true);
            return track;
        }

        private TrackCollection LoadTracks()
        {
            TrackCollection tracks = new TrackCollection();

            Track track;
            for (int i = 0; i < 30; i++) {
                track = new Track() { StartTime = new DateTime(2012, 3, 1, 11, i, 0), EndTime = new DateTime(2012, 3, 1, 11, i, 59) };
                tracks.Add(track);
            }

            for (int i = 0; i < 20; i++) {
                track = new Track() { StartTime = new DateTime(2012, 3, 1, 12, 10 + i, 0), EndTime = new DateTime(2012, 3, 1, 12, 10 + i, 59) };
                tracks.Add(track);
            }

            for (int i = 0; i < 20; i++) {
                track = new Track() { StartTime = new DateTime(2012, 3, 1, 13, 11 + i, 0), EndTime = new DateTime(2012, 3, 1, 13, 11 + i, 59) };
                track.TrackType = TrackType.Event;
                tracks.Add(track);
            }

            for (int i = 0; i < 20; i++) {
                track = new Track() { StartTime = new DateTime(2012, 3, 1, 13, 31 + i, 0), EndTime = new DateTime(2012, 3, 1, 13, 31 + i, 59) };
                tracks.Add(track);
            }

            return tracks;
        }

        #endregion // internal methods


        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.InitLog4Net("Viewer.Common.Tester");
            Track track = LoadTrack();
            TrackCollection tracks = LoadTracks();

            // video
            videoView.Track = track;

            // bing map
            bingView.ActiveTrack = track;

            // google map

            // chart
            accelView.Track = track;
            accelerationView.Track = track;

            // timeline
            timelineView.Tracks = tracks;
        }

        #endregion // event handlers
    }
}
