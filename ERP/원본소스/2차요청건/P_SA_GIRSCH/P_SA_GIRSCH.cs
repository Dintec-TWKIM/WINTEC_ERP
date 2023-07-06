using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;
using Duzon.ERPU;

namespace sale
{
    // **************************************
    // 작   성   자 : 오성영
    // 재 작  성 일 : 2007-08-23
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 현황
    // 페 이 지  명 : 납품의뢰현황
    // 프로젝트  명 : P_SA_GIRSCH
    // **************************************
    public partial class P_SA_GIRSCH : PageBase
    {
        #region ♥ 멤버필드

        private P_SA_GIRSCH_BIZ _biz = new P_SA_GIRSCH_BIZ();

        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;

        #endregion

        #region ♥ 초기화

        #region -> 생성자

        public P_SA_GIRSCH()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flex의뢰번호H, _flex의뢰번호L, _flex품목코드H, _flex품목코드L, _flex거래처H, _flex거래처L, _flex영업그룹H, _flex영업그룹L, _flex의뢰일자H, _flex의뢰일자L, _flex출하형태H, _flex출하형태L, _flex프로젝트H, _flex프로젝트L, _flex품목집계 };
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {

            InitGrid의뢰번호H();
            InitGrid의뢰번호L();

            InitGrid품목코드H();
            InitGrid품목코드L();

            InitGrid거래처H();
            InitGrid거래처L();

            InitGrid영업그룹H();
            InitGrid영업그룹L();

            InitGrid의뢰일자H();
            InitGrid의뢰일자L();

            InitGrid출하형태H();
            InitGrid출하형태L();

            InitGrid프로젝트H();
            InitGrid프로젝트L();

            InitGrid품목집계();


            _flex의뢰번호H.DetailGrids = new FlexGrid[] { _flex의뢰번호L };
            _flex품목코드H.DetailGrids = new FlexGrid[] { _flex품목코드L };
            _flex거래처H.DetailGrids = new FlexGrid[] { _flex거래처L };
            _flex영업그룹H.DetailGrids = new FlexGrid[] { _flex영업그룹L };
            _flex의뢰일자H.DetailGrids = new FlexGrid[] { _flex의뢰일자L };
            _flex출하형태H.DetailGrids = new FlexGrid[] { _flex출하형태L };
            _flex프로젝트H.DetailGrids = new FlexGrid[] { _flex프로젝트L };
        }

        #endregion

        #region -> InitGrid의뢰번호H

