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
    public class TimelineBar : FrameworkElement
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

        private VisualCollection m_elements;
        private PlotElement m_plotElement;
        private XAxisElement m_xaxisElement;
        private TimelineTrackerElement m_trackerElement;

        #endregion // fields


        #region constructors

        public TimelineBar()
        {
            m_elements = new VisualCollection(this);
            m_elements.Add(m_plotElement = new PlotElement(this));
            m_elements.Add(m_xaxisElement = new XAxisElement(this));
            m_elements.Add(m_trackerElement = new TimelineTrackerElement(this));

            SnapsToDevicePixels = true;
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);

            SizeChanged += new SizeChangedEventHandler((sender, e) => {
                VisualClip = new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight));
            });
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
        /// 현재 TrackPoint
        /// </summary>
        public TrackPoint TrackPoint
        {
            get { return m_trackPoint; }
            set
            {
                if (value != m_trackPoint) {
                    TrackPoint oldValue = m_trackPoint;
                    m_trackPoint = value;

                    RoutedPropertyChangedEventArgs<TrackPoint> args = new RoutedPropertyChangedEventArgs<TrackPoint>(oldValue, value);
                    args.RoutedEvent = TrackPointChangedEvent;
                    RaiseEvent(args);
                }
            }
        }
        private TrackPoint m_trackPoint;

        #endregion // properties


        #region methods

        public void RefreshBar(TimelineValueCollection values, AxisLabelProvider labels)
        {
            m_plotElement.AxisLabels = labels;
            m_plotElement.Values = values;
            m_xaxisElement.AxisLabels = labels;

            InvalidateArrange();
        }

        public void SetPosition(Track track, TrackPoint point)
        {
            m_trackerElement.X = 100;
        }

        #endregion // methods


        #region overriden properties

        protected override int VisualChildrenCount
        {
            get { return m_elements.Count; }
        }

        #endregion // overriden properties


        #region overriden methods

        protected override Visual GetVisualChild(int index)
        {
            return m_elements[index];
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutElements(sz.Width, sz.Height);
            return sz;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            /*
            TimelineElement element = GetHitTest(e.GetPosition(this));
             */
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            /*
            Point p = e.GetPosition(this);
            TimelineElement element = GetHitTest(p);
            if (element != null) {
                element.MouseMove(p, e.LeftButton == MouseButtonState.Pressed);
            }
            if (element != m_hoverElement) {
                if (m_hoverElement != null) {
                    m_hoverElement.MouseLeave();
                }
                m_hoverElement = element;
                if (m_hoverElement != null) {
                    m_hoverElement.MouseEnter();
                }
            }
             */
        }

        #endregion // overriden methods


        #region internal methods

        /// <summary>
        /// Element들을 배치한다.
        /// </summary>
        protected void LayoutElements(double width, double height)
        {
            if (width * height == 0) return;

            double paddingX = 15;
            double paddingY = 5;
            double x = paddingX;
            double y = paddingY;
            width -= paddingX * 2;
            height -= paddingY * 2;

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
            m_trackerElement.Height = height - m_xaxisElement.Height / 2;
            m_trackerElement.Move(x, y - paddingY);

            m_plotElement.Draw();
            m_xaxisElement.Draw();
            m_trackerElement.Draw();
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
