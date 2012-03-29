////////////////////////////////////////////////////////////////////////////////
// PersonalTestBase.cs
// 2012.03.27, created by sohong
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

namespace Viewer.Personal.Test {

    public static class PersonalTest {

        #region consts
        #endregion // consts


        #region properties

        public static string TestRoot {
            get {
                string root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");
                Directory.CreateDirectory(root);
                return root;
            }
        }

        public static string StorageRoot {
            get {
                string root = Path.Combine(TestRoot, "storage");
                Directory.CreateDirectory(root);
                return root;
            }
        }

        public static string DeviceRoot {
            get {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "samples");
            }
        }

        #endregion // properties
    }
}
