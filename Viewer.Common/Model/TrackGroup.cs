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

namespace Viewer.Common.Model
{
    public enum TrackGroupLevel
    {
        Hour,
        Day,
        Month,
        Year,
        All
    };


    /// <summary>
    /// 시간/일 단위로 묶여진 track group.
    /// 트랙 목록을 트리로 표현할 때 사용.
    /// 
    /// * 데이터 모델이 아니라 View를 위한 모델이다.
    /// </summary>
    public class TrackGroup : NotificationObject
    {
        #region fields

        private TrackGroup m_parent;
        private DateTime m_date;
        private TrackGroupLevel m_level;
        private List<NotificationObject> m_children;
        private int m_updateLock;

        #endregion // fields


        #region events

        public event Action<TrackGroup, Track, string/* propName */> TrackChanged;
        public event Action<TrackGroup> TrackAllChanged;

        #endregion // events


        #region constructor

        public TrackGroup(DateTime date, TrackGroupLevel level)
        {
            m_date = date;
            m_level = level;
            m_children = new List<NotificationObject>();
        }

        #endregion // constructor


        #region properties

        public TrackGroupLevel Level
        {
            get { return m_level; }
        }

        public DateTime Date
        {
            get { return m_date; }
        }

        public List<NotificationObject> Children
        {
            get { return m_children; }
        }

        /// <summary>
        /// 선택 상태. view에서 사용한다.
        /// </summary>
        [Transient]
        public bool IsChecked
        {
            get { return m_checked; }
            set
            {
                //if (value != m_checked) {
                    BeginUpdate();
                    try {
                        m_checked = value;
                        RaisePropertyChanged(() => IsChecked);

                        foreach (NotificationObject obj in m_children) {
                            if (obj is Track) {
                                ((Track)obj).IsChecked = IsChecked;
                            } else if (obj is TrackGroup) {
                                ((TrackGroup)obj).IsChecked = IsChecked;
                            }
                        }
                    } finally {
                        EndUpdate();
                    }
                //}
            }
        }
        private bool m_checked;

        [Transient]
        public bool IsExpanded
        {
            get { return m_expanded; }
            set
            {
                if (value != m_expanded) {
                    m_expanded = value;
                    RaisePropertyChanged(() => IsExpanded);
                }
            }
        }
        private bool m_expanded = false;

        #endregion // properties


        #region methods

        public void Add(TrackGroup subGroup)
        {
            m_children.Add(subGroup);
            subGroup.m_parent = this;
        }

        public void Add(Track track)
        {
            m_children.Add(track);
            track.m_group = this;
            RegisterTrackEvents(track);
        }

        public void Clear()
        {
            foreach (object obj in Children) {
                if (obj is Track) {
                    ((Track)obj).m_group = null;
                    UnregisterTrackEvents((Track)obj);
                } else if (obj is TrackGroup) {
                    ((TrackGroup)obj).Clear();
                    ((TrackGroup)obj).m_parent = null;
                }
            }

            m_children.Clear();
        }

        public void BeginUpdate()
        {
            m_updateLock++;
        }

        public void EndUpdate()
        {
            m_updateLock = Math.Max(0, m_updateLock - 1);
            if (!IsLocked()) {
                Action<TrackGroup> eh = TrackAllChanged;
                if (eh != null) {
                    eh(this);
                }
            }
        }

        #endregion // methods


        #region overriden methods

        public override string ToString()
        {
            switch (m_level) {
                case TrackGroupLevel.All:
                    return m_date.ToString("All");
                case TrackGroupLevel.Year:
                    return m_date.ToString("yyyy");
                case TrackGroupLevel.Month:
                    return m_date.ToString("yyyy-MM");
                case TrackGroupLevel.Day:
                    return m_date.ToString("yyyy-MM-dd");
                case TrackGroupLevel.Hour:
                    return m_date.ToString("yyyy-MM-dd HH시");
            }

            return m_date.ToString();
        }

        #endregion // overriden methods


        #region internal methods

        private bool IsLocked()
        {
            return m_updateLock > 0 || (m_parent != null && m_parent.IsLocked());
        }

        private void RegisterTrackEvents(Track track)
        {
            track.PropertyChanged += new PropertyChangedEventHandler(track_PropertyChanged);
        }

        private void UnregisterTrackEvents(Track track)
        {
            track.PropertyChanged -= new PropertyChangedEventHandler(track_PropertyChanged);
        }

        private void track_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!IsLocked()) {
                FireTrackChangeEvent(this, (Track)sender, e.PropertyName);
            }
        }

        private void FireTrackChangeEvent(TrackGroup group, Track track, string propName)
        {
            Action<TrackGroup, Track, string> eh = TrackChanged;
            if (eh != null) {
                eh(group, track, propName);
            }

            if (m_parent != null) {
                m_parent.FireTrackChangeEvent(this, track, propName);
            }
        }

        #endregion // internal methods
    }
}
