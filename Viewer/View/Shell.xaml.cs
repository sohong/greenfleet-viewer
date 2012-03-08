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
using GreenFleet.Viewer.ViewModel;

namespace GreenFleet.Viewer.View {

    /// <summary>
    /// Application main visual.
    /// </summary>
    [Export]
    public partial class Shell : Window {

        public Shell() {
            InitializeComponent();
        }
    
        [Import]
        ShellViewModel ViewModel {
            set {
                this.DataContext = value;
            }
        }
    }
}
