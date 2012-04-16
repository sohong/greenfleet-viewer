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

namespace Viewer.Common.UI.AccelChart {
    
    /// <summary>
    /// AccelerationChart 구성 요소.
    /// </summary>
    public abstract class ChartElement : DrawingVisual {

        #region fields

        private AccelerationChart m_chart;
        
        #endregion // fields


        #region constructor

        public ChartElement(AccelerationChart chart) {
            m_chart = chart;
            Draw();
        }

        #endregion // constructor


        #region abstract members

        protected abstract void DoDraw(DrawingContext dc);
        protected abstract Size Measure(double hintWidth, double hintHeight);

        #endregion // abstract members


        #region methods

        public void Invalidate() {
            m_chart.InvalidateArrange();
        }

        public void Draw() {
            DrawingContext dc = RenderOpen();
            DoDraw(dc);
            dc.Close();
        }

        #endregion // methods


        #region internal methods

        protected AccelerationChart Chart {
            get { return m_chart; }
        }

        #endregion // internal methods


        #region internal methods
        #endregion // internal methods
    }
}
