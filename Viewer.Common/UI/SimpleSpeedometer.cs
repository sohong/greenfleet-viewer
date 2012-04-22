////////////////////////////////////////////////////////////////////////////////
// SimpleSpeedometer.cs
// 2012.04.22, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Common.UI
{
    /// <summary>
    /// 속도계
    /// </summary>
    public class SimpleSpeedometer : UIContainer
    {
        #region fields

        private DrawingVisual m_frame;
        private DrawingVisual m_hand;
        private DrawingVisual m_pin;

        #endregion // fields


        #region constructors

        public SimpleSpeedometer()
        {
            RenderOptions.SetEdgeMode(this, EdgeMode.Unspecified);
        }

        #endregion // constructors


        #region properties

        public double StartAngle
        {
            get { return m_startAngle; }
            set
            {
                if (value != m_startAngle) {
                    m_startAngle = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_startAngle;

        public double EndAngle
        {
            get { return m_endAngle; }
            set
            {
                if (value != m_endAngle) {
                    m_endAngle = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_endAngle;

        public double Minimum
        {
            get { return m_minimum; }
            set
            {
                if (value != m_minimum) {
                    m_minimum = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_minimum;

        public double Maximum
        {
            get { return m_maximum; }
            set
            {
                if (value != m_maximum) {
                    m_maximum = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_maximum;

        public double Value
        {
            get { return m_value; }
            set
            {
                if (value != m_value) {
                    m_value = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_value;

        #endregion // properties


        #region overriden methods

        protected override void CreateElements()
        {
            AddElement(m_frame = new DrawingVisual());
            AddElement(m_hand = new DrawingVisual());
            AddElement(m_pin = new DrawingVisual());
        }

        protected override void LayoutElements(double width, double height)
        {
            DrawFrame(width, height);
            DrawHand(width, height);
            DrawPin(width, height);
        }

        #endregion // overriden methods


        #region internal methods

        private void DrawFrame(double width, double height)
        {
        }

        private void DrawHand(double width, double height)
        {
            DrawingContext dc = m_hand.RenderOpen();
            double x = width / 2;
            double y = height / 2;
            Brush fill = new SolidColorBrush(UIElement.ToColor(0xffffffff));

            Point[] pts = new Point[3];
            pts[0] = new Point(x, y);
            pts[1] = new Point(x - 20, y - 20);
            pts[2] = new Point(x - 20, y + 20);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.Segments.Add(new PolyLineSegment(pts, false));
            path.Figures.Add(figure);
            
            dc.DrawGeometry(fill, null, path);
            
            dc.Close();
        }

        private void DrawPin(double width, double height)
        {
            DrawingContext dc = m_pin.RenderOpen();
            double x = width / 2;
            double y = height / 2;
            Brush fill = new SolidColorBrush(UIElement.ToColor(0xffffffff));

            double v = Math.Max(width, height);
            double w = 7 * width / v;
            double h = 7 * height / v;
            dc.DrawEllipse(fill, null, new Point(x, y), w, h);

            dc.Close();
        }

        #endregion // internal methods
    }
}
