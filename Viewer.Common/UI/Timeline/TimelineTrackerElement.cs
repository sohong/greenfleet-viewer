////////////////////////////////////////////////////////////////////////////////
// TimelineTrackerElement.cs
// 2012.03.29, created by sohong
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
using System.Windows.Media;
using System.Globalization;
using Viewer.Common.Model;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// Timeline tracker.
    /// </summary>
    public class TimelineTrackerElement : TimelineElement
    {
        #region fields

        private bool m_dragging;
        private double m_startX;
        private Point m_startPos;
        private DateTime m_dragTime;

        #endregion // fields


        #region constructor

        public TimelineTrackerElement(TimelineBar bar)
            : base(bar)
        {
            this.Fill = new SolidColorBrush(ToColor(0xff0000ff));
        }

        #endregion // constructor


        #region properties

        public DateTime Time
        {
            get { return m_time; }
            set
            {
                if (value != m_time) {
                    m_time = value;
                    Draw();
                }
            }
        }
        private DateTime m_time;

        public bool LeftLabel
        {
            get;
            set;
        }

        public bool ShowLabel
        {
            get;
            set;
        }

        public double StartX
        {
            get;
            set;
        }

        public double EndX
        {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight)
        {
            string s = Time.ToString("MM-dd HH:mm:ss");
            Typeface face = new Typeface("Tahoma");
            FormattedText ft = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 11, Brushes.Black);
            return new Size(ft.Width + 4, ft.Height);
        }

        protected override void DoDraw(DrawingContext dc)
        {
            // vertical line
            Pen pen = new Pen(this.Fill, 1);
            dc.DrawLine(pen, new Point(0, 0), new Point(0, Height));

            // top
            PathGeometry path = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(-6, 0);
            figure.Segments.Add(new LineSegment(new Point(0, 6), true));
            figure.Segments.Add(new LineSegment(new Point(6, 0), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(this.Fill, pen, path);

            // bottom
            double y = Height;
            path = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(-6, y);
            figure.Segments.Add(new LineSegment(new Point(0, y - 6), true));
            figure.Segments.Add(new LineSegment(new Point(6, y), true));
            path.Figures.Add(figure);
            dc.DrawGeometry(this.Fill, pen, path);

            if (ShowLabel || m_dragging) {
                // label
                string s = (m_dragging ? m_dragTime : Time).ToString("MM-dd HH:mm:ss");
                Typeface face = new Typeface("Tahoma");
                Brush fill = new SolidColorBrush(ToColor(0xdd0000ff));
                FormattedText ft = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 11, fill);

                double x = LeftLabel ? -ft.Width - 4 : 4;
                y = this.Height - ft.Height / 2;
                dc.DrawRectangle(Brushes.Transparent, null, new Rect(x, y, ft.Width, ft.Height));
                dc.DrawText(ft, new Point(x, y));
            }
        }

        protected override void DoMouseDown(Point p)
        {
            m_startPos = p;
            m_startX = this.X;
            m_dragging = true;
        }

        protected override void DoMouseMove(Point p, bool pushed)
        {
            if (m_dragging) {
                if (pushed) {
                    double diff = p.X - m_startPos.X;
                    double x = m_startX + diff;
                    x = Math.Max(StartX, Math.Min(EndX, x));
                    DateTime t = new DateTime();
                    Bar.GetTimeAtPos(x - StartX, ref t);
                    m_dragTime = t;
                    this.X = x;
                    Draw();

                } else {
                    m_dragging = false;
                }
            }
        }

        protected override void DoMouseUp(Point p)
        {
            if (m_dragging) {
                m_dragging = false;
                double diff = p.X - m_startPos.X;
                double x = m_startX + diff;
                x = Math.Max(StartX, Math.Min(EndX, x));
                DateTime t = new DateTime();
                if (Bar.GetTimeAtPos(x - StartX, ref t)) {
                } else {
                    this.X = m_startX;
                }
            }
        }

        #endregion // overriden methods
    }
}
