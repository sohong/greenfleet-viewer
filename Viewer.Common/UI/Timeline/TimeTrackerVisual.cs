////////////////////////////////////////////////////////////////////////////////
// TrackerVisual.cs
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
    /// Timeline tracker.
    /// </summary>
    public class TimeTrackerVisual : TimelineElement {

        #region constructor

        public TimeTrackerVisual(FrameworkElement container)
            : base(container) {
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Background fill
        /// </summary>
        public Brush Fill {
            get { return m_fill; }
            set {
                if (value != m_fill) {
                    m_fill = value;
                    Invalidate();
                }
            }
        }
        private Brush m_fill;

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

        /// <summary>
        /// Hover fill
        /// </summary>
        public Brush HoverFill {
            get { return m_hoverFill; }
            set {
                if (value != m_hoverFill) {
                    m_hoverFill = value;
                    Invalidate();
                }
            }
        }
        private Brush m_hoverFill;

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            Rect r = new Rect(0, 0, Width, Height);
            dc.DrawRectangle(IsHover && (HoverFill != null) ? HoverFill : Fill, Border, r);
        }

        #endregion // overriden methods
    }
}
