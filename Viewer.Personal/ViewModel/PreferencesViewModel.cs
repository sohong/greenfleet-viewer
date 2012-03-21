////////////////////////////////////////////////////////////////////////////////
// PreferencesViewModel.cs
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
using Viewer.Personal.Model;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Util;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for PreferencesView
    /// </summary>
    public class PreferencesViewModel : DialogViewModel {

        #region fields

        private Preferences m_prefers;

        #endregion // fields


        #region constructors

        public PreferencesViewModel() {
            m_prefers = PersonalDomain.Domain.Preferences.Clone();

            StorageCommand = new DelegateCommand(DoStorage, CanStorage);

            IsCancelable = true;
        }

        #endregion // constructors


        #region properties

        public Preferences Prefers {
            get { return m_prefers; }
        }

        public ICommand StorageCommand {
            get;
            private set;
        }

        #endregion // properties


        #region overriden methods

        protected override bool CanSubmit(object data) {
            return true;
        }

        protected override void DoSubmit(object data) {
            m_prefers.Assign(PersonalDomain.Domain.Preferences);
            PersonalDomain.Domain.SavePreferences();
        }

        #endregion // overriden methods


        #region internal methods

        // Storage command
        private bool CanStorage() {
            return true;
        }

        private void DoStorage() {
            DialogUtil.SelectFolder("", "");
        }

        #endregion // internal methods
    }
}
