using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_BI_WEB_BROWSER : PageBase
    {
        public P_CZ_BI_WEB_BROWSER()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            webBrowserExt1.IsWebBrowserContextMenuEnabled = false;
            webBrowserExt1.ScriptErrorsSuppressed = true;
            webBrowserExt1.AllowWebBrowserDrop = false;

            if (this.PageID == "P_CZ_BI_SO_GOAL_RPT")
            {
                webBrowserExt1.Navigate("https://app.powerbi.com/view?r=eyJrIjoiODNjODc4ZmQtNDIxNC00MTk2LTllMjctMmVjNjQ3YTAxMjM3IiwidCI6IjA5YWEyYTU3LWY0ZjktNGU0Ni05YzJiLTllODkzYzJlYzI3MiIsImMiOjEwfQ%3D%3D");
            }
        }
    }
}
