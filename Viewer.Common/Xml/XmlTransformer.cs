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
using System.Collections;
using Viewer.Common;
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

        public void Serialize(IEnumerable list, XContainer target, string elementName) {
            foreach (object model in list) {
                XElement elt = new XElement(elementName);
                Serialize(model, elt);
                target.Add(elt);
            }
        }

        public void Serialize(object model, XContainer target) {
            Type t = model.GetType();
            PropertyInfo[] props = t.GetProperties();
            
            foreach (PropertyInfo p in props) {
                if (!p.IsTransient()) {
                    XElement elt = new XElement(p.Name);
                    target.Add(elt);
                    object value = p.GetValue(model, null);

                    if (value == null) {
                        elt.Value = "{null}";
                    } else if (p.IsObject()) {
                        Serialize(value, elt);
                    } else {
                        elt.Value = value.ToString();
                    }
                }
            }
        }

        public IList Deserialize(XContainer source, string elementName, Type modelType, IList target) {
            if (target == null) {
                target = new List<object>();
            }

            IEnumerable<XElement> elts = source.Elements(elementName);
            foreach (XElement elt in elts) {
                object model = Deserialize(elt, modelType);
                target.Add(model);
            }

            return target;
        }

        public object Deserialize(XContainer source, object modelOrType) {
            if (modelOrType == null) {
                throw new ArgumentNullException("modelOrType");
            }

            Type t = modelOrType is Type ? (Type)modelOrType : modelOrType.GetType();
            PropertyInfo[] props = t.GetProperties();
            object model = modelOrType; 

            if (modelOrType is Type) {
                ConstructorInfo ctor = t.GetConstructor(ObjectUtil.EMPTY_TYPES);
                model = ctor.Invoke(ObjectUtil.EMPTY_OBJECTS);
            }

            foreach (PropertyInfo p in props) {
                if (!p.IsTransient()) {
                    XElement elt = source.Element(p.Name);
                    if (elt != null) {
                        Type pt = p.PropertyType;
                        string s = elt.Value;
                        object val = null;

                        if ("{null}".Equals(s, StringComparison.InvariantCultureIgnoreCase)) {
                            val = null;
                        } else if (p.IsObject()) {
                            val = p.GetValue(model, null);
                            val = Deserialize(elt, (val != null) ? val : pt);
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
                            p.SetValue(model, val, ObjectUtil.EMPTY_OBJECTS);
                        }
                    }
                }
            }

            return model;
        }

        #endregion // methods


        #region internal methods


        #endregion // internal methods
    }
}
