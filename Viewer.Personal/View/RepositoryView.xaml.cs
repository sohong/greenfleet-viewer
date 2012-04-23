////////////////////////////////////////////////////////////////////////////////
// RepositoryView.cs
// 2012.03.09, created by sohong
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
using System.ComponentModel.Composition;
using Viewer.Personal.ViewModel;
using Viewer.Common.Model;
using Viewer.Personal.Model;
using Viewer.Personal.Event;
using Viewer.Common.View;
using Viewer.Common.Event;

namespace Viewer.Personal.View
{
    /// <summary>
    /// Device/Local Repository view.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class RepositoryView : UserControl
    {
        #region constructors

        public RepositoryView()
        {
            InitializeComponent();
        }

        #endregion // constructors


        #region properties

        [Import]
        public RepositoryViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        #endregion // properties


        #region internal properties

        private bool IsLocal
        {
            get { return tabMain.SelectedIndex == 1; }
        }

        #endregion // internal properties


        #region event handlers

        // tabMain
        private void tabMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLocal) {
                //chkAll.Visibility = Visibility.Collapsed;
                btnOpen.Visibility = Visibility.Collapsed;
                dateFrom.Visibility = Visibility.Visible;
                txtTilde.Visibility = Visibility.Visible;
                dateTo.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;
                panMode.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Collapsed;
            } else {
                //chkAll.Visibility = Visibility.Visible;
                btnOpen.Visibility = Visibility.Visible;
                dateFrom.Visibility = Visibility.Collapsed;
                txtTilde.Visibility = Visibility.Collapsed;
                dateTo.Visibility = Visibility.Collapsed;
                btnSearch.Visibility = Visibility.Collapsed;
                panMode.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        // trackTreeView
        private void TrackTreeView_ActivateGroup(object sender, TrackGroup group)
        {
        }

        private void TrackTreeView_ActivateTrack(object sender, Track track)
        {
            PersonalDomain.Domain.EventAggregator.GetEvent<TrackActivatedEvent>().Publish(track);
        }

        // videoView
        private void VideoView_PositionChanged(VideoView view, double length, double position)
        {
            // video track 위치가 변경되면 해당하는 track point를 찾아 전역 이벤트를 발생시킨다.
            Track track = view.Track;
            if (track != null) {
                TrackPoint point = track.FindPoint(position);
                if (point != null) {
                    PersonalDomain.Domain.EventAggregator.GetEvent<TrackPointChangedEvent>().Publish(point);
                }
            }
        }

        // googleMapView
        private void GoogleMapView_TrackDoubleClicked(object sender, Track track)
        {
            PersonalDomain.Domain.EventAggregator.GetEvent<TrackActivatedEvent>().Publish(track);
        }

        // bingMapView
        private void BingMapView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("xxx");
        }

        #endregion // event handlers
    }
}
