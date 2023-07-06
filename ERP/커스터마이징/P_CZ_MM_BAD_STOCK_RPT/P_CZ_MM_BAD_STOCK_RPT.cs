using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;

namespace cz
{
	public partial class P_CZ_MM_BAD_STOCK_RPT : PageBase
	{
		string companyCode;

		#region ===================================================================================================== Property

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_MM_BAD_STOCK_RPT()
		{
			StartUp.Certify(this);
			companyCode = LoginInfo.CompanyCode;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();

			base.InitLoad();
		}

		private void InitControl()
		{

		}

		private void InitGrid()
		{
			// ---------------------------------------------------------------------------------------------------- 바인딩
			string query = @"
SELECT 
	CD_FLAG1
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_FIELD = 'CZ_MM00001'
	AND CD_SYSDEF IN ('C01', 'C02', 'C03')
ORDER BY CD_SYSDEF";

			DataTable dtCond = DBMgr.GetDataTable(query);

			DataTable dtCond1 = new DataTable();
			dtCond1.Columns.Add("CODE");
			dtCond1.Columns.Add("NAME");
			dtCond1.Rows.Add("Y", dtCond.Rows[0][0] + DD("개월 이내 견적 없음"));
			dtCond1.Rows.Add("N", "");

			DataTable dtCond2 = new DataTable();
			dtCond2.Columns.Add("CODE");
			dtCond2.Columns.Add("NAME");
			dtCond2.Rows.Add("Y", dtCond.Rows[1][0] + DD("개월 이내 수주 없음"));
			dtCond2.Rows.Add("N", "");

			DataTable dtCond3 = new DataTable();
			dtCond3.Columns.Add("CODE");
			dtCond3.Columns.Add("NAME");
			dtCond3.Rows.Add("Y", dtCond.Rows[2][0] + DD("%이내 수주율"));
			dtCond3.Rows.Add("N", "");

			// ---------------------------------------------------------------------------------------------------- List
			grdList.BeginSetting(1, 1, false);

			grdList.SetCol("CD_ITEM"		 , "재고코드"		, 75);
			grdList.SetCol("NM_ITEM"		 , "재고명"		, 200);
			grdList.SetCol("STND_ITEM"		 , "파트넘버"		, 100);
			grdList.SetCol("MAT_ITEM"		 , "아이템넘버"	, 100);
			grdList.SetCol("STND_DETAIL_ITEM", "U코드"		, 100);
			grdList.SetCol("NO_STND"		 , "K코드"		, 100);
			grdList.SetCol("NM_CLS_L"		 , "대분류"		, 80);
			grdList.SetCol("NM_CLS_M"		 , "중분류"		, 80);
			grdList.SetCol("NM_CLS_S"		 , "소분류"		, 80);
			grdList.SetCol("DT_IO_FIRST"	 , "최초입고일"	, 80	, false, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grdList.SetCol("QT_INV"			 , "현재고"		, 50	, false, typeof(decimal), FormatTpType.QUANTITY);
			grdList.SetCol("UM_AVG"			 , "평균단가"		, 80	, false, typeof(decimal), FormatTpType.MONEY);
			grdList.SetCol("YN_COND1"		 , "조건1"		, 150);
			grdList.SetCol("YN_COND2"		 , "조건2"		, 150);
			grdList.SetCol("YN_COND3"		 , "조건3"		, 150);
			
			grdList.SetDataMap("YN_COND1", dtCond1, "CODE", "NAME");
			grdList.SetDataMap("YN_COND2", dtCond2, "CODE", "NAME");
			grdList.SetDataMap("YN_COND3", dtCond3, "CODE", "NAME");
			
			grdList.SetDefault("18.07.25.06", SumPositionEnum.None);
		}

		private void InitEvent()
		{
			ctxClassL.QueryBefore += new BpQueryHandler(ctxClassL_QueryBefore);
			ctxClassM.QueryBefore += new BpQueryHandler(ctxClassM_QueryBefore);
			ctxClassS.QueryBefore += new BpQueryHandler(ctxClassS_QueryBefore);
		}

		#endregion

		#region ==================================================================================================== 컨트롤이벤트

		private void ctxClassL_QueryBefore(object sender, BpQueryArgs e)
		{			
			e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
			e.HelpParam.P42_CD_FIELD2 = "";
		}

		private void ctxClassM_QueryBefore(object sender, BpQueryArgs e)
		{			
            e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = ctxClassL.CodeValue;
		}

		private void ctxClassS_QueryBefore(object sender, BpQueryArgs e)
		{			
            e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
			e.HelpParam.P42_CD_FIELD2 = ctxClassM.CodeValue;
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr db = new DBMgr();
			db.Procedure = "PS_CZ_MM_BAD_STOCK_RPT";
			db.AddParameter("@CD_COMPANY", companyCode);
			db.AddParameter("@CLS_L", ctxClassL.CodeValue);
			db.AddParameter("@CLS_M", ctxClassM.CodeValue);
			db.AddParameter("@CLS_S", ctxClassS.CodeValue);
			db.AddParameter("@CD_ITEM", tbxItemCode.Text);
			db.AddParameter("@NM_ITEM", tbxItemName.Text);
			db.AddParameter("@YN_COND1", chkCond1.Checked ? "Y" : "");
			db.AddParameter("@YN_COND2", chkCond2.Checked ? "Y" : "");
			db.AddParameter("@YN_COND3", chkCond3.Checked ? "Y" : "");

			DataTable dt = db.GetDataTable();
			grdList.Binding = dt;
		}

		#endregion
	}
}
