////////////////////////////////////////////////////////////////////////////////
// PersonalDomain.cs
// 2012.03.08, created by sohong
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
using Viewer.Common.Util;
using System.IO;
using System.Collections.ObjectModel;
using Viewer.Common.Xml;
using System.Collections.Specialized;
using Microsoft.Practices.Prism.Events;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Personal viewer application domain.
    /// </summary>
    public class PersonalDomain {

        #region consts

        private const string PREFERS_PATH = "preferences.xml";
        private const string WORKING_FOLDER = "workspace";
        
        #endregion // consts


        #region static members

        private static readonly PersonalDomain m_instance;

        static PersonalDomain() {
            m_instance = new PersonalDomain();
        }

        public static PersonalDomain Domain {
            get { return m_instance; }
        }

        #endregion // static members


        #region fields

        private Preferences m_preferences;
        private VehicleManager m_vehicles;
        private Repository m_repository;

        #endregion // fields


        #region constructors

        private PersonalDomain() {
            m_preferences = new Preferences();
            m_vehicles = new VehicleManager();
            m_repository = new Repository();
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 프로그램 환경 설정
        /// </summary>
        public Preferences Preferences {
            get { return m_preferences; }
        }

        /// <summary>
        /// 차량 목록
        /// </summary>
        public ObservableCollection<Vehicle> Vehicles {
            get { return m_vehicles.Vehicles; }
        }

        /// <summary>
        /// 저장소
        /// </summary>
        public Repository Repository {
            get { return m_repository; }
        }

        /// <summary>
        /// Prism global event 사서함.
        /// * 실행 환경에서는 bootstrapper가 설정해줘야 한다.
        /// null인 상태로 domain을 시작하면 예외를 발생시키도록 했다.
        /// </summary>
        public EventAggregator EventAggregator {
            get;
            set;
        }

        /// <summary>
        /// 디바이스 트랙영상 변환 파일 저장 등, 실행 시간에 필요한 처리를 위한 작업 폴더.
        /// 프로그램 종료 시나 시작 시 비운다.
        /// </summary>
        public string WorkingFolder {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, WORKING_FOLDER); }
        }

        #endregion // properties


        #region methods

        public void Start() {
            Logger.Info("Personal Domain start...");

            if (EventAggregator == null) {
                throw new Exception("Event aggregator가 설정되지 않았습니다.");
            }

            m_preferences.Load(PrefersPath);
            m_vehicles.Load();
            m_repository.Open(m_preferences.StorageRoot, m_vehicles.Vehicles);
            EmptyWorkingFolder();

            m_vehicles.Vehicles.CollectionChanged += new NotifyCollectionChangedEventHandler(Vehicles_CollectionChanged);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => {
                EmptyWorkingFolder();
            });

            Logger.Info("Personal Domain started.");
        }

        public void SavePreferences() {
            m_preferences.Save(PrefersPath);
        }

        #endregion // methods


        #region internal properties

        private string PrefersPath {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PREFERS_PATH); }
        }
        
        #endregion // internal properties


        #region internal methods

        /// <summary>
        /// 작업 폴더를 비운다.
        /// </summary>
        private void EmptyWorkingFolder() {
            string dir = WorkingFolder;
            try {
                if (Directory.Exists(dir)) {
                    Directory.Delete(dir, true);
                }

            } catch (Exception) {
            } finally {
                Directory.CreateDirectory(dir);
            }
        }

        private void Vehicles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            m_vehicles.Save();
        }

        #endregion // internal methods
    }
}
