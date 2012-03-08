////////////////////////////////////////////////////////////////////////////////
// SDCard.cs
// 2012.03.05, Deleted by sohong
//
// =============================================================================
// Copyright (C) 2011 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenFleet.Uploader.Model {

    /// <summary>
    /// PC 드라이브로 설정되는 sd card
    /// </summary>
    public class SDCard {

        #region constructors 

        public SDCard() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 시스템이 지정한 Drive 문자 - D, E, F, ...
        /// </summary>
        public char Drive {
            get { return m_drive; }
            set { m_drive = value; }
        }
        private char m_drive;

        /// <summary>
        /// 사용자가 지정하는 관리 번호.
        /// </summary>
        public byte ManageNo {
            get { return m_manageNo; }
            set { m_manageNo = value; }
        }
        private byte m_manageNo;

        #endregion // properties
    }
}
