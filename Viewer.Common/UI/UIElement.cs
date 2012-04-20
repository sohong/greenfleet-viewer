////////////////////////////////////////////////////////////////////////////////
// UIElement.cs
// 2012.04.19, created by sohong
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

namespace Viewer.Common.UI
{
    /// <summary>
    /// Ingridents for UI Component.
    /// </summary>
    public abstract class UIElement : DrawingVisual
    {
        #region static members

        public static Color ToColor(uint argb)
        {
            Color color = Color.FromArgb((byte)((argb & 0xff000000) >> 24),
                (byte)((argb & 0xff0000) >> 16), (byte)((argb & 0xff00) >> 8), (byte)(argb & 0xff));
            return color;
        }

        #endregion // static members


        #region fields

        private FrameworkElement m_container;

        #endregion // fields


        #region constructor

        public UIElement(FrameworkElement container)
        {
            m_container = container;
            CreateChildren();
            Draw();
        }

        #endregion // constructor


        #region abstract members

        protected abstract void DoDraw(DrawingContext dc);
        public abstract Size Measure(double hintWidth, double hintHeight);

        #endregion // abstract members


        #region properties

        public FrameworkElement Container
        {
            get { return m_container; }
        }

        public double X
        {
            get { return Offset.X; }
            set { Move(value, Y); }
        }

        public double Y
        {
            get { return Offset.Y; }
            set { Move(X, value); }
        }

        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get { return m_width; }
            set
            {
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
        public double Height
        {
            get { return m_height; }
            set
            {
                value = Math.Max(1, value);
                if (value != m_height) {
                    m_height = value;
                    Invalidate();
                }
            }
        }
        private double m_height = 0;

        /// <summary>
        /// User data
        /// </summary>
        public object Data
        {
            get;
            set;
        }

        #endregion // properties


        #region methods

        public void Move(double x, double y)
        {
            Offset = new Vector(x, y);
        }

        public void Invalidate()
        {
            Container.InvalidateArrange();
        }

        public virtual void Draw()
        {
            DrawingContext dc = RenderOpen();
            DoDraw(dc);
            dc.Close();
        }

        #endregion // methods


        #region internal methods

        protected virtual void CreateChildren()
        {
        }

        #endregion // internal methods
    }
}
