﻿////////////////////////////////////////////////////////////////////////////////
// DeviceRepositoryViewModel.cs
// 2012.04.02, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Viewer.Common.Model
{
    /// <summary>
    /// Track collection
    /// </summary>
    public class TrackCollection : ObservableCollection<Track>
    {
        #region fields

        private IList<TrackRange> m_ranges;
        private bool m_locked;

        #endregion // fields


        #region constructors

        public TrackCollection()
        {
            m_ranges = new List<TrackRange>();
        }

        #endregion // constructors


        #region properties

        public Track First
        {
            get;
            private set;
        }

        public Track Last
        {
            get;
            private set;
        }

        /// <summary>
        /// 전체 기간을 분단위 값으로 리턴
        /// </summary>
        public long Length
        {
            get
            {
                if (First != null) {
                    return GetPosition(Last) + 1;
                } else {
                    return 0;
                }
            }
        }

        public IEnumerable<TrackRange> Ranges
        {
            get { return m_ranges; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// 시작/끝 등을 다시 계산한다.
        /// </summary>
        public void Refresh()
        {
            m_ranges.Clear();

            if (Count < 1) {
                First = Last = null;
            } else if (Count == 1) {
                First = Last = Items[0];
                m_ranges.Add(new TrackRange(First.TrackType) {
                    StartTrack = First,
                    EndTrack = Last
                });
            } else {
                Calculate();
            }
        }

        public long GetPosition(Track track)
        {
            return (long)new TimeSpan(track.StartTime.Ticks - First.StartTime.Ticks).TotalMinutes;
        }

        /// <summary>
        /// 매개변수 t를 시작 시각으로 하는 Track을 찾아 리턴한다.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Track GetTrackAt(DateTime t)
        {
            t = t.StripSeconds();
            foreach (Track track in this) {
                if (track.StartTime.StripSeconds() == t) {
                    return track;
                }
            }

            return null;
        }

        public void BeginUpdate()
        {
            m_locked = true;
        }

        public void EndUpdate()
        {
            if (m_locked) {
                m_locked = false;
                Refresh();
                NotifyCollectionChangedEventArgs ea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                base.OnCollectionChanged(ea);
            }
        }

        #endregion // methods


        #region overriden methods

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!m_locked) {
                Refresh();
                base.OnCollectionChanged(e);
            }
        }

        #endregion // overriden methods


        #region overriden methods

        private void Calculate()
        {
            Track first = Items[0];
            Track last = Items[0];
            TrackRange range = new TrackRange(first.TrackType) { StartTrack = first };

            for (int i = 1; i < Count; i++) {
                Track track = Items[i];

                if (track.StartTime < first.StartTime) {
                    first = track;
                } else if (track.StartTime > last.StartTime) {
                    last = track;
                }

                if (track.TrackType != range.TrackType) {
                    range.EndTrack = Items[i - 1];
                    m_ranges.Add(range);
                    range = new TrackRange(track.TrackType) { StartTrack = track };
                }
            }

            range.EndTrack = Items[Count - 1];
            m_ranges.Add(range);
            this.First = first;
            this.Last = last;
        }

        #endregion // overriden methods
    }
}
