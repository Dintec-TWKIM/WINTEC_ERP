using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Dintec;
using System.Data.OleDb;

namespace cz
{
	public partial class P_CZ_SA_DEFERRED_DELIVERY_MNG : PageBase
	{
		private P_CZ_SA_DEFERRED_DELIVERY_MNG_BIZ _biz = new P_CZ_SA_DEFERRED_DELIVERY_MNG_BIZ();

		private string 입고상태
		{
			get
			{
				if (this.rdo입고.Checked == true) return "003";
				else if (this.rdo미입고.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo입고.Checked = true;
				else if (value == "002") this.rdo미입고.Checked = true;
				else this.rdo입고전체.Checked = true;
			}
		}

		private string 의뢰상태
		{
			get
			{
				if (this.rdo의뢰.Checked == true) return "003";
				else if (this.rdo미의뢰.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo의뢰.Checked = true;
				else if (value == "002") this.rdo미의뢰.Checked = true;
				else this.rdo의뢰전체.Checked = true;
			}
		}

		private string 출고상태
		{
			get
			{
				if (this.rdo출고.Checked == true) return "003";
				else if (this.rdo미출고.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo출고.Checked = true;
				else if (value == "002") this.rdo미출고.Checked = true;
				else this.rdo출고전체.Checked = true;
			}
		}

		private string 마감상태
		{
			get
			{
				if (this.rdo마감.Checked == true) return "003";
				else if (this.rdo미마감.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo마감.Checked = true;
				else if (value == "002") this.rdo미마감.Checked = true;
				else this.rdo마감전체.Checked = true;
			}
		}

		private enum 탭구분
		{
			수주기준파일,
			수주기준품목,
			발주기준파일,
			발주기준품목
		}

		public P_CZ_SA_DEFERRED_DELIVERY_MNG()
		{
			StartUp.Certify(this);
			this.InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitEvent();
		}

		private void InitEvent()
		{
			this._flex수주기준파일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex발주기준파일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);

			this._flex발주기준파일L.StartEdit += new RowColEventHandler(this._flex발주기준파일L_StartEdit);

			this._flex수주기준파일H.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
			this._flex발주기준파일H.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
			this._flex수주기준파일L.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
			this._flex발주기준파일L.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);

			this.bpc매출처그룹.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.btn발송옵션.Click += new EventHandler(this.btn발송옵션_Click);
            this.btn메일발송.Click += new EventHandler(this.btn메일발송_Click);
			this.btn재고납기업로드.Click += Btn재고납기업로드_Click;
		}

		private void Btn재고납기업로드_Click(object sender, EventArgs e)
		{
			OleDbConnection con = null;
			OpenFileDialog openFileDialog;
			DataTable dtSchema, dtExcel;
			OleDbCommand oconn;
			OleDbDataAdapter sda;

			try
			{
				openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";

				if (openFileDialog.ShowDialog() != DialogResult.OK) return;

				String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + openFileDialog.FileName + ";Extended Properties=Excel 12.0;";

				con = new OleDbConnection(constr);
				con.Open();

				dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				oconn = new OleDbCommand(@"Select * From [Sheet1$]", con);
				sda = new OleDbDataAdapter(oconn);
				dtExcel = new DataTable();

				sda.Fill(dtExcel);

				DataTable dtUpload = new DataTable();
				dtUpload.Columns.Add("NO_ORDER");
				dtUpload.Columns.Add("NO_LINE");
				dtUpload.Columns.Add("DT_EXPECT");

				foreach (DataRow dr in dtExcel.Rows)
				{
					DataRow newRow = dtUpload.NewRow();

					newRow["NO_ORDER"] = dr["공사번호"];
					newRow["NO_LINE"] = dr["딘텍순번"];

					DateTime dtExpect;

					if (DateTime.TryParse(dr["납기예정일"].ToString(), out dtExpect))
						newRow["DT_EXPECT"] = dtExpect.ToString("yyyyMMdd");
					else
						newRow["DT_EXPECT"] = string.Empty;

					dtUpload.Rows.Add(newRow);
				}

			    if (this._biz.SaveExcel(dtUpload))
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn재고납기업로드.Text });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				if (con != null) con.Close();
			}
		}

