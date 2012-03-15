﻿////////////////////////////////////////////////////////////////////////////////
// TrackFolderManager.cs
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
using System.Text.RegularExpressions;
using System.IO;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Repository track 파일들이 저장되는 폴더 구조를 관리한다.
    /// 
    /// (** 아래와 같은 계층구조를 갖는다.
    ///   YYYY -> MM -> DD
    ///              -> DD
    ///              -> DD
    ///        -> MM -> DD
    ///              -> DD
    ///   YYYY -> MM
    ///        -> MM
    ///   YYYY
    /// ********************************)
    /// </summary>
    public class TrackFolderManager {

        #region fields

        private Repository m_owner;

        #endregion // fields


        #region constructor

        public TrackFolderManager(Repository owner) {
            Debug.Assert(owner != null, "owner is null");
            m_owner = owner;
        }

        #endregion // constructor


        #region methods

        /// <summary>
        /// 트랙 파일이 repository에 이미 존재하는 지 리턴한다.
        /// </summary>
        public bool ExistsFile(string trackFile) {
            return false;
        }

        /// <summary>
        /// track file이 저장될 폴더명을 리턴한다.
        /// relative가 true이면 repository root 상대 경로로 리턴한다.
        /// 기존하지 않으면 생성한 후 리턴한다.
        /// </summary>
        public string GetFolder(string trackFile, bool relative) {
            string folder = ParseFolder(trackFile);

            if (folder != null) {
                string path = Path.Combine(m_owner.RootPath, folder);
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                if (!relative) {
                    folder = path;
                }
            }

            return folder;
        }

        #endregion // methods


        #region internal methods

        private string ParseFolder(string trackFile) {
            Match match = Repository.TRACK_DATE_PATTERN.Match(trackFile);
            if (match.Success) {
                string[] arr = match.Value.Split('_');
                string folder = arr[0] + '\\' + arr[1] + '\\' + arr[2];
                return folder;
            }
            return null;
        }

        #endregion // internal methods
    }
}