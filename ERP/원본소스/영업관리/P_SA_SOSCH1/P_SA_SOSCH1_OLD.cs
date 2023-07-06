using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-04-05
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 수주관리
    // 페 이 지  명 : 수주현황
    // 프로젝트  명 : P_SA_SOSCH1
    // **************************************
    public partial class P_SA_SOSCH1 : PageBase
    {
        #region ♥ 멤버필드

        private P_SA_SOSCH1_BIZ _biz = new P_SA_SOSCH1_BIZ();

        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;

        private DataTable ExcelDt = null;
        #endregion

        #region ♥ 초기화

        #region -> 생성자

        public P_SA_SOSCH1()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flexNOH, _flexNOL, _flexPARTH, _flexPARTL, _flexITEMH, _flexITEML, _flexDTH, _flexDTL, _flexSOH, _flexSOL, _flexGRPH, _flexGRPL, _flexPROJECTH, _flexPROJECTL };
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

            InitGridPROJECTH();
            InitGridPROJECTL();

            _flexNOH.DetailGrids = new FlexGrid[] { _flexNOL };
            _flexPARTH.DetailGrids = new FlexGrid[] { _flexPARTL };
            _flexITEMH.DetailGrids = new FlexGrid[] { _flexITEML };
            _flexDTH.DetailGrids = new FlexGrid[] { _flexDTL };
            _flexSOH.DetailGrids = new FlexGrid[] { _flexSOL };
            _flexGRPH.DetailGrids = new FlexGrid[] { _flexGRPL };
            _flexPROJECTH.DetailGrids = new FlexGrid[] { _flexPROJECTL };
        }

        #endregion

        #region -> InitGridNOH

        private void InitGridNOH()
        {
            _flexNOH.BeginSetting(1, 1, false);
            _flexNOH.SetDummyColumn( "S" );
            _flexNOH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexNOH.SetCol("NO_SO", "수주번호", 120);
            _flexNOH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexNOH.SetCol("NM_SO", "수주형태", 150);
            _flexNOH.SetCol("CD_EXCH", "환종", 100);
            _flexNOH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexNOH.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexNOH.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexNOH.SetCol("NO_PROJECT", "프로젝트코드", 100 );
            _flexNOH.SetCol("FG_TAXP", "계산서처리", 100 );
            _flexNOH.SetCol("FG_BILL", "결재방법", 100 );
            _flexNOH.SetCol("DC_RMK", "비고", 100 );
            _flexNOH.SetCol("NO_EMP", "수주담당자", 100);
            _flexNOH.SetCol("NM_KOR", "수주담당자명", 120);
            _flexNOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexNOH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

            //2008.06.17 엑셀내려받는 기능추가
            if (Global.MainFrame.ServerKey == "PEACE2" || Global.MainFrame.ServerKey == "108")
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
            _flexNOL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexNOL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexNOL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexNOL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexNOL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridPARTH

        private void InitGridPARTH()
        {
            _flexPARTH.BeginSetting(1, 1, false);
            _flexPARTH.SetDummyColumn( "S" );
            _flexPARTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexPARTH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexPARTH.SetCol("LN_PARTNER", "거래처명", 120);
            
            _flexPARTH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexPARTH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
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
            _flexPARTL.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexPARTL.SetCol( "RT_EXCH", "환율", 100 );
            _flexPARTL.SetCol( "NM_SO", "수주형태", 100 );
            _flexPARTL.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexPARTL.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexPARTL.SetCol( "FG_BILL", "결재방법", 100 );

            _flexPARTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexPARTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexPARTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexPARTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexPARTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexPARTL.SetCol("NM_KOR", "수주담당자명", 120);
            _flexPARTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridITEMH

        private void InitGridITEMH()
        {
            _flexITEMH.BeginSetting(1, 1, false);
            _flexITEMH.SetDummyColumn( "S" );
            _flexITEMH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexITEMH.SetCol("CD_ITEM", "품목코드", 100);
            _flexITEMH.SetCol("NM_ITEM", "품목명", 120);
            _flexITEMH.SetCol("STND_ITEM", "규격", 80);
         

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
            _flexITEML.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexITEML.SetCol( "RT_EXCH", "환율", 100 );
            _flexITEML.SetCol( "NM_SO", "수주형태", 100 );
            _flexITEML.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexITEML.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexITEML.SetCol( "FG_BILL", "결재방법", 100 );

            _flexITEML.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexITEML.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexITEML.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexITEML.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexITEML.SetCol("NO_EMP", "수주담당자", 100);
            _flexITEML.SetCol("NM_KOR", "수주담당자명", 120);

            _flexITEML.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridDTH

        private void InitGridDTH()
        {
            _flexDTH.BeginSetting(1, 1, false);
            _flexDTH.SetDummyColumn( "S" );
            _flexDTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexDTH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        
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
            _flexDTL.SetCol("QT_SO", "수주수량", 80,false, typeof(decimal), FormatTpType.QUANTITY);
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
            _flexDTL.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexDTL.SetCol( "RT_EXCH", "환율", 100 );
            _flexDTL.SetCol( "NM_SO", "수주형태", 100 );
            _flexDTL.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexDTL.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexDTL.SetCol( "FG_BILL", "결재방법", 100 );

            _flexDTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexDTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexDTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexDTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexDTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexDTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexDTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridSOH

        private void InitGridSOH()
        {
            _flexSOH.BeginSetting(1, 1, false);
            _flexSOH.SetDummyColumn( "S" );
            _flexSOH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexSOH.SetCol("TP_SO", "수주형태코드", 100);
            _flexSOH.SetCol("NM_SO", "수주형태명", 150);
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
            _flexSOL.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexSOL.SetCol( "RT_EXCH", "환율", 100 );
            _flexSOL.SetCol( "NM_SO", "수주형태", 100 );
            _flexSOL.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexSOL.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexSOL.SetCol( "FG_BILL", "결재방법", 100 );

            _flexSOL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexSOL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexSOL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexSOL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexSOL.SetCol("NO_EMP", "수주담당자", 100);
            _flexSOL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexSOL.EndSetting( GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top );
        }

        #endregion

        #region -> InitGridGRPH

        private void InitGridGRPH()
        {
            _flexGRPH.BeginSetting(1, 1, false);
            _flexGRPH.SetDummyColumn( "S" );
            _flexGRPH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
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
            _flexGRPL.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexGRPL.SetCol( "RT_EXCH", "환율", 100 );
            _flexGRPL.SetCol( "NM_SO", "수주형태", 100 );
            _flexGRPL.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexGRPL.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexGRPL.SetCol( "FG_BILL", "결재방법", 100 );

            _flexGRPL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexGRPL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexGRPL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexGRPL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexGRPL.SetCol("NO_EMP", "수주담당자", 100);
            _flexGRPL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexGRPL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitGridPROJECTH

        private void InitGridPROJECTH()
        {
            _flexPROJECTH.BeginSetting(1, 1, false);
            _flexPROJECTH.SetDummyColumn( "S" );
            _flexPROJECTH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexPROJECTH.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexPROJECTH.SetCol("NM_PROJECT", "프로젝트명", 120);
           
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
            _flexPROJECTL.SetCol( "FG_TRANSPORT", "운송방법", 100 );

            _flexPROJECTL.SetCol( "RT_EXCH", "환율", 100 );
            _flexPROJECTL.SetCol( "NM_SO", "수주형태", 100 );
            _flexPROJECTL.SetCol( "NO_PROJECT", "프로젝트코드", 100 );

            _flexPROJECTL.SetCol( "FG_TAXP", "계산서처리", 100 );
            _flexPROJECTL.SetCol( "FG_BILL", "결재방법", 100 );

            _flexPROJECTL.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, 160);
            _flexPROJECTL.SetCol("NM_ITEM_PARTNER", "발주품번", 120, 160);

            _flexPROJECTL.SetCol("GI_PARTNER", "납품처코드", 120, 160);
            _flexPROJECTL.SetCol("GI_NAME", "납품처명", 120, 160);
            _flexPROJECTL.SetCol("NO_EMP", "수주담당자", 100);
            _flexPROJECTL.SetCol("NM_KOR", "수주담당자명", 120);

            _flexPROJECTL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            dp수주일자FROM.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            dp수주일자FROM.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp수주일자FROM.Text = MainFrameInterface.GetStringFirstDayInMonth;

            dp수주일자TO.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            dp수주일자TO.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp수주일자TO.Text = MainFrameInterface.GetStringToday;


            dp납기일자FROM.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            dp납기일자FROM.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp납기일자FROM.Text = MainFrameInterface.GetStringFirstDayInMonth;

            dp납기일자TO.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            dp납기일자TO.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp납기일자TO.Text = MainFrameInterface.GetStringToday;

            DataSet g_dsCombo = GetComboData( "N;MA_PLANT", "S;SA_B000016", "N;MA_B000005", "S;TR_IM00008", "S;SA_B000017", "S;SA_B000002" );

            cbo공장.DataSource = g_dsCombo.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            cbo수주상태.DataSource = g_dsCombo.Tables[1];
            cbo수주상태.DisplayMember = "NAME";
            cbo수주상태.ValueMember = "CODE";

            _flexNOL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexPARTL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexITEML.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexDTL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexSOL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexGRPL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexPROJECTL.SetDataMap("STA_SO", g_dsCombo.Tables[1], "CODE", "NAME");

            _flexNOH.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexPARTL.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexITEML.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexDTL.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexSOL.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexGRPL.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexPROJECTL.SetDataMap("CD_EXCH", g_dsCombo.Tables[2], "CODE", "NAME");

            _flexNOH.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexPARTL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexITEML.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexDTL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexSOL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexGRPL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );
            _flexPROJECTL.SetDataMap( "FG_TRANSPORT", g_dsCombo.Tables [3], "CODE", "NAME" );


            _flexNOH.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexPARTL.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexITEML.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexDTL.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexSOL.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexGRPL.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );
            _flexPROJECTL.SetDataMap( "FG_TAXP", g_dsCombo.Tables [4], "CODE", "NAME" );


            _flexNOH.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexPARTL.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexITEML.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexDTL.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexSOL.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexGRPL.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );
            _flexPROJECTL.SetDataMap( "FG_BILL", g_dsCombo.Tables [5], "CODE", "NAME" );

            cbo공장.SelectedValue = LoginInfo.CdPlant;
        }

        #endregion

        #endregion

        #region ♥ 메인버튼 클릭

        #region -> Check

        public bool Check()
        {
            if (dp수주일자FROM.Text == string.Empty || dp수주일자FROM.Text == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl수주일자.Text);
                dp수주일자FROM.Focus();
                return false;
            }

            if (dp수주일자TO.Text == string.Empty || dp수주일자TO.Text == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl수주일자.Text);
                dp수주일자TO.Focus();
                return false;
            }

            if (chkBox_so.Checked == false && chkBox_due.Checked == false )
            {
                ShowMessage(" 수주기간 혹은 납기일자 둘중하나이상 반드시 선택해야합니다.");
                chkBox_so.Focus();
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

                string 공장     = string.Empty;
                string 수주상태 = string.Empty;

                //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별
                string 탭구분   = tabControlExt1.SelectedIndex.ToString();

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if (cbo수주상태.SelectedValue != null)
                    수주상태 = cbo수주상태.SelectedValue.ToString();

                String 수주기간chk = string.Empty;
                if (chkBox_so.Checked == false )
                {
                    수주기간chk = "N";
                }
                else
                {
                    수주기간chk = "Y";
                }

                String 납기기간chk = string.Empty;
                if (chkBox_due.Checked == false)
                {
                    납기기간chk = "N";
                }
                    else
                {
                    납기기간chk = "Y";
                }

                DataTable dt = _biz.Search(dp수주일자FROM.Text
                                         , dp수주일자TO.Text
                                         , 공장
                                         , bp거래처.CodeValue
                                         , bp영업그룹.CodeValue
                                         , bp담당자.CodeValue
                                         , bp수주형태.CodeValue
                                         , 수주상태
                                         , 탭구분
                                         , 수주기간chk
                                         , 납기기간chk
                                         , dp납기일자FROM.Text
                                         , dp납기일자TO.Text
                                         );

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
                    _flexPROJECTH.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    return;
                }
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
            //base.OnToolBarPrintButtonClicked(sender, e);

            try
            {                
                //this.ShowMessage("1111");
                
                string 메뉴ID = string.Empty;
                string 메뉴명 = string.Empty;

                if ( this.tabControlExt1.SelectedIndex == 0 )
                {
                    메뉴ID = "R_SA_SOSCH_1";
                    메뉴명 = "수주현황-수주번호별";
                    _flexH = _flexNOH;
                    _flexD = _flexNOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    메뉴ID = "R_SA_SOSCH_2";
                    메뉴명 = "수주현황-거래처별";
                    _flexH = _flexPARTH;
                    _flexD = _flexPARTL;
                }
                else if (this.tabControlExt1.SelectedIndex == 2)
                {
                    메뉴ID = "R_SA_SOSCH_3";
                    메뉴명 = "수주현황-품목코드별";
                    _flexH = _flexITEMH;
                    _flexD = _flexITEML;
                }
                else if (this.tabControlExt1.SelectedIndex == 3)
                {
                    메뉴ID = "R_SA_SOSCH_4";
                    메뉴명 = "수주현황-수주일자별";
                    _flexH = _flexDTH;
                    _flexD = _flexDTL;
                }
                else if (this.tabControlExt1.SelectedIndex == 4)
                {
                    메뉴ID = "R_SA_SOSCH_5";
                    메뉴명 = "수주현황-수주형태별";
                    _flexH = _flexSOH;
                    _flexD = _flexSOL;
                }
                else if (this.tabControlExt1.SelectedIndex == 5)
                {
                    메뉴ID = "R_SA_SOSCH_6";
                    메뉴명 = "수주현황-영업그룹별";
                    _flexH = _flexGRPH;
                    _flexD = _flexGRPL;
                }
                else if (this.tabControlExt1.SelectedIndex == 6)
                {
                    메뉴ID = "R_SA_SOSCH_7";
                    메뉴명 = "수주현황-프로젝트별";
                    _flexH = _flexPROJECTH;
                    _flexD = _flexPROJECTL;
                }

                string No_PK_Multi = string.Empty; string No_PK_Multi_SO = string.Empty;

                DataRow[] ldt_Report = _flexH.DataTable.Select("S = 'Y'");

                if (ldt_Report == null || ldt_Report.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    switch (메뉴명)
                    {
                        case "수주현황-수주번호별":

                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "NO_SO"].ToString() + "|";
                            }

                            break;

                        case "수주현황-거래처별":

                            //for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            //{
                            //    if (_flexH[i, "S"].ToString() == "Y")
                            //        No_PK_Multi += _flexH[i, "CD_PARTNER"].ToString() + "|";
                            //}

                            //DataRow[] dr = _flexD.DataTable.Select("CD_PARTNER = '" + _flexH["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                            //foreach(DataRow row in dr)
                            //{
                            //    No_PK_Multi_SO += row["NO_SO"].ToString() + "|";
                            //}

                            DataRow[] drH = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                            foreach(DataRow rowH in drH)
                            {
                                No_PK_Multi += rowH["CD_PARTNER"].ToString() + "|";

                                DataRow[] drL = _flexD.DataTable.Select("CD_PARTNER = '" + rowH["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                                foreach(DataRow rowL in drL)
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

                        case "수주현황-프로젝트별":

                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH[i, "S"].ToString() == "Y")
                                    No_PK_Multi += _flexH[i, "NO_PROJECT"].ToString() + "|";
                            }

                            break;


                        default:
                            break;

                    }
                }

                

                if (!_flexH.HasNormalRow && _flexD.HasNormalRow)
                    return;

         

                ReportHelper rptHelper = new ReportHelper(메뉴ID, 메뉴명);

                rptHelper.가로출력();

                string 공장 = string.Empty;
                string 수주상태 = string.Empty;

                string 탭구분 = tabControlExt1.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if (cbo수주상태.SelectedValue != null)
                    수주상태 = cbo수주상태.SelectedValue.ToString();

                String 수주기간chk = string.Empty;
                if (chkBox_so.Checked == false )
                {
                    수주기간chk = "N";
                }
                else
                {
                    수주기간chk = "Y";
                }

                String 납기기간chk = string.Empty;
                if (chkBox_due.Checked == false)
                {
                    납기기간chk = "N";
                }
                    else
                {
                    납기기간chk = "Y";
                }
                              
                try
                {

                    P_SA_SOSCH1_PRINT print = new P_SA_SOSCH1_PRINT(메뉴ID, 메뉴명, true);

                    print.수주일자fr = dp수주일자FROM.Text; 
                    print.수주일자to = dp수주일자TO.Text;
                    print.공장 = 공장; 
                    print.bp거래처 = bp거래처.CodeValue;
                    print.bp영업그룹 = bp영업그룹.CodeValue; 
                    print.bp담당자 = bp담당자.CodeValue;
                    print.bp수주형태 = bp수주형태.CodeValue; 
                    print.수주상태 = 수주상태;
                    print.멀티키 = No_PK_Multi; 
                    print.탭 = 탭구분; 
                    print.멀티수주번호 = No_PK_Multi_SO;

                    print.수주기간chk  = 수주기간chk;
                    print.납기기간chk  = 납기기간chk;
                    print.납기일자FROM = dp납기일자FROM.Text;
                    print.납기일자TO   = dp납기일자TO.Text;

                     print.n_dtSofr  = dp수주일자FROM.Text;  
                     print.n_dtSoto  = dp수주일자TO.Text;
                     print.n_Plant   = cbo공장.Text;
                     print.n_bpPartner = bp거래처.CodeName.ToString();
                     print.n_bpSaleGRP = bp영업그룹.CodeName.ToString();
                     print.n_Pcharge   = bp담당자.CodeName.ToString();
                     print.n_SoState = cbo수주상태.Text;
                     print.n_SoType = bp수주형태.CodeName;



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
                string 수주상태 = string.Empty;
                string 탭구분 = tabControlExt1.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 수주일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if (cbo수주상태.SelectedValue != null)
                    수주상태 = cbo수주상태.SelectedValue.ToString();

                String 수주기간chk;
                if (chkBox_so.Checked == false)
                {
                    수주기간chk = "N";
                }
                else
                {
                    수주기간chk = "Y";
                }

                String 납기기간chk;
                if (chkBox_due.Checked == false)
                {
                    납기기간chk = "N";
                }
                else
                {
                    납기기간chk = "Y";
                }

                switch (flex.Name)
                {
                    case "_flexNOH":
                        Key = _flexNOH[e.NewRange.r1, "NO_SO"].ToString();
                        Filter = "NO_SO = '" + Key + "'";

                        //DetailQueryNeed 한번 가져왔다고 해서 로우체인지할때 다시 dt를 안가져 오니까~ dt == null 되버린다. 
                        //그래서 원 취지는 다시 안가져 올라고 했다고 하더라도 엑셀 출력하고 싶음 그냥 가져와라~ 알긋나~
                        //if (_flexNOH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장, bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );

                        //원래는 이렇게 하면 나쁜놈인데 ~ 지금 바빠서 일단 엑셀에서 가져가야 하는 라인 그리드 데이터가 필요하다 
                        //그래서 전역 변수 ExcelDt로 가지고 있다가 엑셀에서 출력해줄때만 재사용 해줄것이다.
                        ExcelDt = dt;

                        _flexNOL.Binding = dt;
                        
                        //_flexNOL.BindingAdd(dt, Filter);
                        //_flexNOH.DetailQueryNeed = false;
                        break;
                    case "_flexPARTH":
                        Key = _flexPARTH[e.NewRange.r1, "CD_PARTNER"].ToString();
                        Filter = "CD_PARTNER = '" + Key + "'";
                        
                        if (_flexPARTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장, bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );
                        
                        _flexPARTL.BindingAdd(dt, Filter);
                        _flexPARTH.DetailQueryNeed = false;
                        break;
                    case "_flexITEMH":
                        Key = _flexITEMH[e.NewRange.r1, "CD_ITEM"].ToString();
                        Filter = "CD_ITEM = '" + Key + "'";
                        
                        if (_flexITEMH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장
                                , bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );
                        
                        _flexITEML.BindingAdd(dt, Filter);
                        _flexITEMH.DetailQueryNeed = false;
                        break;
                    case "_flexDTH":
                        Key = _flexDTH[e.NewRange.r1, "DT_SO"].ToString();
                        Filter = "DT_SO = '" + Key + "'";
                        
                        if (_flexDTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장
                                , bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );
                        
                        _flexDTL.BindingAdd(dt, Filter);
                        _flexDTH.DetailQueryNeed = false;
                        break;
                    case "_flexSOH":
                        Key = _flexSOH[e.NewRange.r1, "TP_SO"].ToString();
                        Filter = "TP_SO = '" + Key + "'";
                        
                        if (_flexSOH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장
                                , bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );
                        
                        _flexSOL.BindingAdd(dt, Filter);
                        _flexSOH.DetailQueryNeed = false;
                        break;
                    case "_flexGRPH":
                        Key = _flexGRPH[e.NewRange.r1, "CD_SALEGRP"].ToString();
                        Filter = "CD_SALEGRP = '" + Key + "'";
                        
                        if (_flexGRPH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장
                                , bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );
                        
                        _flexGRPL.BindingAdd(dt, Filter);
                        _flexGRPH.DetailQueryNeed = false;
                        break;
                    case "_flexPROJECTH":
                        Key = _flexPROJECTH[e.NewRange.r1, "NO_PROJECT"].ToString();
                        Filter = "NO_PROJECT = '" + Key + "'";

                        if (_flexPROJECTH.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key
                                , dp수주일자FROM.Text
                                , dp수주일자TO.Text
                                , 공장
                                , bp거래처.CodeValue
                                , bp영업그룹.CodeValue
                                , bp담당자.CodeValue
                                , bp수주형태.CodeValue
                                , 수주상태
                                , 탭구분
                                , 수주기간chk
                                , 납기기간chk
                                , dp납기일자FROM.Text
                                , dp납기일자TO.Text
                                );

                        _flexPROJECTL.BindingAdd(dt, Filter);
                        _flexPROJECTH.DetailQueryNeed = false;
                        break;
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
            string fileName  = "PROFORMA INVOICE";      //엑셀파일명
            string sheetName = "PROFORMA INVOICE";      //시트이름

            //프리폼 데이터 (공통으로 들어가는 부분의 TEXT(data)출력)
            string textData = "";
            
            //그리드형테를 데이터 테이블형태로 변환
            //DataTable dt01 = GetDataTable(_flexNOH.DataTable);
            //활성화된 로우를 추출하여 데이터 테이블 형태로 띵겨준당...
            DataTable dt01 = _flexNOH.DataTable.Clone();
            dt01.Rows.Add(_flexNOH.DataTable.Rows[_flexNOH.Row - _flexNOH.Rows.Fixed].ItemArray);

            DataTable[] dts = new DataTable[2];
            dts[0] = dt01;
            dts[1] = ExcelDt;

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

        #endregion

        #region ♥ 기타 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
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

            string 멀티키 = string.Empty; 
            string 메뉴ID = string.Empty; 
            string 메뉴명 = string.Empty;

            try
            {
                for(int i = _flexNOH.Rows.Fixed;i < _flexNOH.Rows.Count;i++)
                {
                    if(_flexNOH[i, "S"].ToString() == "Y")
                        멀티키 += _flexNOH[i, "NO_SO"].ToString() + "|";
                }

                if(!_flexNOH.HasNormalRow && !_flexNOL.HasNormalRow)
                    return;

          
                string 공장 = string.Empty;
                string 수주상태 = string.Empty;

                if(cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if(cbo수주상태.SelectedValue != null)
                    수주상태 = cbo수주상태.SelectedValue.ToString();

             
                    메뉴ID = "R_SA_SOSCH_0";
                    메뉴명 = "수주현황-수주번호별";

                    if (멀티키 == "")
                        멀티키 = _flexNOH["NO_SO"].ToString() + "|";
                    DataTable dt = _biz.수주서출력(
                                                    dp수주일자FROM.Text, dp수주일자TO.Text,
                                                    공장, bp거래처.CodeValue, bp영업그룹.CodeValue, bp담당자.CodeValue,
                                                    bp수주형태.CodeValue, 수주상태, 멀티키
                                                  );

                    P_SA_SOSCH1_PRINT print = new P_SA_SOSCH1_PRINT("R_SA_SOSCH_0", "수주현황-수주번호별", true);

                    print.수주일자fr = dp수주일자FROM.Text; print.수주일자to = dp수주일자TO.Text;
                    print.공장 = 공장; print.bp거래처 = bp거래처.CodeValue;
                    print.bp영업그룹 = bp영업그룹.CodeValue; print.bp담당자 = bp담당자.CodeValue;
                    print.bp수주형태 = bp수주형태.CodeValue; print.수주상태 = 수주상태;
                    print.멀티키 = 멀티키; print.ShowPrintDialog();
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);

            }
        }

        #endregion

        #region -> 수주서출력버턴 활성화

        private void 수주서출력버튼활성화_Click(object sender, EventArgs e)
        {
            try
            {
                string 탭구분 = tabControlExt1.SelectedIndex.ToString();

                if(탭구분 == "0")
                    btn수주서출력.Visible = true;
                else
                    btn수주서출력.Visible = false;
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #endregion
    }
}

