////////////////////////////////////////////////////////////////////////////////
// AboutViewModel.cs
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
using Viewer.Common.ViewModel;
using Viewer.Common.Util;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View mode for AboutView
    /// </summary>
    public class AboutViewModel : DialogViewModel {

        #region fields
        #endregion // fields


        #region constructors

        public AboutViewModel() {
            IsCancelable = true;
            CancelText = null;
        }

        #endregion // constructors


        #region properties

        public string Version {
            get { return FileUtil.GetVersion(FileUtil.GetAppFilePath()); }
        }

        #endregion // properties


        #region overriden methods

        protected override bool CanSubmit() {
            return true;
        }

        #endregion // overriden methods
    }
}
