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

        public IEnumerable<double> Values {
            get { return m_values; }
            set {
                if (value != m_values) {
                    m_values = value;

                    double minVal = 0;
                    double maxVal = 0;

                    foreach (double v in m_values) {
                        minVal = Math.Min(v, minVal);
                        maxVal = Math.Max(v, maxVal);
                    }

                    MinValue = minVal;
                    MaxValue = maxVal;

                    Invalidate();
                }
            }
        }
        private IEnumerable<double> m_values;

        public double MinValue {
            get;
            set;
        }

        public double MaxValue {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(40, 0);
        }

        protected override void DoDraw(DrawingContext dc) {
            if (m_values == null) return;

            double x = Width;
            double h = Height;
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, 0), new Point(x, h));

            double len = MaxValue - MinValue;
            foreach (double p in m_values) {
                double y = h - (p - MinValue) * h / len;
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(x, y), new Point(x - 5, y));

                /*
                if (i % 10 == 0) {
                    string text = m_startTime.AddSeconds(i).ToString("mm:ss");
                    Typeface face = new Typeface("Tahoma");
                    FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Black);
                    double tw = ft.MinWidth;
                    dc.DrawText(ft, new Point(x - tw / 2, 7));
                }
                 */
            }
        }

        #endregion // overriden methods
    }
}
