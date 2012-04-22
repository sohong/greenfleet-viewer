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
        private double m_startAngle = 30;

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
        private double m_endAngle = 260;

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
        private double m_minimum = 0;

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
        private double m_maximum = 240;

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
        private double m_value = 0;

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
            m_hand.Offset = new Vector(10, 10);
            DrawHand(width - 20, height - 20);
            DrawPin(width, height);
        }

        #endregion // overriden methods


        #region internal methods

        private double ValueToAngle(double value)
        {
            double angle = EndAngle - (EndAngle - StartAngle) * ((value - Minimum) / (Maximum - Minimum));
            return angle;
        }

        private Point GetAnglePos(double cx, double cy, double width, double height, double angle)
        {
            double radius = Math.Max(width, height) / 2;
            double radian = angle * Math.PI / 180;

            double x = Math.Cos(radian) * radius;
            double y = Math.Sin(radian) * radius;

            Point p = new Point(cx + x, cy - y);
            return p;
        }

        private void DrawFrame(double width, double height)
        {
            DrawingContext dc = m_frame.RenderOpen();
            double x = 3;
            double y = 3;
            width -= x * 2;
            height -= x * 2;
            double rd = Math.Max(width, height) / 2;
            double cx = x + rd;
            double cy = y + rd;

            ScaleTransform tx = new ScaleTransform(width > height ? 1 : width / height, height > width ? 1 : height / width);
            dc.PushTransform(tx);

            Brush fill = new SolidColorBrush(UIElement.ToColor(0xffffffff));
            Pen pen = new Pen(fill, 5);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = GetAnglePos(cx, cy, width, height, StartAngle);

            Size sz = new Size(rd, rd);
            Point p = GetAnglePos(cx, cy, width, height, EndAngle);
            ArcSegment arc = new ArcSegment(p, sz, 0, true, SweepDirection.Counterclockwise, true);
            figure.Segments.Add(arc);
            path.Figures.Add(figure);

            dc.DrawGeometry(null, pen, path);

            dc.Pop();
            dc.Close();
        }

        private void DrawHand(double width, double height)
        {
            DrawingContext dc = m_hand.RenderOpen();
            double rd = Math.Max(width, height) / 2;
            Brush fill = new SolidColorBrush(UIElement.ToColor(0xffffffff));
            double angle = ValueToAngle(Value);

            TransformGroup tx = new TransformGroup();
            RotateTransform rotation = new RotateTransform(360 - angle, rd, rd);
            ScaleTransform scale = new ScaleTransform(width > height ? 1 : width / height, height > width ? 1 : height / width);

            tx.Children.Add(rotation);
            tx.Children.Add(scale);
            
            dc.PushTransform(tx);

            Point[] pts = new Point[3];
            pts[0] = new Point(rd, rd - 5);
            pts[1] = new Point(rd, rd + 5);
            pts[2] = new Point(rd * 2, rd);// GetAnglePos(rd, rd, width, height, angle);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = pts[0];
            figure.Segments.Add(new PolyLineSegment(pts, false));
            path.Figures.Add(figure);
            
            dc.DrawGeometry(fill, null, path);

            dc.Pop();
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
