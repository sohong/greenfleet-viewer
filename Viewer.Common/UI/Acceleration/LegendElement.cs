////////////////////////////////////////////////////////////////////////////////
// LegendElement.cs
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

    public class LegendElement : ChartElement {

        #region constructor

        public LegendElement(AccelerationChart chart)
            : base(chart) {
        }

        #endregion // constructor


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            dc.DrawRectangle(Brushes.Green, new Pen(Brushes.Gray, 1), new Rect(0, 0, Width, Height));
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(100, 0);
        }

        #endregion // overriden methods
    }
}
