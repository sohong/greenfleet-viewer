////////////////////////////////////////////////////////////////////////////////
// Track.cs
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
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace Viewer.Common.Model {

    /// <summary>
    /// SD 카드에 저장되는 한 단위의 트랙 정보.
    /// 주행 영상, TrackPoint들로 구성된다.
    /// </summary>
    public class Track : NotificationObject {

        #region fields

        private ObservableCollection<TrackPoint> m_points;
        private ObservableCollection<TrackImpulse> m_impulses;
        
        #endregion // fields


        #region constructors

        public Track() {
            m_points = new ObservableCollection<TrackPoint>();
            m_impulses = new ObservableCollection<TrackImpulse>();
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 시작 시각.
        /// </summary>
        public DateTime StartTime {
            get { return m_startTime; }
            set {
                if (value != m_startTime) {
                    m_startTime = value;
                    RaisePropertyChanged(() => StartTime);
                }
            }
        }
        private DateTime m_startTime;

        /// <summary>
        /// 종료 시각.
        /// </summary>
        public DateTime EndTime {
            get { return m_endTime; }
            set {
                if (value != m_endTime) {
                    m_endTime = value;
                    RaisePropertyChanged(() => EndTime);
                }
            }
        }
        private DateTime m_endTime;

        /// <summary>
        /// 동영상 파일 경로.
        /// </summary>
        public string MovieFile { get; set; }

        /// <summary>
        /// Track points
        /// </summary>
        public ObservableCollection<TrackPoint> Points {
            get { return m_points; }
        }

        /// <summary>
        /// Track Impulses
        /// </summary>
        public ObservableCollection<TrackImpulse> Impulses {
            get { return m_impulses; }
        }

        #endregion // properties
    }
}
