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
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.Model
{
    /// <summary>
    /// Bookmark collection
    /// </summary>
    public class BookmarkCollection
    {
        #region constructor

        public BookmarkCollection()
        {
            this.Items = new ObservableCollection<NotificationObject>();
        }

        #endregion // constructor


        #region properties

        public IEnumerable<NotificationObject> Items
        {
            get;
            private set;
        }

        #endregion // properties


        #region methods

        public bool Contains(NotificationObject item)
        {
            return List.Contains(item);
        }

        public void AddCategory(BookmarkCategory category)
        {
            if (!Contains(category)) {
                List.Add(category);
            }
        }

        public void AddBookmark(Bookmark bookmark)
        {
            if (!Contains(bookmark)) {
                List.Add(bookmark);
            }
        }

        #endregion // methods


        #region internal properties

        private ObservableCollection<NotificationObject> List
        {
            get { return (ObservableCollection<NotificationObject>)Items; }
        }

        #endregion // internal properties
    }
}
