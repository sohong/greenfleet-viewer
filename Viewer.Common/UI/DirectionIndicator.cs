////////////////////////////////////////////////////////////////////////////////
// DirectionIndicator.cs
// 2012.04.22, created by sohong
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
using System.Globalization;
using System.Windows;

namespace Viewer.Common.UI
{
    /// <summary>
    /// 방향계
    /// </summary>
    public class DirectionIndicator : UIContainer
    {

        #region fields

        private DrawingVisual m_labels;
        private DrawingVisual m_hands;

        #endregion // fields


        #region constructors

        public DirectionIndicator()
        {
            RenderOptions.SetEdgeMode(this, EdgeMode.Unspecified);
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// Angle
        /// </summary>
        public double Angle
        {
            get { return m_angle; }
            set
            {
                if (value != m_angle) {
                    m_angle = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_angle;

        #endregion // properties


        #region overriden methods

        protected override void CreateElements()
        {
            AddElement(m_labels = new DrawingVisual());
            AddElement(m_hands = new DrawingVisual());
        }

        protected override void LayoutElements(double width, double height)
        {
            DrawLabels(width, height);
            DrawHands(width, height);
        }

        #endregion // overriden methods


        #region internal methods

        private void DrawLabels(double width, double height)
        {
            DrawingContext dc = m_labels.RenderOpen();
            double x, y;
            Typeface face = new Typeface("Tahoma");
            Brush fill = new SolidColorBrush(UIElement.ToColor(0xffcccccc));

            // north
            x = width / 2;
            y = 0;
            FormattedText ft = new FormattedText("N", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 14, fill);
            dc.DrawText(ft, new Point(x - ft.Width / 2, y));

            // south
            x = width / 2;
            y = height;
            ft = new FormattedText("S", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 14, fill);
            dc.DrawText(ft, new Point(x - ft.Width / 2, y - ft.Height));

            // west
            x = 0;
            y = height / 2;
            ft = new FormattedText("W", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 14, fill);
            dc.DrawText(ft, new Point(x, y - ft.Height / 2));

            // east
            x = width;
            y = height / 2;
            ft = new FormattedText("E", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 14, fill);
            dc.DrawText(ft, new Point(x - ft.Width, y - ft.Height / 2));

            dc.Close();
        }

        private void DrawHands(double width, double height)
        {
            DrawingContext dc = m_hands.RenderOpen();

            double cx = width / 2;
            double cy = height / 2;
            double len = 8;
            double y = 15;
            double half = Math.Min(width, height) / 2 / 2;
            width -= 15 * 2;
            height -= 15 * 2;

            Brush fillWhite = new SolidColorBrush(UIElement.ToColor(0x88ffffff));
            Brush fillGray = new SolidColorBrush(UIElement.ToColor(0x88888888));
            Brush fillYellow = new SolidColorBrush(UIElement.ToColor(0xffffffff));
            Pen pen = new Pen(Brushes.Gray, 1);

            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(cx - half, cy);
            figure.Segments.Add(new LineSegment(new Point(cx, cy - 5), true));
            figure.Segments.Add(new LineSegment(new Point(cx + half, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, cy + 5), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(Brushes.White, pen, path);

            dc.DrawEllipse(Brushes.White, null, new Point(cx, cy), 7, 7);

            path = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(cx, y);
            figure.Segments.Add(new LineSegment(new Point(cx - len, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, y), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(fillWhite, pen, path);

            path = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(cx, y);
            figure.Segments.Add(new LineSegment(new Point(cx + len, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, y), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(fillYellow, pen, path);

            y = cy + height / 2;
            path = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(cx, y);
            figure.Segments.Add(new LineSegment(new Point(cx - len, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, y), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(fillGray, pen, path);

            path = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(cx, y);
            figure.Segments.Add(new LineSegment(new Point(cx + len, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, cy), true));
            figure.Segments.Add(new LineSegment(new Point(cx, y), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(fillGray, pen, path);

            dc.Close();
        }

        #endregion // internal methods
    }
}
