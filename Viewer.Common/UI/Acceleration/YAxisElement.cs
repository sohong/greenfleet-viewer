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


        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(40, 0);
        }

        protected override void DoDraw(DrawingContext dc) {
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(Width, 0), new Point(Width, Height));

            /*
            uint count = Chart.MinValueCount;
            double w = Width / (count - 1);
            for (int i = 0; i < count; i++) {
                double x = i * w;
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, 0), new Point(x, 5));

                if (i % 10 == 0) {
                    string text = m_startTime.AddSeconds(i).ToString("mm:ss");
                    Typeface face = new Typeface("Tahoma");
                    FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Black);
                    double tw = ft.MinWidth;
                    dc.DrawText(ft, new Point(x - tw / 2, 7));
                }
            }
             */
        }

        #endregion // overriden methods
    }
}
