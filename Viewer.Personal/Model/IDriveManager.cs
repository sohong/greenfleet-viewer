////////////////////////////////////////////////////////////////////////////////
// DriveManager.cs
// 2012.03.28, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// 외부 입력 장치(SD 카드등) 관련...
    /// </summary>
    public interface IDriveManager
    {
        /// <summary>
        /// (외부 입력 장치로 연결된)드라이브들 중 greenfleet 트랙 데이터를 포함했는 지
        /// 확인해서 포함된 첫번째 드라이브의 데이터 폴더를 리턴한다.
        /// TODO 여러 드라이브들 중 하나를 선택할 수 있도록...
        /// 
        /// (테스트를 위해)준비된 상태의 Removable 드라이브가 존재하지 않으면
        /// x, y, z 드라이브 중 하나를 Removable로 인식한다.
        /// </summary>
        /// <returns></returns>
        string FindTrackDrive(bool testing);
    }
}
