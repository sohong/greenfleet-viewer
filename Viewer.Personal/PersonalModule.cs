////////////////////////////////////////////////////////////////////////////////
// PersonalModule.cs
// 2012.03.08, created by sohong
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
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Viewer.Personal.Model;
using Viewer.Common.Util;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Viewer.Personal.View;
using Microsoft.Practices.ServiceLocation;

namespace Viewer.Personal {

    /// <summary>
    /// Personal module.
    /// </summary>
    [ModuleExport(typeof(PersonalModule))]
    public class PersonalModule : IModule {

        [Import]
        private IRegionManager m_regionManager;

        public void Initialize() {
            Debug.WriteLine("Personal Module initialize...");
            Logger.Debug("Personal Module initialize...");

            m_regionManager.RegisterViewWithRegion("main", typeof(RepositoryView));

            PersonalDomain.Domain.EventAggregator = (IEventAggregator)ServiceLocator.Current.GetService(typeof(IEventAggregator));
            PersonalDomain.Domain.Start();
        }
    }
}
