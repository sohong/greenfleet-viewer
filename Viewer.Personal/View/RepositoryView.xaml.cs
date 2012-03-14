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
using Viewer.Personal.Model;

namespace Viewer.Personal.View {

    /// <summary>
    /// </summary>
    public partial class RepositoryView : UserControl {

        #region constructor

        public RepositoryView() {
            InitializeComponent();

            dateFrom.SelectedDate = DateTime.Today;
            dateTo.SelectedDate = DateTime.Today;
        }

        #endregion // constructor
    }
}
