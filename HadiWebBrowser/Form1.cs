/*
 HadiWebBrowser
 * Uses CEFSharp
 * Might or might not be Chromium-based (idk if cef counts)
 * Licensed under MIT License!!
 * Copyright Abdulhadi5692HDI2
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace HadiWebBrowser
{
    public class BrowserControls
    {
        // Local instance of chromium and Form1
        private static ChromiumWebBrowser _ilbrowser = null;
        private static Form1 _iMainForm = null;
        #pragma warning disable IDE0044 // Add readonly modifier
        private Control DevTools = null;
        #pragma warning restore IDE0044 // Add readonly modifier
        private String ControlName = "DockedDevtool";
        DockStyle dockStyle = DockStyle.Fill;

        public BrowserControls(ChromiumWebBrowser webBrowser, Form1 MainForm)
        {
            _ilbrowser = webBrowser;
            _iMainForm = MainForm;
        }
        public void openDevTools()
        {
            _ilbrowser.ShowDevTools();
        }
        public void openDevToolsDocked()
        {
            
            _ilbrowser.ShowDevToolsDocked(DevTools, ControlName, dockStyle, 0, 0);
        }
        public void CmdHelloWorld()
        {
            ProcessStartInfo cmd_helloworld = new ProcessStartInfo("cmd.exe", "/c echo Hello World! && pause");
            Process.Start(cmd_helloworld);
        }
    }
    
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser browser;
        public void InitChromiumFrame()
        {
            CefSettings settings = new CefSettings();
            // Make a string called "page" to load into ChromiumWebBrowser();
            String page = string.Format(@"{0}\browser-gui\index.html", Application.StartupPath);

            if (!File.Exists(page))
            {
                MessageBox.Show("File Error! Could not open: ", page);
            }
            // Init Chromium Embedded Framework with settings loaded
            Cef.Initialize(settings);
            // Create a browser object
            browser = new ChromiumWebBrowser(page);
            // Add it to Form1 and fill it to the window
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            // Allow use of local files (enables file:// protocol)
            BrowserSettings bSettings = new BrowserSettings();
            //bSettings.FileAccessFromFileUrls = CefState.Enabled;
            //bSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            browser.BrowserSettings = bSettings;
        }
        public void OpenProg() { ProcessStartInfo ifi = new ProcessStartInfo("cmd.exe", "/c chdir && pause");
            Process.Start(ifi);
        }

        public Form1()
        {
            InitializeComponent();
            InitChromiumFrame();
            CefSharpSettings.WcfEnabled = true;
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.Register("browser_native", new BrowserControls(browser, this), isAsync:false);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
