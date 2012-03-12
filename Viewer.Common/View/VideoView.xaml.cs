////////////////////////////////////////////////////////////////////////////////
// VideoView.cs
// 2012.03.07, created by sohong
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Viewer.Common.Util;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class VideoView : UserControl {

        #region fields

        private double m_videoLength;

        #endregion // fields


        #region constructor

        public VideoView() {
            InitializeComponent();
        }

        #endregion // constructor


        #region properties

        public Uri Source {
            get { return mediaMain.Source; }
            set { mediaMain.Source = value; }
        }

        #endregion // properties


        #region methods

        public void Play() {
            // TODO 항상 false를 리턴하고 있다.
            //if (mediaMain.HasVideo) { 
                mediaMain.Play();
            //}
        }

        public void Stop() {
            mediaMain.Stop();
        }

        public void Pause() {
            mediaMain.Pause();
        }

        public void Home() {
            mediaMain.Position = TimeSpan.FromSeconds(0);
        }

        public void End() {
            mediaMain.Position = TimeSpan.FromMilliseconds(m_videoLength);
        }

        #endregion // methods


        #region event handlers

        private void btnPlay_Click(object sender, RoutedEventArgs e) {
            Play();
        }

        #endregion // event handlers

        private void mediaMain_MediaOpened(object sender, RoutedEventArgs e) {
            LogUtil.Debug("Media opened");  
            m_videoLength = mediaMain.NaturalDuration.TimeSpan.TotalMilliseconds;
        }
    }
}
