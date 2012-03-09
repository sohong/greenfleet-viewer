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
using System.Xml.Linq;
using System.Reflection;
using Viewer.Common.Util;

namespace Viewer.Common.Xml {
    
    /// <summary>
    /// object <--> xml 변환.
    /// 각 property를 <propName>propValue</propName> 형식으로 저장한다.
    /// value가 object이면 <value><propName>propValue</propName>..<propName>propValue</propName></value>
    /// 형식으로 풀어 저장한다.
    /// object 형식의 value인 속성이 readOnly이면 읽어들일 때 그 타입의 개체를 생성하지 않고 속성들만 설정한다.
    /// </summary>
    public class XmlTransformer {

        #region static members

        private static readonly Type[] EMPTY_TYPES = new Type[0];
        private static readonly object[] EMPTY_OBJECTS = new object[0];

        #endregion // static members


        #region methods

        public void Serialize(object model, XContainer target) {
            Type t = model.GetType();
            PropertyInfo[] props = t.GetProperties();
            
            foreach (PropertyInfo p in props) {
                if (!IsTransient(p)) {
                    XElement elt = new XElement(p.Name);
                    target.Add(elt);
                    object value = p.GetValue(model, null);

                    if (value == null) {
                        elt.Value = "{null}";
                    } else if (IsObject(p)) {
                        Serialize(value, elt);
                    } else {
                        elt.Value = value.ToString();
                    }
                }
            }
        }

        public object Deserialze(XContainer source, object modelOrType) {
            if (modelOrType == null) {
                throw new ArgumentNullException("modelOrType");
            }

            Type t = modelOrType is Type ? (Type)modelOrType : modelOrType.GetType();
            PropertyInfo[] props = t.GetProperties();
            object model = modelOrType; 

            if (modelOrType is Type) {
                ConstructorInfo ctor = t.GetConstructor(EMPTY_TYPES);
                model = ctor.Invoke(EMPTY_OBJECTS);
            }

            foreach (PropertyInfo p in props) {
                if (!IsTransient(p)) {
                    XElement elt = source.Element(p.Name);
                    if (elt != null) {
                        Type pt = p.PropertyType;
                        string s = elt.Value;
                        object val = null;

                        if ("{null}".Equals(s, StringComparison.InvariantCultureIgnoreCase)) {
                            val = null;
                        } else if (IsObject(p)) {
                            val = p.GetValue(model, null);
                            val = Deserialze(elt, (val != null) ? val : pt);
                        } else if (pt.IsEnum) {
                            try {
                                val = Enum.Parse(pt, s, true);
                            } catch (Exception) {
                                val = Enum.ToObject(pt, 0);
                            }
                        } else {
                            val = Convert.ChangeType(s, pt);
                        }

                        if (p.CanWrite) {
                            p.SetValue(model, val, EMPTY_OBJECTS);
                        }
                    }
                }
            }

            return model;
        }

        #endregion // methods


        #region internal methods

        private bool IsTransient(PropertyInfo prop) {
            return ObjectUtil.HasAttr(prop.GetCustomAttributes(false), typeof(TransientAttribute));
        }

        private bool IsObject(PropertyInfo prop) {
            Type t = prop.PropertyType;
            return t.IsClass && t != typeof(string);
        }

        #endregion // internal methods
    }
}
