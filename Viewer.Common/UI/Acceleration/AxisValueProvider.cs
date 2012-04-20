////////////////////////////////////////////////////////////////////////////////
// AxisValueProvider.cs
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
using System.Collections;

namespace Viewer.Common.UI.Acceleration
{
    /// <summary>
    /// AccelerationChart Y 축에 표시할 값들을 제공한다.
    /// </summary>
    public class AxisValueProvider : IEnumerable<double>
    {
        #region fields

        private IList<double> m_values;
        private double m_minValue;
        private double m_maxValue;

        #endregion // fields


        #region constructor

        public AxisValueProvider()
        {
            m_values = new List<double>();
        }

        #endregion // constructor


        #region IEnumerable<double>

        public IEnumerator<double> GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        #endregion // IEnumerable<double>


        #region methods

        public void ResetValues(IEnumerable<double> values)
        {
            double minVal = 0;
            double maxVal = 0;
            m_values.Clear();

            foreach (double v in values) {
                m_values.Add(v);
                minVal = Math.Min(v, minVal);
                maxVal = Math.Max(v, maxVal);
            }

            m_minValue = minVal;
            m_maxValue = maxVal;
        }

        #endregion // methods


        #region properties

        public double MinValue
        {
            get { return m_minValue; }
        }

        public double MaxValue
        {
            get { return m_maxValue; }
        }

        #endregion // properties
    }
}
