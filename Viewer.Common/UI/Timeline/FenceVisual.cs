////////////////////////////////////////////////////////////////////////////////
// FenceVisual.cs
// 2012.03.30, created by sohong
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
    /// TimelineBar 배경.
    /// </summary>
    public class FenceVisual : TimelineElement {

        #region constructor

        public FenceVisual(FrameworkElement container)
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

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            Rect r = new Rect(0, 0, Width, Height);
            dc.DrawRectangle(Fill, null, r);
        }

        #endregion // overriden methods
    }
}
