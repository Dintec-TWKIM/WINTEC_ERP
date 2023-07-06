using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

// 20190917
namespace cz
{
	public partial class H_CZ_SUPPLIER : Duzon.Common.Forms.CommonDialog
	{
		string CompanyCode;
		string MODE;

		#region ===================================================================================================== Property
	
		public DataTable Result
		{
			get
			{
				return grd선택.DataTable;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_SUPPLIER(string MODE)
		{
			CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
			this.MODE = MODE;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();

			if (MODE == "INQ")
			{
				oneGridItem1.Enabled = true;
				grd선택.Cols["RANGE"].AllowEditing = true;
			}
			else if (MODE == "EXT")
			{
				oneGridItem1.Enabled = false;
				grd선택.Cols["RANGE"].AllowEditing = false;
			}
		}

		private void InitControl()
		{
			btn확인.TabStop = true;
		}

		private void InitGrid()
		{
			// 검색 그리드
			grd조회.BeginSetting(1, 1, false);
			grd조회.SetCol("CD_PARTNER"	, "코드"			, 50);
			grd조회.SetCol("LN_PARTNER"	, "거래처명"		, 230);
			grd조회.SetCol("NM_CEO"		, "대표자"		, 80);
			grd조회.SetCol("NO_COMPANY"	, "사업자번호"	, 100);
			grd조회.SetCol("CD_AREA"		, "지역구분"		, false);
			grd조회.SetCol("CD_PINQ"		, "INQ발신"		, false);
			grd조회.SetCol("CD_PRINT"	, "인쇄형태"		, false);
			grd조회.SetCol("SN_PARTNER"	, "거래처명(약어)", false);
				
			grd조회.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd조회.Cols["NM_CEO"].TextAlign = TextAlignEnum.CenterCenter;
			grd조회.Cols["NO_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;

			grd조회.SettingVersion = "19.09.10.01";
			grd조회.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// 선택 그리드
			grd선택.BeginSetting(1, 1, false);
			grd선택.SetCol("CD_PARTNER"	, "코드"			, 50);
			grd선택.SetCol("LN_PARTNER"	, "거래처명"		, 230);
			grd선택.SetCol("NM_CEO"		, "대표자"		, 80);
			grd선택.SetCol("RANGE"		, "범위"			, 100	, true);
			grd선택.SetCol("CD_AREA"		, "지역구분"		, false);
			grd선택.SetCol("CD_PINQ"		, "INQ발신"		, false);
			grd선택.SetCol("CD_PRINT"	, "인쇄형태"		, false);
			grd선택.SetCol("SN_PARTNER"	, "거래처명(약어)", false);
				
			grd선택.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd선택.Cols["NM_CEO"].TextAlign = TextAlignEnum.CenterCenter;

			grd선택.SettingVersion = "19.09.10.01";
			grd선택.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			tbx검색.KeyDown += new KeyEventHandler(txt검색_KeyDown);

			btn조회.Click += new EventHandler(btn조회_Click);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);
			btn확인.Click += new EventHandler(btn확인_Click);
			btn취소.Click += new EventHandler(btn취소_Click);

			grd조회.DoubleClick += new EventHandler(grd조회_DoubleClick);
			grd선택.DoubleClick += new EventHandler(grd선택_DoubleClick);

			grd조회.KeyDown += new KeyEventHandler(grd조회_KeyDown);
		}

		protected override void InitPaint()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("CD_PARTNER");
			dt.Columns.Add("LN_PARTNER");
			dt.Columns.Add("NM_CEO");
			dt.Columns.Add("RANGE");
			dt.Columns.Add("CD_AREA");
			dt.Columns.Add("CD_PINQ");
			dt.Columns.Add("CD_PRINT");
			dt.Columns.Add("SN_PARTNER");
			grd선택.Binding = dt;

			btn조회_Click(null, null);
			tbx검색.Focus();			
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape) return false;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btn조회_Click(null, null);
			}
		}		

		private void btn추가_Click(object sender, EventArgs e)
		{
			// 중복 체크
			for (int i = 1; i < grd선택.Rows.Count; i++)
			{
				if (grd조회["CD_PARTNER"].ToString() == grd선택[i, "CD_PARTNER"].ToString())
				{
					MessageBox.Show("중복");
					return;
				}
			}

			// 추가
			grd선택.Rows.Add();
			grd선택.Row = grd선택.Rows.Count - 1;
			grd선택["CD_PARTNER"] = grd조회["CD_PARTNER"];
			grd선택["LN_PARTNER"] = grd조회["LN_PARTNER"];
			grd선택["NM_CEO"] = grd조회["NM_CEO"];
			grd선택["RANGE"] = tbx범위.Text;
			grd선택["CD_AREA"] = grd조회["CD_AREA"];
			grd선택["CD_PINQ"] = grd조회["CD_PINQ"];
			grd선택["CD_PRINT"] = grd조회["CD_PRINT"];
			grd선택["SN_PARTNER"] = grd조회["SN_PARTNER"];
			grd선택.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			grd선택.Rows.Remove(grd선택.Row);
		}

		private void btn확인_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd조회_DoubleClick(object sender, EventArgs e)
		{
			if (!grd조회.HasNormalRow || grd조회.MouseCol <= 0 || grd조회.MouseRow <= 0)
				return;
			
			btn추가_Click(null, null);
		}

		private void grd선택_DoubleClick(object sender, EventArgs e)
		{
			if (!grd선택.HasNormalRow || grd선택.MouseCol <= 0 || grd선택.MouseRow <= 0)
				return;
			
			btn삭제_Click(null, null);
		}

		private void grd조회_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btn추가_Click(null, null);
				tbx검색.Focus();
			}
		}

		#endregion

		#region ==================================================================================================== Search

		private void btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt = DBMgr.GetDataTable("PS_CZ_MA_PARTNER", CompanyCode, tbx검색.Text);
			grd조회.Binding = dt;
		}

		#endregion
	}
}
