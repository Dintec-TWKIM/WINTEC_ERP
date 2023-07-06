using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.ERPU.PU.Common;
using Duzon.Windows.Print;
using DzHelpFormLib;
using pur;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_ITR_REG_WINTEC : PageBase
	{
        private P_CZ_PU_ITR_REG_WINTEC_BIZ _biz = new P_CZ_PU_ITR_REG_WINTEC_BIZ();
        private FreeBinding _header = new FreeBinding();
        public string MNG_LOT = string.Empty;
        public string MNG_SERIAL = string.Empty;
        private string FG_LOAD = string.Empty;
        private string m_sPJT재고사용 = "000";
        private bool m_bPJT사용 = false;
        private DataTable _dt_sl = null;
        private DataTable _dt_pjt = null;
        private DataTable _dt_partner = null;
        private DataTable _dt_gi_prt = null;
        private bool b단가권한 = true;
        private bool b금액권한 = true;
        private bool b수량권한 = true;
        private bool bUmSetting = false;
        private bool bStandard = false;
        private string CD_PLANT;
        private string NO_EMP;
        private string NM_KOR;
        private string CD_SL;
        private string NM_SL;
        private string DT_SV = string.Empty;
        private DataRow[] dt_재고조정 = null;
        private string rt_dt_start;
        private string rt_dt_end;
        private string rt_cd_plant;
        private string rt_no_io;
        private string fg_gubun;
        private string rt_fg_cls = string.Empty;
        private decimal d_SEQ_PROJECT = 0M;
        private string s_CD_PJT_ITEM = string.Empty;
        private string s_NM_PJT_ITEM = string.Empty;
        private string s_PJT_ITEM_STND = string.Empty;
        private ComUser _USER = null;

        public P_CZ_PU_ITR_REG_WINTEC()
        {
            this.InitializeComponent();

            this.FG_LOAD = "1";
        }

        public P_CZ_PU_ITR_REG_WINTEC(string NO_IO, string CD_PLANT, string FG_GUBUN)
        {
            this.InitializeComponent();

            this.FG_LOAD = "1";
            this.rt_no_io = NO_IO;
            this.rt_cd_plant = CD_PLANT;
            this.fg_gubun = FG_GUBUN;
        }

        public P_CZ_PU_ITR_REG_WINTEC( string _CD_PLANT, string _NO_EMP, string _NM_KOR, string _CD_SL, string _NM_SL, string _DT_SV, DataRow[] dt)
        {
            this.InitializeComponent();

            this.FG_LOAD = "2";
            this.CD_PLANT = _CD_PLANT;
            this.NO_EMP = _NO_EMP;
            this.NM_KOR = _NM_KOR;
            this.CD_SL = _CD_SL;
            this.NM_SL = _NM_SL;
            this.DT_SV = _DT_SV;
            this.dt_재고조정 = dt;
        }

        public P_CZ_PU_ITR_REG_WINTEC( string ps_fg_gubun, string pg_no_io, string ps_dt_io, string ps_cd_plant, string ps_fg_cls)
        {
            try
            {
                this.InitializeComponent();

                this.FG_LOAD = "1";
                this.fg_gubun = ps_fg_gubun;
                this.rt_no_io = pg_no_io;
                this.rt_dt_start = ps_dt_io;
                this.rt_dt_end = ps_dt_io;
                this.rt_cd_plant = ps_cd_plant;
                this.rt_fg_cls = ps_fg_cls;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PU_ITR_REG_WINTEC(PageBaseConst.CallType pageCallType, string idMemo) : this("PMS", "", "", "", "")
        {
            this.rt_no_io = this._biz.GetNoIo(idMemo)[0];
            this.rt_cd_plant = this._biz.GetNoIo(idMemo)[1];
        }

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();
                if (MA.ServerKey(false, "STRAFFIC"))
                    this._USER = new ComUser();
                DataTable dataTable = BASIC.MFG_AUTH("P_PU_ITR_REG");
                if (dataTable.Rows.Count > 0)
                {
                    this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
                    this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
                    this.b수량권한 = !(dataTable.Rows[0]["YN_QT"].ToString() == "Y");
                }
                this.InitGrid();
                this.InitEvent();
                this.MA_EXC_SETTING();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);
            this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Grid_AfterCodeHelp);

            this.btn업무공유.Click += new EventHandler(this.btn업무공유_Click);
            this.btnCC적용.Click += new EventHandler(this.btnCC적용_Click);
            this.btn프로젝트.Click += new EventHandler(this.btn프로젝트_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn요청적용.Click += new EventHandler(this.btn요청적용_Click);
            this.btn대체출고2.Click += new EventHandler(this.btn대체출고2_Click);
            this.btn입고단가우선순위.Click += new EventHandler(this.btn입고단가우선순위_Click);
            this.btn대체출고.Click += new EventHandler(this.btn대체출고_Click);
            this.btnBOM.Click += new EventHandler(this.btnBOM_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn관련시리얼정보.Click += new EventHandler(this.btn관련시리얼정보_Click);
            this.btnPJT적용.Click += new EventHandler(this.btnPJT적용_Click);
            this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            this.btn환종적용.Click += new EventHandler(this.btn환종적용_Click);
            this.cbo대체유형.SelectedIndexChanged += new EventHandler(this.cbo대체유형_SelectedIndexChanged);
            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);
            this.ctx수불형태.CodeChanged += new EventHandler(this.tb_CD_QTIO_CodeChanged);
        }

        private void MA_EXC_SETTING()
        {
            this.MNG_LOT = Global.MainFrame.LoginInfo.MngLot;
            this.MNG_SERIAL = this._biz.Search_SERIAL();
            if (string.Compare(this.MNG_SERIAL, "Y") == 0)
                this.btn관련시리얼정보.Visible = true;
            if (BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000025") == "001")
                this.bUmSetting = true;
            if (BASIC.GetMAEXC("계정대체입고등록-입고단가우선순위적용유무") == "000")
            {
                this.bpPanelControl10.Visible = false;
                this.btn입고단가우선순위.Visible = false;
                this.cbo단가유형.Enabled = true;
                this.cbo입고단가적용.Enabled = true;
            }
            else
            {
                this.bpPanelControl10.Visible = true;
                this.btn입고단가우선순위.Visible = true;
                this.cbo단가유형.Enabled = false;
                this.cbo입고단가적용.Enabled = false;
            }
            if (this.bUmSetting)
            {
                this.bpPanelControl10.Visible = true;
                this.btn입고단가우선순위.Visible = true;
                this.btn입고단가우선순위.Text = this.DD("단가우선순위지정");
                this.cbo단가유형.Enabled = true;
                this.cbo입고단가적용.Visible = false;
                this.lbl입고단가적용.Visible = false;
            }
            if (App.SystemEnv.PMS사용)
                this.btn업무공유.Visible = true;
            if (Global.MainFrame.ServerKeyCommon == "SINJINSM")
            {
                this.btn업체전용1.Visible = true;
                this.btn업체전용1.Click += new EventHandler(this.btnOnly1_Click);
                this.btn업체전용1.Text = "중량금액적용";
            }
            if (BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000001") == "001")
                this.btn원가요소별금액.Visible = true;
            if (!(Config.MA_ENV.PJT형여부 == "Y"))
                return;
            this.btn프로젝트.Visible = true;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_ITEM", "품목코드", 120, true);
            this._flex.SetCol("NM_ITEM", "품목명", 150, false);
            this._flex.SetCol("STND_ITEM", "규격", 80, false);
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", 80, false);
            this._flex.SetCol("MAT_ITEM", "재질", 80, false);
            this._flex.SetCol("UNIT_IM", "재고단위", 100, false);
            this._flex.SetCol("NM_ITEMGRP", "품목군", 140, false);
            this._flex.SetCol("NM_GRP_MFG", "제품군", 140, false);
            this._flex.SetCol("FG_SERNO", "S/N,LOT관리", 100, false);
            this._flex.SetCol("YN_LOT", "LOT여부", 100, false);
            this._flex.SetCol("CD_SL", "창고코드", 80, true, typeof(string));
            this._flex.SetCol("NM_SL", "창고명", 100, false);
            this._flex.SetCol("QT_INV", "현재고", 120, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("CD_PJT", "프로젝트", 120, true, typeof(string));
            this._flex.SetCol("NM_PROJECT", "프로젝트명", 120, false, typeof(string));
            if (this.b수량권한)
            {
                this._flex.SetCol("QT_GOOD_INV", "입고수량(재고)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
                this._flex.SetCol("QT_UNIT_MM", "입고수량(발주단위)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            }
            if (this.b단가권한)
            {
                this._flex.SetCol("UM_EX", "단가(재고)", 120, true, typeof(decimal), FormatTpType.UNIT_COST);
                this._flex.SetCol("UM_EX_PSO", "단가(수배단위)", 120, false, typeof(decimal), FormatTpType.UNIT_COST);
            }
            if (this.b금액권한)
            {
                this._flex.SetCol("AM_EX", "외화금액", 120, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM", "원화금액", 120, false, typeof(decimal), FormatTpType.MONEY);
            }
            this._flex.SetCol("PARTNER", "품목거래처코드", 100, false);
            this._flex.SetCol("NM_PARTNER", "품목거래처명", 100, false);
            this._flex.SetCol("DC_RMK", "비고", 120, true);
            this._flex.SetCol("DC_RMK1", "비고1", 120, true);
            if (Global.MainFrame.ServerKeyCommon == "SINJINSM")
                this._flex.SetCol("WEIGHT", "중량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("CD_CC", "C/C코드", 100, true, typeof(string));
            this._flex.SetCol("NM_CC", "C/C명", 100, false, typeof(string));
            this._flex.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }
                                                                                      , new string[] { "CD_CC", "NM_CC" });
            if (BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000021") == "100")
            {
                this._flex.SetCol("CD_PARTNER", "거래처코드", 100, true, typeof(string));
                this._flex.SetCol("LN_PARTNER", "거래처명", 100, true, typeof(string));
                this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }
                                                                                                    , new string[] { "CD_PARTNER", "LN_PARTNER" });
            }
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flex.SetCol("NO_CBS", "CBS번호", 140, true, typeof(string));
                this._flex.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                this._flex.SetCol("NO_LINE_PJTBOM", "프로젝트BOM항번", 140, false, typeof(decimal));
                this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flex.SetCol("CD_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flex.SetCol("NM_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                if (!App.SystemEnv.PMS사용)
                {
                    this._flex.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
                    this._flex.SetCol("NM_COST", "원가명", 140, false, typeof(string));
                }
            }
            this._flex.SetCol("UNIT_PO", "발주단위", 100, false);
            this._flex.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM" }
                                                                                            , new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM" }
                                                                                            , new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM",
                                                                                                             "YN_LOT" }, ResultMode.SlowMode);
            this._flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }
                                                                                      , new string[] { "CD_SL", "NM_SL" });
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_UNIT",
                                                                                                        "NM_UNIT",
                                                                                                        "STND_UNIT" }
                                                                                       , new string[] { "NO_PROJECT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_PJT_ITEM",
                                                                                                        "NM_PJT_ITEM",
                                                                                                        "PJT_ITEM_STND" }
                                                                                       , new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_UNIT",
                                                                                                        "NM_UNIT",
                                                                                                        "STND_UNIT" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("CD_UNIT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_UNIT",
                                                                                                         "NM_UNIT",
                                                                                                         "STND_UNIT" }
                                                                                        , new string[] { "NO_PROJECT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_PJT_ITEM",
                                                                                                         "NM_PJT_ITEM",
                                                                                                         "PJT_ITEM_STND" }
                                                                                        , new string[] { "CD_PJT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_UNIT",
                                                                                                         "NM_UNIT",
                                                                                                         "STND_UNIT" }, ResultMode.FastMode);
            }
            else
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PROJECT" }
                                                                                       , new string[] { "NO_PROJECT", "NM_PROJECT" });
            if (!this.bStandard)
            {
                this._flex.SetCol("WEIGHT", "단위중량", 100, false, CheckTypeEnum.NONE, false, 21, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("QT_WEIGHT", "총중량", 100, false, CheckTypeEnum.NONE, false, 21, typeof(decimal), FormatTpType.MONEY);
            }
            this._flex.SetCodeHelpCol("NO_CBS", "H_PM_CBS_SUB", ShowHelpEnum.Always, new string[] { "NO_CBS", "CD_COST","NM_COST" }
                                                                                   , new string[] { "NO_CBS", "CD_COST", "NM_COST" });
            this._flex.SettingVersion = "20131203";
            if (this._USER != null)
                this._USER.SetCol(this._flex);
            Config.UserColumnSetting.InitGrid_UserMenu(this._flex, this.PageID, true);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex.SetDummyColumn("S");
            this._flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "NM_SL", "YN_LOT", "NM_ITEMGRP", "FG_SERNO", "QT_INV", "AM", "UM_EX_PSO");
            if (Config.MA_ENV.YN_UNIT == "Y")
                this._flex.SetExceptSumCol("UM", "UM_EX", "UM_EX_PSO", "SEQ_PROJECT");
            else
                this._flex.SetExceptSumCol("UM", "UM_EX", "UM_EX_PSO");
            this._flex.AddRow += new EventHandler(this.btn추가_Click);
            this._flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
            List<string> stringList = new List<string>();
            stringList.Add("CD_ITEM");
            stringList.Add("CD_SL");
            if (Config.MA_ENV.프로젝트사용 || Config.MA_ENV.PJT형여부 == "Y")
            {
                stringList.Add("CD_PJT");
                if (Config.MA_ENV.YN_UNIT == "Y")
                    stringList.Add("SEQ_PROJECT");
            }
            if (BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000011") == "100")
                stringList.Add("CD_CC");
            this._flex.VerifyNotNull = stringList.ToArray();
            if (Global.MainFrame.ServerKeyCommon == "TRIGEM" || BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000046") == "100")
                this._flex.VerifyCompare(this._flex.Cols["UM_EX"], 0, OperatorEnum.NotEqual);
            this._flex.EnterKeyAddRow = true;
            this._flex.AddMenuSeperator();
            ToolStripMenuItem parent = this._flex.AddPopup("엑셀관리");
            this._flex.AddMenuItem(parent, "파일생성", new EventHandler(this.Menu_Click));
            this._flex.AddMenuItem(parent, "파일업로드", new EventHandler(this.Menu_Click));
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();
                this.m_sPJT재고사용 = ComFunc.전용코드("프로젝트재고사용");
                this.m_bPJT사용 = App.SystemEnv.PROJECT사용;
                if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
                    this.bStandard = true;
                this.InitControl();
                MsgControl.ShowMsg(" 적용 데이터를 집계중입니다. \r\n잠시만 기다려주세요!");
                DataSet dataSet = this._biz.Search("@#$%", this.FG_LOAD, "@#$");
                this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
                this._header.ClearAndNewRow();
                this.ctx창고.CodeValue = string.Empty;
                this.ctx창고.CodeName = string.Empty;
                this._flex.Binding = dataSet.Tables[1];
                this.수불형태Default셋팅();
                this.dtp입고일.Focus();
                this.oneGrid1.UseCustomLayout = true;
                this.bpPanelControl1.IsNecessaryCondition = true;
                this.bpPanelControl2.IsNecessaryCondition = true;
                this.bpPanelControl3.IsNecessaryCondition = true;
                this.bpPanelControl4.IsNecessaryCondition = true;
                this.bpPanelControl6.IsNecessaryCondition = true;
                this.bpPanelControl10.IsNecessaryCondition = true;
                this.oneGrid1.IsSearchControl = false;
                this.oneGrid1.InitCustomLayout();
                if (this.FG_LOAD == "2")
                {
                    this.cbo공장.SelectedValue = this.CD_PLANT;
                    this._header.CurrentRow["CD_PLANT"] = this.CD_PLANT;
                    this.ctx담당자.CodeValue = this.NO_EMP;
                    this.ctx담당자.CodeName = this.NM_KOR;
                    this._header.CurrentRow["NO_EMP"] = this.NO_EMP;
                    this.dtp입고일.Text = this.DT_SV;
                    this._header.CurrentRow["DT_IO"] = this.DT_SV;
                    this.ctx창고.CodeValue = this.CD_SL;
                    this.ctx창고.CodeName = this.NM_SL;
                    int num = 1;
                    this._flex.Redraw = false;
                    foreach (DataRow dr in this.dt_재고조정)
                        this.그리드한행추가(dr, num++, "자재", false);
                    this.SETUM();
                    this._flex.Redraw = true;
                    this._flex.Select(this._flex.DataTable.Rows.Count, this._flex.Cols.Fixed);
                    this.Page_DataChanged(null, null);
                    this.cbo공장.Enabled = false;
                    this.btn삭제.Enabled = true;
                    this._flex.Focus();
                }
                if (this.cbo대체유형.SelectedText == "" && this.cbo대체유형.DataBindings.Count > 0)
                {
                    this.cbo대체유형.SelectedIndex = 0;
                    this._header.CurrentRow["FG_TPIO"] = this.cbo대체유형.SelectedValue.ToString();
                }
                if (this.fg_gubun == "재고자산수불부" || this.fg_gubun == "PMS" || this.fg_gubun == "계정대체입고현황")
                    this.화면이동();
                MsgControl.CloseMsg();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            try
            {
                DataSet comboData = this.GetComboData("NC;MA_PLANT", "N;PU_C000001", "S;MA_B000022", "N;MA_B000005");
                this.cbo공장.DataSource = comboData.Tables[0];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";
                this.cbo단가유형.DataSource = comboData.Tables[1];
                this.cbo단가유형.DisplayMember = "NAME";
                this.cbo단가유형.ValueMember = "CODE";
                DataRow[] dataRowArray = comboData.Tables[2].Select("CODE IN ('002','003')");
                DataTable dataTable = comboData.Tables[2].Clone();
                foreach (DataRow dataRow in dataRowArray)
                    dataTable.LoadDataRow(dataRow.ItemArray, true);
                this.cbo입고단가적용.DataSource = dataTable;
                this.cbo입고단가적용.DisplayMember = "NAME";
                this.cbo입고단가적용.ValueMember = "CODE";
                this.dtp입고일.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.cbo환종.DataSource = comboData.Tables[3];
                this.cbo환종.DisplayMember = "NAME";
                this.cbo환종.ValueMember = "CODE";
                DataTable dt = MF.GetCode("PU_C000021").Clone();
                foreach (DataRow dataRow in MF.GetCode("PU_C000021").Select("CD_FLAG1 = '1' OR ISNULL(CD_FLAG1,'') = ''"))
                    dt.LoadDataRow(dataRow.ItemArray, true);
                new SetControl().SetCombobox(this.cbo대체유형, dt);
                if (!(this.cbo대체유형.SelectedText == "") || dt.Rows.Count <= 0)
                    return;
                this.cbo대체유형.SelectedIndex = 0;
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
                if (e.JobMode == JobModeEnum.조회후수정)
                {
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                    this.btn첨부파일.Enabled = true;
                    this.SetButtonState(false);
                }
                else
                {
                    this.txt수불번호.Enabled = true;
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = false;
                    this.btn첨부파일.Enabled = false;
                    this.SetButtonState(true);
                    if (this.cbo공장.SelectedValue == null)
                    {
                        if (Global.MainFrame.LoginInfo.CdPlant == null || Global.MainFrame.LoginInfo.CdPlant == "")
                        {
                            this.cbo공장.SelectedIndex = 0;
                        }
                        else
                        {
                            string cdPlant = Global.MainFrame.LoginInfo.CdPlant;
                            this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
                        }
                        this._header.CurrentRow["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                    }
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

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarSaveButtonEnabled = this.IsChanged();
                if (this.추가모드여부)
                    this.SetButtonState(!this.IsChanged());
                if (this._flex.DataTable == null || !this._flex.HasNormalRow)
                    return;
                this.btn삭제.Enabled = this._flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public bool Check() => ComFunc.NullCheck(new Hashtable()
        {
            {
                this.cbo공장,
                this.lbl공장
            }
        });

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch() || !this.Check())
                    return;
                this.dtp입고일.Focus();
                P_PU_ITR_SUB pPuItrSub = new P_PU_ITR_SUB(this.MainFrameInterface, (DataTable)this.cbo공장.DataSource, D.GetString(this.cbo공장.SelectedValue), this.ctx담당자.CodeValue, this.ctx담당자.CodeName, D.GetString(this.ctx창고.CodeValue), D.GetString(this.ctx창고.CodeName));
                if (pPuItrSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataSet dataSet = this._biz.Search(pPuItrSub.m_SelecedRow["NO_IO"].ToString(), this.FG_LOAD, D.GetString(this.cbo공장.SelectedValue));
                    if (dataSet.Tables[0].Rows.Count < 1)
                    {
                        this.ShowMessage(공통메세지._이가존재하지않습니다, pPuItrSub.m_SelecedRow["NO_IO"].ToString());
                        this.OnToolBarAddButtonClicked(sender, e);
                    }
                    else
                    {
                        this._header.SetDataTable(dataSet.Tables[0]);
                        this._flex.Binding = dataSet.Tables[1];
                        this.ctx창고.CodeValue = this._flex.DataTable.Rows[0]["CD_SL"].ToString();
                        this.ctx창고.CodeName = this._flex.DataTable.Rows[0]["NM_SL"].ToString();
                        this.ctx프로젝트.CodeValue = dataSet.Tables[0].Rows[0]["CD_PJT"].ToString();
                        this.ctx프로젝트.CodeName = dataSet.Tables[0].Rows[0]["NM_PROJECT"].ToString();
                        if (D.GetString(this._header.CurrentRow["FG_TPIO"]) == "")
                        {
                            this.cbo대체유형.SelectedIndex = 0;
                            this._header.CurrentRow["FG_TPIO"] = this.cbo대체유형.SelectedValue.ToString();
                        }
                        foreach (DataRow dataRow in this._flex.DataTable.Select())
                            dataRow["FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_TPIO"]);
                        if (this._flex.DataTable.Select("NO_IO_MGMT_APPLY <> '' AND NO_IO_MGMT_APPLY IS NOT NULL").Length != 0)
                        {
                            this.btn추가.Enabled = false;
                            this.btn대체출고.Enabled = true;
                            this.btn대체출고2.Enabled = true;
                        }
                        else
                        {
                            this.btn추가.Enabled = true;
                            this.btn대체출고.Enabled = false;
                            this.btn대체출고2.Enabled = false;
                        }
                    }
                    this.fg_gubun = string.Empty;
                    this.dtp입고일.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!this._flex.HasNormalRow)
            {
                this.ShowMessage("WK1_013");
                return false;
            }
            if (!base.BeforeSave() && !this.CheckFieldHead() || !this.Verify())
                return false;
            if (!MA.ServerKey(false, "FDWL"))
            {
                foreach (DataRow dataRow in this._flex.DataTable.Select("", "", DataViewRowState.CurrentRows))
                {
                    if (!(D.GetDecimal(dataRow["QT_GOOD_INV"]) > 0M))
                    {
                        this.ShowMessage(공통메세지._은_보다커야합니다, this.DD("QT_GOOD_INV"), this.DD("0"));
                        return false;
                    }
                }
            }
            return this._USER == null || this._USER.SaveCheck(this._flex, this.cbo대체유형, "");
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(PageResultMode.SaveGood);
                this.SetButtonState(false);
                for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                {
                    decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[row, "CD_SL"]), D.GetString(this._flex[row, "CD_ITEM"]), D.GetString(this._flex[row, "CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[row, "SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[row, "CD_SL"]), D.GetString(this._flex[row, "CD_ITEM"]));
                    this._flex[row, "QT_INV"] = D.GetDecimal(val);
                }
                this._flex.AcceptChanges();
                if (this._flex.DataTable.Select("NO_IO_MGMT_APPLY <> '' AND NO_IO_MGMT_APPLY IS NOT NULL").Length != 0)
                {
                    this.btn추가.Enabled = false;
                    this.btn대체출고.Enabled = true;
                    this.btn대체출고2.Enabled = true;
                }
                else
                {
                    this.btn추가.Enabled = true;
                    this.btn대체출고.Enabled = false;
                    this.btn대체출고2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!this.BeforeSave() || !this.CheckFieldHead())
                    return false;
                if (this._flex.GetChanges() == null && this._header.CurrentRow.RowState == DataRowState.Unchanged)
                {
                    this.ShowMessage(공통메세지.변경된내용이없습니다);
                    return false;
                }
                string str = "";
                if (this.추가모드여부)
                {
                    str = D.GetString(this.txt수불번호.Text);
                    if (str == "")
                        str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "20", this.dtp입고일.Text.Substring(0, 6));
                    this._header.CurrentRow["NO_IO"] = str;
                    if (this._flex.HasNormalRow)
                    {
                        int num = 0;
                        foreach (DataRow row in this._flex.DataTable.Rows)
                        {
                            row["NO_IO"] = str;
                            row["NO_IOLINE"] = ++num;
                        }
                    }
                }
                else if (!this.추가모드여부)
                {
                    str = D.GetString(this.txt수불번호.Text);
                    decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                    foreach (DataRow row in this._flex.DataTable.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            row["NO_IO"] = str;
                            ++maxValue;
                            row["NO_IOLINE"] = maxValue;
                        }
                    }
                }
                if (D.GetString(this.ctx창고.CodeValue) != "")
                {
                    for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                    {
                        if (this._flex[row, "CD_SL"].ToString() == string.Empty || this._flex[row, "CD_SL"].ToString() == null)
                        {
                            this._flex[row, "CD_SL"] = D.GetString(this.ctx창고.CodeValue);
                            this._flex[row, "NM_SL"] = D.GetString(this.ctx창고.CodeName);
                        }
                    }
                }
                if (BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000021") == "100")
                    this._header.CurrentRow["CD_PARTNER"] = "";
                DataTable changes1 = this._header.GetChanges();
                DataTable changes2 = this._flex.GetChanges();
                DataTable dtLOT = null;
                DialogResult dialogResult;
                changes2.Clone();
                DataTable table1 = new DataView(changes2, "YN_LOT = 'YES'", "", DataViewRowState.CurrentRows).ToTable();
                if (table1 != null && table1.Rows.Count > 0)
                {
                    if (this.m_sPJT재고사용 == "100" && this.m_bPJT사용)
                    {
                        dtLOT = table1;
                        dtLOT.Columns.Add("NO_IOLINE2", typeof(decimal), "0");
                        dtLOT.Columns.Add("DC_LOTRMK", typeof(string), "");
                        dtLOT.Columns.Add("DT_LIMIT", typeof(string), "");
                        dtLOT.Columns.Remove("YN_RETURN");
                        foreach (DataRow row in dtLOT.Rows)
                        {
                            row["NO_LOT"] = row["CD_PJT"];
                            row["QT_IO"] = row["QT_GOOD_INV"];
                        }
                    }
                    else
                    {
                        DataTable table2 = new DataView(changes2, "YN_LOT = 'YES'", "", DataViewRowState.Added).ToTable();
                        if (table2 != null && table2.Rows.Count > 0)
                        {
                            dialogResult = DialogResult.No;
                            P_CZ_PU_LOT_SUB_R pPuLotSubR = new P_CZ_PU_LOT_SUB_R(table2);
                            P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(table2, "Y");
                            if ((!(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "N") ? pPuLotSubI.ShowDialog((IWin32Window)this) : pPuLotSubR.ShowDialog((IWin32Window)this)) != DialogResult.OK)
                                return false;
                            dtLOT = !(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "N") ? pPuLotSubI.dtL : pPuLotSubR.dtL;
                        }
                    }
                }
                DataTable dtSERL = null;
                if (string.Compare(this.MNG_SERIAL, "Y") == 0 && changes2 != null)
                {
                    DataRow[] dataRowArray = changes2.Select("NO_SERL = 'YES'", "", DataViewRowState.Added);
                    DataTable dt = changes2.Clone();
                    if (dataRowArray.Length > 0)
                    {
                        foreach (DataRow row in dataRowArray)
                            dt.ImportRow(row);
                        dialogResult = DialogResult.No;
                        P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dt);
                        dt.Columns.Remove("QT_SERIAL_COUNT");
                        P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dt);
                        if ((!(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "N") ? pPuSerlSubI.ShowDialog((IWin32Window)this) : pPuSerlSubR.ShowDialog((IWin32Window)this)) == DialogResult.OK)
                        {
                            dtSERL = !(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "N") ? pPuSerlSubI.dtL : pPuSerlSubR.dtL;
                        }
                        else
                        {
                            if (this.추가모드여부)
                            {
                                this.txt수불번호.Text = "";
                                this._header.CurrentRow["NO_IO"] = "";
                            }
                            return false;
                        }
                    }
                }
                DataTable dt_location = null;
                if (Config.MA_ENV.YN_LOCATION == "Y")
                {
                    bool b_return = false;
                    DataTable dt = changes2.Clone().Copy();
                    foreach (DataRow dataRow in changes2.Select())
                        dt.LoadDataRow(dataRow.ItemArray, true);
                    dt_location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_R_SUB(dt, out b_return);
                    if (!b_return)
                        return false;
                }
                DataRow currentRow = this._header.CurrentRow;
                bool flag = this._biz.Save(changes1, changes2, dtLOT, this.FG_LOAD, currentRow, dtSERL, dt_location);
                if (!flag)
                    return false;
                if (flag)
                {
                    if (this.추가모드여부)
                        this.txt수불번호.Text = str;
                    this._header.AcceptChanges();
                    this._flex.AcceptChanges();
                    if (App.SystemEnv.PMS사용 || Global.MainFrame.ServerKeyCommon == "KINTEC")
                    {
                        this.rt_no_io = this.txt수불번호.Text;
                        this.rt_cd_plant = D.GetString(this.cbo공장.SelectedValue);
                        this.fg_gubun = "PMS";
                        this.화면이동();
                    }
                }
                this.SetButtonState(false);
                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return false;
        }

        protected override bool BeforeDelete()
        {
            string maexc1 = BASIC.GetMAEXC("POP연동");
            string maexc2 = BASIC.GetMAEXC("MES_POP연동옵션");
            if (!base.BeforeDelete())
                return false;
            if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까, this.PageName) != DialogResult.Yes)
                return false;
            if (maexc1 != "000" || maexc2 != "000")
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT NO_POP FROM MM_QTIOH WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode.ToString() + "' AND NO_IO ='" + this.txt수불번호 + "'");
                if (dataTable.Rows.Count != 0 && D.GetString(dataTable.Rows[0]["NO_POP"]) != string.Empty && this.ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                    return false;
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("PIOLINK"))
            {
                string detail = string.Empty;
                foreach (DataRow row in this._flex.DataTable.Rows)
                {
                    DataTable dataTable = this._biz.SaveCheck(D.GetString(row["NO_IO"]), D.GetString(row["NO_IOLINE"]));
                    if (dataTable != null && dataTable.Rows.Count > 0)
                        detail = detail + "\n수불유형 : " + D.GetString(dataTable.Rows[0]["NM_QTIOTP"]) + " &     수불번호 : " + D.GetString(dataTable.Rows[0]["NO_IO"]);
                }
                if (detail != string.Empty && this.ShowDetailMessage("해당시리얼의 출고이력이 존재합니다. 삭제하시겠습니까?\n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", "", detail, "QY2") != DialogResult.Yes)
                    return false;
            }
            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || this._flex == null || !this._flex.HasNormalRow)
                    return;
                if (this._biz.Delete(new object[] { this._header.CurrentRow["NO_IO"].ToString(),
                                                    this.LoginInfo.CompanyCode,
                                                    Global.MainFrame.LoginInfo.UserID,
                                                    Global.MainFrame.CurrentPageID }))
                {
                    this._header.AcceptChanges();
                    this._flex.AcceptChanges();
                    this.SetButtonState(true);
                    this.OnToolBarAddButtonClicked(null, null);
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

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd())
                    return;
                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();
                this.ctx창고.CodeValue = "";
                this.ctx창고.CodeName = "";
                this.ctx프로젝트.CodeValue = "";
                this.ctx프로젝트.CodeName = "";
                this.btn대체출고.Enabled = true;
                this.btn대체출고2.Enabled = true;
                this.btn추가.Enabled = true;
                this.cbo대체유형.SelectedIndex = 0;
                this.FG_LOAD = "1";
                this.수불형태Default셋팅();
            }
            finally
            {
                this._flex.Focus();
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.추가모드여부)
                    return;
                ReportHelper reportHelper = new ReportHelper("R_PU_ITR_REG_001", "계정대체입고전표");
                DataTable dataTable = this._biz.Search_Print(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            D.GetString( this.txt수불번호.Text),
                                                                            Global.SystemLanguage.MultiLanguageLpoint });
                if (MA.ServerKey(true, "DONG-AH"))
                {
                    reportHelper.SetDataTable(dataTable, 1);
                    reportHelper.SetDataTable(this.makeXML_DONG_AH(dataTable), 2);
                }
                else
                    reportHelper.SetDataTable(this._flex.DataTable);
                reportHelper.SetData("수불번호", this.txt수불번호.Text);
                reportHelper.SetData("입고일", this.dtp입고일.Text);
                reportHelper.SetData("수불형태", this.ctx수불형태.CodeName);
                reportHelper.SetData("입고공장", this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.Text);
                reportHelper.SetData("거래처", this.ctx거래처.CodeName);
                reportHelper.SetData("담당자", this.ctx담당자.CodeName);
                reportHelper.SetData("창고", D.GetString(this.ctx창고.CodeName));
                reportHelper.SetData("프로젝트", this.ctx프로젝트.CodeValue);
                reportHelper.SetData("담당자", this.ctx담당자.CodeName);
                reportHelper.SetData("비고", this.txt비고.Text);
                reportHelper.SetData("대체유형", this.cbo대체유형.Text);
                reportHelper.Print();
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
                if (!this.btn추가.Enabled || !this.CheckFieldHead())
                    return;
                DataRow row = this._flex.DataTable.NewRow();
                decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                row["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                row["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                row["DT_IO"] = this._header.CurrentRow["DT_IO"];
                row["NO_IOLINE"] = ++maxValue;
                row["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                row["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                row["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                row["LN_PARTNER"] = this._header.CurrentRow["LN_PARTNER"];
                row["YN_AM"] = this._header.CurrentRow["YN_AM"];
                row["CD_PJT"] = this.ctx프로젝트.CodeValue;
                row["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                row["FG_IO"] = this._header.CurrentRow["FG_IO"];
                row["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                row["FG_TPIO"] = this.cbo대체유형.SelectedValue;
                row["FG_PS"] = "1";
                if (this._USER != null)
                    this._USER.setApp(row, this._USER.FG_PJT1);
                if (D.GetString(this.ctx창고.CodeValue) != string.Empty)
                {
                    row["CD_SL"] = D.GetString(this.ctx창고.CodeValue);
                    row["NM_SL"] = D.GetString(this.ctx창고.CodeName);
                }
                this._flex.DataTable.Rows.Add(row);
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Col = 2;
                this.cbo공장.Enabled = false;
                this.cbo대체유형.Enabled = false;
                this.btn삭제.Enabled = true;
                this.ctx프로젝트.Enabled = true;
                this.btnPJT적용.Enabled = true;
                this.Page_DataChanged(null, null);
                this._flex.Col = this._flex.Cols.Fixed + 1;
                this._flex.Focus();
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
                if (!this._flex.HasNormalRow)
                    return;
                if (this._flex.DataTable.Select("S ='Y'").Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (Global.MainFrame.ServerKeyCommon.Contains("PIOLINK"))
                    {
                        for (int row = this._flex.Rows.Count - 1; row >= this._flex.Rows.Fixed; --row)
                        {
                            if (D.GetString(this._flex[row, "NO_IO"]) != string.Empty && D.GetString(this._flex[row, "S"]) == "Y")
                            {
                                DataTable dataTable = this._biz.SaveCheck(D.GetString(this._flex[row, "NO_IO"]), D.GetString(this._flex[row, "NO_IOLINE"]));
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    if (this.ShowMessage("해당시리얼의 출고이력이 존재합니다. \n" + ("(수불유형 : " + D.GetString(dataTable.Rows[0]["NM_QTIOTP"]) + ", 수불번호 : " + D.GetString(dataTable.Rows[0]["NO_IO"]) + ")") + " 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                                        return;
                                }
                            }
                        }
                    }
                    this._flex.Redraw = false;
                    for (int index = this._flex.Rows.Count - 1; index >= this._flex.Rows.Fixed; --index)
                    {
                        if (this._flex[index, "S"].ToString() == "Y")
                            this._flex.Rows.Remove(index);
                    }
                    this._flex.Redraw = true;
                    if (!this._flex.HasNormalRow)
                    {
                        this.btn추가.Enabled = true;
                        this.btn삭제.Enabled = false;
                        if (this._header.JobMode == JobModeEnum.추가후수정)
                            this.cbo공장.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnPJT적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.DataTable == null || this.ctx프로젝트.CodeName == "")
                    return;
                foreach (DataRow row in this._flex.DataTable.Rows)
                {
                    row["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    row["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                    if (Config.MA_ENV.YN_UNIT == "Y")
                    {
                        row["SEQ_PROJECT"] = this.d_SEQ_PROJECT;
                        row["CD_UNIT"] = this.s_CD_PJT_ITEM;
                        row["NM_UNIT"] = this.s_NM_PJT_ITEM;
                        row["STND_UNIT"] = this.s_PJT_ITEM_STND;
                    }
                    if (!(D.GetString(row["CD_ITEM"]) == ""))
                    {
                        decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]), D.GetString(row["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(row["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]));
                        row["QT_INV"] = D.GetDecimal(val);
                        if (this._USER != null)
                            this._USER.setApp(row, this._USER.FG_PJT1);
                    }
                }
                this.ShowMessage("적용작업을완료하였습니다");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            if (!this.CheckFieldHead())
                return;
            this.엑셀업로드();
        }

        private void 엑셀업로드()
        {
            try
            {
                string NO_KEY1 = string.Empty;
                bool flag1 = false;
                bool flag2 = false;
                string str1 = string.Empty;
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                string empty3 = string.Empty;
                string empty4 = string.Empty;
                string empty5 = string.Empty;
                string empty6 = string.Empty;
                string multi_sl = string.Empty;
                string NO_KEY2 = string.Empty;
                string NO_KEY3 = string.Empty;
                string NO_KEY4 = string.Empty;
                bool flag3 = true;
                string str2 = string.Empty;
                bool flag4 = true;
                string str3 = string.Empty;
                bool flag5 = true;
                string str4 = string.Empty;
                bool flag6 = true;
                string str5 = string.Empty;
                string empty7 = string.Empty;
                string empty8 = string.Empty;
                string empty9 = string.Empty;
                decimal num1 = 0M;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();
                    DataTable dataTable1 = new Excel().StartLoadExcel(openFileDialog.FileName);
                    string empty10 = string.Empty;
                    string[] p_nm_pjt = null;
                    string empty11 = string.Empty;
                    string empty12 = string.Empty;
                    this._flex.Redraw = false;
                    bool flag7 = dataTable1.Columns.Contains("CD_SL");
                    bool flag8 = dataTable1.Columns.Contains("CD_PJT");
                    bool flag9 = dataTable1.Columns.Contains("UM") && dataTable1.Columns.Contains("AM");
                    bool flag10 = dataTable1.Columns.Contains("DC_RMK");
                    bool flag11 = dataTable1.Columns.Contains("CD_CC");
                    bool flag12 = dataTable1.Columns.Contains("SEQ_PROJECT");
                    bool flag13 = dataTable1.Columns.Contains("CD_PARTNER");
                    bool flag14 = dataTable1.Columns.Contains("GI_PARTNER");
                    bool flag15 = dataTable1.Columns.Contains("QT_GOOD_INV");
                    bool flag16 = dataTable1.Columns.Contains("QT_UNIT_MM");
                    foreach (DataRow row in dataTable1.Rows)
                    {
                        NO_KEY1 = NO_KEY1 + row["CD_ITEM"].ToString().ToUpper().Trim() + "|";
                        if (flag8)
                            NO_KEY2 = NO_KEY2 + D.GetString(row["CD_PJT"]) + "|";
                        if (flag7)
                        {
                            if (D.GetString(row["CD_SL"]) == string.Empty)
                                row["CD_SL"] = D.GetString(this.ctx창고.CodeValue).ToUpper();
                            multi_sl = multi_sl + D.GetString(row["CD_SL"]).ToUpper() + "|";
                        }
                        if (flag13 && D.GetString(row["CD_PARTNER"]) != string.Empty)
                            NO_KEY3 = NO_KEY3 + D.GetString(row["CD_PARTNER"]) + "|";
                        if (flag14 && D.GetString(row["GI_PARTNER"]) != string.Empty)
                            NO_KEY4 = NO_KEY4 + D.GetString(row["GI_PARTNER"]) + "|";
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    string str6 = "품목코드";
                    stringBuilder.AppendLine(str6);
                    string str7 = "-".PadRight(80, '-');
                    stringBuilder.AppendLine(str7);
                    DataTable dataTable2 = this._biz.SearchItem(D.GetString(this.cbo공장.SelectedValue), NO_KEY1);
                    this._dt_sl = this._biz.SearchSL(D.GetString(this.cbo공장.SelectedValue), multi_sl);
                    this._dt_pjt = this._biz.SearchPJT(NO_KEY2);
                    this._dt_partner = this._biz.SearchPARTNER(NO_KEY3);
                    this._dt_gi_prt = this._biz.SearchPARTNER(NO_KEY4);
                    foreach (DataRow row1 in dataTable1.Rows)
                    {
                        if (row1["CD_ITEM"].ToString().ToUpper().Trim() != null && !(row1["CD_ITEM"].ToString().ToUpper().Trim() == string.Empty) && !(row1["CD_ITEM"].ToString().ToUpper().Trim() == ""))
                        {
                            foreach (DataRow row2 in dataTable2.Rows)
                            {
                                if (row1["CD_ITEM"].ToString().ToUpper().Trim() == row2["CD_ITEM"].ToString().ToUpper().Trim())
                                {
                                    flag2 = true;
                                    str1 = row1["CD_ITEM"].ToString().ToUpper().Trim();
                                    empty4 = row2["NM_ITEM"].ToString();
                                    empty2 = row2["STND_ITEM"].ToString();
                                    empty5 = row2["UNIT_IM"].ToString();
                                    empty6 = row2["FG_SERNO"].ToString();
                                    empty7 = row2["GRP_ITEMNM"].ToString();
                                    num1 = D.GetDecimal(row2["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row2["UNIT_PO_FACT"]);
                                    empty8 = row2["GRP_ITEM"].ToString();
                                    empty9 = row2["WEIGHT"].ToString();
                                    empty3 = row2["UNIT_PO"].ToString();
                                    break;
                                }
                                flag2 = false;
                            }
                            DataRow[] dataRowArray1 = null;
                            if (flag8 && D.GetString(row1["CD_PJT"]) != "")
                            {
                                DataRow[] dataRowArray2 = null;
                                if (Config.MA_ENV.YN_UNIT != "Y")
                                    dataRowArray2 = this._dt_pjt.Select("NO_PROJECT = '" + D.GetString(row1["CD_PJT"]) + "'");
                                if (Config.MA_ENV.YN_UNIT == "Y" && flag12)
                                {
                                    dataRowArray1 = this._dt_pjt.Select("NO_PROJECT = '" + D.GetString(row1["CD_PJT"]) + "' AND SEQ_PROJECT = " + D.GetDecimal(row1["SEQ_PROJECT"]) + " AND CD_PJT_ITEM ='" + D.GetString(row1["CD_UNIT"]) + "'");
                                    if (dataRowArray1 == null || dataRowArray1.Length == 0)
                                    {
                                        str3 = str3 + "\n프로젝트번호 : " + D.GetString(row1["CD_PJT"]) + " &     UNIT 항번 : " + D.GetString(row1["SEQ_PROJECT"]) + " &     UNIT 코드 : " + D.GetString(row1["CD_UNIT"]);
                                        flag4 = false;
                                    }
                                }
                                else if (dataRowArray2 == null || dataRowArray2.Length == 0)
                                {
                                    str3 = str3 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     PJT번호 : " + D.GetString(row1["CD_PJT"]);
                                    flag4 = false;
                                    continue;
                                }
                            }
                            if (flag7 && D.GetString(row1["CD_SL"]).ToUpper() != "")
                            {
                                DataRow[] dataRowArray3 = null;
                                if (this._dt_sl != null)
                                    dataRowArray3 = this._dt_sl.Select("CD_SL = '" + D.GetString(row1["CD_SL"]).ToUpper() + "'");
                                if (dataRowArray3 == null || dataRowArray3.Length == 0)
                                {
                                    str2 = str2 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]).ToUpper() + " &     창고코드 : " + D.GetString(row1["CD_SL"]).ToUpper();
                                    flag3 = false;
                                    continue;
                                }
                            }
                            if (flag13 && D.GetString(row1["CD_PARTNER"]) != "")
                            {
                                DataRow[] dataRowArray4 = null;
                                if (this._dt_partner != null)
                                    dataRowArray4 = this._dt_partner.Select("CD_PARTNER = '" + D.GetString(row1["CD_PARTNER"]) + "'");
                                if (dataRowArray4 == null || dataRowArray4.Length == 0)
                                {
                                    str4 = str4 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     거래처코드 : " + D.GetString(row1["CD_PARTNER"]);
                                    flag5 = false;
                                    continue;
                                }
                            }
                            if (flag14 && D.GetString(row1["GI_PARTNER"]) != "")
                            {
                                DataRow[] dataRowArray5 = null;
                                if (this._dt_gi_prt != null)
                                    dataRowArray5 = this._dt_gi_prt.Select("CD_PARTNER = '" + D.GetString(row1["GI_PARTNER"]) + "'");
                                if (dataRowArray5 == null || dataRowArray5.Length == 0)
                                {
                                    str5 = str5 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     납품처코드 : " + D.GetString(row1["GI_PARTNER"]);
                                    flag6 = false;
                                    continue;
                                }
                            }
                            if (flag2)
                            {
                                decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                                DataRow row3 = this._flex.DataTable.NewRow();
                                row3["CD_ITEM"] = row1["CD_ITEM"].ToString().Trim().ToUpper();
                                row3["NM_ITEM"] = empty4;
                                row3["STND_ITEM"] = empty2;
                                row3["UNIT_IM"] = empty5;
                                row3["YN_LOT"] = empty6 == "002" ? "YES" : "NO";
                                row3["NO_SERL"] = empty6 == "003" ? "YES" : "NO";
                                row3["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                                row3["NM_ITEMGRP"] = empty7;
                                row3["GRP_ITEM"] = empty8;
                                row3["GRP_ITEM"] = empty9;
                                row3["UNIT_PO"] = empty3;
                                row3["UNIT_PO_FACT"] = num1;
                                row3["FG_SERNO"] = !(empty6 == "002") ? (!(empty6 == "003") ? this.DD("미관리") : "S/N") : "LOT";
                                if (flag15 && D.GetDecimal(row1["QT_GOOD_INV"]) > 0M)
                                {
                                    row3["QT_GOOD_INV"] = row1["QT_GOOD_INV"];
                                    row3["QT_GOOD_OLD"] = row1["QT_GOOD_INV"];
                                    row3["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row3["QT_GOOD_INV"]) / num1);
                                }
                                else if (flag16 && D.GetDecimal(row1["QT_UNIT_MM"]) > 0M)
                                {
                                    row3["QT_UNIT_MM"] = row1["QT_UNIT_MM"];
                                    row3["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row3["QT_UNIT_MM"]) * num1);
                                    row3["QT_GOOD_OLD"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row3["QT_UNIT_MM"]) * num1);
                                }
                                else
                                {
                                    row3["QT_GOOD_INV"] = 0;
                                    row3["QT_GOOD_OLD"] = 0;
                                    row3["QT_UNIT_MM"] = 0;
                                }
                                if (flag9)
                                {
                                    row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row1["AM"]));
                                    row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]));
                                }
                                else if (dataTable1.Columns.Contains("AM"))
                                {
                                    row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row1["AM"]));
                                    if (D.GetDecimal(row3["AM_EX"]) != 0M && D.GetDecimal(row3["QT_GOOD_INV"]) != 0M)
                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row3["AM_EX"]) / D.GetDecimal(row3["QT_GOOD_INV"]));
                                }
                                else if (BASIC.GetMAEXC("계정대체입고등록-입고단가우선순위적용유무") == "000")
                                {
                                    if (dataTable1.Columns.Contains("UM"))
                                    {
                                        row3["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]) * num1);
                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]));
                                        row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]) * D.GetDecimal(row3["QT_GOOD_INV"]));
                                    }
                                    else
                                    {
                                        if (D.GetString(this.cbo단가유형.SelectedValue) == string.Empty)
                                        {
                                            this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.DD("단가유형"));
                                            return;
                                        }
                                        DataTable dataTable3 = this._biz.Search_um_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                       D.GetString( this.ctx거래처.CodeValue),
                                                                                                       this.dtp입고일.Text,
                                                                                                       D.GetString(row1["CD_ITEM"]),
                                                                                                       D.GetString(this.cbo단가유형.SelectedValue),
                                                                                                       D.GetString(this.cbo입고단가적용.SelectedValue) });
                                        if (dataTable3 == null || dataTable3.Rows.Count == 0)
                                        {
                                            row3["UM_EX"] = 0;
                                            row3["AM_EX"] = 0;
                                        }
                                        else
                                        {
                                            row3["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable3.Rows[0]["UM_ITEM"]));
                                            row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable3.Rows[0]["UM_ITEM"]) / num1);
                                            row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row3["UM_EX"]) * D.GetDecimal(row3["QT_GOOD_INV"]));
                                        }
                                    }
                                }
                                else
                                {
                                    if (dataTable1.Columns.Contains("UM"))
                                    {
                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]));
                                        row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row1["UM"]) * D.GetDecimal(row3["QT_GOOD_INV"]));
                                    }
                                    if (dataTable1.Columns.Contains("AM") && !dataTable1.Columns.Contains("UM") && D.GetDecimal(row3["QT_GOOD_INV"]) != 0M)
                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row3["AM_EX"]) / D.GetDecimal(row3["QT_GOOD_INV"]));
                                }
                                row3["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row3["AM_EX"]));
                                row3["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row3["UM_EX"]));
                                row3["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row3["UM_EX"]) * num1);
                                if (!flag7)
                                {
                                    row3["CD_SL"] = this.ctx창고.CodeValue;
                                    row3["NM_SL"] = this.ctx창고.CodeName;
                                }
                                else if (flag7 && this.ChkData_SL(D.GetString(row1["CD_SL"]).ToUpper(), ref empty10))
                                {
                                    row3["CD_SL"] = D.GetString(row1["CD_SL"]).ToUpper();
                                    row3["NM_SL"] = empty10;
                                }
                                if (Config.MA_ENV.YN_UNIT != "Y")
                                {
                                    if (!flag8)
                                    {
                                        row3["CD_PJT"] = this.ctx프로젝트.CodeValue;
                                        row3["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                                    }
                                    else if (flag8 && this.ChkData_PJT(D.GetString(row1["CD_PJT"]), ref p_nm_pjt))
                                    {
                                        row3["CD_PJT"] = D.GetString(row1["CD_PJT"]);
                                        row3["NM_PROJECT"] = p_nm_pjt[0].ToString();
                                        if (this._USER != null)
                                            this._USER.setApp(row3, p_nm_pjt[1].ToString());
                                    }
                                }
                                if (!flag13)
                                    row3["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                                else if (flag13 && this.ChkData_PARTNER(D.GetString(row1["CD_PARTNER"]), ref empty11))
                                    row3["CD_PARTNER"] = D.GetString(row1["CD_PARTNER"]);
                                if (flag14 && flag14 && this.ChkData_GI_PRT(D.GetString(row1["GI_PARTNER"]), ref empty12))
                                    row3["GI_PARTNER"] = D.GetString(row1["GI_PARTNER"]);
                                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                                {
                                    row3["SEQ_PROJECT"] = dataRowArray1[0]["SEQ_PROJECT"];
                                    row3["CD_UNIT"] = dataRowArray1[0]["CD_PJT_ITEM"];
                                    row3["NM_UNIT"] = dataRowArray1[0]["NM_PJT_ITEM"];
                                    row3["STND_UNIT"] = dataRowArray1[0]["PJT_ITEM_STND"];
                                    row3["CD_PJT"] = D.GetString(dataRowArray1[0]["NO_PROJECT"]);
                                    row3["NM_PROJECT"] = D.GetString(dataRowArray1[0]["NM_PROJECT"]);
                                }
                                if (flag10)
                                    row3["DC_RMK"] = D.GetString(row1["DC_RMK"]);
                                if (flag11)
                                    row3["CD_CC"] = D.GetString(row1["CD_CC"]);
                                row3["NO_IO"] = this._header.CurrentRow["NO_IO"];
                                row3["NO_IOLINE"] = ++maxValue;
                                row3["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                                row3["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                                row3["DT_IO"] = this._header.CurrentRow["DT_IO"];
                                row3["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                                row3["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                                row3["YN_AM"] = this._header.CurrentRow["YN_AM"];
                                row3["FG_IO"] = this._header.CurrentRow["FG_IO"];
                                row3["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                row3["FG_PS"] = "1";
                                row3["FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
                                decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row3["CD_SL"]), D.GetString(row3["CD_ITEM"]), D.GetString(row3["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(row3["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row3["CD_SL"]), D.GetString(row3["CD_ITEM"]));
                                row3["QT_INV"] = D.GetDecimal(val);
                                this._flex.DataTable.Rows.Add(row3);
                            }
                            else
                            {
                                string str8 = row1["CD_ITEM"].ToString().PadRight(10, ' ').Trim();
                                stringBuilder.AppendLine(str8);
                                flag1 = true;
                            }
                        }
                    }
                    if (flag1)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                    }
                    if (!flag3)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 창고에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str2.ToString());
                    }
                    if (!flag4)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 PJT에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str3.ToString());
                    }
                    if (!flag5)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 거래처에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str4.ToString());
                    }
                    if (!flag6)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 납품처에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str5.ToString());
                    }
                    this._flex.Redraw = true;
                    this._flex.Select(this._flex.DataTable.Rows.Count, this._flex.Cols.Fixed);
                    this._flex.IsDataChanged = true;
                    this.Page_DataChanged(null, null);
                    this.btn삭제.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool ChkData_SL(string cd_sl, ref string p_nm_sl)
        {
            if (this._dt_sl == null || this._dt_sl.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_sl.Select("CD_SL = '" + cd_sl + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_nm_sl = D.GetString(dataRowArray[0]["NM_SL"]);
            return true;
        }

        private bool ChkData_PJT(string cd_pjt, ref string[] p_nm_pjt)
        {
            if (this._dt_pjt == null || this._dt_pjt.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_pjt.Select("NO_PROJECT = '" + cd_pjt + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_nm_pjt = new string[] { D.GetString(dataRowArray[0]["NM_PROJECT"]),
                                      D.GetString(dataRowArray[0]["FG_PJT1"]) };
            return true;
        }

        private bool ChkData_PARTNER(string cd_partner, ref string p_ln_partner)
        {
            if (this._dt_partner == null || this._dt_partner.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_partner.Select("CD_PARTNER = '" + cd_partner + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_ln_partner = D.GetString(dataRowArray[0]["LN_PARTNER"]);
            return true;
        }

        private bool ChkData_GI_PRT(string gi_partner, ref string p_ln_gi_prt)
        {
            if (this._dt_gi_prt == null || this._dt_gi_prt.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_gi_prt.Select("CD_PARTNER = '" + gi_partner + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_ln_gi_prt = D.GetString(dataRowArray[0]["LN_PARTNER"]);
            return true;
        }

        private void btn입고단가우선순위_Click(object sender, EventArgs e)
        {
            if (!this._flex.HasNormalRow && !this.bUmSetting)
                return;
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (!this.bUmSetting)
            {
                P_PU_UM_PRIORITIZE_SUB puUmPrioritizeSub = new P_PU_UM_PRIORITIZE_SUB(P_PU_ITR_REG_st.Default.PARAMETER_um, P_PU_ITR_REG_st.Default.PARAMETER_EX);
                if (puUmPrioritizeSub.ShowDialog() != DialogResult.OK)
                    return;
                P_PU_ITR_REG_st.Default.PARAMETER_um = puUmPrioritizeSub.Rtn_stting;
                P_PU_ITR_REG_st.Default.PARAMETER_EX = puUmPrioritizeSub.Rtn_stting_ex;
                P_PU_ITR_REG_st.Default.Save();
                str1 = puUmPrioritizeSub.Rtn_stting;
                DataTable rtnDt = puUmPrioritizeSub.Rtn_dt;
                str2 = puUmPrioritizeSub.Rtn_stting_ex;
                DataTable rtnDtEx = puUmPrioritizeSub.Rtn_dt_ex;
                this.Grid_um_apply(rtnDt, rtnDtEx);
            }
            else
            {
                P_PU_UM_PRIORITIZE_PO_SUB umPrioritizePoSub = new P_PU_UM_PRIORITIZE_PO_SUB(P_PU_ITR_REG_st.Default.ITR_UM_SETTING);
                if (umPrioritizePoSub.ShowDialog() != DialogResult.OK)
                    return;
                P_PU_ITR_REG_st.Default.ITR_UM_SETTING = umPrioritizePoSub.Rtn_stting;
                P_PU_ITR_REG_st.Default.Save();
            }
        }

        private void Grid_um_apply(DataTable dt_um_p, DataTable dt_ex_p)
        {
            try
            {
                string str1 = string.Empty;
                foreach (DataRow row in this._flex.DataTable.Rows)
                    str1 = str1 + row["CD_ITEM"] + "|";
                string str2 = Common.MultiString(dt_ex_p, "CODE", "|");
                DataSet dataSet = this._biz.Search_um_prioritize_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     D.GetString( this.dtp입고일.Text),
                                                                                     D.GetString(this.cbo공장.SelectedValue),
                                                                                     str1,
                                                                                     Global.SystemLanguage.MultiLanguageLpoint.ToString(),
                                                                                     str2 });
                if (dataSet.Tables[0].Rows.Count == 0 && dataSet.Tables[1].Rows.Count == 0 && dataSet.Tables[2].Rows.Count == 0 && dataSet.Tables[3].Rows.Count == 0 && dataSet.Tables[4].Rows.Count == 0)
                    return;
                foreach (DataRow row1 in this._flex.DataTable.Rows)
                {
                    decimal num = D.GetDecimal(row1["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row1["UNIT_PO_FACT"]);
                    foreach (DataRow row2 in dt_um_p.Rows)
                    {
                        if (D.GetString(row2["CODE"]) == "INV")
                        {
                            DataRow[] dataRowArray = dataSet.Tables[0].Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "'");
                            if (dataRowArray.Length > 0)
                            {
                                if (D.GetString(row2["MAXMIN"]) == "MAX")
                                {
                                    row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                    if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                    {
                                        row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                    row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                }
                                else
                                {
                                    row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                    if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                    {
                                        row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                    row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                }
                                row1["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * num);
                            }
                            if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                break;
                        }
                        else if (D.GetString(row2["CODE"]) == "IVL")
                        {
                            DataTable table = dataSet.Tables[1];
                            foreach (DataRow row3 in dt_ex_p.Rows)
                            {
                                DataRow[] dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "' AND  CD_EXCH = '" + D.GetString(row3["CODE"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row2["MAXMIN"]) == "MAX")
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    else
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * num);
                                }
                                if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                    break;
                            }
                            if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                break;
                        }
                        else if (D.GetString(row2["CODE"]) == "APRT")
                        {
                            DataTable table = dataSet.Tables[2];
                            foreach (DataRow row4 in dt_ex_p.Rows)
                            {
                                DataRow[] dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "' AND  CD_EXCH = '" + D.GetString(row4["CODE"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row2["MAXMIN"]) == "MAX")
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]);
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    else
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * num);
                                }
                                if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                    break;
                            }
                            if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                break;
                        }
                        else if (D.GetString(row2["CODE"]) == "POL")
                        {
                            DataTable table = dataSet.Tables[3];
                            foreach (DataRow row5 in dt_ex_p.Rows)
                            {
                                DataRow[] dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "' AND  CD_EXCH = '" + D.GetString(row5["CODE"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row2["MAXMIN"]) == "MAX")
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    else
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * num);
                                }
                                if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                    break;
                            }
                            if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                break;
                        }
                        else if (D.GetString(row2["CODE"]) == "PITEM")
                        {
                            DataTable table = dataSet.Tables[4];
                            foreach (DataRow row6 in dt_ex_p.Rows)
                            {
                                DataRow[] dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "' AND  CD_EXCH = '" + D.GetString(row6["CODE"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row2["MAXMIN"]) == "MAX")
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    else
                                    {
                                        row1["CD_EXCH"] = D.GetString(dataRowArray[0]["CD_EXCH"]);
                                        if (D.GetString(dataRowArray[0]["CD_EXCH"]) == "000")
                                        {
                                            row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                            row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                        }
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row1["QT_GOOD_INV"]));
                                    }
                                    row1["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * num);
                                }
                                if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                    break;
                            }
                            if (D.GetDecimal(row1["UM_EX"]) != 0M)
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

        private void btn환종적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y' AND CD_EXCH ='" + D.GetString(this.cbo환종.SelectedValue) + "'");
                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(this.cur환종.Text));
                        dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM"]) * D.GetDecimal(dataRow["QT_GOOD_INV"]));
                        dataRow["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]));
                        dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["QT_GOOD_INV"]));
                        dataRow["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["UNIT_PO_FACT"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn대체출고_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead() || !this.Check())
                    return;
                P_PU_CGI_APPLY_SUB pPuCgiApplySub = new P_PU_CGI_APPLY_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                                                          this.dtp입고일.Text,
                                                                                          D.GetString(this.cbo대체유형.SelectedValue) });
                if (pPuCgiApplySub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataRow[] returnDataRowArray = pPuCgiApplySub.ReturnDataRowArray;
                    this.대체출고적용_그리드행추가(ref returnDataRowArray);
                    this.Page_DataChanged(null, null);
                    this.btn삭제.Enabled = true;
                    this._flex.Row = this._flex.Rows.Fixed;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn대체출고2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead() || !this.Check())
                    return;
                P_PU_CGI_APPLY_N_SUB pPuCgiApplyNSub = new P_PU_CGI_APPLY_N_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                                                               this.dtp입고일.Text,
                                                                                               D.GetString(this.cbo대체유형.SelectedValue) });
                if (pPuCgiApplyNSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataRow[] returnDataRowArray = pPuCgiApplyNSub.ReturnDataRowArray;
                    this.대체출고적용_그리드행추가(ref returnDataRowArray);
                    this.Page_DataChanged(null, null);
                    this.btn삭제.Enabled = true;
                    this._flex.Row = this._flex.Rows.Fixed;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn요청적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead())
                    return;
                P_PU_REQ_OTH_ITR_SUB pPuReqOthItrSub = new P_PU_REQ_OTH_ITR_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                                                               this.dtp입고일.Text,
                                                                                               D.GetString(this.cbo대체유형.SelectedValue),
                                                                                               this.ctx수불형태.CodeValue,
                                                                                               this.ctx수불형태.CodeName }, this._flex.DataTable);
                if (pPuReqOthItrSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataRow[] returnDataRowArray = pPuReqOthItrSub.ReturnDataRowArray;
                    this.txt비고.Text = pPuReqOthItrSub.ReturnDc50_po;
                    this._header.CurrentRow["DC_RMK"] = pPuReqOthItrSub.ReturnDc50_po;
                    this.ctx거래처.CodeValue = returnDataRowArray[0]["CD_PARTNER"].ToString().Trim();
                    this.ctx거래처.CodeName = returnDataRowArray[0]["LN_PARTNER"].ToString().Trim();
                    this._header.CurrentRow["CD_PARTNER"] = this.ctx거래처.CodeValue;
                    this._header.CurrentRow["LN_PARTNER"] = this.ctx거래처.CodeName;
                    this.ctx요청부서.CodeValue = D.GetString(returnDataRowArray[0]["CD_DEPT"]);
                    this.ctx요청부서.CodeName = D.GetString(returnDataRowArray[0]["NM_DEPT"]);
                    this._header.CurrentRow["CD_DEPT_REQ"] = this.ctx요청부서.CodeValue;
                    this.대체출고적용_그리드행추가_요청(ref returnDataRowArray, pPuReqOthItrSub.bChk_Rmk);
                    this._header.CurrentRow["FG_TPIO"] = D.GetString(returnDataRowArray[0]["FG_GI"]);
                    this.cbo대체유형.SelectedValue = D.GetString(returnDataRowArray[0]["FG_GI"]);
                    this.Page_DataChanged(null, null);
                    this.btn삭제.Enabled = true;
                    this._flex.Row = this._flex.Rows.Fixed;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 대체출고적용_그리드행추가_요청(ref DataRow[] drs, bool bChk_Rmk)
        {
            decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
            foreach (DataRow dr in drs)
                this.그리드한행추가(dr, ++maxValue, "요청", bChk_Rmk);
            this.SETUM();
        }

        private void 대체출고적용_그리드행추가(ref DataRow[] drs)
        {
            decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
            foreach (DataRow dr in drs)
                this.그리드한행추가(dr, ++maxValue, "대체출고", false);
            this.SETUM();
        }

        private void btn관련시리얼정보_Click(object sender, EventArgs e)
        {
            DataTable dataTable = this._flex.DataTable;
            if (string.Compare(this.MNG_SERIAL, "Y") != 0 || dataTable == null)
                return;
            DataRow[] dataRowArray = dataTable.Select("NO_SERL = 'YES'", "");
            DataTable dt = dataTable.Clone();
            if (dataRowArray.Length > 0)
            {
                foreach (DataRow row in dataRowArray)
                    dt.ImportRow(row);
                new P_PU_SERL_SUB_R(dt)
                {
                    SetYnSave = "N"
                }.ShowDialog((IWin32Window)this);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.txt수불번호.Text != ""))
                    return;
                string fileCode = D.GetString((this.txt수불번호.Text + "_" + Global.MainFrame.LoginInfo.CompanyCode));
                new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, fileCode).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn업무공유_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.추가모드여부 || D.GetString(this._flex["ID_MEMO"]) == string.Empty)
                    return;
                object[] objArray = new object[] { "A11",
                                                   "",
                                                   Global.MainFrame.LoginInfo.CompanyCode,
                                                   D.GetString(this._flex["CD_PJT"]),
                                                   D.GetString(this._flex["CD_WBS"]),
                                                   D.GetString(this._flex["NO_SHARE"]),
                                                   D.GetString(this._flex["NO_ISSUE"]),
                                                   "04" };
                (new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flex["ID_MEMO"]), objArray)).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnBOM_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead() || !this.Check())
                    return;
                P_PU_GIREQ_BOM_SUB pPuGireqBomSub = new P_PU_GIREQ_BOM_SUB(D.GetString(this.cbo공장.SelectedValue), this.dtp입고일.Text);
                if (pPuGireqBomSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataTable dtReturn = pPuGireqBomSub.dt_return;
                    if (dtReturn == null && dtReturn.Rows.Count < 1)
                        return;
                    DataTable table;
                    if (Global.MainFrame.ServerKeyCommon.Contains("CHCN") || Global.MainFrame.ServerKeyCommon.Contains("SQL_"))
                        table = dtReturn.DefaultView.ToTable(true, "CD_MATL", "NM_ITEM_MATL", "STND_ITEM_MATL", "STND_DETAIL_ITEM_MATL", "UNIT_IM_MATL", "UNIT_PO", "UNIT_PO_FACT", "GRP_MFG", "NM_GRPMFG", "MAT_ITEM", "FG_SERNO_M", "PARTNER", "LN_PARTNER", "QT_ITEM");
                    else
                        table = dtReturn.DefaultView.ToTable(true, "CD_MATL", "NM_ITEM_MATL", "STND_ITEM_MATL", "STND_DETAIL_ITEM_MATL", "UNIT_IM_MATL", "UNIT_PO", "UNIT_PO_FACT", "GRP_MFG", "NM_GRPMFG", "MAT_ITEM", "FG_SERNO_M", "PARTNER", "LN_PARTNER");
                    foreach (DataRow row in table.Rows)
                    {
                        this.btn추가_Click(null, null);
                        this._flex["CD_ITEM"] = row["CD_MATL"];
                        this._flex["NM_ITEM"] = row["NM_ITEM_MATL"];
                        this._flex["STND_ITEM"] = row["STND_ITEM_MATL"];
                        this._flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM_MATL"];
                        this._flex["UNIT_IM"] = row["UNIT_IM_MATL"];
                        this._flex["UNIT_PO"] = row["UNIT_PO"];
                        this._flex["YN_LOT"] = row["FG_SERNO_M"].ToString() == "002" ? "YES" : "NO";
                        this._flex["NO_SERL"] = row["FG_SERNO_M"].ToString() == "003" ? "YES" : "NO";
                        this._flex["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                        this._flex["GRP_MFG"] = row["GRP_MFG"];
                        this._flex["NM_GRP_MFG"] = row["NM_GRPMFG"];
                        this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                        this._flex["PARTNER"] = row["PARTNER"];
                        this._flex["NM_PARTNER"] = row["LN_PARTNER"];
                        this._flex["FG_SERNO"] = !(D.GetString(row["FG_SERNO_M"]) == "002") ? (!(D.GetString(row["FG_SERNO_M"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
                        this._flex["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                        this._flex["UNIT_PO_FACT"] = (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"]));
                        this._flex["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        this._flex["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                        if (D.GetString(this.ctx창고.CodeValue) != string.Empty)
                        {
                            this._flex["CD_SL"] = D.GetString(this.ctx창고.CodeValue);
                            this._flex["NM_SL"] = D.GetString(this.ctx창고.CodeName);
                            this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                        }
                        decimal num;
                        if (Global.MainFrame.ServerKeyCommon.Contains("CHCN"))
                            num = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReturn.Compute("SUM(QT_ITEM_NET)", "CD_MATL ='" + D.GetString(row["CD_MATL"]) + "' AND QT_ITEM = " + D.GetDecimal(row["QT_ITEM"]))));
                        else
                            num = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReturn.Compute("SUM(QT_ITEM_NET)", "CD_MATL ='" + D.GetString(row["CD_MATL"]) + "'")));
                        this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, num / (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                        if (!this.bUmSetting && BASIC.GetMAEXC("계정대체입고등록-입고단가우선순위적용유무") == "000")
                        {
                            DataTable dataTable = this._biz.Search_um_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          this.ctx거래처.CodeValue,
                                                                                          D.GetString( this.dtp입고일.Text),
                                                                                          D.GetString(this._flex["CD_ITEM"]),
                                                                                          this.cbo단가유형.SelectedValue,
                                                                                          this.cbo입고단가적용.SelectedValue });
                            if (dataTable != null && dataTable.Rows.Count > 0)
                            {
                                this._flex["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]));
                                this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(this._flex["QT_GOOD_INV"]));
                                this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(this._flex["QT_GOOD_INV"]));
                                this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX_PSO"]) / (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"])));
                                this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX_PSO"]) / (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"])));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCC적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.DataTable == null || this.ctxCC.CodeName == "")
                    return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["CD_CC"] = this.ctxCC.CodeValue;
                        dataRow["NM_CC"] = this.ctxCC.CodeName;
                    }
                    this.ShowMessage("적용작업을완료하였습니다");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn프로젝트_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead())
                    return;
                P_PJT_WBS_CBS_SUB pPjtWbsCbsSub = new P_PJT_WBS_CBS_SUB("", "MM_QTIO");
                if (pPjtWbsCbsSub.ShowDialog() == DialogResult.OK)
                {
                    DataTable returnDt = pPjtWbsCbsSub.Return_dt;
                    if (returnDt == null)
                        return;
                    decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                    DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PJT_WBS_CBS_SUB");
                    this._flex.Redraw = false;
                    foreach (DataRow row in returnDt.Rows)
                    {
                        this._flex.Rows.Add();
                        int num = this._flex.Rows.Count - 1;
                        ++maxValue;
                        this._flex[num, "CD_ITEM"] = row["CD_MATL"];
                        this._flex[num, "NM_ITEM"] = row["NM_MATL"];
                        this._flex[num, "STND_ITEM"] = row["STND_ITEM"];
                        this._flex[num, "STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                        this._flex[num, "UNIT_IM"] = row["UNIT_IM"];
                        this._flex[num, "UNIT_PO"] = row["UNIT_PO"];
                        this._flex[num, "YN_LOT"] = row["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                        this._flex[num, "NO_SERL"] = row["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
                        this._flex[num, "CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                        this._flex[num, "GRP_MFG"] = row["GRP_MFG"];
                        this._flex[num, "NM_GRP_MFG"] = row["NM_GRP_MFG"];
                        this._flex[num, "MAT_ITEM"] = row["MAT_ITEM"];
                        this._flex[num, "PARTNER"] = row["PARTNER"];
                        this._flex[num, "NM_PARTNER"] = row["LN_PARTNER"];
                        this._flex[num, "FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
                        this._flex[num, "NM_QTIOTP"] = this.ctx수불형태.CodeName;
                        this._flex[num, "UNIT_PO_FACT"] = (D.GetDecimal(row["UNIT_PO_FACT_ITEM"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT_ITEM"]));
                        this._flex[num, "CD_PJT"] = row["NO_PROJECT"];
                        this._flex[num, "NM_PROJECT"] = row["NM_PROJECT"];
                        this._flex[num, "SEQ_PROJECT"] = row["SEQ_PROJECT"];
                        this._flex[num, "CD_UNIT"] = row["CD_ITEM"];
                        this._flex[num, "NM_UNIT"] = row["NM_ITEM"];
                        this._flex[num, "STND_UNIT"] = row["STND_ITEM_ITEM"];
                        this._flex[num, "NO_WBS"] = row["NO_WBS"];
                        this._flex[num, "NO_CBS"] = row["NO_CBS"];
                        this._flex[num, "CD_COST"] = row["CD_COST"];
                        this._flex[num, "NM_COST"] = row["NM_COST"];
                        this._flex[num, "NO_LINE_PJTBOM"] = row["NO_LINE_PJTBOM"];
                        if (this.ctx창고.CodeValue != string.Empty)
                        {
                            this._flex[num, "CD_SL"] = this.ctx창고.CodeValue;
                            this._flex[num, "NM_SL"] = this.ctx창고.CodeName;
                            decimal val = BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[num, "CD_SL"]), D.GetString(this._flex[num, "CD_ITEM"]), D.GetString(this._flex[num, "CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[num, "SEQ_PROJECT"]) : 0M);
                            this._flex[num, "QT_INV"] = D.GetDecimal(val);
                        }
                        this._flex[num, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_NEED"]));
                        this._flex[num, "QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_NEED"]) / D.GetDecimal(this._flex[num, "UNIT_PO_FACT"]));
                        this._flex[num, "CD_EXCH"] = "000";
                        this._flex[num, "RT_EXCH"] = 1;
                        this._flex[num, "NO_IO"] = this._header.CurrentRow["NO_IO"];
                        this._flex[num, "NO_IOLINE"] = maxValue;
                        this._flex[num, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                        this._flex[num, "CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                        this._flex[num, "DT_IO"] = this._header.CurrentRow["DT_IO"];
                        this._flex[num, "NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                        this._flex[num, "CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                        this._flex[num, "CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                        this._flex[num, "YN_AM"] = this._header.CurrentRow["YN_AM"];
                        this._flex[num, "FG_IO"] = this._header.CurrentRow["FG_IO"];
                        this._flex[num, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                        this._flex[num, "FG_PS"] = "1";
                        this._flex[num, "FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
                        Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, num);
                        this._flex.AddFinished();
                        this._flex.Col = this._flex.Cols.Fixed;
                        this._flex.Focus();
                    }
                    this._flex.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                int num = string.Compare(this.MNG_LOT, "Y") != 0 ? 1 : (this._flex.Cols[e.Col].Name == "QT_GOOD_INV" ? 0 : (!(this._flex.Cols[e.Col].Name == "QT_UNIT_MM") ? 1 : 0));
                e.Cancel = num == 0 && !this._flex.IsAddedRow(this._flex.Row, true);
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "S":
                        e.Cancel = false;
                        break;
                    case "CD_ITEM":
                        if (this._flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "UM":
                    case "AM":
                    case "DC_RMK":
                    case "DC_RMK1":
                    case "CD_PJT":
                        e.Cancel = false;
                        break;
                    case "SEQ_PROJECT":
                    case "NM_UNIT":
                    case "STND_UNIT":
                    case "NM_PROJECT":
                        e.Cancel = true;
                        break;
                    case "NO_CBS":
                        if (D.GetDecimal(this._flex["NO_LINE_PJTBOM"]) > 0M)
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!this._flex.AllowEditing || this._flex.GetData(e.Row, e.Col).ToString() == this._flex.EditData)
                    return;
                string targetValue = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                decimal num1 = D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]);
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "QT_GOOD_INV":
                        this._flex["QT_GOOD_INV"] = this._flex.CDecimal(editData);
                        decimal ld_good_inv = this._flex.CDecimal(editData);
                        this._flex["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this._flex["WEIGHT"]));
                        if (MA.ServerKey(false, "SAMWON"))
                        {
                            if (D.GetDecimal(this._flex[this._flex.Row, "QT_UNIT_MM"]) != 0M)
                                break;
                            if (D.GetDecimal(this._flex["NUM_USERDEF1"]) == 0M)
                                this._flex["NUM_USERDEF1"] = (D.GetDecimal(this._flex["WEIGHT"]) == 0M ? 0M : Math.Round(this._flex.CDecimal(editData) / D.GetDecimal(this._flex["WEIGHT"]), 0, MidpointRounding.AwayFromZero));
                        }
                        this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) / (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                        decimal ld_reject_inv1 = this._flex.CDecimal(this._flex["QT_REJECT_INV"]);
                        decimal ld_UM1 = this._flex.CDecimal(this._flex["UM_EX"].ToString());
                        this.SetAMValue(ld_good_inv, ld_reject_inv1, ld_UM1);
                        break;
                    case "QT_UNIT_MM":
                        this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData));
                        decimal num2 = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) * (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                        if (MA.ServerKey(false, "SAMWON"))
                        {
                            if (D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) != 0M)
                                break;
                            if (D.GetDecimal(this._flex["NUM_USERDEF1"]) == 0M)
                                this._flex["NUM_USERDEF1"] = (D.GetDecimal(this._flex["WEIGHT"]) == 0M ? 0M : Math.Round(this._flex.CDecimal(editData) / D.GetDecimal(this._flex["WEIGHT"]), 0, MidpointRounding.AwayFromZero));
                        }
                        this._flex["QT_GOOD_INV"] = num2;
                        this._flex["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(num2) * D.GetDecimal(this._flex["WEIGHT"]));
                        decimal ld_reject_inv2 = this._flex.CDecimal(this._flex["QT_REJECT_INV"]);
                        decimal ld_UM2 = this._flex.CDecimal(this._flex["UM_EX"].ToString());
                        this.SetAMValue(num2, ld_reject_inv2, ld_UM2);
                        break;
                    case "AM_EX":
                        decimal val1 = D.GetDecimal(editData);
                        decimal val1_1 = this._flex.CDecimal(this._flex["QT_GOOD_INV"].ToString()) + this._flex.CDecimal(this._flex["QT_REJECT_INV"]);
                        if (val1_1 != 0M)
                        {
                            decimal val2 = UDecimal.Getdivision(val1, val1_1);
                            this._flex[this._flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val2);
                            this._flex[this._flex.Row, "UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, val2 * num1);
                            this._flex[this._flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, val2);
                        }
                        this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, val1);
                        this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, val1);
                        break;
                    case "UM_EX":
                        this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(editData));
                        this._flex["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(editData) * num1);
                        this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flex.CDecimal(editData));
                        this._flex["UM_OLD"] = this._flex.CDecimal(targetValue);
                        this.SetAMValue(this._flex.CDecimal(this._flex["QT_GOOD_INV"].ToString()), this._flex.CDecimal(this._flex["QT_REJECT_INV"]), this._flex.CDecimal(editData));
                        break;
                    case "NUM_USERDEF1":
                        if (MA.ServerKey(false, "SAMWON") && D.GetDecimal(this._flex["QT_GOOD_INV"]) == 0M && D.GetDecimal(this._flex["QT_UNIT_MM"]) == 0M)
                        {
                            this._flex["QT_GOOD_INV"] = Math.Round(D.GetDecimal(this._flex["NUM_USERDEF1"]) * D.GetDecimal(this._flex["WEIGHT"]), 0, MidpointRounding.AwayFromZero);
                            this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_GOOD_INV"]) / (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                            this.SetAMValue(this._flex.CDecimal(editData), this._flex.CDecimal(this._flex["QT_REJECT_INV"]), this._flex.CDecimal(this._flex["UM_EX"].ToString()));
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetAMValue(Decimal ld_good_inv, decimal ld_reject_inv, decimal ld_UM)
        {
            this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, (ld_good_inv + ld_reject_inv) * ld_UM);
            this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, (ld_good_inv + ld_reject_inv) * ld_UM);
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Parameter.HelpID)
                {
                    case HelpID.P_USER:
                        if (this._flex.Cols[e.Col].Name == "CD_UNIT" && D.GetString(this._flex["CD_PJT"]) != string.Empty)
                            e.Parameter.P64_CODE4 = D.GetString(this._flex["CD_PJT"]);
                        if (this._flex.Cols[e.Col].Name == "NO_CBS")
                        {
                            if (D.GetDecimal(this._flex["NO_LINE_PJTBOM"]) > 0M)
                            {
                                e.Cancel = true;
                                break;
                            }
                            if (D.GetString(this._flex["CD_PJT"]) == string.Empty)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("프로젝트"));
                                e.Cancel = true;
                            }
                            e.Parameter.P61_CODE1 = "|";
                            e.Parameter.UserParams = D.GetString(this._flex["CD_PJT"]) + "|";
                            break;
                        }
                        break;
                    case HelpID.P_MA_PITEM_SUB1:
                        if (this._flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            break;
                        }
                        e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.Parameter.ResultMode = ResultMode.SlowMode;
                        break;
                    case HelpID.P_MA_SL_SUB:
                        if (this._flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            break;
                        }
                        e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.Parameter.ResultMode = ResultMode.FastMode;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void Menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem toolStripMenuItem))
                    return;
                switch (toolStripMenuItem.Name)
                {
                    case "파일생성":
                        this._flex.ExportToExcel(false, true, true);
                        break;
                    case "파일업로드":
                        this.엑셀업로드();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Result.HelpID)
                {
                    case HelpID.P_MA_PARTNER_SUB:
                        this.SETUMPARTNER();
                        break;
                    case HelpID.P_MA_PITEM_SUB1:
                        if (e.Result.DialogResult != DialogResult.OK)
                            return;
                        decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                        foreach (DataRow row in e.Result.Rows)
                        {
                            if (e.Result.Rows[0] != row)
                            {
                                this.그리드한행추가(row, ++maxValue, "품목", false);
                            }
                            else
                            {
                                if (row.Table.Columns.Contains("QT_ITEM_NET_ORIGIN"))
                                {
                                    this._flex["QT_GOOD_INV"] = row["QT_ITEM_NET"];
                                    this._flex["QT_GOOD_OLD"] = row["QT_ITEM_NET_ORIGIN"];
                                }
                                if (this.FG_LOAD == "2")
                                {
                                    this._flex["QT_GOOD_INV"] = row["QT_GOOD"];
                                    this._flex["NO_IO_MGMT"] = row["NO_BALANCE"];
                                }
                                this._flex["CD_ITEM"] = row["CD_ITEM"];
                                this._flex["NM_ITEM"] = row["NM_ITEM"];
                                this._flex["STND_ITEM"] = row["STND_ITEM"];
                                this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                                this._flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                                this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                                this._flex["UNIT_IM"] = row["UNIT_IM"];
                                this._flex["NO_IOLINE"] = maxValue;
                                this._flex["YN_LOT"] = row["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                                this._flex["NO_SERL"] = row["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
                                this._flex["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                                this._flex["UNIT_PO_FACT"] = (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"]));
                                this._flex["EN_ITEM"] = row["EN_ITEM"];
                                if (!this.bUmSetting && BASIC.GetMAEXC("계정대체입고등록-입고단가우선순위적용유무") == "000")
                                {
                                    DataTable dataTable = this._biz.Search_um_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  this.ctx거래처.CodeValue,
                                                                                                  D.GetString( this.dtp입고일.Text),
                                                                                                  D.GetString(row["CD_ITEM"]),
                                                                                                  this.cbo단가유형.SelectedValue,
                                                                                                  this.cbo입고단가적용.SelectedValue });
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        this._flex["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]));
                                        this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) / D.GetDecimal(this._flex["UNIT_PO_FACT"]));
                                        this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) / D.GetDecimal(this._flex["UNIT_PO_FACT"]));
                                    }
                                }
                                if (D.GetString(this.ctx창고.CodeValue) != string.Empty)
                                {
                                    this._flex["CD_SL"] = D.GetString(this.ctx창고.CodeValue);
                                    this._flex["NM_SL"] = D.GetString(this.ctx창고.CodeName);
                                    this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                                }
                                else
                                {
                                    this._flex["CD_SL"] = row["CD_SL"];
                                    this._flex["NM_SL"] = row["NM_SL"];
                                    if (D.GetString(row["CD_SL"]) != string.Empty)
                                        this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                                }
                                this._flex["NO_IO"] = this._header.CurrentRow["NO_IO"];
                                this._flex["NO_IOLINE"] = maxValue;
                                this._flex["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                                this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                                this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"];
                                this._flex["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                                this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                                this._flex["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                                this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
                                this._flex["CD_PJT"] = this.ctx프로젝트.CodeValue;
                                this._flex["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                                this._flex["FG_IO"] = this._header.CurrentRow["FG_IO"];
                                this._flex["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                this._flex["FG_PS"] = "1";
                                this._flex["FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
                                if (this._USER != null)
                                    this._USER.setApp(this._flex, ComUser.Flag.currentFocus, 0, this._USER.FG_PJT1);
                                this._flex["NM_ITEMGRP"] = row["GRP_ITEMNM"];
                                this._flex["FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
                                if (row.Table.Columns.Contains("NO_IO"))
                                {
                                    this._flex["NO_IO_MGMT_APPLY"] = row["NO_IO"];
                                    this._flex["NO_IOLINE_MGMT_APPLY"] = row["NO_IOLINE"];
                                }
                                this._flex["GRP_ITEM"] = row["GRP_ITEM"];
                                this._flex["GRP_MFG"] = row["GRP_MFG"];
                                this._flex["NM_GRP_MFG"] = row["NM_GRP_MFG"];
                                this._flex["WEIGHT"] = row["WEIGHT"];
                                this._flex["PARTNER"] = row["PARTNER"];
                                this._flex["NM_PARTNER"] = row["LN_PARTNER"];
                                this._flex["UNIT_PO"] = row["UNIT_PO"];
                                this._flex["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_GOOD_INV"]) * D.GetDecimal(row["WEIGHT"]));
                                if (Global.MainFrame.ServerKeyCommon.Contains("ISAAC"))
                                {
                                    this._flex["CD_PARTNER"] = row["PARTNER"];
                                    this._flex["LN_PARTNER"] = row["LN_PARTNER"];
                                }
                                if (this._flex.DataTable.Select("NO_IO_MGMT_APPLY <> '' AND NO_IO_MGMT_APPLY IS NOT NULL").Length != 0)
                                {
                                    this.btn추가.Enabled = false;
                                    this.btn대체출고.Enabled = true;
                                    this.btn대체출고2.Enabled = true;
                                }
                                else
                                {
                                    this.btn추가.Enabled = true;
                                    this.btn대체출고.Enabled = false;
                                    this.btn대체출고2.Enabled = false;
                                }
                            }
                        }
                        this.SETUM();
                        this.Page_DataChanged(null, null);
                        this.cbo공장.Enabled = false;
                        this.btn삭제.Enabled = true;
                        this._flex.Focus();
                        break;
                    case HelpID.P_MA_SL_SUB:
                        if (D.GetString(this._flex["CD_ITEM"]) != "")
                        {
                            this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.Result.CodeValue), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.Result.CodeValue), D.GetString(this._flex["CD_ITEM"]))));
                            break;
                        }
                        break;
                }
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_PJT":
                        if (Config.MA_ENV.PJT형여부 == "Y" && D.GetString(this._flex["CD_ITEM"]) != "")
                            this._flex["QT_INV"] = BASICPU.Item_PINVN_PJT(D.GetString(this.cbo공장.SelectedValue), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(e.Result.CodeValue), 0M);
                        if (this._USER != null)
                        {
                            this._USER.Grid_AfterCodeHelp(this._flex, e.Result.CodeValue);
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_SL_SUB:
                        if (this.cbo공장.SelectedValue == null || D.GetString(this.cbo공장.SelectedValue) == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                            this.cbo공장.Focus();
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                    case HelpID.P_PU_EJTP_SUB:
                        e.HelpParam.P61_CODE1 = "007|";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK)
                    return;
                DataRow[] rows = e.HelpReturn.Rows;
                if (!(sender is BpCodeTextBox bpCodeTextBox))
                    return;
                switch (e.HelpID)
                {
                    case HelpID.P_USER:
                        switch (e.ControlName)
                        {
                            case "bp_프로젝트":
                                if (Config.MA_ENV.YN_UNIT == "Y")
                                {
                                    this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
                                    this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
                                    this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
                                    this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
                                }
                                if (this._USER != null)
                                {
                                    this._USER.Control_AfterCodeHelp(this._flex, e.HelpReturn.CodeValue, this.cbo대체유형);
                                    break;
                                }
                                break;
                        }
                        break;
                    case HelpID.P_MA_EMP_SUB:
                        this._header.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"];
                        break;
                    case HelpID.P_MA_PARTNER_SUB:
                        if (this.FG_LOAD == "2")
                        {
                            this._flex.Redraw = false;
                            foreach (DataRow row in this._flex.DataTable.Rows)
                                row["CD_PARTNER"] = e.HelpReturn.Rows[0]["CD_PARTNER"];
                            this._flex.Redraw = true;
                            break;
                        }
                        break;
                    case HelpID.P_MA_SL_SUB:
                        this._flex.Redraw = false;
                        for (int recordIndex = 0; recordIndex < this._flex.DataView.Count; ++recordIndex)
                        {
                            this._flex.DataView[recordIndex]["NM_SL"] = e.CodeName;
                            this._flex.DataView[recordIndex]["CD_SL"] = e.CodeValue;
                        }
                        this._flex.Redraw = true;
                        break;
                    case HelpID.P_PU_EJTP_SUB:
                        if (rows[0]["FG_IO"].ToString() != "007")
                        {
                            this.ShowMessage("입력하신 입고형태( @[@])가 화면에서 지정할 수 없는 수불형태입니다. 다시 입력해주십시오", e.CodeName, e.CodeValue);
                            bpCodeTextBox.CodeValue = "";
                            bpCodeTextBox.CodeName = "";
                        }
                        this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                        this._header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];
                        this._header.CurrentRow["CD_QTIOTP"] = e.HelpReturn.Rows[0]["CD_QTIOTP"];
                        this._header.CurrentRow["NM_QTIOTP"] = e.HelpReturn.Rows[0]["NM_QTIOTP"];
                        this._header.CurrentRow["FG_IO"] = e.HelpReturn.Rows[0]["FG_IO"];
                        this._header.CurrentRow["YN_RETURN"] = "N";
                        if ((Global.MainFrame.ServerKeyCommon.Contains("GALAXIA") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL")) && D.GetString(e.HelpReturn.Rows[0]["TP_VARIATION"]) == "2")
                            this._header.CurrentRow["YN_RETURN"] = "Y";
                        if (this.FG_LOAD == "2")
                        {
                            this._flex.Redraw = false;
                            foreach (DataRow row in this._flex.DataTable.Rows)
                            {
                                row["YN_AM"] = this._header.CurrentRow["YN_AM"];
                                row["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                row["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                                row["FG_IO"] = this._header.CurrentRow["FG_IO"];
                                row["YN_RETURN"] = "N";
                            }
                            this._flex.Redraw = true;
                        }
                        this._biz.Set설정("회사코드", MA.Login.회사코드);
                        this._biz.Set설정("수불형태코드", this.ctx수불형태.CodeValue);
                        this._biz.Set설정("수불형태명", this.ctx수불형태.CodeName);
                        this._biz.Set설정("FG_IO", D.GetString(this._header.CurrentRow["FG_IO"]));
                        this._biz.Set설정("YN_AM", D.GetString(this._header.CurrentRow["YN_AM"]));
                        break;
                    case HelpID.P_SA_PROJECT_SUB:
                        if (this.FG_LOAD == "2")
                        {
                            this._flex.Redraw = false;
                            foreach (DataRow row in this._flex.DataTable.Rows)
                                row["CD_PJT"] = e.HelpReturn.Rows[0]["NO_PROJECT"];
                            this._flex.Redraw = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ctx창고.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 그리드한행추가(DataRow dr, decimal line, string strDiv, bool bChk_Rmk)
        {
            DataRow row = this._flex.DataTable.NewRow();
            if (dr.Table.Columns.Contains("QT_ITEM_NET_ORIGIN"))
            {
                row["QT_GOOD_INV"] = dr["QT_ITEM_NET"];
                row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dr["QT_ITEM_NET"]) / (D.GetDecimal(dr["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(dr["UNIT_PO_FACT"])));
                row["QT_GOOD_OLD"] = dr["QT_ITEM_NET_ORIGIN"];
            }
            else if (dr.Table.Columns.Contains("QT_IO"))
            {
                row["QT_GOOD_INV"] = dr["QT_IO"];
                row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dr["QT_IO"]) / (D.GetDecimal(dr["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(dr["UNIT_PO_FACT"])));
            }
            if (this.FG_LOAD == "2")
            {
                row["QT_GOOD_INV"] = dr["QT_GOOD"];
                row["NO_IO_MGMT"] = dr["NO_BALANCE"];
                row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dr["QT_GOOD"]) / (D.GetDecimal(dr["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(dr["UNIT_PO_FACT"])));
            }
            row["CD_ITEM"] = !(strDiv == "대체출고") ? dr["CD_ITEM"] : dr["CD_MATL"];
            row["NM_ITEM"] = dr["NM_ITEM"];
            row["STND_ITEM"] = dr["STND_ITEM"];
            if (dr.Table.Columns.Contains("CLS_ITEM"))
                row["CLS_ITEM"] = dr["CLS_ITEM"];
            if (dr.Table.Columns.Contains("STND_DETAIL_ITEM"))
                row["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM"];
            if (dr.Table.Columns.Contains("MAT_ITEM"))
                row["MAT_ITEM"] = dr["MAT_ITEM"];
            if (dr.Table.Columns.Contains("EN_ITEM"))
                row["EN_ITEM"] = dr["EN_ITEM"];
            row["UNIT_IM"] = dr["UNIT_IM"];
            row["YN_LOT"] = dr["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
            row["NO_SERL"] = dr["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
            row["NM_QTIOTP"] = this.ctx수불형태.CodeName;
            row["UNIT_PO_FACT"] = D.GetDecimal(dr["UNIT_PO_FACT"]);
            row["CD_PJT"] = this.ctx프로젝트.CodeValue;
            row["NM_PROJECT"] = this.ctx프로젝트.CodeName;
            if (this._USER != null)
            {
                if (strDiv == "품목")
                    this._USER.setApp(row, this._USER.FG_PJT1);
                if (strDiv == "요청")
                    this._USER.setApp(row, dr["FG_PJT1"].ToString());
            }
            if (strDiv == "자재" && Config.MA_ENV.PJT형여부 == "Y")
            {
                row["CD_PJT"] = D.GetString(dr["CD_PJT"]);
                row["NM_PROJECT"] = D.GetString(dr["NM_PROJECT"]);
                row["SEQ_PROJECT"] = D.GetDecimal(dr["SEQ_PROJECT"]);
                if (Config.MA_ENV.YN_UNIT == "Y")
                {
                    row["CD_UNIT"] = D.GetString(dr["CD_PJT_ITEM"]);
                    row["NM_UNIT"] = D.GetString(dr["NM_PJT_ITEM"]);
                    row["STND_UNIT"] = D.GetString(dr["PJT_ITEM_STND"]);
                }
            }
            if (strDiv == "요청")
            {
                row["CD_PJT"] = D.GetString(dr["CD_PJT"]);
                row["NM_PROJECT"] = D.GetString(dr["NM_PROJECT"]);
                row["SEQ_PROJECT"] = D.GetDecimal(dr["SEQ_PROJECT"]);
                if (Config.MA_ENV.YN_UNIT == "Y")
                {
                    row["CD_UNIT"] = D.GetString(dr["CD_UNIT"]);
                    row["NM_UNIT"] = D.GetString(dr["NM_UNIT"]);
                    row["STND_UNIT"] = D.GetString(dr["STND_UNIT"]);
                }
                row["CD_CC"] = D.GetString(dr["CD_CC"]);
                row["NM_CC"] = D.GetString(dr["NM_CC"]);
                if (bChk_Rmk)
                    row["DC_RMK"] = dr["DC_RMK"];
            }
            if (D.GetString(this.ctx창고.CodeValue) != string.Empty)
            {
                row["CD_SL"] = D.GetString(this.ctx창고.CodeValue);
                row["NM_SL"] = D.GetString(this.ctx창고.CodeName);
            }
            else if (strDiv != "대체출고")
            {
                row["CD_SL"] = dr["CD_SL"];
                row["NM_SL"] = dr["NM_SL"];
            }
            if (strDiv != "품목" && strDiv != "대체출고")
            {
                row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dr["UM"]));
                row["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dr["AM"]));
                if (this.FG_LOAD == "2")
                {
                    row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dr["UM"]) * D.GetDecimal(dr["QT_GOOD"]));
                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dr["UM"]));
                    row["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dr["UM"]) * D.GetDecimal(dr["UNIT_PO_FACT"]));
                }
                if (strDiv == "요청")
                {
                    row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dr["AM_EX"]));
                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dr["UM_EX"]));
                    row["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dr["UM_EX_PSO"]));
                    row["NO_ISURCV"] = D.GetString(dr["NO_GIREQ"]);
                    row["NO_ISURCVLINE"] = D.GetDecimal(dr["NO_LINE"]);
                }
            }
            decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]), D.GetString(row["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(row["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]));
            row["QT_INV"] = D.GetDecimal(val);
            row["NO_IO"] = this._header.CurrentRow["NO_IO"];
            row["NO_IOLINE"] = line;
            row["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
            row["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
            row["DT_IO"] = this._header.CurrentRow["DT_IO"];
            row["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
            row["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
            row["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
            row["YN_AM"] = this._header.CurrentRow["YN_AM"];
            row["FG_IO"] = this._header.CurrentRow["FG_IO"];
            row["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
            row["FG_PS"] = "1";
            row["FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
            if (dr.Table.Columns.Contains("FG_SERNO"))
                row["FG_SERNO"] = !(D.GetString(dr["FG_SERNO"]) == "002") ? (!(D.GetString(dr["FG_SERNO"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
            if (dr.Table.Columns.Contains("GRP_ITEMNM"))
                row["NM_ITEMGRP"] = dr["GRP_ITEMNM"];
            if (!this.bUmSetting && BASIC.GetMAEXC("계정대체입고등록-입고단가우선순위적용유무") == "000" && strDiv != "요청")
            {
                DataTable dataTable = this._biz.Search_um_item(new object[6]
                {
           Global.MainFrame.LoginInfo.CompanyCode,
           this.ctx거래처.CodeValue,
           D.GetString( this.dtp입고일.Text),
           D.GetString(dr["CD_ITEM"]),
          this.cbo단가유형.SelectedValue,
          this.cbo입고단가적용.SelectedValue
                });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (this.FG_LOAD == "2")
                    {
                        if (D.GetDecimal(dr["UM"]) < 1M)
                        {
                            row["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]));
                            row["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(dr["QT_GOOD"]));
                            row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(dr["QT_GOOD"]));
                        }
                    }
                    else
                    {
                        row["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]));
                        row["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(row["QT_GOOD_INV"]));
                        row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(row["QT_GOOD_INV"]));
                        if (dr.Table.Columns.Contains("QT_ITEM_NET_ORIGIN"))
                        {
                            row["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(dr["QT_ITEM_NET"]));
                            row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]) * D.GetDecimal(dr["QT_ITEM_NET"]));
                        }
                    }
                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PSO"]) / (D.GetDecimal(dr["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(dr["UNIT_PO_FACT"])));
                    row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PSO"]) / (D.GetDecimal(dr["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(dr["UNIT_PO_FACT"])));
                }
            }
            if (dr.Table.Columns.Contains("NO_IO"))
            {
                row["NO_IO_MGMT_APPLY"] = dr["NO_IO"];
                row["NO_IOLINE_MGMT_APPLY"] = dr["NO_IOLINE"];
            }
            if (dr.Table.Columns.Contains("GRP_ITEM"))
                row["GRP_ITEM"] = dr["GRP_ITEM"];
            if (dr.Table.Columns.Contains("WEIGHT"))
            {
                row["WEIGHT"] = dr["WEIGHT"];
                row["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["QT_GOOD_INV"]) * D.GetDecimal(dr["WEIGHT"]));
            }
            if (dr.Table.Columns.Contains("GRP_MFG"))
                row["GRP_MFG"] = dr["GRP_MFG"];
            if (dr.Table.Columns.Contains("NM_GRP_MFG"))
                row["NM_GRP_MFG"] = dr["NM_GRP_MFG"];
            if (dr.Table.Columns.Contains("PARTNER"))
                row["PARTNER"] = dr["PARTNER"];
            if (dr.Table.Columns.Contains("LN_PARTNER"))
                row["NM_PARTNER"] = dr["LN_PARTNER"];
            if (dr.Table.Columns.Contains("UNIT_PO"))
                row["UNIT_PO"] = dr["UNIT_PO"];
            if (Global.MainFrame.ServerKeyCommon.Contains("ISAAC"))
            {
                if (dr.Table.Columns.Contains("PARTNER"))
                    row["CD_PARTNER"] = dr["PARTNER"];
                if (dr.Table.Columns.Contains("LN_PARTNER"))
                    row["LN_PARTNER"] = dr["LN_PARTNER"];
            }
            if (this._flex.DataTable.Select("NO_IO_MGMT_APPLY <> '' AND NO_IO_MGMT_APPLY IS NOT NULL").Length != 0)
            {
                this.btn추가.Enabled = false;
                this.btn대체출고.Enabled = true;
                this.btn대체출고2.Enabled = true;
            }
            else
            {
                this.btn추가.Enabled = true;
                this.btn대체출고.Enabled = false;
                this.btn대체출고2.Enabled = false;
            }
            this._flex.DataTable.Rows.Add(row);
        }

        protected override bool IsChanged() => base.IsChanged() || this.헤더변경여부;

        private bool CheckFieldHead()
        {
            try
            {
                if (this.dtp입고일.MaskEditBox.ClipText == "")
                {
                    this.dtp입고일.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl입고일.Text);
                    return false;
                }
                if (this.ctx수불형태.CodeValue == "")
                {
                    this.ctx수불형태.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수불형태.Text);
                    return false;
                }
                if (D.GetString(this.cbo공장.SelectedValue) == "")
                {
                    this.cbo공장.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    return false;
                }
                if (this.ctx담당자.CodeValue == "")
                {
                    this.ctx담당자.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
                    return false;
                }
                if (!(D.GetString(this.cbo대체유형.SelectedValue) == ""))
                    return true;
                this.cbo대체유형.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl대체유형.Text);
                return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }

        private void SetButtonState(bool pb_state)
        {
            this.dtp입고일.Enabled = this.ctx수불형태.Enabled = this.ctx프로젝트.Enabled = this.cbo공장.Enabled = this.ctx거래처.Enabled = this.ctx담당자.Enabled = this.txt수불번호.Enabled = this.cbo대체유형.Enabled = this.ctx요청부서.Enabled = pb_state;
            if (this.FG_LOAD == "2" && this.추가모드여부)
                this.ctx수불형태.Enabled = this.ctx프로젝트.Enabled = this.ctx거래처.Enabled = this.cbo대체유형.Enabled = true;
            if (!(BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000021") == "100"))
                return;
            this.ctx거래처.Enabled = true;
        }

        private void cbo대체유형_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                foreach (DataRow dataRow in this._flex.DataTable.Select())
                    dataRow["FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private decimal Item_PINVN(string 공장, string 창고, string 품목) => BASICPU.Item_PINVN(공장, 창고, 품목);

        private void tb_CD_QTIO_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is BpCodeTextBox bpCodeTextBox) || !(bpCodeTextBox.CodeValue == ""))
                    return;
                this._header.CurrentRow["FG_IO"] = "";
                this._header.CurrentRow["FG_TRANS"] = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 수불형태Default셋팅()
        {
			if (this._biz.Get설정("회사코드") != MA.Login.회사코드)
				return;
			string str1 = this._biz.Get설정("수불형태코드");
            string codename = this._biz.Get설정("수불형태명");
            if (str1 == string.Empty)
                return;
            string str2 = this._biz.Get설정("YN_AM");
            if (str2 == string.Empty)
            {
                this.ctx수불형태.SetCode(string.Empty, string.Empty);
                this.ctx수불형태.Focus();
            }
            else
            {
                this.ctx수불형태.SetCode(str1, codename);
                this._header.CurrentRow["FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
                this._header.CurrentRow["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
                this._header.CurrentRow["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                this._header.CurrentRow["YN_AM"] = str2;
                this._header.CurrentRow["FG_IO"] = this._biz.Get설정("FG_IO");
                DataTable fgIo = this._biz.Get_FG_IO(str1);
                this._header.CurrentRow["YN_RETURN"] = "N";
                if ((Global.MainFrame.ServerKeyCommon.Contains("GALAXIA") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL")) && D.GetString(fgIo.Rows[0]["TP_VARIATION"]) == "2")
                    this._header.CurrentRow["YN_RETURN"] = "Y";
                if (!(str1 != "") || !(D.GetString(this._header.CurrentRow["FG_IO"]).Trim() == "") || fgIo.Rows.Count != 1)
                    return;
                this._header.CurrentRow["FG_IO"] = D.GetString(fgIo.Rows[0][0]);
                if ((Global.MainFrame.ServerKeyCommon.Contains("GALAXIA") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL")) && D.GetString(fgIo.Rows[0]["TP_VARIATION"]) == "2")
                    this._header.CurrentRow["YN_RETURN"] = "Y";
            }
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            object[] args = e.Args;
            if (e.Args[0].GetType() == typeof(PageBaseConst.CallType))
            {
                this.rt_no_io = this._biz.GetNoIo(D.GetString(args[1]))[0];
                this.rt_cd_plant = this._biz.GetNoIo(D.GetString(args[1]))[1];
                this.fg_gubun = "PMS";
                this.화면이동();
            }
            else
            {
                this.rt_no_io = D.GetString(args[0]);
                this.rt_cd_plant = D.GetString(args[1]);
                this.화면이동();
            }
        }

        private void SETUM()
        {
            try
            {
                if (this.bUmSetting)
                {
                    string empty = string.Empty;
                    string val1 = D.GetString(this.ctx거래처.CodeValue);
                    string maexcMenu = BASIC.GetMAEXC_Menu("P_PU_ITR_REG", "PU_A00000021");
                    string str = Common.MultiString(this._flex.DataTable, "ISNULL(UM_EX ,0)= 0", "CD_ITEM", "|");
                    if (maexcMenu == "100")
                        val1 = Common.MultiString(this._flex.DataTable, "ISNULL(UM_EX ,0)= 0", "CD_PARTNER", "|");
                    if (D.GetString(val1) == "")
                        val1 = "|";
                    DataSet dataSet = this._biz.Search_um_prioritize_item2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          D.GetString( this.dtp입고일.Text),
                                                                                          D.GetString(this.cbo공장.SelectedValue),
                                                                                          str,
                                                                                          this.cbo단가유형.SelectedValue,
                                                                                          val1,
                                                                                          Global.SystemLanguage.MultiLanguageLpoint.ToString() });
                    if (P_PU_ITR_REG_st.Default.ITR_UM_SETTING == string.Empty || str == string.Empty)
                        return;
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("S", typeof(string));
                    dataTable.Columns.Add("NO", typeof(string));
                    dataTable.Columns.Add("CODE", typeof(string));
                    dataTable.Columns.Add("NAME", typeof(string));
                    dataTable.Columns.Add("MAXMIN", typeof(string));
                    string itrUmSetting = P_PU_ITR_REG_st.Default.ITR_UM_SETTING;
                    char[] chArray = new char[1] { '&' };
                    foreach (object val2 in (Array)itrUmSetting.Split(chArray))
                    {
                        string[] strArray = D.GetString(val2).Split('%');
                        DataRow row = dataTable.NewRow();
                        row["S"] = strArray[0];
                        if (row["S"].ToString() == "Y")
                        {
                            row["CODE"] = strArray[2];
                            row["NAME"] = strArray[3];
                            row["MAXMIN"] = strArray[4];
                            if (row["CODE"].ToString() == "INV")
                                row["NO"] = "0";
                            else if (row["CODE"].ToString() == "IVL")
                                row["NO"] = "1";
                            else if (row["CODE"].ToString() == "APRT")
                                row["NO"] = "2";
                            else if (row["CODE"].ToString() == "POL")
                                row["NO"] = "3";
                            else if (row["CODE"].ToString() == "SPRT")
                                row["NO"] = "4";
                            else if (row["CODE"].ToString() == "TPUM")
                                row["NO"] = "5";
                            else if (row["CODE"].ToString() == "PITEM")
                                row["NO"] = "6";
                            else if (row["CODE"].ToString() == "PITEM")
                                row["NO"] = "7";
                            else if (row["CODE"].ToString() == "TPUMPU")
                                row["NO"] = "8";
                            dataTable.Rows.Add(row);
                        }
                    }
                    foreach (DataRow row1 in this._flex.DataTable.Rows)
                    {
                        foreach (DataRow row2 in dataTable.Rows)
                        {
                            if (!(D.GetDecimal(row1["UM_EX"]) != 0M))
                            {
                                DataTable table = dataSet.Tables[D.GetInt(row2["NO"])];
                                if (table != null && table.Rows.Count != 0)
                                {
                                    DataRow[] dataRowArray;
                                    if (row2["CODE"].ToString() == "APRT" && maexcMenu == "100")
                                        dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "' AND CD_PARTNER = '" + D.GetString(row1["CD_PARTNER"]) + "'");
                                    else
                                        dataRowArray = table.Select("CD_ITEM ='" + D.GetString(row1["CD_ITEM"]) + "'");
                                    if (dataRowArray != null && dataRowArray.Length > 0)
                                    {
                                        decimal val3 = !(D.GetString(row2["MAXMIN"]) == "MAX") ? D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) : D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]);
                                        row1["UM"] = Unit.원화단가(DataDictionaryTypes.PU, val3);
                                        row1["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val3);
                                        row1["UM_EX_PSO"] = !(D.GetString(row2["CODE"]) == "SPRT") ? Unit.수량(DataDictionaryTypes.PU, val3 * D.GetDecimal(row1["UNIT_PO_FACT"])) : Unit.수량(DataDictionaryTypes.PU, val3 * (D.GetDecimal(table.Rows[0]["UNIT_SU_FACT"]) == 0M ? 1M : D.GetDecimal(table.Rows[0]["UNIT_SU_FACT"])));
                                        row1["AM"] = Unit.원화금액(DataDictionaryTypes.PU, val3 * (D.GetDecimal(row1["QT_GOOD_INV"]) + D.GetDecimal(row1["QT_REJECT_INV"])));
                                        row1["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, val3 * (D.GetDecimal(row1["QT_GOOD_INV"]) + D.GetDecimal(row1["QT_REJECT_INV"])));
                                    }
                                    if (D.GetDecimal(row1["UM_EX"]) != 0M)
                                        break;
                                }
                            }
                            else
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

        private void SETUMPARTNER()
        {
            try
            {
                if (!this.bUmSetting)
                    return;
                IEnumerator enumerator = P_PU_ITR_REG_st.Default.ITR_UM_SETTING.Split('&').GetEnumerator();
                int num = 0;
                bool flag = false;
                while (enumerator.MoveNext())
                {
                    ++num;
                    string[] strArray = D.GetString(enumerator.Current).Split('%');
                    if (strArray[2].ToString() == "APRT" && strArray[0] == "Y")
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    DataTable dataTable = this._biz.Search_um_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  this._flex["CD_PARTNER"].ToString(),
                                                                                  D.GetString( this.dtp입고일.Text),
                                                                                  D.GetString(this._flex["CD_ITEM"]),
                                                                                  "",
                                                                                  "003" });
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        this._flex["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataTable.Rows[0]["UM_ITEM"]));
                        this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX_PSO"]) / (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                        this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * D.GetDecimal(this._flex["QT_GOOD_INV"]));
                        this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["AM"]));
                        this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX_PSO"]) / (D.GetDecimal(this._flex["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex["UNIT_PO_FACT"])));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnOnly1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string MULTI_GRPITEM = "'";
                DataTable table = this._flex.DataTable.DefaultView.ToTable(true, "GRP_ITEM");
                int count = table.Rows.Count;
                for (int index = 0; index < count; ++index)
                {
                    MULTI_GRPITEM = MULTI_GRPITEM + D.GetString(table.Rows[index]["GRP_ITEM"]) + "'";
                    if (index != count - 1)
                        MULTI_GRPITEM += ",'";
                }
                DataTable amWegint = this._biz.GET_AmWegint(MULTI_GRPITEM);
                if (amWegint == null || amWegint.Rows.Count == 0)
                    return;
                foreach (DataRow row in this._flex.DataTable.Rows)
                {
                    if (!(D.GetString(row["GRP_ITEM"]) == string.Empty) && !(D.GetDecimal(row["QT_GOOD_INV"]) == 0M))
                    {
                        DataRow[] dataRowArray = amWegint.Select("GRP_ITEM = '" + D.GetString(row["GRP_ITEM"]) + "'");
                        if (dataRowArray != null && dataRowArray.Length != 0)
                        {
                            row["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["DC_1"]) * D.GetDecimal(row["WEIGHT"]) * D.GetDecimal(row["QT_GOOD_INV"]));
                            row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]));
                            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_GOOD_INV"]));
                            row["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]) * D.GetDecimal(row["UNIT_PO_FACT"]));
                            row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable makeXML_DONG_AH(DataTable dtp)
        {
            dtp.Columns.Add("QT_LOTSIZE", typeof(decimal));
            dtp.Columns.Add("DONGAH_BARCODE2", typeof(string));
            DataTable dataTable = dtp.Clone();
            decimal num;
            for (int index = 0; index < dtp.Rows.Count; ++index)
            {
                if (D.GetString(dtp.Rows[index]["YN_LOT"]) == "YES")
                {
                    if (D.GetDecimal(dtp.Rows[index]["QT_IO_LOT_REMAIN"]) > D.GetDecimal(dtp.Rows[index]["LOTSIZE"]) && D.GetDecimal(dtp.Rows[index]["LOTSIZE"]) > 0M)
                    {
                        dtp.Rows[index]["QT_LOTSIZE"] = D.GetDecimal(dtp.Rows[index]["LOTSIZE"]);
                        dtp.Rows[index]["QT_IO_LOT_REMAIN"] = (D.GetDecimal(dtp.Rows[index]["QT_IO_LOT_REMAIN"]) - D.GetDecimal(dtp.Rows[index]["LOTSIZE"]));
                        DataRow row = dtp.Rows[index];
                        string[] strArray1 = new string[] { D.GetString(dtp.Rows[index]["CD_ITEM"]),
                                                            "/",
                                                            null,
                                                            null,
                                                            null };
                        string[] strArray2 = strArray1;
                        num = D.GetDecimal(dtp.Rows[index]["QT_LOTSIZE"]);
                        string str1 = num.ToString("##############0.####");
                        strArray2[2] = str1;
                        strArray1[3] = "/";
                        strArray1[4] = D.GetString(dtp.Rows[index]["NO_LOT"]);
                        string str2 = string.Concat(strArray1);
                        row["DONGAH_BARCODE2"] = str2;
                        dataTable.ImportRow(dtp.Rows[index]);
                        --index;
                    }
                    else
                    {
                        dtp.Rows[index]["QT_LOTSIZE"] = D.GetDecimal(dtp.Rows[index]["QT_IO_LOT_REMAIN"]);
                        DataRow row = dtp.Rows[index];
                        string[] strArray3 = new string[] { D.GetString(dtp.Rows[index]["CD_ITEM"]),
                                                            "/",
                                                            null,
                                                            null,
                                                            null };
                        string[] strArray4 = strArray3;
                        num = D.GetDecimal(dtp.Rows[index]["QT_LOTSIZE"]);
                        string str3 = num.ToString("##############0.####");
                        strArray4[2] = str3;
                        strArray3[3] = "/";
                        strArray3[4] = D.GetString(dtp.Rows[index]["NO_LOT"]);
                        string str4 = string.Concat(strArray3);
                        row["DONGAH_BARCODE2"] = str4;
                        dataTable.ImportRow(dtp.Rows[index]);
                    }
                }
                else if (D.GetDecimal(dtp.Rows[index]["QT_IO_REMAIN"]) > D.GetDecimal(dtp.Rows[index]["LOTSIZE"]) && D.GetDecimal(dtp.Rows[index]["LOTSIZE"]) > 0M)
                {
                    dtp.Rows[index]["QT_LOTSIZE"] = D.GetDecimal(dtp.Rows[index]["LOTSIZE"]);
                    dtp.Rows[index]["QT_IO_REMAIN"] = (D.GetDecimal(dtp.Rows[index]["QT_IO_REMAIN"]) - D.GetDecimal(dtp.Rows[index]["LOTSIZE"]));
                    DataRow row = dtp.Rows[index];
                    string str5 = D.GetString(dtp.Rows[index]["CD_ITEM"]);
                    num = D.GetDecimal(dtp.Rows[index]["QT_LOTSIZE"]);
                    string str6 = num.ToString("##############0.####");
                    string str7 = str5 + "/" + str6 + "/";
                    row["DONGAH_BARCODE2"] = str7;
                    dataTable.ImportRow(dtp.Rows[index]);
                    --index;
                }
                else
                {
                    dtp.Rows[index]["QT_LOTSIZE"] = D.GetDecimal(dtp.Rows[index]["QT_IO_REMAIN"]);
                    DataRow row = dtp.Rows[index];
                    string str8 = D.GetString(dtp.Rows[index]["CD_ITEM"]);
                    num = D.GetDecimal(dtp.Rows[index]["QT_LOTSIZE"]);
                    string str9 = num.ToString("##############0.####");
                    string str10 = str8 + "/" + str9 + "/";
                    row["DONGAH_BARCODE2"] = str10;
                    dataTable.ImportRow(dtp.Rows[index]);
                }
            }
            dataTable.Columns.Remove("QT_IO_REMAIN");
            return dataTable;
        }

        private bool 추가모드여부 => this._header.JobMode == JobModeEnum.추가후수정 && this.txt수불번호.Text == string.Empty;

        private bool 헤더변경여부
        {
            get
            {
                bool 헤더변경여부 = this._header.GetChanges() != null;
                if (헤더변경여부 && this._header.JobMode == JobModeEnum.추가후수정 && !this._flex.HasNormalRow)
                    헤더변경여부 = false;
                return 헤더변경여부;
            }
        }

        private bool 등록여부(string 품목코드)
        {
            for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
            {
                if (this._flex[row, "CD_ITEM"].Equals(품목코드))
                    return true;
            }
            return false;
        }

        private void 화면이동()
        {
            DataSet dataSet = this._biz.Search(this.rt_no_io, this.FG_LOAD, this.rt_cd_plant);
            if (dataSet.Tables[0].Rows.Count < 1)
            {
                this.ShowMessage(공통메세지._이가존재하지않습니다, this.rt_no_io);
            }
            else
            {
                this._header.SetDataTable(dataSet.Tables[0]);
                this._flex.Binding = dataSet.Tables[1];
                this.ctx창고.CodeValue = this._flex.DataTable.Rows[0]["CD_SL"].ToString();
                this.ctx창고.CodeName = this._flex.DataTable.Rows[0]["NM_SL"].ToString();
                this.ctx프로젝트.CodeValue = dataSet.Tables[0].Rows[0]["CD_PJT"].ToString();
                this.ctx프로젝트.CodeName = dataSet.Tables[0].Rows[0]["NM_PROJECT"].ToString();
                if (D.GetString(this._header.CurrentRow["FG_TPIO"]) == "")
                {
                    this._header.CurrentRow["FG_TPIO"] = "000";
                    this.cbo대체유형.SelectedIndex = 0;
                }
                foreach (DataRow dataRow in this._flex.DataTable.Select())
                    dataRow["FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_TPIO"]);
                if (this._flex.DataTable.Select("NO_IO_MGMT_APPLY <> '' AND NO_IO_MGMT_APPLY IS NOT NULL").Length != 0)
                {
                    this.btn추가.Enabled = false;
                    this.btn대체출고.Enabled = true;
                    this.btn대체출고2.Enabled = true;
                }
                else
                {
                    this.btn추가.Enabled = true;
                    this.btn대체출고.Enabled = false;
                    this.btn대체출고2.Enabled = false;
                }
            }
        }
    }
}