        private void InitGrid의뢰번호H()
        {
            _flex의뢰번호H.BeginSetting(1, 1, false);
            _flex의뢰번호H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex의뢰번호H.SetCol("NO_GIR", "의뢰번호", 120);
            _flex의뢰번호H.SetCol("DT_GIR", "의뢰일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰번호H.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex의뢰번호H.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex의뢰번호H.SetCol("CD_PARTNER", "거래처코드", 120);
            _flex의뢰번호H.SetCol("LN_PARTNER", "거래처명", 140);
            _flex의뢰번호H.SetCol("NM_PLANT", "출하의뢰공장", 100);
            _flex의뢰번호H.SetCol("NM_RETURN", "출고구분", 100);
            _flex의뢰번호H.SetCol("DC_RMK", "비고", 180);
            _flex의뢰번호H.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex의뢰번호H.SettingVersion = "1.0.0.2";
            _flex의뢰번호H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex의뢰번호H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid의뢰번호L

        private void InitGrid의뢰번호L()
        {
            _flex의뢰번호L.BeginSetting(1, 1, false);
            _flex의뢰번호L.SetCol("CD_ITEM", "품목코드", 100);
            _flex의뢰번호L.SetCol("NM_ITEM", "품목명", 120);
            _flex의뢰번호L.SetCol("STND_ITEM", "규격", 80);
            _flex의뢰번호L.SetCol("UNIT", "단위", 80);
            _flex의뢰번호L.SetCol("NM_SL", "출하창고", 90);
            _flex의뢰번호L.SetCol("NM_SO", "수주형태", 90);
            _flex의뢰번호L.SetCol("NM_BUSI", "거래구분", 90);
            _flex의뢰번호L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰번호L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰번호L.SetCol("NM_EXCH", "환종", 100);
            _flex의뢰번호L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex의뢰번호L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex의뢰번호L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex의뢰번호L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex의뢰번호L.SetCol("NO_SO", "수주번호", 100);
            _flex의뢰번호L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex의뢰번호L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex의뢰번호L.SetCol("NM_TP_GI", "출하형태", 150);
            _flex의뢰번호L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰번호L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex의뢰번호L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex의뢰번호L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex의뢰번호L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex의뢰번호L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex의뢰번호L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex의뢰번호L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰번호L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex의뢰번호L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex의뢰번호L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex의뢰번호L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰번호L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex의뢰번호L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰번호L.SettingVersion = "1.0.0.7";
            _flex의뢰번호L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            /* NO_GIR, CD_EXCH, QT_GIR_IM, STA_GIR, TP_BUSI, NM_RETURN, NM_STA_GIR, CD_PLANT - 기존 소스*/

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex의뢰번호L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid품목코드H

        private void InitGrid품목코드H()
        {
            _flex품목코드H.BeginSetting(1, 1, false);
            _flex품목코드H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex품목코드H.SetCol("CD_ITEM", "품목코드", 100);
            _flex품목코드H.SetCol("NM_ITEM", "품목명", 120);
            _flex품목코드H.SetCol("STND_ITEM", "규격", 100);
            _flex품목코드H.SetCol("UNIT_IM", "단위", 100);
            _flex품목코드H.SettingVersion = "1.0.0.1";
            _flex품목코드H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex품목코드H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid품목코드L

        private void InitGrid품목코드L()
        {
            _flex품목코드L.BeginSetting(1, 1, false);
            _flex품목코드L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex품목코드L.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex품목코드L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex품목코드L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex품목코드L.SetCol("QT_GIR", "의뢰수량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목코드L.SetCol("QT_GI", "출하수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목코드L.SetCol("NM_EXCH", "환종", 90);
            _flex품목코드L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex품목코드L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex품목코드L.SetCol("AM_GIR", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex품목코드L.SetCol("AM_GIRAMT", "원화금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex품목코드L.SetCol("NO_SO", "수주번호", 100);
            _flex품목코드L.SetCol("SEQ_SO", "항번", 100);
            _flex품목코드L.SetCol("NM_RETURN", "출고구분", 100);
            _flex품목코드L.SetCol("NM_TP_GI", "출하형태", 100);
            _flex품목코드L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex품목코드L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex품목코드L.SetCol("NM_SL", "출하창고", 100);
            _flex품목코드L.SetCol("NM_SO", "수주형태", 90);
            _flex품목코드L.SetCol("NM_BUSI", "거래구분", 90);
            _flex품목코드L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex품목코드L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex품목코드L.SetCol("CD_PARTNER", "거래처코드", 120);
            _flex품목코드L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex품목코드L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex품목코드L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex품목코드L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex품목코드L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex품목코드L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex품목코드L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex품목코드L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex품목코드L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex품목코드L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex품목코드L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목코드L.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex품목코드L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex품목코드L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목코드L.SettingVersion = "1.0.0.7";
            _flex품목코드L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex품목코드L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid거래처H

        private void InitGrid거래처H()
        {
            _flex거래처H.BeginSetting(1, 1, false);
            _flex거래처H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex거래처H.SetCol("CD_PARTNER", "거래처코드", 100);
            _flex거래처H.SetCol("LN_PARTNER", "거래처명", 120);
            _flex거래처H.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex거래처H.SettingVersion = "1.0.0.2";
            _flex거래처H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex거래처H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid거래처L

        private void InitGrid거래처L()
        {
            _flex거래처L.BeginSetting(1, 1, false);
            _flex거래처L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex거래처L.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처L.SetCol("CD_ITEM", "품목코드", 100);
            _flex거래처L.SetCol("NM_ITEM", "품목명", 120);
            _flex거래처L.SetCol("STND_ITEM", "규격", 80);
            _flex거래처L.SetCol("UNIT", "단위", 80);
            _flex거래처L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처L.SetCol("NM_EXCH", "환종", 100);
            _flex거래처L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex거래처L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex거래처L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex거래처L.SetCol("NO_SO", "수주번호", 100);
            _flex거래처L.SetCol("SEQ_SO", "항번", 100);
            _flex거래처L.SetCol("NM_RETURN", "출고구분", 100);
            _flex거래처L.SetCol("NM_TP_GI", "출하형태", 100);
            _flex거래처L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex거래처L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex거래처L.SetCol("NM_SL", "출하창고", 100);
            _flex거래처L.SetCol("NM_SO", "수주형태", 90);
            _flex거래처L.SetCol("NM_BUSI", "거래구분", 90);
            _flex거래처L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex거래처L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex거래처L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex거래처L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex거래처L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex거래처L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex거래처L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex거래처L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex거래처L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex거래처L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex거래처L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex거래처L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex거래처L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex거래처L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex거래처L.SettingVersion = "1.0.0.7";
            _flex거래처L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex거래처L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid영업그룹H

        private void InitGrid영업그룹H()
        {
            _flex영업그룹H.BeginSetting(1, 1, false);
            _flex영업그룹H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex영업그룹H.SetCol("CD_SALEGRP", "영업그룹코드", 100);
            _flex영업그룹H.SetCol("NM_SALEGRP", "영업그룹명", 120);
            _flex영업그룹H.SettingVersion = "1.0.0.1";
            _flex영업그룹H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex영업그룹H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid영업그룹L

        private void InitGrid영업그룹L()
        {
            _flex영업그룹L.BeginSetting(1, 1, false);
            _flex영업그룹L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex영업그룹L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex영업그룹L.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex영업그룹L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업그룹L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업그룹L.SetCol("NM_EXCH", "환종", 100);
            _flex영업그룹L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex영업그룹L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex영업그룹L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex영업그룹L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex영업그룹L.SetCol("NO_SO", "수주번호", 100);
            _flex영업그룹L.SetCol("SEQ_SO", "항번", 100);
            _flex영업그룹L.SetCol("NM_RETURN", "출고구분", 100);
            _flex영업그룹L.SetCol("NM_TP_GI", "출하형태", 100);
            _flex영업그룹L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex영업그룹L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex영업그룹L.SetCol("NM_SL", "출하창고", 100);
            _flex영업그룹L.SetCol("NM_SO", "수주형태", 90);
            _flex영업그룹L.SetCol("NM_BUSI", "거래구분", 90);
            _flex영업그룹L.SetCol("CD_ITEM", "품목코드", 100);
            _flex영업그룹L.SetCol("NM_ITEM", "품목명", 120);
            _flex영업그룹L.SetCol("STND_ITEM", "규격", 80);
            _flex영업그룹L.SetCol("UNIT", "단위", 80);
            _flex영업그룹L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex영업그룹L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex영업그룹L.SetCol("CD_PARTNER", "거래처코드", 120);
            _flex영업그룹L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex영업그룹L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex영업그룹L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex영업그룹L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex영업그룹L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex영업그룹L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex영업그룹L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex영업그룹L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex영업그룹L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex영업그룹L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex영업그룹L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업그룹L.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex영업그룹L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex영업그룹L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex영업그룹L.SettingVersion = "1.0.0.7";
            _flex영업그룹L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex영업그룹L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid의뢰일자H

        private void InitGrid의뢰일자H()
        {
            _flex의뢰일자H.BeginSetting(1, 1, false);
            _flex의뢰일자H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex의뢰일자H.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰일자H.SettingVersion = "1.0.0.1";
            _flex의뢰일자H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex의뢰일자H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

        }

        #endregion

        #region -> InitGrid의뢰일자L

        private void InitGrid의뢰일자L()
        {
            _flex의뢰일자L.BeginSetting(1, 1, false);
            _flex의뢰일자L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex의뢰일자L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex의뢰일자L.SetCol("CD_PARTNER", "거래처코드", 120);
            _flex의뢰일자L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex의뢰일자L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex의뢰일자L.SetCol("CD_ITEM", "품목코드", 100);
            _flex의뢰일자L.SetCol("NM_ITEM", "품목명", 120);
            _flex의뢰일자L.SetCol("STND_ITEM", "규격", 80);
            _flex의뢰일자L.SetCol("UNIT", "단위", 80);
            _flex의뢰일자L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰일자L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰일자L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰일자L.SetCol("NM_EXCH", "환종", 100);
            _flex의뢰일자L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex의뢰일자L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex의뢰일자L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex의뢰일자L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex의뢰일자L.SetCol("NO_SO", "수주번호", 100);
            _flex의뢰일자L.SetCol("SEQ_SO", "항번", 100);
            _flex의뢰일자L.SetCol("NM_RETURN", "출고구분", 100);
            _flex의뢰일자L.SetCol("NM_TP_GI", "출하형태", 100);
            _flex의뢰일자L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex의뢰일자L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex의뢰일자L.SetCol("NM_SL", "출하창고", 100);
            _flex의뢰일자L.SetCol("NM_SO", "수주형태", 90);
            _flex의뢰일자L.SetCol("NM_BUSI", "거래구분", 90);
            _flex의뢰일자L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex의뢰일자L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex의뢰일자L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex의뢰일자L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex의뢰일자L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex의뢰일자L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex의뢰일자L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex의뢰일자L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex의뢰일자L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex의뢰일자L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex의뢰일자L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰일자L.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex의뢰일자L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex의뢰일자L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex의뢰일자L.SettingVersion = "1.0.0.7";
            _flex의뢰일자L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex의뢰일자L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid출하형태H

        private void InitGrid출하형태H()
        {
            _flex출하형태H.BeginSetting(1, 1, false);
            _flex출하형태H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex출하형태H.SetCol("TP_GI", "출하형태", 100);
            _flex출하형태H.SetCol("NM_QTIOTP", "출하형태", 120);
            _flex출하형태H.SettingVersion = "1.0.0.1";
            _flex출하형태H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex출하형태H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid출하형태L

        private void InitGrid출하형태L()
        {
            _flex출하형태L.BeginSetting(1, 1, false);
            _flex출하형태L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex출하형태L.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex출하형태L.SetCol("CD_ITEM", "품목코드", 100);
            _flex출하형태L.SetCol("NM_ITEM", "품목명", 120);
            _flex출하형태L.SetCol("STND_ITEM", "규격", 80);
            _flex출하형태L.SetCol("UNIT", "단위", 80);
            _flex출하형태L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex출하형태L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex출하형태L.SetCol("NM_EXCH", "환종", 100);
            _flex출하형태L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex출하형태L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex출하형태L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex출하형태L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex출하형태L.SetCol("NO_SO", "수주번호", 100);
            _flex출하형태L.SetCol("SEQ_SO", "항번", 100);
            _flex출하형태L.SetCol("NM_RETURN", "출고구분", 100);
            _flex출하형태L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex출하형태L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex출하형태L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex출하형태L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex출하형태L.SetCol("CD_PARTNER", "거래처코드", 120);
            _flex출하형태L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex출하형태L.SetCol("NM_SL", "출하창고", 100);
            _flex출하형태L.SetCol("NM_SO", "수주형태", 90);
            _flex출하형태L.SetCol("NM_BUSI", "거래구분", 90);
            _flex출하형태L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex출하형태L.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flex출하형태L.SetCol("DC_RMK", "비고", 180);
            _flex출하형태L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex출하형태L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex출하형태L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex출하형태L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex출하형태L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex출하형태L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex출하형태L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex출하형태L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex출하형태L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex출하형태L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex출하형태L.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex출하형태L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex출하형태L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex출하형태L.SettingVersion = "1.0.0.7";
            _flex출하형태L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex출하형태L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid프로젝트H

        private void InitGrid프로젝트H()
        {
            _flex프로젝트H.BeginSetting(1, 1, false);
            _flex프로젝트H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex프로젝트H.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex프로젝트H.SetCol("NM_PROJECT", "프로젝트명", 120);
            _flex프로젝트H.SettingVersion = "1.0.0.1";
            _flex프로젝트H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex프로젝트H.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGrid프로젝트L

        private void InitGrid프로젝트L()
        {
            _flex프로젝트L.BeginSetting(1, 1, false);
            _flex프로젝트L.SetCol("NO_GIR", "의뢰번호", 120);
            _flex프로젝트L.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex프로젝트L.SetCol("CD_ITEM", "품목코드", 100);
            _flex프로젝트L.SetCol("NM_ITEM", "품목명", 120);
            _flex프로젝트L.SetCol("STND_ITEM", "규격", 80);
            _flex프로젝트L.SetCol("UNIT", "단위", 80);
            _flex프로젝트L.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex프로젝트L.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex프로젝트L.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex프로젝트L.SetCol("NM_EXCH", "환종", 100);
            _flex프로젝트L.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex프로젝트L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex프로젝트L.SetCol("AM_GIR", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex프로젝트L.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex프로젝트L.SetCol("NO_SO", "수주번호", 100);
            _flex프로젝트L.SetCol("SEQ_SO", "항번", 100);
            _flex프로젝트L.SetCol("NM_SALEGRP", "영업그룹", 100);
            _flex프로젝트L.SetCol("NM_RETURN", "출고구분", 100);
            _flex프로젝트L.SetCol("NM_TP_GI", "출하형태", 100);
            _flex프로젝트L.SetCol("FG_TRANSPORT", "운송방법", 100);
            _flex프로젝트L.SetCol("CLS_ITEM", "계정구분", 100);
            _flex프로젝트L.SetCol("NM_SL", "출하창고", 100);
            _flex프로젝트L.SetCol("NM_SO", "수주형태", 90);
            _flex프로젝트L.SetCol("NM_BUSI", "거래구분", 90);
            _flex프로젝트L.SetCol("CD_PARTNER_GRP", "거래처그룹코드", 120);
            _flex프로젝트L.SetCol("NM_PARTNER_GRP", "거래처그룹명", 140);
            _flex프로젝트L.SetCol("CD_PARTNER", "거래처", 120);
            _flex프로젝트L.SetCol("LN_PARTNER", "거래처명", 120);
            _flex프로젝트L.SetCol("GI_PARTNER", "납품처코드", 120, false);
            _flex프로젝트L.SetCol("GI_LN_PARTNER", "납품처명", 200, false);
            _flex프로젝트L.SetCol("NM_KOR", "수주담당자", 120, false);
            _flex프로젝트L.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex프로젝트L.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex프로젝트L.SetCol("DTS_INSERT", "등록일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex프로젝트L.SetCol("ID_INSERT", "등록자", 100, false);
            _flex프로젝트L.SetCol("NM_GISL", "저장위치", 100, false);
            _flex프로젝트L.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex프로젝트L.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex프로젝트L.SetCol("NM_EMP_SALE", "거래처담당자", 100);
            _flex프로젝트L.SetCol("L_DC_RMK", "라인비고", 100, false);
            _flex프로젝트L.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex프로젝트L.SettingVersion = "1.0.0.7";
            _flex프로젝트L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flex프로젝트L.SetExceptSumCol("UM", "UM_EX");
        }

        #endregion

        #region -> InitGrid품목집계

        private void InitGrid품목집계()
        {
            _flex품목집계.BeginSetting(1, 1, false);
            _flex품목집계.SetCol("CD_ITEM", "품목코드", 100);
            _flex품목집계.SetCol("NM_ITEM", "품목명", 120);
            _flex품목집계.SetCol("STND_ITEM", "규격", 80);
            _flex품목집계.SetCol("UNIT", "단위", 80);
            _flex품목집계.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목집계.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목집계.SetCol("QT_NOTPROC", "미처리수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목집계.SetCol("AM_GIRAMT", "원화금액(의뢰)", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex품목집계.SetCol("CLS_ITEM", "계정구분", 100);
            _flex품목집계.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex품목집계.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex품목집계.SetCol("NM_GISL", "저장위치", 100, false);
            _flex품목집계.SetCol("UNIT_WEIGHT", "중량단위", false);
            _flex품목집계.SetCol("WEIGHT", "중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목집계.SetCol("QT_GI_WEIGHT", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex품목집계.SettingVersion = "1.0.0.3";
            _flex품목집계.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            dtp의뢰일자.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            dtp의뢰일자.EndDateToString = MainFrameInterface.GetStringToday;
            dtp납기일자.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
            dtp납기일자.EndDateToString = MainFrameInterface.GetStringToday;

            DataSet m_dsCombo = this.GetComboData("S;PU_C000016", "S;SA_B000028", "N;MA_PLANT", "S;PU_C000027", "N;SA_B000031", "S;MA_BIZAREA", "S;TR_IM00008", "S;MA_B000010", "S;SA_B000016");

            //거래구분ComboBox에 Add
            cbo거래구분.DataSource = m_dsCombo.Tables[0];
            cbo거래구분.ValueMember = "CODE";
            cbo거래구분.DisplayMember = "NAME";

            //처리상태ComboBox에 Add  --  SA_B000016
            cbo처리상태.DataSource = m_dsCombo.Tables[1];
            cbo처리상태.ValueMember = "CODE";
            cbo처리상태.DisplayMember = "NAME";

            //출하공장ComboBox에 Add
            cbo공장.DataSource = m_dsCombo.Tables[2];
            cbo공장.ValueMember = "CODE";
            cbo공장.DisplayMember = "NAME";

            cbo공장.SelectedValue = cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString();

            //출하구분ComboBox에 Add
            cbo출하구분.DataSource = m_dsCombo.Tables[3];
            cbo출하구분.ValueMember = "CODE";
            cbo출하구분.DisplayMember = "NAME";

            //단위선택ComboBox에 Add
            cbo단위선택.DataSource = m_dsCombo.Tables[4];
            cbo단위선택.ValueMember = "CODE";
            cbo단위선택.DisplayMember = "NAME";

            // 사업장
            cbo사업장.DataSource = m_dsCombo.Tables[5];
            cbo사업장.DisplayMember = "NAME";
            cbo사업장.ValueMember = "CODE";

            if (LoginInfo.BizAreaCode != "")
                cbo사업장.SelectedValue = LoginInfo.BizAreaCode;

            // 운송수단
            cbo운송방법.DataSource = m_dsCombo.Tables[6];
            cbo운송방법.DisplayMember = "NAME";
            cbo운송방법.ValueMember = "CODE";

            _flex의뢰번호L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex품목코드L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex거래처L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex영업그룹L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex의뢰일자L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex출하형태L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");
            _flex프로젝트L.SetDataMap("FG_TRANSPORT", m_dsCombo.Tables[6], "CODE", "NAME");


            // 계정구분
            this.cbo계정구분.DataSource = m_dsCombo.Tables[7];
            this.cbo계정구분.DisplayMember = "NAME";
            this.cbo계정구분.ValueMember = "CODE";

            _flex의뢰번호L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex품목코드L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex거래처L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex영업그룹L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex의뢰일자L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex출하형태L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex프로젝트L.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");
            _flex품목집계.SetDataMap("CLS_ITEM", m_dsCombo.Tables[7], "CODE", "NAME");

            // 수주상태
            cbo수주상태.DataSource = m_dsCombo.Tables[8];
            cbo수주상태.ValueMember = "CODE";
            cbo수주상태.DisplayMember = "NAME";

            cbo사업장.SelectedValue = cbo사업장.SelectedValue == null ? string.Empty : cbo사업장.SelectedValue.ToString();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
        }

        #endregion

        #endregion

        #region ♥ 메인버튼 클릭

        #region -> BeforeSearch

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (!chk의뢰일자.Checked && !chk납기일자.Checked)
            {
                ShowMessage("의뢰일자 혹은 납기일자 둘 중 하나는 반드시 선택해야 합니다.");
                chk의뢰일자.Focus();
                return false;
            }

            if (chk의뢰일자.Checked)
            {
                if (!Chk의뢰일자)
                    return false;
            }

            if (chk납기일자.Checked)
            {
                if (!Chk납기일자)
                    return false;
            }

            Hashtable hList = new Hashtable();
            hList.Add(cbo사업장, m_lblBizarea);
            hList.Add(cbo단위선택, m_lblSlUnit);

            return ComFunc.NullCheck(hList);
        }

        #endregion

        #region -> 조회버튼클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch())
                    return;

                string 공장 = string.Empty;
                string 사업장 = string.Empty;
                string 처리상태 = string.Empty;
                string 거래구분 = string.Empty;
                string 계정구분 = string.Empty;
                string 운송방법 = string.Empty;
                string 단위선택 = string.Empty;
                string 출하구분 = string.Empty;
                string 수주상태 = string.Empty;

                string 탭구분 = m_tabC.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 의뢰일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if (cbo사업장.SelectedValue != null)
                    사업장 = cbo사업장.SelectedValue.ToString();

                if (cbo처리상태.SelectedValue != null)
                    처리상태 = cbo처리상태.SelectedValue.ToString();

                if (cbo거래구분.SelectedValue != null)
                    거래구분 = cbo거래구분.SelectedValue.ToString();

                if (cbo계정구분.SelectedValue != null)
                    계정구분 = cbo계정구분.SelectedValue.ToString();

                if (cbo운송방법.SelectedValue != null)
                    운송방법 = cbo운송방법.SelectedValue.ToString();

                if (cbo단위선택.SelectedValue != null)
                    단위선택 = cbo단위선택.SelectedValue.ToString();

                if (cbo출하구분.SelectedValue != null)
                    출하구분 = cbo출하구분.SelectedValue.ToString();

                if (cbo수주상태.SelectedValue != null)
                    수주상태 = cbo수주상태.SelectedValue.ToString();
                

                DataTable dt = _biz.Search(
                                                dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, // 의뢰일자
                                                사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString,
                                                수주상태
                                                );

                if (탭구분 == "0")
                {
                    //DataTable dtL = _biz.SearchDetail( );
                    //_flex의뢰번호L.Binding = dtL;
                    _flex의뢰번호H.Binding = dt;
                }
                else if (탭구분 == "1")
                {

                    _flex품목코드H.Binding = dt;
                }
                else if (탭구분 == "2")
                {

                    _flex거래처H.Binding = dt;
                }
                else if (탭구분 == "3")
                {

                    _flex영업그룹H.Binding = dt;
                }
                else if (탭구분 == "4")
                {

                    _flex의뢰일자H.Binding = dt;
                }
                else if (탭구분 == "5")
                {

                    _flex출하형태H.Binding = dt;
                }
                else if (탭구분 == "6")
                {

                    _flex프로젝트H.Binding = dt;
                }
                else if (탭구분 == "7")
                    _flex품목집계.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    ToolBarPrintButtonEnabled = false;
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

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string 메뉴ID = string.Empty;
                string 메뉴명 = string.Empty;
                string 서버키 = Global.MainFrame.ServerKeyCommon.ToUpper();

                bool is의뢰번호별출고검사출력 = false;

                if (m_tabC.SelectedIndex == 0)
                {
                    메뉴ID = "R_SA_GIRSCH_0";
                    메뉴명 = "납품의뢰현황-의뢰번호";
                    _flexH = _flex의뢰번호H;
                    _flexD = _flex의뢰번호L;

                    if (서버키 == "JEDA")
                    {
                        P_SA_GIRSCH_SUB dlg = new P_SA_GIRSCH_SUB();
                        if (dlg.ShowDialog() != DialogResult.OK) return;

                        is의뢰번호별출고검사출력 = dlg.Get출고검사요청출력;
                    }
                }
                else if (m_tabC.SelectedIndex == 1)
                {
                    메뉴ID = "R_SA_GIRSCH_1";
                    메뉴명 = "납품의뢰현황-품목코드별";

                    _flexH = _flex품목코드H;
                    _flexD = _flex품목코드L;

                }
                else if (m_tabC.SelectedIndex == 2)
                {
                    메뉴ID = "R_SA_GIRSCH_2";
                    메뉴명 = "납품의뢰현황-거래처별";

                    _flexH = _flex거래처H;
                    _flexD = _flex거래처L;
                }
                else if (m_tabC.SelectedIndex == 3)
                {
                    메뉴ID = "R_SA_GIRSCH_3";
                    메뉴명 = "납품의뢰현황-영업그룹별";

                    _flexH = _flex영업그룹H;
                    _flexD = _flex영업그룹L;
                }
                else if (m_tabC.SelectedIndex == 4)
                {
                    메뉴ID = "R_SA_GIRSCH_4";
                    메뉴명 = "납품의뢰현황-의뢰일자별";

                    _flexH = _flex의뢰일자H;
                    _flexD = _flex의뢰일자L;
                }
                else if (m_tabC.SelectedIndex == 5)
                {
                    메뉴ID = "R_SA_GIRSCH_5";
                    메뉴명 = "납품의뢰현황-출하형태별";

                    _flexH = _flex출하형태H;
                    _flexD = _flex출하형태L;
                }
                else if (m_tabC.SelectedIndex == 6)
                {
                    메뉴ID = "R_SA_GIRSCH_6";
                    메뉴명 = "납품의뢰현황-프로젝트별";

                    _flexH = _flex프로젝트H;
                    _flexD = _flex프로젝트L;
                }
                else if (m_tabC.SelectedIndex == 7)
                {
                    메뉴ID = "R_SA_GIRSCH_7";
                    메뉴명 = "납품의뢰현황-품목집계";

                }
                string No_PK_Multi = "";

                if (m_tabC.SelectedIndex != 7)
                {

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
                            case "납품의뢰현황-의뢰번호":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "NO_GIR"].ToString() + "|";
                                }
                                break;

                            case "납품의뢰현황-품목코드별":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "CD_ITEM"].ToString() + "|";
                                }
                                break;
                            case "납품의뢰현황-거래처별":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "CD_PARTNER"].ToString() + "|";
                                }
                                break;
                            case "납품의뢰현황-영업그룹별":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "CD_SALEGRP"].ToString() + "|";
                                }
                                break;
                            case "납품의뢰현황-의뢰일자별":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "DT_GIR"].ToString() + "|";
                                }
                                break;
                            case "납품의뢰현황-출하형태별":
                                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                {
                                    if (_flexH[i, "S"].ToString() == "Y")
                                        No_PK_Multi += _flexH[i, "TP_GI"].ToString() + "|";
                                }
                                break;
                            case "납품의뢰현황-프로젝트별":
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

                }

                ReportHelper rptHelper = new ReportHelper(메뉴ID, 메뉴명);

                rptHelper.가로출력();

                if (m_tabC.SelectedIndex == 7)
                {
                    rptHelper.SetDataView(_flex품목집계.DataView);
                    rptHelper.Print();
                }
                else
                {
                    try
                    {
                        string 출고검사요청서출력 = is의뢰번호별출고검사출력 == true ? "Y" : "";



                        if (!chk의뢰일자.Checked && !chk납기일자.Checked)
                        {
                            ShowMessage("의뢰일자 혹은 납기일자 둘 중 하나는 반드시 선택해야 합니다.");
                            chk의뢰일자.Focus();
                            return;
                        }


                        if (chk납기일자.Checked)
                            if (!Chk납기일자)
                                return;

                        if (chk납기일자.Checked)
                            if (!Chk의뢰일자)
                                return;


                        DataTable dt = _biz.Search_req(LoginInfo.CompanyCode,     // 회사코드
                                       dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, // 의뢰일자
                                       cbo사업장.SelectedValue == null ? string.Empty : cbo사업장.SelectedValue.ToString(), // 사업장
                                       cbo거래구분.SelectedValue == null ? string.Empty : cbo거래구분.SelectedValue.ToString(), // 사업장
                                       cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString(), // 사업장
                                       bp창고.CodeValue,
                                       cbo처리상태.SelectedValue == null ? string.Empty : cbo처리상태.SelectedValue.ToString(), // 사업장
                                       cbo출하구분.SelectedValue == null ? string.Empty : cbo출하구분.SelectedValue.ToString(), // 사업장
                                       cbo단위선택.SelectedValue == null ? string.Empty : cbo단위선택.SelectedValue.ToString(), // 사업장
                                       bp수주형태.CodeValue,                                // 수주형태
                                       m_tabC.SelectedIndex.ToString(),           //  상태 
                                       cbo운송방법.SelectedValue == null ? string.Empty : cbo운송방법.SelectedValue.ToString(), // 사업장
                                       No_PK_Multi, 출고검사요청서출력, bp영업조직s.QueryWhereIn_Pipe, bp영업그룹s.QueryWhereIn_Pipe,
                                       chk의뢰일자.Checked == true ? "Y" : "N",
                                       chk납기일자.Checked == true ? "Y" : "N",
                                       dtp납기일자.StartDateToString, dtp납기일자.EndDateToString);

                        //의뢰번호별 금액의 합을 구함
                        DataTable dtSum = dt.DefaultView.ToTable(true, "NO_GIR");

                        string filter = string.Empty;
                        foreach (DataRow dr in dtSum.Rows)
                        {
                            string NO_GIR = D.GetString(dr["NO_GIR"]);
                            filter = "NO_GIR = '" + NO_GIR + "'";

                            decimal AM_GIRAMT_SUM = D.GetDecimal(dt.Compute("SUM(AM_GIRAMT)", filter));
                            decimal AM_VAT_SUM = D.GetDecimal(dt.Compute("SUM(AM_VAT)", filter));
                            decimal AM_TOT_SUM = AM_GIRAMT_SUM + AM_VAT_SUM;
                            
                            //D20130128022 - 최인성 납품의뢰 등록 sum(QT_GRIR)값 추가 요청
                            decimal QT_GIR_SUM = D.GetDecimal(dt.Compute("SUM(QT_GIR)", filter));

                            DataRow[] drs = dt.Select(filter);

                            foreach (DataRow row in drs)
                            {
                                row["AM_GIRAMT_SUM"] = AM_GIRAMT_SUM;
                                row["AM_VAT_SUM"] = AM_VAT_SUM;
                                row["AM_TOT_SUM"] = AM_TOT_SUM;
                                row["QT_GIR_SUM"] = QT_GIR_SUM;
                            }
                        }

                        //거래처별 외화금액의 합을 구함
                        DataTable dtPARTNER = dt.DefaultView.ToTable(true, "CD_PARTNER");
                        string filter1 = string.Empty;
                        foreach (DataRow dr in dtPARTNER.Rows)
                        {
                            string CD_PARTNER = D.GetString(dr["CD_PARTNER"]);
                            filter1 = "CD_PARTNER = '" + CD_PARTNER + "'";

                            decimal AM_GIR_PARTNER = D.GetDecimal(dt.Compute("SUM(AM_GIR)", filter1));

                            DataRow[] drs = dt.Select(filter1);

                            foreach (DataRow row in drs)
                            {
                                row["AM_GIR_PARTNER"] = AM_GIR_PARTNER;
                            }
                        }

                        dt.AcceptChanges();

                        //   사업장, 계정구분, 운송방법, 수주형태, 단위선택, 환종,원화금액 매핑 안됨

                        rptHelper.SetData("의뢰일자Fr", dtp의뢰일자.StartDateToString.Substring(0, 4) + "년" + dtp의뢰일자.StartDateToString.Substring(4, 2) + "월" + dtp의뢰일자.StartDateToString.Substring(6, 2) + "일");
                        rptHelper.SetData("의뢰일자To", dtp의뢰일자.EndDateToString.Substring(0, 4) + "년" + dtp의뢰일자.EndDateToString.Substring(4, 2) + "월" + dtp의뢰일자.EndDateToString.Substring(6, 2) + "일");
                        rptHelper.SetData("처리상태", cbo처리상태.Text);
                        rptHelper.SetData("출고창고", bp창고.CodeName);
                        rptHelper.SetData("거래구분", cbo거래구분.Text);
                        rptHelper.SetData("출고구분", cbo출하구분.Text);
                        rptHelper.SetData("사업장", cbo사업장.Text);
                        rptHelper.SetData("계정구분", cbo계정구분.Text);
                        rptHelper.SetData("운송방법", cbo운송방법.Text);
                        rptHelper.SetData("수주형태", bp수주형태.Text);
                        rptHelper.SetData("단위선택", cbo단위선택.Text);
                        
                        rptHelper.SetDataTable(dt, 1);
                        rptHelper.SetDataTable(dt, 2);
                        rptHelper.SetDataTable(dt, 3);
                        rptHelper.SetDataTable(dt, 4);

                        rptHelper.Print();
                    }
                    catch (Exception Ex)
                    {
                        this.MsgEnd(Ex);
                    }
                }
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }
        }

        #endregion

        #region -> 종료버튼클릭

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeExit())
                    return false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            return true;
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
                string 사업장 = string.Empty;
                string 처리상태 = string.Empty;
                string 거래구분 = string.Empty;
                string 계정구분 = string.Empty;
                string 운송방법 = string.Empty;
                string 단위선택 = string.Empty;
                string 출하구분 = string.Empty;
                string 탭구분 = m_tabC.SelectedIndex.ToString(); //0: 수주번호별, 1: 거래처별, 2: 품목별, 3: 의뢰일자별, 4: 수주형태별, 5: 영업그룹별, 6: 프로젝트별

                if (cbo공장.SelectedValue != null)
                    공장 = cbo공장.SelectedValue.ToString();

                if (cbo사업장.SelectedValue != null)
                    사업장 = cbo사업장.SelectedValue.ToString();

                if (cbo처리상태.SelectedValue != null)
                    처리상태 = cbo처리상태.SelectedValue.ToString();

                if (cbo거래구분.SelectedValue != null)
                    거래구분 = cbo거래구분.SelectedValue.ToString();

                if (cbo계정구분.SelectedValue != null)
                    계정구분 = cbo계정구분.SelectedValue.ToString();

                if (cbo운송방법.SelectedValue != null)
                    운송방법 = cbo운송방법.SelectedValue.ToString();

                if (cbo단위선택.SelectedValue != null)
                    단위선택 = cbo단위선택.SelectedValue.ToString();

                if (cbo출하구분.SelectedValue != null)
                    출하구분 = cbo출하구분.SelectedValue.ToString();


                if (!chk의뢰일자.Checked && !chk납기일자.Checked)
                {
                    ShowMessage("의뢰일자 혹은 납기일자 둘 중 하나는 반드시 선택해야 합니다.");
                    chk의뢰일자.Focus();
                    return;
                }


                if (chk납기일자.Checked)
                    if (!Chk납기일자)
                        return;

                if (chk납기일자.Checked)
                    if (!Chk의뢰일자)
                        return;


                switch (flex.Name)
                {
                    case "_flex의뢰번호H":
                        Key = _flex의뢰번호H[e.NewRange.r1, "NO_GIR"].ToString();
                        Filter = "NO_GIR = '" + Key + "'";

                        if (_flex의뢰번호H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, // 의뢰일자
                                                사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex의뢰번호L.BindingAdd(dt, Filter);
                        _flex의뢰번호H.DetailQueryNeed = false;
                        break;
                    case "_flex품목코드H":
                        Key = _flex품목코드H[e.NewRange.r1, "CD_ITEM"].ToString();
                        Filter = "CD_ITEM = '" + Key + "'";

                        if (_flex품목코드H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                    );

                        _flex품목코드L.BindingAdd(dt, Filter);
                        _flex품목코드H.DetailQueryNeed = false;
                        break;
                    case "_flex거래처H":
                        Key = _flex거래처H[e.NewRange.r1, "CD_PARTNER"].ToString();
                        Filter = "CD_PARTNER = '" + Key + "'";

                        if (_flex거래처H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex거래처L.BindingAdd(dt, Filter);
                        _flex거래처H.DetailQueryNeed = false;
                        break;
                    case "_flex영업그룹H":
                        Key = _flex영업그룹H[e.NewRange.r1, "CD_SALEGRP"].ToString();
                        Filter = "CD_SALEGRP = '" + Key + "'";

                        if (_flex영업그룹H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex영업그룹L.BindingAdd(dt, Filter);
                        _flex영업그룹H.DetailQueryNeed = false;
                        break;
                    case "_flex의뢰일자H":
                        Key = _flex의뢰일자H[e.NewRange.r1, "DT_GIR"].ToString();
                        Filter = "DT_GIR = '" + Key + "'";

                        if (_flex의뢰일자H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex의뢰일자L.BindingAdd(dt, Filter);
                        _flex의뢰일자H.DetailQueryNeed = false;
                        break;
                    case "_flex출하형태H":
                        Key = _flex출하형태H[e.NewRange.r1, "TP_GI"].ToString();
                        Filter = "TP_GI = '" + Key + "'";

                        if (_flex출하형태H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex출하형태L.BindingAdd(dt, Filter);
                        _flex출하형태H.DetailQueryNeed = false;
                        break;
                    case "_flex프로젝트H":
                        Key = _flex프로젝트H[e.NewRange.r1, "NO_PROJECT"].ToString();
                        Filter = "ISNULL(NO_PROJECT, '') = '" + Key + "'";

                        if (_flex프로젝트H.DetailQueryNeed)
                            dt = _biz.SearchDetail(Key, dtp의뢰일자.StartDateToString, dtp의뢰일자.EndDateToString, 사업장,
                                                거래구분,
                                                공장,
                                                bp창고.CodeValue,
                                                처리상태,
                                                출하구분,
                                                단위선택,
                                                bp수주형태.CodeValue,
                                                탭구분,
                                                운송방법,
                                                계정구분,
                                                bp_Emp.CodeValue,
                                                bp거래처그룹.QueryWhereIn_Pipe,
                                                bp거래처.CodeValue,
                                                bp영업조직s.QueryWhereIn_Pipe,
                                                bp영업그룹s.QueryWhereIn_Pipe,
                                                chk의뢰일자.Checked == true ? "Y" : "N",
                                                chk납기일자.Checked == true ? "Y" : "N",
                                                dtp납기일자.StartDateToString,
                                                dtp납기일자.EndDateToString
                                                );

                        _flex프로젝트L.BindingAdd(dt, Filter);
                        _flex프로젝트H.DetailQueryNeed = false;
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

        #region ♥ 기타 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        if (cbo공장.SelectedValue == null || cbo공장.SelectedValue.ToString() == string.Empty)
                        {
                            ShowMessage(공통메세지._자료가선택되어있지않습니다, lbl공장.Text);
                            cbo공장.Focus();
                            e.QueryCancel = true;
                            return;
                        }
                        e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            if (e.DialogResult == DialogResult.Cancel)
                return;

            switch (e.ControlName)
            {
                case "bp창고":
                    bp창고.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
                    bp창고.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();

                    break;
            }
        }

        #endregion

        //bool Chk의뢰일자 { get { return Checker.IsValid(dp의뢰일자FROM, dp의뢰일자TO, true, DD("의뢰일자From"), DD("의뢰일자To")); } }
        //bool Chk납기일자 { get { return Checker.IsValid(dp납기일자FROM, dp납기일자TO, true, DD("납기일자From"), DD("납기일자To")); } }
        bool Chk의뢰일자 { get { return Checker.IsValid(dtp의뢰일자, true, lbl의뢰일자.Text); } }
        bool Chk납기일자 { get { return Checker.IsValid(dtp납기일자, true, lbl납기일자.Text); } }

        #endregion
    }
}