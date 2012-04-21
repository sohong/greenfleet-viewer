////////////////////////////////////////////////////////////////////////////////
// SeriesElement.cs
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
using System.Windows;
using System.Windows.Media;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// TimelineBar series.
    /// </summary>
    public class SeriesElement : TimelineElement
    {
        #region fields

        private IList<TimeRangeElement> m_ranges;
        
        #endregion // fields


        #region constructor

        public SeriesElement(TimelineBar bar)
            : base(bar)
        {
            m_ranges = new List<TimeRangeElement>();

            RangeHeight = 7;
        }

        #endregion // constructor


        #region properties

        public AxisLabelProvider AxisLabels
        {
            get;
            set;
        }

        public TimelineValueCollection Values
        {
            get;
            set;
        }

        public double RangeHeight
        {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        public override Size Measure(double hintWidth, double hintHeight)
        {
            return new Size();
        }

        protected override void DoDraw(DrawingContext dc)
        {
            LayoutRangeElements();
        }

        #endregion // overriden methods


        #region internal methods

        private void LayoutRangeElements()
        {
            Children.Clear();
            if (Values == null || Values.Count < 1)
                return;

            while (m_ranges.Count < Values.Count) {
                m_ranges.Add(new TimeRangeElement(Bar));
            }

            for (int i = 0; i < Values.Count; i++) {
                TimelineValue value = Values[i];
                TimeRangeElement range = m_ranges[i];
                Children.Add(range);

                range.Y = (this.Height - RangeHeight) / 2;
                range.X = AxisLabels.GetPosition(value.Start) * this.Width;
                range.Height = RangeHeight;
                range.Width = (AxisLabels.GetPosition(value.Finish.AddMinutes(1)) - AxisLabels.GetPosition(value.Start)) * this.Width;
                range.Fill = value.Type == TimelineValue.TimelineValueType.Event ? Bar.EventBackground : Bar.AllBackground;
                range.Draw();
            }
        }

        #endregion // internal methods
    }
}
