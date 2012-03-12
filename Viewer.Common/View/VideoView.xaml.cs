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
using System.Windows.Threading;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class VideoView : UserControl {

        #region fields

        private DispatcherTimer m_timer;
        private bool m_sliding;
        private double m_videoLength;

        #endregion // fields


        #region events 

        public event Action<VideoView, double, double> PositionChanged;

        #endregion // events


        #region constructor

        public VideoView() {
            InitializeComponent();

            m_timer = new DispatcherTimer();
            m_timer.Interval = TimeSpan.FromMilliseconds(50);
            m_timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e) {
            if (!m_sliding) {
                timelineSlider.Value = Math.Max(timelineSlider.Value, mediaMain.Position.TotalMilliseconds);
            }
            if (PositionChanged != null) {
                PositionChanged(this, m_videoLength, mediaMain.Position.TotalMilliseconds);
            }
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
                m_timer.Start();
            //}
        }

        public void Stop() {
            mediaMain.Stop();
            m_timer.Start();
        }

        public void Pause() {
            mediaMain.Pause();
            m_timer.Start();
        }

        public void Home() {
            mediaMain.Position = TimeSpan.FromSeconds(0);
        }

        public void End() {
            mediaMain.Position = TimeSpan.FromMilliseconds(m_videoLength);
        }

        #endregion // methods


        #region event handlers

        private void mediaMain_MediaOpened(object sender, RoutedEventArgs e) {
            LogUtil.Debug("Media opened");
            timelineSlider.Maximum = m_videoLength = mediaMain.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void mediaMain_MediaEnded(object sender, RoutedEventArgs e) {
            Stop();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e) {
            Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e) {
            Stop();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e) {
            Pause();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e) {
            Home();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e) {
            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) {

        }

        private void btnEnd_Click(object sender, RoutedEventArgs e) {
            End();
        }

        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //LogUtil.Debug("timeLineSlider.ValueChanged: " + timelineSlider.Value);
            //mediaMain.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
        }

        private void timelineSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            m_sliding = true;
        }

        private void timelineSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            mediaMain.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
            mediaMain.Play();
            m_sliding = false;
        }

        #endregion // event handlers
    }
}
