using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_MA_HTML_VIEWER : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_HTML_VIEWER(string title, string html)
        {
            InitializeComponent();
            this.TitleText = title;

            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.DocumentText = html;
        }

        public P_CZ_MA_HTML_VIEWER(string url)
        {
            InitializeComponent();

            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Navigate(url);
        }
    }
}
