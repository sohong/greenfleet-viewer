////////////////////////////////////////////////////////////////////////////////
// TimelineBar.cs
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
using Viewer.Common.Model;
using Viewer.Common.UI.Timeline;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Windows.Input;

namespace Viewer.Common.UI
{
    /// <summary>
    /// 하나 이상의 트랙그룹이 재생되는 상태를 표시하거나,
    /// 재생 지점을 지정할 수 있도록 한다.
    /// </summary>
    public class TimelineBar : UIContainer
    {
        #region routed events

        public static readonly RoutedEvent TrackPointChangedEvent;

        #endregion // routed events


        #region static constructor

        static TimelineBar()
        {
            // events
            TrackPointChangedEvent = EventManager.RegisterRoutedEvent(
                "TrackPointChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<TrackPoint>), typeof(TimelineBar));
        }

        #endregion // static constructor


        #region fields

        private PlotElement m_plotElement;
        private XAxisElement m_xaxisElement;
        private TimelineTrackerElement m_trackerElement;

        private TimelineElement m_clickedElement;
        private TimelineElement m_hoveredElement;

        #endregion // fields


        #region constructors

        public TimelineBar()
        {
        }

        #endregion // constructors


        #region events

        public event RoutedPropertyChangedEventHandler<TrackPoint> TrackPointChanged
        {
            add { AddHandler(TrackPointChangedEvent, value); }
            remove { RemoveHandler(TrackPointChangedEvent, value); }
        }

        #endregion // events


        #region properties

        /// <summary>
        /// 상시 영역 fill
        /// </summary>
        public Brush AllBackground
        {
            get;
            set;
        }

        /// <summary>
        /// 이벤트 영역 fill
        /// </summary>
        public Brush EventBackground
        {
            get;
            set;
        }

        /// <summary>
        /// padding left
        /// </summary>
        public double PaddingLeft
        {
            get { return m_paddingLeft; }
            set
            {
                if (value != m_paddingLeft) {
                    m_paddingLeft = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_paddingLeft = 15;

        /// <summary>
        /// padding right
        /// </summary>
        public double PaddingRight
        {
            get { return m_paddingRight; }
            set
            {
                if (value != m_paddingRight) {
                    m_paddingRight = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_paddingRight = 15;

        /// <summary>
        /// padding Top
        /// </summary>
        public double PaddingTop
        {
            get { return m_paddingTop; }
            set
            {
                if (value != m_paddingTop) {
                    m_paddingTop = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_paddingTop = 10;

        /// <summary>
        /// padding Bottom
        /// </summary>
        public double PaddingBottom
        {
            get { return m_paddingBottom; }
            set
            {
                if (value != m_paddingBottom) {
                    m_paddingBottom = value;
                    InvalidateArrange();
                }
            }
        }
        private double m_paddingBottom = 10;

        /// <summary>
        /// 현재 TrackPoint
        /// </summary>
        public TrackPoint Position
        {
            get { return m_position; }
            set
            {
                if (value != m_position) {
                    TrackPoint oldValue = m_position;
                    m_position = value;
                    ResetPosition();

                    RoutedPropertyChangedEventArgs<TrackPoint> args = new RoutedPropertyChangedEventArgs<TrackPoint>(oldValue, value);
                    args.RoutedEvent = TrackPointChangedEvent;
                    RaiseEvent(args);
                }
            }
        }
        private TrackPoint m_position;

        #endregion // properties


        #region methods

        public void RefreshBar(TimelineValueCollection values, AxisLabelProvider labels)
        {
            m_plotElement.AxisLabels = labels;
            m_plotElement.Values = values;
            m_xaxisElement.AxisLabels = labels;

            InvalidateArrange();
        }

        public bool GetTimeAtPos(double x, ref DateTime t)
        {
            x = x / m_xaxisElement.Width;
            AxisLabelProvider labels = m_xaxisElement.AxisLabels;
            int mins = (int)TimeSpan.FromTicks(labels.EndTime.Ticks - labels.StartTime.Ticks).TotalMinutes;
            t = labels.StartTime.AddMinutes(mins * x).StripSeconds();

            TimelineValue value = m_plotElement.Values.GetValueAt(t);
            return value != null;
        }

        #endregion // methods


        #region overriden methods

        protected override void CreateElements()
        {
            AddElement(m_plotElement = new PlotElement(this));
            AddElement(m_xaxisElement = new XAxisElement(this));
            AddElement(m_trackerElement = new TimelineTrackerElement(this));
        }

        /// <summary>
        /// Element들을 배치한다.
        /// </summary>
        protected override void LayoutElements(double width, double height)
        {
            if (width * height == 0) return;

            double x = PaddingLeft;
            double y = PaddingTop;
            width -= PaddingLeft + PaddingRight;
            height -= PaddingTop + PaddingBottom;

            // x-axis
            Size sz = m_xaxisElement.Measure(width, height);
            m_xaxisElement.Height = sz.Height;
            m_xaxisElement.Width = width;
            m_xaxisElement.Move(x, y + height - sz.Height);

            // plot
            m_plotElement.Width = width;
            m_plotElement.Height = height - m_xaxisElement.Height;
            m_plotElement.Move(x, y);

            // tracker
            m_trackerElement.StartX = x;
            m_trackerElement.EndX = x + width;
            m_trackerElement.Height = height - m_xaxisElement.Height / 2;
            m_trackerElement.Move(x, y - PaddingTop / 2);

            m_plotElement.Draw();
            m_xaxisElement.Draw();
            m_trackerElement.Draw();

            ResetPosition();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            CaptureMouse();

            Point p = e.GetPosition(this);
            m_clickedElement = GetHitTest(p);
            if (m_clickedElement != null) {
                m_clickedElement.MouseDown(p);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point p = e.GetPosition(this);

            if (m_clickedElement != null && e.LeftButton == MouseButtonState.Pressed) {
                m_clickedElement.MouseMove(p, true);
            } else {
                TimelineElement element = GetHitTest(p);
                if (element != null) {
                    element.MouseMove(p, e.LeftButton == MouseButtonState.Pressed);
                }
                if (element != m_hoveredElement) {
                    if (m_hoveredElement != null) {
                        m_hoveredElement.MouseLeave();
                    }
                    m_hoveredElement = element;
                    if (m_hoveredElement != null) {
                        m_hoveredElement.MouseEnter();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            ReleaseMouseCapture();

            Point p = e.GetPosition(this);

            if (m_clickedElement != null) {
                m_clickedElement.MouseUp(p);
                m_clickedElement = null;
            } else {
                TimelineElement element = GetHitTest(p);
                if (element != null) {
                    element.MouseUp(p);
                }
            }
        }

        #endregion // overriden methods


        #region internal methods

        private void ResetPosition()
        {
            AxisLabelProvider labels = m_xaxisElement.AxisLabels;
            if (labels != null && m_position != null) {
                double x = labels.GetPosition(m_position.PointTime);
                m_trackerElement.Time = m_position.PointTime;

                x = PaddingLeft + x * m_xaxisElement.Width;
                m_trackerElement.ShowLabel = true;
                m_trackerElement.LeftLabel = x > m_xaxisElement.Width - m_trackerElement.Measure(0, 0).Width;
                m_trackerElement.X = x;
            } else {
                m_trackerElement.ShowLabel = false;
                m_trackerElement.Time = new DateTime();
                m_trackerElement.X = PaddingLeft;
            }
        }

        private TimelineElement GetHitTest(Point p)
        {
            HitTestResult hr = VisualTreeHelper.HitTest(this, p);
            if (hr != null) {
                return hr.VisualHit as TimelineElement;
            }
            return null;
        }

        #endregion // internal methods
    }
}
