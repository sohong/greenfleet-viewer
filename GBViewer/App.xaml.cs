using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Viewer.Common.Util;

namespace GBViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Trace.Listeners.Add(new GBTraceListener());

            ViewerBootstrapper bootstrapper = new ViewerBootstrapper();
            bootstrapper.Run();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "Application Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            App.Current.Shutdown();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\r\n" + e.Exception.StackTrace, "Application Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = false;
            App.Current.Shutdown();
        }
    }

    public class GBTraceListener : TraceListener
    {
        public override void WriteLine(string message)
        {
            Logger.Debug("{{TRACE}} " + message);
        }

        public override void Write(string message)
        {
            Logger.Debug("{{TRACE}} " + message);
        }

        public override void Write(object o)
        {
            if (o is Exception) {
                Exception ex = (Exception)o;
                WriteLine(ex.Message);
                WriteLine(ex.StackTrace);
            
            } else {
                base.Write(o);
            }
        }
    }
}
