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
        
        #endregion // fields


        #region constructors

        public Track() {
            m_points = new ObservableCollection<TrackPoint>();
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 단말기 id
        /// </summary>
        public string TerminalId {
            get { return m_terminalId; }
            set {
                if (value != m_terminalId) {
                    m_terminalId = value;
                    RaisePropertyChanged(() => TerminalId);
                }
            }
        }
        private string m_terminalId;

        /// <summary>
        /// 차량 id
        /// </summary>
        public string VehicleId {
            get { return m_vehicleId; }
            set {
                if (value != m_vehicleId) {
                    m_vehicleId = value;
                    RaisePropertyChanged(() => VehicleId);
                }
            }
        }
        private string m_vehicleId;

        /// <summary>
        /// 운전자 id
        /// </summary>
        public string DriverId {
            get { return m_driverId; }
            set {
                if (value != m_driverId) {
                    m_driverId = value;
                    RaisePropertyChanged(() => DriverId);
                }
            }
        }
        private string m_driverId;

        /// <summary>
        /// 파일 생성 일시.
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

        #endregion // properties
    }
}
