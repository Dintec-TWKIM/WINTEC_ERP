using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.ERPU.Utils;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU.Grant;
using System.Data.OleDb;
using Duzon.Common.Util;

namespace cz
{
    public partial class P_CZ_MA_DOCUMENT_MNG_WINTEC : PageBase
    {
        P_CZ_MA_DOCUMENT_MNG_WINTEC_BIZ _biz = new P_CZ_MA_DOCUMENT_MNG_WINTEC_BIZ();

        public P_CZ_MA_DOCUMENT_MNG_WINTEC()
        {
			if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
				StartUp.Certify(this);

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

            DataSet comboData = Global.MainFrame.GetComboData(new string[] { "NC;MA_PLANT",
																			 "S;MA_B000004" });

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.DisplayMember = "NAME";

			this.cbo단위.DataSource = comboData.Tables[1];
			this.cbo단위.ValueMember = "CODE";
			this.cbo단위.DisplayMember = "NAME";

			UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "DOCUMENT", this.btn문서추가, true);
            ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "DOCUMENT", this.btn문서저장, true);
            ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "DOCUMENT", this.btn문서삭제, true);
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex도면관리, this._flex성적서관리 };

			#region 도면관리
			this._flex도면관리.BeginSetting(1, 1, false);

			this._flex도면관리.SetCol("SEQ", "순번", 100);
			this._flex도면관리.SetCol("TP_DRAWING", "도면유형", 100, true);
			this._flex도면관리.SetCol("NO_DRAWING", "도면번호", 100, true);
			this._flex도면관리.SetCol("CD_ITEM", "품목", 100, true);
			this._flex도면관리.SetCol("NM_ITEM", "품명", 100);
			this._flex도면관리.SetCol("DC_RMK", "비고", 100, true);

			this._flex도면관리.SetOneGridBinding(new object[] { }, new IUParentControl[] { this.pnl도면 });

			this._flex도면관리.ExtendLastCol = true;

			this._flex도면관리.VerifyPrimaryKey = new string[] { "SEQ" };
			this._flex도면관리.SetDummyColumn(new string[] { "NM_FILE" });

			this._flex도면관리.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
			this._flex도면관리.SetDataMap("TP_DRAWING", MA.GetCode("CZ_WIN0004", false), "CODE", "NAME");

			this._flex도면관리.SettingVersion = "0.0.0.1";
			this._flex도면관리.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 성적서관리
			this._flex성적서관리.BeginSetting(1, 1, false);

			this._flex성적서관리.SetCol("SEQ", "순번", 100);
			this._flex성적서관리.SetCol("NO_DOC", "문서번호", 100, true);
			this._flex성적서관리.SetCol("CD_SUPPLIER", "매입처코드", 100, true);
			this._flex성적서관리.SetCol("NM_SUPPLIER", "매입처명", 100);
			this._flex성적서관리.SetCol("DC_TYPE", "타입", 100, true);
			this._flex성적서관리.SetCol("CD_ITEM", "품목코드", 100, true);
			this._flex성적서관리.SetCol("NM_ITEM", "품목명", 100);
			this._flex성적서관리.SetCol("NO_DRAWING", "도면번호", 100, true);
			this._flex성적서관리.SetCol("DC_MAT", "재질", 100, true);
			this._flex성적서관리.SetCol("DC_SIZE", "사이즈", 100, true);
			this._flex성적서관리.SetCol("TP_UNIT", "단위", 100, true);
			this._flex성적서관리.SetCol("QT_IN", "입고수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex성적서관리.SetCol("NO_HEAT", "HeatNo.", 100, true);
			this._flex성적서관리.SetCol("NO_LOT", "LotNo.", 100, true);
			this._flex성적서관리.SetCol("DC_RMK", "비고", 100, true);

			this._flex성적서관리.SetOneGridBinding(new object[] { }, new IUParentControl[] { this.pnl성적서 });

			this._flex성적서관리.ExtendLastCol = true;

			this._flex성적서관리.VerifyPrimaryKey = new string[] { "SEQ" };
			this._flex성적서관리.SetDummyColumn(new string[] { "NM_FILE" });

			this._flex성적서관리.SetDataMap("TP_UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");
			this._flex성적서관리.SetCodeHelpCol("CD_SUPPLIER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_SUPPLIER", "NM_SUPPLIER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
			this._flex성적서관리.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });

			this._flex성적서관리.SettingVersion = "0.0.0.1";
			this._flex성적서관리.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
        {
			this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);

            this.btn문서추가.Click += new EventHandler(this.btn문서추가_Click);
            this.btn문서보기.Click += new EventHandler(this.btn문서보기_Click);
            this.btn문서저장.Click += new EventHandler(this.btn문서저장_Click);
            this.btn문서삭제.Click += new EventHandler(this.btn문서삭제_Click);

            this.ctx품목도면.QueryBefore += new BpQueryHandler(this.ctx품목_QueryBefore);
            this.ctx품목S.QueryBefore += new BpQueryHandler(this.ctx품목_QueryBefore);
            this._flex도면관리.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flex성적서관리.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
			try
            {
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				if (this.tabControlExt1.SelectedIndex == 0)
				{
					this._flex도면관리.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				 this.cbo공장.SelectedValue.ToString(),
																				 this.ctx품목S.CodeValue,
																				 this.txt도면번호S.Text,
																			     this.txt세부규격S.Text });
					if (!this._flex도면관리.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else
				{
					this._flex성적서관리.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				    this.cbo공장.SelectedValue.ToString(),
																				    this.ctx품목S.CodeValue,
																				    this.txt도면번호S.Text });
					if (!this._flex성적서관리.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
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

				if (this.tabControlExt1.SelectedIndex == 0)
				{
					this._flex도면관리.Rows.Add();
					this._flex도면관리.Row = this._flex도면관리.Rows.Count - 1;

					this._flex도면관리["SEQ"] = this.SeqMax();

					this._flex도면관리.AddFinished();
					this._flex도면관리.Col = this._flex도면관리.Cols["SEQ"].Index;
					this._flex도면관리.Focus();
				}
				else
				{
					this._flex성적서관리.Rows.Add();
					this._flex성적서관리.Row = this._flex성적서관리.Rows.Count - 1;

					this._flex성적서관리["SEQ"] = this.SeqMax1();

					this._flex성적서관리.AddFinished();
					this._flex성적서관리.Col = this._flex성적서관리.Cols["SEQ"].Index;
					this._flex성적서관리.Focus();
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

			FlexGrid grid;

			if (this.tabControlExt1.SelectedIndex == 0)
				grid = this._flex도면관리;
			else
				grid = this._flex성적서관리;

			if (!string.IsNullOrEmpty(D.GetString(grid["NM_FILE"])))
            {
                this.ShowMessage("등록된 문서 삭제 후 다시 진행하시기 바랍니다.");
                return false;
            }

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
			FlexGrid grid;

			try
            {
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (this.tabControlExt1.SelectedIndex == 0)
					grid = this._flex도면관리;
				else
					grid = this._flex성적서관리;

				if (!this.BeforeDelete() || !grid.HasNormalRow) return;

				grid.Rows.Remove(grid.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
			FlexGrid grid;

			if (this.tabControlExt1.SelectedIndex == 0)
				grid = this._flex도면관리;
			else
				grid = this._flex성적서관리;

			if (grid.IsDataChanged == false) return false;

			if (this.tabControlExt1.SelectedIndex == 0)
			{
				if (!this._biz.Save(this.cbo공장.SelectedValue.ToString(), grid.GetChanges()))
					return false;
			}
			else
			{
				if (!this._biz.Save1(this.cbo공장.SelectedValue.ToString(), grid.GetChanges())) 
					return false;
			}
			
			grid.AcceptChanges();

            return true;
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn문서추가_Click(object sender, EventArgs e)
        {
			FlexGrid grid;
			string fileName, query, 문서구분, 파일명;

            try
            {
				if (this.tabControlExt1.SelectedIndex == 0)
				{
					grid = this._flex도면관리;
					문서구분 = "DRAW";
					파일명 = this.txt도면.Text;
				}
				else
				{
					grid = this._flex성적서관리;
					문서구분 = "CERT";
					파일명 = this.txt성적서.Text;
				}
				
				if (!grid.HasNormalRow) return;

                if (!string.IsNullOrEmpty(파일명))
                {
                    this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                    return;
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                fileName = openFileDialog.SafeFileName;

                if (FileUploader.UploadFile(fileName, openFileDialog.FileName, "Upload/P_CZ_MA_DOCUMENT_MNG_WINTEC", Global.MainFrame.LoginInfo.CompanyCode + "/" + 문서구분 + "/" + D.GetString(grid["SEQ"])) == "Success")
                {
					if (this.tabControlExt1.SelectedIndex == 0)
					{
						query = @"UPDATE CZ_MA_DRAWING_WINTEC
								  SET NM_FILE = '" + fileName + "'" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'" + Environment.NewLine +
								 "AND SEQ = '" + D.GetString(grid["SEQ"]) + "'";
					}
					else
					{
						query = @"UPDATE CZ_MA_CERT_WINTEC
								  SET NM_FILE = '" + fileName + "'" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'" + Environment.NewLine +
								 "AND SEQ = '" + D.GetString(grid["SEQ"]) + "'";
					}
                    
                    DBHelper.ExecuteScalar(query);

					if (this.tabControlExt1.SelectedIndex == 0)
						this.txt도면.Text = fileName;
					else
						this.txt성적서.Text = fileName;

					grid["NM_FILE"] = fileName;

                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
                else
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn문서보기_Click(object sender, EventArgs e)
        {
			FlexGrid grid;
			WebBrowser webControl;
			string 경로, 문서구분, 파일명;

            try
            {
				if (this.tabControlExt1.SelectedIndex == 0)
				{
					grid = this._flex도면관리;
					문서구분 = "DRAW";
					파일명 = this.txt도면.Text;
					webControl = this.web도면보기;
				}
				else
				{
					grid = this._flex성적서관리;
					문서구분 = "CERT";
					파일명 = this.txt성적서.Text;
					webControl = this.web성적서보기;
				}

				if (!grid.HasNormalRow) return;

                경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_DOCUMENT_MNG_WINTEC/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 문서구분 + "/" + D.GetString(grid["SEQ"]) + "/";

                if (string.IsNullOrEmpty(파일명))
					webControl.Navigate(string.Empty);
                else
					webControl.Navigate(경로 + 파일명);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn문서저장_Click(object sender, EventArgs e)
        {
			FlexGrid grid;
			string 문서구분, 파일명;

			try
            {
				if (this.tabControlExt1.SelectedIndex == 0)
				{
					grid = this._flex도면관리;
					문서구분 = "DRAW";
					파일명 = this.txt도면.Text;
				}
				else
				{
					grid = this._flex성적서관리;
					문서구분 = "CERT";
					파일명 = this.txt성적서.Text;
				}

				if (!grid.HasNormalRow) return;

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

                FileUploader.DownloadFile(파일명, folderBrowserDialog.SelectedPath, "Upload/P_CZ_MA_DOCUMENT_MNG_WINTEC", Global.MainFrame.LoginInfo.CompanyCode + "/" + 문서구분 + "/" + D.GetString(grid["SEQ"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn문서삭제_Click(object sender, EventArgs e)
        {
			FlexGrid grid;
			WebBrowser webControl;
			string query, 문서구분, 파일명;

            try
            {
				if (this.tabControlExt1.SelectedIndex == 0)
				{
					grid = this._flex도면관리;
					문서구분 = "DRAW";
					webControl = this.web도면보기;
					파일명 = this.txt도면.Text;
				}
				else
				{
					grid = this._flex성적서관리;
					문서구분 = "CERT";
					webControl = this.web성적서보기;
					파일명 = this.txt성적서.Text;
				}

				if (!grid.HasNormalRow) return;

                if (FileUploader.DeleteFile("Upload/P_CZ_MA_DOCUMENT_MNG_WINTEC", Global.MainFrame.LoginInfo.CompanyCode + "/" + 문서구분 + "/" + D.GetString(grid["SEQ"]), 파일명))
                {
					if (this.tabControlExt1.SelectedIndex == 0)
					{
						query = @"UPDATE CZ_MA_DRAWING_WINTEC
								  SET NM_FILE = NULL" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'" + Environment.NewLine +
								 "AND SEQ = '" + D.GetString(grid["SEQ"]) + "'";
					}
					else
					{
						query = @"UPDATE CZ_MA_CERT_WINTEC
								  SET NM_FILE = NULL" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'" + Environment.NewLine +
								 "AND SEQ = '" + D.GetString(grid["SEQ"]) + "'";
					}
                    
                    DBHelper.ExecuteScalar(query);

					webControl.Navigate(string.Empty);

					if (this.tabControlExt1.SelectedIndex == 0)
						this.txt도면.Text = string.Empty;
					else
						this.txt성적서.Text = string.Empty;

					grid["NM_FILE"] = string.Empty;

                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                }
                else
                {
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx품목_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장.Text });

                    this.cbo공장.Focus();
                    e.QueryCancel = true;
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;
			StringBuilder Values;
			FlexGrid grid;

			try
			{
				bool bState = true;
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath, serverPath;

				if (this.tabControlExt1.SelectedIndex == 0)
				{
					localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_도면관리_" + Global.MainFrame.GetStringToday + ".xls";
					serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_DOCUMENT_MNG_WINTEC.xls";
					grid = this._flex도면관리;
				}
				else
				{
					localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_성적서관리_" + Global.MainFrame.GetStringToday + ".xls";
					serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_DOCUMENT_MNG_WINTEC1.xls";
					grid = this._flex성적서관리;
				}

				System.Net.WebClient client = new System.Net.WebClient();
				client.DownloadFile(serverPath, localPath);

				if (grid.HasNormalRow)
				{
					if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
						bState = false;
				}

				ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

				if (bState == false) return;

				// 확장명 XLS (Excel 97~2003 용)
				string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 8.0;";

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

				// Insert Data
				foreach (DataRow dr in grid.DataTable.Rows)
				{
					Values = new StringBuilder();

					foreach (DataColumn Column in ds.Tables[0].Columns)
					{
						if (!grid.DataTable.Columns.Contains(Column.ColumnName)) continue;

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

				bState = true;
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

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			FlexGrid grid;
			DataRow[] dataRowArray;
			DataRow dr1;
			int index;

			if (this.tabControlExt1.SelectedIndex == 0)
				grid = this._flex도면관리;
			else
				grid = this._flex성적서관리;

			try
			{
				#region btn엑셀업로드
				OpenFileDialog fileDlg = new OpenFileDialog(); 
			    fileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

				if (fileDlg.ShowDialog() != DialogResult.OK) return;

				grid.Redraw = false;

				string FileName = fileDlg.FileName;

				Excel excel = new Excel();
				DataTable dtExcel = null;
				dtExcel = excel.StartLoadExcel(FileName, 0, 3); // 3번째 라인부터 저장

				// 필요한 컬럼 존재 유무 파악
				string[] argsPk = new string[] { "SEQ" };
				string[] argsPkNm = new string[] { DD("순번") };

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
					MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dtExcel.Rows.Count) });

					dataRowArray = grid.DataTable.Select("SEQ = '" + D.GetString(dr["SEQ"]) + "'");

					if (dataRowArray.Length > 0)
						dr1 = dataRowArray[0];
					else
					{
						dr1 = grid.DataTable.NewRow();

						if (this.tabControlExt1.SelectedIndex == 0)
							dr1["SEQ"] = this.SeqMax();
						else
							dr1["SEQ"] = this.SeqMax1();
					}

					if (this.tabControlExt1.SelectedIndex == 0)
					{
						if (!string.IsNullOrEmpty(D.GetString(dr["TP_DRAWING"])))
							dr1["TP_DRAWING"] = dr["TP_DRAWING"];
						if (!string.IsNullOrEmpty(D.GetString(dr["NO_DRAWING"])))
							dr1["NO_DRAWING"] = dr["NO_DRAWING"];
						if (!string.IsNullOrEmpty(D.GetString(dr["CD_ITEM"])))
							dr1["CD_ITEM"] = dr["CD_ITEM"];
						if (!string.IsNullOrEmpty(D.GetString(dr["DC_RMK"])))
							dr1["DC_RMK"] = dr["DC_RMK"];
					}
					else
					{
						if (!string.IsNullOrEmpty(D.GetString(dr["NO_DOC"])))
							dr1["NO_DOC"] = dr["NO_DOC"];
						if (!string.IsNullOrEmpty(D.GetString(dr["CD_SUPPLIER"])))
							dr1["CD_SUPPLIER"] = dr["CD_SUPPLIER"];
						if (!string.IsNullOrEmpty(D.GetString(dr["DC_TYPE"])))
							dr1["DC_TYPE"] = dr["DC_TYPE"];
						if (!string.IsNullOrEmpty(D.GetString(dr["CD_ITEM"])))
							dr1["CD_ITEM"] = dr["CD_ITEM"];
						if (!string.IsNullOrEmpty(D.GetString(dr["NO_DRAWING"])))
							dr1["NO_DRAWING"] = dr["NO_DRAWING"];
						if (!string.IsNullOrEmpty(D.GetString(dr["DC_MAT"])))
							dr1["DC_MAT"] = dr["DC_MAT"];
						if (!string.IsNullOrEmpty(D.GetString(dr["DC_SIZE"])))
							dr1["DC_SIZE"] = dr["DC_SIZE"];
						if (!string.IsNullOrEmpty(D.GetString(dr["TP_UNIT"])))
							dr1["TP_UNIT"] = dr["TP_UNIT"];
						if (D.GetDecimal(dr["QT_IN"]) != 0)
							dr1["QT_IN"] = dr["QT_IN"];
						if (!string.IsNullOrEmpty(D.GetString(dr["NO_HEAT"])))
							dr1["NO_HEAT"] = dr["NO_HEAT"];
						if (!string.IsNullOrEmpty(D.GetString(dr["NO_LOT"])))
							dr1["NO_LOT"] = dr["NO_LOT"];
						if (!string.IsNullOrEmpty(D.GetString(dr["DC_RMK"])))
							dr1["DC_RMK"] = dr["DC_RMK"];
					}

					if (dataRowArray.Length == 0)
						grid.DataTable.Rows.Add(dr1);
				}
				MsgControl.CloseMsg();

				if (grid.HasNormalRow)
				{
					this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
					this.ToolBarSaveButtonEnabled = true;
				}
				else
				{
					this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
				}
				grid.Redraw = true;
				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				grid.Redraw = true;
				MsgControl.CloseMsg();
			}
		}

		private Decimal SeqMax()
		{
			Decimal num = 1;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM CZ_MA_DRAWING_WINTEC WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
														 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'");

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

			if (num <= this._flex도면관리.GetMaxValue("SEQ"))
				num = (this._flex도면관리.GetMaxValue("SEQ") + 1);

			return num;
		}

		private Decimal SeqMax1()
		{
			Decimal num = 1;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM CZ_MA_CERT_WINTEC WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
														 "AND CD_PLANT = '" + Global.MainFrame.LoginInfo.CdPlant + "'");

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

			if (num <= this._flex성적서관리.GetMaxValue("SEQ"))
				num = (this._flex성적서관리.GetMaxValue("SEQ") + 1);

			return num;
		}
	}
}
