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
using Microsoft.Windows.Controls;

namespace Viewer.Personal.View {

    /// <summary>
    /// 입력 장치(SD 카드 등)에 저장된 트랙 정보를 표시하고 관리한다.
    /// </summary>
    public partial class DeviceRepositoryView : UserControl {

        public DeviceRepositoryView() {
            InitializeComponent();
        }
    }
}
