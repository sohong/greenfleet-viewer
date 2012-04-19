////////////////////////////////////////////////////////////////////////////////
// ChartElement.cs
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
    
    /// <summary>
    /// AccelerationChart 구성 요소.
    /// </summary>
    public abstract class ChartElement : UIElement {

        #region constructor

        public ChartElement(AccelerationChart chart) : base(chart) {
        }

        #endregion // constructor


        #region properties

        public AccelerationChart Chart {
            get { return Container as AccelerationChart; }
        }

        #endregion // properties


        #region methods
        #endregion // methods


        #region internal methods
        #endregion // internal methods
    }
}
