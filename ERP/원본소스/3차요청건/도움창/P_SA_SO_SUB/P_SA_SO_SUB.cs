using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.ERPU.OLD;
using System.Text;

namespace sale
{
    // ****************************************************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-10-09
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하의뢰관리
    // 페 이 지  명 : 고객납품의뢰등록 -> 납품의뢰(수주적용) 도움창
    // 프로젝트  명 : P_SA_SO_SUB
    // ****************************************************************************
    public partial class P_SA_SO_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 멤버필드

        bool is정상수주 = true;
        string 영업그룹코드 = string.Empty;
        string 영업그룹명 = string.Empty;
        string 과세구분 = string.Empty;
        string 환종 = string.Empty;
        string 계산서처리 = string.Empty;
        string 단가유형 = string.Empty;
        string 납품처코드 = string.Empty;
        string 납품처명 = string.Empty;
        string 수불유형 = string.Empty;
        

        private P_SA_SO_SUB_BIZ _biz = new P_SA_SO_SUB_BIZ();
        DataTable _dt = new DataTable();
        DataTable dt_Append = new DataTable();  //도움창에서 이미 받아간 수주내역
        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        string flag = string.Empty;             //flag는 적용을 받은건지 추가적용을 받을건지를 알려주는 적용구분 flag 값이다. : APPEND(적용), ADDAPPEND(추가적용)
        string so_DcRmk = string.Empty;         //so_DcRmk는 수주헤더의 비고를 top 1 로 가져다가 의뢰의 헤더에 바로 셋팅해줄것인지를 가져간다.

        //영업환경설정  : 수주수량 초과허용
        private string qtso_AddAllowYN = "N";  //수주수량 초과허용 추가 2009.07.17 NJin (Default Value = "N" 으로 셋팅)
        private string Am_Recalc = "000";   //영업환경설정 : 단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

        #endregion

        #region -> 초기화

