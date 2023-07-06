using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PU.Common;
using DzHelpFormLib;
using pur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_PO_REG2 : PageBase
    {
        #region 전역변수 & 생성자
        private P_CZ_PU_PO_REG2_BIZ _biz = new P_CZ_PU_PO_REG2_BIZ();
        private FreeBinding _header = null;
        private CDT_PU_RCVH cPU_RCVH = new CDT_PU_RCVH();
        private CDT_PU_RCV cPU_RCVL = new CDT_PU_RCV();
        private string str복사구분 = string.Empty;
        private string _전용설정 = "000";
        private string m_sEnv = "N";
        private string m_sEnv_CC = "000";
        private string m_sEnv_CC_Menu = "000";
        private string m_sEnv_FG_TAX = "000";
        private string m_sEnv_Nego = BASIC.GetMAEXC("발주등록(공장)-업체별프로세스선택");
        private string _m_partner_use = "000";
        private string _m_tppo_change = "000";
        private string _구매예산CHK설정FI = "000";
        private string _YN_CdBizplan = "0";
        private string _m_dt = "000";
        private string _m_Company_only = "000";
        private string _반품발주 = BASIC.GetMAEXC("반품발주사용여부");
        private DataTable dt공장 = null;
        private string sPUSU = BASIC.GetMAEXC("구매발주-외주유무");
        private string sFG_TAXcheck = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000001");
        private bool bStandard = false;
        private bool _YN_REBATE = false;
        private Decimal d_SEQ_PROJECT = 0;
        private string s_CD_PJT_ITEM = string.Empty;
        private string s_NM_PJT_ITEM = string.Empty;
        private string s_PJT_ITEM_STND = string.Empty;
        private string _지급관리통제설정 = "N";
        private string _지급예정일통제설정 = "000";
        private string s_vat_fictitious = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");
        private string s_PTR_SUB = BASIC.GetMAEXC("거래처부가정보-발주정보매핑여부");
        private P_PU_OPTION_INFO_SUB _infosub_dlg = new P_PU_OPTION_INFO_SUB(string.Empty, string.Empty, true);
        private string _m_pjtbom_rq_mng = "000";
        private bool b단가권한 = true;
        private bool b금액권한 = true;
        private string str발주번호;
        private string _ComfirmState;
        private string strSOURCE;
        private Decimal dNO_LINE;

        private bool 헤더변경여부
        {
            get
            {
                bool flag = this._header.GetChanges() != null;

                if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flex발주품목.HasNormalRow)
                    flag = false;
                
                return flag;
            }
        }

        private bool 추가모드여부
        {
            get
            {
                return this._header.JobMode == JobModeEnum.추가후수정;
            }
        }

        private bool 차수여부
        {
            get
            {
                if (D.GetDecimal(this._header.CurrentRow["NO_HST"]) != 0 && this._flex발주품목.HasNormalRow)
                    return false;

                if (BASIC.GetMAEXC("전자결재-ERP수정여부") == "100")
                {
                    int fiGwdocu = this._biz.GetFI_GWDOCU(this.txt발주번호.Text);

                    if (fiGwdocu != 2 && fiGwdocu != 999)
                        return false;
                }

                return true;
            }
        }

        private bool 전자결재여부
        {
            get
            {
                if (BASIC.GetMAEXC("전자결재-ERP수정여부") == "100")
                {
                    int fiGwdocu = this._biz.GetFI_GWDOCU(this.txt발주번호.Text);

                    if (fiGwdocu != 2 && fiGwdocu != 999)
                        return false;
                }

                return true;
            }
        }

        private bool 자동입고여부체크
        {
            get
            {
                if (this._header.CurrentRow["YN_REQ"].ToString() == "N" && this._header.CurrentRow["FG_TRANS"].ToString() != "001")
                {
                    this.ShowMessage(메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다, string.Empty);
                    return false;
                }

                if (this._header.CurrentRow["YN_AUTORCV"].ToString() == "Y" && this._header.CurrentRow["YN_REQ"].ToString() == "N")
                {
                    if (!this._flex발주품목.HasNormalRow)
                        return false;

                    if (this._flex발주품목.DataTable.Select("Len(CD_SL) = 0").Length > 0)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("창고") });
                        return false;
                    }
                }

                return true;
            }
        }

        private Decimal 최대차수
        {
            get
            {
                Decimal num = 0;

                for (int index = this._flex발주품목.Rows.Fixed; index < this._flex발주품목.Rows.Count; ++index)
                {
                    if (this._flex발주품목.CDecimal(this._flex발주품목[index, "NO_LINE"]) > num)
                        num = this._flex발주품목.CDecimal(this._flex발주품목[index, "NO_LINE"]);
                }

                return num;
            }
        }

        public P_CZ_PU_PO_REG2()
            : this(string.Empty)
        {
        }

        public P_CZ_PU_PO_REG2(string str발주번호)
            : this(str발주번호, 0, string.Empty)
        {
        }

        public P_CZ_PU_PO_REG2(string str발주번호, Decimal dNO_LINE, string strSOURCE)
        {
            try
            {
				StartUp.Certify(this);
				this.InitializeComponent();
                this.MainGrids = new FlexGrid[] { this._flex발주품목 };
                this.str발주번호 = str발주번호;
                this.dNO_LINE = dNO_LINE;
                this.strSOURCE = strSOURCE;
                
                this._header = new FreeBinding();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PU_PO_REG2(PageBaseConst.CallType pageCallType, string idMemo)
            : this(string.Empty)
        {
            this.str발주번호 = this._biz.GetNoPo(idMemo);
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this.MA_EXC_SETTING();
            
            if (this._m_Company_only == "003")
                this.MA_Pjt_Setting();
            
            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.InitControl();
            this.원그리드적용하기();
            
            switch (this.strSOURCE)
            {
                case "PU_POL":
                    DataSet dataSet1 = this._biz.Search("@#$%");
                    this._header.SetBinding(dataSet1.Tables[0], this.tce발주등록헤더);
                    this._header.ClearAndNewRow();
                    
                    this._flex발주품목.Binding = dataSet1.Tables[1];
                    this.조회(this.str발주번호, "OK");
                    
                    if (!this._flex발주품목.HasNormalRow)
                        break;
                    
                    this._flex발주품목.Row = this._flex발주품목.FindRow(this.dNO_LINE, this._flex발주품목.Rows.Fixed, this._flex발주품목.Cols["NO_LINE"].Index, true);
                    break;
                default:
                    if (!string.IsNullOrEmpty(this.str발주번호))
                    {
                        this.Reload(this.str발주번호);
                    }
                    else
                    {
                        DataSet dataSet2 = this._biz.Search("@#$%");
                        this._header.SetBinding(dataSet2.Tables[0], this.tce발주등록헤더);
                        this._header.ClearAndNewRow();
                        this._flex발주품목.Binding = dataSet2.Tables[1];
                        this.dtp발주일자.Focus();

                        this.cbo통화_SelectionChangeCommitted(null, null);

                        this.Setting_pu_poh_sub();
                        
                        if (D.GetString(dataSet2.Tables[0].Rows[0]["NO_PO"]) != string.Empty)
                        {
                            this._header.AcceptChanges();
                            this._flex발주품목.AcceptChanges();
                        }
                    }

                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;

                    this.ctx부가세사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
                    this.ctx부가세사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;

                    this.cbo지급구분.SelectedValue = "001";

                    if (this._header.CurrentRow["FG_TRANS"].ToString() == "001")
                    {
                        this.cbo전표유형.SelectedValue = "45";
                        this.cbo매입형태.SelectedValue = "001";
                    }
                    else
                    {
                        this.cbo전표유형.SelectedValue = "46";
                        this.cbo매입형태.SelectedValue = "002";
                    }
                    break;
            }
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);
            this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this._flex발주품목.AddRow += new EventHandler(this.btn추가_Click);
            this._flex발주품목.StartEdit += new RowColEventHandler(this._flex발주품목_StartEdit);
            this._flex발주품목.ValidateEdit += new ValidateEditEventHandler(this._flex발주품목_ValidateEdit);
            this._flex발주품목.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex발주품목_BeforeCodeHelp);
            this._flex발주품목.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex발주품목_AfterCodeHelp);
            this._flex발주품목.DoubleClick += new EventHandler(this._flex발주품목_DoubleClick);
            this._flex발주품목.CellContentChanged += new CellContentEventHandler(this._flex발주품목_CellContentChanged);

            this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx매입처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx담당자.CodeChanged += new EventHandler(this.Control_CodeChanged);
            this.ctx구매그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx구매그룹.CodeChanged += new EventHandler(this.Control_CodeChanged);
            this.ctx발주유형.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx발주유형.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.btn매입형태적용.Click += new EventHandler(this.btn매입형태적용_Click);
            this.btn창고적용.Click += new EventHandler(this.btn창고적용_Click);
            this.btn프로젝트적용.Click += new EventHandler(this.btn프로젝트적용_Click);
            this.btn납기일적용.Click += new EventHandler(this.btn납기일적용_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn통화명.Click += new EventHandler(this.btn통화명_Click);

            this.cur공급가액.Validated += new EventHandler(this.cur공급가액_Validated);
            this.cur부가세액.Validated += new EventHandler(this.cur부가세액_Validated);
            this.cur통화명.Validated += new EventHandler(this.cur통화_Validated);

            this.cbo통화.SelectionChangeCommitted += new EventHandler(this.cbo통화_SelectionChangeCommitted);
            this.cbo과세구분.SelectionChangeCommitted += new EventHandler(this.cbo과세구분_SelectionChangeCommitted);

            this.dtp발주일자.DateChanged += new EventHandler(this.dtp발주일자_DateChanged);
        }

        private void InitGrid()
        {
            #region 발주품목
            this._flex발주품목.BeginSetting(1, 1, false);

            this._flex발주품목.SetCol("CD_ITEM", "품목코드", 120, true);
            this._flex발주품목.SetCol("NM_ITEM", "품목명", 150, false);

            this._flex발주품목.SetCol("DT_LIMIT", "납기일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex발주품목.SetCol("DT_PLAN", "납품예정일", 75, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex발주품목.SetCol("QT_PO_MM", "발주량", 80, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex발주품목.SetCol("UNIT_IM", "재고단위", 80, false, typeof(string));
            
            if (this.b단가권한)
                this._flex발주품목.SetCol("UM_EX_PO", "단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            
            if (this.b금액권한)
            {
                this._flex발주품목.SetCol("AM_EX", "금액", 150, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex발주품목.SetCol("AM", "원화금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);

                this._flex발주품목.SetCol("VAT", "부가세", 150, 17, false, typeof(decimal), FormatTpType.MONEY);

                this._flex발주품목.SetCol("AM_TOTAL", "총금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
            }

            this._flex발주품목.SetCol("FG_TAX", "과세구분", 70, true);
            this._flex발주품목.SetCol("RATE_VAT", "부가세율", 0, false, typeof(decimal));

            this._flex발주품목.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
            this._flex발주품목.SetCol("NM_SL", "창고명", 120, false, typeof(string));
            this._flex발주품목.SetCol("CD_PJT", "프로젝트", 120, true, typeof(string));
            this._flex발주품목.SetCol("NO_LINE", "항번", 40, false, typeof(decimal), FormatTpType.QUANTITY);

            DataTable code2 = MA.GetCode("PU_C000093", false);
            
            if (code2 != null && code2.Rows.Count != 0)
            {
                foreach (DataRow row in code2.Rows)
                {
                    string colCaptionDD = D.GetString(row["CD_FLAG1"]) == string.Empty ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    this._flex발주품목.SetCol(D.GetString(row["NAME"]), colCaptionDD, 80, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                }
            }

            this._flex발주품목.SetDummyColumn("S", "QT_INVC", "FG_POCON", "NM_SYSDEF", "MEMO_CD", "CHECK_PEN");

            this._flex발주품목.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                     "NM_ITEM",
                                                                                                                     "STND_ITEM",
                                                                                                                     "UNIT_IM",
                                                                                                                     "CD_SL",
                                                                                                                     "NM_SL",
                                                                                                                     "CD_UNIT_MM",
                                                                                                                     "PI_PARTNER",
                                                                                                                     "PI_LN_PARTNER" }, new string[] { "CD_ITEM",
                                                                                                                                                       "NM_ITEM",
                                                                                                                                                       "STND_ITEM",
                                                                                                                                                       "UNIT_IM",
                                                                                                                                                       "CD_SL",
                                                                                                                                                       "NM_SL",
                                                                                                                                                       "UNIT_PO",
                                                                                                                                                       "PARTNER",
                                                                                                                                                       "LN_PARTNER" }, ResultMode.SlowMode);
            
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flex발주품목.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                                "NM_PJT",
                                                                                                                "SEQ_PROJECT",
                                                                                                                "CD_PJT_ITEM",
                                                                                                                "NM_PJT_ITEM",
                                                                                                                "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
                                                                                                                                                  "NM_PROJECT",
                                                                                                                                                  "SEQ_PROJECT",
                                                                                                                                                  "CD_PJT_ITEM",
                                                                                                                                                  "NM_PJT_ITEM",
                                                                                                                                                  "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
                                                                                                                                                                                    "NM_PROJECT",
                                                                                                                                                                                    "SEQ_PROJECT",
                                                                                                                                                                                    "CD_PJT_ITEM",
                                                                                                                                                                                    "PJT_NM_ITEM",
                                                                                                                                                                                    "PJT_STND_ITEM" }, ResultMode.FastMode);
                this._flex발주품목.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                                     "NM_PJT",
                                                                                                                     "SEQ_PROJECT",
                                                                                                                     "CD_PJT_ITEM",
                                                                                                                     "NM_PJT_ITEM",
                                                                                                                     "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
                                                                                                                                                       "NM_PROJECT",
                                                                                                                                                       "SEQ_PROJECT",
                                                                                                                                                       "CD_PJT_ITEM",
                                                                                                                                                       "NM_PJT_ITEM",
                                                                                                                                                       "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
                                                                                                                                                                                         "NM_PROJECT",
                                                                                                                                                                                         "SEQ_PROJECT",
                                                                                                                                                                                         "CD_PJT_ITEM",
                                                                                                                                                                                         "PJT_NM_ITEM",
                                                                                                                                                                                         "PJT_STND_ITEM" }, ResultMode.FastMode);
            }
            else
                this._flex발주품목.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
            
            this._flex발주품목.SetCodeHelpCol("NO_CBS", "H_PM_CBS_SUB", ShowHelpEnum.Always, new string[] { "NO_CBS", "CD_COST", "NM_COST" }, new string[] { "NO_CBS", "CD_COST", "NM_COST" });
            this._flex발주품목.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });
            this._flex발주품목.SetCodeHelpCol("CD_BUDGET", HelpID.P_FI_BGCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_BUDGET", "NM_BUDGET" }, new string[] { "CD_BUDGET", "NM_BUDGET" });
            
            if (this._YN_CdBizplan == "1")
                this._flex발주품목.SetCodeHelpCol("CD_BGACCT", "H_FI_BUDGETACCTJO_SUB", ShowHelpEnum.Always, new string[] { "CD_BGACCT", "NM_BGACCT" }, new string[] { "CD_BGACCT", "NM_BGACCT" });
            else
                this._flex발주품목.SetCodeHelpCol("CD_BGACCT", HelpID.P_FI_BGACCT_SUB, ShowHelpEnum.Always, new string[] { "CD_BGACCT", "NM_BGACCT" }, new string[] { "CD_BGACCT", "NM_BGACCT" });
            
            if (this.bStandard)
            {
                this._flex발주품목.SetCodeHelpCol("NM_CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L", "NM_CLS_L" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flex발주품목.SetCodeHelpCol("NM_CLS_M", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_M", "NM_CLS_M" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flex발주품목.SetCodeHelpCol("NM_CLS_S", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_S", "NM_CLS_S" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flex발주품목.SetCodeHelpCol("NM_ITEMGRP", HelpID.P_MA_ITEMGP_SUB, ShowHelpEnum.Always, new string[] { "GRP_ITEM", "NM_ITEMGRP" }, new string[] { "CD_ITEMGRP", "NM_ITEMGRP" });
            }

            this._flex발주품목.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_UNIT_MM", "NM_SL", "NM_PJT");
            this._flex발주품목.SetExceptEditCol("NM_CC", "NM_BUDGET", "NM_BGACCT");

            this._flex발주품목.VerifyAutoDelete = new string[] { "CD_ITEM" };
            
            List<string> stringList = new List<string>();
            stringList.Add("CD_ITEM");
            stringList.Add("DT_LIMIT");
            
            if (App.SystemEnv.PROJECT사용)
            {
                stringList.Add("CD_PJT");
                if (Config.MA_ENV.YN_UNIT == "Y")
                    stringList.Add("SEQ_PROJECT");
            }
            
            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000036") == "100")
                stringList.Add("UM_EX_PO");
            
            this._flex발주품목.VerifyNotNull = stringList.ToArray();
            //this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["QT_PO_MM"], 0, OperatorEnum.GreaterOrEqual);
            
            //if (this.b단가권한)
            //    this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["UM_EX_PO"], 0, OperatorEnum.GreaterOrEqual);
            
            //if (this.b금액권한)
            //{
            //    this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["AM_EX"], 0, OperatorEnum.GreaterOrEqual);
            //    this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["AM"], 0, OperatorEnum.GreaterOrEqual);
            //    this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["VAT"], 0, OperatorEnum.GreaterOrEqual);
            //}

            Config.UserColumnSetting.InitGrid_UserMenu(this._flex발주품목, this.PageID, true);
            this._flex발주품목.SettingVersion = "1.1.6";
            this._flex발주품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex발주품목.DisableNumberColumnSort();
            
            if (this._YN_REBATE)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    this._flex발주품목.SetExceptSumCol("UM_EX_PO", "UM_REBATE", "SEQ_PROJECT");
                else
                    this._flex발주품목.SetExceptSumCol("UM_EX_PO", "UM_REBATE");
            }
            else if (Config.MA_ENV.YN_UNIT == "Y")
                this._flex발주품목.SetExceptSumCol("UM_EX_PO", "SEQ_PROJECT");
            else
                this._flex발주품목.SetExceptSumCol("UM_EX_PO");

            this._flex발주품목.EnterKeyAddRow = true;
            
            //this._flex발주품목.VerifyCompare(this._flex발주품목.Cols["QT_PO_MM"], 0, OperatorEnum.Greater);
            
            this._flex발주품목.CellNoteInfo.EnabledCellNote = true;
            this._flex발주품목.CellNoteInfo.CategoryID = this.Name;
            this._flex발주품목.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";
            this._flex발주품목.CheckPenInfo.EnabledCheckPen = true;
            #endregion
        }

        private void InitControl()
        {
            this.SetHeadControlEnabled(true, 2);

            this.dtp발주일자.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp발주일자.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp발주일자.Text = Global.MainFrame.GetStringToday;

            this.dtp납기일.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp납기일.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp납기일.Text = Global.MainFrame.GetStringToday;

            DataSet comboData = this.GetComboData("N;MA_CODEDTL_005;MA_B000046",
                                                  "N;MA_B000005",
                                                  "N;PU_C000009",
                                                  "N;PU_C000014",
                                                  "S;FI_J000002",
                                                  "N;MA_AISPOSTH;200",
                                                  "S;PU_C000044",
                                                  "N;MA_B000141",
                                                  "N;MA_B000004");

            this.cbo과세구분.DataSource = comboData.Tables[0]; //N;MA_CODEDTL_005;MA_B000046
            this.cbo과세구분.DisplayMember = "NAME";
            this.cbo과세구분.ValueMember = "CODE";

            this._flex발주품목.SetDataMap("FG_TAX", comboData.Tables[0], "CODE", "NAME");
            
            this.cbo통화.DataSource = comboData.Tables[1]; //N;MA_B000005
            this.cbo통화.DisplayMember = "NAME";
            this.cbo통화.ValueMember = "CODE";

            DataRow[] dataRowArray1 = comboData.Tables[2].Select("CODE ='P'"); //N;PU_C000009
            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                this._ComfirmState = dataRowArray1[0]["NAME"].ToString();

            this.cbo지급조건.DataSource = comboData.Tables[3]; //N;PU_C000014
            this.cbo지급조건.DisplayMember = "NAME";
            this.cbo지급조건.ValueMember = "CODE";

            this.cbo전표유형.DataSource = comboData.Tables[4]; //S;FI_J000002
            this.cbo전표유형.DisplayMember = "NAME";
            this.cbo전표유형.ValueMember = "CODE";

            this.cbo매입형태.DataSource = comboData.Tables[5]; //N;MA_AISPOSTH;200
            this.cbo매입형태.DisplayMember = "NAME";
            this.cbo매입형태.ValueMember = "CODE";

            this._flex발주품목.SetDataMap("UNIT_IM", comboData.Tables[8].Copy(), "CODE", "NAME"); //N;MA_B000004
            
            if (this._지급관리통제설정 == "N")
            {
                this.cbo지급구분.DataSource = comboData.Tables[6]; //S;PU_C000044
                this.cbo지급구분.DisplayMember = "NAME";
                this.cbo지급구분.ValueMember = "CODE";
            }
            else
            {
                DataTable payList = ComFunc.GetPayList();

                if (payList != null)
                {
                    this.cbo지급구분.DataSource = payList;
                    this.cbo지급구분.DisplayMember = "NAME";
                    this.cbo지급구분.ValueMember = "CODE";
                }
            }
            
            this.cbo지급구분.SelectedValue = "001";
            
            if (!this.bStandard && !(Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2"))
                return;

            DataTable dataTable = comboData.Tables[7].Copy(); //N;MA_B000141
            
            DataRow[] dataRowArray2 = dataTable.Select("CODE = '001'");
            if (dataRowArray2.Length > 0)
            {
                this._flex발주품목.Cols["NUM_STND_ITEM_1"].Caption = D.GetString(dataRowArray2[0]["NAME"]);
                this._flex발주품목.Cols["NUM_STND_ITEM_1"].Visible = true;
                this._flex발주품목.Cols["NUM_STND_ITEM_1"].DataType = typeof(decimal);
                this._flex발주품목.Cols["NUM_STND_ITEM_1"].Format = "#,###,##0.####";
                this._flex발주품목.Cols["NUM_STND_ITEM_1"].AllowEditing = true;
            }
            
            DataRow[] dataRowArray3 = dataTable.Select("CODE = '002'");
            if (dataRowArray3.Length > 0)
            {
                this._flex발주품목.Cols["NUM_STND_ITEM_2"].Caption = D.GetString(dataRowArray3[0]["NAME"]);
                this._flex발주품목.Cols["NUM_STND_ITEM_2"].Visible = true;
                this._flex발주품목.Cols["NUM_STND_ITEM_2"].DataType = typeof(decimal);
                this._flex발주품목.Cols["NUM_STND_ITEM_2"].Format = "#,###,##0.####";
                this._flex발주품목.Cols["NUM_STND_ITEM_2"].AllowEditing = true;
            }
            
            DataRow[] dataRowArray4 = dataTable.Select("CODE = '003'");
            if (dataRowArray4.Length > 0)
            {
                this._flex발주품목.Cols["NUM_STND_ITEM_3"].Caption = D.GetString(dataRowArray4[0]["NAME"]);
                this._flex발주품목.Cols["NUM_STND_ITEM_3"].Visible = true;
                this._flex발주품목.Cols["NUM_STND_ITEM_3"].DataType = typeof(decimal);
                this._flex발주품목.Cols["NUM_STND_ITEM_3"].Format = "#,###,##0.####";
                this._flex발주품목.Cols["NUM_STND_ITEM_3"].AllowEditing = true;
            }

            DataRow[] dataRowArray5 = dataTable.Select("CODE = '004'");
            if (dataRowArray5.Length > 0)
            {
                this._flex발주품목.Cols["NUM_STND_ITEM_4"].Caption = D.GetString(dataRowArray5[0]["NAME"]);
                this._flex발주품목.Cols["NUM_STND_ITEM_4"].Visible = true;
                this._flex발주품목.Cols["NUM_STND_ITEM_4"].DataType = typeof(decimal);
                this._flex발주품목.Cols["NUM_STND_ITEM_4"].Format = "#,###,##0.####";
                this._flex발주품목.Cols["NUM_STND_ITEM_4"].AllowEditing = true;
            }
            
            DataRow[] dataRowArray6 = dataTable.Select("CODE = '005'");
            if (dataRowArray6.Length > 0)
            {
                this._flex발주품목.Cols["NUM_STND_ITEM_5"].Caption = D.GetString(dataRowArray6[0]["NAME"]);
                this._flex발주품목.Cols["NUM_STND_ITEM_5"].Visible = true;
                this._flex발주품목.Cols["NUM_STND_ITEM_5"].DataType = typeof(decimal);
                this._flex발주품목.Cols["NUM_STND_ITEM_5"].Format = "#,###,##0.####";
                this._flex발주품목.Cols["NUM_STND_ITEM_5"].AllowEditing = true;
            }
        }
        #endregion

        #region 메인버튼 이벤트
        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                this.cur부가세율.Enabled = false;

                if (e.JobMode == JobModeEnum.조회후수정)
                {
                    this._header.SetControlEnabled(false);
                    this.ctx담당자.Enabled = true;
                    this.txt비고.Enabled = true;
                    this.txt비고2.Enabled = true;
                    this.txt오더번호.Enabled = true;
                    this.btn매입형태적용.Enabled = false;
                }
                else
                {
                    this._header.SetControlEnabled(true);
                    this._header.CurrentRow["FG_UM"] = "001";
                    this._header.CurrentRow["TP_UM_TAX"] = "002";
                    this._header.CurrentRow["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

                    this.cbo지급조건.Enabled = false;
                    this.cbo매입형태.Enabled = false;
                    this.cbo전표유형.Enabled = false;
                    this.cbo지급구분.Enabled = false;
                    
                    this.ctx부가세사업장.ReadOnly = ReadOnly.TotalReadOnly;
                    
                    this.cbo과세구분.SelectedIndex = 0;
                    this.btn매입형태적용.Enabled = true;
                    this.cbo통화.SelectedIndex = 0;
                    
                    if (D.GetString(Global.MainFrame.LoginInfo.PurchaseGroupCode) != string.Empty)
                        this._header.CurrentRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                    
                    this.기초값설정();
                    this.ControlButtonEnabledDisable(null, true);
                }
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
                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool IsChanged()
        {
            if (base.IsChanged())
                return true;

            return this.헤더변경여부;
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarDeleteButtonEnabled = true;
                
                if (!this.IsChanged())
                    return;
                
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            this.str발주번호 = D.GetString(e.Args[0]);
            this.InitPaint();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch())
                    return;

                P_CZ_PU_PO_SUB2 dialog;

                if (BASIC.GetMAEXC_Menu("P_PU_PO_REG_AUTO", "PU_A00000001") != "100")
                    dialog = new P_CZ_PU_PO_SUB2();
                else
                    dialog = new P_CZ_PU_PO_SUB2("M|A|I|G");

                if (dialog.ShowDialog() == DialogResult.OK)
                    this.조회(dialog.m_NO_PO, dialog.m_btnType);
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

                if (!this.BeforeAdd())
                    return;

                this._flex발주품목.DataTable.Rows.Clear();
                this._flex발주품목.AcceptChanges();
                this._header.ClearAndNewRow();
                
                this.ControlButtonEnabledDisable(null, true);
                this.SetHeadControlEnabled(true, 2);
                this.cbo과세구분_SelectionChangeCommitted(null, null);
                this.ctx프로젝트.CodeValue = string.Empty;
                this.ctx프로젝트.CodeName = string.Empty;
                this.ctx창고.CodeValue = string.Empty;
                this.ctx창고.CodeName = string.Empty;
                this.one기본정보.Enabled = true;

                this._header.CurrentRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                
                this.기초값설정();
                
                if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                {
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;

                    this.ctx부가세사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
                    this.ctx부가세사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;

                    this.cbo지급구분.SelectedValue = "001";

                    if (this._header.CurrentRow["FG_TRANS"].ToString() == "001")
                    {
                        this.cbo전표유형.SelectedValue = "45";
                        this.cbo매입형태.SelectedValue = "001";
                    }
                    else
                    {
                        this.cbo전표유형.SelectedValue = "46";
                        this.cbo매입형태.SelectedValue = "002";
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete())
                    return;

                string _text_sub = string.Empty;
                
                this._biz.Delete(this.txt발주번호.Text, _text_sub);
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다, new string[0]);
                this.OnToolBarAddButtonClicked(sender, e);
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
            
            if (D.GetString(this._header.CurrentRow["YN_ORDER"]) == "Y" && this._flex발주품목.DataTable.Select("FG_POST = 'R'", string.Empty, DataViewRowState.CurrentRows).Length > 0)
            {
                if (this.ShowMessage(this.DD("발주상태가 확정입니다. 삭제하시겠습니까?"), "QY2") != DialogResult.Yes)
                    return false;
            }
            else if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까, new string[] { this.PageName }) != DialogResult.Yes)
                return false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave())
                    return;

                this.ToolBarSaveButtonEnabled = false;
                
                if (this.MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);

                    if (App.SystemEnv.PMS사용)
                        this.Reload(this.txt발주번호.Text);
                    
                    this.str복사구분 = "OK";
                }
                else
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this.ToolBarSaveButtonEnabled = true;

                if (this.추가모드여부)
                {
                    this.txt발주번호.Enabled = true;
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                }
            }
        }

        protected override bool BeforeSave()
        {
            if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보) && this.ShowMessage("발주유형이 매입자동프로세스 입니다.\n매입정보TAP에 데이터를 입력하셨으면'확인'버튼을 눌러주세요.", "QK2") != DialogResult.OK)
                return false;

            if (this._m_dt == "100")
            {
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    if (D.GetDecimal(this._header.CurrentRow["DT_PO"]) > D.GetDecimal(dataRow["DT_LIMIT"]) || D.GetDecimal(this._header.CurrentRow["DT_PO"]) > D.GetDecimal(dataRow["DT_PLAN"]))
                    {
                        this.ShowMessage("발주일자보다 납기일/납품예정일이 빠릅니다.");
                        return false;
                    }

                    if (D.GetDecimal(dataRow["DT_LIMIT"]) < D.GetDecimal(dataRow["DT_PLAN"]))
                    {
                        this.ShowMessage("납기일보다 납품예정일이 느립니다");
                        return false;
                    }
                }
            }

            if (!this.HeaderCheck(0))
                return false;
            
            if (!this.자동입고여부체크)
                return false;
            
            if (D.GetString(this._header.CurrentRow["TP_GR"]) == "103" || D.GetString(this._header.CurrentRow["TP_GR"]) == "104")
            {
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    if (D.GetString(dataRow["CD_SL"]) == string.Empty)
                    {
                        this.ShowMessage("발주유형이 입고 후까지 처리되는 경우 창고데이터는 필수입니다.");
                        return false;
                    }
                }
            }

            if (this.m_sEnv_FG_TAX == "100" && this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y")
            {
                DataTable table = this._flex발주품목.DataTable.DefaultView.ToTable(true, "FG_TAX");

                if (table.Rows.Count != 1 || D.GetString(table.Rows[0]["FG_TAX"]) != D.GetString(this._header.CurrentRow["FG_TAX"]))
                {
                    this.ShowMessage("과세구분이 일치하지 않는 품목이 있습니다.");
                    return false;
                }
            }

            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000035") == "100")
            {
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    if (D.GetString(dataRow["CD_SL"]) == string.Empty)
                    {
                        this.ShowMessage("저장시 창고데이터는 필수입니다. (메뉴별 통제)");
                        return false;
                    }
                }
            }

            return true;
        }

        protected override bool SaveData()
        {
            bool lb_RcvSave = false;
            bool lb_RevSave = false;

            if (!base.SaveData() || !this.Verify() || !this._flex발주품목.HasNormalRow && this._header.JobMode == JobModeEnum.추가후수정)
                return false;
            
            if (!this._flex발주품목.HasNormalRow)
            {
                this.OnToolBarDeleteButtonClicked(null, null);
                return false;
            }
            
            this.SUMFunction();
            StringBuilder stringBuilder1 = new StringBuilder();
            string str2 = "품목코드\t 매입형태코드\t";
            stringBuilder1.AppendLine(str2);
            string str3 = "-".PadRight(75, '-');
            stringBuilder1.AppendLine(str3);
            bool flag1 = true;
            string 발주번호 = string.Empty;

            if (this.추가모드여부)
            {
                발주번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "PY", this.dtp발주일자.Text.Substring(0, 6).Trim());
                this._header.CurrentRow["NO_PO"] = 발주번호;
                
                if (this._flex발주품목.HasNormalRow)
                {
                    int num1 = 0;

                    foreach (DataRow dataRow1 in this._flex발주품목.DataTable.Select())
                    {
                        ++num1;

                        if (dataRow1.RowState != DataRowState.Deleted)
                        {
                            dataRow1["NO_PO"] = 발주번호;
                            dataRow1["NO_LINE"] = num1;
                        }
                    }
                }

                if (this._flex발주품목.HasNormalRow && this._header.CurrentRow["YN_REQ"].ToString() == "N")
                {
                    this.cPU_RCVH.DT_PURCVH.Clear();
                    this.cPU_RCVL.DT_PURCV.Clear();
                    this.GetPU_RCV_Save(this._header.GetChanges(), this._flex발주품목.DataTable);
                    lb_RcvSave = true;
                }

                if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") == "200" && D.GetDecimal(this._flex발주품목["AM_OLD"]) > 0 && D.GetDecimal(this._flex발주품목["AM_OLD"]) < D.GetDecimal(this._flex발주품목["AM"]))
                {
                    this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                    return false;
                }
            }

            if (this._flex발주품목.HasNormalRow && this._header.CurrentRow["TP_GR"].ToString() == "101")
                lb_RevSave = true;
            
            DataTable ynSu = this._biz.GetYN_SU(D.GetString(this._header.CurrentRow["CD_TPPO"]));
            
            if (this._flex발주품목.HasNormalRow)
            {
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    if (dataRow.RowState == DataRowState.Added || this.str복사구분 == "COPY")
                    {
                        if (dataRow["YN_ORDER"].ToString() == "Y" || dataRow["YN_REQ"].ToString() == "N" || lb_RevSave)
                        {
                            dataRow["FG_POST"] = "R";
                            dataRow["FG_POCON"] = "002";
                            this.m_pnlHeader_Enabled();
                            this.SetHeadControlEnabled(false, 2);
                        }
                        else
                        {
                            dataRow["FG_POST"] = "O";
                            dataRow["FG_POCON"] = "001";
                            this.SetHeadControlEnabled(false, 1);
                        }

                        if (dataRow["NO_PO"].ToString().Trim().Length < 3)
                        {
                            if (this._header.CurrentRow["NO_PO"].ToString() == string.Empty)
                            {
                                this.ShowMessage("발주번호는 공백이 될 수 없습니다.");
                                return false;
                            }

                            dataRow["NO_PO"] = this._header.CurrentRow["NO_PO"].ToString();
                        }

                        if (D.GetString(this._header.CurrentRow["FG_TRANS"]) == "001" && D.GetString(dataRow["FG_TAX"]) == string.Empty)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("과세구분") });
                            return false;
                        }
                    }

                    if (this._m_Company_only == "001" && D.GetString(this._header.CurrentRow["FG_TPPURCHASE"]) != D.GetString(dataRow["CD_TP"]))
                    {
                        string str4 = dataRow["CD_ITEM"].ToString().PadRight(15, ' ') + " " + dataRow["CD_TP"].ToString().PadRight(15, ' ');
                        stringBuilder1.AppendLine(str4);
                        flag1 = false;
                    }
                }

                if (!flag1)
                {
                    this.ShowDetailMessage("발주형태의 매입정보와 품목정보의 매입정보가 다릅니다.\n▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(stringBuilder1));
                    return false;
                }
            }

            this._header.CurrentRow["DC50_PO"] = this.txt비고.Text;
            this._header.CurrentRow["FG_TRACK"] = "M";
            this._header.CurrentRow["DC_RMK2"] = this.txt비고2.Text;
            
            if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
            {
                #region 딘텍전용
                string 지급예정일 = string.Empty;
                decimal 원화금액 = this.cur공급가액.DecimalValue + this.cur부가세액.DecimalValue;
                
                DataTable dt = DBHelper.GetDataTable(string.Format(@"SELECT ISNULL(MP.DT_PAY_PREARRANGED, 0) AS DT_PAY_PREARRANGED
                                                                     FROM MA_PARTNER MP WITH(NOLOCK)
                                                                     LEFT JOIN CZ_MA_PARTNER MP1 WITH(NOLOCK) ON MP1.CD_COMPANY = MP.CD_COMPANY AND MP1.CD_PARTNER = MP.CD_PARTNER
                                                                     WHERE MP.CD_COMPANY = '{0}'
                                                                     AND MP.CD_PARTNER = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this.ctx매입처.CodeValue));

                지급예정일 = this.지급예정일계산(this.dtp매입일자.Text, 원화금액, Convert.ToInt32(dt.Rows[0]["DT_PAY_PREARRANGED"]));

                this.dtp지급예정일자.Text = 지급예정일;
                this.dtp만기일자.Text = 지급예정일;
                #endregion

                this._header.CurrentRow["DT_PROCESS_IV"] = this.dtp매입일자.Text;
                this._header.CurrentRow["DT_PAY_PRE_IV"] = this.dtp지급예정일자.Text;
                this._header.CurrentRow["DT_DUE_IV"] = this.dtp만기일자.Text;
                this._header.CurrentRow["FG_PAYBILL_IV"] = this.cbo지급구분.SelectedValue;
                this._header.CurrentRow["CD_DOCU_IV"] = !(D.GetString(this.cbo전표유형.SelectedValue) == string.Empty) ? this.cbo전표유형.SelectedValue : "45";
                this._header.CurrentRow["AM_K_IV"] = this.cur공급가액.DecimalValue;
                this._header.CurrentRow["VAT_TAX_IV"] = this.cur부가세액.DecimalValue;
                this._header.CurrentRow["DC_RMK_IV"] = this.txt매입정보비고.Text;
            }

            DataTable changes1 = this._header.GetChanges();
            DataTable dataTable1 = this._flex발주품목.GetChanges();

            if (changes1 == null && dataTable1 == null)
                return true;
            
            this.cPU_RCVH.DT_PURCVH.GetChanges();
            this.cPU_RCVL.DT_PURCV.GetChanges();
            string empty3 = string.Empty;
            
            DataTable dt_budget = this._biz.PU_BUDGET_HST();
            
            if (this._구매예산CHK설정FI == "100" && dataTable1 != null)
            {
                string str4 = "YN_BUDGET = 'Y' AND ( CD_BUDGET = '' OR CD_BUDGET IS NULL  OR CD_BGACCT = '' OR CD_BGACCT IS NULL ";

                if (this._YN_CdBizplan == "1")
                    str4 += " OR CD_BIZPLAN = '' OR CD_BIZPLAN IS NULL ";
                
                DataRow[] dataRowArray1 = this._flex발주품목.DataTable.Select(str4 + " ) ");
                
                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("예산확인 선택(Y)시 발주형태,CC코드,예산단위,예산계정은 필수입력입니다.");
                    this._flex발주품목.Focus();
                    return false;
                }

                string empty1 = string.Empty;
                DataTable dataTable2 = this.예산chk(this._flex발주품목.DataTable);
                
                if (dataTable2.Rows.Count > 0)
                {
                    string filterExpression = !(this._구매예산CHK설정FI == "000") ? "( AM_JAN < 0 AND TP_BUNIT = 'Y') AND ERROR_MSG IS NOT NULL" : "( AM_JAN < 0 AND TP_BUNIT = '4') AND ERROR_MSG IS NOT NULL";
                    DataRow[] dataRowArray2 = dataTable2.Select(filterExpression);
                    bool flag2;

                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage("예산통제대상계정이 예산잔액을 초과했거나 CHK시 오류가 발생했습니다");
                        flag2 = false;
                        this._header.CurrentRow["BUDGET_PASS"] = "N";

                        for (int index = 0; index < this._flex발주품목.DataTable.Rows.Count; ++index)
                        {
                            if (this._flex발주품목.DataTable.Rows[index].RowState != DataRowState.Deleted && D.GetString(this._flex발주품목.DataTable.Rows[index]["YN_BUDGET"]) == "Y")
                                this._flex발주품목.DataTable.Rows[index]["BUDGET_PASS"] = "N";
                        }
                    }
                    else
                    {
                        flag2 = true;
                        this._header.CurrentRow["BUDGET_PASS"] = "Y";

                        for (int index = 0; index < this._flex발주품목.DataTable.Rows.Count; ++index)
                        {
                            if (this._flex발주품목.DataTable.Rows[index].RowState != DataRowState.Deleted && D.GetString(this._flex발주품목.DataTable.Rows[index]["YN_BUDGET"]) == "Y")
                                this._flex발주품목.DataTable.Rows[index]["BUDGET_PASS"] = "Y";
                        }
                    }

                    P_PU_BUDGET_SUB pPuBudgetSub = new P_PU_BUDGET_SUB(this._flex발주품목.DataTable, this.dtp발주일자.Text, "NO_PO");
                    pPuBudgetSub.ShowDialog();

                    if (pPuBudgetSub.ShowDialog() == DialogResult.OK || !flag2)
                        return false;
                    
                    foreach (DataRow row1 in dataTable2.Rows)
                    {
                        DataRow row2 = dt_budget.NewRow();

                        row2["NO_PU"] = this._header.CurrentRow["NO_PO"].ToString();
                        row2["NENU_TYPE"] = "PU_PO_REG";
                        row2["CD_BUDGET"] = row1["CD_BUDGET"];
                        row2["NM_BUDGET"] = row1["NM_BUDGET"];
                        row2["CD_BGACCT"] = row1["CD_BGACCT"];
                        row2["NM_BGACCT"] = row1["NM_BGACCT"];
                        row2["CD_BIZPLAN"] = row1["CD_BIZPLAN"];
                        row2["NM_BIZPLAN"] = row1["NM_BIZPLAN"];
                        row2["AM_ACTSUM"] = row1["AM_ACTSUM"];
                        row2["AM_JSUM"] = row1["AM"];
                        row2["RT_JSUM"] = row1["RT_JSUM"];
                        row2["AM"] = row1["AM"];
                        row2["AM_JAN"] = row1["AM_JAN"];
                        row2["TP_BUNIT"] = row1["TP_BUNIT"];
                        row2["ERROR_MSG"] = row1["ERROR_MSG"];
                        row2["ID_INSERT"] = Global.MainFrame.LoginInfo.EmployeeNo;
                        
                        dt_budget.Rows.Add(row2);
                    }
                }
            }

            if (this.추가모드여부 && D.GetString(this._header.CurrentRow["YN_ORDER"]) == "Y" && BASIC.GetMAEXC("발주등록(공장)-프로젝트예산통제설정") == "100")
            {
                P_PU_PJT_BUDGET_CTL_SUB puPjtBudgetCtlSub = new P_PU_PJT_BUDGET_CTL_SUB(dataTable1, "NO_PO", "REG");

                if (puPjtBudgetCtlSub.ShowDialog() != DialogResult.OK)
                    return false;
                
                if (puPjtBudgetCtlSub.ret_data != null && puPjtBudgetCtlSub.ret_data.Rows.Count != 0)
                    dataTable1 = puPjtBudgetCtlSub.ret_data;
            }

            DataTable dtLOT = null;
            string strNOIO = string.Empty;

            if (D.GetString(this._header.CurrentRow["TP_GR"]) == "103" || D.GetString(this._header.CurrentRow["TP_GR"]) == "104" && Global.MainFrame.LoginInfo.MngLot == "Y")
            {
                DataRow[] dataRowArray = this._flex발주품목.DataTable.Select("FG_SERNO = '002'");

                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    DataTable dt = this._biz.dtLot_Schema();

                    if (this.추가모드여부)
                    {
                        strNOIO = !(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y") ? (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6)) : (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "27", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6));
                    }
                    else
                    {
                        string lotio = this._biz.getLOTIO(D.GetString(this._header.CurrentRow["NO_PO"]));

                        if (lotio == string.Empty)
                            return false;

                        strNOIO = lotio;
                    }

                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (dataRow.RowState == DataRowState.Added)
                        {
                            DataRow row = dt.NewRow();
                            row["NO_IO"] = strNOIO;
                            row["NO_IOLINE"] = dataRow["NO_LINE"];
                            row["CD_QTIOTP"] = D.GetString(this._header.CurrentRow["FG_TPRCV"]);
                            row["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드,
                                                                                                                                D.GetString(this._header.CurrentRow["FG_TPRCV"]) })["NM_QTIOTP"];
                            row["FG_TRANS"] = D.GetString(this._header.CurrentRow["FG_TRANS"]);
                            row["DT_IO"] = D.GetString(this._header.CurrentRow["DT_PO"]);
                            row["CD_SL"] = dataRow["CD_SL"];
                            row["NM_SL"] = dataRow["NM_SL"];
                            row["CD_ITEM"] = dataRow["CD_ITEM"];
                            row["NM_ITEM"] = dataRow["NM_ITEM"];
                            row["UNIT_IM"] = dataRow["UNIT_IM"];
                            row["STND_ITEM"] = dataRow["STND_ITEM"];
                            row["CD_PJT"] = dataRow["CD_PJT"];
                            row["NM_PROJECT"] = dataRow["NM_PJT"];
                            row["AM"] = dataRow["AM"];
                            row["AM_EX"] = dataRow["AM_EX"];
                            row["UM_EX"] = dataRow["UM_EX"];
                            row["QT_GOOD_INV"] = dataRow["QT_PO"];

                            if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                            {
                                row["FG_IO"] = "030";
                                row["YN_RETURN"] = "Y";
                            }
                            else
                                row["FG_IO"] = "001";

                            row["CD_PLANT"] = dataRow["CD_PLANT"];
                            row["YN_RETURN"] = "N";
                            dt.Rows.Add(row);
                        }
                    }

                    if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                    {
                        P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(dt, "Y");

                        if (pPuLotSubI.ShowDialog() != DialogResult.OK)
                            return false;

                        dtLOT = pPuLotSubI.dtL;
                    }
                    else
                    {
                        P_PU_LOT_SUB_R pPuLotSubR = new P_PU_LOT_SUB_R(dt);

                        if (pPuLotSubR.ShowDialog() != DialogResult.OK)
                            return false;

                        dtLOT = pPuLotSubR.dtL;
                    }
                }
            }
            
            if (!this._biz.Save(changes1, dataTable1, lb_RcvSave, this.cPU_RCVH.DT_PURCVH, this.cPU_RCVL.DT_PURCV, this.str복사구분, dt_budget, this._header.CurrentRow["NO_PO"].ToString(), this._infosub_dlg.si_return, lb_RevSave, dtLOT, strNOIO))
                return false;
            
            if (this.추가모드여부)
                this.txt발주번호.Text = 발주번호;
            
            this._flex발주품목.Focus();
            this._header.AcceptChanges();
            this._flex발주품목.AcceptChanges();
            
            DataTable dataValue = (DataTable)this._infosub_dlg.si_return.DataValue;
            
            if (dataValue != null)
                dataValue.AcceptChanges();
            
            return true;
        }
        #endregion

        #region 버튼 이벤트
        private void btn프로젝트적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex발주품목.DataTable == null || this.ctx프로젝트.CodeName.ToString() == string.Empty || !this.확정여부())
                    return;
                
                for (int index = this._flex발주품목.Rows.Fixed; index < this._flex발주품목.Rows.Count; ++index)
                {
                    if (this._flex발주품목.RowState(index) != DataRowState.Deleted)
                    {
                        this._flex발주품목.Rows[index]["CD_PJT"] = this.ctx프로젝트.CodeValue;

                        if (Config.MA_ENV.YN_UNIT == "Y")
                        {
                            this._flex발주품목.Rows[index]["SEQ_PROJECT"] = this.d_SEQ_PROJECT;
                            this._flex발주품목.Rows[index]["CD_PJT_ITEM"] = this.s_CD_PJT_ITEM;
                            this._flex발주품목.Rows[index]["NM_PJT_ITEM"] = this.s_NM_PJT_ITEM;
                            this._flex발주품목.Rows[index]["PJT_ITEM_STND"] = this.s_PJT_ITEM_STND;
                        }
                        
                        if (this.m_sEnv_CC == "200")
                            this.SetCC(index, string.Empty);
                        
                        if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(this._flex발주품목.Rows[index]["CD_USERDEF14"]) == "001")
                        {
                            string str = this._biz.pjt_item_josun(D.GetString(this._flex발주품목.Rows[index]["CD_PJT"]));
                            
                            if (str != string.Empty)
                            {
                                this._flex발주품목.Rows[index]["FG_TAX"] = str;
                                this._flex발주품목.Rows[index]["RATE_VAT"] = 0;
                                Decimal num1 = 0;
                                this._flex발주품목.Rows[index]["RATE_VAT"] = num1;
                                Decimal num2 = num1 == 0 ? 0 : num1 / 100;

                                if (num2 == 0 || Convert.ToDecimal(this._flex발주품목.Rows[index]["AM"]) == 0)
                                    this._flex발주품목.Rows[index]["VAT"] = 0;
                                else
                                    this._flex발주품목.Rows[index]["VAT"] = this.원화계산(Convert.ToDecimal(this._flex발주품목.Rows[index]["AM"]) * num2);
                                
                                this._flex발주품목.Rows[index]["AM_TOTAL"] = this.원화계산(D.GetDecimal(this._flex발주품목.Rows[index]["AM"]) + D.GetDecimal(this._flex발주품목.Rows[index]["VAT"]));
                            }
                            else
                            {
                                this._flex발주품목.Rows[index]["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                                this._flex발주품목.Rows[index]["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                Decimal decimalValue = this.cur부가세율.DecimalValue;
                                this._flex발주품목.Rows[index]["RATE_VAT"] = decimalValue;
                                Decimal num = decimalValue == 0 ? 0 : decimalValue / 100;
                                
                                if (num == 0 || Convert.ToDecimal(this._flex발주품목.Rows[index]["AM"]) == 0)
                                    this._flex발주품목.Rows[index]["VAT"] = 0;
                                else
                                    this._flex발주품목.Rows[index]["VAT"] = this.원화계산(Convert.ToDecimal(this._flex발주품목.Rows[index]["AM"]) * num);
                                
                                this._flex발주품목.Rows[index]["AM_TOTAL"] = this.원화계산(D.GetDecimal(this._flex발주품목.Rows[index]["AM"]) + D.GetDecimal(this._flex발주품목.Rows[index]["VAT"]));
                            }
                        }
                    }
                }

                this.ShowMessage("적용작업을완료하였습니다");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn창고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ctx창고.CodeValue == string.Empty)
                    return;
                
                foreach (DataRow row in this._flex발주품목.DataTable.Select())
                {
                    row["CD_SL"] = this.ctx창고.CodeValue;
                    row["NM_SL"] = this.ctx창고.CodeName;
                    DataTable dataTable = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              this.dtp발주일자.Text.Substring(0, 4),
                                                                              Global.MainFrame.LoginInfo.CdPlant,
                                                                              D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]) });

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        row["QT_INVC"] = dataTable.Rows[0]["QT_INVC"];
                        row["QT_ATPC"] = dataTable.Rows[0]["QT_ATPC"];
                    }
                    
                    if (this.m_sEnv_CC_Menu == "100")
                        this.SetCC_Priority(row, null, null, null, null);
                }

                this.ShowMessage(this.DD("적용작업을완료하였습니다"));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn통화명_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this._flex발주품목.DataTable.Select())
                    this.부가세계산(row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn납기일적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex발주품목.DataTable.Rows == null || this.dtp납기일.Text == string.Empty)
                    return;
                
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    dataRow["DT_LIMIT"] = this.dtp납기일.Text;
                    dataRow["DT_PLAN"] = this.dtp납기일.Text;
                }
                
                this.ShowMessage("적용작업을완료하였습니다");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn매입형태적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                    return;
                
                foreach (DataRow row in this._flex발주품목.DataTable.Rows)
                    row["FG_PURCHASE"] = this.cbo매입형태.SelectedValue;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur공급가액_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                    return;

                Decimal num2 = 0;
                int num3 = this._flex발주품목.Rows.Count - this._flex발주품목.Rows.Fixed;
                Decimal num4 = D.GetDecimal(this._flex발주품목.DataTable.Compute("SUM(AM)", string.Empty));
                Decimal num5 = num4 + 99;
                Decimal num6 = num4 - 99;
                this.cur부가세액.DecimalValue = this.원화계산(this.cur공급가액.DecimalValue * this.cur부가세율.DecimalValue * new Decimal(1, 0, 0, false, (byte)2));
                
                if (this.cur공급가액.DecimalValue > num5 || this.cur공급가액.DecimalValue < num6)
                {
                    this.ShowMessage("수정가능금액은 100원 범위 입니다.");
                    this.cur공급가액.DecimalValue = num4;
                }
                else
                {
                    Decimal num7 = 0;
                    
                    for (int index = this._flex발주품목.Rows.Fixed; index < this._flex발주품목.Rows.Count; ++index)
                    {
                        num7 += D.GetDecimal(this._flex발주품목.Rows[index]["AM"]);
                        num2 += D.GetDecimal(this._flex발주품목.Rows[index]["VAT"]);
                    }

                    this._flex발주품목.Rows[num3 + 1]["AM"] = (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["AM"]) + (this.cur공급가액.DecimalValue - num7));
                    this._flex발주품목.Rows[num3 + 1]["VAT"] = (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["VAT"]) + (this.cur부가세액.DecimalValue - num2));
                    this._flex발주품목.Rows[num3 + 1]["AM_TOTAL"] = (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["VAT"]) + D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["AM"]));
                    
                    if (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["QT_PO"]) != 0)
                        this._flex발주품목.Rows[num3 + 1]["UM"] = (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["AM"]) / D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["QT_PO"]));
                    
                    if (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["QT_PO_MM"]) != 0)
                        this._flex발주품목.Rows[num3 + 1]["UM_P"] = (D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["AM"]) / D.GetDecimal(this._flex발주품목.Rows[num3 + 1]["QT_PO_MM"]));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur부가세액_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                    return;
                
                Decimal num1 = 0;
                int num2 = this._flex발주품목.Rows.Count - this._flex발주품목.Rows.Fixed;
                
                for (int index = this._flex발주품목.Rows.Fixed; index < this._flex발주품목.Rows.Count; ++index)
                    num1 += D.GetDecimal(this._flex발주품목.Rows[index]["VAT"]);
                
                this._flex발주품목.Rows[num2 + 1]["VAT"] = (D.GetDecimal(this._flex발주품목.Rows[num2 + 1]["VAT"]) + (this.cur부가세액.DecimalValue - num1));
                this._flex발주품목.Rows[num2 + 1]["AM_TOTAL"] = (D.GetDecimal(this._flex발주품목.Rows[num2 + 1]["VAT"]) + D.GetDecimal(this._flex발주품목.Rows[num2 + 1]["AM"]));
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
                if (e.ControlName == this.ctx발주유형.Name)
                {
                    e.HelpParam.P61_CODE1 = "N";

                    if (this._반품발주 == "Y")
                        e.HelpParam.P41_CD_FIELD1 = "Y";
                }
                else if (e.ControlName == this.ctx창고.Name)
                    e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
                else if (e.ControlName == this.ctx프로젝트.Name)
                    e.HelpParam.P41_CD_FIELD1 = "프로젝트";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.ctx담당자.Name)
                {
                    this._header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"];
                    this._header.CurrentRow["NM_DEPT"] = e.HelpReturn.Rows[0]["NM_DEPT"];
                }
                else if (name == this.ctx구매그룹.Name)
                {
                    if (e.CodeValue != null)
                    {
                        this._header.CurrentRow["PURGRP_NO_TEL"] = e.HelpReturn.Rows[0]["NO_TEL"];
                        this._header.CurrentRow["PURGRP_NO_FAX"] = e.HelpReturn.Rows[0]["NO_FAX"];
                        this._header.CurrentRow["PURGRP_E_MAIL"] = e.HelpReturn.Rows[0]["E_MAIL"];
                        this._header.CurrentRow["PO_PRICE"] = "N";
                        string arg_cd_purgrp = e.HelpReturn.Rows[0]["CD_PURGRP"].ToString();

                        DataTable dataTable = Global.MainFrame.FillDataTable(@"SELECT O.PO_PRICE
                                                                               FROM MA_PURGRP G WITH(NOLOCK) 
                                                                               LEFT JOIN MA_PURORG O WITH(NOLOCK) ON G.CD_COMPANY = O.CD_COMPANY AND G.CD_PURORG = O.CD_PURORG
                                                                               WHERE G.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                              "AND G.CD_PURGRP  = '" + arg_cd_purgrp + "'");

                        if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["PO_PRICE"] != DBNull.Value && dataTable.Rows[0]["PO_PRICE"].ToString().Trim() != string.Empty))
                            this._header.CurrentRow["PO_PRICE"] = dataTable.Rows[0]["PO_PRICE"].ToString().Trim();

                        this.SetCC(0, arg_cd_purgrp);

                        return;
                    }

                    this.ctx구매그룹.CodeValue = string.Empty;
                    this.ctx구매그룹.CodeName = string.Empty;

                    this._header.CurrentRow["PURGRP_NO_TEL"] = string.Empty;
                    this._header.CurrentRow["PURGRP_NO_FAX"] = string.Empty;
                    this._header.CurrentRow["PURGRP_E_MAIL"] = string.Empty;
                    this._header.CurrentRow["PO_PRICE"] = "N";
                    this._header.CurrentRow["CD_CC_PURGRP"] = string.Empty;
                    this._header.CurrentRow["NM_CC_PURGRP"] = string.Empty;
                }
                else if (name == this.ctx프로젝트.Name)
                {
                    P_CZ_PU_PO_SUB2 dialog = new P_CZ_PU_PO_SUB2(this.ctx프로젝트.CodeValue);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow dr in this._flex발주품목.DataTable.Rows)
                        {
                            dr["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        }

                        if (Config.MA_ENV.YN_UNIT == "Y")
                        {
                            this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
                            this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
                            this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
                            this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
                        }
                    }
                    else
                    {
                        this.ctx프로젝트.CodeValue = string.Empty;
                        this.ctx프로젝트.CodeName = string.Empty;
                    }
                }
                else if (name == this.ctx매입처.Name)
                {
                    DataTable dt = this._biz.GetFG_PAYMENT(this.ctx매입처.CodeValue);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this._header.CurrentRow["FG_PAYMENT"] = dt.Rows[0]["FG_PAYMENT"].ToString();
                        this.cbo지급조건.SelectedValue = dt.Rows[0]["FG_PAYMENT"].ToString();
                    }
                    else
                    {
                        this._header.CurrentRow["FG_PAYMENT"] = string.Empty;
                        this.cbo지급조건.SelectedValue = string.Empty;
                    }
                    
                    if (this.s_PTR_SUB == "001")
                    {
                        DataTable maPartnerSub = this._biz.get_MA_PARTNER_SUB(this.ctx매입처.CodeValue);

                        if (maPartnerSub != null && maPartnerSub.Rows.Count > 0)
                        {
                            this.SetTppoAfter(D.GetString(maPartnerSub.Rows[0]["CD_TPPO"]), D.GetString(maPartnerSub.Rows[0]["NM_TPPO"]));
                            this.SetPurgrpAfter(D.GetString(maPartnerSub.Rows[0]["CD_PURGRP"]), D.GetString(maPartnerSub.Rows[0]["NM_PURGRP"]));
                        }
                    }
                }
                else if (name == this.ctx발주유형.Name)
                {
                    DataTable ynSu = this._biz.GetYN_SU(D.GetString(e.HelpReturn.Rows[0]["CD_TPPO"]));

                    if (this.sPUSU == "100" && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y" && (D.GetString(e.HelpReturn.Rows[0]["FG_TRANS"]) != "004" && D.GetString(e.HelpReturn.Rows[0]["FG_TRANS"]) != "005"))
                    {
                        this.ShowMessage(this.DD("국내인 외주발주가 있습니다. 발주유형을 확인하세요."));
                        this.ctx발주유형.CodeValue = string.Empty;
                        this.ctx발주유형.CodeName = string.Empty;
                        this._header.CurrentRow["CD_TPPO"] = string.Empty;
                        return;
                    }

                    this._header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];
                    this._header.CurrentRow["FG_TPRCV"] = e.HelpReturn.Rows[0]["FG_TPRCV"];
                    this._header.CurrentRow["FG_TPPURCHASE"] = e.HelpReturn.Rows[0]["FG_TPPURCHASE"];
                    this._header.CurrentRow["YN_AUTORCV"] = e.HelpReturn.Rows[0]["YN_AUTORCV"];
                    this._header.CurrentRow["YN_RCV"] = e.HelpReturn.Rows[0]["YN_RCV"];
                    this._header.CurrentRow["YN_RETURN"] = e.HelpReturn.Rows[0]["YN_RETURN"];
                    this._header.CurrentRow["YN_SUBCON"] = e.HelpReturn.Rows[0]["YN_SUBCON"];
                    this._header.CurrentRow["YN_IMPORT"] = e.HelpReturn.Rows[0]["YN_IMPORT"];
                    this._header.CurrentRow["YN_ORDER"] = e.HelpReturn.Rows[0]["YN_ORDER"];
                    this._header.CurrentRow["YN_REQ"] = e.HelpReturn.Rows[0]["YN_REQ"];
                    this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                    this._header.CurrentRow["NM_TRANS"] = e.HelpReturn.Rows[0]["NM_TRANS"];
                    this._header.CurrentRow["FG_TAX"] = e.HelpReturn.Rows[0]["FG_TAX"];
                    this._header.CurrentRow["TP_GR"] = e.HelpReturn.Rows[0]["TP_GR"];
                    this._header.CurrentRow["CD_CC_TPPO"] = e.HelpReturn.Rows[0]["CD_CC"];
                    this._header.CurrentRow["NM_CC_TPPO"] = this._biz.GetCCCodeSearch(e.HelpReturn.Rows[0]["CD_CC"].ToString());
                    this.거래구분(e.HelpReturn.Rows[0]["FG_TRANS"].ToString(), D.GetString(e.HelpReturn.Rows[0]["FG_TAX"]));
                    this.Setting_pu_poh_sub();

                    if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                    {
                        this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                        this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                        this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                        
                        this.ctx부가세사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
                        this.ctx부가세사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;

                        this.cbo지급구분.SelectedValue = "001";

                        if (this._header.CurrentRow["FG_TRANS"].ToString() == "001")
                        {
                            this.cbo전표유형.SelectedValue = "45";
                            this.cbo매입형태.SelectedValue = "001";
                        }
                        else
                        {
                            this.cbo전표유형.SelectedValue = "46";
                            this.cbo매입형태.SelectedValue = "002";
                        }
                    }

                    this.FillPol();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_CodeChanged(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.ctx담당자.Name)
                {
                    if (!(this.ctx담당자.CodeValue == string.Empty))
                        return;

                    this._header.CurrentRow["CD_DEPT"] = string.Empty;
                    this._header.CurrentRow["NM_DEPT"] = string.Empty;
                }
                else if (name == this.ctx구매그룹.Name)
                {
                    if (!(this.ctx구매그룹.CodeValue == string.Empty))
                        return;

                    this._header.CurrentRow["PURGRP_NO_TEL"] = string.Empty;
                    this._header.CurrentRow["PURGRP_NO_FAX"] = string.Empty;
                    this._header.CurrentRow["PURGRP_E_MAIL"] = string.Empty;
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
                if (!this.btn추가.Enabled)
                    return;

                if (!this.전자결재여부)
                {
                    this.ShowMessage("전자결제 진행된 발주건은 추가가 불가능 합니다.");
                }
                else if (!this.차수여부)
                {
                    this.ShowMessage("변경된 차수이므로 추가가 불가능 합니다.");
                }
                else
                {
                    if (!this.HeaderCheck(0) || this._flex발주품목.DataTable == null)
                        return;

                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.btn추가.Enabled = false;
                    Decimal num4 = this._flex발주품목.GetMaxValue("NO_LINE") + 1;
                    this._flex발주품목.Rows.Add();
                    this._flex발주품목.Row = this._flex발주품목.Rows.Count - 1;

                    if (this.txt발주번호.Text != string.Empty)
                        this._flex발주품목["NO_PO"] = this.txt발주번호.Text;

                    this._flex발주품목["NO_LINE"] = num4;
                    this._flex발주품목["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
                    this._flex발주품목["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    this._flex발주품목["NM_PJT"] = this.ctx프로젝트.CodeName;
                    this._flex발주품목["NO_PR"] = string.Empty;
                    this._flex발주품목["CD_EXCH"] = this.cbo통화.SelectedValue.ToString();
                    this._flex발주품목["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flex발주품목["FG_POST"].ToString());

                    foreach (DataRow row in ((DataTable)this.cbo통화.DataSource).Rows)
                    {
                        if (row["CODE"].ToString() == this.cbo통화.SelectedValue.ToString())
                        {
                            this._flex발주품목["NM_EXCH"] = row["NAME"].ToString();
                            break;
                        }
                    }

                    if (this.dtp납기일.Text == string.Empty)
                    {
                        if (this._flex발주품목.Row != this._flex발주품목.Rows.Fixed)
                            this._flex발주품목["DT_LIMIT"] = this._flex발주품목[this._flex발주품목.Row - 1, "DT_LIMIT"];
                    }
                    else
                        this._flex발주품목["DT_LIMIT"] = this.dtp납기일.Text;

                    this.FillPol(this._flex발주품목.Row);
                    this._flex발주품목.AddFinished();
                    this._flex발주품목.Col = this._flex발주품목.Cols.Fixed;
                    this._flex발주품목.Focus();
                    this.SetHeadControlEnabled(false, 1);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn추가.Enabled = true;
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                {
                    this.ctx프로젝트.Enabled = true;
                    this.btn프로젝트적용.Enabled = true;
                    this.ctx창고.Enabled = true;
                    this.btn창고적용.Enabled = true;
                }
                else if (!this.전자결재여부)
                {
                    this.ShowMessage("전자결제 진행된 발주건은 삭제가 불가능 합니다.");
                }
                else if (!this.차수여부)
                {
                    this.ShowMessage("변경된 차수이므로 삭제가 불가능 합니다.");
                }
                else
                {
                    if (D.GetString(this._flex발주품목["YN_ORDER"]) == "Y" && D.GetString(this._flex발주품목["FG_POST"]) == "R")
                    {
                        if (this.ShowMessage("발주상태가 확정입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                            return;

                        this._flex발주품목.Rows.Remove(this._flex발주품목.Row);
                    }
                    else if (this._flex발주품목["FG_POST"].ToString() == "R")
                    {
                        this.ShowMessage("발주상태가 미정일 경우에만 삭제가능합니다");
                    }
                    else
                    {
                        this._flex발주품목.Rows.Remove(this._flex발주품목.Row);
                    }

                    if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                        this.SUMFunction();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                if (!this._flex발주품목.HasNormalRow)
                {
                    this.ControlButtonEnabledDisable(null, true);
                    this.SetHeadControlEnabled(true, 1);
                    this.btn추가.Enabled = true;
                }

                if (this.txt발주번호.Text != string.Empty)
                {
                    this.ctx매입처.Enabled = false;
                    this.ctx발주유형.Enabled = false;
                    this.cbo통화.Enabled = false;
                }
            }
        }

        private void cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.cbo통화.SelectedValue) == "000")
                {
                    this.cur통화명.DecimalValue = 1;
                    this._header.CurrentRow["RT_EXCH"] = 1;
                    this.cur통화명.Enabled = false;
                }
                else
                {
                    this.SetExchageApply();

                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        this.cur통화명.Enabled = false;
                    else
                        this.cur통화명.Enabled = true;
                }

                this.SetExchageMoney();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur통화_Validated(object sender, EventArgs e)
        {
            try
            {
                this.SetExchageMoney();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void dtp발주일자_DateChanged(object sender, EventArgs e)
        {
            this.SetExchageApply();
        }

        private void cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {   
                Decimal num1 = new Decimal(10, 0, 0, false, (byte)1);
                Decimal num2 = new Decimal(1, 0, 0, false, (byte)1);
                string ps_taxp = string.Empty;

                if (this.cbo과세구분.SelectedValue != null)
                    ps_taxp = this.cbo과세구분.SelectedValue.ToString();

                if (this.s_vat_fictitious == "100")
                {
                    if (this.의제매입여부(ps_taxp))
                        this._header.CurrentRow["TP_UM_TAX"] = "001";
                    else
                        this._header.CurrentRow["TP_UM_TAX"] = "002";
                }

                this.부가세율(ps_taxp);

                if (this._flex발주품목.HasNormalRow)
                {
                    num1 = this._flex발주품목.CDecimal(this.cur통화명.ClipText);
                    Decimal num3 = this._flex발주품목.CDecimal(this.cur부가세율.ClipText) / 100;

                    for (int index = 0; index < this._flex발주품목.DataTable.Rows.Count; ++index)
                    {
                        try
                        {
                            Decimal num4 = this._flex발주품목.CDecimal(this._flex발주품목.DataTable.Rows[index]["AM"]);
                            this._flex발주품목.DataTable.Rows[index]["VAT"] = this.원화계산(num4 * num3);
                            this._flex발주품목.DataTable.Rows[index]["FG_TAX"] = ps_taxp;
                        }
                        catch (Exception ex)
                        {
                            this.MsgEnd(ex);
                        }
                    }

                    this._flex발주품목.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            this.SUMFunction();
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            string name = ((Control)sender).Name;

            if (name == this.txt비고.Name ||
                name == this.txt비고2.Name)
            {
                this.ToolBarSaveButtonEnabled = true;

                if (name != this.txt비고.Name && name != this.txt비고2.Name)
                    return;

                this.SetHeadControlEnabled(false, 2);
            }
            else
            {
                if (e.KeyData != Keys.Return)
                    return;

                SendKeys.SendWait("{TAB}");
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex발주품목_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flex발주품목["FG_POST"].ToString().Trim() != "O" && this._flex발주품목.RowState() != DataRowState.Added)
                    e.Cancel = true;
                
                if (!this.차수여부 || !this.전자결재여부)
                    e.Cancel = true;
                
                Dass.FlexGrid.FlexGrid flexGrid = sender as Dass.FlexGrid.FlexGrid;
                
                if (flexGrid.Name == this._flex발주품목.Name)
                {
                    if (this.m_sEnv.Equals("N") && this._flex발주품목.Cols[e.Col].Name == "QT_PO")
                        e.Cancel = true;
                    
                    if (D.GetString(this._flex발주품목["TP_UM_TAX"]) == "001")
                    {
                        if (this._flex발주품목.Cols[e.Col].Name == "AM" || this._flex발주품목.Cols[e.Col].Name == "AM_EX" || this._flex발주품목.Cols[e.Col].Name == "VAT")
                            e.Cancel = true;
                    }
                    else if (this._flex발주품목.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true;
                    
                    if ((this._flex발주품목.Cols[e.Col].Name == "UM_EX_PO" || this._flex발주품목.Cols[e.Col].Name == "AM_EX" || this._flex발주품목.Cols[e.Col].Name == "AM") && this._header.CurrentRow["PO_PRICE"].ToString() == "Y")
                    {
                        this.ShowMessage("구매 단가 통제된 구매그룹 입니다.");
                        e.Cancel = true;
                    }
                    
                    switch (this._flex발주품목.Cols[e.Col].Name)
                    {
                        case "AM":
                            if (D.GetString(this._header.CurrentRow["CD_EXCH"]) == "000" || this._m_tppo_change == "001")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_ITEM":
                            if (this._flex발주품목.DataTable.Columns.Contains("APP_PJT") && D.GetString(this._flex발주품목["APP_PJT"]) == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "QT_PO_MM":
                        case "UM_EX_PO":
                        case "AM_EX":
                        case "AM_TOTAL":
                        case "QT_PO":
                            if (this._m_tppo_change == "001" && D.GetString(flexGrid["NO_APP"]) != string.Empty)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "FG_TAX":
                            if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "TP_UM_TAX":
                            if (this.의제매입여부(D.GetString(this._flex발주품목["FG_TAX"])) && this.s_vat_fictitious == "100")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "YN_BUDGET":
                        case "CD_BUDGET":
                        case "CD_BIZPLAN":
                        case "CD_BGACCT":
                        case "CD_ACCT":
                            if (this._flex발주품목.RowState() != DataRowState.Added || D.GetString(this._flex발주품목["YN_BUDGET_PR"]) == "Y" || D.GetString(this._flex발주품목["YN_BUDGET_APP"]) == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "NO_CBS":
                            if (D.GetDecimal(this._flex발주품목["NO_LINE_PJTBOM"]) > 0)
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                    }
                }
                
                if (this.bStandard && D.GetDecimal(flexGrid["UM_WEIGHT"]) > 0 && (flexGrid.Cols[e.Col].Name == "UM_EX_PO" || flexGrid.Cols[e.Col].Name == "UM_EX" || flexGrid.Cols[e.Col].Name == "AM_EX" || flexGrid.Cols[e.Col].Name == "AM"))
                {
                    e.Cancel = true;
                }
                else
                {
                    if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") == "100" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty))
                    {
                        if (flexGrid.Cols[e.Col].Name == "UM_EX_PO" || flexGrid.Cols[e.Col].Name == "AM_EX" || flexGrid.Cols[e.Col].Name == "AM" || flexGrid.Cols[e.Col].Name == "AM_TOTAL")
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2" && D.GetString(this._flex발주품목["CD_UNIT_MM"]) != string.Empty)
                    {
                        string str = "발주단위";
                        DataRow codeInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                               "MA_B000004",
                                                                                                                               D.GetString(this._flex발주품목["CD_UNIT_MM"]) });
                        if (codeInfo != null && D.GetString(codeInfo["CD_FLAG1"]) == "2")
                            str = "재고단위";
                        
                        if (str == "발주단위")
                        {
                            if (flexGrid.Cols[e.Col].Name == "UM" || flexGrid.Cols[e.Col].Name == "UM_EX")
                                e.Cancel = true;
                        }
                        else if (flexGrid.Cols[e.Col].Name == "UM_EX_PO")
                            e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex발주품목_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid flexGrid = sender as Dass.FlexGrid.FlexGrid;
                
                if (flexGrid == null)
                    return;
                
                if (e.StartEditCancel)
                    e.Cancel = true;
                
                else if (flexGrid.Name == this._flex발주품목.Name)
                {
                    switch (this._flex발주품목.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (D.GetString(this._flex발주품목["FG_POST"]) != "O" || this._m_tppo_change == "001" && D.GetString(flexGrid["NO_APP"]) != string.Empty)
                            {
                                e.Cancel = true;
                                break;
                            }
                            
                            if (this._flex발주품목.DataTable.Columns.Contains("APP_PJT") && D.GetString(this._flex발주품목["APP_PJT"]) == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flex발주품목["CD_PLANT"].ToString();
                            break;
                        case "CD_PURGRP":
                            e.Parameter.P92_DETAIL_SEARCH_CODE = e.EditValue;
                            break;
                        case "CD_SL":
                        case "CDSL_USERDEF1":
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flex발주품목["CD_PLANT"].ToString();
                            break;
                        case "GI_PARTNER":
                            e.Parameter.P14_CD_PARTNER = this.ctx매입처.CodeValue;
                            e.Parameter.P61_CODE1 = "003";
                            break;
                        case "NM_CLS_L":
                        case "CLS_L":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000030";
                            break;
                        case "NM_CLS_M":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000031";
                            break;
                        case "NM_CLS_S":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000032";
                            break;
                        case "CD_BIZPLAN":
                            if (D.GetString(this._flex발주품목["CD_BUDGET"]) == string.Empty)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("예산단위") });
                                e.Cancel = true;
                                return;
                            }

                            e.Parameter.P61_CODE1 = D.GetString(this._flex발주품목["CD_BUDGET"]);
                            break;
                        case "CD_BGACCT":
                            if (this._YN_CdBizplan == "1")
                            {
                                e.Parameter.UserParams = "예산계정;H_FI_BUDGETACCTJO_SUB6;" + string.Empty + ";" + D.GetString(this._flex발주품목["CD_BUDGET"]) + "|;" + D.GetString(this._flex발주품목["CD_BIZPLAN"]) + "|;1;" + string.Empty + ";";
                                break;
                            }
                            break;
                        case "CD_ACCT":
                            e.Parameter.P62_CODE2 = "Y";
                            
                            if (D.GetString(this._flex발주품목[e.Row, "CD_BGACCT"]) == string.Empty)
                            {
                                Global.MainFrame.ShowMessage("예산계정을 먼저 입력하세요.");
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case "CD_PJT_ITEM":
                            if (D.GetString(this._flex발주품목["CD_PJT"]) != string.Empty)
                            {
                                e.Parameter.P64_CODE4 = D.GetString(this._flex발주품목["CD_PJT"]);
                                break;
                            }
                            break;
                        case "NO_CBS":
                            if (D.GetString(this._flex발주품목["CD_PJT"]) == string.Empty)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("프로젝트") });
                                e.Cancel = true;
                            }

                            e.Parameter.P61_CODE1 = "|";
                            e.Parameter.UserParams = D.GetString(this._flex발주품목["CD_PJT"]) + "|";
                            break;
                        case "CD_ITEM_ORIGIN":
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flex발주품목["CD_PLANT"].ToString();
                            break;
                    }
                    if (BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001"))
                    {
                        switch (this._flex발주품목.Cols[e.Col].Name)
                        {
                            case "NM_CLS_M":
                                if (D.GetString(this._flex발주품목["CLS_L"]) == string.Empty)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("대분류코드") });
                                    e.Cancel = true;
                                    break;
                                }

                                e.Parameter.P42_CD_FIELD2 = D.GetString(this._flex발주품목["CLS_L"]);
                                break;
                            case "NM_CLS_S":
                                if (D.GetString(this._flex발주품목["CLS_M"]) == string.Empty)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("중분류코드") });
                                    e.Cancel = true;
                                    break;
                                }

                                e.Parameter.P42_CD_FIELD2 = D.GetString(this._flex발주품목["CLS_M"]);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex발주품목_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid flexGrid = sender as Dass.FlexGrid.FlexGrid;
                HelpReturn result = e.Result;
                int index1 = 0;

                if (flexGrid.Name == this._flex발주품목.Name)
                {
                    switch (this._flex발주품목.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (e.Result.DialogResult == DialogResult.Cancel)
                            {
                                this._flex발주품목[e.Row, "CD_ITEM"] = string.Empty;
                                break;
                            }

                            bool flag1 = true;
                            bool flag2 = false;
                            StringBuilder stringBuilder = new StringBuilder();
                            string str1 = "품목코드\t 품목명\t";
                            stringBuilder.AppendLine(str1);
                            string str2 = "-".PadRight(80, '-');
                            stringBuilder.AppendLine(str2);
                            this._flex발주품목.Redraw = false;
                            this._flex발주품목.SetDummyColumnAll();
                            string str3 = !(this._flex발주품목[this._flex발주품목.Rows.Count - 1, "DT_LIMIT"].ToString() != string.Empty) ? Global.MainFrame.GetStringToday : this._flex발주품목[this._flex발주품목.Rows.Count - 1, "DT_LIMIT"].ToString();
                            
                            foreach (DataRow row1 in result.Rows)
                            {
                                int row2;

                                if (flag1)
                                {
                                    if (this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y" && D.GetString(row1["FG_TAX_PU"]) != D.GetString(this.cbo과세구분.SelectedValue))
                                    {
                                        this._flex발주품목[e.Row, "CD_ITEM"] = string.Empty;
                                        string str4 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row1["NM_ITEM"].ToString().PadRight(15, ' ');
                                        stringBuilder.AppendLine(str4);
                                        flag2 = true;
                                        continue;
                                    }

                                    row2 = e.Row;
                                    this._flex발주품목[e.Row, "CD_ITEM"] = row1["CD_ITEM"];
                                    this._flex발주품목[e.Row, "NM_ITEM"] = row1["NM_ITEM"];
                                    this._flex발주품목[e.Row, "STND_ITEM"] = row1["STND_ITEM"];
                                    this._flex발주품목[e.Row, "UNIT_IM"] = row1["UNIT_IM"];
                                    this._flex발주품목[e.Row, "CD_UNIT_MM"] = row1["UNIT_PO"];
                                    this._flex발주품목[e.Row, "NM_CLS_ITEM"] = row1["CLS_ITEMNM"];
                                    this._flex발주품목[e.Row, "FG_SERNO"] = row1["FG_SERNO"];
                                    this._flex발주품목[e.Row, "TP_PART"] = row1["TP_PART"];

                                    if (this.txt발주번호.Text != string.Empty)
                                        this._flex발주품목[e.Row, "NO_PO"] = this.txt발주번호.Text;
                                    
                                    if (this._flex발주품목[e.Row, "DT_LIMIT"].ToString() == string.Empty)
                                        this._flex발주품목[e.Row, "DT_LIMIT"] = str3;
                                    
                                    if (this._flex발주품목[e.Row, "DT_PLAN"].ToString() == string.Empty)
                                        this._flex발주품목["DT_PLAN"] = this._flex발주품목["DT_LIMIT"];

                                    flag1 = false;
                                }
                                else
                                {
                                    if (this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y" && D.GetString(row1["FG_TAX_PU"]) != D.GetString(this.cbo과세구분.SelectedValue))
                                    {
                                        string str4 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row1["NM_ITEM"].ToString().PadRight(15, ' ');
                                        stringBuilder.AppendLine(str4);
                                        flag2 = true;
                                        continue;
                                    }

                                    this._flex발주품목.Rows.Add();
                                    this._flex발주품목.Row = this._flex발주품목.Rows.Count - 1;
                                    this._flex발주품목["CD_ITEM"] = row1["CD_ITEM"];
                                    this._flex발주품목["NM_ITEM"] = row1["NM_ITEM"];
                                    this._flex발주품목["STND_ITEM"] = row1["STND_ITEM"];
                                    this._flex발주품목["UNIT_IM"] = row1["UNIT_IM"];
                                    this._flex발주품목["CD_UNIT_MM"] = row1["UNIT_PO"];
                                    this._flex발주품목["NM_CLS_ITEM"] = row1["CLS_ITEMNM"];
                                    
                                    if (this.txt발주번호.Text != string.Empty)
                                        this._flex발주품목["NO_PO"] = this.txt발주번호.Text;
                                    
                                    this._flex발주품목["NO_LINE"] = this.최대차수 + 1;
                                    row2 = this._flex발주품목.Row;
                                    this._flex발주품목[row2, "CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
                                    this._flex발주품목[row2, "CD_PJT"] = this.ctx프로젝트.CodeValue;
                                    this._flex발주품목[row2, "NM_PJT"] = this.ctx프로젝트.CodeName;
                                    this._flex발주품목[row2, "DT_LIMIT"] = str3;
                                    
                                    if (this._flex발주품목[row2, "DT_PLAN"].ToString() == string.Empty)
                                        this._flex발주품목[row2, "DT_PLAN"] = this._flex발주품목[row2, "DT_LIMIT"];
                                    
                                    this._flex발주품목["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flex발주품목["FG_POST"].ToString());
                                    
                                    this._flex발주품목["TP_PART"] = row1["TP_PART"];
                                }

                                this._flex발주품목[row2, "RT_PO"] = row1["UNIT_PO_FACT"];
                                this._flex발주품목[row2, "TP_UM_TAX"] = "002";
                                object[] m_obj = new object[] { row1["CD_ITEM"],
                                                                Global.MainFrame.LoginInfo.CdPlant,
                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                "001",
                                                                this.cbo통화.SelectedValue.ToString(),
                                                                this.dtp발주일자.Text,
                                                                this.ctx매입처.CodeValue,
                                                                this.ctx구매그룹.CodeValue,
                                                                "N",
                                                                !(D.GetString(this._flex발주품목[e.Row, "CD_PJT"]) != string.Empty) ?  this.ctx프로젝트.CodeValue : this._flex발주품목[e.Row, "CD_PJT"],
                                                                Global.MainFrame.ServerKeyCommon.ToUpper() };
                                
                                this._flex발주품목[row2, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                this._flex발주품목[row2, "FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                this._flex발주품목[row2, "YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                                this._flex발주품목[row2, "YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                                this._flex발주품목[row2, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                                this._flex발주품목[row2, "YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                                this._flex발주품목[row2, "YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                                this._flex발주품목[row2, "YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                                this._flex발주품목[row2, "FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                                this._flex발주품목[row2, "YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                                this._flex발주품목[row2, "FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                this._flex발주품목[row2, "NO_PR"] = string.Empty;

                                this._flex발주품목[row2, "CD_SL"] = row1["CD_SL"];
                                this._flex발주품목[row2, "NM_SL"] = row1["NM_SL"];
                                
                                this._flex발주품목[row2, "PI_PARTNER"] = row1["PARTNER"];
                                this._flex발주품목[row2, "PI_LN_PARTNER"] = row1["LN_PARTNER"];
                                this._flex발주품목[row2, "CD_EXCH"] = this.cbo통화.SelectedValue.ToString();
                                
                                foreach (DataRow row3 in ((DataTable)this.cbo통화.DataSource).Rows)
                                {
                                    if (row3["CODE"].ToString() == this.cbo통화.SelectedValue.ToString())
                                    {
                                        this._flex발주품목[row2, "NM_EXCH"] = row3["NAME"];
                                        break;
                                    }
                                }

                                this.SetCC(row2, string.Empty);
                                this._flex발주품목.Redraw = true;
                                this.금액계산(row2, D.GetDecimal(this._flex발주품목[row2, "UM_EX_PO"]), D.GetDecimal(this._flex발주품목[row2, "QT_PO_MM"]), string.Empty, 0);
                                
                                if (this.bStandard)
                                {
                                    this._flex발주품목[row2, "CLS_L"] = row1["CLS_L"];
                                    this._flex발주품목[row2, "CLS_M"] = row1["CLS_M"];
                                    this._flex발주품목[row2, "CLS_S"] = row1["CLS_S"];
                                    this._flex발주품목[row2, "NM_CLS_L"] = row1["CLS_LN"];
                                    this._flex발주품목[row2, "NM_CLS_M"] = row1["CLS_MN"];
                                    this._flex발주품목[row2, "NM_CLS_S"] = row1["CLS_SN"];
                                    this._flex발주품목[row2, "NUM_STND_ITEM_1"] = row1["NUM_STND_ITEM_1"];
                                    this._flex발주품목[row2, "NUM_STND_ITEM_2"] = row1["NUM_STND_ITEM_2"];
                                    this._flex발주품목[row2, "NUM_STND_ITEM_3"] = row1["NUM_STND_ITEM_3"];
                                    this._flex발주품목[row2, "NUM_STND_ITEM_4"] = row1["NUM_STND_ITEM_4"];
                                    this._flex발주품목[row2, "NUM_STND_ITEM_5"] = row1["NUM_STND_ITEM_5"];
                                }

                                this._flex발주품목.Redraw = true;
                                this._flex발주품목.AddFinished();
                                this._flex발주품목.Col = this._flex발주품목.Cols.Fixed;
                                this._flex발주품목.Select(row2, this._flex발주품목.Cols.Fixed);

                                if (this.sPUSU == "100")
                                    this.GET_SU_BOM();
                            }

                            if (flag2)
                            {
                                this.ShowDetailMessage("과세구분이 일치하지 않는 품목이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                                break;
                            }
                            break;
                        case "CD_SL":
                            DataTable dataTable1 = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this.dtp발주일자.Text.Substring(0, 4),
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       D.GetString(this._flex발주품목["CD_ITEM"]),
                                                                                       D.GetString(result.Rows[0]["CD_SL"]) });

                            if (dataTable1 != null && dataTable1.Rows.Count > 0)
                            {
                                this._flex발주품목["QT_INVC"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[0]["QT_INVC"]));
                                this._flex발주품목["QT_ATPC"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[0]["QT_ATPC"]));
                            }
                            
                            if (this.m_sEnv_CC_Menu == "100")
                            {
                                this._flex발주품목["CD_SL"] = D.GetString(result.Rows[0]["CD_SL"]);
                                this._flex발주품목["NM_SL"] = D.GetString(result.Rows[0]["NM_SL"]);
                                this.SetCC_Priority(this._flex발주품목.Row, null, null, null, null);
                                break;
                            }
                            break;
                        case "CD_PJT":
                            this._flex발주품목["CD_PJT"] = D.GetString(result.Rows[0]["NO_PROJECT"]);
                            this._flex발주품목["NM_PJT"] = D.GetString(result.Rows[0]["NM_PROJECT"]);
                            this._flex발주품목["SEQ_PROJECT"] = !(Config.MA_ENV.YN_UNIT == "Y") ? 0 : D.GetDecimal(result.Rows[0]["SEQ_PROJECT"]);
                            
                            if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(this._flex발주품목["CD_USERDEF14"]) == "001")
                            {
                                string str4 = this._biz.pjt_item_josun(D.GetString(this._flex발주품목["CD_PJT"]));
                                Decimal num2;
                                
                                if (str4 != string.Empty)
                                {
                                    this._flex발주품목[index1, "FG_TAX"] = str4;
                                    num2 = 0;
                                    this._flex발주품목[e.Row, "RATE_VAT"] = num2;
                                }
                                else
                                {
                                    this._flex발주품목["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                                    this._flex발주품목["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                    num2 = this.cur부가세율.DecimalValue;
                                }

                                Decimal num3 = num2 == 0 ? 0 : num2 / 100;
                                this._flex발주품목["VAT"] = !(num3 == 0) && !(Convert.ToDecimal(this._flex발주품목["AM"]) == 0) ? this.원화계산(Convert.ToDecimal(this._flex발주품목["AM"]) * num3) : 0;
                                this._flex발주품목["AM_TOTAL"] = this.원화계산(D.GetDecimal(this._flex발주품목["AM"]) + D.GetDecimal(this._flex발주품목["VAT"]));
                            }

                            if (this.m_sEnv_CC == "200")
                                this.SetCC(e.Row, string.Empty);
                            break;
                        case "CD_BUDGET":
                            this._flex발주품목["CD_BIZPLAN"] = string.Empty;
                            this._flex발주품목["NM_BIZPLAN"] = string.Empty;
                            this._flex발주품목["CD_BGACCT"] = string.Empty;
                            this._flex발주품목["NM_BGACCT"] = string.Empty;
                            this._flex발주품목["CD_ACCT"] = string.Empty;
                            this._flex발주품목["NM_ACCT"] = string.Empty;
                            break;
                        case "CD_BIZPLAN":
                            this._flex발주품목["CD_BGACCT"] = string.Empty;
                            this._flex발주품목["NM_BGACCT"] = string.Empty;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex발주품목.Redraw = true;
            }
        }

        private void _flex발주품목_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid flexGrid = sender as Dass.FlexGrid.FlexGrid;

                if (flexGrid.Name == this._flex발주품목.Name)
                {
                    string str1 = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                    string editData = ((Dass.FlexGrid.FlexGrid)sender).EditData;

                    if (str1.ToUpper() == editData.ToUpper())
                        return;
                    
                    string name = ((C1FlexGridBase)sender).Cols[e.Col].Name;
                    
                    if (Global.MainFrame.CurrentPageID != "P_PU_Z_JONGHAP_PO_REG2" && this._flex발주품목["CD_ITEM"].ToString() == string.Empty)
                        return;
                    
                    Decimal 부가세율 = new Decimal(1, 0, 0, false, (byte)1);
                    Decimal 부가세 = 0;
                    Decimal 원화금액 = 0;
                    Decimal 외화금액 = 0;
                    Decimal num1 = 0;
                    Decimal d = D.GetDecimal(this._flex발주품목[e.Row, "RATE_VAT"]) == 0 ? 0 : D.GetDecimal(this._flex발주품목[e.Row, "RATE_VAT"]) / 100;
                    Decimal 수량 = D.GetDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]);
                    Decimal num2 = D.GetDecimal(this._flex발주품목[e.Row, "UM_EX_PO"]);
                    Decimal 환율 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                    string str2 = D.GetString(this._flex발주품목[e.Row, "FG_TAX"]);
                    bool 부가세포함 = D.GetString(this._flex발주품목[e.Row, "TP_UM_TAX"]) == "001";

                    switch (this._flex발주품목.Cols[e.Col].Name)
                    {
                        case "QT_PO_MM":
                            if (this._flex발주품목.CDecimal(editData) < this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_REQ_MM"]))
                            {
                                this.ShowMessage(공통메세지._은_보다크거나같아야합니다, new string[] { this.DD("발주수량"),
                                                                                                       this.DD("입고수량") });

                                ((Dass.FlexGrid.FlexGrid)sender)["QT_PO_MM"] = ((C1FlexGridBase)sender).GetData(e.Row, e.Col);
                                break;
                            }

                            if (this._m_Company_only == "001")
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "QT_PO_MM");
                            
                            this.금액계산(e.Row, this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "UM_EX_PO"]), Convert.ToDecimal(editData), "QT_PO_MM", Convert.ToDecimal(editData));
                            
                            if (!this.bStandard)
                                this._flex발주품목[e.Row, "QT_WEIGHT"] = (this._flex발주품목.CDecimal(editData) * this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "WEIGHT"]));
                            
                            this.CalcRebate(D.GetDecimal(editData), D.GetDecimal(this._flex발주품목["UM_REBATE"]));
                            break;
                        case "UM_EX_PO":
                            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < this.원화계산(D.GetDecimal(editData) * this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]) * this.cur통화명.DecimalValue))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["UM_EX_PO"] = str1;
                                e.Cancel = true;
                                break;
                            }

                            if (this._m_Company_only == "001")
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "UM_EX_PO");
                            
                            this.금액계산(e.Row, Convert.ToDecimal(editData), this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]), "UM_EX_PO", Convert.ToDecimal(editData));
                            break;
                        case "UM_EX":
                            this.금액계산(e.Row, Convert.ToDecimal(editData), this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO"]), "UM_EX", Convert.ToDecimal(editData));
                            break;
                        case "AM_EX":
                            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < this.원화계산(D.GetDecimal(editData) * this.cur통화명.DecimalValue))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["AM_EX"] = str1;
                                e.Cancel = true;
                                break;
                            }

                            this.금액계산(e.Row, this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "UM_EX_PO"]), this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]), "AM_EX", this._flex발주품목.CDecimal(editData));
                            
                            if (this._m_Company_only == "001")
                            {
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "AM_EX");
                                break;
                            }
                            break;
                        case "AM":
                            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < D.GetDecimal(editData))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["AM"] = str1;
                                e.Cancel = true;
                                break;
                            }

                            if (Math.Abs(D.GetDecimal(this._flex발주품목["AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) - D.GetDecimal(editData)) > 500)
                            {
                                this.ShowMessage("500원 범위에서 수정 가능합니다.(단수차 관리)");
                                this._flex발주품목["AM"] = D.GetDecimal(str1);
                                e.Cancel = true;
                                break;
                            }

                            this.부가세만계산();
                            break;
                        case "VAT":
                            if (this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "RATE_VAT"]) == new Decimal(0, 0, 0, false, (byte)1) && this.cur부가세율.DecimalValue == 0)
                                this._flex발주품목[e.Row, "VAT"] = 0;
                            else
                                this.SUMFunction();
                            break;
                        case "DT_LIMIT":
                        case "DT_PLAN":
                            if (editData.Trim().Length != 8)
                            {
                                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                                
                                if (this._flex발주품목.Editor != null)
                                    this._flex발주품목.Editor.Text = string.Empty;
                                
                                e.Cancel = true;
                                break;
                            }

                            if (!this._flex발주품목.IsDate(name))
                            {
                                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                                
                                if (this._flex발주품목.Editor != null)
                                    this._flex발주품목.Editor.Text = string.Empty;
                                
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "FG_TAX":
                            num1 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(Math.Round(수량 * num2 * 환율), MidpointRounding.AwayFromZero) : this.원화계산(수량 * num2 * 환율);
                            
                            if (this._header.CurrentRow["FG_TRANS"].ToString() != "001" || this._flex발주품목["FG_TAX"].ToString() == string.Empty)
                            {
                                부가세율 = this.cur부가세율.DecimalValue;
                            }
                            else
                            {
                                if (this.의제매입여부(editData) && this.s_vat_fictitious == "100")
                                {
                                    부가세포함 = true;
                                    this._flex발주품목[e.Row, "TP_UM_TAX"] = "001";
                                }
                                else
                                    this._flex발주품목[e.Row, "TP_UM_TAX"] = "002";

                                if (this.의제매입여부(editData) && this.s_vat_fictitious == "000")
                                {
                                    부가세율 = 0;
                                }
                                else
                                {
                                    DataTable tableSearch = ComFunc.GetTableSearch("MA_CODEDTL", new object[] { this.LoginInfo.CompanyCode,
                                                                                                                "MA_B000046",
                                                                                                                editData });

                                    if (tableSearch.Rows.Count > 0 && tableSearch.Rows[0]["CD_FLAG1"].ToString() != string.Empty)
                                        부가세율 = Convert.ToDecimal(tableSearch.Rows[0]["CD_FLAG1"]);
                                }
                            }

                            this._flex발주품목[e.Row, "RATE_VAT"] = 부가세율;
                            
                            if (수량 == 0)
                                break;
                            
                            Duzon.ERPU.MF.Common.Calc.GetAmt(수량, num2, 환율, str2, 부가세율, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액, out 부가세);
                            this._flex발주품목[e.Row, "VAT"] = 부가세;
                            this._flex발주품목[e.Row, "AM"] = 원화금액;
                            this._flex발주품목[e.Row, "AM_EX"] = 외화금액;
                            this._flex발주품목[e.Row, "AM_TOTAL"] = Duzon.ERPU.MF.Common.Calc.합계금액(원화금액, 부가세);
                            this.SUMFunction();
                            break;
                        case "WEIGHT":
                            if (!this.bStandard)
                            {
                                this._flex발주품목[e.Row, "QT_WEIGHT"] = (this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]) * this._flex발주품목.CDecimal(editData));
                                break;
                            }
                            break;
                        case "UM_EX_AR":
                            this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "UM_EX_AR");
                            break;
                        case "TP_UM_TAX":
                            if (수량 == 0)
                                break;
                            
                            Decimal num4;
                            Decimal num5;
                            Decimal ldb_amEx1;
                            
                            if (부가세포함)
                            {
                                Decimal num3 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(수량 * num2 * 환율, MidpointRounding.AwayFromZero) : this.원화계산(수량 * num2 * 환율);
                                num4 = (!this.의제매입여부(D.GetString(this._flex발주품목["FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num3 / ++d * d, MidpointRounding.AwayFromZero) : this.원화계산(num3 / ++d * d)) : this.원화계산(num3 * d);
                                num5 = this.원화계산(num3 - num4);
                                ldb_amEx1 = this.외화계산(num5 / 환율);
                            }
                            else
                            {
                                num5 = this.원화계산(수량 * num2 * 환율);
                                num4 = this.원화계산(num5 * d);
                                num1 = this.원화계산(num5 + num4);
                                ldb_amEx1 = this.외화계산(num5 / 환율);
                            }
                            
                            this._flex발주품목[e.Row, "UM_EX_PO"] = this.외화계산(num2);
                            this._flex발주품목[e.Row, "UM_P"] = this.원화계산(num2 * 환율);
                            this._flex발주품목[e.Row, "UM_EX"] = this.외화계산(num2 / (D.GetDecimal(this._flex발주품목["RT_PO"]) == 0 ? 1 : D.GetDecimal(this._flex발주품목["RT_PO"])));
                            this._flex발주품목[e.Row, "UM"] = this.원화계산(num2 / (D.GetDecimal(this._flex발주품목["RT_PO"]) == 0 ? 1 : D.GetDecimal(this._flex발주품목["RT_PO"])) * 환율);
                            this._flex발주품목[e.Row, "AM_EX"] = ldb_amEx1;
                            this._flex발주품목[e.Row, "AM"] = num5;
                            this._flex발주품목[e.Row, "VAT"] = num4;
                            this._flex발주품목[e.Row, "AM_TOTAL"] = (num5 + num4);
                            this.SUMFunction();
                            break;
                        case "AM_TOTAL":
                            if (수량 == 0 || !부가세포함)
                                break;
                            
                            Decimal num6 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(D.GetDecimal(this._flex발주품목.EditData), MidpointRounding.AwayFromZero) : this.원화계산(D.GetDecimal(this._flex발주품목.EditData));
                            Decimal num7 = new Decimal(0, 0, 0, false, (byte)1);
                            Decimal num8 = (!this.의제매입여부(str2) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num6 / ++d * d, MidpointRounding.AwayFromZero) : this.원화계산(num6 / ++d * d)) : this.원화계산(num6 * d);
                            Decimal num9 = this.원화계산(num6 - num8);
                            Decimal ldb_amEx2 = this.외화계산(num9 / 환율);
                            this._flex발주품목[e.Row, "AM_EX"] = ldb_amEx2;
                            this._flex발주품목[e.Row, "AM"] = num9;
                            this._flex발주품목[e.Row, "VAT"] = num8;
                            Decimal val = num6 / 수량 / 환율;
                            this._flex발주품목[e.Row, "UM_EX_PO"] = this.외화계산(val);
                            this._flex발주품목[e.Row, "UM_P"] = this.원화계산(val * 환율);
                            this._flex발주품목[e.Row, "UM_EX"] = this.외화계산(UDecimal.Getdivision(val, D.GetDecimal(this._flex발주품목["RT_PO"]) == 0 ? 1 : D.GetDecimal(this._flex발주품목["RT_PO"])));
                            this._flex발주품목[e.Row, "UM"] = this.원화계산(D.GetDecimal(this._flex발주품목[e.Row, "UM_EX"]) * 환율);
                            this.SUMFunction();
                            break;
                        case "AM_EX_TRANS":
                            this._flex발주품목["AM_TRANS"] = this.원화계산(D.GetDecimal(editData) * D.GetDecimal(this.cur통화명.Text));
                            break;
                        case "AM_TRANS":
                            this._flex발주품목["AM_EX_TRANS"] = this.외화계산(UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this.cur통화명.Text)));
                            break;
                        case "UM_REBATE":
                            this.CalcRebate(D.GetDecimal(this._flex발주품목["QT_PO_MM"]), D.GetDecimal(editData));
                            break;
                        case "AM_REBATE_EX":
                            this._flex발주품목["UM_REBATE"] = this.외화계산(UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this._flex발주품목["QT_PO_MM"])));
                            this._flex발주품목["AM_REBATE"] = this.원화계산(D.GetDecimal(editData) * D.GetDecimal(this.cur통화명.Text));
                            break;
                        case "AM_REBATE":
                            this._flex발주품목["AM_REBATE_EX"] = this.외화계산(UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this.cur통화명.Text)));
                            this._flex발주품목["UM_REBATE"] = this.외화계산(UDecimal.Getdivision(D.GetDecimal(this._flex발주품목["AM_REBATE_EX"]), D.GetDecimal(this._flex발주품목["QT_PO_MM"])));
                            break;
                        case "CLS_L":
                        case "CLS_S":
                        case "NM_ITEMGRP":
                        case "NUM_STND_ITEM_1":
                        case "NUM_STND_ITEM_2":
                        case "NUM_STND_ITEM_3":
                            this.calcAM(e.Row);
                            break;
                        case "UM_WEIGHT":
                            this.calcAM(e.Row);
                            break;
                        case "TOT_WEIGHT":
                            this.calcAM(e.Row, D.GetDecimal(editData));
                            break;
                        case "NUM_USERDEF4_PO":
                            this.금액계산(e.Row, this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "UM_EX_PO"]), this._flex발주품목.CDecimal(this._flex발주품목[e.Row, "QT_PO_MM"]), "NUM_USERDEF4_PO", this._flex발주품목.CDecimal(editData));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex발주품목_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow || this._m_tppo_change == "001" || this._flex발주품목["FG_POST"].ToString().Trim() != "O" || (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_Z00000002") != "000" && (D.GetString(this._flex발주품목["NO_PR"]) != string.Empty || D.GetString(this._flex발주품목["NO_APP"]) != string.Empty)))
                    return;
                
                if (this._flex발주품목.Cols[this._flex발주품목.Col].Name == "UM_EX_PO" && Global.MainFrame.ServerKeyCommon != "LUKEN" || this._flex발주품목.Cols[this._flex발주품목.Col].Name == "NUM_USERDEF3_PO" && Global.MainFrame.ServerKeyCommon == "LUKEN")
                {
                    P_PU_UM_HISTORY_SUB pPuUmHistorySub = new P_PU_UM_HISTORY_SUB(new object[] { this.ctx매입처.CodeValue,
                                                                                                 this.ctx매입처.CodeName,
                                                                                                 this.ctx발주유형.CodeValue,
                                                                                                 this.ctx발주유형.CodeName,
                                                                                                 Global.MainFrame.LoginInfo.CdPlant,
                                                                                                 D.GetString(this._flex발주품목["CD_ITEM"]),
                                                                                                 D.GetString(this._flex발주품목["NM_ITEM"]),
                                                                                                 D.GetString(this._flex발주품목["CD_EXCH"]),
                                                                                                 string.Empty,
                                                                                                 string.Empty,
                                                                                                 string.Empty,
                                                                                                 string.Empty,
                                                                                                 string.Empty,
                                                                                                 D.GetString(this._flex발주품목["STND_ITEM"]) });

                    if (pPuUmHistorySub.ShowDialog() == DialogResult.OK)
                    {
                        Decimal subUm = pPuUmHistorySub.SUB_UM;
                        this.금액계산(this._flex발주품목.Row, subUm, D.GetDecimal(this._flex발주품목["QT_PO_MM"]), "UM_EX_PO", subUm);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex발주품목_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flex발주품목[e.Row, "NO_PO"]), D.GetDecimal(this._flex발주품목[e.Row, "NO_LINE"]), e.SettingValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void ControlButtonEnabledDisable(Control ctr, bool lb_enable)
        {
            if (ctr == null || ctr.Name == this._flex발주품목.Name)
            {
                this.btn추가.Enabled = lb_enable;
                this.btn삭제.Enabled = lb_enable;
                this.ctx프로젝트.Enabled = lb_enable;
                this.btn프로젝트적용.Enabled = lb_enable;
                this.ctx창고.Enabled = lb_enable;
                this.btn창고적용.Enabled = lb_enable;
            }
            else
            {
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;
                this.ctx담당자.Enabled = true;
                this.ctx프로젝트.Enabled = true;
                this.btn프로젝트적용.Enabled = true;
                this.ctx창고.Enabled = true;
                this.btn창고적용.Enabled = true;

                if (!(ctr is RoundedButton))
                    return;

                if (ctr.Name == this.btn추가.Name)
                {
                    this.btn추가.Enabled = lb_enable;

                    if (this._header.JobMode == JobModeEnum.추가후수정)
                        this._header.CurrentRow["TP_PROCESS"] = "2";
                }
            }
        }

        private void SetHeadControlEnabled(bool isEnabled, int pi_type)
        {
            this.dtp발주일자.Enabled = isEnabled;
            this.ctx매입처.Enabled = isEnabled;
            this.ctx구매그룹.Enabled = isEnabled;
            this.ctx발주유형.Enabled = isEnabled;
            this.ctx담당자.Enabled = isEnabled;
            this.cbo통화.Enabled = isEnabled;

            if (pi_type == 2 || pi_type == 3)
            {
                this.ctx프로젝트.Enabled = isEnabled;
                this.btn추가.Enabled = isEnabled;
                this.btn삭제.Enabled = isEnabled;
                this.btn프로젝트적용.Enabled = isEnabled;
                this.ctx창고.Enabled = isEnabled;
                this.btn창고적용.Enabled = isEnabled;

                if (pi_type == 3)
                    this.cur부가세율.Enabled = isEnabled;
            }

            switch (pi_type)
            {
                case 1:
                    this.cur부가세율.Enabled = isEnabled;
                    break;
                case 4:
                    this.btn프로젝트적용.Enabled = isEnabled;
                    break;
            }

            if (pi_type == 5 && this._m_partner_use == "100")
                this.ctx매입처.Enabled = true;
            
            if (pi_type != 6)
                return;
        }

        private void MA_EXC_SETTING()
        {
            this._구매예산CHK설정FI = ComFunc.전용코드("구매발주등록-예산체크사용유무(회계예산연동)");
            
            this._YN_CdBizplan = new 시스템환경설정().Get환경설정("TP_BUDGETMNG", false);
            
            this.m_sEnv = this._biz.EnvSearch();
            
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                this.m_sEnv = "Y";
            
            this.m_sEnv_CC = ComFunc.전용코드("발주등록-C/C설정");
            
            DataTable partnerCodeSearch = this._biz.GetPartnerCodeSearch();
            
            if (partnerCodeSearch.Rows.Count > 0 && (partnerCodeSearch.Rows[0]["CD_EXC"] != DBNull.Value && partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim() != string.Empty))
                this._전용설정 = partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim();
            
            if (this._전용설정 == "200")
            {
                this.btn추가.Visible = false;
            }
            
            this._m_partner_use = BASIC.GetMAEXC("구매요청_품의_거래처적용");
            this._m_tppo_change = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000006");
            
            this.m_sEnv_FG_TAX = ComFunc.전용코드("과세구분설정");
            BASICPU.CacheDataClear(BASICPU.CacheEnums.MA_MENU_CONTROL);
            
            this._m_dt = ComFunc.전용코드("발주등록-납품일자 통제");

            this.cur통화명.Size = new Size(100, 21);
            
            this._m_Company_only = BASIC.GetMAEXC("업체별프로세스");
            
            if (BASIC.GetMAEXC("리베이트사용여부") == "100")
                this._YN_REBATE = true;
            
            this._지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            this._지급예정일통제설정 = BASIC.GetMAEXC("매입등록-지급예정일통제설정");
            
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
                this.bStandard = true;
            
            if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
            {
                this.cur공급가액.CurrencyDecimalDigits = 4;
                this.cur부가세액.CurrencyDecimalDigits = 4;
            }
            
            if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000034") == "100" || BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000034") == "200")
            {
                this.btn추가.Visible = false;
                if (BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000034") == "200")
                    this.btn삭제.Visible = false;
            }
            
            DataTable dataTable = BASIC.MFG_AUTH("P_PU_PO_REG2");
            
            if (dataTable.Rows.Count > 0)
            {
                this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
                this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
            }
            
            this.m_sEnv_CC_Menu = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000052");
            this._m_pjtbom_rq_mng = BASIC.GetMAEXC("프로젝트BOM적용_잔량관리_사용유무");
        }

        private void 조회(string NO_PO, string BTN_TYPE)
        {
            DataSet dataSet = this._biz.Search(NO_PO);

            if (dataSet.Tables[0].Rows.Count < 1)
            {
                this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { NO_PO });
                this.OnToolBarAddButtonClicked(null, null);
            }
            else
            {
                this.str복사구분 = BTN_TYPE;
                this._flex발주품목.Binding = null;
                
                if (this.str복사구분 == "OK")
                {
                    this._header.SetDataTable(dataSet.Tables[0]);
                    
                    this.ControlButtonEnabledDisable((Control)this.btn추가, true);
                    
                    this._flex발주품목.Binding = dataSet.Tables[1];
                    
                    if (this._m_Company_only == "001")
                    {
                        int rowSel = this._flex발주품목.RowSel;
                        DataTable dt = dataSet.Tables[1].Clone();
                        
                        foreach (DataRow row in dataSet.Tables[1].Rows)
                        {
                            dt.ImportRow(row);
                            this.AsahiKasei_Only_Item(rowSel, dt);
                            dt.Clear();
                            ++rowSel;
                        }
                    }

                    this._flex발주품목.AcceptChanges();
                }
                else if (this.str복사구분 == "COPY")
                {
                    this._flex발주품목.Redraw = false;
                    this._header.SetDataTable(dataSet.Tables[0]);
                    this._header.CurrentRow["NO_PO"] = string.Empty;
                    this.txt발주번호.Text = string.Empty;
                    
                    this.SetHeadControlEnabled(true, 3);
                    this.ControlButtonEnabledDisable(null, true);
                    this._header.JobMode = JobModeEnum.추가후수정;
                    this._flex발주품목.IsDataChanged = true;
                    this.ToolBarDeleteButtonEnabled = false;
                    this._flex발주품목.Binding = new DataView(dataSet.Tables[1], string.Empty, "NO_LINE ASC", DataViewRowState.CurrentRows);
                    
                    for (int index = 0; index < this._flex발주품목.DataTable.Rows.Count; ++index)
                    {
                        this._flex발주품목.DataTable.Rows[index].SetAdded();
                        this._flex발주품목.Redraw = false;
                        this._flex발주품목.DataTable.Rows[index]["NO_APP"] = string.Empty;
                        this._flex발주품목.DataTable.Rows[index]["NO_APPLINE"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["NO_PR"] = string.Empty;
                        this._flex발주품목.DataTable.Rows[index]["NO_PRLINE"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["NO_RCV"] = string.Empty;
                        this._flex발주품목.DataTable.Rows[index]["NO_RCVLINE"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["QT_REQ_MM"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["QT_REQ"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["QT_RCV"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["QT_TR"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["QT_TR_MM"] = 0;
                        this._flex발주품목.DataTable.Rows[index]["FG_POST"] = "O";
                        this._flex발주품목.DataTable.Rows[index]["FG_POCON"] = "001";
                        this._flex발주품목.DataTable.Rows[index]["NO_SO"] = string.Empty;
                        this._flex발주품목.DataTable.Rows[index]["NO_SOLINE"] = 0;
                    }
                    
                    if (this.sPUSU == "100")
                    {
                        for (int row = 1; row < this._flex발주품목.Rows.Count; ++row)
                        {
                            this._flex발주품목.Select(row, 1);
                        }
                    }
                    
                    this._flex발주품목.Redraw = true;
                    this.SUMFunction();
                }

                this.Setting_pu_poh_sub();
                
                if (this.str복사구분 == "COPY")
                {
                    this.txt발주번호.Enabled = true;
                }
                else
                    this.txt발주번호.Enabled = false;
                
                if (!this.차수여부 || !this.전자결재여부)
                {
                    this.ctx프로젝트.Enabled = false;
                    this.btn프로젝트적용.Enabled = false;
                    this.m_pnlHeader_Enabled();
                    this.ctx창고.Enabled = false;
                    this.btn창고적용.Enabled = false;
                }
                else
                {
                    this.ctx프로젝트.Enabled = true;
                    this.btn프로젝트적용.Enabled = true;
                    this.one기본정보.Enabled = true;
                    this.ctx창고.Enabled = true;
                    this.btn창고적용.Enabled = true;
                }
            }
        }

        private bool HeaderCheck(int p_chk)
        {
            if (p_chk != 1 && this.MainFrameInterface.ServerKeyCommon != "INITECH" && this.ctx매입처.CodeValue == string.Empty)
            {
                this.ctx매입처.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl매입처.Text });
                return false;
            }

            if (this.dtp발주일자.Text == string.Empty)
            {
                this.dtp발주일자.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주일자.Text });
                return false;
            }

            if (!this.dtp발주일자.IsValidated)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                this.dtp발주일자.Focus();
                return false;
            }

            if (this.MainFrameInterface.ServerKeyCommon != "HCT" && this.MainFrameInterface.ServerKeyCommon != "DAEJOOKC" && this.ctx구매그룹.CodeValue == string.Empty)
            {
                this.ctx구매그룹.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl구매그룹.Text });
                return false;
            }

            if (this.ctx담당자.CodeValue == string.Empty)
            {
                this.ctx담당자.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
                return false;
            }

            if (this._m_tppo_change != "001" && this.MainFrameInterface.ServerKeyCommon != "DAEJOOKC" && this.ctx발주유형.CodeValue == string.Empty)
            {
                this.ctx발주유형.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주유형.Text });
                return false;
            }

            if (this.cbo통화.SelectedValue == null || this.cbo통화.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl통화명.Text });
                this.cbo통화.Focus();
                return false;
            }

            if (this.cur통화명.DecimalValue == 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl통화명.Text });
                this.cur통화명.Focus();
                return false;
            }

            DataTable dataTable = BASICPU.MA_MENU_CONTROL_VALUES(Global.MainFrame.CurrentPageID, Global.MainFrame.CurrentModule);
            
            if (!((dataTable == null || dataTable.Rows.Count <= 0 ? "N" : D.GetString(dataTable.Rows[0]["YN_DT_CONTROL"])) == "Y") || !(D.GetDecimal(this.dtp발주일자.Text) < D.GetDecimal(Global.MainFrame.GetStringToday)))
                return true;
            
            this.ShowMessage(공통메세지._보다커야합니다, new string[] { "현재일자" });
            this.dtp발주일자.Text = Global.MainFrame.GetStringToday;
            this.dtp발주일자.Focus();
            return false;
        }

        private void GetPU_RCV_Save(DataTable pdt_Head, DataTable pdt_Line)
        {
            this.cPU_RCVH = new CDT_PU_RCVH();

            for (int index1 = 0; index1 < pdt_Line.Rows.Count; ++index1)
            {
                if (pdt_Line.Rows[index1].RowState != DataRowState.Deleted && pdt_Line.Rows[index1]["NO_RCV"].ToString() == string.Empty)
                {
                    string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", this.dtp발주일자.Text.Substring(0, 6));
                    DataRow[] dataRowArray = pdt_Line.Select("CD_PLANT ='" + pdt_Line.Rows[index1]["CD_PLANT"].ToString() + "'");
                    DataRow row = this.cPU_RCVH.DT_PURCVH.NewRow();

                    row["NO_RCV"] = seq;
                    row["CD_PLANT"] = pdt_Line.Rows[index1]["CD_PLANT"];
                    row["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                    row["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];
                    row["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                    row["FG_TRANS"] = pdt_Head.Rows[0]["FG_TRANS"];
                    row["FG_PROCESS"] = pdt_Head.Rows[0]["FG_TAXP"];
                    row["YN_AM"] = pdt_Head.Rows[0]["YN_AM"];
                    row["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                    row["YN_RETURN"] = "N";
                    row["CD_SL"] = pdt_Line.Rows[index1]["CD_SL"];
                    row["YN_AUTORCV"] = pdt_Line.Rows[0]["YN_AUTORCV"];
                    row["DT_IO"] = pdt_Head.Rows[0]["DT_PO"];
                    row["CD_DEPT"] = pdt_Head.Rows[0]["CD_DEPT"];
                    row["FG_RCV"] = pdt_Head.Rows[0]["FG_TPRCV"];
                    
                    this.cPU_RCVH.DT_PURCVH.Rows.Add(row);
                    
                    for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
                    {
                        dataRowArray[index2]["NO_RCV"] = seq;
                        dataRowArray[index2]["NO_RCVLINE"] = (index2 + 1);
                        dataRowArray[index2]["FG_POCON"] = "002";
                    }
                }
            }

            if (this.cPU_RCVH.DT_PURCVH == null || this.cPU_RCVH.DT_PURCVH.Rows.Count <= 0)
                return;
            
            for (int index1 = 0; index1 < this.cPU_RCVH.DT_PURCVH.Rows.Count; ++index1)
            {
                DataTable dataTable = pdt_Line.Clone();
                DataRow[] dataRowArray = pdt_Line.Select("NO_RCV ='" + this.cPU_RCVH.DT_PURCVH.Rows[index1]["NO_RCV"].ToString() + "'");

                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
                        dataTable.ImportRow(dataRowArray[index2]);
                    this.cPU_RCVL = new CDT_PU_RCV();
                    
                    for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                    {
                        DataRow row1 = dataTable.Rows[index2];
                        DataRow row2 = this.cPU_RCVL.DT_PURCV.NewRow();

                        row2["NO_RCV"] = this.cPU_RCVH.DT_PURCVH.Rows[index1]["NO_RCV"];
                        row2["NO_LINE"] = (index2 + 1);
                        row2["DT_IO"] = this.cPU_RCVH.DT_PURCVH.Rows[index1]["DT_IO"];
                        row2["NO_PO"] = row1["NO_PO"];
                        row2["NO_POLINE"] = row1["NO_LINE"];
                        row2["CD_PURGRP"] = pdt_Head.Rows[0]["CD_PURGRP"];
                        row2["CD_ITEM"] = row1["CD_ITEM"];
                        row2["CD_UNIT_MM"] = row1["CD_UNIT_MM"];
                        row2["QT_REQ_MM"] = row1["QT_PO_MM"];
                        row2["QT_REQ"] = row1["QT_PO"];
                        row2["DT_LIMIT"] = row1["DT_LIMIT"];
                        row2["DT_PLAN"] = row1["DT_PLAN"];
                        row2["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                        row2["RT_EXCH"] = pdt_Head.Rows[0]["RT_EXCH"];
                        row2["YN_INSP"] = "N";
                        row2["UM_EX_PO"] = row1["UM_EX_PO"];
                        row2["UM_EX"] = row1["UM_EX"];
                        row2["AM_EX"] = row1["AM_EX"];
                        row2["AM"] = row1["AM"];
                        row2["AM_EXREQ"] = row1["AM_EX"];
                        row2["AM_REQ"] = row1["AM"];
                        row2["VAT"] = row1["VAT"];
                        row2["UM"] = row1["UM"];
                        row2["CD_PJT"] = row1["CD_PJT"];
                        row2["YN_RETURN"] = row1["YN_RETURN"];
                        row2["FG_TPPURCHASE"] = row1["FG_TPPURCHASE"];
                        row2["FG_TAXP"] = pdt_Head.Rows[0]["FG_TAXP"];
                        row2["FG_TAX"] = row1["FG_TAX"];
                        row2["FG_RCV"] = row1["FG_RCV"];
                        row2["FG_TRANS"] = row1["FG_TRANS"];
                        row2["YN_AUTORCV"] = row1["YN_AUTORCV"];
                        row2["YN_REQ"] = row1["YN_REQ"];
                        row2["CD_SL"] = row1["CD_SL"];
                        row2["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                        row2["CD_PLANT"] = row1["CD_PLANT"];
                        row2["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                        row2["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];

                        this.cPU_RCVL.DT_PURCV.Rows.Add(row2);
                    }
                }
            }
        }

        private void SetExchageMoney()
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                    return;
                
                foreach (DataRow row in this._flex발주품목.DataTable.Rows)
                {
                    Decimal num4 = 0;
                    string empty = string.Empty;
                    Decimal d = D.GetDecimal(row["RATE_VAT"]) == 0 ? 0 : D.GetDecimal(row["RATE_VAT"]) / 100;
                    Decimal num5 = D.GetDecimal(row["QT_PO_MM"]);
                    Decimal val = D.GetDecimal(row["UM_EX_PO"]);
                    Decimal num6 = D.GetDecimal(this.cur통화명.DecimalValue) == 0 ? 1 : D.GetDecimal(this.cur통화명.DecimalValue);
                    
                    if (num5 == 0)
                        return;
                    
                    Decimal num7;
                    Decimal num8;
                    Decimal num9;
                    
                    if (D.GetString(row["TP_UM_TAX"]) == "001")
                    {
                        Decimal num10 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num5 * val * num6, MidpointRounding.AwayFromZero) : this.원화계산(num5 * val * num6);
                        num7 = !(this.s_vat_fictitious == "100") && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num10 / ++d * d, MidpointRounding.AwayFromZero) : this.원화계산(num10 / ++d * d)) : this.원화계산(num10 * d);
                        num8 = this.원화계산(num10 - num7);
                        num9 = this.외화계산(num8 / num6);
                    }
                    else
                    {
                        num9 = this.외화계산(num5 * val);
                        num8 = this.원화계산(num5 * val * num6);
                        num7 = this.원화계산(num8 * d);
                        num4 = this.원화계산(num8 + num7);
                    }
                    
                    row["UM_EX_PO"] = this.외화계산(val);
                    row["UM_P"] = this.원화계산(val * num6);
                    row["UM_EX"] = this.외화계산(val / (D.GetDecimal(row["RT_PO"]) == 0 ? 1 : D.GetDecimal(row["RT_PO"])));
                    row["UM"] = this.원화계산(val / (D.GetDecimal(row["RT_PO"]) == 0 ? 1 : D.GetDecimal(row["RT_PO"])) * num6);
                    row["AM_EX"] = num9;
                    row["AM"] = num8;
                    row["VAT"] = num7;
                    row["AM_TOTAL"] = (num8 + num7);
                    
                    this.SUMFunction();
                }

                this._flex발주품목.SumRefresh();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            this.SUMFunction();
        }

        private void SetExchageApply()
        {
            Decimal num = 1;

            if (this._header.CurrentRow == null || this.cbo통화.SelectedValue == null || this._flex발주품목.HasNormalRow || this._header.CurrentRow.RowState == DataRowState.Unchanged)
                return;
            
            if (this.dtp발주일자.Text != string.Empty)
            {
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    num = MA.기준환율적용(this.dtp발주일자.Text, D.GetString(this.cbo통화.SelectedValue.ToString()));
                
                if (D.GetString(this.cbo통화.SelectedValue.ToString()) == "000")
                    num = 1;
            }
            
            this.cur통화명.Text = num.ToString();
            this._header.CurrentRow["RT_EXCH"] = num;
        }

        private void SUMFunction()
        {
            try
            {
                if (this._flex발주품목.HasNormalRow)
                {
                    this._header.CurrentRow["AM_EX"] = this.외화계산(D.GetDecimal(this._flex발주품목.DataTable.Compute("SUM(AM_EX)", string.Empty)));
                    this._header.CurrentRow["AM"] = this._flex발주품목.DataTable.Compute("SUM(AM)", string.Empty);
                    this._header.CurrentRow["VAT"] = this._flex발주품목.DataTable.Compute("SUM(VAT)", string.Empty);
                    
                    if (!this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                        return;
                    
                    this.cur공급가액.DecimalValue = D.GetDecimal(this._flex발주품목.DataTable.Compute("SUM(AM)", string.Empty));
                    this.cur부가세액.DecimalValue = D.GetDecimal(this._flex발주품목.DataTable.Compute("SUM(VAT)", string.Empty));
                    this._header.CurrentRow["AM_EX_IV"] = this.외화계산(D.GetDecimal(this._flex발주품목.DataTable.Compute("SUM(AM_EX)", string.Empty)));
                }
                else
                {
                    this._header.CurrentRow["AM_EX"] = 0;
                    this._header.CurrentRow["AM"] = 0;
                    this._header.CurrentRow["VAT"] = 0;
                    this.cur공급가액.DecimalValue = 0;
                    this.cur부가세액.DecimalValue = 0;
                    this._header.CurrentRow["AM_EX_IV"] = 0;
                }
            }
            catch
            {
                this._header.CurrentRow["AM_EX"] = 0;
                this._header.CurrentRow["AM"] = 0;
                this._header.CurrentRow["VAT"] = 0;
                this.cur공급가액.DecimalValue = 0;
                this.cur부가세액.DecimalValue = 0;
            }
        }

        private void GET_SU_BOM()
        {
            if (!this._flex발주품목.HasNormalRow)
                return;
            
            DataTable dataTable = new DataTable();
            string filter = "NO_POLINE = " + this._flex발주품목.CDecimal(this._flex발주품목["NO_LINE"]);
            DataTable suBom = this._biz.GET_SU_BOM(this._header.CurrentRow["CD_PLANT"].ToString(), this._header.CurrentRow["CD_PARTNER"].ToString(), this._flex발주품목["CD_ITEM"].ToString());
            Decimal num = 0;
            
            foreach (DataRow row in suBom.Rows)
            {
                row["QT_PO"] = this._flex발주품목.CDecimal(this._flex발주품목["QT_PO"]);
                row["NO_PO"] = this._flex발주품목["NO_PO"].ToString();
                row["NO_POLINE"] = this._flex발주품목.CDecimal(this._flex발주품목["NO_LINE"]);
                row["NO_LINE"] = ++num;
            }
            
            this._flex발주품목.DetailQueryNeed = false;
        }

        private void 기초값설정()
        {
            string str1 = this._header.CurrentRow["CD_PURGRP"].ToString();
            string str2 = this._header.CurrentRow["CD_TPPO"].ToString();
            this.cbo지급조건.SelectedValue = this._header.CurrentRow["FG_PAYMENT"].ToString();

            DataSet tppoPurgrp = this._biz.Get_TPPO_PURGRP(new object[] { this.LoginInfo.CompanyCode,
                                                                          str1,
                                                                          str2,
                                                                          Global.SystemLanguage.MultiLanguageLpoint.ToString() });
            
            if (tppoPurgrp.Tables[0].Rows.Count > 0)
            {
                DataRow row = tppoPurgrp.Tables[0].Rows[0];
                this._header.CurrentRow["CD_PURGRP"] = str1;
                this.ctx구매그룹.SetCode(str1, row["NM_PURGRP"].ToString());
                this._header.CurrentRow["PURGRP_NO_TEL"] = row["NO_TEL"].ToString();
                this._header.CurrentRow["PURGRP_NO_FAX"] = row["NO_FAX"].ToString();
                this._header.CurrentRow["PURGRP_E_MAIL"] = row["E_MAIL"].ToString();
                this._header.CurrentRow["PO_PRICE"] = row["PO_PRICE"].ToString();
                this.SetCC(0, str1);
            }
            
            if (tppoPurgrp.Tables[1].Rows.Count <= 0)
                return;

            DataTable ynSu = this._biz.GetYN_SU(D.GetString(this._header.CurrentRow["CD_TPPO"]));
            
            DataRow row1 = tppoPurgrp.Tables[1].Rows[0];
            
            if (this.sPUSU == "100" && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y" && (D.GetString(row1["FG_TRANS"]) != "004" && D.GetString(row1["FG_TRANS"]) != "005"))
            {
                this.ShowMessage(this.DD("국내인 외주발주가 있습니다. 발주유형을 확인하세요."));
                this._header.CurrentRow["CD_TPPO"] = string.Empty;
            }
            else
            {
                this._header.CurrentRow["CD_TPPO"] = str2;
                this.ctx발주유형.SetCode(str2, row1["NM_TPPO"].ToString());
                this._header.CurrentRow["FG_TRANS"] = row1["FG_TRANS"].ToString();
                this._header.CurrentRow["FG_TPRCV"] = row1["FG_TPRCV"].ToString();
                this._header.CurrentRow["FG_TPPURCHASE"] = row1["FG_TPPURCHASE"].ToString();
                this._header.CurrentRow["YN_AUTORCV"] = row1["YN_AUTORCV"].ToString();
                this._header.CurrentRow["YN_RCV"] = row1["YN_RCV"].ToString();
                this._header.CurrentRow["YN_RETURN"] = row1["YN_RETURN"].ToString();
                this._header.CurrentRow["YN_SUBCON"] = row1["YN_SUBCON"].ToString();
                this._header.CurrentRow["YN_IMPORT"] = row1["YN_IMPORT"].ToString();
                this._header.CurrentRow["YN_ORDER"] = row1["YN_ORDER"].ToString();
                this._header.CurrentRow["YN_REQ"] = row1["YN_REQ"].ToString();
                this._header.CurrentRow["YN_AM"] = row1["YN_AM"].ToString();
                this._header.CurrentRow["NM_TRANS"] = row1["NM_TRANS"].ToString();
                this._header.CurrentRow["FG_TAX"] = row1["FG_TAX"].ToString();
                this._header.CurrentRow["TP_GR"] = row1["TP_GR"].ToString();
                this._header.CurrentRow["CD_CC_TPPO"] = row1["CD_CC"].ToString();
                this._header.CurrentRow["NM_CC_TPPO"] = this._biz.GetCCCodeSearch(row1["CD_CC"].ToString());
                this.거래구분(row1["FG_TRANS"].ToString(), D.GetString(row1["FG_TAX"]));
                this.Setting_pu_poh_sub();
            }
        }

        private void 거래구분(string str거래구분, string strTax)
        {
            if (str거래구분 == "001")
            {
                this.cbo통화.SelectedValue = "000";
                this._header.CurrentRow["CD_EXCH"] = "000";
                this.cbo통화_SelectionChangeCommitted(null, null);
                this.cbo과세구분.Enabled = true;
                this.cbo과세구분.SelectedValue = this._header.CurrentRow["FG_TAX"];
                this.cbo과세구분_SelectionChangeCommitted(null, null);
            }
            else
            {
                this.cbo통화.SelectedValue = string.Empty;
                this._header.CurrentRow["CD_EXCH"] = string.Empty;
                this.cbo통화_SelectionChangeCommitted(null, null);
                this.cbo과세구분.Enabled = true;
                this.cbo과세구분.SelectedValue = this._header.CurrentRow["FG_TAX"];
                this.cbo과세구분_SelectionChangeCommitted(null, null);
            }
        }

        private void 부가세율(string ps_taxp)
        {
            if (this._flex발주품목.HasNormalRow)
                this.cur부가세율.Enabled = false;
            else
                this.cur부가세율.Enabled = true;

            if (ps_taxp == string.Empty)
            {
                this.cur부가세율.Enabled = false;
                this.cur부가세율.DecimalValue = 0;
                this._header.CurrentRow["VAT_RATE"] = 0;
            }
            else
            {
                if (this.의제매입여부(ps_taxp) && this.s_vat_fictitious == "100")
                {
                    this._header.CurrentRow["TP_UM_TAX"] = "001";
                }
                
                if (this.의제매입여부(ps_taxp) && this.s_vat_fictitious == "000")
                {
                    this.cur부가세율.Enabled = false;
                    this.cur부가세율.DecimalValue = 0;
                    this._header.CurrentRow["VAT_RATE"] = 0;
                }
                else
                {
                    DataRow[] dataRowArray = ((DataTable)this.cbo과세구분.DataSource).Select("CODE = '" + ps_taxp + "'");

                    if (dataRowArray != null && dataRowArray.Length > 0)
                    {
                        Decimal num = this._flex발주품목.CDecimal(dataRowArray[0]["CD_FLAG1"]);
                        this.cur부가세율.DecimalValue = num;
                        this._header.CurrentRow["VAT_RATE"] = num;
                    }
                    else
                    {
                        this.cur부가세율.Enabled = true;
                        this.cur부가세율.DecimalValue = 0;
                        this._header.CurrentRow["VAT_RATE"] = 0;
                    }
                }
            }
        }

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다:
                    return this.ShowMessage("PU_M000121");
                case 메세지.공장을먼저선택하십시오:
                    return this.ShowMessage("PU_M000070");
                case 메세지.삭제할수없습니다:
                    return this.ShowMessage("MA_M000094");
                default:
                    return DialogResult.None;
            }
        }

        public void SetCC(int row, string arg_cd_purgrp)
        {
            if (row == 0)
            {
                if (arg_cd_purgrp == string.Empty)
                {
                    this._header.CurrentRow["CD_CC_PURGRP"] = string.Empty;
                    this._header.CurrentRow["NM_CC_PURGRP"] = string.Empty;
                }
                else
                {
                    DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(arg_cd_purgrp);

                    if (cdCcCodeSearch == null || cdCcCodeSearch.Rows.Count <= 0)
                        return;
                    
                    this._header.CurrentRow["CD_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["CD_CC"].ToString();
                    this._header.CurrentRow["NM_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["NM_CC"].ToString();
                }
            }
            else if (this.m_sEnv_CC_Menu == "100")
                this.SetCC_Priority(row, (string)null, (string)null, (string)null, (string)null);
            else if (this.m_sEnv_CC == "100")
            {
                this._flex발주품목[row, "CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                this._flex발주품목[row, "NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
            }
            else if (this.m_sEnv_CC == "200" && D.GetString(this._flex발주품목[row, "CD_PJT"]) != string.Empty)
            {
                DataTable cdCcCodeSearchPjt = this._biz.GetCD_CC_CodeSearch_pjt(D.GetString(this._flex발주품목[row, "CD_PJT"]));

                if (cdCcCodeSearchPjt != null && cdCcCodeSearchPjt.Rows.Count > 0)
                {
                    this._flex발주품목[row, "CD_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["CD_CC"]);
                    this._flex발주품목[row, "NM_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["NM_CC"]);
                }
            }
            else if (this.m_sEnv_CC == "300" && D.GetString(this._flex발주품목[row, "CD_ITEM"]) != string.Empty)
            {
                DataTable codeSearchCdItem = this._biz.GetCD_CC_CodeSearch_cd_item(D.GetString(this._flex발주품목[row, "CD_ITEM"]), Global.MainFrame.LoginInfo.CdPlant);
                
                if (codeSearchCdItem != null && codeSearchCdItem.Rows.Count > 0)
                {
                    this._flex발주품목[row, "CD_CC"] = D.GetString(codeSearchCdItem.Rows[0]["CD_CC"]);
                    this._flex발주품목[row, "NM_CC"] = D.GetString(codeSearchCdItem.Rows[0]["NM_CC"]);
                }
            }
            else
            {
                this._flex발주품목[row, "CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                this._flex발주품목[row, "NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
            }
        }

        private void 금액계산(int row, Decimal 단가, Decimal 수량, string p_call, Decimal p_newValue)
        {
            this._flex발주품목.Row = row;
            Decimal num1 = new Decimal(1, 0, 0, false, (byte)1);
            Decimal num2 = 1;
            Decimal num3 = 0;
            Decimal num4 = 0;
            Decimal num5 = 0;
            
            if (D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]) == 0 && p_call != string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "발주수량" });
            }
            else
            {
                Decimal num8 = this._flex발주품목.CDecimal(this._flex발주품목[row, "RT_PO"]);
                
                if (num8 == 0)
                    num8 = 1;
                
                if (this.cur통화명.DecimalValue != 0)
                    num2 = this.cur통화명.DecimalValue;
                
                Decimal num9 = 단가;

                Decimal d;

                if (!(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flex발주품목[row, "FG_TAX"].ToString() == string.Empty))
                {
                    if (this._flex발주품목.CDecimal(this._flex발주품목[row, "RATE_VAT"]) == 0)
                        d = 0;
                    else
                        d = this._flex발주품목.CDecimal(this._flex발주품목[row, "RATE_VAT"]) / 100;
                }
                else
                    d = this.cur부가세율.DecimalValue / 100;

                if (p_call == "AM_EX")
                {
                    num5 = this.외화계산(p_newValue);
                    num4 = this.원화계산(p_newValue * num2);
                    num3 = this.원화계산(num4 * d);
                    this._flex발주품목[row, "UM_EX_PO"] = this.외화계산(num5 / 수량);
                    this._flex발주품목[row, "UM_EX"] = this.외화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "QT_PO"]) == 0 ? 0 : D.GetDecimal(this._flex발주품목[row, "AM_EX"]) / this._flex발주품목.CDecimal(this._flex발주품목[row, "QT_PO"]));
                    this._flex발주품목[row, "UM"] = this.원화계산(num5 / (수량 * num8) * num2);
                    this._flex발주품목[row, "UM_P"] = this.원화계산(num5 / 수량 * num2);
                }
                else
                {
                    if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM" && D.GetDecimal(this._flex발주품목[row, "UM_WEIGHT"]) != 0)
                    {
                        this.calcAM(row);
                        num9 = D.GetDecimal(this._flex발주품목[row, "UM_EX_PO"]);
                        단가 = num9;
                    }

                    if (D.GetString(this._flex발주품목[row, "TP_UM_TAX"]) == "001")
                    {
                        Decimal num10 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(수량 * 단가 * num2, MidpointRounding.AwayFromZero) : this.원화계산(수량 * 단가 * num2);
                        
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KFL")
                        {
                            num3 = Decimal.Ceiling(num10 / ++d * d);
                            num4 = Decimal.Ceiling(num10 - num3);
                        }
                        else
                        {
                            num3 = (!this.의제매입여부(D.GetString(this._flex발주품목[row, "FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num10 / ++d * d, MidpointRounding.AwayFromZero) : this.원화계산(num10 / ++d * d)) : this.원화계산(num10 * d);
                            num4 = this.원화계산(num10 - num3);
                        }

                        num5 = this.외화계산(num4 / num2);
                    }
                    else
                    {
                        num5 = this.외화계산(수량 * num9);
                        Decimal num10 = this.원화계산(num5 * num2);

                        num3 = this.원화계산(num10 * d);
                        num4 = this.원화계산(num10);
                    }

                    if (p_call == "UM_EX")
                    {
                        this._flex발주품목[row, "UM_EX"] = this.외화계산(단가);
                        this._flex발주품목[row, "UM_EX_PO"] = this.외화계산(단가 * num8);
                        this._flex발주품목[row, "UM"] = this.원화계산(단가 * num2);
                        this._flex발주품목[row, "UM_P"] = this.원화계산(단가 * num8 * num2);
                    }
                    else
                    {
                        this._flex발주품목[row, "UM_EX_PO"] = this.외화계산(단가);
                        this._flex발주품목[row, "UM_P"] = this.원화계산(단가 * num2);
                        this._flex발주품목[row, "UM_EX"] = this.외화계산(단가 / num8);
                        this._flex발주품목[row, "UM"] = this.원화계산(단가 / num8 * num2);
                    }
                }

                this._flex발주품목[row, "AM_EX"] = this.외화계산(num5);
                
                if (p_call != "UM_EX")
                    this._flex발주품목[row, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, 수량 * num8);
                
                this._flex발주품목[row, "AM"] = num4;
                this._flex발주품목[row, "VAT"] = num3;
                this._flex발주품목[row, "AM_TOTAL"] = (num4 + num3);
                
                this.SUMFunction();
            }
        }

        private void 부가세계산(DataRow row)
        {
            Decimal num4 = 0;
            string empty = string.Empty;
            Decimal d = D.GetDecimal(row["RATE_VAT"]) == 0 ? 0 : D.GetDecimal(row["RATE_VAT"]) / 100;
            Decimal num5 = D.GetDecimal(row["QT_PO_MM"]);
            Decimal val = D.GetDecimal(row["UM_EX_PO"]);
            Decimal num6 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
            
            if (num5 == 0)
                return;
            
            Decimal num7;
            Decimal num8;
            Decimal num9;
            
            if (D.GetString(row["TP_UM_TAX"]) == "001")
            {
                Decimal num10 = Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num5 * val * num6, MidpointRounding.AwayFromZero) : this.원화계산(num5 * val * num6);
                num7 = !(this.s_vat_fictitious == "100") && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? Decimal.Round(num10 / ++d * d, MidpointRounding.AwayFromZero) : this.원화계산(num10 / ++d * d)) : this.원화계산(num10 * d);
                num8 = this.원화계산(num10 - num7);
                num9 = this.외화계산(num8 / num6);
            }
            else
            {
                num9 = this.외화계산(num5 * val);
                num8 = this.원화계산(num5 * val * num6);
                num7 = this.원화계산(num8 * d);
                num4 = this.원화계산(num8 + num7);
            }
            
            row["UM_EX_PO"] = this.외화계산(val);
            row["UM_P"] = this.원화계산(val * num6);
            row["UM_EX"] = this.외화계산(val / (D.GetDecimal(row["RT_PO"]) == 0 ? 1 : D.GetDecimal(row["RT_PO"])));
            row["UM"] = this.원화계산(val / (D.GetDecimal(row["RT_PO"]) == 0 ? 1 : D.GetDecimal(row["RT_PO"])) * num6);
            row["AM_EX"] = num9;
            row["AM"] = num8;
            row["VAT"] = num7;
            row["AM_TOTAL"] = (num8 + num7);
            
            this.SUMFunction();
        }

        private void 부가세만계산()
        {
            Decimal num1 = D.GetDecimal(this._flex발주품목["RATE_VAT"]) == 0 ? 0 : D.GetDecimal(this._flex발주품목["RATE_VAT"]) / 100;
            Decimal num2 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
            this._flex발주품목["VAT"] = this.원화계산(D.GetDecimal(this._flex발주품목["AM"]) * num1);
            this._flex발주품목["AM_TOTAL"] = this.원화계산(D.GetDecimal(this._flex발주품목["AM"]) + D.GetDecimal(this._flex발주품목["VAT"]));
        }

        private void m_pnlHeader_Enabled()
        {
            this.txt발주번호.Enabled = false;
            this.dtp발주일자.Enabled = false;
            this.ctx매입처.Enabled = false;
            this.ctx구매그룹.Enabled = false;
            this.ctx담당자.Enabled = false;
            this.ctx발주유형.Enabled = false;
            this.cbo통화.Enabled = false;
            this.cur통화명.Enabled = false;
            this.ctx프로젝트.Enabled = false;

            this.txt오더번호.Enabled = false;
            
            this.ctx창고.Enabled = false;
            this.btn창고적용.Enabled = false;
        }

        public void CalcRebate(Decimal p_qt_mm, Decimal p_um_rebate)
        {
            if (!this._YN_REBATE)
                return;
            
            this._flex발주품목["AM_REBATE_EX"] = this.외화계산(p_um_rebate * p_qt_mm);
            this._flex발주품목["AM_REBATE"] = this.원화계산(D.GetDecimal(this._flex발주품목["AM_REBATE_EX"]) * D.GetDecimal(this.cur통화명.Text));
        }

        private void setNecessaryCondition(object[] obj, OneGrid _OneGrid, bool state)
        {
            try
            {
                List<Control> controlList = _OneGrid.GetControlList();
                
                for (int index1 = 0; index1 < controlList.Count; ++index1)
                {
                    if (controlList[index1].GetType().Name == "BpPanelControl")
                    {
                        BpPanelControl bpPanelControl = (BpPanelControl)controlList[index1];
                        
                        if (!state)
                        {
                            for (int index2 = 0; index2 < obj.Length; ++index2)
                            {
                                if (bpPanelControl.Name != D.GetString(obj[index2]))
                                {
                                    bpPanelControl.IsNecessaryCondition = !state;
                                }
                                else
                                {
                                    bpPanelControl.IsNecessaryCondition = state;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int index2 = 0; index2 < obj.Length; ++index2)
                            {
                                if (bpPanelControl.Name != D.GetString(obj[index2]))
                                {
                                    bpPanelControl.IsNecessaryCondition = state;
                                }
                                else
                                {
                                    bpPanelControl.IsNecessaryCondition = !state;
                                    break;
                                }
                            }
                        }

                        if (obj.Length == 0)
                            bpPanelControl.IsNecessaryCondition = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetCC_Priority(int row, string cd_cc, string nm_cc, string cd_pr_emp_cc, string nm_pr_emp_cc)
        {
            try
            {
                foreach (DataRow row1 in this._biz.GetCD_CC_Priority().Rows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("CD_CC", typeof(string));
                    dataTable.Columns.Add("NM_CC", typeof(string));
                    DataRow dataRow = dataTable.NewRow();

                    if (D.GetString(row1["CD_SYSDEF"]) == "001")
                    {
                        dataRow["CD_CC"] = D.GetString(this._header.CurrentRow["CD_CC_PURGRP"]);
                        dataRow["NM_CC"] = D.GetString(this._header.CurrentRow["NM_CC_PURGRP"]);
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "002")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "003")
                        dataTable = this._biz.GetCD_CC(Global.MainFrame.LoginInfo.CdPlant, D.GetString(this._flex발주품목[row, "CD_PJT"]), "CD_PJT");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "004")
                        dataTable = this._biz.GetCD_CC(Global.MainFrame.LoginInfo.CdPlant, D.GetString(this._flex발주품목[row, "CD_ITEM"]), "CD_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "005")
                    {
                        dataRow["CD_CC"] = cd_pr_emp_cc;
                        dataRow["NM_CC"] = nm_pr_emp_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "006")
                    {
                        dataRow["CD_CC"] = cd_cc;
                        dataRow["NM_CC"] = nm_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "007")
                        dataTable = this._biz.GetCD_CC(D.GetString(this._flex발주품목[row, "CD_PLANT"]), D.GetString(this._flex발주품목[row, "GRP_ITEM"]), "GRP_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "008")
                        dataTable = this._biz.GetCD_CC(D.GetString(this._flex발주품목[row, "CD_PLANT"]), D.GetString(this._flex발주품목[row, "CD_SL"]), "CD_SL");
                    
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        if (D.GetString(dataTable.Rows[0]["CD_CC"]) != string.Empty)
                        {
                            this._flex발주품목[row, "CD_CC"] = D.GetString(dataTable.Rows[0]["CD_CC"]);
                            this._flex발주품목[row, "NM_CC"] = D.GetString(dataTable.Rows[0]["NM_CC"]);
                            break;
                        }
                    }
                    else if (D.GetString(dataRow["CD_CC"]) != string.Empty)
                    {
                        this._flex발주품목[row, "CD_CC"] = D.GetString(dataRow["CD_CC"]);
                        this._flex발주품목[row, "NM_CC"] = D.GetString(dataRow["NM_CC"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetCC_Priority(DataRow row, string cd_cc, string nm_cc, string cd_pr_emp_cc, string nm_pr_emp_cc)
        {
            try
            {
                foreach (DataRow row1 in this._biz.GetCD_CC_Priority().Rows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("CD_CC", typeof(string));
                    dataTable.Columns.Add("NM_CC", typeof(string));
                    DataRow dataRow = dataTable.NewRow();
                    
                    if (D.GetString(row1["CD_SYSDEF"]) == "001")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "002")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "003")
                        dataTable = this._biz.GetCD_CC(Global.MainFrame.LoginInfo.CdPlant, D.GetString(row["CD_PJT"]), "CD_PJT");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "004")
                        dataTable = this._biz.GetCD_CC(Global.MainFrame.LoginInfo.CdPlant, D.GetString(row["CD_ITEM"]), "CD_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "005")
                    {
                        dataRow["CD_CC"] = cd_pr_emp_cc;
                        dataRow["NM_CC"] = nm_pr_emp_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "006")
                    {
                        dataRow["CD_CC"] = cd_cc;
                        dataRow["NM_CC"] = nm_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "007")
                        dataTable = this._biz.GetCD_CC(D.GetString(row["CD_PLANT"]), D.GetString(row["GRP_ITEM"]), "GRP_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "008")
                        dataTable = this._biz.GetCD_CC(D.GetString(row["CD_PLANT"]), D.GetString(row["CD_SL"]), "CD_SL");
                    
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        if (D.GetString(dataTable.Rows[0]["CD_CC"]) != string.Empty)
                        {
                            row["CD_CC"] = D.GetString(dataTable.Rows[0]["CD_CC"]);
                            row["NM_CC"] = D.GetString(dataTable.Rows[0]["NM_CC"]);
                            break;
                        }
                    }
                    else if (D.GetString(dataRow["CD_CC"]) != string.Empty)
                    {
                        row["CD_CC"] = D.GetString(dataRow["CD_CC"]);
                        row["NM_CC"] = D.GetString(dataRow["NM_CC"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable 예산chk(DataTable dt_tg)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CD_BUDGET", typeof(string));
            dataTable1.Columns.Add("NM_BUDGET", typeof(string));
            dataTable1.Columns.Add("CD_BGACCT", typeof(string));
            dataTable1.Columns.Add("NM_BGACCT", typeof(string));
            dataTable1.Columns.Add("CD_BIZPLAN", typeof(string));
            dataTable1.Columns.Add("NM_BIZPLAN", typeof(string));
            dataTable1.Columns.Add("AM_ACTSUM", typeof(decimal));
            dataTable1.Columns.Add("AM_JSUM", typeof(decimal));
            dataTable1.Columns.Add("RT_JSUM", typeof(decimal));
            dataTable1.Columns.Add("AM", typeof(decimal));
            dataTable1.Columns.Add("AM_JAN", typeof(decimal));
            dataTable1.Columns.Add("AM_ORG", typeof(decimal));
            dataTable1.Columns.Add("TP_BUNIT", typeof(string));
            dataTable1.Columns.Add("ERROR_MSG", typeof(string));

            for (int index = 0; index < dt_tg.Rows.Count; ++index)
            {
                if (dt_tg.Rows[index].RowState != DataRowState.Deleted && (!(D.GetString(dt_tg.Rows[index]["YN_BUDGET"]) != "Y") && !(D.GetString(dt_tg.Rows[index]["YN_BUDGET_PR"]) == "Y") && !(D.GetString(dt_tg.Rows[index]["YN_BUDGET_APP"]) == "Y") || !(this._구매예산CHK설정FI == "100")) && (dt_tg.Rows[index]["CD_BUDGET"] != null && !(dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim() == string.Empty) && dt_tg.Rows[index]["CD_BGACCT"] != null && !(dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim() == string.Empty) && (!(this._YN_CdBizplan == "1") || !(D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]) == string.Empty))))
                {
                    string empty = string.Empty;
                    string filterExpression = " CD_BUDGET = '" + dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim() + "' AND CD_BGACCT = '" + dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim() + "'";
                    
                    if (this._YN_CdBizplan == "1")
                        filterExpression = filterExpression + " AND CD_BIZPLAN = '" + D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]) + "'";
                    
                    DataRow[] dataRowArray = dataTable1.Select(filterExpression);
                    
                    if (dataRowArray.Length == 0)
                    {
                        DataRow row = dataTable1.NewRow();
                        row["CD_BUDGET"] = dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim();
                        row["NM_BUDGET"] = dt_tg.Rows[index]["NM_BUDGET"].ToString().Trim();
                        row["CD_BGACCT"] = dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim();
                        row["NM_BGACCT"] = dt_tg.Rows[index]["NM_BGACCT"].ToString().Trim();
                        row["CD_BIZPLAN"] = D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]);
                        row["NM_BIZPLAN"] = D.GetString(dt_tg.Rows[index]["NM_BIZPLAN"]);
                        row["AM"] = this._flex발주품목.CDecimal(dt_tg.Rows[index]["AM"]);
                        row["AM_ORG"] = D.GetDecimal(dt_tg.Rows[index]["AM_ORG"]);
                        dataTable1.Rows.Add(row);
                    }
                    else
                    {
                        dataRowArray[0]["AM"] = (this._flex발주품목.CDecimal(dataRowArray[0]["AM"]) + this._flex발주품목.CDecimal(this._flex발주품목.CDecimal(dt_tg.Rows[index]["AM"])));
                        dataRowArray[0]["AM_ORG"] = (D.GetDecimal(dataRowArray[0]["AM_ORG"]) + D.GetDecimal(dt_tg.Rows[index]["AM_ORG"]));
                    }
                }
            }

            foreach (DataRow row in dataTable1.Rows)
            {
                DataTable dataTable2 = !(this._구매예산CHK설정FI == "100") ? this._biz.CheckBUDGET(row["CD_BUDGET"].ToString().Trim(), row["CD_BGACCT"].ToString().Trim(), this.dtp발주일자.Text, string.Empty, "000") : this._biz.CheckBUDGET(row["CD_BUDGET"].ToString().Trim(), row["CD_BGACCT"].ToString().Trim(), this.dtp발주일자.Text, D.GetString(row["CD_BIZPLAN"]), "100");
                
                if (dataTable2.Rows.Count > 0)
                {
                    row["AM_ACTSUM"] = this._flex발주품목.CDecimal(dataTable2.Rows[0]["AM_ACTSUM"]);
                    row["AM_JSUM"] = this._flex발주품목.CDecimal(dataTable2.Rows[0]["AM_JSUM"]);
                    row["TP_BUNIT"] = dataTable2.Rows[0]["TP_BUNIT"].ToString().Trim();
                    
                    if (this._flex발주품목.CDecimal(row["AM_ACTSUM"]) != 0)
                        row["RT_JSUM"] = (this._flex발주품목.CDecimal(row["AM_JSUM"]) / this._flex발주품목.CDecimal(row["AM_ACTSUM"]) * 100);
                    
                    row["AM_JAN"] = (this._flex발주품목.CDecimal(row["AM_ACTSUM"]) - this._flex발주품목.CDecimal(row["AM_JSUM"]) - this._flex발주품목.CDecimal(row["AM"]) + D.GetDecimal(row["AM_ORG"]));
                    row["ERROR_MSG"] = dataTable2.Rows[0]["ERROR_MSG"].ToString().Trim();
                }
            }

            return dataTable1;
        }

        private bool Check()
        {
            string[] strArray = new string[3];

            if (this.m_sEnv_Nego == "100")
            {
                strArray[0] = "Nego금액";
                strArray[1] = "QT_PO_MM";
                strArray[2] = "발주수량";
            }
            else if (Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                strArray[0] = "할인금액";
                strArray[1] = "NUM_USERDEF4_PO";
                strArray[2] = "기준발주금액";
            }
            else
            {
                strArray[0] = "할인율";
                strArray[1] = "UM_EX_PO";
                strArray[2] = "단가";
            }

            if (Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select("S = 'Y'"))
                {
                    if (D.GetString(dataRow["FG_POST"]) != "O")
                    {
                        this.ShowMessage("발주 확정/종결 건은 처리할 수 없습니다");
                        return false;
                    }

                    if (D.GetDecimal(D.GetDecimal(dataRow[strArray[1]])) == 0)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD(strArray[2]) });
                        return false;
                    }
                }
            }
            else
            {
                for (int index = this._flex발주품목.Rows.Fixed; index < this._flex발주품목.Rows.Count; ++index)
                {
                    if (D.GetString(this._flex발주품목[index, "FG_POST"]) != "O")
                    {
                        this.ShowMessage("발주 확정/종결 건은 처리할 수 없습니다");
                        return false;
                    }

                    if (D.GetDecimal(D.GetDecimal(this._flex발주품목[index, strArray[1]])) == 0)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD(strArray[2]) });
                        return false;
                    }
                }
            }

            return true;
        }

        protected void AsahiKasei_Only_Item(int apply_row, DataTable dt)
        {
            this._flex발주품목[apply_row, "QT_LENGTH"] = D.GetDecimal(dt.Rows[0]["QT_LENGTH"]);
            this._flex발주품목[apply_row, "QT_WIDTH"] = (D.GetDecimal(dt.Rows[0]["QT_WIDTH"]) * 1000);
            this._flex발주품목[apply_row, "QT_AREA"] = (D.GetDecimal(dt.Rows[0]["QT_LENGTH"]) * D.GetDecimal(dt.Rows[0]["QT_WIDTH"]));
            this._flex발주품목[apply_row, "CD_TP"] = D.GetString(dt.Rows[0]["CD_TP"]);

            if (!(D.GetDecimal(this._flex발주품목[apply_row, "QT_PO_MM"]) > 0))
                return;
            
            this._flex발주품목[apply_row, "TOTAL_AREA"] = (D.GetDecimal(this._flex발주품목[apply_row, "QT_PO_MM"]) * D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]));
        }

        protected void AsahiKasei_Only_ValidateEdit(int apply_row, Decimal newvalue, string colname)
        {
            if (D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]) <= 0)
                return;
            
            switch (colname)
            {
                case "QT_PO_MM":
                    this._flex발주품목[apply_row, "TOTAL_AREA"] = (newvalue * D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]));
                    break;
                case "UM_EX_PO":
                    this._flex발주품목[apply_row, "UM_EX_AR"] = (newvalue / D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]));
                    break;
                case "AM_EX":
                    this._flex발주품목[apply_row, "UM_EX_AR"] = (D.GetDecimal(this._flex발주품목[apply_row, "UM_EX_PO"]) / D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]));
                    break;
                case "UM_EX_AR":
                    this._flex발주품목[apply_row, "UM_EX_AR"] = newvalue;
                    this._flex발주품목[apply_row, "UM_EX_PO"] = (newvalue * D.GetDecimal(this._flex발주품목[apply_row, "QT_AREA"]));
                    this.금액계산(apply_row, D.GetDecimal(this._flex발주품목[apply_row, "UM_EX_PO"]), D.GetDecimal(this._flex발주품목[apply_row, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(this._flex발주품목[apply_row, "UM_EX_PO"]));
                    break;
            }
        }

        private void FillPol()
        {
            if (!this._flex발주품목.HasNormalRow)
                return;
            
            for (int index = 0; index < this._flex발주품목.DataTable.Rows.Count; ++index)
            {
                this._flex발주품목.DataTable.Rows[index]["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                this._flex발주품목.DataTable.Rows[index]["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                this._flex발주품목.DataTable.Rows[index]["FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                this._flex발주품목.DataTable.Rows[index]["YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                this._flex발주품목.DataTable.Rows[index]["YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                this._flex발주품목.DataTable.Rows[index]["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                this._flex발주품목.DataTable.Rows[index]["YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                this._flex발주품목.DataTable.Rows[index]["YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                this._flex발주품목.DataTable.Rows[index]["YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                this._flex발주품목.DataTable.Rows[index]["FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                this._flex발주품목.DataTable.Rows[index]["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                this._flex발주품목.DataTable.Rows[index]["FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            }
        }

        private void FillPol(int i)
        {
            if (!this._flex발주품목.HasNormalRow)
                return;
            
            this._flex발주품목[i, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
            this._flex발주품목[i, "FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            this._flex발주품목[i, "YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
            this._flex발주품목[i, "YN_RCV"] = this._header.CurrentRow["YN_RCV"];
            this._flex발주품목[i, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
            this._flex발주품목[i, "YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
            this._flex발주품목[i, "YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
            this._flex발주품목[i, "YN_REQ"] = this._header.CurrentRow["YN_REQ"];
            this._flex발주품목[i, "FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
            this._flex발주품목[i, "YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
            this._flex발주품목[i, "FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            this._flex발주품목[i, "FG_TAX"] = this._header.CurrentRow["FG_TAX"];
            this._flex발주품목[i, "RATE_VAT"] = this._header.CurrentRow["VAT_RATE"];
            this._flex발주품목[i, "TP_UM_TAX"] = this._header.CurrentRow["TP_UM_TAX"];
        }

        private bool 의제매입여부(string ps_taxp)
        {
            return ps_taxp == "27" || ps_taxp == "28" || (ps_taxp == "29" || ps_taxp == "30") || (ps_taxp == "32" || ps_taxp == "33" || (ps_taxp == "34" || ps_taxp == "35")) || (ps_taxp == "36" || ps_taxp == "40" || (ps_taxp == "41" || ps_taxp == "42") || (ps_taxp == "48" || ps_taxp == "49" || (ps_taxp == "51" || ps_taxp == "52"))) || (ps_taxp == "53" || ps_taxp == "56" || (ps_taxp == "57" || ps_taxp == "58")) || ps_taxp == "59";
        }

        private bool 확정여부()
        {
            try
            {
                if (!this._flex발주품목.HasNormalRow)
                    return false;
                
                foreach (DataRow dataRow in this._flex발주품목.DataTable.Select())
                {
                    if (dataRow["FG_POST"].ToString().Trim() != "O" || !this.차수여부)
                    {
                        this.ShowMessage(this.DD("발주 확정/종결 건은 처리할 수 없습니다"));
                        return false;
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                return false;
            }
        }

        public void Reload(string NO_PO)
        {
            try
            {
                DataSet dataSet = this._biz.Search("@#$%");
                this._header.SetBinding(dataSet.Tables[0], (Control)this.tce발주등록헤더);
                this._header.ClearAndNewRow();
                
                this._flex발주품목.Binding = dataSet.Tables[1];
                this.조회(NO_PO, "OK");
                
                if (!this._flex발주품목.HasNormalRow)
                    return;
                
                this._flex발주품목.Row = this._flex발주품목.FindRow(this.dNO_LINE, this._flex발주품목.Rows.Fixed, this._flex발주품목.Cols["NO_LINE"].Index, true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void MA_Pjt_Setting()
        {
            try
            {
                this.dt공장 = MA.GetCode("MA_B000093");
            }
            catch
            {
            }
        }

        private void Setting_pu_poh_sub()
        {
            if (this._header.CurrentRow["TP_GR"].ToString() == "104" && !this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
            {
                this.tce발주등록헤더.TabPages.Add(this.tpg매입정보);
            }
            else
            {
                if (this._header.CurrentRow["TP_GR"].ToString() == "104" || !this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                    return;
                
                this.tce발주등록헤더.TabPages.Remove(this.tpg매입정보);
            }
        }

        private void 원그리드적용하기()
        {
            Size size1 = this.one기본정보.Size;
            this.one기본정보.UseCustomLayout = true;
            this.one매입정보.UseCustomLayout = true;

            this.setNecessaryCondition(new object[] { this.bpPanelControl7.Name,
                                                      this.bpPanelControl8.Name,
                                                      this.bpPanelControl15.Name,
                                                      this.bpPanelControl21.Name,
                                                      this.bpPanelControl21.Name,
                                                      this.bpPanelControl22.Name }, this.one기본정보, false);
            
            this.setNecessaryCondition(new object[0], this.one매입정보, true);
            this.one기본정보.IsSearchControl = false;
            this.one매입정보.IsSearchControl = false;
            this.one기본정보.InitCustomLayout();
            this.one매입정보.InitCustomLayout();
            Size size2 = this.one기본정보.Size;
            Size size3 = this.tce발주등록헤더.Size;
            size3.Height += size2.Height - size1.Height;
            this.tce발주등록헤더.Size = size3;
        }

        private void calcAM(int row)
        {
            if (!this.bStandard || !(Global.MainFrame.ServerKey == "SINJINSM") || !(D.GetDecimal(this._flex발주품목[row, "UM_WEIGHT"]) != 0))
                return;
            
            switch (D.GetString(this._flex발주품목[row, "SG_TYPE"]))
            {
                case "100":
                case "200":
                case "400":
                    this._flex발주품목[row, "WEIGHT"] = (D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_1"]) * D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_2"]) * D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(this._flex발주품목[row, "QT_SG"]) / new Decimal(1000000));
                    break;
                case "300":
                    this._flex발주품목[row, "WEIGHT"] = ((D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_1"]) + D.GetDecimal("1.5")) * D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_2"]) * D.GetDecimal(this._flex발주품목[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(this._flex발주품목[row, "QT_SG"]) / new Decimal(1000000));
                    break;
            }
            
            this._flex발주품목[row, "TOT_WEIGHT"] = Math.Round(D.GetDecimal(this._flex발주품목[row, "WEIGHT"]) * D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]), 1);
            
            if (D.GetDecimal(this._flex발주품목[row, "TOT_WEIGHT"]) != 0)
            {
                Decimal val1 = this._flex발주품목.CDecimal(this._flex발주품목[row, "RT_PO"]) == 0 ? 1 : this._flex발주품목.CDecimal(this._flex발주품목[row, "RT_PO"]);
                this._flex발주품목[row, "AM_EX"] = this.외화계산(Math.Round(D.GetDecimal(this._flex발주품목[row, "TOT_WEIGHT"]) * D.GetDecimal(this._flex발주품목[row, "UM_WEIGHT"])));
                this._flex발주품목[row, "UM_EX"] = !(D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]) != 0) ? 0 : this.외화계산(UDecimal.Getdivision(D.GetDecimal(this._flex발주품목[row, "AM_EX"]) / D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]), val1));
                this._flex발주품목[row, "AM"] = this.원화계산(D.GetDecimal(this._flex발주품목[row, "AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flex발주품목[row, "UM_EX_PO"] = this.외화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) * val1);
                this._flex발주품목["UM_P"] = this.원화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flex발주품목["UM"] = this.원화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) / val1 * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
            }
            else
            {
                this._flex발주품목[row, "AM_EX"] = 0;
                this._flex발주품목[row, "UM_EX"] = 0;
                this._flex발주품목[row, "AM"] = 0;
                this._flex발주품목[row, "UM_EX_PO"] = 0;
                this._flex발주품목[row, "UM_P"] = 0;
                this._flex발주품목[row, "UM_"] = 0;
            }

            this.부가세만계산();
        }

        private void calcAM(int row, Decimal TOT_WEIGHT)
        {
            if (!this.bStandard || !(Global.MainFrame.ServerKey == "SINJINSM"))
                return;
            
            if (D.GetDecimal(this._flex발주품목[row, "UM_WEIGHT"]) != 0)
            {
                Decimal val1 = this._flex발주품목.CDecimal(this._flex발주품목[row, "RT_PO"]) == 0 ? 1 : this._flex발주품목.CDecimal(this._flex발주품목[row, "RT_PO"]);
                this._flex발주품목[row, "AM_EX"] = this.외화계산(Math.Round(TOT_WEIGHT * D.GetDecimal(this._flex발주품목[row, "UM_WEIGHT"])));
                this._flex발주품목[row, "UM_EX"] = !(D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]) != 0) ? 0 : this.외화계산(UDecimal.Getdivision(D.GetDecimal(this._flex발주품목[row, "AM_EX"]) / D.GetDecimal(this._flex발주품목[row, "QT_PO_MM"]), val1));
                this._flex발주품목[row, "AM"] = this.원화계산(D.GetDecimal(this._flex발주품목[row, "AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flex발주품목[row, "UM_EX_PO"] = this.외화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) * val1);
                this._flex발주품목["UM_P"] = this.원화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flex발주품목["UM"] = this.원화계산(this._flex발주품목.CDecimal(this._flex발주품목[row, "UM_EX"]) / val1 * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
            }
            
            this.부가세만계산();
        }

        private void SetTppoAfter(string strCDTPPO, string strNMTPPO)
        {
            try
            {
                this.ctx발주유형.CodeValue = strCDTPPO;
                this.ctx발주유형.CodeName = strNMTPPO;
                this._header.CurrentRow["CD_TPPO"] = strCDTPPO;

                if (!(strCDTPPO != string.Empty))
                    return;
                
                DataRow tppo = BASIC.GetTPPO(strCDTPPO);

                this._header.CurrentRow["FG_TRANS"] = tppo["FG_TRANS"];
                this._header.CurrentRow["FG_TPRCV"] = tppo["FG_TPRCV"];
                this._header.CurrentRow["FG_TPPURCHASE"] = tppo["FG_TPPURCHASE"];
                this._header.CurrentRow["YN_AUTORCV"] = tppo["YN_AUTORCV"];
                this._header.CurrentRow["YN_RCV"] = tppo["YN_RCV"];
                this._header.CurrentRow["YN_RETURN"] = tppo["YN_RETURN"];
                this._header.CurrentRow["YN_SUBCON"] = tppo["YN_SUBCON"];
                this._header.CurrentRow["YN_IMPORT"] = tppo["YN_IMPORT"];
                this._header.CurrentRow["YN_ORDER"] = tppo["YN_ORDER"];
                this._header.CurrentRow["YN_REQ"] = tppo["YN_REQ"];
                this._header.CurrentRow["YN_AM"] = tppo["YN_AM"];
                this._header.CurrentRow["NM_TRANS"] = tppo["NM_TRANS"];
                this._header.CurrentRow["FG_TAX"] = tppo["FG_TAX"];
                this._header.CurrentRow["TP_GR"] = tppo["TP_GR"];
                this._header.CurrentRow["CD_CC_TPPO"] = tppo["CD_CC"];
                this._header.CurrentRow["NM_CC_TPPO"] = tppo["NM_CC"];
                
                this.거래구분(D.GetString(this._header.CurrentRow["FG_TRANS"]), D.GetString(this._header.CurrentRow["FG_TAX"]));
                this.Setting_pu_poh_sub();
                
                if (this.tce발주등록헤더.TabPages.Contains(this.tpg매입정보))
                {
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;

                    this.ctx부가세사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
                    this.ctx부가세사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;

                    this.cbo지급구분.SelectedValue = "001";

                    if (this._header.CurrentRow["FG_TRANS"].ToString() == "001")
                    {
                        this.cbo전표유형.SelectedValue = "45";
                        this.cbo매입형태.SelectedValue = "001";
                    }
                    else
                    {
                        this.cbo전표유형.SelectedValue = "46";
                        this.cbo매입형태.SelectedValue = "002";
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetPurgrpAfter(string strCDPURGRP, string strNMPURGRP)
        {
            try
            {
                this.ctx구매그룹.CodeValue = strCDPURGRP;
                this.ctx구매그룹.CodeName = strNMPURGRP;
                this._header.CurrentRow["CD_PURGRP"] = strCDPURGRP;

                if (strCDPURGRP != string.Empty)
                {
                    DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(strCDPURGRP);
                    
                    if (cdCcCodeSearch != null && cdCcCodeSearch.Rows.Count > 0)
                    {
                        this._header.CurrentRow["PURGRP_NO_TEL"] = D.GetString(cdCcCodeSearch.Rows[0]["NO_TEL"]);
                        this._header.CurrentRow["PURGRP_NO_FAX"] = D.GetString(cdCcCodeSearch.Rows[0]["NO_FAX"]);
                        this._header.CurrentRow["PURGRP_E_MAIL"] = D.GetString(cdCcCodeSearch.Rows[0]["E_MAIL"]);
                    }
                    
                    this._header.CurrentRow["PO_PRICE"] = "N";
                    DataTable dataTable = Global.MainFrame.FillDataTable(@"SELECT O.PO_PRICE    
                                                                           FROM MA_PURGRP G WITH(NOLOCK)
                                                                           LEFT JOIN MA_PURORG O WITH(NOLOCK) ON G.CD_COMPANY = O.CD_COMPANY AND G.CD_PURORG  = O.CD_PURORG" + Environment.NewLine +
                                                                          "WHERE G.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                          "AND G.CD_PURGRP = '" + strCDPURGRP + "'");
                    
                    if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["PO_PRICE"] != DBNull.Value && dataTable.Rows[0]["PO_PRICE"].ToString().Trim() != string.Empty))
                        this._header.CurrentRow["PO_PRICE"] = dataTable.Rows[0]["PO_PRICE"].ToString().Trim();
                    
                    this.SetCC(0, strCDPURGRP);
                }
                else
                {
                    this._header.CurrentRow["PURGRP_NO_TEL"] = string.Empty;
                    this._header.CurrentRow["PURGRP_NO_FAX"] = string.Empty;
                    this._header.CurrentRow["PURGRP_E_MAIL"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
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

        private string 지급예정일계산(string 처리일자, decimal 원화금액, int dtPay)
        {
            try
            {
                string 지급예정일 = string.Empty;
                int 월말일;
                DateTime 월말일자, dt처리일자, 익월;

                dt처리일자 = DateTime.ParseExact(처리일자, "yyyyMMdd", null);

                if (dtPay > 0)
                {
                    switch (dtPay)
                    {
                        case 930:
                            #region 익월 말일 결제 (30일)
                            익월 = dt처리일자.AddMonths(1);
                            지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                            #endregion
                            break;
                        case 960:
                            #region 익익월 말일 결제 (60일)
                            DateTime 익익월 = dt처리일자.AddMonths(2);
                            지급예정일 = 익익월.ToString("yyyyMM") + DateTime.DaysInMonth(익익월.Year, 익익월.Month).ToString();
                            #endregion
                            break;
                        case 907:
                            #region 세금계산서 발행일 + 7일
                            지급예정일 = dt처리일자.AddDays(7).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 914:
                            #region 세금계산서 발행일 + 14일
                            지급예정일 = dt처리일자.AddDays(14).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 931:
                            #region 세금계산서 발행일 + 30일
                            지급예정일 = dt처리일자.AddDays(30).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 996:
                            #region 세금계산서 발행일 + 59일
                            지급예정일 = dt처리일자.AddDays(59).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 961:
                            #region 세금계산서 발행일 + 60일
                            지급예정일 = dt처리일자.AddDays(60).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 997:
                            #region 익월말 하루전 결제
                            익월 = dt처리일자.AddMonths(1);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(-1).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 998:
                            #region 선지급
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        case 999:
                            #region 즉시결제
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        default:
                            #region 지급예정일이 정해져 있는 매입처 (익월 n일, 1달을 30일로 봄)
                            익월 = dt처리일자.AddMonths(dtPay / 30);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(dtPay % 30).ToString("yyyyMMdd");
                            #endregion
                            break;
                    }
                }
                //else if (원화금액 <= 1000000)
                //{
                //    #region 100만원 이하, 10일
                //    지급예정일 = dt처리일자.AddDays(10).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 1000000 && 원화금액 <= 3000000)
                //{
                //    #region 100만원 초과 300만원 이하, 익월 15일
                //    지급예정일 = dt처리일자.AddMonths(1).ToString("yyyyMM") + "15";
                //    #endregion
                //}
                //else if (원화금액 > 3000000 && 원화금액 <= 5000000)
                //{
                //    #region 300만원 초과 500만원 이하, 익월 말일
                //    DateTime 익월 = dt처리일자.AddMonths(1);
                //    지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                //    #endregion
                //}
                //else if (원화금액 > 5000000 && 원화금액 <= 10000000)
                //{
                //    #region 500만원 초과 1000만원 이하, 월 마감 후 45일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(45).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 10000000 && 원화금액 <= 30000000)
                //{
                //    #region 1000만원 초과 3000만원 이하, 월 마감 후 60일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(60).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 30000000)
                //{
                //    #region 3000만원 초과, 월 마감 후 75일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(75).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else
                //{
                //    지급예정일 = 처리일자;
                //}

                return 지급예정일;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return string.Empty;
        }
        #endregion
    }
}