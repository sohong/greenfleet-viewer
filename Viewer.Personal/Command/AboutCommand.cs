////////////////////////////////////////////////////////////////////////////////
// AboutCommand.cs
// 2012.03.21, created by sohong
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
using Viewer.Common.Service;
using Viewer.Personal.View;
using Viewer.Personal.ViewModel;

namespace Viewer.Personal.Command {

    /// <summary>
    /// Personal viewer를 소개한다.
    /// </summary>
    public class AboutCommand : SimpleCommand {

        #region overriden methods

        public override void Execute(object parameter) {
            DialogService.Run("About GreenFleets Viewer", new AboutView(), new AboutViewModel());
        }

        #endregion // overriden methods
    }
}
