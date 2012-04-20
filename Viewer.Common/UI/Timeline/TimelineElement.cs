////////////////////////////////////////////////////////////////////////////////
// TimelineElement.cs
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
using System.Windows.Media;
using System.Windows;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// Visual element base for TimelineBar
    /// </summary>
    public abstract class TimelineElement : UIElement
    {
        #region constructor

        public TimelineElement(TimelineBar bar)
            : base(bar)
        {
        }

        #endregion // constructor


        #region properties

        public TimelineBar Bar
        {
            get { return Container as TimelineBar; }
        }

        /// <summary>
        /// Background fill
        /// </summary>
        public Brush Fill
        {
            get;
            set;
        }

        public bool IsHover
        {
            get;
            set;
        }

        /// <summary>
        /// Hover fill
        /// </summary>
        public Brush HoverFill
        {
            get;
            set;
        }

        #endregion // properties


        #region methods
        #endregion // methods


        #region internal methods

        protected Brush GetFill()
        {
            return IsHover && (HoverFill != null) ? HoverFill : Fill;
        }

        internal void MouseDown(Point p)
        {
            DoMouseDown(p);
        }

        internal void MouseMove(Point p, bool pushed)
        {
            DoMouseMove(p, pushed);
        }

        internal void MouseUp(Point p)
        {
            DoMouseUp(p);
        }

        internal void MouseEnter()
        {
            IsHover = true;
            DoMouseEnter();
        }

        internal void MouseLeave()
        {
            IsHover = false;
            DoMouseLeave();
        }

        protected virtual void DoMouseDown(Point p)
        {
        }

        protected virtual void DoMouseMove(Point p, bool pushed)
        {
        }

        protected virtual void DoMouseUp(Point p)
        {
        }

        protected virtual void DoMouseEnter()
        {
        }

        protected virtual void DoMouseLeave()
        {
        }

        #endregion // internal methods
    }
}
