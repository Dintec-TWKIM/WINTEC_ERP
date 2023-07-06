using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Duzon.Common.Forms;
using DzHelpFormLib;
using Duzon.Common.BpControls;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using System.Windows.Forms;
using Duzon.ERPU.Grant;

namespace cz
{
    public partial class P_CZ_SA_USER_SUB : Duzon.Common.Forms.CommonDialog
    {
        FreeBinding _fb;
        P_CZ_SA_USER_SUB_BIZ _biz;

        public P_CZ_SA_USER_SUB()
        {
            InitializeComponent();

            this._biz = new P_CZ_SA_USER_SUB_BIZ();
            this._fb = new FreeBinding();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
        }

        protected override void InitPaint()
        {
            DataTable dt;

            try
            {
                base.InitPaint();

                UGrant ugrant = new UGrant();

                if (ugrant.GrantButton("P_CZ_SA_INQ_RPT", "ID_USER"))
                {
                    this.chk할당제외.Visible = true;
                    this.ctx사용자ID.ReadOnly = ReadOnly.None;
                }
                else
                {
                    this.chk할당제외.Visible = false;
                    this.ctx사용자ID.ReadOnly = ReadOnly.TotalReadOnly;
                }
                
                this.ctx사용자ID.CodeValue = Global.MainFrame.LoginInfo.UserID;
                this.ctx사용자ID.CodeName = Global.MainFrame.LoginInfo.UserName;

                this.ctx사용자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
                this.ctx사용자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

                this.cbo담당업무.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_SA00024");
                this.cbo담당업무.ValueMember = "CODE";
                this.cbo담당업무.DisplayMember = "NAME";

                this.cbo매출처그룹.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000065");
                this.cbo매출처그룹.ValueMember = "CODE";
                this.cbo매출처그룹.DisplayMember = "NAME";

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     Global.MainFrame.LoginInfo.Language,
                                                     string.Empty });

                this._fb.SetBinding(dt, this.tlpBorder);
                this._fb.ClearAndNewRow();
                this._fb.AcceptChanges();

                this.btn조회_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.ctx사용자ID.QueryAfter += new BpQueryHandler(this.ctx사용자ID_QueryAfter);

            this.bpc영업유형.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc영업담당.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc영업물류.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc구매담당.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc입력지원.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

            this.bpc영업유형.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc영업담당.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc영업물류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc구매담당.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc입력지원.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn등록.Click += new EventHandler(this.btn등록_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     Global.MainFrame.LoginInfo.Language,
                                                     this.ctx사용자ID.CodeValue });

                this.bpc영업유형.Clear();
                this.bpc영업담당.Clear();
                this.bpc입력지원.Clear();
                this.bpc구매담당.Clear();
                this.bpc영업물류.Clear();

