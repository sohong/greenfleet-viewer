////////////////////////////////////////////////////////////////////////////////
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

namespace Viewer.Common.Model {

    /// <summary>
    /// Track collection
    /// </summary>
    public class TrackCollection : ObservableCollection<Track> {

        #region constructors

        public TrackCollection() {
        }

        #endregion // constructors


        #region properties

        public Track First {
            get;
            private set;
        }

        public Track Last {
            get;
            private set;
        }

        /// <summary>
        /// 전체 기간을 분단위 값으로 리턴
        /// </summary>
        public long Length {
            get {
                if (First != null) {
                    return GetPosition(Last) + 1;
                } else {
                    return 0;
                }
            }
        }
        
        #endregion // properties


        #region methods

        /// <summary>
        /// 시작/끝 등을 다시 계산한다.
        /// </summary>
        public void Refresh() {
            if (Count < 1) {
                First = Last = null;
            } else if (Count == 1) {
                First = Last = Items[0];
            } else {
                Calculate();
            }
        }

        public long GetPosition(Track track) {
            return (long)new TimeSpan(track.StartTime.StripSeconds().Ticks - First.StartTime.StripSeconds().Ticks).TotalMinutes;
        }

        #endregion // methods


        #region overriden methods

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);
            Refresh();
        }
        
        #endregion // overriden methods


        #region overriden methods

        private void Calculate() {
            Track first = Items[0];
            Track last = Items[0];
            for (int i = 1; i < Count; i++) {
                if (Items[i].StartTime < first.StartTime) {
                    first = Items[i];
                } else if (Items[i].StartTime > last.StartTime) {
                    last = Items[i];
                }
            }

            this.First = first;
            this.Last = last;
        }

        #endregion // overriden methods
    }
}
