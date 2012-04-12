////////////////////////////////////////////////////////////////////////////////
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
    ///   vehicle1 -> YYYY -> MM -> DD
    ///                          -> DD
    ///                          -> DD
    ///                    -> MM -> DD
    ///                          -> DD
    ///               YYYY -> MM
    ///                    -> MM
    ///               YYYY
    ///   vehicle2 -> YYYY ...
    /// ********************************)
    ///  각 vehicle 폴더 아래 카타로그 파일들이 생성된다.
    /// 
    /// </summary>
    public class TrackFolderManager {

        #region fields

        private LocalRepository m_owner;

        #endregion // fields


        #region constructor

        public TrackFolderManager(LocalRepository owner) {
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

        public string GetRoot(Vehicle vehicle) {
            return Path.Combine(m_owner.RootPath, vehicle.VehicleId);
        }

        /// <summary>
        /// track file이 저장될 폴더명을 리턴한다.
        /// relative가 true이면 repository root 상대 경로로 리턴한다.
        /// 기존하지 않으면 생성한 후 리턴한다.
        /// </summary>
        public string GetFolder(Vehicle vehicle, string trackFile, bool relative) {
            string folder = Path.Combine(vehicle.VehicleId, ParseFolder(trackFile));

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

        /// <summary>
        /// root로 지정된 vehicle 폴더 내에서 date에 해당하는 폴더를 절대경로로 리턴한다.
        /// 폴더가 존재하지 않으면 null을 리턴한다.
        /// </summary>
        public string DateTimeToFolder(string root, DateTime date) {
            string folder = string.Format(@"{0:0000}\{1:00}\{2:00}", date.Year, date.Month, date.Day);
            return Path.Combine(root, folder);
        }

        /// <summary>
        /// 가장 최근에 등록된 트랙파일 일자를 리턴한다. 
        /// </summary>
        /// <returns></returns>
        public DateTime FindRecentDay() {
            DateTime d = DateTime.Today;

            return d;
        }

        #endregion // methods


        #region internal methods

        private string ParseFolder(string trackFile) {
            Match match = LocalRepository.TRACK_DATE_PATTERN.Match(trackFile);
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
