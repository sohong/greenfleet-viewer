////////////////////////////////////////////////////////////////////////////////
// UploadRunner.cs
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

namespace GreenFleet.Uploader.Common {

    /// <summary>
    /// SD 카드가 연결된 드라이브에서 (자동으로?)지정한 영상및 관련 파일들을
    /// 지정된 로칼 스토리지로 복사하고,
    /// 서버 전송 요구를 메시지큐로 보낸다.
    /// 별도로 생성된 쓰레드에서 실행한다.
    /// </summary>
    public class UploadRunner {

        #region constructor

        public UploadRunner() {
        }
        
        #endregion // constructor

    }
}
