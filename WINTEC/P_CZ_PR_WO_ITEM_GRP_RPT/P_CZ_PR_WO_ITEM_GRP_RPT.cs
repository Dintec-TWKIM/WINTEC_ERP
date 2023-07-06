using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
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
	public partial class P_CZ_PR_WO_ITEM_GRP_RPT : PageBase
	{
		P_CZ_PR_WO_ITEM_GRP_RPT_BIZ _biz = new P_CZ_PR_WO_ITEM_GRP_RPT_BIZ();

		private bool _금액권한 = true;

		public P_CZ_PR_WO_ITEM_GRP_RPT()
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

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexD, this._flexDL };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexD };
			this._flexD.DetailGrids = new FlexGrid[] { this._flexDL };

            #region Header
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
			this._flexH.SetCol("DC1", "비고", 100, true);
			this._flexH.SetCol("TXT_USERDEF8", "비고1", 100, true);
			this._flexH.SetCol("DC_RMK1", "비고2", 100, true);

			this._flexH.SetDummyColumn("S", "DC1", "TXT_USERDEF8", "DC_RMK1");

			//this._flex.AllowCache = false;
			this._flexH.SettingVersion = "0.0.0.2";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.SelectionMode = SelectionModeEnum.Cell;

			this._flexH.Styles.Add("외주").BackColor = Color.Brown;
			this._flexH.Styles.Add("외주").ForeColor = Color.White;
			this._flexH.Styles.Add("선삭").BackColor = Color.Blue;
			this._flexH.Styles.Add("선삭").ForeColor = Color.White;
			this._flexH.Styles.Add("MCT").BackColor = Color.Orange;
			this._flexH.Styles.Add("MCT").ForeColor = Color.Black;
			this._flexH.Styles.Add("열처리").BackColor = Color.Yellow;
			this._flexH.Styles.Add("열처리").ForeColor = Color.Black;
			this._flexH.Styles.Add("마킹").BackColor = Color.Purple;
			this._flexH.Styles.Add("마킹").ForeColor = Color.White;
			this._flexH.Styles.Add("연마").BackColor = Color.Green;
			this._flexH.Styles.Add("연마").ForeColor = Color.White;
			this._flexH.Styles.Add("조립").BackColor = Color.Navy;
			this._flexH.Styles.Add("조립").ForeColor = Color.White;
			this._flexH.Styles.Add("완료").BackColor = Color.Gray;
			this._flexH.Styles.Add("완료").ForeColor = Color.White;
			this._flexH.Styles.Add("없음").ForeColor = Color.Black;
			this._flexH.Styles.Add("없음").BackColor = Color.White;
			#endregion

			#region Detail
			this._flexD.BeginSetting(1, 1, false);

			this._flexD.SetCol("IDX", "우선순위", 100);
			this._flexD.SetCol("NO_WO", "작업지시번호", 100);
			this._flexD.SetCol("NM_WC", "작업장", 100);
			this._flexD.SetCol("DC_RMK", "재고공정", 100);
			this._flexD.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flexD.SettingVersion = "0.0.0.1";
			this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region _flexDL
			this._flexDL.BeginSetting(1, 1, false);

			this._flexDL.SetCol("CD_OP", "OP", 40);
			this._flexDL.SetCol("NM_OP", "공정명", 100);
			this._flexDL.SetCol("NM_WC", "작업장명", 80);
			this._flexDL.SetCol("NM_ST_OP", "상태", 60);
			this._flexDL.SetCol("QT_WO", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_START", "입고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("QT_WIP", "대기수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexDL.SetCol("RT_WORK", "진행율(%)", 70, false, typeof(decimal), FormatTpType.MONEY);
			this._flexDL.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexDL.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexDL.SetCol("DC_RMK", "비고", 100, 100, true);
			this._flexDL.SetCol("DC_RMK_1", "비고1", 100, 100, true);
			this._flexDL.SetCol("NO_SFT", "SFT", 80);
			this._flexDL.SetCol("NM_SFT", "SFT명", 100);
			this._flexDL.SetCol("CD_EQUIP", "설비코드", 80, true);
			this._flexDL.SetCol("NM_EQUIP", "설비명", 100);
			this._flexDL.SetCol("NO_EMP", "담당자코드", 100);
			this._flexDL.SetCol("NM_KOR", "담당자명", 100);

			this._flexDL.SettingVersion = "0.0.0.1";
			this._flexDL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
		{
			this._flexH.OwnerDrawCell += _flexH_OwnerDrawCell;

			this._flexH.AfterSelChange += _flexH_AfterSelChange;
			this._flexH.AfterEdit += _flex_AfterEdit;

			this._flexD.AfterRowChange += _flexD_AfterRowChange;

			this.btn검사신청.Click += Btn검사신청_Click;
			this.btn납품의뢰.Click += Btn납품의뢰_Click;
			this.btn포장완료.Click += Btn포장완료_Click;
			this.btnTRUST마킹.Click += BtnTRUST마킹_Click;

			this.btn설정저장.Click += Btn설정저장_Click;
			this.btn설정불러오기.Click += Btn설정불러오기_Click;

			this.cbo품목그룹.SelectedIndexChanged += this.Cbo품목그룹_SelectedIndexChanged;
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
				sql.Parameter.Add2("@GRID_UID", "P_CZ_PR_WO_ITEM_GRP_RPT" + "_" + this.cbo품목그룹.SelectedValue.ToString());
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
				sql.Parameter.Add2("@GRID_UID", "P_CZ_PR_WO_ITEM_GRP_RPT" + "_" + this.cbo품목그룹.SelectedValue.ToString());
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

		private void _flexD_AfterRowChange(object sender, RangeEventArgs e)
        {
			DataTable dt;

			try
            {
				dt = this._biz.SearchDetail1(new object[] { this.LoginInfo.CompanyCode,
															this.LoginInfo.CdPlant,
															this._flexD["NO_WO"].ToString() });
				this._flexDL.Binding = dt;
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
			string 도면번호;

            try
            {
				if (this._flexH.Rows[1][this._flexH.ColSel] == null ||
					this._flexH.Rows[1][this._flexH.ColSel].ToString() != "도면번호" || 
					!this._flexH.Cols[this._flexH.ColSel].Name.Contains("DC_") ||
					this._flexH.RowSel < this._flexH.Rows.Fixed)
					return;

				obj = this._flexH.Rows[this._flexH.RowSel][this._flexH.ColSel];

				if (obj == null) return;

				도면번호 = obj.ToString();

				if (string.IsNullOrEmpty(도면번호)) return;

				dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														   this._flexH["NO_SO"].ToString(),
														   this._flexH["SEQ_SO"].ToString(),
														   도면번호 });
				this._flexD.Binding = dt;
            }
			catch (Exception ex)
            {
				this.MsgEnd(ex);
            }
        }

        private void Btn포장완료_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			
			try
			{
				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						this._biz.포장완료(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														  dr["NO_SO"].ToString(),
														  dr["SEQ_SO"].ToString(),
														  Global.MainFrame.LoginInfo.UserID});
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn포장완료.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnTRUST마킹_Click(object sender, EventArgs e)
        {
			DataRow[] dataRowArray;
			try
			{
                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
				{
					foreach (DataRow dr in dataRowArray)
                    {
						this._biz.TRUST마킹(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														   dr["NO_SO"].ToString(),
														   dr["SEQ_SO"].ToString(),
														   Global.MainFrame.LoginInfo.UserID });
                    }
                }
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnTRUST마킹.Text);
                
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
        }

		private void Btn납품의뢰_Click(object sender, EventArgs e)
		{
			decimal 재고수량, 의뢰수량;
			string query;
			try
			{
				if (this.ShowMessage("입력된 수량 만큼 납품의뢰 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				if (this.cur납품의뢰.DecimalValue <= 0)
				{
					this.ShowMessage(공통메세지._은_보다커야합니다, "납품의뢰수량", "0");
					return;
				}

				query = @"SELECT  
	SUM(X.QT_INV) AS QT_INV
FROM (  
	SELECT CD_COMPANY, CD_PLANT, CD_SL, CD_ITEM, QT_GOOD_INV QT_INV 
	FROM MM_OPENQTL  
	WHERE CD_COMPANY = 'W100'  
	AND CD_PLANT = 'F001'  
	AND YM_STANDARD = LEFT(CONVERT(VARCHAR(8), GETDATE(), 112)  , 4) + '00'
	UNION ALL    
	SELECT CD_COMPANY, CD_PLANT, CD_SL, CD_ITEM, SUM(QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR + QT_TRANS_GR) - SUM(QT_GOOD_GI + QT_REJECT_GI + QT_INSP_GI + QT_TRANS_GI) QT_INV
	FROM MM_OHSLINVM  
	WHERE CD_COMPANY = 'W100'  
	AND YM_IO >= LEFT(CONVERT(VARCHAR(8), GETDATE(), 112)  , 4) + '00'   
	AND YM_IO <= LEFT(CONVERT(VARCHAR(8), GETDATE(), 112)  , 6) - 1  
	AND CD_PLANT = 'F001'     
	GROUP BY CD_COMPANY, CD_PLANT, CD_SL , CD_ITEM  
	UNION ALL  
	SELECT L.CD_COMPANY, L.CD_PLANT, L.CD_SL, L.CD_ITEM, SUM(L.QT_GOOD_GR - L.QT_GOOD_GI + L.QT_REJECT_GR - L.QT_REJECT_GI + L.QT_TRANS_GR - L.QT_TRANS_GI + L.QT_INSP_GR - L.QT_INSP_GI) AS QT_INV
	FROM MM_OHSLINVD L 
	INNER JOIN DZSN_MA_PITEM P ON P.CD_COMPANY = L.CD_COMPANY  
	AND P.CD_PLANT = L.CD_PLANT  
	AND P.CD_ITEM = L.CD_ITEM  
	WHERE L.CD_COMPANY = 'W100'  
	AND L.CD_PLANT = 'F001'  
	AND L.DT_IO <= CONVERT(VARCHAR(8), GETDATE(), 112)    
	AND L.DT_IO > LEFT(CONVERT(VARCHAR(8), GETDATE(), 112)  , 6) + '00'  
	GROUP BY L.CD_COMPANY, L.CD_PLANT, L.CD_SL, L.CD_ITEM 
 ) X  
INNER JOIN DZSN_MA_PITEM P ON P.CD_COMPANY = X.CD_COMPANY AND P.CD_PLANT = X.CD_PLANT AND P.CD_ITEM = X.CD_ITEM AND P.CD_SL = X.CD_SL
WHERE P.YN_USE = 'Y'
AND X.CD_COMPANY = '{0}'
AND P.CD_ITEM = '{1}'
GROUP BY X.CD_COMPANY, X.CD_PLANT, X.CD_ITEM, X.CD_SL
HAVING SUM(X.QT_INV) > 0";

				재고수량 = D.GetDecimal(DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flexH["CD_ITEM"].ToString())));

				query = @"SELECT SUM(QT_GIR - QT_GI)
FROM SA_GIRL
WHERE CD_COMPANY = '{0}'
AND CD_ITEM = '{1}'
GROUP BY CD_ITEM";

				의뢰수량 = D.GetDecimal(DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flexH["CD_ITEM"].ToString())));

				if (재고수량 - 의뢰수량 < this.cur납품의뢰.DecimalValue)
				{
					this.ShowMessage("재고 수량이 부족하여 납품의뢰를 등록할 수 없습니다.");
					return;
				}

					this._biz.납품의뢰(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
												  this._flexH["NO_SO"].ToString(),
												  this._flexH["SEQ_SO"].ToString(),
												  this.cur납품의뢰.DecimalValue,
												  Global.MainFrame.LoginInfo.UserID });

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn납품의뢰.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn검사신청_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
				
				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach(DataRow dr in dataRowArray)
					{
						this._biz.검사신청(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
													      dr["NO_SO"].ToString(),
														  Global.MainFrame.LoginInfo.UserID });
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn검사신청.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
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
				if (flexGrid.Cols[e.Col].Name != "DC1" && 
					flexGrid.Cols[e.Col].Name != "TXT_USERDEF8") return;

				query = @"UPDATE SL
