////////////////////////////////////////////////////////////////////////////////
// GridElement.cs
// 2012.04.16, created by sohong
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

namespace Viewer.Common.UI.Acceleration
{
    /// <summary>
    /// Acceleration chart grid.
    /// </summary>
    public class GridElement : ChartElement
    {
        #region constructor

        public GridElement(AccelerationChart chart)
            : base(chart)
        {
        }

        #endregion // constructor


        #region properties

        public AxisValueProvider AxisValues
        {
            get;
            set;
        }

        public AxisLabelProvider AxisLabels
        {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc)
        {
            if (AxisValues == null) return;

            Rect r = new Rect(0, 0, Width, Height);
            Pen pen = new Pen(new SolidColorBrush(ToColor(0x33000000)), 1);
            pen.DashStyle = DashStyles.Dot;

            // horizontal lines
            double h = Height;
            double len = AxisValues.MaxValue - AxisValues.MinValue;
            foreach (double p in AxisValues) {
                double y = h - h * (p - AxisValues.MinValue) / len;
                dc.DrawLine(pen, new Point(0, y), new Point(Width, y));
            }
        }

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        #endregion // overriden methods
    }
}
