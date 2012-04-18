////////////////////////////////////////////////////////////////////////////////
// SeriesElement.cs
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

namespace Viewer.Common.UI.Acceleration {

    public class SeriesElement : ChartElement {

        #region constructor

        public SeriesElement(AccelerationChart chart)
            : base(chart) {
        }

        #endregion // constructor


        #region properties

        public AxisValueProvider AxisValues {
            get;
            set;
        }

        public AxisLabelProvider AxisLabels {
            get;
            set;
        }

        public IList<double> Values {
            get;
            set;
        }

        public Color Color {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            if (AxisValues == null || AxisLabels == null || Values == null) 
                return;

            // create points
            Point[] pts = new Point[Values.Count];
            double h = Height;
            double len = AxisValues.MaxValue - AxisValues.MinValue;
            for (int i = 0; i < Values.Count; i++) {
                double v = Values[i];
                Point p = new Point();
                p.X = AxisLabels.GetPosition(i) * Width;
                p.Y = h - h * (v - AxisValues.MinValue) / len;
                pts[i] = p;
            }

            // lines

            // data points
            Brush fill = new SolidColorBrush(Color);
            for (int i = 0; i < pts.Length; i++) {
                Point p = pts[i];
                dc.DrawEllipse(fill, null, p, 10, 10);
            }
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size();
        }

        #endregion // overriden methods
    }
}
