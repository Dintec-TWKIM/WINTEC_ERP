using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Forms.Help.Forms;
using Duzon.ERPU;
using Dintec;

namespace cz
{
	public partial class H_CZ_STATUS_TABLE : Duzon.Common.Forms.CommonDialog
	{
		string companyCode;
		string empNumber;
		string orderNumber;
		string headHtml;
		string bodyHtml;

		#region ==================================================================================================== Constructor

		public H_CZ_STATUS_TABLE(string orderNumber)
		{			
			InitializeComponent();
			this.companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			this.empNumber = Global.MainFrame.LoginInfo.UserID;
			this.orderNumber = orderNumber;
			this.headHtml = "";
			this.bodyHtml = "";
		}

		#endregion

		protected override void InitLoad()
		{
			InitEvent();
		}

		private void InitControl()
		{
		}

		private void InitGrid()
		{
			
		}

		private void InitEvent()
		{
			btn전자결재.Click += new EventHandler(btn전자결재_Click);
		}

		protected override void InitPaint()
		{
			Search();
		}

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			string query = "";
			string docuNumber = "";

			// 결재 상태 체크
			query = "SELECT NO_DOCU_STATUS FROM SA_SOH WITH(NOLOCK) WHERE CD_COMPANY = '" + companyCode + "' AND NO_SO = '" + orderNumber + "'";
			DataTable dt = DBMgr.GetDataTable(query);
			docuNumber = dt.Rows[0][0].ToString();

			if (!GroupWare.CheckSTAT(docuNumber))
			{
				return;
			}

			// 비고 저장
			query = @"
UPDATE SA_SOH SET
	  DC_STATUS		= @DC_STATUS
	, ID_UPDATE		= @ID_USER
	, DTS_UPDATE	= NEOE.SF_SYSDATE(GETDATE())
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_SO = @NO_SO";

			DBMgr dbm = new DBMgr();
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", companyCode);
			dbm.AddParameter("@NO_SO", orderNumber);
			dbm.AddParameter("@DC_STATUS", txt비고.Text);
			dbm.AddParameter("@ID_USER", empNumber);
			dbm.ExecuteNonQuery();

			// html을 비고 포함하도록 수정
			string remark = txt비고.Text;
			remark = remark.Replace(" ", "&nbsp;");	// 키보드 스페이스바
			remark = remark.Replace(" ", "&nbsp;");	// 특수문자 스페이스바
			remark = remark.Replace("\r\n", "<br />");

			string remarkHtml = @"
  <div class='category'>
    ▣ 비고
  </div>

  <div class='remark'>
" + remark + @"
  </div>";

			bodyHtml = bodyHtml.Replace("<!-- 비고 -->", remarkHtml);

			// 문서번호 저장 및 전자결재 팝업
			query = "UPDATE SA_SOH SET NO_DOCU_STATUS = '@NO_DOCU' WHERE CD_COMPANY = '" + companyCode + "' AND NO_SO = '" + orderNumber + "'";
			GroupWare.Save("수주현황표 - " + orderNumber, bodyHtml, docuNumber, 1017, query, true);

			this.DialogResult = DialogResult.OK;
		}

		#region ==================================================================================================== Constructor

