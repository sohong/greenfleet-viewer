////////////////////////////////////////////////////////////////////////////////
// AccelerationChart.cs
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
using System.Windows;

namespace Viewer.Common.UI {

    /// <summary>
    /// x/y/x 가속도 챠트.
    /// </summary>
    public class AccelerationChart : FrameworkElement {

        public struct Value {
            public double X;
            public double Y;
            public double Z;

            public Value(double x, double y, double z) {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }

        #region fields

        private double m_minimum = -5;
        private double m_maximum = 5;
        private IList<Value> m_values;

        #endregion // fields


        #region constructor

        public AccelerationChart() {
            m_values = new List<Value>();
        }

        #endregion // constructor


        #region properties

        #endregion // properties


        #region methods

        public void Clear() {
        }

        public void AddValue(double x, double y, double z) {
            m_values.Add(new Value(x, y, z));
            Recalculate();
            RefreshChart();
        }

        #endregion // methods


        #region internal methods

        private void Recalculate() {
        }

        private void RefreshChart() {
        }

        #endregion // internal methods
    }
}
