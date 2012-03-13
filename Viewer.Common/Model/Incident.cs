////////////////////////////////////////////////////////////////////////////////
// Incident.cs
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
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.Model {

    /// <summary>
    /// 사고 트랙.
    /// 주행 영상, TrackPoint들로 구성된다.
    /// </summary>
    public class Incident : Track {

        #region constructors

        public Incident() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 이벤트 id.
        /// </summary>
        public string EventId {
            get { return m_eventId; }
            set {
                m_eventId = value;
                RaisePropertyChanged(() => EventId);
            }
        }
        private string m_eventId;

        /// <summary>
        /// 샘플링 시각.
        /// </summary>
        public DateTime IncidentTime {
            get { return m_incidentTime; }
            set {
                m_incidentTime = value;
                RaisePropertyChanged(() => IncidentTime);
            }
        }
        private DateTime m_incidentTime;

        #endregion // properties
    }
}