                if (dt.Rows.Count == 0)
                {
                    this._fb.ClearAndNewRow();
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this._fb.SetDataTable(dt);

                    this.chk할당제외.Checked = (D.GetString(this._fb.CurrentRow["YN_EXPECT"]) == "Y" ? true : false);

                    this.기본값설정(this.bpc영업유형, D.GetString(this._fb.CurrentRow["TP_SALES"]));
                    this.bpc영업유형.SelectedValue = this._fb.CurrentRow["TP_SALES_DEF"];

                    this.기본값설정(this.bpc영업담당, D.GetString(this._fb.CurrentRow["ID_SALES"]));
                    this.bpc영업담당.SelectedValue = this._fb.CurrentRow["ID_SALES_DEF"];

                    this.기본값설정(this.bpc입력지원, D.GetString(this._fb.CurrentRow["ID_TYPIST"]));
                    this.bpc입력지원.SelectedValue = this._fb.CurrentRow["ID_TYPIST_DEF"];

                    this.기본값설정(this.bpc구매담당, D.GetString(this._fb.CurrentRow["ID_PUR"]));
                    this.bpc구매담당.SelectedValue = this._fb.CurrentRow["ID_PUR_DEF"];

                    this.기본값설정(this.bpc영업물류, D.GetString(this._fb.CurrentRow["ID_LOG"]));
                    this.bpc영업물류.SelectedValue = this._fb.CurrentRow["ID_LOG_DEF"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 기본값설정(BpComboBox control, string pipeCode)
        {
            string query;
            DataTable dt;

            try
            {
                query = string.Empty;

                if (control.Name == this.bpc영업유형.Name)
                {
                    query = @"SELECT ST.CD_STR AS CODE,
                                     (SELECT (CASE '" + Global.MainFrame.LoginInfo.Language + @"' WHEN 'KR' THEN NM_SYSDEF
		                      						                                              WHEN 'US' THEN NM_SYSDEF_E
		                      						                                              WHEN 'JP' THEN NM_SYSDEF_JP
		                      						                                              WHEN 'CH' THEN NM_SYSDEF_CH END)
		                              FROM MA_CODEDTL
		                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
		                              AND CD_FIELD = 'CZ_SA00023'
		                              AND CD_SYSDEF = ST.CD_STR) AS NAME
                              FROM GETTABLEFROMSPLIT('" + pipeCode + "|') ST";
                }
                else
                {
                    query = "SELECT ST.CD_STR AS CODE," + Environment.NewLine +
                               "(CASE WHEN '" + Global.MainFrame.LoginInfo.Language + "' = 'KR' THEN ME.NM_KOR" + Environment.NewLine +
                                     "WHEN '" + Global.MainFrame.LoginInfo.Language + "' = 'US' THEN ME.NM_ENG" + Environment.NewLine +
                                     "WHEN '" + Global.MainFrame.LoginInfo.Language + "' = 'JP' THEN ME.NM_KOR" + Environment.NewLine +
                                     "WHEN '" + Global.MainFrame.LoginInfo.Language + "' = 'CH' THEN ME.NM_CHIN END) AS NAME" + Environment.NewLine +
                            "FROM GETTABLEFROMSPLIT('" + pipeCode + "|') ST" + Environment.NewLine +
                            "LEFT JOIN MA_USER MU WITH(NOLOCK) ON MU.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND MU.ID_USER = ST.CD_STR" + Environment.NewLine +
                            "LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = MU.CD_COMPANY AND ME.NO_EMP = MU.NO_EMP";
                }
                
                dt = Global.MainFrame.FillDataTable(query);

                foreach (DataRow dr in dt.Rows)
                {
                    control.AddItem(D.GetString(dr["CODE"]), D.GetString(dr["NAME"]));
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx영업그룹.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl영업그룹.Text);
                    this.ctx영업그룹.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(this.ctx1차결재.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl1차결재.Text);
                    this.ctx1차결재.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(this.ctx2차결재.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl2차결재.Text);
                    this.ctx2차결재.Focus();
                    return;
                }

                this._fb.CurrentRow["YN_EXPECT"] = (this.chk할당제외.Checked ? "Y" : "N");

                this._fb.CurrentRow["TP_SALES"] = this.bpc영업유형.QueryWhereIn_Pipe.TrimEnd('|');
                this._fb.CurrentRow["TP_SALES_DEF"] = this.bpc영업유형.SelectedValue;

                this._fb.CurrentRow["ID_SALES"] = this.bpc영업담당.QueryWhereIn_Pipe.TrimEnd('|');
                this._fb.CurrentRow["ID_SALES_DEF"] = this.bpc영업담당.SelectedValue;

                this._fb.CurrentRow["ID_TYPIST"] = this.bpc입력지원.QueryWhereIn_Pipe.TrimEnd('|');
                this._fb.CurrentRow["ID_TYPIST_DEF"] = this.bpc입력지원.SelectedValue;

                this._fb.CurrentRow["ID_PUR"] = this.bpc구매담당.QueryWhereIn_Pipe.TrimEnd('|');
                this._fb.CurrentRow["ID_PUR_DEF"] = this.bpc구매담당.SelectedValue;

                this._fb.CurrentRow["ID_LOG"] = this.bpc영업물류.QueryWhereIn_Pipe.TrimEnd('|');
                this._fb.CurrentRow["ID_LOG_DEF"] = this.bpc영업물류.SelectedValue;

                if (this._biz.SaveData(this._fb.GetChanges(), this.ctx사용자ID.CodeValue) == true)
                {
                    Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    this._fb.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                {
                    if (this._biz.DeleteData(this.ctx사용자ID.CodeValue) == true)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        this._fb.ClearAndNewRow();

                        this.bpc영업유형.Clear();
                        this.bpc영업담당.Clear();
                        this.bpc입력지원.Clear();
                        this.bpc구매담당.Clear();
                        this.bpc영업물류.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            Control control;

            try
            {
                control = ((Control)sender);

                if (control.Name == this.bpc영업유형.Name)
                {
                    e.HelpParam.P11_ID_MENU = this.Name;
                    e.HelpParam.P41_CD_FIELD1 = "CZ_SA00023";
                    e.HelpParam.P42_CD_FIELD2 = Global.MainFrame.LoginInfo.Language;
                }
                else
                {
                    e.HelpParam.P41_CD_FIELD1 = Global.MainFrame.LoginInfo.CompanyCode;
                    e.HelpParam.P42_CD_FIELD2 = Global.MainFrame.LoginInfo.CompanyName;

                    if (control.Name == this.bpc영업담당.Name)
                        e.HelpParam.P61_CODE1 = "SA";
                    else if (control.Name == this.bpc영업물류.Name)
                        e.HelpParam.P61_CODE1 = "LO";
                    else if (control.Name == this.bpc구매담당.Name)
                        e.HelpParam.P61_CODE1 = "PU";
                    else if (control.Name == this.bpc입력지원.Name)
                        e.HelpParam.P61_CODE1 = "TP";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            BpComboBox control;

            try
            {
                control = ((BpComboBox)sender);
                if (control == null || control.CodeValues == null) return;

                if (control.CodeValues.Length > 0)
                    control.SelectedValue = control.CodeValues[0];
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx사용자ID_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.btn조회_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}