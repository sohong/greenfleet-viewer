////////////////////////////////////////////////////////////////////////////////
// AxisHelper.cs
// 2012.04.17, created by sohong
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

namespace Viewer.Common.UI.Acceleration {

    /// <summary>
    /// Y축에 표시할 값들을 계산한다.
    /// </summary>
    public class AxisHelper {

        /*
        public static IEnumerable<double> GetValues(double minValue, double maxValue, int maxCount) {
            double interval = CalcInterval(minValue, maxValue, maxCount);
            Debug.WriteLine(interval);
            yield break;
        }
         */

        public static IEnumerable<double> GetValues(double minValue, double maxValue, int maxCount) {
            IList<double> values = new List<double>();
            double interval = CalcInterval(minValue, maxValue, maxCount);
            double start = Math.Floor(minValue / interval) * interval;

            double val = start;
            for (int i = 1; val < maxValue; i++) {
                values.Add(val);
                val = start + (i * interval);
            }
            values.Add(val);

            return values;
        }

        private static double CalcInterval(double minValue, double maxValue, int maxCount) {
            maxCount = Math.Max(1, maxCount);
            double range = maxValue - minValue;
            double bestInterval = range / maxCount;
            double minInterval = Math.Pow(10, Math.Floor(Math.Log10(bestInterval)));

            foreach (int m in new int[] { 10, 5, 2, 1 }) {
                double interval = minInterval * m;
                if (maxCount < range / interval)
                    break;
                bestInterval = interval;
            }

            return bestInterval;
        }
    }
}
