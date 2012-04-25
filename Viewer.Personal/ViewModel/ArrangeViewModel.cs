////////////////////////////////////////////////////////////////////////////////
// ArrangeViewModel.cs
// 2012.04.24, created by sohong
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
using Viewer.Common.ViewModel;

namespace Viewer.Personal.ViewModel
{
    /// <summary>
    /// View model for ArrangeView.
    /// 로컬 저장소를 정리(삭제, 복사, ...)한다.
    /// </summary>
    public class ArrangeViewModel : DialogViewModel
    {
        #region constructors

        public ArrangeViewModel()
        {
            IsCancelable = true;
            CancelText = null; // cancel 버튼이 표시되지 않고 esc 키만 동작하도록.
            SubmitText = "닫기";

            DateTime d = DateTime.Today;
            SearchFrom = new DateTime(d.Year, d.Month, 1, 0, 0, 0);
            SearchTo = SearchFrom.AddMonths(1).AddDays(-1).AddMinutes(24 * 60 - 1);
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 검색 조건 - 시작 시각(시/분), 혹은 시작 일시(일/시).
        /// </summary>
        public DateTime SearchFrom
        {
            get { return m_searchFrom; }
            set
            {
                if (value != m_searchFrom) {
                    m_searchFrom = value;
                    RaisePropertyChanged(() => SearchFrom);
                    //if (!IsLoading) {
                        SearchAll = false;
                    //}
                }
            }
        }
        private DateTime m_searchFrom;

        /// <summary>
        /// 검색 조건 - 끝 시각(시/분), 혹은 끝 일시(일/시).
        /// </summary>
        public DateTime SearchTo
        {
            get { return m_searchTo; }
            set
            {
                if (value != m_searchTo) {
                    m_searchTo = value;
                    RaisePropertyChanged(() => SearchTo);
                    //if (!IsLoading) {
                        SearchAll = false;
                    //}
                }
            }
        }
        private DateTime m_searchTo;

        /// <summary>
        /// 모두 가져오기
        /// </summary>
        public bool SearchAll
        {
            get { return m_searchAll; }
            set
            {
                if (value != m_searchAll) {
                    m_searchAll = value;
                    RaisePropertyChanged(() => SearchAll);
                }
            }
        }
        private bool m_searchAll;

        #endregion // properties


        #region overriden methods

        protected override bool CanSubmit()
        {
            return true;
        }

        #endregion // overriden methods
    }
}
