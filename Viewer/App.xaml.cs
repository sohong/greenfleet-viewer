////////////////////////////////////////////////////////////////////////////////
// App.xml.cs
// 2012.03.06, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace GreenFleet.Viewer {

    /// <summary>
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ViewerBootstrapper bootstrapper = new ViewerBootstrapper();
            bootstrapper.Run();
        }
    }
}
