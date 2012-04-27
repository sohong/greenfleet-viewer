////////////////////////////////////////////////////////////////////////////////
// RetentionManager.cs
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

namespace Viewer.Personal.Model
{
    /// <summary>
    /// 하나 이상의 Retention schecule을 실행한다.
    /// 스케쥴 정보들을 xml에 보관한다.
    /// </summary>
    public class RetentionManager
    {
        #region fields

        private ObservableCollection<RetentionSchedule> m_schedules;

        #endregion // fields


        #region constructor

        public RetentionManager()
        {
        }

        #endregion // constructor
    }
}
