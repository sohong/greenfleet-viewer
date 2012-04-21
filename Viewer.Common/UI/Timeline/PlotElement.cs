////////////////////////////////////////////////////////////////////////////////
// PlotElement.cs
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
using System.Windows.Media;
using System.Windows;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// TimelineBar plot area.
    /// </summary>
    public class PlotElement : TimelineElement
    {
        #region fields

        private GridElement m_gridElement;
        private SeriesElement m_seriesElement;

        //private SeriesElement m_seriesZ;
        //private DrawingVisual m_indicator;
        //private DrawingVisual m_panel;

        #endregion // fields


        #region constructor

        public PlotElement(TimelineBar bar)
            : base(bar)
        {
        }

        #endregion // constructor


        #region properties

        public AxisLabelProvider AxisLabels
        {
            get;
            set;
        }

        public TimelineValueCollection Values
        {
            get;
            set;
        }

        #endregion // properties


        #region methods

        public void LayoutChildren()
        {
            m_gridElement.Height = this.Height;
        }

        #endregion // methods


        #region overriden methods

        protected override void CreateChildren()
        {
            base.CreateChildren();

            Children.Add(m_gridElement = new GridElement(Bar));
            Children.Add(m_seriesElement = new SeriesElement(Bar));
        }

        public override void Draw()
        {
            base.Draw();

            m_gridElement.Width = this.Width;
            m_gridElement.Height = this.Height;
            m_gridElement.AxisLabels = this.AxisLabels;
            m_gridElement.Draw();

            m_seriesElement.Width = this.Width;
            m_seriesElement.Height = this.Height;
            m_seriesElement.AxisLabels = this.AxisLabels;
            m_seriesElement.Values = this.Values;
            m_seriesElement.Draw();
        }

        protected override void DoDraw(DrawingContext dc)
        {
            LinearGradientBrush brush = new LinearGradientBrush(Colors.LightGray, Colors.White, 90);
            dc.DrawRectangle(brush, new Pen(Brushes.Green, 1), new Rect(0, 0, Width, Height));
        }

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        #endregion // overriden methods


        #region internal methods

        /*
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

        private void DrawIndicator() {
            DrawingContext dc = m_indicator.RenderOpen();

            Pen pen = new Pen(new SolidColorBrush(ToColor(0x880000ff)), 1);
            pen.DashStyle = DashStyles.Dash;
            double x = this.AxisLabels.GetPosition(this.Values.Count - 1) * this.Width;
            dc.DrawLine(pen, new Point(x, -2), new Point(x, this.Height + 2));

            dc.Close();
        }

        private void DrawPanel() {
            DrawingContext dc = m_panel.RenderOpen();

            Brush fill = new SolidColorBrush(ToColor(0x110000ff));
            double x = this.AxisLabels.GetPosition(this.Values.Count - 1) * this.Width;
            dc.DrawRectangle(fill, null, new Rect(x, 0, this.Width - x, this.Height));

            if (x < this.Width - 4) {
                string s = this.AxisLabels.GetLabel(this.Values.Count - 1);
                Typeface face = new Typeface("Tahoma");
                FormattedText ft = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Blue);
                dc.DrawText(ft, new Point(x + 4, this.Height - ft.Height - 2));
            }

            dc.Close();
        }
         */

        #endregion // internal methods
    }
}
