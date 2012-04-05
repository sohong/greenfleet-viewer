////////////////////////////////////////////////////////////////////////////////
// ProgressViewModel.cs
// 2012.04.04, created by sohong
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
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Viewer.Common.ViewModel {

    /// <summary>
    /// View model for ProgressView
    /// </summary>
    public class ProgressViewModel : ViewModelBase {

        #region fields

        private bool m_canceled;

        #endregion // fields


        #region constructors

        public ProgressViewModel() {
            CancelCommand = new DelegateCommand(DoCancel, CanCancel);
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 다이얼로그 상단에 표시하는 타이틀.
        /// </summary>
        public string Caption {
            get { return m_caption; }
            set {
                if (value != m_caption) {
                    m_caption = value;
                    RaisePropertyChanged(() => Caption);
                }
            }
        }
        private string m_caption;

        /// <summary>
        /// 진행 메시지.
        /// </summary>
        public string Message {
            get { return m_message; }
            set {
                if (value != m_message) {
                    m_message = value;
                    RaisePropertyChanged(() => Message);
                }
            }
        }
        private string m_message;

        public double Minimum {
            get { return m_minimum; }
            set {
                if (value != m_minimum) {
                    m_minimum = value;
                    RaisePropertyChanged(() => Minimum);
                }
            }
        }
        private double m_minimum;

        public double Maximum {
            get { return m_maximum; }
            set {
                if (value != m_maximum) {
                    m_maximum = value;
                    RaisePropertyChanged(() => Maximum);
                }
            }
        }
        private double m_maximum;

        public double Value {
            get { return m_value; }
            set {
                if (value != m_value) {
                    m_value = value;
                    RaisePropertyChanged(() => Value);
                }
            }
        }
        private double m_value;

        public bool IsCancelable {
            get { return m_cancelable; }
            set {
                if (value != m_cancelable) {
                    m_cancelable = value;
                    RaisePropertyChanged(() => IsCancelable);
                }
            }
        }
        private bool m_cancelable = true;

        public string CancelText {
            get { return m_cancelText; }
            set {
                if (value != m_cancelText) {
                    m_cancelText = value;
                    RaisePropertyChanged(() => CancelText);
                }
            }
        }
        private string m_cancelText = "취소";

        /// <summary>
        /// Cancel command
        /// </summary>
        public ICommand CancelCommand {
            get;
            private set;
        }

        public bool IsCanceled {
            get { return m_canceled; }
            private set {
                if (value != m_canceled) {
                    m_canceled = value;
                    RaisePropertyChanged(() => IsCanceled);
                }
            }
        }

        #endregion // properties


        #region methods

        public void Cancel() {
            IsCanceled = true;
        }

        #endregion // methods


        #region internal methods

        private void DoCancel() {
            Cancel();
        }

        private bool CanCancel() {
            return IsCancelable;
        }

        #endregion // internal methods
    }
}
