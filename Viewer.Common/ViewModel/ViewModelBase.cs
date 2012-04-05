////////////////////////////////////////////////////////////////////////////////
// ViewModelBase.cs
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
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.ViewModel {
    
    /// <summary>
    /// View model abstract.
    /// </summary>
    public abstract class ViewModelBase : NotificationObject {

        public object Tag;
    }
}