		private void SetHeaderGrid(FlexGrid grid)
		{
			grid.BeginSetting(1, 1, false);

			if (grid.Name == this._flex수주기준파일H.Name)
			{
				grid.SetCol("NO_KEY", "수주번호", 100);
				grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
				grid.SetCol("NO_ORDER", "매입처발주번호", 100);
				grid.SetCol("NM_PARTNER", "매출처", 100);
				grid.SetCol("NM_SUPPLIER", "매입처", 100);
				grid.SetCol("NM_VESSEL", "호선명", 100);
				grid.SetCol("NM_SALEGRP", "영업그룹", 100);
				grid.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
				grid.SetCol("NM_NATION", "국가", 100);
				grid.SetCol("NM_SO_EMP", "수주담당자", 80);
				grid.SetCol("NM_LOG_EMP", "물류담당자", 80);
				grid.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_LIMIT", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("AM_SO", "수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				grid.SetCol("QT_SO", "수주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_PO", "발주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_IN", "입고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_GIR", "의뢰종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_OUT", "출고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("YN_CLOSE", "마감여부", 40);
				grid.SetCol("YN_ALL_IN", "전량입고", 40, false, CheckTypeEnum.Y_N);
				grid.SetCol("DC_RMK_TEXT", "수주비고", 100);
				grid.SetCol("DC_RMK_TEXT2", "물류비고", 100, true);
				grid.SetCol("DT_EXPECT_IN", "예상입고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_EXPECT", "예상출고일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_REPLY", "회신일자", 0, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DC_RMK", "납기/출고지연 사유", 100, true);

				grid.SetCol("CD_DELAY", "지연유형", 100, true);

				grid.SetDataMap("CD_DELAY", DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, 
																						   NULL AS NAME 
																				    UNION ALL
																					SELECT CD_SYSDEF AS CODE,
																					       NM_SYSDEF AS NAME
																					FROM CZ_MA_CODEDTL WITH(NOLOCK)
																					WHERE CD_COMPANY = '{0}'
																					AND CD_FIELD = 'CZ_SA00072'", Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");

			

				grid.ExtendLastCol = true;
			}
			else if (grid.Name == this._flex수주기준품목H.Name)
			{
				grid.SetCol("NO_SO", "수주번호", 100);
				grid.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
				grid.SetCol("NO_ORDER", "매입처발주번호", 100);
				grid.SetCol("NM_PARTNER", "매출처", 100);
				grid.SetCol("NM_SUPPLIER", "매입처", 100);
				grid.SetCol("NM_VESSEL", "호선명", 100);
				grid.SetCol("NM_SALEGRP", "영업그룹", 100);
				grid.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
				grid.SetCol("NM_NATION", "국가", 100);
				grid.SetCol("NM_SO_EMP", "수주담당자", 80);
				grid.SetCol("NM_LOG_EMP", "물류담당자", 80);
				grid.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("NO_DSP", "순번", 80);
				grid.SetCol("SEQ_SO", "수주항번", 80);
				grid.SetCol("CD_ITEM", "품목코드", 100);
				grid.SetCol("NM_ITEM", "품목명", 120);
				grid.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
				grid.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
				grid.SetCol("CD_SPEC", "U코드(견적)", 100);
				grid.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
				grid.SetCol("STND_ITEM", "파트번호", 100);
				grid.SetCol("MAT_ITEM", "아이템번호", 100);
				grid.SetCol("DT_LIMIT", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_EXPECT", "예상입고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("AM_SO", "수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				grid.SetCol("QT_SO", "수주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_PO", "발주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_IN", "입고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_GIR", "의뢰종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_OUT", "출고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("YN_CLOSE", "마감여부", 40);
				grid.SetCol("YN_ALL_IN", "전량입고", 40, false, CheckTypeEnum.Y_N);
				grid.SetCol("MAT_ITEM", "아이템번호", 100);
				grid.SetCol("DC_RMK_LOG", "재고비고", 100);
			}
			else if (grid.Name == this._flex발주기준파일H.Name)
			{
                grid.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
				grid.SetCol("NO_SO", "수주번호", 100);
				grid.SetCol("NM_PARTNER", "매출처", 100);
				grid.SetCol("NM_VESSEL", "호선명", 100);
				grid.SetCol("NM_SALEGRP", "영업그룹", 100);
				grid.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
				grid.SetCol("NM_NATION", "국가", 100);
				grid.SetCol("NM_SO_EMP", "수주담당자", 80);
				grid.SetCol("NM_LOG_EMP", "물류담당자", 80);
				grid.SetCol("NO_KEY", "발주번호", 100);
				grid.SetCol("NM_PO_EMP", "발주담당자", 80);
				grid.SetCol("NM_SUPPLIER", "매입처", 100);
				grid.SetCol("NO_ORDER", "매입처발주번호", 100);
				grid.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_LIMIT", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				grid.SetCol("QT_SO", "수주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_PO", "발주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_IN", "입고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_GIR", "의뢰종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_OUT", "출고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                grid.SetCol("DT_IN_DAYS", "소요일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("YN_CLOSE", "마감여부", 40);
				grid.SetCol("YN_ALL_IN", "전량입고", 40, false, CheckTypeEnum.Y_N);
				grid.SetCol("DC_RMK_SO", "수주비고", 100);
				grid.SetCol("DC_RMK_TEXT2", "물류비고", 100, true);
				grid.SetCol("DT_EXPECT", "예상입고일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_REPLY", "회신일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DC_RMK", "납기/입고지연 사유", 100, true);
                grid.SetCol("TP_SEND", "발송유형", 80);
                grid.SetCol("DTS_SEND", "발송일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("CD_DELAY", "지연유형", 0, false);
				


				grid.Cols["DTS_SEND"].Format = "####/##/##/##:##:##";
                grid.SetDummyColumn("S");
				grid.ExtendLastCol = true;
			}
			else if (grid.Name == this._flex발주기준품목H.Name)
			{
				grid.SetCol("NO_SO", "수주번호", 100);
				grid.SetCol("NM_PARTNER", "매출처", 100);
				grid.SetCol("NM_VESSEL", "호선명", 100);
				grid.SetCol("NM_SALEGRP", "영업그룹", 100);
				grid.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
				grid.SetCol("NM_NATION", "국가", 100);
				grid.SetCol("NM_SO_EMP", "수주담당자", 80);
				grid.SetCol("NM_LOG_EMP", "물류담당자", 80);
				grid.SetCol("NO_PO", "발주번호", 100);
				grid.SetCol("NM_PO_EMP", "발주담당자", 80);
				grid.SetCol("NM_SUPPLIER", "매입처", 100);
				grid.SetCol("NO_ORDER", "매입처발주번호", 100);
				grid.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("NO_DSP", "순번", 80);
				grid.SetCol("NO_LINE", "발주항번", 80);
				grid.SetCol("CD_ITEM", "품목코드", 100);
				grid.SetCol("NM_ITEM", "품목명", 120);
				grid.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
				grid.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
				grid.SetCol("CD_SPEC", "U코드(견적)", 100);
				grid.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
				grid.SetCol("STND_ITEM", "파트번호", 100);
				grid.SetCol("MAT_ITEM", "아이템번호", 100);
				grid.SetCol("DT_LIMIT", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_EXPECT", "예상입고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				grid.SetCol("QT_SO", "수주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_PO", "발주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_IN", "입고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_GIR", "의뢰종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("QT_OUT", "출고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                grid.SetCol("DT_IN_DAYS", "소요일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				grid.SetCol("YN_CLOSE", "마감여부", 40);
				grid.SetCol("YN_ALL_IN", "전량입고", 40, false, CheckTypeEnum.Y_N);
				grid.SetCol("DC_RMK_LOG", "재고비고", 100);
			}

			//grid.SettingVersion = "1.0.0.1";
			//grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			grid.SetDefault("1.0.0.0", SumPositionEnum.None);

			grid.SetExceptSumCol("DT_DELAY", "DT_IN_DAYS");
		}

		private void SetLineGrid(FlexGrid grid)
		{
			grid.BeginSetting(1, 1, false);

			grid.SetCol("NO_LINE", "항번", 40);
			grid.SetCol("NO_DSP", "순번", 40);
			grid.SetCol("CD_ITEM", "품목코드", 100);
			grid.SetCol("NM_ITEM", "품목명", 120);
			grid.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			grid.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			grid.SetCol("CD_SPEC", "U코드(견적)", 100);
			grid.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
			grid.SetCol("STND_ITEM", "파트번호", 100);
			grid.SetCol("MAT_ITEM", "아이템번호", 100);
			
            if (grid.Name == this._flex발주기준파일L.Name)
            {
                grid.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("DT_LIMIT", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            }
            
            grid.SetCol("QT_SO", "수주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			grid.SetCol("QT_PO", "발주종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			grid.SetCol("QT_IN", "입고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			grid.SetCol("QT_GIR", "의뢰종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			grid.SetCol("QT_OUT", "출고종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			
			if (grid.Name == this._flex수주기준파일L.Name)
			{
				grid.SetCol("NM_SUPPLIER", "매입처", 100);
				grid.SetCol("NO_ORDER", "매입처발주번호", 100);
				grid.SetCol("DT_LIMIT_PO", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				grid.SetCol("DT_DELAY", "지연일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			}
            else if (grid.Name == this._flex발주기준파일L.Name)
                grid.SetCol("DT_IN_DAYS", "소요일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            
			grid.SetCol("DT_EXPECT", "예상입고일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("YN_CLOSE", "마감여부", 40);
			grid.SetCol("YN_ALL_IN", "전량입고", 40, false, CheckTypeEnum.Y_N);
			grid.SetCol("DC_RMK_LOG", "재고비고", 100, true);

			//grid.SettingVersion = "1.0.0.1";
			//grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			grid.SetDefault("1.0.0.0", SumPositionEnum.None);

			grid.SetExceptSumCol("DT_DELAY", "DT_IN_DAYS");
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp수주일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
			this.dtp수주일자.EndDateToString = Global.MainFrame.GetStringToday;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt = null;
			object[] obj;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
									 this.dtp수주일자.StartDateToString,
									 this.dtp수주일자.EndDateToString,
									 this.txt수주번호.Text,
									 this.입고상태,
									 this.의뢰상태,
									 this.출고상태,
									 this.마감상태,
									 this.ctx호선.CodeValue,
									 this.ctx매출처.CodeValue,
									 this.bpc매입처.QueryWhereIn_Pipe,
									 this.txt매입처발주번호.Text,
									 this.bpc영업그룹.QueryWhereIn_Pipe,
									 this.bpc매출처그룹.QueryWhereIn_Pipe,
									 this.cur지연일수.DecimalValue,
									 this.cbx물류담당자.CodeValue };

				switch ((탭구분)this.tabControl.SelectedIndex)
				{
					case 탭구분.수주기준파일:
						dt = this._biz.Search(obj);

						if (this._flex수주기준파일H.DataTable == null)
						{
							this._flex수주기준파일H.DetailGrids = new FlexGrid[] { this._flex수주기준파일L };

							this.SetHeaderGrid(this._flex수주기준파일H);
							this.SetLineGrid(this._flex수주기준파일L);
						}

						this._flex수주기준파일H.Binding = dt;

						if (this.chk셀병합.Checked == true)
							this.셀병합(this._flex수주기준파일H);
						break;
					case 탭구분.수주기준품목:
						dt = this._biz.Search1(obj);

						if (this._flex수주기준품목H.DataTable == null)
							this.SetHeaderGrid(this._flex수주기준품목H);

						this._flex수주기준품목H.Binding = dt;

						if (this.chk셀병합.Checked == true)
							this.셀병합(this._flex수주기준품목H);
						break;
					case 탭구분.발주기준파일:
						dt = this._biz.Search2(obj);

						if (this._flex발주기준파일H.DataTable == null)
						{
							this._flex발주기준파일H.DetailGrids = new FlexGrid[] { this._flex발주기준파일L };

							this.SetHeaderGrid(this._flex발주기준파일H);
							this.SetLineGrid(this._flex발주기준파일L);
						}

						this._flex발주기준파일H.Binding = dt;

						if (this.chk셀병합.Checked == true)
							this.셀병합(this._flex발주기준파일H);
						break;
					case 탭구분.발주기준품목:
						dt = this._biz.Search3(obj);

						if (this._flex발주기준품목H.DataTable == null)
							this.SetHeaderGrid(this._flex발주기준품목H);

						this._flex발주기준품목H.Binding = dt;

						if (this.chk셀병합.Checked == true)
							this.셀병합(this._flex발주기준품목H);
						break;
				}

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, string.Empty);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string key, key1, key2, filter;
			FlexGrid flex;

			try
			{
				flex = ((FlexGrid)sender);

				key = flex["NO_SO"].ToString();
				key1 = flex["NO_KEY"].ToString();
				key2 = flex["DT_LIMIT"].ToString();
				filter = "NO_SO = '" + key + "' AND NO_KEY = '" + key1 + "' AND DT_LIMIT = '" + key2 + "'";

				if (flex.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   this.tabControl.SelectedIndex.ToString(),                                
															   key,
															   key1,
															   key2 });
				}

				flex.DetailGrids[0].BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void _flex발주기준파일L_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (this._flex발주기준파일L.Cols[e.Col].Name)
				{
					case "DT_EXPECT":
                        if (this._flex발주기준파일L["DT_EXPECT"].Equals("STOCK"))
						{
							ShowMessage("재고 입고예정일은 수주기준(파일)에 입력되어야 합니다.");
							e.Cancel = true;
						}
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;
				//예상출고일, 회신일자, 물류비고, 납기/출고지연사유, 재고비고, 지연유형
				if (flexGrid.Cols[e.Col].Name != "DT_EXPECT" &&
					flexGrid.Cols[e.Col].Name != "DT_REPLY" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK_TEXT2" && 
					flexGrid.Cols[e.Col].Name != "DC_RMK" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK_LOG" &&
					flexGrid.Cols[e.Col].Name != "CD_DELAY") return;

				if (flexGrid.Name == this._flex수주기준파일H.Name || 
					flexGrid.Name == this._flex발주기준파일H.Name)
				{
					this._biz.SaveDataHeader(this.tabControl.SelectedIndex.ToString(), flexGrid.GetChanges());
				}
				else if (flexGrid.Name == this._flex수주기준파일L.Name ||
						 flexGrid.Name == this._flex발주기준파일L.Name)
				{
					this._biz.SaveDataLine(this.tabControl.SelectedIndex.ToString(), flexGrid.GetChanges());
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
		}

		private void 셀병합(FlexGrid grid)
		{
			CellRange cellRange;
			string key;

			try
			{
				if (grid.HasNormalRow == false) return;

				grid.Redraw = false;

				for (int row = grid.Rows.Fixed; row < grid.Rows.Count; row++)
				{
					if (grid.Name == this._flex수주기준파일H.Name || 
						grid.Name == this._flex발주기준파일H.Name)
						key = D.GetString(grid.GetData(row, "NO_SO")) + "-" + D.GetString(grid.GetData(row, "NO_KEY"));
					else if (grid.Name == this._flex수주기준품목H.Name)
						key = D.GetString(grid.GetData(row, "NO_SO"));
					else
						key = D.GetString(grid.GetData(row, "NO_SO")) + "-" + D.GetString(grid.GetData(row, "NO_PO"));
					
					foreach (Column column in grid.Cols)
					{
						if (grid.Name == this._flex수주기준파일H.Name)
						{
							#region 수주기준(파일)
							switch (column.Name)
							{
								case "DT_LIMIT":
								case "AM_SO":
								case "QT_SO":
								case "QT_PO":
								case "QT_IN":
								case "QT_GIR":
								case "QT_OUT":
								case "DT_DELAY":
								case "YN_ALL_IN":
								case "DT_EXPECT_IN":
								case "DT_EXPECT":
								case "DT_REPLY":
								case "DC_RMK":
								case "CD_DELAY":
									continue;
							}
							#endregion
						}
						else if (grid.Name == this._flex수주기준품목H.Name)
						{
							#region 수주기준(품목)
							switch (column.Name)
							{
								case "NO_DSP":
								case "SEQ_SO":
								case "CD_ITEM":
								case "NM_ITEM":
								case "CD_ITEM_PARTNER":
								case "NM_ITEM_PARTNER":
								case "CD_SPEC":
								case "STND_DETAIL_ITEM":
								case "STND_ITEM":
								case "MAT_ITEM":
								case "DT_LIMIT":
								case "AM_SO":
								case "QT_SO":
								case "QT_PO":
								case "QT_IN":
								case "QT_GIR":
								case "QT_OUT":
								case "DT_EXPECT":
								case "DT_DELAY":
								case "YN_ALL_IN":
									continue;
							}
							#endregion
						}
						else if (grid.Name == this._flex발주기준파일H.Name)
						{
							#region 발주기준(파일)
							switch (column.Name)
							{
								case "DT_LIMIT":
                                case "DT_IN":
								case "AM_PO":
								case "QT_SO":
								case "QT_PO":
								case "QT_IN":
								case "QT_GIR":
								case "QT_OUT":
								case "DT_DELAY":
                                case "DT_IN_DAYS":
								case "YN_ALL_IN":
								case "DT_EXPECT":
								case "DT_REPLY":
								case "DC_RMK":
                                case "TP_SEND":
                                case "DTS_SEND":
								case "CD_DELAY":
									continue;
							}
							#endregion
						}
						else if (grid.Name == this._flex발주기준품목H.Name)
						{
							#region 발주기준(품목)
							switch (column.Name)
							{
								case "NO_DSP":
								case "NO_LINE":
								case "CD_ITEM":
								case "NM_ITEM":
								case "CD_ITEM_PARTNER":
								case "NM_ITEM_PARTNER":
								case "CD_SPEC":
								case "STND_DETAIL_ITEM":
								case "STND_ITEM":
								case "MAT_ITEM":
								case "DT_LIMIT":
                                case "DT_IN":
								case "AM_PO":
								case "QT_SO":
								case "QT_PO":
								case "QT_IN":
								case "QT_GIR":
								case "QT_OUT":
								case "DT_EXPECT":
								case "DT_DELAY":
                                case "DT_IN_DAYS":
								case "YN_ALL_IN":
									continue;
							}
							#endregion
						}

						cellRange = grid.GetCellRange(row, column.Name, row, column.Name);
						cellRange.UserData = key + "_" + column.Name;
					}
				}

				grid.DoMerge();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				grid.Redraw = true;
			}
		}

        private void btn발송옵션_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_SA_DEFERRED_DELIVERY_MNG_SUB dialog = new P_CZ_SA_DEFERRED_DELIVERY_MNG_SUB();
                dialog.ShowDialog();
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
        }

        private void btn메일발송_Click(object sender, EventArgs e)
        {
			string 보내는사람, 받는사람, 참조, 제목, 본문; //첨부파일;
            string 수주번호, 매입처코드, 매입처명;
            string 메일주소, 회사명, 담당자명, 직급, 내선번호, 팩스번호, 홈페이지;
            DataTable 사원정보;
            DataRow[] dataRowArray;
            P_CZ_MA_EMAIL_SUB dialog;

            try
            {
                if (!this._flex발주기준파일H.HasNormalRow) return;

                dataRowArray = this._flex발주기준파일H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage("선택된 항목이 없습니다.");
                    return;
                }
                else
                {
                    if (Settings.Default.자동발송여부 && this.ShowMessage("※경고 : 담당자 확인 없이 매입처로 메일이 바로 발송 됩니다.\n진행 하시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return;

                    사원정보 = DBHelper.GetDataTable(@"SELECT (CASE WHEN ME.CD_COMPANY = 'TEST' THEN '딘텍'
                                                       			    WHEN ME.CD_COMPANY = 'K100' THEN '딘텍'
                                                       	            WHEN ME.CD_COMPANY = 'K200' THEN '두베코'
                                                       			    WHEN ME.CD_COMPANY = 'S100' THEN 'DINTEC SINGAPORE' END) AS NM_COMPANY,
                                                       	      ME.NM_KOR,
                                                       	      MC.CD_FLAG2 AS NM_DUTY_RANK,
                                                       	      ME.NO_EMAIL,
                                                       	      ME.NO_TEL,
                                                       	      ME.DC_RMK1 AS NO_FAX,
                                                       	      (CASE WHEN ME.CD_COMPANY = 'TEST' THEN 'www.dintec.co.kr'
                                                       		        WHEN ME.CD_COMPANY = 'K100' THEN 'www.dintec.co.kr'
                                                       	            WHEN ME.CD_COMPANY = 'K200' THEN 'www.dubheco.com'
                                                       		        WHEN ME.CD_COMPANY = 'S100' THEN 'www.dintec.co.kr' END) AS HOMEPAGE
                                                       FROM MA_EMP ME WITH(NOLOCK)
                                                       LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_FIELD = 'HR_H000002' AND MC.CD_SYSDEF = ME.CD_DUTY_RANK
                                                       WHERE ME.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                      "AND ME.NO_EMP = '" + Global.MainFrame.LoginInfo.EmployeeNo + "'");

                    if (사원정보 == null || 사원정보.Rows.Count == 0)
                    {
                        this.ShowMessage("사원정보가 없습니다.");
                        return;
                    }
                    else
                    {
                        DataRow 담당자정보 = 사원정보.Rows[0];

                        메일주소 = 담당자정보["NO_EMAIL"].ToString();
                        회사명 = 담당자정보["NM_COMPANY"].ToString();
                        담당자명 = 담당자정보["NM_KOR"].ToString();
                        직급 = 담당자정보["NM_DUTY_RANK"].ToString();
                        내선번호 = 담당자정보["NO_TEL"].ToString();
                        팩스번호 = 담당자정보["NO_FAX"].ToString();
                        홈페이지 = 담당자정보["HOMEPAGE"].ToString();
                    }

                    보내는사람 = 메일주소 + "/" + 회사명 + " " + 담당자명;
                    참조 = 메일주소;

                    int index = 0;

                    foreach (DataRow dr in dataRowArray)
                    {
                        if (Settings.Default.자동발송여부)
                            MsgControl.ShowMsg("메일을 발송 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

						DataTable dt = DBHelper.GetDataTable(@"SELECT FP.NM_EMAIL 
												               FROM PU_POH PH WITH(NOLOCK)
											                   LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
												               LEFT JOIN FI_PARTNERPTR	FP ON FP.CD_COMPANY = PH.CD_COMPANY AND FP.CD_PARTNER = PH.CD_PARTNER
												               WHERE PH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
															 @"AND PH.NO_PO = '" + dr["NO_KEY"].ToString() + "'" + Environment.NewLine +
											                  "AND FP.TP_PTR = '007'");

						if (dt != null && dt.Rows.Count > 0)
						{
							받는사람 = string.Empty;

							foreach (DataRow dr1 in dt.Rows)
							{
								받는사람 += dr1["NM_EMAIL"].ToString() + ";";
							}
						}
						else
						{
							받는사람 = DBHelper.ExecuteScalar(@"SELECT ISNULL(FP.NM_EMAIL, MP.E_MAIL) AS MAIL 
                                                                FROM PU_POH PH WITH(NOLOCK)
                                                                LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
                                                                LEFT JOIN FI_PARTNERPTR	FP ON FP.CD_COMPANY = PH.CD_COMPANY AND FP.CD_PARTNER = PH.CD_PARTNER AND FP.SEQ = PH.SEQ_ATTN
                                                                WHERE PH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
															   "AND PH.NO_PO = '" + dr["NO_KEY"].ToString() + "'").ToString();
						}

                        수주번호 = dr["NO_SO"].ToString();
                        매입처코드 = dr["CD_SUPPLIER"].ToString();
                        매입처명 = dr["NM_SUPPLIER"].ToString();

                        제목 = "[" + 회사명 + "] " + 수주번호 + " " + Settings.Default.메일유형;

                        본문 = string.Empty;

                        if (Settings.Default.발송자표시)
                            본문 += "수신 : " + 매입처명 + " 담당자님" + Environment.NewLine +
                                    "발신 : " + 담당자명 + " " + 직급 + "/" + 회사명 + Environment.NewLine + Environment.NewLine;

                        if (Settings.Default.자동발송문구)
                            본문 += "** 본 메일은 자동으로 발송되는 메일입니다." + Environment.NewLine + Environment.NewLine;

                        if (string.IsNullOrEmpty(Settings.Default.메일내용))
                        {
                            if (Settings.Default.메일유형 == "납기문의")
                            {
                                본문 += "안녕하십니까 ? " + 회사명 + " " + 담당자명 + " 입니다." + Environment.NewLine + Environment.NewLine +
                                        수주번호 + " 건 납기가 지연되고 있습니다." + Environment.NewLine +
                                        "예상납기일 회신 부탁 드립니다." + Environment.NewLine + Environment.NewLine +
                                        "감사합니다.";
                            }
                            else
                            {
                                본문 += "안녕하십니까 ? " + 회사명 + " " + 담당자명 + " 입니다." + Environment.NewLine + Environment.NewLine +
                                        수주번호 + " 건 납기가 도래하고 있습니다." + Environment.NewLine +
                                        "납기 내 입고 될 수 있도록 확인 부탁 드립니다." + Environment.NewLine + Environment.NewLine +
                                        "감사합니다.";
                            }
                        }
                        else
                            본문 += Settings.Default.메일내용;

                        if (Settings.Default.서명표시)
                            본문 += Environment.NewLine + Environment.NewLine +
                                    담당자명 + " " + 직급 + "/" + 회사명 + Environment.NewLine +
                                    "TEL : " + 내선번호 + Environment.NewLine +
                                    "FAX : " + 팩스번호 + Environment.NewLine +
                                    "E-MAIL : " + 메일주소 + Environment.NewLine +
                                    "HOMEPAGE : " + 홈페이지;

						
						if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
							dialog = new P_CZ_MA_EMAIL_SUB(보내는사람, 받는사람, 참조, string.Empty, 제목, null, null, 본문, 수주번호, 매입처코드, Settings.Default.자동발송여부);
						else
						{
							object temp = DBHelper.ExecuteScalar(@"SELECT TOP 1 WL.NM_FILE_REAL 
																   FROM CZ_MA_WORKFLOWL WL WITH(NOLOCK) 
																   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																  "AND TP_STEP = '10'" + Environment.NewLine +
																  "AND NO_KEY_REF = '" + dr["NO_KEY"].ToString() + "'" + Environment.NewLine +
																  "ORDER BY NO_LINE DESC");

							if (temp != null)
								dialog = new P_CZ_MA_EMAIL_SUB(보내는사람, 받는사람, 참조, string.Empty, 제목, new string[] { temp.ToString() }, null, 본문, 수주번호, 매입처코드, Settings.Default.자동발송여부);
							else
								dialog = new P_CZ_MA_EMAIL_SUB(보내는사람, 받는사람, 참조, string.Empty, 제목, null, null, 본문, 수주번호, 매입처코드, Settings.Default.자동발송여부);
						}
						
						if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            dr["TP_SEND"] = Settings.Default.메일유형;
                            dr["DTS_SEND"] = Global.MainFrame.GetStringDetailToday;
                        }
                    }

                    this._biz.SaveDataHeader(this.tabControl.SelectedIndex.ToString(), this._flex발주기준파일H.GetChanges());
                }   
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
            finally
            {
                MsgControl.CloseMsg();
            }
        }
	}
}