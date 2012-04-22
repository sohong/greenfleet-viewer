////////////////////////////////////////////////////////////////////////////////
// SimpleSpeedometer.cs
// 2012.04.22, created by sohong
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
    /// <summary>
    /// 속도계
    /// </summary>
    public class SimpleSpeedometer : UIContainer
    {
        #region fields

        private DrawingVisual m_frame;
        private DrawingVisual m_hand;
        private DrawingVisual m_pin;

        #endregion // fields


        #region constructors

        public SimpleSpeedometer()
        {
        }

        #endregion // constructors


        #region properties
        #endregion // properties


        #region overriden properties
        #endregion // overriden properties


        #region overriden methods

        protected override void CreateElements()
        {
            AddElement(m_frame = new DrawingVisual());
            AddElement(m_hand = new DrawingVisual());
            AddElement(m_pin = new DrawingVisual());
        }

        #endregion // overriden methods
    }
}
