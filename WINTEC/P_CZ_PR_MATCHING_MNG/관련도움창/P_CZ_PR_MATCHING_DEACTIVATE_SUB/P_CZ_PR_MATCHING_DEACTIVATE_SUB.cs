using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
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
    public partial class P_CZ_PR_MATCHING_DEACTIVATE_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PR_MATCHING_DEACTIVATE_SUB_BIZ _biz = new P_CZ_PR_MATCHING_DEACTIVATE_SUB_BIZ();

        public P_CZ_PR_MATCHING_DEACTIVATE_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_ID", "일련번호", 100);
            this._flex.SetCol("STA_DEACTIVATE", "상태", 100, true);
            this._flex.SetCol("DC_RMK", "비고", 100, true);
            this._flex.SetCol("YN_DEACTIVATE", "폐기처리여부", 100, false, CheckTypeEnum.Y_N);

            this._flex.SetDataMap("STA_DEACTIVATE", MA.GetCode("CZ_WIN0010", false), "CODE", "NAME");
            this._flex.SetDummyColumn(new string[] { "S" });
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()


        {
			this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

			this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn폐기처리.Click += new EventHandler(this.btn폐기처리_Click);
            this.btn폐기취소.Click += new EventHandler(this.btn폐기취소_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);
        }

		protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo상태.DataSource = MA.GetCode("CZ_WIN0010", true);
            this.cbo상태.DisplayMember = "NAME";
            this.cbo상태.ValueMember = "CODE";
        }

		private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			FlexGrid grid;
			string colName, oldValue, newValue;
			try
			{
				grid = sender as FlexGrid;

				colName = grid.Cols[e.Col].Name;
				oldValue = D.GetString(grid.GetData(e.Row, e.Col));
				newValue = grid.EditData;

				if (colName == "STA_DEACTIVATE")
				{
					if (grid["YN_DEACTIVATE"].ToString() == "Y")
					{
						grid["STA_DEACTIVATE"] = oldValue;
						e.Cancel = true;
						return;
					}
					else
					{
						grid["STA_DEACTIVATE"] = newValue;
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
                if (this._flex["YN_DEACTIVATE"].ToString() == "Y")
                    this._flex.Cols["STA_DEACTIVATE"].AllowEditing = false;
                else
                    this._flex.Cols["STA_DEACTIVATE"].AllowEditing = true;
            }
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}


		private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                if (this._flex.IsDataChanged)
                {
                    Global.MainFrame.ShowMessage("변경 사항이 있습니다. 저장 후 다시 시도해 주세요.");
                    return;
                }
                foreach (DataRow dr in dataRowArray)
                {
                    if (dr["STA_DEACTIVATE"].ToString() != "001")
                    {
                        Global.MainFrame.ShowMessage("상태가 정상인 건만 삭제 가능합니다.");
                        return;
                    }
                }
                foreach (DataRow dr in dataRowArray)
                {
                    dr.Delete();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     Global.MainFrame.LoginInfo.CdPlant,
                                                                     this.cbo상태.SelectedValue,
                                                                     this.txtID번호.Text });

                if (this._flex.DataTable == null || this._flex.DataTable.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.IsDataChanged) return;

                if (!this._biz.Save(this._flex.GetChanges()))
                    return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn폐기처리_Click(object sender, EventArgs e)
		{
            DataRow[] dataRowArray;
            DataTable dt1, dt2;
            string query1, query2, query3, query4, MES번호, 작업지시번호;
            decimal noLine, seqWo;
            object[] outParameters = new object[1];
			try
			{
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (!this._flex.HasNormalRow) return;
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                if (this._flex.IsDataChanged)
				{
                    Global.MainFrame.ShowMessage("변경 사항이 있습니다. 저장 후 다시 시도해 주세요.");
                    return;
				}
                foreach (DataRow dr in dataRowArray)
				{
                    if (dr["STA_DEACTIVATE"].ToString() != "004")
					{
                        Global.MainFrame.ShowMessage("상태가 폐기가 아닌 건이 포함되어 있습니다.");
                        return;
					}
                    if (dr["YN_DEACTIVATE"].ToString() == "Y")
                    {
                        Global.MainFrame.ShowMessage("이미 폐기처리 된 건이 포함되어 있습니다.");
                        return;
                    }
                }
                query1 = @"SELECT MAX(LM.NO_MES) AS NO_MES, WO.NO_WO, WR.NO_LINE, WD.SEQ_WO
                           FROM PR_WO WO
                           LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
                           LEFT JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO
                           LEFT JOIN PR_LINK_MES LM ON LM.CD_COMPANY = WR.CD_COMPANY AND LM.NO_WO = WR.NO_WO AND LM.CD_OP = WR.CD_OP
                           WHERE WD.NO_ID = '{0}'
                           AND WR.YN_FINAL = 'Y'
                           AND LM.QT_MOVE > 0
                           GROUP BY WO.NO_WO, WR.NO_LINE, WD.SEQ_WO"; 
                query2 = @"SELECT *
                           FROM PR_LINK_MES WIRH(NOLOCK)
                           WHERE NO_MES = '{0}'";
                query3 = @"UPDATE CZ_PR_WO_INSP
                           SET NO_INSP = -1
                           WHERE NO_WO = '{0}'
                           AND NO_LINE = '{1}'
                           AND SEQ_WO = '{2}'";
                query4 = @"UPDATE CZ_PR_MATCHING_DEACTIVATE
                           SET YN_DEACTIVATE = 'Y'
                           WHERE NO_ID = '{0}'";
                foreach (DataRow dr in dataRowArray)
				{
                    dt1 = DBHelper.GetDataTable(string.Format(query1, dr["NO_ID"].ToString()));
                    if (dt1 == null || dt1.Rows.Count == 0) return;
                    MES번호 = dt1.Rows[0]["NO_MES"].ToString();
                    작업지시번호 = dt1.Rows[0]["NO_WO"].ToString();
                    noLine = D.GetDecimal(dt1.Rows[0]["NO_LINE"]);
                    seqWo = D.GetDecimal(dt1.Rows[0]["SEQ_WO"]);
                    dt2 = DBHelper.GetDataTable(string.Format(query2, MES번호));
                    if (dt2 == null || dt2.Rows.Count == 0) return;
                    DBHelper.ExecuteNonQuery("UP_PR_LINK_MES_REG_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    Global.MainFrame.LoginInfo.CdPlant,
                                                                                    MES번호 });
                    DataRow row = dt2.Rows[0];
                    row["QT_REJECT"] = Convert.ToDecimal(row["QT_REJECT"]) + 1;
                    row["QT_BAD"] = Convert.ToDecimal(row["QT_BAD"]) + 1;
                    DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_I", new object[] { row["CD_COMPANY"],
                                                                                   row["CD_PLANT"],
                                                                                   row["CD_ITEM"],
                                                                                   row["NO_EMP"],
                                                                                   row["CD_WC"],
                                                                                   row["CD_OP"],
                                                                                   row["CD_WCOP"],
                                                                                   "N",
                                                                                   row["NO_WO"],
                                                                                   "Y",
                                                                                   D.GetDecimal(row["QT_WORK"]),
                                                                                   D.GetDecimal(row["QT_REJECT"]),
                                                                                   D.GetDecimal(row["QT_BAD"]),
                                                                                   row["CD_SL_IN"],
                                                                                   row["CD_SL_BAD_IN"],
                                                                                   row["YN_REWORK"],
                                                                                   row["YN_MANDAY"],
                                                                                   0,
                                                                                   0,
                                                                                   0,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   row["CD_EQUIP"],
                                                                                   999,
                                                                                   999,
                                                                                   string.Empty,
                                                                                   row["ID_INSERT"] }, out outParameters);
                    DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_BATCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       outParameters[0].ToString() });
                    DBHelper.ExecuteScalar(string.Format(string.Format(query3, 작업지시번호, noLine, seqWo)));
                    DBHelper.ExecuteScalar(string.Format(string.Format(query4, dr["NO_ID"].ToString())));
                }
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn폐기처리.Text);
                this.btn조회_Click(null, null);
            }
            catch (Exception ex)
			{
                Global.MainFrame.MsgEnd(ex);
			}
		}

        private void btn폐기취소_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            DataTable dt1, dt2;
            string query1, query2, query3, query4, MES번호, 작업지시번호, CD_REJECT, CD_RESOURCE;
            decimal noLine, seqWo;
            object[] outParameters = new object[1];
            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (!this._flex.HasNormalRow) return;
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                if (this._flex.IsDataChanged)
                {
                    Global.MainFrame.ShowMessage("변경 사항이 있습니다. 저장 후 다시 시도해 주세요.");
                    return;
                }
                foreach (DataRow dr in dataRowArray)
                {
                    if (dr["YN_DEACTIVATE"].ToString() != "Y")
                    {
                        Global.MainFrame.ShowMessage("폐기처리가 안 된 건이 포함되어 있습니다.");
                        return;
                    }
                }
                query1 = @"SELECT MAX(LM.NO_MES) AS NO_MES, WO.NO_WO, WR.NO_LINE, WD.SEQ_WO
                           FROM PR_WO WO
                           LEFT JOIN PR_WO_ROUT WR ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
                           LEFT JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO
                           LEFT JOIN PR_LINK_MES LM ON LM.CD_COMPANY = WR.CD_COMPANY AND LM.NO_WO = WR.NO_WO AND LM.CD_OP = WR.CD_OP
                           WHERE WD.NO_ID = '{0}'
                           AND WR.YN_FINAL = 'Y'
                           AND LM.QT_REJECT > 0
                           GROUP BY WO.NO_WO, WR.NO_LINE, WD.SEQ_WO";
                query2 = @"SELECT *
                           FROM PR_LINK_MES WIRH(NOLOCK)
                           WHERE NO_MES = '{0}'";
                query3 = @"UPDATE CZ_PR_WO_INSP
                           SET NO_INSP = 999
                           WHERE NO_WO = '{0}'
                           AND NO_LINE = '{1}'
                           AND SEQ_WO = '{2}'";
                query4 = @"UPDATE CZ_PR_MATCHING_DEACTIVATE
                           SET YN_DEACTIVATE = 'N'
                           WHERE NO_ID = '{0}'";
                foreach (DataRow dr in dataRowArray)
                {
                    dt1 = DBHelper.GetDataTable(string.Format(query1, dr["NO_ID"].ToString()));
                    if (dt1 == null || dt1.Rows.Count == 0) return;
                    MES번호 = dt1.Rows[0]["NO_MES"].ToString();
                    작업지시번호 = dt1.Rows[0]["NO_WO"].ToString();
                    noLine = D.GetDecimal(dt1.Rows[0]["NO_LINE"]);
                    seqWo = D.GetDecimal(dt1.Rows[0]["SEQ_WO"]);
                    dt2 = DBHelper.GetDataTable(string.Format(query2, MES번호));
                    if (dt2 == null || dt2.Rows.Count == 0) return;
                    DBHelper.ExecuteNonQuery("UP_PR_LINK_MES_REG_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    Global.MainFrame.LoginInfo.CdPlant,
                                                                                    MES번호 });
                    DataRow row = dt2.Rows[0];
                    row["QT_REJECT"] = Convert.ToDecimal(row["QT_REJECT"]) - 1;
                    row["QT_BAD"] = Convert.ToDecimal(row["QT_BAD"]) - 1;
                    if (Convert.ToDecimal(row["QT_REJECT"]) == 0)
					{
                        CD_REJECT = string.Empty;
                        CD_RESOURCE = string.Empty;
                    }
					else
					{
                        CD_REJECT = "999";
                        CD_RESOURCE = "999";
                    }
                    DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_I", new object[] { row["CD_COMPANY"],
                                                                                   row["CD_PLANT"],
                                                                                   row["CD_ITEM"],
                                                                                   row["NO_EMP"],
                                                                                   row["CD_WC"],
                                                                                   row["CD_OP"],
                                                                                   row["CD_WCOP"],
                                                                                   "N",
                                                                                   row["NO_WO"],
                                                                                   "Y",
                                                                                   D.GetDecimal(row["QT_WORK"]),
                                                                                   D.GetDecimal(row["QT_REJECT"]),
                                                                                   D.GetDecimal(row["QT_BAD"]),
                                                                                   row["CD_SL_IN"],
                                                                                   row["CD_SL_BAD_IN"],
                                                                                   row["YN_REWORK"],
                                                                                   row["YN_MANDAY"],
                                                                                   0,
                                                                                   0,
                                                                                   0,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   row["CD_EQUIP"],
                                                                                   CD_REJECT,
                                                                                   CD_RESOURCE,
                                                                                   string.Empty,
                                                                                   row["ID_INSERT"] }, out outParameters);
                    DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_BATCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       outParameters[0].ToString() });
                    DBHelper.ExecuteScalar(string.Format(string.Format(query3, 작업지시번호, noLine, seqWo)));
                    DBHelper.ExecuteScalar(string.Format(string.Format(query4, dr["NO_ID"].ToString())));
                }
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn폐기취소.Text);
                this.btn조회_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
