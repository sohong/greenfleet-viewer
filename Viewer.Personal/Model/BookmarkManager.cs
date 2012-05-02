////////////////////////////////////////////////////////////////////////////////
// BookmarkManager.cs
// 2012.05.02, created by sohong
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
using Viewer.Common.Model;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// Bookmark나 bookmark 카테고리들을 추가/삭제/수정하고,
    /// xml 파일에 저장 및 로드한다.
    /// </summary>
    public class BookmarkManager
    {
        #region fields

        private BookmarkCollection m_items;
        
        #endregion // fields


        #region constructor

        public BookmarkManager()
        {
            m_items = new BookmarkCollection();
        }

        #endregion // constructor
    }
}
