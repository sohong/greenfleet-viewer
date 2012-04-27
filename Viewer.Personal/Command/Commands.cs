////////////////////////////////////////////////////////////////////////////////
// Commands.cs
// 2012.03.19, created by sohong
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
using System.Windows.Input;

namespace Viewer.Personal.Command
{
    public class Commands
    {
        #region static members

        private static Commands m_instance;

        static Commands()
        {
            m_instance = new Commands();
        }

        public static Commands Instance
        {
            get { return m_instance; }
        }

        #endregion // static members


        #region fields

        private ICommand m_import;
        private ICommand m_about;

        #endregion // fields


        #region constructor

        internal Commands()
        {
            m_import = new ImportCommand();
            m_about = new AboutCommand();
        }

        #endregion // constructor


        #region properties

        public ICommand Import
        {
            get { return m_import; }
        }

        public ICommand About
        {
            get { return m_about; }
        }

        #endregion // properties
    }
}