		private void Search()
		{
			string myCurrency = (companyCode != "S100") ? "￦" : "＄";
			DataSet ds = DBMgr.GetDataSet("PS_CZ_SA_SO_STATUS_TABLE", companyCode, orderNumber);
			DataTable dtHead = ds.Tables[0];
			DataTable dtLine = ds.Tables[1];

			// ***** 결재상태
			string gwStatus = dtHead.Rows[0]["ST_STAT"].ToString();

			DataTable dtCode = GetDb.Code("FI_J000031");
			lbl결재.Text = dtCode.Select("CODE = '" + gwStatus + "'")[0]["NAME"].ToString();

			// 글자색상
			if (gwStatus == "0")
				lbl결재.ForeColor = Color.Blue;
			else if (gwStatus == "1")
				lbl결재.ForeColor = Color.Blue;
			else if (gwStatus == "-1")
				lbl결재.ForeColor = Color.Red;
			else
				lbl결재.ForeColor = Color.Black;

			// 컨트롤 상태
			if (gwStatus == "0" || gwStatus == "1")
			{
				btn전자결재.Enabled = false;
				SetCon.Enabled(bpPanelControl1, false);
			}
			else
			{
				btn전자결재.Enabled = true;
				SetCon.Enabled(bpPanelControl1, true);
			}

			// ***** head
			headHtml = @"
<style type='text/css'>
	.erpContents { padding:8px; width:" + (webBrowser1.Width - 17) + @"px; }
	.erpContents div,
	.erpContents th,
	.erpContents td { font-family:맑은 고딕; }
	.erpContents table { width:100%; }
	.erpContents th { height:30px; padding:0px 3px; border:solid 1px #000000; background:#f6f6f6; }
	.erpContents td { height:30px; padding:2px 3px; border:solid 1px #000000; line-height:13px; }

	.erpContents .category { padding:10px 0 5px 0; }

	.erpContents .statusTable1 .col1 { width:15%; text-align:center; }
	.erpContents .statusTable1 .col2 { width:45%; }
	.erpContents .statusTable1 .col3 { width:28%; }
	.erpContents .statusTable1 .col4 { width:12%; text-align:center; }

    .erpContents .statusTable2 .col1 { width:25px; text-align:center; }
	.erpContents .statusTable2 .col2 { width:80px; text-align:right; }
	.erpContents .statusTable2 .col3 { width:38px; text-align:right; }
	.erpContents .statusTable2 .col4 { width:80px; text-align:right; }
	.erpContents .statusTable2 .col5 { width:135px; }
	.erpContents .statusTable2 .col6 { width:80px; text-align:right; }
	.erpContents .statusTable2 .col7 { width:80px; text-align:right; }
	.erpContents .statusTable2 .col8 { width:38px; text-align:right; }

    .erpContents .statusTable3 .col1 { width:30%; text-align:right; }
	.erpContents .statusTable3 .col2 { width:30%; text-align:right; }
	.erpContents .statusTable3 .col3 { width:30%; text-align:right; }
	.erpContents .statusTable3 .col4 { width:10%; text-align:right; }

    .erpContents .statusTable4 .col1 { width:230px; }
	.erpContents .statusTable4 .col2 { width:100px; text-align:right; }
	.erpContents .statusTable4 .col3 { width:100px; text-align:right; }
	.erpContents .statusTable4 .col4 { width:45px; text-align:right; }
</style>
";
			// ***** body
			bodyHtml = @"
  <div class='category'>
    ▣ 매출처 정보
  </div>

  <div>
    <table class='statusTable1'>
      <tr>
        <th>File No.</th>
        <th>매출처</th>
        <th>호선</th>
        <th>환율</th>
      </tr>
      <tr>
        <td class='col1'>" + orderNumber + @"</td>
        <td class='col2'>" + dtHead.Rows[0]["LN_PARTNER"] + @"</td>
        <td class='col3'>" + dtHead.Rows[0]["NO_HULL"] + ", " + dtHead.Rows[0]["NM_VESSEL"] + @"</td>
        <td class='col4'>" + dtHead.Rows[0]["NM_EXCH"] + " " + string.Format("{0:#,##0.##}", dtHead.Rows[0]["RT_EXCH"]) + @"</td>
      </tr>
    </table>
  </div>

  <div class='category'>
    ▣ 견적 정보
  </div>

  <div>
    <table class='statusTable2'>
      <tr>
        <th rowspan='2'>구분</th>
        <th colspan='3'>견적</th>
        <th colspan='2'>매입</th>
        <th colspan='2'>이윤</th>
      </tr>
      <tr>
        <th>견적가</th>
        <th>D/C율</th>
        <th>D/C후</th>
        <th>매입처</th>
        <th>매입가</th>
  	    <th>금액</th>
        <th>율</th>
      </tr>
      <tr>
        <td class='col1'>" + dtHead.Rows[0]["GRP_ITEM"] + @"</td>
        <td class='col2'>" + dtHead.Rows[0]["NM_EXCH"] + " " + string.Format("{0:#,##0.##}", dtHead.Rows[0]["AM_EX_Q"]) + @"</td>
        <td class='col3'>" + string.Format("{0:#,##0.##}", dtHead.Rows[0]["RT_DC"]) + @"%</td>
        <td class='col4'>" + dtHead.Rows[0]["NM_EXCH"] + " " + string.Format("{0:#,##0.##}", dtHead.Rows[0]["AM_EX_S"]) + @"</td>
        <td class='col5'>" + dtLine.Rows[0]["LN_PARTNER"] + (dtLine.Rows.Count == 1 ? "" : " 외 " + (dtLine.Rows.Count - 1)) + @"</td>
        <td class='col6'>" + string.Format(myCurrency + " {0:#,##0.##}", dtHead.Rows[0]["AM_KR_P"]) + @"</td>
        <td class='col7'>" + string.Format(myCurrency + " {0:#,##0.##}", dtHead.Rows[0]["AM_MARGIN"]) + @"</td>
        <td class='col8'>" + string.Format("{0:#,##0.##}", dtHead.Rows[0]["RT_MARGIN"]) + @"%</td>
      </tr>
    </table>
  </div>
";

			// 부대비용 포함한 이윤 (있을 경우만)
			if (GetTo.Decimal(dtHead.Rows[0]["AM_KR_S_EXTRA"]) != 0 || GetTo.Decimal(dtHead.Rows[0]["AM_KR_P_EXTRA"]) != 0)
			{
				bodyHtml += @"
  <div class='category'>
    ▣ 부대 비용
  </div>

  <div>
    <table class='statusTable3'>
      <tr>
        <th>매출 부대비용</th>
        <th>매입 부대비용</th>
        <th>비용 포함 이윤</th>
        <th>최종 이윤율</th>
      </tr>
      <tr>
        <td class='col1'>" + string.Format(myCurrency + " {0:#,##0.##}", dtHead.Rows[0]["AM_KR_S_EXTRA"]) + @"</td>
        <td class='col2'>" + string.Format(myCurrency + " {0:#,##0.##}", dtHead.Rows[0]["AM_KR_P_EXTRA"]) + @"</td>
        <td class='col3'>" + string.Format(myCurrency + " {0:#,##0.##}", dtHead.Rows[0]["AM_MARGIN_ALL"]) + @"</td>
        <td class='col4'>" + string.Format(" {0:#,##0.##}", dtHead.Rows[0]["RT_MARGIN_ALL"]) + @"%</td>
      </tr>
    </table>
  </div>";
			}

			// 매입처 복수일 경우
			if (dtLine.Rows.Count > 1)
			{
				bodyHtml += @"
  <div class='category'>
    ▣ 상세 내역
  </div>
  
  <div>
    <table class='statusTable4'>
      <tr>
        <th>매입처</th>
        <th>매입가</th>
        <th>매출가</th>
        <th>이윤율</th>
      </tr>";

				for (int i = 0; i < dtLine.Rows.Count; i++)
				{
					bodyHtml += @"
      <tr>
        <td class='col1'>" + string.Format("{0:#,##0.##}", dtLine.Rows[i]["LN_PARTNER"]) + @"</td>
        <td class='col2'>" + string.Format(myCurrency + "{0:#,##0.##}", dtLine.Rows[i]["AM_KR_P"]) + @"</td>
        <td class='col3'>" + string.Format(myCurrency + "{0:#,##0.##}", dtLine.Rows[i]["AM_KR_S"]) + @"</td>
        <td class='col4'>" + string.Format("{0:#,##0.##}", dtLine.Rows[i]["RT_MARGIN"]) + @"%</td>
      </tr>";
				}

				bodyHtml += @"
    </table>
  </div>";
			}

			// 최종 전체를 감싸는 DIV 만들기
			bodyHtml = @"
<div class='erpContents'>
" + bodyHtml + @"
  <!-- 비고 -->
</div>";

			webBrowser1.DocumentText = Html.MakeHtml(headHtml, bodyHtml);

			// 비고
			txt비고.Text = dtHead.Rows[0]["DC_STATUS"].ToString();
		}

		#endregion
	}
}
