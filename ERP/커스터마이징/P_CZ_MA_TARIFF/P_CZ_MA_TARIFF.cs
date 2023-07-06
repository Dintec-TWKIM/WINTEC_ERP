using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_MA_TARIFF : PageBase
    {
		P_CZ_MA_TARIFF_BIZ _biz = new P_CZ_MA_TARIFF_BIZ();

        public P_CZ_MA_TARIFF()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

			this.InitGrid();
			this.InitEvent();
        }

		private void InitGrid()
        {
			this.MainGrids = new FlexGrid[] { this._flex목공포장H, this._flex목공포장L, this._flexDHL기본요금H, this._flexDHL기본요금L, this._flexDHL청구요금H };
			this._flex목공포장H.DetailGrids = new FlexGrid[] { this._flex목공포장L };
			this._flexDHL기본요금H.DetailGrids = new FlexGrid[] { this._flexDHL기본요금L };
			this._flexDHL청구요금H.DetailGrids = new FlexGrid[] { this._flexDHL청구요금L1 };

			#region 목공포장

			#region Header
			this._flex목공포장H.BeginSetting(1, 1, false);

			this._flex목공포장H.SetCol("YN_USE", "사용유무", 60, true, CheckTypeEnum.Y_N);
			this._flex목공포장H.SetCol("CD_PARTNER", "거래처코드", 100, true);
			this._flex목공포장H.SetCol("LN_PARTNER", "거래처명", 100);

			this._flex목공포장H.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

			this._flex목공포장H.SettingVersion = "0.0.0.1";
			this._flex목공포장H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flex목공포장L.BeginSetting(1, 1, false);

			this._flex목공포장L.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex목공포장L.SetCol("SIZE", "사이즈(CBM)", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex목공포장L.SetCol("SEQ", "순번", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex목공포장L.SetCol("DT_START", "시작일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex목공포장L.SetCol("DT_END", "종료일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex목공포장L.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			this._flex목공포장L.SetDummyColumn(new string[] { "S" });

			this._flex목공포장L.SettingVersion = "0.0.0.1";
			this._flex목공포장L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#endregion

			#region DHL (항송)

			#region 기본요금

			#region Header
			this._flexDHL기본요금H.BeginSetting(1, 1, false);

			this._flexDHL기본요금H.SetCol("DT_YEAR", "적용년도", 100, true);
			this._flexDHL기본요금H.SetCol("TP_TARIFF", "요금유형", 100, true);

			this._flexDHL기본요금H.SettingVersion = "0.0.0.1";
			this._flexDHL기본요금H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flexDHL기본요금H.SetDataMap("TP_TARIFF", MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "일반", "할인" }), "CODE", "NAME");


			#endregion

			#region Line
			this._flexDHL기본요금L.BeginSetting(1, 1, false);

			this._flexDHL기본요금L.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexDHL기본요금L.SetCol("WEIGHT", "무게", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE1", "Zone1", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE2", "Zone2", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE3", "Zone3", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE4", "Zone4", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE5", "Zone5", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE6", "Zone6", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE7", "Zone7", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE8", "Zone8", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL기본요금L.SetCol("AM_ZONE9", "Zone9", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			this._flexDHL기본요금L.SetDummyColumn(new string[] { "S" });

			this._flexDHL기본요금L.SettingVersion = "0.0.0.1";
			this._flexDHL기본요금L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#endregion

			#region 청구요금

			#region Header
			this._flexDHL청구요금H.BeginSetting(1, 1, false);

			this._flexDHL청구요금H.SetCol("DT_MONTH", "등록년월", 100, true, typeof(string), FormatTpType.YEAR_MONTH);
			this._flexDHL청구요금H.SetCol("RT_FSC", "유류할증료", 100, true, typeof(decimal), FormatTpType.RATE);

			this._flexDHL청구요금H.SettingVersion = "0.0.0.1";
			this._flexDHL청구요금H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flexDHL청구요금L1.BeginSetting(1, 1, false);

			this._flexDHL청구요금L1.SetCol("WEIGHT", "무게", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE1", "Zone1", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE2", "Zone2", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE3", "Zone3", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE4", "Zone4", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE5", "Zone5", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE6", "Zone6", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE7", "Zone7", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE8", "Zone8", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L1.SetCol("AM_ZONE9", "Zone9", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			this._flexDHL청구요금L1.SettingVersion = "0.0.0.1";
			this._flexDHL청구요금L1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flexDHL청구요금L2.BeginSetting(1, 1, false);

			this._flexDHL청구요금L2.SetCol("WEIGHT", "무게", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE1", "Zone1", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE2", "Zone2", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE3", "Zone3", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE4", "Zone4", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE5", "Zone5", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE6", "Zone6", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE7", "Zone7", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE8", "Zone8", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexDHL청구요금L2.SetCol("AM_ZONE9", "Zone9", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			this._flexDHL청구요금L2.SettingVersion = "0.0.0.1";
			this._flexDHL청구요금L2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#endregion

			#endregion
		}

		private void InitEvent()
        {
            this._flex목공포장H.AfterRowChange += _flex목공포장H_AfterRowChange;
            this._flexDHL기본요금H.AfterRowChange += _flexDHL기본요금H_AfterRowChange;
            this._flexDHL청구요금H.AfterRowChange += _flexDHL청구요금H_AfterRowChange;

            this.btn목공포장추가.Click += btn목공포장추가_Click;
            this.btn목공포장삭제.Click += btn목공포장삭제_Click;

            this.btnDHL기본요금추가.Click += BtnDHL기본요금추가_Click;
            this.btnDHL기본요금삭제.Click += BtnDHL기본요금삭제_Click;
            this.btn엑셀양식다운로드.Click += Btn엑셀양식다운로드_Click;
            this.btn엑셀업로드.Click += Btn엑셀업로드_Click;
		}
		private void Btn엑셀양식다운로드_Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;
			string localPath = string.Empty,
				   serverPath = string.Empty;

			try
			{
				if (!this._flexDHL기본요금H.HasNormalRow) return;

				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_외주가격표등록_DHL_" + Global.MainFrame.GetStringToday + ".xlsx";
				serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_TARIFF_DHL.xlsx";

				System.Net.WebClient client = new System.Net.WebClient();
				client.DownloadFile(serverPath, localPath);

				this.ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				if (conn != null) conn.Close();
			}
		}

		private void Btn엑셀업로드_Click(object sender, EventArgs e)
        {
			OpenFileDialog fileDlg = new OpenFileDialog();
			DataRow dataRow;
			int index;

			try
			{
				if (!this._flexDHL기본요금H.HasNormalRow) return;

				#region btn엑셀업로드
				fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

				if (fileDlg.ShowDialog() != DialogResult.OK) return;

				this._flexDHL기본요금L.Redraw = false;

				String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileDlg.FileName + ";Extended Properties=Excel 12.0;";

				OleDbConnection con = new OleDbConnection(constr);
				con.Open();

				DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
				OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
				DataTable dtExcel = new DataTable();

				sda.Fill(dtExcel);
				con.Close();

				for (int i = 0; i < 2; i++)
				{
					dtExcel.Rows[i].Delete();
				}

				dtExcel.AcceptChanges();

				// 필요한 컬럼 존재 유무 파악
				string[] argsPk, argsPkNm;

				argsPk = new string[] { "WEIGHT", "AM_ZONE1", "AM_ZONE2", "AM_ZONE3", "AM_ZONE4", "AM_ZONE5", "AM_ZONE6", "AM_ZONE7", "AM_ZONE8", "AM_ZONE9" };
				argsPkNm = new string[] { "무게", "Zone1", "Zone2", "Zone3", "Zone4", "Zone5", "Zone6", "Zone7", "Zone8", "Zone9" };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

				// 데이터 읽으면서 해당하는 값 셋팅
				index = 0;
				foreach (DataRow dr in dtExcel.Rows)
				{
					MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { (++index).ToString(), dtExcel.Rows.Count.ToString() });

					dataRow = this._flexDHL기본요금L.DataTable.NewRow();

					dataRow["DT_YEAR"] = this._flexDHL기본요금H["DT_YEAR"].ToString();
					dataRow["TP_TARIFF"] = this._flexDHL기본요금H["TP_TARIFF"].ToString();
					dataRow["WEIGHT"] = Convert.ToDecimal(dr["WEIGHT"]);
					dataRow["AM_ZONE1"] = Convert.ToDecimal(dr["AM_ZONE1"]);
					dataRow["AM_ZONE2"] = Convert.ToDecimal(dr["AM_ZONE2"]);
					dataRow["AM_ZONE3"] = Convert.ToDecimal(dr["AM_ZONE3"]);
					dataRow["AM_ZONE4"] = Convert.ToDecimal(dr["AM_ZONE4"]);
					dataRow["AM_ZONE5"] = Convert.ToDecimal(dr["AM_ZONE5"]);
					dataRow["AM_ZONE6"] = Convert.ToDecimal(dr["AM_ZONE6"]);
					dataRow["AM_ZONE7"] = Convert.ToDecimal(dr["AM_ZONE7"]);
					dataRow["AM_ZONE8"] = Convert.ToDecimal(dr["AM_ZONE8"]);
					dataRow["AM_ZONE9"] = Convert.ToDecimal(dr["AM_ZONE9"]);

					this._flexDHL기본요금L.DataTable.Rows.Add(dataRow);
				}
				
				MsgControl.CloseMsg();

				if (this._flexDHL기본요금L.HasNormalRow)
				{
					this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
					this.ToolBarSaveButtonEnabled = true;
				}
				else
				{
					this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
				}

				this._flexDHL기본요금L.Redraw = true;
				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexDHL기본요금L.Redraw = true;
				MsgControl.CloseMsg();
			}
		}

        private void BtnDHL기본요금추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flexDHL기본요금L.Rows.Add();
				this._flexDHL기본요금L.Row = this._flexDHL기본요금L.Rows.Count - 1;

				this._flexDHL기본요금L["DT_YEAR"] = this._flexDHL기본요금H["DT_YEAR"].ToString();
				this._flexDHL기본요금L["TP_TARIFF"] = this._flexDHL기본요금H["TP_TARIFF"].ToString();

				this._flexDHL기본요금L.Col = this._flexDHL기본요금L.Cols.Fixed;
				this._flexDHL기본요금L.AddFinished();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnDHL기본요금삭제_Click(object sender, EventArgs e)
		{
			try
			{
				DataRow[] dataRowArray;

				dataRowArray = this._flexDHL기본요금L.DataTable.Select("S = 'Y'");

				if (dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					this._flexDHL기본요금L.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						dr.Delete();
					}

					this._flexDHL기본요금L.Redraw = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
            {
				this._flexDHL기본요금L.Redraw = true;
			}
		}

        protected override void InitPaint()
		{
			base.InitPaint();
		}

		private void btn목공포장추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex목공포장L.Rows.Add();
				this._flex목공포장L.Row = this._flex목공포장L.Rows.Count - 1;

				this._flex목공포장L["CD_PARTNER"] = this._flex목공포장H["CD_PARTNER"].ToString();
				this._flex목공포장L["DT_START"] = Global.MainFrame.GetStringToday;
				this._flex목공포장L["DT_END"] = "99991231";

				this._flex목공포장L.Col = this._flex목공포장L.Cols.Fixed;
				this._flex목공포장L.AddFinished();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn목공포장삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				dataRowArray = this._flex목공포장L.DataTable.Select("S = 'Y'");

				if (dataRowArray.Length == 0)
                {
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
                {
					foreach(DataRow dr in dataRowArray)
                    {
						dr.Delete();
                    }
                }
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex목공포장H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string key, filter;

			try
			{
				dt = null;
				key = this._flex목공포장H["CD_PARTNER"].ToString();
				filter = "CD_PARTNER = '" + key + "'";

				if (this._flex목공포장H.DetailQueryNeed == true)
				{
					dt = this._biz.Search목공포장L(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															      key });
				}

				this._flex목공포장L.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexDHL기본요금H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string key, key1, filter;

			try
			{
				dt = null;
				key = this._flexDHL기본요금H["DT_YEAR"].ToString();
				key1 = this._flexDHL기본요금H["TP_TARIFF"].ToString();
				filter = "DT_YEAR = '" + key + "' AND TP_TARIFF = '" + key1 + "'";

				if (this._flexDHL기본요금H.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDHL기본요금L(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	 key,
																	 key1 });
				}

				this._flexDHL기본요금L.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexDHL청구요금H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt, dt1;
			string key, filter;

			try
			{
				dt = null;
				dt1 = null;
				key = this._flexDHL청구요금H["DT_MONTH"].ToString();
				filter = "DT_MONTH = '" + key + "'";

				if (this._flexDHL청구요금H.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDHL청구요금L1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																      key });

					dt1 = this._biz.SearchDHL청구요금L2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	   key });
				}

				this._flexDHL청구요금L1.BindingAdd(dt, filter);
				this._flexDHL청구요금L2.BindingAdd(dt1, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				if (this.tabControl1.SelectedIndex == 0)
                {
					this._flex목공포장H.Binding = this._biz.Search목공포장H(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   this.ctx거래처.CodeValue });

					if (!this._flex목공포장H.HasNormalRow)
					{
						ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					}
				}
				else
                {
					if (this.tabControl2.SelectedIndex == 0)
                    {
						this._flexDHL기본요금H.Binding = this._biz.SearchDHL기본요금H(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

						if (!this._flexDHL기본요금H.HasNormalRow)
						{
							ShowMessage(공통메세지.조건에해당하는내용이없습니다);
						}
					}
					else
                    {
						this._flexDHL청구요금H.Binding = this._biz.SearchDHL청구요금H(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

						if (!this._flexDHL청구요금H.HasNormalRow)
						{
							ShowMessage(공통메세지.조건에해당하는내용이없습니다);
						}
					}
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!this.BeforeAdd()) return;

				if (this.tabControl1.SelectedIndex == 0)
                {
					this._flex목공포장H.Rows.Add();
					this._flex목공포장H.Row = this._flex목공포장H.Rows.Count - 1;

					this._flex목공포장H.Col = this._flex목공포장H.Cols.Fixed;
					this._flex목공포장H.AddFinished();
				}
				else
                {
					if (this.tabControl2.SelectedIndex == 0)
                    {
						this._flexDHL기본요금H.Rows.Add();
						this._flexDHL기본요금H.Row = this._flexDHL기본요금H.Rows.Count - 1;

						this._flexDHL기본요금H["DT_YEAR"] = Global.MainFrame.GetStringToday.Substring(0, 4);

						this._flexDHL기본요금H.Col = this._flexDHL기본요금H.Cols.Fixed;
						this._flexDHL기본요금H.AddFinished();
					}
					else
                    {
						this._flexDHL청구요금H.Rows.Add();
						this._flexDHL청구요금H.Row = this._flexDHL청구요금H.Rows.Count - 1;

						this._flexDHL청구요금H["DT_MONTH"] = Global.MainFrame.GetStringToday.Substring(0, 6);

						this._flexDHL청구요금H.Col = this._flexDHL청구요금H.Cols.Fixed;
						this._flexDHL청구요금H.AddFinished();
					}
                }
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

        protected override bool BeforeDelete()
        {
			if (this.tabControl1.SelectedIndex == 0)
			{
				if (this._flex목공포장L.DataTable.Select("CD_PARTNER = '" + this._flex목공포장H["CD_PARTNER"].ToString() + "'").Length > 0)
				{
					this.ShowMessage("선택한 거래처에 해당하는 데이터가 존재 합니다.");
					return false;
				}
			}
			else
            {
				if (this.tabControl2.SelectedIndex == 0)
				{
					if (this._flexDHL기본요금L.DataTable.Select("DT_YEAR = '" + this._flexDHL기본요금H["DT_YEAR"].ToString() + "'").Length > 0)
					{
						this.ShowMessage("선택한 년도에 해당하는 데이터가 존재 합니다.");
						return false;
					}
				}
			}

			return base.BeforeDelete();
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete()) return;

				if (this.tabControl1.SelectedIndex == 0)
				{
					if (!this._flex목공포장H.HasNormalRow) return;

					this._flex목공포장H.Rows.Remove(this._flex목공포장H.Row);
				}
				else
				{
					if (this.tabControl2.SelectedIndex == 0)
					{
						if (!this._flexDHL기본요금H.HasNormalRow) return;

						this._flexDHL기본요금H.Rows.Remove(this._flexDHL기본요금H.Row);
					}
					else
                    {
						if (!this._flexDHL청구요금H.HasNormalRow) return;

						this._flexDHL청구요금H.Rows.Remove(this._flexDHL청구요금H.Row);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			try
			{
				if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
				if (this._flex목공포장H.IsDataChanged == false && this._flex목공포장L.IsDataChanged == false &&
					this._flexDHL기본요금H.IsDataChanged == false && this._flexDHL기본요금L.IsDataChanged == false && this._flexDHL청구요금H.IsDataChanged == false) 
					return false;

				if (this._biz.Save(this._flex목공포장H.GetChanges(), 
								   this._flex목공포장L.GetChanges(),
								   this._flexDHL기본요금H.GetChanges(),
								   this._flexDHL기본요금L.GetChanges(),
								   this._flexDHL청구요금H.GetChanges()))
				{
					this._flex목공포장H.AcceptChanges();
					this._flex목공포장L.AcceptChanges();
					this._flexDHL기본요금H.AcceptChanges();
					this._flexDHL기본요금L.AcceptChanges();
					this._flexDHL청구요금H.AcceptChanges();

					return true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}
    }
}
