using Duzon.Common.Forms;
using Duzon.DASS.Erpu.Windows.FX;
using System.IO;
using System;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_CRM_CHART_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_SA_CRM_CHART_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_SA_CRM_CHART_SUB(UChart chart)
        {
            InitializeComponent();

            this.Controls.Add(chart);
        }
    }
}
