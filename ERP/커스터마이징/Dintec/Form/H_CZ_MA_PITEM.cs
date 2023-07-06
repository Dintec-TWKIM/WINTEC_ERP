using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using Duzon.Common.BpControls;

namespace Dintec
{
	public partial class H_CZ_MA_PITEM : Duzon.Common.Forms.CommonDialog
	{
		private string CLS_ITEM = "'005','009','010','013','015'";	// 009:재고품, 013:상품(마스터), 005:상품(일반), 010:비용, 015:업무대행

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public DataRow ITEM { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_MA_PITEM()
		{
			InitializeComponent();
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		}

		public H_CZ_MA_PITEM(string keyWord)
		{
			InitializeComponent();
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			txt검색.Text = keyWord;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();
		}

		private void InitGrid()
		{
			flex.BeginSetting(1, 1, false);

			flex.SetCol("CD_ITEM"		, "재고코드"	, 120);
			flex.SetCol("NM_ITEM"		, "재고명"	, 250);
			flex.SetCol("STND_ITEM"		, "규격"		, 200);
			flex.SetCol("STAND_PRC"		, "표준원가"	, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			flex.SetCol("NM_CLS_ITEM"	, "계정구분"	, 100);

			flex.Cols["NM_CLS_ITEM"].TextAlign = TextAlignEnum.CenterCenter;

			flex.SettingVersion = "15.09.21.03";
			flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			txt검색.KeyDown += new KeyEventHandler(txt검색_KeyDown);

			flex.HelpClick += new EventHandler(flex_HelpClick);

			btn조회.Click += new EventHandler(btn조회_Click);
			btn확인.Click += new EventHandler(btn확인_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
		}

		protected override void InitPaint()
		{
			// 계정구분
			string query = @"
SELECT
	CD_SYSDEF, NM_SYSDEF
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = 'MA_B000010'
	AND CD_SYSDEF IN (" + CLS_ITEM + @")
ORDER BY NM_SYSDEF";

			DataTable dt = DBHelper.GetDataTable(query);
			dt.Rows.InsertAt(dt.NewRow(), 0);

			cbo계정구분.ValueMember = "CD_SYSDEF";
			cbo계정구분.DisplayMember = "NM_SYSDEF";
			cbo계정구분.DataSource = dt;	
	
			// 조회
			btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn조회_Click(object sender, EventArgs e)
		{
			string query = @"
SELECT
	  A.CD_ITEM
	, A.NM_ITEM
	, A.STND_ITEM
	, A.STAND_PRC
	, B.NM_SYSDEF	AS NM_CLS_ITEM
	, A.UNIT_IM
	, A.UNIT_SO
	, A.UNIT_PO
	, A.UNIT_MO
FROM      MA_PITEM		AS A WITH(NOLOCK)
LEFT JOIN MA_CODEDTL	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CLS_ITEM = B.CD_SYSDEF AND B.CD_FIELD = 'MA_B000010'
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.YN_USE = 'Y'";

			if (cbo계정구분.SelectedValue.ToString() == "")
				query += "\n" + "	AND A.CLS_ITEM IN (" + CLS_ITEM + ")";
			else
				query += "\n" + "	AND A.CLS_ITEM = '" + cbo계정구분.SelectedValue + "'";

			if (txt검색.Text.Trim() != "")
				query += "\n" + "	AND (A.CD_ITEM = '%" + txt검색.Text + "%' OR A.NM_ITEM LIKE '%" + txt검색.Text + "%')";

			DataTable dt = DBHelper.GetDataTable(query);
			flex.Binding = dt;
		}

		private void btn확인_Click(object sender, EventArgs e)
		{
			ITEM = flex.GetDataRow(flex.Row);
			this.DialogResult = DialogResult.OK;
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flex_HelpClick(object sender, EventArgs e)
		{
			btn확인_Click(null, null);
		}

		#endregion
	}
}
