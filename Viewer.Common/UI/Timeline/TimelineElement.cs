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

namespace Viewer.Common.UI.Timeline {
    
    /// <summary>
    /// Visual element base for TimelineBar
    /// </summary>
    public class TimelineElement : DrawingVisual {

        #region fields

        private FrameworkElement m_container;
        
        #endregion // fields


        #region constructor

        public TimelineElement(FrameworkElement container) {
            m_container = container;
            Draw();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Width
        /// </summary>
        public double Width {
            get { return m_width; }
            set {
                value = Math.Max(1, value);
                if (value != m_width) {
                    m_width = value;
                    Invalidate();
                }
            }
        }
        private double m_width = 0;

        /// <summary>
        /// Height
        /// </summary>
        public double Height {
            get { return m_height; }
            set {
                value = Math.Max(1, value);
                if (value != m_height) {
                    m_height = value;
                    Invalidate();
                }
            }
        }
        private double m_height = 0;

        /// <summary>
        /// Background fill
        /// </summary>
        public Brush Fill {
            get { return m_fill; }
            set {
                if (value != m_fill) {
                    m_fill = value;
                    Invalidate();
                }
            }
        }
        private Brush m_fill;

        public bool IsHover {
            get { return m_isHover; }
            set {
                if (value != m_isHover) {
                    m_isHover = value;
                    Invalidate();
                }
            }
        }
        private bool m_isHover;

        /// <summary>
        /// Hover fill
        /// </summary>
        public Brush HoverFill {
            get { return m_hoverFill; }
            set {
                if (value != m_hoverFill) {
                    m_hoverFill = value;
                    Invalidate();
                }
            }
        }
        private Brush m_hoverFill;

        /// <summary>
        /// User data
        /// </summary>
        public object Data {
            get;
            set;
        }
        
        #endregion // properties


        #region methods

        public void Invalidate() {
            m_container.InvalidateArrange();
        }

        public void Draw() {
            DrawingContext dc = RenderOpen();
            DoDraw(dc);
            dc.Close();
        }

        #endregion // methods


        #region internal methods

        protected Brush GetFill() {
            return IsHover && (HoverFill != null) ? HoverFill : Fill;
        }

        protected virtual void DoDraw(DrawingContext dc) {
            dc.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Black, 1), new Rect(0, 0, Width, Height));
        }

        internal void MouseDown(Point p) {
            DoMouseDown(p);
        }

        internal void MouseMove(Point p, bool pushed) {
            DoMouseMove(p, pushed);
        }

        internal void MouseUp(Point p) {
            DoMouseUp(p);
        }

        internal void MouseEnter() {
            IsHover = true;
            DoMouseEnter();
        }

        internal void MouseLeave() {
            IsHover = false;
            DoMouseLeave();
        }

        protected virtual void DoMouseDown(Point p) {
        }

        protected virtual void DoMouseMove(Point p, bool pushed) {
        }

        protected virtual void DoMouseUp(Point p) {
        }

        protected virtual void DoMouseEnter() {
        }

        protected virtual void DoMouseLeave() {
        }

        #endregion // internal methods
    }
}
