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
using Viewer.Common.Xml;

namespace Viewer.Common.Model {

    public enum TrackType {
        All,    // 상시
        Event,  // 이벤트
        Dummy   // 가짜
    };

    /// <summary>
    /// SD 카드에 저장되는 한 단위의 트랙 정보.
    /// 주행 영상, TrackPoint들로 구성된다.
    /// </summary>
    public class Track : NotificationObject {

        #region fields

        private IList<TrackPoint> m_points;
        private string m_id;
        
        #endregion // fields


        #region constructors

        public Track() {
            m_points = new List<TrackPoint>();
            m_id = Guid.NewGuid().ToString();
        }

        #endregion // constructors


        #region properties

        [Transient]
        public string Id {
            get { return m_id; }
        }

        /// <summary>
        /// 상시 or 이벤트.
        /// </summary>
        public TrackType TrackType {
            get { return m_trackType; }
            set {
                if (value != m_trackType) {
                    m_trackType = value;
                    RaisePropertyChanged(() => TrackType);
                }
            }
        }
        private TrackType m_trackType;

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
        /// 트랙 파일 경로.
        /// </summary>
        [Transient]
        public string TrackFile { get; set; }

        /// <summary>
        /// 동영상 원본 파일 경로.
        /// </summary>
        public string VideoFile { get; set; }

        /// <summary>
        /// 실제 출력할 변환된 동영상 파일 경로.
        /// </summary>
        public string MpegFile { get; set; }

        /// <summary>
        /// Track points
        /// </summary>
        public IEnumerable<TrackPoint> Points {
            get { return m_points; }
        }

        public int PointCount {
            get { return m_points.Count; }
        }

        public TrackPoint this[int index] {
            get { return m_points[index]; }
        }

        public TrackPoint First {
            get { return m_points.Count > 0 ? m_points[0] : null; }
        }

        public TrackPoint Last {
            get { return m_points.Count > 0 ? m_points[m_points.Count - 1] : null; }
        }

        /// <summary>
        /// 선택 상태. view에서 사용한다.
        /// </summary>
        [Transient]
        public bool IsChecked {
            get { return m_checked; }
            set {
                if (value != m_checked) {
                    m_checked = value;
                    RaisePropertyChanged(() => IsChecked);
                }
            }
        }
        private bool m_checked;

        #endregion // properties


        #region methods

        public void AddPoint(TrackPoint p) {
            if (p != null && !m_points.Contains(p)) {
                m_points.Add(p);

                if (m_points.Count == 1) {
                    StartTime = EndTime = m_points[0].PointTime;
                } else {
                    EndTime = m_points[PointCount - 1].PointTime;
                }
            }
        }

        public TrackPoint FindPoint(double miliseconds) {
            foreach (TrackPoint p in m_points) {
                double term = TimeSpan.FromTicks(p.PointTime.Ticks - StartTime.Ticks).TotalMilliseconds;
                if (miliseconds < term)
                    return p;
            }
            return null;
        }

        #endregion // methods


        #region overriden methods

        public override string ToString() {
            return "[" + TrackType + "] " + CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion // overriden methods
    }
}
