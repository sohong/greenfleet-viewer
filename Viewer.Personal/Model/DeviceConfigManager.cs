////////////////////////////////////////////////////////////////////////////////
// DeviceConfig.cs
// 2012.03.26, created by sohong
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

namespace Viewer.Personal.Model {

    /// <summary>
    /// 기기 설정 정보를 load/save 한다.
    /// </summary>
    public class DeviceConfigManager {

        #region constructors

        public DeviceConfigManager() {
        }

        #endregion // constructors


        #region methods

        public DeviceConfig Load(string filename) {
            return new DeviceConfig();
        }

        public void Save(DeviceConfig config) {
        }

        #endregion // methods
    }
}
