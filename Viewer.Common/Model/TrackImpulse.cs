////////////////////////////////////////////////////////////////////////////////
// TrackImpulse.cs
// 2012.03.09, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Common.Model {

    /// <summary>
    /// 충격 데이터.
    /// </summary>
    public class TrackImpulse : NotificationObject {

        #region constructors

        public TrackImpulse() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// X
        /// </summary>
        public double X {
            get { return m_x; }
            set {
                if (value != m_x) {
                    m_x = value;
                    RaisePropertyChanged(() => X);
                }
            }
        }
        private double m_x;

        /// <summary>
        /// Y
        /// </summary>
        public double Y {
            get { return m_y; }
            set {
                if (value != m_y) {
                    m_y = value;
                    RaisePropertyChanged(() => Y);
                }
            }
        }
        private double m_y;

        /// <summary>
        /// Z
        /// </summary>
        public double Z {
            get { return m_z; }
            set {
                if (value != m_z) {
                    m_z = value;
                    RaisePropertyChanged(() => Z);
                }
            }
        }
        private double m_z;

        #endregion // properties
    }
}
