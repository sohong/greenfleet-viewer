////////////////////////////////////////////////////////////////////////////////
// ViewerBootstrapper.cs
// 2012.03.06, created by sohong
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
using GreenFleet.Viewer.View;

namespace GreenFleet.Viewer {

    /// <summary>
    /// Prism을 초기화하고 Application을 시작한다.
    /// </summary>
    public class ViewerBootstrapper : MefBootstrapper {

        #region constructors

        public ViewerBootstrapper() {
        }

        #endregion // constructors


        #region overriden methods

        protected override DependencyObject CreateShell() {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();

            // Shell 등을 export한다.
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewerBootstrapper).Assembly));

            /*
            // Add this assembly to export ModuleTracker
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(QuickStartBootstrapper).Assembly));

            // Module A is referenced in in the project and directly in code.
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleA).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleC).Assembly));

            // Module B and Module D are copied to a directory as part of a post-build step.
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Both projects have a post-build step to copy themselves into that directory.
            DirectoryCatalog catalog = new DirectoryCatalog("DirectoryModules");
            this.AggregateCatalog.Catalogs.Add(catalog);
            */
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();

            // Because we created the CallbackLogger and it needs to be used immediately, we compose it to satisfy any imports it has.
            //this.Container.ComposeExportedValue<CallbackLogger>(this.callbackLogger);
        }


        protected override ILoggerFacade CreateLogger() {
            return base.CreateLogger();
        }

        #endregion // overriden methods

    }
}
