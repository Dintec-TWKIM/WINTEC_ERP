using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PR;
using Duzon.ERPU.Utils;
using DX;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_ROUT_REG : PageBase
    {
        private P_CZ_PR_ROUT_REG_BIZ _biz = new P_CZ_PR_ROUT_REG_BIZ();
        private DataTable dt_Paste;
        private DataTable dt_PasteD;
        private bool bGridrowChanging = false;
        private string str공정경로복사 = BASIC.GetMAEXC("공정경로등록_공정경로복사");
        private string strROUTNO자동입력여부 = BASIC.GetMAEXC("공정경로등록_ROUTNO자동입력여부");
        private string s조달구분통제 = BASIC.GetMAEXC_Menu("P_PR_ROUT_REG_NEW", "PR_A00000001");
        private Hashtable htDetailQueryCollection;
        private P_CZ_PR_ROUT_REG.stDetailQuery st;
        private bool b_StateMode = false;
        private DataRow dr측정항목;

        public struct stDetailQuery
        {
            public string strItem;
            public string strNO_OPPATH;
        }

        public bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");

        public P_CZ_PR_ROUT_REG()
        {
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.htDetailQueryCollection = new Hashtable();
            this.st = new P_CZ_PR_ROUT_REG.stDetailQuery();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex공정경로, this._flex공정, this._flex작업지침서 };
            this._flex품목.DetailGrids = new FlexGrid[] { this._flex공정경로 };
            this._flex공정.DetailGrids = new FlexGrid[] { this._flex작업지침서, this._flex측정항목, this._flex설비 };

            #region 품목
            this._flex품목.BeginSetting(1, 1, false);

            this._flex품목.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex품목.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flex품목.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex품목.SetCol("STND_ITEM", "규격", 70, false);
            this._flex품목.SetCol("UNIT_MO", "단위", 70, false);
            this._flex품목.SetCol("TP_PROC", "조달구분명", 70, false);
            this._flex품목.SetCol("CLS_ITEM", "품목계정명", 70, false);
            this._flex품목.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flex품목.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex품목.SetCol("NM_ITEMGRP", "품목군", 100, false);
            this._flex품목.SetCol("CLS_L", "대분류", 100, false);
            this._flex품목.SetCol("CLS_M", "중분류", 100, false);
            this._flex품목.SetCol("CLS_S", "소분류", 100, false);
            this._flex품목.SetCol("GRP_MFG", "제품군", false);
            this._flex품목.SetCol("DT_BOM", "BOM등록일", false);
            this._flex품목.SetCol("QT_INSP", "측정등록", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목.SetCol("QT_FILE", "지첨서등록", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목.SetCol("DC_RMK1", "비고", 100, true);

            this._flex품목.Cols["DT_BOM"].Format = "####/##/##";
            this._flex품목.SetStringFormatCol("DT_BOM");
            this._flex품목.SetDummyColumn("CHK");

            this._flex품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 공정경로유형
            this._flex공정경로.BeginSetting(1, 1, true);

            this._flex공정경로.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex공정경로.SetCol("TP_OPPATH", "공정경로유형", 90, 3, false);
            this._flex공정경로.SetCol("NO_OPPATH", "ROUTNO", 90, 3, true);
            this._flex공정경로.SetCol("DC_OPPATH", "경로설명", 90, 100, true);
            this._flex공정경로.SetCol("NO_EMP_WRIT", "작성자", 80, true);
            this._flex공정경로.SetCol("NM_EMP_WRIT", "작성자명", 80, false);
            this._flex공정경로.SetCol("NO_EMP_CONF", "승인자", 80, true);
            this._flex공정경로.SetCol("NM_EMP_CONF", "승인자명", 80, false);
            this._flex공정경로.SetCol("NO_PROJECT", "프로젝트", false);
            this._flex공정경로.SetCol("NM_PROJECT", "프로젝트명", false);
            this._flex공정경로.SetCol("TP_WO", "오더형태", 120, true);

            this._flex공정경로.VerifyPrimaryKey = new string[] { "NO_OPPATH" };
            this._flex공정경로.VerifyNotNull = new string[] { "TP_OPPATH", "NO_OPPATH", "DC_OPPATH" };
            this._flex공정경로.SetCodeHelpCol("NO_EMP_WRIT", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP_WRIT", "NM_EMP_WRIT" }, new string[] { "NO_EMP", "NM_KOR" });
            this._flex공정경로.SetCodeHelpCol("NO_EMP_CONF", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP_CONF", "NM_EMP_CONF" }, new string[] { "NO_EMP", "NM_KOR" });

            this._flex공정경로.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
            this._flex공정경로.SetDummyColumn("CHK");
            #endregion

            #region 공정
            this._flex공정.BeginSetting(1, 1, true);

            this._flex공정.SetCol("CD_OP", "OP", 50, 4, true);
            this._flex공정.SetCol("CD_WC", "작업장", 90, 7, true);
            this._flex공정.SetCol("NM_WC", "작업장명", 120, false);
            this._flex공정.SetCol("CD_WCOP", "공정코드", 90, 4, true);
            this._flex공정.SetCol("NM_OP", "공정명", 120, false);

			//this._flex공정.SetCol("TM_SETUP", "준비시간", 90, true);
			//this._flex공정.SetCol("TM", "인력가동시간", 90, true);
			//this._flex공정.SetCol("TM_MOVE", "이동시간", 90, true);
			//this._flex공정.SetCol("CD_RSRC", "생산자원", 90, 7, true);
			this._flex공정.SetCol("CD_EQUIP", "설비번호", 90, true);
			this._flex공정.SetCol("NM_EQUIP", "설비명", 90, false);
			this._flex공정.SetCol("TM_SETUP", "준비시간", false);
			this._flex공정.SetCol("TM", "인력가동시간", false);
			this._flex공정.SetCol("TM_MOVE", "이동시간", false);
			this._flex공정.SetCol("CD_RSRC", "생산자원", false);

            //this._flex공정.SetCol("YN_QC", "검사여부", 90, 1, true);
            this._flex공정.SetCol("YN_QC", "검사여부", false);

            this._flex공정.SetCol("YN_FINAL", "최종실적", 90, 1, true);

			//this._flex공정.SetCol("RT_YIELD", "수율", 90, true, typeof(decimal), FormatTpType.QUANTITY);
			//this._flex공정.SetCol("QT_LABOR_PLAN", "표준공수", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			//this._flex공정.SetCol("SET_REASON", "설정근거", 90, true);
			//this._flex공정.SetCol("SET_METHOD", "설정방법", 90, true);
			//this._flex공정.SetCol("DY_PLAN", "기간(작업공기)", 110, 5, true, typeof(decimal));
			this._flex공정.SetCol("RT_YIELD", "수율", false);
			this._flex공정.SetCol("QT_LABOR_PLAN", "표준공수", false);
			this._flex공정.SetCol("SET_REASON", "설정근거", false);
			this._flex공정.SetCol("SET_METHOD", "설정방법", false);
			this._flex공정.SetCol("DY_PLAN", "기간(작업공기)", false);

			this._flex공정.SetCol("DC_RMK", "비고", 100, true);
            this._flex공정.SetCol("INSERT_DT", "등록일", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정.SetCol("INSERT_NM_EMP", "등록자", 90, false);
            this._flex공정.SetCol("UPDATE_DT", "변경일", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정.SetCol("UPDATE_NM_EMP", "변경자", 90, false);
            this._flex공정.SetCol("CD_ITEM", "품목코드", 0, false);
            this._flex공정.SetCol("NO_OPPATH", "ROUTNO", 0, false);

			//this._flex공정.SetCol("NO_SFT", "SFT", 40, 3, true);
			//this._flex공정.SetCol("NM_SFT", "SFT명", 100, false);
			this._flex공정.SetCol("NO_SFT", "SFT", false);
			this._flex공정.SetCol("NM_SFT", "SFT명", false);
            this._flex공정.SetCol("CD_USERDEF1", "배치그룹코드", 100, true);
            this._flex공정.SetCol("NM_GPE", "배치그룹명", 100, false);

            this._flex공정.SetCol("YN_ROUT_SU_IV", "공정외주매입여부", 120, true);
            this._flex공정.SetCol("YN_INSP", "측정여부", 120, true);
            this._flex공정.SetCol("YN_USE", "사용유무", 60, true, CheckTypeEnum.Y_N);
            this._flex공정.SetCol("QT_INSP", "측정등록수", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("QT_FILE", "작업지침서등록수", false);
            this._flex공정.SetCol("NUM_USERDEF1", "공정경로등록사용자정의숫자1", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF2", "공정경로등록사용자정의숫자2", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF3", "공정경로등록사용자정의숫자3", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF4", "공정경로등록사용자정의숫자4", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF5", "공정경로등록사용자정의숫자5", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF6", "공정경로등록사용자정의숫자6", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF7", "공정경로등록사용자정의숫자7", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF8", "공정경로등록사용자정의숫자8", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("NUM_USERDEF9", "공정경로등록사용자정의숫자9", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정.SetCol("TXT_USERDEF1", "공정경로등록사용자정의텍스트1", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF2", "공정경로등록사용자정의텍스트2", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF3", "공정경로등록사용자정의텍스트3", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF4", "공정경로등록사용자정의텍스트4", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF5", "공정경로등록사용자정의텍스트5", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF6", "공정경로등록사용자정의텍스트6", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF7", "공정경로등록사용자정의텍스트7", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF8", "공정경로등록사용자정의텍스트8", 100, true, typeof(string));
            this._flex공정.SetCol("TXT_USERDEF9", "공정경로등록사용자정의텍스트9", 100, true, typeof(string));
            this._flex공정.SetCol("CD_USERDEF2", "공정경로등록사용자정의코드2", 100, true, typeof(string));
            this._flex공정.SetCol("CD_USERDEF3", "공정경로등록사용자정의코드3", 100, true, typeof(string));

            this._flex공정.VerifyPrimaryKey = new string[] { "CD_ITEM", "CD_OP", "NO_OPPATH" };
            this._flex공정.VerifyAutoDelete = new string[] { "CD_OP", "CD_WC" };
            this._flex공정.VerifyNotNull = new string[] { "CD_ITEM", "NO_OPPATH", "CD_OP", "CD_WC", "CD_WCOP", "TM_SETUP", "TM", "TM_MOVE" };
            this._flex공정.SetCodeHelpCol("CD_WC", HelpID.P_MA_WC_SUB, ShowHelpEnum.Always, new string[] { "CD_WC", "NM_WC" }, new string[] { "CD_WC", "NM_WC" }, new string[] { "CD_WC", "NM_WC", "CD_WCOP", "NM_OP", "CD_RSRC", "TP_RSRC" }, ResultMode.SlowMode);
            this._flex공정.SetCodeHelpCol("CD_WCOP", HelpID.P_PR_WCOP_SUB, ShowHelpEnum.Always, new string[] { "CD_WCOP", "NM_OP" }, new string[] { "CD_WCOP", "NM_OP" }, new string[] { "CD_WCOP", "NM_OP", "CD_RSRC", "TP_RSRC" }, ResultMode.SlowMode);
            this._flex공정.SetCodeHelpCol("CD_RSRC", HelpID.P_PR_RSRC_SUB, ShowHelpEnum.Always, new string[] { "CD_RSRC", "TP_RSRC" }, new string[] { "CD_RSRC", "TP_RSRC" }, new string[] { "CD_RSRC", "TP_RSRC" }, ResultMode.SlowMode);
            this._flex공정.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP" }, new string[] { "CD_EQUIP", "NM_EQUIP" });
            this._flex공정.SetCodeHelpCol("NO_SFT", "H_PR_SFT_SUB", ShowHelpEnum.Always, new string[] { "NO_SFT", "NM_SFT" }, new string[] { "NO_SFT", "NM_SFT" });
            this._flex공정.SetCodeHelpCol("CD_USERDEF1", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_USERDEF1", "NM_GPE" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flex공정.SetExceptEditCol("NM_WC", "NM_OP", "CD_ITEM", "NO_OPPATH");

            this._flex공정.SetBinding(this.panelExt1, null);

            this._flex공정.SettingVersion = "0.0.0.2";
            this._flex공정.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex공정.Cols["TM_SETUP"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex공정.Cols["TM_SETUP"].EditMask = "99\\:99\\:99";
            this._flex공정.Cols["TM_SETUP"].Format = "##\\:##\\:##";
            this._flex공정.Cols["TM"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex공정.Cols["TM"].EditMask = "99\\:99\\:99";
            this._flex공정.Cols["TM"].Format = "##\\:##\\:##";
            this._flex공정.Cols["TM_MOVE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex공정.Cols["TM_MOVE"].EditMask = "99\\:99\\:99";
            this._flex공정.Cols["TM_MOVE"].Format = "##\\:##\\:##";

            this._flex공정.SetStringFormatCol("TM_SETUP");
            this._flex공정.SetStringFormatCol("TM");
            this._flex공정.SetStringFormatCol("TM_MOVE");
            this._flex공정.SetNoMaskSaveCol("TM_SETUP");
            this._flex공정.SetNoMaskSaveCol("TM");
            this._flex공정.SetNoMaskSaveCol("TM_MOVE");

            this.SetColCaption_USERDEF();
            #endregion

            #region 작업지침서
            this._flex작업지침서.BeginSetting(1, 1, false);

            this._flex작업지침서.SetCol("NO_SEQ", "순번", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업지침서.SetCol("NM_FILE", "파일명", 300);
            this._flex작업지침서.SetCol("NM_INSERT", "등록자", 100);
            this._flex작업지침서.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex작업지침서.SetCol("DC_FILE", "비고", 100, true);

            this._flex작업지침서.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

            this._flex작업지침서.ExtendLastCol = true;

            this._flex작업지침서.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 측정항목
            this._flex측정항목.BeginSetting(1, 1, true);

            this._flex측정항목.SetCol("DC_ITEM", "구분", 100);
            this._flex측정항목.SetCol("CD_MEASURE", "측정장비", 100, true);
            this._flex측정항목.SetCol("DC_MEASURE", "측정장비", 100);
            this._flex측정항목.SetCol("DC_LOCATION", "위치", 100);
            this._flex측정항목.SetCol("DC_SPEC", "사양", 100);
            this._flex측정항목.SetCol("QT_MIN", "최소값", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex측정항목.SetCol("QT_MAX", "최대값", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex측정항목.SetCol("TP_DATA", "대표값유형", 100);
            this._flex측정항목.SetCol("CNT_INSP", "측정포인트", 100);
            this._flex측정항목.SetCol("DC_RMK", "비고", 100);
            this._flex측정항목.SetCol("YN_CERT", "성적서여부", 60, true, CheckTypeEnum.Y_N);
            this._flex측정항목.SetCol("YN_USE", "사용유무", 60, true, CheckTypeEnum.Y_N);
            this._flex측정항목.SetCol("YN_SAMPLING", "샘플링검사여부", 60, true, CheckTypeEnum.Y_N);
            this._flex측정항목.SetCol("YN_ASSY", "현합치수", 60, true, CheckTypeEnum.Y_N);
            this._flex측정항목.SetCol("CD_CLEAR_GRP", "클리어런스그룹", 100, true);

            this._flex측정항목.SetDataMap("CD_MEASURE", Global.MainFrame.GetComboDataCombine("S;CZ_WIN0012"), "CODE", "NAME");
            this._flex측정항목.SetDataMap("TP_DATA", MA.GetCodeUser(new string[] { "MIN", "MAX" }, new string[] { "최소값", "최대값" }), "CODE", "NAME");
            this._flex측정항목.SetDataMap("CNT_INSP", MA.GetCodeUser(new string[] { "1", "2", "3", "4", "5" }, new string[] { "1", "2", "3", "4", "5" }), "CODE", "NAME");
            this._flex측정항목.SetDataMap("CD_CLEAR_GRP", MA.GetCode("CZ_WIN0013"), "CODE", "NAME");

            this._flex측정항목.SetDummyColumn("YN_USE");

            this._flex측정항목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 설비
            this._flex설비.BeginSetting(1, 1, false);

            this._flex설비.SetCol("CD_EQUIP", "설비코드", 100);
            this._flex설비.SetCol("NM_EQUIP", "설비명", 250);
            this._flex설비.SetCol("CD_EQIP_GRP", "설비그룹", 100);
            this._flex설비.SetCol("QT_LABOR_PLAN", "표준공수", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex설비.SetCol("NM_INSERT", "등록자", 100);
            this._flex설비.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex설비.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

            this._flex설비.ExtendLastCol = true;

            this._flex설비.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP", "CD_EQIP_GRP" }, new string[] { "CD_EQUIP", "NM_EQUIP", "CD_EQIP_GRP" });

            this._flex설비.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

		private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn붙여넣기.Click += new EventHandler(this.btn붙여넣기_Click);
            this.btn복사하기.Click += new EventHandler(this.btn복사하기_Click);
            this.btn측정항목.Click += Btn측정항목_Click;
            this.btn측정항목복사.Click += Btn측정항목복사_Click;
			this.btn측정항목붙여넣기.Click += Btn측정항목붙여넣기_Click;
            this.btn배치그룹관리.Click += new EventHandler(this.Btn배치그룹관리_Click);
            this.btnAPS연동.Click += new EventHandler(this.BtnAPS연동_Click);
			
			this.btn작업지침서추가.Click += Btn작업지침서추가_Click;
            this.btn작업지침서삭제.Click += Btn작업지침서삭제_Click;
			this.btn작업지침서열기.Click += Btn작업지침서열기_Click;
			this.btn작업지침서미리보기.Click += Btn작업지침서미리보기_Click;

			this.btn측정항목추가.Click += Btn측정항목추가_Click;
			this.btn측정항목삭제.Click += Btn측정항목삭제_Click;
			this.btn측정항목저장.Click += Btn측정항목저장_Click;

            this.btn설비추가.Click += Btn설비추가_Click;
            this.btn설비삭제.Click += Btn설비삭제_Click;
            this.btn설비저장.Click += Btn설비저장_Click;

            this._flex품목.AfterRowChange += new RangeEventHandler(this._flex품목_AfterRowChange);
            this._flex품목.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex품목.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

            this._flex공정경로.BeforeRowChange += new RangeEventHandler(this._flex공정경로_BeforeRowChange);
            this._flex공정경로.AfterRowChange += new RangeEventHandler(this._flex공정경로_AfterRowChange);
            this._flex공정경로.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex공정경로.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex공정경로.CheckHeaderClick += new EventHandler(this._flex공정경로_CheckHeaderClick);

            this._flex공정.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex공정.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
            this._flex공정.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex공정.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex공정.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex공정.AddRow += new EventHandler(this.btn추가_Click);

			this._flex공정.AfterRowChange += _flex공정_AfterRowChange;

            this._flex설비.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);

            this._flex측정항목.ValidateEdit += _flex측정항목_ValidateEdit;
			this._flex측정항목.AfterEdit += _flex측정항목_AfterEdit;
        }

		private void _flex측정항목_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "YN_USE") return;

                query = @"UPDATE A
                          SET A.YN_USE = '{7}',
                              A.ID_UPDATE = '{8}',
                              A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                          FROM CZ_PR_ROUT_INSP A
                          WHERE A.CD_COMPANY = '{0}'
                          AND A.CD_PLANT = '{1}'
                          AND A.CD_ITEM = '{2}'
                          AND A.NO_OPPATH = '{3}'
                          AND A.CD_OP = '{4}'
                          AND A.CD_WCOP = '{5}'
                          AND A.NO_INSP = '{6}'";

                DBHelper.ExecuteScalar(string.Format(query, new object[] { flexGrid["CD_COMPANY"].ToString(),
                                                                           flexGrid["CD_PLANT"].ToString(),
                                                                           flexGrid["CD_ITEM"].ToString(),
                                                                           flexGrid["NO_OPPATH"].ToString(),
                                                                           flexGrid["CD_OP"].ToString(),
                                                                           flexGrid["CD_WCOP"].ToString(),
                                                                           flexGrid["NO_INSP"].ToString(),
                                                                           flexGrid["YN_USE"].ToString(),
                                                                           Global.MainFrame.LoginInfo.UserID }));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex측정항목_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid grid = ((FlexGrid)sender);

                string filter = string.Format("CD_ITEM = '{0}' AND NO_OPPATH = '{1}' AND CD_OP = '{2}' AND CD_WCOP = '{3}'", new object[] { this._flex공정["CD_ITEM"].ToString(),
                                                                                                                                            this._flex공정["NO_OPPATH"].ToString(),
                                                                                                                                            this._flex공정["CD_OP"].ToString(),
                                                                                                                                            this._flex공정["CD_WCOP"].ToString() });

                switch (grid.Cols[grid.Col].Name)
                {
                    case "CNT_INSP":
                        if ((grid.DataTable.Select(filter)
                                           .Where(x => x["YN_USE"].ToString() == "Y")
                                           .Sum(x => D.GetInt(x["CNT_INSP"])) + D.GetDecimal(grid.EditData)) > 15)
						{
                            this.ShowMessage("총 15 번까지만 측정 가능 합니다.");
                            e.Cancel = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn측정항목저장_Click(object sender, EventArgs e)
		{
            DataTable dt;

            try
            {
                if (!this._flex측정항목.IsDataChanged) return;

                dt = this._flex측정항목.GetChanges();

                if (dt.Select("ISNULL(DC_ITEM, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("구분이 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(DC_MEASURE, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("측정장비가 입력되지 않은 행이 존재 합니다.");
                    return;
                }

				if (dt.Select("ISNULL(CD_MEASURE, '') = ''").Length > 0)
				{
					Global.MainFrame.ShowMessage("측정장비가 입력되지 않은 행이 존재 합니다.");
					return;
				}

				if (dt.Select("ISNULL(DC_LOCATION, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("위치가 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(DC_SPEC, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("사양이 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(TP_DATA, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("대표값유형이 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (!this._biz.Save측정항목(dt)) return;

                this._flex측정항목.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void Btn측정항목추가_Click(object sender, EventArgs e)
		{
            decimal seq;

            try
            {
                seq = this.Seq측정항목Max();

                this._flex측정항목.Row = this._flex측정항목.Rows.Count - 1;
                this._flex측정항목.Rows.Add();

                this._flex측정항목["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex측정항목["CD_PLANT"] = this.cbo공장.SelectedValue;
                this._flex측정항목["CD_ITEM"] = this._flex공정["CD_ITEM"].ToString();
                this._flex측정항목["NO_OPPATH"] = this._flex공정["NO_OPPATH"].ToString();
                this._flex측정항목["CD_OP"] = this._flex공정["CD_OP"].ToString();
                this._flex측정항목["CD_WCOP"] = this._flex공정["CD_WCOP"].ToString();
                this._flex측정항목["NO_INSP"] = seq;
                this._flex측정항목["DC_SPEC"] = "Ø";
                this._flex측정항목["YN_USE"] = "Y";

                this._flex측정항목.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn측정항목삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex측정항목.Row < 0) return;

                this._flex측정항목.RemoveItem(this._flex측정항목.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn설비저장_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                if (!this._flex설비.IsDataChanged) return;

                dt = this._flex설비.GetChanges();

                if (dt.Select("ISNULL(CD_EQUIP, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("장비코드가 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (!this._biz.Save설비(dt)) return;

                this._flex설비.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn설비추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex설비.Row = this._flex설비.Rows.Count - 1;
                this._flex설비.Rows.Add();

                this._flex설비["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex설비["CD_PLANT"] = this.cbo공장.SelectedValue;
                this._flex설비["CD_ITEM"] = this._flex공정["CD_ITEM"].ToString();
                this._flex설비["NO_OPPATH"] = this._flex공정["NO_OPPATH"].ToString();
                this._flex설비["CD_OP"] = this._flex공정["CD_OP"].ToString();
                this._flex설비["CD_WC"] = this._flex공정["CD_WC"].ToString();
                this._flex설비["CD_WCOP"] = this._flex공정["CD_WCOP"].ToString();

                this._flex설비.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn설비삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex설비.Row < 0) return;

                this._flex설비.RemoveItem(this._flex설비.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn측정항목복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex공정.HasNormalRow) return;
                if (this._flex공정["YN_INSP"].ToString() != "Y")
                {
                    this.ShowMessage("측정하지 않는 공정 입니다.");
                    return;
                }
                if (D.GetDecimal(this._flex공정["QT_INSP"]) == 0)
                {
                    this.ShowMessage("측정항목이 등록되어 있지 않습니다.");
                    return;
                }

                DataRow dataRow = this._flex공정.GetDataRow(this._flex공정.Row);


                this.dr측정항목 = this._flex공정.DataTable.NewRow();
                this.dr측정항목["CD_ITEM"] = dataRow["CD_ITEM"].ToString();
                this.dr측정항목["NO_OPPATH"] = dataRow["NO_OPPATH"].ToString();
                this.dr측정항목["CD_OP"] = dataRow["CD_OP"].ToString();
                this.dr측정항목["CD_WCOP"] = dataRow["CD_WCOP"].ToString();

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn측정항목복사.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn측정항목붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dr측정항목 == null)
				{
                    this.ShowMessage("복사한 측정항목이 없습니다.");
                    return;
				}

                if (this._flex공정["YN_INSP"].ToString() != "Y")
                {
                    this.ShowMessage("측정하지 않는 공정 입니다.");
                    return;
                }

                if (Global.MainFrame.ShowMessage("측정항목을 붙여 넣으시겠습니까 ?\n확인 시 즉시 저장", "QY2") == DialogResult.Yes)
                {
                    DBHelper.ExecuteNonQuery("SP_CZ_PR_ROUT_INSP_COPY", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       Global.MainFrame.LoginInfo.CdPlant,
                                                                                       this.dr측정항목["CD_ITEM"].ToString(),
                                                                                       this.dr측정항목["NO_OPPATH"].ToString(),
                                                                                       this.dr측정항목["CD_OP"].ToString(),
                                                                                       this.dr측정항목["CD_WCOP"].ToString(),
                                                                                       this._flex공정["CD_ITEM"].ToString(),
                                                                                       this._flex공정["NO_OPPATH"].ToString(),
                                                                                       this._flex공정["CD_OP"].ToString(),
                                                                                       this._flex공정["CD_WCOP"].ToString(),
                                                                                       Global.MainFrame.LoginInfo.UserID });
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn측정항목붙여넣기.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn배치그룹관리_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_PR_ROUT_BATCH_GROUP_SUB dialog = new P_CZ_PR_ROUT_BATCH_GROUP_SUB();

                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void BtnAPS연동_Click(object sender, EventArgs e)
        {
			try
			{
                MsgControl.ShowMsg(" 연동 중입니다. \r\n잠시만 기다려주세요!");

                DBHelper.ExecuteNonQuery("SP_CZ_PR_ERP_TO_APS_IF", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                MsgControl.CloseMsg();
            }
            catch (Exception ex)
			{
                this.MsgEnd(ex);
			}
        }

        protected override void InitPaint()
        {
            try
			{
                base.InitPaint();

                DataSet comboData = Global.MainFrame.GetComboData("S;MA_B000010",
                                                                  "NC;MA_PLANT",
                                                                  "S;PR_0000002",
                                                                  "S;MA_B000009",
                                                                  "S;MA_B000010",
                                                                  "S;QU_2000022",
                                                                  "S;PR_0000052",
                                                                  "S;PR_0000053",
                                                                  "S;PR_TPWO",
                                                                  "S;MA_B000030",
                                                                  "S;MA_B000031",
                                                                  "S;MA_B000032",
                                                                  "S;MA_B000066");

                this.cbo품목계정.DataSource = comboData.Tables[0];
                this.cbo품목계정.DisplayMember = "NAME";
                this.cbo품목계정.ValueMember = "CODE";

                this.cbo공장.DataSource = comboData.Tables[1];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";

                this.cbo공정경로유형.DataSource = comboData.Tables[2];
                this.cbo공정경로유형.DisplayMember = "NAME";
                this.cbo공정경로유형.ValueMember = "CODE";

                this.cbo조달구분.DataSource = comboData.Tables[3];
                this.cbo조달구분.DisplayMember = "NAME";
                this.cbo조달구분.ValueMember = "CODE";

                this.cbo측정항목등록.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "등록", "미등록" }, true);
                this.cbo측정항목등록.DisplayMember = "NAME";
                this.cbo측정항목등록.ValueMember = "CODE";

                this.cbo작업지침서등록.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "등록", "미등록" }, true);
                this.cbo작업지침서등록.DisplayMember = "NAME";
                this.cbo작업지침서등록.ValueMember = "CODE";

                if (this.s조달구분통제 == "100")
                {
                    this.cbo조달구분.SelectedValue = "M";
                    this.cbo조달구분.Enabled = false;
                }

                this._flex공정경로.SetDataMap("TP_OPPATH", comboData.Tables[2].Copy(), "CODE", "NAME");
                this._flex품목.SetDataMap("TP_PROC", comboData.Tables[3], "CODE", "NAME");
                this._flex품목.SetDataMap("CLS_ITEM", comboData.Tables[4], "CODE", "NAME");
                this._flex공정.SetDataMap("YN_QC", comboData.Tables[5], "CODE", "NAME");
                this._flex공정.SetDataMap("YN_FINAL", comboData.Tables[5].Copy(), "CODE", "NAME");
                this._flex공정.SetDataMap("YN_INSP", comboData.Tables[5].Copy(), "CODE", "NAME");
                this._flex공정.SetDataMap("SET_REASON", comboData.Tables[6].Copy(), "CODE", "NAME");
                this._flex공정.SetDataMap("SET_METHOD", comboData.Tables[7].Copy(), "CODE", "NAME");
                this._flex공정.SetDataMap("YN_ROUT_SU_IV", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true), "CODE", "NAME");
                this._flex품목.SetDataMap("CLS_L", comboData.Tables[9].Copy(), "CODE", "NAME");
                this._flex품목.SetDataMap("CLS_M", comboData.Tables[10].Copy(), "CODE", "NAME");
                this._flex품목.SetDataMap("CLS_S", comboData.Tables[11].Copy(), "CODE", "NAME");
                this._flex품목.SetDataMap("GRP_MFG", comboData.Tables[12].Copy(), "CODE", "NAME");

                if (Pr_Global.bMfg_AuthH_YN)
                {
                    DataTable maMfgAuth = Pr_ComFunc.Get_MA_MFG_AUTH(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    Global.MainFrame.LoginInfo.DeptCode,
                                                                                    Global.MainFrame.LoginInfo.EmployeeNo,
                                                                                    "TPWO",
                                                                                    Global.SystemLanguage.MultiLanguageLpoint });
                    DataRow row = maMfgAuth.NewRow();
                    row["CODE"] = string.Empty;
                    row["NAME"] = string.Empty;
                    maMfgAuth.Rows.Add(row);
                    maMfgAuth.DefaultView.Sort = "CODE ASC";
                    this._flex공정경로.SetDataMap("TP_WO", maMfgAuth.DefaultView, "CODE", "NAME");
                }
                else
                    this._flex공정경로.SetDataMap("TP_WO", comboData.Tables[8].Copy(), "CODE", "NAME");

                this._flex공정.SetDataMap("CD_USERDEF2", MA.GetCode("PR_0000111", true), "CODE", "NAME");
                this._flex공정.SetDataMap("CD_USERDEF3", MA.GetCode("PR_0000112", true), "CODE", "NAME");

                this.oneGrid1.UseCustomLayout = true;
                this.bpPnl_plant.IsNecessaryCondition = true;
                this.bpPnl_radioButton.IsNecessaryCondition = true;
                this.oneGrid1.InitCustomLayout();

                this.splitContainer1.SplitterDistance = 1298;
                this.splitContainer3.SplitterDistance = 1432;
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
                this.ToolBarPrintButtonEnabled = false;

                if (this.str공정경로복사 == "100")
                {
                    if (!this._flex공정.HasNormalRow)
					{
                        this.btn삭제.Enabled = false;
                        this.btn측정항목복사.Enabled = false;
                        this.btn측정항목붙여넣기.Enabled = false;
                    }
                    else
					{
                        this.btn삭제.Enabled = true;
                        this.btn측정항목복사.Enabled = true;
                        this.btn측정항목붙여넣기.Enabled = true;
                    }
                    
                    if (!this._flex공정경로.HasNormalRow)
                    {
                        this.btn붙여넣기.Enabled = true;
                        this.btn복사하기.Enabled = false;
                        this.btn추가.Enabled = false;
                    }
                    else
                    {
                        this.btn붙여넣기.Enabled = false;
                        this.btn복사하기.Enabled = true;
                        this.btn추가.Enabled = true;
                    }
                }
                else
                {
                    if (!this._flex공정.HasNormalRow)
                    {
                        this.btn붙여넣기.Enabled = true;
                        this.btn복사하기.Enabled = false;
                        this.btn삭제.Enabled = false;
                        this.btn측정항목복사.Enabled = false;
                        this.btn측정항목붙여넣기.Enabled = false;
                    }
                    else
                    {
                        this.btn붙여넣기.Enabled = false;
                        this.btn복사하기.Enabled = true;
                        this.btn삭제.Enabled = true;
                        this.btn측정항목복사.Enabled = true;
                        this.btn측정항목붙여넣기.Enabled = true;
                    }

                    if (!this._flex공정경로.HasNormalRow)
                        this.btn추가.Enabled = false;
                    else
                        this.btn추가.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void initState(bool state)
        {
            this.b_StateMode = state;
            MsgControl.CloseMsg();
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;
            if (this.공장선택여부) return true;

            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
            this.cbo공장.Focus();
            
            return false;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch() || this.b_StateMode) return;

                this.initState(true);
                MsgControl.ShowMsg(" 조회 중입니다. \r\n잠시만 기다려주세요!");

                string str = "A";
                
                if (this.rdo전체조회.Checked)
                    str = "A";
                else if (this.rdo등록된품목만조회.Checked)
                    str = "Y";
                else if (this.rdo등록되지않은품목만조회.Checked)
                    str = "N";
                else if (this.rdo작업장미등록품목조회.Checked)
                    str = "W";
                
                DataTable dataTable = this._biz.Search품목(new object[] { this.LoginInfo.CompanyCode,
                                                                           this.cbo공장.SelectedValue.ToString(),
                                                                           this.ctx품목From.CodeValue,
                                                                           this.ctx품목To.CodeValue,
                                                                           this.cbo품목계정.SelectedValue.ToString(),
                                                                           str,
                                                                           this.cbo공정경로유형.SelectedValue.ToString(),
                                                                           this.cbo조달구분.SelectedValue.ToString(),
                                                                           this.ctx품목군S.CodeValue,
                                                                           this.ctx대분류.CodeValue,
                                                                           this.ctx중분류.CodeValue,
                                                                           this.ctx소분류.CodeValue,
                                                                           this.bpc작업장.QueryWhereIn_Pipe,
                                                                           this.bpc공정.QueryWhereIn_Pipe,
                                                                           this.ctx제품군.CodeValue,
                                                                           Global.SystemLanguage.MultiLanguageLpoint,
                                                                           this.cbo측정항목등록.SelectedValue.ToString(),
                                                                           this.cbo작업지침서등록.SelectedValue.ToString() });

                if (this._flex공정경로.DataTable != null)
                    this._flex공정경로.DataTable.Rows.Clear();
                
                if (this._flex공정.DataTable != null)
                    this._flex공정.DataTable.Rows.Clear();

                if (this._flex작업지침서.DataTable != null)
                    this._flex작업지침서.DataTable.Rows.Clear();

                if (this._flex측정항목.DataTable != null)
                    this._flex측정항목.DataTable.Rows.Clear();

                if (this._flex설비.DataTable != null)
                    this._flex설비.DataTable.Rows.Clear();

                this.htDetailQueryCollection.Clear();
                this._flex품목.Binding = dataTable;
                
                if (!this._flex품목.HasNormalRow)
                    this.ShowMessage("공정경로대상이 될 공장품목이 없습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.initState(false);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd()) return;

                if (!this.공장선택여부)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    this.cbo공장.Focus();
                }
                else
                {
                    this._flex공정경로.Rows.Add();
                    this._flex공정경로.Row = this._flex공정경로.Rows.Count - 1;
                    this._flex공정경로["CHK"] = "N";
                    this._flex공정경로["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    this._flex공정경로["CD_ITEM"] = this._flex품목["CD_ITEM"];
                    this._flex공정경로["NM_ITEM"] = this._flex품목["NM_ITEM"];
                    this._flex공정경로["STND_ITEM"] = this._flex품목["STND_ITEM"];
                    this._flex공정경로["UNIT_MO"] = this._flex품목["UNIT_MO"];
                    this._flex공정경로["TP_PROC"] = this._flex품목["TP_PROC"];
                    this._flex공정경로["CLS_ITEM"] = this._flex품목["CLS_ITEM"];
                    DataRow[] dataRowArray = this._flex공정경로.DataView.ToTable().Select("TP_OPPATH = 'S'", "NO_OPPATH DESC", DataViewRowState.CurrentRows);
                    string str1 = "";
                    string str2 = "";
                    string str3 = "";

                    if (dataRowArray.Length == 0) str2 = "S";

                    foreach (DataRow row in this._flex공정경로.GetDataMapTable("TP_OPPATH").Rows)
                    {
                        if (row["CODE"].ToString() == str2)
                        {
                            str3 = row["NAME"].ToString();
                            break;
                        }
                    }

                    this._flex공정경로["NO_OPPATH"] = str1;
                    this._flex공정경로["TP_OPPATH"] = str2;
                    this._flex공정경로["DC_OPPATH"] = str3;

                    if (this.strROUTNO자동입력여부 == "100")
                        this._flex공정경로["NO_OPPATH"] = str2.ToUpper();

                    this._flex공정경로["NO_EMP_WRIT"] = string.Empty;
                    this._flex공정경로["NO_EMP_CONF"] = string.Empty;
                    this._flex공정경로.AddFinished();
                    this._flex공정경로.Col = this._flex공정경로.Cols.Fixed;
                    this._flex공정경로.Focus();
                    this._flex공정경로.Select(this._flex공정경로.Row, "TP_OPPATH");
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
                if (!this.BeforeDelete() || 
                    !this._flex공정경로.HasNormalRow || 
                    DialogResult.Yes != this.ShowMessage(공통메세지.자료를삭제하시겠습니까)) return;

                DataRow[] dataRowArray = this._flex품목.DataTable.Select("CHK = 'Y'", string.Empty, DataViewRowState.CurrentRows);
                
                if (dataRowArray.Length == 0)
                {
                    if (!this.RowDelete());
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        string str = D.GetString(dataRow["CD_ITEM"]);

                        for (int index = this._flex품목.Rows.Fixed; index < this._flex품목.Rows.Count; ++index)
                        {
                            if (D.GetString(this._flex품목.Rows[index]["CD_ITEM"]) == str)
                            {
                                this._flex품목.Row = index;
                                break;
                            }
                        }

                        if (this._flex공정.DataTable.Select(this._flex공정.RowFilter, "", DataViewRowState.CurrentRows).Length != 0 && !this.RowDelete())
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool RowDelete()
        {
            bool flag = false;
            string detail = "OP\t\t\n";

            foreach (DataRow dataRow in this._flex공정.DataTable.Select(this._flex공정.RowFilter, "", DataViewRowState.CurrentRows))
            {
                if (D.GetDecimal(dataRow["ROUT_ANS_CNT"]) > 0M)
                {
                    detail = detail + D.GetString(dataRow["CD_OP"]) + "\t\t\n";
                    flag = true;
                }
            }

            if (flag && this.ShowDetailMessage("공정경로BOM이 존재합니다\n▼ 버튼을 눌러서 목록을 확인하세요!\n예(삭제), 아니요(취소)", (object[])null, detail, "QY2") != DialogResult.Yes)
                return false;
            
            this._flex공정.RemoveViewAll();
            this._flex공정경로.Rows.Remove(this._flex공정경로.Row);
            
            return true;
        }

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(PageResultMode.SaveGood);
                string str = this._flex품목["CD_ITEM"].ToString();
                int count = this._flex품목.DataView.Count;
                this.OnToolBarSearchButtonClicked(null, null);
                
                if (!this._flex품목.HasNormalRow || count != this._flex품목.DataView.Count) return;
                
                for (int index = this._flex품목.Rows.Fixed; index < this._flex품목.Rows.Count; ++index)
                {
                    if (!(this._flex품목.Rows[index]["CD_ITEM"].ToString() != str))
                    {
                        this._flex품목.Row = index;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.Verify()) return false;

            DataTable changes1 = this._flex공정경로.DataTable.GetChanges();
            DataTable changes2 = this._flex공정.DataTable.GetChanges();
            DataTable changes3 = this._flex작업지침서.DataTable.GetChanges();

            if (changes1 == null && 
                changes2 == null && 
                changes3 == null)
                return true;
            
            foreach (ResultData resultData in this._biz.SaveData(changes2, changes1, changes3))
            {
                if (!resultData.Result) return false;
            }

            if (this._flex공정경로.HasNormalRow)
            {
                this.st.strItem = D.GetString(this._flex공정경로["CD_ITEM"]);
                this.st.strNO_OPPATH = D.GetString(this._flex공정경로["NO_OPPATH"]);
                this.htDetailQueryCollection.Clear();
                this.htDetailQueryCollection.Add(this.st, null);
            }

            this._flex공정.AcceptChanges();
            this._flex공정경로.AcceptChanges();
            this._flex작업지침서.AcceptChanges();

            return true;
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd() || 
                    this.HasNullColumn()) return;

                this._flex공정.Rows.Add();
                this._flex공정.Row = this._flex공정.Rows.Count - 1;
                this._flex공정["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                string input = D.GetString(this._flex공정.DataTable.Compute("MAX(CD_OP)", "CD_ITEM ='" + this._flex공정경로[this._flex공정경로.Row, "CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로[this._flex공정경로.Row, "NO_OPPATH"].ToString() + "'")).ToUpper().Trim();
                bool flag = Regex.IsMatch(input, "[A-Z]");
                
                if (this._flex공정.Rows.Count > this._flex공정.Rows.Fixed + 1)
                {
                    if (!flag)
                        this._flex공정["CD_OP"] = (D.GetDecimal(input) + 10M).ToString();
                    else
                    {
                        this.ShowMessage("Op.에 문자열이 들어가 있어 자동채번을 할 수 없습니다.", "EK1");
                        this._flex공정["CD_OP"] = "";
                    }
                }

                this._flex공정["CD_ITEM"] = this._flex공정경로["CD_ITEM"];
                this._flex공정["NO_OPPATH"] = this._flex공정경로["NO_OPPATH"];
                this._flex공정["TP_OPPATH"] = this._flex공정경로["TP_OPPATH"];
                this._flex공정["YN_USE"] = "Y";
                this._flex공정.AddFinished();
                this._flex공정.Col = this._flex공정.Cols.Fixed;
                this._flex공정.Focus();

                for (int index = this._flex공정.Rows.Fixed; index < this._flex공정.Rows.Count; ++index)
                {
                    if (this._flex공정.Rows[index]["YN_FINAL"].ToString() == "Y")
                        this._flex공정.Rows[index]["YN_FINAL"] = "N";

                    if (this._flex공정.Rows.Count - this._flex공정.Rows.Fixed == index)
                        this._flex공정.Rows[index]["YN_FINAL"] = "Y";
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
                if (!this.BeforeDelete() || 
                    !this._flex공정.HasNormalRow || 
                    D.GetDecimal(this._flex공정["ROUT_ANS_CNT"]) > 0M && 
                    this.ShowMessage("OP에 공정경로BOM이 존재합니다 삭제하시겠습니까?", "QY2") == DialogResult.No) return;

                if (DBHelper.GetDataTable(string.Format(@"SELECT 1 
                                                          FROM CZ_PR_ROUT_INSP
                                                          WHERE CD_COMPANY = '{0}'
                                                          AND CD_PLANT = '{1}'
                                                          AND CD_ITEM = '{2}'
                                                          AND NO_OPPATH = '{3}'
                                                          AND CD_OP = '{4}'
                                                          AND CD_WCOP = '{5}'", Global.MainFrame.LoginInfo.CompanyCode,
                                                                                this._flex공정["CD_PLANT"].ToString(),
                                                                                this._flex공정["CD_ITEM"].ToString(),
                                                                                this._flex공정["NO_OPPATH"].ToString(),
                                                                                this._flex공정["CD_OP"].ToString(),
                                                                                this._flex공정["CD_WCOP"].ToString())).Rows.Count > 0)
				{
                    this.ShowMessage("OP에 측정항목이 등록 되어 있습니다.\n측정항목 삭제 후 삭제가능 합니다.");
                    return;
				}

                if (DBHelper.GetDataTable(string.Format(@"SELECT 1 
                                                          FROM CZ_PR_ROUT_FILE
                                                          WHERE CD_COMPANY = '{0}'
                                                          AND CD_PLANT = '{1}'
                                                          AND CD_ITEM = '{2}'
                                                          AND NO_OPPATH = '{3}'
                                                          AND CD_OP = '{4}'
                                                          AND CD_WCOP = '{5}'", Global.MainFrame.LoginInfo.CompanyCode,
                                                                                this._flex공정["CD_PLANT"].ToString(),
                                                                                this._flex공정["CD_ITEM"].ToString(),
                                                                                this._flex공정["NO_OPPATH"].ToString(),
                                                                                this._flex공정["CD_OP"].ToString(),
                                                                                this._flex공정["CD_WCOP"].ToString())).Rows.Count > 0)
                {
                    this.ShowMessage("OP에 작업지침서가 등록 되어 있습니다.\n작업지침서 삭제 후 삭제가능 합니다.");
                    return;
                }

                string str = "N";

                if (this._flex공정.Rows[this._flex공정.Row]["YN_FINAL"].ToString() == "Y")
                    str = "Y";

                if (this._flex공정.HasNormalRow)
                    this._flex공정.Rows.Remove(this._flex공정.Row);
                
                if (str == "Y" && this._flex공정.Rows.Count > this._flex공정.Rows.Fixed)
                    this._flex공정.Rows[this._flex공정.Rows.Count - 1]["YN_FINAL"] = str;
                
                for (int index = this._flex공정.Rows.Fixed; index < this._flex공정.Rows.Count; ++index)
                {
                    this._flex공정.Rows[index]["YN_FINAL"] = "N";

                    if (this._flex공정.Rows.Count - 1 == index)
                        this._flex공정.Rows[index]["YN_FINAL"] = "Y";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn복사하기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.공장선택여부)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    this.cbo공장.Focus();
                }
                else if (this._flex공정경로.DataSource == null || this._flex공정.DataSource == null || !this._flex공정.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (MA.ServerKey(false, "TAERYUK", "ASUNG", "SOLIDTECH") || this.str공정경로복사 == "100")
                {
                    DataRow[] dataRowArray1 = this._flex공정경로.DataView.ToTable().Select(" CHK = 'Y' ");
                    if (dataRowArray1 == null || dataRowArray1.Length == 0)
                    {
                        int num2 = (int)this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.DD("복사할 경로유형"));
                    }
                    else
                    {
                        this.dt_Paste = this._flex공정경로.DataTable.Clone();
                        this.dt_PasteD = this._flex공정.DataTable.Clone();
                        this._flex품목.SetCellCheck(this._flex품목.Row, this._flex품목.Cols["CHK"].Index, CheckEnum.Unchecked);
                        for (int index = 0; index < dataRowArray1.Length; ++index)
                        {
                            this.dt_Paste.ImportRow(dataRowArray1[index]);
                            this._flex공정경로.SetCellCheck(index + 1, this._flex공정경로.Cols["CHK"].Index, CheckEnum.Unchecked);
                            DataRow[] dataRowArray2 = this._flex공정.DataTable.Select("CD_ITEM = '" + dataRowArray1[index]["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + dataRowArray1[index]["NO_OPPATH"].ToString() + "' AND TP_OPPATH = '" + dataRowArray1[index]["TP_OPPATH"].ToString() + "' ", "", DataViewRowState.OriginalRows);
                            if (dataRowArray1 != null && dataRowArray1.Length != 0)
                            {
                                foreach (DataRow dataRow in dataRowArray2)
                                    this.dt_PasteD.Rows.Add(dataRow.ItemArray);
                            }
                        }
                        this.btn붙여넣기.Focus();
                    }
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flex공정경로.DataTable.Select(" CHK = 'Y' ");
                    if (dataRowArray1 == null || dataRowArray1.Length > 1)
                    {
                        int num2 = (int)this.ShowMessage("PR_M100083");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flex공정.DataTable.Select("CD_ITEM = '" + this._flex공정경로["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로["NO_OPPATH"].ToString() + "' AND TP_OPPATH = '" + this._flex공정경로["TP_OPPATH"].ToString() + "' ", "", DataViewRowState.OriginalRows);
                        this.dt_Paste = this._flex공정.DataTable.Clone();
                        foreach (DataRow dataRow in dataRowArray2)
                            this.dt_Paste.Rows.Add(dataRow.ItemArray);
                        this.dt_Paste.AcceptChanges();
                        if (dataRowArray2.Length < 1)
                        {
                            int num3 = (int)this.ShowMessage("SA_M000072");
                        }
                        else
                            this.btn붙여넣기.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
            }
        }

        private void btn붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.공장선택여부)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    this.cbo공장.Focus();
                }
                else
                {
                    if (this.b_StateMode) return;

                    this.initState(true);
                    MsgControl.ShowMsg(" 적용 중입니다. \r\n잠시만 기다려주세요!");
                    this.mDataArea.Enabled = false;
                    this.flowLayoutPanel1.Enabled = false;
                    
                    if (this.str공정경로복사 == "100")
                    {
                        this._flex품목.Redraw = false;
                        this._flex공정경로.Redraw = false;
                        this._flex공정.Redraw = false;
                        DataRow[] dataRowArray1 = this._flex품목.DataTable.Select(" CHK = 'Y' ");

                        if (dataRowArray1 == null || dataRowArray1.Length == 0)
                        {
                            this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.DD("붙여넣기 할 품목"));
                        }
                        else
                        {
                            foreach (DataRow dataRow1 in dataRowArray1)
                            {
                                this._flex품목.Row = this._flex품목.FindRow(dataRow1["CD_ITEM"], 0, this._flex품목.Cols["CD_ITEM"].Index, true);
                                DataRow[] dataRowArray2 = this._flex공정경로.DataTable.Select(" CD_ITEM = '" + dataRow1["CD_ITEM"].ToString() + "'");

                                if (dataRowArray2 == null || dataRowArray2.Length <= 0)
                                {
                                    foreach (DataRow row in this.dt_Paste.Rows)
                                    {
                                        this.OnToolBarAddButtonClicked(null, (EventArgs)null);
                                        this._flex공정경로["CHK"] = "N";
                                        this._flex공정경로["TP_OPPATH"] = row["TP_OPPATH"];
                                        this._flex공정경로["NO_OPPATH"] = row["NO_OPPATH"];
                                        this._flex공정경로["DC_OPPATH"] = row["DC_OPPATH"];
                                        this._flex공정경로["NO_EMP_WRIT"] = row["NO_EMP_WRIT"];
                                        this._flex공정경로["NM_EMP_WRIT"] = row["NM_EMP_WRIT"];
                                        this._flex공정경로["NO_EMP_CONF"] = row["NO_EMP_CONF"];
                                        this._flex공정경로["NM_EMP_CONF"] = row["NM_EMP_CONF"];
                                        this._flex공정경로["CD_ITEM"] = dataRow1["CD_ITEM"];
                                        this._flex공정경로["NM_ITEM"] = dataRow1["NM_ITEM"];
                                        this._flex공정경로["STND_ITEM"] = dataRow1["STND_ITEM"];
                                        this._flex공정경로["UNIT_MO"] = dataRow1["UNIT_MO"];
                                        this._flex공정경로["NM_TP_OPPATH"] = row["NM_TP_OPPATH"];
                                        this._flex공정경로["TP_PROC"] = dataRow1["TP_PROC"];
                                        this._flex공정경로["CLS_ITEM"] = dataRow1["CLS_ITEM"];
                                        this._flex공정경로["CD_DEPT"] = row["CD_DEPT"];
                                        this._flex공정경로["NO_EMP"] = row["NO_EMP"];
                                        this._flex공정경로["CD_PLANT"] = dataRow1["CD_PLANT"];
                                        this._flex공정경로["TP_BOM"] = dataRow1["TP_BOM"];
                                        this._flex공정.DataView.RowFilter = "CD_ITEM = '" + this._flex공정경로["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로["NO_OPPATH"].ToString() + "'";
                                        DataRow[] dataRowArray3 = this.dt_PasteD.Select("CD_ITEM = '" + row["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + row["NO_OPPATH"].ToString() + "'");
                                        
                                        if (dataRowArray3 != null && dataRowArray3.Length != 0)
                                        {
                                            foreach (DataRow dataRow2 in dataRowArray3)
                                            {
                                                this.btn추가_Click(null, (EventArgs)null);
                                                this._flex공정["CD_PLANT"] = this._flex공정경로["CD_PLANT"];
                                                this._flex공정["CD_ITEM"] = this._flex공정경로["CD_ITEM"];
                                                this._flex공정["CD_WCOP"] = dataRow2["CD_WCOP"];
                                                this._flex공정["NM_OP"] = dataRow2["NM_OP"];
                                                this._flex공정["CD_OP"] = dataRow2["CD_OP"];
                                                this._flex공정["CD_WC"] = dataRow2["CD_WC"];
                                                this._flex공정["NM_WC"] = dataRow2["NM_WC"];
                                                this._flex공정["NO_OPPATH"] = this._flex공정경로["NO_OPPATH"];
                                                this._flex공정["TP_OPPATH"] = this._flex공정경로["TP_OPPATH"];
                                                this._flex공정["TM_SETUP"] = dataRow2["TM_SETUP"];
                                                this._flex공정["TM"] = dataRow2["TM"];
                                                this._flex공정["TM_MOVE"] = dataRow2["TM_MOVE"];
                                                this._flex공정["CD_RSRC"] = dataRow2["CD_RSRC"];
                                                this._flex공정["TP_RSRC"] = dataRow2["TP_RSRC"];
                                                this._flex공정["YN_RECEIPT"] = dataRow2["YN_RECEIPT"];
                                                this._flex공정["YN_BF"] = dataRow2["YN_BF"];
                                                this._flex공정["YN_QC"] = dataRow2["YN_QC"];
                                                this._flex공정["QT_OVERLAP"] = dataRow2["QT_OVERLAP"];
                                                this._flex공정["N_OPSPLIT"] = dataRow2["N_OPSPLIT"];
                                                this._flex공정["YN_PAR"] = dataRow2["YN_PAR"];
                                                this._flex공정["DY_SBCNT"] = dataRow2["DY_SBCNT"];
                                                this._flex공정["CD_TOOL"] = dataRow2["CD_TOOL"];
                                                this._flex공정["CD_MATL"] = dataRow1["CD_ITEM"];
                                                this._flex공정["NM_ITEM"] = dataRow1["NM_ITEM"];
                                                this._flex공정["STND_ITEM"] = dataRow1["STND_ITEM"];
                                                this._flex공정["UNIT_MO"] = dataRow1["UNIT_MO"];
                                                this._flex공정["DC_OPPATH"] = dataRow2["DC_OPPATH"];
                                                this._flex공정["NM_WCOP"] = dataRow2["NM_WCOP"];
                                                this._flex공정["QT_LABOR_PLAN"] = dataRow2["QT_LABOR_PLAN"];
                                                this._flex공정["SET_REASON"] = dataRow2["SET_REASON"];
                                                this._flex공정["SET_METHOD"] = dataRow2["SET_METHOD"];
                                                this._flex공정["DY_PLAN"] = dataRow2["DY_PLAN"];
                                                this._flex공정["NO_SFT"] = dataRow2["NO_SFT"];
                                                this._flex공정["NM_SFT"] = dataRow2["NM_SFT"];
                                                this._flex공정["YN_FINAL"] = dataRow2["YN_FINAL"];
                                                this._flex공정["DC_RMK"] = dataRow2["DC_RMK"];
                                                this._flex공정["INSERT_NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                                                this._flex공정["INSERT_NM_EMP"] = Global.MainFrame.LoginInfo.EmployeeName;
                                                this._flex공정["CD_EQUIP"] = dataRow2["CD_EQUIP"];
                                                this._flex공정["NM_EQUIP"] = dataRow2["NM_EQUIP"];
                                                this._flex공정["RT_YIELD"] = dataRow2["RT_YIELD"];
                                                this._flex공정["YN_ROUT_SU_IV"] = dataRow2["YN_ROUT_SU_IV"];
                                                this._flex공정["YN_INSP"] = dataRow2["YN_INSP"];
                                                this._flex공정["NUM_USERDEF1"] = dataRow2["NUM_USERDEF1"];
                                                this._flex공정["NUM_USERDEF2"] = dataRow2["NUM_USERDEF2"];
                                                this._flex공정["NUM_USERDEF3"] = dataRow2["NUM_USERDEF3"];
                                                this._flex공정["TXT_USERDEF1"] = dataRow2["TXT_USERDEF1"];
                                                this._flex공정["TXT_USERDEF2"] = dataRow2["TXT_USERDEF2"];
                                                this._flex공정["TXT_USERDEF3"] = dataRow2["TXT_USERDEF3"];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (this._flex공정경로.DataSource == null || !this._flex공정경로.HasNormalRow)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (this.dt_Paste == null || this.dt_Paste.Rows.Count == 0)
                    {
                        int num2 = (int)this.ShowMessage("SA_M000072");
                    }
                    else if (this._flex공정경로["NO_OPPATH"].ToString() == "" || this._flex공정경로["TP_OPPATH"].ToString() == "")
                    {
                        int num3 = (int)this.ShowMessage("붙여넣기 전에 공정경로유형 과 ROUTNO 를 입력해야 합니다.");
                    }
                    else if (this._flex공정경로["CD_ITEM"].ToString() == "")
                    {
                        int num4 = (int)this.ShowMessage("붙여넣기전에품목을입력해야합니다.");
                    }
                    else
                    {
                        this._flex공정경로.Focus();
                        int row1 = this._flex공정경로.FindRow("Y", this._flex공정경로.Rows.Fixed, this._flex공정경로.Cols["CHK"].Index, false, true, true);
                        
                        if (row1 < 1)
                        {
                            if (!this.PasteMainLogic());
                        }
                        else
                        {
                            int row2 = row1;
                            while (row2 <= this._flex공정경로.DataView.Count)
                            {
                                if (this._flex공정경로[row2, "CHK"].ToString() == "Y")
                                {
                                    this._flex공정경로.Select(row2, 1);
                                    if (!this.PasteMainLogic())
                                        return;
                                    row2 = this._flex공정경로.FindRow("Y", row2 + 1, this._flex공정경로.Cols["CHK"].Index, false, true, true);
                                    if (row1 == row2)
                                        break;
                                }
                            }
                            int num5 = (int)this.ShowMessage("SA_M000073");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.initState(false);
                this._flex품목.Redraw = true;
                this._flex공정경로.Redraw = true;
                this._flex공정.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
            }
        }

        private void Btn작업지침서추가_Click(object sender, EventArgs e)
        {
            string key, key1, key2, key3, serverPath, fileName;

            try
            {
                if (!this._flex공정.HasNormalRow) return;

                key = this._flex공정["CD_ITEM"].ToString();
                key1 = this._flex공정["NO_OPPATH"].ToString();
                key2 = this._flex공정["CD_OP"].ToString();
                key3 = this._flex공정["CD_WCOP"].ToString();

                serverPath = "/Upload/P_CZ_PR_ROUT_REG/";

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.PNG|PDF Files(*.PDF)|*.PDF";
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                fileName = FileMgr.GetUniqueFileName(Global.MainFrame.HostURL + serverPath + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3, openFileDialog.FileName);

                if (FileUploader.UploadFile(fileName, openFileDialog.FileName, serverPath, Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3) == "Success")
                {
                    this._flex작업지침서.Rows.Add();
                    this._flex작업지침서.Row = this._flex작업지침서.Rows.Count - 1;

                    this._flex작업지침서["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    this._flex작업지침서["CD_PLANT"] = this._flex공정["CD_PLANT"].ToString();
                    this._flex작업지침서["CD_ITEM"] = key;
                    this._flex작업지침서["NO_OPPATH"] = key1;
                    this._flex작업지침서["CD_OP"] = key2;
                    this._flex작업지침서["CD_WCOP"] = key3;
                    this._flex작업지침서["NO_SEQ"] = this.SeqMax();
                    this._flex작업지침서["NM_FILE"] = fileName;

                    this._flex작업지침서.AddFinished();
                    this._flex작업지침서.Col = this._flex작업지침서.Cols.Fixed;
                    this._flex작업지침서.Focus();

                    if (this._biz.SaveFile(this._flex작업지침서.GetChanges()))
                        this._flex작업지침서.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn작업지침서삭제_Click(object sender, EventArgs e)
        {
            string key, key1, key2, key3, serverPath, fileName;

            try
            {
                if (!this._flex작업지침서.HasNormalRow) return;

                fileName = this._flex작업지침서["NM_FILE"].ToString();

                key = this._flex공정["CD_ITEM"].ToString();
                key1 = this._flex공정["NO_OPPATH"].ToString();
                key2 = this._flex공정["CD_OP"].ToString();
                key3 = this._flex공정["CD_WCOP"].ToString();

                serverPath = "/Upload/P_CZ_PR_ROUT_REG";

                if (this.ShowMessage("파일을 삭제 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

                if (FileUploader.DeleteFile(serverPath, Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3, fileName))
				{
                    this._flex작업지침서.GetDataRow(this._flex작업지침서.Row).Delete();

                    if (this._biz.SaveFile(this._flex작업지침서.GetChanges()))
                        this._flex작업지침서.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn작업지침서열기_Click(object sender, EventArgs e)
        {
            string key, key1, key2, key3, serverPath, localPath, fileName;

            try
            {
                if (!this._flex작업지침서.HasNormalRow) return;

                fileName = this._flex작업지침서["NM_FILE"].ToString();

                key = this._flex공정["CD_ITEM"].ToString();
                key1 = this._flex공정["NO_OPPATH"].ToString();
                key2 = this._flex공정["CD_OP"].ToString();
                key3 = this._flex공정["CD_WCOP"].ToString();

                serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PR_ROUT_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3 + "/";
                localPath = Application.StartupPath + "/temp/";

                if (string.IsNullOrEmpty(fileName))
                    return;
                else
                {
                    WebClient wc = new WebClient();

                    Directory.CreateDirectory(localPath);
                    wc.DownloadFile(serverPath + fileName, localPath + fileName);
                    Process.Start(localPath + fileName);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn작업지침서미리보기_Click(object sender, EventArgs e)
        {
            string key, key1, key2, key3, serverPath, fileName;

            try
            {
                if (!this._flex작업지침서.HasNormalRow) return;

                fileName = this._flex작업지침서["NM_FILE"].ToString();

                key = this._flex공정["CD_ITEM"].ToString();
                key1 = this._flex공정["NO_OPPATH"].ToString();
                key2 = this._flex공정["CD_OP"].ToString();
                key3 = this._flex공정["CD_WCOP"].ToString();

                serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PR_ROUT_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3 + "/";

                if (string.IsNullOrEmpty(fileName))
                {
                    this.web미리보기.Navigate(string.Empty);
                    return;
                }

                this.web미리보기.Navigate(serverPath + fileName);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn측정항목_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_PR_ROUT_INSP_SUB dialog = new P_CZ_PR_ROUT_INSP_SUB();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool PasteMainLogic()
        {
            try
            {
                if (this.dt_Paste == null || this.dt_Paste.Rows.Count == 0)
                    return false;

                if (this.dt_Paste.Rows[0]["CD_ITEM"].ToString() == this._flex공정경로["CD_ITEM"].ToString() && this.dt_Paste.Rows[0]["NO_OPPATH"].ToString() == this._flex공정경로["NO_OPPATH"].ToString() && this.dt_Paste.Rows[0]["TP_OPPATH"].ToString() == this._flex공정경로["TP_OPPATH"].ToString())
                {
                    this.ShowMessage("PR_M000008");
                    return false;
                }

                DataRow[] dataRowArray = this._flex공정경로.DataView.ToTable().Select("CD_ITEM = '" + this._flex공정경로["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로["NO_OPPATH"].ToString() + "' AND TP_OPPATH = '" + this._flex공정경로["TP_OPPATH"].ToString() + "' ", "", DataViewRowState.OriginalRows);
                for (int index = 0; index < dataRowArray.Length; ++index)
                {
                    dataRowArray[index]["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    dataRowArray[index]["CD_ITEM"] = this._flex공정경로["CD_ITEM"];
                    dataRowArray[index]["TP_OPPATH"] = this._flex공정경로["TP_OPPATH"];
                    dataRowArray[index]["NO_OPPATH"] = this._flex공정경로["NO_OPPATH"];
                    dataRowArray[index]["DC_OPPATH"] = this._flex공정경로["DC_OPPATH"];
                }

                if (this._flex공정.HasNormalRow)
                {
                    if (DialogResult.Yes != this.ShowMessage(공통메세지.자료를삭제하시겠습니까))
                        return false;
                    this._flex공정.Redraw = false;
                    this._flex공정.UnBindingStart();
                    this._flex공정.RemoveViewAll();
                    this._flex공정.UnBindingEnd();
                    this._flex공정.Redraw = true;
                }

                this._flex공정.Redraw = false;
                this._flex공정.UnBindingStart();

                for (int index = 0; index < this.dt_Paste.Rows.Count; ++index)
                {
                    if (this.dt_Paste.Rows[index]["CD_OP"].ToString().Trim() == "" || this.dt_Paste.Rows[index]["CD_WC"].ToString() == "" || this.dt_Paste.Rows[index]["CD_WCOP"].ToString() == "")
                        return false;
                    this._flex공정.Rows.Add();
                    this._flex공정.Row = this._flex공정.Rows.Count - this._flex공정.Rows.Fixed;
                    this._flex공정["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    this._flex공정["CD_ITEM"] = this._flex공정경로["CD_ITEM"];
                    this._flex공정["CD_WCOP"] = this.dt_Paste.Rows[index]["CD_WCOP"];
                    this._flex공정["NM_OP"] = this.dt_Paste.Rows[index]["NM_OP"];
                    this._flex공정["CD_OP"] = this.dt_Paste.Rows[index]["CD_OP"];
                    this._flex공정["CD_WC"] = this.dt_Paste.Rows[index]["CD_WC"];
                    this._flex공정["NM_WC"] = this.dt_Paste.Rows[index]["NM_WC"];
                    this._flex공정["NO_OPPATH"] = this._flex공정경로["NO_OPPATH"];
                    this._flex공정["TP_OPPATH"] = this._flex공정경로["TP_OPPATH"];
                    this._flex공정["TM_SETUP"] = this.dt_Paste.Rows[index]["TM_SETUP"];
                    this._flex공정["TM"] = this.dt_Paste.Rows[index]["TM"];
                    this._flex공정["TM_MOVE"] = this.dt_Paste.Rows[index]["TM_MOVE"];
                    this._flex공정["CD_RSRC"] = this.dt_Paste.Rows[index]["CD_RSRC"];
                    this._flex공정["TP_RSRC"] = this.dt_Paste.Rows[index]["TP_RSRC"];
                    this._flex공정["YN_RECEIPT"] = this.dt_Paste.Rows[index]["YN_RECEIPT"];
                    this._flex공정["YN_BF"] = this.dt_Paste.Rows[index]["YN_BF"];
                    this._flex공정["YN_QC"] = this.dt_Paste.Rows[index]["YN_QC"];
                    this._flex공정["QT_OVERLAP"] = this.dt_Paste.Rows[index]["QT_OVERLAP"];
                    this._flex공정["N_OPSPLIT"] = this.dt_Paste.Rows[index]["N_OPSPLIT"];
                    this._flex공정["YN_PAR"] = this.dt_Paste.Rows[index]["YN_PAR"];
                    this._flex공정["DY_SBCNT"] = this.dt_Paste.Rows[index]["DY_SBCNT"];
                    this._flex공정["CD_TOOL"] = this.dt_Paste.Rows[index]["CD_TOOL"];
                    this._flex공정["CD_MATL"] = this.dt_Paste.Rows[index]["CD_MATL"];
                    this._flex공정["NM_ITEM"] = this.dt_Paste.Rows[index]["NM_ITEM"];
                    this._flex공정["STND_ITEM"] = this.dt_Paste.Rows[index]["STND_ITEM"];
                    this._flex공정["UNIT_MO"] = this.dt_Paste.Rows[index]["UNIT_MO"];
                    this._flex공정["YN_FINAL"] = !MA.ServerKey(false, "KODACO") ? (index != this.dt_Paste.Rows.Count - 1 ? "N" : "Y") : this.dt_Paste.Rows[index]["YN_FINAL"];
                    this._flex공정["CD_EQUIP"] = this.dt_Paste.Rows[index]["CD_EQUIP"];
                    this._flex공정["NM_EQUIP"] = this.dt_Paste.Rows[index]["NM_EQUIP"];
                    this._flex공정["RT_YIELD"] = this.dt_Paste.Rows[index]["RT_YIELD"];
                    this._flex공정["QT_LABOR_PLAN"] = this.dt_Paste.Rows[index]["QT_LABOR_PLAN"];
                    this._flex공정["SET_REASON"] = this.dt_Paste.Rows[index]["SET_REASON"];
                    this._flex공정["SET_METHOD"] = this.dt_Paste.Rows[index]["SET_METHOD"];
                    this._flex공정["DY_PLAN"] = this.dt_Paste.Rows[index]["DY_PLAN"];
                    this._flex공정["DC_RMK"] = this.dt_Paste.Rows[index]["DC_RMK"];
                    this._flex공정["NO_SFT"] = this.dt_Paste.Rows[index]["NO_SFT"];
                    this._flex공정["NM_SFT"] = this.dt_Paste.Rows[index]["NM_SFT"];
                    this._flex공정["YN_ROUT_SU_IV"] = this.dt_Paste.Rows[index]["YN_ROUT_SU_IV"];
                    this._flex공정["YN_INSP"] = this.dt_Paste.Rows[index]["YN_INSP"];
                    this._flex공정["NUM_USERDEF1"] = this.dt_Paste.Rows[index]["NUM_USERDEF1"];
                    this._flex공정["NUM_USERDEF2"] = this.dt_Paste.Rows[index]["NUM_USERDEF2"];
                    this._flex공정["NUM_USERDEF3"] = this.dt_Paste.Rows[index]["NUM_USERDEF3"];
                    this._flex공정["TXT_USERDEF1"] = this.dt_Paste.Rows[index]["TXT_USERDEF1"];
                    this._flex공정["TXT_USERDEF2"] = this.dt_Paste.Rows[index]["TXT_USERDEF2"];
                    this._flex공정["TXT_USERDEF3"] = this.dt_Paste.Rows[index]["TXT_USERDEF3"];
                    this._flex공정["YN_USE"] = this.dt_Paste.Rows[index]["YN_USE"];
                    this._flex공정.AddFinished();
                }

                this._flex공정.UnBindingEnd();
                this._flex공정.Redraw = true;
                this._flex공정.Col = this._flex공정.Cols.Fixed;

                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);

                return false;
            }
        }

        private bool HasNullColumn()
        {
            if (this._flex공정경로["CD_ITEM"].ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this._flex공정경로.Cols["CD_ITEM"].Caption);
                return true;
            }

            if (this._flex공정경로["TP_OPPATH"].ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this._flex공정경로.Cols["TP_OPPATH"].Caption);
                return true;
            }

            if (this._flex공정경로["NO_OPPATH"].ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this._flex공정경로.Cols["NO_OPPATH"].Caption);
                return true;
            }

            if (!(this._flex공정경로["DC_OPPATH"].ToString() == ""))
                return false;

            this.ShowMessage(공통메세지._은는필수입력항목입니다, this._flex공정경로.Cols["DC_OPPATH"].Caption);
            
            return true;
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                BpCodeNTextBox bpCodeNtextBox1 = sender as BpCodeNTextBox;
                BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
                BpComboBox bpComboBox = sender as BpComboBox;

                if (bpCodeNtextBox1 == null && bpCodeTextBox == null && bpComboBox == null)
                    return;

                string str1 = "|&&|";
                string str2 = "|&|";

                switch (e.HelpID)
                {
                    case HelpID.P_USER:
                        if (bpCodeNtextBox1.UserHelpID == "H_MA_PITEM_SUB")
                        {
                            if (!this.공장선택여부)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                                e.QueryCancel = true;
                                this.cbo공장.Focus();
                                break;
                            }
                            e.HelpParam.P41_CD_FIELD1 = "공장품목";
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                            e.HelpParam.P65_CODE5 = this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("("));
                            bpCodeNtextBox1.UserParams = "YN_PHANTOM" + str2 + "N" + str2 + "Unabled" + str1;
                            BpCodeNTextBox bpCodeNtextBox2 = bpCodeNtextBox1;
                            bpCodeNtextBox2.UserParams = bpCodeNtextBox2.UserParams + "TP_PROC" + str2 + "M" + str2 + "Enabled" + str2 + "('M', 'S')" + str1;
                            e.HelpParam.P61_CODE1 = str1;
                            e.HelpParam.P62_CODE2 = str2;
                            break;
                        }
                        break;
                    case HelpID.P_MA_CODE_SUB:
                        if (bpCodeTextBox.Name == this.ctx대분류.Name)
						{
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                        }
                        else if (bpCodeTextBox.Name == this.ctx중분류.Name)
						{
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                            e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(this.ctx대분류.CodeValue);
                        }
                        else if (bpCodeTextBox.Name == this.ctx소분류.Name)
						{
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                            e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(this.ctx중분류.CodeValue);
                        }
                        else if (bpCodeTextBox.Name == this.ctx제품군.Name)
						{
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                        }
                        break;
                    case HelpID.P_MA_WC_SUB1:
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                    case HelpID.P_PR_WCOP_SUB1:
                        if (D.GetString(this.bpc작업장.SelectedValue) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.bpc작업장.Text);
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P20_CD_WC = this.bpc작업장.QueryWhereIn_Pipe;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (!(e.ControlName == this.ctx품목From.Name))
                    return;
                this.ctx품목To.CodeValue = e.CodeValue;
                this.ctx품목To.CodeName = e.CodeName;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!(sender is Dass.FlexGrid.FlexGrid flexGrid)) return;

                if (flexGrid.Name == this._flex품목.Name)
				{
                    DataRow[] dataRowArray = this._flex공정경로.DataTable.Select(" CD_ITEM = '" + this._flex품목["CD_ITEM"].ToString() + "'", "", DataViewRowState.CurrentRows);
                    if (this._flex품목[e.Row, "CHK"].ToString() == "N")
                    {
                        for (int row = this._flex공정경로.Rows.Fixed; row < dataRowArray.Length + 1; ++row)
                        {
                            this._flex공정경로.SetCellCheck(row, this._flex공정경로.Cols["CHK"].Index, CheckEnum.Checked);
                            this._flex공정경로.Row = row;
                        }
                    }
                    for (int index = this._flex공정경로.Rows.Fixed; index < dataRowArray.Length + 1; ++index)
                    {
                        if (this._flex공정경로.RowState(index) != DataRowState.Deleted)
                            this._flex공정경로.SetCellCheck(index, this._flex공정경로.Cols["CHK"].Index, CheckEnum.Unchecked);
                    }
                }
                else if (flexGrid.Name == this._flex공정경로.Name)
				{
                    switch (flexGrid.Cols[flexGrid.Col].Name)
                    {
                        case "TP_OPPATH":
                        case "NO_OPPATH":
                            if (this._flex공정.DataView != null && this._flex공정.DataView.Count > 0)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "YN_FINAL":
                            if (flexGrid["YN_FINAL"].ToString() == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_OP":
                            if (flexGrid["CD_MATL"].ToString() != "")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                if (!(sender is Dass.FlexGrid.FlexGrid flexGrid)) return;

                if (flexGrid.Name == this._flex공정경로.Name)
				{
                    if (e.Result.HelpID == HelpID.P_MA_PITEM_SUB)
                    {
                        if (e.Result.Rows[0]["TP_PROC"].ToString() != "M")
                        {
                            this.ShowMessage("PR_M000013");
                            flexGrid.Editor.Text = "";
                        }
                        else
						{
                            DataRow[] dataRowArray1 = flexGrid.DataView.ToTable().Select("CD_ITEM = '" + e.Result.CodeValue + "'", "NO_OPPATH DESC", DataViewRowState.CurrentRows);
                            string str1;

                            if (dataRowArray1.Length <= 1)
                            {
                                str1 = "001";
                            }
                            else
                            {
                                str1 = (Convert.ToInt32(dataRowArray1[0]["NO_OPPATH"].ToString()) + 1).ToString();
                                while (str1.Length < dataRowArray1[0]["NO_OPPATH"].ToString().Length)
                                    str1 = "0" + str1;
                            }

                            this._flex공정경로["NO_OPPATH"] = str1;
                            if (this.cbo공정경로유형.DataSource is DataTable dataSource)
							{
                                string str2;
                                string str3;

                                if (flexGrid.DataView.ToTable().Select("CD_ITEM = '" + e.Result.CodeValue + "' AND TP_OPPATH = 'S'", "", DataViewRowState.CurrentRows).Length < 1)
                                {
                                    str2 = "S";
                                    str3 = "표준";
                                }
                                else
                                {
                                    DataRow[] dataRowArray2 = dataSource.Select("CODE NOT IN ('S', '')", "", DataViewRowState.CurrentRows);
                                    if (dataRowArray2 == null || dataRowArray2.Length == 0)
                                    {
                                        this.ShowMessage("공정경로유형이 표준('S')외에 없습니다. 같은 품목(@)을 여러건 추가할 수 없습니다.", e.Result.CodeValue);
                                        e.Cancel = true;
                                        return;
                                    }
                                    str2 = dataRowArray2[0]["CODE"].ToString();
                                    str3 = dataRowArray2[0]["NAME"].ToString();
                                }

                                this._flex공정경로["TP_OPPATH"] = str2;
                                flexGrid["DC_OPPATH"] = str3;
                                flexGrid["NM_TP_OPPATH"] = str3;
                                flexGrid.Focus();
                                flexGrid.Select(flexGrid.Row, "TP_OPPATH");
                                this._flex공정경로["CD_ITEM"] = e.Result.CodeValue;
                                this.ChangeFilter();
                            }
                        }   
                    }
                }
                else if (flexGrid.Name == this._flex공정.Name)
				{
                    if (e.Result.HelpID == HelpID.P_MA_WC_SUB)
                    {
                        flexGrid.Focus();
                        flexGrid.Select(flexGrid.Row, "CD_WCOP");
                    }
                    else if (e.Result.HelpID == HelpID.P_PR_WCOP_SUB)
                    {
                        flexGrid.Focus();
                        flexGrid.Select(flexGrid.Row, "TM_SETUP");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!(sender is Dass.FlexGrid.FlexGrid flexGrid)) return;
                string str1 = "|&&|";
                string str2 = "|&|";

                if (flexGrid.Name == this._flex공정경로.Name)
				{
                    if (this._flex공정.HasNormalRow)
                    {
                        e.Cancel = true;
                    }
                    else if (e.Parameter.HelpID == HelpID.P_MA_PITEM_SUB)
                    {
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.Parameter.P41_CD_FIELD1 = "M";
                    }
                }
                else if (flexGrid.Name == this._flex공정.Name)
				{
                    if (e.Parameter.HelpID == HelpID.P_MA_WC_SUB)
                    {
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.Parameter.P65_CODE5 = this.cbo공장.Text.Replace(" ", "").Remove(0, this.cbo공장.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                    }
                    else if (e.Parameter.HelpID == HelpID.P_PR_WCOP_SUB)
                    {
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.Parameter.P20_CD_WC = flexGrid["CD_WC"].ToString();
                    }
                    else if (e.Parameter.HelpID == HelpID.P_PR_RSRC_SUB)
                    {
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.Parameter.P20_CD_WC = flexGrid["CD_WC"].ToString();
                    }
                    else if (e.Parameter.UserHelpID == "H_PR_EQUIP_SUB")
                    {
                        e.Parameter.P41_CD_FIELD1 = "설비 도움창";
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.Parameter.P65_CODE5 = D.GetString(this.cbo공장.Text);
                    }
                    else if (e.Parameter.UserHelpID == "H_PR_SFT_SUB")
                    {
                        e.Parameter.P41_CD_FIELD1 = "SFT";
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.Parameter.P65_CODE5 = D.GetString(this.cbo공장.Text);
                        HelpParam parameter1 = e.Parameter;
                        parameter1.UserParams = parameter1.UserParams + "USE_Y" + str2 + "Y" + str2 + "Enabled" + str1;
                        HelpParam parameter2 = e.Parameter;
                        parameter2.UserParams = parameter2.UserParams + "USE_N" + str2 + "N" + str2 + "Enabled" + str1;
                        HelpParam parameter3 = e.Parameter;
                        parameter3.UserParams = parameter3.UserParams + "USE_C" + str2 + "C" + str2 + "Enabled" + str1;
                        e.Parameter.P61_CODE1 = str1;
                        e.Parameter.P62_CODE2 = str2;
                    }
                    switch (this._flex공정.Cols[e.Col].Name)
                    {
                        case "CD_USERDEF1":
                            e.Parameter.P41_CD_FIELD1 = "CZ_WIN0016";
                            break;
                    }
                }
                else if (flexGrid.Name == this._flex설비.Name)
				{
                    if (e.Parameter.UserHelpID == "H_PR_EQUIP_SUB")
                    {
                        e.Parameter.P41_CD_FIELD1 = "설비 도움창";
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.Parameter.P65_CODE5 = D.GetString(this.cbo공장.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex공정경로_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this.Verify_Grid(this._flex공정경로))
                    e.Cancel = true;
                if (this.bGridrowChanging)
                    return;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex공정경로_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;
                Dass.FlexGrid.FlexGrid flexGrid = sender as Dass.FlexGrid.FlexGrid;
                if (sender == null)
                    return;
                this.ChangeFilter();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ToolBarSearchButtonEnabled = true;
                this.bGridrowChanging = true;
            }
        }

        private void _flex품목_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!(sender is Dass.FlexGrid.FlexGrid flexGrid))
                    return;
                DataTable dt = new DataTable();
                string filter = " CD_ITEM = '" + flexGrid["CD_ITEM"].ToString() + "'";
                object[] objArray = new object[11]
                {
           this.LoginInfo.CompanyCode,
           this.cbo공장.SelectedValue.ToString(),
           this.ctx품목From.CodeValue,
           this.ctx품목To.CodeValue,
           this.cbo품목계정.SelectedValue.ToString(),
           this.cbo공정경로유형.SelectedValue.ToString(),
           flexGrid["CD_ITEM"].ToString(),
           this.cbo조달구분.SelectedValue.ToString(),
           this.bpc작업장.QueryWhereIn_Pipe,
           this.bpc공정.QueryWhereIn_Pipe,
           Global.SystemLanguage.MultiLanguageLpoint
                };
                if (flexGrid.DetailQueryNeed)
                    dt = this._biz.Search공정경로유형(objArray);
                flexGrid.DetailGrids[0].BindingAdd(dt, filter);
                flexGrid.DetailQueryNeed = false;
                if (!flexGrid.DetailGrids[0].HasNormalRow)
                    this._flex공정.RowFilter = " CD_ITEM = '' AND NO_OPPATH = '' ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid flexGrid = (Dass.FlexGrid.FlexGrid)sender;
                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "CD_OP":
                        if (!this._flex공정.IsDataChanged)
                            break;
                        string str = this._flex공정.DataTable.Compute("MAX(CD_OP)", "CD_ITEM= '" + this._flex공정경로["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로["NO_OPPATH"].ToString() + "'").ToString();
                        for (int row = 1; row < this._flex공정.Rows.Count; ++row)
                            this._flex공정[row, "YN_FINAL"] = !(this._flex공정[row, "CD_OP"].ToString() == str) ? "N" : "Y";
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
            DataTable dt;
            DataRow drTemp;
            string oldValue, newValue;
            try
            {
                FlexGrid flex = (FlexGrid)sender;
                switch (flex.Cols[flex.Col].Name)
                {
                    case "TP_OPPATH":
                        flex["DC_OPPATH"] = flex.Editor.Text;
                        flex["NM_TP_OPPATH"] = flex.Editor.Text;
                        if (this.strROUTNO자동입력여부 == "100")
                        {
                            string filterExpression = " CD_ITEM= '" + D.GetString(flex["CD_ITEM"]) + "' AND NO_OPPATH = '" + D.GetString(this._flex공정경로["TP_OPPATH"]) + "'";
                            if (flex.DataTable.Select(filterExpression).Length == 0)
                                flex["NO_OPPATH"] = D.GetString(flex["TP_OPPATH"]).ToUpper();
                        }
                        if (flex[flex.Cols[flex.Col].Name].ToString() == "S")
                        {
                            string filterExpression = "CD_ITEM = '" + flex["CD_ITEM"].ToString() + "' AND TP_OPPATH = 'S'";
                            if (flex.DataView.ToTable().Select(filterExpression, "", DataViewRowState.CurrentRows).Length > 0)
                            {
                                this.ShowMessage("같은 품목(@)에 공정경로유형이 표준(S)인 경우가 있습니다. 한 품목에 표준은 1건만 있을 수 있습니다.", flex["CD_ITEM"].ToString());
                                e.Cancel = true;
                                break;
                            }
                        }
                        if (!this.Verify_Grid(flex))
                        {
                            e.Cancel = true;
                            break;
                        }
                        this.ChangeFilter();
                        break;
                    case "NO_OPPATH":
                        flex.Editor.Text = flex.Editor.Text.ToUpper();
                        if (!this.Verify_Grid(flex))
                        {
                            e.Cancel = true;
                            break;
                        }
                        this.ChangeFilter();
                        break;
                    case "CD_OP":
                        flex.Editor.Text = flex.Editor.Text.ToUpper();
                        break;
                    case "TM_SETUP":
                    case "TM":
                    case "TM_MOVE":
                        string str = D.GetString(flex[flex.Cols[e.Col].Name]).Replace(":", "");
                        if (str == "")
                            flex[flex.Cols[e.Col].Name] = "00:00:00";
                        if (str.Length != 6)
                        {
                            e.Cancel = true;
                            break;
                        }
                        if (int.Parse(str.Substring(2, 2)) >= 60 || int.Parse(str.Substring(4, 2)) >= 60)
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "YN_FINAL":
                        if (flex.EditData != "Y") break;

                        for (int row = this._flex공정.Rows.Fixed; row < this._flex공정.Rows.Count; ++row)
                            this._flex공정[row, "YN_FINAL"] = "N";
                        
                        break;
                    case "DC_RMK1":
                        oldValue = flex[e.Row, e.Col].ToString();
                        newValue = flex.EditData;

                        dt = new DataTable();

                        dt.Columns.Add("CD_COMPANY");
                        dt.Columns.Add("CD_ITEM");
                        dt.Columns.Add("DC_RMK1");

                        drTemp = dt.NewRow();

                        drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                        drTemp["CD_ITEM"] = flex["CD_ITEM"].ToString();
                        drTemp["DC_RMK1"] = newValue;

                        dt.Rows.Add(drTemp);
                        drTemp.AcceptChanges();
                        drTemp.SetModified();

                        if (dt.Rows.Count > 0)
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_PR_ROUT_REG_JSON1", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ChangeFilter()
        {
            DataTable dt = new DataTable();
            string filter = " CD_ITEM= '" + this._flex공정경로["CD_ITEM"].ToString() + "' AND NO_OPPATH = '" + this._flex공정경로["NO_OPPATH"].ToString() + "'";

            this.st.strItem = this._flex공정경로["CD_ITEM"].ToString();
            this.st.strNO_OPPATH = this._flex공정경로["NO_OPPATH"].ToString();
            
            if (!this.htDetailQueryCollection.Contains(this.st))
            {
                object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   this.cbo공장.SelectedValue.ToString(),
                                                   this._flex공정경로["NO_OPPATH"].ToString(),
                                                   this._flex공정경로["CD_ITEM"].ToString(),
                                                   this.bpc작업장.QueryWhereIn_Pipe,
                                                   this.bpc공정.QueryWhereIn_Pipe,
                                                   Global.SystemLanguage.MultiLanguageLpoint,
                                                   this.cbo측정항목등록.SelectedValue.ToString(),
                                                   this.cbo작업지침서등록.SelectedValue.ToString(),
                                                   (this.chk공정사용여부.Checked == true ? "Y" : "N") };

                this.htDetailQueryCollection.Add(this.st, null);
                dt = this._biz.Search공정(objArray);
            }

            this._flex공정.BindingAdd(dt, filter);
        }

        private void _flex공정경로_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid) || !flexGrid.HasNormalRow || (flexGrid[flexGrid.Rows.Fixed, "CHK"].ToString() == "Y" ? 1 : 2) == 2)
                    return;

                int row1 = flexGrid.Row;
                
                for (int row2 = flexGrid.Rows.Fixed; row2 <= flexGrid.Rows.Count - 1; ++row2)
                {
                    if (flexGrid.DetailQueryNeedByRow(row2))
                        flexGrid.Row = row2;
                }

                flexGrid.Row = row1;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex공정_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dt1, dt2;
            string key, key1, key2, key3, filter;

            try
            {
                dt = null;
                dt1 = null;
                dt2 = null;
                key = this._flex공정["CD_ITEM"].ToString();
                key1 = this._flex공정["NO_OPPATH"].ToString();
                key2 = this._flex공정["CD_OP"].ToString();
                key3 = this._flex공정["CD_WCOP"].ToString();

                filter = "CD_ITEM = '" + key + "' AND NO_OPPATH = '" + key1 + "' AND CD_OP = '" + key2 + "' AND CD_WCOP = '" + key3 + "'";

                if (this._flex공정.DetailQueryNeed == true)
                {
                    dt = this._biz.Search작업지침서(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   this.cbo공장.SelectedValue.ToString(),
                                                                   key,
                                                                   key1,
                                                                   key2,
                                                                   key3 });

                    dt1 = this._biz.Search측정항목(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this.cbo공장.SelectedValue.ToString(),
                                                                  key,
                                                                  key1,
                                                                  key2,
                                                                  key3,
                                                                  (this.chk측정항목사용여부.Checked == true ? "Y" : "N") });

                    dt2 = this._biz.Search설비(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                              this.cbo공장.SelectedValue.ToString(),
                                                              key,
                                                              key1,
                                                              key2,
                                                              key3 });
                }

                this._flex작업지침서.BindingAdd(dt, filter);
                this._flex측정항목.BindingAdd(dt1, filter);
                this._flex설비.BindingAdd(dt2, filter);

                this.web미리보기.Navigate(string.Empty);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetColCaption_USERDEF()
        {
            try
            {
                DataTable code1 = MA.GetCode("PR_0000107");
                for (int index = 1; index <= 9; ++index)
                {
                    string columnName = "NUM_USERDEF" + index.ToString();
                    if (code1.Rows.Count < index)
                    {
                        this._flex공정.Cols[columnName].Visible = false;
                    }
                    else
                    {
                        this._flex공정.Cols[columnName].Caption = D.GetString(code1.Rows[index - 1]["NAME"]);
                        this._flex공정.Cols[columnName].Visible = true;
                    }
                }

                DataTable code2 = MA.GetCode("PR_0000108");
                for (int index = 1; index <= 9; ++index)
                {
                    string columnName = "TXT_USERDEF" + index.ToString();
                    if (code2.Rows.Count < index)
                    {
                        this._flex공정.Cols[columnName].Visible = false;
                    }
                    else
                    {
                        this._flex공정.Cols[columnName].Caption = D.GetString(code2.Rows[index - 1]["NAME"]);
                        this._flex공정.Cols[columnName].Visible = true;
                    }
                }

                DataTable code3 = MA.GetCode("PR_0000109");
                for (int index = 1; index <= 3; ++index)
                {
                    string columnName = "CD_USERDEF" + index.ToString();
                    if (code3.Rows.Count < index)
                    {
                        this._flex공정.Cols[columnName].Visible = false;
                    }
                    else
                    {
                        this._flex공정.Cols[columnName].Caption = D.GetString(code3.Rows[index - 1]["NAME"]);
                        this._flex공정.Cols[columnName].Visible = true;
                    }
                    this._flex공정.Cols["CD_USERDEF1"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private Decimal SeqMax()
        {
            Decimal num = 1;
            string query = @"SELECT MAX(NO_SEQ) AS NO_SEQ 
                             FROM CZ_PR_ROUT_FILE WITH(NOLOCK)  
                             WHERE CD_COMPANY = '{0}'
                             AND CD_PLANT = '{1}'
                             AND CD_ITEM = '{2}'
                             AND NO_OPPATH = '{3}'
                             AND CD_OP = '{4}'
                             AND CD_WCOP = '{5}'";

            query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                         this.cbo공장.SelectedValue.ToString(),
                                         this._flex공정["CD_ITEM"].ToString(),
                                         this._flex공정["NO_OPPATH"].ToString(),
                                         this._flex공정["CD_OP"].ToString(),
                                         this._flex공정["CD_WCOP"].ToString());

            DataTable dataTable = DBHelper.GetDataTable(query);

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["NO_SEQ"]) + 1);

            if (num <= this._flex작업지침서.GetMaxValue("NO_SEQ"))
                num = (this._flex작업지침서.GetMaxValue("NO_SEQ") + 1);

            return num;
        }

        private Decimal Seq측정항목Max()
        {
            Decimal num = 1;
            string query;

            query = @"SELECT MAX(NO_INSP) AS SEQ
                      FROM CZ_PR_ROUT_INSP WITH(NOLOCK)
                      WHERE CD_COMPANY = '{0}'
                      AND CD_PLANT = '{1}'
                      AND CD_ITEM = '{2}'
                      AND NO_OPPATH = '{3}'
                      AND CD_OP = '{4}'
                      AND CD_WCOP = '{5}'";

            query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                         this.cbo공장.SelectedValue,
                                         this._flex공정["CD_ITEM"].ToString(),
                                         this._flex공정["NO_OPPATH"].ToString(),
                                         this._flex공정["CD_OP"].ToString(),
                                         this._flex공정["CD_WCOP"].ToString());

            DataTable dataTable = DBHelper.GetDataTable(query);

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

            if (this._flex측정항목.DataTable.Rows.Count > 0 && num <= this._flex측정항목.GetMaxValue("NO_INSP"))
                num = (this._flex측정항목.GetMaxValue("NO_INSP") + 1);

            return num;
        }
    }
}