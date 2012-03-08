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

namespace Viewer.Model {

    /// <summary>
    /// SD 카드에 저장되는 한 단위의 트랙 정보.
    /// 주행 영상, TrackPoint들로 구성된다.
    /// </summary>
    public class Track : NotificationObject {

        #region fields

        private ObservableCollection<TrackPoint> m_points;
        
        #endregion // fields


        #region constructors

        public Track() {
            m_points = new ObservableCollection<TrackPoint>();
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 생성일시.
        /// </summary>
        public DateTime CreateDate {
            get { return m_createDate; }
            set {
                if (value != m_createDate) {
                    m_createDate = value;
                    RaisePropertyChanged(() => CreateDate);
                }
            }
        }
        private DateTime m_createDate;

        /// <summary>
        /// Track points
        /// </summary>
        public ObservableCollection<TrackPoint> Points {
            get { return m_points; }
        }

        #endregion // properties
    }
}
