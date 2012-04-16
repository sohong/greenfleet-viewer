﻿////////////////////////////////////////////////////////////////////////////////
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

namespace Viewer.Common.UI.Acceleration {

    public class GridElement : ChartElement {

        #region constructor

        public GridElement(AccelerationChart chart)
            : base(chart) {
        }

        #endregion // constructor


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size();
        }

        #endregion // overriden methods
    }
}