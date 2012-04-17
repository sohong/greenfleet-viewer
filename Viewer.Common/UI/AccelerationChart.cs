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

namespace Viewer.Common.UI {

    /// <summary>
    /// x/y/x 가속도 챠트.
    /// </summary>
    public class AccelerationChart : FrameworkElement {

        #region struct Value

        public struct Value {
            public double X;
            public double Y;
            public double Z;

            public Value(double x, double y, double z) {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }

        #endregion // struct Value


        #region dependency properties

        /// <summary>
        /// MinValueCount
        /// </summary>
        public static readonly DependencyProperty MinValueCountProperty = DependencyProperty.Register(
            "ValueCount", typeof(uint), typeof(AccelerationChart),
            new FrameworkPropertyMetadata((uint)60, OnMinValueCountChanged));
        private static void OnMinValueCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((AccelerationChart)d).RefreshChart();
        }

        #endregion dependency properties


        #region fields

        private VisualCollection m_elements;
        private PlotElement m_plotElement;
        private XAxisElement m_xaxisElement;
        private YAxisElement m_yaxisElement;
        private LegendElement m_legendElement;

        private IList<Value> m_values;
        private double m_minimum = -1;
        private double m_maximum = 1;
        private IList<double> m_axisValues;

        #endregion // fields


        #region constructor

        public AccelerationChart() {
            m_elements = new VisualCollection(this);
            m_elements.Add(m_plotElement = new PlotElement(this));
            m_elements.Add(m_xaxisElement = new XAxisElement(this));
            m_elements.Add(m_yaxisElement = new YAxisElement(this));
            m_elements.Add(m_legendElement = new LegendElement(this));

            m_values = new List<Value>();
            m_axisValues = new List<double>();

            SnapsToDevicePixels = true;
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// X 축에 표시할 최소 value count.
        /// 실제 value 갯수가 적어도 자리를 차지한다.
        /// </summary>
        public uint MinValueCount {
            get { return (uint)GetValue(MinValueCountProperty); }
            set { SetValue(MinValueCountProperty, value); }
        }

        #endregion // properties


        #region methods

        public void Clear() {
            m_values.Clear();
            RefreshChart();
        }

        public void AddValue(double x, double y, double z) {
            m_values.Add(new Value(x, y, z));
            RefreshChart();
        }

        #endregion // methods


        #region overriden methods

        protected override int VisualChildrenCount {
            get { return m_elements.Count; }
        }

        protected override Visual GetVisualChild(int index) {
            return m_elements[index];
        }

        protected override Size ArrangeOverride(Size finalSize) {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutElements(sz.Width, sz.Height);

            return sz;
        }

        #endregion // overriden methods


        #region internal methods

        private void RefreshChart() {
            Recalculate();

            InvalidateArrange();
        }

        private void Recalculate() {
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

            m_axisValues.Clear();
            foreach (double v in AxisHelper.GetValues(m_minimum, m_maximum, 6)) {
                m_axisValues.Add(v);
            }
            m_yaxisElement.Values = m_axisValues;
        }

        private void LayoutElements(double width, double height) {
            if (width * height == 0) return;

            Recalculate();

            double padding = 10;
            double x = padding;
            double y = padding;
            width -= padding * 2;
            height -= padding * 2;

            // legend
            Size sz = m_legendElement.Measure(width, height);
            m_legendElement.Width = sz.Width;
            m_legendElement.Height = height;
            m_legendElement.X = x + width - sz.Width;
            m_legendElement.Y = y;

            width -= m_legendElement.Width;

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

            m_xaxisElement.Width = width;
            m_yaxisElement.Height = height;
            m_xaxisElement.X = x;

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

        #endregion // internal methods
    }
}
