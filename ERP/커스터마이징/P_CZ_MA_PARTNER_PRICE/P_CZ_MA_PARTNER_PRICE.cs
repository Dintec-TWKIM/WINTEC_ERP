using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms.Help;
using Dintec;
using Duzon.Common.BpControls;
using System.Data.OleDb;
using System.Net;
using Duzon.ERPU.OLD;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;

namespace cz
{
	public partial class P_CZ_MA_PARTNER_PRICE : PageBase
	{
		#region 초기화
		P_CZ_MA_PARTNER_PRICE_BIZ _biz;
		private string 구분;

		public P_CZ_MA_PARTNER_PRICE()
		{
			InitializeComponent();

			StartUp.Certify(this);

			this._biz = new P_CZ_MA_PARTNER_PRICE_BIZ();
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			#region Header
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("CD_PARTNER", "거래처코드", 80, false);
			this._flexH.SetCol("LN_PARTNER", "거래처명", 150, false);
			this._flexH.SetCol("NM_CLS_PARTNER", "거래처분류", 150, false);
			this._flexH.SetCol("USE_YN", "사용유무", 80, false, CheckTypeEnum.Y_N);
			this._flexH.SetCol("QT_REG", "등록수", 60, false);

			this._flexH.SetDummyColumn("S");
			this._flexH.SettingVersion = "1.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexL.SetCol("NO_LINE", "순번", false);
			this._flexL.SetCol("CD_ITEM", "품목코드", 100, 20, true);
			this._flexL.SetCol("NM_ITEM", "품목명", 140);
			this._flexL.SetCol("CLS_ITEM", "계정구분", false);
			this._flexL.SetCol("CLS_L", "대분류", false);
			this._flexL.SetCol("CLS_M", "중분류", false);
			this._flexL.SetCol("CLS_S", "소분류", false);
			this._flexL.SetCol("STND_DETAIL_ITEM", "U코드", 100);
			this._flexL.SetCol("STND_ITEM", "파트번호", 100);
			this._flexL.SetCol("MAT_ITEM", "아이템번호", false);
			this._flexL.SetCol("DC_RMK1", "공품비고1", 100);
			this._flexL.SetCol("DC_RMK2", "공품비고2", 100);
			this._flexL.SetCol("CD_EXCH", "통화명", 80, true);
			this._flexL.SetCol("FG_UM", "단가유형", 80, true);
			this._flexL.SetCol("UM_ITEM", "단가", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("STAND_PRC", "재고평균단가", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("RT_UM", "재고할인율", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("DC_PRICE_TERMS", "가격조건", 100, true);
			this._flexL.SetCol("LT", "납기", 60, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("SDT_UM", "시작일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexL.SetCol("EDT_UM", "종료일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexL.SetCol("CD_EXCH_S", "통화명(S)", 80, true);
			this._flexL.SetCol("UM_ITEM_S", "단가(S)", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("RT_PROFIT_A", "민감도(높음)", 80, true, typeof(decimal), FormatTpType.RATE);
			this._flexL.SetCol("RT_PROFIT_B", "민감도(중간)", 80, true, typeof(decimal), FormatTpType.RATE);
			this._flexL.SetCol("RT_PROFIT_C", "민감도(낮음)", 80, true, typeof(decimal), FormatTpType.RATE);
			this._flexL.SetCol("QT_AVST", "가용재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_AVPO", "가용발주", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("NM_INSERT", "등록자", 80);
			this._flexL.SetCol("DTS_INSERT", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexL.SetCol("NM_UPDATE", "수정자", 80);
			this._flexL.SetCol("DTS_UPDATE", "수정일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexL.SetCol("DC_RMK", "비고", 120, true);
			this._flexL.SetCol("TXT_USERDEF1", "비고2", 120, true);
			this._flexL.SetCol("CD_PARTNER_STD", "기준매입처", 120, true);
			this._flexL.SetCol("LN_PARTNER_STD", "기준매입처명", 120);

			this._flexL.SetCodeHelpCol("CD_ITEM", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_DETAIL_ITEM", "STND_ITEM", "MAT_ITEM", "CLS_ITEM", "CLS_L", "CLS_M", "CLS_S" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_DETAIL_ITEM", "STND_ITEM", "MAT_ITEM", "CLS_ITEM", "CLS_L", "CLS_M", "CLS_S" });
			this._flexL.SetCodeHelpCol("CD_PARTNER_STD", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER_STD", "LN_PARTNER_STD" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

			this._flexL.SetDataMap("CLS_ITEM", Global.MainFrame.GetComboDataCombine("S;MA_B000010"), "CODE", "NAME");
			this._flexL.SetDataMap("CLS_L", Global.MainFrame.GetComboDataCombine("S;MA_B000030"), "CODE", "NAME");
			this._flexL.SetDataMap("CLS_M", Global.MainFrame.GetComboDataCombine("S;MA_B000031"), "CODE", "NAME");
			this._flexL.SetDataMap("CLS_S", Global.MainFrame.GetComboDataCombine("S;MA_B000032"), "CODE", "NAME");
			this._flexL.SetDataMap("CD_EXCH", Global.MainFrame.GetComboDataCombine("S;MA_B000005"), "CODE", "NAME");
			this._flexL.SetDataMap("CD_EXCH_S", Global.MainFrame.GetComboDataCombine("S;MA_B000005"), "CODE", "NAME");
			this._flexL.SetDataMap("FG_UM", Global.MainFrame.GetComboDataCombine("S;SA_B000021"), "CODE", "NAME");

			this._flexL.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
			this._flexL.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flexL.UseMultySorting = true;
			this._flexL.SetDummyColumn("S");
			this._flexL.SettingVersion = "1.0.0.3";
			this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.btn전체삭제.Click += new EventHandler(this.btn전체삭제_Click);
			this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn단가동기화.Click += new EventHandler(this.btn단가동기화_Click);
            this.btn단가갱신.Click += new EventHandler(this.btn단가갱신_Click);

			this.ctx소분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx중분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
			this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);

			this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
			this._flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flexL_AfterCodeHelp);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

            UGrant ugrant = new UGrant();

            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn전체삭제);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn단가동기화);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn단가갱신);

			object[] items = new object[] { this.DD("품목코드"),
											this.DD("품목명"),
											this.DD("U코드"),
											this.DD("파트번호"),
											this.DD("아이템번호"),
											this.DD("비고1"),
											this.DD("비고2") };

			this.cbo검색1.Items.AddRange(items);
			this.cbo검색2.Items.AddRange(items);
			this.cbo검색1.SelectedIndex = 0;
			this.cbo검색2.SelectedIndex = 0;

			this.cbo구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "매입", "매출" });
			this.cbo구분.DisplayMember = "NAME";
			this.cbo구분.ValueMember = "CODE";

			this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("N;MA_PLANT");
			this.cbo공장.DisplayMember = "NAME";
			this.cbo공장.ValueMember = "CODE";

			this.cbo사용여부.DataSource = MA.GetCodeUser(new string[] { "", "Y", "N" }, new string[] { "", "사용", "미사용" });
			this.cbo사용여부.DisplayMember = "NAME";
			this.cbo사용여부.ValueMember = "CODE";

			this.cbo등록여부.DataSource = MA.GetCodeUser(new string[] { "", "Y", "N" }, new string[] { "", "등록", "미등록" });
			this.cbo등록여부.DisplayMember = "NAME";
			this.cbo등록여부.ValueMember = "CODE";

			SetControl setControl = new SetControl();
			setControl.SetCombobox(this.cbo조달구분, MF.GetCode(MF.코드.MASTER.품목.조달구분, true));
		}
		#endregion

		#region 메인버튼 이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				this.구분 = D.GetString(this.cbo구분.SelectedValue);

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  D.GetString(this.cbo공장.SelectedValue),
																	  this.ctx거래처.CodeValue,
																	  this.구분,
																	  ("00" + D.GetString(this.cbo검색1.SelectedIndex)),
																	  ("00" + D.GetString(this.cbo검색2.SelectedIndex)),
																	  this.txt검색1.Text,
																	  this.txt검색2.Text,
																	  this.ctx소분류.CodeValue,
																	  this.ctx중분류.CodeValue,
																	  D.GetString(this.cbo사용여부.SelectedValue),
																	  D.GetString(this.cbo등록여부.SelectedValue),
																	  this.ctx품목군.CodeValue,
																	  (this.chk유효단가.Checked == true ? "Y" : "N"),
																	  this.cbo조달구분.SelectedValue.ToString() });

