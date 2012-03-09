////////////////////////////////////////////////////////////////////////////////
// Log4NetLogger.cs
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
using log4net;
using System.IO;
using log4net.Config;

namespace Viewer.Common {

    /// <summary>
    /// Prism LoggerFacade implemented with log4net.
    /// </summary>
    public class Log4NetLogger : ILoggerFacade {

        #region static members

        static Log4NetLogger() {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml"));
        }

        #endregion // static members


        #region fields

        private readonly ILog m_logger;

        #endregion // fields


        #region constructors

        public Log4NetLogger(string logName) {
            m_logger = LogManager.GetLogger(logName);
        }

        #endregion // constructors


        #region ILoggerFacade

        public void Log(string message, Category category, Priority priority) {
            switch (category) {
                case Category.Debug:
                    if (m_logger.IsDebugEnabled) 
                        m_logger.Debug(message);
                    break;
                case Category.Info:
                    if (m_logger.IsInfoEnabled) 
                        m_logger.Info(message);
                    break;
                case Category.Warn:
                    if (m_logger.IsWarnEnabled) 
                        m_logger.Warn(message);
                    break;
                case Category.Exception:
                    m_logger.Error(message);
                    break;
            }
        }

        #endregion // ILoggerFacade
    }
}