        #region -> 생성자
        public P_SA_SO_SUB(string 거래처코드,   string 거래처명,   string 출하공장코드, string 출하공장명, 
                           string 거래구분코드, string 거래구분명, string 프로젝트코드, string 프로젝트명, 
                           DataTable dts)
		{
            try
            {
                InitializeComponent();

                m_cdePlant.CodeValue = 출하공장코드;
                m_cdePlant.CodeName = 출하공장명;
                m_cdeTpBusi.CodeValue = 거래구분코드;
                m_cdeTpBusi.CodeName = 거래구분명;

                bp_PJT.CodeValue = 프로젝트코드;
                bp_PJT.CodeName = 프로젝트명;
                bp_Partner.CodeValue = 거래처코드;
                bp_Partner.CodeName = 거래처명;

                if (bp_Partner.CodeValue != string.Empty)
                {
                    bp_Partner.Enabled = false;
                    bp_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
                }


                dt_Append = dts;

                //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
                DataTable dt = _biz.search_EnvMng();

                if (dt.Rows.Count > 0)
                {
                    // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                    if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                    {
                        qtso_AddAllowYN = dt.Select("FG_TP = '000'")[0]["CD_TP"].ToString();    // 수주수량 초과허용
                    }
                }

                Am_Recalc = Sa_Global.AM_ReCalc; //단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        //아성프라텍 전용
        public P_SA_SO_SUB(string 거래처코드, string 거래처명, string 출하공장코드, string 출하공장명,
                           string 거래구분코드, string 거래구분명, string 프로젝트코드, string 프로젝트명,
                           DataTable dts, string 출고일자)
        {
            try
            {
                InitializeComponent();

                m_cdePlant.CodeValue = 출하공장코드;
                m_cdePlant.CodeName = 출하공장명;
                m_cdeTpBusi.CodeValue = 거래구분코드;
                m_cdeTpBusi.CodeName = 거래구분명;

                bp_PJT.CodeValue = 프로젝트코드;
                bp_PJT.CodeName = 프로젝트명;
                bp_Partner.CodeValue = 거래처코드;
                bp_Partner.CodeName = 거래처명;

                if (bp_Partner.CodeValue != string.Empty)
                {
                    bp_Partner.Enabled = false;
                    bp_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
                }


                dt_Append = dts;

                //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
                DataTable dt = _biz.search_EnvMng();

                if (dt.Rows.Count > 0)
                {
                    // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                    if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                    {
                        qtso_AddAllowYN = dt.Select("FG_TP = '000'")[0]["CD_TP"].ToString();    // 수주수량 초과허용
                    }
                }

                Am_Recalc = Sa_Global.AM_ReCalc; //단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

                m_mskStart.Text = 출고일자;

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();
                InitGridH();
                if (Global.MainFrame.ServerKeyCommon == "HANSU")
                {
                    InitGridL_한수();
                    InitGridITEM_HANSU();
                }
                else
                {
                    InitGridL();
                    InitGridITEM();
                }
                _flexH.DetailGrids = new FlexGrid[] { _flexL };

                if (Global.MainFrame.CurrentPageID == "P_SA_GIR" && MA.ServerKey(false, "SOLIDTECH"))
                    InitGrid_쏠리드();
                else
                    tab수주.TabPages.Remove(SOLIDTECH);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitGridH
        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_SO", "수주번호", 140);
            _flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NM_SO", "수주형태", 100);
            _flexH.SetCol("CD_EXCH", "환종", 80);
            _flexH.SetCol("NM_SALEGRP", "영업그룹", 80);
            _flexH.SetCol("NM_KOR", "담당자", 80);
            _flexH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexH.SetCol("LN_PARTNER", "거래처명", 100);
            _flexH.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexH.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexH.SetCol("DC_RMK", "비고", 200);
            _flexH.SetCol("DC_RMK1", "비고1", 200);
            _flexH.SetCol("SN_PARTNER", "거래처약칭", 120);
 
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.AfterDataRefresh += new ListChangedEventHandler(_flexH_AfterDataRefresh);
            _flexH.StartEdit += new RowColEventHandler(_flexH_StartEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.LoadUserCache("P_SA_SO_SUB_flexH");

        }
        #endregion

        #region -> InitGridL
        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM", "품목코드", 100);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 65);
            _flexL.SetCol("UNIT_SO", "단위", 65);
            _flexL.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            if (Global.MainFrame.ServerKeyCommon == "WINPLUS")
            {
                _flexL.SetCol("QT_STANDBY", "출하대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            _flexL.SetCol("QT_GIR", "의뢰적용수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NM_SL", "창고", 120, false);
            _flexL.SetCol("UNIT", "관리단위", 65, false);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("GI_PARTNER", "납품처", 120, false );
            _flexL.SetCol("LN_PARTNER", "납품처명", 120, false );
           
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 100);

            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);

            _flexL.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flexL.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flexL.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flexL.SetCol("DC_RMK4", "수주라인비고2", 150, false);
            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetCol("SEQ_PROJECT", "UNIT 항번", 120);
                _flexL.SetCol("CD_UNIT", "UNIT 코드", 120);
                _flexL.SetCol("NM_UNIT", "UNIT 명", 120);
                _flexL.SetCol("STND_UNIT", "UNIT 규격", 100);
            }

            _flexL.SettingVersion = "1.0.0.4";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetExceptSumCol("UM", "SEQ_PROJECT");
            }
            else
            {
                //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
                _flexL.SetExceptSumCol("UM");
            }

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexL.LoadUserCache("P_SA_SO_SUB_flexL");
        } 
        #endregion

        #region-> InitGridL 한수전용

        private void InitGridL_한수()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM", "제조코드", 100);
            _flexL.SetCol("NM_ITEM", "제조코드명", 120);
            _flexL.SetCol("TXT_USERDEF2", "전용코드", 100, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "전용품명", 100, false);
            _flexL.SetCol("CD_PACK", "포장단위", 100, false);
            _flexL.SetCol("NM_PACK", "포장단위명", 100, false);
            _flexL.SetCol("GI_PARTNER", "납품처", 120, false);
            _flexL.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flexL.SetCol("TXT_USERDEF1", "납품부서", 100, false);
            _flexL.SetCol("NM_PART", "납품부서명", 100, false);
            _flexL.SetCol("TXT_USERDEF4", "목적지", 100, false);

            _flexL.SetCol("STND_ITEM", "규격", false);
            _flexL.SetCol("UNIT_SO", "단위", false);
            _flexL.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
         
            _flexL.SetCol("QT_GIR", "의뢰적용수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NM_SL", "창고", 120, false);
            _flexL.SetCol("UNIT", "관리단위", 65, false);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);

            _flexL.SetCol("NM_PROJECT", "프로젝트명", 100);

            _flexL.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flexL.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flexL.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flexL.SetCol("DC_RMK4", "수주라인비고2", 150, false);
            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 100);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetCol("SEQ_PROJECT", "UNIT 항번", 120);
                _flexL.SetCol("CD_UNIT", "UNIT 코드", 120);
                _flexL.SetCol("NM_UNIT", "UNIT 명", 120);
                _flexL.SetCol("STND_UNIT", "UNIT 규격", 100);
            }

            _flexL.SettingVersion = "2.0.0.1";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetExceptSumCol("UM", "SEQ_PROJECT");
            }
            else
            {
                //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
                _flexL.SetExceptSumCol("UM");
            }

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexL.LoadUserCache("P_SA_SO_SUB_flexL");
        } 

        #endregion

        #region -> InitGridITEM
        private void InitGridITEM()
        {

            _flexITEM.BeginSetting(1, 1, false);
            _flexITEM.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexITEM.SetCol("CD_ITEM", "품목코드", 100);
            _flexITEM.SetCol("NM_ITEM", "품목명", 120);
            _flexITEM.SetCol("STND_ITEM", "규격", 65);
            _flexITEM.SetCol("UNIT_SO", "단위", 65);

            _flexITEM.SetCol("NO_SO", "수주번호", 140);
            _flexITEM.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("NM_SO", "수주형태", 100);
            _flexITEM.SetCol("CD_EXCH", "환종", 80);
            _flexITEM.SetCol("NM_SALEGRP", "영업그룹", 80);
            _flexITEM.SetCol("NM_KOR", "담당자", 80);
            _flexITEM.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexITEM.SetCol("NM_PARTNER", "거래처명", 100);
            _flexITEM.SetCol("NM_PROJECT", "프로젝트명", 100);
            //_flexITEM.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexITEM.SetCol("SOH_DC_RMK", "비고", 200);
            _flexITEM.SetCol("SOH_DC_RMK1", "비고1", 200);

            _flexITEM.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("QT_GIR", "의뢰적용수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexITEM.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexITEM.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexITEM.SetCol("NM_SL", "창고", 120, false);
            _flexITEM.SetCol("UNIT", "관리단위", 65, false);
            _flexITEM.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("GI_PARTNER", "납품처", 120, false);
            _flexITEM.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flexITEM.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexITEM.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flexITEM.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flexITEM.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flexITEM.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flexITEM.SetCol("DC_RMK4", "수주라인비고2", 150, false);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexITEM.SetCol("SEQ_PROJECT", "프로젝트항번", 120);
                _flexITEM.SetCol("CD_UNIT", "프로젝트품목코드", 120);
                _flexITEM.SetCol("NM_UNIT", "프로젝트품목명", 120);
            }
            _flexITEM.SetCol("SN_PARTNER", "거래처약칭", 120);

            _flexITEM.SettingVersion = "1.0.0.3";
            _flexITEM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexITEM.AfterDataRefresh += new ListChangedEventHandler(_flexITEM_AfterDataRefresh);
            _flexITEM.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexITEM.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexITEM.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);
            _flexITEM.AfterRowChange += new RangeEventHandler(_flexITEM_AfterRowChange);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexITEM.SetExceptSumCol("UM");

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexITEM.LoadUserCache("P_SA_SO_SUB_flexITEM");
        }

        #endregion

        #region InitGridITEM 한수전용

        private void InitGridITEM_HANSU()
        {

            _flexITEM.BeginSetting(1, 1, false);
            _flexITEM.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);

            _flexITEM.SetCol("CD_ITEM", "제조코드", 100);
            _flexITEM.SetCol("NM_ITEM", "제조코드명", 120);
            _flexITEM.SetCol("TXT_USERDEF2", "전용코드", 100, false);
            _flexITEM.SetCol("NM_ITEM_PARTNER", "전용품명", 100, false);
            _flexITEM.SetCol("CD_PACK", "포장단위", 100, false);
            _flexITEM.SetCol("NM_PACK", "포장단위명", 100, false);
            _flexITEM.SetCol("GI_PARTNER", "납품처", 120, false);
            _flexITEM.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flexITEM.SetCol("TXT_USERDEF1", "납품부서", 100, false);
            _flexITEM.SetCol("NM_PART", "납품부서명", 100, false);
            _flexITEM.SetCol("TXT_USERDEF4", "목적지", 100, false);
            
            _flexITEM.SetCol("STND_ITEM", "규격", false);
            _flexITEM.SetCol("UNIT_SO", "단위", false);

            _flexITEM.SetCol("NO_SO", "수주번호", 140);
            _flexITEM.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("NM_SO", "수주형태", 100);
            _flexITEM.SetCol("CD_EXCH", "환종", 80);
            _flexITEM.SetCol("NM_SALEGRP", "영업그룹", 80);
            _flexITEM.SetCol("NM_KOR", "담당자", 80);
            _flexITEM.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexITEM.SetCol("NM_PARTNER", "거래처명", 100);
            _flexITEM.SetCol("NM_PROJECT", "프로젝트명", 100);
            //_flexITEM.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
            _flexITEM.SetCol("SOH_DC_RMK", "비고", 200);
            _flexITEM.SetCol("SOH_DC_RMK1", "비고1", 200);

            _flexITEM.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexITEM.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("QT_GIR", "의뢰적용수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexITEM.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexITEM.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexITEM.SetCol("NM_SL", "창고", 120, false);
            _flexITEM.SetCol("UNIT", "관리단위", 65, false);
            _flexITEM.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexITEM.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flexITEM.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flexITEM.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flexITEM.SetCol("DC_RMK4", "수주라인비고2", 150, false);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexITEM.SetCol("SEQ_PROJECT", "프로젝트항번", 120);
                _flexITEM.SetCol("CD_UNIT", "프로젝트품목코드", 120);
                _flexITEM.SetCol("NM_UNIT", "프로젝트품목명", 120);
            }

            _flexITEM.SettingVersion = "2.0.0.1";
            _flexITEM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexITEM.AfterDataRefresh += new ListChangedEventHandler(_flexITEM_AfterDataRefresh);
            _flexITEM.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexITEM.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexITEM.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);
            _flexITEM.AfterRowChange += new RangeEventHandler(_flexITEM_AfterRowChange);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexITEM.SetExceptSumCol("UM");

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexITEM.LoadUserCache("P_SA_SO_SUB_flexITEM");
        }

        #endregion

        private void InitGrid_쏠리드()
        {
            _flex엑셀.BeginSetting(1, 1, false);
            _flex엑셀.SetCol("CD_PARTNER", "거래처코드", 100);
            _flex엑셀.SetCol("LN_PARTNER", "거래처명", 120);
            _flex엑셀.SetCol("CD_ITEM", "품목코드", 100);
            _flex엑셀.SetCol("NM_ITEM", "품목명", 120);
            _flex엑셀.SetCol("STND_ITEM", "규격", 80);
            _flex엑셀.SetCol("UNIT_IM", "단위", 80);
            _flex엑셀.SetCol("QT_SO", "요청수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex엑셀.SetCol("GI_PARTNER", "납품처코드", 100);
            _flex엑셀.SetCol("GI_LN_PARTNER", "납품처명", 100);
            _flex엑셀.SetCol("CD_SL", "출하창고", 100);
            _flex엑셀.SetCol("DT_DUEDATE", "납기요청일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex엑셀.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            //_flex엑셀.SetCol("DC_RMK", "비고", 100);
            _flex엑셀.SettingVersion = "0.0.0.0.2";
            _flex엑셀.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex수주.BeginSetting(1, 1, false);
            //_flex수주.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex수주.SetCol("CD_ITEM", "품목코드", 100);
            _flex수주.SetCol("NM_ITEM", "품목명", 120);
            _flex수주.SetCol("STND_ITEM", "규격", 65);
            _flex수주.SetCol("UNIT_SO", "단위", 65);
            _flex수주.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex수주.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex수주.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex수주.SetCol("QT_GIR", "의뢰적용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex수주.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex수주.SetCol("UM", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex수주.SetCol("AM_GIR", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex수주.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex수주.SetCol("NM_SL", "창고", 120, false);
            _flex수주.SetCol("UNIT", "관리단위", 65, false);
            _flex수주.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex수주.SetCol("GI_PARTNER", "납품처", 120, false);
            _flex수주.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flex수주.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex수주.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flex수주.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flex수주.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flex수주.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flex수주.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flex수주.SetCol("DC_RMK4", "수주라인비고2", 150, false);
            _flex수주.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex수주.SettingVersion = "1.0.0.5";
            _flex수주.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex수주.SetExceptSumCol("UM");
            _flex수주.HelpClick += new EventHandler(_flex수주_HelpClick);


            _flex적용.BeginSetting(1, 1, false);
            _flex적용.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flex적용.SetCol("CD_ITEM", "품목코드", 100);
            _flex적용.SetCol("NM_ITEM", "품목명", 120);
            _flex적용.SetCol("STND_ITEM", "규격", 65);
            _flex적용.SetCol("UNIT_SO", "단위", 65);
            _flex적용.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex적용.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex적용.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex적용.SetCol("QT_GIR", "의뢰적용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex적용.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex적용.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex적용.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex적용.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flex적용.SetCol("NM_SL", "창고", 120, false);
            _flex적용.SetCol("UNIT", "관리단위", 65, false);
            _flex적용.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex적용.SetCol("GI_PARTNER", "납품처", 120, false);
            _flex적용.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flex적용.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flex적용.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flex적용.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flex적용.SetCol("NO_PO_PARTNER", "거래처P0번호", 100);
            _flex적용.SetCol("NO_POLINE_PARTNER", "거래처P0항번", 100);
            _flex적용.SetCol("DC_RMK3", "수주라인비고1", 150, false);
            _flex적용.SetCol("DC_RMK4", "수주라인비고2", 150, false);
            _flex적용.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flex적용.SettingVersion = "1.0.0.5";
            _flex적용.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flex적용.SetExceptSumCol("UM");
            _flex적용.HelpClick += new EventHandler(_flex적용_HelpClick);
            _flex적용.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flex적용.ValidateEdit += new ValidateEditEventHandler(_flex적용_ValidateEdit);
        }

        #region -> InitPaint
        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();  
                DataSet ds = Global.MainFrame.GetComboData("N;MA_B000005");
                DataTable dt = MA.GetCodeUser(new string[] { "SO", "DU", "GI" }, new string[] { Global.MainFrame.DD("수주일자"), Global.MainFrame.DD("납기일자"), Global.MainFrame.DD("출하예정일") });

                cbo_DtGubun.DataSource = dt;
                cbo_DtGubun.DisplayMember = "NAME";
                cbo_DtGubun.ValueMember = "CODE";

                if (MA.ServerKey(false, new string[] { "SLFIRE" }))
                    cbo_DtGubun.SelectedValue = "GI";

                _flexH.SetDataMap("CD_EXCH", ds.Tables[0], "CODE", "NAME");

                //수주담당자는 Setting에 저장되어서 Local 별로 셋팅한 내역으로 보여준다.
                //bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
                //bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

                m_mskStart.Mask = "####/##/##";
                m_mskEnd.Mask = "####/##/##";

                m_mskStart.Text = Global.MainFrame.GetStringFirstDayInMonth;
                m_mskEnd.Text = Global.MainFrame.GetStringToday;

                if (bp_PJT.CodeValue != string.Empty)
                    bp_PJT.Enabled = false;

                //if (영업그룹코드 != string.Empty)
                //{
                //    bpSalegrp.SetCode(영업그룹코드, 영업그룹명);
                //    bpSalegrp.Enabled = false;
                //}

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> 화면내버튼 클릭

        #region -> 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            Hashtable hList = new Hashtable();

            //레이블이 아니므로 이렇게 상속받아서 처리함
            LabelExt lbl = new LabelExt();
            LabelExt lbl2 = new LabelExt();

            lbl.Text = "조회일";
            lbl2.Text = "조회일구분";

            hList.Add(m_mskStart, lbl);
            hList.Add(m_mskEnd, lbl);
            hList.Add(cbo_DtGubun, lbl2);

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region -> 조회버튼클릭
        private void OnSearchButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                if (!Check()) return;

                List<object> list = new List<object>();
                list.Add(Global.MainFrame.LoginInfo.CompanyCode);
                list.Add(m_mskStart.Text);
                list.Add(m_mskEnd.Text);
                list.Add(m_cdePlant.CodeValue);
                list.Add(m_cdeTpBusi.CodeValue);
                list.Add(bp_Partner.CodeValue);
                list.Add(bpSalegrp.CodeValue);
                list.Add(bpNm_Sl.CodeValue);
                list.Add(bp_PJT.CodeValue);
                list.Add(bp_Emp.CodeValue);
                list.Add(bp_ITEM.CodeValue);
                list.Add(D.GetString(cbo_DtGubun.SelectedValue));
                list.Add(is정상수주 == true ? "N" : "Y");   // 반품여부
                list.Add(과세구분);
                list.Add(환종);
                list.Add(계산서처리);
                list.Add(단가유형);
                list.Add(ctx납품처.CodeValue);
                list.Add(수불유형);
                list.Add(txt거래처PO.Text);
                list.Add(Am_Recalc);
                list.Add(txt비고.Text);
                list.Add(txt비고1.Text);
                list.Add(bp제품군.QueryWhereIn_Pipe);
                

                switch (tab수주.SelectedTab.Name)
                {
                    case "NO_SO":
                        list.Add("NO_SO");
                        list.Add(MA.Login.사원번호);
                        list.Add(ctx수주번호.CodeValue);
                        list.Add(bp수주형태.CodeValue);
                        _flexH.Binding = _biz.Search(list.ToArray(), "NO_SO");

                        if (!_flexH.HasNormalRow)
                        {
                            m_btnApply.Enabled = false;
                            btn_Add_Append.Enabled = false;
                            Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                        }
                        break;
                    case "CD_ITEM":
                        list.Add("CD_ITEM");
                        list.Add(MA.Login.사원번호);
                        list.Add(ctx수주번호.CodeValue);
                        list.Add(bp수주형태.CodeValue);
                        _flexITEM.Binding = _biz.Search(list.ToArray(), "CD_ITEM");

                        if (!_flexITEM.HasNormalRow)
                        {
                            m_btnApply.Enabled = false;
                            btn_Add_Append.Enabled = false;
                            Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                        }
                        break;

                    case "SOLIDTECH": //쏠리드전용
                        list.Add("CD_ITEM");
                        list.Add(MA.Login.사원번호);
                        list.Add(ctx수주번호.CodeValue);
                        list.Add(bp수주형태.CodeValue);
                        _flex수주.Binding = _biz.Search(list.ToArray(), "CD_ITEM");

                        if (!_flex수주.HasNormalRow)
                        {
                            m_btnApply.Enabled = false;
                            btn_Add_Append.Enabled = false;
                            Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                        }
                        m_btnApply.Enabled = true;
                        _flex적용.Binding = _flex수주.DataTable.Clone();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion
        
        #region -> 적용버튼클릭

        #region -> Settings 로 인해 사용자 값 Local 별로 셋팅 하고자 함

        private void P_SA_SO_SUB_Load(object sender, EventArgs e)
        {
            bp_Emp.CodeValue = Settings.Default.no_emp_so;
            bp_Emp.CodeName = Settings.Default.nm_emp_so;
            chk_YN.Checked = ComFunc.Convert_TF(Settings.Default.chk_YN);
        }

        #endregion

        #region -> 적용

        private void OnApply(object sender, System.EventArgs e)
        {
            try
            {
                Btn_Click("APPEND");
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가적용
        private void btn_Add_Append_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Click("ADDAPPEND");
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 적용/추가적용 
        private void Btn_Click(string str)
        {
            try
            {
                FlexGrid _flex = null;
                switch (tab수주.SelectedTab.Name)
                {
                    case "NO_SO":
                        _flex = _flexL;
                        break;
                    case "CD_ITEM":
                        _flex = _flexITEM;
                        break;
                    case "SOLIDTECH":
                        _flex = _flex적용;
                        break;

                }

                if (!_flex.HasNormalRow) return;

                DataRow[] dr = _flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _dt = _flex.DataTable.Clone();
 
                ////선택된 라인 데이터의 거래처,환종, 단가유형이 섞이면 Exception 처리 하기
                //string[] str_Filter = new string[] { "CD_PARTNER", "CD_EXCH", "TP_PRICE_IMSI", "CD_SALEGRP" };
                //DataTable dt = ComFunc.getGridGroupBy(dr, str_Filter, true);
                //if (dt.Rows.Count != 1)
                //{
                //    Global.MainFrame.ShowMessage("동일한 거래처명 또는 환종 또는 단가유형을 선택해주세요.!!");
                //    return;
                //}

                if (!중복체크(dr, new string[] { "CD_PARTNER" }, "거래처")) return;
                if (!중복체크(dr, new string[] { "CD_EXCH" }, "환종")) return;
                if (!중복체크(dr, new string[] { "TP_PRICE_IMSI" }, "단가유형")) return;

                if (is정상수주 == false && !중복체크(dr, new string[] { "CD_SALEGRP" }, "영업그룹")) return;

                foreach (DataRow row in dr)
                {
                    if (D.GetDecimal(row["CHK_QT_GIR"]) != 0)
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flexH.CDecimal(row["AM_GIRAMT"]) * (_flexH.CDecimal(row["RT_VAT"]) / 100)); //부가세

                    row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_GIRAMT"]) + D.GetDecimal(row["AM_VAT"])); //합계금액(원화금액 + 부가세) 지금 쿼리가 잘못되어 있음 적용시 재계산
                    _dt.ImportRow(row);
                }

                flag = str; //적용(APPEND), 추가적용(ADDAPPEND)
                so_DcRmk = ComFunc.Convert_YN(chk_YN.Checked);

                //화면이 닫히기전에 Setting 하고자 하는 내역을 반듯이 저장해주어야 한다.
                Settings.Default.no_emp_so = bp_Emp.CodeValue;
                Settings.Default.nm_emp_so = bp_Emp.CodeName;
                Settings.Default.chk_YN = ComFunc.Convert_YN(chk_YN.Checked);

                //꼭 저장을 해줘야 한다.
                Settings.Default.Save();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        
        #endregion

        #region -> 리턴데이터
        public DataTable 수주데이터
        {
            get { return _dt; }
        }

        public string 적용구분
        {
            get { return flag; }
        }

        public string 수주헤더셋팅구분
        {
            get { return so_DcRmk; }
        }
        #endregion

        #endregion

        #region -> OnClosed(화면이 닫힐때)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
             
            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_SA_SO_SUB_flexH");
            _flexL.SaveUserCache("P_SA_SO_SUB_flexL");
            _flexITEM.SaveUserCache("P_SA_SO_SUB_flexITEM");
        }
        #endregion 

        #endregion

        #region -> 그리드 이벤트

        #region -> _flexH_AfterDataRefresh

        private void _flexH_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow)
                {
                    m_btnApply.Enabled = false;
                    btn_Add_Append.Enabled = false;
                }
                else
                {
                    m_btnApply.Enabled = true;

                    //이미 적용받은 것은 
                    //1. 수량을 전부 적용 받은 것은 행을 삭제하여 보여주는 것 조차 막고 
                    //2. 수량이 남은것은 의뢰수량에서 빼어서 보여준다.
                    if (dt_Append == null || dt_Append.Rows.Count == 0)
                        btn_Add_Append.Enabled = false;
                    else
                        btn_Add_Append.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexITEM_AfterDataRefresh

        void _flexITEM_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!_flexITEM.HasNormalRow)
                {
                    m_btnApply.Enabled = false;
                    btn_Add_Append.Enabled = false;
                }
                else
                {
                    m_btnApply.Enabled = true;

                    //이미 적용받은 것은 
                    //1. 수량을 전부 적용 받은 것은 행을 삭제하여 보여주는 것 조차 막고 
                    //2. 수량이 남은것은 의뢰수량에서 빼어서 보여준다.
                    if (dt_Append == null || dt_Append.Rows.Count == 0)
                        btn_Add_Append.Enabled = false;
                    else
                        btn_Add_Append.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _StartEdit

        #region -> _flexH_StartEdit

        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[e.Row, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                    {
                        if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                        _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexL_StartEdit

        private void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                if (so_Price == "")
                {
                    Global.MainFrame.ShowMessage("등록되지 않은 영업그룹입니다.");
                    e.Cancel = true;
                    return;
                }
                if (so_Price == "Y")
                {
                    if (_flex.Cols[e.Col].Name == "UM" || _flex.Cols[e.Col].Name == "AM_GIR")
                    {
                        Global.MainFrame.ShowMessage("영업단가통제된 영업그룹입니다.");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region -> _flexH_AfterRowChange

        private void _flexH_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH[e.NewRange.r1, "NO_SO"].ToString();
                string Filter = "NO_SO = '" + Key + "'";

                if (_flexH.DetailQueryNeed)
                {
                    //object[] obj_Detail = new object[11];
                    //obj_Detail[0] = Global.MainFrame.LoginInfo.CompanyCode;
                    //obj_Detail[1] = Key;
                    //obj_Detail[2] = m_cdePlant.CodeValue;
                    //obj_Detail[3] = m_cdeTpBusi.CodeValue;
                    //obj_Detail[4] = bpNm_Sl.CodeValue;
                    //obj_Detail[5] = bp_ITEM.CodeValue;
                    //obj_Detail[6] = m_mskStart.Text;
                    //obj_Detail[7] = m_mskEnd.Text;
                    //obj_Detail[8] = cbo_DtGubun.SelectedValue == null ? string.Empty : cbo_DtGubun.SelectedValue.ToString();
                    //obj_Detail[9] = Am_Recalc;   //영업환경설정 : 단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다);
                    //obj_Detail[10] = txt거래처PO.Text;

                    object[] obj_Detail = null;

                    if (Global.MainFrame.ServerKeyCommon == "HANSU")
                    {

                        obj_Detail = new object[] {
                        Global.MainFrame.LoginInfo.CompanyCode, 
                        Key, 
                        m_cdePlant.CodeValue, 
                        m_cdeTpBusi.CodeValue, 
                        bpNm_Sl.CodeValue, 
                        bp_PJT.CodeValue, 
                        bp_ITEM.CodeValue, 
                        m_mskStart.Text, 
                        m_mskEnd.Text, 
                        cbo_DtGubun.SelectedValue, 
                        Am_Recalc, 
                        txt거래처PO.Text,
                        ctx납품처.CodeValue,
                        bp제품군.QueryWhereIn_Pipe
                    };

                    }
                    else
                    {
                        obj_Detail = new object[] {
                        Global.MainFrame.LoginInfo.CompanyCode, 
                        Key, 
                        m_cdePlant.CodeValue, 
                        m_cdeTpBusi.CodeValue, 
                        bpNm_Sl.CodeValue, 
                        bp_PJT.CodeValue, 
                        bp_ITEM.CodeValue, 
                        m_mskStart.Text, 
                        m_mskEnd.Text, 
                        cbo_DtGubun.SelectedValue, 
                        Am_Recalc, 
                        txt거래처PO.Text,
                        ctx납품처.CodeValue,
                        bp제품군.QueryWhereIn_Pipe
                    };
                    }
                    dt = _biz.SearchDetail(obj_Detail);

                    //기존 적용건 있을 시 제외하는 로직
                    if (dt_Append != null && dt_Append.Rows.Count > 0)
                    {
                        dt.PrimaryKey = new DataColumn[] { dt.Columns["NO_SO"], dt.Columns["SEQ_SO"] };

                        foreach (DataRow row in dt_Append.Rows)
                        {
                            DataRow rowFind = dt.Rows.Find(new object[] { row["NO_SO"], row["SEQ_SO"] });
                            if (rowFind != null) rowFind.Delete();
                        }
                    }
                }
                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;

                //단가통제 적용 여부 조회
                object[] obj = new object[2];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                obj[1] = _flexH[e.NewRange.r1, "CD_SALEGRP"].ToString();
                so_Price = _biz.GetSaleOrgUmCheck(obj);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexITEM_AfterRowChange

        void _flexITEM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                so_Price = D.GetString(_flexITEM["SO_PRICE"]);

                //이미 적용받은 것은 
                //1. 수량을 전부 적용 받은 것은 행을 삭제하여 보여주는 것 조차 막고 
                //2. 수량이 남은것은 의뢰수량에서 빼어서 보여준다.
                if (dt_Append == null || dt_Append.Rows.Count == 0 || !_flexITEM.HasNormalRow)
                    return;

                DataRow[] drs = _flexITEM.DataTable.Select("NO_SO = '" + D.GetString(_flexITEM["NO_SO"]) + "' AND SEQ_SO = " + D.GetString(_flexITEM["SEQ_SO"]), "", DataViewRowState.CurrentRows);

                DataRow dr = drs[0];

                foreach (DataRow dr_Del in dt_Append.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted || dr_Del.RowState == DataRowState.Deleted) continue;

                    if (dr["NO_SO"].ToString() == dr_Del["NO_SO"].ToString() &&
                        dr["SEQ_SO"].ToString() == dr_Del["SEQ_SO"].ToString())
                    {
                        ////적용수량으로 비교하면 안되고 수주의 의뢰수량과 적용수량을 비교해줘야 한다. 
                        //if (D.GetDecimal(dr["REQ_QT_GIR"]) == D.GetDecimal(dr_Del["QT_GIR"]))
                        //    dr.Delete();
                        //else
                        //{
                        //    dr["QT_GIR"] = D.GetDecimal(dr["REQ_QT_GIR"]) - D.GetDecimal(dr_Del["QT_GIR"]);
                        //    dr["AM_GIR"] = D.GetDecimal(dr["UM"]) * D.GetDecimal(dr["QT_GIR"]);
                        //    dr["AM_GIRAMT"] = Decimal.Truncate(D.GetDecimal(dr["AM_GIR"]) * D.GetDecimal(dr["RT_EXCH"]));
                        //    //부가세는 적용/추가적용시점에 이미 받은 수량이 있을경우에만 재계산하고 받은 수량이 0일경우에는 수주에서 넣은 그대로 가져간다.
                        //    dr["AM_VAT"] = Decimal.Truncate(D.GetDecimal(dr["AM_GIRAMT"]) * (D.GetDecimal(dr["RT_VAT"]) / 100));
                        //    dr["QT_GIR_IM"] = D.GetDecimal(dr["QT_GIR"]) * (D.GetDecimal(dr["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(dr["UNIT_SO_FACT"]));
                        //}

                        /*2009.04.09 고객납품의뢰 등록에서 이미 보여진 수주항번은 도움창에서 보여주지 않는다.로 수정*/
                        dr.Delete();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexL_ValidateEdit

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "S":

                        _flex["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                        if (_flex.Name == "_flexL")
                        {
                            DataRow[] drArr = _flex.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                            if (drArr.Length != 0)
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                            else
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        break;

                    case "QT_GIR":
                        #region
                        // qtso_AddAllowYN = "Y" 일 경우 수주 수량이 초과허용 가능하다.
                        decimal qtso_AddAllow = D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) + (D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));

                        if (D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) < D.GetDecimal(_flex["QT_GIR"]) && qtso_AddAllowYN == "N")
                        {
                            _flex["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["REQ_QT_GIR"]));
                            Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        }
                        else
                        {
                            if (qtso_AddAllow < D.GetDecimal(_flex["QT_GIR"]))
                            {
                                _flex["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["REQ_QT_GIR"]));
                                Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                            }
                            else
                            {
                                _flex["UNIT_SO_FACT"] = D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0 ? 1 : _flex["UNIT_SO_FACT"];

                                _flex["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(_flex["UM"]));    //수주금액(외화) = 수량 * 단가
                                _flex["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flex["AM_GIR"]) * D.GetDecimal(_flex["RT_EXCH"])));//수주금액(원화) = 수량 * 환율
                                _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) * D.GetDecimal(_flex["RT_VAT"]) / 100); //부가세 = 수주금액(원화) * 부가세율 / 100
                                _flex["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(_flex["UNIT_SO_FACT"]));
                                _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                            }
                        }

                        #region 기존소스빽업
                        //// qtso_AddAllowYN = "Y" 일 경우 수주 수량이 초과허용 가능하다.
                        //decimal qtso_AddAllow = D.GetDecimal(_flexL[e.Row, "REQ_QT_GIR"]) + (D.GetDecimal(_flexL[e.Row, "REQ_QT_GIR"]) * (D.GetDecimal(_flexL[e.Row, "RT_PLUS"]) / 100));

                        //if (D.GetDecimal(_flexL[e.Row, "REQ_QT_GIR"]) < D.GetDecimal(_flexL["QT_GIR"]) && qtso_AddAllowYN == "N")
                        //{
                        //    _flexL["QT_GIR"] = _flexL["REQ_QT_GIR"];
                        //    MessageBox.Show("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        //}
                        //else
                        //{
                        //    if (qtso_AddAllow < D.GetDecimal(_flexL["QT_GIR"]))
                        //    {
                        //        _flexL["QT_GIR"] = _flexL["REQ_QT_GIR"];
                        //        MessageBox.Show("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        //    }
                        //    else
                        //    {
                        //        _flexL["UNIT_SO_FACT"] = D.GetDecimal(_flexL["UNIT_SO_FACT"]) == 0 ? 1 : _flexL["UNIT_SO_FACT"];

                        //        _flexL["AM_GIR"] = D.GetDecimal(_flexL["QT_GIR"]) * D.GetDecimal(_flexL["UM"]);    //수주금액(외화) = 수량 * 단가
                        //        _flexL["AM_GIRAMT"] = Decimal.Truncate(D.GetDecimal(_flexL["AM_GIR"]) * D.GetDecimal(_flexL["RT_EXCH"]));//수주금액(원화) = 수량 * 환율
                        //        _flexL["AM_VAT"] = Decimal.Truncate(D.GetDecimal(_flexL["AM_GIRAMT"]) * D.GetDecimal(_flexL["RT_VAT"]) / 100); //부가세 = 수주금액(원화) * 부가세율 / 100
                        //        _flexL["QT_GIR_IM"] = D.GetDecimal(_flexL["QT_GIR"]) * D.GetDecimal(_flexL["UNIT_SO_FACT"]);
                        //        _flexL["AMT"] = D.GetDecimal(_flexL["AM_GIRAMT"]) + D.GetDecimal(_flexL["AM_VAT"]);
                        //    }
                        //}
                        #endregion
                        #endregion
                        break;

                    case "UM":
                        _flex["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(_flex["UM"]));     //수주금액(외화) = 수량 * 단가
                        _flex["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flex["AM_GIR"]) * D.GetDecimal(_flex["RT_EXCH"]))); //수주금액(원화) = 수량 * 환율
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) * D.GetDecimal(_flex["RT_VAT"]) / 100);  //부가세 = 수주금액(원화) * 부가세율 / 100
                        _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                        break;

                    case "AM_GIR":
                        if (D.GetDecimal(_flex["QT_GIR"]) != 0)
                            _flex["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) / D.GetDecimal(_flex["QT_GIR"]));
                        else
                            _flex["UM"] = 0;

                        _flex["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) * D.GetDecimal(_flex["RT_EXCH"])); //수주금액(원화) = 수량 * 환율
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) * D.GetDecimal(_flex["RT_VAT"]) / 100);  //부가세 = 수주금액(원화) * 부가세율 / 100
                        _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) + D.GetDecimal(_flex["AM_VAT"])); 
                        break; 
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_CheckHeaderClick

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                        for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        {
                            _flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                                _flexL[i, "S"] = _flexH["S"].ToString();
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트
                        if (!_flexL.HasNormalRow) return;

                        _flexH["S"] = _flexL["S"].ToString();

                        break;
                    case "_flexITEM":
                        //내용 없음
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex적용_ValidateEdit
        void _flex적용_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "UM":
                        _flex["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(_flex["UM"]));     //수주금액(외화) = 수량 * 단가
                        _flex["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flex["AM_GIR"]) * D.GetDecimal(_flex["RT_EXCH"]))); //수주금액(원화) = 수량 * 환율
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) * D.GetDecimal(_flex["RT_VAT"]) / 100);  //부가세 = 수주금액(원화) * 부가세율 / 100
                        _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                        break;

                    case "AM_GIR":
                        if (D.GetDecimal(_flex["QT_GIR"]) != 0)
                            _flex["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) / D.GetDecimal(_flex["QT_GIR"]));
                        else
                            _flex["UM"] = 0;

                        _flex["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) * D.GetDecimal(_flex["RT_EXCH"])); //수주금액(원화) = 수량 * 환율
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) * D.GetDecimal(_flex["RT_VAT"]) / 100);  //부가세 = 수주금액(원화) * 부가세율 / 100
                        _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #region -> _flex수주_HelpClick
        void _flex수주_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flex엑셀.HasNormalRow) return;
                if (!_flex수주.HasNormalRow) return;

                DataRow[] drH = _flex엑셀.DataTable.Select("CD_PARTNER = '" + D.GetString(_flex수주[_flex수주.Row, "CD_PARTNER"]) + "' AND CD_ITEM = '" + D.GetString(_flex수주[_flex수주.Row, "CD_ITEM"])  + "'");

                if (drH == null || drH.Length == 0)
                {
                    Global.MainFrame.ShowMessage("상단 그리드에 일치하는 품목이 없습니다.");
                    return;
                }

                string Filter = "CD_PARTNER = '" + D.GetString(drH[0]["CD_PARTNER"]) + "' AND CD_ITEM = '" + D.GetString(drH[0]["CD_ITEM"]) + "'";

                decimal QT_GOOD = 0m;
                decimal QT_GOOD_INV = 0m;
                DataTable dtL = _flex수주.DataTable.Clone();

                //헤더의 처리대상수량
                QT_GOOD_INV = D.GetDecimal(drH[0]["QT_SO"]);

                //맨 하단에 이미 추가해놓은 수량을 sum 한 값
                decimal QT_GOOD_LAST = 0m;

                //헤더의 품목으로 이미 추가 해놓은 마지막 그리드에서 같은품목인것가져와서 헤더의 수량과 - 추가한 수량 - 추가해놓은 수량이 초과되서는 안된다.

                if (_flex적용.HasNormalRow)
                {
                    DataRow[] DR = _flex적용.DataTable.Select(Filter);

                    if (DR.Length != 0)
                    {
                        //맨 하단에 이미 추가해놓은 수량을 sum 한 값
                        foreach (DataRow dr in DR)
                            QT_GOOD_LAST = QT_GOOD_LAST + D.GetDecimal(dr["QT_GIR"]);
                    }
                }

                //해당 입고 lot수량에 대해 이미 하단에 할당된 총수량
                decimal QT_MGMT_USE = 0m;
                if (_flex적용.HasNormalRow)
                    QT_MGMT_USE = D.GetDecimal(_flex적용.DataTable.Compute("SUM(QT_GIR)", Filter));
               

                //라인의 잔량
                QT_GOOD = D.GetDecimal(_flex수주[_flex수주.Row, "QT_GIR"]);
                DataRow[] DR_flex적용 = _flex적용.DataTable.Select("NO_SO = '" + D.GetString(_flex수주[_flex수주.Row, "NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(_flex수주[_flex수주.Row, "SEQ_SO"]) + "'");
                if (DR_flex적용.Length > 0)
                {
                    Global.MainFrame.ShowMessage(Filter + "는 이미 내역에 할당이 되었습니다.");
                    return;
                }

                if (QT_MGMT_USE < QT_GOOD_INV)
                {
                    //서로다른 처리항번에 동일품목이 있을경우 Select결과는 복수가 바인딩되므로 한번으로 제한한다 
                    DataRow[] DR_flexL = _flex수주.DataTable.Select("NO_SO = '" + D.GetString(_flex수주[_flex수주.Row, "NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(_flex수주[_flex수주.Row, "SEQ_SO"]) + "'");
                   // DataRow[] DR_flex적용 = _flex적용.DataTable.Select("NO_SO = '" + D.GetString(_flex수주[_flex수주.Row, "NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(_flex수주[_flex수주.Row, "SEQ_SO"]) + "'");
                    int i = 0;
                    foreach (DataRow Dr_row in DR_flexL)
                    {
                        if (i == 1) break;

                        //if (D.GetDecimal(_flex수주["QT_SO_ORG"]) == QT_MGMT_USE)
                        //if (DR_flex적용.Length > 0)
                        //{
                        //    Global.MainFrame.ShowMessage(Filter + "는 이미 내역에 할당이 되었습니다.");
                        //    return;
                        //}
                        //적용시 상단 출고항번에 이미 할당된 수량을 제외한 나머지 잔량으로 비교하여 할당한다.
                        //대상수량에 이미 적용된 수량 차감 (QT_MGMT_USE) 20090107
                        //if ((D.GetDecimal(_flex수주["QT_SO_ORG"]) - QT_MGMT_USE) < (QT_GOOD_INV - QT_GOOD_LAST))
                        //    Dr_row["QT_GIR"] = D.GetDecimal(_flex수주["QT_SO_ORG"]) - QT_MGMT_USE; //모자라면 전체할당 
                        //else
                        //    Dr_row["QT_GIR"] = QT_GOOD_INV - QT_GOOD_LAST; //충분하거나 남으면 남은 잔량전부 할당

                        if (QT_GOOD_INV - QT_MGMT_USE > D.GetDecimal(_flex수주[_flex수주.Row, "QT_GIR"]))
                            Dr_row["S"] = "Y";
                        else
                        {
                            Dr_row["QT_GIR"] = QT_GOOD_INV - QT_GOOD_LAST; //충분하거나 남으면 남은 잔량전부 할당
                            Dr_row["S"] = "Y";
                        }

                        decimal qtso_AddAllow = D.GetDecimal(Dr_row["REQ_QT_GIR"]) + (D.GetDecimal(Dr_row["REQ_QT_GIR"]) * (D.GetDecimal(Dr_row["RT_PLUS"]) / 100));

                        if (D.GetDecimal(Dr_row["REQ_QT_GIR"]) < D.GetDecimal(Dr_row["QT_GIR"]) && qtso_AddAllowYN == "N")
                        {
                            Dr_row["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["REQ_QT_GIR"]));
                            Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        }
                        else
                        {
                            if (qtso_AddAllow < D.GetDecimal(Dr_row["QT_GIR"]))
                            {
                                Dr_row["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["REQ_QT_GIR"]));
                                Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                            }
                            else
                            {
                                Dr_row["UNIT_SO_FACT"] = D.GetDecimal(Dr_row["UNIT_SO_FACT"]) == 0 ? 1 : Dr_row["UNIT_SO_FACT"];
                                Dr_row["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["QT_GIR"]) * D.GetDecimal(Dr_row["UM"]));    //수주금액(외화) = 수량 * 단가
                                Dr_row["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(Dr_row["AM_GIR"]) * D.GetDecimal(Dr_row["RT_EXCH"])));//수주금액(원화) = 수량 * 환율
                                Dr_row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["AM_GIRAMT"]) * D.GetDecimal(Dr_row["RT_VAT"]) / 100); //부가세 = 수주금액(원화) * 부가세율 / 100
                                Dr_row["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["QT_GIR"]) * D.GetDecimal(Dr_row["UNIT_SO_FACT"]));
                                Dr_row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(Dr_row["AM_GIRAMT"]) + D.GetDecimal(Dr_row["AM_VAT"]));
                            }
                        }

                        Dr_row["GI_PARTNER"] = D.GetString(drH[0]["GI_PARTNER"]);
                        Dr_row["DT_DUEDATE"] = D.GetString(drH[0]["DT_DUEDATE"]);
                        Dr_row["DT_REQGI"] = D.GetString(drH[0]["DT_REQGI"]);

                        dtL.ImportRow(Dr_row);
                        i++;
                    }
                }
                else
                {
                    Global.MainFrame.ShowMessage("거래처 = " + D.GetString(drH[0]["CD_PARTNER"]) + "," + "품목 = " + D.GetString(drH[0]["CD_ITEM"]) + "'의 입고수량이 부족하여 선입선출 할수 없습니다.");
                    return;
                }

                //라인 그리드에 선입 선출 된것을 맨 하단 그리드에 반영 하는 부분
                if (dtL == null) return;
                else
                {
                    int idx = 0;

                    //입력된 데이터가 하나도 없는경우에는 바로 적용
                    if (_flex적용.DataTable != null)
                    {
                        DataRow[] dr_flexD = _flex적용.DataTable.Select("", "", DataViewRowState.CurrentRows);

                        //입력된 데이터가 존재하는경우에는 같은 것을 두번 선입선출 적용 눌럿을때 반응 하지 않도록 필터링 걸어주는 부분
                        foreach (DataRow ddr in dr_flexD)
                        {

                            if (D.GetString(_flex수주[_flex수주.Row, "NO_SO"]) == D.GetString(ddr["NO_SO"]) &&
                                D.GetString(_flex수주[_flex수주.Row, "SEQ_SO"]) == D.GetString(ddr["SEQ_SO"]))
                                idx++;
                        }
                    }

                    if (idx == 0)
                        _flex적용.BindingAdd(dtL, string.Empty, true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #region -> _flex적용_HelpClick
        void _flex적용_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flex적용.HasNormalRow) return;

                DataRow[] drs = _flex수주.DataTable.Select("NO_SO = '" + D.GetString(_flex적용[_flex적용.Row, "NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(_flex적용[_flex적용.Row, "SEQ_SO"]) + "'", "", DataViewRowState.CurrentRows);

                foreach (DataRow row in drs)
                {
                    row["QT_GIR"] = D.GetDecimal(row["QT_SO_ORG"]) - D.GetDecimal(row["CHK_QT_GIR"]);
                    row["REQ_QT_GIR"] = D.GetDecimal(row["QT_SO_ORG"]) - D.GetDecimal(row["CHK_QT_GIR"]);
                }

                _flex적용.RemoveItem(_flex적용.Row);
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #endregion

        #region -> 도움창 셋팅

        #region -> Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                        e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_USER:
                        if (e.ControlName == "bp_PJT")
                            e.HelpParam.P41_CD_FIELD1 = "프로젝트"; //도움창 이름 찍어줄 값
                        if (e.ControlName == "ctx수주번호")
                        {
                            e.HelpParam.P22_DATE_START = m_mskStart.Text;
                            e.HelpParam.P23_DATE_END = m_mskEnd.Text;
                        }
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> Control_QueryAfter
        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == "bp_PJT")
                {
                    bp_Partner.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                    bp_Partner.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
                    bpSalegrp.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
                    bpSalegrp.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> 쏠리드 전용
        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog m_FileDlg = new OpenFileDialog();
                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (m_FileDlg.ShowDialog() != DialogResult.OK) return;

                Application.DoEvents();

                if (_flex엑셀.HasNormalRow)
                    _flex엑셀.DataTable.Clear();

                MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");

                _flex엑셀.Redraw = false;

                if (_flex수주.HasNormalRow)
                    _flex수주.DataTable.Clear();

                if (_flex적용.HasNormalRow)
                    _flex적용.DataTable.Clear();

                string FileName = m_FileDlg.FileName;

                Excel excel = new Excel();
                DataTable dtExcel = excel.StartLoadExcel(FileName);

                if (!IsValidCol(dtExcel, "CD_PARTNER", "거래처코드")) return;
                if (!IsValidCol(dtExcel, "CD_ITEM", "품목코드")) return;
                if (!IsValidCol(dtExcel, "QT_SO", "요청수량")) return;
                if (!IsValidCol(dtExcel, "GI_PARTNER", "납품처코드")) return;
                if (!IsValidCol(dtExcel, "CD_SL", "창고코드")) return;
                if (!IsValidCol(dtExcel, "DT_DUEDATE", "납기요청일")) return;
                if (!IsValidCol(dtExcel, "DT_REQGI", "출하예정일")) return;

                bool is거래처 = true;
                bool is품목 = true;

                StringBuilder str거래처 = new StringBuilder();
                StringBuilder str품목 = new StringBuilder();

                DataTable dt_COPY = new DataTable();
                dt_COPY.Columns.Add("CD_PARTNER", typeof(string));
                dt_COPY.Columns.Add("CD_ITEM", typeof(string));
                dt_COPY.Columns.Add("QT_SO", typeof(decimal));
                dt_COPY.Columns.Add("GI_PARTNER", typeof(string));
                dt_COPY.Columns.Add("CD_SL", typeof(string));
                dt_COPY.Columns.Add("DT_DUEDATE", typeof(string));
                dt_COPY.Columns.Add("DT_REQGI", typeof(string));
                DataTable dt_group = dtExcel.DefaultView.ToTable(true, "CD_PARTNER", "CD_ITEM", "GI_PARTNER", "CD_SL", "DT_DUEDATE", "DT_REQGI");
                dt_group.Columns.Add("QT_SO", typeof(decimal));

                foreach (DataRow row in dtExcel.Rows)
                    dt_COPY.Rows.Add(row.ItemArray);

                DataTable dt거래처정보 = _biz.거래처정보(dt_group, ref str거래처, ref is거래처);
                DataTable dt품목정보 = _biz.품목(dt_group, m_cdePlant.CodeValue, ref str품목, ref is품목);
                DataTable dt = _biz.수주Schema();

                foreach (DataRow row in dt_group.Rows)
                {
                    DataRow row거래처 = dt거래처정보.Rows.Find(row["CD_PARTNER"]);
                    DataRow row품목 = dt품목정보.Rows.Find(row["CD_ITEM"]);

                    if (row거래처 == null || row품목 == null) continue;

                    DataRow newrow = dt.NewRow();
                    newrow["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                    newrow["LN_PARTNER"] = D.GetString(row거래처["LN_PARTNER"]);
                    newrow["CD_ITEM"] = D.GetString(row["CD_ITEM"]);
                    newrow["NM_ITEM"] = D.GetString(row품목["NM_ITEM"]);
                    newrow["STND_ITEM"] = D.GetString(row품목["STND_ITEM"]);
                    newrow["UNIT_IM"] = D.GetString(row품목["UNIT_IM"]);
                    newrow["QT_SO"] = D.GetDecimal(dt_COPY.Compute("SUM(QT_SO)", "CD_PARTNER = '" + D.GetString(row["CD_PARTNER"]) + "' AND CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "' AND ISNULL(GI_PARTNER,'') = '" + D.GetString(row["GI_PARTNER"]) + "' AND ISNULL(CD_SL,'') = '" + D.GetString(row["CD_SL"]) + "' AND ISNULL(DT_DUEDATE,'') = '" + D.GetString(row["DT_DUEDATE"]) + "' AND ISNULL(DT_REQGI,'') = '" + D.GetString(row["DT_REQGI"]) + "'"));
                    newrow["GI_PARTNER"] = D.GetString(row["GI_PARTNER"]);
                    newrow["GI_LN_PARTNER"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER, new object[] { MA.Login.회사코드, row["GI_PARTNER"] })["LN_PARTNER"];
                    newrow["CD_SL"] = D.GetString(row["CD_SL"]);
                    newrow["DT_DUEDATE"] = D.GetString(row["DT_DUEDATE"]);
                    newrow["DT_REQGI"] = D.GetString(row["DT_REQGI"]);

                    dt.Rows.Add(newrow.ItemArray);
                }

                dt.DefaultView.Sort = "CD_ITEM, CD_PARTNER, GI_PARTNER, CD_SL";
                _flex엑셀.Binding = dt;

                if (!is거래처 || !is품목)
                {
                    StringBuilder sb = new StringBuilder();

                    if (!is거래처)
                    {
                        sb.AppendLine("<" + ("거래처") + ">");
                        sb.AppendLine(str거래처.ToString());
                    }

                    if (!is품목)
                    {
                        sb.AppendLine("<" + ("품목") + ">");
                        sb.AppendLine(str품목.ToString());
                    }

                    Global.MainFrame.ShowDetailMessage("엑셀자료에 적합하지 않은 내용이 존재합니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", sb.ToString());
                }

                _flex엑셀.Redraw = true;

                MsgControl.CloseMsg();

                if (_flex엑셀.HasNormalRow)
                    btn선입선출적용.Enabled = btn선입선출취소.Enabled = m_btnQuery.Enabled = true;

                Global.MainFrame.ShowMessage("엑셀 작업을 완료하였습니다.");

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                _flex엑셀.Redraw = true;
            }
        }


        private void tab수주_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tab수주.SelectedTab.Name == "SOLIDTECH")
                {
                    btn엑셀업로드.Visible = btn선입선출적용.Visible = btn선입선출취소.Visible = true;

                    if (!_flex엑셀.HasNormalRow)
                        btn선입선출적용.Enabled = btn선입선출취소.Enabled = m_btnQuery.Enabled = false;

                    btn_Add_Append.Visible = false;
                }
                else
                {
                    btn엑셀업로드.Visible = btn선입선출적용.Visible = btn선입선출취소.Visible = false;
                    btn_Add_Append.Visible = true;
                    m_btnQuery.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn선입선출적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex엑셀.HasNormalRow) return;
                if (!_flex수주.HasNormalRow) return;

                //할당받으거 초기화0
                if (_flex적용.HasNormalRow)
                    _flex적용.DataTable.Rows.Clear();

                bool is품목 = true;
                bool is요청품목 = true;
                StringBuilder str품목 = new StringBuilder();
                StringBuilder str요청품목 = new StringBuilder();

                _flex적용.Redraw = false;

                for (int k = _flex엑셀.Rows.Fixed; k < _flex엑셀.Rows.Count; k++)
                {
                    string Filter = "CD_PARTNER = '" + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "' AND CD_ITEM = '" + D.GetString(_flex엑셀[k, "CD_ITEM"]) + "'";

                    decimal QT_적용수량 = 0m;
                    decimal QT_헤더수량 = 0m;    //헤더의 처리수량

                    DataTable dtL = _flex수주.DataTable.Clone();

                    //헤더의 처리수량
                    QT_헤더수량 = D.GetDecimal(_flex엑셀[k, "QT_SO"]);

                    //헤더의 품목으로 라인에서 같은품목인것만 가져온다.
                    DataRow[] DR = _flex수주.DataTable.Select(Filter);
                    if (DR.Length == 0)
                    {
                        str품목.AppendLine("거래처 : " + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "  품목 : " + D.GetString(_flex엑셀[k, "CD_ITEM"]));
                        is품목 = false;
                        continue;
                    }

                    decimal QT_MGMT_USE = 0;
                    //라인의 잔량

                    foreach (DataRow dr in DR)
                    {
                        QT_적용수량 = QT_적용수량 + D.GetDecimal(dr["REQ_QT_GIR"]);

                        if (_flex적용.HasNormalRow)
                        {
                            //해당 lot재고의 총량에서 이미 할당된 수량만큼은 제외한다 20090108
                            QT_MGMT_USE = D.GetDecimal(_flex적용.DataTable.Compute("SUM(QT_GIR)", Filter));
                            QT_적용수량 = QT_적용수량 - QT_MGMT_USE;
                        }
                    }

                    //라인의 잔량이 헤더의 처리수량을 초과할수 없다.
                    if (QT_적용수량 >= QT_헤더수량) // QT_GOOD_INV 헤더
                    {
                        int j = -1;
                        QT_적용수량 = 0;
                        QT_MGMT_USE = 0;

                        //헤더의 처리 수량만큼 라인에 잔량에 반영하고 해당 로우를 체크해주는 부분(선입선출)
                        while (QT_적용수량 < QT_헤더수량)
                        {
                            ++j;
                            //할당대상 재고에 기할당된 수량 제외 20090107
                            QT_MGMT_USE = 0;

                            if (_flex적용.HasNormalRow)
                                QT_MGMT_USE = D.GetDecimal(_flex적용.DataTable.Compute("SUM(QT_GIR)", Filter));

                            if (D.GetDecimal(DR[j]["REQ_QT_GIR"]) - QT_MGMT_USE > 0)
                            {   //기할당한 수량제외하고도 할당할 수량이 있는것만 대상으로
                                QT_적용수량 = QT_적용수량 + (D.GetDecimal(DR[j]["REQ_QT_GIR"]) - QT_MGMT_USE);
                                DR[j]["QT_GIR"] = D.GetDecimal(DR[j]["REQ_QT_GIR"]) - QT_MGMT_USE;
                                DR[j]["S"] = "Y";
                            }
                        }
                        QT_적용수량 = QT_적용수량 - QT_헤더수량;
                        DR[j]["QT_GIR"] = D.GetDecimal(DR[j]["REQ_QT_GIR"]) - QT_적용수량;
                    }
                    else if (QT_적용수량 <= QT_헤더수량)
                    {
                        //str요청품목.AppendLine("거래처 : " + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "  품목 : " + D.GetString(_flex엑셀[k, "CD_ITEM"]));
                        //is요청품목 = false;
                        foreach (DataRow Dr_row in DR)
                        {
                            if (QT_헤더수량 - QT_MGMT_USE > D.GetDecimal(Dr_row["QT_GIR"]))
                                Dr_row["S"] = "Y";
                            else
                            {
                                Dr_row["QT_GIR"] = QT_헤더수량 - QT_MGMT_USE; //충분하거나 남으면 남은 잔량전부 할당
                                Dr_row["S"] = "Y";
                            }

                            str요청품목.AppendLine("거래처 : " + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "  품목 : " + D.GetString(_flex엑셀[k, "CD_ITEM"]) + " 요청수량 :" + D.GetString(_flex엑셀[k, "QT_SO"]) + " 의뢰수량 :" + D.GetString(Dr_row["QT_GIR"]));
                            is요청품목 = false;
                        }
                    }
                    else
                    {
                        Global.MainFrame.ShowMessage("거래처 = " + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "," + "품목 = " + D.GetString(_flex엑셀[k, "CD_ITEM"]) + "'의 입고수량이 부족하여 선입선출 할수 없습니다.");
                        return;
                    }

                    DataRow[] DR_1 = _flex수주.DataTable.Select("S = 'Y' AND CD_PARTNER = '" + D.GetString(_flex엑셀[k, "CD_PARTNER"]) + "' AND CD_ITEM = '" + D.GetString(_flex엑셀[k, "CD_ITEM"]) + "'");
                    foreach (DataRow row적용 in DR_1)
                    {
                        decimal qtso_AddAllow = D.GetDecimal(row적용["REQ_QT_GIR"]) + (D.GetDecimal(row적용["REQ_QT_GIR"]) * (D.GetDecimal(row적용["RT_PLUS"]) / 100));
                        if (D.GetDecimal(row적용["REQ_QT_GIR"]) < D.GetDecimal(row적용["QT_GIR"]) && qtso_AddAllowYN == "N")
                        {
                            row적용["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row적용["REQ_QT_GIR"]));
                            Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        }
                        else
                        {
                            if (qtso_AddAllow < D.GetDecimal(row적용["QT_GIR"]))
                            {
                                row적용["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row적용["REQ_QT_GIR"]));
                                Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                            }
                            else
                            {
                                row적용["UNIT_SO_FACT"] = D.GetDecimal(row적용["UNIT_SO_FACT"]) == 0 ? 1 : row적용["UNIT_SO_FACT"];
                                row적용["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row적용["QT_GIR"]) * D.GetDecimal(row적용["UM"]));    //수주금액(외화) = 수량 * 단가
                                row적용["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(row적용["AM_GIR"]) * D.GetDecimal(row적용["RT_EXCH"])));//수주금액(원화) = 수량 * 환율
                                row적용["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row적용["AM_GIRAMT"]) * D.GetDecimal(row적용["RT_VAT"]) / 100); //부가세 = 수주금액(원화) * 부가세율 / 100
                                row적용["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row적용["QT_GIR"]) * D.GetDecimal(row적용["UNIT_SO_FACT"]));
                                row적용["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row적용["AM_GIRAMT"]) + D.GetDecimal(row적용["AM_VAT"]));
                            }
                        }
                        row적용["GI_PARTNER"] = D.GetString(_flex엑셀[k, "GI_PARTNER"]);
                        row적용["DT_DUEDATE"] = D.GetString(_flex엑셀[k, "DT_DUEDATE"]);
                        row적용["DT_REQGI"] = D.GetString(_flex엑셀[k, "DT_REQGI"]);

                        dtL.ImportRow(row적용);
                    }

                    //라인 그리드에 선입 선출 된것을 맨 하단 그리드에 반영 하는 부분
                    if (dtL == null) return;
                    else
                    {
                        int idx = 0;

                        //입력된 데이터가 하나도 없는경우에는 바로 적용
                        if (_flex적용.DataTable != null)
                        {
                            DataRow[] dr_flexD = _flex적용.DataTable.Select(Filter, "", DataViewRowState.CurrentRows);

                            //입력된 데이터가 존재하는경우에는 같은 것을 두번 선입선출 적용 눌럿을때 반응 하지 않도록 필터링 걸어주는 부분
                            foreach (DataRow ddr in dr_flexD)
                            {
                                for (int i = 0; i < dtL.Rows.Count; i++)
                                {
                                    if (D.GetString(dtL.Rows[i]["NO_SO"]) == D.GetString(ddr["NO_SO"]) && D.GetString(dtL.Rows[i]["SEQ_SO"]) == D.GetString(ddr["SEQ_SO"]))
                                    {
                                        ddr["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dtL.Rows[i]["QT_GIR"]));

                                        decimal qtso_AddAllow = D.GetDecimal(ddr["REQ_QT_GIR"]) + (D.GetDecimal(ddr["REQ_QT_GIR"]) * (D.GetDecimal(ddr["RT_PLUS"]) / 100));

                                        if (D.GetDecimal(ddr["REQ_QT_GIR"]) < D.GetDecimal(ddr["QT_GIR"]) && qtso_AddAllowYN == "N")
                                        {
                                            ddr["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(ddr["REQ_QT_GIR"]));
                                            Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                                        }
                                        else
                                        {
                                            if (qtso_AddAllow < D.GetDecimal(ddr["QT_GIR"]))
                                            {
                                                ddr["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(ddr["REQ_QT_GIR"]));
                                                Global.MainFrame.ShowMessage("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                                            }
                                            else
                                            {
                                                ddr["UNIT_SO_FACT"] = D.GetDecimal(ddr["UNIT_SO_FACT"]) == 0 ? 1 : ddr["UNIT_SO_FACT"];
                                                ddr["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(ddr["QT_GIR"]) * D.GetDecimal(ddr["UM"]));    //수주금액(외화) = 수량 * 단가
                                                ddr["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(ddr["AM_GIR"]) * D.GetDecimal(ddr["RT_EXCH"])));//수주금액(원화) = 수량 * 환율
                                                ddr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(ddr["AM_GIRAMT"]) * D.GetDecimal(ddr["RT_VAT"]) / 100); //부가세 = 수주금액(원화) * 부가세율 / 100
                                                ddr["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(ddr["QT_GIR"]) * D.GetDecimal(ddr["UNIT_SO_FACT"]));
                                                ddr["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(ddr["AM_GIRAMT"]) + D.GetDecimal(ddr["AM_VAT"]));
                                            }
                                        }

                                    }
                                    idx++;
                                }
                            }
                        }

                        if (idx == 0)
                            _flex적용.BindingAdd(dtL, string.Empty, true);
                    }
                }
                _flex적용.Redraw = true;

                if (!is품목)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(str품목.ToString());
                    Global.MainFrame.ShowDetailMessage("가운데 그리드에 해당품목이 존재하지 않습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", sb.ToString());
                }

                if (!is요청품목)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(str요청품목.ToString());
                    Global.MainFrame.ShowDetailMessage("요청수량보다 의뢰적용 수량이 작은건." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", sb.ToString());
                }

                _flex적용.SumRefresh();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flex적용.Redraw = true;
            }
        }

        private void btn선입선출취소_Click(object sender, EventArgs e)
        {
            try
            {
                _flex적용.Binding = null;
                OnSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {

                Global.MainFrame.MsgEnd(ex);
            }
        }

        bool IsValidCol(DataTable dt, string ColName, string msg)
        {
            if (dt.Columns.Contains(ColName)) return true;

            Global.MainFrame.ShowMessage("컬럼정보가 존재하지 않습니다. => " + msg);
            return false;
        }
        #endregion

        private bool 중복체크(DataRow[] dr, string[] str_Filter, string ColName)
        {

            DataTable dt = ComFunc.getGridGroupBy(dr, str_Filter, true);

            if (dt.Rows.Count != 1)
            {
                Global.MainFrame.ShowMessage(공통메세지._의값이중복되었습니다, Global.MainFrame.DD(ColName));
                return false;
            }

            return true;
        }

        public DataTable Get수주적용Schema { get { return _biz.Get수주적용Schema; } }
        public DataTable Set수주적용내용 { set { dt_Append = value; } }
        
        public bool Set정상수주 { set { is정상수주 = value; } }

        public string Set영업그룹코드 { set { 영업그룹코드 = value; } }
        public string Set영업그룹명 { set { 영업그룹명 = value; } }
        public string Set과세구분 { set { 과세구분 = value; } }
        public string Set환종 { set { 환종 = value; } }
        public string Set계산서처리 { set { 계산서처리 = value; } }
        public string Set단가유형 { set { 단가유형 = value; } }
        public string Set납품처코드 { set { ctx납품처.CodeValue = value; } }
        public string Set납품처명 { set { ctx납품처.CodeName = value; } }
        public string Set수불유형 { set { 수불유형 = value; } }
    }
}
