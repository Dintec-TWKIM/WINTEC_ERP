using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.Controls;

namespace cz
{
	public partial class H_CZ_QTN_CLOSE : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }
		
		public string NO_FILE { get; set; }
			
		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_QTN_CLOSE(string NO_FILE)
		{
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			InitializeComponent();

			this.NO_FILE = NO_FILE;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitEvent();
		}

		private void InitControl()
		{
			DataTable dt = Util.GetDB_CODE("CZ_SA00022", false);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				RadioButtonExt rdo = new RadioButtonExt();
				rdo.Name = dt.Rows[i]["CODE"].ToString();
				rdo.Text = dt.Rows[i]["NAME"].ToString();
				rdo.Location = new Point(20, i * 23);
				rdo.Size = new Size(200, 24);

				pnl사유.Controls.Add(rdo);
			}
		}

		private void InitEvent()
		{
			btn마감.Click += new EventHandler(btn마감_Click);
			btn복구.Click += new EventHandler(btn복구_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
		}
		

		protected override void InitPaint()
		{
			string query = @"
SELECT
	*
FROM CZ_SA_QTNH
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_FILE = '" + NO_FILE + "'";

			DataTable dtH = DBMgr.GetDataTable(query);
			string YN_CLOSE = dtH.Rows[0]["YN_CLOSE"].ToString();
			string TP_CLOSE = dtH.Rows[0]["TP_CLOSE"].ToString();

			// 버튼상태값
			if (YN_CLOSE == "Y")
			{
				btn마감.Enabled = false;
				btn복구.Enabled = true;
			}
			else
			{
				btn마감.Enabled = true;
				btn복구.Enabled = false;
			}

			// 마감사유
			foreach (Control con in pnl사유.Controls)
			{
				if (con is RadioButtonExt)
				{
					if (((RadioButtonExt)con).Name == TP_CLOSE)
					{
						((RadioButtonExt)con).Checked = true;
						break;
					}
				}
			}

			// 비고
			txt비고.Text = dtH.Rows[0]["DC_CLOSE"].ToString();
		}

		private void btn마감_Click(object sender, EventArgs e)
		{
			string TP_CLOSE = "";

			foreach (Control con in pnl사유.Controls)
			{
				if (con is RadioButtonExt)
				{
					if (((RadioButtonExt)con).Checked)
					{
						TP_CLOSE = ((RadioButtonExt)con).Name;
						break;
					}
				}
			}

			DBMgr.ExecuteNonQuery("SP_CZ_SA_QTNH_REG_UPDATE_CLOSE", new object[] { CD_COMPANY, NO_FILE, "Y", TP_CLOSE, txt비고.Text, NO_EMP });
			this.DialogResult = DialogResult.OK;
		}

		private void btn복구_Click(object sender, EventArgs e)
		{
			string TP_CLOSE = "";

			foreach (Control con in pnl사유.Controls)
			{
				if (con is RadioButtonExt)
				{
					if (((RadioButtonExt)con).Checked)
					{
						TP_CLOSE = ((RadioButtonExt)con).Name;
						break;
					}
				}
			}

			DBMgr.ExecuteNonQuery("SP_CZ_SA_QTNH_REG_UPDATE_CLOSE", new object[] { CD_COMPANY, NO_FILE, "N", TP_CLOSE, txt비고.Text, NO_EMP });
			this.DialogResult = DialogResult.OK;
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}
