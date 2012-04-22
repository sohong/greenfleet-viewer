////////////////////////////////////////////////////////////////////////////////
// AccelerationChart.cs
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
using Viewer.Common.UI.Acceleration;
using System.Windows.Media;

namespace Viewer.Common.UI
{
    /// <summary>
    /// x/y/x 가속도 챠트.
    /// </summary>
    public class AccelerationChart : UIContainer
    {
        #region struct Value

        public struct Value
        {
            public DateTime T;
            public double X;
            public double Y;
            public double Z;

            public Value(DateTime t, double x, double y, double z)
            {
                this.T = t;
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }

        #endregion // struct Value


        #region struct Series

        public struct Series
        {
            public string Label;
            public Color Color;

            public Series(string label, Color color)
            {
                this.Label = label;
                this.Color = color;
            }
        }

        #endregion // struct Series


        #region dependency properties

        /// <summary>
        /// MinValueCount
        /// </summary>
        public static readonly DependencyProperty MinValueCountProperty = DependencyProperty.Register(
            "ValueCount", typeof(uint), typeof(AccelerationChart),
            new FrameworkPropertyMetadata((uint)60, OnMinValueCountChanged));
        private static void OnMinValueCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((AccelerationChart)d).RefreshChart();
        }

        /// <summary>
        /// Title
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(AccelerationChart),
            new FrameworkPropertyMetadata(null, OnTitleChanged));
        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((AccelerationChart)d).RefreshChart();
        }

        #endregion dependency properties


        #region fields

        private PlotElement m_plotElement;
        private XAxisElement m_xaxisElement;
        private YAxisElement m_yaxisElement;
        private LegendElement m_legendElement;

        private IList<Value> m_values;
        private IList<Series> m_series;
        private double m_minimum = -1;
        private double m_maximum = 1;
        private AxisValueProvider m_axisValues;
        private AxisLabelProvider m_axisLabels;

        #endregion // fields


        #region constructor

        public AccelerationChart()
        {
            m_values = new List<Value>();

            m_series = new List<Series>();
            m_series.Add(new Series("accel X", Colors.CadetBlue));
            m_series.Add(new Series("accel Y", Colors.Goldenrod));
            m_series.Add(new Series("accel Z", Colors.PaleVioletRed));

            m_axisValues = new AxisValueProvider();
            m_axisLabels = new AxisLabelProvider();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// X 축에 표시할 최소 value count.
        /// 실제 value 갯수가 적어도 자리를 차지한다.
        /// </summary>
        public uint MinValueCount
        {
            get { return (uint)GetValue(MinValueCountProperty); }
            set { SetValue(MinValueCountProperty, value); }
        }

        /// <summary>
        /// Chart title.
        /// 왼쪽 아래에 표시한다.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion // properties


        #region methods

        public void Clear()
        {
            m_values.Clear();
            RefreshChart();
        }

        public void AddValue(DateTime t, double x, double y, double z)
        {
            m_values.Add(new Value(t, x, y, z));
            RefreshChart();
        }

        #endregion // methods


        #region overriden methods

        protected override void CreateElements()
        {
            AddElement(m_plotElement = new PlotElement(this));
            AddElement(m_xaxisElement = new XAxisElement(this));
            AddElement(m_yaxisElement = new YAxisElement(this));
            AddElement(m_legendElement = new LegendElement(this));

        }

        protected override void LayoutElements(double width, double height)
        {
            if (width * height == 0) return;

            Recalculate(width, height);

            double paddingX = 15;
            double paddingY = 5;
            double x = paddingX;
            double y = paddingY;
            width -= paddingX * 2;
            height -= paddingY * 2;

            // legend
            Size sz = m_legendElement.Measure(width, height);
            m_legendElement.Width = width;
            m_legendElement.Height = sz.Height;
            m_legendElement.X = x;
            m_legendElement.Y = y + height - sz.Height;

            height -= m_legendElement.Height;

            // x-axis
            sz = m_xaxisElement.Measure(width, height);
            m_xaxisElement.Height = sz.Height;
            m_xaxisElement.Y = y + height - sz.Height;

            height -= m_xaxisElement.Height;

            // y-axis
            sz = m_yaxisElement.Measure(width, height);
            m_yaxisElement.Width = sz.Width;
            m_yaxisElement.Height = height;
            m_yaxisElement.Move(x, y);

            x += m_yaxisElement.Width;
            width -= m_yaxisElement.Width;

            m_xaxisElement.Width = width;
            m_yaxisElement.Height = height;
            m_xaxisElement.X = x;

            m_legendElement.X = x;
            m_legendElement.Width = width;
            m_legendElement.Title = Title;

            // plot
            m_plotElement.Width = width;
            m_plotElement.Height = height;
            m_plotElement.Move(x, y);

            m_legendElement.Draw();
            m_xaxisElement.Draw();
            m_yaxisElement.Draw();

            m_plotElement.LayoutChildren();
            m_plotElement.Draw();
        }

        #endregion // overriden methods


        #region internal methods

        private void RefreshChart()
        {
            InvalidateArrange();
        }

        private void Recalculate(double width, double height)
        {
            double min = -1;
            double max = 1;

            foreach (Value v in m_values) {
                min = Math.Min(min, v.X);
                min = Math.Min(min, v.Y);
                min = Math.Min(min, v.Z);

                max = Math.Max(max, v.X);
                max = Math.Max(max, v.Y);
                max = Math.Max(max, v.Z);
            }

            m_minimum = min;
            m_maximum = max;

            int maxCount = this.ActualHeight >= 400 ? 10 : height >= 200 ? 6 : height >= 140 ? 4 : 2;
            m_axisValues.ResetValues(AxisHelper.GetValues(m_minimum, m_maximum, maxCount));

            m_axisLabels.StartTime = m_values.Count > 0 ? m_values[0].T : DateTime.MinValue;
            m_axisLabels.Count = 60;

            m_xaxisElement.AxisLabels = m_axisLabels;
            m_yaxisElement.AxisValues = m_axisValues;

            m_plotElement.AxisLabels = m_axisLabels;
            m_plotElement.AxisValues = m_axisValues;
            m_plotElement.Values = m_values;
            m_plotElement.Series = m_series;

            m_legendElement.Series = m_series;
        }

        #endregion // internal methods
    }
}
