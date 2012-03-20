////////////////////////////////////////////////////////////////////////////////
// TrackGroup.cs
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
using Viewer.Common.Xml;
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.Model {

    public enum TrackGroupLevel {
        Year,
        Month,
        Week,
        Day,
        Hour
    };

    /// <summary>
    /// 시간/일 단위로 묶여진 track group.
    /// 트랙 목록을 트리로 표현할 때 사용.
    /// </summary>
    public class TrackGroup : NotificationObject {

        #region fields

        private DateTime m_date;
        private TrackGroupLevel m_level;
        private List<object> m_children;
        
        #endregion // fields


        #region constructor

        public TrackGroup(DateTime date, TrackGroupLevel level) {
            m_date = date;
            m_level = level;
            m_children = new List<object>();
        }

        #endregion // constructor


        #region properties

        public DateTime Date {
            get { return m_date; }
        }

        public List<object> Children {
            get { return m_children; }
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

                    foreach (object obj in m_children) {
                        if (obj is Track) {
                            ((Track)obj).IsChecked = IsChecked;
                        } else if (obj is TrackGroup) {
                            ((TrackGroup)obj).IsChecked = IsChecked;
                        }
                    }
                }
            }
        }
        private bool m_checked;
        
        #endregion // properties


        #region methods

        public void Add(TrackGroup subGroup) {
            m_children.Add(subGroup);
        }

        public void Add(Track track) {
            m_children.Add(track);
        }

        #endregion // methods


        #region overriden methods

        public override string ToString() {
            switch (m_level) {
            case TrackGroupLevel.Year:
                return m_date.ToString("yyyy");
            case TrackGroupLevel.Month:
                return m_date.ToString("yyyy-MM");
            case TrackGroupLevel.Week:
            case TrackGroupLevel.Day:
                return m_date.ToString("yyyy-MM-dd");
            case TrackGroupLevel.Hour:
                return m_date.ToString("yyyy-MM-dd HH시");
            }

            return m_date.ToString();
        }

        #endregion // overriden methods
    }
}
