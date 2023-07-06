using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using DzHelpFormLib;
using sale;

namespace cz
{
    public partial class P_CZ_SA_RCPBILL : PageBase
    {
        #region 초기화 & 전역변수
        private P_CZ_SA_RCPBILL_BIZ _biz;
        private FreeBinding _header;
        private object[] param;


        private bool 추가모드여부
        {
            get
            {
                return this._header.JobMode == JobModeEnum.추가후수정;
            }
        }

        private bool 전표처리여부
        {
            get
            {
                return this._header.CurrentRow["ST_BILL"].Equals("Y");
            }
        }

        private bool 수금번호여부
        {
            get
            {
                return this.txt정리번호.Text != null && !(this.txt정리번호.Text == "");
            }
        }

        private bool 헤더변경여부
        {
            get
            {
                bool flag = this._header.GetChanges() != null;
                if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flexH.HasNormalRow)
                    flag = false;
                return flag;
            }
        }

        private bool 수금내용등록여부
        {
            get
            {
                return this._flexH.HasNormalRow;
            }
        }

        private bool 반제등록여부
        {
            get
            {
                return this._flexL.HasNormalRow;
            }
        }

        public enum 메세지
        {
            이미처리된전표입니다,
            등록된수금내용이존재하지않습니다,
            선수금중에선수금정리된건이있어수정이불가합니다,
            반제가등록되어있어서수정할수없습니다,
            총수금액과반제액이동일합니다,
            전표처리가되지않은수금전표입니다,
        }

        public P_CZ_SA_RCPBILL()
        {
            StartUp.Certify(this);
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._header = new FreeBinding();
        }

        public P_CZ_SA_RCPBILL(object[] param)
            :this()
        {
            this.param = param;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_RCPBILL_BIZ(this.MainFrameInterface);

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, true);

            this._flexH.SetCol("NO_RCP", "수금번호", 100);
            this._flexH.SetCol("DT_RCP", "수금일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("AM_TARGET_EX", "대상액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_BILLS_EX", "정리액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("CD_EXCH", "환종", 100, false);
            this._flexH.SetCol("CD_EXCH_NAME", "통화명", 100, false);
            this._flexH.SetCol("RT_EXCH", "기표환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_TARGET", "대상액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_BILLS", "정리액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("NO_MGMT", "관리번호", 120, false);
            this._flexH.SetCol("CD_PJT", "프로젝트", 100);
            this._flexH.SetCol("NO_DOCU", "전표번호", 100);
            this._flexH.SetCol("NO_DOLINE", "전표라인", 100);

            this._flexH.SetCodeHelpCol("NO_MGMT", HelpID.P_FI_DEPOSIT_SUB, ShowHelpEnum.HelpWindowOnly, new string[] { "NO_MGMT" }, new string[] { "NO_DEPOSIT" });
            this._flexH.SetExceptEditCol("AM_TARGET_EX", "CD_EXCH", "CD_EXCH_NAME", "RT_EXCH", "AM_TARGET", "AM_BILLS");
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_IV", "계산서번호", 100);
            this._flexL.SetCol("DT_IV", "매출일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("TP_SO", "매출형태", 80);
            this._flexL.SetCol("AM_IV", "공급가액", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("CD_EXCH_IV", "환종", 100, true);
            this._flexL.SetCol("RT_EXCH_IV", "기표환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("AM_TARGET_EX", "대상액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_RCPS_EX", "정리액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_TARGET", "대상액(원화)", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_RCPS", "정리액(원화)", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PL_LOSS", "환차손(-)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PL_PROFIT", "환차익(+)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PL", "환차손익", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NO_RCP", "참조수금번호", 100);
            this._flexL.SetCol("NO_DOCU_RCP", "선수금전표번호", 100);
            this._flexL.SetCol("NO_DOLINE_RCP", "선수금전표라인", 100);
            this._flexL.SetCol("NO_DOCU_IV", "매출전표번호", 100);
            this._flexL.SetCol("NO_DOLINE_IV", "매출전표라인", 100);

            this._flexL.SetDataMap("TP_SO", Global.MainFrame.GetComboData("N;MA_AISPOSTH;100").Tables[0], "CODE", "NAME");

            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            //this._flexL.Cols["AM_PL"].Visible = false;
            #endregion
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);
            this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this.btn전표이동.Click += new EventHandler(this.btn전표이동_Click);
            this.btn회계전표처리.Click += new EventHandler(this.btn회계전표처리_Click);
            this.btn회계전표취소.Click += new EventHandler(this.btn회계전표취소_Click);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn반제추가.Click += new EventHandler(this.btn반제추가_Click);
            this.btn반제삭제.Click += new EventHandler(this.btn반제삭제_Click);

            this._flexH.StartEdit += new RowColEventHandler(this.flex_StartEdit);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this.flex_ValidateEdit);
            this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.flexH_BeforeCodeHelp);
            this._flexH.AddRow += new EventHandler(this.btn추가_Click);

            this._flexL.StartEdit += new RowColEventHandler(this.flex_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this.flex_ValidateEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.IsSearchControl = false;
            
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.bpPanelControl3.IsNecessaryCondition = true;

            this.bpPanelControl4.IsNecessaryCondition = true;
            this.bpPanelControl5.IsNecessaryCondition = true;
            this.bpPanelControl6.IsNecessaryCondition = true;
            
            this.bpPanelControl8.IsNecessaryCondition = true;

            this.bpPanelControl10.IsNecessaryCondition = true;

            this.bpPanelControl13.IsNecessaryCondition = true;
            this.bpPanelControl14.IsNecessaryCondition = true;
            
            this.oneGrid1.InitCustomLayout();

            this.dtp정리일.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp정리일.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp정리일.Text = this.MainFrameInterface.GetStringToday;

            this.cur정리액원화.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);
            this.cur반제액원화.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);

