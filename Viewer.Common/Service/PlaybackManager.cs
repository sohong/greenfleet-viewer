////////////////////////////////////////////////////////////////////////////////
// PlaybackManager.cs
// 2012.04.23, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viewer.Common.Model;
using System.Windows.Data;
using System.ComponentModel;

namespace Viewer.Common.Service
{
    /// <summary>
    /// 하나 이상의 트랙을 재생한다.
    /// </summary>
    public class PlaybackManager
    {
        #region fields

        private ListCollectionView m_tracks;

        #endregion // fields


        #region constructors

        public PlaybackManager(TrackCollection tracks)
        {
            m_tracks = new ListCollectionView(tracks);
            m_tracks.SortDescriptions.Add(new SortDescription("CreateDate", ListSortDirection.Ascending));
        }

        #endregion // constructors


        #region methods

        public Track GetFirst()
        {
            return m_tracks.Count > 0 ? (Track)m_tracks.GetItemAt(0) : null;
        }

        public Track GetNext(Track track, bool isAll, bool isLoop) {
            if (isAll) {
                int index = m_tracks.IndexOf(track);
                if (index < m_tracks.Count - 1) {
                    track = (Track)m_tracks.GetItemAt(index + 1);
                } else if (isLoop) {
                    track = (Track)m_tracks.GetItemAt(0);
                } else {
                    track = null;
                }
            } else {
                track = null;
            }
            return track;
        }

        #endregion // methods
    }
}
