////////////////////////////////////////////////////////////////////////////////
// TrackPoint.cs
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

namespace Viewer.Common.Model {

    /// <summary>
    /// Track을 구성하는 한 지점 정보.
    /// </summary>
    public class TrackPoint : NotificationObject {

        #region constructors

        public TrackPoint() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 위도.
        /// </summary>
        public double Lattitude {
            get { return m_lattitude; }
            set {
                if (value != m_lattitude) {
                    m_lattitude = value;
                    RaisePropertyChanged(() => Lattitude);
                }
            }
        }
        private double m_lattitude = 0.0;

        /// <summary>
        /// 경도.
        /// </summary>
        public double Longitude {
            get { return m_longitude; }
            set {
                if (value != m_longitude) {
                    m_longitude = value;
                    RaisePropertyChanged(() => Longitude);
                }
            }
        }
        private double m_longitude = 0.0;

        /// <summary>
        /// 가속도.
        /// </summary>
        public double Velocity {
            get { return m_velocity; }
            set {
                if (value != m_velocity) {
                    m_velocity = value;
                    RaisePropertyChanged(() => Velocity);
                }
            }
        }
        private double m_velocity = 0.0;

        #endregion // properties
    }
}
