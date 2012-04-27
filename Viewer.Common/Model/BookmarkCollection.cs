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
using System.Collections.ObjectModel;

namespace Viewer.Common.Model
{
    /// <summary>
    /// Bookmark collection
    /// </summary>
    public class BookmarkCollection : ObservableCollection<BookmarkCategory>
    {
        #region constructor

        public BookmarkCollection()
        {
        }

        #endregion // constructor
    }
}
