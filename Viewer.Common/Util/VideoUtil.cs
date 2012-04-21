////////////////////////////////////////////////////////////////////////////////
// VideoUtil.cs
// 2012.03.13, created by sohong
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

namespace Viewer.Common.Util
{
    /// <summary>
    /// Video 관련 유틸리티들.
    /// </summary>
    public class VideoUtil
    {
        #region consts

        const string FFMPEG = "ffmpeg.exe";
        const string EXTENSION = "mp4";
        //const string EXTENSION = "wmv";

        #endregion // consts;


        /// <summary>
        /// h264 raw 영상을 mpeg4 컨테이너로 변환한다.
        /// </summary>
        public static string RawToMpeg(string sourcePath, string targetFolder, string extension = EXTENSION)
        {
            string exeName = Path.Combine(FileUtil.GetAppFolder(), FFMPEG);
            if (!File.Exists(exeName)) {
                exeName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FFMPEG);
            }
            if (!File.Exists(exeName)) {
                exeName = FFMPEG;
            }

            string path;
            if (string.IsNullOrWhiteSpace(targetFolder)) {
                path = Path.ChangeExtension(sourcePath, extension);
            } else {
                path = Path.Combine(targetFolder, Path.ChangeExtension(Path.GetFileNameWithoutExtension(sourcePath), extension));
                Directory.CreateDirectory(targetFolder);
            }

            if (File.Exists(exeName)) {
                Process proc = new Process();
                proc.StartInfo.FileName = exeName;
                proc.StartInfo.Arguments = "-y -i \"" + sourcePath + "\" -vcodec copy \"" + path + "\"";
                //proc.StartInfo.Arguments = "-y -i \"" + sourcePath + "\" -vcodec wmv2 \"" + path + "\"";
                //proc.StartInfo.Arguments = "-y -i \"" + sourcePath + "\" -sameq -vcodec wmv2 -acodec wmav2 \"" + path + "\"";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.WaitForExit();
            }

            return path;
        }
    }
}
