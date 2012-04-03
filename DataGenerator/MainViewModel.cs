////////////////////////////////////////////////////////////////////////////////
// MainViewModel.cs
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
using Viewer.Common;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Util;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace DataGenerator {

    public class MainViewModel : NotificationObjectEx {

        #region fields

        private ObservableCollection<string> m_files;
        
        #endregion // fields


        #region constructors

        public MainViewModel() {
            m_files = new ObservableCollection<string>();
            SelectFolderCommand = new DelegateCommand(DoSelectFolder);
            GenerateCommand = new DelegateCommand(DoGenerate);
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// 시작 일시
        /// </summary>
        public DateTime StartTime {
            get { return m_startTime; }
            set {
                if (value != m_startTime) {
                    m_startTime = value;
                    RaisePropertyChanged(() => StartTime);
                }
            }
        }
        private DateTime m_startTime = new DateTime(2012, 4, 1, 10, 0, 0);

        /// <summary>
        /// 종료 일시
        /// </summary>
        public DateTime EndTime {
            get { return m_endTime; }
            set {
                if (value != m_endTime) {
                    m_endTime = value;
                    RaisePropertyChanged(() => EndTime);
                }
            }
        }
        private DateTime m_endTime = new DateTime(2012, 4, 1, 20, 59, 0);

        /// <summary>
        /// 시작 위도
        /// </summary>
        public double StartLattitude {
            get { return m_startLattitude; }
            set {
                if (value != m_startLattitude) {
                    m_startLattitude = value;
                    RaisePropertyChanged(() => StartLattitude);
                }
            }
        }
        private double m_startLattitude = 37.380039;

        /// <summary>
        /// 시작 경도
        /// </summary>
        public double StartLongitude {
            get { return m_startLongitude; }
            set {
                if (value != m_startLongitude) {
                    m_startLongitude = value;
                    RaisePropertyChanged(() => StartLongitude);
                }
            }
        }
        private double m_startLongitude = 127.115263;

        /// <summary>
        /// 시작 위도
        /// </summary>
        public double EndLattitude {
            get { return m_endLattitude; }
            set {
                if (value != m_endLattitude) {
                    m_endLattitude = value;
                    RaisePropertyChanged(() => EndLattitude);
                }
            }
        }
        private double m_endLattitude = 37.390039;

        /// <summary>
        /// 시작 경도
        /// </summary>
        public double EndLongitude {
            get { return m_endLongitude; }
            set {
                if (value != m_endLongitude) {
                    m_endLongitude = value;
                    RaisePropertyChanged(() => EndLongitude);
                }
            }
        }
        private double m_endLongitude = 127.125263;

        /// <summary>
        /// 최저 속도
        /// </summary>
        public int LowVelocity {
            get { return m_lowVelocity; }
            set {
                if (value != m_lowVelocity) {
                    m_lowVelocity = value;
                    RaisePropertyChanged(() => LowVelocity);
                }
            }
        }
        private int m_lowVelocity = 30;

        /// <summary>
        /// 최저 속도
        /// </summary>
        public int HighVelocity {
            get { return m_highVelocity; }
            set {
                if (value != m_highVelocity) {
                    m_highVelocity = value;
                    RaisePropertyChanged(() => HighVelocity);
                }
            }
        }
        private int m_highVelocity = 130;

        /// <summary>
        /// 최저 가속도
        /// </summary>
        public int LowAccelate {
            get { return m_lowAccelate; }
            set {
                if (value != m_lowAccelate) {
                    m_lowAccelate = value;
                    RaisePropertyChanged(() => LowAccelate);
                }
            }
        }
        private int m_lowAccelate = -10;

        /// <summary>
        /// 최저 가속도
        /// </summary>
        public int HighAccelate {
            get { return m_highAccelate; }
            set {
                if (value != m_highAccelate) {
                    m_highAccelate = value;
                    RaisePropertyChanged(() => HighAccelate);
                }
            }
        }
        private int m_highAccelate = 10;

        /// <summary>
        /// 생성 폴더
        /// </summary>
        public string SelectedFolder {
            get { return m_selectedFolder; }
            set {
                if (!string.Equals(value, SelectedFolder)) {
                    m_selectedFolder = value;
                    RaisePropertyChanged(() => SelectedFolder);
                }
            }
        }
        private string m_selectedFolder = @"x:\gfdata";

        /// <summary>
        /// 폴더 내의 기존 파일들 삭제.
        /// </summary>
        public bool DeleteFiles {
            get { return m_deleteFiles; }
            set {
                if (value != m_deleteFiles) {
                    m_deleteFiles = value;
                    RaisePropertyChanged(() => DeleteFiles);
                }
            }
        }
        private bool m_deleteFiles = true;

        /// <summary>
        /// 생성 파일들.
        /// </summary>
        public ObservableCollection<string> Files {
            get { return m_files; }
        }

        /// <summary>
        /// 생성 파일 총 개수
        /// </summary>
        public int TotalCount {
            get { return m_totalCount; }
            set {
                if (value != m_totalCount) {
                    m_totalCount = value;
                    RaisePropertyChanged(() => TotalCount);
                }
            }
        }
        private int m_totalCount;

        /// <summary>
        /// 현재 생성한 파일 개수
        /// </summary>
        public int Current {
            get { return m_current; }
            set {
                if (value != m_current) {
                    m_current = value;
                    RaisePropertyChanged(() => Current);
                }
            }
        }
        private int m_current;

        /// <summary>
        /// 폴더 선택 커맨드
        /// </summary>
        public ICommand SelectFolderCommand {
            get;
            private set;
        }

        /// <summary>
        /// 생성 커맨드
        /// </summary>
        public ICommand GenerateCommand {
            get;
            private set;
        }

        #endregion // properties


        #region internal methods

        private void DoSelectFolder() {
            SelectedFolder = DialogUtil.SelectFolder("파일들을 생성할 위치를 선택하세요.", SelectedFolder);
        }

        private void DoGenerate() {
            if (!Directory.Exists(SelectedFolder)) {
                MessageUtil.Show("파일들을 생성할 폴더가 존재하지 않습니다.");
                return;
            }

            // 기존 파일들 삭제
            if (DeleteFiles) {
                DirectoryInfo dir = new DirectoryInfo(SelectedFolder);
                foreach (FileInfo file in dir.GetFiles()) {
                    file.Delete();
                }
            }

            TimeSpan duration = EndTime - StartTime;
            TotalCount = (int)duration.TotalMinutes + 1;
            Current = 0;
            m_files.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) => {
                // 파일 생성
                DateTime t = StartTime;
                while (t <= EndTime) {
                    App.Current.Dispatcher.Invoke((Action)(() => {
                        CreateFile(t);
                        Current++;
                        t = t.AddMinutes(1);
                        Thread.Sleep(20);
                    }));
                    worker.ReportProgress(Current);
                }
            });
            worker.ProgressChanged += (sender, e) => { };
            worker.RunWorkerCompleted += (sender, e) => {
                MessageUtil.Show("완료됐습니다.");
            };
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void CreateFile(DateTime t) {
            string filename = t.ToString("yyyy_MM_dd_hh_mm_ss");
            filename = "all_" + filename;
            CreateIncFile(t, Path.Combine(SelectedFolder, filename + ".inc"));
            CreateLogFile(t, Path.Combine(SelectedFolder, filename + ".log"));
            CreateMovieFile(Path.Combine(SelectedFolder, filename + ".264"));
            m_files.Add(filename);
        }

        private void CreateIncFile(DateTime t, string filePath) {
            
        }

        private void CreateLogFile(DateTime t, string filePath) {
            File.WriteAllText(filePath, "xxx");
        }

        private void CreateMovieFile(string filePath) {
            string templMovie = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"templates\sample01.264");
            File.Copy(templMovie, filePath);
        }

        #endregion // internal methods
    }
}
