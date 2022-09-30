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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace HadiWebBrowser
{
    
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser browser;
        public void InitChromiumFrame()
        {
            CefSettings settings = new CefSettings();
            // Init Chromium Embedded Framework with settings loaded
            Cef.Initialize(settings);
            // Create a browser object
            browser = new ChromiumWebBrowser("https://google.com");
            // Add it to Form1 and fill it to the window
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }
        public Form1()
        {
            InitializeComponent();
            InitChromiumFrame();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
