////////////////////////////////////////////////////////////////////////////////
// DialogService.cs
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
using System.Windows;

namespace Viewer.Common.Service {

    /// <summary>
    /// Dialog를 실행 시킨다.
    /// </summary>
    public class DialogService {

        #region constructor 

        public DialogService() {
        }

        #endregion // constructor


        #region methods

        public static void Run(string title, UserControl view, IDialogViewModel viewModel, Action<object> callback = null, bool modal = true) {
            if (view == null)
                throw new ArgumentNullException("view");
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            DialogView dialog = new DialogView();
            dialog.Title = title;
            dialog.View = view;
            dialog.Model = viewModel;
            dialog.SubmitText = viewModel.SubmitText;
            dialog.CancelText = viewModel.CancelText;
            dialog.ShowCancel = viewModel.IsCancelable && !string.IsNullOrWhiteSpace(viewModel.CancelText);

            if (modal) {
                dialog.ShowCallback(callback);
            } else {
                dialog.Show();
            }
        }

        public static void RunProgress(string title, ProgressViewModel viewModel, bool modal = true) {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            ProgressView dialog = new ProgressView();
            dialog.Title = title;
            dialog.DataContext = viewModel;

            if (modal) {
                dialog.ShowCallback(callback);
            } else {
                dialog.Show();
            }
        }

        #endregion // methods
    }
}
