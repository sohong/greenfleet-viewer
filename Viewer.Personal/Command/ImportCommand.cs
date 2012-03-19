////////////////////////////////////////////////////////////////////////////////
// ImportCommand.cs
// 2012.03.19, created by sohong
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
using System.Windows.Input;
using Viewer.Personal.Model;
using System.Windows.Forms;
using Viewer.Common.Util;

namespace Viewer.Personal.Command {

    /// <summary>
    /// 스토리지 밖의 폴더(예컨대 sd카드)에서 트랙 파일들을 읽어 
    /// 스토리지 내 지정된 위치로 복사하고, 카탈로그에 인덱스를 추가한다.
    /// </summary>
    public class ImportCommand : SimpleCommand {

        #region overriden methods

        public override bool CanExecute(object parameter) {
            return parameter is Vehicle;
        }

        public override void Execute(object parameter) {
            Vehicle v = parameter as Vehicle;
            if (v != null) {
                MessageUtil.Show(v.Name);
                /*
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK) {
                }
                 */
            }
        }

        #endregion // overriden methods
    }
}
