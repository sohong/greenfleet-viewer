////////////////////////////////////////////////////////////////////////////////
// AxisLabelProvider.cs
// 2012.04.18, created by sohong
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

namespace Viewer.Common.UI.Acceleration
{
    /// <summary>
    /// AccelerationChart X 축에 표시할 label들의 위치와 text를 리턴한다.
    /// </summary>
    public class AxisLabelProvider
    {
        #region fields
        #endregion // fields


        #region constructor

        public AxisLabelProvider()
        {
            Count = 60;
        }

        #endregion // constructor


        #region properties

        public DateTime StartTime
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        #endregion // properties


        #region methods

        public double GetPosition(int index)
        {
            double p = (double)index / Count;
            return p;
        }

        public string GetLabel(int index, string format = "mm:ss")
        {
            string text = StartTime.AddSeconds(index).ToString(format);
            return text;
        }

        #endregion // methods

    }
}
