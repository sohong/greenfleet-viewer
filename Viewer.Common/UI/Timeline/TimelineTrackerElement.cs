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
using System.Globalization;

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

        public DateTime Time
        {
            get { return m_time; }
            set
            {
                if (value != m_time) {
                    m_time = value;
                    Draw();
                }
            }
        }
        private DateTime m_time;

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        protected override void DoDraw(DrawingContext dc)
        {
            // vertical line
            Pen pen = new Pen(this.Fill, 2);
            dc.DrawLine(pen, new Point(0, 0), new Point(0, Height));

            // top / bottom
            pen = new Pen(this.Fill, 1);
            dc.DrawLine(pen, new Point(-5, 1), new Point(5, 1));
            dc.DrawLine(pen, new Point(-5, Height), new Point(5, Height));

            // label
            string s = Time.ToString("MM-dd HH:mm:ss");
            Typeface face = new Typeface("Tahoma");
            Brush fill = new SolidColorBrush(ToColor(0xccff0000));
            FormattedText ft = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 11, fill);
            dc.DrawText(ft, new Point(4, this.Height - ft.Height / 3));
        }

        #endregion // overriden methods
    }
}
