﻿////////////////////////////////////////////////////////////////////////////////
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
            dc.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Gray, 1), new Rect(0, 0, Width, Height));
        }

        #endregion // overriden methods
    }
}