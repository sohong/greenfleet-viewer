////////////////////////////////////////////////////////////////////////////////
// Bookmark.cs
// 2012.04.27, created by sohong
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
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.Model
{
    /// <summary>
    /// Bookmark.
    /// </summary>
    public class Bookmark : NotificationObject
    {
        #region constructor

        public Bookmark()
        {
        }

        #endregion // constructor


        #region properties

        [Transient]
        public Track Track
        {
            get;
            set;
        }

        public string TrackFile
        {
            get;
            set;
        }

        public string Tags
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        #endregion // properties
    }
}