SET SL.DC1 = '{3}',
	SL.TXT_USERDEF8 = '{4}'
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
															flexGrid["NO_SO"].ToString(), 
															flexGrid["SEQ_SO"].ToString(),
															flexGrid["DC1"].ToString(),
															flexGrid["TXT_USERDEF8"].ToString()));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;
				if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed) return;

				if (this._금액권한)
					if (e.Col < 25) return;
				else
					if (e.Col < 24) return;
				
				if (this._flexH[e.Row, e.Col] == null) return;

				CellStyle cellStyle = this._flexH.GetCellStyle(e.Row, e.Col);

				if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "선삭") || 
					(this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "선삭") ||
					(this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "선삭") ||
					(this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "선삭") ||
					(this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "선삭"))
				{
					if (cellStyle == null || cellStyle.Name != "선삭")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["선삭"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "MCT") ||
					     (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "MCT") ||
					     (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "MCT") ||
					     (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "MCT") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "MCT"))
				{
					if (cellStyle == null || cellStyle.Name != "MCT")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["MCT"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "열처리") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "열처리") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "열처리") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "열처리") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "열처리"))
				{
					if (cellStyle == null || cellStyle.Name != "열처리")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["열처리"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "마킹") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "마킹") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "마킹") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "마킹") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "마킹"))
				{
					if (cellStyle == null || cellStyle.Name != "마킹")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["마킹"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "연마") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "연마") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "연마") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "연마") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "연마"))
				{
					if (cellStyle == null || cellStyle.Name != "연마")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["연마"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "부가공정") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "부가공정") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "부가공정") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "부가공정") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "부가공정"))
				{
					if (cellStyle == null || cellStyle.Name != "외주")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["외주"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "조립") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "조립") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "조립") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "조립") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "조립"))
				{
					if (cellStyle == null || cellStyle.Name != "조립")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["조립"]);
				}
				else if ((this._flexH[e.Row, e.Col] != null && this._flexH[e.Row, e.Col].ToString() == "완료") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "완료") ||
						 (this._flexH[e.Row, e.Col + 1] != null && this._flexH[e.Row, e.Col + 1].ToString() == "완료") ||
						 (this._flexH[e.Row, e.Col - 1] != null && this._flexH[e.Row, e.Col - 1].ToString() == "완료") ||
						 (this._flexH.Cols[e.Col].Name == "DC_2VP20" && this._flexH[e.Row, e.Col + 2] != null && this._flexH[e.Row, e.Col + 2].ToString() == "완료"))
				{
					if (cellStyle == null || cellStyle.Name != "완료")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["완료"]);
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "없음")
						this._flexH.SetCellStyle(e.Row, e.Col, this._flexH.Styles["없음"]);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
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
ORDER BY IG.NM_ITEMGRP ASC";

			this.cbo품목그룹.DataSource = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));
		    this.cbo품목그룹.ValueMember = "CD_ITEMGRP";
			this.cbo품목그룹.DisplayMember = "NM_ITEMGRP";

			this.cbo경로유형.DataSource = DBHelper.GetDataTable("UP_PR_PATN_ROUT_ALL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										 Global.SystemLanguage.MultiLanguageLpoint });
			this.cbo경로유형.DisplayMember = "NAME";
			this.cbo경로유형.ValueMember = "CODE";

			this.cbo진행상태.DataSource = MA.GetCodeUser(new string[] { "", "001", "002", "003", "004", "005", "006" }, new string[] { "", "생산완료", "현합완료", "조립완료", "검사신청", "검사완료", "포장완료" });
			this.cbo진행상태.ValueMember = "CODE";
			this.cbo진행상태.DisplayMember = "NAME";
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flexH.Binding = this._biz.Search(this.cbo품목그룹.SelectedValue.ToString(), new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										        this.txt수주번호.Text,
																												this.ctx매출처.CodeValue,
																												this.dtp납기일자.StartDateToString,
																												this.dtp납기일자.EndDateToString,
																												this.txt도면번호.Text,
																												(this.chk납품완료제외.Checked == true ? "Y" : "N"),
																											    this.cbo경로유형.SelectedValue,
																												this.cbo진행상태.SelectedValue,
																												(this.chk수주종결제외.Checked == true ? "Y" : "N"),
																												(this.chk납품의뢰제외.Checked == true ? "Y" : "N") });

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
				query = @";WITH ITEM_GRP
