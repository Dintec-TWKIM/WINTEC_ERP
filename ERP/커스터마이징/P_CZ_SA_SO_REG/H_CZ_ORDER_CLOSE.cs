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
	public partial class H_CZ_ORDER_CLOSE : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		public string NO_SO { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_ORDER_CLOSE(string NO_SO)
		{
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			InitializeComponent();

			this.NO_SO = NO_SO;
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
			DataTable dt = Util.GetDB_CODE("CZ_SA00033", false);

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
			btn전자결재.Click += new EventHandler(btn전자결재_Click);
			btn복구.Click += new EventHandler(btn복구_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
		}


		protected override void InitPaint()
		{
			DataTable dtH = DBMgr.GetDataTable("PS_CZ_SA_SO_REG_CLOSE", new object[] { CD_COMPANY, NO_SO });
			
			string YN_CLOSE = dtH.Rows[0]["YN_CLOSE"].ToString();
			string TP_CLOSE = dtH.Rows[0]["TP_CLOSE"].ToString();
			string NO_DOCU = dtH.Rows[0]["NO_DOCU"].ToString();
			string ST_STAT = dtH.Rows[0]["ST_STAT"].ToString();

			// 결재상태
			DataTable dtCODE = Util.GetDB_CODE("FI_J000031", true);
			lbl결재.Text = dtCODE.Select("CODE = '" + ST_STAT + "'")[0]["NAME"].ToString();

			// 글자색상
			if (ST_STAT == "0") lbl결재.ForeColor = Color.Blue;
			else if (ST_STAT == "1") lbl결재.ForeColor = Color.Blue;
			else if (ST_STAT == "-1") lbl결재.ForeColor = Color.Red;
			else lbl결재.ForeColor = Color.Black;

			// 컨트롤 상태
			if (ST_STAT == "0" || ST_STAT == "1")
			{
				btn전자결재.Enabled = false;
				btn복구.Enabled = false;
				Util.SetCON_ReadOnly(pnl사아유, true);
				Util.SetCON_ReadOnly(pnl비고, true);
			}
			else
			{
				btn전자결재.Enabled = true;
				btn복구.Enabled = true;
				Util.SetCON_ReadOnly(pnl사아유, false);
				Util.SetCON_ReadOnly(pnl비고, false);
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

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			// 리포트 상에 표시할 통화 결정
			string currencyStr = "";

			if (CD_COMPANY != "S100")
				currencyStr = "원";
			else
				currencyStr = "$";

			string TP_CLOSE = "";
			string NM_CLOSE = "";

			// 사유
			foreach (Control con in pnl사유.Controls)
			{
				if (con is RadioButtonExt)
				{
					if (((RadioButtonExt)con).Checked)
					{
						TP_CLOSE = ((RadioButtonExt)con).Name;
						NM_CLOSE = ((RadioButtonExt)con).Text;
						break;
					}
				}
			}
			
			// 전자결재
			DataTable dtSO = DBMgr.GetDataTable("PS_CZ_SA_SO_REG_CLOSE", new object[] { CD_COMPANY, NO_SO });
			
			// 결재 상태 체크
			string NO_DOCU = dtSO.Rows[0]["NO_DOCU"].ToString();
			if (!GroupWare.CheckSTAT(NO_DOCU)) return;

			// 마감 상태 저장
			DBMgr.ExecuteNonQuery("PU_CZ_SA_SO_REG_CLOSE", new object[] { CD_COMPANY, NO_SO, "P", TP_CLOSE, txt비고.Text, NO_EMP });

			// html 만들기
			decimal AM_KR_S = Util.GetTO_Decimal(dtSO.Rows[0]["AM_KR_S"]);
			decimal AM_KR_P = Util.GetTO_Decimal(dtSO.Rows[0]["AM_KR_P"]);
			decimal AM_KR_C = Util.GetTO_Decimal(dtSO.Rows[0]["AM_KR_C"]);

			string html = @"
<div class='header'>
  1. 수주 정보
</div>
<table>
  <tr>
    <th>수주번호</th>
    <td>" + NO_SO + @"</td>
    <th>수주일자</th>
    <td>" + Util.GetTo_DateStringS(dtSO.Rows[0]["DT_SO"]) + @"</td>
  </tr>
  <tr>
    <th>매 출 처</th>
    <td>" + dtSO.Rows[0]["LN_PARTNER"] + @"</td>
    <th>선 &nbsp;&nbsp; 명</th>
    <td>" + dtSO.Rows[0]["NM_VESSEL"] + @"</td>
  </tr>
  <tr>
    <th>수주금액</th>
    <td class='money'>" + Util.GetTO_Money(AM_KR_S) + " " + currencyStr + @"</td>
    <th>발주금액</th>
    <td class='money'>" + Util.GetTO_Money(AM_KR_P) + " " + currencyStr + @"</td>
  </tr>
  <tr>
	<th>이 윤</th>
    <td class='money'>" + Util.GetTO_Money(AM_KR_S - AM_KR_P) + " " + currencyStr + @"</td>
    <th>이 윤 율</th>
    <td class='money'>" + string.Format("{0:#,##0.##}", (AM_KR_S == 0) ? 0 : 100 * (1 - AM_KR_P / AM_KR_S)) + @" %</td>
  </tr>
</table>
<br />
<div class='header'>
  2. 마감 내용
</div>
<table>
  <tr>
	<th>마감사유</th>
    <td>" + NM_CLOSE + @"</td>
    <th>마감일자</th>
    <td>" + Util.GetTo_DateStringS(Util.GetToday()) + @"</td>    
  </tr>
  <tr>
    <th>마감금액</th>
    <td class='money'>" + Util.GetTO_Money(AM_KR_C) + " " + currencyStr + @"</td>
    <th>마감아이템</th>
    <td class='money'>" + dtSO.Rows[0]["CNT_CLOSE"] + " / " + dtSO.Rows[0]["CNT_ITEM"] + @" 종</td>
  </tr>
  <tr>
    <th>비 &nbsp;&nbsp; 고</th>
    <td colspan='3' class='rmk'>" + txt비고.Text.Replace(" ", "&nbsp;").Replace("\n", "<br />") + @"</td>
  </tr>
</table>";

			// html 업데이트 및 전자결재 팝업
			string query = "UPDATE SA_SOH SET NO_DOCU = '@NO_DOCU' WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_SO = '" + NO_SO + "'";
			GroupWare.Save("수주 마감 보고서 - " + NO_SO, html, NO_DOCU, 1009, query, true);

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

			DBMgr.ExecuteNonQuery("PU_CZ_SA_SO_REG_CLOSE", new object[] { CD_COMPANY, NO_SO, "N", TP_CLOSE, txt비고.Text, NO_EMP });
			this.DialogResult = DialogResult.OK;
			
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}
