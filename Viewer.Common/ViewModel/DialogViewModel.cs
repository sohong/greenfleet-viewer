////////////////////////////////////////////////////////////////////////////////
// DialogViewModel.cs
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
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace Viewer.Common.ViewModel {

    /// <summary>
    /// View model for Dialog view.
    /// </summary>
    public class DialogViewModel : ViewModelBase, IDialogViewModel {

        #region constructor

        public DialogViewModel() {
            SubmitText = "OK";
            CancelText = "Cancel";

            SubmitCommand = new DelegateCommand(OnSubmit, IsSubmitable);
        }

        #endregion // constructor


        #region IDialogViewModel

        public ICommand SubmitCommand {
            get;
            private set;
        }

        public object SubmitData {
            get { return GetSubmitData(); }
        }

        public bool IsCancelable {
            get;
            set;
        }

        public bool Cancel() {
            return DoCancel();
        }

        public string SubmitText {
            get;
            set;
        }

        public string CancelText {
            get;
            set;
        }

        #endregion // IDialogViewModel


        #region internal methods

        private void OnSubmit() {
            DoSubmit();
        }

        private bool IsSubmitable() {
            return CanSubmit();
        }

        protected virtual object GetSubmitData() {
            return null;
        }

        protected virtual bool CanSubmit() {
            return false;
        }

        protected virtual void DoSubmit() {
        }

        protected virtual bool DoCancel() {
            return true;
        }

        /// <summary>
        /// Submit 가능 상태가 변경될 수 있을 때 호출한다.
        /// </summary>
        public void CheckSubmit() {
            ((DelegateCommand)SubmitCommand).RaiseCanExecuteChanged();
        }

        #endregion // internal methods
    }
}
