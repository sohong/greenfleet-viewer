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
        }

        #endregion // constructor


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


        #region properties

        /// <summary>
        /// Width
        /// </summary>
        public double Width {
            get { return m_width; }
            set {
                if (value != m_width) {
                    m_width = value;
                    Invalidate();
                }
            }
        }
        private double m_width;

        /// <summary>
        /// Height
        /// </summary>
        public double Height {
            get { return m_height; }
            set {
                if (value != m_height) {
                    m_height = value;
                    Invalidate();
                }
            }
        }
        private double m_height;

        
        #endregion // properties


        #region internal methods

        protected virtual void DoDraw(DrawingContext dc) {
            dc.DrawRectangle(Brushes.Yellow, new Pen(Brushes.Black, 1), new Rect(0, 0, Width, Height));
        }

        #endregion // internal methods
    }
}
