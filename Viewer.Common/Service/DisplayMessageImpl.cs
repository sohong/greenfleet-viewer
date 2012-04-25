////////////////////////////////////////////////////////////////////////////////
// DisplayMessageImpl.cs
// 2012.04.25, created by sohong
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
using System.ComponentModel.Composition;
using System.Windows;

namespace Viewer.Common.Service
{
    /// <summary>
    /// IDisplayMessageService 기본 구현.
    /// </summary>
    [Export(typeof(IDisplayMessageService))]
    public class DisplayMessageImpl : IDisplayMessageService
    {
        #region IDisplayMessageService

        public bool Confirm(string caption, string message)
        {
            return
                MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes;
        }

        #endregion // IDisplayMessageService
    }
}
