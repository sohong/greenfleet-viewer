////////////////////////////////////////////////////////////////////////////////
// DialogUtil.cs
// 2012.03.21, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Viewer.Common.Util
{
    /// <summary>
    /// 다이얼로그 관련 유티리티들.
    /// </summary>
    public class DialogUtil
    {
        /// <summary>
        /// 폴더를 선택한다.
        /// </summary>
        /// <param name="caption">다이얼로그에 표시할 메시지.</param>
        /// <param name="startFolder">시작 폴더</param>
        /// <returns>선택했으면 선택 폴더, 아니면 null.</returns>
        public static string SelectFolder(string caption, string startFolder)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = caption;
            dlg.SelectedPath = startFolder;

            if (dlg.ShowDialog() == DialogResult.OK) {
                return dlg.SelectedPath;
            }
            return null;
        }
    }
}
