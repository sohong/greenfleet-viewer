////////////////////////////////////////////////////////////////////////////////
// ObjectUtil.cs
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
using System.Reflection;
using Viewer.Common.Xml;

namespace Viewer.Common.Util
{
    public class ObjectUtil
    {
        public static readonly Type[] EMPTY_TYPES = new Type[0];
        public static readonly object[] EMPTY_OBJECTS = new object[0];


        public static string ToString(Object obj)
        {
            if (obj == null) {
                return "null";
            }

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] props = obj.GetType().GetProperties();

            sb.AppendLine("[" + obj.GetType().Name + "]");
            foreach (PropertyInfo prop in props) {
                if (prop.GetIndexParameters().Length == 0) {
                    sb.Append(prop.Name);
                    sb.Append(" = ");
                    sb.Append(prop.GetValue(obj, null));
                    sb.AppendLine();
                }
            }

            if (obj is IEnumerable<object>) {
                foreach (object item in (IEnumerable<object>)obj) {
                    sb.AppendLine();
                    sb.Append(ToString(item));
                }
            }

            return sb.ToString();
        }

        public static void Assign(object source, object target)
        {
            if (source == target)
                return;
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            Type t = source.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                object[] attrs = p.GetCustomAttributes(false);
                if (p.CanWrite &&
                    !HasAttr(p.GetCustomAttributes(false), typeof(TransientAttribute))) {
                    object val = p.GetValue(source, null);
                    p.SetValue(target, val, null);
                }
            }
        }

        public static void Write(object source, object target)
        {
            Type t = source.GetType();
            PropertyInfo[] props = t.GetProperties();

            Type t2 = target.GetType();
            PropertyInfo[] props2 = t2.GetProperties();

            foreach (PropertyInfo p in props) {
                foreach (PropertyInfo p2 in props2) {
                    if (p2.Name.Equals(p.Name) && p.CanRead && p2.CanWrite) {
                        object[] attrs = p.GetCustomAttributes(false);
                        if (!HasAttr(p2.GetCustomAttributes(false), typeof(TransientAttribute))) {
                            object val = p.GetValue(source, null);
                            p2.SetValue(target, val, null);
                        }

                        break;
                    }
                }
            }
        }

        public static bool HasAttr(object[] attrs, Type attr)
        {
            foreach (object a in attrs) {
                if (a.GetType() == attr) {
                    return true;
                }
            }
            return false;
        }
    }
}
