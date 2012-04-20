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
        #region dependency properties

        public static readonly DependencyProperty TracksProperty = DependencyProperty.Register(
            "Tracks", typeof(TrackCollection), typeof(TimelineBar),
            new FrameworkPropertyMetadata(null, OnTracksChanged));
        private static void OnTracksChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).UnregisterTracksEvents(a.OldValue);
            ((TimelineBar)d).RefreshElements();
            ((TimelineBar)d).RegisterTracksEvents(a.NewValue);
        }

        /// <summary>
        /// HoverFill
        /// </summary>
        public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
            "HoverFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.White, OnHoverFillChanged));
        private static void OnHoverFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
        }

        /// <summary>
        /// FenceHeight
        /// </summary>
        public static readonly DependencyProperty FenceHeightProperty = DependencyProperty.Register(
            "FenceHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFenceHeightChanged));
        private static void OnFenceHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateArrange();
        }

        /// <summary>
        /// PlotFill
        /// </summary>
        public static readonly DependencyProperty PlotFillProperty = DependencyProperty.Register(
            "PlotFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Green, OnPlotFillChanged));
        private static void OnPlotFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            //((TimelineBar)d).m_fence.Fill = (Brush)a.NewValue;
        }

        /// <summary>
        /// Tracker Fill
        /// </summary>
        public static readonly DependencyProperty TrackerFillProperty = DependencyProperty.Register(
            "TrackerFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Yellow, OnTrackerFillChanged));
        private static void OnTrackerFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            //((TimelineBar)d).m_tracker.Fill = (Brush)a.NewValue;
        }

        /// <summary>
        /// Tracker Border
        /// </summary>
        public static readonly DependencyProperty TrackerBorderProperty = DependencyProperty.Register(
            "TrackerBorder", typeof(Pen), typeof(TimelineBar),
            new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1), OnTrackerBorderChanged));
        private static void OnTrackerBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            //((TimelineBar)d).m_tracker.Border = (Pen)a.NewValue;
        }

        /// <summary>
        /// RangeMarker Fill
        /// </summary>
        public static readonly DependencyProperty RangeMarkerFillProperty = DependencyProperty.Register(
            "RangeMarkerFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Black, OnRangeMarkerFillChanged));
        private static void OnRangeMarkerFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            /*
            foreach (RangeMarkerElement marker in ((TimelineBar)d).m_markerLayer.Children) {
                marker.Fill = (Brush)a.NewValue;
            }
             */
        }

        /// <summary>
        /// TimeBarHeight
        /// </summary>
        public static readonly DependencyProperty TimeBarHeightProperty = DependencyProperty.Register(
            "TimeBarHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnTimeBarHeightChanged));
        private static void OnTimeBarHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// NormalFill
        /// </summary>
        public static readonly DependencyProperty NormalFillProperty = DependencyProperty.Register(
            "NormalFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Blue, OnNormalFillChanged));
        private static void OnNormalFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// EventFill
        /// </summary>
        public static readonly DependencyProperty EventFillProperty = DependencyProperty.Register(
            "EventFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Red, OnEventFillChanged));
        private static void OnEventFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// Font family
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily", typeof(string), typeof(TimelineBar),
            new FrameworkPropertyMetadata(null, OnFontFamilyChanged));
        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontSize
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFontSizeChanged));
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontStyle
        /// </summary>
        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            "FontStyle", typeof(FontStyle), typeof(TimelineBar),
            new FrameworkPropertyMetadata(FontStyles.Normal, OnFontStyleChanged));
        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontWeight
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight", typeof(FontWeight), typeof(TimelineBar),
            new FrameworkPropertyMetadata(FontWeights.Normal, OnFontWeightChanged));
        private static void OnFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        #endregion // dependency properties


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

        private TimelineValueCollection m_values;

        #endregion // fields


        #region constructors

        public TimelineBar()
        {
            m_elements = new VisualCollection(this);
            m_elements.Add(m_plotElement = new PlotElement(this));
            m_elements.Add(m_xaxisElement = new XAxisElement(this));

            m_values = new TimelineValueCollection(DateTime.Today, DateTime.Today.AddDays(1));

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

        public TimelineValueCollection Values
        {
            get { return m_values; }
        }
       
        /// <summary>
        /// Element들이 hovered 상태일 때 fill.
        /// </summary>
        public Brush HoverFill
        {
            get { return (Brush)GetValue(HoverFillProperty); }
            set { SetValue(HoverFillProperty, value); }
        }

        public TrackCollection Tracks
        {
            get { return (TrackCollection)GetValue(TracksProperty); }
            set { SetValue(TracksProperty, value); }
        }

        /// <summary>
        /// Fence height
        /// </summary>
        public double FenceHeight
        {
            get { return (double)GetValue(FenceHeightProperty); }
            set { SetValue(FenceHeightProperty, value); }
        }

        /// <summary>
        /// Fence fill
        /// </summary>
        public Brush PlotFill
        {
            get { return (Brush)GetValue(PlotFillProperty); }
            set { SetValue(PlotFillProperty, value); }
        }

        /// <summary>
        /// Tracker fill
        /// </summary>
        public Brush TrackerFill
        {
            get { return (Brush)GetValue(TrackerFillProperty); }
            set { SetValue(TrackerFillProperty, value); }
        }

        /// <summary>
        /// Tracker Border
        /// </summary>
        public Pen TrackerBorder
        {
            get { return (Pen)GetValue(TrackerBorderProperty); }
            set { SetValue(TrackerBorderProperty, value); }
        }

        /// <summary>
        /// RangeMarker fill
        /// </summary>
        public Brush RangeMarkerFill
        {
            get { return (Brush)GetValue(RangeMarkerFillProperty); }
            set { SetValue(RangeMarkerFillProperty, value); }
        }

        /// <summary>
        /// TimeBar height
        /// </summary>
        public double TimeBarHeight
        {
            get { return (double)GetValue(TimeBarHeightProperty); }
            set { SetValue(TimeBarHeightProperty, value); }
        }

        /// <summary>
        /// TimeBar normal fill
        /// </summary>
        public Brush NormalFill
        {
            get { return (Brush)GetValue(NormalFillProperty); }
            set { SetValue(NormalFillProperty, value); }
        }

        /// <summary>
        /// TimeBar event fill
        /// </summary>
        public Brush EventFill
        {
            get { return (Brush)GetValue(EventFillProperty); }
            set { SetValue(EventFillProperty, value); }
        }

        /// <summary>
        /// Font family
        /// </summary>
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Font size
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Font style
        /// </summary>
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        /// Font weight
        /// </summary>
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
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

        private void tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshElements();
        }

        private void RegisterTracksEvents(object source)
        {
            INotifyCollectionChanged coll = source as INotifyCollectionChanged;
            if (coll != null) {
                coll.CollectionChanged += new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }
        }

        private void UnregisterTracksEvents(object source)
        {
            INotifyCollectionChanged coll = source as INotifyCollectionChanged;
            if (coll != null) {
                coll.CollectionChanged -= new NotifyCollectionChangedEventHandler(tracks_CollectionChanged);
            }
        }

        /// <summary>
        /// TimelineValue들을 새로 계산/생성해서 element들에 반영시킨다.
        /// </summary>
        private void RefreshElements()
        {
        }

        /// <summary>
        /// Element들을 배치한다.
        /// </summary>
        protected void LayoutElements(double width, double height)
        {
            /*
            if (width * height == 0) return;

            // fence
            m_fence.Offset = new Vector(0, 5);
            m_fence.Width = width;
            m_fence.Height = height - 5;
            m_fence.Draw();

            // ranges
            foreach (TimeRangeElement range in m_rangeLayer.Children) {
                TrackRange r = (TrackRange)(range.Data);
                double x = width * Tracks.GetPosition(r.StartTrack) / Tracks.Length;
                range.Offset = new Vector(x, 10);
                range.Height = height - 20;
                range.Width = width * (Tracks.GetPosition(r.EndTrack) + 1) / Tracks.Length - x;
                range.Draw();
            }

            // tracker
            m_tracker.Offset = new Vector(100, 0);
            m_tracker.Width = 5;
            m_tracker.Height = height;
            m_tracker.Draw();

            // range markers
            foreach (RangeMarkerElement marker in m_markerLayer.Children) {
                double x = width * Tracks.GetPosition((Track)marker.Data) / Tracks.Length;
                marker.Offset = new Vector(x, height - marker.Height);
                marker.HoverFill = this.HoverFill;
                marker.Draw();
            }
             */
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
