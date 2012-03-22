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
using System.ComponentModel;

namespace Viewer.Common.Model {

    public enum TrackGroupLevel {
        Year,
        Month,
        Week,
        Day,
        Hour
    };

    
    public interface ITrackStateObserver {

        void TrackChanged(Track track, string propName);
    }


    /// <summary>
    /// 시간/일 단위로 묶여진 track group.
    /// 트랙 목록을 트리로 표현할 때 사용.
    /// 
    /// * 데이터 모델이 아니라 View를 위한 모델이다.
    /// </summary>
    public class TrackGroup : NotificationObject {

        #region fields

        private DateTime m_date;
        private TrackGroupLevel m_level;
        private List<NotificationObject> m_children;
        
        #endregion // fields


        #region constructor

        public TrackGroup(DateTime date, TrackGroupLevel level) {
            m_date = date;
            m_level = level;
            m_children = new List<NotificationObject>();
        }

        #endregion // constructor


        #region properties

        public DateTime Date {
            get { return m_date; }
        }

        public List<NotificationObject> Children {
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

                    foreach (NotificationObject obj in m_children) {
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

        /// <summary>
        /// 트랙 상태 관찰자 설정.
        /// </summary>
        public ITrackStateObserver Observer {
            get { return m_observer; }
            set {
                m_observer = value;
                foreach (object obj in Children) {
                    TrackGroup group = obj as TrackGroup;
                    if (group != null) {
                        group.Observer = value;
                    }
                }
            }
        }
        private ITrackStateObserver m_observer;
        
        #endregion // properties


        #region methods

        public void Add(TrackGroup subGroup) {
            m_children.Add(subGroup);
        }

        public void Add(Track track) {
            m_children.Add(track);
            RegisterTrackEvents(track);
        }

        public void Clear() {
            foreach (object obj in Children) {
                if (obj is Track) {
                    UnregisterTrackEvents((Track)obj);
                } else if (obj is TrackGroup) {
                    ((TrackGroup)obj).Clear();
                }
            }

            m_children.Clear();
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


        #region internal methods

        private void track_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (m_observer != null) {
                m_observer.TrackChanged((Track)sender, e.PropertyName);
            }
        }

        private void RegisterTrackEvents(Track track) {
            track.PropertyChanged += new PropertyChangedEventHandler(track_PropertyChanged);
        }

        private void UnregisterTrackEvents(Track track) {
            track.PropertyChanged -= new PropertyChangedEventHandler(track_PropertyChanged);
        }

        #endregion // internal methods
    }
}
