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
using DX;

namespace cz
{
	public partial class H_CZ_PO_SEARCH : Duzon.Common.Forms.CommonDialog
	{
		string CompanyCode;

		public FlexGrid List
		{
			get
			{
				return grdList;
			}
		}

		public H_CZ_PO_SEARCH(string fileNumber)
		{
			InitializeComponent();
			CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
			tbx번호.Text = fileNumber;
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			// 콤보 바인딩
			DataTable dt = new DataTable();
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("@NO_SO", "파일번호");
			dt.Rows.Add("@NO_PO", "발주번호");
			dt.Rows.Add("@NO_PO", "재고발주번호");

			cbo번호.DataBind(dt, false);
			cbo번호.SelectedIndex = 0;			
		}

		private void InitGrid()
		{			
			grdList.BeginSetting(1, 1, false);

			grdList.SetCol("NO_SO"			, "파일번호"	, 85);
			grdList.SetCol("NO_PO"			, "발주번호"	, 100);
			grdList.SetCol("NO_LINE"		, "항번"		, false);
			grdList.SetCol("NO_DSP"			, "순번"		, 40);
			grdList.SetCol("CD_ITEM_PARTNER", "품목코드"	, 130);
			grdList.SetCol("NM_ITEM_PARTNER", "품목명"	, 250);
			grdList.SetCol("QT_PO"			, "발주"		, 50	, false, typeof(decimal), FormatTpType.QUANTITY);
			grdList.SetCol("QT_GR"			, "(입고)"	, 50	, false, typeof(decimal), FormatTpType.QUANTITY);
			grdList.SetCol("UM"				, "단가"		, 82	, false, typeof(decimal), FormatTpType.MONEY);

			grdList.Cols["NO_SO"].TextAlign = TextAlignEnum.CenterCenter;
			grdList.Cols["NO_DSP"].Format = "####.##";
			grdList.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;

			grdList.SetDefault("18.09.27.12", SumPositionEnum.None);

			grdList.Styles.Add("INACTIVE").ForeColor = Color.LightGray;
		}

		private void InitEvent()
		{
			btn조회.Click += new EventHandler(btn조회_Click);
			btn확정.Click += new EventHandler(btn확정_Click);
			btn취소.Click += new EventHandler(btn취소_Click);

			tbx번호.KeyDown += new KeyEventHandler(tbx번호_KeyDown);

			grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
		}

		

		protected override void InitPaint()
		{
			btn조회_Click(null, null);
			tbx포커스.Left = -1000;
			tbx포커스.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void tbx번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grdList_DoubleClick(object sender, EventArgs e)
		{
			int row = grdList.MouseRow;
			int col = grdList.MouseCol;

			// 버리는 컬럼 이벤트
			if (col <= 0)
				return;

			// 헤더 클릭
			if (row < grdList.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 아이템 클릭
			if (row >= grdList.Rows.Fixed)
				btn확정_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Search

		private void btn조회_Click(object sender, EventArgs e)
		{
			if (tbx번호.Text.Trim() == "")
			{
				Global.MainFrame.ShowMessage("");
				return;
			}

			if (cbo번호.글() == "재고발주번호")
			{
				string query = @"


SELECT
		A.NO_FILE
	, A.NO_PO
	, A.NO_ORDER
	, B.NO_LINE
	, C.NO_DSP
	, CASE WHEN A.YN_STOCK = 'Y' THEN B.DC1 ELSE C.CD_ITEM_PARTNER END	AS CD_ITEM_PARTNER
	, CASE WHEN A.YN_STOCK = 'Y' THEN B.DC2 ELSE C.NM_ITEM_PARTNER END	AS NM_ITEM_PARTNER
	, A.YN_STOCK
	, C.CD_SPEC		AS UCODE
	, B.QT_PO

, B.UM
, B.CD_ITEM
FROM	  CZ_PU_POH_EXT	AS A
JOIN	  PU_POL		AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_PO = B.NO_PO
LEFT JOIN CZ_SA_QTNL	AS C ON B.CD_COMPANY = C.CD_COMPANY AND B.CD_PJT = C.NO_FILE AND B.NO_LINE = C.NO_LINE
WHERE 1 = 1
	AND A.CD_COMPANY = 'K100'
	AND A.NO_PO = '" + tbx번호.Text + @"'";


				DataTable dt = TSQL.결과(query);
				grdList.DataBind(dt);
				SetGridStyle();
			}
			else
			{
				DBMgr dbm = new DBMgr();
				dbm.Procedure = "PS_CZ_PU_PO_RPT_L";
				//dbm.DebugMode = DebugMode.Popup;
				dbm.AddParameter("@CD_COMPANY", CompanyCode);
				dbm.AddParameter(cbo번호.GetValue(), tbx번호.Text);

				DataTable dt = dbm.GetDataTable();
				dt.컬럼추가("YN_STOCK", typeof(string), "N");
				grdList.DataBind(dt);
				SetGridStyle();
			}
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn확정_Click(object sender, EventArgs e)
		{
			if (grdList.Rows[grdList.Row].Style != null && grdList.Rows[grdList.Row].Style.Name == "INACTIVE")
			{
				Global.MainFrame.ShowMessage("이미 입고되었습니다.");
				return;
			}

			//DataTable dtL = flexL.GetCheckedRows("CHK");
			//if (dtL == null || dtL.Rows.Count == 0) { Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다); return; }

			//foreach (DataRow row in dtL.Rows) row["YN_EXT"] = "Y";
			//string xmlL = Util.GetTO_Xml(dtL);

			//try
			//{
			//    DBMgr.ExecuteNonQuery("PX_CZ_PU_QTN_REG_CONTRACT_UM", new object[] { xmlL });
			//    Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			//    this.DialogResult = DialogResult.OK;
			//}
			//catch (Exception ex)
			//{
			//    Global.MainFrame.ShowMessage(Util.GetErrorMessage(ex.Message));
			//}
			this.DialogResult = DialogResult.OK;
		}

		#endregion

		#region ==================================================================================================== 취소

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		private void SetGridStyle()
		{
			grdList.Redraw = false;

			for (int i = grdList.Rows.Fixed; i < grdList.Rows.Count; i++)
			{
				//if (GetTo.Decimal(grdList[i, "QT_PO"]) > GetTo.Decimal(grdList[i, "QT_GR"]))
					grdList.Rows[i].Style = null;
				//else
				//    grdList.Rows[i].Style = grdList.Styles["INACTIVE"];
			
			}

			grdList.Redraw = true;
		}
	}
}
