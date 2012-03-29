////////////////////////////////////////////////////////////////////////////////
// MockDriveManager.cs
// 2012.03.28, created by sohong
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
using System.IO;
using Viewer.Personal.Model;

namespace Viewer.Personal.Test.Mocks {

    public class MockDriveManager : IDriveManager {

        #region IDriveManager

        public string FindTrackDrive(bool testing) {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PersonalTest.DeviceRoot);
            return folder;
        }

        #endregion // IDriveManager
    }
}
