using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_SA_EALRY_WARNING_SYSTEM_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_SA_EALRY_WARNING_SYSTEM_SUB()
        {
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
            #region 메시지
            this._flex메시지.BeginSetting(1, 1, false);

            this._flex메시지.SetCol("CD_EWS", "코드", 100, true);
            this._flex메시지.SetCol("DC_EWS", "메시지(한글)", 200, true);
            this._flex메시지.SetCol("DC_EWS1", "메시지(영문)", 200, true);
            this._flex메시지.SetCol("DC_EWS2", "경고레벨", 100, true);
            this._flex메시지.SetCol("DC_EWS3", "비고", 100, true);

            this._flex메시지.SettingVersion = "0.0.0.1";
            this._flex메시지.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 조건
            this._flex조건.BeginSetting(1, 1, false);

            this._flex조건.SetCol("CD_EWS", "코드", 100, true);
            this._flex조건.SetCol("DC_EWS", "값", 100, true);
            this._flex조건.SetCol("DC_EWS1", "비고", 500, true);

            this._flex조건.SettingVersion = "0.0.0.1";
            this._flex조건.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 경과일수
            this._flex경과일수.BeginSetting(1, 1, false);

            this._flex경과일수.SetCol("DT_RCP_PREARRANGED", "수금예정일", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex경과일수.SetCol("DT_FIRST_WARNING", "1차경고", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex경과일수.SetCol("DT_SECOND_WARNING", "2차경고", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex경과일수.SetCol("DT_FINAL_WARNING", "최종경고", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex경과일수.SetCol("DC_RMK", "비고", 500, true);

            this._flex경과일수.SettingVersion = "0.0.0.1";
            this._flex경과일수.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 예외
            this._flex예외.BeginSetting(1, 1, false);

            this._flex예외.SetCol("IDX_HST", "순번", 80);
            this._flex예외.SetCol("USE_YN", "거래처사용유무", 80, true, CheckTypeEnum.Y_N);
            this._flex예외.SetCol("YN_USE_GIR", "협조전사용유무", 80, true, CheckTypeEnum.Y_N);
            this._flex예외.SetCol("CD_PARTNER", "매출처코드", 100, true);
            this._flex예외.SetCol("LN_PARTNER", "매출처명", 200, false);
            this._flex예외.SetCol("DT_EXPIRE", "만료일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex예외.SetCol("AM_LIMIT", "한도금액", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex예외.SetCol("YN_PAY", "지불조건예외", 80, true, CheckTypeEnum.Y_N);
            this._flex예외.SetCol("DC_RMK", "비고", 500, true);

            this._flex예외.SettingVersion = "0.0.0.1";
            this._flex예외.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

		private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn제거.Click += new EventHandler(this.btn제거_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this.btn결과적용.Click += new EventHandler(this.btn결과적용_Click);

            this._flex예외.AfterEdit += new RowColEventHandler(this._flex예외_AfterEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
            this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

            this.btn결과적용.Enabled = false;

            if (Global.MainFrame.LoginInfo.CompanyCode == "K200" &&
                Global.MainFrame.LoginInfo.UserID != "D-004A")
			{
                this._flex메시지.Enabled = false;
                this._flex조건.Enabled = false;
                this._flex경과일수.Enabled = false;
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataSet ds;

            try
            {
                if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                    return;
                }

                ds = DBHelper.GetDataSet("SP_CZ_SA_EALRY_WARNING_SYSTEM_SUB_S", new object[] { this.ctx회사.CodeValue,
                                                                                               (this.chk시뮬레이션.Checked == true ? "Y" : "N") });

                this._flex메시지.Binding = ds.Tables[0];
                this._flex조건.Binding = ds.Tables[1];
                this._flex경과일수.Binding = ds.Tables[2];
                this._flex예외.Binding = ds.Tables[3];

                this.ctx회사.ReadOnly = ReadOnly.TotalReadOnly;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            FlexGrid grid;
            
            try
            {
                if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                    return;
                }

                if (this.tabControl1.SelectedIndex == 0)
                    grid = this._flex메시지;
                else if (this.tabControl1.SelectedIndex == 1)
                    grid = this._flex조건;
                else if  (this.tabControl1.SelectedIndex == 2)
                    grid = this._flex경과일수;
                else
                    grid = this._flex예외;

                grid.Rows.Add();
                grid.Row = grid.Rows.Count - 1;

                grid["YN_SIMULATION"] = (this.chk시뮬레이션.Checked == true ? "Y" : "N");

                if (this.tabControl1.SelectedIndex == 0)
                {
                    grid["CD_COMPANY"] = "K100";
                    grid["TP_GUBUN"] = "M";
                }    
                else if (this.tabControl1.SelectedIndex == 1)
                {
                    grid["CD_COMPANY"] = "K100";
                    grid["TP_GUBUN"] = "C";
                }
                else if (this.tabControl1.SelectedIndex == 2)
                {
                    grid["CD_COMPANY"] = "K100";
                }
                else
                {
                    grid["CD_COMPANY"] = this.ctx회사.CodeValue;
                }
                
                grid.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn제거_Click(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                if (this.tabControl1.SelectedIndex == 0)
                    grid = this._flex메시지;
                else if (this.tabControl1.SelectedIndex == 1)
                    grid = this._flex조건;
                else if (this.tabControl1.SelectedIndex == 2)
                    grid = this._flex경과일수;
                else
                    grid = this._flex예외;

                if (!grid.HasNormalRow) return;

                grid.Rows.Remove(grid.Row);
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
                this.Close();
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
                if (this._flex메시지.GetChanges() == null &&
                    this._flex조건.GetChanges() == null &&
                    this._flex경과일수.GetChanges() == null &&
                    this._flex예외.GetChanges() == null) return;

                SpInfoCollection sc = new SpInfoCollection();

                if (this._flex메시지.GetChanges() != null)
                {
                    SpInfo si = new SpInfo();
                    si.DataValue = Util.GetXmlTable(this._flex메시지.GetChanges());
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "SP_CZ_SA_EWS_CONDITION_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                    sc.Add(si);
                }

                if (this._flex조건.GetChanges() != null)
                {
                    SpInfo si = new SpInfo();
                    si.DataValue = Util.GetXmlTable(this._flex조건.GetChanges());
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "SP_CZ_SA_EWS_CONDITION_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                    sc.Add(si);
                }

                if (this._flex경과일수.GetChanges() != null)
                {
                    SpInfo si = new SpInfo();
                    si.DataValue = Util.GetXmlTable(this._flex경과일수.GetChanges());
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "SP_CZ_SA_EWS_WARNING_DATE_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                    sc.Add(si);
                }

                if (this._flex예외.GetChanges() != null)
                {
                    SpInfo si = new SpInfo();
                    si.DataValue = Util.GetXmlTable(this._flex예외.GetChanges());
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "SP_CZ_SA_EWS_EXCEPT_PARTNER_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                    sc.Add(si);
                }

                DBHelper.Save(sc);

                this._flex메시지.AcceptChanges();
                this._flex조건.AcceptChanges();
                this._flex경과일수.AcceptChanges();
                this._flex예외.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn결과적용_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper.ExecuteNonQuery("SP_CZ_SA_EALRY_WARNING_SYSTEM_SUB_CONFIRM", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex예외_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query = string.Empty;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;

                if (flexGrid.Cols[e.Col].Name == "USE_YN")
                {
                    query = @"UPDATE MA_PARTNER
                              SET USE_YN = '" + flexGrid["USE_YN"].ToString() + "'," + Environment.NewLine +
                                @"ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
                                 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
                             "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                             "AND CD_PARTNER = '" + flexGrid["CD_PARTNER"].ToString() + "'";
                }
                else if (flexGrid.Cols[e.Col].Name == "YN_USE_GIR")
                {
                    query = @"UPDATE CZ_MA_PARTNER
                              SET YN_USE_GIR = '" + flexGrid["YN_USE_GIR"].ToString() + "'," + Environment.NewLine +
                                @"ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
                                 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
                             "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                             "AND CD_PARTNER = '" + flexGrid["CD_PARTNER"].ToString() + "'";
                }
                else
                    return;

                DBHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) return false;

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