(
	CD_COMPANY,
	CD_ITEMGRP,
	NM_ITEMGRP,
    HD_ITEMGRP,
	DC_RMK,
	LEVEL,
	PATH,
	TXT_USERDEF2
)
AS
(
	SELECT CD_COMPANY,
		   CD_ITEMGRP,
	       NM_ITEMGRP,
		   HD_ITEMGRP,
		   DC_RMK,
		   LEVEL = 1,
		   CONVERT(VARCHAR(1000), CD_ITEMGRP),
		   TXT_USERDEF2
	FROM MA_ITEMGRP WITH(NOLOCK)
	WHERE CD_COMPANY = '{0}'
	AND CD_ITEMGRP = '{1}'
	UNION ALL
	SELECT IG1.CD_COMPANY,
		   IG1.CD_ITEMGRP,
	       IG1.NM_ITEMGRP,
		   IG1.HD_ITEMGRP,
		   IG1.DC_RMK,
		   LEVEL + 1, 
		   CONVERT(VARCHAR(1000), PATH + ' -> ' + IG1.CD_ITEMGRP),
		   IG1.TXT_USERDEF2
	FROM ITEM_GRP IG
	JOIN MA_ITEMGRP IG1 WITH(NOLOCK) ON IG1.CD_COMPANY = IG.CD_COMPANY AND IG1.HD_ITEMGRP = IG.CD_ITEMGRP
)
SELECT CD_ITEMGRP,
	   NM_ITEMGRP
