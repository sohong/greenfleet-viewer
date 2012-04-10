////////////////////////////////////////////////////////////////////////////////
// Repository.cs
// 2012.04.10, created by sohong
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
using System.Windows.Data;
using System.ComponentModel;
using Viewer.Common.Model;
using System.Collections.ObjectModel;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Repository base.
    /// </summary>
    public abstract class Repository : NotificationObjectEx {

        #region fields

        private string m_name;
        private ObservableCollection<Track> m_tracks;

        #endregion // fields


        #region constructor

        public Repository(string name) {
            m_name = name;
            m_tracks = new ObservableCollection<Track>();
        }

        #endregion // constructor


        #region properties

        public IEnumerable<Track> Tracks {
            get { return m_tracks; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks() {
            ListCollectionView view = new ListCollectionView(m_tracks);
            view.SortDescriptions.Add(new SortDescription("CreateDate", ListSortDirection.Ascending));
            return view;
        }

        /// <summary>
        /// 트랙 목록으로부터 트랙 그룹 hierarchy를 생성한다.
        /// 트랙들은 CreateDate 순서로 정렬되었다고 가정한다.
        /// </summary>
        /// <param name="tracks"></param>
        /// <returns></returns>
        public TrackGroup CreateGroupsFromTracks(ListCollectionView tracks) {
            if (tracks.IsEmpty)
                return null;

            IList<TrackGroup> groups = new List<TrackGroup>();
            TrackGroup group = null;

            for (int i = 0, count = tracks.Count; i < count; i++) {
                Track track = (Track)tracks.GetItemAt(i);

                if (group == null || track.CreateDate.Hour != group.Date.Hour) {
                    group = new TrackGroup(track.CreateDate, TrackGroupLevel.Hour);
                    groups.Add(group);
                }

                group.Add(track);
            }

            TrackGroup root = CreateGroupHierarchy(groups);
            return root;
        }

        #endregion // methods


        #region overriden methods

        public override string ToString() {
            return m_name + " Repository";
        }

        #endregion // overriden methods


        #region internal properties

        protected ObservableCollection<Track> TrackList {
            get { return m_tracks; }
        }

        #endregion // internal properties


        #region internal methods

        private TrackGroup CreateGroupHierarchy(IList<TrackGroup> groups) {
            IList<TrackGroup> parents = new List<TrackGroup>();
            TrackGroup parent = CreateParent(groups[0]);
            parent.Add(groups[0]);
            parents.Add(parent);

            for (int i = 1, count = groups.Count; i < count; i++) {
                if (!Containable(parent, groups[i])) {
                    parent = CreateParent(groups[i]);
                    parents.Add(parent);
                }
                parent.Add(groups[i]);
            }

            return parents.Count > 1 ? CreateGroupHierarchy(parents) : parent;
        }

        private TrackGroup CreateParent(TrackGroup group) {
            TrackGroupLevel level = (TrackGroupLevel)(group.Level + 1);
            TrackGroup parent = new TrackGroup(group.Date, level);
            return parent;
        }

        private bool Containable(TrackGroup parent, TrackGroup group) {
            switch (parent.Level) {
            case TrackGroupLevel.Day:
                return group.Date.Day == parent.Date.Day;
            case TrackGroupLevel.Month:
                return group.Date.Month == parent.Date.Month;
            case TrackGroupLevel.Year:
                return group.Date.Year == parent.Date.Year;
            }

            return true;
        }

        private TrackGroup CreateRoot(Track startTrack, Track endTrack) {
            TrackGroupLevel level;
            DateTime dStart = startTrack.CreateDate;
            DateTime dEnd = endTrack.CreateDate;

            if (dStart.Year != dEnd.Year) {
                level = TrackGroupLevel.All;
            } else if (dStart.Month != dEnd.Month) {
                level = TrackGroupLevel.Year;
            } else if (dStart.Day != dEnd.Day) {
                level = TrackGroupLevel.Month;
            } else {
                level = TrackGroupLevel.Day;
            }

            TrackGroup root = new TrackGroup(startTrack.CreateDate, level);
            return root;
        }

        #endregion // internal methods
    }
}
