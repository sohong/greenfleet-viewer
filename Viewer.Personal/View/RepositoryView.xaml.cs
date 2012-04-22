////////////////////////////////////////////////////////////////////////////////
// RepositoryView.cs
// 2012.03.09, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
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
using Viewer.Personal.ViewModel;

namespace Viewer.Personal.View
{
    /// <summary>
    /// Device/Local Repository view.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class RepositoryView : UserControl
    {
        #region constructors

        public RepositoryView()
        {
            InitializeComponent();
        }

        #endregion // constructors


        #region properties

        [Import]
        public RepositoryViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        #endregion // properties
    }
}