				if (!this._flexH.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!this.BeforeAdd()) return;

				this._flexL.Rows.Add();
				this._flexL.Row = this._flexL.Rows.Count - 1;

				this._flexL["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flexL["CD_PARTNER"] = D.GetString(this._flexH["CD_PARTNER"]);
				this._flexL["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
				this._flexL["TP_UMMODULE"] = this.구분;
				this._flexL["CD_EXCH"] = "000";
				this._flexL["FG_UM"] = "001";

				this._flexL["SDT_UM"] = this.MainFrameInterface.GetStringToday;
				this._flexL["EDT_UM"] = "99991231";

				this._flexL.AddFinished();
				this._flexL.Col = this._flexL.Cols.Fixed;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
            int index;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete()) return;
                if (this.ShowMessage("선택된 데이터가 바로 삭제 및 저장 됩니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes) return;

				DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
                    DataSet ds = new DataSet();
                    bool result = true;
                    int groupUnit = 1000;

                    for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                    {
                        ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                    }

                    index = 0;
                    foreach (DataTable dt1 in ds.Tables)
                    {
                        MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });
                        result = this._biz.Save(true, dt1);
                    }

                    if (result)
                        this.OnToolBarSearchButtonClicked(null, null);
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

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            if (this._flexL.GetChanges() != null && this._flexL.GetChanges().Select("ISNULL(CD_ITEM, '') = ''").Length > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, "품목코드");
                return false;
            }

