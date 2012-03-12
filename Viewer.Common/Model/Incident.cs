﻿////////////////////////////////////////////////////////////////////////////////
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
    /// 사고 데이터.
    /// </summary>
    public class Incident : NotificationObject {

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

        /// <summary>
        /// GPS 샘플링 경도.
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
        /// GPS 샘플링 속도.
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

        /// <summary>
        /// 감지 충격량 절대값.
        /// </summary>
        public double Impluse {
            get { return m_impulse; }
            set {
                if (value != m_impulse) {
                    m_impulse = value;
                    RaisePropertyChanged(() => Impluse);
                }
            }
        }
        private double m_impulse;

        /// <summary>
        /// 감지 충격량 X축
        /// </summary>
        public double ImpulseX {
            get { return m_impulseX; }
            set {
                if (value != m_impulseX) {
                    m_impulseX = value;
                    RaisePropertyChanged(() => ImpulseX);
                }
            }
        }
        private double m_impulseX;

        /// <summary>
        /// 감지 충격량 Y축
        /// </summary>
        public double ImpulseY {
            get { return m_impulseY; }
            set {
                if (value != m_impulseY) {
                    m_impulseY = value;
                    RaisePropertyChanged(() => ImpulseY);
                }
            }
        }
        private double m_impulseY;

        /// <summary>
        /// 감지 충격량 Z축
        /// </summary>
        public double ImpulseZ {
            get { return m_impulseZ; }
            set {
                if (value != m_impulseZ) {
                    m_impulseZ = value;
                    RaisePropertyChanged(() => ImpulseZ);
                }
            }
        }
        private double m_impulseZ;

        #endregion // properties
    }
}
