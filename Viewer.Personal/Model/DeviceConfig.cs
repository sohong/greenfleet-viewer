////////////////////////////////////////////////////////////////////////////////
// DeviceConfig.cs
// 2012.03.26, created by sohong
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
using Viewer.Common;

namespace Viewer.Personal.Model {

    /// <summary>
    /// 기기 설정 정보.
    /// </summary>
    public class DeviceConfig : NotificationObjectEx {

        #region constructors

        public DeviceConfig() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 녹화 해상도
        /// </summary>
        public int RecordingResolution {
            get { return m_recordingResolution; }
            set {
                if (value != m_recordingResolution) {
                    m_recordingResolution = value;
                    RaisePropertyChanged(() => RecordingResolution);
                }
            }
        }
        private int m_recordingResolution;

        /// <summary>
        /// 녹화 화질
        /// </summary>
        public int RecordingQuality {
            get { return m_recordingQuality; }
            set {
                if (value != m_recordingQuality) {
                    m_recordingQuality = value;
                    RaisePropertyChanged(() => RecordingQuality);
                }
            }
        }
        private int m_recordingQuality;

        /// <summary>
        /// 전송 해상도
        /// </summary>
        public int TransmitResolution {
            get { return m_transmitResolution; }
            set {
                if (value != m_transmitResolution) {
                    m_transmitResolution = value;
                    RaisePropertyChanged(() => TransmitResolution);
                }
            }
        }
        private int m_transmitResolution;

        /// <summary>
        /// 전송 화질
        /// </summary>
        public int TransmitQuality {
            get { return m_transmitQuality; }
            set {
                if (value != m_transmitQuality) {
                    m_transmitQuality = value;
                    RaisePropertyChanged(() => TransmitQuality);
                }
            }
        }
        private int m_transmitQuality;

        /// <summary>
        /// AP SSID
        /// </summary>
        public string ApSsid {
            get { return m_apSsid; }
            set {
                if (value != m_apSsid) {
                    m_apSsid = value;
                    RaisePropertyChanged(() => ApSsid);
                }
            }
        }
        private string m_apSsid;

        /// <summary>
        /// AP Key
        /// </summary>
        public string ApKey {
            get { return m_apKey; }
            set {
                if (value != m_apKey) {
                    m_apKey = value;
                    RaisePropertyChanged(() => ApKey);
                }
            }
        }
        private string m_apKey;

        /// <summary>
        /// Client Ap SSID
        /// </summary>
        public string ClientApSsid {
            get { return m_clientApSsid; }
            set {
                if (value != m_clientApSsid) {
                    m_clientApSsid = value;
                    RaisePropertyChanged(() => ClientApSsid);
                }
            }
        }
        private string m_clientApSsid;

        /// <summary>
        /// Client Ap Key
        /// </summary>
        public string ClientApKey {
            get { return m_clientKey; }
            set {
                if (value != m_clientKey) {
                    m_clientKey = value;
                    RaisePropertyChanged(() => ClientApKey);
                }
            }
        }
        private string m_clientKey;

        #endregion // properties


        #region methods

        public DeviceConfig Clone() {
            return (DeviceConfig)base.Clone();
        }

        #endregion // methods
    }
}
