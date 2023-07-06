using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{	
	public partial class P_CZ_QR_LABEL : PageBase
	{
		P_CZ_QR_LABEL_BIZ _biz = new P_CZ_QR_LABEL_BIZ();
		private bool _isNetwork;
		private string _printer;
		private string _파일번호_NGR;
		private string _파일번호_SGR;

		public P_CZ_QR_LABEL()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo회사.DataSource = DBHelper.GetDataTable(@"SELECT CD_COMPANY, NM_COMPANY 
FROM MA_COMPANY WITH(NOLOCK)
WHERE CD_COMPANY IN ('K100', 'K200', 'S100')");
			this.cbo회사.ValueMember = "CD_COMPANY";
			this.cbo회사.DisplayMember = "NM_COMPANY";

			DataTable dt = new DataTable();
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			DataRow dataRow;

			dataRow = dt.NewRow();

			dataRow["CODE"] = string.Empty;
			dataRow["NAME"] = string.Empty;

			dt.Rows.Add(dataRow);

			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				dataRow = dt.NewRow();

				dataRow["CODE"] = printer;
				dataRow["NAME"] = printer;

				dt.Rows.Add(dataRow);
			}

			foreach (DataRow dr in DBHelper.GetDataTable(@"SELECT CD_FLAG1 AS CODE, 'NET_' + NM_SYSDEF AS NAME
FROM MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_PDA0001'").Rows)
			{
				dataRow = dt.NewRow();

				dataRow["CODE"] = dr["CODE"].ToString();
				dataRow["NAME"] = dr["NAME"].ToString();

				dt.Rows.Add(dataRow);
			}

			this.cbo프린터.DataSource = dt;
			this.cbo프린터.ValueMember = "CODE";
			this.cbo프린터.DisplayMember = "NAME";

			this.cbo구분_NGR.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신규발행", "재발행(취소)" });
			this.cbo구분_NGR.ValueMember = "CODE";
			this.cbo구분_NGR.DisplayMember = "NAME";

			this.cbo구분_LC.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신규발행", "재발행" });
			this.cbo구분_LC.ValueMember = "CODE";
			this.cbo구분_LC.DisplayMember = "NAME";

			this.cbo구분_NGI.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신규발행", "재발행(취소)" });
			this.cbo구분_NGI.ValueMember = "CODE";
			this.cbo구분_NGI.DisplayMember = "NAME";

			this.cbo구분_SGR.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신규발행", "재발행(취소)" });
			this.cbo구분_SGR.ValueMember = "CODE";
			this.cbo구분_SGR.DisplayMember = "NAME";

			this.cbo구분_SGI.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신규발행", "재발행(취소)" });
			this.cbo구분_SGI.ValueMember = "CODE";
			this.cbo구분_SGI.DisplayMember = "NAME";

			this.cbo번호_NGR.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "파일번호", "호선번호" });
			this.cbo번호_NGR.ValueMember = "CODE";
			this.cbo번호_NGR.DisplayMember = "NAME";

			this.cbo번호_LC.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "파일번호", "호선번호" });
			this.cbo번호_LC.ValueMember = "CODE";
			this.cbo번호_LC.DisplayMember = "NAME";

			this.cbo번호_NGI.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "파일번호", "호선번호" });
			this.cbo번호_NGI.ValueMember = "CODE";
			this.cbo번호_NGI.DisplayMember = "NAME";

			this.cbo번호_SGR.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "발주번호", "호선번호" });
			this.cbo번호_SGR.ValueMember = "CODE";
			this.cbo번호_SGR.DisplayMember = "NAME";

			this.cbo번호_SGI.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "파일번호", "호선번호" });
			this.cbo번호_SGI.ValueMember = "CODE";
			this.cbo번호_SGI.DisplayMember = "NAME";
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex일반품입고, this._flex일반품출고, this._flex일반품로케이션, this._flex재고품입고, this._flex재고품출고 };

			#region 일반품입고 
			this._flex일반품입고.BeginSetting(1, 1, false);

			this._flex일반품입고.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품입고.SetCol("NO_FILE", "파일번호", 100);
			this._flex일반품입고.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex일반품입고.SetCol("NO_DSP", "순번", 100);
			this._flex일반품입고.SetCol("NM_SUBJECT", "주제", 100);
			this._flex일반품입고.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex일반품입고.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex일반품입고.SetCol("CD_ITEM", "재고코드", 100);
			this._flex일반품입고.SetCol("NM_UNIT", "단위", 100);
			this._flex일반품입고.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고.SetCol("QT_GR", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고.SetCol("NO_ORDER", "주문번호", 100);
			this._flex일반품입고.SetCol("NM_BUYER", "매출처", 100);
			this._flex일반품입고.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품입고.SetDummyColumn("S");

			this._flex일반품입고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex일반품입고L.BeginSetting(1, 1, false);

			this._flex일반품입고L.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품입고L.SetCol("NO_FILE", "파일번호", 100);
			this._flex일반품입고L.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex일반품입고L.SetCol("NO_DSP", "순번", 100);
			this._flex일반품입고L.SetCol("NM_SUBJECT", "주제", 100);
			this._flex일반품입고L.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex일반품입고L.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex일반품입고L.SetCol("CD_ITEM", "재고코드", 100);
			this._flex일반품입고L.SetCol("NM_UNIT", "단위", 100);
			this._flex일반품입고L.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고L.SetCol("QT_GR", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고L.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고L.SetCol("QT_WORK", "인쇄수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품입고L.SetCol("NO_ORDER", "주문번호", 100);
			this._flex일반품입고L.SetCol("NM_BUYER", "매출처", 100);
			this._flex일반품입고L.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품입고L.SetDummyColumn("S");

			this._flex일반품입고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
			#endregion

			#region 일반품로케이션
			this._flex일반품로케이션.BeginSetting(1, 1, false);

			this._flex일반품로케이션.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품로케이션.SetCol("NO_FILE", "파일번호", 100);
			this._flex일반품로케이션.SetCol("NO_PO", "발주번호", 100);
			this._flex일반품로케이션.SetCol("LN_PARTNER", "매입처", 100);
			this._flex일반품로케이션.SetCol("NO_RCV", "입고번호", 100);
			this._flex일반품로케이션.SetCol("NM_EMP", "담당자", 100);
			this._flex일반품로케이션.SetCol("DTS_INSERT", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex일반품로케이션.SetCol("CNT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품로케이션.SetCol("NO_ORDER", "주문번호", 100);
			this._flex일반품로케이션.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품로케이션.SetDummyColumn("S");
			this._flex일반품로케이션.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

			this._flex일반품로케이션.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex일반품로케이션L.BeginSetting(1, 1, false);

			this._flex일반품로케이션L.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품로케이션L.SetCol("NO_FILE", "파일번호", 100);
			this._flex일반품로케이션L.SetCol("NO_PO", "발주번호", 100);
			this._flex일반품로케이션L.SetCol("LN_PARTNER", "매입처", 100);
			this._flex일반품로케이션L.SetCol("NO_RCV", "입고번호", 100);
			this._flex일반품로케이션L.SetCol("NM_EMP", "담당자", 100);
			this._flex일반품로케이션L.SetCol("DTS_INSERT", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex일반품로케이션L.SetCol("CNT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품로케이션L.SetCol("NO_ORDER", "주문번호", 100);
			this._flex일반품로케이션L.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품로케이션L.SetDummyColumn("S");
			this._flex일반품로케이션L.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

			this._flex일반품로케이션L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion

			#region 일반품출고
			this._flex일반품출고.BeginSetting(1, 1, false);

			this._flex일반품출고.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품출고.SetCol("NO_GIR", "협조전번호", 100);
			this._flex일반품출고.SetCol("NO_SO", "파일번호", 100);
			this._flex일반품출고.SetCol("NO_DSP", "순번", 100);
			this._flex일반품출고.SetCol("NM_SUBJECT", "주제", 100);
			this._flex일반품출고.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex일반품출고.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex일반품출고.SetCol("NM_UNIT", "단위", 100);
			this._flex일반품출고.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고.SetCol("NO_REF", "주문번호", 100);
			this._flex일반품출고.SetCol("NM_BUYER", "매출처", 100);
			this._flex일반품출고.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품출고.SetDummyColumn("S");

			this._flex일반품출고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex일반품출고L.BeginSetting(1, 1, false);

			this._flex일반품출고L.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex일반품출고L.SetCol("NO_GIR", "협조전번호", 100);
			this._flex일반품출고L.SetCol("NO_SO", "파일번호", 100);
			this._flex일반품출고L.SetCol("NO_DSP", "순번", 100);
			this._flex일반품출고L.SetCol("NM_SUBJECT", "주제", 100);
			this._flex일반품출고L.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex일반품출고L.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex일반품출고L.SetCol("NM_UNIT", "단위", 100);
			this._flex일반품출고L.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고L.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고L.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고L.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex일반품출고L.SetCol("NO_REF", "주문번호", 100);
			this._flex일반품출고L.SetCol("NM_BUYER", "매출처", 100);
			this._flex일반품출고L.SetCol("NM_VESSEL", "호선명", 100);

			this._flex일반품출고L.SetDummyColumn("S");

			this._flex일반품출고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion

			#region 재고품입고
			this._flex재고품입고.BeginSetting(1, 1, false);

			this._flex재고품입고.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex재고품입고.SetCol("NO_PO", "발주번호", 100);
			this._flex재고품입고.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex재고품입고.SetCol("NO_DSP", "순번", 100);
			this._flex재고품입고.SetCol("NM_SUBJECT", "주제", 100);
			this._flex재고품입고.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex재고품입고.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex재고품입고.SetCol("CD_ITEM", "재고코드", 100);
			this._flex재고품입고.SetCol("NM_UNIT", "단위", 100);
			this._flex재고품입고.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고.SetCol("QT_GR", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고.SetCol("NO_ORDER", "주문번호", 100);
			this._flex재고품입고.SetCol("NM_EMP", "담당자", 100);

			this._flex재고품입고.SetDummyColumn("S");

			this._flex재고품입고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex재고품입고L.BeginSetting(1, 1, false);

			this._flex재고품입고L.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex재고품입고L.SetCol("NO_PO", "발주번호", 100);
			this._flex재고품입고L.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex재고품입고L.SetCol("NO_DSP", "순번", 100);
			this._flex재고품입고L.SetCol("NM_SUBJECT", "주제", 100);
			this._flex재고품입고L.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex재고품입고L.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex재고품입고L.SetCol("CD_ITEM", "재고코드", 100);
			this._flex재고품입고L.SetCol("NM_UNIT", "단위", 100);
			this._flex재고품입고L.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고L.SetCol("QT_GR", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고L.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고L.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품입고L.SetCol("NO_ORDER", "주문번호", 100);
			this._flex재고품입고L.SetCol("NM_EMP", "담당자", 100);

			this._flex재고품입고L.SetDummyColumn("S");

			this._flex재고품입고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion

			#region 재고품출고
			this._flex재고품출고.BeginSetting(1, 1, false);

			this._flex재고품출고.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex재고품출고.SetCol("NO_GIR", "협조전번호", 100);
			this._flex재고품출고.SetCol("NO_SO", "파일번호", 100);
			this._flex재고품출고.SetCol("NO_DSP", "순번", 100);
			this._flex재고품출고.SetCol("NM_SUBJECT", "주제", 100);
			this._flex재고품출고.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex재고품출고.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex재고품출고.SetCol("CD_ITEM", "재고코드", 100);
			this._flex재고품출고.SetCol("CD_ZONE", "로케이션", 100);
			this._flex재고품출고.SetCol("NM_UNIT", "단위", 100);
			this._flex재고품출고.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고.SetCol("NO_REF", "주문번호", 100);
			this._flex재고품출고.SetCol("NM_BUYER", "매출처", 100);
			this._flex재고품출고.SetCol("NM_VESSEL", "선명", 100);

			this._flex재고품출고.SetDummyColumn("S");

			this._flex재고품출고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex재고품출고L.BeginSetting(1, 1, false);

			this._flex재고품출고L.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex재고품출고L.SetCol("NO_GIR", "협조전번호", 100);
			this._flex재고품출고L.SetCol("NO_SO", "파일번호", 100);
			this._flex재고품출고L.SetCol("NO_DSP", "순번", 100);
			this._flex재고품출고L.SetCol("NM_SUBJECT", "주제", 100);
			this._flex재고품출고L.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
			this._flex재고품출고L.SetCol("NM_ITEM_PARTNER", "품목명", 100);
			this._flex재고품출고L.SetCol("CD_ITEM", "재고코드", 100);
			this._flex재고품출고L.SetCol("CD_ZONE", "로케이션", 100);
			this._flex재고품출고L.SetCol("NM_UNIT", "단위", 100);
			this._flex재고품출고L.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고L.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고L.SetCol("QT_LABEL", "라벨수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고L.SetCol("QT_WORK", "인쇄수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex재고품출고L.SetCol("NO_REF", "주문번호", 100);
			this._flex재고품출고L.SetCol("NM_BUYER", "매출처", 100);
			this._flex재고품출고L.SetCol("NM_VESSEL", "선명", 100);

			this._flex재고품출고L.SetDummyColumn("S");

			this._flex재고품출고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.cbo프린터.SelectedValueChanged += Cbo프린터_SelectedValueChanged;

			this.tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
			this.cbo구분_NGR.SelectedIndexChanged += Control_SelectedIndexChanged;
			this.cbo구분_NGI.SelectedIndexChanged += Control_SelectedIndexChanged;
			this.cbo구분_LC.SelectedIndexChanged += Control_SelectedIndexChanged;
			this.cbo구분_SGR.SelectedIndexChanged += Control_SelectedIndexChanged;
			this.cbo구분_SGI.SelectedIndexChanged += Control_SelectedIndexChanged;

			this.btn발행취소.Click += Btn취소_Click;
			this.btn초기화.Click += Btn초기화_Click;

			this.txt번호_NGR.KeyDown += Control_KeyDown;
			this.txt번호_NGI.KeyDown += Control_KeyDown;
			this.txt번호_LC.KeyDown += Control_KeyDown;
			this.txt번호_SGR.KeyDown += Control_KeyDown;
			this.txt번호_SGI.KeyDown += Control_KeyDown;

			this._flex일반품입고.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex일반품출고.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex일반품로케이션.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex재고품입고.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex재고품출고.MouseDoubleClick += Grid_MouseDoubleClick;
		}

		private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FlexGrid grid, gridL;
			DataRow dr;

			try
			{
				grid = ((FlexGrid)sender);

				if (grid == this._flex일반품입고)
					gridL = this._flex일반품입고L;
				else if (grid == this._flex일반품로케이션)
					gridL = this._flex일반품로케이션L;
				else if (grid == this._flex일반품출고)
					gridL = this._flex일반품출고L;
				else if (grid == this._flex재고품입고)
					gridL = this._flex재고품입고L;
				else if (grid == this._flex재고품출고)
					gridL = this._flex재고품출고L;
				else
					return;

				dr = ((DataRowView)grid.Rows[grid.RowSel].DataSource).Row;

				gridL.DataTable.ImportRow(dr);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn초기화_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex일반품입고.DataTable != null)
					this._flex일반품입고L.Binding = this._flex일반품입고.DataTable.Clone();

				if (this._flex일반품로케이션.DataTable != null)
					this._flex일반품로케이션L.Binding = this._flex일반품로케이션.DataTable.Clone();

				if (this._flex일반품출고.DataTable != null)
					this._flex일반품출고L.Binding = this._flex일반품출고.DataTable.Clone();

				if (this._flex재고품입고.DataTable != null)
					this._flex재고품입고L.Binding = this._flex재고품입고.DataTable.Clone();

				if (this._flex재고품출고.DataTable != null)
					this._flex재고품출고L.Binding = this._flex재고품출고.DataTable.Clone();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Enter)
					return;

				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_SelectedIndexChanged(object sender, EventArgs e)
		{
			Control control;
			FlexGrid grid, gridL;

			try
			{
				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
				{
					control = this.cbo구분_NGR;
					grid = this._flex일반품입고;
					gridL = this._flex일반품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
				{
					control = this.cbo구분_NGI;
					grid = this._flex일반품출고;
					gridL = this._flex일반품출고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					this.btn발행취소.Enabled = false;

					if (this._flex일반품로케이션.HasNormalRow)
						this.OnToolBarSearchButtonClicked(null, null);

					return;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
				{
					control = this.cbo구분_SGR;
					grid = this._flex재고품입고;
					gridL = this._flex재고품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
				{
					control = this.cbo구분_SGI;
					grid = this._flex재고품출고;
					gridL = this._flex재고품출고L;
				}
				else
					return;

				if (((DropDownComboBox)control).SelectedIndex == 0)
					this.btn발행취소.Enabled = false;
				else
					this.btn발행취소.Enabled = true;

				if (grid.HasNormalRow)
				{
					this.OnToolBarSearchButtonClicked(null, null);
				}

				if (grid.DataTable != null)
					gridL.Binding = grid.DataTable.Clone();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Control control;

			try
			{
				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
					control = this.cbo구분_NGR;
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
					control = this.cbo구분_NGI;
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					this.btn발행취소.Enabled = false;
					return;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
					control = this.cbo구분_SGR;
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
					control = this.cbo구분_SGI;
				else
					return;

				if (((DropDownComboBox)control).SelectedIndex == 0)
					this.btn발행취소.Enabled = false;
				else
					this.btn발행취소.Enabled = true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn취소_Click(object sender, EventArgs e)
		{
			FlexGrid gridL = null;
			DataRow[] dataRowArray;

			try
			{
				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
					gridL = this._flex일반품입고L;
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
					gridL = this._flex일반품출고L;
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
					gridL = this._flex재고품입고L;
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
					gridL = this._flex재고품출고L;
				else
					return;

				dataRowArray = gridL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					gridL.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						if (this.tabControl1.SelectedTab == this.tpg일반품입고)
						{
							DBHelper.ExecuteNonQuery("SP_CZ_QR_LABEL_GR_D", new object[] { this.cbo회사.SelectedValue,
																						   dr["NO_PO"].ToString(),
																						   dr["NO_LINE"].ToString(),
																						   dr["NO_SEQ"].ToString(),
																						   "Y" });
						}
						else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
						{
							DBHelper.ExecuteNonQuery("SP_CZ_QR_LABEL_GI_D", new object[] { this.cbo회사.SelectedValue,
																						   dr["NO_GIR"].ToString(),
																						   dr["SEQ_GIR"].ToString(),
																						   dr["NO_SEQ"].ToString() });
						}
						else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
						{
							DBHelper.ExecuteNonQuery("SP_CZ_QR_LABEL_GR_D", new object[] { this.cbo회사.SelectedValue,
																						   dr["NO_PO"].ToString(),
																						   dr["NO_LINE"].ToString(),
																						   dr["NO_SEQ"].ToString(),
																						   "N" });
						}
						else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
						{
							DBHelper.ExecuteNonQuery("SP_CZ_QR_LABEL_GI_D", new object[] { this.cbo회사.SelectedValue,
																						   dr["NO_GIREQ"].ToString(),
																						   dr["NO_LINE"].ToString(),
																						   dr["NO_SEQ"].ToString() });
						}
						else
							return;

						dr.Delete();
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn발행취소.Text);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				gridL.Redraw = true;
			}
		}

		private void Cbo프린터_SelectedValueChanged(object sender, EventArgs e)
		{
			try
			{
				this._printer = this.cbo프린터.SelectedValue.ToString();

				if (this.cbo프린터.Text.StartsWith("NET_"))
					this._isNetwork = true;
				else
					this._isNetwork = false;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			FlexGrid grid;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
				{
					if (string.IsNullOrEmpty(this.txt번호_NGR.Text))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, "파일번호");
						return;
					}

					if (this._파일번호_NGR != this.txt번호_NGR.Text)
					{
						string query = @"SELECT A.CD_PARTNER, 
										        A.LN_PARTNER
										 FROM V_CZ_QR_POH AS A WITH(NOLOCK)
										 JOIN V_CZ_QR_SOH AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_FILE = B.NO_FILE
										 WHERE 1 = 1";

						query += "\n" + "AND A.CD_COMPANY = '" + this.cbo회사.SelectedValue + "'";

						if (!string.IsNullOrEmpty(this.txt번호_NGR.Text))
						{
							if (this.cbo번호_NGR.SelectedIndex == 0)
								query += "\n" + "	AND A.NO_FILE = '" + this.txt번호_NGR.Text + "'";
							if (this.cbo번호_NGR.SelectedIndex == 1)
								query += "\n" + "	AND B.NO_HULL = '" + this.txt번호_NGR.Text + "'";
						}

						DataTable dt = DBHelper.GetDataTable(query);
						dt.Rows.InsertAt(dt.NewRow(), 0);

						this.cbo매입처_NGR.DataSource = dt;
						this.cbo매입처_NGR.ValueMember = "CD_PARTNER";
						this.cbo매입처_NGR.DisplayMember = "LN_PARTNER";

						this._파일번호_NGR = this.txt번호_NGR.Text;
					}

					grid = this._flex일반품입고;
					grid.Binding = this._biz.Search일반품입고(new object[] { this.cbo구분_NGR.SelectedIndex,
																	         this.cbo회사.SelectedValue,
																	         (this.cbo번호_NGR.SelectedIndex == 0 ? this.txt번호_NGR.Text : string.Empty),
																	         (this.cbo번호_NGR.SelectedIndex == 1 ? this.txt번호_NGR.Text : string.Empty),
																	         this.cbo매입처_NGR.SelectedValue,
																	         this.txt재고코드_NGR.Text });

					if (this._flex일반품입고L.DataTable == null)
						this._flex일반품입고L.Binding = grid.DataTable.Clone();
					
					if (!grid.HasNormalRow)
					{
						string query = @"SELECT PH.NO_PO 
FROM PU_POH PH WITH(NOLOCK)
WHERE PH.CD_COMPANY = '{0}'
AND PH.NO_PO = '{1}-ST'";

						DataTable dt = DBHelper.GetDataTable(string.Format(query, this.cbo회사.SelectedValue, this.txt번호_NGR.Text));

						if (dt != null && dt.Rows.Count > 0)
						{
							MessageBox.Show(this.txt번호_NGR.Text + "-ST 번호로 재고발주 된 건이 있습니다.\n재고품 입고라벨에서 해당 번호로 조회 하시기 바랍니다.");
						}
					}
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					grid = this._flex일반품로케이션;
					grid.Binding = this._biz.Search로케이션(new object[] { this.cbo구분_LC.SelectedIndex,
																	       this.cbo회사.SelectedValue,
																		   (this.cbo번호_LC.SelectedIndex == 0 ? this.txt번호_LC.Text : string.Empty),
																		   (this.cbo번호_LC.SelectedIndex == 1 ? this.txt번호_LC.Text : string.Empty),
																		   this.cbo매입처_LC.SelectedValue,
																		   this.dtp입고일자_LC.StartDateToString,
																		   this.dtp입고일자_LC.EndDateToString,
																		   this.ctx담당자_LC.CodeValue });

					if (this._flex일반품로케이션L.DataTable == null)
						this._flex일반품로케이션L.Binding = grid.DataTable.Clone();

					string query = @"SELECT DISTINCT B.CD_PARTNER,
									 	             D.LN_PARTNER
									 FROM CZ_QR_LABELH AS A WITH(NOLOCK)
									 JOIN PU_RCVH AS B WITH(NOLOCK) ON B.CD_COMPANY = A.CD_COMPANY AND B.NO_RCV = A.NO_RCV
									 JOIN PU_RCVL AS C WITH(NOLOCK) ON C.CD_COMPANY = B.CD_COMPANY AND C.NO_RCV = B.NO_RCV
									 LEFT JOIN MA_PARTNER AS D WITH(NOLOCK) ON D.CD_COMPANY = B.CD_COMPANY AND D.CD_PARTNER = B.CD_PARTNER
									 LEFT JOIN V_CZ_SA_QTNH AS E WITH(NOLOCK) ON E.CD_COMPANY = C.CD_COMPANY AND E.NO_FILE = C.CD_PJT
									 WHERE 1 = 1";

					query += "\n" + "AND A.CD_COMPANY = '" + this.cbo회사.SelectedValue + "'";
					query += "\n" + "AND SUBSTRING(A.DTS_INSERT, 1, 8) BETWEEN '" + this.dtp입고일자_LC.StartDateToString + "' AND '" + this.dtp입고일자_LC.EndDateToString + "'";

					if (!string.IsNullOrEmpty(this.txt번호_LC.Text))
					{
						if (this.cbo번호_LC.SelectedIndex == 0)
							query += "\n" + "	AND C.CD_PJT = '" + this.txt번호_LC.Text + "'";
						if (this.cbo번호_LC.SelectedIndex == 1)
							query += "\n" + "	AND E.NO_HULL = '" + this.txt번호_LC.Text + "'";
					}

					DataTable dt = DBHelper.GetDataTable(query);
					dt.Rows.InsertAt(dt.NewRow(), 0);

					this.cbo매입처_LC.DataSource = dt;
					this.cbo매입처_LC.ValueMember = "CD_PARTNER";
					this.cbo매입처_LC.DisplayMember = "LN_PARTNER";
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
				{
					grid = this._flex일반품출고;
					grid.Binding = this._biz.Search일반품출고(new object[] { this.cbo구분_NGI.SelectedIndex,
																			 this.cbo회사.SelectedValue,
																			 this.dtp작성일자_NGI.StartDateToString,
																			 this.dtp작성일자_NGI.EndDateToString,
																			 (this.cbo번호_NGI.SelectedIndex == 0 ? this.txt번호_NGI.Text : string.Empty),
																		     (this.cbo번호_NGI.SelectedIndex == 1 ? this.txt번호_NGI.Text : string.Empty),
																	         this.txt재고코드_NGI.Text });

					if (this._flex일반품출고L.DataTable == null)
						this._flex일반품출고L.Binding = grid.DataTable.Clone();
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
				{
					if (string.IsNullOrEmpty(this.txt번호_SGR.Text))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, "파일번호");
						return;
					}

					if (this._파일번호_SGR != this.txt번호_SGR.Text)
					{
						string query = @"SELECT A.CD_PARTNER, 
										        A.LN_PARTNER
										 FROM V_CZ_QR_POH AS A WITH(NOLOCK)
										 JOIN V_CZ_QR_SOH AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_FILE = B.NO_FILE
										 WHERE 1 = 1";

						query += "\n" + "AND A.CD_COMPANY = '" + this.cbo회사.SelectedValue + "'";

						if (!string.IsNullOrEmpty(this.txt번호_SGR.Text))
						{
							if (this.cbo번호_SGR.SelectedIndex == 0)
								query += "\n" + "	AND A.NO_FILE = '" + this.txt번호_SGR.Text + "'";
							if (this.cbo번호_SGR.SelectedIndex == 1)
								query += "\n" + "	AND B.NO_HULL = '" + this.txt번호_SGR.Text + "'";
						}

						DataTable dt = DBHelper.GetDataTable(query);
						dt.Rows.InsertAt(dt.NewRow(), 0);

						this.cbo매입처_SGR.DataSource = dt;
						this.cbo매입처_SGR.ValueMember = "CD_PARTNER";
						this.cbo매입처_SGR.DisplayMember = "LN_PARTNER";

						this._파일번호_SGR = this.txt번호_SGR.Text;
					}

					grid = this._flex재고품입고;
					grid.Binding = this._biz.Search재고품입고(new object[] { this.cbo구분_SGR.SelectedIndex,
																			 this.cbo회사.SelectedValue,
																			 (this.cbo번호_SGR.SelectedIndex == 0 ? this.txt번호_SGR.Text : string.Empty),
																			 (this.cbo번호_SGR.SelectedIndex == 1 ? this.txt번호_SGR.Text : string.Empty),
																			 this.cbo매입처_SGR.SelectedValue,
																			 this.txt재고코드_SGR.Text });

					if (this._flex재고품입고L.DataTable == null)
						this._flex재고품입고L.Binding = grid.DataTable.Clone();
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
				{
					grid = this._flex재고품출고;
					grid.Binding = this._biz.Search재고품출고(new object[] { this.cbo구분_SGI.SelectedIndex,
																			 this.cbo회사.SelectedValue,
																			 this.dtp작성일자_SGI.StartDateToString,
																			 this.dtp작성일자_SGI.EndDateToString,
																			 (this.cbo번호_SGI.SelectedIndex == 0 ? this.txt번호_SGI.Text : string.Empty),
																			 (this.cbo번호_SGI.SelectedIndex == 1 ? this.txt번호_SGI.Text : string.Empty),
																			 this.txt재고코드_SGI.Text });

					if (this._flex재고품출고L.DataTable == null)
						this._flex재고품출고L.Binding = grid.DataTable.Clone();
				}
				else
					return;
				
				if (!grid.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			FlexGrid grid, gridL = null;
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
				{
					grid = this._flex일반품입고;
					gridL = this._flex일반품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					grid = this._flex일반품로케이션;
					gridL = this._flex일반품로케이션L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
				{
					grid = this._flex일반품출고;
					gridL = this._flex일반품출고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
				{
					grid = this._flex재고품입고;
					gridL = this._flex재고품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
				{
					grid = this._flex재고품출고;
					gridL = this._flex재고품출고L;
				}
				else
					return;

				dataRowArray = grid.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					gridL.Redraw = false;

					for (int index = 0; index < dataRowArray.Length; ++index)
					{
						gridL.DataTable.ImportRow(dataRowArray[index]);
					}

					gridL.SumRefresh();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				gridL.Redraw = true;
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			FlexGrid gridL = null;
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
				{
					gridL = this._flex일반품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					gridL = this._flex일반품로케이션L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
				{
					gridL = this._flex일반품출고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
				{
					gridL = this._flex재고품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
				{
					gridL = this._flex재고품출고L;
				}
				else
					return;

				dataRowArray = gridL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					gridL.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						dr.Delete();
					}

					gridL.SumRefresh();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				gridL.Redraw = true;
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			FlexGrid gridL = null;
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg일반품입고)
				{
					gridL = this._flex일반품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
				{
					gridL = this._flex일반품로케이션L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
				{
					gridL = this._flex일반품출고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
				{
					gridL = this._flex재고품입고L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
				{
					gridL = this._flex재고품출고L;
				}
				else
					return;

				dataRowArray = gridL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this.cbo회사.Enabled = false;
					this.cbo프린터.Enabled = false;

					gridL.Redraw = false;
					
					Zebra zebra = new Zebra();

					if (this._isNetwork == true)
						zebra.Connect(this._printer, 6101);

					foreach (DataRow dr in dataRowArray)
					{
						if (this.tabControl1.SelectedTab == this.tpg일반품입고)
						{
							#region 일반품입고
							if (Convert.ToInt32(dr["QT_WORK"]) <= 0)
							{
								this.ShowMessage("인쇄 수량을 확인하세요! (0 또는 음수값 존재)");
								return;
							}

							string itemCode = dr["CD_ITEM_PARTNER"].ToString();
							string itemName = dr["NM_ITEM_PARTNER"].ToString();

							if (!string.IsNullOrEmpty(itemCode) && itemCode.Length > 23)
								itemCode = itemCode.Substring(0, 23);

							if (!string.IsNullOrEmpty(itemName) && itemName.Length > 125)
								itemName = itemName.Substring(0, 125);

							zebra.LabelType = LabelType.GRGI;
							zebra.NO_DSP = dr["NO_DSP"];
							zebra.NO_REF = dr["NO_REF"];
							zebra.NO_FILE = dr["NO_FILE"];
							zebra.NM_BUYER = dr["NM_BUYER"];
							zebra.NM_SUBJECT = dr["NM_SUBJECT"];
							zebra.NM_VESSEL = dr["NM_VESSEL"];
							zebra.NO_HULL = dr["NO_HULL"];
							zebra.CD_ITEM_PARTNER = itemCode;
							zebra.QT_WORK = dr["QT_WORK"];
							zebra.NM_ITEM_PARTNER = itemName;
							zebra.NO_PO = dr["NO_PO"];
							zebra.NO_LINE = dr["NO_LINE"];
							zebra.NO_SEQ = dr["NO_SEQ"];
							zebra.NM_SUPPLIER = dr["NM_SUPPLIER"];
							zebra.NM_EMP = dr["NM_EMP"];
							zebra.NO_ACTION = dr["NO_ORDER"];

							zebra.QR = dr["QR"];
							zebra.EQ = dr["EQ"];
							zebra.PART = dr["PART"];
							zebra.LOCATION = dr["LOCATION"];
							zebra.PART_DESC = dr["PART_DESC"];

							if (this.cbo구분_NGR.SelectedIndex == 0)
							{
								DataTable dt = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GR_I", new object[] { this.cbo회사.SelectedValue.ToString(),
																										   zebra.NO_PO,
																										   zebra.NO_LINE,
																										   zebra.QT_WORK,
																										   "Y" });
								zebra.NO_SEQ = dt.Rows[0]["NO_SEQ"];
							}

							zebra.Print(this.cbo회사.SelectedValue.ToString(), this._printer, this._isNetwork, this.chk선용라벨.Checked, this.chk로고표시.Checked);
							#endregion
						}
						else if (this.tabControl1.SelectedTab == this.tpg일반품로케이션)
						{
							#region 일반품로케이션
							zebra.LabelType = LabelType.Location;
							zebra.CD_COMPANY = dr["CD_COMPANY"];
							zebra.NO_FILE = dr["NO_FILE"];
							zebra.NM_SUPPLIER = dr["LN_PARTNER"];
							zebra.NM_VESSEL = dr["NM_VESSEL"];
							zebra.CNT_ITEM = dr["CNT_ITEM"];
							zebra.DT_GR = dr["DTS_INSERT"];
							zebra.NM_EMP = dr["NM_EMP"];
							zebra.NO_ACTION = dr["NO_ORDER"];
							zebra.NO_ML = dr["NO_RCV"];

							if (this.cbo구분_LC.SelectedIndex == 0)
								DBHelper.ExecuteNonQuery("SP_CZ_QR_LABEL_LOC_U", new object[] { zebra.CD_COMPANY, zebra.NO_ML });

							zebra.Print(this.cbo회사.SelectedValue.ToString(), this._printer, this._isNetwork, this.chk선용라벨.Checked, this.chk로고표시.Checked);
							#endregion
						}
						else if (this.tabControl1.SelectedTab == this.tpg일반품출고)
						{
							#region 일반품출고
							if (Convert.ToInt32(dr["QT_WORK"]) <= 0)
							{
								this.ShowMessage("인쇄 수량을 확인하세요! (0 또는 음수값 존재)");
								return;
							}

							string itemCode = dr["CD_ITEM_PARTNER"].ToString();
							string itemName = dr["NM_ITEM_PARTNER"].ToString();

							if (!string.IsNullOrEmpty(itemCode) && itemCode.Length > 23)
								itemCode = itemCode.Substring(0, 23);

							if (!string.IsNullOrEmpty(itemName) && itemName.Length > 125)
								itemName = itemName.Substring(0, 125);

							zebra.LabelType = LabelType.GI;
							zebra.NO_DSP = dr["NO_DSP"];
							zebra.NO_REF = dr["NO_REF"];
							zebra.NO_FILE = dr["NO_SO"];
							zebra.NM_BUYER = dr["NM_BUYER"];
							zebra.NM_SUBJECT = dr["NM_SUBJECT"];
							zebra.NM_VESSEL = dr["NM_VESSEL"];
							zebra.CD_ITEM_PARTNER = itemCode;
							zebra.QT_WORK = dr["QT_WORK"];
							zebra.NM_ITEM_PARTNER = itemName;
							zebra.NO_LINE = dr["SEQ_SO"];
							zebra.NO_SEQ = dr["NO_SEQ"];

							zebra.QR = dr["QR"];
							zebra.EQ = dr["EQ"];
							zebra.PART = dr["PART"];
							zebra.LOCATION = dr["LOCATION"];
							zebra.PART_DESC = dr["PART_DESC"];

							if (this.cbo구분_NGI.SelectedIndex == 0)
							{
								DataTable dt;

								dt = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GI_I", new object[] { this.cbo회사.SelectedValue.ToString(),
																								 dr["NO_GIR"].ToString(),
																								 dr["SEQ_GIR"].ToString(),
																								 dr["QT_WORK"].ToString() });

								zebra.NO_SEQ = dt.Rows[0]["NO_SEQ"];
							}

							zebra.Print(this.cbo회사.SelectedValue.ToString(), this._printer, this._isNetwork, this.chk선용라벨.Checked, this.chk로고표시.Checked);
							#endregion
						}
						else if (this.tabControl1.SelectedTab == this.tpg재고품입고)
						{
							#region 재고품입고
							if (Convert.ToInt32(dr["QT_WORK"]) <= 0)
							{
								this.ShowMessage("인쇄 수량을 확인하세요! (0 또는 음수값 존재)");
								return;
							}

							string itemCode = dr["CD_ITEM"].ToString();
							string itemName = dr["NM_ITEM"].ToString();

							if (!string.IsNullOrEmpty(itemCode) && itemCode.Length > 23)
								itemCode = itemCode.Substring(0, 23);

							if (!string.IsNullOrEmpty(itemName) && itemName.Length > 125)
								itemName = itemName.Substring(0, 125);

							zebra.LabelType = LabelType.GR_Stock;
							zebra.NO_DSP = dr["NO_DSP"];
							zebra.NO_REF = dr["NO_REF"];
							zebra.NO_FILE = dr["NO_PO"];
							zebra.NM_BUYER = dr["NM_BUYER"];
							zebra.NM_SUBJECT = dr["NM_SUBJECT"];
							zebra.NM_VESSEL = dr["NM_VESSEL"];
							zebra.CD_ITEM_PARTNER = itemCode;
							zebra.QT_WORK = dr["QT_WORK"];
							zebra.NM_ITEM_PARTNER = itemName;
							zebra.NO_PO = dr["NO_PO"];
							zebra.NO_LINE = dr["NO_LINE"];
							zebra.NO_SEQ = dr["NO_SEQ"];
							zebra.NM_SUPPLIER = dr["NM_SUPPLIER"];
							zebra.NM_EMP = dr["NM_EMP"];
							zebra.NO_ACTION = dr["NO_ORDER"];

							if (this.cbo구분_SGR.SelectedIndex == 0)
							{
								DataTable dt = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GR_I", new object[] { this.cbo회사.SelectedValue.ToString(),
																										   zebra.NO_PO,
																										   zebra.NO_LINE,
																										   zebra.QT_WORK,
																										   "N" });
								zebra.NO_SEQ = dt.Rows[0]["NO_SEQ"];
							}

							zebra.Print(this.cbo회사.SelectedValue.ToString(), this._printer, this._isNetwork, this.chk선용라벨.Checked, this.chk로고표시.Checked);
							#endregion
						}
						else if (this.tabControl1.SelectedTab == this.tpg재고품출고)
						{
							#region 재고품출고
							if (Convert.ToInt32(dr["QT_WORK"]) <= 0)
							{
								this.ShowMessage("인쇄 수량을 확인하세요! (0 또는 음수값 존재)");
								return;
							}

							string itemCode = dr["CD_ITEM_PARTNER"].ToString();
							string itemName = dr["NM_ITEM_PARTNER"].ToString();

							if (!string.IsNullOrEmpty(itemCode) && itemCode.Length > 23)
								itemCode = itemCode.Substring(0, 23);

							if (!string.IsNullOrEmpty(itemName) && itemName.Length > 125)
								itemName = itemName.Substring(0, 125);

							zebra.LabelType = LabelType.GI_Stock;
							zebra.NO_DSP = dr["NO_DSP"];
							zebra.NO_REF = dr["NO_REF"];
							zebra.NO_FILE = dr["NO_SO"];
							zebra.NM_BUYER = dr["NM_BUYER"];
							zebra.NM_SUBJECT = dr["NM_SUBJECT"];
							zebra.NM_VESSEL = dr["NM_VESSEL"];
							zebra.CD_ITEM_PARTNER = itemCode;
							zebra.QT_WORK = dr["QT_WORK"];
							zebra.NM_ITEM_PARTNER = itemName;
							zebra.NO_LINE = dr["SEQ_SO"];
							zebra.NO_SEQ = dr["NO_SEQ"];

							zebra.STOCK_CODE = dr["CD_ITEM"];
							zebra.CD_LOCATION = dr["CD_ZONE"];

							if (this.cbo구분_SGI.SelectedIndex == 0)
							{
								DataTable dt;

								dt = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GI_I", new object[] { this.cbo회사.SelectedValue.ToString(),
																								 dr["NO_GIREQ"].ToString(),
																								 dr["NO_LINE"].ToString(),
																								 dr["QT_WORK"].ToString() });

								zebra.NO_SEQ = dt.Rows[0]["NO_SEQ"];
							}

							zebra.Print(this.cbo회사.SelectedValue.ToString(), this._printer, this._isNetwork, this.chk선용라벨.Checked, this.chk로고표시.Checked);
							#endregion
						}
						
						dr.Delete();
					}

					if (this._isNetwork == true)
						zebra.Disconnect();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				gridL.Redraw = true;

				this.cbo회사.Enabled = true;
				this.cbo프린터.Enabled = true;
			}
		}
	}
}
