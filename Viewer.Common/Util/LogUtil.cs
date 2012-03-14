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

        #region static fileds

        private static ILoggerFacade m_logger;
        private static bool m_tracing;

        #endregion // static fields


        #region static methods

        public static void InitLog4Net(string logName) {
            if (m_logger == null) {
                m_logger = new Log4NetLogger(logName);
            }
        }

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

        #endregion // static methods


        #region // static internal methods

        private static void Log(string message, Category category) {
            if (m_logger == null && !m_tracing) {
                try {
                    m_logger = (ILoggerFacade)ServiceLocator.Current.GetInstance(typeof(ILoggerFacade));
                } catch (Exception) {
                    m_tracing = true;
                }
            }

            if (m_logger != null) {
                m_logger.Log(message, category, Priority.None);
            } else {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        #endregion // static internal methods
    }
}
