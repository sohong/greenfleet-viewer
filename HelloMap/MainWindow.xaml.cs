using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;

namespace HelloMap {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            MapScriptHelper helper = new MapScriptHelper();
            browser.ObjectForScripting = helper;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
            browser.Navigate(path);
        }

        private void btnTest_Click(object sender, RoutedEventArgs e) {
            browser.InvokeScript("addMarker", 111, 37.390039, 127.115263);
        }
    }
}
