////////////////////////////////////////////////////////////////////////////////
// MessageUtil.cs
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
using System.Windows;

namespace Viewer.Common.Util {

    /// <summary>
    /// Message box 관련 유틸리티들.
    /// </summary>
    public class MessageUtil {

        public static void Show(string title, string message) {
            MessageBox.Show(message, title);
        }

        public static bool Conform(string title, string message) {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }
    }
}
