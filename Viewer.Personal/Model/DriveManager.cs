////////////////////////////////////////////////////////////////////////////////
// DriveManager.cs
// 2012.03.22, created by sohong
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
using Viewer.Common.Util;
using System.IO;

namespace Viewer.Personal.Model {

    /// <summary>
    /// 외부 입력 장치(SD 카드등) 관련...
    /// </summary>
    public class DriveManager : IDriveManager {

        #region IDriveManager

        /// <summary>
        /// (외부 입력 장치로 연결된)드라이브들 중 greenfleet 트랙 데이터를 포함했는 지
        /// 확인해서 포함된 첫번째 드라이브의 데이터 폴더를 리턴한다.
        /// TODO 여러 드라이브들 중 하나를 선택할 수 있도록...
        /// 
        /// (테스트를 위해)준비된 상태의 Removable 드라이브가 존재하지 않으면
        /// x, y, z 드라이브 중 하나를 Removable로 인식한다.
        /// </summary>
        /// <returns></returns>
        public string FindTrackDrive(bool testing) {
            DriveInfo[] drives = DriveUtil.GetDrives();
            foreach (DriveInfo drive in drives) {
                Logger.Debug(drive.Name + ": " + drive.DriveType + ", " + drive.IsReady);

                if (drive.IsReady) {
                    string name = drive.Name;
                    if (drive.DriveType == DriveType.Removable || (testing && (name.StartsWith("X") || name.StartsWith("Y") || name.StartsWith("Z")))) {
                        string path = CheckDrive(name);
                        if (path != null)
                            return path;
                    }
                }
            }

            return null;
        }

        #endregion // IDriveManager


        #region internal methods

        /// <summary>
        /// 드라이브 루트 바로 아래 gfdata 폴더가 존재하면 track 데이터 드라이브라고 간주한다.
        /// TODO 스펙이 정해지면 반영할 것!
        /// </summary>
        private string CheckDrive(string root) {
            string path = Path.Combine(root, "gfdata");
            if (Directory.Exists(path)) {
                return path;
            }
            return null;
        }

        #endregion // internal methods
    }
}
