////////////////////////////////////////////////////////////////////////////////
// RangeMarkerVisual.cs
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
using System.Windows.Shapes;

namespace Viewer.Common.UI.Timeline {
    
    public class TimeRangeMarkerVisual : TimelineElement {

        #region constructor

        public TimeRangeMarkerVisual(FrameworkElement container) 
            : base(container) {
        }

        #endregion // constructor

        
        #region properties

        /// <summary>
        /// Border
        /// </summary>
        public Pen Border {
            get { return m_border; }
            set {
                if (value != m_border) {
                    m_border = value;
                    Invalidate();
                }
            }
        }
        private Pen m_border;

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            PathGeometry geom = new PathGeometry();
            PathFigure figure = new PathFigure();
            geom.Figures.Add(figure);

            figure.StartPoint = new Point(0, 0);
            figure.Segments.Add(new LineSegment() {
                Point = new Point(Width, Height / 2)
            });
            figure.Segments.Add(new LineSegment() {
                Point = new Point(0, Height)
            });

            dc.DrawGeometry(GetFill(), Border, geom);
        }

        #endregion // overriden methods
    }
}
