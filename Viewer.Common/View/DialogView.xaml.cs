////////////////////////////////////////////////////////////////////////////////
// DialogView.cs
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
using System.Windows.Shapes;
using Viewer.Common.ViewModel;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class DialogView : Window {

        #region constructor

        public DialogView() {
            InitializeComponent();
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Dialog content view model
        /// </summary>
        public IDialogViewModel Model {
            get { return DataContext as IDialogViewModel; }
            set {
                if (value != DataContext) {
                    DataContext = value;

                    if (value != null) {
                        RefreshView();
                    }
                }
            }
        }

        /// <summary>
        /// Dialog content view
        /// </summary>
        public UserControl View {
            get { return m_view; }
            set {
                if (value != m_view) {
                    if (m_view != null) {
                        grdContent.Children.Remove(m_view);
                    }
                    m_view = value;
                    if (m_view != null) {
                        grdContent.Children.Add(m_view);
                    }
                }
            }
        }
        private UserControl m_view;

        #endregion // properties


        #region internal methods

        protected virtual void RefreshView() {
            IDialogViewModel model = this.Model;
            
            if (model != null) {
                btnCancel.Content = model.CancelText;
                btnCancel.IsEnabled = model.IsCancelable;
                btnOK.Content = model.ConfirmText;
            
            } else {
                btnCancel.IsEnabled = true;
                btnOK.IsEnabled = false;
            }
        }

        protected virtual void DoConfirm() {
            if (Model != null && Model.Confirm()) {
                DialogResult = true;
                Close();
            }
        }

        protected virtual void DoCancel() {
            if (Model != null && !Model.Cancel()) {
                return;
            }

            Close();
        }

        #endregion // internal methods


        #region event handlers

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            DoConfirm();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            DoCancel();
        }

        #endregion // event handlers
    }
}
