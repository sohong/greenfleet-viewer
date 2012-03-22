////////////////////////////////////////////////////////////////////////////////
// TrackTreeView.cs
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using Viewer.Common.Model;
using Viewer.Common.Util;

namespace Viewer.Common.View {

    /// <summary>
    /// 트랙 정보들을 트리로 표시한다.
    /// </summary>
    public partial class TrackTreeView : UserControl {

        #region dependency properties

        /// <summary>
        /// ItemsSource
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(TrackTreeView),
                new PropertyMetadata(ItemsSourcePropertyChanged));

        private static void ItemsSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            TrackTreeView view = (TrackTreeView)obj;
            view.tvMain.ItemsSource = (IEnumerable)e.NewValue;
        }

        #endregion dependency properties


        #region events 

        public event Action<object, TrackGroup> ActivateGroup;
        public event Action<object, Track> ActivateTrack;

        #endregion // events


        #region constructors

        public TrackTreeView() {
            InitializeComponent();
        }

        #endregion // constructors


        #region properties

        public IEnumerable ItemsSource {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value);  }
        }

        #endregion // propertis


        #region methods

        /// <summary>
        /// 전체를 선택하거나 선택하지 않는다.
        /// </summary>
        public void SelectAll(bool select) {
            if (ItemsSource != null) {
                foreach (TrackGroup group in ItemsSource) {
                    SelectAll(group, select);
                }
            }
        }

        #endregion // methods


        #region internal methods

        private void SelectAll(TrackGroup group, bool select) {
            group.IsChecked = select;
            foreach (object obj in group.Children) {
                if (obj is Track) {
                    ((Track)obj).IsChecked = select;
                } else if (obj is TrackGroup) {
                    SelectAll((TrackGroup)obj, select);
                }
            }
        }

        #endregion // internal methods


        #region event handlers

        // tvMain
        private void tvMain_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Track track = tvMain.SelectedItem as Track;
            if (track != null) {
                if (ActivateTrack != null) {
                    ActivateTrack(this, track);
                }
                return;
            }

            TrackGroup group = tvMain.SelectedItem as TrackGroup;
            if (group != null) {
                if (ActivateGroup != null) {
                    ActivateGroup(this, group);
                }
            }
        }

        // chkAll
        private void chkAll_Click(object sender, RoutedEventArgs e) {
            Logger.Debug("chkAll: " + chkAll.IsChecked);
            SelectAll(chkAll.IsChecked == true);
        }

        #endregion // event handlers
    }
}
