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

        #region fields

        private bool m_submitable;

        #endregion // fields


        #region constructor

        public DialogViewModel() {
            SubmitCommand = new DelegateCommand<object>(OnSubmit, IsSubmitable);
            m_submitable = CanSubmit(SubmitData);
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

        public bool Cancel() {
            return DoCancel();
        }

        #endregion // IDialogViewModel


        #region internal methods

        private void OnSubmit(object data) {
            DoSubmit(data);
        }

        private bool IsSubmitable(object data) {
            return m_submitable;
        }

        protected virtual object GetSubmitData() {
            return null;
        }

        protected virtual bool CanSubmit(object data) {
            return false;
        }

        protected virtual void DoSubmit(object data) {
        }

        protected virtual bool DoCancel() {
            return true;
        }

        /// <summary>
        /// Submit 가능 상태가 변경될 수 있을 때 호출한다.
        /// </summary>
        public void CheckSubmit() {
            bool v = CanSubmit(GetSubmitData());
            if (v != m_submitable) {
                m_submitable = v;
                ((DelegateCommand)SubmitCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion // internal methods
    }
}
