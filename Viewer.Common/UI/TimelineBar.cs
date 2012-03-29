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

namespace Viewer.Common.UI {
    
    public class TimelineBar : FrameworkElement {

        #region dependency properties

        /// <summary>
        /// TimeBarHeight
        /// </summary>
        public static readonly DependencyProperty TimeBarHeightProperty = DependencyProperty.Register(
            "TimeBarHeight", typeof(double), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnTimeBarHeightChanged));
        private static void OnTimeBarHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        /// <summary>
        /// TimeBarColor
        /// </summary>
        public static readonly DependencyProperty TimeBarColorProperty = DependencyProperty.Register(
            "TimeBarColor", typeof(Color), typeof(TimelineBar),
            new FrameworkPropertyMetadata(0.8, OnTimeBarColorChanged));
        private static void OnTimeBarColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs a) {
            ((TimelineBar)d).InvalidateVisual();
        }

        #endregion // dependency properties


        #region constructors

        public TimelineBar() {
        }

        #endregion // constructors


        #region properties
        #endregion // properties
    }
}
