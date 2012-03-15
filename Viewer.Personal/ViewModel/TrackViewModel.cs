////////////////////////////////////////////////////////////////////////////////
// TrackView.cs
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
using Viewer.Common.ViewModel;
using Viewer.Common.Model;
using System.Collections.ObjectModel;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for TrackView.
    /// </summary>
    public class TrackViewModel : ViewModelBase {

        #region fields

        private ObservableCollection<Track> m_tracks;
        
        #endregion // fields


        #region constructors

        public TrackViewModel() {
            m_tracks = new ObservableCollection<Track>();
        }

        #endregion // constructors


        #region properties

        public ObservableCollection<Track> Tracks {
            get { return m_tracks; }
        }

        #endregion // properties
    }
}