            DataSet comboData = this.GetComboData("N;PU_C000016", "N;SA_B000002", "S;SA_B000012", "N;MA_B000043", "N;MA_AISPOSTH;300", "N;SA_B000028", "N;SA_B000010", "N;MA_B000005", "N;FI_J000002");

            this.cbo수금형태.DataSource = comboData.Tables[4];
            this.cbo수금형태.ValueMember = "CODE";
            this.cbo수금형태.DisplayMember = "NAME";

            this.cbo거래구분.DataSource = comboData.Tables[0];
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo거래구분.DisplayMember = "NAME";

            this.cbo처리여부.DataSource = comboData.Tables[5];
            this.cbo처리여부.ValueMember = "CODE";
            this.cbo처리여부.DisplayMember = "NAME";
            this.cbo처리여부.SelectedValue = "N";

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CODE", typeof(string));
            dataTable.Columns.Add("NAME", typeof(string));
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "N";
            row1["NAME"] = this.DD("선수금정리");
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "R";
            row2["NAME"] = this.DD("선수금반환");
            dataTable.Rows.Add(row2);

            this.cbo정리구분.DataSource = dataTable;
            this.cbo정리구분.ValueMember = "CODE";
            this.cbo정리구분.DisplayMember = "NAME";

            this.cbo전표유형.DataSource = comboData.Tables[8];
            this.cbo전표유형.ValueMember = "CODE";
            this.cbo전표유형.DisplayMember = "NAME";
            
            DataSet dataSet = this._biz.Search("");
            
            this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
            this._header.ClearAndNewRow();
            this.cbo전표유형.SelectedValue = "34";
            this._header.CurrentRow["CD_DOCU"] = "34";

            if (this.param != null)
            {
                this.ctx매출처.CodeValue = D.GetString(this.param[0]);
                this.ctx매출처.CodeName = D.GetString(this.param[1]);
                this._header.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                this._header.CurrentRow["NM_PARTNER"] = this.ctx매출처.CodeName;

                this.ctx수금처.CodeValue = D.GetString(this.param[0]);
                this.ctx수금처.CodeName = D.GetString(this.param[1]);
                this._header.CurrentRow["BILL_PARTNER"] = this.ctx수금처.CodeValue;
                this._header.CurrentRow["NM_BILL_PARTNER"] = this.ctx수금처.CodeName;

                this.cbo거래구분.SelectedValue = D.GetString(this.param[2]);
                this._header.CurrentRow["TP_BUSI"] = this.cbo거래구분.SelectedValue;

                this.dtp정리일.Text = D.GetString(this.param[3]);
                this._header.CurrentRow["DT_BILLS"] = this.dtp정리일.Text;

                if (D.GetString(this.cbo거래구분.SelectedValue) == "001")
                {
                    this.cbo수금형태.SelectedValue = "002";
                    this._header.CurrentRow["CD_TP"] = "002";
                }
                else
                {
                    this.cbo수금형태.SelectedValue = "001";
                    this._header.CurrentRow["CD_TP"] = "001";
                }
            }
            