            if (this._flexL.GetChanges() != null && this._flexL.GetChanges().Select("ISNULL(CD_EXCH, '') = ''").Length > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, "통화명");
                return false;
            }

            if (this._flexL.GetChanges() != null && this._flexL.GetChanges().Select("ISNULL(FG_UM, '') = ''").Length > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, "단가유형");
                return false;
            }

            if (this._flexL.GetChanges() != null && this._flexL.GetChanges().Select("ISNULL(UM_ITEM, 0) = 0").Length > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, "단가");
                return false;
            }

			if (this._flexL.GetChanges() != null && this._flexL.GetChanges().Select("ISNULL(SDT_UM, 0) = 0").Length > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, "시작일");
				return false;
			}

			return true;
        }

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

				if (MsgAndSave(PageActionMode.Save))
				{
					ShowMessage(PageResultMode.SaveGood);
					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			int index;
			DataRow[] dataRowArray;

			try
			{
				if (!base.SaveData())
                    return false;

                #region 저장
                dataRowArray = this._flexL.DataTable.AsEnumerable()
                                                    .Where(x => x.RowState != DataRowState.Unchanged)
                                                    .ToArray();

                DataSet ds = new DataSet();
                bool result = true;
                int groupUnit = 1000;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                index = 0;
                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });
                    result = this._biz.Save(false, dt1);
                }

                if (result)
                    this._flexL.AcceptChanges();
                #endregion
                
				return true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flexL.Redraw = true;
				MsgControl.CloseMsg();
			}

			return false;
		}
		#endregion

		#region 컨트롤 이벤트
		private void btn전체삭제_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				if (this.ShowMessage(D.GetString(this._flexH["LN_PARTNER"]) + "의 단가 데이터가 모두 삭제 됩니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				if (!Util.CheckPW()) return;

				query = @"DELETE 
						  FROM MA_ITEM_UMPARTNER
						  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
						 "AND CD_PARTNER = '" + D.GetString(this._flexH["CD_PARTNER"]) + "'";

				DBHelper.ExecuteScalar(query);

				this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;
			DataRow[] dataRowArrary;

			try
			{
				bool bState = true;
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_거래처별단가관리_" + Global.MainFrame.GetStringToday + ".xlsx";
				string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_PARTNER_PRICE.xlsx";

				WebClient client = new WebClient();
				client.DownloadFile(serverPath, localPath);

				if (this._flexL.HasNormalRow && this._flexL.DataTable.Select("S = 'Y'").Length > 0)
				{
					if (ShowMessage("선택된 데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
						bState = false;
				}

				this.ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

				if (bState == false) return;

				// 확장명 XLS (Excel 97~2003 용)
				string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 12.0;";

				conn = new OleDbConnection(strConn);
				conn.Open();

				OleDbCommand Cmd = null;
				OleDbDataAdapter OleDBAdap = null;

				string sTableName = string.Empty;

				DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				DataSet ds = new DataSet();

				// 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
				foreach (DataRow dr in dtSchema.Rows)
				{
					OleDBAdap = new OleDbDataAdapter(dr["TABLE_NAME"].ToString(), conn);

					OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
					OleDBAdap.AcceptChangesDuringFill = false;

					sTableName = dr["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

					if (dr["TABLE_NAME"].ToString().Contains("$"))
						OleDBAdap.Fill(ds, sTableName);
					break;
				}

				StringBuilder FldsInfo = new StringBuilder();
				StringBuilder Flds = new StringBuilder();

				// Create Field(s) String : 현재 테이블의 Field 명 생성
				foreach (DataColumn Column in ds.Tables[0].Columns)
				{
					if (FldsInfo.Length > 0)
					{
						FldsInfo.Append(",");
						Flds.Append(",");
					}

					FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
					Flds.Append(Column.ColumnName.Replace("'", "''"));
				}

				int index = 0;

				// Insert Data
				if (this._flexL.HasNormalRow)
				{
					dataRowArrary = this._flexL.DataTable.Select("S = 'Y'");

					foreach (DataRow dr in dataRowArrary)
					{
						MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArrary.Length) });
						StringBuilder Values = new StringBuilder();

						foreach (DataColumn Column in ds.Tables[0].Columns)
						{
							if (Column.ColumnName == "YN_DELETE")
							{
								if (Values.Length > 0) Values.Append(",");
								Values.Append("'N'");
								continue;
							}
							else if (!this._flexL.DataTable.Columns.Contains(Column.ColumnName)) 
								continue;

							if (Values.Length > 0) Values.Append(",");
							Values.Append("'" + dr[Column.ColumnName].ToString().Replace("'", "''") + "'");
						}

						Cmd = new OleDbCommand(
							"INSERT INTO [" + sTableName + "$]" +
							"(" + Flds.ToString() + ") " +
							"VALUES (" + Values.ToString() + ")",
							conn);
						Cmd.ExecuteNonQuery();
					}
				}
				
				bState = true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();

				if (conn != null)
					conn.Close();
			}
		}

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			OleDbConnection con = null;
			DataTable dtSchema, dtExcel;
			OleDbCommand oconn;
			OleDbDataAdapter sda;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				OpenFileDialog fileDlg = new OpenFileDialog();
				fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

				if (fileDlg.ShowDialog() != DialogResult.OK) return;

				String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileDlg.FileName + ";Extended Properties=Excel 12.0;";

				con = new OleDbConnection(constr);
				con.Open();

				dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
				sda = new OleDbDataAdapter(oconn);
				dtExcel = new DataTable();

				sda.Fill(dtExcel);

				for (int i = 0; i < 2; i++)
				{
					dtExcel.Rows[i].Delete();
				}

				dtExcel.AcceptChanges();

				// 필요한 컬럼 존재 유무 파악
				string[] argsPk = new string[] { "CD_PARTNER", "CD_ITEM", "CD_EXCH", "FG_UM", "UM_ITEM", "SDT_UM", "EDT_UM" };
				string[] argsPkNm = new string[] { "거래처코드", "품목코드", "통화코드", "단가유형", "단가", "시작일", "종료일" };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

				dtExcel.Columns.Add("CD_COMPANY");
				dtExcel.Columns.Add("CD_PLANT");
				dtExcel.Columns.Add("TP_UMMODULE");

				foreach (DataRow dr in dtExcel.Rows)
                {
					dr["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					dr["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
					dr["TP_UMMODULE"] = this.구분;
				}

				if (this._biz.SaveExcel(dtExcel))
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn엑셀업로드.Text });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn첨부파일_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;

				P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "MA", "P_CZ_MA_PARTNER_PRICE", D.GetString(this._flexH["CD_PARTNER"]) + "_" + this.구분, "P_CZ_MA_PARTNER_PRICE");
				dlg.ShowDialog(this);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				if (e.ControlName == this.ctx중분류.Name)
					e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
				else if (e.ControlName == this.ctx소분류.Name)
					e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

        private void btn단가동기화_Click(object sender, EventArgs e)
        {
            if (this.ShowMessage("선택된 거래처의 단가를 두베코에 동기화 하시겠습니까 ?", "QY2") == DialogResult.Yes)
            {
                DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_PRICE_SYNC", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       this._flexH["CD_PARTNER"].ToString(),
                                                                                       "K200",
                                                                                       "001" });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn단가동기화.Text);
            }
            else if (this.ShowMessage("선택된 거래처의 단가를 싱가폴에 동기화 하시겠습니까 ?", "QY2") == DialogResult.Yes)
            {
                DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_PRICE_SYNC", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       this._flexH["CD_PARTNER"].ToString(),
                                                                                       "S100",
                                                                                       "S01" });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn단가동기화.Text);
            }
        }

        private void btn단가갱신_Click(object sender, EventArgs e)
        {
            if (this.ShowMessage("선택된 거래처 단가를 갱신 하시겠습니까 ?", "QY2") != DialogResult.Yes)
                return;

            DBHelper.ExecuteNonQuery("PX_CZ_MA_PITEM_UM_EXT", new object[] { DBNull.Value,
                                                                             Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this._flexH["CD_PARTNER"].ToString() });


			DBHelper.ExecuteNonQuery("PX_CZ_MA_QLINK_UM", new object[] { DBNull.Value,
																		 Global.MainFrame.LoginInfo.CompanyCode,
																	     this._flexH["CD_PARTNER"].ToString() });

			this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn단가갱신.Text);
        }
		#endregion

		#region 그리드 이벤트
		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string key, filter;

			try
			{
				DataTable dt = null;

				key = D.GetString(this._flexH["CD_PARTNER"]);
				filter = "CD_PARTNER = '" + key + "'";

				if (this._flexH.DetailQueryNeed)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   this.구분,
															   key,
															   D.GetString(this.cbo공장.SelectedValue),
															   ("00" + D.GetString(this.cbo검색1.SelectedIndex)),
															   ("00" + D.GetString(this.cbo검색2.SelectedIndex)),
															   this.txt검색1.Text,
															   this.txt검색2.Text,
															   this.ctx소분류.CodeValue,
															   this.ctx품목군.CodeValue,
															   (this.chk유효단가.Checked == true ? "Y" : "N"),
															   this.cbo조달구분.SelectedValue.ToString() });
				}

				this._flexL.Redraw = false;
				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexL.Redraw = true;
			}
		}

		private void _flexH_AfterEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (this._flexH.Cols[e.Col].Name)
				{
					case "S":
						if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
						{
							for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
								this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
						}
						else
						{
							for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
								this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
						}
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				if (e.Parameter.HelpID == HelpID.P_USER)
				{
					e.Parameter.UserParams = "공장품목;H_CZ_MA_CUSTOMIZE_SUB";
					e.Parameter.P11_ID_MENU = "H_MA_PITEM_SUB";
					e.Parameter.P21_FG_MODULE = "N";
					e.Parameter.P92_DETAIL_SEARCH_CODE = D.GetString(this._flexL["CD_ITEM"]);
					e.Parameter.MultiHelp = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
		{
			FlexGrid flexGrid = ((FlexGrid)sender);
			int index;

			try
			{
				if (e.Result.HelpID == HelpID.P_USER)
				{
					flexGrid.Redraw = false;
					index = 0;

					foreach (DataRow dataRow in e.Result.Rows)
					{
						MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(e.Result.Rows.Length) });

						if (e.Result.Rows[0] != dataRow)
							this.OnToolBarAddButtonClicked(null, null);

						flexGrid["CD_ITEM"] = dataRow["CD_ITEM"];
						flexGrid["NM_ITEM"] = dataRow["NM_ITEM"];
						flexGrid["CLS_ITEM"] = dataRow["CLS_ITEM"];
						flexGrid["STND_DETAIL_ITEM"] = dataRow["STND_DETAIL_ITEM"];
						flexGrid["STND_ITEM"] = dataRow["STND_ITEM"];
						flexGrid["MAT_ITEM"] = dataRow["MAT_ITEM"];
						flexGrid["CLS_L"] = dataRow["CLS_L"];
						flexGrid["CLS_M"] = dataRow["CLS_M"];
						flexGrid["CLS_S"] = dataRow["CLS_S"];
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				flexGrid.Redraw = true;
			}
		}
		#endregion
	}
}