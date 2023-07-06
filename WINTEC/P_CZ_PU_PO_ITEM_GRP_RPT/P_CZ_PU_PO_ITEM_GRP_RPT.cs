using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
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
	public partial class P_CZ_PU_PO_ITEM_GRP_RPT : PageBase
	{
		P_CZ_PU_PO_ITEM_GRP_RPT_BIZ _biz = new P_CZ_PU_PO_ITEM_GRP_RPT_BIZ();

		private bool _금액권한 = true;

		public P_CZ_PU_PO_ITEM_GRP_RPT()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			DataTable dataTable = BASIC.MFG_AUTH("P_SA_SO_MNG");

			if (dataTable.Rows.Count > 0)
				this._금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp납기일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
			this.dtp납기일자.EndDateToString = Global.MainFrame.GetStringToday;

			string query = @"SELECT IG.CD_ITEMGRP,
	   IG.NM_ITEMGRP
FROM MA_ITEMGRP IG WITH(NOLOCK)
WHERE IG.CD_COMPANY = '{0}'
AND ISNULL(IG.HD_ITEMGRP, '')  = ''
AND IG.USE_YN = 'Y'
ORDER BY IG.NM_ITEMGRP ASC";

			this.cbo경로유형.DataSource = DBHelper.GetDataTable("UP_PR_PATN_ROUT_ALL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										Global.SystemLanguage.MultiLanguageLpoint });
			this.cbo경로유형.DisplayMember = "NAME";
			this.cbo경로유형.ValueMember = "CODE";

			this.cbo품목그룹.DataSource = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));
			this.cbo품목그룹.ValueMember = "CD_ITEMGRP";
			this.cbo품목그룹.DisplayMember = "NM_ITEMGRP";

			this.cbo진행상태.DataSource = MA.GetCodeUser(new string[] { "", "001", "002" }, new string[] { "", "발주완료", "입고완료" });
			this.cbo진행상태.ValueMember = "CODE";
			this.cbo진행상태.DisplayMember = "NAME";
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexD };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexD };

			#region _flexH
			this._flexH.BeginSetting(2, 1, false);

			this._flexH.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("NO_SO", "수주번호", 100);
			this._flexH.SetCol("LN_PARTNER", "발주처", 100);
			this._flexH.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_ENGINE", "엔진유형", 100);
			this._flexH.SetCol("CD_ITEM", "품목코드", 100);
			this._flexH.SetCol("NM_ITEM", "품목명", 100);
			this._flexH.SetCol("NO_HULL", "호선", 100);
			this._flexH.SetCol("NO_PO_PARTNER", "거래처발주번호", 100);
			this._flexH.SetCol("NO_DESIGN1", "도면번호", 100);
			this._flexH.SetCol("TXT_USERDEF7", "도면번호(수주)", 100);
			this._flexH.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_NOT_GI", "미납수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			if (this._금액권한)
				this._flexH.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("NM_CERT", "선급", 100);
			this._flexH.SetCol("DT_EXPECT", "소요납기(최초)", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_DUEDATE", "관리납기", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_REQGI", "수정소요납기(최종)", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_IO", "납품일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_DELIVERY", "납품처", 100);
			this._flexH.SetCol("NM_ST_WO", "진행상태", 100);
			this._flexH.SetCol("DC_RMK1", "비고", 100);
			this._flexH.SetCol("TXT_USERDEF10", "비고1", 100, true);

			this._flexH.SetDummyColumn("S", "TXT_USERDEF10");

			this._flexH.SettingVersion = "2023.4.12.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.SelectionMode = SelectionModeEnum.Cell;

			this._flexH.Styles.Add("모품목재고").BackColor = Color.Gray;
			this._flexH.Styles.Add("모품목재고").ForeColor = Color.White;
			this._flexH.Styles.Add("모품목투입").BackColor = Color.Green;
			this._flexH.Styles.Add("모품목투입").ForeColor = Color.White;
			this._flexH.Styles.Add("요청").BackColor = Color.Yellow;
			this._flexH.Styles.Add("요청").ForeColor = Color.Black;
			this._flexH.Styles.Add("발주").BackColor = Color.Blue;
			this._flexH.Styles.Add("발주").ForeColor = Color.White;
			this._flexH.Styles.Add("재고").BackColor = Color.Orange;
			this._flexH.Styles.Add("재고").ForeColor = Color.White;
			#endregion

			#region _flexD
			this._flexD.BeginSetting(1, 1, false);

			this._flexD.SetCol("NO_PO", "발주번호", 100, false);
			this._flexD.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("CD_PARTNER", "외주처코드", 100, false);
			this._flexD.SetCol("LN_PARTNER", "외주처명", 100, false);
			this._flexD.SetCol("DT_PO", "발주일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexD.SetCol("CD_ITEM", "품목코드", 100, 20, false);
			this._flexD.SetCol("NM_ITEM", "품목명", 140, false);
			this._flexD.SetCol("NO_DESIGN", "도면번호", 100, false);
			this._flexD.SetCol("STND_ITEM", "규격", 120, false);
			this._flexD.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_RCV", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("DT_LIMIT", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexD.SetCol("DT_PLAN", "납품예정일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flexD.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

			this._flexD.SettingVersion = "2023.4.12.1";
			this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flexH.OwnerDrawCell += new OwnerDrawCellEventHandler(_flexH_OwnerDrawCell);

			this._flexH.AfterSelChange += _flexH_AfterSelChange;
			this._flexH.AfterEdit += _flex_AfterEdit;

			this.btn설정저장.Click += new EventHandler(this.Btn설정저장_Click);
			this.btn설정불러오기.Click += new EventHandler(this.Btn설정불러오기_Click);

			this.cbo품목그룹.SelectedIndexChanged += new EventHandler(this.Cbo품목그룹_SelectedIndexChanged);
		}

		private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;
				if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed) return;

				if (this._금액권한)
					if (e.Col < 24) return;
					else
					if (e.Col < 23) return;

				if (this._flexH[e.Row, e.Col] == null) return;

				CellStyle cellStyle = this._flexH.GetCellStyle(e.Row, e.Col);

				if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString().Contains("PRQ")) ||
					(this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString().Contains("PRQ")) ||
					(this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString().Contains("PRQ") && this._flexH.Cols[e.Col].Caption == "납품예정일"))
				{
					if (cellStyle == null || cellStyle.Name != "요청")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["요청"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString().Contains("POZ")) ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString().Contains("POZ")) ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString().Contains("POZ")))
				{
					if (cellStyle == null || cellStyle.Name != "발주")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["발주"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString().Contains("창고")) ||
		 				 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString().Contains("창고")) ||
		 				 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString().Contains("창고") && this._flexH.Cols[e.Col].Caption == "납품예정일"))
				{
					if (cellStyle == null || cellStyle.Name != "재고")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["재고"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString().Contains("생산투입완료")) ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString().Contains("생산투입완료")) ||
		  				 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString().Contains("생산투입완료") && this._flexH.Cols[e.Col].Caption == "납품예정일"))
				{
					if (cellStyle == null || cellStyle.Name != "모품목투입")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["모품목투입"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString().Contains("상위품목재고사용")) ||
		  				 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString().Contains("상위품목재고사용")) ||
		  				 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString().Contains("상위품목재고사용") && this._flexH.Cols[e.Col].Caption == "납품예정일"))
				{
					if (cellStyle == null || cellStyle.Name != "모품목재고")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["모품목재고"]);
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "없음")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["없음"]);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_AfterSelChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			object obj;
			string 발주번호;
			try
			{
				if (this._flexH.Rows[1][this._flexH.ColSel + 1] == null ||
					this._flexH.Rows[1][this._flexH.ColSel + 1].ToString() != "발주번호" ||
					!this._flexH.Cols[this._flexH.ColSel + 1].Name.Contains("DC_") ||
					this._flexH.RowSel < this._flexH.Rows.Fixed)
					return;

				obj = this._flexH.Rows[this._flexH.RowSel][this._flexH.ColSel + 1];

				if (obj == null) return;

				발주번호 = obj.ToString();

				if (string.IsNullOrEmpty(발주번호)) return;

				dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														   발주번호 });
				this._flexD.Binding = dt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;
				if (flexGrid.Cols[e.Col].Name != "TXT_USERDEF10") return;

				query = @"UPDATE SL
