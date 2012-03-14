////////////////////////////////////////////////////////////////////////////////
// TrackImportHelper.cs
// 2012.03.12, created by sohong
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
using System.IO;

namespace Viewer.Personal.Model {

    /// <summary>
    /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
    /// </summary>
    public class TrackImportHelper {

        #region static methods

        /// <summary>
        /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// files에는 확장자 없는 파일명들이 들어있다.
        /// </summary>
        public static void Import(IEnumerable<string> files, Repository repository) {
            foreach (string file in files) {
                ImportTrackFile(file, repository);
            }
        }

        #endregion // static methods


        #region internal methods

        /// <summary>
        /// file명에 해당하는 log, inc, 264 파일들을 해당하는 스토리지에 복사한다.
        /// 264파일은 mp4파일로 변환하여 저장한다.
        /// 264파을을 삭제하지는 않는다.
        /// </summary>
        /// <param name="file"></param>
        private static void ImportTrackFile(string file, Repository repository) {
            string path = Path.Combine(file, ".inc");
            if (File.Exists(path)) {
            }
        }

        #endregion // internal methods
    }
}
