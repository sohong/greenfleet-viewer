using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Windows;

namespace HelloMap {

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    [ComVisible(true)]
    public class MapScriptHelper {

        public MapScriptHelper() {
        }

        public void MarkerDblClicked(string track) {
            MessageBox.Show("marker double clicked at " + track, "marker");
        }
    }
}
