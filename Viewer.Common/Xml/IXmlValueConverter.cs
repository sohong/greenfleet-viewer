////////////////////////////////////////////////////////////////////////////////
// IXmlValueConverter.cs
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

    public interface IXmlValueConverter {

        string ValueToString(object value, Type type);
        object StringToValue(string value, Type type);
    }
}