            this._flexH.Binding = dataSet.Tables[1];
            this._flexL.Binding = dataSet.Tables[2];
        }
        #endregion

        #region 메인버튼
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                P_SA_RCPBILL_PRE_SCH pSaRcpbillPreSch = new P_SA_RCPBILL_PRE_SCH(this.MainFrameInterface, new object[] { this.ctx매출처.CodeValue,
                                                                                                                         this.ctx수금처.CodeValue,
                                                                                                                         this.ctx영업그룹.CodeValue,
                                                                                                                         this.ctx담당자.CodeValue,
                                                                                                                         this.ctx매출처.CodeName,
                                                                                                                         this.ctx수금처.CodeName,
                                                                                                                         this.ctx영업그룹.CodeName,
                                                                                                                         this.ctx담당자.CodeName });
                if (pSaRcpbillPreSch.ShowDialog() == DialogResult.OK)
                {
                    DataSet dataSet = this._biz.Search(((DataRowView)((IHelpWindow)pSaRcpbillPreSch).ReturnValues[0]).Row["NO_BILLS"].ToString());
                    
                    this._header.SetDataTable(dataSet.Tables[0]);

                    this._flexH.Binding = dataSet.Tables[1];
                    this._flexL.Binding = dataSet.Tables[2];

                    this.dtp정리일.Enabled = false;
                    this.ctx매출처.Enabled = false;
                    this.ctx수금처.Enabled = false;
                    this.ctx영업그룹.Enabled = false;
                    this.ctx담당자.Enabled = false;
                    this.cbo정리구분.Enabled = false;
                    this.cbo전표유형.Enabled = false;
                    this.cbo수금형태.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.ControlEnabledDisable();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            return base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search);
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                this._flexH.DataTable.Rows.Clear();
                this._flexH.AcceptChanges();
                this._flexL.DataTable.Rows.Clear();
                this._flexL.AcceptChanges();

                this._header.ClearAndNewRow();
                
                this.dtp정리일.Enabled = true;
                this.cbo수금형태.Enabled = true;
                this.cbo거래구분.Enabled = true;
                this.ctx매출처.Enabled = true;
                this.ctx수금처.Enabled = true;
                this.ctx영업그룹.Enabled = true;
                this.ctx담당자.Enabled = true;
                this.cbo정리구분.Enabled = true;
                this.cbo전표유형.Enabled = true;

                this.cbo전표유형.SelectedValue = "34";
                this._header.CurrentRow["CD_DOCU"] = "34";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            if (this.전표처리여부)
            {
                this.ShowMessage(메세지.이미처리된전표입니다);
                return false;
            }

            return this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                this._biz.Delete(this.txt정리번호.Text);
                
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                
                this.OnToolBarAddButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (this.전표처리여부)
            {
                this.ShowMessage("이미 전표처리된 건입니다.");
                return false;
            }

            if (!this._flexH.HasNormalRow)
            {
                this.ShowMessage("라인없이 저장할 수 없습니다");
                return false;
            }

            if (!this.HeaderCheck()) return false;

            foreach (DataRow dataRow in (InternalDataCollectionBase)this._flexH.DataTable.Rows)
            {
                if (dataRow.RowState != DataRowState.Deleted && !this.Check())
                    return false;
            }

            for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
            {
                if (D.GetDecimal(this._flexL[@fixed, "AM_TARGET_EX"]) == D.GetDecimal(this._flexL[@fixed, "AM_RCPS_EX"]) && D.GetDecimal(this._flexL[@fixed, "AM_TARGET"]) != D.GetDecimal(this._flexL[@fixed, "AM_RCPS"]) + D.GetDecimal(this._flexL[@fixed, "AM_PL"]))
                {
                    this.ShowMessage("대상액(원화)와 정리액(원화) + 환차손익이 일치해야 수정이 가능합니다.");
                    return false;
                }
            }

            this.수금합계();
            
            if (!(this._header.CurrentRow["TP_BILLS"].ToString() == "N") || !(this.cur정리액원화.DecimalValue != this.cur반제액원화.DecimalValue))
                return true;

            this.ShowMessage("선수금정리: 입력한 정리액이 일치해야만 저장이 가능합니다.");
            return false;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            this.영업그룹설정();

            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            if (this.추가모드여부)
            {
                string str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "SA", "10", this.dtp정리일.Text.Substring(0, 6).Trim());
                this._header.CurrentRow["NO_BILLS"] = str;
                
                for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                    this._flexH[@fixed, "NO_BILLS"] = str;
                
                for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                    this._flexL[@fixed, "NO_BILLS"] = str;
                
                this.txt정리번호.Text = str;
            }

            this.수금합계();
            
            DataTable changes1 = this._header.GetChanges();
            DataTable changes2 = this._flexH.GetChanges();
            DataTable changes3 = this._flexL.GetChanges();
            
            if (changes1 == null && changes2 == null && changes3 == null)
                return true;

            if (!this._biz.Save(this.dtp정리일.Text, this.ctx수금처.CodeValue, changes1, changes2, changes3))
                return false;
            
            this._header.AcceptChanges();
            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();
            return true;
        }

        private void 영업그룹설정()
        {
            DataTable 영업그룹;

            try
            {
                if (string.IsNullOrEmpty(this.ctx영업그룹.CodeValue) && string.IsNullOrEmpty(this.ctx영업그룹.CodeName))
                {
                    if (this._flexL.DataTable.Rows.Count > 0)
                        영업그룹 = this._biz.영업그룹(this._flexL.DataTable.Rows[0]["NO_IV"].ToString());
                    else
                        영업그룹 = this._biz.영업그룹(string.Empty);

                    if (영업그룹.Rows[0] != null)
                    {
                        this.ctx영업그룹.CodeValue = 영업그룹.Rows[0]["CD_SALEGRP"].ToString();
                        this.ctx영업그룹.CodeName = 영업그룹.Rows[0]["NM_SALEGRP"].ToString();
                        this._header.CurrentRow["CD_BILLTGRP"] = this.ctx영업그룹.CodeValue;
                        this._header.CurrentRow["NM_SALEGRP"] = this.ctx영업그룹.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn전표이동_Click(object sender, EventArgs e)
        {
            if (!(D.GetString(this._header.CurrentRow["NO_DOCU"]) != "")) return;

            this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._header.CurrentRow["NO_DOCU"]),
                                                                                                                             "1",
                                                                                                                             D.GetString(this._header.CurrentRow["CD_PC"]),
                                                                                                                             Global.MainFrame.LoginInfo.CompanyCode });
        }

        private void btn회계전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.전표처리여부)
                {
                    this.ShowMessage("이미 전표처리된 건입니다.");
                }
                else
                {
                    if (!this.수금번호여부) return;

                    this._biz.미결전표처리(this.txt정리번호.Text);
                    this._header.AcceptChanges();
                    this._flexH.AcceptChanges();

                    DataTable dataTable;
                    dataTable = Global.MainFrame.FillDataTable(@"SELECT ISNULL((SELECT TOP 1 FD.NO_DOCU
                                                                                FROM FI_DOCU FD WITH(NOLOCK)
                                                                                WHERE FD.CD_COMPANY = A.CD_COMPANY
                                                                                AND FD.NO_MDOCU = A.NO_BILLS), '') NO_DOCU, 
                                                                        CC.CD_PC AS CD_PC
                                                                 FROM SA_BILLSH A WITH(NOLOCK)
                                                                 LEFT JOIN MA_SALEGRP E WITH(NOLOCK) ON E.CD_COMPANY = A.CD_COMPANY AND E.CD_SALEGRP = A.CD_BILLTGRP
                                                                 LEFT JOIN MA_CC CC WITH(NOLOCK) ON E.CD_COMPANY = CC.CD_COMPANY AND E.CD_CC = CC.CD_CC
                                                                 WHERE A.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                "AND A.NO_BILLS   = '" + this.txt정리번호.Text + "'");
                    
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows[0]["NO_DOCU"] != DBNull.Value && dataTable.Rows[0]["NO_DOCU"].ToString().Trim() != string.Empty)
                        {
                            #region 전표승인
                            if (Global.MainFrame.LoginInfo.StDocuApp == "2" || Global.MainFrame.LoginInfo.StDocuApp == "3")
                            {
                                object[] obj = new object[1];
                                DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  dataTable.Rows[0]["CD_PC"].ToString(),
                                                                                                  "FI04",
                                                                                                  this.dtp정리일.Text }, out obj);

                                decimal 회계번호 = D.GetDecimal(obj[0]);

                                DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { dataTable.Rows[0]["NO_DOCU"].ToString(),
                                                                                                dataTable.Rows[0]["CD_PC"].ToString(),
                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                this.dtp정리일.Text,
                                                                                                회계번호,
                                                                                                "2",
                                                                                                Global.MainFrame.LoginInfo.UserID,
                                                                                                Global.MainFrame.LoginInfo.UserID });
                            }
                            #endregion

                            this.cbo처리여부.SelectedValue = "Y";
                            this._header.CurrentRow["ST_BILL"] = "Y";
                            this.txt처리여부.Text = dataTable.Rows[0]["NO_DOCU"].ToString();
                            this._header.CurrentRow["NO_DOCU"] = dataTable.Rows[0]["NO_DOCU"].ToString();
                            this._header.CurrentRow["CD_PC"] = dataTable.Rows[0]["CD_PC"].ToString();
                            this._header.AcceptChanges();
                            this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn회계전표처리.Text });
                            this.ControlEnabledDisable();
                        }
                        else
                        {
                            this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                        }
                    }
                    else
                    {
                        this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.cbo처리여부.SelectedValue.Equals("Y"))
                {
                    this.ShowMessage(메세지.전표처리가되지않은수금전표입니다);
                }
                else
                {
                    this._biz.미결전표취소(this.txt정리번호.Text);
                    this.cbo처리여부.SelectedValue = "N";
                    this._header.CurrentRow["ST_BILL"] = "N";
                    this._header.AcceptChanges();
                    this._flexH.AcceptChanges();
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn회계전표취소.Text });
                    this.ControlEnabledDisable();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck()) return;

                if (this.dtp정리일.Text == "" || this.dtp정리일.Text == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl정리일.Text });
                }
                else if (this.전표처리여부)
                {
                    this.ShowMessage("이미 전표처리된 건입니다.");
                }
                else
                {
                    this.dtp정리일.Enabled = false;
                    this.cbo수금형태.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.ctx매출처.Enabled = false;
                    this.ctx수금처.Enabled = false;
                    this.ctx영업그룹.Enabled = false;
                    this.ctx담당자.Enabled = false;
                    this.dtp정리일.Enabled = false;
                    this.cbo정리구분.Enabled = false;
                    this.cbo전표유형.Enabled = false;

                    P_CZ_SA_BILL_SCH_PRE subDialog = new P_CZ_SA_BILL_SCH_PRE(new object[] { this.ctx매출처.CodeValue,
                                                                                             this.ctx수금처.CodeValue,
                                                                                             this.ctx영업그룹.CodeValue,
                                                                                             string.Empty, //담당자 코드 
                                                                                             this.ctx매출처.CodeName,
                                                                                             this.ctx수금처.CodeName,
                                                                                             this.ctx영업그룹.CodeName,
                                                                                             string.Empty, //담당자 명
                                                                                             D.GetString(this.cbo거래구분.SelectedValue),
                                                                                             D.GetString(this.cbo수금형태.SelectedValue) });

                    if (subDialog.ShowDialog() != DialogResult.OK) return;

                    this._flexH.Redraw = false;

                    foreach (DataRow dataRow in subDialog.resultDt.Rows)
                    {
                        DataRow row = this._flexH.DataTable.NewRow();

                        row["NO_BILLS"] = this.txt정리번호.Text;
                        row["NO_RCP"] = D.GetString(dataRow["NO_RCP"]);
                        row["DT_RCP"] = D.GetString(dataRow["DT_RCP"]);
                        row["CD_EXCH"] = D.GetString(dataRow["CD_EXCH"]);
                        row["CD_EXCH_NAME"] = D.GetString(dataRow["CD_EXCH_NAME"]);
                        row["RT_EXCH"] = Convert.ToDecimal(dataRow["RT_EXCH"]);
                        row["AM_TARGET_EX"] = this.외화계산(D.GetDecimal(dataRow["AM_REMAIN_EX"]));
                        row["AM_BILLS_EX"] = this.외화계산(D.GetDecimal(dataRow["AM_REMAIN_EX"]));
                        row["AM_TARGET"] = this.원화계산(D.GetDecimal(dataRow["AM_REMAIN"]));
                        row["AM_BILLS"] = this.원화계산(D.GetDecimal(dataRow["AM_REMAIN"]));
                        row["FG_MAP"] = D.GetString(dataRow["FG_MAP"]);
                        row["CD_PJT"] = D.GetString(dataRow["CD_PJT"]);
                        row["NO_DOCU"] = D.GetString(dataRow["NO_DOCU"]);
                        row["NO_DOLINE"] = D.GetString(dataRow["NO_DOLINE"]);

                        this._flexH.DataTable.Rows.Add(row);
                    }

                    for (int i = 0; i <= this._flexH.DataTable.Rows.Count - 1; i++)
                    {
                        this._flexH.DataTable.Rows[i]["NO_LINE"] = i + 1;
                    }

                    this._flexH.IsDataChanged = true;
                    this._flexH.Row = this._flexH.Rows.Count - 1;

                    this.수금합계();
                    this.ToolBarSaveButtonEnabled = true;
                    this.ShowStatusBarMessage(2);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
            }
        }

        private void btn반제추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._header.CurrentRow["TP_BILLS"].ToString() == "R")
                {
                    this.ShowMessage("CZ_선수금반환은 계산서반제 대상이 아닙니다.");
                }
                else if (this.전표처리여부)
                {
                    this.ShowMessage("이미 전표처리된 건입니다.");
                }
                else if (!this.수금내용등록여부)
                {
                    this.ShowMessage(메세지.등록된수금내용이존재하지않습니다);
                }
                else if (this._flexH["NO_RCP"].ToString().Length == 0)
                {
                    this.ShowMessage("추가하신 수금내역의 정리할 수금번호를 지정해주십시요");
                }
                else
                {
                    if (!this.HeaderCheck()) return;

                    DataTable 매출반제 = this._biz.매출반제();

                    for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                    {
                        if (this._flexL.RowState(@fixed) == DataRowState.Added)
                        {
                            DataRow row = 매출반제.NewRow();

                            row["NO_DOCU"] = D.GetString(this._flexL[@fixed, "NO_DOCU_IV"]);
                            row["NO_DOLINE"] = D.GetString(this._flexL[@fixed, "NO_DOLINE_IV"]);
                            row["AM_BAN_EX"] = D.GetDecimal(this._flexL[@fixed, "AM_RCPS_EX"]);
                            row["AM_BAN"] = (D.GetDecimal(this._flexL[@fixed, "AM_RCPS"]) + D.GetDecimal(this._flexL[@fixed, "AM_PL"]));
                            
                            매출반제.Rows.Add(row);
                        }
                    }

                    P_CZ_SA_RCPBILL_SUB pSaBillSub = new P_CZ_SA_RCPBILL_SUB(매출반제, D.GetString(this._flexH["DT_RCP"]), D.GetString(this.cbo수금형태.SelectedValue));

                    pSaBillSub.거래구분코드 = this.cbo거래구분.SelectedValue == null ? string.Empty : this.cbo거래구분.SelectedValue.ToString();
                    pSaBillSub.거래구분명 = this.cbo거래구분.Text;
                    pSaBillSub.매출처코드 = this.ctx매출처.CodeValue;
                    pSaBillSub.매출처명 = this.ctx매출처.CodeName;
                    pSaBillSub.수금처코드 = this.ctx수금처.CodeValue;
                    pSaBillSub.수금처명 = this.ctx수금처.CodeName;
                    pSaBillSub.총수금원화 = Convert.ToDecimal(this._flexH["AM_BILLS"]);
                    pSaBillSub.반제액원화 = 0;
                    pSaBillSub.총수금 = Convert.ToDecimal(this._flexH["AM_BILLS_EX"]);
                    pSaBillSub.반제액 = 0;
                    pSaBillSub.통화코드 = this._flexH["CD_EXCH"].ToString();
                    pSaBillSub.통화명 = this._flexH["CD_EXCH_NAME"].ToString();
                    pSaBillSub.환율 = Convert.ToDecimal(this._flexH["RT_EXCH"]);
                    pSaBillSub.Set매출구분 = D.GetString(this._flexH["FG_MAP"]);

                    if (pSaBillSub.ShowDialog() != DialogResult.OK) return;
                    
                    DataTable getDataTable = pSaBillSub.GetDataTable;
                    this._flexL.Redraw = false;
                    
                    foreach (DataRow dataRow in (InternalDataCollectionBase)getDataTable.Rows)
                    {
                        DataRow row = this._flexL.DataTable.NewRow();

                        row["NO_BILLS"] = this.txt정리번호.Text;
                        row["NO_IV"] = dataRow["NO_IV"];
                        row["DT_IV"] = dataRow["DT_PROCESS"];
                        row["TP_SO"] = dataRow["TP_IV"];
                        row["AM_IV"] = dataRow["AM_DOCU"];
                        row["GUBUN"] = "0";
                        row["CD_EXCH_IV"] = dataRow["CD_EXCH"];
                        row["RT_EXCH_IV"] = dataRow["RT_EXCH"];
                        row["AM_TARGET_EX"] = dataRow["AM_RCP_JAN_EX"];
                        row["AM_TARGET"] = dataRow["AM_RCP_JAN"];
                        row["AM_RCPS_EX"] = dataRow["AM_RCP_EX"];
                        row["AM_RCPS"] = dataRow["AM_RCP"];
                        row["AM_PL"] = dataRow["AM_PL"];
                        row["AM_PL_LOSS"] = dataRow["AM_PL_LOSS"];
                        row["AM_PL_PROFIT"] = dataRow["AM_PL_PROFIT"];
                        row["NO_RCP"] = this._flexH["NO_RCP"].ToString();
                        row["NO_DOCU_RCP"] = this._flexH["NO_DOCU"];
                        row["NO_DOLINE_RCP"] = this._flexH["NO_DOLINE"];
                        row["NO_DOCU_IV"] = dataRow["NO_DOCU"];
                        row["NO_DOLINE_IV"] = dataRow["NO_DOLINE"];

                        this._flexL.DataTable.Rows.Add(row);
                    }

                    this._flexL.Redraw = true;
                    this._flexL.IsDataChanged = true;
                    this._flexL.SumRefresh();
                    this._flexL.Row = this._flexL.Rows.Count - 1;
                    
                    this.ToolBarSaveButtonEnabled = true;
                    
                    this.수금합계();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else if (this.반제등록여부 && this._flexH["NO_RCP"].ToString().Length != 0)
                {
                    this.ShowMessage("하단에 반제가등록되어있어서수정할수없습니다");
                }
                else
                {
                    this._flexH.Rows.Remove(this._flexH.Row);
                    
                    this.수금합계();

                    if (!this._flexH.HasNormalRow)
                    {
                        this.dtp정리일.Enabled = true;
                        this.cbo수금형태.Enabled = true;
                        this.cbo거래구분.Enabled = true;
                        this.ctx매출처.Enabled = true;
                        this.ctx수금처.Enabled = true;
                        this.ctx영업그룹.Enabled = true;
                        this.ctx담당자.Enabled = true;
                        this.cbo정리구분.Enabled = true;
                        this.cbo전표유형.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn반제삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else
                {
                    this._flexL.Rows.Remove(this._flexL.Row);
                    this.수금합계();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (((C1FlexGridBase)sender).Cols[e.Col].Name == "NO_RCP") e.Cancel = true;
                
                if (!this.전표처리여부) return;

                this.ShowMessage("이미 전표처리된 건입니다.");
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;

                if (str.ToUpper() == editData.ToUpper()) return;
                
                string name = ((C1FlexGridBase)sender).Cols[e.Col].Name;

                if (name == "AM_BILLS_EX" && Convert.ToDecimal(editData) > Convert.ToDecimal(this._flexH["AM_TARGET_EX"]))
                {
                    this.ShowMessage("정리액이 대상금액을 초과할수없습니다.");

                    if (this._flexH.Editor != null)
                        this._flexH.Editor.Text = str;
                    
                    this._flexH["AM_BILLS_EX"] = str;
                }
                else if (name == "AM_BILLS_EX")
                {
                    this._flexH[e.Row, "AM_BILLS"] = Decimal.Round(this._flexH.CDecimal(editData) * this._flexH.CDecimal(this._flexH[e.Row, "RT_EXCH"]), MidpointRounding.AwayFromZero);

                    this.수금합계();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void flexH_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID != HelpID.P_MA_PARTNER_SUB) return;

                e.Parameter.P61_CODE1 = "002";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 이벤트
        protected override bool IsChanged()
        {
            if (base.IsChanged()) return true;

            return this.헤더변경여부;
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.전표처리여부)
                {
                    this.ToolBarDeleteButtonEnabled = false;
                    this.ToolBarSaveButtonEnabled = false;
                }
                else
                {
                    this.ToolBarDeleteButtonEnabled = true;
                    if (this.IsChanged())
                        this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (this.전표처리여부)
                    this._header.SetControlEnabled(false);
                else
                    this._header.SetControlEnabled(true);

                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    this.dtp정리일.Enabled = true;
                    this.cbo거래구분.Enabled = true;
                    this.ctx매출처.Enabled = true;
                    this.ctx수금처.Enabled = true;
                }
                else
                {
                    this.dtp정리일.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.ctx매출처.Enabled = false;
                    this.ctx수금처.Enabled = false;
                }

                this.ControlEnabledDisable();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if(((Control)sender).Name == this.ctx매출처.Name)
                {
                    this.ctx수금처.CodeValue = this.ctx매출처.CodeValue;
                    this.ctx수금처.CodeName = this.ctx매출처.CodeName;
                    this._header.CurrentRow["BILL_PARTNER"] = this.ctx매출처.CodeValue;
                    this._header.CurrentRow["NM_BILL_PARTNER"] = this.ctx매출처.CodeName;
                }
                
                if (this.dtp정리일.Text != string.Empty && this.dtp정리일.IsValidated &&
                    (string.IsNullOrEmpty(this.cbo수금형태.SelectedValue.ToString()) == false) &&
                    (string.IsNullOrEmpty(this.cbo거래구분.SelectedValue.ToString()) == false) && 
                    (string.IsNullOrEmpty(this.ctx매출처.CodeValue) == false) &&
                    (string.IsNullOrEmpty(this.ctx담당자.CodeValue) == false))
                {
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                    this.btn반제추가.Enabled = true;
                    this.btn반제삭제.Enabled = true;
                }
                else
                {
                    this.btn추가.Enabled = false;
                    this.btn삭제.Enabled = false;
                    this.btn반제추가.Enabled = false;
                    this.btn반제삭제.Enabled = false;
                }

                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타
        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.이미처리된전표입니다:
                    return this.ShowMessage("SA_M000124");
                case 메세지.등록된수금내용이존재하지않습니다:
                    return this.ShowMessage("SA_M000128");
                case 메세지.선수금중에선수금정리된건이있어수정이불가합니다:
                    return this.ShowMessage("SA_M000122");
                case 메세지.반제가등록되어있어서수정할수없습니다:
                    return this.ShowMessage("SA_M000130");
                case 메세지.총수금액과반제액이동일합니다:
                    return this.ShowMessage("SA_M000129");
                case 메세지.전표처리가되지않은수금전표입니다:
                    return this.ShowMessage("SA_M000125");
                default:
                    return DialogResult.None;
            }
        }

        private void ControlEnabledDisable()
        {
            this.cbo처리여부.Enabled = false;
            bool flag = !this.전표처리여부;

            if (this.전표처리여부 || this.반제등록여부)
            {
                this.btn추가.Enabled = false;
                this.btn삭제.Enabled = false;
                this.btn반제추가.Enabled = false;
                this.btn반제삭제.Enabled = false;
            }
            else
            {
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;
                this.btn반제추가.Enabled = true;
                this.btn반제삭제.Enabled = true;
            }

            if (this._header.JobMode == JobModeEnum.추가후수정)
            {
                this.btn회계전표처리.Enabled = false;
                this.btn회계전표취소.Enabled = false;
                this.btn전표이동.Enabled = false;
            }
            else
            {
                this.btn회계전표처리.Enabled = flag;
                this.btn회계전표취소.Enabled = !flag;
                this.btn전표이동.Enabled = !flag;
            }
        }

        private bool HeaderCheck()
        {
            if (this.dtp정리일.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl정리일.Text });
                this.dtp정리일.Focus();
                return false;
            }

            if (this.cbo수금형태.SelectedValue == null || this.cbo수금형태.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금형태.Text });
                this.cbo수금형태.Focus();
                return false;
            }

            if (this.cbo거래구분.SelectedValue == null || this.cbo거래구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl거래구분.Text });
                this.cbo거래구분.Focus();
                return false;
            }

            if (this.ctx매출처.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl매출처.Text });
                this.ctx매출처.Focus();
                return false;
            }

            if (this.ctx수금처.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금처.Text });
                this.ctx수금처.Focus();
                return false;
            }

            if (this.ctx담당자.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
                this.ctx담당자.Focus();
                return false;
            }

            if (this.dtp정리일.IsValidated)
                return true;

            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
            this.dtp정리일.Focus();

            return false;
        }

        private bool Check()
        {
            for (int row = 0; row < this._flexH.Rows.Count; ++row)
            {
                if (this._flexH[row, "NO_RCP"].ToString().Length == 0)
                {
                    this.ShowMessage("추가하신 수금내역의 정리할 수금번호를 지정해주십시요");
                    this._flexH.Select(row, "NO_RCP");
                    this._flexH.Focus();
                    return false;
                }
            }
            return true;
        }

        private void 수금합계()
        {
            decimal num1 = 0;
            decimal num2 = 0;

            for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                num1 += Convert.ToDecimal(this._flexH[@fixed, "AM_BILLS"]);

            for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                num2 += Convert.ToDecimal(this._flexL[@fixed, "AM_RCPS"]);

            if (this.cbo처리여부.SelectedValue.Equals("N"))
            {
                DataRow currentRow = this._header.CurrentRow;
                currentRow["AM_BILLS"] = num1;
                currentRow["AM_IV"] = num2;
            }

            this.cur정리액원화.DecimalValue = num1;
            this.cur반제액원화.DecimalValue = num2;
        }

        private decimal 원화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    result = Decimal.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private decimal 외화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }
        #endregion
    }
}
