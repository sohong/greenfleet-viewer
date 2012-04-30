////////////////////////////////////////////////////////////////////////////////
// Shell.cs
// 2012.03.05, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Viewer.Personal.Model;
using Viewer.Common.Util;
using Microsoft.Practices.Prism.Events;
using Viewer.Personal.ViewModel;

namespace GBViewer.View
{
    /// <summary>
    /// Application main visual.
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        #region constructors

        public Shell()
        {
            Logger.Debug("Creating Shell...");
            InitializeComponent();

            if (SystemParameters.PrimaryScreenWidth > 1280) {
                this.WindowState = WindowState.Normal;
                this.Width = 1280;
                this.Height = Math.Min(SystemParameters.PrimaryScreenHeight - 40, 900);
            }
        }

        #endregion // constructors


        #region properties

        [Import]
        public PersonalViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        #endregion // properties


        #region event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Starting GBViewer shell...");
        }

        #endregion // event handlers
    }
}
