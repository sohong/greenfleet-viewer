////////////////////////////////////////////////////////////////////////////////
// HelloBox.cs
// 2012.03.29, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Common.UI
{
    public class HelloBox : FrameworkElement
    {
        private DrawingVisual childA;
        private DrawingVisual childB;

        public HelloBox()
        {
            SnapsToDevicePixels = true;

            childA = new DrawingVisual();
            DrawChild(childA);
            AddVisualChild(childA);

            childB = new DrawingVisual();
            AddVisualChild(childB);
            DrawChild(childB);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 2;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? childA : childB;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutChildren(sz.Width, sz.Height);
            return sz;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawRectangle(new SolidColorBrush(Colors.Yellow), new Pen(new SolidColorBrush(Colors.Black), 0.5), new Rect(0, 0, ActualWidth, ActualHeight));
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            //InvalidateArrange();
            //LayoutChildren();
        }

        private void DrawChild(DrawingVisual child)
        {
            DrawingContext dc = child.RenderOpen();
            dc.DrawRectangle(new SolidColorBrush(Colors.Red), new Pen(Brushes.Black, 1), new Rect(0, 0, 20, 20));
            dc.Close();
        }

        private void LayoutChildren(double width, double height)
        {
            childA.Offset = new Vector(width / 3, (height - 20) / 2);
            childB.Offset = new Vector(width * 2 / 3, (height - 20) / 2);
        }
    }
}
