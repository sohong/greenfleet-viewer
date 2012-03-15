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
        }

        #endregion // constructor


        #region IDialogViewModel

        public virtual string CancelText {
            get { return "Cancel"; }
        }

        public virtual string ConfirmText {
            get { return "OK"; }
        }

        public virtual bool IsCancelable {
            get { return true; }
        }

        public virtual bool IsConfirmable {
            get { return false; }
        }

        public virtual bool Cancel() {
            return true;
        }

        public virtual bool Confirm() {
            ICommand command = GetCommand();
            if (command != null) {
                command.Execute(null);
                return true;
            }
            return false;
        }

        #endregion // IDialogViewModel


        #region internal methods

        protected virtual ICommand GetCommand() {
            return null;
        }

        #endregion // internal methods
    }
}
