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
using Viewer.Common.Model;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class VideoView : UserControl {

        #region dependency properties

        /// <summary>
        /// Track
        /// </summary>
        public static readonly DependencyProperty TrackProperty =
            DependencyProperty.Register(
                "Track",
                typeof(Track),
                typeof(VideoView),
                new PropertyMetadata(TrackPropertyChanged));

        private static void TrackPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            VideoView view = (VideoView)obj;
            Track track = e.NewValue as Track;

            view.Stop();
            if (track != null && !string.IsNullOrWhiteSpace(track.MpegFile)) {
                view.mediaMain.Source = new Uri(track.MpegFile, UriKind.Absolute);
                if (view.AutoPlay) {
                    view.Play();
                }
            } else {
                view.mediaMain.Source = null;
            }
        }

        /// <summary>
        /// Steps
        /// </summary>
        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register(
                "Steps",
                typeof(int),
                typeof(VideoView), 
                new PropertyMetadata(20, null), 
                StepsPropertyValidate);

        private static bool StepsPropertyValidate(object newValue) {
            return (int)newValue >= 2 && (int)newValue <= 100;
        }

        /// <summary>
        /// AutoPlay.
        /// track이 재설정되면 자동으로 play
        /// </summary>
        public static readonly DependencyProperty AutoPlayProperty =
            DependencyProperty.Register(
                "AutoPlay",
                typeof(bool),
                typeof(VideoView),
                new PropertyMetadata(false, null));

        #endregion dependency properties

        
        #region fields

        private DispatcherTimer m_timer;
        private bool m_sliding;
        private double m_videoLength;

        #endregion // fields


        #region events 

        public event Action<VideoView, double/* length */, double/* current */> PositionChanged;

        #endregion // events


        #region constructor

        public VideoView() {
            InitializeComponent();

            Steps = 20;

            m_timer = new DispatcherTimer();
            m_timer.Interval = TimeSpan.FromMilliseconds(50);
            m_timer.Tick += new EventHandler(timer_Tick);

            timelineSlider.AddHandler(Slider.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler((obj, args) => {
                if (VisualUtil.FindAncestor<System.Windows.Controls.Primitives.Thumb>((DependencyObject)args.OriginalSource) != null) {
                    m_sliding = true;
                } else {
                    mediaMain.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
                }
            }), true);
        }

        void timer_Tick(object sender, EventArgs e) {
            if (!m_sliding) {
                timelineSlider.Value = mediaMain.Position.TotalMilliseconds;
            }
            if (PositionChanged != null) {
                PositionChanged(this, m_videoLength, mediaMain.Position.TotalMilliseconds);
            }
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Track 정보.
        /// </summary>
        public Track Track {
            get { return (Track)GetValue(TrackProperty); }
            set { SetValue(TrackProperty, value); }
        }

        public int Steps {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public bool AutoPlay {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        #endregion // properties


        #region methods

        public void Play() {
            //if (mediaMain.HasVideo) { // TODO 항상 false를 리턴하고 있다.
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

        public void Previous() {
            int steps = Math.Max(2, Steps);
            double v = Math.Max(0, mediaMain.Position.TotalMilliseconds - m_videoLength / steps);
            mediaMain.Position = TimeSpan.FromMilliseconds(v);
        }

        public void Next() {
            int steps = Math.Max(2, Steps);
            double v = Math.Min(m_videoLength, mediaMain.Position.TotalMilliseconds + m_videoLength / steps);
            mediaMain.Position = TimeSpan.FromMilliseconds(v);
        }

        public void End() {
            mediaMain.Position = TimeSpan.FromMilliseconds(m_videoLength);
        }

        #endregion // methods


        #region internal methods

        #endregion // internal methods


        #region event handlers

        private void mediaMain_MediaOpened(object sender, RoutedEventArgs e) {
            Logger.Debug("Media opened");
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
            Previous(); 
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) {
            Next();
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e) {
            End();
        }

        private void timelineSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            if (m_sliding) {
                mediaMain.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
                m_sliding = false;
            }
        }

        #endregion // event handlers
    }
}
