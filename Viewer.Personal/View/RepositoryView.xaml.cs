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
using Viewer.Common.Service;
using Viewer.Personal.ViewModel;
using Viewer.Common.Util;

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


        #region event handlers

        // Test
        private void btnTest_Click(object sender, RoutedEventArgs e) {
            Vehicle v = new Vehicle() {
                VehicleId = "Id",
                Name = "Name",
                Description = "..."
            };
            DialogService.Run("차량 추가", new VehicleView(), new VehicleViewModel() { Vehicle = v }, (obj) => {
                Vehicle v2 = (Vehicle)obj;
                MessageUtil.Show(v2.Name);
            });
        }

        #endregion // event handlers
    }
}
