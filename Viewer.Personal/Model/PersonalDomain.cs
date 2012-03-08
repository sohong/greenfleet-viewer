////////////////////////////////////////////////////////////////////////////////
// PersonalDomain.cs
// 2012.03.08, created by sohong
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
using Viewer.Common.Util;
using System.IO;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Personal viewer application domain.
    /// </summary>
    public class PersonalDomain {

        #region consts

        private const string PREFERS_PATH = "preferences.xml";
        
        #endregion // consts


        #region static members

        private static readonly PersonalDomain m_instance;

        static PersonalDomain() {
            m_instance = new PersonalDomain();
        }

        public static PersonalDomain Domain {
            get { return m_instance; }
        }

        #endregion // static members


        #region fields

        private Preferences m_preferences;
        private Repository m_repository;

        #endregion // fields


        #region constructors

        private PersonalDomain() {
            m_preferences = new Preferences();
            m_repository = new Repository();
        }

        #endregion // constructors


        #region properties

        public Preferences Preferences {
            get { return m_preferences; }
        }

        public Repository Repository {
            get { return m_repository; }
        }

        #endregion // properties


        #region methods

        public void Start() {
            m_preferences.Load(Path.Combine(FileUtil.GetAppFolder(), PREFERS_PATH));
        }

        #endregion // methods
    }
}
