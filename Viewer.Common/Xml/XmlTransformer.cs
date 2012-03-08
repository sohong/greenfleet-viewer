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

        #region methods

        public void Serialize<T>(T model, XElement target) {
            Type t = typeof(T);
            PropertyInfo[] props = t.GetProperties();
            
            foreach (PropertyInfo p in props) {
                if (!IsTransient(p)) {
                    XElement elt = new XElement(p.Name);
                    object value = p.GetValue(model, null);

                    if (value != null) {
                        Type pt = p.PropertyType;
                        if (pt.IsClass && pt != typeof(string)) {
                            Serialize(value, elt);
                        } else {
                            elt.Value = value.ToString();
                        }
                    } else {
                        XAttribute attr = new XAttribute("isNull", "true");
                        elt.Add(attr);
                    }

                    target.Add(elt);
                }
            }
        }

        public void Deserialze<T>(XElement source, T model) {
            Type t = typeof(T);
            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo p in props) {
                if (!IsTransient(p)) {
                    Type pt = p.PropertyType;

                    if (pt.IsClass && pt != typeof(string)) {
                        if (p.CanWrite) {
                            XAttribute attr = source.Attribute("isNull");
                            if (attr != null) {
                            }

                        } else if (p.CanRead) {
                        }

                    } else {

                    }
                }
            }
        }

        #endregion // methods


        #region internal methods

        private bool IsTransient(PropertyInfo prop) {
            return ObjectUtil.HasAttr(prop.GetCustomAttributes(false), typeof(TransientAttribute));
        }

        #endregion // internal methods
    }
}
