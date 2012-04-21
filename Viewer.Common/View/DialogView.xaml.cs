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

        private EventHandler m_canExecuteChangedHandler;
        private Action<object> m_callback;
        
        #endregion // fields


        #region constructor

        public DialogView() {
            InitializeComponent();
            m_canExecuteChangedHandler = new EventHandler(Submit_CanExecuteChanged);
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
                        DelegateCommand cmd = ((IDialogViewModel)DataContext).SubmitCommand as DelegateCommand;
                        if (cmd != null) {
                            cmd.CanExecuteChanged -= m_canExecuteChangedHandler;
                        }
                    }

                    DataContext = value;

                    if (DataContext is IDialogViewModel) {
                        DelegateCommand cmd = ((IDialogViewModel)DataContext).SubmitCommand as DelegateCommand;
                        if (cmd != null) {
                            cmd.CanExecuteChanged += m_canExecuteChangedHandler;
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

        /// <summary>
        /// Submit button content
        /// </summary>
        public string SubmitText {
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    btnOK.Content = value;
                }
            }
        }

        /// <summary>
        /// Cancel button content
        /// </summary>
        public string CancelText {
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    btnCancel.Content = value;
                }
            }
        }

        /// <summary>
        /// Cancel 버튼을 표시할 것인가?
        /// </summary>
        public bool ShowCancel {
            set {
                btnCancel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion // properties


        #region methods

        public void ShowCallback(Action<object> callback) {
            m_callback = callback;

            if (m_view != null) {
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            } 

            ShowDialog();
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
        }

        protected override Size MeasureOverride(Size availableSize) {
            Size sz = base.MeasureOverride(availableSize);
            if (m_view != null && !Double.IsNaN(m_view.Width) && ActualWidth > 0) {
                this.Width = ActualWidth;
                this.Height = ActualHeight;
                SizeToContent = System.Windows.SizeToContent.Manual;
                m_view.Width = Double.NaN;
                m_view.Height = Double.NaN;
            }
            return sz;
        }

        protected override Size ArrangeOverride(Size arrangeBounds) {
            Size sz = base.ArrangeOverride(arrangeBounds);
            return sz;
        }

        #endregion // methods


        #region internal methods

        private void Submit_CanExecuteChanged(object sender, EventArgs e) {
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape && Model.IsCancelable) {
                DoCancel();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            DoConfirm();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            DoCancel();
        }

        #endregion // event handlers
    }
}
