////////////////////////////////////////////////////////////////////////////////
// SaveOption.cs
// 2012.03.27, created by sohong
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
using Viewer.Common;

namespace Viewer.Personal.Model {

    public enum SaveScope {
        All,
        Selection,
        Range
    }

    /// <summary>
    /// Sd 카드 파일들을 저장소로 저장할 때 사용자가 지정할 수 있는 옵션.
    /// </summary>
    public class SaveOption : NotificationObjectEx {

        #region constructors

        public SaveOption() {

            Scope = SaveScope.All;
            Convert = true;
            Overwrite = false;
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 저장 범위
        /// </summary>
        public SaveScope Scope {
            get { return m_scope; }
            set {
                if (value != m_scope) {
                    m_scope = value;
                    RaisePropertyChanged(() => Scope);
                }
            }
        }
        private SaveScope m_scope;

        public bool IsAll {
            get { return m_scope == SaveScope.All; }
            set { this.m_scope = SaveScope.All; }
        }

        public bool IsSelection {
            get { return m_scope == SaveScope.Selection; }
            set { this.m_scope = SaveScope.Selection; }
        }

        public bool IsRange {
            get { return m_scope == SaveScope.Range; }
            set { this.m_scope = SaveScope.Range; }
        }

        /// <summary>
        /// 범위 지정 저장시 시작 일시.
        /// </summary>
        public DateTime StartDate {
            get { return m_startDate; }
            set {
                if (value != m_startDate) {
                    m_startDate = value;
                    RaisePropertyChanged(() => StartDate);
                }
            }
        }
        private DateTime m_startDate;

        /// <summary>
        /// 범위 지정 저장시 끝 일시.
        /// </summary>
        public DateTime EndDate {
            get { return m_endDate; }
            set {
                if (value != m_endDate) {
                    m_endDate = value;
                    RaisePropertyChanged(() => EndDate);
                }
            }
        }
        private DateTime m_endDate;

        /// <summary>
        /// 저장하면서 .264파일을 mp4로 변환할 것인가?
        /// </summary>
        public bool Convert {
            get { return m_convert; }
            set {
                if (value != m_convert) {
                    m_convert = value;
                    RaisePropertyChanged(() => Convert);
                }
            }
        }
        private bool m_convert;

        /// <summary>
        /// 같은 파일이 존재하면 덮어 쓸 것인가?
        /// </summary>
        public bool Overwrite {
            get { return m_overwrite; }
            set {
                if (value != m_overwrite) {
                    m_overwrite = value;
                    RaisePropertyChanged(() => Overwrite);
                }
            }
        }
        private bool m_overwrite;

        #endregion // properties
    }
}
