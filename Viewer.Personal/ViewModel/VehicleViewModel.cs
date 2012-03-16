////////////////////////////////////////////////////////////////////////////////
// VehicleViewModel.cs
// 2012.03.15, created by sohong
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
using System.Windows.Input;

namespace Viewer.Personal.ViewModel {
    
    /// <summary>
    /// View model for VehicleView.
    /// </summary>
    public class VehicleViewModel : DialogViewModel {

        #region constructor

        public VehicleViewModel() {
        }

        #endregion // constructor


        #region properties

        public Vehicle Vehicle {
            get;
            set;
        }

        #endregion // properties


        #region overriden methods

        protected override object GetSubmitData() {
            return this.Vehicle;
        }

        protected override bool CanSubmit(object data) {
            return true;
        }

        protected override void DoSubmit(object data) {
        }

        #endregion // overriden methods
    }
}
