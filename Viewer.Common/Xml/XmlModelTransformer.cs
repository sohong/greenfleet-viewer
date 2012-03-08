////////////////////////////////////////////////////////////////////////////////
// XmlModelTransformer.cs
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
using System.Xml;
using System.Reflection;
using Viewer.Common.Util;
using System.Collections.Specialized;

namespace Viewer.Common.Xml {

    public class XmlModelTransformer {
        private static object[] EmptyObjects = new object[0];

        public static T XmlToModel<T>(XmlElement xml, string path, T model, object[] args, IXmlValueConverter converter) {
            if (xml.Name == "null") {
                return default(T);
            }

            Type[] argTypes = (args != null && args.Length > 0) ? new Type[args.Length] : Type.EmptyTypes;
            for (int i = 0; i < argTypes.Length; i++) {
                argTypes[i] = args[i].GetType();
            }

            Type t = typeof(T);

            if (model == null) {
                ConstructorInfo ctor = t.GetConstructor(argTypes);
                model = (T)ctor.Invoke(args != null ? args : EmptyObjects);
            }

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                object[] attrs = p.GetCustomAttributes(false);
                if (!ObjectUtil.HasAttr(p.GetCustomAttributes(false), typeof(TransientAttribute))) {
                    string name = p.Name.Uncapitalize();
                    // composite key인 경우 key element 아래에 값이 들어 있다.
                    XmlNode node = xml.SelectSingleNode(path + name + "|" + path + "key/" + name);
                    if (node != null) {
                        string value = node.InnerText;
                        object val;
                        if (converter != null) {
                            val = converter.StringToValue(value, p.PropertyType);
                        } else if (p.PropertyType.IsEnum) {
                            //val = Convert.ChangeType(int.Parse(node.InnerText), p.PropertyType);
                            val = Enum.ToObject(p.PropertyType, int.Parse(node.InnerText));
                        } else {
                            val = Convert.ChangeType(node.InnerText, p.PropertyType);
                        }
                        p.SetValue(model, val, null);
                    }
                }
            }

            return model;
        }

        public static T XmlToModel<T>(XmlElement xml, T model, object[] args, IXmlValueConverter converter) {
            return XmlToModel<T>(xml, String.Empty, model, args, converter);
        }

        public static T XmlToModel<T>(XmlElement xml, object[] args, IXmlValueConverter converter) {
            return XmlToModel<T>(xml, String.Empty, default(T), args, converter);
        }

        public static IList<T> XmlToModelList<T>(XmlElement xml, string path, object[] args, IXmlValueConverter converter) {
            Type[] argTypes = (args != null && args.Length > 0) ? new Type[args.Length] : Type.EmptyTypes;
            for (int i = 0; i < argTypes.Length; i++) {
                argTypes[i] = args[i].GetType();
            }

            Type t = typeof(T);
            ConstructorInfo ctor = t.GetConstructor(argTypes);

            if (ctor == null) {
                string msg = "";
                foreach (Type t2 in argTypes) {
                    if (msg.Length > 0) msg += ", ";
                    msg += t2.Name;
                }
                msg = t.Name + "타입에 (" + msg + ") (들)을 매개변수로 갖는 생성자가 존재하지 않습니다.";
                throw new ArgumentException(msg, "argTypes");
            }

            PropertyInfo[] props = t.GetProperties();
            IList<T> list = new List<T>();

            foreach (XmlElement elt in xml.SelectNodes(path)) {
                T m = (T)ctor.Invoke(args != null ? args : EmptyObjects);
                list.Add(m);

                foreach (PropertyInfo p in props) {
                    object[] attrs = p.GetCustomAttributes(false);
                    if (!ObjectUtil.HasAttr(p.GetCustomAttributes(false), typeof(TransientAttribute))) {
                        string name = p.Name.Uncapitalize();
                        // composite key인 경우 key element 아래에 값이 들어 있다.
                        XmlNode node = elt.SelectSingleNode(name + "|" + "key/" + name);
                        if (node != null) {
                            object val;
                            if (converter != null) {
                                val = converter.StringToValue(node.InnerText, p.PropertyType);
                            } else if (p.PropertyType.IsEnum) {
                                //val = Convert.ChangeType(int.Parse(node.InnerText), p.PropertyType);
                                val = Enum.ToObject(p.PropertyType, int.Parse(node.InnerText));
                            } else {
                                val = Convert.ChangeType(node.InnerText, p.PropertyType);
                            }
                            p.SetValue(m, val, null);
                        }
                    }
                }
            }

            return list;
        }

        public static IList<T> XmlToModelList<T>(XmlElement xml, object[] args, IXmlValueConverter converter) {
            return XmlToModelList<T>(xml, String.Empty, args, converter);
        }

        public static IDictionary<string, object> ModelToMap(object model) {
            IDictionary<string, object> map = new Dictionary<string, object>();

            Type t = model.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                object[] attrs = p.GetCustomAttributes(false);
                if (!ObjectUtil.HasAttr(p.GetCustomAttributes(false), typeof(TransientAttribute))) {
                    object val;
                    if (p.PropertyType.IsEnum) {
                        val = (int)p.GetValue(model, null);
                    } else {
                        val = p.GetValue(model, null);
                    }
                    map[p.Name.Uncapitalize()] = val;
                }
            }

            return map;
        }

        public static NameValueCollection ModelToPairs(object model, IXmlValueConverter converter) {
            NameValueCollection cols = new NameValueCollection();

            Type t = model.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                if (!ObjectUtil.HasAttr(p.GetCustomAttributes(false), typeof(TransientAttribute))) {
                    object val;
                    if (p.PropertyType.IsEnum) {
                        val = (int)p.GetValue(model, null);
                    } else {
                        val = p.GetValue(model, null);
                    }

                    string v;
                    if (converter != null)
                        v = converter.ValueToString(val, p.PropertyType);
                    else
                        v = val != null ? val.ToString() : "";
                    cols.Add(p.Name.Uncapitalize(), v);
                }
            }

            return cols;
        }

        public static XmlDocument ModelToXml(object model) {
            return null;
        }
    }
}
