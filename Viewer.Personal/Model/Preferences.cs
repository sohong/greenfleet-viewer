////////////////////////////////////////////////////////////////////////////////
// Preferences.cs
// 2012.03.07, created by sohong
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
using Microsoft.Practices.Prism.ViewModel;
using Viewer.Common.Model;
using System.IO;
using System.Xml.Linq;
using Viewer.Common.Xml;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Personal viewer 사용자 설정 정보들.
    /// </summary>
    public class Preferences : NotificationObject {

        #region const

        private const string ROOT_ELEMENT = "GreenFleet";
        private const string STORAGE_ROOT = @"C:\GreenFleets\storage";
        
        #endregion // const


        #region constructors

        public Preferences() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 트랙 파일 저장소 루트 경로.
        /// </summary>
        public string StorageRoot {
            get { 
                return string.IsNullOrWhiteSpace(m_storageRoot) ? STORAGE_ROOT : m_storageRoot; 
            }
            set {
                if (value != m_storageRoot) {
                    m_storageRoot = value;
                    RaisePropertyChanged(() => StorageRoot);
                }
            }
        }
        private string m_storageRoot = STORAGE_ROOT;

        /// <summary>
        /// Retention 적용 방법.
        /// </summary>
        public RetentionMode RetentionMode {
            get { return m_retentionMode; }
            set {
                if (value != m_retentionMode) {
                    m_retentionMode = value;
                    RaisePropertyChanged(() => RetentionMode);
                }
            }
        }
        private RetentionMode m_retentionMode;

        /// <summary>
        /// 보존 기간. YY,MM,DD로 지정.
        /// </summary>
        public string Retention {
            get { return m_retention; }
            set {
                if (value != m_retention) {
                    m_retention = value;
                    RaisePropertyChanged(() => Retention);
                }
            }
        }
        private string m_retention = "00,00,00";

        #endregion // properties


        #region methods

        /// <summary>
        /// path로 지정된 xml 파일에서 설정 정보들을 가져온다.
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path) {
            Debug.WriteLine("Preferences load...");

            if (!File.Exists(path)) {
                Save(path);
                return;
            }

            XDocument doc = XDocument.Load(path);
            XElement root = doc.Root;
            if (!ROOT_ELEMENT.Equals(root.Name.LocalName)) {
                throw new Exception("Xml is not a GreenFleet perferences document.");
            }

            new XmlTransformer().Deserialize(root, this);
            Debug.WriteLine("Preferences loaded.");
        }

        /// <summary>
        /// path로 지정된 파일에 설정 정보를 xml로 저장한다.
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path) {
            Debug.WriteLine("Preferences save...");
            
            XDocument doc = new XDocument();
            XElement root = new XElement(ROOT_ELEMENT);
            doc.Add(root);
            new XmlTransformer().Serialize(this, root);

            doc.Save(path);

            Debug.WriteLine("Preferences saved.");
            Debug.WriteLine(doc.ToString());
        }

        public Preferences Clone() {
            Preferences prefers = new Preferences();
            Assign(prefers);
            return prefers;
        }

        public void Assign(Preferences target) {
            if (target != null) {
                target.StorageRoot = StorageRoot;
                target.RetentionMode = RetentionMode;
                target.Retention = Retention;
            }
        }

        #endregion // methods
    }
}
