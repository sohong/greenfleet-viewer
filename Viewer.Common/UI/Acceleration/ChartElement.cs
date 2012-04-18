////////////////////////////////////////////////////////////////////////////////
// ChartElement.cs
// 2012.04.16, created by sohong
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

namespace Viewer.Common.UI.Acceleration {
    
    /// <summary>
    /// AccelerationChart 구성 요소.
    /// </summary>
    public abstract class ChartElement : DrawingVisual {

        #region static members

        public static Color ToColor(uint argb) {
            Color color = Color.FromArgb((byte)((argb & 0xff000000) >> 24), 
                (byte)((argb & 0xff0000) >> 16), (byte)((argb & 0xff00) >> 8), (byte)(argb & 0xff));
            return color;

        }

        #endregion // static members


        #region fields

        private AccelerationChart m_chart;
        
        #endregion // fields


        #region constructor

        public ChartElement(AccelerationChart chart) {
            m_chart = chart;
            CreateChildren();
            Draw();
        }

        #endregion // constructor


        #region abstract members

        protected abstract void DoDraw(DrawingContext dc);
        public abstract Size Measure(double hintWidth, double hintHeight);

        #endregion // abstract members


        #region properties

        public AccelerationChart Chart {
            get { return m_chart; }
        }

        public double X {
            get { return Offset.X; }
            set { Move(value, Y); }
        }

        public double Y {
            get { return Offset.Y; }
            set { Move(X, value); }
        }

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

        #endregion // properties


        #region methods

        public void Invalidate() {
            m_chart.InvalidateArrange();
        }

        public virtual void Draw() {
            DrawingContext dc = RenderOpen();
            DoDraw(dc);
            dc.Close();
        }

        public void Move(double x, double y) {
            Offset = new Vector(x, y);
        }

        #endregion // methods


        #region internal methods

        protected virtual void CreateChildren() {
        }

        #endregion // internal methods
    }
}
