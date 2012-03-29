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

namespace Viewer.Common.UI {
    
    public class TimelineBar : FrameworkElement {

        #region dependency properties

        /// <summary>
        /// FenceHeight
        /// </summary>
        public static readonly DependencyProperty FenceHeightProperty = DependencyProperty.Register(
            "FenceHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.3, OnFenceHeightChanged));
        private static void OnFenceHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FenceColor
        /// </summary>
        public static readonly DependencyProperty FenceFillProperty = DependencyProperty.Register(
            "FenceFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFenceFillChanged));
        private static void OnFenceFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
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
            new FrameworkPropertyMetadata(Colors.Blue, OnNormalFillChanged));
        private static void OnNormalFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// EventFill
        /// </summary>
        public static readonly DependencyProperty EventFillProperty = DependencyProperty.Register(
            "EventFill", typeof(Brush), typeof(TimelineBar),
            new FrameworkPropertyMetadata(Colors.Red, OnEventFillChanged));
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
            new FrameworkPropertyMetadata(0.8, OnFontStyleChanged));
        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateArrange();
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// FontWeight
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight", typeof(FontWeight), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnFontWeightChanged));
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
            TrackPointChangedEvent = EventManager.RegisterRoutedEvent(
                "TrackPointChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<TrackPoint>), typeof(TimelineBar));
        }
        
        #endregion // static constructor


        #region constructors

        public TimelineBar() {
        }

        #endregion // constructors


        #region events

        public event RoutedPropertyChangedEventHandler<TrackPoint> TrackPointChanged {
            add { AddHandler(TrackPointChangedEvent, value); }
            remove { RemoveHandler(TrackPointChangedEvent, value); }
        }
        
        #endregion // events


        #region properties

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

        protected override Size ArrangeOverride(Size finalSize) {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutChldren(sz.Width, sz.Height);
            return sz;
        }

        #endregion // overriden methods


        #region internal methods

        protected void LayoutChldren(double width, double height) {
        }

        #endregion // internal methods
    }
}
