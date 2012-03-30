////////////////////////////////////////////////////////////////////////////////
// TimeTick.cs
// 2012.03.29, created by sohong
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
using System.Windows.Media;
using System.Globalization;

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// TimelineBar 하단에 표시되는 time mark.
    /// </summary>
    public class TimeTickVisual : TimelineElement {

        #region constructor

        public TimeTickVisual(FrameworkElement container) 
            : base(container) {
        }

        #endregion // constructor


        #region properties

        /// <summary>
        /// Text
        /// </summary>
        public string Text {
            get { return m_text; }
            set {
                if (!string.Equals(value, m_text)) {
                    m_text = value;
                    Invalidate();
                }
            }
        }
        private string m_text;

        /// <summary>
        /// Font family
        /// </summary>
        public string FontFamily {
            get { return m_fontFamily; }
            set {
                if (!string.Equals(value, m_fontFamily)) {
                    m_fontFamily = value;
                    Invalidate();
                }
            }
        }
        private string m_fontFamily;

        /// <summary>
        /// Font style
        /// </summary>
        public FontStyle FontStyle {
            get { return m_fontStyle; }
            set {
                if (value != m_fontStyle) {
                    m_fontStyle = value;
                    Invalidate();
                }
            }
        }
        private FontStyle m_fontStyle;

        /// <summary>
        /// Font weight
        /// </summary>
        public FontWeight FontWeight {
            get { return m_fontWeight; }
            set {
                if (value != m_fontWeight) {
                    m_fontWeight = value;
                    Invalidate();
                }
            }
        }
        private FontWeight m_fontWeight;

        /// <summary>
        /// Font size
        /// </summary>
        public double FontSize {
            get { return m_fontSize; }
            set {
                if (value != m_fontSize) {
                    m_fontSize = value;
                    Invalidate();
                }
            }
        }
        private double m_fontSize;

        /// <summary>
        /// Foreground brush
        /// </summary>
        public Brush Foreground {
            get { return m_foreground; }
            set {
                if (value != m_foreground) {
                    m_foreground = value;
                    Invalidate();
                }
            }
        }
        private Brush m_foreground;

        #endregion // properties


        #region overriden methods

        protected override void DoDraw(DrawingContext dc) {
            Rect r = new Rect(0, 0, Width, Height);
            FormattedText ft = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                new Typeface(FontFamily), FontSize, Foreground);
            ft.SetFontWeight(FontWeight);
            ft.SetFontStyle(FontStyle);

            dc.DrawText(ft, new Point(0, 0));
        }

        #endregion // overriden methods

    }
}
