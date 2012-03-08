////////////////////////////////////////////////////////////////////////////////
// StorageViewModel.cs
// 2012.03.08, created by sohong
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
using Viewer.ViewModel.Common;
using Viewer.Personal.Model;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for RepositoryView
    /// </summary>
    public class RepositoryViewModel : ViewModelBase {

        #region constructors

        public RepositoryViewModel() {
        }

        #endregion // constructors


        #region properties

        public Repository Repository {
            get { return PersonalDomain.Domain.Repository; }
        }

        #endregion // properties
    }
}
