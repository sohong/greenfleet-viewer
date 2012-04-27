////////////////////////////////////////////////////////////////////////////////
// DeviceRepository.cs
// 2012.03.20, created by sohong
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
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.IO;
using Viewer.Common.Model;
using Viewer.Common.Loader;
using System.ComponentModel;
using System.Windows;
using Viewer.Common;
using Viewer.Common.ViewModel;
using Viewer.Common.Service;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// SD Card 등 블랙박스 저장 미디어에 포함된 트랙 정보들.
    /// </summary>
    public class DeviceRepository : Repository
    {
        #region fields

        private Vehicle m_vehicle;
        private string m_rootPath;

        #endregion // fields


        #region constructor

        public DeviceRepository()
            : base("Device")
        {
        }

        #endregion // constructor


        #region properties

        public Vehicle Vehicle
        {
            get { return m_vehicle; }
        }

        public string RootPath
        {
            get { return m_rootPath; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// 입력 디바이스의 트랙 목록을 읽어들인다.
        /// 시작/끝 일시를 계산한다.
        /// </summary>
        /// <param name="rootPath"></param>
        public void Open(Vehicle vehicle, string rootPath, Action callback)
        {
            m_vehicle = vehicle;
            m_rootPath = rootPath;

            TrackList.Clear();
            if (Directory.Exists(rootPath)) {
                LoadTracks(callback);
            }
        }

        #endregion // methods


        #region internal methods

        private void LoadTracks(Action callback)
        {
            string[] files = Directory.GetFiles(m_rootPath, "*.inc");
            if (files.Length > 0) {
                TrackList.Load(files, files.Length, "SD 카드 로딩", callback);
            }
        }

        #endregion // interanl methods
    }
}
