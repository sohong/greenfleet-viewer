////////////////////////////////////////////////////////////////////////////////
// DeviceSaveHelper.cs
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
using Viewer.Common.Model;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Sd 카드 등 외부 드라이브의 트랙 파일들을 지정한 룰에 따라
    /// 로칼 저장소에 저장한다.
    /// </summary>
    public class DeviceSaveHelper {

        #region constructors

        public DeviceSaveHelper() {
        }

        #endregion // constructors


        #region methods

        public void Save(DeviceRepository source, Repository target, SaveOption options) {
            Debug.Assert(source != null);
            Debug.Assert(target != null);
            Debug.Assert(options != null);

            IEnumerable<Track> m_tracks = null;
            switch (options.Scope) {
            case SaveScope.All:
                m_tracks = source.Tracks;
                break;
            case SaveScope.Selection:
                m_tracks = source.Selection;
                break;
            case SaveScope.Range:
                m_tracks = source.Tracks.Where((t) =>
                    (t.StartTime >= options.StartDate) && (t.StartTime <= options.EndDate));
                break;
            }

            if (m_tracks != null) {
                List<string> files = new List<string>();
                foreach (Track track in m_tracks) {
                    files.Add(track.TrackFile);
                }
                new TrackImportHelper(target).Import(source.Vehicle, files, options.Convert, options.Overwrite);
            }
        }
        
        #endregion // methods
    }
}
