////////////////////////////////////////////////////////////////////////////////
// PersonalViewModel.cs
// 2012.03.20, created by sohong
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
using Viewer.Common.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Viewer.Common.Service;
using Viewer.Personal.View;
using Viewer.Common.Util;
using Viewer.Personal.Command;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// Main view model for Personal viewer.
    /// </summary>
    public class PersonalViewModel : ViewModelBase {

        #region constructor

        public PersonalViewModel() {
            VehicleCommand = new DelegateCommand<object>(DoVechicle, CanVehicle);
            PreferencesCommand = new DelegateCommand<object>(DoPreferences, CanPreferences);
            TestCommand = new DelegateCommand<object>(DoTest, CanTest);
        }

        #endregion // constructor


        #region properties

        public Commands Commands {
            get { return Commands.Instance; }
        }

        /// <summary>
        /// 차량 관리 command
        /// </summary>
        public ICommand VehicleCommand {
            get;
            private set;
        }

        /// <summary>
        /// 환경 설정 command
        /// </summary>
        public ICommand PreferencesCommand {
            get;
            private set;
        }

        /// <summary>
        /// Test command
        /// </summary>
        public ICommand TestCommand {
            get;
            private set;
        }

        #endregion // properties


        #region internal methods

        // Vehicle Command
        private bool CanVehicle(object data) {
            return true;
        }

        private void DoVechicle(object data) {
            DialogService.Run("차량 정보 관리", new VehicleListView(), new VehicleListViewModel());
        }

        // Preferences command
        private bool CanPreferences(object data) {
            return true;
        }

        private void DoPreferences(object data) {
            DialogService.Run("환경 설정", new PreferencesView(), new PreferencesViewModel());
        }

        // Test command
        private bool CanTest(object data) {
            return true;
        }

        private void DoTest(object data) {
            MessageUtil.Show("Test what?");
        }

        #endregion // internal methods
    }
}
