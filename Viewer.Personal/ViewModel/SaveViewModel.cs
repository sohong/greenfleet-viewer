////////////////////////////////////////////////////////////////////////////////
// SaveViewModel.cs
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
using Viewer.Common.ViewModel;
using Viewer.Personal.Model;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for SaveView
    /// </summary>
    public class SaveViewModel : DialogViewModel {

        #region constructors

        public SaveViewModel(DeviceRepository repository, DateTime dateFrom, DateTime dateTo) {
            Repository = repository;
            Options = new SaveOption();
            Options.StartDate = dateFrom;
            Options.EndDate = dateTo;
            Options.Convert = false;
            Options.Overwrite = true;
            IsCancelable = true;
            CancelText = "취소";
            SubmitText = "저장";
        }

        #endregion // constructors


        #region properties

        public DeviceRepository Repository {
            get;
            private set;
        }

        public SaveOption Options {
            get;
            private set;
        }

        #endregion // properties


        #region overriden methods

        protected override object GetSubmitData() {
            return Options;
        }

        protected override bool CanSubmit() {
            return true;
        }

        protected override void DoSubmit() {
            PersonalDomain.Domain.SaveDevice(Repository, Options);
        }

        #endregion // overriden methods
    }
}
