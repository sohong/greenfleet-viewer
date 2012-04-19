////////////////////////////////////////////////////////////////////////////////
// XAxisElement.cs
// 2012.04.19, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Globalization;

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// TimelineBar X Axis.
    /// </summary>
    public class XAxisElement : TimelineElement {
        #region constructor

        public XAxisElement(TimelineBar bar)
            : base(bar) {
        }

        #endregion // constructor


        #region properties

        public AxisLabelProvider AxisLabels {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(0, 0), new Point(Width, 0));

            if (AxisLabels == null)
                return;

            for (int i = 0; i <= AxisLabels.Count; i++) {
                double x = AxisLabels.GetPosition(i) * Width;
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, 0), new Point(x, 5));

                if (i % 10 == 0) {
                    string text = AxisLabels.GetLabel(i);
                    Typeface face = new Typeface("Tahoma");
                    FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Black);
                    double tw = ft.Width;
                    dc.DrawText(ft, new Point(x - tw / 2, 7));
                }
            }
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(0, 25);
        }

        #endregion // overriden methods
    }
}
