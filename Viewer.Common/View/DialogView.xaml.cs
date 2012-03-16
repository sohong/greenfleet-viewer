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
using Microsoft.Practices.Prism.Commands;

namespace Viewer.Common.View {

    /// <summary>
    /// </summary>
    public partial class DialogView : Window {

        #region fields

        private Action<object> m_callback;
        
        #endregion // fields


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
                    if (DataContext is IDialogViewModel) {
                        DelegateCommand<object> cmd = ((IDialogViewModel)DataContext).SubmitCommand as DelegateCommand<object>;
                        if (cmd != null) {
                            cmd.CanExecuteChanged -= new EventHandler(Submit_CanExecuteChanged);
                        }
                    }

                    DataContext = value;

                    if (DataContext is IDialogViewModel) {
                        DelegateCommand<object> cmd = ((IDialogViewModel)DataContext).SubmitCommand as DelegateCommand<object>;
                        if (cmd != null) {
                            cmd.CanExecuteChanged += new EventHandler(Submit_CanExecuteChanged);
                        }
                    }
                    RefreshDialog();
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


        #region methods

        public void ShowCallback(Action<object> callback) {
            m_callback = callback;
            ShowDialog();
        }

        #endregion // methods


        #region internal methods

        void Submit_CanExecuteChanged(object sender, EventArgs e) {
            RefreshDialog();
        }

        protected virtual void RefreshDialog() {
            IDialogViewModel model = this.Model;
            ICommand cmd = (model != null) ? model.SubmitCommand : null;

            if (cmd != null) {
                btnOK.IsEnabled = cmd.CanExecute(model.SubmitData);
            
            } else {
                btnOK.IsEnabled = false;
                btnCancel.IsEnabled = true;
            }
        }

        protected virtual void DoConfirm() {
            IDialogViewModel model = this.Model;
            ICommand cmd = (model != null) ? model.SubmitCommand : null;

            if (cmd != null) {
                object data = model.SubmitData;
                cmd.Execute(data);
                DialogResult = true;
                Close();

                if (m_callback != null) {
                    m_callback(data);
                }
            }
        }

        protected virtual void DoCancel() {
            if (Model != null && Model.Cancel()) {
                Close();
            }
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
