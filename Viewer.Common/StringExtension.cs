////////////////////////////////////////////////////////////////////////////////
// StringExtension.cs
// 2012.03.08, created by sohong
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

namespace Viewer.Common {

    /// <summary>
    /// string type extension
    /// </summary>
    public static class StringExtension {

        public static bool IsEmpty(this string str) {
            return str == null || str.Length < 1;
        }

        public static bool IsNotEmpty(this string str) {
            return str != null || str.Length > 0;
        }

        public static string Capitalize(this string str) {
            if (IsNotEmpty(str)) {
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            }
            return str;
        }

        public static string Uncapitalize(this string str) {
            if (IsNotEmpty(str)) {
                return str.Substring(0, 1).ToLower() + str.Substring(1);
            }
            return str;
        }

        public static string[] Split2(this string str, char sep) {
            string[] arr = str.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries);
            return arr;
        }
    }
}
