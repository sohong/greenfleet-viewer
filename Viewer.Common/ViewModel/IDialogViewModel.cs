////////////////////////////////////////////////////////////////////////////////
// IDialogViewModel.cs
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

namespace Viewer.Common.ViewModel {

    /// <summary>
    /// View model interface for DialogView.
    /// </summary>
    public interface IDialogViewModel {

        string CancelText { get; }
        string ConfirmText { get; }
        bool IsCancelable { get; }
        bool IsConfirmable { get; }

        bool Cancel();
        bool Confirm();
    }
}
