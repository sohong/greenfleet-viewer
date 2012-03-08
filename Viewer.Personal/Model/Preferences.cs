////////////////////////////////////////////////////////////////////////////////
// Preferences.cs
// 2012.03.07, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Viewer.Common.Model;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Personal viewer 사용자 설정 정보들.
    /// </summary>
    public class Preferences : NotificationObject {

        #region constructors

        public Preferences() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 트랙 파일 저장소 루트 경로.
        /// </summary>
        public string StorageRoot {
            get { return m_storageRoot; }
            set {
                if (value != m_storageRoot) {
                    m_storageRoot = value;
                    RaisePropertyChanged(() => StorageRoot);
                }
            }
        }
        private string m_storageRoot = @"C:\GreenFleet\storage";

        /// <summary>
        /// Retention 적용 방법.
        /// </summary>
        public RetentionMode RetentionMode {
            get { return m_retentionMode; }
            set {
                if (value != m_retentionMode) {
                    m_retentionMode = value;
                    RaisePropertyChanged(() => RetentionMode);
                }
            }
        }
        private RetentionMode m_retentionMode;

        /// <summary>
        /// 보존 기간. YY,MM,DD로 지정.
        /// </summary>
        public string Retention {
            get { return m_retention; }
            set {
                if (value != m_retention) {
                    m_retention = value;
                    RaisePropertyChanged(() => Retention);
                }
            }
        }
        private string m_retention = "00,00,00";

        #endregion // properties
    }
}
