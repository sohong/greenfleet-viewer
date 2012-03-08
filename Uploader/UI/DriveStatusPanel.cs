////////////////////////////////////////////////////////////////////////////////
// DriveStatusPanel.cs
// 2012.03.05, Deleted by sohong
//
// =============================================================================
// Copyright (C) 2011 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GreenFleet.Uploader.UI {

    /// <summary>
    /// SD 카드 삽입 상태 표시.
    /// </summary>
    public partial class DriveStatusPanel : Component {

        #region constructors

        public DriveStatusPanel() {
            InitializeComponent();
        }

        public DriveStatusPanel(IContainer container) {
            container.Add(this);
            InitializeComponent();
        }

        #endregion // constructors
    }
}
