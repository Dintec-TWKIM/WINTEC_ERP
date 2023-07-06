using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_LOG_CHARGE_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_SA_LOG_CHARGE_SUB()
        {
            InitializeComponent();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn포장비계산.Click += Btn포장비계산_Click;
        }

        private void Btn포장비계산_Click(object sender, EventArgs e)
        {
            string query;
            DataTable dt;

            try
            {
                this.curCBM.DecimalValue = (this.cur가로.DecimalValue * (decimal)0.001) * 
                                           (this.cur세로.DecimalValue * (decimal)0.001) * 
                                           (this.cur높이.DecimalValue * (decimal)0.001);
                
                query = @"SELECT TL.UM 
	  				  	  FROM CZ_MA_TARIFF_CBM_H TH WITH(NOLOCK)
						  JOIN CZ_MA_TARIFF_CBM_L TL WITH(NOLOCK) ON TL.CD_COMPANY = TH.CD_COMPANY AND TL.CD_PARTNER = TH.CD_PARTNER
	  				  	  WHERE TH.CD_COMPANY = '{0}'
						  AND ISNULL(TH.YN_USE, 'N') = 'Y'
	  				  	  AND TL.SIZE = (CASE WHEN {1} > 1 THEN 1 ELSE {1} END)
						  AND CONVERT(NVARCHAR(8), GETDATE(), 112) BETWEEN TL.DT_START AND TL.DT_END";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.curCBM.DecimalValue));
                
                if (dt != null && dt.Rows.Count > 0)
                    this.cur금액.DecimalValue = (this.curCBM.DecimalValue >= 1 ? Convert.ToDecimal(dt.Rows[0]["UM"]) * this.curCBM.DecimalValue : Convert.ToDecimal(dt.Rows[0]["UM"]));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
