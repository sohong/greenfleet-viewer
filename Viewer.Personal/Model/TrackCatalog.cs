////////////////////////////////////////////////////////////////////////////////
// TrackCatalog.cs
// 2012.03.19, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Viewer.Common.Model;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Track file index.
    /// 차량/월 단위로 관리한다.
    /// </summary>
    public class TrackCatalog {

        #region consts
        
        private const string ROOT_ELEMENT = "TrackCatalog";
        private const string TRACK_ELEMENT = "track";
        private const string CREATED_ATTR = "created";
        private const string DATE_FORMAT = "yyyyMMdd HHmmss";

        #endregion // consts


        #region static members

        public static string MakeFileName(int year, int month) {
            return "cat_" + year.ToString("0000") + "_" + month.ToString("00") + ".xml";
        }

        public static bool GetFileData(string fileName, out int year, out int month) {
            year = 0;
            month = 0;

            if (fileName != null) {
                if (fileName.Length == 15 && fileName.StartsWith("cat_")) {
                    string s = fileName.Substring(4, 4);
                    if (int.TryParse(s, out year)) {
                        s = fileName.Substring(9, 2);
                        if (int.TryParse(s, out month)) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion // static members


        #region fields

        private Vehicle m_vehicle;
        private int m_year;
        private int m_month;
        private List<Track> m_tracks;
        
        #endregion // fields


        #region constructor

        public TrackCatalog(Vehicle vehicle, int year, int month) {
            m_vehicle = vehicle;
            m_year = year;
            m_month = month;
        }

        #endregion // constructor


        #region properties

        public List<Track> Tracks {
            get { return m_tracks; }
        }

        #endregion // properties


        #region methods

        public void Load(string catalogPath) {
            m_tracks = new List<Track>();
            XDocument doc = XDocument.Load(catalogPath);
            if (doc != null) {
                Deserialize(doc);
            }
        }

        public void Add(IEnumerable<string> trackFiles) {
            if (m_tracks == null) {
                m_tracks = new List<Track>();
            }

            foreach (string path in trackFiles) {
                string file = Path.GetFileNameWithoutExtension(path);
                Add(file);
            }
        }

        public void Save(string catalogPath) {
            XDocument doc = new XDocument();
            Serialize(doc);
            Directory.CreateDirectory(Path.GetDirectoryName(catalogPath));
            doc.Save(catalogPath);
        }

        #endregion // methods


        #region internal methods

        /// <summary>
        /// Track file을 읽어 Track 개체를 생성한다.
        /// </summary>
        /// <param name="fileName">경로와 확장자가 제외된 파일명.</param>
        private Track Add(string fileName) {
            DateTime date = new DateTime();
            if (Repository.ParseTrackFile(fileName, ref date)) {
                if (date.Year == m_year && date.Month == m_month) {
                    Track track = new Track();
                    track.VehicleId = m_vehicle.VehicleId;
                    track.CreateDate = date;
                    if (m_tracks != null) {
                        m_tracks.Add(track);
                    }
                    return track;
                }
            }
            return null;
        }

        private void Serialize(XDocument doc) {
            XElement root = new XElement(ROOT_ELEMENT);
            doc.Add(root);

            if (m_tracks != null) {
                foreach (Track track in m_tracks) {
                    XElement elt = new XElement(TRACK_ELEMENT);
                    elt.Add(new XAttribute(CREATED_ATTR, track.CreateDate.ToString(DATE_FORMAT)));

                    root.Add(elt);
                }
            }
        }

        private void Deserialize(XDocument doc) {
            XElement root = doc.Root;
            if (!ROOT_ELEMENT.Equals(root.Name.LocalName)) {
                throw new Exception("Xml is not a Track catalog file.");
            }

            IEnumerable<XElement> elts = root.Elements(TRACK_ELEMENT);
            foreach (XElement elt in elts) {
                Track track = new Track();
                var val = elt.Attribute(CREATED_ATTR);
                track.CreateDate = DateTime.ParseExact((string)val, DATE_FORMAT, null);

                m_tracks.Add(track);
            }
        }

        #endregion // internal methods
    }
}
