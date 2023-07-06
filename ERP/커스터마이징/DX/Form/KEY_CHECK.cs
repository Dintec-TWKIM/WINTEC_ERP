using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dintec;

namespace DX
{
	public partial class KEY_CHECK : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		
		public string[] 키워드
		{
			get; set;
		}

		public DataRow 아이템
		{
			get; set;
		}

		#endregion

		#region ==================================================================================================== Constructor

		public KEY_CHECK(string[] 키워드, DataRow 아이템)
		{
			InitializeComponent();

			this.키워드 = 키워드;
			this.아이템 = 아이템;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			//spc메인.SplitterDistance = 500;
			//spc재고.SplitterDistance = 200;

			spc메인.Panel2Collapsed = true;

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			Search();
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			//grd품목.BeginSetting(1, 1, false);
			   
			//grd품목.SetCol("CD_ITEM"		, "재고코드"		, 120	, TextAlignEnum.CenterCenter);
			//grd품목.SetCol("NM_ITEM"		, "재고명"		, 300);
			//grd품목.SetCol("DC_MODEL"	, "적용모델"		, 110);
			//grd품목.SetCol("UCODE"		, "U코드"		, 110);			
			//grd품목.SetCol("DC_RMK1"		, "추가정보1"		, 180);
			//grd품목.SetCol("DC_RMK2"		, "추가정보2"		, 180);
			//grd품목.SetCol("DC_RMK"		, "비고"			, false);
			   
			//grd품목.SetDefault("21.07.22.01", SumPositionEnum.None);
			//grd품목.SetAlternateRow();
			//grd품목.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			//btn품목검색.Click += Btn품목검색_Click;
			//grd품목.AfterRowChange += Grd품목_AfterRowChange;
		}

		private void Grd품목_AfterRowChange(object sender, RangeEventArgs e)
		{
			
		}

		private void Btn품목검색_Click(object sender, EventArgs e)
		{
			////throw new NotImplementedException();
			//string query = "SELECT CD_ITEM FROM MA_PITEM WHERE CD_COMPANY = 'K100' AND CD_ITEM = '" + tbx품목.Text + "'";
			//DataTable dt = SQL.GetDataTable(query);
			//grd품목.DataBind(dt);
		}

		#endregion

		#region ==================================================================================================== Search

		private void Search()
		{			
			// ********** 키워드 강조	
			string 주제		= 아이템["NM_SUBJECT"].ToString();
			string 품목코드	= 아이템["CD_ITEM_PARTNER"].ToString();
			string 품목명	= 아이템["NM_ITEM_PARTNER"].ToString();
			string 재고코드	= 아이템["CD_ITEM"].ToString();

			UTIL.키워드강조(키워드, ref 주제, ref 품목코드, ref 품목명, ref 재고코드);

			// ********** HTML
			string body = @"
<div>
	<table class='dx-viewbox2'>
		<tr>
			<th class='first'>키워드</th>
		</tr>
		<tr>
			<td>" + string.Join(" ", 키워드) + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>주제</th>
		</tr>
		<tr>
			<td>" + 주제.Trim().Replace("\n", "<br />") + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>품목코드</th>
		</tr>
		<tr>
			<td>" + 품목코드 + @"<br style='visibility:hidden'></td>
		 </tr>
		<tr>
			<th>품목명</th>
		</tr>
		<tr>
			<td>" + 품목명.Trim().Replace("\n", "<br />") + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>재고코드</th>
		</tr>
		<tr>
			<td class='last'>" + 재고코드 + @"<br style='visibility:hidden'></td>
		</tr>
	</table>
</div>";

			web아이템.SetDefault();
			web아이템.AddBody(body);
			
			//// 조회
			//SQL sql = new SQL("PS_CZ_SA_INQ_ITEM_LINK_SNGL", SQLType.Procedure, SQLDebug.Print);
			//sql.Parameter.Add2("@CD_COMPANY", CompanyCode);
			//sql.Parameter.Add2("@NO_FILE"	, FileNumber);
			//sql.Parameter.Add2("@NO_LINE"	, Item["NO_LINE"]);

			//DataTable dt = sql.GetDataTable();
			//grd재고.DataBind(dt);
		}

		#endregion
	}
}
