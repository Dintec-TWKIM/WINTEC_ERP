using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;

namespace sale
{
    // **************************************
    // 작   성   자 : 안종호
    // 재 작  성 일 : 2009-12-16
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 수주관리
    // 페 이 지  명 : 수주현황
    // 프로젝트  명 : P_SA_SOSCH1
    // 수정사항 : 
    // 2013.03.19 : D20130319069 : 데이타 바인딩 방식변경 : GetComboData
    // 2013.07.30 : P20130726012 : 수주현황 각 tab별 대,중,소분류명 표시
    // **************************************
    public partial class P_SA_SOSCH1 : PageBase
    {
        #region ♥ 멤버필드

        private P_SA_SOSCH1_BIZ _biz = new P_SA_SOSCH1_BIZ();

        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;

        #endregion

        #region ♥ 초기화

        #region -> 생성자

        public P_SA_SOSCH1()
        {
            InitializeComponent();
            //MainGrids = new FlexGrid[] { _flexNOH, _flexPARTH, _flexITEMH, _flexDTH, _flexSOH, _flexGRPH, _flexPROJECTH };
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            InitGridNOH();
            InitGridNOL();

            InitGridPARTH();
            InitGridPARTL();

            InitGridITEMH();
            InitGridITEML();

            InitGridDTH();
            InitGridDTL();

            InitGridSOH();
            InitGridSOL();

            InitGridGRPH();
            InitGridGRPL();

            InitGridORGH();
            InitGridORGL();

            InitGridPROJECTH();
            InitGridPROJECTL();

            InitGrid납품처H();
            InitGrid납품처L();

            InitGrid납기일자H();
            InitGrid납기일자L();

            InitGrid영업담당자H();
            InitGrid영업담당자L();

            InitGrid제품군H();
            InitGrid제품군L();

            InitGrid거래처그룹H();
            InitGrid거래처그룹L();

            InitGrid거래처그룹2H();
            InitGrid거래처그룹2L();

            InitGridVAT구분H();
            InitGridVAT구분L();

            InitGrid품목군H();
            InitGrid품목군L();

            _flexNOH.DetailGrids = new FlexGrid[] { _flexNOL };
            _flexPARTH.DetailGrids = new FlexGrid[] { _flexPARTL };
            _flexITEMH.DetailGrids = new FlexGrid[] { _flexITEML };
            _flexDTH.DetailGrids = new FlexGrid[] { _flexDTL };
            _flexSOH.DetailGrids = new FlexGrid[] { _flexSOL };
            _flexGRPH.DetailGrids = new FlexGrid[] { _flexGRPL };
            _flexORGH.DetailGrids = new FlexGrid[] { _flexORGL };
            _flexPROJECTH.DetailGrids = new FlexGrid[] { _flexPROJECTL };
            _flex납품처H.DetailGrids = new FlexGrid[] { _flex납품처L };
            _flex납기일자H.DetailGrids = new FlexGrid[] { _flex납기일자L };
            _flex영업담당자H.DetailGrids = new FlexGrid[] { _flex영업담당자L };
            _flex제품군H.DetailGrids = new FlexGrid[] { _flex제품군L };
            _flex거래처그룹H.DetailGrids = new FlexGrid[] { _flex거래처그룹L };
            _flex거래처그룹2H.DetailGrids = new FlexGrid[] { _flex거래처그룹2L };
            _flexVAT구분H.DetailGrids = new FlexGrid[] { _flexVAT구분L };
            _flex품목군H.DetailGrids = new FlexGrid[] { _flex품목군L };
        }

        #endregion

        #region -> InitGridNOH

        private void InitGridNOH()
        {
            _flexNOH.BeginSetting(1, 1, false);
            _flexNOH.SetDummyColumn("S");
            _flexNOH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexNOH.SetCol("NO_SO", "수주번호", 120);
            _flexNOH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOH.SetCol("NM_SO", "수주형태", 150);
            _flexNOH.SetCol("CD_EXCH", "환종", 100);
            _flexNOH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexNOH.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flexNOH.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexNOH.SetCol("FG_TAXP", "계산서처리", 100);
            _flexNOH.SetCol("FG_BILL", "결재방법", 100);
            _flexNOH.SetCol("DC_RMK", "비고", 100);
            _flexNOH.SetCol("NO_EMP", "수주담당자", 100);
            _flexNOH.SetCol("NM_KOR", "수주담당자명", 120);
            _flexNOH.SetCol("NO_PO_PARTNER", "거래처 PO번호", 100);
            _flexNOH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexNOH.SetCol("LN_PARTNER", "거래처명", 100);
            _flexNOH.SetCol("ATTACHMENT", "첨부파일", 120);
            _flexNOH.SettingVersion = "1.0.1.2";
            _flexNOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexNOH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexNOH.HelpClick += new EventHandler(_flexNOH_HelpClick);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL"
                || Global.MainFrame.ServerKey == "SQL_"
                || Global.MainFrame.ServerKey == "SINJINSM")
            {
                _flexNOH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
                _flexNOH.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            }

            //2008.06.17 엑셀내려받는 기능추가
            if (Global.MainFrame.ServerKey == "PEACE2" || Global.MainFrame.ServerKey == "108" || Global.MainFrame.ServerKey == "DZSQL")
            {
                _flexNOH.AddMenuSeperator();
                ToolStripMenuItem parent = _flexNOH.AddPopup("엑셀파일관리");
                _flexNOH.AddMenuItem(parent, "PROFORMA INVOICE", Menu_Click);
            }
        }

        #endregion

        #region -> InitGridNOL

        private void InitGridNOL()
        {
            _flexNOL.BeginSetting(1, 1, false);
            _flexNOL.SetCol("NM_PLANT", "공장", 120);
            _flexNOL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexNOL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexNOL.SetCol("CD_ITEM", "품목코드", 100);
            _flexNOL.SetCol("NM_ITEM", "품목명", 120);
            _flexNOL.SetCol("STND_ITEM", "규격", 80);
            _flexNOL.SetCol("UNIT_SO", "단위", 80);
            _flexNOL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexNOL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexNOL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexNOL.SetCol("STA_SO", "수주상태", 60);
            _flexNOL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120);
            _flexNOL.SetCol("NM_ITEM_PARTNER", "발주품번", 120);

