////////////////////////////////////////////////////////////////////////////////
// SeriesElement.cs
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
using System.Windows;
using System.Windows.Media;

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// TimelineBar series.
    /// </summary>
    public class SeriesElement : TimelineElement {

        #region constructor

        public SeriesElement(TimelineBar bar)
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

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        protected override void DoDraw(DrawingContext dc)
        {
        }

        #endregion // overriden methods
    }
}
