////////////////////////////////////////////////////////////////////////////////
// RangeMarkerVisual.cs
// 2012.03.29, created by sohong
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
using System.Windows;
using System.Windows.Media;

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// TimelineBar의 한 구간을 표시하는 element.
    /// </summary>
    public class TimeRangeVisual : TimelineElement {

        #region constructor

        public TimeRangeVisual(FrameworkElement container) 
            : base(container) {
        }

        #endregion // constructor

        
        #region properties

        /// <summary>
        /// Border
        /// </summary>
        public Pen Border {
            get { return m_border; }
            set {
                if (value != m_border) {
                    m_border = value;
                    Invalidate();
                }
            }
        }
        private Pen m_border;

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            Rect r = new Rect(0, 0, Width, Height);
            dc.DrawRectangle(Fill, Border, r);
        }

        #endregion // overriden methods
    }
}
