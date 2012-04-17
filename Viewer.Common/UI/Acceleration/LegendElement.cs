////////////////////////////////////////////////////////////////////////////////
// LegendElement.cs
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

    public class LegendElement : ChartElement {

        #region fields

        private LegendItemElement m_elementX;
        private LegendItemElement m_elementY;
        private LegendItemElement m_elementZ;

        #endregion // fields


        #region constructor

        public LegendElement(AccelerationChart chart)
            : base(chart) {

            Children.Add(m_elementX = new LegendItemElement());
            Children.Add(m_elementY = new LegendItemElement());
            Children.Add(m_elementZ = new LegendItemElement());
        }

        #endregion // constructor


        #region overriden methods

        public override void Draw() {
            base.Draw();

            Size boxSize = new Size(10, 10);
            double y = (this.Height - boxSize.Height) / 2;
            Pen border = new Pen(Brushes.Gray, 1);

            if (m_elementX != null) {
                m_elementX.Text = "accel X";
                m_elementX.BoxSize = boxSize;
                m_elementX.Offset = new Vector(this.Width - 200, y);
                m_elementX.Background = new SolidColorBrush(Colors.CadetBlue);
                m_elementX.Border = border;
                m_elementX.Draw();

                m_elementY.Text = "accel Y";
                m_elementY.BoxSize = boxSize;
                m_elementY.Offset = new Vector(this.Width - 130, y);
                m_elementY.Background = new SolidColorBrush(Colors.Goldenrod);
                m_elementY.Border = border;
                m_elementY.Draw();

                m_elementZ.Text = "accel Z";
                m_elementZ.BoxSize = boxSize;
                m_elementZ.Offset = new Vector(this.Width - 60, y);
                m_elementZ.Background = new SolidColorBrush(Colors.PaleVioletRed);
                m_elementZ.Border = border;
                m_elementZ.Draw();
            }
        }

        protected override void DoDraw(DrawingContext dc) {
            if (Width > 0 && Height > 0) {
                double w = this.Width;
                double h = this.Height;

                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#05000000"));
                dc.DrawRectangle(brush, new Pen(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#110000ff")), 1), new Rect(0, 0, w, h));
            }
        }

        public override Size Measure(double hintWidth, double hintHeight) {
            return new Size(0, 21);
        }

        #endregion // overriden methods
    }
}
