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

namespace cz
{
	public partial class P_CZ_PU_QTN_HGS : Duzon.Common.Forms.CommonDialog
	{
		DataTable QtnTable;
		DataTable HgsTable;

		#region ===================================================================================================== Property

		public DataTable Result
		{
			get
			{
				return grd목록.GetCheckedRows("CHK");
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_QTN_HGS(DataTable qtnTable, DataTable hgsTable)
		{
			InitializeComponent();
			QtnTable = qtnTable;
			HgsTable = hgsTable;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			
		}

		private void InitGrid()
		{			
			grd목록.BeginSetting(1, 1, false);
						
			grd목록.SetCol("CHK"				, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			grd목록.SetCol("NO_LINE"			, "항번"			, false);
			grd목록.SetCol("NO_DSP"			, "순번"			, 40);
			grd목록.SetCol("CD_ITEM_PARTNER", "품목코드"		, 100	, true);
			grd목록.SetCol("NM_ITEM_PARTNER", "품목명"		, 200);
			grd목록.SetCol("ARROW"			, ""			, 30);
			grd목록.SetCol("NO_PLATE"		, "품목코드(HGS)"	, 100);
			grd목록.SetCol("NM_PLATE"		, "품목명(HGS)"	, 200);
			grd목록.SetCol("CD_ITEM"			, "재고코드"		, 80);
			grd목록.SetCol("NM_ITEM"			, "재고명"		, 200);
			grd목록.SetCol("UCODE"			, "U코드"		, false);
			grd목록.SetCol("UM"				, "단가"			, 82	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd목록.SetCol("LT"				, "납기"			, 40	, false	, typeof(decimal), FormatTpType.MONEY);

			grd목록.Cols["NO_DSP"].Format = "####.##";
			grd목록.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd목록.Cols["ARROW"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd목록.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			
			grd목록.SetDefault("19.01.03.01", SumPositionEnum.None);
			grd목록.SetEditColumn("CD_ITEM_PARTNER");
		}

		private void InitEvent()
		{
			btn조회.Click += new EventHandler(btn조회_Click);
			btn확정.Click += new EventHandler(btn확정_Click);
			btn취소.Click += new EventHandler(btn취소_Click);

			grd목록.DoubleClick += new EventHandler(grdList_DoubleClick);
		}

		protected override void InitPaint()
		{			
			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Procedure = "PS_CZ_PU_QTN_PARSING_HGS_SUB";
			dbm.AddParameter("@XML_QTN", GetTo.Xml(QtnTable));
			dbm.AddParameter("@XML_HGS", GetTo.Xml(HgsTable));
			
			DataTable dt = dbm.GetDataTable();
			grd목록.DataBind(dt);
			SetGridStyle();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grdList_DoubleClick(object sender, EventArgs e)
		{
			int row = grd목록.MouseRow;
			int col = grd목록.MouseCol;

			// 버리는 컬럼 이벤트
			if (!grd목록.HasNormalRow || col <= 0)
				return;

			// 헤더클릭
			if (row < grd목록.Rows.Fixed)
				SetGridStyle();
		}

		private void SetGridStyle()
		{
			for (int i = grd목록.Rows.Fixed; i < grd목록.Rows.Count; i++)
			{
				if (grd목록[i, "NO_LINE"].ToString() != "" && grd목록[i, "NO_PLATE"].ToString() != "")
					grd목록.SetCellImage(i, grd목록.Cols["ARROW"].Index, Icons.Arrow_16x16);
				else
					grd목록.SetCellImage(i, grd목록.Cols["ARROW"].Index, null);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		private void btn조회_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == Keys.Control ? DebugMode.Popup : DebugMode.None;

			string qtnXml = GetTo.Xml(GetTo.DataTable(grd목록.DataTable, "NO_LINE", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER"), "NO_LINE IS NOT NULL");
			string hgsXml = GetTo.Xml(HgsTable);

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = debugMode;
			dbm.Procedure = "PS_CZ_PU_QTN_PARSING_HGS_SUB";
			dbm.AddParameter("@XML_QTN", qtnXml);
			dbm.AddParameter("@XML_HGS", hgsXml);
			DataTable dt = dbm.GetDataTable();

			grd목록.DataBind(dt);
			SetGridStyle();

			for (int i = grd목록.Rows.Fixed; i < grd목록.Rows.Count; i++)
			{
				if (grd목록[i, "CD_ITEM_PARTNER"].ToString() == grd목록[i, "NO_PLATE"].ToString())
					grd목록[i, "CHK"] = "Y";
			}			
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn확정_Click(object sender, EventArgs e)
		{
			if (grd목록.GetCheckedRows("CHK") != null)
				this.DialogResult = DialogResult.OK;
			else				
				Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
		}

		#endregion

		#region ==================================================================================================== 취소

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}
