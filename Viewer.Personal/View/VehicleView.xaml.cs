////////////////////////////////////////////////////////////////////////////////
// VehicleView.cs
// 2012.03.15, created by sohong
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

namespace Viewer.Personal.View
{
    /// <summary>
    /// 차량 정보 등록/수정
    /// </summary>
    public partial class VehicleView : UserControl
    {
        #region constructor

        public VehicleView()
        {
            InitializeComponent();
        }

        #endregion // constructor


        #region event handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.SelectAll();
            txtName.Focus();
        }

        #endregion // event handlers
    }
}
