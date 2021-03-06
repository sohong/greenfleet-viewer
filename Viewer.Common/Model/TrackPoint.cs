﻿////////////////////////////////////////////////////////////////////////////////
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

namespace Viewer.Common.Model
{
    /// <summary>
    /// Track을 구성하는 한 시점 정보.
    /// </summary>
    public class TrackPoint : NotificationObject
    {
        #region static members

        public static readonly TrackPoint Zero = new TrackPoint();

        #endregion // static members


        #region constructors

        public TrackPoint()
        {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// Sampling 시각.
        /// </summary>
        public DateTime PointTime
        {
            get { return m_pointTime; }
            set
            {
                if (value != m_pointTime) {
                    m_pointTime = value;
                    RaisePropertyChanged(() => PointTime);
                }
            }
        }
        private DateTime m_pointTime;

        /// <summary>
        /// GPS 샘플링 위도.
        /// </summary>
        public double Latitude
        {
            get { return m_lattitude; }
            set
            {
                if (value != m_lattitude) {
                    m_lattitude = value;
                    RaisePropertyChanged(() => Latitude);
                }
            }
        }
        private double m_lattitude = 0.0;

        /// <summary>
        /// GPS 샘플링 경도.
        /// </summary>
        public double Longitude
        {
            get { return m_longitude; }
            set
            {
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
        public double Velocity
        {
            get { return m_velocity; }
            set
            {
                if (value != m_velocity) {
                    m_velocity = value;
                    RaisePropertyChanged(() => Velocity);
                }
            }
        }
        private double m_velocity = 0.0;

        /// <summary>
        /// 감지 충격량 X축
        /// </summary>
        public double AccelerationX
        {
            get { return m_accelerationX; }
            set
            {
                if (value != m_accelerationX) {
                    m_accelerationX = value;
                    RaisePropertyChanged(() => AccelerationX);
                }
            }
        }
        private double m_accelerationX;

        /// <summary>
        /// 감지 충격량 Y축
        /// </summary>
        public double AccelerationY
        {
            get { return m_accelerationY; }
            set
            {
                if (value != m_accelerationY) {
                    m_accelerationY = value;
                    RaisePropertyChanged(() => AccelerationY);
                }
            }
        }
        private double m_accelerationY;

        /// <summary>
        /// 감지 충격량 Z축
        /// </summary>
        public double AccelerationZ
        {
            get { return m_accelerationZ; }
            set
            {
                if (value != m_accelerationZ) {
                    m_accelerationZ = value;
                    RaisePropertyChanged(() => AccelerationZ);
                }
            }
        }
        private double m_accelerationZ;

        #endregion // properties
    }
}
