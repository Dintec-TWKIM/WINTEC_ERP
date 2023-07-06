using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.ERPU.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_MA_PITEM_WINTEC : PageBase
    {
        private string 회사코드 = MA.Login.회사코드;
        private P_CZ_MA_PITEM_WINTEC_BIZ _biz = new P_CZ_MA_PITEM_WINTEC_BIZ();
        private List<object> deleteRowList = (List<object>)null;
        private DataRow row복사 = (DataRow)null;
        private string _sCls_Lms = "";
        private string m_ImageFilePath = string.Empty;
        private string[] image_delete = new string[6];
        private IU_Language L = new IU_Language();

        public P_CZ_MA_PITEM_WINTEC()
        {
            this.InitializeComponent();
            this.MainGrids = new Dass.FlexGrid.FlexGrid[2]
            {
        this._flex,
        this._flex버전관리
            };
            this._flex.DetailGrids = new Dass.FlexGrid.FlexGrid[1]
            {
        this._flex버전관리
            };
            this.DataChanged += new EventHandler(this.Page_DataChanged);
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.m_ImageFilePath = Application.StartupPath + "\\ItemImages";
            this.InitGrid();
            this.InitEvent();
            if (BASIC.GetMAEXC("공장품목등록-버전관리탭") == "000")
                this.tab.TabPages.Remove(this.tag버전관리);
            if (BASIC.GetMAEXC("공장품목등록-회사간품목전송") == "100")
                this.btnCopyToCompany.Visible = true;
            if (BASIC.GetMAEXC("공장품목등록-입력영역스크롤바활성화") == "100")
            {
                this.tab.SizeMode = TabSizeMode.Normal;
                this.splitContainer1.FixedPanel = FixedPanel.None;
                this.splitContainer1.Panel2MinSize = 25;
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CD_PLANT", "공장코드", false);
            this._flex.SetCol("CD_ITEM", "품목코드", 90);
            string languageTag = this.L.GetLanguageTag("NM_ITEM");
            this.txt품명.Tag = languageTag;
            this._flex.SetCol(languageTag, "품목명", 90);
            if (languageTag != "NM_ITEM")
                this._flex.SetCol("NM_ITEM", "품목명", false);
            this._flex.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex.SetCol("TP_ITEM", "품목타입", false);
            this._flex.SetCol("UNIT_IM", "재고단위", false);
            this._flex.SetCol("UNIT_SO", "수주단위", false);
            this._flex.SetCol("UNIT_PO", "발주단위", false);
            this._flex.SetCol("UNIT_MO", "생산단위", false);
            this._flex.SetCol("UNIT_SU", "외주단위", false);
            this._flex.SetCol("URL_ITEM", "품목URL", false);
            this._flex.SetCol("MAT_ITEM", "재질", false);
            this._flex.SetCol("STND_ITEM", "규격", 60);
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", 60);
            this._flex.SetCol("TP_PART", "내외자구분", false);
            this._flex.SetCol("BARCODE", "BARCODE", false);
            this._flex.SetCol("DT_VALID", "유효일", 0, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.Cols["DT_VALID"].Visible = false;
            this._flex.SetCol("YN_USE", "사용유무", false);
            this._flex.SetCol("CD_PURGRP", "구매그룹", false);
            this._flex.SetCol("LT_GI", "출하LT", 60, false, typeof(Decimal));
            this._flex.Cols["LT_GI"].Visible = false;
            this._flex.SetCol("QT_SSTOCK", "안전재고", 60, false, typeof(Decimal));
            this._flex.Cols["QT_SSTOCK"].Visible = false;
            this._flex.SetCol("LT_SAFE", "안전LT", 60, false, typeof(Decimal));
            this._flex.Cols["LT_SAFE"].Visible = false;
            this._flex.SetCol("DY_IMCLY", "순환실사주기", 60, false, typeof(Decimal));
            this._flex.Cols["DY_IMCLY"].Visible = false;
            this._flex.SetCol("DT_IMMNG", "최종재고실사일", 60, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.Cols["DT_IMMNG"].Visible = false;
            this._flex.SetCol("QT_ROP", "ROP수량", 60, false, typeof(Decimal));
            this._flex.Cols["QT_ROP"].Visible = false;
            this._flex.SetCol("FG_BF", "BF여부", false);
            this._flex.SetCol("DY_VALID", "유효기간", 0, false, typeof(Decimal));
            this._flex.Cols["DY_VALID"].Visible = false;
            this._flex.SetCol("NO_MODEL", "모델번호", false);
            this._flex.SetCol("FG_GIR", "불출관리", false);
            this._flex.SetCol("TP_MANU", "제조전략", false);
            this._flex.SetCol("YN_PHANTOM", "PHANTOM구분", false);
            this._flex.SetCol("TP_BOM", "BOM형태", false);
            this._flex.SetCol("STND_ST", "표준ST", 60, false);
            this._flex.SetCol("FG_LONG", "장단납기구분", false);
            this._flex.SetCol("QT_MIN", "최소수배수", 60, false, typeof(Decimal));
            this._flex.Cols["QT_MIN"].Visible = false;
            this._flex.SetCol("QT_MAX", "최대수배수", 60, false, typeof(Decimal));
            this._flex.Cols["QT_MAX"].Visible = false;
            this._flex.SetCol("RT_QM", "품목불량율", 60, false, typeof(Decimal));
            this._flex.Cols["RT_QM"].Visible = false;
            this._flex.SetCol("PARTNER", "주거래처", false);
            this._flex.SetCol("FG_TRACKING", "TRACKING", false);
            this._flex.SetCol("RT_MINUS", "과입고허용율(-)", 60, false, typeof(Decimal));
            this._flex.Cols["RT_MINUS"].Visible = false;
            this._flex.SetCol("RT_PLUS", "과입고허용율(+)", 60, false, typeof(Decimal));
            this._flex.Cols["RT_PLUS"].Visible = false;
            this._flex.SetCol("LT_ITEM", "품목LT", 60, false, typeof(Decimal));
            this._flex.Cols["LT_ITEM"].Visible = false;
            this._flex.SetCol("TP_PO", "발주방침", false);
            this._flex.SetCol("CLS_PO", "발주정책", false);
            this._flex.SetCol("QT_FOQ", "FOQ수량", 60, false, typeof(Decimal));
            this._flex.Cols["QT_FOQ"].Visible = false;
            this._flex.SetCol("FG_FOQ", "FOQ오더정리", 60, false, typeof(Decimal));
            this._flex.Cols["FG_FOQ"].Visible = false;
            this._flex.SetCol("DY_POQ", "POQ기간", 60, false, typeof(Decimal));
            this._flex.Cols["DY_POQ"].Visible = false;
            this._flex.SetCol("PATN_ROUT", "제조패턴", false);
            this._flex.SetCol("FG_LOTNO", "LOT구분", false);
            this._flex.SetCol("LOTSIZE", "LOT크기", 60, true, typeof(Decimal));
            this._flex.Cols["LOTSIZE"].Visible = false;
            this._flex.SetCol("FG_SERNO", "LOT,S/N관리", false);
            this._flex.SetCol("STAND_PRC", "표준단위원가", 60, true, typeof(Decimal));
            this._flex.Cols["STAND_PRC"].Visible = false;
            this._flex.SetCol("UPD", "UPD", 60, true, typeof(Decimal));
            this._flex.Cols["UPD"].Visible = false;
            this._flex.SetCol("UPH", "UPH", 60, true, typeof(Decimal));
            this._flex.Cols["UPH"].Visible = false;
            this._flex.SetCol("LT_QC", "검사LT", 60, true, typeof(Decimal));
            this._flex.Cols["LT_QC"].Visible = false;
            this._flex.SetCol("NO_STND", "규격번호", false);
            this._flex.SetCol("FG_IQC", "수입검사", false);
            this._flex.SetCol("FG_SQC", "출하검사", false);
            this._flex.SetCol("FG_OQC", "외주검사", false);
            this._flex.SetCol("FG_OPQC", "공정검사", false);
            this._flex.SetCol("FG_SLQC", "이동검사", false);
            this._flex.SetCol("CD_BIZAREA", "사업장", false);
            this._flex.SetCol("UNIT_SO_FACT", "수주단위수량", 60, true, typeof(Decimal));
            this._flex.Cols["UNIT_SO_FACT"].Visible = false;
            this._flex.SetCol("UNIT_PO_FACT", "발주단위수량", 60, true, typeof(Decimal));
            this._flex.Cols["UNIT_PO_FACT"].Visible = false;
            this._flex.SetCol("UNIT_SU_FACT", "외주단위수량", 60, true, typeof(Decimal));
            this._flex.Cols["UNIT_SU_FACT"].Visible = false;
            this._flex.SetCol("UNIT_MO_FACT", "생산단위수량", 60, true, typeof(Decimal));
            this._flex.Cols["UNIT_MO_FACT"].Visible = false;
            this._flex.SetCol("CD_ZONE", "LOCATION", false);
            this._flex.SetCol("CLS_ITEM", "계정구분", 60);
            this._flex.SetCol("TP_PROC", "조달구분", 60);
            this._flex.SetCol("UNIT_GI", "출하단위", 60);
            this._flex.SetCol("UNIT_GI_FACT", "출하단위 수량", 60, true, typeof(Decimal));
            this._flex.SetCol("GRP_ITEM", "품목군", 80);
            this._flex.SetCol("NM_GRP_ITEM", "품목군명", 60);
            this._flex.SetCol("GRP_MFG", "제품군", 60);
            this._flex.SetCol("NO_HS", "HS CODE", 60);
            this._flex.SetCol("UNIT_HS", "HS단위", false);
            this._flex.SetCol("WEIGHT", "중량", 60, false, typeof(Decimal));
            this._flex.SetCol("UNIT_WEIGHT", "중량 단위", 60);
            this._flex.SetCol("NO_DESIGN", "도면번호", 60);
            this._flex.SetCol("FG_ABC", "ABC구분", 60);
            this._flex.SetCol("CD_SL", "입고창고코드", 60);
            this._flex.SetCol("NM_SL", "입고창고명", 60);
            this._flex.SetCol("CD_GISL", "출하창고코드", 60);
            this._flex.SetCol("NM_GISL", "출하창고명", 60);
            this._flex.SetCol("CLS_L", "대구분", 60);
            this._flex.SetCol("CLS_M", "중구분", 60);
            this._flex.SetCol("CLS_S", "소구분", 60);
            this._flex.SetCol("LN_PARTNER", "주거래처명", 60);
            this._flex.SetCol("NM_MAKER", "Maker", 100);
            this._flex.SetCol("NM_PURGRP", "구매구룹명", 60);
            this._flex.SetCol("FG_TAX_PU", "매입부가세", 60);
            this._flex.SetCol("FILE_PATH_MNG_HELP", "파일도움창_첨부파일", 200);
            this._flex.SetCol("FILE_PATH_MNG", "공장품목_품질텝_첨부파일", 200);
            this._flex.SetCol("NO_MANAGER1", "관리자1", 60);
            this._flex.SetCol("NM_MANAGER1", "관리자1명", 60);
            this._flex.SetCol("NO_MANAGER2", "관리자2", 60);
            this._flex.SetCol("NM_MANAGER2", "관리자2명", 60);
            this._flex.SetCol("FG_PQC", "생산입고검사", 60);
            this._flex.SetCol("FG_MODEL", "도면구분", 60);
            this._flex.SetCol("FG_TAX_SA", "매출부가세", 60);
            this._flex.SetCol("YN_ATP", "ATP적용여부", false);
            this._flex.SetCol("CUR_ATP_DAY", "ATP허용일", 0, false, typeof(Decimal));
            this._flex.Cols["CUR_ATP_DAY"].Visible = false;
            this._flex.SetCol("INSERT_ID", "등록자코드", 80, false);
            this._flex.SetCol("NM_INSERT_ID", "등록자명", 80, false);
            this._flex.SetCol("DTS_INSERT", "등록일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("UPDATE_ID", "수정자코드", 80, false);
            this._flex.SetCol("NM_UPDATE_ID", "수정자명", 80, false);
            this._flex.SetCol("DTS_UPDATE", "수정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_USERDEF1", "사용자정의1", 60);
            this._flex.Cols["NM_USERDEF1"].Visible = false;
            this._flex.SetCol("NM_USERDEF2", "사용자정의2", 60);
            this._flex.Cols["NM_USERDEF2"].Visible = false;
            this._flex.SetCol("NM_USERDEF3", "사용자정의3", 60);
            this._flex.Cols["NM_USERDEF3"].Visible = false;
            this._flex.SetCol("NM_USERDEF4", "사용자정의4", 60);
            this._flex.Cols["NM_USERDEF4"].Visible = false;
            this._flex.SetCol("NM_USERDEF5", "사용자정의5", 60);
            this._flex.Cols["NM_USERDEF5"].Visible = false;
            this._flex.SetCol("CD_USERDEF1", "사용자정의6", 60);
            this._flex.Cols["CD_USERDEF1"].Visible = false;
            this._flex.SetCol("CD_USERDEF2", "사용자정의7", 60);
            this._flex.Cols["CD_USERDEF2"].Visible = false;
            this._flex.SetCol("CD_USERDEF3", "사용자정의8", 60);
            this._flex.Cols["CD_USERDEF3"].Visible = false;
            this._flex.SetCol("CD_USERDEF4", "사용자정의9", 60);
            this._flex.Cols["CD_USERDEF4"].Visible = false;
            this._flex.SetCol("CD_USERDEF5", "사용자정의10", 60);
            this._flex.Cols["CD_USERDEF5"].Visible = false;
            this._flex.SetCol("CD_USERDEF6", "사용자정의11", 60);
            this._flex.Cols["CD_USERDEF6"].Visible = false;
            this._flex.SetCol("CD_USERDEF7", "사용자정의12", 60);
            this._flex.Cols["CD_USERDEF7"].Visible = false;
            this._flex.SetCol("CD_USERDEF8", "사용자정의13", 60);
            this._flex.Cols["CD_USERDEF8"].Visible = false;
            this._flex.SetCol("CD_USERDEF9", "사용자정의14", 60);
            this._flex.Cols["CD_USERDEF9"].Visible = false;
            this._flex.SetCol("CD_USERDEF10", "사용자정의15", 60);
            this._flex.Cols["CD_USERDEF10"].Visible = false;
            this._flex.SetCol("CD_USERDEF11", "사용자정의26", 60);
            this._flex.Cols["CD_USERDEF11"].Visible = false;
            this._flex.SetCol("CD_USERDEF12", "사용자정의27", 60);
            this._flex.Cols["CD_USERDEF12"].Visible = false;
            this._flex.SetCol("CD_USERDEF13", "사용자정의28", 60);
            this._flex.Cols["CD_USERDEF13"].Visible = false;
            this._flex.SetCol("CD_USERDEF14", "사용자정의29", 60);
            this._flex.Cols["CD_USERDEF14"].Visible = false;
            this._flex.SetCol("CD_USERDEF15", "사용자정의30", 60);
            this._flex.Cols["CD_USERDEF15"].Visible = false;
            this._flex.SetCol("CD_USERDEF16", "사용자정의31", 60);
            this._flex.Cols["CD_USERDEF16"].Visible = false;
            this._flex.SetCol("CD_USERDEF17", "사용자정의32", 60);
            this._flex.Cols["CD_USERDEF17"].Visible = false;
            this._flex.SetCol("CD_USERDEF18", "사용자정의33", 60);
            this._flex.Cols["CD_USERDEF18"].Visible = false;
            this._flex.SetCol("CD_USERDEF19", "사용자정의34", 60);
            this._flex.Cols["CD_USERDEF19"].Visible = false;
            this._flex.SetCol("CD_USERDEF20", "사용자정의35", 60);
            this._flex.Cols["CD_USERDEF20"].Visible = false;
            this._flex.SetCol("NUM_USERDEF1", "사용자정의16", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF1"].Visible = false;
            this._flex.SetCol("NUM_USERDEF2", "사용자정의17", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF2"].Visible = false;
            this._flex.SetCol("NUM_USERDEF3", "사용자정의18", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF3"].Visible = false;
            this._flex.SetCol("NUM_USERDEF4", "사용자정의19", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF4"].Visible = false;
            this._flex.SetCol("NUM_USERDEF5", "사용자정의20", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF5"].Visible = false;
            this._flex.SetCol("NUM_USERDEF6", "사용자정의21", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF6"].Visible = false;
            this._flex.SetCol("NUM_USERDEF7", "사용자정의22", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF7"].Visible = false;
            this._flex.SetCol("NUM_USERDEF8", "사용자정의23", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF8"].Visible = false;
            this._flex.SetCol("NUM_USERDEF9", "사용자정의24", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF9"].Visible = false;
            this._flex.SetCol("NUM_USERDEF10", "사용자정의25", 60, true, typeof(Decimal));
            this._flex.Cols["NUM_USERDEF10"].Visible = false;
            this._flex.SetCol("CD_CC", "C/C코드", false);
            this._flex.SetCol("NM_CC", "C/C명", false);
            this._flex.SetCol("CD_ITEM_RELATION", "관련품목", false);
            this._flex.SetCol("NM_ITEM_RELATION", "관련품목", false);
            this._flex.SetCol("CD_STND_ITEM_1", "규격1", 60);
            this._flex.SetCol("CD_STND_ITEM_2", "규격2", 60);
            this._flex.SetCol("CD_STND_ITEM_3", "규격3", 60);
            this._flex.SetCol("CD_STND_ITEM_4", "규격4", 60);
            this._flex.SetCol("CD_STND_ITEM_5", "규격5", 60);
            this._flex.SetCol("NUM_STND_ITEM_1", "규격6", 60);
            this._flex.SetCol("NUM_STND_ITEM_2", "규격7", 60);
            this._flex.SetCol("NUM_STND_ITEM_3", "규격8", 60);
            this._flex.SetCol("NUM_STND_ITEM_4", "규격9", 60);
            this._flex.SetCol("NUM_STND_ITEM_5", "규격10", 60);
            this._flex.SetCol("CD_USERDEF21", "사용자정의36", 60);
            this._flex.Cols["CD_USERDEF21"].Visible = false;
            this._flex.SetCol("CD_USERDEF22", "사용자정의37", 60);
            this._flex.Cols["CD_USERDEF22"].Visible = false;
            this._flex.SetCol("UM_ROYALTY", "ROYALTY", 60, false, typeof(Decimal));
            this._flex.SetCol("NM_ITEM_L1", "다국어1", false);
            this._flex.SetCol("NM_ITEM_L2", "다국어2", false);
            this._flex.SetCol("NM_ITEM_L3", "다국어3", false);
            this._flex.SetCol("NM_ITEM_L4", "다국어4", false);
            this._flex.SetCol("NM_ITEM_L5", "다국어5", false);
            this._flex.SetCol("DC_RMK", "비고", false);
            this._flex.Cols["CD_STND_ITEM_1"].Visible = false;
            this._flex.Cols["CD_STND_ITEM_2"].Visible = false;
            this._flex.Cols["CD_STND_ITEM_3"].Visible = false;
            this._flex.Cols["CD_STND_ITEM_4"].Visible = false;
            this._flex.Cols["CD_STND_ITEM_5"].Visible = false;
            this._flex.Cols["NUM_STND_ITEM_1"].Visible = false;
            this._flex.Cols["NUM_STND_ITEM_2"].Visible = false;
            this._flex.Cols["NUM_STND_ITEM_3"].Visible = false;
            this._flex.Cols["NUM_STND_ITEM_4"].Visible = false;
            this._flex.Cols["NUM_STND_ITEM_5"].Visible = false;
            this._flex.SetCol("CD_USERDEF23", "사용자정의38", 60);
            this._flex.Cols["CD_USERDEF23"].Visible = false;
            this._flex.SetCol("CD_USERDEF24", "사용자정의39", 60);
            this._flex.Cols["CD_USERDEF24"].Visible = false;
            this._flex.SetOneGridBinding(new object[1]
            {
         this.txt품목코드
            }, this.pnl기본, this.pnl자재, this.pnl오더, this.pnl품질, this.pnl첨부파일, this.pnl기타정보, this.pnlFileList, this.pnlStndItem);
            ControlBinding controlBinding1 = (ControlBinding)null;
            if (this._sCls_Lms == "100")
                controlBinding1 = new ControlBinding(this._flex, new Control[1]
                {
           this.pnl자재
                }, new object[4]
                {
           this.txt품목코드,
           this.ctx대분류,
           this.ctx중분류,
           this.ctx소분류
                });
            this._flex.VerifyPrimaryKey = new string[2]
                {
          "CD_PLANT",
          "CD_ITEM"
                };
            this._flex.VerifyAutoDelete = new string[2]
            {
          "CD_ITEM",
          "NM_ITEM"
            };
            this._flex.BeforeRowChange += new RangeEventHandler(this._flex_BeforeRowChange);
            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex.DoubleClick += new EventHandler(this._flex_DoubleClick);
            this._flex.AddMyMenu = true;
            this._flex.AddMenuSeperator();
            ToolStripMenuItem parent = this._flex.AddPopup(this.DD("엑셀관리"));
            this._flex.AddMenuItem(parent, this.DD("파일생성"), new EventHandler(this.Menu_Click));
            this._flex.AddMenuItem(parent, this.DD("파일업로드"), new EventHandler(this.Menu_Click));
            this._flex.AddMenuItem(parent, this.DD("파일수정업로드"), new EventHandler(this.Menu_Click));
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flex버전관리.BeginSetting(1, 1, false);
            this._flex버전관리.SetCol("SEQ", "순번", 50, false);
            this._flex버전관리.SetCol("NM_VERSION", "버전", 50, false);
            this._flex버전관리.SetCol("NM_DEV_EMP", "개발자", 80, false);
            this._flex버전관리.SetCol("DTS_DSTB", "배포일자", 80, false);
            this._flex버전관리.SetCol("YN_USE", "지원여부", 100, false);
            ControlBinding controlBinding2 = new ControlBinding(this._flex버전관리, this.pnl버전관리, (object[])null);
            this._flex버전관리.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            int num = 0;
            if (this.splitContainer1.Width - this.splitContainer1.Panel2MinSize < 50)
                num = this.splitContainer1.Width - this.splitContainer1.Panel2MinSize - 50;
            this.splitContainer1.Panel2MinSize += num;
            this.splitContainer1.SplitterDistance = this.splitContainer1.Width - this.splitContainer1.Panel2MinSize;
            this._sCls_Lms = BASIC.GetMAEXC("공장품목등록-대중소분류수정여부");
            this.txt품목코드.MaxLength = this._biz.GetDigitPitem();
            this.Page_DataChanged(null, (EventArgs)null);
            this.btn엑셀업로드.Enabled = false;
            SetControl setControl = new SetControl();
            this.사용자정의셋팅_New();
            setControl.SetCombobox(this.cbo공장코드S, this.GetComboData("N;MA_PLANT").Tables[0]);
            setControl.SetCombobox(this.cbo조달구분S, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.조달구분, true));
            setControl.SetCombobox(this.cbo사용유무S, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            this.cbo사용유무S.SelectedValue = "Y";
            setControl.SetCombobox(this.cbo계정구분S, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목계정, true));
            setControl.SetCombobox(this.cbo제품군S, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.제품군, true));
            setControl.SetCombobox(this.cbo내외자구분S, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.내외자구분, true));
            setControl.SetCombobox(this.cbo계정구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목계정));
            setControl.SetCombobox(this.cbo조달구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.조달구분));
            setControl.SetCombobox(this.cbo출하단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo재고단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo수주단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo구매단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo생산단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo외주단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo품목타입, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목타입));
            setControl.SetCombobox(this.cbo내외자구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.내외자구분));
            setControl.SetCombobox(this.cboHS단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo중량단위, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위, true));
            setControl.SetCombobox(this.cbo도면유무, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo사용유무, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cbo매입형태, this.GetComboData("S;MA_AISPOSTH;200").Tables[0]);
            setControl.SetCombobox(this.cboABC구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.ABC구분));
            setControl.SetCombobox(this.cbo불출관리, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cbo과세구분매입, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.과세구분매입, true));
            setControl.SetCombobox(this.cbo과세구분매출, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.과세구분매출, true));
            setControl.SetCombobox(this.cboATP적용여부, MA.GetCode("MA_B000057", true));
            setControl.SetCombobox(this.cbo발주방침, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.발주방침, true));
            setControl.SetCombobox(this.cboLOT관리, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.LOTSN관리, true));
            setControl.SetCombobox(this.cbo트래킹, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cboBOM형태, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.BOM형태, true));
            setControl.SetCombobox(this.cbo제조전략, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.제조전략, true));
            setControl.SetCombobox(this.cbo장단납기구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cbo발주정책, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.발주정책, true));
            setControl.SetCombobox(this.cbo팬텀구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cbo백플러쉬, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무));
            setControl.SetCombobox(this.cbo수입검사여부, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo출하검사여부, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo생산입고검사, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo외주검사, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo공정검사, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo이동검사, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo생산입고검사불량, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo사용자정의6, MA.GetCode("MA_B000081", true));
            setControl.SetCombobox(this.cbo사용자정의7, MA.GetCode("MA_B000082", true));
            setControl.SetCombobox(this.cbo사용자정의8, MA.GetCode("MA_B000083", true));
            setControl.SetCombobox(this.cbo사용자정의9, MA.GetCode("MA_B000084", true));
            setControl.SetCombobox(this.cbo사용자정의10, MA.GetCode("MA_B000085", true));
            setControl.SetCombobox(this.cbo사용자정의11, MA.GetCode("MA_B000086", true));
            setControl.SetCombobox(this.cbo사용자정의12, MA.GetCode("MA_B000087", true));
            setControl.SetCombobox(this.cbo사용자정의13, MA.GetCode("MA_B000088", true));
            setControl.SetCombobox(this.cbo사용자정의14, MA.GetCode("MA_B000089", true));
            setControl.SetCombobox(this.cbo사용자정의15, MA.GetCode("MA_B000090", true));
            setControl.SetCombobox(this.cbo사용자정의26, MA.GetCode("MA_B000120", true));
            setControl.SetCombobox(this.cbo사용자정의27, MA.GetCode("MA_B000121", true));
            setControl.SetCombobox(this.cbo사용자정의28, MA.GetCode("MA_B000122", true));
            setControl.SetCombobox(this.cbo사용자정의29, MA.GetCode("MA_B000123", true));
            setControl.SetCombobox(this.cbo사용자정의33, MA.GetCode("MA_B000125", true));
            setControl.SetCombobox(this.cbo사용자정의34, MA.GetCode("MA_B000126", true));
            setControl.SetCombobox(this.cbo사용자정의35, MA.GetCode("MA_B000127", true));
            setControl.SetCombobox(this.cbo수입검사레벨, MA.GetCode("MA_B000131", true));
            setControl.SetCombobox(this.cbolotsn동시사용, MA.GetCodeUser(new string[2]
            {
        "100",
        "200"
            }, new string[2] { this.DD("사용안함"), this.DD("사용함") }, true));
            setControl.SetCombobox(this.cbo지원여부, MA.GetCodeUser(new string[2]
            {
        "Y",
        "N"
            }, new string[2] { "Y", "N" }));
            setControl.SetCombobox(this.cbo난이도, MA.GetCodeUser(new string[3]
            {
        "A",
        "B",
        "C"
            }, new string[3] { "A", "B", "C" }));
            setControl.SetCombobox(this.cbo최종소스여부, MA.GetCodeUser(new string[2]
            {
        "Y",
        "N"
            }, new string[2] { "Y", "N" }));
            this._flex.SetDataMap("CLS_ITEM", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목계정), "CODE", "NAME");
            this._flex.SetDataMap("TP_PROC", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.조달구분), "CODE", "NAME");
            this._flex.SetDataMap("UNIT_GI", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위), "CODE", "NAME");
            this._flex.SetDataMap("GRP_MFG", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.제품군), "CODE", "NAME");
            this._flex.SetDataMap("UNIT_WEIGHT", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위), "CODE", "NAME");
            this._flex.SetDataMap("FG_ABC", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.ABC구분), "CODE", "NAME");
            this._flex.SetDataMap("CLS_L", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.대분류), "CODE", "NAME");
            this._flex.SetDataMap("CLS_M", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.중분류), "CODE", "NAME");
            this._flex.SetDataMap("CLS_S", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.소분류), "CODE", "NAME");
            this._flex.SetDataMap("TP_ITEM", Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목타입), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF1", MA.GetCode("MA_B000081"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF2", MA.GetCode("MA_B000082"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF3", MA.GetCode("MA_B000083"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF4", MA.GetCode("MA_B000084"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF5", MA.GetCode("MA_B000085"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF6", MA.GetCode("MA_B000086"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF7", MA.GetCode("MA_B000087"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF8", MA.GetCode("MA_B000088"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF9", MA.GetCode("MA_B000089"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF10", MA.GetCode("MA_B000090"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF11", MA.GetCode("MA_B000120"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF12", MA.GetCode("MA_B000121"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF13", MA.GetCode("MA_B000122"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF14", MA.GetCode("MA_B000123"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF15", MA.GetCode("MA_B000136"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF16", MA.GetCode("MA_B000137"), "CODE", "NAME");
            this._flex.SetDataMap("CD_USERDEF17", MA.GetCode("MA_B000138"), "CODE", "NAME");
            if (BASIC.GetMAEXC("원가계산-요소배부") == "200")
            {
                this.lblcc코드.Visible = true;
                this.ctxcc.Visible = true;
            }
            bool flag = false;
            switch (BASIC.GetMAEXC("공장품목등록-첨부파일"))
            {
                case "000":
                    flag = false;
                    break;
                case "001":
                    flag = true;
                    break;
            }
            this.btn첨부파일.Visible = flag;
            switch (BASIC.GetMAEXC("업체별프로세스"))
            {
                case "001":
                    this.cur폭.Visible = true;
                    this.bp코어코드.Visible = true;
                    this.cur길이.Visible = true;
                    this.cbo매입형태.Visible = true;
                    break;
            }
            switch (BASIC.GetMAEXC("공장품목등록-자동채번"))
            {
                case "100":
                    this._btnAutoCdItem.Visible = true;
                    this.txt품목코드.Enabled = false;
                    break;
            }
            if (BASIC.GetMAEXC("공장품목등록-규격형") != "100")
                this.tab.Controls.Remove(this.tpgStndItem);
            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();
            object[] items = new object[11]
            {
         this.DD("상세검색조건"),
         this.DD("품목코드"),
         this.DD("품목명"),
         this.DD("규격"),
         this.DD("품목명(영)"),
         this.DD("세부규격"),
         this.DD("MAKER"),
         this.DD("BARCODE"),
         this.DD("모델코드"),
         this.DD("재질"),
         this.DD("도면번호")
            };
            this.cboSearch1.Items.AddRange(items);
            this.cboSearch2.Items.AddRange(items);
            this.cboSearch3.Items.AddRange(items);
            this.cboSearch1.SelectedIndex = 0;
            this.cboSearch2.SelectedIndex = 0;
            this.cboSearch3.SelectedIndex = 0;

            if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                this._chkUpper.Visible = true;
                this._chkUpper.Checked = true;
                this._chkUpper.CheckState = CheckState.Checked;
            }
        }

        private void 사용자정의셋팅_New()
        {
            for (int index = 1; index <= 5; ++index)
                this._flex.Cols["NM_USERDEF" + D.GetString(index)].Visible = false;
            DataTable code1 = this._biz.GetCode("MA_B000091");
            int num1 = 1;
            foreach (DataRow row in code1.Rows)
            {
                string str1 = D.GetString(row["NAME"]);
                string str2 = D.GetString(row["CD_FLAG1"]);
                string str3 = D.GetString(row["CODE"]);
                if (str2 == "Y")
                {
                    string str4 = string.Empty;
                    switch (str3)
                    {
                        case "010":
                            this.lbl사용자정의1.Text = str1;
                            this.lbl사용자정의1.Visible = this.txt사용자정의1.Visible = true;
                            str4 = "1";
                            break;
                        case "020":
                            this.lbl사용자정의2.Text = str1;
                            this.lbl사용자정의2.Visible = this.txt사용자정의2.Visible = true;
                            str4 = "2";
                            break;
                        case "030":
                            this.lbl사용자정의3.Text = str1;
                            this.lbl사용자정의3.Visible = this.txt사용자정의3.Visible = true;
                            str4 = "3";
                            break;
                        case "040":
                            this.lbl사용자정의4.Text = str1;
                            this.lbl사용자정의4.Visible = this.txt사용자정의4.Visible = true;
                            str4 = "4";
                            break;
                        case "050":
                            this.lbl사용자정의5.Text = str1;
                            this.lbl사용자정의5.Visible = this.txt사용자정의5.Visible = true;
                            str4 = "5";
                            break;
                    }
                    if (this._flex.Cols.Contains("NM_USERDEF" + str4))
                    {
                        this._flex.Cols["NM_USERDEF" + str4].Caption = str1;
                        this._flex.Cols["NM_USERDEF" + str4].Visible = true;
                    }
                }
                else if (string.IsNullOrEmpty(str2) || str2 != "Y" && str2 != "N")
                {
                    switch (num1)
                    {
                        case 1:
                            this.lbl사용자정의1.Text = str1;
                            this.lbl사용자정의1.Visible = this.txt사용자정의1.Visible = true;
                            break;
                        case 2:
                            this.lbl사용자정의2.Text = str1;
                            this.lbl사용자정의2.Visible = this.txt사용자정의2.Visible = true;
                            break;
                        case 3:
                            this.lbl사용자정의3.Text = str1;
                            this.lbl사용자정의3.Visible = this.txt사용자정의3.Visible = true;
                            break;
                        case 4:
                            this.lbl사용자정의4.Text = str1;
                            this.lbl사용자정의4.Visible = this.txt사용자정의4.Visible = true;
                            break;
                        case 5:
                            this.lbl사용자정의5.Text = str1;
                            this.lbl사용자정의5.Visible = this.txt사용자정의5.Visible = true;
                            break;
                    }
                    if (this._flex.Cols.Contains("NM_USERDEF" + D.GetString(num1)))
                    {
                        this._flex.Cols["NM_USERDEF" + D.GetString(num1)].Caption = str1;
                        this._flex.Cols["NM_USERDEF" + D.GetString(num1)].Visible = true;
                    }
                }
                ++num1;
            }
            for (int index = 1; index <= 10; ++index)
                this._flex.Cols["CD_USERDEF" + D.GetString(index)].Visible = false;
            DataTable code2 = this._biz.GetCode("MA_B000092");
            int num2 = 1;
            foreach (DataRow row in code2.Rows)
            {
                string str1 = D.GetString(row["NAME"]);
                string str2 = D.GetString(row["CD_FLAG1"]);
                string str3 = D.GetString(row["CODE"]);
                if (str2 == "Y")
                {
                    string str4 = string.Empty;
                    switch (str3)
                    {
                        case "010":
                            this.lbl사용자정의6.Text = str1;
                            this.lbl사용자정의6.Visible = this.cbo사용자정의6.Visible = true;
                            str4 = "1";
                            break;
                        case "020":
                            this.lbl사용자정의7.Text = str1;
                            this.lbl사용자정의7.Visible = this.cbo사용자정의7.Visible = true;
                            str4 = "2";
                            break;
                        case "030":
                            this.lbl사용자정의8.Text = str1;
                            this.lbl사용자정의8.Visible = this.cbo사용자정의8.Visible = true;
                            str4 = "3";
                            break;
                        case "040":
                            this.lbl사용자정의9.Text = str1;
                            this.lbl사용자정의9.Visible = this.cbo사용자정의9.Visible = true;
                            str4 = "4";
                            break;
                        case "050":
                            this.lbl사용자정의10.Text = str1;
                            this.lbl사용자정의10.Visible = this.cbo사용자정의10.Visible = true;
                            str4 = "5";
                            break;
                        case "060":
                            this.lbl사용자정의11.Text = str1;
                            this.lbl사용자정의11.Visible = this.cbo사용자정의11.Visible = true;
                            str4 = "6";
                            break;
                        case "070":
                            this.lbl사용자정의12.Text = str1;
                            this.lbl사용자정의12.Visible = this.cbo사용자정의12.Visible = true;
                            str4 = "7";
                            break;
                        case "080":
                            this.lbl사용자정의13.Text = str1;
                            this.lbl사용자정의13.Visible = this.cbo사용자정의13.Visible = true;
                            str4 = "8";
                            break;
                        case "090":
                            this.lbl사용자정의14.Text = str1;
                            this.lbl사용자정의14.Visible = this.cbo사용자정의14.Visible = true;
                            str4 = "9";
                            break;
                        case "100":
                            this.lbl사용자정의15.Text = str1;
                            this.lbl사용자정의15.Visible = this.cbo사용자정의15.Visible = true;
                            str4 = "10";
                            break;
                    }
                    if (this._flex.Cols.Contains("CD_USERDEF" + str4))
                    {
                        this._flex.Cols["CD_USERDEF" + str4].Caption = str1;
                        this._flex.Cols["CD_USERDEF" + str4].Visible = true;
                    }
                }
                else if (string.IsNullOrEmpty(str2) || str2 != "Y" && str2 != "N")
                {
                    switch (num2)
                    {
                        case 1:
                            this.lbl사용자정의6.Text = str1;
                            this.lbl사용자정의6.Visible = this.cbo사용자정의6.Visible = true;
                            break;
                        case 2:
                            this.lbl사용자정의7.Text = str1;
                            this.lbl사용자정의7.Visible = this.cbo사용자정의7.Visible = true;
                            break;
                        case 3:
                            this.lbl사용자정의8.Text = str1;
                            this.lbl사용자정의8.Visible = this.cbo사용자정의8.Visible = true;
                            break;
                        case 4:
                            this.lbl사용자정의9.Text = str1;
                            this.lbl사용자정의9.Visible = this.cbo사용자정의9.Visible = true;
                            break;
                        case 5:
                            this.lbl사용자정의10.Text = str1;
                            this.lbl사용자정의10.Visible = this.cbo사용자정의10.Visible = true;
                            break;
                        case 6:
                            this.lbl사용자정의11.Text = str1;
                            this.lbl사용자정의11.Visible = this.cbo사용자정의11.Visible = true;
                            break;
                        case 7:
                            this.lbl사용자정의12.Text = str1;
                            this.lbl사용자정의12.Visible = this.cbo사용자정의12.Visible = true;
                            break;
                        case 8:
                            this.lbl사용자정의13.Text = str1;
                            this.lbl사용자정의13.Visible = this.cbo사용자정의13.Visible = true;
                            break;
                        case 9:
                            this.lbl사용자정의14.Text = str1;
                            this.lbl사용자정의14.Visible = this.cbo사용자정의14.Visible = true;
                            break;
                        case 10:
                            this.lbl사용자정의15.Text = str1;
                            this.lbl사용자정의15.Visible = this.cbo사용자정의15.Visible = true;
                            break;
                    }
                    if (this._flex.Cols.Contains("CD_USERDEF" + D.GetString(num2)))
                    {
                        this._flex.Cols["CD_USERDEF" + D.GetString(num2)].Caption = str1;
                        this._flex.Cols["CD_USERDEF" + D.GetString(num2)].Visible = true;
                    }
                }
                ++num2;
            }
            for (int index = 1; index <= 10; ++index)
                this._flex.Cols["NUM_USERDEF" + D.GetString(index)].Visible = false;
            DataTable code3 = this._biz.GetCode("MA_B000093");
            int num3 = 1;
            foreach (DataRow row in code3.Rows)
            {
                string str1 = D.GetString(row["NAME"]);
                string str2 = D.GetString(row["CD_FLAG1"]);
                string str3 = D.GetString(row["CODE"]);
                if (str2 == "Y")
                {
                    string str4 = string.Empty;
                    switch (str3)
                    {
                        case "010":
                            this.lbl사용자정의16.Text = str1;
                            this.lbl사용자정의16.Visible = this.cur사용자정의16.Visible = true;
                            str4 = "1";
                            break;
                        case "020":
                            this.lbl사용자정의17.Text = str1;
                            this.lbl사용자정의17.Visible = this.cur사용자정의17.Visible = true;
                            str4 = "2";
                            break;
                        case "030":
                            this.lbl사용자정의18.Text = str1;
                            this.lbl사용자정의18.Visible = this.cur사용자정의18.Visible = true;
                            str4 = "3";
                            break;
                        case "040":
                            this.lbl사용자정의19.Text = str1;
                            this.lbl사용자정의19.Visible = this.cur사용자정의19.Visible = true;
                            str4 = "4";
                            break;
                        case "050":
                            this.lbl사용자정의20.Text = str1;
                            this.lbl사용자정의20.Visible = this.cur사용자정의20.Visible = true;
                            str4 = "5";
                            break;
                        case "060":
                            this.lbl사용자정의21.Text = str1;
                            this.lbl사용자정의21.Visible = this.cur사용자정의21.Visible = true;
                            str4 = "6";
                            break;
                        case "070":
                            this.lbl사용자정의22.Text = str1;
                            this.lbl사용자정의22.Visible = this.cur사용자정의22.Visible = true;
                            str4 = "7";
                            break;
                        case "080":
                            this.lbl사용자정의23.Text = str1;
                            this.lbl사용자정의23.Visible = this.cur사용자정의23.Visible = true;
                            str4 = "8";
                            break;
                        case "090":
                            this.lbl사용자정의24.Text = str1;
                            this.lbl사용자정의24.Visible = this.cur사용자정의24.Visible = true;
                            str4 = "9";
                            break;
                        case "100":
                            this.lbl사용자정의25.Text = str1;
                            this.lbl사용자정의25.Visible = this.cur사용자정의25.Visible = true;
                            str4 = "10";
                            break;
                    }
                    if (this._flex.Cols.Contains("NUM_USERDEF" + str4))
                    {
                        this._flex.Cols["NUM_USERDEF" + str4].Caption = str1;
                        this._flex.Cols["NUM_USERDEF" + str4].Visible = true;
                    }
                }
                else if (string.IsNullOrEmpty(str2) || str2 != "Y" && str2 != "N")
                {
                    switch (num3)
                    {
                        case 1:
                            this.lbl사용자정의16.Text = str1;
                            this.lbl사용자정의16.Visible = this.cur사용자정의16.Visible = true;
                            break;
                        case 2:
                            this.lbl사용자정의17.Text = str1;
                            this.lbl사용자정의17.Visible = this.cur사용자정의17.Visible = true;
                            break;
                        case 3:
                            this.lbl사용자정의18.Text = str1;
                            this.lbl사용자정의18.Visible = this.cur사용자정의18.Visible = true;
                            break;
                        case 4:
                            this.lbl사용자정의19.Text = str1;
                            this.lbl사용자정의19.Visible = this.cur사용자정의19.Visible = true;
                            break;
                        case 5:
                            this.lbl사용자정의20.Text = str1;
                            this.lbl사용자정의20.Visible = this.cur사용자정의20.Visible = true;
                            break;
                        case 6:
                            this.lbl사용자정의21.Text = str1;
                            this.lbl사용자정의21.Visible = this.cur사용자정의21.Visible = true;
                            break;
                        case 7:
                            this.lbl사용자정의22.Text = str1;
                            this.lbl사용자정의22.Visible = this.cur사용자정의22.Visible = true;
                            break;
                        case 8:
                            this.lbl사용자정의23.Text = str1;
                            this.lbl사용자정의23.Visible = this.cur사용자정의23.Visible = true;
                            break;
                        case 9:
                            this.lbl사용자정의24.Text = str1;
                            this.lbl사용자정의24.Visible = this.cur사용자정의24.Visible = true;
                            break;
                        case 10:
                            this.lbl사용자정의25.Text = str1;
                            this.lbl사용자정의25.Visible = this.cur사용자정의25.Visible = true;
                            break;
                    }
                    if (this._flex.Cols.Contains("NUM_USERDEF" + D.GetString(num3)))
                    {
                        this._flex.Cols["NUM_USERDEF" + D.GetString(num3)].Caption = str1;
                        this._flex.Cols["NUM_USERDEF" + D.GetString(num3)].Visible = true;
                    }
                }
                ++num3;
            }
            for (int index = 1; index <= 7; ++index)
                this._flex.Cols["CD_USERDEF1" + D.GetString(index)].Visible = false;
            DataTable code4 = this._biz.GetCode("MA_B000124");
            int num4 = 1;
            foreach (DataRow row in code4.Rows)
            {
                string str1 = D.GetString(row["NAME"]);
                string str2 = D.GetString(row["CD_FLAG1"]);
                string str3 = D.GetString(row["CODE"]);
                if (str2 == "Y")
                {
                    string str4 = string.Empty;
                    switch (str3)
                    {
                        case "010":
                            this.lbl사용자정의26.Text = str1;
                            this.lbl사용자정의26.Visible = this.cbo사용자정의26.Visible = true;
                            str4 = "1";
                            break;
                        case "020":
                            this.lbl사용자정의27.Text = str1;
                            this.lbl사용자정의27.Visible = this.cbo사용자정의27.Visible = true;
                            str4 = "2";
                            break;
                        case "030":
                            this.lbl사용자정의28.Text = str1;
                            this.lbl사용자정의28.Visible = this.cbo사용자정의28.Visible = true;
                            str4 = "3";
                            break;
                        case "040":
                            this.lbl사용자정의29.Text = str1;
                            this.lbl사용자정의29.Visible = this.cbo사용자정의29.Visible = true;
                            str4 = "4";
                            break;
                        case "050":
                            this.lbl사용자정의30.Text = str1;
                            this.lbl사용자정의30.Visible = this.bp_사용자정의30.Visible = true;
                            str4 = "5";
                            break;
                        case "060":
                            this.lbl사용자정의31.Text = str1;
                            this.lbl사용자정의31.Visible = this.bp_사용자정의31.Visible = true;
                            str4 = "6";
                            break;
                        case "070":
                            this.lbl사용자정의32.Text = str1;
                            this.lbl사용자정의32.Visible = this.bp_사용자정의32.Visible = true;
                            str4 = "7";
                            break;
                    }
                    if (this._flex.Cols.Contains("CD_USERDEF1" + str4))
                    {
                        this._flex.Cols["CD_USERDEF1" + str4].Caption = str1;
                        this._flex.Cols["CD_USERDEF1" + str4].Visible = true;
                    }
                }
                else if (string.IsNullOrEmpty(str2) || str2 != "Y" && str2 != "N")
                {
                    switch (num4)
                    {
                        case 1:
                            this.lbl사용자정의26.Text = str1;
                            this.lbl사용자정의26.Visible = this.cbo사용자정의26.Visible = true;
                            break;
                        case 2:
                            this.lbl사용자정의27.Text = str1;
                            this.lbl사용자정의27.Visible = this.cbo사용자정의27.Visible = true;
                            break;
                        case 3:
                            this.lbl사용자정의28.Text = str1;
                            this.lbl사용자정의28.Visible = this.cbo사용자정의28.Visible = true;
                            break;
                        case 4:
                            this.lbl사용자정의29.Text = str1;
                            this.lbl사용자정의29.Visible = this.cbo사용자정의29.Visible = true;
                            break;
                        case 5:
                            this.lbl사용자정의30.Text = str1;
                            this.lbl사용자정의30.Visible = this.bp_사용자정의30.Visible = true;
                            break;
                        case 6:
                            this.lbl사용자정의31.Text = str1;
                            this.lbl사용자정의31.Visible = this.bp_사용자정의31.Visible = true;
                            break;
                        case 7:
                            this.lbl사용자정의32.Text = str1;
                            this.lbl사용자정의32.Visible = this.bp_사용자정의32.Visible = true;
                            break;
                    }
                    if (this._flex.Cols.Contains("CD_USERDEF1" + D.GetString(num4)))
                    {
                        this._flex.Cols["CD_USERDEF1" + D.GetString(num4)].Caption = str1;
                        this._flex.Cols["CD_USERDEF1" + D.GetString(num4)].Visible = true;
                    }
                }
                ++num4;
            }
            for (int index = 18; index <= 20; ++index)
                this._flex.Cols["CD_USERDEF" + D.GetString(index)].Visible = false;
            DataTable code5 = this._biz.GetCode("MA_B000128");
            int num5 = 17;
            foreach (DataRow row in code5.Rows)
            {
                string str1 = D.GetString(row["NAME"]);
                string str2 = D.GetString(row["CD_FLAG1"]);
                string str3 = D.GetString(row["CODE"]);
                if (str2 == "Y")
                {
                    string str4 = string.Empty;
                    switch (str3)
                    {
                        case "010":
                            this.lbl사용자정의33.Text = str1;
                            this.lbl사용자정의33.Visible = this.cbo사용자정의33.Visible = true;
                            str4 = "18";
                            break;
                        case "020":
                            this.lbl사용자정의34.Text = str1;
                            this.lbl사용자정의34.Visible = this.cbo사용자정의34.Visible = true;
                            str4 = "19";
                            break;
                        case "030":
                            this.lbl사용자정의35.Text = str1;
                            this.lbl사용자정의35.Visible = this.cbo사용자정의35.Visible = true;
                            str4 = "20";
                            break;
                    }
                    ++num5;
                    if (this._flex.Cols.Contains("CD_USERDEF" + str4))
                    {
                        this._flex.Cols["CD_USERDEF" + str4].Caption = str1;
                        this._flex.Cols["CD_USERDEF" + str4].Visible = true;
                    }
                }
                else if (string.IsNullOrEmpty(str2) || str2 != "Y" && str2 != "N")
                {
                    switch (num5)
                    {
                        case 17:
                            this.lbl사용자정의33.Text = str1;
                            this.lbl사용자정의33.Visible = this.cbo사용자정의33.Visible = true;
                            break;
                        case 18:
                            this.lbl사용자정의34.Text = str1;
                            this.lbl사용자정의34.Visible = this.cbo사용자정의34.Visible = true;
                            break;
                        case 19:
                            this.lbl사용자정의35.Text = str1;
                            this.lbl사용자정의35.Visible = this.cbo사용자정의35.Visible = true;
                            break;
                    }
                    ++num5;
                    if (num5 - 17 <= 3 && this._flex.Cols.Contains("CD_USERDEF" + D.GetString(num5)))
                    {
                        this._flex.Cols["CD_USERDEF" + D.GetString(num5)].Caption = str1;
                        this._flex.Cols["CD_USERDEF" + D.GetString(num5)].Visible = true;
                    }
                }
            }
            this.pnlStndItem.ItemCollection.Clear();
            DataTable code6 = MA.GetCode("MA_B000140");
            OneGridItem[] items = new OneGridItem[10];
            int num6 = 0;
            for (int index = 1; index <= code6.Rows.Count && index <= 5; ++index)
            {
                string str = D.GetString(code6.Rows[index - 1]["NAME"]);
                switch (index)
                {
                    case 1:
                        this.lblStndItem1.Text = str;
                        this.lblStndItem1.Visible = this._txtStndItem01.Visible = true;
                        items[num6++] = this.oneGridItem85;
                        break;
                    case 2:
                        this.lblStndItem2.Text = str;
                        this.lblStndItem2.Visible = this._txtStndItem02.Visible = true;
                        items[num6++] = this.oneGridItem86;
                        break;
                    case 3:
                        this.lblStndItem3.Text = str;
                        this.lblStndItem3.Visible = this._txtStndItem03.Visible = true;
                        items[num6++] = this.oneGridItem87;
                        break;
                    case 4:
                        this.lblStndItem4.Text = str;
                        this.lblStndItem4.Visible = this._txtStndItem04.Visible = true;
                        items[num6++] = this.oneGridItem88;
                        break;
                    case 5:
                        this.lblStndItem5.Text = str;
                        this.lblStndItem5.Visible = this._txtStndItem05.Visible = true;
                        items[num6++] = this.oneGridItem89;
                        break;
                }
                this._flex.Cols["CD_STND_ITEM_" + D.GetString(index)].Caption = str;
                this._flex.Cols["CD_STND_ITEM_" + D.GetString(index)].Visible = true;
            }
            DataTable code7 = MA.GetCode("MA_B000141");
            for (int index = 1; index <= code7.Rows.Count && index <= 5; ++index)
            {
                string str = D.GetString(code7.Rows[index - 1]["NAME"]);
                switch (index)
                {
                    case 1:
                        this.lblStndItem6.Text = str;
                        this.lblStndItem6.Visible = this._txtNumStndItem01.Visible = true;
                        items[num6++] = this.oneGridItem90;
                        break;
                    case 2:
                        this.lblStndItem7.Text = str;
                        this.lblStndItem7.Visible = this._txtNumStndItem02.Visible = true;
                        items[num6++] = this.oneGridItem91;
                        break;
                    case 3:
                        this.lblStndItem8.Text = str;
                        this.lblStndItem8.Visible = this._txtNumStndItem03.Visible = true;
                        items[num6++] = this.oneGridItem92;
                        break;
                    case 4:
                        this.lblStndItem9.Text = str;
                        this.lblStndItem9.Visible = this._txtNumStndItem04.Visible = true;
                        items[num6++] = this.oneGridItem93;
                        break;
                    case 5:
                        this.lblStndItem10.Text = str;
                        this.lblStndItem10.Visible = this._txtNumStndItem05.Visible = true;
                        items[num6++] = this.oneGridItem94;
                        break;
                }
                this._flex.Cols["NUM_STND_ITEM_" + D.GetString(index)].Caption = str;
                this._flex.Cols["NUM_STND_ITEM_" + D.GetString(index)].Visible = true;
            }
            this.pnlStndItem.ItemCollection.AddRange(items);
            DataTable code8 = MA.GetCode("MA_B000144");
            for (int index = 1; index <= code8.Rows.Count; ++index)
            {
                string str = D.GetString(code8.Rows[index - 1]["NAME"]);
                switch (index)
                {
                    case 1:
                        this.lbl사용자정의36.Text = str;
                        this.lbl사용자정의36.Visible = this._datePicker36.Visible = true;
                        break;
                    case 2:
                        this.lbl사용자정의37.Text = str;
                        this.lbl사용자정의37.Visible = this._datePicker37.Visible = true;
                        break;
                    case 3:
                        this.lbl사용자정의38.Text = str;
                        this.lbl사용자정의38.Visible = this._datePicker38.Visible = true;
                        break;
                    case 4:
                        this.lbl사용자정의39.Text = str;
                        this.lbl사용자정의39.Visible = this._datePicker39.Visible = true;
                        break;
                }
                int num7 = 20 + index;
                if (index <= 4)
                {
                    this._flex.Cols["CD_USERDEF" + D.GetString(num7)].Caption = str;
                    this._flex.Cols["CD_USERDEF" + D.GetString(num7)].Visible = true;
                }
            }
        }

        private void InitEvent()
        {
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn품목복사.Click += new EventHandler(this.btn품목복사_Click);
            this.btn붙여넣기.Click += new EventHandler(this.btn붙여넣기_Click);
            this.btn업로드.Click += new EventHandler(this.btn업로드_Click);
            this.btn다운로드.Click += new EventHandler(this.btn다운로드_Click);
            this.btn파일삭제.Click += new EventHandler(this.btn파일삭제_Click);
            this.ctx입고SL.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx출고SL.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpcClslS.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx대분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpcClsmS.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpcClssS.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx소분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc품목타입S.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bp코어코드.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx제품군.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this._bpCdItemR.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.cbo구매단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo출하단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo수주단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo외주단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cur로트사이즈.Validating += new CancelEventHandler(this.cur로트사이즈_Validating);
            this.cbo백플러쉬.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo불출관리.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btnPDM적용.Click += new EventHandler(this.btnPDM적용_Click);
            this.btnCopyToCompany.Click += new EventHandler(this.btnCopyToCompany_Click);
            this.txt품명.Search += new EventHandler(this.Control_Search);
            this.cbo공장코드S.SelectedIndexChanged += new EventHandler(this.cbo공장코드S_SelectedIndexChanged);
            this.m_lblUnitIm.Click += new EventHandler(this.m_lblUnitIm_Click);
            this.cbo과세구분매출.SelectedIndexChanged += new EventHandler(this.cbo과세구분매출_SelectedIndexChanged);
            this.rduFile6View.Click += new EventHandler(this.rduFile6View_Click);
            this.rduFile5View.Click += new EventHandler(this.rduFile5View_Click);
            this.rduFile4View.Click += new EventHandler(this.rduFile4View_Click);
            this.rduFileIns6.Click += new EventHandler(this.rduFileIns6_Click);
            this.rduFileIns4.Click += new EventHandler(this.rduFileIns4_Click);
            this.rduFileIns5.Click += new EventHandler(this.rduFileIns5_Click);
            this.rduFileDel6.Click += new EventHandler(this.rduFileDel6_Click);
            this.rduFileDel4.Click += new EventHandler(this.rduFileDel4_Click);
            this.rduFileDel5.Click += new EventHandler(this.rduFileDel5_Click);
            this.rduFile3View.Click += new EventHandler(this.rduFile3View_Click);
            this.rduFile2View.Click += new EventHandler(this.rduFile2View_Click);
            this.rduFile1View.Click += new EventHandler(this.rduFile1View_Click);
            this.rduFileIns3.Click += new EventHandler(this.rduFileIns3_Click);
            this.rduFileIns1.Click += new EventHandler(this.rduFileIns1_Click);
            this.rduFileIns2.Click += new EventHandler(this.rduFileIns2_Click);
            this.rduFileDel3.Click += new EventHandler(this.rduFileDel3_Click);
            this.rduFileDel1.Click += new EventHandler(this.rduFileDel1_Click);
            this.rduFileDel2.Click += new EventHandler(this.rduFileDel2_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this._btnBulkSave.Click += new EventHandler(this._btnBulkSave_Click);
            this._btnZSATREC_PASS.Click += new EventHandler(this._btnZSATREC_PASS_Click);
            this._btnAutoCdItem.Click += new EventHandler(this._btnAutoCdItem_Click);
        }

        private void btnCopyToCompany_Click(object sender, EventArgs e)
        {
            try
            {
                new P_CZ_MA_PITEM_COMPANY_TRANS_SUB(new object[2]
                {
           D.GetString(this.cbo공장코드S.SelectedValue),
           D.GetString( this.cbo공장코드S.Text)
                }).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 단위구분선택시수량셋팅()
        {
            if (!this._flex.HasNormalRow)
                return;
            this.cur구매단위수량.Enabled = !(D.GetString(this.cbo구매단위.SelectedValue) == string.Empty);
            this.cur출하단위수량.Enabled = !(D.GetString(this.cbo출하단위.SelectedValue) == string.Empty);
            this.cur수주단위수량.Enabled = !(D.GetString(this.cbo수주단위.SelectedValue) == string.Empty);
            this.cur외주단위수량.Enabled = !(D.GetString(this.cbo외주단위.SelectedValue) == string.Empty);
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !this.Chk공장코드)
                return false;
            this.row복사 = null;
            bool flag = true;
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.deleteRowList == null)
                    this.deleteRowList = new List<object>();
                else
                    this.deleteRowList.Clear();
                if (!this.BeforeSearch())
                    return;
                MsgControl.ShowMsg("자료를 조회 진행중입니다.");
                this._flex.Binding = null;
                DataTable dataTable = (DataTable)null;
                List<object> objectList1 = new List<object>();
                objectList1.Add(this.회사코드);
                objectList1.Add(this.cbo공장코드S.SelectedValue);
                objectList1.Add(this.cbo조달구분S.SelectedValue);
                objectList1.Add(this.bpc품목군S.QueryWhereIn_Pipe);
                objectList1.Add(this.cbo사용유무S.SelectedValue);
                objectList1.Add(this.txt검색S.Text);
                objectList1.Add(this.cbo계정구분S.SelectedValue);
                objectList1.Add(this.cbo제품군S.SelectedValue);
                objectList1.Add(this.cbo내외자구분S.SelectedValue);
                objectList1.Add(this.bpcClslS.QueryWhereIn_Pipe);
                objectList1.Add(this.bpcClsmS.QueryWhereIn_Pipe);
                objectList1.Add(this.bpcClssS.QueryWhereIn_Pipe);
                objectList1.Add(this.bpc품목타입S.QueryWhereIn_Pipe);
                objectList1.Add(this.ctx주거래처S.CodeValue);
                objectList1.Add(this._cboUserDef06_S.SelectedValue);
                objectList1.Add(this._cboUserDef07_S.SelectedValue);
                objectList1.Add(this._ctxUserDef30_S.CodeValue);
                objectList1.Add(this._ctxUserDef31_S.CodeValue);
                objectList1.Add(this._ctxUserDef32_S.CodeValue);
                objectList1.Add(this._cboUserDef33_S.SelectedValue);
                int selectedIndex;
                if (this.cboSearch1.SelectedIndex < 10)
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch1.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch1.SelectedIndex;
                        str = "00" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                else
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch1.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch1.SelectedIndex;
                        str = "0" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                if (this.cboSearch2.SelectedIndex < 10)
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch2.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch2.SelectedIndex;
                        str = "00" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                else
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch2.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch2.SelectedIndex;
                        str = "0" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                if (this.cboSearch3.SelectedIndex < 10)
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch3.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch3.SelectedIndex;
                        str = "00" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                else
                {
                    List<object> objectList2 = objectList1;
                    string str;
                    if (this.cboSearch3.SelectedIndex <= 0)
                    {
                        str = "";
                    }
                    else
                    {
                        selectedIndex = this.cboSearch3.SelectedIndex;
                        str = "0" + selectedIndex.ToString();
                    }
                    objectList2.Add(str);
                }
                objectList1.Add(this.txt검색1.Text);
                objectList1.Add(this.txt검색2.Text);
                objectList1.Add(this.txt검색3.Text);
                objectList1.Add(Global.SystemLanguage.MultiLanguageLpoint);
                objectList1.Add(this._chkSString.Checked ? "Y" : "N");
                objectList1.Add(this._chkUpper.Checked ? "Y" : "N");
                string chk_top = this._chkTop.Checked ? "Y" : "N";
                bool flag = true;
                string empty = string.Empty;
                if (this.cbo계정구분.DataSource != null && ((DataTable)this.cbo계정구분.DataSource).Rows.Count > 0)
                    empty = D.GetString(((DataTable)this.cbo계정구분.DataSource).Rows[0]["CODE"]);
                if (this._biz.IsDbDirect())
                {
                    try
                    {
                        this._biz.DBD_Test();
                        dataTable = this._biz.DBD_Search(objectList1.ToArray());
                    }
                    catch (Exception ex)
                    {
                        if (flag)
                            dataTable = this._biz.Search(objectList1.ToArray(), empty, chk_top);
                    }
                }
                else
                    dataTable = this._biz.Search(objectList1.ToArray(), empty, chk_top);
                dataTable?.AcceptChanges();
                this._flex.Binding = dataTable;
                MsgControl.CloseMsg();
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this._flex.IsDataChanged = false;
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
                if (!this.BeforeAdd() || !this.Chk공장코드)
                    return;
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex["CD_PLANT"] = this.cbo공장코드S.SelectedValue;
                this._flex["INSERT_ID"] = MA.Login.사원번호;
                this._flex["NM_INSERT_ID"] = MA.Login.사원명;
                this._flex["CD_BIZAREA"] = this._biz.GetCdBizarea((string)this.cbo공장코드S.SelectedValue);
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.AddFinished();
                if (this.tab.SelectedTab == this.tpg기본)
                    this.tab.SelectedTab = this.tpg기본;
                this.txt품목코드.Focus();
                this.ToolBarAddButtonEnabled = true;
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
                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;
                if (this._flex버전관리.DataTable != null)
                {
                    DataRow[] dataRowArray = this._flex버전관리.DataTable.Select("CD_ITEM = '" + this._flex["CD_ITEM"].ToString() + "'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray.Length != 0)
                    {
                        foreach (DataRow dataRow in dataRowArray)
                            dataRow.Delete();
                    }
                }
                this.deleteRowList.Add(this._flex.Rows[this._flex.Row]["CD_ITEM"]);
                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.OK;
            bool bMsgShow = false;
            string strDeleteData = (string)null;
            if (this.deleteRowList.Count > 0)
                this.DeleteListShow(ref bMsgShow, ref strDeleteData);
            try
            {
                if (!this.BeforeSave())
                    return;
                if (bMsgShow)
                    dialogResult = this.ShowDetailMessage("▽아래 품목내역이 삭제됩니다.\n정말 변경사항을 저장하시겠습니까?", strDeleteData);
                if (dialogResult == DialogResult.OK && this.MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this._flex.IsDataChanged = false;
                this._flex버전관리.IsDataChanged = false;
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt엑셀 = this.Open_Excel();
                if (dt엑셀 == null)
                    return;
                this.AddExcelData(dt엑셀);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn엑셀수정업로드_Click()
        {
            DataTable dt엑셀 = this.Open_Excel();
            if (dt엑셀 == null)
                return;
            this.EditExcelData(dt엑셀);
        }

        private void btn품목복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                DataRow dataRow = this._flex.GetDataRow(this._flex.Row);
                DataTable dataTable = this._flex.DataTable.Clone();
                dataTable.ImportRow(dataRow);
                this.row복사 = dataTable.Rows[0];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                if (this.row복사 == null)
                {
                    this.ShowMessage("복사할 내용이 존재하지 않습니다.");
                }
                else
                {
                    string str = BASIC.GetMAEXC("공장품목등록-품목코드붙여넣기여부");
                    if (string.IsNullOrEmpty(str))
                        str = "000";
                    DataRow dataRow = this._flex.GetDataRow(this._flex.Row);
                    foreach (DataColumn column in this._flex.DataTable.Columns)
                    {
                        if (!(column.ColumnName == "DTS_INSERT") && !(column.ColumnName == "DTS_UPDATE") && (!(column.ColumnName == "INSERT_ID") && !(column.ColumnName == "UPDATE_ID")) && (!(column.ColumnName == "NM_INSERT_ID") && !(column.ColumnName == "NM_UPDATE_ID") && (!(column.ColumnName == "STND_ITEM"))) && ((!(column.ColumnName == "CD_ITEM") && !(column.ColumnName == "NM_ITEM") || !(str == "000")) && (!(column.ColumnName == "IMAGE1") && !(column.ColumnName == "IMAGE1_NAME") && (!(column.ColumnName == "IMAGE2") && !(column.ColumnName == "IMAGE2_NAME")))) && (!(column.ColumnName == "IMAGE3") && !(column.ColumnName == "IMAGE3_NAME") && (!(column.ColumnName == "IMAGE4") && !(column.ColumnName == "IMAGE4_NAME")) && (!(column.ColumnName == "IMAGE5") && !(column.ColumnName == "IMAGE5_NAME") && !(column.ColumnName == "IMAGE6"))) && !(column.ColumnName == "IMAGE6_NAME"))
                            dataRow[column.ColumnName] = this.row복사[column.ColumnName];
                    }
                    this._flex.RefreshBindng(this._flex.Row);
                    if (this.tab.SelectedTab != this.tpg기본)
                        this.tab.SelectedTab = this.tpg기본;
                    this.txt품목코드.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || !this.Chk품목코드)
                    return;
                string str = D.GetString(this._flex["CD_ITEM"]);
                if (this.txt첨부파일명.Text == str)
                {
                    this.ShowMessage("첨부파일 삭제 후 다시 파일을 첨부하시기 바랍니다.");
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    string safeFileName = openFileDialog.SafeFileName;
                    if (!safeFileName.Contains(str))
                    {
                        this.ShowMessage("첨부하려고하는 파일명과 품목코드가 일치하여야 합니다.");
                    }
                    else
                    {
                        this._flex["FILE_PATH_MNG"] = (this.txt첨부파일명.Text = safeFileName + "*" + openFileDialog.FileName);
                        this._flex["YN_FILE_ADD"] = "Y";
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn다운로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || !this.Chk품목코드)
                    return;
                string text = this.txt첨부파일명.Text;
                if (text == string.Empty)
                {
                    this.ShowMessage("첨부파일이 존재하지 않습니다.");
                }
                else
                {
                    D.GetString(this._flex["CD_ITEM"]);
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                        return;
                    MsgControl.ShowMsg("파일 다운로드 작업 중입니다..");
                    FileManager.FileDownload(Path.GetFileName(text), folderBrowserDialog.SelectedPath, "/shared/MF_File_Mng/" + this.회사코드, "PITEM");
                    MsgControl.ShowMsg("파일 다운로드 작업을 완료했습니다.");
                    MsgControl.CloseMsg();
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn다운로드.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || !this.Chk품목코드)
                    return;
                string text = this.txt첨부파일명.Text;
                if (text == string.Empty)
                {
                    this.ShowMessage("첨부파일이 존재하지 않습니다.");
                }
                else
                {
                    D.GetString(this._flex["CD_ITEM"]);
                    bool flag = D.GetString(this._flex["YN_FILE_ADD"]) == "Y";
                    if (flag && !this.CheckFileName(text))
                        return;
                    if (!flag)
                        FileUploader.DeleteFile("/shared/MF_File_Mng/" + this.회사코드, "PITEM", text);
                    this._flex["FILE_PATH_MNG"] = (this.txt첨부파일명.Text = "");
                    this._flex["YN_FILE_ADD"] = "N";
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn파일삭제.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnPDM적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk공장코드 || !this._biz.PDMApply(this.cbo공장코드S.SelectedValue.ToString()))
                    return;
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnPDM적용.Text);
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
                if (!this._flex.HasNormalRow)
                    return;
                this._flex버전관리.Rows.Add();
                this._flex버전관리.Row = this._flex버전관리.Rows.Count - 1;
                this._flex버전관리["CD_PLANT"] = D.GetString(this.cbo공장코드S.SelectedValue);
                this._flex버전관리["CD_ITEM"] = this._flex["CD_ITEM"];
                this._flex버전관리["SEQ"] = D.GetDecimal(this._flex버전관리.GetMaxValue("SEQ")) + 1;
                this._flex버전관리.AddFinished();
                this._flex버전관리.Focus();
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
                if (!this.BeforeDelete() || !this._flex버전관리.HasNormalRow)
                    return;
                this._flex버전관리.Rows.Remove(this._flex버전관리.Row);
                if (!this._flex버전관리.HasNormalRow)
                    this._flex버전관리.SetDefaultDisplayToSetBinding();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _btnBulkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataTable changes1 = this._flex.GetChanges();
                    DataTable changes2 = this._flex버전관리.GetChanges();
                    if (changes1 == null)
                    {
                        this.ShowMessage("저장할 데이터가 없습니다");
                    }
                    else
                    {
                        DialogResult dialogResult = this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까);
                        MsgControl.ShowMsg("자료를 저장 진행중입니다.");
                        if (dialogResult == DialogResult.Yes)
                        {
                            this._biz.Save_Bulk(changes1, changes2, this.cbo공장코드S.SelectedValue.ToString());
                            this._flex.AcceptChanges();
                        }
                        MsgControl.CloseMsg();
                        int num3 = (int)this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _btnZSATREC_PASS_Click(object sender, EventArgs e)
        {
            try
            {
                object[] objArray = new object[1]
                {
           D.GetString(this.cbo공장코드S.SelectedValue)
                };
                if (this.MainFrameInterface.IsExistPage("P_MA_Z_SATREC_PITEM_REQ", false))
                    this.MainFrameInterface.UnLoadPage("P_MA_Z_SATREC_PITEM_REQ", false);
                this.MainFrameInterface.LoadPageFrom("P_MA_Z_SATREC_PITEM_REQ", this.DD("품목등록요청"), this.Grant, (object[])null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool IsChanged() => this.Get저장권한 && base.IsChanged() && (this._flex.IsDataChanged || this._flex버전관리.IsDataChanged);

        protected override bool Verify()
        {
            if (!base.Verify())
                return false;
            if (!this._flex.HasNormalRow)
                return true;
            DataTable code = Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목계정);
            for (int index = this._flex.Rows.Fixed; index < this._flex.Rows.Count; ++index)
            {
                if (this._flex.GetDataRow(index).RowState != DataRowState.Unchanged)
                {
                    if (D.GetString(this._flex[index, "FG_GIR"]) == "N" && D.GetString(this._flex[index, "FG_BF"]) == "Y")
                    {
                        this.ShowMessage("불출여부가 N이면 Back Flushs는 Y 일 수 없습니다.");
                        this.tab.SelectedTab = this.tpg자재;
                        return false;
                    }
                    foreach (string str in this.GetNotNullCol(index, code))
                    {
                        TabPage tabPage = (TabPage)null;
                        if (this._flex.DataTable.Columns[str].DataType == typeof(string))
                        {
                            if (D.GetString(this._flex[index, str]) == string.Empty)
                            {
                                tabPage = this.GetNotNullCol(str);
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, this._flex.Cols[str].Caption);
                            }
                        }
                        else if (D.GetDecimal(this._flex[index, str]) == 0M)
                        {
                            tabPage = this.GetNotNullCol(str);
                            this.ShowMessage(공통메세지._은_보다커야합니다, this._flex.Cols[str].Caption, "0");
                        }
                        if (tabPage != null)
                        {
                            if (this.tab.SelectedTab != tabPage)
                                this.tab.SelectedTab = tabPage;
                            this._flex.Row = index;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify())
                return false;
            if (!this.Get저장권한)
            {
                if (Global.CurLanguage == Language.KR)
                {
                    this.ShowMessage("저장권한이 없으므로 해당 메뉴를 수정할 수 없습니다");
                }
                else
                {
                    this.ShowMessage("You are not authorized to modify.");
                }
                return false;
            }
            if (!this.FileUpLoad())
                return false;
            for (int index = 0; index < 6; ++index)
            {
                if (D.GetString(this.image_delete[index]) != "")
                    FileUploader.DeleteFile("/shared/MF_File_Mng/" + this.회사코드, "PITEM", this.image_delete[index]);
            }
            DataTable changes1 = this._flex.GetChanges();
            DataTable changes2 = this._flex버전관리.GetChanges();
            if (!this.PictureFileUpLoad(changes1))
                return false;
            if (this._biz.Save(changes1, changes2))
            {
                this._flex.AcceptChanges();
                this._flex버전관리.AcceptChanges();
            }
            return true;
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarDeleteButtonEnabled = this.btn품목복사.Enabled = this.btn붙여넣기.Enabled = this._flex.HasNormalRow;
                this.btn엑셀업로드.Enabled = true;
                this.ToolBarSaveButtonEnabled = this.IsChanged();
                this.pnl기본.Enabled = this.pnl자재.Enabled = this.pnl오더.Enabled = this.pnl품질.Enabled = this.pnl첨부파일.Enabled = this.pnl기타정보.Enabled = this.pnlFileList.Enabled = this._flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.PictureFileDownLoad();
                DataTable dt = (DataTable)null;
                string str = D.GetString(this._flex[e.NewRange.r1, "CD_ITEM"]);
                string filter = "CD_ITEM ='" + str + "'";
                object[] args = new object[3]
                {
           MA.Login.회사코드,
          this.cbo공장코드S.SelectedValue,
           str
                };
                if (this._flex.DetailQueryNeed)
                    dt = this._biz.Search버전관리(args);
                this._flex버전관리.BindingAdd(dt, filter);
                this._flex버전관리.DetailQueryNeed = false;

                if (this._flex.RowState(e.NewRange.r1) != DataRowState.Added)
                    this._btnAutoCdItem.Enabled = false;
                else
                    this._btnAutoCdItem.Enabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Dass.FlexGrid.FlexGrid flexGrid = (Dass.FlexGrid.FlexGrid)sender;
                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "FILE_PATH_MNG_HELP":
                        string fileCode = D.GetString(this._flex["CD_PLANT"]) + "_" + D.GetString(this._flex["CD_ITEM"]);
                        if (new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, fileCode).ShowDialog() == DialogResult.Cancel)
                            break;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                DropDownComboBox dropDownComboBox = sender as DropDownComboBox;
                if (!dropDownComboBox.Modified)
                    return;
                bool flag = true;
                switch (dropDownComboBox.Name)
                {
                    case "cbo구매단위":
                        if (D.GetString(this.cbo구매단위.SelectedValue) == string.Empty)
                        {
                            this._flex["UNIT_PO_FACT"] = (this.cur구매단위수량.DecimalValue = 0M);
                            goto case "cbo백플러쉬";
                        }
                        else
                            goto case "cbo백플러쉬";
                    case "cbo출하단위":
                        if (D.GetString(this.cbo출하단위.SelectedValue) == string.Empty)
                        {
                            this._flex["UNIT_GI_FACT"] = (this.cur출하단위수량.DecimalValue = 0M);
                            goto case "cbo백플러쉬";
                        }
                        else
                            goto case "cbo백플러쉬";
                    case "cbo수주단위":
                        if (D.GetString(this.cbo수주단위.SelectedValue) == string.Empty)
                        {
                            this._flex["UNIT_SO_FACT"] = (this.cur수주단위수량.DecimalValue = 0M);
                            goto case "cbo백플러쉬";
                        }
                        else
                            goto case "cbo백플러쉬";
                    case "cbo외주단위":
                        if (D.GetString(this.cbo외주단위.SelectedValue) == string.Empty)
                        {
                            this._flex["UNIT_SU_FACT"] = (this.cur외주단위수량.DecimalValue = 0M);
                            goto case "cbo백플러쉬";
                        }
                        else
                            goto case "cbo백플러쉬";
                    case "cbo백플러쉬":
                        if (flag)
                        {
                            this.단위구분선택시수량셋팅();
                            break;
                        }
                        break;
                    case "cbo불출관리":
                        if (D.GetString(this.cbo불출관리.SelectedValue) == "N")
                        {
                            this._flex[D.GetString(this.cbo백플러쉬.Tag)] = this.cbo백플러쉬.SelectedValue = "N";
                            this.cbo백플러쉬.Enabled = false;
                            goto case "cbo백플러쉬";
                        }
                        else
                        {
                            this.cbo백플러쉬.Enabled = true;
                            goto case "cbo백플러쉬";
                        }
                    default:
                        flag = false;
                        goto case "cbo백플러쉬";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장코드S_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.DataTable != null)
                    this._flex.Binding = this._flex.DataTable.Clone();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur로트사이즈_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.cur로트사이즈.Modified || !(this.cur로트사이즈.DecimalValue < 0M))
                    return;
                e.Cancel = true;
                if (Global.CurLanguage == Language.KR)
                {
                    this.ShowMessage("LOT SIZE 는 0 보다 커야 합니다.");
                }
                else
                {
                    this.ShowMessage("LOT SIZE is Bigger Than 0.");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool ChkBF
        {
            get
            {
                if (!(D.GetString(this.cbo백플러쉬.SelectedValue) == "Y") || !(D.GetString(this.cbo불출관리.SelectedValue) == "N"))
                    return true;
                this.ShowMessage("불출관리가 N 이므로 수정하실 수 없습니다.");
                return false;
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_CODE_SUB:
                        switch (e.ControlName)
                        {
                            case "ctx대분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                                break;
                            case "ctx중분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(this.ctx대분류.CodeValue);
                                break;
                            case "ctx소분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(this.ctx중분류.CodeValue);
                                break;
                            case "ctx제품군":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                                break;
                        }
                        break;
                    case HelpID.P_MA_CODE_SUB1:
                        switch (e.ControlName)
                        {
                            case "bpc품목타입S":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000011";
                                break;
                            case "bpcClslS":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                                break;
                            case "bpcClsmS":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                                e.HelpParam.P61_CODE1 = PitemCls.CdFlag1(this.bpcClslS.QueryWhereIn_Pipe);
                                break;
                            case "bpcClssS":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                                e.HelpParam.P61_CODE1 = PitemCls.CdFlag1(this.bpcClsmS.QueryWhereIn_Pipe);
                                break;
                        }
                        break;
                    case HelpID.P_MA_PITEM_SUB:
                    case HelpID.P_MA_SL_SUB:
                        if (!this.Chk공장코드)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = D.GetString(this._flex["CD_PLANT"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_Search(object sender, EventArgs e)
        {
            try
            {
                this.txt품명.Text = this.L.GetLanguageName(this._flex, this._flex.Row, "NM_ITEM");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private string[] GetNotNullCol(int nrow, DataTable dtClsItem)
        {
            List<string> list = new List<string>();
            list.Add("NM_ITEM");
            list.Add("CLS_ITEM");
            list.Add("TP_PROC");
            list.Add("TP_ITEM");
            list.Add("TP_PART");
            list.Add("YN_USE");
            list.Add("FG_ABC");
            list.Add("FG_GIR");
            list.Add("FG_TRACKING");
            list.Add("LOTSIZE");
            list.Add("FG_LONG");
            list.Add("YN_PHANTOM");
            list.Add("FG_BF");
            list.Add("FG_SQC");
            return list.ToArray();
        }

        private TabPage GetNotNullCol(string colName)
        {
            switch (colName)
            {
                case "NM_ITEM":
                case "CLS_ITEM":
                case "TP_PROC":
                case "TP_ITEM":
                case "TP_PART":
                case "YN_USE":
                case "STND_ITEM":
                case "GRP_ITEM":
                    return this.tpg기본;
                case "FG_ABC":
                case "FG_GIR":
                    return this.tpg자재;
                case "FG_TRACKING":
                case "LOTSIZE":
                case "FG_LONG":
                case "YN_PHANTOM":
                case "FG_BF":
                    return this.tpg오더;
                case "FG_SQC":
                    return this.tpg품질;
                default:
                    return (TabPage)null;
            }
        }

        private bool FileUpLoad()
        {
            if (!this._flex.HasNormalRow)
                return true;
            try
            {
                string empty1 = string.Empty;
                for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                {
                    if (this._flex.GetDataRow(row).RowState != DataRowState.Unchanged && !(D.GetString(this._flex["YN_FILE_ADD"]) == "N"))
                    {
                        string fineName = D.GetString(this._flex[row, "FILE_PATH_MNG"]);
                        if (!(fineName == string.Empty))
                        {
                            if (!this.CheckFileName(fineName))
                                return false;
                            MsgControl.ShowMsg("File Upload......");
                            string[] strArray = fineName.Split('*');
                            string fileName = strArray[0];
                            string str1 = strArray[1];
                            this._biz.UploadPath(fileName);
                            string empty2 = string.Empty;
                            string str2 = FileManager.FileUpload(Path.GetFileName(strArray[0]), strArray[1], "/shared/MF_File_Mng/" + this.회사코드, "PITEM");
                            if (str2 == "Success")
                            {
                                this._flex[row, "FILE_PATH_MNG"] = fileName;
                                this._flex[row, "YN_FILE_ADD"] = "N";
                            }
                            else if (str2 == "Failure")
                            {
                                empty1 += "전송실패 하였습니다.";
                            }
                            else
                            {
                                MsgControl.CloseMsg();
                                empty1 += string.Format("- 파일명 : {0}\n  {1}\n", fileName, str2);
                            }
                            if (row == this._flex.Row)
                                this.txt첨부파일명.Text = fileName;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(empty1))
                {
                    Global.MainFrame.ShowDetailMessage("파일 전송 중 오류가 발생했습니다.\n상세내역을 확인하세요. ↓", empty1);
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return true;
        }

        private bool CheckFileName(string fineName)
        {
            if (fineName.Contains("*"))
                return true;
            this.ShowMessage("첨부파일 이름 형식이 올바르지 않습니다.");
            return false;
        }

        public void Menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem toolStripMenuItem))
                    return;
                if (toolStripMenuItem.Name == this.DD("파일생성"))
                    this._flex.ExportToExcel(true, false, true);
                else if (toolStripMenuItem.Name == this.DD("파일업로드"))
                    this.btn엑셀업로드_Click(null, (EventArgs)null);
                else if (toolStripMenuItem.Name == this.DD("파일수정업로드"))
                    this.btn엑셀수정업로드_Click();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable Open_Excel()
        {
            if (this._flex.DataTable == null)
                return (DataTable)null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return (DataTable)null;
            DataTable dataTable = new Excel().StartLoadExcel(openFileDialog.FileName);
            if (!dataTable.Columns.Contains("CD_ITEM"))
            {
                this.ShowMessage("품목코드에 대한 스키마가 존재하지 않습니다.");
                return (DataTable)null;
            }
            foreach (DataRow dataRow in dataTable.Select("CD_ITEM IS NULL"))
                dataRow.Delete();
            dataTable.AcceptChanges();
            return dataTable;
        }

        private void EditExcelData(DataTable dt엑셀)
        {
            try
            {
                MsgControl.ShowMsg("엑셀업로드 중 입니다." + Environment.NewLine + "잠시만 기다려주십시요.");
                this._flex.Redraw = false;
                dt엑셀.Columns.Add("EXCEL_EDIT", typeof(string));
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("  < 품목코드 >  ");
                stringBuilder.AppendLine("----------------");
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                bool flag = false;
                this._flex.DataTable.PrimaryKey = new DataColumn[2]
                {
          this._flex.DataTable.Columns["CD_PLANT"],
          this._flex.DataTable.Columns["CD_ITEM"]
                };

                if (!this.ChkExcel(dt엑셀, "EDIT"))
                    return;

                foreach (DataRow row in dt엑셀.Rows)
                {
                    row["EXCEL_EDIT"] = "Y";
                    string str1 = D.GetString(row["CD_PLANT"]);
                    string str2 = D.GetString(row["CD_ITEM"]);
                    DataRow dataRow = this._flex.DataTable.Rows.Find(new object[2]
                    {
             str1,
             str2
                    });
                    if (dataRow == null)
                    {
                        stringBuilder.AppendLine(" " + str2);
                        flag = true;
                    }
                    else
                    {
                        foreach (DataColumn column in dataRow.Table.Columns)
                        {
                            if (row.Table.Columns.Contains(column.ColumnName))
                            {
                                if (!(this._sCls_Lms == "100") || !(column.ColumnName == "CLS_L") && !(column.ColumnName == "CLS_M") && !(column.ColumnName == "CLS_S"))
                                    dataRow[column.ColumnName] = row[column.ColumnName];
                            }
                        }
                        this._flex.RefreshBindng(this._flex.RowSel);
                    }
                }
                if (flag)
                {
                    if (this.ShowDetailMessage("엑셀자료에 화면에 표시된 품목 중 없는 품목이 있습니다." + Environment.NewLine + "존재하는 품목만 업로드 하시겠습니까?" + Environment.NewLine + "없는 품목은 [︾] 버튼을 눌러 품목을 확인하세요.", "", stringBuilder.ToString(), "QY2") != DialogResult.Yes)
                    {
                        this._flex.AcceptChanges();
                        this.OnToolBarSearchButtonClicked(null, (EventArgs)null);
                        return;
                    }
                }
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn엑셀업로드.Text);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void AddExcelData(DataTable dt엑셀)
        {
            try
            {
                MsgControl.ShowMsg("엑셀업로드 중 입니다." + Environment.NewLine + "잠시만 기다려주십시요.");
                string pipe = string.Empty;
                Decimal num = 1M;
                Letter letter = new Letter();
                string title = "<품목>";
                string str1 = "";
                bool flag1 = true;
                string empty = string.Empty;
                foreach (DataRow row in dt엑셀.Rows)
                {
                    string str2 = D.GetString(row["CD_ITEM"]);
                    if (num % 200M == 0M)
                    {
                        string msg = string.Empty;
                        flag1 = letter.ChkLetter(title, pipe, out msg);
                        str1 += msg;
                        pipe = string.Empty;
                    }
                    pipe = pipe + str2 + "|";
                    ++num;
                }
                string msg1 = string.Empty;
                bool flag2 = letter.ChkLetter(title, pipe, out msg1);
                string detail = str1 + msg1;
                DialogResult dialogResult = DialogResult.OK;
                if (!flag2 && this.ShowDetailMessage("▽아래의 내용과 같이 소문자 품목이 존재합니다. \n계속 진행하게 되면 '품목코드'를 대문자로 변경합니다. \n정말 계속 진행하시겠습니까?" + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", detail) != DialogResult.OK)
                    return;
                foreach (DataRow row in dt엑셀.Rows)
                {
                    row["CD_ITEM"] = D.GetString(row["CD_ITEM"]).ToUpper();
                    if (string.IsNullOrEmpty(D.GetString(row["DT_IMMNG"])) || D.GetString(row["DT_IMMNG"]) == "00000000")
                        row["DT_IMMNG"] = "19000101";
                }
                if (!dt엑셀.Columns.Contains("CD_PLANT"))
                    dt엑셀.Columns.Add("CD_PLANT", typeof(string));
                foreach (DataRow row in dt엑셀.Rows)
                    row["CD_PLANT"] = D.GetString(row["CD_PLANT"]) == string.Empty ? this.cbo공장코드S.SelectedValue : row["CD_PLANT"];
                string msg2 = string.Empty;
                DataTable dtExists = (DataTable)null;
                dialogResult = DialogResult.OK;
                if (this._biz.ExistsITEM(dt엑셀, out msg2, out dtExists))
                {
                    if (this.ShowDetailMessage("이미 등록된 품목이 존재합니다." + Environment.NewLine + "정말 계속 진행하시겠습니까?" + Environment.NewLine + "[︾] 버튼을 눌러 품목을 확인하세요.", msg2) != DialogResult.OK)
                        return;
                }
                this._flex.Redraw = false;
                this.ChkExcel_Upload(dt엑셀, dtExists);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private bool ChkExcel(DataTable dt엑셀, string 추가수정구분)
        {
            this._biz.Set엑셀체크데이터();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            StringBuilder stringBuilder4 = new StringBuilder();
            int num1;
            bool flag1 = (num1 = 1) != 0;
            bool flag2 = num1 != 0;
            bool flag3 = num1 != 0;
            bool flag4 = num1 != 0;
            bool flag5 = false;
            foreach (DataRow row in dt엑셀.Rows)
            {
                DataRow dataRow = this._flex.DataTable.NewRow();
                Duzon.ERPU.SA.Settng.Data.DataCopy(row, dataRow);
                string msg계정구분;
                bool flag6 = this._biz.Chk계정구분(dataRow, out msg계정구분);
                string 품목군명;
                string msg품목군;
                bool flag7 = this._biz.Chk품목군(dataRow, out 품목군명, out msg품목군);
                string NameCol1;
                string msg창고1;
                bool flag8 = this._biz.Chk창고(dataRow, "CD_SL", out NameCol1, out msg창고1);
                string NameCol2;
                string msg창고2;
                bool flag9 = this._biz.Chk창고(dataRow, "CD_GISL", out NameCol2, out msg창고2);
                dataRow["NM_GRP_ITEM"] = 품목군명;
                dataRow["NM_SL"] = NameCol1;
                dataRow["NM_GISL"] = NameCol2;
                dataRow["CLS_PO"] = D.GetString(dataRow["CLS_PO"]);
                if (flag6 && flag7 && (flag8 && flag9) && 추가수정구분 == "ADD")
                {
                    dataRow["DT_VALID"] = D.GetString(dataRow["DT_VALID"]).Replace("/", string.Empty).Replace("-", string.Empty);
                    this._flex.DataTable.Rows.Add(dataRow);
                    flag5 = true;
                }
                else
                {
                    flag4 = flag6 && flag4;
                    flag3 = flag7 && flag3;
                    flag2 = flag8 && flag2;
                    flag1 = flag9 && flag1;
                    if (!flag6)
                        stringBuilder1.AppendLine(msg계정구분);
                    if (!flag7)
                        stringBuilder2.AppendLine(msg품목군);
                    if (!flag8)
                        stringBuilder3.AppendLine(msg창고1);
                    if (!flag9)
                        stringBuilder4.AppendLine(msg창고2);
                }
            }
            if (flag5 && 추가수정구분 == "ADD")
            {
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.IsDataChanged = true;
                this._flex.RefreshBindng(this._flex.Row);
                this.Page_DataChanged(null, (EventArgs)null);
            }
            if (추가수정구분 == "ADD")
            {
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn엑셀업로드.Text);
            }
            if (flag4 && flag3 && flag2 && flag1)
                return true;
            StringBuilder stringBuilder5 = new StringBuilder();
            if (!flag4)
            {
                stringBuilder5.AppendLine("<계정구분>");
                stringBuilder5.AppendLine(stringBuilder1.ToString());
            }
            if (!flag3)
            {
                stringBuilder5.AppendLine("<품목군>");
                stringBuilder5.AppendLine(stringBuilder2.ToString());
            }
            if (!flag2)
            {
                stringBuilder5.AppendLine("<입고창고>");
                stringBuilder5.AppendLine(stringBuilder3.ToString());
            }
            if (!flag1)
            {
                stringBuilder5.AppendLine("<출고창고>");
                stringBuilder5.AppendLine(stringBuilder4.ToString());
            }
            int num3 = (int)this.ShowDetailMessage("엑셀자료에 적합하지 않은 내용이 존재합니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", stringBuilder5.ToString());
            return false;
        }

        private void DeleteListShow(ref bool bMsgShow, ref string strDeleteData)
        {
            strDeleteData = "  " + this.DD("품목코드") + "\n------------------------------------\n";
            for (int index = 0; index < this.deleteRowList.Count; ++index)
            {
                if (this.deleteRowList[index] != null)
                    strDeleteData += string.Format("{0}) {1}\n", index, this.deleteRowList[index].ToString());
            }
            bMsgShow = true;
        }

        private bool ChkExcel_Upload(DataTable dt엑셀, DataTable dtExists)
        {
            this._biz.Set엑셀체크데이터();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            StringBuilder stringBuilder4 = new StringBuilder();
            StringBuilder stringBuilder5 = new StringBuilder();
            int num1;
            bool flag1 = (num1 = 1) != 0;
            bool flag2 = num1 != 0;
            bool flag3 = num1 != 0;
            bool flag4 = num1 != 0;
            bool flag5 = num1 != 0;
            bool flag6 = false;
            int count = dt엑셀.Rows.Count;
            int num2 = 1;
            foreach (DataRow row in dt엑셀.Rows)
            {
                MsgControl.ShowMsg("엑셀업로드 중 입니다. " + Environment.NewLine + Environment.NewLine + "진행 상황 : " + num2.ToString() + "/" + count.ToString() + "  업로드 중...");
                ++num2;
                if (dtExists == null || dtExists.Select("CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'").Length <= 0)
                {
                    DataRow dataRow = this._flex.DataTable.NewRow();
                    Duzon.ERPU.SA.Settng.Data.DataCopy(row, dataRow);
                    string msgCls = "";
                    string msg계정구분;
                    bool flag7 = this._biz.Chk계정구분(dataRow, out msg계정구분);
                    string 품목군명;
                    string msg품목군;
                    bool flag8 = this._biz.Chk품목군(dataRow, out 품목군명, out msg품목군);
                    string NameCol1;
                    string msg창고1;
                    bool flag9 = this._biz.Chk창고(dataRow, "CD_SL", out NameCol1, out msg창고1);
                    string NameCol2;
                    string msg창고2;
                    bool flag10 = this._biz.Chk창고(dataRow, "CD_GISL", out NameCol2, out msg창고2);
                    bool flag11 = true;
                    if (this._sCls_Lms == "100")
                        flag11 = this._biz.ChkCls(dataRow, out msgCls);
                    dataRow["NM_GRP_ITEM"] = 품목군명;
                    dataRow["NM_SL"] = NameCol1;
                    dataRow["NM_GISL"] = NameCol2;
                    dataRow["CLS_PO"] = D.GetString(dataRow["CLS_PO"]);
                    if (flag7 && flag8 && (flag9 && flag10) && flag11)
                    {
                        dataRow["DT_VALID"] = D.GetString(dataRow["DT_VALID"]).Replace("/", string.Empty).Replace("-", string.Empty);
                        this._flex.DataTable.Rows.Add(dataRow);
                        flag6 = true;
                    }
                    else
                    {
                        flag5 = flag7 && flag5;
                        flag4 = flag8 && flag4;
                        flag3 = flag9 && flag3;
                        flag2 = flag10 && flag2;
                        flag1 = flag11 && flag1;
                        if (!flag7)
                            stringBuilder1.AppendLine(msg계정구분);
                        if (!flag8)
                            stringBuilder2.AppendLine(msg품목군);
                        if (!flag9)
                            stringBuilder3.AppendLine(msg창고1);
                        if (!flag10)
                            stringBuilder4.AppendLine(msg창고2);
                        if (!flag11)
                            stringBuilder5.AppendLine(msgCls);
                    }
                }
            }
            if (flag6)
            {
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.IsDataChanged = true;
                this._flex.RefreshBindng(this._flex.Row);
                this.Page_DataChanged(null, (EventArgs)null);
            }
            int num3 = (int)this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn엑셀업로드.Text);
            if (flag5 && flag4 && (flag3 && flag2) && flag1)
                return true;
            StringBuilder stringBuilder6 = new StringBuilder();
            if (!flag5)
            {
                stringBuilder6.AppendLine("<계정구분>");
                stringBuilder6.AppendLine(stringBuilder1.ToString());
            }
            if (!flag4)
            {
                stringBuilder6.AppendLine("<품목군>");
                stringBuilder6.AppendLine(stringBuilder2.ToString());
            }
            if (!flag3)
            {
                stringBuilder6.AppendLine("<입고창고>");
                stringBuilder6.AppendLine(stringBuilder3.ToString());
            }
            if (!flag2)
            {
                stringBuilder6.AppendLine("<출고창고>");
                stringBuilder6.AppendLine(stringBuilder4.ToString());
            }
            if (!flag1)
            {
                stringBuilder6.AppendLine("<대중소분류>");
                stringBuilder6.AppendLine(stringBuilder5.ToString());
            }
            int num4 = (int)this.ShowDetailMessage("엑셀자료에 적합하지 않은 내용이 존재합니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", stringBuilder6.ToString());
            return false;
        }

        private bool Chk공장코드 => !Checker.IsEmpty(this.cbo공장코드S, this.DD("공장코드"));

        private bool Get저장권한 => this.Menu_Level != MenuLevel.EMP && this.Grant.CanSave;

        private bool Chk품목코드
        {
            get
            {
                if (!(D.GetString(this._flex["CD_ITEM"]) == string.Empty))
                    return true;
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("품목"));
                if (this.tab.SelectedTab != this.tpg기본)
                {
                    this.tab.SelectedTab = this.tpg기본;
                    this.txt품목코드.Focus();
                }
                return false;
            }
        }

        private void rduFileIns1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile1, "1");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileIns2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile2, "2");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileIns3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile3, "3");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileIns4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile4, "4");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileIns5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile5, "5");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileIns6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                this.SetImageFileName(this.txtFile6, "6");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile1View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE1"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE1"])) && !string.IsNullOrEmpty(this.txtFile1.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE1"])))
                        path = D.GetString(this._flex["IMAGE1"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE1_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE1_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile1.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile2View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE2"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE2"])) && !string.IsNullOrEmpty(this.txtFile2.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE2"])))
                        path = D.GetString(this._flex["IMAGE2"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE2_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE2_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile2.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile3View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE3"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE3"])) && !string.IsNullOrEmpty(this.txtFile3.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE3"])))
                        path = D.GetString(this._flex["IMAGE3"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE3_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE3_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile3.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile4View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE4"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE4"])) && !string.IsNullOrEmpty(this.txtFile4.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE4"])))
                        path = D.GetString(this._flex["IMAGE4"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE4_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE4_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile4.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile5View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE5"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE5"])) && !string.IsNullOrEmpty(this.txtFile5.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE5"])))
                        path = D.GetString(this._flex["IMAGE5"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE5_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE5_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile5.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFile6View_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                D.GetString(this._flex["IMAGE6"]);
                string path = "";
                if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE6"])) && !string.IsNullOrEmpty(this.txtFile6.Text))
                {
                    if (System.IO.File.Exists(D.GetString(this._flex["IMAGE6"])))
                        path = D.GetString(this._flex["IMAGE6"]);
                    else if (System.IO.File.Exists(this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE6_NAME"])))
                        path = this.m_ImageFilePath + "\\" + D.GetString(this._flex["IMAGE6_NAME"]);
                    this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(path)));
                    this.Image보기경로색깔변경(false);
                    this.txtFile6.BackColor = Color.FromArgb(226, 239, 243);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE1"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE1"] = (this.txtFile1.Text = "");
                this._flex["IMAGE1_NAME"] = "";
                this.image_delete[0] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE2"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE2"] = (this.txtFile2.Text = "");
                this._flex["IMAGE2_NAME"] = "";
                this.image_delete[1] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE3"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE3"] = (this.txtFile3.Text = "");
                this._flex["IMAGE3_NAME"] = "";
                this.image_delete[2] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE4"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE4"] = (this.txtFile4.Text = "");
                this._flex["IMAGE4_NAME"] = "";
                this.image_delete[3] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE5"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE5"] = (this.txtFile5.Text = "");
                this._flex["IMAGE5_NAME"] = "";
                this.image_delete[4] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void rduFileDel6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string str = D.GetString(this._flex["IMAGE6"]);
                if (string.IsNullOrEmpty(str))
                    return;
                this._flex["IMAGE6"] = (this.txtFile6.Text = "");
                this._flex["IMAGE6_NAME"] = "";
                this.image_delete[5] = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void PictureFileDownLoad()
        {
            if (this.pixFile.Image != null)
            {
                this.pixFile.Image = (Image)null;
                this.Image보기경로색깔변경(false);
            }
            if (!this._flex.HasNormalRow)
                return;
            try
            {
                for (int index = 1; index <= 6; ++index)
                {
                    string str = D.GetString(this._flex["IMAGE" + index + "_NAME"]);
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (!Directory.Exists(this.m_ImageFilePath))
                            Directory.CreateDirectory(this.m_ImageFilePath);
                        string path = this.m_ImageFilePath + "\\" + str;
                        string fileName = Path.GetFileName(path);
                        string savePath = path.Replace(fileName, "");
                        FileManager.FileDownload(fileName, savePath, "/shared/MF_File_Mng/" + this.회사코드 + "/" + D.GetString(this._flex["CD_PLANT"]), "PITEM");
                    }
                }
            }
            finally
            {
            }
        }

        private bool PictureFileUpLoad(DataTable dt)
        {
            try
            {
                if (!this._flex.HasNormalRow || (dt == null || dt.Rows.Count == 0))
                    return true;
                MsgControl.ShowMsg("File Upload......");
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        for (int index = 1; index <= 6; ++index)
                        {
                            string str1 = D.GetString(row["IMAGE" + index + "_NAME"]);
                            string filePath = D.GetString(row["IMAGE" + index]);
                            string str2 = "";
                            string str3 = "";
                            if (row.HasVersion(DataRowVersion.Original))
                                str2 = D.GetString(row["IMAGE" + index + "_NAME", DataRowVersion.Original]);
                            if (row.HasVersion(DataRowVersion.Original))
                                str3 = D.GetString(row["IMAGE" + index, DataRowVersion.Original]);
                            if ((!(str1 == str2) || !(filePath == str3)) && filePath != "")
                            {
                                this._biz.UploadPlantPath(str1, D.GetString(row["CD_PLANT"]));
                                string str4 = string.Empty;
                                str4 = FileManager.FileUpload(Path.GetFileName(str1), filePath, "/shared/MF_File_Mng/" + this.회사코드 + "/" + D.GetString(row["CD_PLANT"]), "PITEM");
                                row["IMAGE" + index] = str1;
                            }
                        }
                    }
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return true;
        }

        private void Image보기경로색깔변경(bool bTrue)
        {
            if (!bTrue)
            {
                this.txtFile1.BackColor = SystemColors.Control;
                this.txtFile2.BackColor = SystemColors.Control;
                this.txtFile3.BackColor = SystemColors.Control;
                this.txtFile4.BackColor = SystemColors.Control;
                this.txtFile5.BackColor = SystemColors.Control;
                this.txtFile6.BackColor = SystemColors.Control;
            }
            else
            {
                this.txtFile1.BackColor = Color.FromArgb(226, 239, 243);
                this.txtFile2.BackColor = Color.FromArgb(226, 239, 243);
                this.txtFile3.BackColor = Color.FromArgb(226, 239, 243);
                this.txtFile4.BackColor = Color.FromArgb(226, 239, 243);
                this.txtFile5.BackColor = Color.FromArgb(226, 239, 243);
                this.txtFile6.BackColor = Color.FromArgb(226, 239, 243);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    string fileCode = D.GetString(this._flex["CD_PLANT"]) + "_" + D.GetString(this._flex["CD_ITEM"]);
                    if (new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, fileCode).ShowDialog() == DialogResult.Cancel)
                        ;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _btnAutoCdItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx대분류.CodeValue))
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.lbl대분류.Text);
                    this.ctx대분류.Focus();
                }
                else if (string.IsNullOrEmpty(this.ctx중분류.CodeValue))
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.lbl중분류.Text);
                    this.lbl중분류.Focus();
                }
                else if (string.IsNullOrEmpty(this.ctx소분류.CodeValue))
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, this.lbl소분류.Text);
                    this.lbl소분류.Focus();
                }
                else
                {
                    string cls1 = this._biz.GetCLS("04");
                    string cls2 = this._biz.GetCLS("01");
                    string cls3 = this._biz.GetCLS("02");
                    string cls4 = this._biz.GetCLS("03");
                    int num1 = int.Parse(cls1);
                    int length1 = int.Parse(cls2);
                    int length2 = int.Parse(cls3);
                    int length3 = int.Parse(cls4);
                    string maxCdItem = this._biz.GetMaxCdItem(D.GetString(this.cbo공장코드S.SelectedValue), this.ctx대분류.CodeValue, this.ctx중분류.CodeValue, this.ctx소분류.CodeValue, length1 + length2 + length3, num1);
                    if (num1 + length1 + length2 + length3 > 20)
                    {
                        this.ShowMessage("'자동채번등록'메뉴의 대분류 + 중분류 + 소분류 + 품목채번의 자릿수가 20자리를 넘으면 안됩니다!");
                    }
                    else
                    {
                        string cdFlag2_1 = this._biz.GetCdFlag2(this.ctx대분류.CodeValue, "CLS_L");
                        string cdFlag2_2 = this._biz.GetCdFlag2(this.ctx중분류.CodeValue, "CLS_M");
                        string cdFlag2_3 = this._biz.GetCdFlag2(this.ctx소분류.CodeValue, "CLS_S");
                        DataRow[] dataRowArray = this._flex.DataTable.Select("CLS_L = '" + this.ctx대분류.CodeValue + "' AND CLS_M = '" + this.ctx중분류.CodeValue + "' AND CLS_S = '" + this.ctx소분류.CodeValue + "' AND CD_ITEM <> '" + D.GetString(this._flex["CD_ITEM"]) + "'");
                        Decimal num3 = 0M;
                        foreach (DataRow dataRow in dataRowArray)
                        {
                            if (D.GetString(dataRow["CD_ITEM"]).Length == length1 + length2 + length3 + num1)
                            {
                                string s = D.GetString(dataRow["CD_ITEM"]).Substring(length1 + length2 + length3, num1);
                                if (!string.IsNullOrEmpty(s))
                                {
                                    Decimal result = 0M;
                                    Decimal.TryParse(s, out result);
                                    if (result > num3)
                                        num3 = result;
                                }
                            }
                        }
                        Decimal result1 = 0M;
                        Decimal.TryParse(maxCdItem, out result1);
                        if (result1 > num3)
                            num3 = result1;
                        this.txt품목코드.Text = (length1 - cdFlag2_1.Length != 0 ? string.Format("{0:D" + (length1 - cdFlag2_1.Length).ToString() + "}", 0) + cdFlag2_1 : cdFlag2_1) + (length2 - cdFlag2_2.Length != 0 ? string.Format("{0:D" + (length2 - cdFlag2_2.Length).ToString() + "}", 0) + cdFlag2_2 : cdFlag2_2) + (length3 - cdFlag2_3.Length != 0 ? string.Format("{0:D" + (length3 - cdFlag2_3.Length).ToString() + "}", 0) + cdFlag2_3 : cdFlag2_3) + (!(num3 == 0M) ? string.Format("{0:D" + cls1 + "}", ((int)num3 + 1)) : string.Format("{0:D" + cls1 + "}", 1));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetImageFileName(TextBoxExt textbox, string seq)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
            string str = D.GetString(this._flex["CD_ITEM"]) + "_0" + seq + fileInfo.Extension;
            textbox.Text = openFileDialog.FileName;
            this._flex["IMAGE" + seq] = openFileDialog.FileName;
            this._flex["IMAGE" + seq + "_NAME"] = str;
        }

        private void m_lblUnitIm_Click(object sender, EventArgs e)
        {
        }

        private void cbo과세구분매출_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