            _flexNOL.SetCol("GI_PARTNER", "납품처코드", 120);
            _flexNOL.SetCol("GI_NAME", "납품처명", 120);
            _flexNOL.SetCol("NO_PO_PARTNER", "거래처PO번호", 120);
            _flexNOL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 120);
            _flexNOL.SetCol("NM_KOR", "영업담당자", 100);

            _flexNOL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexNOL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexNOL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexNOL.SetCol("CD_SL", "창고코드", 100);
            _flexNOL.SetCol("NM_SL", "창고명", 100);
            _flexNOL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexNOL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            //2013.07.30 대,중,소 분류명 컬럼 추가
            _flexNOL.SetCol("CLS_L", "대분류", 80, false);
            _flexNOL.SetCol("CLS_M", "중분류", 80, false);
            _flexNOL.SetCol("CLS_S", "소분류", 80, false);

            _flexNOL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexNOL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexNOL.SetCol("GRP_MFG", "제품군코드", false);
            _flexNOL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexNOL.SetCol("MAT_ITEM", "재질", false);
            _flexNOL.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            _flexNOL.SetCol("NO_RELATION", "연동번호", false);
            _flexNOL.SetCol("SEQ_RELATION", "연동항번", false);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL" 
                || Global.MainFrame.ServerKey == "SQL_" 
                || Global.MainFrame.ServerKey == "SINJINSM")
            {
                _flexNOL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);

                _flexNOL.SetCol("CD_USERDEF4", "출력여부", 80, false, typeof(string));
                _flexNOL.SetCol("TXT_USERDEF6", "최종출력시간", 150, false, typeof(string));
                _flexNOL.SetCol("NUM_USERDEF10", "출력횟수", 80, false, typeof(decimal));

                _flexNOL.Cols["NUM_USERDEF10"].Format = "####";

                _flexNOL.Cols["CD_USERDEF4"].TextAlign = TextAlignEnum.CenterCenter;
                _flexNOL.Cols["TXT_USERDEF6"].TextAlign = TextAlignEnum.CenterCenter;
                _flexNOL.Cols["NUM_USERDEF10"].TextAlign = TextAlignEnum.CenterCenter;

                _flexNOL.SetDummyColumn("S");

                _flexNOL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
                _flexNOL.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            }

            _flexNOL.SettingVersion = "1.0.0.5";    //2013.07.30 수정
            _flexNOL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexNOL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridPARTH

        private void InitGridPARTH()
        {
            _flexPARTH.BeginSetting(1, 1, false);
            _flexPARTH.SetDummyColumn("S");
            _flexPARTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexPARTH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexPARTH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexPARTH.SetCol("NM_KOR", "영업담당자", 120);
            _flexPARTH.SettingVersion = "1.0.1.1";
            _flexPARTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexPARTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL"
                || Global.MainFrame.ServerKey == "SQL_"
                || Global.MainFrame.ServerKey == "SINJINSM")
            {
                _flexPARTH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
                _flexPARTH.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            }
        }

        #endregion

        #region -> InitGridPARTL

        private void InitGridPARTL()
        {
            _flexPARTL.BeginSetting(1, 1, false);
            _flexPARTL.SetCol("NM_PLANT", "공장", 120);
            _flexPARTL.SetCol("NO_SO", "수주번호", 120);
            _flexPARTL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPARTL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPARTL.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexPARTL.SetCol("CD_ITEM", "품목코드", 100);
            _flexPARTL.SetCol("NM_ITEM", "품목명", 120);
            _flexPARTL.SetCol("STND_ITEM", "규격", 80);
            _flexPARTL.SetCol("UNIT_SO", "단위", 80);
            _flexPARTL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexPARTL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexPARTL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexPARTL.SetCol("CD_EXCH", "환종", 100);
            _flexPARTL.SetCol("STA_SO", "수주상태", 60);
            _flexPARTL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexPARTL.SetCol("RT_EXCH", "환율", 100);
            _flexPARTL.SetCol("NM_SO", "수주형태", 100);
            _flexPARTL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexPARTL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexPARTL.SetCol("FG_BILL", "결재방법", 100);

            _flexPARTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexPARTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexPARTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexPARTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexPARTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexPARTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexPARTL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexPARTL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);

            _flexPARTL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            _flexPARTL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexPARTL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexPARTL.SetCol("CD_SL", "창고코드", 100);
            _flexPARTL.SetCol("NM_SL", "창고명", 100);

            _flexPARTL.SetCol("DC_RMK", "헤더비고", 100, false);
            _flexPARTL.SetCol("DC_RMK1", "헤더비고1", 100, false);
            _flexPARTL.SetCol("DC1", "라인비고1", 100, false);
            _flexPARTL.SetCol("DC2", "라인비고2", 100, false);
            _flexPARTL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexPARTL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexPARTL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexPARTL.SetCol("GRP_MFG", "제품군코드", false);
            _flexPARTL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexPARTL.SetCol("MAT_ITEM", "재질", false);
            _flexPARTL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL"
                || Global.MainFrame.ServerKey == "SQL_"
                || Global.MainFrame.ServerKey == "SINJINSM")
            {
                _flexPARTL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);

                _flexPARTL.SetCol("CD_USERDEF4", "출력여부", 80, false, typeof(string));
                _flexPARTL.SetCol("TXT_USERDEF6", "최종출력시간", 150, false, typeof(string));
                _flexPARTL.SetCol("NUM_USERDEF10", "출력횟수", 80, false, typeof(decimal));

                _flexPARTL.Cols["NUM_USERDEF10"].Format = "####";

                _flexPARTL.Cols["CD_USERDEF4"].TextAlign = TextAlignEnum.CenterCenter;
                _flexPARTL.Cols["TXT_USERDEF6"].TextAlign = TextAlignEnum.CenterCenter;
                _flexPARTL.Cols["NUM_USERDEF10"].TextAlign = TextAlignEnum.CenterCenter;

                _flexPARTL.SetDummyColumn("S");

                _flexPARTL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
                _flexPARTL.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            }

            _flexPARTL.SettingVersion = "1.0.0.4";
            _flexPARTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexPARTL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridITEMH

        private void InitGridITEMH()
        {
            _flexITEMH.BeginSetting(1, 1, false);
            _flexITEMH.SetDummyColumn("S");
            _flexITEMH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexITEMH.SetCol("CD_ITEM", "품목코드", 100);
            _flexITEMH.SetCol("NM_ITEM", "품목명", 120);
            _flexITEMH.SetCol("STND_ITEM", "규격", 80);

            _flexITEMH.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexITEMH.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexITEMH.SetCol("GRP_MFG", "제품군코드", false);
            _flexITEMH.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexITEMH.SetCol("MAT_ITEM", "재질", false);
            _flexITEMH.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexITEMH.SettingVersion = "1.0.1.0";

            _flexITEMH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexITEMH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridITEML

        private void InitGridITEML()
        {
            _flexITEML.BeginSetting(1, 1, false);
            _flexITEML.SetCol("NM_PLANT", "공장", 120);
            _flexITEML.SetCol("NO_SO", "수주번호", 120);
            _flexITEML.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEML.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEML.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexITEML.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexITEML.SetCol("LN_PARTNER", "거래처명", 120);

            _flexITEML.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexITEML.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexITEML.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexITEML.SetCol("CD_EXCH", "환종", 100);
            _flexITEML.SetCol("STA_SO", "수주상태", 60);
            _flexITEML.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexITEML.SetCol("RT_EXCH", "환율", 100);
            _flexITEML.SetCol("NM_SO", "수주형태", 100);
            _flexITEML.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexITEML.SetCol("FG_TAXP", "계산서처리", 100);
            _flexITEML.SetCol("FG_BILL", "결재방법", 100);

            _flexITEML.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexITEML.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexITEML.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexITEML.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexITEML.SetCol("NO_EMP", "수주담당자", 100);
            _flexITEML.SetCol("NM_KOR", "수주담당자명", 120);

            _flexITEML.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexITEML.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexITEML.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexITEML.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexITEML.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexITEML.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexITEML.SetCol("CD_SL", "창고코드", 100);
            _flexITEML.SetCol("NM_SL", "창고명", 100);

            _flexITEML.SetCol("DC_RMK", "헤더비고", 100, false);
            _flexITEML.SetCol("DC_RMK1", "헤더비고1", 100, false);
            _flexITEML.SetCol("DC1", "라인비고1", 100, false);
            _flexITEML.SetCol("DC2", "라인비고2", 100, false);
            _flexITEML.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexITEML.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexITEML.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexITEML.SetCol("GRP_MFG", "제품군코드", false);
            _flexITEML.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexITEML.SetCol("MAT_ITEM", "재질", false);
            _flexITEML.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexITEML.SettingVersion = "1.0.0.4";
            _flexITEML.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexITEML.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridDTH

        private void InitGridDTH()
        {
            _flexDTH.BeginSetting(1, 1, false);
            _flexDTH.SetDummyColumn("S");
            _flexDTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexDTH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexDTH.SettingVersion = "1.0.1.0";

            _flexDTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexDTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridDTL

        private void InitGridDTL()
        {
            _flexDTL.BeginSetting(1, 1, false);
            _flexDTL.SetCol("NM_PLANT", "공장", 120);
            _flexDTL.SetCol("NO_SO", "수주번호", 120);
            _flexDTL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexDTL.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexDTL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexDTL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexDTL.SetCol("CD_ITEM", "품목코드", 100);
            _flexDTL.SetCol("NM_ITEM", "품목명", 120);
            _flexDTL.SetCol("STND_ITEM", "규격", 80);
            _flexDTL.SetCol("UNIT_SO", "단위", 80);
            _flexDTL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexDTL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexDTL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexDTL.SetCol("CD_EXCH", "환종", 100);
            _flexDTL.SetCol("STA_SO", "수주상태", 60);
            _flexDTL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexDTL.SetCol("RT_EXCH", "환율", 100);
            _flexDTL.SetCol("NM_SO", "수주형태", 100);
            _flexDTL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexDTL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexDTL.SetCol("FG_BILL", "결재방법", 100);

            _flexDTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexDTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexDTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexDTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexDTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexDTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexDTL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexDTL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexDTL.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexDTL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexDTL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexDTL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexDTL.SetCol("CD_SL", "창고코드", 100);
            _flexDTL.SetCol("NM_SL", "창고명", 100);

            _flexDTL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexDTL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexDTL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexDTL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexDTL.SetCol("GRP_MFG", "제품군코드", false);
            _flexDTL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexDTL.SetCol("MAT_ITEM", "재질", false);
            _flexDTL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexDTL.SettingVersion = "1.0.0.2";
            _flexDTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexDTL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridSOH

        private void InitGridSOH()
        {
            _flexSOH.BeginSetting(1, 1, false);
            _flexSOH.SetDummyColumn("S");
            _flexSOH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexSOH.SetCol("TP_SO", "수주형태코드", 100);
            _flexSOH.SetCol("NM_SO", "수주형태명", 150);
            _flexSOH.SettingVersion = "1.0.1.1";
            _flexSOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexSOH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridSOL

        private void InitGridSOL()
        {
            _flexSOL.BeginSetting(1, 1, false);
            _flexSOL.SetCol("NM_PLANT", "공장", 120);
            _flexSOL.SetCol("NO_SO", "수주번호", 120);
            _flexSOL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexSOL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexSOL.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexSOL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexSOL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexSOL.SetCol("CD_ITEM", "품목코드", 100);
            _flexSOL.SetCol("NM_ITEM", "품목명", 120);
            _flexSOL.SetCol("STND_ITEM", "규격", 80);
            _flexSOL.SetCol("UNIT_SO", "단위", 80);
            _flexSOL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexSOL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexSOL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexSOL.SetCol("CD_EXCH", "환종", 100);
            _flexSOL.SetCol("STA_SO", "수주상태", 60);
            _flexSOL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexSOL.SetCol("RT_EXCH", "환율", 100);
            _flexSOL.SetCol("NM_SO", "수주형태", 100);
            _flexSOL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexSOL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexSOL.SetCol("FG_BILL", "결재방법", 100);

            _flexSOL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexSOL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexSOL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexSOL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexSOL.SetCol("NO_EMP", "수주담당자", 100);
            _flexSOL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexSOL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexSOL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexSOL.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexSOL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexSOL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexSOL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexSOL.SetCol("CD_SL", "창고코드", 100);
            _flexSOL.SetCol("NM_SL", "창고명", 100);

            _flexSOL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexSOL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexSOL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexSOL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexSOL.SetCol("GRP_MFG", "제품군코드", false);
            _flexSOL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexSOL.SetCol("MAT_ITEM", "재질", false);
            _flexSOL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexSOL.SettingVersion = "1.0.0.2";
            _flexSOL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexSOL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridGRPH

        private void InitGridGRPH()
        {
            _flexGRPH.BeginSetting(1, 1, false);
            _flexGRPH.SetDummyColumn("S");
            _flexGRPH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexGRPH.SetCol("CD_SALEGRP", "영업그룹코드", 100);
            _flexGRPH.SetCol("NM_SALEGRP", "영업그룹명", 120);
            _flexGRPH.SettingVersion = "1.0.1.5";

            _flexGRPH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexGRPH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridGRPL

        private void InitGridGRPL()
        {
            _flexGRPL.BeginSetting(1, 1, false);
            _flexGRPL.SetCol("NM_PLANT", "공장", 120);
            _flexGRPL.SetCol("NO_SO", "수주번호", 120);
            _flexGRPL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexGRPL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexGRPL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexGRPL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexGRPL.SetCol("CD_ITEM", "품목코드", 100);
            _flexGRPL.SetCol("NM_ITEM", "품목명", 120);
            _flexGRPL.SetCol("STND_ITEM", "규격", 80);
            _flexGRPL.SetCol("UNIT_SO", "단위", 80);
            _flexGRPL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexGRPL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexGRPL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexGRPL.SetCol("CD_EXCH", "환종", 100);
            _flexGRPL.SetCol("STA_SO", "수주상태", 60);
            _flexGRPL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexGRPL.SetCol("RT_EXCH", "환율", 100);
            _flexGRPL.SetCol("NM_SO", "수주형태", 100);
            _flexGRPL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexGRPL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexGRPL.SetCol("FG_BILL", "결재방법", 100);

            _flexGRPL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexGRPL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexGRPL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexGRPL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexGRPL.SetCol("NO_EMP", "수주담당자", 100);
            _flexGRPL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexGRPL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexGRPL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexGRPL.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexGRPL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexGRPL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexGRPL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexGRPL.SetCol("CD_SL", "창고코드", 100);
            _flexGRPL.SetCol("NM_SL", "창고명", 100);

            _flexGRPL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexGRPL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexGRPL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexGRPL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexGRPL.SetCol("GRP_MFG", "제품군코드", false);
            _flexGRPL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexGRPL.SetCol("MAT_ITEM", "재질", false);
            _flexGRPL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexGRPL.SettingVersion = "1.0.0.2";
            _flexGRPL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexGRPL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridORGH

        private void InitGridORGH()
        {
            _flexORGH.BeginSetting(1, 1, false);
            _flexORGH.SetDummyColumn("S");
            _flexORGH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);

            _flexORGH.SetCol("CD_SALEORG", "영업조직코드", 100);
            _flexORGH.SetCol("NM_SALEORG", "영업조직", 100);
            _flexORGH.SettingVersion = "1.0.0.0";

            _flexORGH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexORGH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridORGL

        private void InitGridORGL()
        {
            _flexORGL.BeginSetting(1, 1, false);
            _flexORGL.SetCol("NM_PLANT", "공장", 120);
            _flexORGL.SetCol("NO_SO", "수주번호", 120);
            _flexORGL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexORGL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexORGL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexORGL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexORGL.SetCol("CD_ITEM", "품목코드", 100);
            _flexORGL.SetCol("NM_ITEM", "품목명", 120);
            _flexORGL.SetCol("STND_ITEM", "규격", 80);
            _flexORGL.SetCol("UNIT_SO", "단위", 80);
            _flexORGL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexORGL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexORGL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexORGL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexORGL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexORGL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexORGL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexORGL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexORGL.SetCol("CD_EXCH", "환종", 100);
            _flexORGL.SetCol("STA_SO", "수주상태", 60);
            _flexORGL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexORGL.SetCol("RT_EXCH", "환율", 100);
            _flexORGL.SetCol("NM_SO", "수주형태", 100);
            _flexORGL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexORGL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexORGL.SetCol("FG_BILL", "결재방법", 100);

            _flexORGL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexORGL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexORGL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexORGL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexORGL.SetCol("NO_EMP", "수주담당자", 100);
            _flexORGL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexORGL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexORGL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexORGL.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexORGL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexORGL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexORGL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexORGL.SetCol("CD_SL", "창고코드", 100);
            _flexORGL.SetCol("NM_SL", "창고명", 100);

            _flexORGL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexORGL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexORGL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexORGL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexORGL.SetCol("GRP_MFG", "제품군코드", false);
            _flexORGL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexORGL.SetCol("MAT_ITEM", "재질", false);
            _flexORGL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexORGL.SettingVersion = "1.0.0.2";
            _flexORGL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexORGL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridPROJECTH

        private void InitGridPROJECTH()
        {
            _flexPROJECTH.BeginSetting(1, 1, false);
            _flexPROJECTH.SetDummyColumn("S");
            _flexPROJECTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexPROJECTH.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexPROJECTH.SetCol("NM_PROJECT", "프로젝트명", 120);
            _flexPROJECTH.SettingVersion = "1.0.1.1";

            _flexPROJECTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexPROJECTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridPROJECTL

        private void InitGridPROJECTL()
        {
            _flexPROJECTL.BeginSetting(1, 1, false);
            _flexPROJECTL.SetCol("NM_PLANT", "공장", 120);
            _flexPROJECTL.SetCol("NO_SO", "수주번호", 120);
            _flexPROJECTL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPROJECTL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPROJECTL.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexPROJECTL.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexPROJECTL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexPROJECTL.SetCol("CD_ITEM", "품목코드", 100);
            _flexPROJECTL.SetCol("NM_ITEM", "품목명", 120);
            _flexPROJECTL.SetCol("STND_ITEM", "규격", 80);
            _flexPROJECTL.SetCol("UNIT_SO", "단위", 80);
            _flexPROJECTL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPROJECTL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPROJECTL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPROJECTL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPROJECTL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPROJECTL.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexPROJECTL.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexPROJECTL.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexPROJECTL.SetCol("CD_EXCH", "환종", 100);
            _flexPROJECTL.SetCol("STA_SO", "수주상태", 60);
            _flexPROJECTL.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexPROJECTL.SetCol("RT_EXCH", "환율", 100);
            _flexPROJECTL.SetCol("NM_SO", "수주형태", 100);
            _flexPROJECTL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexPROJECTL.SetCol("FG_TAXP", "계산서처리", 100);
            _flexPROJECTL.SetCol("FG_BILL", "결재방법", 100);

            _flexPROJECTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexPROJECTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexPROJECTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexPROJECTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexPROJECTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexPROJECTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexPROJECTL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexPROJECTL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexPROJECTL.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexPROJECTL.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexPROJECTL.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexPROJECTL.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexPROJECTL.SetCol("CD_SL", "창고코드", 100);
            _flexPROJECTL.SetCol("NM_SL", "창고명", 100);

            _flexPROJECTL.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexPROJECTL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexPROJECTL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexPROJECTL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexPROJECTL.SetCol("GRP_MFG", "제품군코드", false);
            _flexPROJECTL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexPROJECTL.SetCol("MAT_ITEM", "재질", false);
            _flexPROJECTL.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexPROJECTL.SettingVersion = "1.0.0.2";
            _flexPROJECTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexPROJECTL.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid납품처H

        private void InitGrid납품처H()
        {
            _flex납품처H.BeginSetting(1, 1, false);
            _flex납품처H.SetDummyColumn("S");
            _flex납품처H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex납품처H.SetCol("GI_PARTNER", "납품처코드", 100);
            _flex납품처H.SetCol("LN_GI_PARTNER", "납품처명", 120);
            _flex납품처H.SettingVersion = "1.0.0.0";

            _flex납품처H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex납품처H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid납품처L

        private void InitGrid납품처L()
        {
            _flex납품처L.BeginSetting(1, 1, false);
            _flex납품처L.SetCol("NM_PLANT", "공장", 120);
            _flex납품처L.SetCol("NO_SO", "수주번호", 120);
            _flex납품처L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex납품처L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex납품처L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex납품처L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex납품처L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex납품처L.SetCol("CD_ITEM", "품목코드", 100);
            _flex납품처L.SetCol("NM_ITEM", "품목명", 120);
            _flex납품처L.SetCol("STND_ITEM", "규격", 80);
            _flex납품처L.SetCol("UNIT_SO", "단위", 80);
            _flex납품처L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납품처L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납품처L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납품처L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납품처L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납품처L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex납품처L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex납품처L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex납품처L.SetCol("CD_EXCH", "환종", 100);
            _flex납품처L.SetCol("STA_SO", "수주상태", 60);
            _flex납품처L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex납품처L.SetCol("RT_EXCH", "환율", 100);
            _flex납품처L.SetCol("NM_SO", "수주형태", 100);
            _flex납품처L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex납품처L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex납품처L.SetCol("FG_BILL", "결재방법", 100);

            _flex납품처L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex납품처L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex납품처L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex납품처L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex납품처L.SetCol("NO_EMP", "수주담당자", 100);
            _flex납품처L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex납품처L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex납품처L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex납품처L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex납품처L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex납품처L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex납품처L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex납품처L.SetCol("CD_SL", "창고코드", 100);
            _flex납품처L.SetCol("NM_SL", "창고명", 100);

            _flex납품처L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex납품처L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex납품처L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex납품처L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex납품처L.SetCol("GRP_MFG", "제품군코드", false);
            _flex납품처L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex납품처L.SetCol("MAT_ITEM", "재질", false);
            _flex납품처L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex납품처L.SettingVersion = "1.0.0.2";
            _flex납품처L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex납품처L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid납기일자H

        private void InitGrid납기일자H()
        {
            _flex납기일자H.BeginSetting(1, 1, false);
            _flex납기일자H.SetDummyColumn("S");
            _flex납기일자H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex납기일자H.SetCol("DT_DUEDATE", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex납기일자H.SettingVersion = "1.0.0.0";
            _flex납기일자H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex납기일자H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid납기일자L

        private void InitGrid납기일자L()
        {
            _flex납기일자L.BeginSetting(1, 1, false);
            _flex납기일자L.SetCol("NM_PLANT", "공장", 120);
            _flex납기일자L.SetCol("NO_SO", "수주번호", 120);
            _flex납기일자L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납기일자L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex납기일자L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex납기일자L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex납기일자L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex납기일자L.SetCol("CD_ITEM", "품목코드", 100);
            _flex납기일자L.SetCol("NM_ITEM", "품목명", 120);
            _flex납기일자L.SetCol("STND_ITEM", "규격", 80);
            _flex납기일자L.SetCol("UNIT_SO", "단위", 80);
            _flex납기일자L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납기일자L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납기일자L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납기일자L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex납기일자L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex납기일자L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex납기일자L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex납기일자L.SetCol("CD_EXCH", "환종", 100);
            _flex납기일자L.SetCol("STA_SO", "수주상태", 60);
            _flex납기일자L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex납기일자L.SetCol("RT_EXCH", "환율", 100);
            _flex납기일자L.SetCol("NM_SO", "수주형태", 100);
            _flex납기일자L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex납기일자L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex납기일자L.SetCol("FG_BILL", "결재방법", 100);

            _flex납기일자L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex납기일자L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex납기일자L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex납기일자L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex납기일자L.SetCol("NO_EMP", "수주담당자", 100);
            _flex납기일자L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex납기일자L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex납기일자L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex납기일자L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex납기일자L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex납기일자L.Cols["UMVAT_SO"].Visible = false;
            _flex납기일자L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex납기일자L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex납기일자L.SetCol("CD_SL", "창고코드", 100);
            _flex납기일자L.SetCol("NM_SL", "창고명", 100);

            _flex납기일자L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex납기일자L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex납기일자L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex납기일자L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex납기일자L.SetCol("GRP_MFG", "제품군코드", false);
            _flex납기일자L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex납기일자L.SetCol("MAT_ITEM", "재질", false);
            _flex납기일자L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex납기일자L.SettingVersion = "1.0.0.2";
            _flex납기일자L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex납기일자L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid영업담당자H

        private void InitGrid영업담당자H()
        {
            _flex영업담당자H.BeginSetting(1, 1, false);
            _flex영업담당자H.SetDummyColumn("S");
            _flex영업담당자H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex영업담당자H.SetCol("CD_EMP_SALE", "영업담당자코드", 100);
            _flex영업담당자H.SetCol("NM_EMP_SALE", "영업담당자명", 150);
            _flex영업담당자H.SettingVersion = "1.0.0.0";

            _flex영업담당자H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex영업담당자H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid영업담당자L

        private void InitGrid영업담당자L()
        {
            _flex영업담당자L.BeginSetting(1, 1, false);
            _flex영업담당자L.SetCol("NM_PLANT", "공장", 120);
            _flex영업담당자L.SetCol("NO_SO", "수주번호", 120);
            _flex영업담당자L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업담당자L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex영업담당자L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex영업담당자L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex영업담당자L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex영업담당자L.SetCol("CD_ITEM", "품목코드", 100);
            _flex영업담당자L.SetCol("NM_ITEM", "품목명", 120);
            _flex영업담당자L.SetCol("STND_ITEM", "규격", 80);
            _flex영업담당자L.SetCol("UNIT_SO", "단위", 80);
            _flex영업담당자L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업담당자L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업담당자L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업담당자L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업담당자L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex영업담당자L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex영업담당자L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex영업담당자L.SetCol("CD_EXCH", "환종", 100);
            _flex영업담당자L.SetCol("STA_SO", "수주상태", 60);
            _flex영업담당자L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex영업담당자L.SetCol("RT_EXCH", "환율", 100);
            _flex영업담당자L.SetCol("NM_SO", "수주형태", 100);
            _flex영업담당자L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex영업담당자L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex영업담당자L.SetCol("FG_BILL", "결재방법", 100);

            _flex영업담당자L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex영업담당자L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex영업담당자L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex영업담당자L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex영업담당자L.SetCol("NO_EMP", "수주담당자", 100);
            _flex영업담당자L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex영업담당자L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex영업담당자L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);

            _flex영업담당자L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex영업담당자L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex영업담당자L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex영업담당자L.SetCol("CD_SL", "창고코드", 100);
            _flex영업담당자L.SetCol("NM_SL", "창고명", 100);

            _flex영업담당자L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex영업담당자L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex영업담당자L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex영업담당자L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex영업담당자L.SetCol("GRP_MFG", "제품군코드", false);
            _flex영업담당자L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex영업담당자L.SetCol("MAT_ITEM", "재질", false);
            _flex영업담당자L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex영업담당자L.SettingVersion = "1.0.0.2";
            _flex영업담당자L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex영업담당자L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid제품군H

        private void InitGrid제품군H()
        {
            _flex제품군H.BeginSetting(1, 1, false);
            _flex제품군H.SetDummyColumn("S");
            _flex제품군H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex제품군H.SetCol("GRP_MFG", "제품군", 100);
            _flex제품군H.SetCol("NM_GRP_MFG", "제품군명", 150);
            _flex제품군H.SettingVersion = "1.0.1.1";

            _flex제품군H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex제품군H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid제품군L

        private void InitGrid제품군L()
        {
            _flex제품군L.BeginSetting(1, 1, false);
            _flex제품군L.SetCol("NM_PLANT", "공장", 120);
            _flex제품군L.SetCol("NO_SO", "수주번호", 120);
            _flex제품군L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex제품군L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex제품군L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex제품군L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex제품군L.SetCol("CD_ITEM", "품목코드", 100);
            _flex제품군L.SetCol("NM_ITEM", "품목명", 120);
            _flex제품군L.SetCol("STND_ITEM", "규격", 80);
            _flex제품군L.SetCol("UNIT_SO", "단위", 80);
            _flex제품군L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex제품군L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex제품군L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex제품군L.SetCol("CD_EXCH", "환종", 100);
            _flex제품군L.SetCol("STA_SO", "수주상태", 60);
            _flex제품군L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex제품군L.SetCol("RT_EXCH", "환율", 100);
            _flex제품군L.SetCol("NM_SO", "수주형태", 100);
            _flex제품군L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex제품군L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex제품군L.SetCol("FG_BILL", "결재방법", 100);

            _flex제품군L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex제품군L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex제품군L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex제품군L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex제품군L.SetCol("NO_EMP", "수주담당자", 100);
            _flex제품군L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex제품군L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex제품군L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex제품군L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex제품군L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex제품군L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex제품군L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex제품군L.SetCol("CD_SL", "창고코드", 100);
            _flex제품군L.SetCol("NM_SL", "창고명", 100);

            _flex제품군L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex제품군L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex제품군L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex제품군L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex제품군L.SetCol("GRP_MFG", "제품군코드", false);
            _flex제품군L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex제품군L.SetCol("MAT_ITEM", "재질", false);
            _flex제품군L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex제품군L.SettingVersion = "1.0.0.2";
            _flex제품군L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex제품군L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid거래처그룹H

        private void InitGrid거래처그룹H()
        {
            _flex거래처그룹H.BeginSetting(1, 1, false);
            _flex거래처그룹H.SetDummyColumn("S");
            _flex거래처그룹H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex거래처그룹H.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 100);
            _flex거래처그룹H.SetCol("NM_PARTNER_GRP", "거래처그룹", 150);
            _flex거래처그룹H.SettingVersion = "1.0.0.1";
            _flex거래처그룹H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex거래처그룹H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid거래처그룹L

        private void InitGrid거래처그룹L()
        {
            _flex거래처그룹L.BeginSetting(1, 1, false);
            _flex거래처그룹L.SetCol("NM_PLANT", "공장", 120);
            _flex거래처그룹L.SetCol("NO_SO", "수주번호", 120);
            _flex거래처그룹L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처그룹L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처그룹L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex거래처그룹L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex거래처그룹L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex거래처그룹L.SetCol("CD_ITEM", "품목코드", 100);
            _flex거래처그룹L.SetCol("NM_ITEM", "품목명", 120);
            _flex거래처그룹L.SetCol("STND_ITEM", "규격", 80);
            _flex거래처그룹L.SetCol("UNIT_SO", "단위", 80);
            _flex거래처그룹L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처그룹L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex거래처그룹L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹L.SetCol("CD_EXCH", "환종", 100);
            _flex거래처그룹L.SetCol("STA_SO", "수주상태", 60);
            _flex거래처그룹L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex거래처그룹L.SetCol("RT_EXCH", "환율", 100);
            _flex거래처그룹L.SetCol("NM_SO", "수주형태", 100);
            _flex거래처그룹L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex거래처그룹L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex거래처그룹L.SetCol("FG_BILL", "결재방법", 100);

            _flex거래처그룹L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex거래처그룹L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex거래처그룹L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex거래처그룹L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex거래처그룹L.SetCol("NO_EMP", "수주담당자", 100);
            _flex거래처그룹L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex거래처그룹L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex거래처그룹L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex거래처그룹L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex거래처그룹L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처그룹L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹L.SetCol("CD_SL", "창고코드", 100);
            _flex거래처그룹L.SetCol("NM_SL", "창고명", 100);

            _flex거래처그룹L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex거래처그룹L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex거래처그룹L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex거래처그룹L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex거래처그룹L.SetCol("GRP_MFG", "제품군코드", false);
            _flex거래처그룹L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex거래처그룹L.SetCol("MAT_ITEM", "재질", false);
            _flex거래처그룹L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex거래처그룹L.SettingVersion = "1.0.0.1";
            _flex거래처그룹L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex거래처그룹L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid거래처그룹2H

        private void InitGrid거래처그룹2H()
        {
            _flex거래처그룹2H.BeginSetting(1, 1, false);
            _flex거래처그룹2H.SetDummyColumn("S");
            _flex거래처그룹2H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex거래처그룹2H.SetCol("CD_PARTNER_GRP_2", "거래처그룹2코드", 100);
            _flex거래처그룹2H.SetCol("NM_PARTNER_GRP_2", "거래처그룹2", 150);
            _flex거래처그룹2H.SettingVersion = "1.0.0.1";
            _flex거래처그룹2H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex거래처그룹2H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid거래처그룹2L

        private void InitGrid거래처그룹2L()
        {
            _flex거래처그룹2L.BeginSetting(1, 1, false);
            _flex거래처그룹2L.SetCol("NM_PLANT", "공장", 120);
            _flex거래처그룹2L.SetCol("NO_SO", "수주번호", 120);
            _flex거래처그룹2L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처그룹2L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처그룹2L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex거래처그룹2L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex거래처그룹2L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex거래처그룹2L.SetCol("CD_ITEM", "품목코드", 100);
            _flex거래처그룹2L.SetCol("NM_ITEM", "품목명", 120);
            _flex거래처그룹2L.SetCol("STND_ITEM", "규격", 80);
            _flex거래처그룹2L.SetCol("UNIT_SO", "단위", 80);
            _flex거래처그룹2L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹2L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹2L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹2L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹2L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처그룹2L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처그룹2L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex거래처그룹2L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹2L.SetCol("CD_EXCH", "환종", 100);
            _flex거래처그룹2L.SetCol("STA_SO", "수주상태", 60);
            _flex거래처그룹2L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex거래처그룹2L.SetCol("RT_EXCH", "환율", 100);
            _flex거래처그룹2L.SetCol("NM_SO", "수주형태", 100);
            _flex거래처그룹2L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex거래처그룹2L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex거래처그룹2L.SetCol("FG_BILL", "결재방법", 100);

            _flex거래처그룹2L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex거래처그룹2L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex거래처그룹2L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex거래처그룹2L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex거래처그룹2L.SetCol("NO_EMP", "수주담당자", 100);
            _flex거래처그룹2L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex거래처그룹2L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex거래처그룹2L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex거래처그룹2L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex거래처그룹2L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처그룹2L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹2L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처그룹2L.SetCol("CD_SL", "창고코드", 100);
            _flex거래처그룹2L.SetCol("NM_SL", "창고명", 100);

            _flex거래처그룹2L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex거래처그룹2L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex거래처그룹2L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex거래처그룹2L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex거래처그룹2L.SetCol("GRP_MFG", "제품군코드", false);
            _flex거래처그룹2L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex거래처그룹2L.SetCol("MAT_ITEM", "재질", false);
            _flex거래처그룹2L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex거래처그룹2L.SettingVersion = "1.0.0.1";
            _flex거래처그룹2L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex거래처그룹2L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGridVAT구분H

        private void InitGridVAT구분H()
        {
            _flexVAT구분H.BeginSetting(1, 1, false);
            _flexVAT구분H.SetDummyColumn("S");
            _flexVAT구분H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexVAT구분H.SetCol("TP_VAT", "VAT코드", 60);
            _flexVAT구분H.SetCol("NM_VAT", "VAT구분", 100);
            _flexVAT구분H.SettingVersion = "1.0.0.3";
            _flexVAT구분H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexVAT구분H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridVAT구분L

        private void InitGridVAT구분L()
        {
            _flexVAT구분L.BeginSetting(1, 1, false);
            _flexVAT구분L.SetCol("NM_PLANT", "공장", 120);
            _flexVAT구분L.SetCol("NO_SO", "수주번호", 120);
            _flexVAT구분L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexVAT구분L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexVAT구분L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexVAT구분L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexVAT구분L.SetCol("LN_PARTNER", "거래처명", 120);
            _flexVAT구분L.SetCol("CD_ITEM", "품목코드", 100);
            _flexVAT구분L.SetCol("NM_ITEM", "품목명", 120);
            _flexVAT구분L.SetCol("STND_ITEM", "규격", 80);
            _flexVAT구분L.SetCol("UNIT_SO", "단위", 80);
            _flexVAT구분L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexVAT구분L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexVAT구분L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexVAT구분L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexVAT구분L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexVAT구분L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexVAT구분L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexVAT구분L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexVAT구분L.SetCol("CD_EXCH", "환종", 100);
            _flexVAT구분L.SetCol("STA_SO", "수주상태", 60);
            _flexVAT구분L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flexVAT구분L.SetCol("RT_EXCH", "환율", 100);
            _flexVAT구분L.SetCol("NM_SO", "수주형태", 100);
            _flexVAT구분L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flexVAT구분L.SetCol("FG_TAXP", "계산서처리", 100);
            _flexVAT구분L.SetCol("FG_BILL", "결재방법", 100);

            _flexVAT구분L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexVAT구분L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexVAT구분L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexVAT구분L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexVAT구분L.SetCol("NO_EMP", "수주담당자", 100);
            _flexVAT구분L.SetCol("NM_KOR", "수주담당자명", 120);

            _flexVAT구분L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexVAT구분L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexVAT구분L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flexVAT구분L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexVAT구분L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexVAT구분L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexVAT구분L.SetCol("CD_SL", "창고코드", 100);
            _flexVAT구분L.SetCol("NM_SL", "창고명", 100);

            _flexVAT구분L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flexVAT구분L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexVAT구분L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexVAT구분L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexVAT구분L.SetCol("GRP_MFG", "제품군코드", false);
            _flexVAT구분L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexVAT구분L.SetCol("MAT_ITEM", "재질", false);
            _flexVAT구분L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flexVAT구분L.SettingVersion = "1.0.0.0";
            _flexVAT구분L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexVAT구분L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        }

        #endregion

        #region -> InitGrid품목군H
        private void InitGrid품목군H()
        {
            _flex품목군H.BeginSetting(1, 1, false);
            _flex품목군H.SetDummyColumn("S");
            _flex품목군H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex품목군H.SetCol("GRP_ITEM", "품목군", 100);
            _flex품목군H.SetCol("NM_ITEMGRP", "품목군명", 120);
            _flex품목군H.SettingVersion = "1.0.1.1";
            _flex품목군H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex품목군H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        } 
        #endregion

        #region -> InitGrid품목군L
        private void InitGrid품목군L()
        {
            _flex품목군L.BeginSetting(1, 1, false);
            _flex품목군L.SetCol("NM_PLANT", "공장", 120);
            _flex품목군L.SetCol("NO_SO", "수주번호", 120);
            _flex품목군L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex품목군L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex품목군L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex품목군L.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex품목군L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex품목군L.SetCol("CD_ITEM", "품목코드", 100);
            _flex품목군L.SetCol("NM_ITEM", "품목명", 120);
            _flex품목군L.SetCol("STND_ITEM", "규격", 80);
            _flex품목군L.SetCol("UNIT_SO", "단위", 80);
            _flex품목군L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목군L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목군L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목군L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목군L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목군L.SetCol("UM_SO", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex품목군L.SetCol("AM_SO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex품목군L.SetCol("AM_WONAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex품목군L.SetCol("CD_EXCH", "환종", 100);
            _flex품목군L.SetCol("STA_SO", "수주상태", 60);
            _flex품목군L.SetCol("FG_TRANSPORT", "운송방법", 100);

            _flex품목군L.SetCol("RT_EXCH", "환율", 100);
            _flex품목군L.SetCol("NM_SO", "수주형태", 100);
            _flex품목군L.SetCol("NO_PROJECT", "프로젝트코드", 100);

            _flex품목군L.SetCol("FG_TAXP", "계산서처리", 100);
            _flex품목군L.SetCol("FG_BILL", "결재방법", 100);

            _flex품목군L.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flex품목군L.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flex품목군L.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flex품목군L.SetCol("GI_NAME", "납품처명", 120, 160);
            _flex품목군L.SetCol("NO_EMP", "수주담당자", 100);
            _flex품목군L.SetCol("NM_KOR", "수주담당자명", 120);

            _flex품목군L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex품목군L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex품목군L.SetCol("NM_EMP_KOR", "영업담당자", 100);

            _flex품목군L.SetCol("UMVAT_SO", "단가(부가세포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex품목군L.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex품목군L.SetCol("AM_WONAMT_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex품목군L.SetCol("CD_SL", "창고코드", 100);
            _flex품목군L.SetCol("NM_SL", "창고명", 100);

            _flex품목군L.SetCol("DC_RMK1", "헤더비고1", 100);
            _flex품목군L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex품목군L.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flex품목군L.SetCol("NM_ITEMGRP", "품목군명", false);
            _flex품목군L.SetCol("GRP_MFG", "제품군코드", false);
            _flex품목군L.SetCol("NM_GRP_MFG", "제품군명", false);
            _flex품목군L.SetCol("MAT_ITEM", "재질", false);
            _flex품목군L.SetCol("STND_DETAIL_ITEM", "세부규격", false);

            _flex품목군L.SettingVersion = "1.0.0.1";
            _flex품목군L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex품목군L.SetExceptSumCol("UM_SO", "UMVAT_SO");
        } 
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            dtp수주일자.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            dtp수주일자.EndDateToString = MainFrameInterface.GetStringToday;
            dtp납기일자.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            dtp납기일자.EndDateToString = MainFrameInterface.GetStringToday;

            // 2013.03.19 : D20130319069 null reference 오류처리함.
            DataTable dtTemp0 = MA.GetCode("MA_PLANT", false);
            DataTable dtTemp1 = MA.GetCode("SA_B000016", true);
            DataTable dtTemp2 = MA.GetCode("MA_B000005", false);
            DataTable dtTemp3 = MA.GetCode("TR_IM00008", true);
            DataTable dtTemp4 = MA.GetCode("SA_B000017", true);
            DataTable dtTemp5 = MA.GetCode("SA_B000002", true);
            DataTable dtTemp6 = MA.GetCode("MA_B000065", true);
            DataTable dtTemp7 = MA.GetCode("MA_B000067", true);
            DataTable dtTemp8 = MA.GetCode("SA_B000038", true);
            DataTable dtTemp9 = MA.GetCodeUser(new string[] { "", "N", "Y" }, new string[] { "", "미출력", "출력" });

            cbo공장.DataSource = dtTemp0;
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            _flexNOL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexPARTL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexITEML.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexDTL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexSOL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexGRPL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flexPROJECTL.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flex납품처L.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");
            _flex품목군L.SetDataMap("STA_SO", dtTemp1, "CODE", "NAME");

            _flexNOH.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexPARTL.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexITEML.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexDTL.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexSOL.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexGRPL.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flexPROJECTL.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flex납품처L.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");
            _flex품목군L.SetDataMap("CD_EXCH", dtTemp2, "CODE", "NAME");

            _flexNOH.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexPARTL.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexITEML.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexDTL.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexSOL.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexGRPL.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flexPROJECTL.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flex납품처L.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");
            _flex품목군L.SetDataMap("FG_TRANSPORT", dtTemp3, "CODE", "NAME");

            _flexNOH.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexPARTL.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexITEML.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexDTL.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexSOL.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexGRPL.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flexPROJECTL.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flex납품처L.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");
            _flex품목군L.SetDataMap("FG_TAXP", dtTemp4, "CODE", "NAME");

            _flexNOH.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexPARTL.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexITEML.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexDTL.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexSOL.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexGRPL.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flexPROJECTL.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flex납품처L.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");
            _flex품목군L.SetDataMap("FG_BILL", dtTemp5, "CODE", "NAME");

            cbo거래처그룹.DataSource = dtTemp6;
            cbo거래처그룹.DisplayMember = "NAME";
            cbo거래처그룹.ValueMember = "CODE";

            cbo거래처그룹2.DataSource = dtTemp7;
            cbo거래처그룹2.DisplayMember = "NAME";
            cbo거래처그룹2.ValueMember = "CODE";

            cbo진행상태.DataSource = dtTemp8;
            cbo진행상태.DisplayMember = "NAME";
            cbo진행상태.ValueMember = "CODE";

            cbo출력여부.DataSource = dtTemp9;
            cbo출력여부.DisplayMember = "NAME";
            cbo출력여부.ValueMember = "CODE";

            cbo공장.SelectedValue = LoginInfo.CdPlant;

            bp품목.QueryBefore += new BpQueryHandler(Control_QueryBefore);
            bpc수주상태.QueryBefore += new BpQueryHandler(Control_QueryBefore);

            switch (ServerKey)
            {
                case "DZSQL":
                case "SQL_":
                    btn일괄출력.Visible = true;
                    bpPnl출력여부.Visible = true;
                    break;

                case "KOREAF":
                    btn일괄출력.Visible = true;
                    break;

                case "SINJINSM":
                    bpPnl출력여부.Visible = true;
                    _flexNOL.SetDataMap("CD_USERDEF4", dtTemp9, "CODE", "NAME");
                    _flexPARTL.SetDataMap("CD_USERDEF4", dtTemp9, "CODE", "NAME");
                    break;

                default:
                    break;
            }

            DataTable dt_DT_GUBUN = new DataTable();
            dt_DT_GUBUN.Columns.Add("CODE", typeof(string));
            dt_DT_GUBUN.Columns.Add("NAME", typeof(string));
            dt_DT_GUBUN.Rows.Add("DUE", "납기요구일");
            dt_DT_GUBUN.Rows.Add("REQ", "출하예정일");
            dt_DT_GUBUN.AcceptChanges();

            cbo_DT_GUBUN.DataSource = dt_DT_GUBUN;
            cbo_DT_GUBUN.DisplayMember = "NAME";
            cbo_DT_GUBUN.ValueMember = "CODE";

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
        }

        #endregion

        #endregion

        #region ♥ 메인버튼 클릭

        #region -> Check

        public bool Check()
        {
            if (chkBox_so.Checked == false && chkBox_due.Checked == false)
            {
                ShowMessage(" 수주기간 혹은 납기일자 둘중하나이상 반드시 선택해야합니다.");
                chkBox_so.Focus();
                return false;
            }

            if (chkBox_so.Checked)
            {
                if (!Chk수주일자) return false;
            }

            if (chkBox_due.Checked)
            {
                if (!Chk납기일자) return false;
            }

            return true;
        }

        #endregion

        #region -> 조회버튼클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                if (!Check()) return;

                string STR_CD_PLANT = string.Empty;

                //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별
                string tab_splt = tabControlExt1.SelectedIndex.ToString();

                if (cbo공장.SelectedValue != null)
                    STR_CD_PLANT = D.GetString(cbo공장.SelectedValue);

                String DT_SOSchk = string.Empty;//수주일 체크
                if (chkBox_so.Checked == false)
                {
                    DT_SOSchk = "N";
                }
                else
                {
                    DT_SOSchk = "Y";
                }

                String DT_pymnt_chk = string.Empty;//납기일 체크
                if (chkBox_due.Checked == false)
                {
                    DT_pymnt_chk = "N";
                }
                else
                {
                    DT_pymnt_chk = "Y";
                }

                object[] obj = new object[]
                                         { Global.MainFrame.LoginInfo.CompanyCode,
                                          dtp수주일자.StartDateToString,
                                          dtp수주일자.EndDateToString,
                                          STR_CD_PLANT,
                                          bp거래처.CodeValue,
                                          bp영업그룹.QueryWhereIn_Pipe,
                                          bp담당자.CodeValue,
                                          bp수주형태.QueryWhereIn_Pipe,
                                          bpc수주상태.QueryWhereIn_Pipe,
                                          tab_splt,
                                          DT_SOSchk,
                                          DT_pymnt_chk,
                                          dtp납기일자.StartDateToString,
                                          dtp납기일자.EndDateToString,
                                          bp영업담당자.CodeValue,
                                          bp품목.QueryWhereIn_Pipe,
                                          bp영업조직.QueryWhereIn_Pipe,
                                          D.GetString(cbo거래처그룹.SelectedValue),
                                          D.GetString(cbo거래처그룹2.SelectedValue),
                                          bp대분류.CodeValue,
                                          bp중분류.CodeValue,
                                          bp소분류.CodeValue,
                                          Global.MainFrame.LoginInfo.EmployeeNo,
                                          cbo_DT_GUBUN.SelectedValue,
                                          D.GetString(cbo진행상태.SelectedValue),
                                          D.GetString(cbo출력여부.SelectedValue)

                                         };

                DataTable dt = _biz.Search(obj);

                if (tab_splt == "0")
                    _flexNOH.Binding = dt;
                else if (tab_splt == "1")
                    _flexPARTH.Binding = dt;
                else if (tab_splt == "2")
                    _flexITEMH.Binding = dt;
                else if (tab_splt == "3")
                    _flexDTH.Binding = dt;
                else if (tab_splt == "4")
                    _flexSOH.Binding = dt;
                else if (tab_splt == "5")
                    _flexGRPH.Binding = dt;
                else if (tab_splt == "6")
                    _flexORGH.Binding = dt;
                else if (tab_splt == "7")
                    _flexPROJECTH.Binding = dt;
                else if (tab_splt == "8")
                    _flex납품처H.Binding = dt;
                else if (tab_splt == "9")
                    _flex납기일자H.Binding = dt;
                else if (tab_splt == "10")
                    _flex영업담당자H.Binding = dt;
                else if (tab_splt == "11")
                    _flex제품군H.Binding = dt;
                else if (tab_splt == "12")
                    _flex거래처그룹H.Binding = dt;
                else if (tab_splt == "13")
                    _flex거래처그룹2H.Binding = dt;
                else if (tab_splt == "14")
                    _flexVAT구분H.Binding = dt;
                else if (tab_splt == "15")
                    _flex품목군H.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    btn수주서출력.Enabled = btn일괄출력.Enabled = ToolBarPrintButtonEnabled = false;
                    return;
                }

                btn수주서출력.Enabled = btn일괄출력.Enabled = ToolBarPrintButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼클릭
        // ******************
        //  인쇄시 XML 작업 
        //  2007-12-18
        //  장기주         
        // ******************
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {     
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL" 
                    || Global.MainFrame.ServerKey == "SQL_" 
                    || Global.MainFrame.ServerKey == "SINJINSM")
                {
                    if (this.tabControlExt1.SelectedIndex == 0 || this.tabControlExt1.SelectedIndex == 1)
                    {
                        Z_SINJINSM_PRINT();
                        return;
                    }
                }

                string ID_menu = string.Empty;//메뉴 아이디. 레포트번호
                string NM_menu = string.Empty;//메뉴명

                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    ID_menu = "R_SA_SOSCH_0";
                    NM_menu = "수주현황-수주번호별";
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    ID_menu = "R_SA_SOSCH_1";
                    NM_menu = "수주현황-거래처별";
                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;
                }
                else if (this.tabControlExt1.SelectedIndex == 2)
                {
                    ID_menu = "R_SA_SOSCH_2";
                    NM_menu = "수주현황-품목코드별";
                    _flexH = _flexITEMH;
                    _flexD = _flexITEML;
                }
                else if (this.tabControlExt1.SelectedIndex == 3)
                {
                    ID_menu = "R_SA_SOSCH_3";
                    NM_menu = "수주현황-수주일자별";
                    _flexH = _flexDTH;
                    _flexD = _flexDTL;
                }
                else if (this.tabControlExt1.SelectedIndex == 4)
                {
                    ID_menu = "R_SA_SOSCH_4";
                    NM_menu = "수주현황-수주형태별";
                    _flexH = _flexSOH;
                    _flexD = _flexSOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 5)
                {
                    ID_menu = "R_SA_SOSCH_5";
                    NM_menu = "수주현황-영업그룹별";
                    _flexH = _flexGRPH;
                    _flexD = _flexGRPL;
                }
                else if (this.tabControlExt1.SelectedIndex == 6)
                {
                    ID_menu = "R_SA_SOSCH_7";
                    NM_menu = "수주현황-영업조직별";
                    _flexH = _flexORGH;
                    _flexD = _flexORGL;
                }
                else if (this.tabControlExt1.SelectedIndex == 7)
                {
                    ID_menu = "R_SA_SOSCH_6";
                    NM_menu = "수주현황-프로젝트별";
                    _flexH = _flexPROJECTH;
                    _flexD = _flexPROJECTL;
                }
                else if (this.tabControlExt1.SelectedIndex == 8)
                {
                    ID_menu = "R_SA_SOSCH_8";
                    NM_menu = "수주현황-납품처별";
                    _flexH = _flex납품처H;
                    _flexD = _flex납품처L;
                }
                else if (this.tabControlExt1.SelectedIndex == 9)
                {
                    ID_menu = "R_SA_SOSCH_9";
                    NM_menu = "수주현황-납기일자별";
                    _flexH = _flex납기일자H;
                    _flexD = _flex납기일자L;
                }
                else if (this.tabControlExt1.SelectedIndex == 10)
                {
                    ID_menu = "R_SA_SOSCH_10";
                    NM_menu = "수주현황-영업담당자별";
                    _flexH = _flex영업담당자H;
                    _flexD = _flex영업담당자L;
                }
                else if (this.tabControlExt1.SelectedIndex == 11)
                {
                    ID_menu = "R_SA_SOSCH_11";
                    NM_menu = "수주현황-제품군별";
                    _flexH = _flex제품군H;
                    _flexD = _flex제품군L;
                }
                else if (this.tabControlExt1.SelectedIndex == 12)
                {
                    ID_menu = "R_SA_SOSCH_12";
                    NM_menu = "수주현황-거래처그룹별";
                    _flexH = _flex거래처그룹H;
                    _flexD = _flex거래처그룹L;
                }
                else if (this.tabControlExt1.SelectedIndex == 13)
                {
                    ID_menu = "R_SA_SOSCH_13";
                    NM_menu = "수주현황-거래처그룹2별";
                    _flexH = _flex거래처그룹2H;
                    _flexD = _flex거래처그룹2L;
                }
                else if (this.tabControlExt1.SelectedIndex == 14)
                {
                    ID_menu = "R_SA_SOSCH_14";
                    NM_menu = "수주현황-VAT구분별";
                    _flexH = _flexVAT구분H;
                    _flexD = _flexVAT구분L;
                }
                else if (this.tabControlExt1.SelectedIndex == 15)
                {
                    ID_menu = "R_SA_SOSCH_15";
                    NM_menu = "수주현황-품목군별";
                    _flexH = _flex품목군H;
                    _flexD = _flex품목군L;
                }

                if (!_flexH.HasNormalRow)
                    return;

                string No_PK_Multi = string.Empty; string No_PK_Multi_SO = string.Empty;

                DataRow[] ldt_Report = _flexH.DataTable.Select("S = 'Y'");

                if (ldt_Report == null || ldt_Report.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    switch (NM_menu)
                    {
                        case "수주현황-수주번호별":

                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "NO_SO"].ToString() + "|";
                            }
                            break;
                        case "수주현황-거래처별":
                            DataRow[] drH = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                            foreach (DataRow rowH in drH)
                            {
                                No_PK_Multi += rowH["CD_PARTNER"].ToString() + "|";

                                DataRow[] drL = _flexD.DataTable.Select("CD_PARTNER = '" + rowH["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                                foreach (DataRow rowL in drL)
                                {
                                    No_PK_Multi_SO += rowL["NO_SO"].ToString() + "|";
                                }
                            }
                            break;
                        case "수주현황-품목코드별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "CD_ITEM"].ToString() + "|";
                            }
                            break;
                        case "수주현황-수주일자별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "DT_SO"].ToString() + "|";
                            }
                            break;
                        case "수주현황-수주형태별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "TP_SO"].ToString() + "|";
                            }
                            break;
                        case "수주현황-영업그룹별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "CD_SALEGRP"].ToString() + "|";
                            }
                            break;
                        case "수주현황-영업조직별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "CD_SALEORG"].ToString() + "|";
                            }
                            break;
                        case "수주현황-프로젝트별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "NO_PROJECT"]) + "|";
                            }
                            break;
                        case "수주현황-납품처별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "GI_PARTNER"]) + "|";
                            }
                            break;
                        case "수주현황-납기일자별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "DT_DUEDATE"].ToString() + "|";
                            }
                            break;
                        case "수주현황-영업담당자별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "CD_EMP_SALE"]) + "|";
                            }
                            break;
                        case "수주현황-제품군별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "GRP_MFG"]) + "|";
                            }
                            break;
                        case "수주현황-거래처그룹별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "CD_PARTNER_GRP"]) + "|";
                            }
                            break;
                        case "수주현황-거래처그룹2별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "CD_PARTNER_GRP_2"]) + "|";
                            }
                            break;
                        case "수주현황-VAT구분별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "TP_VAT"]) + "|";
                            }
                            break;
                        case "수주현황-품목군별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += D.GetString(_flexH[i, "GRP_ITEM"]) + "|";
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (!_flexH.HasNormalRow && _flexD.HasNormalRow)
                    return;

                ReportHelper rptHelper = new ReportHelper(ID_menu, NM_menu);

                rptHelper.가로출력();

                string 공장 = string.Empty;

                string 탭구분 = tabControlExt1.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                String DT_SO_chk = string.Empty;
                if (chkBox_so.Checked == false)
                {
                    DT_SO_chk = "N";
                }
                else
                {
                    DT_SO_chk = "Y";
                }

                String DT_pymnt_chk = string.Empty;
                if (chkBox_due.Checked == false)
                {
                    DT_pymnt_chk = "N";
                }
                else
                {
                    DT_pymnt_chk = "Y";
                }

                try
                {
                    P_SA_SOSCH1_PRINT print = new P_SA_SOSCH1_PRINT(ID_menu, NM_menu, true);

                    print.수주일자fr = dtp수주일자.StartDateToString;
                    print.수주일자to = dtp수주일자.EndDateToString;
                    print.공장 = 공장;
                    print.bp거래처 = bp거래처.CodeValue;
                    print.bp영업그룹 = bp영업그룹.QueryWhereIn_Pipe;
                    print.bp담당자 = bp담당자.CodeValue;
                    print.bp수주형태 = bp수주형태.QueryWhereIn_Pipe;
                    print.수주상태 = bpc수주상태.QueryWhereIn_Pipe;
                    print.멀티키 = No_PK_Multi;
                    print.탭 = 탭구분;
                    print.멀티수주번호 = No_PK_Multi_SO;
                    print.수주기간chk = DT_SO_chk;
                    print.납기기간chk = DT_pymnt_chk;
                    print.납기일자FROM = dtp납기일자.StartDateToString;
                    print.납기일자TO = dtp납기일자.EndDateToString;
                    print.영업담당자 = bp영업담당자.CodeValue;
                    print.품목멀티 = bp품목.QueryWhereIn_Pipe;
                    print.영업조직멀티 = bp영업조직.QueryWhereIn_Pipe;
                    print.거래처그룹 = D.GetString(cbo거래처그룹.SelectedValue);
                    print.거래처그룹2 = D.GetString(cbo거래처그룹2.SelectedValue);
                    print.대분류 = bp대분류.CodeValue;
                    print.중분류 = bp중분류.CodeValue;
                    print.소분류 = bp소분류.CodeValue;
                    print.날짜구분 = D.GetString(cbo_DT_GUBUN.SelectedValue);
                    print.진행상태 = D.GetString(cbo진행상태.SelectedValue);

                    //헤더부분값 넘기는곳
                    if (DT_SO_chk == "Y")
                    {
                        print.n_dtSofr = dtp수주일자.StartDateToString;
                        print.n_dtSoto = dtp수주일자.EndDateToString;
                    }
                    else
                    {
                        print.n_dtSofr = dtp납기일자.StartDateToString;
                        print.n_dtSoto = dtp납기일자.EndDateToString;
                    }
                    print.n_Plant = cbo공장.Text;
                    print.n_bpPartner = bp거래처.CodeName;
                    print.n_bpSaleGRP = bp영업그룹.QueryWhereIn_Pipe;
                    print.n_bpSaleGRPNAME = bp영업그룹.QueryWhereIn_PipeDisplayMember;
                    print.n_Empno = bp담당자.CodeName;
                    print.n_SoState = bpc수주상태.QueryWhereIn_PipeDisplayMember;
                    print.n_SoType = bp수주형태.QueryWhereIn_Pipe;
                    print.n_PartnerGrp = cbo거래처그룹.Text;
                    print.n_PartnerGrp2 = cbo거래처그룹2.Text;
                    print.n_Cls_L = bp대분류.CodeName;
                    print.n_Cls_M = bp중분류.CodeName;
                    print.n_Cls_S = bp소분류.CodeName;

                    print.ShowPrintDialog();
                }
                catch (Exception Ex)
                {
                    this.MsgEnd(Ex);
                }
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }
        }

        #endregion

        #endregion

        #region ♥ 그리드 이벤트

        #region -> _flex_AfterRowChange

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                DataTable dt = null;
                string Key = string.Empty;
                string Filter = string.Empty;
                string 공장 = string.Empty;
                string tab_splt = tabControlExt1.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                String DT_SO_chk;
                if (chkBox_so.Checked == false)
                {
                    DT_SO_chk = "N";
                }
                else
                {
                    DT_SO_chk = "Y";
                }

                String DT_Duedte_chk;
                if (chkBox_due.Checked == false)
                {
                    DT_Duedte_chk = "N";
                }
                else
                {
                    DT_Duedte_chk = "Y";
                }

                object[] obj = new object[] { null };

                switch (flex.Name)
                {
                    case "_flexNOH":
                        Key = D.GetString(_flexNOH[e.NewRange.r1, "NO_SO"]);
                        Filter = "NO_SO = '" + Key + "'";

                        if (_flexNOH.DetailQueryNeed)
                        {
                            obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                            };

                            dt = _biz.SearchDetail(obj);
                        }

                        _flexNOL.BindingAdd(dt, Filter);
                        _flexNOH.DetailQueryNeed = false;
                        break;
                    case "_flexPARTH":
                        Key = D.GetString(_flexPARTH[e.NewRange.r1, "CD_PARTNER"]);
                        Filter = "CD_PARTNER = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexPARTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexPARTL.BindingAdd(dt, Filter);
                        _flexPARTH.DetailQueryNeed = false;
                        break;
                    case "_flexITEMH":
                        Key = D.GetString(_flexITEMH[e.NewRange.r1, "CD_ITEM"]);
                        Filter = "CD_ITEM = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexITEMH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexITEML.BindingAdd(dt, Filter);
                        _flexITEMH.DetailQueryNeed = false;
                        break;
                    case "_flexDTH":
                        Key = D.GetString(_flexDTH[e.NewRange.r1, "DT_SO"]);
                        Filter = "DT_SO = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexDTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexDTL.BindingAdd(dt, Filter);
                        _flexDTH.DetailQueryNeed = false;
                        break;
                    case "_flexSOH":
                        Key = D.GetString(_flexSOH[e.NewRange.r1, "TP_SO"]);
                        Filter = "TP_SO = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexSOH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexSOL.BindingAdd(dt, Filter);
                        _flexSOH.DetailQueryNeed = false;
                        break;
                    case "_flexGRPH":
                        Key = D.GetString(_flexGRPH[e.NewRange.r1, "CD_SALEGRP"]);
                        Filter = "CD_SALEGRP = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexGRPH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexGRPL.BindingAdd(dt, Filter);
                        _flexGRPH.DetailQueryNeed = false;
                        break;

                    case "_flexORGH":
                        Key = D.GetString(_flexORGH[e.NewRange.r1, "CD_SALEORG"]);
                        Filter = "CD_SALEORG = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexORGH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexORGL.BindingAdd(dt, Filter);
                        _flexORGH.DetailQueryNeed = false;
                        break;
                    case "_flexPROJECTH":
                        Key = D.GetString(_flexPROJECTH[e.NewRange.r1, "NO_PROJECT"]);
                        Filter = "NO_PROJECT = '" + Key + "'";
                        if (_flexPROJECTH[e.NewRange.r1, "NO_PROJECT"] == DBNull.Value)
                            Filter += " OR NO_PROJECT IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexPROJECTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexPROJECTL.BindingAdd(dt, Filter);
                        _flexPROJECTH.DetailQueryNeed = false;
                        break;
                    case "_flex납품처H":
                        Key = D.GetString(_flex납품처H[e.NewRange.r1, "GI_PARTNER"]);
                        Filter = "GI_PARTNER = '" + Key + "'";
                        if (_flex납품처H[e.NewRange.r1, "GI_PARTNER"] == DBNull.Value)
                            Filter += " OR GI_PARTNER IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex납품처H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex납품처L.BindingAdd(dt, Filter);
                        _flex납품처H.DetailQueryNeed = false;
                        break;
                    case "_flex납기일자H":
                        Key = D.GetString(_flex납기일자H[e.NewRange.r1, "DT_DUEDATE"]);
                        Filter = "DT_DUEDATE = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex납기일자H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex납기일자L.BindingAdd(dt, Filter);
                        _flex납기일자H.DetailQueryNeed = false;
                        break;
                    case "_flex영업담당자H":
                        Key = D.GetString(_flex영업담당자H[e.NewRange.r1, "CD_EMP_SALE"]);
                        Filter = "CD_EMP_SALE = '" + Key + "'";
                        if (_flex영업담당자H[e.NewRange.r1, "CD_EMP_SALE"] == DBNull.Value)
                            Filter += " OR CD_EMP_SALE IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex영업담당자H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex영업담당자L.BindingAdd(dt, Filter);
                        _flex영업담당자H.DetailQueryNeed = false;
                        break;
                    case "_flex제품군H":
                        Key = D.GetString(_flex제품군H[e.NewRange.r1, "GRP_MFG"]);
                        Filter = "GRP_MFG = '" + Key + "'";
                        if (_flex제품군H[e.NewRange.r1, "GRP_MFG"] == DBNull.Value)
                            Filter += " OR GRP_MFG IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex제품군H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex제품군L.BindingAdd(dt, Filter);
                        _flex제품군H.DetailQueryNeed = false;
                        break;
                    case "_flex거래처그룹H":
                        Key = D.GetString(_flex거래처그룹H[e.NewRange.r1, "CD_PARTNER_GRP"]);
                        Filter = "CD_PARTNER_GRP = '" + Key + "'";
                        if (_flex거래처그룹H[e.NewRange.r1, "CD_PARTNER_GRP"] == DBNull.Value)
                            Filter += " OR CD_PARTNER_GRP IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex거래처그룹H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex거래처그룹L.BindingAdd(dt, Filter);
                        _flex거래처그룹H.DetailQueryNeed = false;
                        break;
                    case "_flex거래처그룹2H":
                        Key = D.GetString(_flex거래처그룹2H[e.NewRange.r1, "CD_PARTNER_GRP_2"]);
                        Filter = "CD_PARTNER_GRP_2 = '" + Key + "'";
                        if (_flex거래처그룹2H[e.NewRange.r1, "CD_PARTNER_GRP_2"] == DBNull.Value)
                            Filter += " OR CD_PARTNER_GRP_2 IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex거래처그룹2H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex거래처그룹2L.BindingAdd(dt, Filter);
                        _flex거래처그룹2H.DetailQueryNeed = false;
                        break;

                    case "_flexVAT구분H":
                        Key = D.GetString(_flexVAT구분H[e.NewRange.r1, "TP_VAT"]);
                        Filter = "TP_VAT = '" + Key + "'";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flexVAT구분H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flexVAT구분L.BindingAdd(dt, Filter);
                        _flexVAT구분H.DetailQueryNeed = false;
                        break;

                    case "_flex품목군H":
                        Key = D.GetString(_flex품목군H[e.NewRange.r1, "GRP_ITEM"]);
                        Filter = "GRP_ITEM = '" + Key + "'";
                        if (_flex품목군H[e.NewRange.r1, "GRP_ITEM"] == DBNull.Value)
                            Filter += " OR GRP_ITEM IS NULL";

                        obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            Key,
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            공장,
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            tab_splt,
                                            DT_SO_chk,
                                            DT_Duedte_chk,
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                        };

                        if (_flex품목군H.DetailQueryNeed)
                            dt = _biz.SearchDetail(obj);

                        _flex품목군L.BindingAdd(dt, Filter);
                        _flex품목군H.DetailQueryNeed = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexNOH_HelpClick

        void _flexNOH_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flexNOH.HasNormalRow) return;
                if (D.GetString(_flexNOH["ATTACHMENT"]) == string.Empty) return;

                if (_flexNOH.Cols[_flexNOH.Col].Name == "ATTACHMENT")
                {
                    string cdFile = D.GetString(_flexNOH["NO_SO"]) + "_" + D.GetString(_flexNOH["NO_HST"]);
                    master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, cdFile);
                    m_dlg.UseGrant = false; //UseGrant를 false로 지정하면 첨부파일 Down만 가능함
                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 2008.06.17 EXCEL 내려받는 기능추가
        public void Menu_Click(object sender, EventArgs e)
        {
            /*
             * 엑셀명 || 쉬트명 암만 띄어쓰기 해봐도~ ㅋㅋ 막아놨음~ 2가지 문제가 생김 
             * 1. ExcelUtil.cs 의 하단부분에 엑셀창을 띄워주는 역할을 하는 부분에서 띄어쓰기된 엑셀 파일을 못 찾고
             *    띄어쓰기를 구분으로 앞뒤로 각각 따로 엑셀 파일명으로 찾을라하는 바보같은 일이 있었음.
             * 2. 나중에 실재 Excel 파일에서 VBA 언어로 엑셀의 시트 명을 가져오는데 띄어쓰기 하면 또 바보같이 못 찾음~ 
             *    꼭 띄어 쓰기 하고싶음~ 출력한다음 다른이름으로 저장해서 사용하든가~ 막 요럼~
             * 3. fileName 이나 SheetName 을 한글로 옮기지 말것~ 우리 엑셀 파일 베포해줄려고 할때 
             *    DeployManager 가 베포를 못 해준다. 한글이 깨져버린다.~ 2008.11.28
             */

            /* 가급적이면 엑셀파일명과 시트이름을 같게 하는게 좋을걸... 어허 말 듣는것도 좋을걸 */
            string fileName = "PROFORMA INVOICE";      //엑셀파일명
            string sheetName = "PROFORMA INVOICE";      //시트이름

            //프리폼 데이터 (공통으로 들어가는 부분의 TEXT(data)출력)
            string textData = "";

            object[] obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                            D.GetString(_flexNOH["NO_SO"]),
                                            dtp수주일자.StartDateToString,
                                            dtp수주일자.EndDateToString,
                                            D.GetString(cbo공장.SelectedValue),
                                            bp거래처.CodeValue,
                                            bp영업그룹.QueryWhereIn_Pipe,
                                            bp담당자.CodeValue,
                                            bp수주형태.QueryWhereIn_Pipe,
                                            bpc수주상태.QueryWhereIn_Pipe,
                                            D.GetString(tabControlExt1.SelectedIndex),
                                            chkBox_so.Checked ? "Y" : "N",
                                            chkBox_due.Checked ? "Y" : "N",
                                            dtp납기일자.StartDateToString,
                                            dtp납기일자.EndDateToString,
                                            bp영업담당자.CodeValue,
                                            bp품목.QueryWhereIn_Pipe,
                                            bp영업조직.QueryWhereIn_Pipe,
                                            D.GetString(cbo거래처그룹.SelectedValue),
                                            D.GetString(cbo거래처그룹2.SelectedValue),
                                            bp대분류.CodeValue,
                                            bp중분류.CodeValue,
                                            bp소분류.CodeValue,
                                            D.GetString(cbo_DT_GUBUN.SelectedValue),
                                            D.GetString(cbo진행상태.SelectedValue),
                                            D.GetString(cbo출력여부.SelectedValue)
                            };

            DataTable dt = _biz.SearchDetail(obj);

            //그리드형테를 데이터 테이블형태로 변환
            //DataTable dt01 = GetDataTable(_flexNOH.DataTable);
            //활성화된 로우를 추출하여 데이터 테이블 형태로 띵겨준당...
            DataTable dt01 = _flexNOH.DataTable.Clone();
            dt01.Rows.Add(_flexNOH.DataView.ToTable().Rows[_flexNOH.Row - _flexNOH.Rows.Fixed].ItemArray);

            DataTable[] dts = new DataTable[2];
            dts[0] = dt01;
            dts[1] = dt;

            ExcelUtil excelUtil = new ExcelUtil();
            bool b = excelUtil.ExcelExport(fileName, sheetName, textData, dts);

            if (!b)
                return;
        }

        #region 엑셀을 여러건 띠워야하니까~ 루핑을 ... 음 하하하
        //이거 건별로 일괄 처리 해달라고 하면 체크된 수주 번호별로 헤더/라인 데이터 가져와서 각각 한번씩 엑셀 호출해줘야한다.
        //private DataTable GetDataTable(DataTable dt)
        //{
        //    for (int i = 0; i < _flexNOH.DataTable.Rows.Count; i++)
        //    {
        //        if (_flexNOH[i, "S"].ToString() == "Y")
        //        { 

        //        }
        //    }
        //}
        #endregion
        #endregion

        #region -> _flex_CheckHeaderClick

        void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;
                }

                FlexGrid _flex = sender as FlexGrid;

                if (!_flexH.HasNormalRow) return;

                _flexD.Redraw = false;

                bool istrue = false;


                if (_flex.Name == _flexH.Name)
                {
                    #region 상단그리드 체크시
                    if (_flexH.GetCellCheck(_flexH.Row, _flexH.Cols["S"].Index) == CheckEnum.Checked)
                        istrue = true;


                    for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                    {
                        _flexH.Row = i;

                        if (istrue)
                        {
                            _flexH.SetCellCheck(i, _flexH.Cols["S"].Index, CheckEnum.Checked);
                        }
                        else
                        {
                            _flexH.SetCellCheck(i, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                        }

                        for (int j = _flexD.Rows.Fixed; j < _flexD.Rows.Count; j++)
                        {
                            if (_flexH.GetCellCheck(_flexH.Row, _flexH.Cols["S"].Index) == CheckEnum.Checked)
                            {
                                _flexD.SetCellCheck(j, _flexD.Cols["S"].Index, CheckEnum.Checked);
                            }
                            else
                            {
                                _flexD.SetCellCheck(j, _flexD.Cols["S"].Index, CheckEnum.Unchecked);
                            }
                        }
                    }
                    #endregion
                }
                else if (_flex.Name == _flexD.Name)
                {
                    if (_flexD.GetCellCheck(_flexD.Row, _flexD.Cols["S"].Index) == CheckEnum.Checked)
                        istrue = true;

                    if (istrue)
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                    }
                    else
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }


                    //초기화
                    istrue = false;

                    //라인쪽이 체크가 되어있는게 있는지 없는지 체크
                    for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                    {
                        if (_flexD.GetCellCheck(_flexD.Row, _flexD.Cols["S"].Index) == CheckEnum.Checked)
                            istrue = true;
                    }

                    if (istrue)
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                    }
                    else
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
            }
        }

        #endregion

        #region -> _flex_ValidateEdit

        void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;
                }

                FlexGrid _flex = sender as FlexGrid;

                string OldValue = _flex.GetData(e.Row, e.Col).ToString();
                string NewValue = _flex.EditData;

                if (OldValue.ToUpper() == NewValue.ToUpper()) return;


                if (_flex.Name == _flexH.Name)
                {
                    _flexD.Redraw = false;
                    if (e.Checkbox == CheckEnum.Checked)
                    {
                        for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        {
                            _flexD.SetCellCheck(i, _flexD.Cols["S"].Index, CheckEnum.Checked);
                        }
                    }
                    else
                    {
                        for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        {
                            _flexD.SetCellCheck(i, _flexD.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                    }
                }
                else if (_flex.Name == _flexD.Name)
                {
                    #region 라인체크 시
                    //라인이 하나라도 체크되면 
                    if (e.Checkbox == CheckEnum.Checked)
                    {
                        _flexD.SetCellCheck(_flexD.Row, _flexD.Cols["S"].Index, CheckEnum.Checked);
                    }
                    else
                    {
                        _flexD.SetCellCheck(_flexD.Row, _flexD.Cols["S"].Index, CheckEnum.Unchecked);
                    }


                    bool istrue = false;

                    for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                    {
                        if (_flexD.GetCellCheck(i, _flexD.Cols["S"].Index) == CheckEnum.Checked)
                        {
                            istrue = true;
                            break;
                        }
                    }

                    if (istrue)
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                    }
                    else
                    {
                        _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
            }
        }

        #endregion

        #endregion

        #region ♥ 기타 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB1:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1:
                        if (this.cbo공장.SelectedValue.ToString() == "")
                        {
                            this.ShowMessage("공장을 먼저 선택하세요!");  //공장을 먼저 선택하세요!
                            cbo공장.Focus();
                            e.QueryCancel = true;
                            return;
                        }
                        e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB:
                        switch (e.ControlName)
                        {
                            case "bp대분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                                break;
                            case "bp중분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(bp대분류.CodeValue);
                                break;
                            case "bp소분류":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(bp중분류.CodeValue);
                                break;
                        }
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
                        switch (e.ControlName)
                        {
                            case "bpc수주상태":
                                e.HelpParam.P41_CD_FIELD1 = "SA_B000016";
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 수주서출력

        private void btn수주서출력_Click(object sender, EventArgs e)
        {

            try
            {
                OnToolBarPrintButtonClicked(sender, e);// 인쇄버튼으로 링크

                #region -> 수주서를 따로 타겠끔한 로직
                //따로 타게할 이유가 없어서 인쇄버튼으로 링크거는것으로 바꿈 
                //이렇게 한것이 틀리다면 주석만 푸시고 위에 링크거는 부분만 주석처리해주시면 됩니다.

                //string 멀티키 = string.Empty; 
                //string 메뉴ID = string.Empty; 
                //string 메뉴명 = string.Empty;

                //for(int i = _flexNOH.Rows.Fixed;i < _flexNOH.Rows.Count;i++)
                //{
                //    if(_flexNOH[i, "S"].ToString() == "Y")
                //        멀티키 += _flexNOH[i, "NO_SO"].ToString() + "|";
                //}

                //if(!_flexNOH.HasNormalRow && !_flexNOL.HasNormalRow)
                //    return;

                //string 공장 = string.Empty;
                //string 수주상태 = string.Empty;

                //if(cbo공장.SelectedValue != null)
                //    공장 = cbo공장.SelectedValue.ToString();

                //if(cbo수주상태.SelectedValue != null)
                //    수주상태 = cbo수주상태.SelectedValue.ToString();

                //    메뉴ID = "R_SA_SOSCH_0";
                //    메뉴명 = "수주현황-수주번호별";

                //    if (멀티키 == "")
                //        멀티키 = _flexNOH["NO_SO"].ToString() + "|";

                //이부분은 왜 불러왔는지 몰라서 일단은 주석처리해놓았는데 지워도 상관없을것 같음----여기부터
                //    //object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, dp수주일자FROM.Text, dp수주일자TO.Text,
                //    //                                공장, bp거래처.CodeValue, bp영업그룹.CodeValue, bp담당자.CodeValue,
                //    //                                bp수주형태.CodeValue, 수주상태, 멀티키};
                //    //DataTable dt = _biz.Print_type1(obj,"R_SA_SOSCH_001");
                //-----여기까지

                //    P_SA_SOSCH1_PRINT print = new P_SA_SOSCH1_PRINT("R_SA_SOSCH_0", "수주현황_수주번호별", true);

                //    print.수주일자fr = dp수주일자FROM.Text; 
                //    print.수주일자to = dp수주일자TO.Text;
                //    print.공장 = 공장; 
                //    print.bp거래처 = bp거래처.CodeValue;
                //    print.bp영업그룹 = bp영업그룹.CodeValue; 
                //    print.bp담당자 = bp담당자.CodeValue;
                //    print.bp수주형태 = bp수주형태.CodeValue;
                //    print.수주상태 = 수주상태;
                //    print.멀티키 = 멀티키;

                //    //헤더부분 넘기는곳
                //    if (chkBox_so.Checked == true)
                //    {
                //        print.n_dtSofr = dp수주일자FROM.Text;
                //        print.n_dtSoto = dp수주일자TO.Text;
                //    }
                //    else
                //    {
                //        print.n_dtSofr = dp납기일자FROM.Text;
                //        print.n_dtSoto = dp납기일자TO.Text;
                //    }
                //    print.n_Plant = cbo공장.Text;
                //    print.n_bpPartner = bp거래처.CodeName;
                //    print.n_bpSaleGRP = bp영업그룹.CodeName;
                //    print.n_Empno = bp담당자.CodeName;
                //    print.n_SoState = cbo수주상태.Text;
                //    print.n_SoType = bp수주형태.CodeName;

                //    print.ShowPrintDialog();
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 수주서출력, 일괄출력 버튼 활성화

        private void 수주서출력버튼활성화_Click(object sender, EventArgs e)
        {
            try
            {
                string 탭구분 = D.GetString(tabControlExt1.SelectedIndex);

                if (탭구분 == "0")
                {
                    btn수주서출력.Visible = true;

                    switch (ServerKey)
                    {
                        case "DZSQL":
                        case "SQL_":
                        case "KOREAF":
                            btn일괄출력.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                    btn수주서출력.Visible = btn일괄출력.Visible = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 일괄출력

        private void btn일괄출력_Click(object sender, EventArgs e)
        {
            try
            {
                string NO_SO = string.Empty;
                string DT_SO_chk = string.Empty;
                string DT_DUE_chk = string.Empty;

                for (int i = _flexNOH.Rows.Fixed; i < _flexNOH.Rows.Count; i++)
                {
                    if (_flexNOH[i, "S"].ToString() == "Y")
                        NO_SO += _flexNOH[i, "NO_SO"].ToString() + "|";
                }

                if (chkBox_so.Checked == false)
                {
                    DT_SO_chk = "N";
                }
                else
                {
                    DT_SO_chk = "Y";
                }

                String DT_pymnt_chk = string.Empty;
                if (chkBox_due.Checked == false)
                {
                    DT_DUE_chk = "N";
                }
                else
                {
                    DT_DUE_chk = "Y";
                }

                object[] obj = new object[]
                {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    NO_SO,
                    dtp수주일자.StartDateToString,
                    dtp수주일자.EndDateToString,
                    DT_SO_chk,
                    cbo공장.SelectedValue,
                    bp거래처.CodeValue,
                    dtp납기일자.StartDateToString,
                    dtp납기일자.EndDateToString,
                    DT_DUE_chk,
                    bpc수주상태.QueryWhereIn_Pipe,
                    bp수주형태.QueryWhereIn_Pipe,
                    bp품목.QueryWhereIn_Pipe,
                    bp담당자.CodeValue,
                    bp영업담당자.CodeValue,
                    bp영업조직.QueryWhereIn_Pipe,
                    bp영업그룹.QueryWhereIn_Pipe,
                    D.GetString(cbo거래처그룹.SelectedValue),
                    D.GetString(cbo거래처그룹2.SelectedValue),
                    bp대분류.CodeValue,
                    bp중분류.CodeValue,
                    bp소분류.CodeValue,
                    D.GetString(cbo_DT_GUBUN.SelectedValue),
                    D.GetString(cbo진행상태.SelectedValue)
                };

                DataTable dt = _biz.Print_KOREAF(obj);

                ReportHelper rptHelper = new ReportHelper("R_SA_Z_KOREAF_SOSCH_0", "수주현황-일괄출력(제원)");
                rptHelper.가로출력();
                rptHelper.SetDataTable(dt);

                if (chkBox_so.Checked == true)
                {
                    rptHelper.SetData("수주일자FROM", dtp수주일자.StartDateToString);
                    rptHelper.SetData("수주일자TO", dtp수주일자.EndDateToString);
                }
                else
                {
                    rptHelper.SetData("납기일자FROM", dtp납기일자.StartDateToString);
                    rptHelper.SetData("납기일자TO", dtp납기일자.EndDateToString);
                }
                rptHelper.SetData("공장", cbo공장.Text);
                rptHelper.SetData("거래처", bp거래처.CodeName);
                rptHelper.SetData("수주상태", bpc수주상태.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("수주형태", bp수주형태.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("품목", bp품목.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("수주담당자", bp담당자.CodeName);
                rptHelper.SetData("영업담당자", bp영업담당자.CodeName);
                rptHelper.SetData("영업조직", bp영업조직.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("영업그룹", bp영업그룹.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("거래처그룹", cbo거래처그룹.Text);
                rptHelper.SetData("거래처그룹2", cbo거래처그룹2.Text);
                rptHelper.SetData("대분류", bp대분류.CodeName);
                rptHelper.SetData("중분류", bp중분류.CodeName);
                rptHelper.SetData("소분류", bp소분류.CodeName);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♥ 기타 Property
        string ServerKey { get { return Global.MainFrame.ServerKeyCommon.ToUpper(); } }
        bool Chk수주일자 { get { return Checker.IsValid(dtp수주일자, true, lbl수주일자.Text); } }
        bool Chk납기일자 { get { return Checker.IsValid(dtp납기일자, true, D.GetString(cbo_DT_GUBUN.Text)); } }
        #endregion

        #region ♥ 업체별 전용 로직

        #region -> 신진에스엠 전용 프린트

        public void Z_SINJINSM_PRINT()
        {
            try
            {
                string ID_menu = string.Empty;//메뉴 아이디. 레포트번호
                string NM_menu = string.Empty;//메뉴명

                string Sort = string.Empty;

                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    ID_menu = "R_SA_SOSCH_0";
                    NM_menu = "수주현황-수주번호별";
                    Sort = "NO_SO ASC, CD_ITEMGRP ASC, NUM_STND_ITEM_1 ASC, NUM_STND_ITEM_2 ASC, NUM_STND_ITEM_3 ASC, NUM_STND_ITEM_4 ASC, NUM_STND_ITEM_5 ASC";
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    ID_menu = "R_SA_SOSCH_1";
                    NM_menu = "수주현황-거래처별";
                    Sort = "CD_PARTNER ASC, CD_ITEMGRP ASC, NUM_STND_ITEM_1 ASC, NUM_STND_ITEM_2 ASC, NUM_STND_ITEM_3 ASC, NUM_STND_ITEM_4 ASC, NUM_STND_ITEM_5 ASC";
                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;
                }

                DataTable dt_Print = _flexD.DataTable.Clone();
                DataRow[] drs_chk = _flexD.DataTable.Select("S = 'Y'");

                if (drs_chk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                foreach (DataRow dr in drs_chk)
                {
                    dr["CD_USERDEF4"] = "Y";
                    dr["NUM_USERDEF10"] = D.GetDecimal(dr["NUM_USERDEF10"]) + 1;
                    dr["TXT_USERDEF6"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    dt_Print.Rows.Add(dr.ItemArray);
                }

                dt_Print.Columns.Add("NM_CUST_PARTNER", typeof(string));

                string sNO_SO = string.Empty;
                string sNM_CUST_PARTNER = string.Empty;

                foreach (DataRow dr in dt_Print.Rows)
                {
                    if (sNO_SO != D.GetString(dr["NO_SO"]))
                    {
                        sNO_SO = D.GetString(dr["NO_SO"]);
                        sNM_CUST_PARTNER = _biz.Z_SINJINSM_PRINT_Search(sNO_SO);
                    }

                    if (!string.IsNullOrEmpty(sNM_CUST_PARTNER))
                        dr["NM_CUST_PARTNER"] = sNM_CUST_PARTNER;
                }

                dt_Print.DefaultView.Sort = Sort;
                dt_Print = dt_Print.DefaultView.ToTable();
                dt_Print.AcceptChanges();

                _biz.Z_SINJINSM_PRINT_Save(dt_Print);

                ReportHelper rptHelper = new ReportHelper(ID_menu, NM_menu);

                rptHelper.가로출력();
                rptHelper.SetDataTable(dt_Print);

                if (chkBox_so.Checked == true)
                {
                    rptHelper.SetData("수주일자FROM", dtp수주일자.StartDateToString);
                    rptHelper.SetData("수주일자TO", dtp수주일자.EndDateToString);
                }
                else
                {
                    rptHelper.SetData("납기일자FROM", dtp납기일자.StartDateToString);
                    rptHelper.SetData("납기일자TO", dtp납기일자.EndDateToString);
                }
                rptHelper.SetData("공장", cbo공장.Text);
                rptHelper.SetData("거래처", bp거래처.CodeName);
                rptHelper.SetData("수주상태", bpc수주상태.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("수주형태", bp수주형태.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("품목", bp품목.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("수주담당자", bp담당자.CodeName);
                rptHelper.SetData("영업담당자", bp영업담당자.CodeName);
                rptHelper.SetData("영업조직", bp영업조직.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("영업그룹", bp영업그룹.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("거래처그룹", cbo거래처그룹.Text);
                rptHelper.SetData("거래처그룹2", cbo거래처그룹2.Text);
                rptHelper.SetData("대분류", bp대분류.CodeName);
                rptHelper.SetData("중분류", bp중분류.CodeName);
                rptHelper.SetData("소분류", bp소분류.CodeName);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #endregion
    }
}