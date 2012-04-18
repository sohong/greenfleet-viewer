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
using Viewer.Common.UI;

namespace Viewer.Common.View {

    /// <summary>
    /// AccelerationChart testing view
    /// </summary>
    public partial class AccelerationChartView : UserControl {
    
        public AccelerationChartView() {
            InitializeComponent();

            chart.AddValue(3, 0, 0);
            chart.AddValue(2, 1.2, -1);
            chart.AddValue(2, 1.5, -2.1);
        }
    }
}
