////////////////////////////////////////////////////////////////////////////////
// TimelineTrackerElement.cs
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

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// Timeline tracker.
    /// </summary>
    public class TimelineTrackerElement : TimelineElement
    {
        #region constructor

        public TimelineTrackerElement(TimelineBar bar)
            : base(bar)
        {
            this.Fill = new SolidColorBrush(ToColor(0xffff0000));
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Border
        /// </summary>
        public Pen Border
        {
            get { return m_border; }
            set
            {
                if (value != m_border) {
                    m_border = value;
                    Invalidate();
                }
            }
        }
        private Pen m_border;

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        protected override void DoDraw(DrawingContext dc)
        {
            Pen pen = new Pen(this.Fill, 3);
            dc.DrawLine(pen, new Point(0, 0), new Point(0, Height));

            pen = new Pen(this.Fill, 1);
            dc.DrawLine(pen, new Point(-5, 0), new Point(5, 0));
            dc.DrawLine(pen, new Point(-5, Height), new Point(5, Height));
        }

        #endregion // overriden methods
    }
}
