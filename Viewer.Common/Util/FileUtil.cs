////////////////////////////////////////////////////////////////////////////////
// FileUtil.cs
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
using System.IO;

namespace Viewer.Common.Util {

    /// <summary>
    /// File 관련 유틸리티들.
    /// </summary>
    public class FileUtil {

        /// <summary>
        /// 실행 파일 경로.
        /// </summary>
        public static string GetAppFilePath() {
            string path = Process.GetCurrentProcess().MainModule.FileName;
            return path;
        }

        /// <summary>
        /// 실행 파일이 위치한 폴더.
        /// </summary>
        public static string GetAppFolder() {
            string path = GetAppFilePath();
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// 실행 파일 버전
        /// </summary>
        public static string GetVersion(string filePath) {
            if (File.Exists(filePath)) {
                FileVersionInfo vi = FileVersionInfo.GetVersionInfo(filePath);
                return vi.ProductVersion;
            } else {
                return string.Empty;
            }
        }

        /// <summary>
        /// 버전 문자열로 새 버전인 지 검사.
        /// </summary>
        public static bool IsNewVersion(string oldVer, string newVer) {
            if (string.IsNullOrEmpty(newVer))
                return false;
            if (string.IsNullOrEmpty(oldVer))
                return true;

            string[] newVals = newVer.Split('.');
            string[] oldVals = oldVer.Split('.');
            for (int i = 0; i < 4; i++) {
                if (int.Parse(newVals[i]) > int.Parse(oldVals[i])) {
                    return true;
                } else if (int.Parse(newVals[i]) < int.Parse(oldVals[i])) {
                    return false;
                }
            }
            return false;
        }
    }
}
