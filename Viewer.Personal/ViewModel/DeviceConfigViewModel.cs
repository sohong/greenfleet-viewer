////////////////////////////////////////////////////////////////////////////////
// DeviceConfigViewModel.cs
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
using Viewer.Common.ViewModel;
using Viewer.Personal.Model;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for DeviceConfigView
    /// </summary>
    public class DeviceConfigViewModel : DialogViewModel {

        #region fields

        private DeviceConfig m_config;

        #endregion // fields


        #region constructor

        public DeviceConfigViewModel() {
            m_config = PersonalDomain.Domain.DeviceConfig.Clone();
        }

        #endregion // constructor


        #region properties

        public DeviceConfig Config {
            get { return m_config; }
        }

        #endregion // properties


        #region overriden methods

        protected override object GetSubmitData() {
            return Config;
        }

        protected override bool CanSubmit(object data) {
            return true;
        }

        protected override void DoSubmit(object data) {
            PersonalDomain.Domain.SaveDeviceConfig(m_config);
        }

        #endregion // overriden methods
    }
}
