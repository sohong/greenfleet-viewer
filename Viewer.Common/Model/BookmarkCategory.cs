////////////////////////////////////////////////////////////////////////////////
// BookmarkCategory.cs
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
using System.Collections.ObjectModel;

namespace Viewer.Common.Model
{
    /// <summary>
    /// Bookmark category.
    /// </summary>
    public class BookmarkCategory : NotificationObject
    {
        #region constructor

        public BookmarkCategory()
        {
            this.Items = new ObservableCollection<Bookmark>();
        }

        #endregion // constructor


        #region properties

        public string Name
        {
            get;
            set;
        }

        public ObservableCollection<Bookmark> Items
        {
            get;
            private set;
        }
        
        #endregion // properties
    }
}