SET SL.TXT_USERDEF10 = '{3}'
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
															flexGrid["NO_SO"].ToString(),
															flexGrid["SEQ_SO"].ToString(),
															flexGrid["TXT_USERDEF10"].ToString()));
			}

			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn설정저장_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 저장 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;


				DataTable dt = new DataTable();
				dt.Columns.Add("SEQ", typeof(int));
				dt.Columns.Add("COL_NAME", typeof(string));
				dt.Columns.Add("COL_WIDTH", typeof(int));
				dt.Columns.Add("VISIBLE", typeof(int));

				for (int i = 1; i < this._flexH.Cols.Count; i++)
					dt.Rows.Add(i, _flexH.Cols[i].Name, _flexH.Cols[i].Width, _flexH.Cols[i].Visible ? 1 : 0);

				SQL sql = new SQL("PX_CZ_MA_USER_CONFIG_GRID", SQLType.Procedure);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP", Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID", "P_CZ_PU_PO_ITEM_GRP_RPT" + "_" + this.cbo품목그룹.SelectedValue.ToString());
				sql.Parameter.Add2("@XML", dt.ToXml());
				sql.ExecuteNonQuery();

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn설정저장.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn설정불러오기_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 불러오시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				string query = "SELECT * FROM CZ_MA_USER_CONFIG_GRID WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = @NO_EMP AND GRID_UID = @GRID_UID ORDER BY SEQ";

				SQL sql = new SQL(query, SQLType.Text);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP", Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID", "P_CZ_PU_PO_ITEM_GRP_RPT" + "_" + this.cbo품목그룹.SelectedValue.ToString());
				DataTable dt = sql.GetDataTable();

				foreach (DataRow row in dt.Rows)
				{
					if (!this._flexH.Cols.Contains(row["COL_NAME"].ToString()))
						continue;

					int indexOld = this._flexH.Cols[row["COL_NAME"].ToString()].Index;
					int indexNew = row["SEQ"].ToInt();

					// 순서 설정
					if (indexOld != indexNew)
						this._flexH.Cols.Move(indexOld, indexNew);

					// 넓이 설정
					if (row["COL_WIDTH"].ToInt() > -1)
						this._flexH.Cols[indexNew].Width = row["COL_WIDTH"].ToInt();

					// Visible 설정
					this._flexH.Cols[indexNew].Visible = row["VISIBLE"].ToBoolean();
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn설정불러오기.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Cbo품목그룹_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this._flexH.RemoveFilter();

				this.SetColumn();
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
				MsgControl.ShowMsg("조회 중입니다. \n잠시만 기다려주세요!");
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  this.cbo품목그룹.SelectedValue,
																	  this.txt수주번호.Text,
																	  this.ctx매출처.CodeValue,
																	  this.dtp납기일자.StartDateToString,
																	  this.dtp납기일자.EndDateToString,
																	  this.txt도면번호.Text,
																	  (this.chk납품완료제외.Checked == true ? "Y" : "N"),
																	  this.cbo경로유형.SelectedValue,
																	  (this.chk수주종결제외.Checked == true ? "Y" : "N"),
																	  (this.chk납품의뢰제외.Checked == true ? "Y" : "N") });
				MsgControl.CloseMsg();
				if (!this._flexH.HasNormalRow)
				{
					this.ShowMessage(PageResultMode.SearchNoData);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void SetColumn()
		{
			DataTable dt;
			string query;

			try
			{
				MsgControl.ShowMsg("컬럼 적용 중입니다. \n잠시만 기다려주세요!");

				dt = DBHelper.GetDataTable("SP_CZ_PU_PO_ITEM_GRP_RPT_COLUMNS_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																								this.cbo품목그룹.SelectedValue.ToString(),
																								this.cbo경로유형.SelectedValue.ToString() });

				this._flexH.BeginSetting(2, 1, false);

				if (this._금액권한)
					this._flexH.Cols.Count = 24;
				else
					this._flexH.Cols.Count = 23;

				foreach (DataRow dr in dt.Rows)
				{
					this._flexH.SetCol("DC_" + dr["GRP_ITEM"].ToString(), "도면번호", 100);
					this._flexH.SetCol("DC_" + dr["GRP_ITEM"].ToString() + "2", "발주번호", 100);
					this._flexH.SetCol("DC_" + dr["GRP_ITEM"].ToString() + "1", "납품예정일", 100);
					this._flexH.Cols["DC_" + dr["GRP_ITEM"].ToString() + "2"].Visible = false;
					this._flexH.Cols["DC_" + dr["GRP_ITEM"].ToString() + "1"].TextAlign = TextAlignEnum.CenterCenter;

					this._flexH[0, this._flexH.Cols["DC_" + dr["GRP_ITEM"].ToString()].Index] = dr["NM_ITEMGRP"].ToString();
					this._flexH[0, this._flexH.Cols["DC_" + dr["GRP_ITEM"].ToString() + "2"].Index] = dr["NM_ITEMGRP"].ToString();
					this._flexH[0, this._flexH.Cols["DC_" + dr["GRP_ITEM"].ToString() + "1"].Index] = dr["NM_ITEMGRP"].ToString();
				}

				this._flexH.AllowCache = false;
				this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}
