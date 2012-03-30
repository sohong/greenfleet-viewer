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

namespace Viewer.Common.UI {
    
    public class TimelineBar : FrameworkElement {

        #region dependency properties

        public static readonly DependencyProperty TracksProperty = DependencyProperty.Register(
            "Tracks", typeof(IEnumerable<Track>), typeof(TimelineBar),
            new FrameworkPropertyMetadata(null, OnTracksChanged));
        private static void OnTracksChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).UnregisterTracksEvents(a.OldValue);
            ((TimelineBar)d).RefrechElements();
            ((TimelineBar)d).RegisterTracksEvents(a.NewValue);
        }

        /// <summary>
        /// FenceHeight
        /// </summary>
        public static readonly DependencyProperty FenceHeightProperty = DependencyProperty.Register(
            "FenceHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFenceHeightChanged));
        private static void OnFenceHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
        }

        /// <summary>
        /// FenceFill
        /// </summary>
        public static readonly DependencyProperty FenceFillProperty = DependencyProperty.Register(
            "FenceFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Green, OnFenceFillChanged));
        private static void OnFenceFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).m_fence.Fill = (Brush)a.NewValue;
        }

        /// <summary>
        /// Tracker Fill
        /// </summary>
        public static readonly DependencyProperty TrackerFillProperty = DependencyProperty.Register(
            "TrackerFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Yellow, OnTrackerFillChanged));
        private static void OnTrackerFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).m_tracker.Fill = (Brush)a.NewValue;
        }

        /// <summary>
        /// Tracker Border
        /// </summary>
        public static readonly DependencyProperty TrackerBorderProperty = DependencyProperty.Register(
            "TrackerBorder", typeof(Pen), typeof(TimelineBar),
            new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1), OnTrackerBorderChanged));
        private static void OnTrackerBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).m_tracker.Border = (Pen)a.NewValue;
        }

        /// <summary>
        /// RangeMarker Fill
        /// </summary>
        public static readonly DependencyProperty RangeMarkerFillProperty = DependencyProperty.Register(
            "RangeMarkerFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Yellow, OnRangeMarkerFillChanged));
        private static void OnRangeMarkerFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            foreach (TimeRangeMarkerVisual marker in ((TimelineBar)d).m_markerLayer.Children) {
                marker.Fill = (Brush)a.NewValue;
            }
        }

        /// <summary>
        /// TimeBarHeight
        /// </summary>
        public static readonly DependencyProperty TimeBarHeightProperty = DependencyProperty.Register(
            "TimeBarHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnTimeBarHeightChanged));
        private static void OnTimeBarHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// NormalFill
        /// </summary>
        public static readonly DependencyProperty NormalFillProperty = DependencyProperty.Register(
            "NormalFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Blue, OnNormalFillChanged));
        private static void OnNormalFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// EventFill
        /// </summary>
        public static readonly DependencyProperty EventFillProperty = DependencyProperty.Register(
            "EventFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Brushes.Red, OnEventFillChanged));
        private static void OnEventFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// Font family
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily", typeof(string), typeof(TimelineBar),
            new FrameworkPropertyMetadata(null, OnFontFamilyChanged));
        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontSize
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFontSizeChanged));
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontStyle
        /// </summary>
        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            "FontStyle", typeof(FontStyle), typeof(TimelineBar),
            new FrameworkPropertyMetadata(FontStyles.Normal, OnFontStyleChanged));
        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontWeight
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight", typeof(FontWeight), typeof(TimelineBar),
            new FrameworkPropertyMetadata(FontWeights.Normal, OnFontWeightChanged));
        private static void OnFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        #endregion // dependency properties


        #region routed events

        public static readonly RoutedEvent TrackPointChangedEvent;

        #endregion // routed events


        #region static constructor
        
        static TimelineBar() {
            // events
            TrackPointChangedEvent = EventManager.RegisterRoutedEvent(
                "TrackPointChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<TrackPoint>), typeof(TimelineBar));
        }
        
        #endregion // static constructor


        #region fields

        private IList<TimelineElement> m_elements;
        private TimelineElement m_rangeLayer;
        private TimelineElement m_markerLayer;
        private TimelineElement m_tickLayer;
        private FenceVisual m_fence;
        private TimeTrackerVisual m_tracker;
        
        #endregion // fields


        #region constructors

        public TimelineBar() {
            m_elements = new List<TimelineElement>();

            AddElement(m_fence = new FenceVisual(this) {
                Fill = FenceFill
            });
            AddElement(m_rangeLayer = new TimelineElement(this));
            AddElement(m_markerLayer = new TimelineElement(this));
            AddElement(m_tracker = new TimeTrackerVisual(this) {
                Fill = TrackerFill,
                Border = TrackerBorder
            });
            AddElement(m_tickLayer = new TimelineElement(this));
        }

        #endregion // constructors


        #region events

        public event RoutedPropertyChangedEventHandler<TrackPoint> TrackPointChanged {
            add { AddHandler(TrackPointChangedEvent, value); }
            remove { RemoveHandler(TrackPointChangedEvent, value); }
        }
        
        #endregion // events


        #region properties

        public IEnumerable<Track> Tracks {
            get { return (IEnumerable<Track>)GetValue(TracksProperty); }
            set { SetValue(TracksProperty, value); }
        }

        /// <summary>
        /// Fence height
        /// </summary>
        public double FenceHeight {
            get { return (double)GetValue(FenceHeightProperty); }
            set { SetValue(FenceHeightProperty, value); }
        }

        /// <summary>
        /// Fence fill
        /// </summary>
        public Brush FenceFill {
            get { return (Brush)GetValue(FenceFillProperty); }
            set { SetValue(FenceFillProperty, value); }
        }

        /// <summary>
        /// Tracker fill
        /// </summary>
        public Brush TrackerFill {
            get { return (Brush)GetValue(TrackerFillProperty); }
            set { SetValue(TrackerFillProperty, value); }
        }

        /// <summary>
        /// Tracker Border
        /// </summary>
        public Pen TrackerBorder {
            get { return (Pen)GetValue(TrackerBorderProperty); }
            set { SetValue(TrackerBorderProperty, value); }
        }

        /// <summary>
        /// TimeBar height
        /// </summary>
        public double TimeBarHeight {
            get { return (double)GetValue(TimeBarHeightProperty); }
            set { SetValue(TimeBarHeightProperty, value); }
        }

        /// <summary>
        /// TimeBar normal fill
        /// </summary>
        public Brush NormalFill {
            get { return (Brush)GetValue(NormalFillProperty); }
            set { SetValue(NormalFillProperty, value); }
        }

        /// <summary>
        /// TimeBar event fill
        /// </summary>
        public Brush EventFill {
            get { return (Brush)GetValue(EventFillProperty); }
            set { SetValue(EventFillProperty, value); }
        }

        /// <summary>
        /// Font family
        /// </summary>
        public string FontFamily {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Font size
        /// </summary>
        public double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Font style
        /// </summary>
        public FontStyle FontStyle {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        /// Font weight
        /// </summary>
        public FontWeight FontWeight {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        /// 현재 TrackPoint
        /// </summary>
        public TrackPoint TrackPoint {
            get { return m_trackPoint; }
            set {
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


        #region overriden methods

        protected override int VisualChildrenCount {
            get {
                return m_elements.Count;
            }
        }

        protected override Visual GetVisualChild(int index) {
            return m_elements[index];
        }

        protected override Size ArrangeOverride(Size finalSize) {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutChldren(sz.Width, sz.Height);

            return sz;
        }

        #endregion // overriden methods


        #region internal methods

        private void RegisterTracksEvents(object source) {
        }

        private void UnregisterTracksEvents(object source) {
        }

        private void AddElement(TimelineElement element) {
            if (!m_elements.Contains(element)) {
                m_elements.Add(element);
            }
        }

        private void RemoveElement(TimelineElement element) {
            if (m_elements.Contains(element)) {
                m_elements.Remove(element);
            }
        }

        /// <summary>
        /// track element를 제외한 모든 element들을 새로 생성한다.
        /// </summary>
        private void RefrechElements() {
            m_markerLayer.Children.Clear();
            for (int i = 0; i < 3; i++) {
                TimeRangeMarkerVisual v = new TimeRangeMarkerVisual(this);
                m_markerLayer.Children.Add(v);
            }

            InvalidateArrange();
        }

        /// <summary>
        /// Element들을 배치한다.
        /// </summary>
        protected void LayoutChldren(double width, double height) {
            // fence
            m_fence.Offset = new Vector(0, 5);
            m_fence.Width = width;
            m_fence.Height = height - 5;
            m_fence.Draw();

            // tracker
            m_tracker.Offset = new Vector(100, 0);
            m_tracker.Width = 5;
            m_tracker.Height = height;
            m_tracker.Draw();

            // range markers
            foreach (TimeRangeMarkerVisual marker in m_markerLayer.Children) {
                marker.Width = 10;
                marker.Height = 7;
                marker.Draw();
            }
        }

        #endregion // internal methods
    }
}
