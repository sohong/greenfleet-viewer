////////////////////////////////////////////////////////////////////////////////
// LegendItemElement.cs
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
using System.Globalization;

namespace Viewer.Common.UI.Acceleration
{
    public class LegendItemElement : DrawingVisual
    {
        #region constructor

        public LegendItemElement()
        {
            BoxSize = new Size(10, 10);
        }

        #endregion // constructor


        #region properties

        public Size BoxSize
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public Pen Border
        {
            get;
            set;
        }

        public Brush Background
        {
            get;
            set;
        }

        public Brush Foreground
        {
            get;
            set;
        }

        #endregion // properties


        #region methods

        public void Draw()
        {
            DrawingContext dc = RenderOpen();
            DoDraw(dc);
            dc.Close();
        }

        #endregion // methods


        #region internal methods

        protected void DoDraw(DrawingContext dc)
        {
            if (Background != null) {
                dc.DrawRectangle(Background, Border, new Rect(0, 0, BoxSize.Width, BoxSize.Height));
            }
            Typeface face = new Typeface("Tahoma");
            FormattedText ft = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, 12, Brushes.Black);
            dc.DrawText(ft, new Point(BoxSize.Width + 4, (BoxSize.Height - ft.Height) / 2));
        }

        #endregion // internal methods
    }
}
