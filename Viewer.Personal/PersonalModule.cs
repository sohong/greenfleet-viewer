////////////////////////////////////////////////////////////////////////////////
// PersonalModule.cs
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
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Viewer.Personal.Model;
using Viewer.Common.Util;

namespace Viewer.Personal {

    /// <summary>
    /// Personal module.
    /// </summary>
    [ModuleExport(typeof(PersonalModule))]
    public class PersonalModule : IModule {
    
        public void Initialize() {
            Debug.WriteLine("Personal Module initialize...");
            LogUtil.Debug("Personal Module initialize...");
            PersonalDomain.Domain.Start();
        }
    }
}
