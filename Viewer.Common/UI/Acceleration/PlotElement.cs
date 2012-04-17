////////////////////////////////////////////////////////////////////////////////
// PlotElement.cs
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

    public class PlotElement : ChartElement {

        #region fields

        private GridElement m_gridElement;
        private SeriesElement m_seriesX;
        private SeriesElement m_seriesY;
        private SeriesElement m_seriesZ;
        
        #endregion // fields


        #region constructor

        public PlotElement(AccelerationChart chart)
            : base(chart) {
        }

        #endregion // constructor


        #region methods

        public void LayoutChildren() {
            m_gridElement.Height = m_seriesX.Height = m_seriesY.Height = m_seriesZ.Height = this.Height;
            m_gridElement.Height = m_seriesX.Height = m_seriesY.Height = m_seriesZ.Height = this.Height;
        }
        
        #endregion // methods


        #region overriden methods

        protected override void CreateChildren() {
            base.CreateChildren();

            Children.Add(m_gridElement = new GridElement(Chart));
            Children.Add(m_seriesX = new SeriesElement(Chart));
            Children.Add(m_seriesY = new SeriesElement(Chart));
            Children.Add(m_seriesZ = new SeriesElement(Chart));
        }

        public override void Draw() {
            base.Draw();

            m_gridElement.Draw();
            m_seriesX.Draw();
            m_seriesY.Draw();
            m_seriesZ.Draw();
        }

        protected override void DoDraw(DrawingContext dc) {
            LinearGradientBrush brush = new LinearGradientBrush(Colors.LightGray, Colors.White, 90);
            dc.DrawRectangle(brush, new Pen(Brushes.Gray, 1), new Rect(0, 0, Width, Height));
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size();
        }

        #endregion // overriden methods
    }
}
