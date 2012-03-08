////////////////////////////////////////////////////////////////////////////////
// ShellViewModel.cs
// 2012.03.06, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace GreenFleet.Viewer.ViewModel {

    /// <summary>
    /// View model for Shell.
    /// </summary>
    [Export]
    public class ShellViewModel : ViewModelBase {

        #region constructors

        public ShellViewModel() {
        }

        #endregion // constructors


        #region properties

        public string AppTitle {
            get { return "GreenFleet Viewer"; }
        }

        #endregion // properties
    }
}
