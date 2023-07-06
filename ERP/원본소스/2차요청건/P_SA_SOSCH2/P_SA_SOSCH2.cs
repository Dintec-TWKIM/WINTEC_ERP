using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Windows.Print;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using DzHelpFormLib;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-04-03
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 수주관리
    // 페 이 지  명 : 수주진행현황
    // 프로젝트  명 : P_SA_SOSCH2
    // **************************************
    // 2010.04.29 - 프로젝트별 탭 추가
    public partial class P_SA_SOSCH2 : PageBase
    {
        #region ♥ 멤버필드

        private P_SA_SOSCH2_BIZ _biz = new P_SA_SOSCH2_BIZ();

        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;

        string 서버키 = Global.MainFrame.ServerKeyCommon.ToUpper();

        #endregion                                                                                                                           

        #region ♥ 초기화

        #region -> 생성자

        public P_SA_SOSCH2()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flexNOH, _flexNOL, _flexPARTH, _flexPARTL, _flexITEMH, _flexITEML, _flexDTH, _flexDTL, _flexSOH, _flexSOL, _flexGRPH, _flexGRPL, _flexItemGRP, _flexPRJH, _flexPRJL };
            //DataChanged += new EventHandler(Page_DataChanged);

            //this.VerifyError += new EventHandler(Page_VerifyError);
            //tabCtrl.Selected += new TabControlEventHandler(tabCtrl_Selected);
            //tabCtrl.Deselecting += new TabControlCancelEventHandler(tabCtrl_Deselecting);
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

            InitGridItemGRP();

            InitGridPJTH();
            InitGridPJTL();

            InitGird제품군();

            _flexNOH.DetailGrids = new FlexGrid[] { _flexNOL };
            _flexPARTH.DetailGrids = new FlexGrid[] { _flexPARTL };
            _flexITEMH.DetailGrids = new FlexGrid[] { _flexITEML };
            _flexDTH.DetailGrids = new FlexGrid[] { _flexDTL };
            _flexSOH.DetailGrids = new FlexGrid[] { _flexSOL };
            _flexGRPH.DetailGrids = new FlexGrid[] { _flexGRPL };
            _flexPRJH.DetailGrids = new FlexGrid[] { _flexPRJL };

            _flex제품군H.DetailGrids = new FlexGrid[] { _flex제품군L };

            bpc제품군.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(bpc제품군_QueryBefore);
        }

        #endregion

        #region -> InitGridNOH

        private void InitGridNOH()
        {
            _flexNOH.BeginSetting(1, 1, false);
            _flexNOH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexNOH.SetCol("NO_SO", "수주번호", 120);
            _flexNOH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexNOH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexNOH.SetCol("FG_TRANSPORT", "운송방법", 100 );
            _flexNOH.SetCol("NM_SO", "수주형태", 120);
            _flexNOH.SetCol("DC_RMK", "비고", 100);
            _flexNOH.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexNOH.SetCol("NM_KOR", "수주담당자명", 120);
            _flexNOH.SetDummyColumn("S");
            _flexNOH.SettingVersion = "1.0";
            _flexNOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexNOH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridNOL
        private void InitGridNOL()
        {
            _flexNOL.BeginSetting(1, 1, false);
            _flexNOL.SetCol("CD_ITEM", "품목코드", 100);
            _flexNOL.SetCol("NM_ITEM", "품목명", 120);
            _flexNOL.SetCol("STND_ITEM", "규격", 80);
            _flexNOL.SetCol("UNIT_SO", "단위", 80);
            _flexNOL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            if (Global.MainFrame.ServerKeyCommon == "WINPLUS" || Global.MainFrame.ServerKeyCommon == "DZSQL")
            {
                _flexNOL.SetCol("QT_STANDBY", "출하대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            _flexNOL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("STA_SO", "수주상태", 60);
            _flexNOL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexNOL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexNOL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexNOL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexNOL.SetCol("NM_PROJECT", "프로젝트명", 100);

            _flexNOL.SetCol("NM_TP_DLV", "배송방법", 100); //090122 추가
            _flexNOL.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flexNOL.SetCol("NM_SL", "창고", 100);

            _flexNOL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexNOL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);

            _flexNOL.SettingVersion = "1.4";

            _flexNOL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }
        #endregion

        #region -> InitGridPARTH

        private void InitGridPARTH()
        {
            _flexPARTH.BeginSetting(1, 1, false);
            _flexPARTH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexPARTH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexPARTH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexPARTH.SetDummyColumn("S");
            _flexPARTH.SettingVersion = "1.0";
            _flexPARTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexPARTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridPARTL

        private void InitGridPARTL()
        {
            _flexPARTL.BeginSetting(1, 1, false);
            _flexPARTL.SetCol("NO_SO", "수주번호", 120);
            _flexPARTL.SetCol("CD_ITEM", "품목코드", 100);
            _flexPARTL.SetCol("NM_ITEM", "품목명", 120);
            _flexPARTL.SetCol("STND_ITEM", "규격", 80);
            _flexPARTL.SetCol("UNIT_SO", "단위", 80);
            _flexPARTL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPARTL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPARTL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPARTL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("STA_SO", "수주상태", 60);
            _flexPARTL.SetCol( "FG_TRANSPORT", "운송방법", 100 );
            _flexPARTL.SetCol("NM_SO", "수주형태", 120);
            _flexPARTL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexPARTL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexPARTL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPARTL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexPARTL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexPARTL.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexPARTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexPARTL.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flexPARTL.SetCol("NM_TP_DLV", "배송방법", 100);
            _flexPARTL.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가
            _flexPARTL.SetCol("NM_SL", "창고", 100);
            _flexPARTL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexPARTL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexPARTL.SettingVersion = "1.3";
            _flexPARTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridITEMH

        private void InitGridITEMH()
        {
            _flexITEMH.BeginSetting(1, 1, false);
            _flexITEMH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexITEMH.SetCol("CD_ITEM", "품목코드", 100);
            _flexITEMH.SetCol("NM_ITEM", "품목명", 120);
            _flexITEMH.SetCol("STND_ITEM", "규격", 80);
            _flexITEMH.SetDummyColumn("S");
            _flexITEMH.SettingVersion = "1.0";
            _flexITEMH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexITEMH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridITEML

        private void InitGridITEML()
        {
            _flexITEML.BeginSetting(1, 1, false);
            _flexITEML.SetCol("NO_SO", "수주번호", 120);
            _flexITEML.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEML.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEML.SetCol("UNIT_SO", "단위", 80);
            _flexITEML.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEML.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("STA_SO", "수주상태", 60);
            _flexITEML.SetCol( "FG_TRANSPORT", "운송방법", 100 );
            _flexITEML.SetCol("NM_SO", "수주형태", 120);
            _flexITEML.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexITEML.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexITEML.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEML.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexITEML.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexITEML.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexITEML.SetCol("NM_KOR", "수주담당자명", 120);
            _flexITEML.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexITEML.SetCol("LN_PARTNER", "거래처명", 120);

            _flexITEML.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flexITEML.SetCol("NM_TP_DLV", "배송방법", 100);
            _flexITEML.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flexITEML.SetCol("NM_SL", "창고", 100);
            _flexITEML.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexITEML.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexITEML.SettingVersion = "1.3";

            _flexITEML.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridDTH

        private void InitGridDTH()
        {
            _flexDTH.BeginSetting(1, 1, false);
            _flexDTH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexDTH.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexDTH.SetDummyColumn("S");
            _flexDTH.SettingVersion = "1.0";
            _flexDTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexDTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridDTL

        private void InitGridDTL()
        {
            _flexDTL.BeginSetting(1, 1, false);
            _flexDTL.SetCol("NO_SO", "수주번호", 120);
            _flexDTL.SetCol("CD_ITEM", "품목코드", 100);
            _flexDTL.SetCol("NM_ITEM", "품목명", 120);
            _flexDTL.SetCol("STND_ITEM", "규격", 80);
            _flexDTL.SetCol("UNIT_SO", "단위", 80);
            _flexDTL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexDTL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexDTL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("STA_SO", "수주상태", 60);
            _flexDTL.SetCol( "FG_TRANSPORT", "운송방법", 100 );
            _flexDTL.SetCol("NM_SO", "수주형태", 120);
            _flexDTL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexDTL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexDTL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexDTL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexDTL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexDTL.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexDTL.SetCol("NM_KOR", "수주담당자명", 120);
            _flexDTL.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexDTL.SetCol("LN_PARTNER", "거래처명", 120);

            _flexDTL.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flexDTL.SetCol("NM_TP_DLV", "배송방법", 100);
            _flexDTL.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flexDTL.SetCol("NM_SL", "창고", 100);
            _flexDTL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexDTL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexDTL.SettingVersion = "1.3";
            _flexDTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridSOH

        private void InitGridSOH()
        {
            _flexSOH.BeginSetting(1, 1, false);
            _flexSOH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexSOH.SetCol("TP_SO", "수주형태코드", 100);
            _flexSOH.SetCol("NM_SO", "수주형태명", 150);
            _flexSOH.SetDummyColumn("S");
            _flexSOH.SettingVersion = "1.0";
            _flexSOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexSOH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridSOL

        private void InitGridSOL()
        {
            _flexSOL.BeginSetting(1, 1, false);
            _flexSOL.SetCol("NO_SO", "수주번호", 120);
            _flexSOL.SetCol("CD_ITEM", "품목코드", 100);
            _flexSOL.SetCol("NM_ITEM", "품목명", 120);
            _flexSOL.SetCol("STND_ITEM", "규격", 80);
            _flexSOL.SetCol("UNIT_SO", "단위", 80);
            _flexSOL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexSOL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexSOL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexSOL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("STA_SO", "수주상태", 60);
            _flexSOL.SetCol( "FG_TRANSPORT", "운송방법", 100 );
            _flexSOL.SetCol("NM_SO", "수주형태", 120);
            _flexSOL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexSOL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexSOL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexSOL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexSOL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexSOL.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexSOL.SetCol("NM_KOR", "수주담당자명", 120);
            _flexSOL.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexSOL.SetCol("LN_PARTNER", "거래처명", 120);

            _flexSOL.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flexSOL.SetCol("NM_TP_DLV", "배송방법", 100);
            _flexSOL.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flexSOL.SetCol("NM_SL", "창고", 100);
            _flexSOL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexSOL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexSOL.SettingVersion = "1.3";

            _flexSOL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridGRPH

        private void InitGridGRPH()
        {
            _flexGRPH.BeginSetting(1, 1, false);
            _flexGRPH.SetCol( "S", "선택", 45, true, CheckTypeEnum.Y_N );
            _flexGRPH.SetCol("CD_SALEGRP", "영업그룹코드", 100);
            _flexGRPH.SetCol("NM_SALEGRP", "영업그룹명", 120);
        
            _flexGRPH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexGRPH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridGRPL

        private void InitGridGRPL()
        {
            _flexGRPL.BeginSetting(1, 1, false);
            _flexGRPL.SetCol("NO_SO", "수주번호", 120);
            _flexGRPL.SetCol("CD_ITEM", "품목코드", 100);
            _flexGRPL.SetCol("NM_ITEM", "품목명", 120);
            _flexGRPL.SetCol("STND_ITEM", "규격", 80);
            _flexGRPL.SetCol("UNIT_SO", "단위", 80);
            _flexGRPL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexGRPL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexGRPL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexGRPL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("STA_SO", "수주상태", 60);
            _flexGRPL.SetCol("FG_TRANSPORT", "운송방법", 100 );
            _flexGRPL.SetCol("NM_SO", "수주형태", 120);
            _flexGRPL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexGRPL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexGRPL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexGRPL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexGRPL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexGRPL.SetCol("NO_EMP", "수주담당자코드", 100);
            _flexGRPL.SetCol("NM_KOR", "수주담당자명", 120);
            _flexGRPL.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexGRPL.SetCol("LN_PARTNER", "거래처명", 120);
            _flexGRPL.SetCol("CD_SL", "창고코드", 120);

            _flexGRPL.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flexGRPL.SetCol("NM_TP_DLV", "배송방법", 100);
            _flexGRPL.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flexGRPL.SetCol("NM_SL", "창고", 100);
            _flexGRPL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexGRPL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexGRPL.SettingVersion = "1.3";
            _flexGRPL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridItemGRP
        private void InitGridItemGRP()
        {
            _flexItemGRP.BeginSetting(1, 1, false);
            _flexItemGRP.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexItemGRP.SetCol("GRP_ITEM", "품목군", 100, 20, false);
            _flexItemGRP.SetCol("NM_ITEMGRP", "품목군명", 140, 50, false);

            _flexItemGRP.SetCol("GRP_MFG", "제품군", 100, 20, false);
            _flexItemGRP.SetCol("NM_GRPMFG", "제품군명", 140, 50, false);

            //_flexItemGRP.SetCol("GRP_ITEM", "소분류", 100, 20, false);
            _flexItemGRP.SetCol("CLS_S", "소분류명", 140, 50, false);

            //_flexItemGRP.SetCol("GRP_ITEM", "중분류", 100, 20, false);
            _flexItemGRP.SetCol("CLS_M", "중분류명", 140, 50, false);

            //_flexItemGRP.SetCol("GRP_ITEM", "대분류", 100, 20, false);
            _flexItemGRP.SetCol("CLS_L", "대분류명", 100, 20, false);

            _flexItemGRP.SetCol("CD_ITEM", "품목코드", 100);
            _flexItemGRP.SetCol("NM_ITEM", "품목명", 120);
            _flexItemGRP.SetCol("STND_ITEM", "규격", 80);
            _flexItemGRP.SetCol("UNIT_SO", "단위", 80);
            _flexItemGRP.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexItemGRP.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexItemGRP.SetCol("AM_WONAMT", "수주원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexItemGRP.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexItemGRP.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexItemGRP.SetCol("AM_GIR_WONAMT", "의뢰원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            //_flexItemGRP.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexItemGRP.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexItemGRP.SetCol("AM_GI", "출하금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexItemGRP.SetCol("AM_GI_WONAMT", "출하원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexItemGRP.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            //_flexItemGRP.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            //_flexItemGRP.SetCol("AM_IV_WONAMT", "매출원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexItemGRP.SetCol("QT_IV_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexItemGRP.SetCol("AM_IV_REMAIN", "미납금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexItemGRP.SetCol("AM_IV_WONAMT_REMAIN", "미납원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexItemGRP.SetCol("QT_INV", "현재고 수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexItemGRP.SetDummyColumn("S");
            _flexItemGRP.SettingVersion = "1.3";
            _flexItemGRP.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }
        #endregion

        #region -> InitGridPJTH

        private void InitGridPJTH()
        {
            _flexPRJH.BeginSetting(1, 1, false);
            _flexPRJH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexPRJH.SetCol("NO_PROJECT", "프로젝트코드", 100, 20, false);
            _flexPRJH.SetCol("NM_PROJECT", "프로젝트명", 140, 50, false);
            _flexPRJH.SetDummyColumn("S");
            _flexPRJH.SettingVersion = "1.0";
            _flexPRJH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexPRJH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridPJTL
        private void InitGridPJTL()
        {
            _flexPRJL.BeginSetting(1, 1, false);
            _flexPRJL.SetCol("CD_ITEM", "품목코드", 100);
            _flexPRJL.SetCol("NM_ITEM", "품목명", 120);
            _flexPRJL.SetCol("STND_ITEM", "규격", 80);
            _flexPRJL.SetCol("UNIT_SO", "단위", 80);
            _flexPRJL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexPRJL.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("STA_SO", "수주상태", 60);
            _flexPRJL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexPRJL.SetCol("NM_SL", "창고", 100);
            _flexPRJL.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexPRJL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flexPRJL.SettingVersion = "1.5";
            _flexPRJL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }
        #endregion

        #region -> InitGird제품군
        void InitGird제품군()
        {
            _flex제품군H.BeginSetting(1, 1, false);
            _flex제품군H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex제품군H.SetCol("GRP_MFG", "제품군코드", 100);
            _flex제품군H.SetCol("NM_GRP_MFG", "제품군명", 120);
            _flex제품군H.SetDummyColumn("S");
            _flex제품군H.SettingVersion = "1.0.0.0";
            _flex제품군H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex제품군H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

            _flex제품군L.BeginSetting(1, 1, false);
            _flex제품군L.SetCol("NO_SO", "수주번호", 120);
            _flex제품군L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex제품군L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex제품군L.SetCol("UNIT_SO", "단위", 80);
            _flex제품군L.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex제품군L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_GI_REMAIN", "미납수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_IV", "매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("QT_IV_REMAIN", "미매출수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("STA_SO", "수주상태", 60);
            _flex제품군L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex제품군L.SetCol("NM_SO", "수주형태", 120);
            _flex제품군L.SetCol("GI_PARTNER", "납품처", 100, false);
            _flex제품군L.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flex제품군L.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex제품군L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex제품군L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex제품군L.SetCol("NO_EMP", "수주담당자코드", 100);
            _flex제품군L.SetCol("NM_KOR", "수주담당자명", 120);
            _flex제품군L.SetCol("CD_PARTNER", "거래처코드", 100);
            _flex제품군L.SetCol("LN_PARTNER", "거래처명", 120);

            _flex제품군L.SetCol("DC_RMK", "비고", 100); //090122 추가
            _flex제품군L.SetCol("NM_TP_DLV", "배송방법", 100);
            _flex제품군L.SetCol("NM_TP_DLV_DUE", "납품방법", 100); //100127 추가

            _flex제품군L.SetCol("NM_SL", "창고", 100);
            _flex제품군L.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flex제품군L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100);
            _flex제품군L.SettingVersion = "1.0.0.2";

            _flex제품군L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

        }
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            dtp수주일자.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            dtp수주일자.EndDateToString = MainFrameInterface.GetStringToday;

            DataSet g_dsCombo = GetComboData("N;MA_PLANT", "S;PU_C000016", "N;SA_B000031", "S;SA_B000016", "S;SA_B000038", "S;TR_IM00008");

            // 공장 콤보
            cbo공장.DataSource = g_dsCombo.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            // 거래구분 콤보
            cbo거래구분.DataSource = g_dsCombo.Tables[1];
            cbo거래구분.DisplayMember = "NAME";
            cbo거래구분.ValueMember = "CODE"; 

            cbo단위선택.DataSource = g_dsCombo.Tables[2];
            cbo단위선택.DisplayMember = "NAME";
            cbo단위선택.ValueMember = "CODE";

            cbo수주상태.DataSource = g_dsCombo.Tables[3];
            cbo수주상태.DisplayMember = "NAME";
            cbo수주상태.ValueMember = "CODE";

            cbo진행상태.DataSource = g_dsCombo.Tables[4];
            cbo진행상태.DisplayMember = "NAME";
            cbo진행상태.ValueMember = "CODE";

            _flexNOL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexPARTL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexITEML.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexDTL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexSOL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexGRPL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");

            _flexNOH.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexPARTL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexITEML.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexDTL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexSOL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexGRPL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [5], "CODE", "NAME" );

            cbo공장.SelectedValue = LoginInfo.BizAreaCode;

            MA_EXC_SET();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            bpcom_CD_SL.UseGrant = false;
        }

        #endregion

        #endregion

        #region ♥ 메인버튼 클릭

        #region -> Check

        public bool Check()
        {
            if (!Chk수주일자) return false;

            if (cbo공장.SelectedValue == null || cbo공장.SelectedValue.ToString() == string.Empty || cbo공장.SelectedValue.ToString() == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
                cbo공장.Focus();
                return false;
            }

            if (cbo단위선택.SelectedValue == null || cbo단위선택.SelectedValue.ToString() == string.Empty || cbo단위선택.SelectedValue.ToString() == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl단위선택.Text);
                cbo단위선택.Focus();
                return false;
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

                DataTable dt = null;

                string 탭구분 = m_tabC.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 납기요구일별, 4: 수주형태별, 5: 영업그룹별, 6: 품목집계별

                if (탭구분 == "6")
                {
                    string Key = string.Empty;
                    string 단위선택 = cbo단위선택.SelectedValue.ToString();  //001: 수주단위, 002:재고단위

                    dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), 
                                                bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 
                                                단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(),
                                                cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, bpcom_CD_SL.QueryWhereIn_Pipe, bp거래처.CodeValue, bpc제품군.QueryWhereIn_Pipe,txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                }
                else
                {
                    dt = _biz.Search(dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, D.GetString(cbo공장.SelectedValue), bp영업그룹.QueryWhereIn_Pipe, D.GetString(cbo거래구분.SelectedValue),
                                     D.GetString(cbo수주상태.SelectedValue),
                                     D.GetString(cbo진행상태.SelectedValue), bpTpSo.CodeValue, 탭구분, bp거래처.CodeValue, bp_Emp.CodeValue, bpcom_CD_SL.QueryWhereIn_Pipe, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text);
                }

                if (탭구분 == "0")
                    _flexNOH.Binding = dt;
                else if (탭구분 == "1")
                    _flexPARTH.Binding = dt;
                else if (탭구분 == "2")
                    _flexITEMH.Binding = dt;
                else if (탭구분 == "3")
                    _flexDTH.Binding = dt;
                else if (탭구분 == "4")
                    _flexSOH.Binding = dt;
                else if (탭구분 == "5")
                    _flexGRPH.Binding = dt;
                else if (탭구분 == "6")
                    _flexItemGRP.Binding = dt;
                else if (탭구분 == "7")
                    _flexPRJH.Binding = dt;
                else if (탭구분 == "8")
                    _flex제품군H.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    return;
                }

                ToolBarAddButtonEnabled = ToolBarDeleteButtonEnabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼클릭

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string 메뉴ID = string.Empty;
                string 메뉴명 = string.Empty;

                if ( m_tabC.SelectedIndex == 0 )
                {
                    메뉴ID = "R_SA_SOSCH2_0";
                    메뉴명 = "수주진행현황-수주번호별";
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if ( m_tabC.SelectedIndex == 1 )
                {
                    메뉴ID = "R_SA_SOSCH2_1";
                    메뉴명 = "수주진행현황-거래처별";

                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;

                }
                else if ( m_tabC.SelectedIndex == 2 )
                {
                    메뉴ID = "R_SA_SOSCH2_2";
                    메뉴명 = "수주진행현황-품목별";

                    _flexH = _flexITEMH;
                    _flexD = _flexITEML;
                }
                else if ( m_tabC.SelectedIndex == 3 )
                {
                    메뉴ID = "R_SA_SOSCH2_3";
                    메뉴명 = "수주진행현황-납기일별";

                    _flexH = _flexDTH;
                    _flexD = _flexDTL;
                }
                else if ( m_tabC.SelectedIndex == 4 )
                {
                    메뉴ID = "R_SA_SOSCH2_4";
                    메뉴명 = "수주진행현황-수주형태별";

                    _flexH = _flexSOH;
                    _flexD = _flexSOL;
                }
                else if ( m_tabC.SelectedIndex == 5 )
                {
                    메뉴ID = "R_SA_SOSCH2_5";
                    메뉴명 = "수주진행현황-영업그룹별";

                    _flexH = _flexGRPH;
                    _flexD = _flexGRPL;
                }
                else if (m_tabC.SelectedIndex == 6)
                {
                    메뉴ID = "R_SA_SOSCH2_6";
                    메뉴명 = "수주진행현황-품목집계별";

                    _flexH = _flexItemGRP;
                }
                else if (m_tabC.SelectedIndex == 7)
                {
                    메뉴ID = "R_SA_SOSCH2_7";
                    메뉴명 = "수주진행현황-프로젝트별";

                    _flexH = _flexPRJH;
                    _flexD = _flexPRJL;
                }
                else if (m_tabC.SelectedIndex == 8)
                {
                    메뉴ID = "R_SA_SOSCH2_8";
                    메뉴명 = "수주진행현황-제품군별";

                    _flexH = _flex제품군H;
                    _flexD = _flex제품군L;
                }

                string No_PK_Multi = "";

                DataRow [] ldt_Report = _flexH.DataTable.Select( "S = 'Y'" );

                if ( ldt_Report == null || ldt_Report.Length == 0 )
                {
                    ShowMessage( 공통메세지.선택된자료가없습니다 );
                    return;
                }
                else
                {

                    switch ( 메뉴명 )
                    {
                        case "수주진행현황-수주번호별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "NO_SO"].ToString() + "|";
                            }
                            break;

                        case "수주진행현황-거래처별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_PARTNER"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-품목별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_ITEM"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-납기일별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "DT_DUEDATE"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-수주형태별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "TP_SO"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-영업그룹별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_SALEGRP"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-품목집계별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "GRP_ITEM"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-프로젝트별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "NO_PROJECT"].ToString() + "|";
                            }
                            break;
                        case "수주진행현황-제품군별":
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "GRP_MFG"].ToString() + "|";
                            }
                            break;
                 
                        default:
                            break;
                    }
                }

                if ( !_flexH.HasNormalRow && _flexD.HasNormalRow )
                    return;

                ReportHelper rptHelper = new ReportHelper( 메뉴ID, 메뉴명 );

                rptHelper.가로출력();

                try
                {
                    DataTable dt = null;
                    if (m_tabC.SelectedIndex == 6)
                    {
                        dt = _flexItemGRP.GetTableFromGrid("S='Y'");
                    }
                    else
                    {
                        dt = _biz.출력(
                                                                dtp수주일자.StartDateToString,
                                                                dtp수주일자.EndDateToString,
                                                                cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString(),
                                                                bp영업그룹.QueryWhereIn_Pipe,
                                                                cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(),
                                                                cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(),
                                                                cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(),
                                                                cbo단위선택.SelectedValue == null ? string.Empty : cbo단위선택.SelectedValue.ToString(),
                                                                bpTpSo.CodeValue, m_tabC.SelectedIndex.ToString(),
                                                                No_PK_Multi,
                                                                bpcom_CD_SL.QueryWhereIn_Pipe    //bpcom_CD_SL.SelectedValue != null ? bpcom_CD_SL.SelectedValue.ToString() : ""
                                                                , bp거래처.CodeValue, bpc제품군.QueryWhereIn_Pipe
                                                                ,txt거래처PO번호.Text
                                                             );
                    }                                                 
                    rptHelper.SetDataTable( dt );
                    rptHelper.SetData("수주일자Fr", dtp수주일자.StartDateToString.Substring(0, 4) + "년" + dtp수주일자.StartDateToString.Substring(4, 2) + "월" + dtp수주일자.StartDateToString.Substring(6, 2) + "일");
                    rptHelper.SetData( "의뢰일자To", dtp수주일자.EndDateToString.Substring( 0, 4 ) + "년" + dtp수주일자.EndDateToString.Substring( 4, 2 ) + "월" + dtp수주일자.EndDateToString.Substring( 6, 2 ) + "일" );
                    rptHelper.SetData( "거래구분", cbo거래구분.Text);
                    rptHelper.SetData( "진행상태", cbo진행상태.Text);
                    rptHelper.SetData( "공장",  cbo공장.Text);
                    rptHelper.SetData( "단위선택", cbo단위선택.Text);
                    rptHelper.SetData( "수주형태", bpTpSo.CodeName);                    
                    rptHelper.SetData( "영업그룹", bp영업그룹.QueryWhereIn_PipeDisplayMember); 
                    rptHelper.SetData( "수주상태", cbo수주상태.Text );
                    rptHelper.Print();
                }
                catch ( Exception Ex )
                {
                    this.MsgEnd( Ex );
                }
            }
            catch ( Exception Ex )
            {
                this.MsgEnd( Ex );
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
                string 탭구분 = m_tabC.SelectedIndex.ToString(); //0: 출하번호별, 1: 거래처별
                string 단위선택 = cbo단위선택.SelectedValue.ToString();  //001: 수주단위, 002:재고단위
                string 창고 = bpcom_CD_SL.QueryWhereIn_Pipe;
                string 거래처 = D.GetString(bp거래처.CodeValue);
                switch (flex.Name)
                {
                    case "_flexNOH":
                        Key = _flexNOH[e.NewRange.r1, "NO_SO"].ToString();
                        Filter = "NO_SO = '" + Key + "'";

                        if (_flexNOH.DetailQueryNeed)
                        {
                            if (Global.MainFrame.ServerKeyCommon == "WINPLUS" || Global.MainFrame.ServerKeyCommon == "DZSQL")
                                dt = _biz.SearchWINPLUS(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                            else
                                dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        }
                        
                        _flexNOL.BindingAdd(dt, Filter);
                        _flexNOH.DetailQueryNeed = false;
                        break;
                    case "_flexPARTH":
                        Key = _flexPARTH[e.NewRange.r1, "CD_PARTNER"].ToString();
                        Filter = "CD_PARTNER = '" + Key + "'";
                        
                        if (_flexPARTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        
                        _flexPARTL.BindingAdd(dt, Filter);
                        _flexPARTH.DetailQueryNeed = false;
                        break;
                    case "_flexITEMH":
                        Key = _flexITEMH[e.NewRange.r1, "CD_ITEM"].ToString();
                        Filter = "CD_ITEM = '" + Key + "'";
                        
                        if (_flexITEMH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        
                        _flexITEML.BindingAdd(dt, Filter);
                        _flexITEMH.DetailQueryNeed = false;
                        break;
                    case "_flexDTH":
                        Key = _flexDTH[e.NewRange.r1, "DT_DUEDATE"].ToString();
                        Filter = "DT_DUEDATE = '" + Key + "'";
                        
                        if (_flexDTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        
                        _flexDTL.BindingAdd(dt, Filter);
                        _flexDTH.DetailQueryNeed = false;
                        break;
                    case "_flexSOH":
                        Key = _flexSOH[e.NewRange.r1, "TP_SO"].ToString();
                        Filter = "TP_SO = '" + Key + "'";
                        
                        if (_flexSOH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        
                        _flexSOL.BindingAdd(dt, Filter);
                        _flexSOH.DetailQueryNeed = false;
                        break;
                    case "_flexGRPH":
                        Key = _flexGRPH[e.NewRange.r1, "CD_SALEGRP"].ToString();
                        Filter = "CD_SALEGRP = '" + Key + "'";
                        
                        if (_flexGRPH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");
                        
                        _flexGRPL.BindingAdd(dt, Filter);
                        _flexGRPH.DetailQueryNeed = false;
                        break;
                    case "_flexPRJH":
                        Key = _flexPRJH[e.NewRange.r1, "NO_PROJECT"].ToString();
                        Filter = "NO_PROJECT = '" + Key + "'";

                        if (_flexPRJH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");

                        _flexPRJL.BindingAdd(dt, Filter);
                        _flexPRJH.DetailQueryNeed = false;
                        break;
                    case "_flex제품군H" :
                        Key = D.GetString(_flex제품군H["GRP_MFG"]);
                        Filter = "GRP_MFG = '" + Key + "'";

                        if (_flex제품군H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp수주일자.StartDateToString, dtp수주일자.EndDateToString, cbo공장.SelectedValue.ToString(), bp영업그룹.QueryWhereIn_Pipe, cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), 단위선택, cbo수주상태.SelectedValue == null ? string.Empty : cbo수주상태.SelectedValue.ToString(), cbo진행상태.SelectedValue == null ? string.Empty : cbo진행상태.SelectedValue.ToString(), bpTpSo.CodeValue, 탭구분, 창고, 거래처, bpc제품군.QueryWhereIn_Pipe, txt거래처PO번호.Text, chkMRP창고.Checked ? "Y" : "N");

                        _flex제품군L.BindingAdd(dt, Filter);
                        _flex제품군H.DetailQueryNeed = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♥ 도움창 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;

                case HelpID.P_MA_SL_SUB1:       // 창고 도움창(멀티)
                    e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                    break;
            }
        }

        #endregion

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion


        void bpc제품군_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        #endregion

        #region -> PageOpenSetting
        private void PageOpenSetting()
        {
            if (서버키 == "DZSQL" || 서버키 == "TEMPUR")
            {
                object[] obj = new object[] {
                    D.GetString(Global.MainFrame.LoginInfo.CompanyCode),
                    D.GetString(Global.MainFrame.LoginInfo.UserID)
                };

                DataTable dt = _biz.PageDefaultSetting(obj);

                cbo공장.SelectedValue = D.GetString(dt.Rows[0]["CD_PLANT"]);
                cbo공장.DisplayMember = "NAME";
                cbo공장.ValueMember = "CODE";

                bpTpSo.CodeValue = D.GetString(dt.Rows[0]["TP_SO"]);
                bpTpSo.CodeName = D.GetString(dt.Rows[0]["NM_SO"]);

                bp거래처.CodeValue = D.GetString(dt.Rows[0]["CD_PARTNER_SO"]);
                bp거래처.CodeName = D.GetString(dt.Rows[0]["NM_PARTNER_SO"]);

                bp_Emp.CodeValue = D.GetString(dt.Rows[0]["NO_EMP"]);
                bp_Emp.CodeName = D.GetString(dt.Rows[0]["NM_USER"]);

                if(D.GetString(dt.Rows[0]["CD_SALEGRP_SO"]) == string.Empty) { }
                else
                    bp영업그룹.AddItem(D.GetString(dt.Rows[0]["CD_SALEGRP_SO"]), D.GetString(dt.Rows[0]["NM_SALEGRP_SO"]));

                if (D.GetString(dt.Rows[0]["CD_SL_SO"]) == string.Empty) { }
                else
                    bpcom_CD_SL.AddItem(D.GetString(dt.Rows[0]["CD_SL_SO"]), D.GetString(dt.Rows[0]["NM_SL_SO"]));

                if (D.GetString(dt.Rows[0]["FG_FIX_SO"]) == "Y")
                {
                    bp_Emp.Enabled = false;
                    cbo공장.Enabled = false;

                    if (D.GetString(dt.Rows[0]["CD_SALEGRP_SO"]) == string.Empty)
                        bp영업그룹.Enabled = true;
                    else
                        bp영업그룹.Enabled = false;
                    
                    if (D.GetString(dt.Rows[0]["TP_SO"]) == string.Empty)
                        bpTpSo.Enabled = true;
                    else
                        bpTpSo.Enabled = false;
                    if (D.GetString(dt.Rows[0]["CD_SL_SO"]) == string.Empty)
                        bpcom_CD_SL.Enabled = true;
                    else
                        bpcom_CD_SL.Enabled = false;
                    
                    if (D.GetString(dt.Rows[0]["CD_PARTNER_SO"]) == string.Empty)
                        bp거래처.Enabled = true;
                    else
                        bp거래처.Enabled = false;
                }
            }
        }
        #endregion

        #region -> 시스템통제설정
        private void MA_EXC_SET()
        {
            if (Duzon.ERPU.MF.ComFunc.전용코드("물류사용자기준정보등록 적용") == "Y")
                PageOpenSetting();
        }
        #endregion

        bool Chk수주일자 { get { return Checker.IsValid(dtp수주일자, true, lbl수주일자.Text); } }
    }
}
