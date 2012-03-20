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
using Viewer.Common.Util;

namespace Viewer.Personal.Model {

    /// <summary>
    /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
    /// </summary>
    public class TrackImportHelper {

        #region fields

        private Repository m_owner;

        #endregion // fields


        #region constructors

        public TrackImportHelper(Repository owner) {
            Debug.Assert(owner != null, "owner is null");
            m_owner = owner;
        }

        #endregion // constructors


        #region methods

        /// <summary>
        /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// </summary>
        public int Import(Vehicle vehicle, IEnumerable<string> files, bool overwrite) {
            int count = 0;
            foreach (string file in files) {
                if (ImportTrackFile(vehicle, file, overwrite)) {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 외부 폴더에 있는 모든 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="overwrite"></param>
        public int ImportAll(Vehicle vehicle, string folder, bool overwrite) {
            int count = 0;
            if (Directory.Exists(folder)) {
                string[] files = Directory.GetFiles(folder, "*.inc");
                foreach (string file in files) {
                    if (ImportTrackFile(vehicle, file, overwrite)) {
                        count++;
                    }
                }
            }
            return count;
        }

        #endregion // static methods


        #region internal methods

        /// <summary>
        /// file명에 해당하는 inc, log, 264 파일들을 해당하는 스토리지에 복사한다.
        /// 264파일은 mp4파일로 변환하여 저장한다.
        /// 264파을을 삭제하지는 않는다.
        /// </summary>
        private bool ImportTrackFile(Vehicle vehicle, string file, bool overwrite) {
            // inc 파일은 반드시 존재해야 한다.
            string source = Path.ChangeExtension(file, ".inc");
            if (File.Exists(source)) {
                string folder = m_owner.GetFolder(vehicle, file, false);
                if (string.IsNullOrWhiteSpace(folder)) {
                    return false;
                }
                string name = Path.GetFileName(source);

                // inc
                string target = Path.Combine(folder, name);
                if (overwrite || !File.Exists(target)) {
                    File.Copy(source, target, true);
                }

                // log
                source = Path.ChangeExtension(file, ".log");
                if (File.Exists(source)) {
                    target = Path.Combine(folder, name);
                    target = Path.ChangeExtension(target, ".log");
                    if (overwrite || !File.Exists(target)) {
                        File.Copy(source, target, true);
                    }
                }

                // 264
                source = Path.ChangeExtension(file, ".264");
                if (File.Exists(source)) {
                    target = Path.Combine(folder, name);
                    target = Path.ChangeExtension(target, ".264");
                    if (overwrite || !File.Exists(target)) {
                        File.Copy(source, target, true);
                    }

                    // convert to mp4;
                    VideoUtil.RawToMpeg(target, null);
                }

                return true;
            }
            return false;
        }

        #endregion // internal methods
    }
}
