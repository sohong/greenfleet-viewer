////////////////////////////////////////////////////////////////////////////////
// TransientAttribute.cs
// 2012.03.08, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer.Common.Xml {

    /// <summary>
    /// 저장하지 않는 필드.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TransientAttribute : Attribute {

        public TransientAttribute() {
        }
    }
}
