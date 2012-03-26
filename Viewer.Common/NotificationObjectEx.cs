////////////////////////////////////////////////////////////////////////////////
// NotificationObjectEx.cs
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
using Microsoft.Practices.Prism.ViewModel;
using System.Reflection;
using Viewer.Common.Util;
using Viewer.Common.Xml;

namespace Viewer.Common {

    /// <summary>
    /// Prism NotificationObject + Clone
    /// </summary>
    public class NotificationObjectEx : NotificationObject {

        #region methods

        public virtual void AssignTo(NotificationObjectEx target) {
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                if (!p.IsTransient()) {
                    p.SetValue(target, p.GetValue(this, null), ObjectUtil.EMPTY_OBJECTS);
                }
            }
        }

        public NotificationObjectEx Clone() {
            Type t = this.GetType();
            ConstructorInfo ctor = t.GetConstructor(ObjectUtil.EMPTY_TYPES);
            NotificationObjectEx model = ctor.Invoke(ObjectUtil.EMPTY_OBJECTS) as NotificationObjectEx;
            if (model != null) {
                this.AssignTo(model);
            }
            return model;
        }

        public bool EqualsTo(NotificationObjectEx other) {
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props) {
                if (!p.IsTransient()) {
                    if (!p.GetValue(this, null).Equals(p.GetValue(other, null))) {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion // methods
    }
}
