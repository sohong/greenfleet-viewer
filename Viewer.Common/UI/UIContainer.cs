////////////////////////////////////////////////////////////////////////////////
// UIContainer.cs
// 2012.04.22, created by sohong
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

namespace Viewer.Common.UI
{
    /// <summary>
    /// UI component base.
    /// </summary>
    public class UIContainer : FrameworkElement
    {
        #region fields

        private VisualCollection m_elements;

        #endregion // fields


        #region constructors

        public UIContainer()
        {
            m_elements = new VisualCollection(this);
            CreateElements();

            SnapsToDevicePixels = true;
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);

            SizeChanged += new SizeChangedEventHandler((sender, e) => {
                VisualClip = new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight));
            });
        }

        #endregion // constructors


        #region methods 

        public int AddElement(Visual element)
        {
            if (element != null && !m_elements.Contains(element)) {
                return m_elements.Add(element);
            }
            return -1;
        }

        #endregion // methods


        #region overriden properties

        protected override int VisualChildrenCount
        {
            get { return m_elements.Count; }
        }

        #endregion // overriden properties


        #region overriden methods

        protected override Visual GetVisualChild(int index)
        {
            return m_elements[index];
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size sz = base.ArrangeOverride(finalSize);
            LayoutElements(sz.Width, sz.Height);
            return sz;
        }

        #endregion // overriden methods


        #region internal methods

        protected virtual void CreateElements()
        {
        }

        protected virtual void LayoutElements(double width, double height)
        {
        }

        #endregion // internal methods
    }
}
