////////////////////////////////////////////////////////////////////////////////
// LogUtil.cs
// 2012.03.09, created by sohong
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
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;

namespace Viewer.Common.Util {

    /// <summary>
    /// Logging관련 함수들.
    /// </summary>
    public class LogUtil {

        public static void Debug(string message) {
            Log(message, Category.Debug);
        }

        public static void Info(string message) {
            Log(message, Category.Info);
        }

        public static void Warn(string message) {
            Log(message, Category.Warn);
        }
        
        public static void Error(string message) {
            Log(message, Category.Exception);
        }

        private static void Log(string message, Category category) {
            ILoggerFacade logger = (ILoggerFacade)ServiceLocator.Current.GetInstance(typeof(ILoggerFacade));
            logger.Log(message, category, Priority.None);
        }
    }
}
