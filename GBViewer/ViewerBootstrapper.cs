﻿////////////////////////////////////////////////////////////////////////////////
// ViewerBootstrapper.cs
// 2012.04.23, created by sohong
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
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Logging;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Viewer.Common;
using Viewer.Common.Util;
using GBViewer.View;

namespace GBViewer {

    /// <summary>
    /// Prism을 초기화하고 Application을 시작한다.
    /// </summary>
    public class ViewerBootstrapper : MefBootstrapper {

        #region constructors

        public ViewerBootstrapper() {
        }

        #endregion // constructors


        #region overriden methods

        protected override ILoggerFacade CreateLogger() {
            return Viewer.Common.Util.Logger.InitLog4Net("GBiewer");
        }

        protected override DependencyObject CreateShell() {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog() {
            IModuleCatalog cat = base.CreateModuleCatalog();
            return cat;
        }

        protected override void ConfigureModuleCatalog() {
            base.ConfigureModuleCatalog();
        }

        protected override AggregateCatalog CreateAggregateCatalog() {
            AggregateCatalog cat = base.CreateAggregateCatalog();
            return cat;
        }

        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();

            // Shell 등을 export한다.
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewerBootstrapper).Assembly));

            DirectoryCatalog catalog = new DirectoryCatalog(".");
            this.AggregateCatalog.Catalogs.Add(catalog);
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();

            // Because we created the CallbackLogger and it needs to be used immediately, we compose it to satisfy any imports it has.
            //this.Container.ComposeExportedValue<CallbackLogger>(this.callbackLogger);
        }


        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {
            return base.ConfigureRegionAdapterMappings();
        }

        #endregion // overriden methods

    }
}
