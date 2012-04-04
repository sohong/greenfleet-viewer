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

namespace Viewer.Common.ViewModel {

    /// <summary>
    /// View model for ProgressView
    /// </summary>
    public class ProgressViewModel : ViewModelBase {

        #region constructors

        public ProgressViewModel() {
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

        public bool Cancelable {
            get { return m_cancelable; }
            set {
                if (value != m_cancelable) {
                    m_cancelable = value;
                    RaisePropertyChanged(() => Cancelable);
                }
            }
        }
        private bool m_cancelable;

        public string CancelText {
            get { return m_cancelText; }
            set {
                if (value != m_cancelText) {
                    m_cancelText = value;
                    RaisePropertyChanged(() => CancelText);
                }
            }
        }
        private string m_cancelText;

        #endregion // properties
    }
}
