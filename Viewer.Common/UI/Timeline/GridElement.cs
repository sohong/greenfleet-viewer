////////////////////////////////////////////////////////////////////////////////
// GridElement.cs
// 2012.04.19, created by sohong
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
using System.Windows.Media;
using System.Windows;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// Timeline bar grid.
    /// </summary>
    public class GridElement : TimelineElement
    {
        #region constructor

        public GridElement(TimelineBar bar)
            : base(bar)
        {
        }

        #endregion // constructor


        #region properties

        public AxisLabelProvider AxisLabels
        {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc)
        {
            if (AxisLabels == null) return;

            Rect r = new Rect(0, 0, Width, Height);
            Pen pen = new Pen(new SolidColorBrush(ToColor(0xaa333333)), 1);
            pen.DashStyle = DashStyles.Dash;

            // vertical lines
            for (int i = 1; i < AxisLabels.Count - 1; i++) {
                double x = AxisLabels.GetPosition(i) * Width;
                dc.DrawLine(pen, new Point(x, 0), new Point(x, Height));
            }
        }

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        #endregion // overriden methods
    }
}
