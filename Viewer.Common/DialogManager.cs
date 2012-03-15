////////////////////////////////////////////////////////////////////////////////
// DialogManager.cs
// 2012.03.15, created by sohong
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
using System.Windows.Controls;
using Viewer.Common.ViewModel;
using Viewer.Common.View;

namespace Viewer.Common {

    /// <summary>
    /// Dialog를 실행 시킨다.
    /// </summary>
    public class DialogManager {

        #region constructor 

        public DialogManager() {
        }

        #endregion // constructor


        #region methods

        public void Run(UserControl view, IDialogViewModel viewModel, Action callback, bool modal = true) {
            DialogView dialog = new DialogView();
            dialog.View = view;
            dialog.Model = viewModel;

            if (modal) {
                dialog.ShowDialog();
            
            } else {
                dialog.Show();
            }
        }

        #endregion // methods
    }
}
