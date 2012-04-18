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


        #region properties

        public AxisValueProvider AxisValues {
            get;
            set;
        }

        public AxisLabelProvider AxisLabels {
            get;
            set;
        }

        public IList<AccelerationChart.Value> Values {
            get;
            set;
        }

        #endregion // properties


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

            m_gridElement.Width = this.Width;
            m_gridElement.Height = this.Height;
            m_gridElement.AxisValues = this.AxisValues;
            m_gridElement.AxisLabels = this.AxisLabels;
            m_gridElement.Draw();

            m_seriesX.Width = this.Width;
            m_seriesX.Height = this.Height;
            m_seriesX.AxisValues = this.AxisValues;
            m_seriesX.AxisLabels = this.AxisLabels;
            m_seriesX.Values = GetSeriesValues(0);
            m_seriesX.Draw();

            m_seriesY.Width = this.Width;
            m_seriesY.Height = this.Height;
            m_seriesY.AxisValues = this.AxisValues;
            m_seriesY.AxisLabels = this.AxisLabels;
            m_seriesY.Values = GetSeriesValues(1);
            m_seriesY.Draw();

            m_seriesZ.Width = this.Width;
            m_seriesZ.Height = this.Height;
            m_seriesZ.AxisValues = this.AxisValues;
            m_seriesZ.AxisLabels = this.AxisLabels;
            m_seriesZ.Values = GetSeriesValues(2);
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


        #region internal methods

        private IList<double> GetSeriesValues(int accel) {
            IList<double> values = new List<double>();

            if (Values != null) {
                foreach (AccelerationChart.Value v in Values) {
                    switch (accel) {
                    case 0: // X
                        values.Add(v.X);
                        break;
                    case 1: // Y
                        values.Add(v.Y);
                        break;
                    default:
                        values.Add(v.Z);
                        break;
                    }
                }
            }

            return values;
        }

        #endregion // internal methods
    }
}
