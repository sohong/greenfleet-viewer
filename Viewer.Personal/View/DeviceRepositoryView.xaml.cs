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
using Microsoft.Windows.Controls;
using Viewer.Personal.Model;
using Viewer.Common.Event;
using Viewer.Common.Model;
using Viewer.Personal.Event;
using Viewer.Common.View;

namespace Viewer.Personal.View {

    /// <summary>
    /// 입력 장치(SD 카드 등)에 저장된 트랙 정보를 표시하고 관리한다.
    /// </summary>
    public partial class DeviceRepositoryView : UserControl {

        #region constructors

        public DeviceRepositoryView() {
            InitializeComponent();
        }

        #endregion // constructors


        #region event handlers

        // trackTreeView
        private void TrackTreeView_ActivateGroup(object sender, TrackGroup group) {
        }

        private void TrackTreeView_ActivateTrack(object sender, Track track) {
            PersonalDomain.Domain.EventAggregator.GetEvent<DeviceTrackActivatedEvent>().Publish(track);
        }

        // videoView
        private void VideoView_PositionChanged(VideoView view, double length, double position) {  
            // video track 위치가 변경되면 해당하는 track point를 찾아 전역 이벤트를 발생시킨다.
            Track track = view.Track;
            TrackPoint point = track.FindPoint(position);
            if (point != null) {
                PersonalDomain.Domain.EventAggregator.GetEvent<TrackPointChangedEvent>().Publish(point);
            }
        }

        // googleMapView
        private void GoogleMapView_TrackDoubleClicked(object sender, Track track) {
            PersonalDomain.Domain.EventAggregator.GetEvent<TrackActivatedEvent>().Publish(track);
        }

        // bingMapView
        private void BingMapView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Debug.WriteLine("xxx");
        }

        #endregion // event handlers
    }
}