FROM ITEM_GRP
WHERE ISNULL(TXT_USERDEF2, 'N') <> 'Y'
ORDER BY DC_RMK ASC";

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
																this.cbo품목그룹.SelectedValue.ToString()));

				this._flexH.BeginSetting(2, 1, false);

				if (this._금액권한)
					this._flexH.Cols.Count = 25;
				else
					this._flexH.Cols.Count = 24;

				foreach (DataRow dr in dt.Rows)
				{
					this._flexH.SetCol("DC_" + dr["CD_ITEMGRP"].ToString(), "도면번호", 100);

					if (dr["CD_ITEMGRP"].ToString() == "2VP20")
						this._flexH.SetCol("DC_" + dr["CD_ITEMGRP"].ToString() + "3", "규격", 100);
					
					this._flexH.SetCol("DC_" + dr["CD_ITEMGRP"].ToString() + "2", "작업장", 100);
					this._flexH.SetCol("DC_" + dr["CD_ITEMGRP"].ToString() + "1", "재고공정", 100);

                    this._flexH[0, this._flexH.Cols["DC_" + dr["CD_ITEMGRP"].ToString()].Index] = dr["NM_ITEMGRP"].ToString();

                    if (dr["CD_ITEMGRP"].ToString() == "2VP20")
                        this._flexH[0, this._flexH.Cols["DC_" + dr["CD_ITEMGRP"].ToString() + "3"].Index] = dr["NM_ITEMGRP"].ToString();

                    this._flexH[0, this._flexH.Cols["DC_" + dr["CD_ITEMGRP"].ToString() + "2"].Index] = dr["NM_ITEMGRP"].ToString();
                    this._flexH[0, this._flexH.Cols["DC_" + dr["CD_ITEMGRP"].ToString() + "1"].Index] = dr["NM_ITEMGRP"].ToString();
                }

                //this._flex.AllowCache = false;
                this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}
