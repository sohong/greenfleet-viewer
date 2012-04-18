////////////////////////////////////////////////////////////////////////////////
// YAxisElement.cs
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
using System.Windows.Media;
using System.Windows;
using System.Globalization;

namespace Viewer.Common.UI.Acceleration {

    public class YAxisElement : ChartElement {

        #region constructor

        public YAxisElement(AccelerationChart chart)
            : base(chart) {
        }

        #endregion // constructor


        #region properties

        public AxisValueProvider AxisValues {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(16, 0);
        }

        protected override void DoDraw(DrawingContext dc) {
            if (AxisValues == null) return;

            double x = Width;
            double h = Height;
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, 0), new Point(x, h));

            double len = AxisValues.MaxValue - AxisValues.MinValue;
            foreach (double p in AxisValues) {
                double y = h - h * (p - AxisValues.MinValue) / len;
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, y), new Point(x - 5, y));

                string text = p + "";
                Typeface face = new Typeface("Tahoma");
                FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Black);
                double tw = ft.Width;
                double th = ft.Height;
                dc.DrawText(ft, new Point(x - tw - 5 - 2, y - th / 2));
            }
        }

        #endregion // overriden methods
    }
}
