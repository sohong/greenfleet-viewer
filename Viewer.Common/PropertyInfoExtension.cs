////////////////////////////////////////////////////////////////////////////////
// PropertyInfoExtension.cs
// 2012.03.26, created by sohong
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
using System.Reflection;
using Viewer.Common.Util;

namespace Viewer.Common {

    public static class PropertyInfoExtension {
    
        public static bool IsTransient(this PropertyInfo prop) {
            return ObjectUtil.HasAttr(prop.GetCustomAttributes(false), typeof(TransientAttribute));
        }

        public static bool IsObject(this PropertyInfo prop) {
            Type t = prop.PropertyType;
            return t.IsClass && t != typeof(string);
        }
    }
}
