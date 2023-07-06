using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;
using Duzon.ERPU.SA.Custmize;
using Duzon.ERPU.SA.Settng;
using Duzon.Windows.Print;
using DzHelpFormLib;
using pur;
using Duzon.BizOn.Erpu.Net.File;

namespace sale
{
    public partial class P_SA_SO : PageBase
    {
        #region ▶ 멤버필드        

        #region ▷ 클레스변수 선언             
        private P_SA_SO_BIZ _biz = new P_SA_SO_BIZ();
        private FreeBinding _header = new FreeBinding();
        CommonFunction _CommFun = new CommonFunction();
        private 수주관리.Config 수주Config = new 수주관리.Config();
        #endregion

        #region ▷ 전역변수 선언               
        string _수주상태 = string.Empty;    //수주상태 O(수주등록), R(수주확정)
        string _구분 = string.Empty;        //조회도움창에서 적용인지 복사인지 
        string _거래구분 = string.Empty;    //수주유형 선택시 거래구분을 넣어준다.
        string _출하형태 = string.Empty;    //수주유형 선택시 출하형태를 넣어준다.
        string _매출형태 = string.Empty;    //수주유형 선택시 매출형태를 넣어준다.
        string _의뢰여부 = string.Empty;    //수주유형 선택시 의뢰여부를 넣어준다.
        string _출하여부 = string.Empty;    //수주유형 선택시 출하여부를 넣어준다.
        string _매출여부 = string.Empty;    //수주유형 선택시 매출여부를 넣어준다.
        string _수출여부 = string.Empty;    //수주유형 선택시 수출여부를 넣어준다.
        string _반품여부 = string.Empty;    //수주유형 선택시 수출여부를 넣어준다.
        bool _헤더수정여부 = true;              //조회도움창에서 라인에 있는 수주상태가 하나라도 'O'가 아니면 false
        string _단가적용형태 = string.Empty;    //영업그룹을 선택시 단가정보(TP_SALEPRICE)도 가져온다.
        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        private string disCount_YN = "N";       //할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
        private string _수주번호 = string.Empty;
        #endregion

        #endregion

        #region ▶ 초기화          

        #region ▷ 생성자                      

        public P_SA_SO()
        {
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flex, _flexUser };
            this.DataChanged += new EventHandler(Page_DataChanged);

            _header = new FreeBinding();
            _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
            _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

            //영업환경설정 : 수주수량 초과허용:000, 재고단위EDIT여부(2중단위관리):001, 할인율 적용:002
            disCount_YN = BASIC.GetSAENV("002");
        }

        public P_SA_SO(string noSo) : this() { _수주번호 = noSo; }

        #endregion

        #region ▷ InitLoad                    

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        #endregion

        #region ▷ InitGrid                    

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false); 
            _flex.SetCol("CD_PLANT", "공장", 140, true);
            _flex.SetCol("CD_ITEM", "품목코드", 120, true);
            _flex.SetCol("NM_ITEM", "품목명", 120, false);
            _flex.SetCol("EN_ITEM", "품목명(영)", false);
            _flex.SetCol("STND_ITEM", "규격", 65, false);
            _flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            _flex.SetCol("UNIT_SO", "단위", 65, false);
            _flex.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("QT_SO", "수량", 60, 17, true, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("CD_EXCH", "화폐단위", false);     //화폐단위추가 2011.08.04 SJH

            // 장은경 : 2010.07.20
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                _flex.SetCol("UNIT_WEIGHT", "중량단위", 70, false);
                _flex.SetCol("WEIGHT", "중량", 60, 17, false, typeof(decimal)); _flex.Cols["WEIGHT"].Format = "#,###,###.####";
                _flex.SetCol("UM_OPT", "중량단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }
            _flex.SetCol("NUM_USERDEF1", "사용자정의1", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF1"].Visible = false;
            _flex.SetCol("NUM_USERDEF2", "사용자정의2", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF2"].Visible = false;

            _flex.SetCol("UM_SO", "단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (수주Config.부가세포함단가사용())
                _flex.SetCol("UMVAT_SO", "부가세포함단가", 100, 17, true, typeof(decimal), FormatTpType.UNIT_COST);

            if (disCount_YN == "Y")
            {
                _flex.SetCol("UM_BASE", "기준단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("RT_DSCNT", "할인율", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }

            _flex.SetCol("AM_SO", "금액", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);  
            _flex.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_VAT", "부가세", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AMVAT_SO", "합계금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_IM", "관리단위", 65, false);

            if(Sa_Global.Two_Unit_Mng)
                _flex.SetCol("QT_IM", "관리수량", 65, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            else
                _flex.SetCol("QT_IM", "관리수량", 65, 17, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("CD_SL", "창고코드", 80, true);
            _flex.SetCol("NM_SL", "창고명", 120, false);
            _flex.SetCol("TP_ITEM", "품목타입", false);
            _flex.SetCol("UNIT_SO_FACT", "수주단위수량", false);
            _flex.SetCol("LT_GI", "출하LT", false);
            _flex.SetCol("GI_PARTNER", "납품처코드", 120, true );
            _flex.SetCol("LN_PARTNER", "납품처명", 200, false);
            _flex.SetCol("NO_PROJECT", "프로젝트코드", 120, true);
            _flex.SetCol("NM_PROJECT", "프로젝트명", 140, false);
            _flex.SetCol("NO_PO_PARTNER", "거래처PO번호", 140, true);
            _flex.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, 3, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("DC1", "비고1", 100, true);
            _flex.SetCol("DC2", "비고2", 100, true);
            _flex.SetCol("FG_MODEL", "도면구분", 70, false);
            _flex.SetCol("FG_USE", "수주용도", 100, true);
            _flex.SetCol("NM_MANAGER1", "품목담당자", 70, false);

            if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
            {
                _flex.SetCol("UM_INV", "재고단가", 100, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
                _flex.SetCol("AM_PROFIT", "예상이익", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            }

            if (_biz.Get과세변경유무 == "Y")
            {
                _flex.SetCol("TP_VAT", "VAT구분", 80, true);
                _flex.SetCol("RT_VAT", "VAT율", 70, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            if (Sa_Global.SoL_CdCc_ModifyYN == "Y") //영업환경설정의 수주라인-C/C설정수정유무 추가 2010.04.06 NJin (Default Value = "N" 으로 셋팅)
            {
                _flex.SetCol("CD_CC", "코스트 센터", 100, true);
                _flex.SetCol("NM_CC", "코스트센터명", 120, false);
            } 

            _flex.SetCol("NO_IO_MGMT", "관련수불번호", 100);
            _flex.SetCol("NO_IOLINE_MGMT", "관련수불라인번호", 100, false, typeof(decimal));

            if (_biz.Get_WH적용 == "100")
            {
                _flex.SetCol("CD_WH", "W/H코드", 100, false);
                _flex.SetCol("NM_WH", "W/H명", 100, false);
            }

            _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = _biz.수주반품사용여부;

            _flex.SetCol("NO_SO_ORIGINAL", "원천수주번호", 100, false);
            _flex.SetCol("SEQ_SO_ORIGINAL", "원천수주항번", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("YN_ATP", "ATP적용여부", false);

            if (_biz.Get업체별프로세스 == "001")    //아사히카세이
            {
                _flex.SetCol("QT_WIDTH", "폭(㎜)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("QT_LENGTH", "길이(m)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("AREA", "면적(㎡)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("TOTAL_AREA", "총면적(㎡)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            else if (_biz.Get업체별프로세스 == "003")    //아카데미과학(PIMS:D20111115050)
            {
                _flex.SetCol("AM_PACKING", "수축비", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("QT_PACKING", "포장수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("PITEM_NUM_USERDEF4", "품목숫자사용자정의4", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF4"].Visible = false;
                _flex.SetCol("PITEM_NUM_USERDEF5", "품목숫자사용자정의5", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF5"].Visible = false;
                _flex.SetCol("PITEM_NUM_USERDEF6", "품목숫자사용자정의6", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF6"].Visible = false;
                _flex.SetCol("PITEM_NUM_USERDEF7", "품목숫자사용자정의7", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF7"].Visible = false;
            }
            else if (_biz.Get업체별프로세스 == "008")   //(주)아이코닉스엔터테인먼트
            {
                _flex.SetCol("PITEM_NUM_USERDEF1", "품목숫자사용자정의1", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF1"].Visible = false;
                _flex.SetCol("PITEM_NUM_USERDEF2", "품목숫자사용자정의2", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["PITEM_NUM_USERDEF2"].Visible = false;
            }

            _flex.SetCol("SL_QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            if (Use가용재고 || 서버키 == "KOREAF" || 서버키 == "DZSQL")
                _flex.SetCol("QT_USEINV", "가용재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            else
                _flex.SetCol("QT_USEINV", "가용재고", 0, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("NUM_USERDEF3", "사용자정의3", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF3"].Visible = false;
            _flex.SetCol("NUM_USERDEF4", "사용자정의4", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF4"].Visible = false;
            _flex.SetCol("NUM_USERDEF5", "사용자정의5", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF5"].Visible = false;
            _flex.SetCol("NUM_USERDEF6", "사용자정의6", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flex.Cols["NUM_USERDEF6"].Visible = false;

            if (서버키 == "MACROGEN")
            {
                _flex.SetCol("NM_MNGD1", "관리내역1", 100, true);
                _flex.SetCol("NM_MNGD2", "관리내역2", 100, true);
                _flex.SetCol("NM_MNGD3", "관리내역3", 100, true);
                _flex.SetCol("CD_MNGD4", "관리내역4", 100, true);
            }

            _flex.SetCol("TXT_USERDEF1", "TEXT사용자정의1", 150, true); _flex.Cols["TXT_USERDEF1"].Visible = false;
            _flex.SetCol("TXT_USERDEF2", "TEXT사용자정의2", 150, true); _flex.Cols["TXT_USERDEF2"].Visible = false;

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", "UNIT 항번", 100, false, typeof(decimal));
                _flex.SetCol("CD_UNIT", "UNIT 코드", 100, true);
                _flex.SetCol("NM_UNIT", "UNIT 명", 100, false);
                _flex.SetCol("STND_UNIT", "UNIT 규격", 100, false);
            }

            if (_biz.Get사양등록사용여부)
                _flex.SetCol("YN_OPTION", "옵션여부", 80, false, CheckTypeEnum.Y_N);

            if (_biz.Get_WH적용 == "100")
            {
                _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "CD_WH", "NM_WH", "GRP_MFG", "NM_GRP_MFG" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "CD_WH", "NM_WH", "GRP_MFG", "NM_GRP_MFG" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
                _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always,
                                      new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" },
                                      new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            }
            else
            {
                _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "GRP_MFG", "NM_GRP_MFG" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "GRP_MFG", "NM_GRP_MFG" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
                _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always,
                                  new string[] { "CD_SL", "NM_SL" },
                                  new string[] { "CD_SL", "NM_SL" }, new string[] { "SL_QT_INV", "QT_USEINV" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            }
            _flex.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, 
                                  new string [] { "GI_PARTNER", "LN_PARTNER" }, 
                                  new string [] { "CD_TPPTR", "NM_TPPTR" } );
            _flex.SetCodeHelpCol("CD_CC", Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB, ShowHelpEnum.Always,
                                  new string[] { "CD_CC", "NM_CC" },
                                  new string[] { "CD_CC", "NM_CC" });

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, ResultMode.SlowMode);
                _flex.SetCodeHelpCol("CD_UNIT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new string[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, ResultMode.SlowMode);
            }
            else
                _flex.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });

            _flex.SetCodeHelpCol("NM_MNGD1", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD1", "NM_MNGD1" }, new string[] { "CD_MNGD", "NM_MNGD" });
            _flex.SetCodeHelpCol("NM_MNGD2", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD2", "NM_MNGD2" }, new string[] { "CD_MNGD", "NM_MNGD" });
            _flex.SetCodeHelpCol("NM_MNGD3", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD3", "NM_MNGD3" }, new string[] { "CD_MNGD", "NM_MNGD" });

            if(Config.MA_ENV.YN_UNIT == "Y")
                _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "FG_MODEL", "NM_SL", "LN_PARTNER", "RT_VAT", "NM_CC", "NM_MANAGER1", "NO_IO_MGMT", "NO_IOLINE_MGMT", "NM_PROJECT", "NO_SO_ORIGINAL", "SEQ_SO_ORIGINAL", "SEQ_PROJECT", "NM_UNIT", "STND_UNIT");
            else
                _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "FG_MODEL", "NM_SL", "LN_PARTNER", "RT_VAT", "NM_CC", "NM_MANAGER1", "NO_IO_MGMT", "NO_IOLINE_MGMT", "NM_PROJECT", "NO_SO_ORIGINAL", "SEQ_SO_ORIGINAL");

            List<string> listNotNullCol = new List<string>();
            listNotNullCol.Add("CD_PLANT");     listNotNullCol.Add("CD_ITEM");      listNotNullCol.Add("DT_DUEDATE");   
            listNotNullCol.Add("DT_REQGI");     listNotNullCol.Add("TP_VAT");       listNotNullCol.Add("CD_CC");
            if (Use가용재고) listNotNullCol.Add("CD_SL");
            if (서버키 == "MACROGEN")
            {
                listNotNullCol.Add("NM_MNGD1");
                listNotNullCol.Add("NM_MNGD2");
                listNotNullCol.Add("CD_MNGD4");
            }
            if (Config.MA_ENV.YN_UNIT == "Y")
                listNotNullCol.Add("SEQ_PROJECT");

            _flex.VerifyNotNull = listNotNullCol.ToArray();
            _flex.VerifyCompare(_flex.Cols["QT_SO"], 0, OperatorEnum.Greater);
            _flex.VerifyCompare(_flex.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["QT_IM"], 0, OperatorEnum.Greater);

            _flex.SettingVersion = "1.0.3.3";

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);

            _flex.AddMenuSeperator();
            ToolStripMenuItem parent = _flex.AddPopup("관련 현황");
            _flex.AddMenuItem(parent, "현재고조회", Menu_Click);
            if (Use루미시트) _flex.AddMenuItem(parent, "품목수정", Menu_Click);
            _flex.AddMenuItem(parent, "창고별재고인쇄", Menu_Click);

            List<string> list = new List<string>();
            list.Add("UM_SO");
            list.Add("RT_VAT");
            list.Add("NUM_USERDEF1");
            list.Add("NUM_USERDEF2");

            if (disCount_YN == "Y")
            {
                list.Add("UM_BASE");
                list.Add("RT_DSCNT");
            }
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                list.Add("WEIGHT");
                list.Add("UM_OPT");
            }
            if (수주Config.부가세포함단가사용())
                list.Add("UMVAT_SO");

            if (Config.MA_ENV.YN_UNIT == "Y")
                list.Add("SEQ_PROJECT");

            _flex.SetExceptSumCol(list.ToArray());
            _flex.SetDummyColumn("AM_PROFIT");

            // 그리드 이벤트 선언
            _flex.HelpClick += new EventHandler(_flex_HelpClick);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.AddRow += new EventHandler(m_btnAppend_Click);

            switch (서버키)
            {
                case "KLW":
                    _flex.Cols["UM_SO"].AllowEditing = false;
                    break;
                case "KORAVL":
                    _flex.Cols["AM_WONAMT"].AllowEditing = true;
                    break;
                default:
                    break;
            }

            #region -> 루미시트 그리드 세팅
            if (Use루미시트)
            {
                _flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

                _flexUser.BeginSetting(1, 1, false);
                _flexUser.SetCol("CD_MATL", "소요자재코드", 100, true);
                _flexUser.SetCol("NM_ITEM", "소요자재명", 120, false);
                _flexUser.SetCol("STND_ITEM", "규격", 100, false);
                _flexUser.SetCol("STND_DETAIL_ITEM", "세부규격", 100, false);
                _flexUser.SetCol("QT_NEED", "소모수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexUser.SetCol("UNIT_IM", "단위", 50, false);
                _flexUser.SetCol("UNIT_GI_FACT", "출하단위수량/LED 모듈 총길이", 200, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexUser.SetCol("QT_NEED_UNIT", "단위별소요수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexUser.SetCol("UNIT_GI2", "출하단위수량/LED 모듈 길이(단위별)", 220, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexUser.SetCol("DC_RMK_1", "비고1", 150, true);
                _flexUser.SetCol("DC_RMK_2", "비고2", 150, true);
                _flexUser.SetCol("DC_RMK_3", "비고3", 150, true);
                _flexUser.SetCodeHelpCol("CD_MATL", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                      new string[] { "CD_MATL", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_IM", "UNIT_GI_FACT" },
                                      new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_IM", "UNIT_GI_FACT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
                _flexUser.SettingVersion = "1.0.0.9";
                _flexUser.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                _flexUser.AddMenuSeperator();
                ToolStripMenuItem parent2 = _flexUser.AddPopup("관련 현황");
                _flexUser.AddMenuItem(parent2, "현재고조회", Menu_Click2);

                _flexUser.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexUser_BeforeCodeHelp);
                _flexUser.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexUser_AfterCodeHelp);
                _flexUser.ValidateEdit += new ValidateEditEventHandler(_flexUser_ValidateEdit);
                _flexUser.StartEdit += new RowColEventHandler(_flexUser_StartEdit);
            }
            #endregion

            else if (_biz.Get사양등록사용여부)
            {
                _flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            }
        }

        #endregion

        #region ▷ InitPaint                   

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp수주일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.dtp수주일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            dtp납기일.Text = Global.MainFrame.GetStringToday;

            DataTable dt부가세 = MA.GetCode("MA_B000040");
            dt부가세.PrimaryKey = new DataColumn[] { dt부가세.Columns["CODE"] };

            SetControl str = new SetControl();
            str.SetCombobox(cbo화폐단위, MA.GetCode("MA_B000005"));

            DataTable dtFgUm;
            string ynMfgAuth = BASIC.GetMAENV("YN_MFG_AUTH");

            if (ynMfgAuth == "Y" && 서버키 == "CARGOTEC")
            {
                dtFgUm = _biz.SearchMfgAuth();
            }
            else
            {
                dtFgUm = MA.GetCode("SA_B000021", true);
            }

            str.SetCombobox(cbo단가유형, dtFgUm);
            str.SetCombobox(cbo부가세구분, dt부가세);
            str.SetCombobox(m_cboFgBill, MA.GetCode("SA_B000002", true));
            str.SetCombobox(cbo운송방법, MA.GetCode("TR_IM00008", true));
            str.SetCombobox(cbo부가세포함, MA.GetCode("YESNO"));
            str.SetCombobox(cboReason, MA.GetCode("SA_B000064", true)); //반품사유 콤보박스 추가 : 2011. 03. 24 SetControl str
            str.SetCombobox(cbo공장, MA.GetCode("MA_PLANT_AUTH"));
            str.SetCombobox(cbo결제형태, MA.GetCode("TR_IM00004", true));
            str.SetCombobox(cbo포장형태, MA.GetCode("TR_IM00011", true));
            str.SetCombobox(cbo운송방법, MA.GetCode("TR_IM00008", true));
            str.SetCombobox(cbo운송형태, MA.GetCode("TR_IM00009", true));
            str.SetCombobox(cbo원산지, MA.GetCode("MA_B000020", true));
            str.SetCombobox(cbo가격조건, MA.GetCode("TR_IM00002", true));

            cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;

            if (_biz.Get과세변경유무 == "Y")
                _flex.SetDataMap("TP_VAT", dt부가세.Copy(), "CODE", "NAME");
            _flex.SetDataMap("CD_PLANT", MA.GetCode("MA_PLANT_AUTH").Copy(), "CODE", "NAME");
            _flex.SetDataMap("UNIT_SO", MA.GetCode("MA_B000004", true), "CODE", "NAME");
            _flex.SetDataMap("UNIT_IM", MA.GetCode("MA_B000004", true), "CODE", "NAME");
            _flex.SetDataMap("FG_USE", MA.GetCode("SA_B000057", true), "CODE", "NAME"); //수주용도 
            _flex.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005", true), "CODE", "NAME");

            ctx창고.Clear();

            // 프리폼 초기화
            DataSet dsTemp = _biz.Search("#%#%");
            _header.SetBinding(dsTemp.Tables[0], m_tabSo);
            _header.ClearAndNewRow();

            _flex.Binding = dsTemp.Tables[1];

            영업그룹Default셋팅();
            수주형태Default셋팅();
            화폐단위Default셋팅();
            ControlVisibleSetting();
        }

        #endregion

        #region ▷ InitEvent                   

        void InitEvent()
        {
            btn견적적용.Click += new EventHandler(btn견적적용_Click);
            ctx창고.QueryAfter += new BpQueryHandler(Control_QueryAfter);
            btn_apply.Click += new EventHandler(btn_apply_Click);           //창고 적용
            btn적용.Click += new EventHandler(btn적용_Click);               //납품처 적용
            btn_Due_Apply.Click += new EventHandler(btn_Due_Apply_Click);   //납기일 적용
            btn프로젝트적용.Click += new EventHandler(btn프로젝트적용_Click);
            btn할인율적용.Click += new EventHandler(btn할인율적용_Click);

            btnBOM적용.Click += new EventHandler(btnBOM적용_Click);
            btn소요자재추가.Click += new EventHandler(btn소요자재추가_Click);
            btn소요자재삭제.Click += new EventHandler(btn소요자재삭제_Click);
            btn품목전개.Click += new EventHandler(btn품목전개_Click);
            btn전자결재.Click += new EventHandler(btn전자결재_Click);
            btn결제조건.Click += new EventHandler(btn결제조건_Click);

            cbo화폐단위.SelectionChangeCommitted += new EventHandler(Control_SelectionChangeCommitted);
            cbo부가세구분.SelectionChangeCommitted += new EventHandler(Control_SelectionChangeCommitted);
            cbo부가세포함.SelectionChangeCommitted += new EventHandler(Control_SelectionChangeCommitted);
            cbo공장.SelectionChangeCommitted += new EventHandler(Control_SelectionChangeCommitted);

            bp관리내역1.QueryBefore += new BpQueryHandler(Control_QueryBefore);
            bp관리내역2.QueryBefore += new BpQueryHandler(Control_QueryBefore);
            bp관리내역3.QueryBefore += new BpQueryHandler(Control_QueryBefore);

            ctx납품처.QueryAfter += new BpQueryHandler(Control_QueryAfter);
        }

        #endregion

        #endregion

        #region ▶ 메인버튼 클릭   

        #region ▷ 필수입력 체크               
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool FieldCheck(string flag)
        {
            Hashtable hList = new Hashtable();

            //프로젝트적용을 받을때에만 거래처랑 영업그룹이 셋팅되지 않아도 프로젝트 적용을 받을수 있도록 한다. 
            //프로젝트 적용을 받으면 기본적으로 프로젝트에 속한 거래처와 영업그룹이 지정되기 때문이다.
            if (flag != "btn_PRJ_SUB")
            {
                hList.Add(ctx거래처, m_lblIvPartner); //거래처
                hList.Add(ctx영업그룹, m_lblCdSalegrp);   //영업그룹

                if (App.SystemEnv.PROJECT사용)
                    hList.Add(ctx프로젝트, m_lblNoProject);
            }

            hList.Add(ctx수주형태, m_lblTpSo);      //수주유형
            hList.Add(cbo화폐단위, m_lblCdExch);    //화폐단위
            hList.Add(cbo단가유형, m_lblTpPrice);   //단가유형
            hList.Add(cbo부가세구분, m_lblTpVat);   //VAT구분
            hList.Add(dtp수주일자, m_lblDtSo);      //수주일자

            if (서버키 == "CSIT") hList.Add(cbo운송방법, m_lblFgTransport); //PIMS:D20111130014

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region ▷ 메인조회버튼 클릭           

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                P_SA_SO_SCH_SUB dlg = new P_SA_SO_SCH_SUB();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _수주상태 = dlg.수주상태;
                    _구분 = dlg.구분;
                    _헤더수정여부 = dlg.헤더수정유무;
                    _거래구분 = dlg.거래구분;
                    _출하형태 = dlg.출하형태;
                    _매출형태 = dlg.매출형태;
                    _의뢰여부 = dlg.의뢰여부;
                    _출하여부 = dlg.출하여부;
                    _매출여부 = dlg.매출여부;
                    _수출여부 = dlg.수출여부;
                    _단가적용형태 = dlg.단가적용형태;

                    SearchSo(dlg.수주번호);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 메인추가버튼 클릭           

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd())
                return false;

            if ( !MsgAndSave( PageActionMode.Search ) )
                return false;

            m_tabSo.SelectTab(tabPage1);

            return true;
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _구분 = string.Empty;
                _헤더수정여부 = true;

                this.Enabled = true;

                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();
                _header.ClearAndNewRow();

                if (Use루미시트 || _biz.Get사양등록사용여부)
                {
                    _flexUser.DataTable.Rows.Clear();
                    _flexUser.AcceptChanges();
                }

                영업그룹Default셋팅();
                수주형태Default셋팅();
                화폐단위Default셋팅();

                Authority(true);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 메인삭제버튼 클릭           

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;
           
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                return false;

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                if (_biz.Delete(txt수주번호.Text))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    OnToolBarAddButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 메인저장버튼 클릭           

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;
            if (!FieldCheck("") || !Chk수주일자 || !Chk담당자) return false;
            if (!this.Verify()) return false;
            if (!ChkBizarea()) return false;
            if (서버키 == "HKCOS" && _반품여부 == "Y")
            {
                if (!Chk반품사유) return false;
            }

            // 2010.09.13 장은경
            수주관리.Setting chk수주관리 = new 수주관리.Setting();
            chk수주관리.거래구분에따른과세구분(_거래구분, D.GetString(cbo부가세구분.SelectedValue));

            #region 영우일경우
            if (BASIC.GetMAEXC("여신한도") == "200")
            {
                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                        continue;

                    if (D.GetString(dr["CD_SL"]) == string.Empty)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, DD("창고"));
                        return false;
                    }

                    if (D.GetString(dr["NM_CUST_DLV"]) == string.Empty ||
                        D.GetString(dr["CD_ZIP"]) == string.Empty ||
                        D.GetString(dr["ADDR1"]) == string.Empty ||
                        D.GetString(dr["TP_DLV"]) == string.Empty)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, DD("배송정보"));
                        return false;
                    }
                }
            }
            #endregion

            #region 과세구분, 과세율, CC 체크
            foreach (DataRow dr in _flex.DataTable.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                    continue;

                if (D.GetString(dr["TP_VAT"]) == string.Empty)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _flex.Cols["TP_VAT"].Caption);
                    return false;
                }

                bool 과세변경유무 = 수주Config.부가세변경();

                if (!과세변경유무)
                {
                    if (D.GetString(cbo부가세구분.SelectedValue) != D.GetString(dr["TP_VAT"]))
                    {
                        ShowMessage("헤더와 라인의 과세구분이 일치하지 않습니다.");
                        return false;
                    }
                }

                decimal rtVat = BASIC.GetTPVAT(D.GetString(dr["TP_VAT"]));

                //면세나 영세가 아닌 과세 대상이 부가세율이 0 이면 바보다.
                if (D.GetDecimal(dr["RT_VAT"]) != rtVat)
                {
                    ShowMessage("라인부과세율 정보가 잘못되었습니다. \n\n 라인부과세율 정보를 확인하세요.");
                    return false;
                }

                if (D.GetString(dr["CD_CC"]) == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, _flex.Cols["CD_CC"].Caption);
                    return false;
                }

                if (Use가용재고 && D.GetDecimal(dr["UM_SO"]) == 0m)    //한국화장품 판매법인인 경우
                {
                    한국화장품 hkcos = new 한국화장품();
                    bool is필수단가체크 = hkcos.필수단가(D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), dtp수주일자.Text, D.GetString(cbo단가유형.SelectedValue), ctx수주형태.CodeValue, D.GetString(cbo화폐단위.SelectedValue), ctx창고.CodeValue);
                    if (!is필수단가체크) return false;
                }
            }
            #endregion

            if (_biz.GetATP사용여부 == "001")
            {
                if (!ATP체크로직(true)) return false;
            }

            if (Use가용재고 && !Chk가용재고()) return false;
            if (수주Config.결제조건도움창사용() && !Chk결제조건()) return false;

            return true;
        }
        
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _flex.Focus();

                if (!_flex.HasNormalRow && !추가모드여부)
                    OnToolBarDeleteButtonClicked(null, null);
                else
                {
                    if (MsgAndSave(PageActionMode.Save))
                        this.ShowMessage(PageResultMode.SaveGood);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!BeforeSave()) return false;
            if (!base.SaveData()) return false;

            string NO_SO = string.Empty;

            if (추가모드여부)
            {
                if (D.GetString(txt수주번호.Text) == string.Empty)
                {
                    NO_SO = (string)GetSeq(LoginInfo.CompanyCode, "SA", get_Ctrl(), dtp수주일자.Text.Substring(0, 6));//수주채번
                    txt수주번호.Text = NO_SO;
                }
                else
                {
                    if (!VerifyNoSo()) return false;
                    NO_SO = txt수주번호.Text;
                }

                _header.CurrentRow["NO_SO"] = NO_SO;
            }
            else
                NO_SO = txt수주번호.Text;

            if (서버키 == "FORTIS") //포티스 일 때 수주번호를 프로젝트번호에 세팅해준다.(2011.06.23)
            {
                _header.CurrentRow["NO_PROJECT"] = NO_SO;
                ctx프로젝트.SetCode(NO_SO, NO_SO);
            }

            DataTable dtH = new DataTable();
            DataTable dtL = new DataTable();
            DataTable dtLL = new DataTable();
            DataTable dtLot = new DataTable();

            dtH = _header.GetChanges();
            dtL = _flex.DataTable.Clone();
            dtLL = _flexUser.GetChanges();

            //복사일 경우에 상태값 초기화 시켜주는 것은 원래 조회후 바로 실행되어야 하는데 
            //기존에 내가 몰라서 이렇게 해놨음
            if (_구분 == "복사")
            {
                //복사일경우 저장시점에 수주유형을 읽어와서 수주상태를 체크한다.
                string[] TP_BUSI = new string[3];
                TP_BUSI = _biz.거래구분(ctx수주형태.CodeValue, D.GetString(cbo부가세구분.SelectedValue));

                if (D.GetString(TP_BUSI[2]) == "Y") //자동승인여부 "Y" 자동승인, "N" 자동승인안됨
                    _수주상태 = "R";
                else
                    _수주상태 = "O";

                foreach (DataRow row in _flex.DataTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;
                    {
                        //복사일경우에는 저장 시점에 수주상태를 체크하여 라인에 일괄 적용한다.
                        row["STA_SO1"] = _수주상태;
                        dtL.ImportRow(row);
                    }
                }
            }
            else
                dtL = _flex.GetChanges();

            if (dtH == null && dtL == null && dtLL == null)
                return true;

            #region -> 여신 & 미수채권일자 체크
            if (dtL != null && _수주상태 == "R")
            {
                if (_거래구분 == "001") //여신체크(거래구분이 국내인것만)
                {
                    decimal 금액 = 0;

                    if (_biz.GetExcCredit == "300") //환종별 여신사용
                    {
                        if (D.GetString(cbo화폐단위) == "000")
                            금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", ""));
                        else
                            금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_SO)", ""));

                        if(!_biz.CheckCreditExec(ctx거래처.CodeValue, D.GetString(cbo화폐단위.SelectedValue), Unit.원화금액(DataDictionaryTypes.SA, 금액)))
                            return false;
                    }
                    else
                    {
                        금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", ""));
                        if (!_biz.CheckCredit(ctx거래처.CodeValue, Unit.원화금액(DataDictionaryTypes.SA, 금액), _의뢰여부, _출하여부, ref _수주상태))
                            return false;
                    }
                }

                //미수채권일자 체크
                if (!IsAgingCheck()) return false;
            }
            #endregion

            //수주등록 추적컬럼 : 'M' 수주등록, 'P' 수주등록(거래처), 'H' 수주이력등록, 'W' 수주웹등록, 'ME' 일괄 수주등록, 'MEV' 일괄 수주등록(부가세포함), 'YV' 수주등록(용역)
            if (dtH != null)
                dtH.Rows[0]["FG_TRACK"] = "M";

            string fgVat = D.GetString(cbo부가세포함.SelectedValue);

            foreach (DataRow df in _flex.DataTable.Rows)
            {
                if (df.RowState == DataRowState.Deleted) continue;
                else if (df.RowState == DataRowState.Added || df.RowState == DataRowState.Modified)
                {
                    if (서버키 == "FORTIS") //포티스 일 때 수주번호를 프로젝트번호에 세팅해준다.(2011.06.23)
                    {
                        df["NO_PROJECT"] = NO_SO;
                    }
                    else
                    {
                        if (D.GetString(df["NO_PROJECT"]) == string.Empty)
                            df["NO_PROJECT"] = ctx프로젝트.CodeValue;
                    }

                    df["FG_TRACK"] = "SO";  //배송정보 TRACK 기능 => FG_TRACK : SO(수주등록), M(창고이동, 출고요청등록), R(출하반품의뢰등록)
                }
            }

            dtL = _flex.GetChanges();

            if (dtH == null && dtL == null && dtLL == null)
                return true;

            #region 자동프로세스(수주-의뢰-출하) 일 경우 로직

            if (_출하여부 == "Y")
            {
                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if (D.GetString(_flex[i, "CD_SL"]) == string.Empty)
                    {
                        ShowMessage(공통메세지._은는필수입력항목입니다, _flex.Cols["CD_SL"].Caption);
                        _flex.Select(i, _flex.Cols["CD_SL"].Index);
                        return false;
                    }
                }

                DataRow[] drs = _flex.DataTable.Select("FG_SERNO = '002'"); //002:LOT, 003:SERIAL

                if (Config.MA_ENV.LOT관리 && drs != null && drs.Length != 0)
                {
                    dtLot = _biz.dtLot_Schema(dtLot);

                    foreach (DataRow dr in drs)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        DataRow dr2 = dtLot.NewRow();
                        dr2["NO_IO"] = string.Empty;
                        dr2["NO_IOLINE"] = dr["SEQ_SO"];
                        dr2["NO_ISURCV"] = string.Empty;
                        dr2["NO_GIR"] = string.Empty;
                        dr2["DT_DUEDATE"] = dr["DT_DUEDATE"];  //납품일자
                        dr2["FG_TRANS"] = _거래구분;           //거래구분
                        dr2["CD_QTIOTP"] = _출하형태;          //출하형태(출고형태)
                        dr2["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, _출하형태 })["NM_QTIOTP"];  //출하형태(출고형태)
                        dr2["DT_IO"] = dtp수주일자.Text;      //출하일자
                        dr2["CD_SL"] = dr["CD_SL"];            //창고코드
                        dr2["NM_SL"] = dr["NM_SL"];            //창고명
                        dr2["CD_ITEM"] = dr["CD_ITEM"];        //품목코드
                        dr2["NM_ITEM"] = dr["NM_ITEM"];        //품목명
                        dr2["STND_ITEM"] = dr["STND_ITEM"];    //규격
                        dr2["UNIT"] = dr["UNIT_SO"];           //단위
                        dr2["UNIT_IM"] = dr["UNIT_IM"];        //단위
                        if (_반품여부 == "Y")
                            dr2["FG_IO"] = "041";               ////판매반품
                        else
                            dr2["FG_IO"] = "010";                  //판매출고
                        dr2["QT_GIR"] = dr["QT_SO"];
                        dr2["UNIT_SO_FACT"] = dr["UNIT_SO_FACT"];
                        dr2["QT_GIR_IM"] = dr["QT_IM"];
                        dr2["QT_IO"] = dr["QT_IM"];
                        dr2["QT_GOOD_INV"] = dr["QT_IM"];
                        dr2["CD_PLANT"] = dr["CD_PLANT"];
                        dr2["CD_PJT"] = dr["NO_PROJECT"];
                        dr2["NO_PROJECT"] = dr["NO_PROJECT"];
                        dr2["NM_PROJECT"] = dr["NM_PROJECT"];
                        dr2["NO_EMP"] = ctx담당자.CodeValue;
                        dr2["NO_LOT"] = "YES";
                        dr2["NO_SERL"] = "NO";
                        dr2["NO_PSO_MGMT"] = txt수주번호.Text;
                        dr2["NO_PSOLINE_MGMT"] = dr["SEQ_SO"];
                        dr2["NO_IO_MGMT"] = dr["NO_IO_MGMT"]; // 수주 반품시에만 사용함.
                        dr2["NO_IOLINE_MGMT"] = dr["NO_IOLINE_MGMT"]; // 수주 반품시에만 사용함.
                        dtLot.Rows.Add(dr2.ItemArray);
                    }

                    if (_반품여부 == "N")
                    {
                        string[] param = new string[3];
                        param[0] = "N";                     //YN_RETURN
                        param[1] = ctx프로젝트.CodeValue;   //NO_PROJECT
                        param[2] = ctx프로젝트.CodeName;    //NM_PROJECT

                        pur.P_PU_LOT_SUB_I m_dlg = new pur.P_PU_LOT_SUB_I(dtLot, param);

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtLot = m_dlg.dtL;
                        else
                            return false;
                    }
                    else if (_biz.수주반품사용여부 && _반품여부 == "Y")
                    {
                        pur.P_PU_LOT_SUB_R m_dlg = new pur.P_PU_LOT_SUB_R(dtLot);
                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtLot = m_dlg.dtL;
                        else
                            return false;
                    }

                    if (drs.Length != 0 && (dtLot == null || dtLot.Rows.Count == 0))
                    {
                        ShowMessage("LOT품목 수불이 발생하였으나 해당 LOT가 생성되지 않았습니다.");
                        return false;
                    }
                  
                    string str_filter = string.Empty;
                    string qt_colname = string.Empty;

                    foreach (DataRow dr in drs)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        decimal QT_GOOD_MNG_SUM = 0m;

                        if (_반품여부 == "N")
                        {
                            str_filter = "출고항번 = " + D.GetDecimal(dr["SEQ_SO"]) + "";
                            qt_colname = "QT_GOOD_MNG";
                        }
                        else
                        {
                            str_filter = "NO_IOLINE = " + D.GetDecimal(dr["SEQ_SO"]) + "";
                            qt_colname = "QT_IO";
                        }
                        
                        DataRow[] drs2 = dtLot.Select(str_filter);
                       
                        foreach (DataRow dr2 in drs2)
                        {
                            if (dr2.RowState == DataRowState.Deleted) continue;
                            if (D.GetString(dr["CD_ITEM"]) != D.GetString(dr["CD_ITEM"]))
                            {
                                ShowMessage("LOT품목과 수불품목이 일치하지 않습니다.");
                                return false;
                            }

                            QT_GOOD_MNG_SUM += D.GetDecimal(dr2[qt_colname]);
                        }

                        if (QT_GOOD_MNG_SUM != D.GetDecimal(dr["QT_IM"]))
                        {
                            ShowMessage("LOT수량과 수불수량이 일치하지 않습니다.");
                            return false;
                        }
                    }
                }
            }
    
            #endregion

            #region -> 수주등록 최저단가를 사용할 경우
            
            if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
            {
                StringBuilder strBuilder = new StringBuilder();
                bool b_최저단가존재여부 = true;

                string msg = DD("항번") + " " + DD("품번") + "                 " + DD("품목명") + "                               " + DD("최저단가") + "       " + DD("단가");
                strBuilder.AppendLine(msg);
                msg = "-".PadRight(92, '-');
                strBuilder.AppendLine(msg);

                foreach (DataRow dr in dtL.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;

                    if (D.GetDecimal(dr["UM_SO"]) < D.GetDecimal(dr["NUM_USERDEF1"]))
                    {
                        msg = D.GetString(dr["SEQ_SO"]).PadRight(4, ' ') + " " + D.GetString(dr["CD_ITEM"]).PadRight(20, ' ') + " " + D.GetString(dr["NM_ITEM"]).PadRight(36, ' ') + " "
                            + D.GetString(D.GetInt(dr["NUM_USERDEF1"])).PadRight(14, ' ') + " " + D.GetString(D.GetInt(dr["UM_SO"]));

                        strBuilder.AppendLine(msg);
                        b_최저단가존재여부 = false;
                    }
                }

                if (!b_최저단가존재여부)
                {
                    if(ShowDetailMessage("수주단가가 최저단가보다 적은 품목이 있습니다. 그래도 저장하시겠습니까?", "", strBuilder.ToString(), "QY2") == DialogResult.No)
                        return false;
                }
            }

            #endregion

            string[] strArr = new string[] { NO_SO, _수주상태, _거래구분, _출하형태, _매출형태, _의뢰여부, _출하여부, _매출여부, _수출여부, _구분, _반품여부 };
            bool bSuccess = _biz.Save(dtH, dtL, dtLL, dtLot, strArr, Use루미시트);
            if (!bSuccess) return false;

            foreach (DataRow row in _flex.DataTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                if (D.GetString(row["STA_SO1"]) == string.Empty)
                    row["STA_SO1"] = _수주상태;
            }

            if (_수주상태 == "R")
                _헤더수정여부 = false;

            _header.AcceptChanges();
            _flex.AcceptChanges();
            _flexUser.AcceptChanges();

            _구분 = "적용";
            Page_DataChanged(null, null);
            btn_ATP.Enabled = false;
            m_tabSo.SelectTab(tabPage1);
            return true;
        }

        #endregion

        #endregion

        #region ▷ 메인인쇄버튼 클릭           

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (추가모드여부) return;

                if (Use루미시트)
                {
                    if (IsChanged())
                    {
                        if (ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, "QY2") == DialogResult.No)
                            return;
                        if (SaveData()) return;
                    }
                    PrintRumiSheet prt = new PrintRumiSheet(txt수주번호.Text);
                    prt.Print();
                    return;
                }

                DataRow rowPartnerInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, ctx거래처.CodeValue });
                DataRow rowBizareaInfo = _biz.SerchBizarea(D.GetString(_flex[_flex.Rows.Fixed, "CD_PLANT"]));

                ReportHelper rptHelper = rptHelper = new ReportHelper("R_SA_SO_RPT_001", "수주서");

                rptHelper.SetData("NO_SO", txt수주번호.Text);                    
                rptHelper.SetData("DT_SO", dtp수주일자.Text);
                rptHelper.SetData("PARTNER", ctx거래처.CodeValue);   //거래처코드
                rptHelper.SetData("CD_PARTNER", ctx거래처.CodeName); //거래처명
                rptHelper.SetData("CD_SALEGRP", ctx영업그룹.CodeName); 
                rptHelper.SetData("NO_KOR", ctx담당자.CodeName); 
                rptHelper.SetData("TP_SO", ctx수주형태.CodeName);
                rptHelper.SetData("CD_EXCH", D.GetString(cbo화폐단위.SelectedValue));
                rptHelper.SetData("NM_CD_EXCH", cbo화폐단위.Text);
                rptHelper.SetData("RT_EXCH", D.GetString(cur환율.DecimalValue)); 
                rptHelper.SetData("TP_PRICE", cbo단가유형.Text);
                rptHelper.SetData("NO_PROJECT", ctx프로젝트.CodeName);
                rptHelper.SetData("TP_VAT", cbo부가세구분.Text); 
                rptHelper.SetData("RT_VAT", D.GetString(cur부가세율.DecimalValue));
                rptHelper.SetData("FG_VAT", cbo부가세포함.Text); 

                if (rdo계산서처리일괄.Checked)
                    rptHelper.SetData("FG_TAXP", rdo계산서처리일괄.Text); 
                else
                    rptHelper.SetData("FG_TAXP", rdo계산서처리건별.Text); 

                rptHelper.SetData("DC_RMK", txt비고.Text);
                rptHelper.SetData("FG_BILL", m_cboFgBill.Text);
                rptHelper.SetData("FG_TRANSPORT", cbo운송방법.Text);
                rptHelper.SetData("NO_CONTRACT", txt계약번호.Text);
                rptHelper.SetData("NO_PO_PARTNER", txt거래처PO.Text);
                rptHelper.SetData("NM_CEO", D.GetString(rowPartnerInfo["NM_CEO"]));
                rptHelper.SetData("NO_TEL", D.GetString(rowPartnerInfo["NO_TEL"]));
                rptHelper.SetData("NO_FAX", D.GetString(rowPartnerInfo["NO_FAX"]));
                rptHelper.SetData("NO_COMPANY", D.GetString(rowPartnerInfo["NO_COMPANY"]));
                rptHelper.SetData("NM_PTR", D.GetString(rowPartnerInfo["NM_PTR"]));
                rptHelper.SetData("TP_JOB", D.GetString(rowPartnerInfo["TP_JOB"]));
                rptHelper.SetData("CLS_JOB", D.GetString(rowPartnerInfo["CLS_JOB"]));
                rptHelper.SetData("DC_ADS1_H", D.GetString(rowPartnerInfo["DC_ADS1_H"]));
                rptHelper.SetData("DC_ADS1_D", D.GetString(rowPartnerInfo["DC_ADS1_D"]));
                rptHelper.SetData("DC_ADS1", D.GetString(rowPartnerInfo["DC_ADS1_H"]) + " " + D.GetString(rowPartnerInfo["DC_ADS1_D"]));
                rptHelper.SetData("AM_WONAMT_SUM", D.GetString(_flex[1, "AM_WONAMT"]));
                rptHelper.SetData("AM_VAT_SUM", D.GetString(_flex[1, "AM_VAT"]));
                rptHelper.SetData("AMVAT_SO_SUM", D.GetString(_flex[1, "AMVAT_SO"]));
                rptHelper.SetData("NM_BUSI", D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "PU_C000016", _거래구분 })["NM_SYSDEF"]));

                //해외정보추가(PIMS:D20120521044, 2012.05.29)
                rptHelper.SetData("NM_EXPORT", ctx수출자.CodeName);
                rptHelper.SetData("NM_PRODUCT", ctx제조자.CodeName);
                rptHelper.SetData("COND_TRANS", txt인도조건.Text);
                rptHelper.SetData("NM_INSPECT", txt검사기관.Text);
                rptHelper.SetData("DESTINATION", txt목적지.Text);
                rptHelper.SetData("NM_COND_PAY", cbo결제형태.Text);
                rptHelper.SetData("COND_DAYS", cur결제일.Text);
                rptHelper.SetData("DT_EXPIRY", dtp유효일자해외.Text);
                rptHelper.SetData("NM_TP_PACKING", cbo포장형태.Text);
                rptHelper.SetData("NM_TP_TRANSPORT", cbo운송형태.Text);
                rptHelper.SetData("NM_CD_ORIGIN", cbo원산지.Text);
                rptHelper.SetData("PORT_LOADING", txt선적항.Text);
                rptHelper.SetData("PORT_ARRIVER", txt도착항.Text);
                rptHelper.SetData("NM_COND_PRICE", cbo가격조건.Text);
                rptHelper.SetData("NM_NOTIFY", ctx착하통지처.CodeName);
                rptHelper.SetData("NM_CONSIGNEE", ctx수하인.CodeName);
                rptHelper.SetData("DC_RMK_TEXT", txt멀티비고.Text);
                rptHelper.SetData("DC_RMK1", txt비고1.Text);

                //사업장정보추가
                rptHelper.SetData("NO_BIZAREA", D.GetString(rowBizareaInfo["NO_BIZAREA"]));
                rptHelper.SetData("NM_BIZAREA", D.GetString(rowBizareaInfo["NM_BIZAREA"]));
                rptHelper.SetData("NM_MASTER_BIZAREA", D.GetString(rowBizareaInfo["NM_MASTER"]));
                rptHelper.SetData("ADS_H_BIZAREA", D.GetString(rowBizareaInfo["ADS_H"]));
                rptHelper.SetData("ADS_D_BIZAREA", D.GetString(rowBizareaInfo["ADS_D"]));
                rptHelper.SetData("ADS_BIZAREA", D.GetString(rowBizareaInfo["ADS_H"]) + " " + D.GetString(rowBizareaInfo["ADS_D"]));
                rptHelper.SetData("TP_JOB_BIZAREA", D.GetString(rowBizareaInfo["TP_JOB"]));
                rptHelper.SetData("CLS_JOB_BIZAREA", D.GetString(rowBizareaInfo["CLS_JOB"]));
                rptHelper.SetData("NO_TEL_BIZAREA", D.GetString(rowBizareaInfo["NO_TEL"]));

                DataTable dt = _flex.DataTable.Copy();
                dt.Columns.Add("QT_PACK", typeof(decimal));     //포장수량
                dt.Columns.Add("QT_REMAIN", typeof(decimal));   //잔량

                foreach (DataRow row in dt.Rows)
                {
                    row["QT_PACK"] = Decimal.Truncate(D.GetDecimal(row["QT_SO"]) / D.GetDecimal(row["UNIT_GI_FACT"]));
                    row["QT_REMAIN"] = Decimal.Remainder(D.GetDecimal(row["QT_SO"]), D.GetDecimal(row["UNIT_GI_FACT"]));
                }

                if (서버키 == "WONIK") _biz.Print(dt);
                if (서버키 == "KORAVL") dt = new DataView(_flex.DataTable, "CD_ITEM <> 'ZZ9999'", "", DataViewRowState.CurrentRows).ToTable();

                dt.AcceptChanges();
                rptHelper.SetDataTable(dt, 1);
                rptHelper.SetDataTable(dt, 2);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ▶ 화면내버튼 클릭 

        #region ▷ 견적적용                    

        void btn견적적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (일반추가() || 출하적용건())
                {
                    ShowMessage("견적적용이 아닌 데이터가 존재합니다.");
                    return;
                }

                P_SA_ESTMT_SUB dlg = new P_SA_ESTMT_SUB();
                dlg.Set거래처코드 = ctx거래처.CodeValue; dlg.Set거래처명 = ctx거래처.CodeName;
                dlg.Set영업그룹코드 = ctx영업그룹.CodeValue; dlg.Set영업그룹명 = ctx영업그룹.CodeName;
                if (!ctx담당자.IsEmpty())
                {
                    dlg.Set담당자코드 = ctx담당자.CodeValue; dlg.Set담당자명 = ctx담당자.CodeName;
                }
                dlg.Set수주형태코드 = ctx수주형태.CodeValue; dlg.Set수주형태명 = ctx수주형태.CodeName;
                dlg.Set부가세구분 = D.GetString(cbo부가세구분.SelectedValue);
                dlg.Set환종 = D.GetString(cbo화폐단위.SelectedValue);
                if (dlg.ShowDialog() != DialogResult.OK) return;

                DataRow row견적H = dlg.Get견적H;
                DataRow[] dr견적D = dlg.Get견적D;

                pnl해외.Enabled = true;
                견적H셋팅(row견적H);
                견적품목셋팅(dr견적D);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 프로젝트 적용               
        private void btn_PRJ_SUB_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FieldCheck("btn_PRJ_SUB")) return;

                if (견적적용건())
                {
                    ShowMessage("견적적용건이 존재합니다.");
                    return;
                }

                string[] Params = new string[5];
                Params[0] = ctx프로젝트.CodeValue;
                Params[1] = ctx프로젝트.CodeName;
                Params[2] = ctx거래처.CodeValue;
                Params[3] = ctx거래처.CodeName;
                Params[4] = D.GetString(cbo부가세구분.SelectedValue);

                P_SA_SO_PRJ_SUB dlg = new P_SA_SO_PRJ_SUB(Params);
                DataSet dsReturn = new DataSet();

                if (dlg.ShowDialog() != DialogResult.OK) return;

                dsReturn = dlg.ReturnDataSet;

                if (dsReturn == null || dsReturn.Tables.Count == 0) return;

                DataRow rowReturn = dsReturn.Tables[0].Rows[0];

                ctx프로젝트.SetCode(D.GetString(rowReturn["NO_PROJECT"]), D.GetString(rowReturn["NM_PROJECT"]));
                ctx거래처.SetCode(D.GetString(rowReturn["CD_PARTNER"]), D.GetString(rowReturn["LN_PARTNER"]));
                ctx영업그룹.SetCode(D.GetString(rowReturn["CD_SALEGRP"]), D.GetString(rowReturn["NM_SALEGRP"]));

                영업그룹변경시셋팅(ctx영업그룹.CodeValue);

                string giPartner = D.GetString(rowReturn["GI_PARTNER"]);
                string gnPartner = D.GetString(rowReturn["GN_PARTNER"]);

                ctx납품처.CodeValue = giPartner == string.Empty ? ctx거래처.CodeValue : giPartner;
                ctx납품처.CodeName = gnPartner == string.Empty ? ctx거래처.CodeName : gnPartner;

                if (서버키 == "TAERYUK" && (MA.Login.회사코드 == "2000" || MA.Login.회사코드 == "8000"))
                {
                    DataRow row납품처정보 = BASIC.GetPartner(ctx납품처.CodeValue);
                    txt비고1.Text = (D.GetString(row납품처정보["DC_ADS1_H"]) + " " + D.GetString(row납품처정보["DC_ADS1_D"])).Trim();
                    _header.CurrentRow["DC_RMK1"] = txt비고1.Text;
                }

                txt비고.Text = D.GetString(rowReturn["DC_RMK"]);  // 2011.04.15 추가
                _header.CurrentRow["DC_RMK"] = txt비고.Text;

                if (_biz.Get프로젝트적용 == "001")
                {
                    ctx담당자.CodeValue = D.GetString(rowReturn["NO_EMP"]);
                    ctx담당자.CodeName = D.GetString(rowReturn["NM_KOR"]);
                    _header.CurrentRow["NO_EMP"] = D.GetString(rowReturn["NO_EMP"]);

                    txt비고.Text = D.GetString(rowReturn["NM_PROJECT"]);
                    _header.CurrentRow["DC_RMK"] = D.GetString(rowReturn["NM_PROJECT"]);
                }

                if (dtp납기일.Text == string.Empty)
                    dtp납기일.Text = D.GetString(dsReturn.Tables[1].Rows[0]["DT_DUEDATE"]);

                _header.CurrentRow["NO_PROJECT"] = D.GetString(rowReturn["NO_PROJECT"]);
                _header.CurrentRow["CD_PARTNER"] = D.GetString(rowReturn["CD_PARTNER"]);
                _header.CurrentRow["CD_SALEGRP"] = D.GetString(rowReturn["CD_SALEGRP"]);

                if (!(D.GetString(rowReturn["FG_VAT"]) == "Y" && D.GetString(cbo부가세포함.SelectedValue) == "Y"))
                {
                    cbo부가세포함.SelectedValue = "N";
                    _header.CurrentRow["FG_VAT"] = "N";
                }

                if (서버키 == "KORAVL" || 서버키 == "CARGOTEC") //2012.04.19 한국AVL(PIMS:D20120409075)
                {
                    string oldTpSo = ctx수주형태.CodeValue;
                    string pjtTpSo = D.GetString(rowReturn["TP_SO"]);
                    string pjtCdExch = D.GetString(rowReturn["CD_EXCH"]);

                    if (pjtTpSo != string.Empty && oldTpSo != pjtTpSo)
                    {
                        ctx수주형태.SetCode(D.GetString(rowReturn["TP_SO"]), D.GetString(rowReturn["NM_SO"]));
                        _header.CurrentRow["TP_SO"] = ctx수주형태.CodeValue;
                        수주형태변경시셋팅(ctx수주형태.CodeValue);
                    }
                    if (pjtCdExch != string.Empty)
                    {
                        cbo화폐단위.SelectedValue = D.GetString(rowReturn["CD_EXCH"]);
                        cur환율.DecimalValue = D.GetDecimal(rowReturn["RT_EXCH"]);
                        _header.CurrentRow["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                        _header.CurrentRow["RT_EXCH"] = cur환율.DecimalValue;

                        if (pjtCdExch != "000")
                        {
                            if (MA.기준환율.Option != MA.기준환율옵션.적용_수정불가)
                                cur환율.Enabled = true;
                        }
                    }
                }

                DataTable dt = null;
                dt = _flex.DataTable.Clone();
                decimal idx = _flex.DataView.Table.Rows.Count + 1;
                string cdCc, nmCc;

                foreach (DataRow dr in dsReturn.Tables[1].Rows)
                {
                    DataRow row = dt.NewRow();

                    row["SEQ_SO"] = idx;    //수주라인항번:원래 프로젝트 항번과 동일했으나 배송지를 분할하기 위해서 라인단위로 항번을 만들어 준다.
                    idx++;

                    row["NO_PROJECT"] = D.GetString(dr["NO_PROJECT"]);          //프로젝트번호
                    row["NM_PROJECT"] = D.GetString(rowReturn["NM_PROJECT"]);   //프로젝트명
                    row["SEQ_PROJECT"] = D.GetString(dr["SEQ_PROJECT"]);        //프로젝트라인항번
                    row["CD_PLANT"] = D.GetString(dr["CD_PLANT"]);
                    row["CD_ITEM"] = D.GetString(dr["CD_ITEM"]);
                    row["NM_ITEM"] = D.GetString(dr["NM_ITEM"]);
                    row["STND_ITEM"] = D.GetString(dr["STND_ITEM"]);
                    row["UNIT_SO"] = D.GetString(dr["UNIT"]);

                    row["EN_ITEM"] = dr["EN_ITEM"];
                    row["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM"];
                    row["TP_ITEM"] = dr["TP_ITEM"];
                    row["GRP_MFG"] = dr["GRP_MFG"];
                    row["LT_GI"] = D.GetDecimal(dr["LT_GI"]);
                    row["WEIGHT"] = dr["WEIGHT"];
                    row["UNIT_WEIGHT"] = dr["UNIT_WEIGHT"];
                    row["FG_SERNO"] = dr["FG_SERNO"];
                    row["YN_ATP"] = dr["YN_ATP"];
                    row["CUR_ATP_DAY"] = dr["CUR_ATP_DAY"];
                    row["FG_MODEL"] = dr["FG_MODEL"];

                    if (D.GetString(dr["DT_DUEDATE"]) == string.Empty)
                    {
                        if (dtp납기일.Text != string.Empty)
                        {
                            row["DT_DUEDATE"] = dtp납기일.Text;
                            row["DT_REQGI"] = _CommFun.DateAdd(dtp납기일.Text, "D", D.GetInt(row["LT_GI"]) * -1);
                        }
                    }
                    else
                    {
                        row["DT_DUEDATE"] = D.GetString(dr["DT_DUEDATE"]);  // 2011.04.15 추가 납기예정일
                        row["DT_REQGI"] = _CommFun.DateAdd(D.GetString(dr["DT_DUEDATE"]), "D", D.GetInt(row["LT_GI"]) * -1);
                    }

                    row["DC1"] = D.GetString(dr["DC_RMK9"]);                // 2011.04.15 추가 비고

                    if (수주Config.수주라인CC설정유형() == 수주관리.수주라인CC설정.프로젝트라인)
                    {
                        cdCc = D.GetString(dr["CD_CC"]);
                        nmCc = D.GetString(dr["NM_CC"]);
                    }
                    else
                    {
                        CC조회(D.GetString(dr["CD_ITEMGRP"]), out cdCc, out nmCc);
                    }

                    row["CD_CC"] = cdCc;
                    row["NM_CC"] = nmCc;
                    row["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                    row["RT_VAT"] = cur부가세율.DecimalValue;
                    row["GI_PARTNER"] = ctx납품처.CodeValue;
                    row["LN_PARTNER"] = ctx납품처.CodeName;
                    row["QT_SO"] = D.GetString(dr["QT_PROJECT"]);
                    row["UNIT_SO_FACT"] = D.GetDecimal(dr["UNIT_SO_FACT"]) == 0m ? 1m : dr["UNIT_SO_FACT"];
                    row["UNIT_GI_FACT"] = D.GetDecimal(dr["UNIT_GI_FACT"]) == 0m ? 1m : dr["UNIT_GI_FACT"];
                    row["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) * (D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"])));
                    row["UNIT_IM"] = D.GetString(dr["UNIT_IM"]);

                    if (Use부가세포함)
                    {
                        row["UMVAT_SO"] = dr["UM_WON"];
                        row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) * D.GetDecimal(row["UMVAT_SO"]));
                        row["AM_WONAMT"] = Decimal.Round(D.GetDecimal(row["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(row["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        row["AM_VAT"] = D.GetDecimal(row["AMVAT_SO"]) - D.GetDecimal(row["AM_WONAMT"]);
                        row["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) / cur환율.DecimalValue);
                        row["UM_SO"] = D.GetDecimal(row["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) / D.GetDecimal(row["QT_SO"]));
                    }
                    else
                    {
                        //거래구분이 국내건이면서 환종이 원화인 것만 수주의 단가/금액에 프로젝트의 원화단가/원화금액을 셋팅해준다.
                        if (_거래구분 == "001" && D.GetString(cbo화폐단위.SelectedValue) == "000") //KRW
                        {
                            dr["UM"] = dr["UM_WON"];
                            dr["AM_PROJECT"] = dr["AM_WONAMT"];
                        }

                        if (disCount_YN == "N")
                            row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM"]));
                        else if (disCount_YN == "Y")
                        {
                            row["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM"]));
                            row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_BASE"]) - (D.GetDecimal(row["UM_BASE"]) * D.GetDecimal(row["RT_DSCNT"])) / 100);
                        }

                        row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_PROJECT"]));
                        if (서버키 == "KORAVL")
                            row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]));
                        else
                            row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) * cur환율.DecimalValue);
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                        row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]));
                        if (D.GetDecimal(row["QT_SO"]) != 0)
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));
                        else
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]));
                    }

                    row["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                    row["CD_MNGD1"] = bp관리내역1.CodeValue;
                    row["NM_MNGD1"] = bp관리내역1.CodeName;
                    row["CD_MNGD2"] = bp관리내역2.CodeValue;
                    row["NM_MNGD2"] = bp관리내역2.CodeName;
                    row["CD_MNGD3"] = bp관리내역3.CodeValue;
                    row["NM_MNGD3"] = bp관리내역3.CodeName;
                    row["CD_MNGD4"] = txt관리내역4.Text;

                    if (Config.MA_ENV.YN_UNIT == "Y")
                    {
                        row["CD_UNIT"] = dr["CD_ITEM"];
                        row["NM_UNIT"] = dr["NM_ITEM"];
                        row["STND_UNIT"] = dr["STND_ITEM"];
                    }

                    dt.Rows.Add(row);
                }

                _flex.BindingAdd(dt, "", false);
                Authority(false);

                //한국AVL에선 프로젝트 적용 받아온 후에도 거래처 수정할 수 있게한다.(PIMS:D20120409075, 2012.04.19)
                //            부가세구분도 수정 할 수 있도록 한다(PIMS:D20120521047, 2012.05.25)
                if (서버키 == "KORAVL")
                {
                    ctx거래처.Enabled = true;
                }
                else
                {
                    if (!ConfigSA.SA_EXC.수주라인_VAT수정 && _flex.HasNormalRow)
                        cbo부가세구분.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ▷ 단가적용                    

        private void btn단가적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataSet ds의뢰 = null;
                DataTable dt의뢰내역 = null;
                DataTable dt의뢰않된내역 = null;

                _flex.Redraw = false;

                if (추가모드여부)
                {
                    단가변경();
                    return;
                }

                /* 이미 저장된 수주에 대해서는 의뢰내역을 체크한다 */
                ds의뢰 = _biz.의뢰된건조회(txt수주번호.Text);
                dt의뢰내역 = ds의뢰.Tables[0];
                dt의뢰않된내역 = ds의뢰.Tables[1];

                StringBuilder 검증리스트 = new StringBuilder();

                #region -> 의뢰내역 추가

                string msg = DD("품목코드") + "\t " + DD("품목명") + "\t";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);

                검증리스트.AppendLine("의뢰내역.......................");

                foreach (DataRow dr in dt의뢰내역.Rows)
                {
                    msg = D.GetString(dr["CD_ITEM"]) + "\t" + D.GetString(dr["NM_ITEM"]);
                    검증리스트.AppendLine(msg);
                }

                검증리스트.AppendLine(Environment.NewLine);

                #endregion

                #region -> 의뢰되지 않은내역 추가

                검증리스트.AppendLine("의뢰내역되지 않은내역..........");

                foreach (DataRow dr in dt의뢰않된내역.Rows)
                {
                    msg = D.GetString(dr["CD_ITEM"]) + "\t" + D.GetString(dr["NM_ITEM"]);
                    검증리스트.AppendLine(msg);
                }

                #endregion

                if (dt의뢰않된내역.Rows.Count == 0) //의뢰 않된 내역이 없으면 작업을 진행할 수 없다.
                {
                    ShowMessage("모든 품목이 의뢰가 된 건입니다. 작업을 진행할 수 없습니다.");
                    return;
                }

                if (dt의뢰내역.Rows.Count > 0)  //의뢰된 내역이 있으면
                {
                    DialogResult dlg = ShowDetailMessage("의뢰된 내역이 있습니다. 의뢰되지 않은 건에 대해서 계속 진행하시겠습니까?", "", 검증리스트.ToString(), "QY2");

                    if (dlg == DialogResult.No) return;
                }

                dt의뢰내역.PrimaryKey = new DataColumn[] { dt의뢰내역.Columns["SEQ_SO"] };

                for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                {
                    DataRow dr = _flex.DataTable.Rows[i];

                    if (dr.RowState == DataRowState.Deleted) continue;

                    DataRow drFind = dt의뢰내역.Rows.Find(dr["SEQ_SO"]);

                    if (drFind != null) continue;

                    단가변경(ref dr);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #region -> 단가변경()
        /// <summary>
        /// 단가적용에서 받아온 단가를 이용해서 그리드 내역 전체의 단가를 변경한다.
        /// </summary>
        private void 단가변경()
        {
            for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
            {
                DataRow dr = _flex.DataTable.Rows[i];

                if (dr.RowState == DataRowState.Deleted) continue;

                단가변경(ref dr);
            }
        }

        #endregion

        #region -> 단가변경(ref DataRow dr)
        /// <summary>
        /// 단가적용에서 받아온 단가를 이용해서 해당 DataRow의 단가를 변경한다.
        /// </summary>
        /// <param name="dr"></param>
        private void 단가변경(ref DataRow dr)
        {
            object UM_BASE;
            decimal dUM_BASE = 0m;
            decimal dUM_TEMP = 0m;

            bool bChangeUM = false;

            switch (서버키)
            {
                case "KLW":     // 로스트왁스
                    dr["NUM_USERDEF1"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, "010", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));
                    dr["NUM_USERDEF2"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, "020", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));
                    dr["UM_SO"] = D.GetDecimal(dr["NUM_USERDEF1"]) + D.GetDecimal(dr["NUM_USERDEF2"]);
                    bChangeUM = true;
                    break;
                case "HKCOS":   // 한국화장품
                    한국화장품 hkcos = new 한국화장품();
                    decimal 한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, hkcos.단가(D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]), dtp수주일자.Text, D.GetString(cbo단가유형.SelectedValue), ctx수주형태.CodeValue, D.GetString(cbo화폐단위.SelectedValue), ctx창고.CodeValue, ctx거래처.CodeValue));
                    dr["UM_BASE"] = 한국화장품단가;
                    한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, 한국화장품단가 - (한국화장품단가 * D.GetDecimal(dr["RT_DSCNT"]) * 0.01M));
                    dr["UM_SO"] = Decimal.Round(한국화장품단가, MidpointRounding.AwayFromZero);
                    bChangeUM = true;
                    break;
                default:
                    if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                    {
                        if (_단가적용형태 != "002" && _단가적용형태 != "003") break;
                        UM_BASE = _biz.UmSearch(D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text);
                        if (UM_BASE == null || UM_BASE == DBNull.Value) break;
                        else dUM_BASE = D.GetDecimal(UM_BASE);

                        if (Use부가세포함)    //부가세포함단가 사용 시 할인율은 일단 배제하였음(2011.11.11)
                        {
                            dUM_BASE = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);

                            if (dUM_BASE != D.GetDecimal(dr["UMVAT_SO"]))
                            {
                                dr["UMVAT_SO"] = dUM_BASE;
                                bChangeUM = true;
                            }
                        }
                        else
                        {
                            dUM_BASE = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);

                            if (disCount_YN == "N")
                            {
                                if (dUM_BASE != D.GetDecimal(dr["UM_SO"]))
                                {
                                    dr["UM_SO"] = dUM_BASE;
                                    bChangeUM = true;
                                }
                            }
                            else if (disCount_YN == "Y")
                            {
                                if (dUM_BASE != D.GetDecimal(dr["UM_BASE"]))
                                {
                                    dr["UM_BASE"] = dUM_BASE;
                                    dr["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE - (dUM_BASE * D.GetDecimal(dr["RT_DSCNT"])) / 100);
                                    bChangeUM = true;
                                }
                            }
                        }
                    }
                    else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                    {
                        if (_단가적용형태 != "002" && _단가적용형태 != "003") break;
                        UM_BASE = _biz.UmSearch(D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text);
                        if (UM_BASE == null || UM_BASE == DBNull.Value) break;
                        else dUM_BASE = D.GetDecimal(UM_BASE);

                        dUM_BASE = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);

                        if (dUM_BASE != D.GetDecimal(dr["UM_OPT"]))
                        {
                            dr["UM_OPT"] = dUM_BASE;
                            bChangeUM = true;
                        }

                        특수단가사용시단가계산(ref dr);

                        if (disCount_YN == "Y")
                        {
                            dUM_TEMP = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_BASE"]) - (D.GetDecimal(dr["UM_BASE"]) * D.GetDecimal(dr["RT_DSCNT"])) / 100);
                            if (dUM_TEMP != D.GetDecimal(dr["UM_SO"]))
                            {
                                dr["UM_SO"] = dUM_TEMP;
                                bChangeUM = true;
                            }
                        }
                    }
                    else if (_biz.Get특수단가적용 == 특수단가적용.조선호텔베이커리단가) //2011.12.21 SJH(PIMS:D20111205101) : 조선호텔 베이커리는 할인율을 안쓰는걸로 가정한다.
                    {
                        UM_BASE = _biz.조선호텔베이커리단가(D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, dtp수주일자.Text, D.GetString(cbo화폐단위.SelectedValue));
                        if (UM_BASE == null || UM_BASE == DBNull.Value) break;
                        else dUM_BASE = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(UM_BASE));

                        if (dUM_BASE != D.GetDecimal(dr["UM_SO"]))
                        {
                            dr["UM_SO"] = dUM_BASE;
                            bChangeUM = true;
                        }
                    }
                    else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                    {
                        DataTable dtUmFixed = _biz.SearchUmFixed(ctx거래처.CodeValue, D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]));

                        if (dtUmFixed == null || dtUmFixed.Rows.Count == 0)
                        {
                            if (_단가적용형태 != "002" && _단가적용형태 != "003") break;
                            UM_BASE = _biz.UmSearch(D.GetString(dr["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text);
                            if (UM_BASE == null || UM_BASE == DBNull.Value) break;
                            else dUM_BASE = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(UM_BASE));
                        }
                        else
                            dUM_BASE = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dtUmFixed.Rows[0]["UM_FIXED"]));

                        if (dUM_BASE != D.GetDecimal(dr["UM_SO"]))
                        {
                            dr["UM_SO"] = dUM_BASE;
                            bChangeUM = true;
                        }
                    }
                    break;
            }

            if (bChangeUM)
            {
                if (Use부가세포함)
                {
                    dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_SO"]) * D.GetDecimal(dr["UMVAT_SO"]));
                    dr["AM_WONAMT"] = Decimal.Round(D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))), MidpointRounding.AwayFromZero);
                    dr["AM_VAT"] = D.GetDecimal(dr["AMVAT_SO"]) - D.GetDecimal(dr["AM_WONAMT"]);
                    dr["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) / cur환율.DecimalValue);
                    dr["UM_SO"] = D.GetDecimal(dr["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]) / D.GetDecimal(dr["QT_SO"]));
                }
                else
                {
                    dr["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]) * D.GetDecimal(dr["QT_SO"]));
                    dr["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]) * cur환율.DecimalValue);
                    dr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) * (D.GetDecimal(dr["RT_VAT"]) / 100));
                    dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) + D.GetDecimal(dr["AM_VAT"]));

                    if (D.GetDecimal(dr["QT_SO"]) != 0)
                        dr["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]) / D.GetDecimal(dr["QT_SO"]));
                    else
                        dr["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]));
                }

                if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                {
                    수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                    dr["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(dr["QT_SO"]), D.GetDecimal(dr["UM_INV"]), D.GetDecimal(dr["AM_WONAMT"]));
                }
                    
                if (_biz.Get업체별프로세스 == "001")
                    dr["NUM_USERDEF1"] = D.GetDecimal(dr["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]) / D.GetDecimal(dr["AREA"]));
            }
        }

        #endregion

        #endregion

        #region ▷ ATP 체크                    

        private void btn_ATP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                    return;

                if (ATP체크로직(false))
                    ShowMessage("납기일에 이상이 없습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 출하적용                    

        private void 출하적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk수주일자 || !Chk거래처 || !Chk수주유형 || !Chk화폐단위 || !Chk단가유형 || !Chk과세구분 || !Chk공장) return;

                if (일반추가() || 견적적용건())
                {
                    ShowMessage("출하적용이 아닌 데이터가 존재합니다.");
                    return;
                }

                string 공장코드 = D.GetString(cbo공장.SelectedValue);
                string 공장명 = cbo공장.Text;

                if (!m_pnlBaseInfo.Enabled)
                {
                    공장코드 = Global.MainFrame.LoginInfo.CdPlant;
                    공장명 = Global.MainFrame.LoginInfo.NmPlant;
                }

                string 거래처코드 = ctx거래처.CodeValue;
                string 거래처명 = ctx거래처.CodeName;
                string 과세구분코드 = D.GetString(cbo부가세구분.SelectedValue);
                string 과세구분명 = cbo부가세구분.Text;

                P_SA_GIRR_REG_SUB dlg = new P_SA_GIRR_REG_SUB(공장코드, 공장명, 거래처코드, 거래처명, 과세구분코드, 과세구분명);
                dlg.Set수주등록출하적용 = true;
                dlg.Set프로젝트사용 = App.SystemEnv.PROJECT사용;
                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (dlg.출하테이블.Rows.Count == 0) return;

                DataTable dt = _flex.DataTable.Clone();

                decimal idx = 1;
                string cdCc, nmCc;

                string multiItem = Common.MultiString(dlg.출하테이블, "CD_ITEM", "|");
                DataTable dtQtInv = BASIC.GetQtInvMulti(multiItem, dtp수주일자.Text);
                dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_SL"], dtQtInv.Columns["CD_ITEM"] };

                foreach (DataRow dr in dlg.출하테이블.Rows)
                {
                    DataRow row = dt.NewRow();

                    row["SEQ_SO"] = idx;       //수주라인항번  
                    idx++;

                    row["NO_PROJECT"] = dr["NO_PROJECT"];      //프로젝트번호
                    row["SEQ_PROJECT"] = dr["SEQ_PROJECT"];
                    row["CD_PLANT"] = dr["CD_PLANT"];
                    row["CD_ITEM"] = dr["CD_ITEM"];
                    row["NM_ITEM"] = dr["NM_ITEM"];
                    row["EN_ITEM"] = dr["EN_ITEM"];
                    row["STND_ITEM"] = dr["STND_ITEM"];
                    row["UNIT_SO"] = dr["UNIT_SO"];
                    row["TP_ITEM"] = dr["TP_ITEM"];

                    row["EN_ITEM"] = dr["EN_ITEM"];
                    row["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM"];
                    row["GRP_MFG"] = dr["GRP_MFG"];
                    row["LT_GI"] = D.GetDecimal(dr["LT_GI"]);
                    row["WEIGHT"] = dr["WEIGHT"];
                    row["UNIT_WEIGHT"] = dr["UNIT_WEIGHT"];
                    row["FG_SERNO"] = dr["FG_SERNO"];
                    row["YN_ATP"] = dr["YN_ATP"];
                    row["CUR_ATP_DAY"] = dr["CUR_ATP_DAY"];
                    row["FG_MODEL"] = dr["FG_MODEL"];

                    row["GI_PARTNER"] = ctx납품처.CodeValue;
                    row["LN_PARTNER"] = ctx납품처.CodeName;

                    row["CD_SL"] = dr["CD_SL"];
                    row["NM_SL"] = dr["NM_SL"];

                    // 현재고 세팅(2011.09.19)
                    DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_SL"]), D.GetString(dr["CD_ITEM"]) });
                    if (rowQtInv != null) row["SL_QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);

                    CC조회(D.GetString(dr["CD_ITEMGRP"]), out cdCc, out nmCc);
                    row["CD_CC"] = cdCc;
                    row["NM_CC"] = nmCc;

                    row["TP_VAT"] = D.GetString(dr["TP_VAT"]);
                    row["RT_VAT"] = D.GetDecimal(dr["RT_VAT"]);

                    row["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR"]));
                    row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_EX"]));

                    /* 부가세포함을 사용 하는 경우 출하적용시 금액이 재계산 되어지면 안된다(출하금액 그대로 뿌려줌)
                     * 하지만 부가세포함이라 하더라도 이미 반품된 건이 있다면 재계산 해주는 수밖에 없다. 2012.02.16(PIMS:D20120214062) */
                    if (Use부가세포함 && D.GetDecimal(dr["QT_RETURN"]) == 0m)
                    {
                        row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_EX_ORIGINAL"]));
                        row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_ORIGINAL"]));
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["VAT_ORIGINAL"]));
                    }
                    else
                    {
                        row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_EX"]));
                        row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) * cur환율.DecimalValue);         //원화금액
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) * (D.GetDecimal(row["RT_VAT"]) / 100));  //부가세
                    }

                    row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]));
                    row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));

                    row["UNIT_SO_FACT"] = D.GetDecimal(dr["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(dr["UNIT_SO_FACT"]);
                    row["UNIT_GI_FACT"] = D.GetDecimal(dr["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(dr["UNIT_GI_FACT"]);

                    row["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR_IM"]));
                    row["UNIT_IM"] = dr["UNIT"];

                    if (D.GetString(row["DT_DUEDATE"]) == string.Empty && dtp납기일.Text != string.Empty)
                    {
                        row["DT_DUEDATE"] = dtp납기일.Text;
                        row["DT_REQGI"] = _CommFun.DateAdd(dtp납기일.Text, "D", D.GetInt(row["LT_GI"]) * -1);
                    }
                    row["NO_IO_MGMT"] = dr["NO_IO_MGMT"];
                    row["NO_IOLINE_MGMT"] = dr["NO_IOLINE_MGMT"];

                    row["NO_SO_ORIGINAL"] = dr["NO_SO_MGMT"];
                    row["SEQ_SO_ORIGINAL"] = dr["NO_SOLINE_MGMT"];

                    row["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                    row["CD_MNGD1"] = bp관리내역1.CodeValue;
                    row["NM_MNGD1"] = bp관리내역1.CodeName;
                    row["CD_MNGD2"] = bp관리내역2.CodeValue;
                    row["NM_MNGD2"] = bp관리내역2.CodeName;
                    row["CD_MNGD3"] = bp관리내역3.CodeValue;
                    row["NM_MNGD3"] = bp관리내역3.CodeName;
                    row["CD_MNGD4"] = txt관리내역4.Text;
                    dt.Rows.Add(row);
                }

                _flex.Binding = dt;
                _flex.IsDataChanged = true;
                ToolBarDeleteButtonEnabled = false;
                ToolBarSaveButtonEnabled = true;
                Page_DataChanged(null, null);
                버튼Enabled(false);

                if (App.SystemEnv.PROJECT사용)
                {
                    ctx프로젝트.SetCode(D.GetString(dlg.출하테이블.Rows[dlg.출하테이블.Rows.Count - 1]["NO_PROJECT"]), D.GetString(dlg.출하테이블.Rows[dlg.출하테이블.Rows.Count - 1]["NM_PROJECT"]));
                    _header.CurrentRow["NO_PROJECT"] = ctx프로젝트.CodeValue;
                    _header.CurrentRow["NM_PROJECT"] = ctx프로젝트.CodeName;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion        
         
        #region ▷ 상품적용                    
                    
        private void btn_Spitem_Help_Click(object sender, EventArgs e)
        {
            if (!FieldCheck("")) return;
            버튼Enabled(false);

            P_SA_SO_SPITEM_SUB dlg = null;

            try
            {
                DataTable dt = new DataTable();
                DataTable dt_Dlg = null;

                object[] obj = new object[8];
                obj[0] = dtp수주일자.Text;            //수주일자를 기준으로 상품의 품목들을 끌고 온다.
                obj[1] = cur환율.DecimalValue;  //환율을 가져와서 단가를 보여준다.
                obj[2] = ctx거래처.CodeValue;
                obj[3] = D.GetString(cbo단가유형.SelectedValue); //단가유형
                obj[4] = D.GetString(cbo화폐단위.SelectedValue);  //환종
                obj[5] = _단가적용형태;
                obj[6] = so_Price;
                obj[7] = cur부가세율.DecimalValue;

                dlg = new P_SA_SO_SPITEM_SUB(obj);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.ReturnDataTable == null) return;

                    dt_Dlg = dlg.ReturnDataTable.Copy();
                    dt = _flex.DataTable.Clone();

                    decimal idx = _flex.DataView.Table.Rows.Count + 1;
                    string cdCc, nmCc;

                    string multiItem = Common.MultiString(dt_Dlg, "CD_ITEM", "|");
                    DataTable dtQtInv = BASIC.GetQtInvMulti(multiItem, dtp수주일자.Text);
                    dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_SL"], dtQtInv.Columns["CD_ITEM"] };

                    if (dt_Dlg == null || dt_Dlg.Rows.Count == 0) return;
                    foreach (DataRow dr in dt_Dlg.Rows)
                    {
                        DataRow row = dt.NewRow();

                        row["SEQ_SO"] = idx;       //수주라인항번 : 원래 프로젝트 항번과 동일했으나 배송지를 분할하기 위해서 라인단위로 항번을 만들어 준다.
                        idx++;

                        row["CD_SHOP"] = D.GetString(dr["CD_SHOP"]);
                        row["CD_SPITEM"] = D.GetString(dr["CD_SPITEM"]);
                        row["CD_OPT"] = D.GetString(dr["CD_OPT"]);
                        row["NO_PROJECT"] = ctx프로젝트.CodeValue;      //프로젝트번호
                        row["SEQ_PROJECT"] = 0;                         //프로젝트라인항번 : 프로젝트를 적용받은 데이터가 아니기때문에 항번이 없다.
                        row["CD_PLANT"] = D.GetString(dr["CD_PLANT"]);
                        row["CD_ITEM"] = D.GetString(dr["CD_ITEM"]);
                        row["NM_ITEM"] = D.GetString(dr["NM_ITEM"]);
                        row["STND_ITEM"] = D.GetString(dr["STND_ITEM"]);
                        row["UNIT_SO"] = D.GetString(dr["UNIT_SO"]);
                        row["TP_ITEM"] = D.GetString(dr["TP_ITEM"]);

                        row["GI_PARTNER"] = ctx납품처.CodeValue;
                        row["LN_PARTNER"] = ctx납품처.CodeName;

                        row["CD_SL"] = D.GetString(dr["CD_SL"]);
                        row["NM_SL"] = D.GetString(dr["NM_SL"]);

                        // 현재고 세팅(2011.09.19)
                        DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_SL"]), D.GetString(dr["CD_ITEM"]) });
                        if (rowQtInv != null) row["SL_QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);

                        CC조회(D.GetString(dr["CD_ITEMGRP"]), out cdCc, out nmCc);
                        row["CD_CC"] = cdCc;
                        row["NM_CC"] = nmCc;

                        row["TP_VAT"] = D.GetString(dr["FG_VAT"]);
                        row["RT_VAT"] = D.GetDecimal(dr["RT_VAT"]);

                        row["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_SO"]));

                        row["WEIGHT"] = D.GetString(dr["WEIGHT"]);
                        row["UNIT_WEIGHT"] = D.GetString(dr["UNIT_WEIGHT"]);

                        if (서버키 != "DNC")
                        {
                            if (disCount_YN == "N")
                                row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]));
                            else if (disCount_YN == "Y")
                            {
                                row["RT_DSCNT"] = D.GetDecimal(row["RT_DSCNT"]) == 0 ? 1 : row["RT_DSCNT"];
                                row["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]));
                                row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_BASE"]) - (D.GetDecimal(row["UM_BASE"]) * D.GetDecimal(row["RT_DSCNT"])) / 100);
                            }

                            row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]));
                            row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]));
                            row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_VAT"]));
                            row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]));
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UMVAT_SO"]));
                        }
                        else  //디엔씨전용
                        {
                            DataRow dr2 = dr;
                            디엔씨상품적용계산(ref row, dr);
                        }
                        row["UNIT_SO_FACT"] = D.GetDecimal(dr["UNIT_SO_FACT"]) == 0 ? 1 : dr["UNIT_SO_FACT"];

                        row["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_IM"]) * (D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"])));
                        row["UNIT_IM"] = dr["UNIT_IM"];

                        row["DC1"] = D.GetString(dr["CD_SPITEM"]) + "/" + D.GetString(dr["NM_SPITEM"]);

                        row["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                        row["CD_MNGD1"] = bp관리내역1.CodeValue;
                        row["NM_MNGD1"] = bp관리내역1.CodeName;
                        row["CD_MNGD2"] = bp관리내역2.CodeValue;
                        row["NM_MNGD2"] = bp관리내역2.CodeName;
                        row["CD_MNGD3"] = bp관리내역3.CodeValue;
                        row["NM_MNGD3"] = bp관리내역3.CodeName;
                        row["CD_MNGD4"] = txt관리내역4.Text;
                        dt.Rows.Add(row);
                    }

                    _flex.BindingAdd(dt, "", false);

                    foreach (DataRow dx in _flex.DataTable.Rows)
                    {
                        if (dx.RowState == DataRowState.Deleted) continue;

                        if (D.GetString(dx["DT_DUEDATE"]) == string.Empty && dtp납기일.Text != string.Empty)
                        {
                            dx["DT_DUEDATE"] = dtp납기일.Text;
                            dx["DT_REQGI"] = _CommFun.DateAdd(dtp납기일.Text, "D", D.GetInt(dx["LT_GI"]) * -1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 배송지주소입력              
        private void btn_AddData_Click(object sender, EventArgs e)
        {
            P_SA_SO_DLV_SUB dlg = null;

            try
            {
                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0)
                {
                    this.ShowMessage("라인을 먼저 입력하세요!");
                    return;
                }

                object[] obj = new object[2];
                obj[0] = MA.Login.회사코드;
                obj[1] = ctx거래처.CodeValue;

                //배송정보 도움창에 헤더의 거래처에 있는 필요정보를 넘겨준다.
                DataTable dt_partinfo = _biz.GetPartnerInfoSearch(obj);

                string[] str = new string[7];
                if (dt_partinfo.Rows.Count == 1)
                {
                    str[0] = D.GetString(dt_partinfo.Rows[0]["CD_PARTNER"]);
                    str[1] = D.GetString(dt_partinfo.Rows[0]["NO_POST2"]);
                    str[2] = D.GetString(dt_partinfo.Rows[0]["DC_ADS2_H"]);
                    str[3] = D.GetString(dt_partinfo.Rows[0]["DC_ADS2_D"]);
                    str[4] = D.GetString(dt_partinfo.Rows[0]["NO_TEL2"]);
                    str[5] = D.GetString(dt_partinfo.Rows[0]["CD_EMP_PARTNER"]);
                    str[6] = D.GetString(dt_partinfo.Rows[0]["NO_HPEMP_PARTNER"]);
                }

                DataTable dt = _flex.DataTable.Clone();

                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;

                    dt.Rows.Add(dr.ItemArray);
                }

                dlg = new P_SA_SO_DLV_SUB(dt, str, "SEQ_SO");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow dx in dlg.ReturnTable.Rows)
                    {
                        for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                        {
                            if (D.GetString(dx["SEQ_SO"]) == D.GetString(_flex[i + 2, "SEQ_SO"]))
                            {
                                _flex[i + 2, "NM_CUST_DLV"] = dx["NM_CUST_DLV"];
                                _flex[i + 2, "NO_TEL_D1"] = dx["NO_TEL_D1"];
                                _flex[i + 2, "NO_TEL_D2"] = dx["NO_TEL_D2"];
                                _flex[i + 2, "CD_ZIP"] = D.GetString(dx["CD_ZIP"]).Replace("-", "");
                                _flex[i + 2, "ADDR1"] = dx["ADDR1"];
                                _flex[i + 2, "ADDR2"] = dx["ADDR2"];
                                _flex[i + 2, "TP_DLV"] = dx["TP_DLV"];
                                _flex[i + 2, "DC_REQ"] = dx["DC_REQ"];
                                _flex[i + 2, "TP_DLV_DUE"] = dx["TP_DLV_DUE"];
                            }
                            else
                                continue;
                        }
                    }

                    _flex.IsDataChanged = true;
                    Page_DataChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion    

        #region ▷ 엑셀기능                    
        private void 엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FieldCheck("")) return; //필수항목 검사                

                Duzon.Common.Util.Excel excel = null;
                string 적합품목 = string.Empty;
                string 품목코드 = string.Empty;
                string 납기요구일 = string.Empty;
                string 공장 = LoginInfo.CdPlant;
                string cdItemExcel = string.Empty;

                DataTable _dt_EXCEL = null;
                OpenFileDialog m_FileDlg = new OpenFileDialog();

                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (m_FileDlg.ShowDialog() != DialogResult.OK) return;

                Application.DoEvents();

                string FileName = m_FileDlg.FileName;
                excel = new Duzon.Common.Util.Excel();
                _dt_EXCEL = excel.StartLoadExcel(FileName);

                DataTable dtExcelGroup = _dt_EXCEL.DefaultView.ToTable(true, new string[] { "CD_ITEM" });

                string multiItem = Common.MultiString(dtExcelGroup, "CD_ITEM", "|");

                bool 검증여부 = false; bool 품목적합 = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "품목코드";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(20, '-');
                검증리스트.AppendLine(msg);

                DataTable 엑셀dt = _biz.엑셀(_dt_EXCEL.Copy());
                DataTable 공장품목dt = _biz.공장품목(multiItem, 공장);
                공장품목dt.PrimaryKey = new DataColumn[] { 공장품목dt.Columns["CD_ITEM"] };
                DataRow NewRow;
                string giPartner = string.Empty;
                string cdCc, nmCc;
                
                DataTable dtQtInv = BASIC.GetQtInvMulti(multiItem, dtp수주일자.Text);
                dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_SL"], dtQtInv.Columns["CD_ITEM"] };

                DataTable dtUmFixed = null;
                if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                {
                    dtUmFixed = _biz.SearchUmFixed(ctx거래처.CodeValue, 공장, multiItem);
                    dtUmFixed.PrimaryKey = new DataColumn[] { dtUmFixed.Columns["CD_ITEM"] };
                }

                // 장은경 2010.07.21 추가
                DataTable dt할인율 = null;
                if (!ctx거래처.IsEmpty() && disCount_YN == "Y" && _biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
                {
                    dt할인율 = _biz.할인율(공장, ctx거래처.CodeValue, 엑셀dt.Select());
                }

                _flex.Redraw = false;

                foreach (DataRow row in 엑셀dt.Rows)
                {
                    cdItemExcel = D.GetString(row["CD_ITEM"]);
                    if (cdItemExcel == string.Empty) { continue; }

                    DataRow rowFindItem = 공장품목dt.Rows.Find(cdItemExcel);

                    if (rowFindItem == null) 품목적합 = false;
                    else 품목적합 = true;
                    
                    if (품목적합)
                    {
                        NewRow = _flex.DataTable.NewRow();
                        NewRow["CD_ITEM"] = cdItemExcel;
                        NewRow["NM_ITEM"] = rowFindItem["NM_ITEM"];
                        NewRow["STND_ITEM"] = rowFindItem["STND_ITEM"];
                        NewRow["UNIT_SO"] = rowFindItem["UNIT_SO"];
                        NewRow["UNIT_IM"] = rowFindItem["UNIT_IM"];
                        NewRow["TP_ITEM"] = rowFindItem["TP_ITEM"];
                        NewRow["EN_ITEM"] = rowFindItem["EN_ITEM"];
                        NewRow["STND_DETAIL_ITEM"] = rowFindItem["STND_DETAIL_ITEM"];
                        NewRow["GRP_MFG"] = rowFindItem["GRP_MFG"];
                        NewRow["WEIGHT"] = rowFindItem["WEIGHT"];
                        NewRow["UNIT_WEIGHT"] = rowFindItem["UNIT_WEIGHT"];
                        NewRow["YN_ATP"] = rowFindItem["YN_ATP"];
                        NewRow["CUR_ATP_DAY"] = rowFindItem["CUR_ATP_DAY"];
                        NewRow["FG_MODEL"] = rowFindItem["FG_MODEL"];

                        if (D.GetString(row["CD_SL"]) == string.Empty)
                        {
                            NewRow["CD_SL"] = rowFindItem["CD_SL"];
                            NewRow["NM_SL"] = rowFindItem["NM_SL"];
                        }
                        else
                        {
                            NewRow["CD_SL"] = row["CD_SL"];
                            DataRow rowSl = BASIC.GetSL(LoginInfo.CdPlant, D.GetString(row["CD_SL"]));
                            if (rowSl == null)
                            {
                                ShowMessage(공통메세지._이가존재하지않습니다, "[" + D.GetString(row["CD_SL"]) + "] " + DD("창고"));
                                return;
                            }
                            NewRow["NM_SL"] = rowSl["NM_SL"];
                        }

                        NewRow["DT_DUEDATE"] = row["DT_DUEDATE"];
                        NewRow["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]));
                        NewRow["LT_GI"] = D.GetDecimal(rowFindItem["LT_GI"]);
                        NewRow["FG_SERNO"] = rowFindItem["FG_SERNO"];

                        if (_dt_EXCEL.Columns.Contains("GI_PARTNER"))
                        {
                            giPartner = D.GetString(row["GI_PARTNER"]);
                            NewRow["GI_PARTNER"] = giPartner;

                            if (giPartner != string.Empty)
                                NewRow["LN_PARTNER"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER, new object[] { MA.Login.회사코드, giPartner })["LN_PARTNER"];
                        }
                        else
                        {
                            NewRow["GI_PARTNER"] = ctx납품처.CodeValue;
                            NewRow["LN_PARTNER"] = ctx납품처.CodeName;
                        }

                        NewRow["UNIT_SO_FACT"] = D.GetDecimal(rowFindItem["UNIT_SO_FACT"]) == 0 ? 1 : rowFindItem["UNIT_SO_FACT"];
                        NewRow["UNIT_GI_FACT"] = D.GetDecimal(rowFindItem["UNIT_GI_FACT"]) == 0 ? 1 : rowFindItem["UNIT_GI_FACT"];

                        if (_biz.Get업체별프로세스 == "003")   //아카데미과학(PIMS:D20120716070)
                        {
                            NewRow["PITEM_NUM_USERDEF3"] = rowFindItem["NUM_USERDEF3"];
                            NewRow["PITEM_NUM_USERDEF4"] = rowFindItem["NUM_USERDEF4"];
                            NewRow["PITEM_NUM_USERDEF5"] = rowFindItem["NUM_USERDEF5"];
                            NewRow["PITEM_NUM_USERDEF6"] = rowFindItem["NUM_USERDEF6"];
                            NewRow["PITEM_NUM_USERDEF7"] = rowFindItem["NUM_USERDEF7"];
                            NewRow["AM_PACKING"] = D.GetDecimal(NewRow["QT_SO"]) * D.GetDecimal(rowFindItem["NUM_USERDEF3"]);
                            NewRow["QT_PACKING"] = D.GetDecimal(NewRow["QT_SO"]) / D.GetDecimal(NewRow["UNIT_GI_FACT"]);
                        }
                        else if (_biz.Get업체별프로세스 == "008")   //아이코닉스
                        {
                            NewRow["PITEM_NUM_USERDEF1"] = rowFindItem["NUM_USERDEF1"];
                            NewRow["PITEM_NUM_USERDEF2"] = rowFindItem["NUM_USERDEF2"];
                        }

                        NewRow["SEQ_SO"] = 최대차수 + 1;
                        NewRow["CD_PLANT"] = LoginInfo.CdPlant;

                        납기요구일 = D.GetString(row["DT_DUEDATE"]);

                        if (납기요구일 != string.Empty)
                        {
                            NewRow["DT_DUEDATE"] = row["DT_DUEDATE"];
                            NewRow["DT_REQGI"] = _CommFun.DateAdd(D.GetString(row["DT_DUEDATE"]), "D", D.GetInt(NewRow["LT_GI"]) * -1);
                        }

                        // 현재고 세팅(2011.09.19)
                        DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { LoginInfo.CdPlant, D.GetString(NewRow["CD_SL"]), cdItemExcel });
                        if (rowQtInv != null) NewRow["SL_QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);

                        // 가용재고 세팅(2011.10.17)
                        if (Use가용재고)
                        {
                            한국화장품 hkcos = new 한국화장품();
                            NewRow["QT_USEINV"] = hkcos.Search가용재고(LoginInfo.CdPlant, D.GetString(NewRow["CD_SL"]), cdItemExcel + "|");
                        }

                        CC조회(D.GetString(rowFindItem["CD_ITEMGRP"]), out cdCc, out nmCc);
                        NewRow["CD_CC"] = cdCc;
                        NewRow["NM_CC"] = nmCc;

                        string 품목과세구분 = D.GetString(rowFindItem["FG_TAX_SA"]).Trim();

                        if (_biz.Get과세변경유무 == "N" || 품목과세구분 == string.Empty)
                        {
                            NewRow["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                            NewRow["RT_VAT"] = cur부가세율.DecimalValue;
                        }
                        else
                        {
                            NewRow["TP_VAT"] = 품목과세구분;
                            NewRow["RT_VAT"] = D.GetDecimal(rowFindItem["RT_TAX_SA"]);
                        }

                        NewRow["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) * D.GetDecimal(NewRow["UNIT_SO_FACT"]));

                        if (_구분 == "복사")
                            NewRow["STA_SO1"] = _수주상태;

                        if (dt할인율 != null)
                        {
                            DataRow row할인율 = dt할인율.Rows.Find(NewRow["CD_ITEM"]);
                            NewRow["RT_DSCNT"] = row할인율 == null ? 0M : row할인율["DC_RATE"];
                        }

                        if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                        {
                            if (disCount_YN == "Y")
                            {
                                //(유형별 OR 거래처별)단가관리를 사용하고 판매단가통제를 하는 경우
                                if ((_단가적용형태 == "002" || _단가적용형태 == "003") && so_Price == "Y")
                                {
                                    NewRow["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(NewRow["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));

                                    if (D.GetDecimal(NewRow["RT_DSCNT"]) != 0)
                                        NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["UM_BASE"]) - (D.GetDecimal(NewRow["UM_BASE"]) * D.GetDecimal(NewRow["RT_DSCNT"])) / 100);
                                    else
                                        NewRow["UM_SO"] = 0m;
                                }
                                else if ((_단가적용형태 != "002" && _단가적용형태 != "003") && so_Price == "Y")
                                {
                                    NewRow["UM_BASE"] = 0m;
                                    NewRow["UM_SO"] = 0m;
                                }
                                else //엑셀 업로드시 엑셀 업로드 양식이 1.단가미포함 2.단가포함 이 있는데 단가가 있고 단가적용이 사용안하는 품목의 단가를 셋팅한다.
                                {
                                    //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
                                    //할인율적용 여부가 "Y" 일 경우에는 
                                    //기준단가에 단가를 셋팅하고 기준단가-(기준단가*할인율)/100 으로 단가를 구해서 단가에 넣는다.
                                    NewRow["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"]));
                                    NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["UM_BASE"]) - (D.GetDecimal(NewRow["UM_BASE"]) * D.GetDecimal(NewRow["RT_DSCNT"])) / 100);
                                }
                            }
                            else if (disCount_YN == "N")
                            {
                                if ((_단가적용형태 == "002" || _단가적용형태 == "003") && so_Price == "Y")
                                    NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(NewRow["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));
                                else //엑셀 업로드시 엑셀 업로드 양식이 1.단가미포함 2.단가포함 이 있는데 단가가 있고 단가적용이 사용안하는 품목의 단가를 셋팅한다.
                                    NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"]));
                            }
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                        {
                            if ((_단가적용형태 == "002" || _단가적용형태 == "003"))
                            {
                                if (NewRow.Table.Columns.Contains("UM_OPT"))
                                {
                                    NewRow["UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(NewRow["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));
                                }
                            }

                            decimal 중량 = 0;
                            decimal 중량단가 = 0;

                            if (NewRow.Table.Columns.Contains("WEIGHT"))
                            {
                                중량 = D.GetDecimal(NewRow["WEIGHT"]);
                            }
                            if (NewRow.Table.Columns.Contains("UM_OPT"))
                            {
                                중량단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["UM_OPT"]));
                            }
                            decimal 단가 = 중량 * 중량단가;

                            if (disCount_YN == "Y")
                            {
                                NewRow["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, 단가);
                                NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["UM_BASE"]) - (D.GetDecimal(NewRow["UM_BASE"]) * D.GetDecimal(NewRow["RT_DSCNT"])) / 100);
                            }
                            else
                                NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, 단가);
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.조선호텔베이커리단가) //2011.12.21 SJH(PIMS:D20111205101) : 조선호텔 베이커리는 할인율을 안쓰는걸로 가정한다.
                        {
                            NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.조선호텔베이커리단가(D.GetString(NewRow["CD_PLANT"]), D.GetString(NewRow["CD_ITEM"]), ctx거래처.CodeValue, dtp수주일자.Text, D.GetString(cbo화폐단위.SelectedValue))));
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)   //2011.12.28 SJH(PIMS:D20111226030) : 제원요청(할인율, 단가통제 사용안함)
                        {
                            DataRow rowUmFixed = dtUmFixed.Rows.Find(D.GetString(NewRow["CD_ITEM"]));

                            if (rowUmFixed == null)
                                NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(NewRow["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp수주일자.Text)));
                            else
                                NewRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(rowUmFixed["UM_FIXED"]));
                        }

                        NewRow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(NewRow["QT_SO"]) * D.GetDecimal(NewRow["UM_SO"]));
                        NewRow["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(NewRow["AM_SO"]) * cur환율.DecimalValue);
                        NewRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(NewRow["AM_WONAMT"]) * (D.GetDecimal(NewRow["RT_VAT"]) / 100));
                        NewRow["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(NewRow["AM_WONAMT"]) + D.GetDecimal(NewRow["AM_VAT"]));

                        if (D.GetDecimal(NewRow["QT_SO"]) != 0)
                            NewRow["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["AMVAT_SO"]) / D.GetDecimal(NewRow["QT_SO"]));
                        else
                            NewRow["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(NewRow["AMVAT_SO"]));

                        NewRow["DC1"] = row["DC1"];
                        NewRow["DC2"] = row["DC2"];
                        NewRow["FG_USE"] = row["FG_USE"];

                        NewRow["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                        NewRow["CD_MNGD1"] = bp관리내역1.CodeValue;
                        NewRow["NM_MNGD1"] = bp관리내역1.CodeName;
                        NewRow["CD_MNGD2"] = bp관리내역2.CodeValue;
                        NewRow["NM_MNGD2"] = bp관리내역2.CodeName;
                        NewRow["CD_MNGD3"] = bp관리내역3.CodeValue;
                        NewRow["NM_MNGD3"] = bp관리내역3.CodeName;
                        NewRow["CD_MNGD4"] = txt관리내역4.Text;
                        _flex.DataTable.Rows.Add(NewRow);
                        품목적합 = false;
                    }
                    else
                    {
                        검증리스트.AppendLine(cdItemExcel);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치하는 항목들이 있습니다. \n " +
                    " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                _flex.Redraw = true;

                _flex.Row = _flex.Rows.Count - 1;
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();

                _flex.SumRefresh();

                _flex.IsDataChanged = true;
                Page_DataChanged(null, null);

                버튼Enabled(false);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }
        #endregion

        #region ▷ 라인추가                    

        private void m_btnAppend_Click(object sender, EventArgs e)
        {
            try
            {
                if (서버키 != "HKCOS" && 서버키 != "TOKIMEC")
                {
                    if (!Chk확정여부()) return;
                }

                if (견적적용건())
                {
                    ShowMessage("견적적용건이 존재합니다.");
                    return;
                }
                if (출하적용건())
                {
                    ShowMessage("출하적용건이 존재합니다.");
                    return;
                }

                if (!FieldCheck("")) return;

                버튼Enabled(false);

                _flex.Rows.Add();

                _flex.Row = _flex.Rows.Count - 1;
                _flex["SEQ_SO"] = 최대차수 + 1;
                _flex["CD_PLANT"] = LoginInfo.CdPlant;
                _flex["GI_PARTNER"] = ctx납품처.CodeValue;
                _flex["LN_PARTNER"] = ctx납품처.CodeName;
                _flex["QT_SO"] = 0m;
                _flex["UM_SO"] = 0m;
                _flex["AM_SO"] = 0m;
                _flex["AM_VAT"] = 0m;
                _flex["QT_IM"] = 0m;
                _flex["NO_PO_PARTNER"] = txt거래처PO.Text;

                if (!ctx프로젝트.IsEmpty())
                {
                    _flex["NO_PROJECT"] = ctx프로젝트.CodeValue;
                    _flex["NM_PROJECT"] = ctx프로젝트.CodeName;
                }

                if (disCount_YN == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
                {
                    _flex["RT_DSCNT"] = 0m;  //할인율
                    _flex["UM_BASE"] = 0m;   //기준단가
                }

                _flex["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                _flex["RT_VAT"] = cur부가세율.DecimalValue;

                if (_flex.Rows.Count == 3)
                {
                    _flex["DT_DUEDATE"] = dtp납기일.Text;
                    _flex["DT_REQGI"] = dtp납기일.Text;
                    _flex["CD_MNGD1"] = bp관리내역1.CodeValue; _flex["NM_MNGD1"] = bp관리내역1.CodeName;
                    _flex["CD_MNGD2"] = bp관리내역2.CodeValue; _flex["NM_MNGD2"] = bp관리내역2.CodeName;
                    _flex["CD_MNGD3"] = bp관리내역3.CodeValue; _flex["NM_MNGD3"] = bp관리내역3.CodeName;
                    _flex["CD_MNGD4"] = txt관리내역4.Text;
                }
                else if (_flex.Rows.Count > 3)
                {
                    _flex["DT_DUEDATE"] = _flex[_flex.Row - 1, "DT_DUEDATE"];
                    _flex["DT_REQGI"] = _flex[_flex.Row - 1, "DT_REQGI"];
                    _flex["FG_USE"] = _flex[_flex.Row - 1, "FG_USE"];
                    _flex["CD_MNGD1"] = _flex[_flex.Row - 1, "CD_MNGD1"]; _flex["NM_MNGD1"] = _flex[_flex.Row - 1, "NM_MNGD1"];
                    _flex["CD_MNGD2"] = _flex[_flex.Row - 1, "CD_MNGD2"]; _flex["NM_MNGD2"] = _flex[_flex.Row - 1, "NM_MNGD2"];
                    _flex["CD_MNGD3"] = _flex[_flex.Row - 1, "CD_MNGD3"]; _flex["NM_MNGD3"] = _flex[_flex.Row - 1, "NM_MNGD3"];
                    _flex["CD_MNGD4"] = _flex[_flex.Row - 1, "CD_MNGD4"];
                }

                if (_구분 != "복사")
                    _flex["STA_SO1"] = _수주상태;

                _flex["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);

                _flex.Col = _flex.Cols.Fixed;
                _flex.AddFinished();
                _flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 라인삭제                    

        private void m_btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk확정여부()) return;

                _flexUser.RemoveViewAll();
                _flex.RemoveItem(_flex.Row);

                if (!_flex.HasNormalRow)
                {
                    if (추가모드여부)
                    {
                        _flex.AcceptChanges();
                        ctx수주형태.Enabled = true;
                        cbo화폐단위.Enabled = true;
                        if (D.GetString(cbo화폐단위.SelectedValue) != "000")
                            cur환율.Enabled = true;
                        cbo부가세구분.Enabled = true;
                        ctx프로젝트.Enabled = true;
                        ctx영업그룹.Enabled = true;
                        ctx거래처.Enabled = true;
                        cbo부가세포함.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 창고 일괄 적용              
        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                string cdPlant = D.GetString(cbo공장.SelectedValue);
                string cdSl = ctx창고.CodeValue;

                DataRow[] ldrchk = null;

                if (추가모드여부)
                    ldrchk = _flex.DataTable.Select("CD_PLANT = '" + cdPlant + "'", "", DataViewRowState.CurrentRows);
                else
                    ldrchk = _flex.DataTable.Select("(STA_SO1 = '' OR STA_SO1 = 'O' OR STA_SO1 IS NULL) AND CD_PLANT = '" + cdPlant + "'", "", DataViewRowState.CurrentRows);

                if (ldrchk == null || ldrchk.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("적용대상"));
                    return;
                }

                string multiItem = Common.MultiString(ldrchk, "CD_ITEM", "|");
                DataTable dt = BASIC.GetQtInvMulti(cdPlant, cdSl, multiItem, dtp수주일자.Text);
                dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_PLANT"], dt.Columns["CD_SL"], dt.Columns["CD_ITEM"] };

                DataTable dtQtMiGi = null;
                if (서버키 == "KOREAF" || 서버키 == "DZSQL")    //가용재고(현재고 - 미출하수량) 2012.02.24(PIMS:D20120220022)
                {
                    dtQtMiGi = _biz.SearchMiGi(cdPlant, cdSl, multiItem);
                    dtQtMiGi.PrimaryKey = new DataColumn[] { dtQtMiGi.Columns["CD_ITEM"] };
                }

                _flex.Redraw = false;
                foreach (DataRow row in ldrchk)
                {
                    row["CD_SL"] = ctx창고.CodeValue;
                    row["NM_SL"] = ctx창고.CodeName;
                    row["CD_WH"] = bp_WH.CodeValue;
                    row["NM_WH"] = bp_WH.CodeName;

                    DataRow dr = dt.Rows.Find(new object[] { cdPlant, cdSl, D.GetString(row["CD_ITEM"]) });

                    if (dr == null)
                        row["SL_QT_INV"] = 0m;
                    else
                        row["SL_QT_INV"] = D.GetDecimal(dr["QT_INV"]);

                    if (Use가용재고)
                    {
                        한국화장품 hkcos = new 한국화장품();
                        row["QT_USEINV"] = hkcos.Search가용재고(D.GetString(row["CD_PLANT"]), cdSl, D.GetString(row["CD_ITEM"]) + "|");
                    }
                    if (서버키 == "KOREAF" || 서버키 == "DZSQL")    //가용재고(현재고 - 미출하수량) 2012.02.24(PIMS:D20120220022)
                    {
                        DataRow rowQtMiGi = dtQtMiGi.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]) });

                        if (rowQtMiGi == null)
                            row["QT_USEINV"] = D.GetDecimal(row["SL_QT_INV"]);
                        else
                            row["QT_USEINV"] = D.GetDecimal(row["SL_QT_INV"]) - D.GetDecimal(rowQtMiGi["QT_MI_GI"]);
                    }
                }
                _flex.Redraw = true;

                _flex.SumRefresh();

                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ▷ 납품처 적용                 

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (ctx납품처.CodeValue == string.Empty) return;

                DataRow[] drs = null;

                if (추가모드여부)
                    drs = _flex.DataTable.Select();
                else
                    drs = _flex.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("적용대상"));
                    return;
                }

                _flex.Redraw = false;
                foreach (DataRow row in drs)
                {
                    row["GI_PARTNER"] = ctx납품처.CodeValue;
                    row["LN_PARTNER"] = ctx납품처.CodeName;
                }
                _flex.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, DD("납품처적용"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region ▷ btnB가단가적용(한국화장품)  

        void btnB가단가적용_HKCOS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (!Chk적용건(true))
                {
                    return;
                }
                                
                string 품목 = string.Empty;

                for(int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if(!추가모드여부)
                    {
                        if(D.GetString(_flex[i, "STA_SO1"]) != string.Empty && D.GetString(_flex[i, "STA_SO1"]) != "O")
                        {
                            continue;
                        }
                    }
                    
                    품목 += D.GetString(_flex[i, "CD_ITEM"]) + "|";
                }

                Duzon.ERPU.SA.Custmize.한국화장품 biz = new Duzon.ERPU.SA.Custmize.한국화장품();

                string 공장 = D.GetString(_flex["CD_PLANT"]);
                string 거래처 = ctx거래처.CodeValue;
                string 그룹코드 = ctx영업그룹.CodeValue;
                string 기준일자 = dtp수주일자.Text;
                string 단가유형 = D.GetString(cbo단가유형.SelectedValue);
                string 환종 = D.GetString(cbo화폐단위.SelectedValue);
                string 모듈구분 = "002";
                string 수주형태 = ctx수주형태.CodeValue;
                DataTable dt단가 = biz.GetUM(공장, 품목, 거래처, 그룹코드, 기준일자, 단가유형, 환종, 모듈구분, 수주형태);
                dt단가.PrimaryKey = new DataColumn[] { dt단가.Columns["CD_ITEM"] };

                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if (!추가모드여부)
                    {
                        if (D.GetString(_flex[i, "STA_SO1"]) != string.Empty && D.GetString(_flex[i, "STA_SO1"]) != "O")
                        {
                            continue;
                        }
                    }
                    
                    DataRow row단가 = dt단가.Rows.Find(_flex[i, "CD_ITEM"]);
                    _flex[i, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row단가 == null ? 0 : row단가["UM_SO"]));

                    _flex[i, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "QT_SO"]) * D.GetDecimal(_flex[i, "UM_SO"]));
                    _flex[i, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_SO"]) * cur환율.DecimalValue);
                    _flex[i, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_WONAMT"]) * (D.GetDecimal(_flex[i, "RT_VAT"]) / 100));
                    _flex[i, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_WONAMT"]) + D.GetDecimal(_flex[i, "AM_VAT"]));

                    if (D.GetDecimal(_flex[i, "QT_SO"]) != 0)
                        _flex[i, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AMVAT_SO"]) / D.GetDecimal(_flex[i, "QT_SO"]));
                    else
                        _flex[i, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AMVAT_SO"]));

                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                    {
                        _biz.예상이익(_flex, i);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ BOM적용                     

        void btnBOM적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk확정여부()) return;
                BOM적용();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void BOM적용()
        {
            List<string> List = new List<string>();
            List.Add(MA.Login.회사코드);
            List.Add(D.GetString(_flex["CD_PLANT"]));
            List.Add(D.GetString(_flex["CD_ITEM"]));

            DataTable dt = _biz.BOM적용(List.ToArray());

            if (dt == null || dt.Rows.Count == 0)
            {
                //ShowMessage(공통메세지._이가존재하지않습니다, "적용할 BOM");
                ShowMessage(공통메세지._이가존재하지않습니다, DD("적용할 BOM"));
                return;
            }

            _flexUser.Redraw = false;

            try
            {
                소요자재그리드CLEAR();  //기존에 소요자재가 존재하면 지워준다.

                foreach (DataRow dr in dt.Rows)
                {
                    _flexUser.Rows.Add();
                    _flexUser.Row = _flexUser.Rows.Count - 1;
                    _flexUser["NO_SO"] = txt수주번호.Text;
                    _flexUser["SEQ_SO"] = _flex["SEQ_SO"];
                    _flexUser["SEQ_SO_LINE"] = D.GetString(dr["SEQ_SO_LINE"]);
                    _flexUser["CD_MATL"] = D.GetString(dr["CD_MATL"]);
                    _flexUser["NM_ITEM"] = D.GetString(dr["NM_ITEM"]);
                    _flexUser["UNIT_IM"] = D.GetString(dr["UNIT_IM"]);
                    _flexUser["STND_ITEM"] = D.GetString(dr["STND_ITEM"]);
                    _flexUser["STND_DETAIL_ITEM"] = D.GetString(dr["STND_DETAIL_ITEM"]);
                    _flexUser["QT_NEED"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_NEED"]) * D.GetDecimal(_flex["QT_SO"]));
                    _flexUser["QT_NEED_UNIT"] = D.GetString(dr["QT_NEED_UNIT"]);
                    _flexUser.AddFinished();
                }

                _flexUser.Col = _flexUser.Cols.Fixed;
                _flexUser.Focus();
            }
            finally
            {
                _flexUser.Redraw = true;
            }

            ShowMessage(공통메세지._작업을완료하였습니다, btnBOM적용.Text);
        }

        #endregion

        #region ▷ 소요자재추가                

        void btn소요자재추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk확정여부()) return;
                if (!_flex.HasNormalRow) return;

                if (D.GetString(_flex["CD_ITEM"]) == string.Empty || D.GetDecimal(_flex["QT_SO"]) == 0m)
                {
                    //ShowMessage(공통메세지._은는필수입력항목입니다, "품목과 수량");
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("품목과 수량"));
                    return;
                }

                decimal MaxValue = _flexUser.GetMaxValue("SEQ_SO_LINE");

                _flexUser.Rows.Add();
                _flexUser.Row = _flexUser.Rows.Count - 1;
                _flexUser["NO_SO"] = txt수주번호.Text;
                _flexUser["SEQ_SO"] = _flex["SEQ_SO"];
                _flexUser["SEQ_SO_LINE"] = ++MaxValue;
                _flexUser["QT_NEED"] = 0m;
                _flexUser["QT_NEED_UNIT"] = 0m;
                _flexUser.AddFinished();
                _flexUser.Col = _flexUser.Cols.Fixed;
                _flexUser.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 소요자재삭제                

        void btn소요자재삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk확정여부()) return;
                if (!_flexUser.HasNormalRow) return;

                _flexUser.RemoveItem(_flexUser.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ btn업체전용1                
        /// <summary>
        /// 한국화스너
        /// </summary>
        void btnSET전개_KFL_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (!Chk적용건(true))
                {
                    return;
                }
                if (!Chk확정여부()) return;

                H_SA_SET_OPEN_SUB dlg = new H_SA_SET_OPEN_SUB(D.GetString(cbo공장.SelectedValue));

                if (dlg.ShowDialog() != DialogResult.OK) return;

                m_btnAppend_Click(null, null);
                품목추가(_flex.Rows.Count - 1, dlg.GetData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 화우테크놀러지
        /// </summary>
        void btn신규품목생성_FAWOO_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (!Chk적용건(true))
                {
                    return;
                }

                if (!Chk확정여부()) return;

                H_SA_Z_FAWOO_SO_ITEM_SUB dlg = new H_SA_Z_FAWOO_SO_ITEM_SUB();

                if (dlg.ShowDialog() != DialogResult.OK) return;

                m_btnAppend_Click(null, null);
                품목추가(_flex.Rows.Count - 1, dlg.GetData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 엘케이
        /// </summary>
        void btn사양등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (!Chk확정여부()) return;
                if (D.GetString(_flex["CD_ITEM"]) == string.Empty || D.GetDecimal(_flex["QT_SO"]) == 0) return;

                List<object> list = new List<object>();
                list.Add(D.GetDecimal(_flex["SEQ_SO"]));
                list.Add(D.GetString(cbo공장.SelectedValue));
                list.Add(D.GetString(_flex["CD_ITEM"]));
                list.Add(D.GetString(_flex["NM_ITEM"]));
                list.Add(D.GetDecimal(_flex["QT_SO"]));

                DataTable dt = new DataView(_flexUser.DataTable, "SEQ_SO = " + D.GetDecimal(_flex["SEQ_SO"]) + "", "", DataViewRowState.CurrentRows).ToTable();

                H_SA_Z_LK_SO_SPEC_SUB dlg = new H_SA_Z_LK_SO_SPEC_SUB(list.ToArray(), dt);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                _flex["YN_OPTION"] = "Y";
                _flexUser.RemoveViewAll();
                _flexUser.BindingAdd(dlg.GetDataTable, "", false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ btn업체전용2                

        void btn업체전용2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk수주일자) return;

                _biz.WEBORDER(dtp수주일자.Text);

                ShowMessage(공통메세지._작업을완료하였습니다, btn업체전용2.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ btn첨부파일                 

        void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                if (_header.CurrentRow.RowState == DataRowState.Added)
                {
                    ShowMessage("저장 후 파일첨부하여 주십시오");
                    return;
                }

                string cd_file_code = D.GetString(_header.CurrentRow["NO_SO"]) + "_" + D.GetString(_header.CurrentRow["NO_HST"]); //파일 PK설정   공장코드_검사성적서번호

                DataTable dt = _biz.IsFileHelpCheck();
                if (D.GetString(dt.Rows[0]["TP_FILESERVER"]) == "0")  // "0" 기존 도움창, "1" 대용량파일서버 도움창
                {
                    master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, cd_file_code);
                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                        return;
                }
                else
                {
                    if (서버키 == "CIS" || 서버키 == "DZSQL" ) // 씨아이에스 영업폴더 밑에 첨부파일 저장 
                    {
                        AttachmentManager file = new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, "수주등록\\"+ cd_file_code);
                        if (file.ShowDialog(this) == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        AttachmentManager file = new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, cd_file_code);
                        if (file.ShowDialog(this) == DialogResult.Cancel)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 납기일 일괄 적용            

        void btn_Due_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (dtp납기일.Text == string.Empty) return;

                DataRow[] drs = null;

                if (추가모드여부)
                    drs = _flex.DataTable.Select();
                else
                    drs = _flex.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("적용대상"));
                    return;
                }

                _flex.Redraw = false;
                foreach (DataRow row in drs)
                {
                    row["DT_DUEDATE"] = dtp납기일.Text;
                    row["DT_REQGI"] = _CommFun.DateAdd(dtp납기일.Text, "D", D.GetInt(row["LT_GI"]) * -1);

                    if (_biz.Get업체별프로세스 == "001")    //아세히카세이 전용건 : 단가를 가지고 오는 기준이 수주일자가 아니고 납기일이기 때문에 아래 로직 추가
                    {
                        row["UM_SO"] = BASIC.GetUM(D.GetString(row["CD_PLANT"]), D.GetString(row["CD_ITEM"]), ctx거래처.CodeValue, ctx영업그룹.CodeValue, dtp납기일.Text, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), "SA");
                        row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"]) * D.GetDecimal(row["QT_SO"]));
                        row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) * cur환율.DecimalValue);
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                        row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]));

                        if (D.GetDecimal(row["QT_SO"]) != 0)
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));
                        else
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]));

                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        {
                            수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                            row["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(row["QT_SO"]), D.GetDecimal(row["UM_INV"]), D.GetDecimal(row["AM_WONAMT"]));
                        }
                        row["NUM_USERDEF1"] = D.GetDecimal(row["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"]) / D.GetDecimal(row["AREA"]));
                    }
                }
                _flex.Redraw = true;
                _flex.SumRefresh();

                ShowMessage(공통메세지._작업을완료하였습니다, DD("납기일적용"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region ▷ 품목전개                    

        void btn품목전개_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk거래처 || !Chk영업그룹 || !Chk수주유형 || !Chk화폐단위 || !Chk과세구분) return;
                if (!Chk적용건(true))
                {
                    return;
                }
                if (!Chk확정여부()) return;

                pur.P_PU_PO_ITEMEXPSUB m_dlg = new pur.P_PU_PO_ITEMEXPSUB(D.GetString(cbo공장.SelectedValue), ctx거래처.CodeValue, ctx거래처.CodeName,
                    D.GetString(cbo단가유형.SelectedValue), dtp수주일자.Text, D.GetString(cbo화폐단위.SelectedValue));

                m_dlg.SetModule = "002";    //001:구매모듈, 002:영업모듈

                if (m_dlg.ShowDialog(this) != DialogResult.OK) return;

                m_btnAppend_Click(null, null);
                품목추가(_flex.Rows.Count - 1, m_dlg.GetData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 전자결재                    

        void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                {
                    ShowMessage("저장후 결재버튼을 클릭하세요.");
                    return;
                }

                // LED라이텍은 확정인 건도 전자결재를 태울 수 있음
                // 단, 이후 프로세스에는 영향을 미치지 않는다.
                if (_수주상태 != "O" && 서버키 != "LEDLITEK")
                {
                    ShowMessage("미확정건에 대해서만 전자결재를 할 수 있습니다.");
                    return;
                }

                SoInterWork siw = new SoInterWork();

                if (siw.전자결재(_header.CurrentRow, _flex.DataTable))
                    ShowMessage("전자결재가 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 결제조건                    

        void btn결제조건_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                if (IsChanged())
                {
                    ShowMessage("저장 후에 결제조건을 등록 할 수 있습니다.");
                    return;
                }

                H_SA_SO_PAYCOND_SUB dlg = new H_SA_SO_PAYCOND_SUB();
                dlg.SetNoSo = txt수주번호.Text;
                dlg.SetisConfirm = _biz.IsConfirm(txt수주번호.Text);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 프로젝트 일괄적용           

        void btn프로젝트적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (ctx프로젝트.IsEmpty()) return;

                DataRow[] drs = null;

                if (추가모드여부)
                    drs = _flex.DataTable.Select();
                else
                    drs = _flex.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("적용대상"));
                    return;
                }

                _flex.Redraw = false;
                foreach (DataRow row in drs)
                {
                    row["NO_PROJECT"] = ctx프로젝트.CodeValue;
                    row["NM_PROJECT"] = ctx프로젝트.CodeName;
                }
                _flex.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, DD("프로젝트적용"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region ▷ 할인율적용(아이코닉스)      

        void btn할인율적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (cur할인율.DecimalValue == decimal.Zero) return;

                DataRow[] drs = null;

                if (추가모드여부)
                    drs = _flex.DataTable.Select();
                else
                    drs = _flex.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("적용대상"));
                    return;
                }

                _flex.Redraw = false;
                foreach (DataRow row in drs)
                {
                    row["NUM_USERDEF1"] = cur할인율.DecimalValue;

                    if (Use부가세포함)
                    {
                        row["UMVAT_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["NUM_USERDEF1"]) / 100m);
                        row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) * D.GetDecimal(row["UMVAT_SO"]));
                        row["AM_WONAMT"] = Decimal.Round(D.GetDecimal(row["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(row["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        row["AM_VAT"] = D.GetDecimal(row["AMVAT_SO"]) - D.GetDecimal(row["AM_WONAMT"]);
                        row["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) / cur환율.DecimalValue);
                        row["UM_SO"] = D.GetDecimal(row["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) / D.GetDecimal(row["QT_SO"]));
                    }
                    else
                    {
                        row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["NUM_USERDEF1"]) / 100m);
                        row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"]) * D.GetDecimal(row["QT_SO"]));
                        row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_SO"]) * cur환율.DecimalValue);
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) * (D.GetDecimal(row["RT_VAT"]) / 100m));
                        row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]));

                        if (D.GetDecimal(row["QT_SO"]) != 0)
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));
                        else
                            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]));
                    }

                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                    {
                        수주관리.Calc 수주관리계산 = new 수주관리.Calc();
                        row["AM_PROFIT"] = 수주관리계산.예상이익계산(D.GetDecimal(row["QT_SO"]), D.GetDecimal(row["UM_INV"]), D.GetDecimal(row["AM_WONAMT"]));
                    }
                }
                _flex.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, DD("할인율 적용"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ▶ 그리드 이벤트   

        #region ▷ _flex_BeforeCodeHelp        

        void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!추가모드여부 && _flex.RowState(e.Row) != DataRowState.Added)
                {
                    if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (!Chk공장)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (D.GetDecimal(_flex["SEQ_PROJECT"]) != 0)
                        {
                            ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        if (disCount_YN == "Y" && _biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
                        {
                            if (!Chk거래처)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        
                        e.Parameter.P09_CD_PLANT = D.GetString(_flex[e.Row, "CD_PLANT"]);
                        break;
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(_flex[e.Row, "CD_PLANT"]);
                        break;
                    case "GI_PARTNER":
                        e.Parameter.P14_CD_PARTNER = ctx거래처.CodeValue;
                        break;
                    case "NO_PROJECT":
                        if(D.GetDecimal(_flex["SEQ_PROJECT"]) != 0 )
                        {
                            ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        if (!Chk거래처 || !Chk영업그룹)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                        e.Parameter.P14_CD_PARTNER = ctx거래처.CodeValue; //거래처코드
                        e.Parameter.P63_CODE3 = ctx거래처.CodeName;       //거래처명
                        e.Parameter.P17_CD_SALEGRP = ctx영업그룹.CodeValue;   //영업그룹코드
                        e.Parameter.P62_CODE2 = ctx영업그룹.CodeName;         //영업그룹명                       
                        break;
                    case "NM_MNGD1":
                        e.Parameter.P34_CD_MNG = "A21";
                        break;
                    case "NM_MNGD2":
                        e.Parameter.P34_CD_MNG = "A22";
                        break;
                    case "NM_MNGD3":
                        e.Parameter.P34_CD_MNG = "A25";
                        break;
                    case "CD_UNIT":
                        if (D.GetString(_flex["NO_PROJECT"]) != string.Empty)
                            e.Parameter.P64_CODE4 = D.GetString(_flex["NO_PROJECT"]);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flex_AfterCodeHelp         

        void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel) return;
                        if (_biz.Get업체별프로세스 == "003")
                        {
                            CheckItem(D.GetString(_flex[e.Row, "CD_PLANT"]), D.GetDecimal(_flex[e.Row, "SEQ_SO"]), helpReturn.Rows, e.Row);
                        }
                        else
                            품목추가(e.Row, helpReturn.Rows);
                        break;

                    case "CD_SL":
                        _flex["SL_QT_INV"] = BASIC.GetQtInv(D.GetString(_flex["CD_PLANT"]), D.GetString(helpReturn.Rows[0]["CD_SL"]), D.GetString(_flex["CD_ITEM"]), dtp수주일자.Text);
                        if (Use가용재고)
                        {
                            한국화장품 hkcos = new 한국화장품();
                            _flex["QT_USEINV"] = hkcos.Search가용재고(D.GetString(_flex["CD_PLANT"]), D.GetString(helpReturn.Rows[0]["CD_SL"]), D.GetString(_flex["CD_ITEM"]) + "|");
                        }
                        if (서버키 == "KOREAF" || 서버키 == "DZSQL")    //가용재고(현재고 - 미출하수량) 2012.02.24(PIMS:D20120220022)
                        {
                            DataTable dtQtMiGi = _biz.SearchMiGi(D.GetString(_flex["CD_PLANT"]), D.GetString(helpReturn.Rows[0]["CD_SL"]), D.GetString(_flex["CD_ITEM"]) + "|");

                            if (dtQtMiGi == null || dtQtMiGi.Rows.Count == 0)
                                _flex["QT_USEINV"] = D.GetDecimal(_flex["SL_QT_INV"]);
                            else
                                _flex["QT_USEINV"] = D.GetDecimal(_flex["SL_QT_INV"]) - D.GetDecimal(dtQtMiGi.Rows[0]["QT_MI_GI"]);
                        }
                        break;
                    default:
                        break;
                }
            } 
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flex_ValidateEdit          

        void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;
                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_PLANT":
                        _flex["CD_ITEM"] = string.Empty;
                        _flex["NM_ITEM"] = string.Empty;
                        _flex["STND_ITEM"] = string.Empty;
                        _flex["UNIT_SO"] = string.Empty;
                        _flex["UNIT_IM"] = string.Empty;
                        _flex["TP_ITEM"] = string.Empty;
                        _flex["DT_REQGI"] = string.Empty;
                        _flex["QT_IM"] = 0m;
                        _flex["CD_SL"] = string.Empty;
                        _flex["NM_SL"] = string.Empty;
                        _flex["AM_PROFIT"] = 0m;
                        소요자재그리드CLEAR();
                        break;

                    case "DT_DUEDATE":
                        if (!D.StringDate.IsValidDate(newValue, false, _flex.Cols[ColName].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (newValue != string.Empty)
                        {
                            if (D.GetDecimal(dtp수주일자.Text) > D.GetDecimal(newValue))
                            {
                                ShowMessageKor("납기일은 수주일보다 이전일수 없습니다.");
                                _flex["DT_DUEDATE"] = oldValue;
                                _flex["DT_REQGI"] = oldValue;
                                e.Cancel = true;
                                return;
                            }

                            수주관리.Calc _calc = new 수주관리.Calc();
                            _flex["DT_REQGI"] = _calc.출하예정일조회(newValue, D.GetInt(_flex["LT_GI"]));
                        }
                        
                        if (_biz.Get업체별프로세스 == "001")
                        {
                            _flex["UM_SO"] = BASIC.GetUM(D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), ctx거래처.CodeValue, ctx영업그룹.CodeValue, newValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), "SA");
                            Calc금액변경(e.Row);
                            Calc부가세포함(e.Row);

                            if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                                _biz.예상이익(_flex, e.Row);
                            _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SO"]) / D.GetDecimal(_flex["AREA"]));
                        }
                        break;

                    case "DT_REQGI":
                        if (!D.StringDate.IsValidDate(newValue, false, _flex.Cols[ColName].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (newValue != string.Empty)
                        {
                            if (D.GetDecimal(dtp수주일자.Text) > D.GetDecimal(newValue))
                            {
                                ShowMessageKor("출하예정일은 수주일보다 이전일수 없습니다.");
                                _flex["DT_REQGI"] = oldValue;
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;

                    case "UM_OPT":  // 특수단가
                        특수단가사용시단가계산(_flex.Row); 
                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);

                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        break;

                    case "TP_VAT":
                        _flex["RT_VAT"] = BASIC.GetTPVAT(newValue);
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        Calc부가세포함(e.Row);

                        break;

                    case "QT_SO":
                        _flex["UNIT_SO_FACT"] = D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0 ? 1 : _flex["UNIT_SO_FACT"];
                        _flex["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        if (Use부가세포함)
                        {
                            _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex["UMVAT_SO"]));
                            _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                            _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                            _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                            _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        }
                        else
                        {
                            Calc금액변경(e.Row);
                            Calc부가세포함(e.Row);
                        }
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        if (_biz.Get업체별프로세스 == "001")
                            _flex["TOTAL_AREA"] = D.GetDecimal(newValue) * D.GetDecimal(_flex["AREA"]);
                        else if (_biz.Get업체별프로세스 == "003")   //아카데미과학(PIMS:D20111115050)
                        {
                            _flex["AM_PACKING"] = D.GetDecimal(newValue) * D.GetDecimal(_flex["PITEM_NUM_USERDEF3"]);
                            _flex["QT_PACKING"] = D.GetDecimal(newValue) / D.GetDecimal(_flex["UNIT_GI_FACT"]);
                        }
                        break;

                    case "UM_SO":
                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);

                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        if (_biz.Get업체별프로세스 == "001")
                            _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) / D.GetDecimal(_flex["AREA"]));
                        break;

                    case "AM_SO":
                        if (D.GetDecimal(_flex["QT_SO"]) != 0)
                            _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) / D.GetDecimal(_flex["QT_SO"]));
                        else
                            _flex["UM_SO"] = 0;

                        _flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * cur환율.DecimalValue);
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        
                        Calc부가세포함(e.Row);

                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        if (_biz.Get업체별프로세스 == "001")
                            _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SO"]) / D.GetDecimal(_flex["AREA"]));
                        break;

                    case "RT_DSCNT": //할인율
                        if (D.GetDecimal(_flex["UM_BASE"]) != 0)
                        {
                            if (서버키 == "KFL") // (한국화스너전용 : 할인율 KEY-IN시 단가 올림). 2011.06.13 SJH
                                _flex["UM_SO"] = decimal.Ceiling(D.GetDecimal(_flex["UM_BASE"]) - (D.GetDecimal(_flex["UM_BASE"]) * D.GetDecimal(newValue)) / 100);
                            else
                                _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_BASE"]) - (D.GetDecimal(_flex["UM_BASE"]) * D.GetDecimal(newValue)) / 100);
                        }
                        else
                            _flex["UM_SO"] = 0;

                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);
                        
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        if (_biz.Get업체별프로세스 == "001")
                            _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SO"]) / D.GetDecimal(_flex["AREA"]));
                        break;

                    case "UM_BASE": //기준단가
                        if (D.GetDecimal(_flex["RT_DSCNT"]) != 0)
                            _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) - (D.GetDecimal(newValue) * D.GetDecimal(_flex["RT_DSCNT"])) / 100);
                        else
                            _flex["UM_SO"] = 0;

                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);

                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);
                        if (_biz.Get업체별프로세스 == "001")
                            _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SO"]) / D.GetDecimal(_flex["AREA"]));
                        break;

                    case "NUM_USERDEF1":
                    case "NUM_USERDEF2":
                        switch (서버키)
                        {
                            case "KLW":
                                _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["NUM_USERDEF1"]) + D.GetDecimal(_flex["NUM_USERDEF2"]));
                                Calc금액변경(e.Row);
                                Calc부가세포함(e.Row);

                                if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                                    _biz.예상이익(_flex, e.Row);
                                break;

                            default:
                                if (_biz.Get업체별프로세스 == "001")
                                {
                                    _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex["AREA"]));
                                    Calc금액변경(e.Row);
                                    Calc부가세포함(e.Row);

                                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                                        _biz.예상이익(_flex, e.Row);
                                }
                                else if (_biz.Get업체별프로세스 == "008")   //아이코닉스
                                {
                                    if (Use부가세포함)
                                    {
                                        _flex["UMVAT_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["PITEM_NUM_USERDEF1"]) * D.GetDecimal(_flex["NUM_USERDEF1"]) / 100m);
                                        _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UMVAT_SO"]));
                                        _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                        _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                                        _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                                        _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                                    }
                                    else
                                    {
                                        _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["PITEM_NUM_USERDEF1"]) * D.GetDecimal(_flex["NUM_USERDEF1"]) / 100m);
                                        Calc금액변경(e.Row);
                                        Calc부가세포함(e.Row);
                                    }

                                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                                        _biz.예상이익(_flex, e.Row);
                                }
                                break;
                        }
                        break;

                    case "AM_VAT":
                        decimal AM_VAT_ORI = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        if (Math.Abs(D.GetDecimal(newValue) - AM_VAT_ORI) > AM_VAT_ORI * 0.3m)
                        {
                            ShowMessage("부가세를 원부가세의 (±)30% 초과 수정 할 수 없습니다.");
                            _flex["AM_VAT"] = D.GetDecimal(oldValue);
                            e.Cancel = true;
                            return;
                        }
                        Calc부가세포함(e.Row);
                        break;

                    case "UMVAT_SO":
                        _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(newValue));
                        _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                        _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        break;

                    case "AM_WONAMT":   //한국AVL일때만 원화금액 KEY-IN이 허용되어 있음
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        Calc부가세포함(e.Row);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flex_HelpClick             

        void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!추가모드여부)
                {
                    if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                    {
                        return;
                    }
                }

                switch (_flex.Cols[_flex.Col].Name)
                {
                    case "UM_SO":
                        //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                        if (so_Price == "Y") return;
                        if (Use부가세포함) return;

                        P_SA_UM_HISTORY_SUB dlg = new P_SA_UM_HISTORY_SUB(ctx거래처.CodeValue, ctx거래처.CodeName, ctx수주형태.CodeValue, ctx수주형태.CodeName, D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(cbo화폐단위.SelectedValue));
                        if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                            dlg.Set출하기준단가 = false;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (disCount_YN == "N")
                                _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, dlg.단가);
                            else if (disCount_YN == "Y")
                            {
                                _flex["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, dlg.단가);
                                _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_BASE"]) - (D.GetDecimal(_flex["UM_BASE"]) * D.GetDecimal(_flex["RT_DSCNT"])) / 100);
                            }
                            Calc금액변경(_flex.Row);
                            Calc부가세포함(_flex.Row);
                        }
                        break;
                    case "UMVAT_SO":
                        if (so_Price == "Y") return;
                        if (!Use부가세포함) return;

                        P_SA_UM_HISTORY_SUB dlr = new P_SA_UM_HISTORY_SUB(ctx거래처.CodeValue, ctx거래처.CodeName, ctx수주형태.CodeValue, ctx수주형태.CodeName, D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(cbo화폐단위.SelectedValue));
                        dlr.Set출하기준단가 = false;
                        dlr.Set부가세포함단가 = true;

                        if (dlr.ShowDialog() == DialogResult.OK)
                        {
                            _flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, dlr.단가);
                            _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UMVAT_SO"]));
                            _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                            _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                            _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                            _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        }
                        break;
                    case "NO_SO_ORIGINAL":
                        P_SA_SO_ORIGINAL_SUB dlg2 = new P_SA_SO_ORIGINAL_SUB(ctx거래처.CodeValue, ctx거래처.CodeName, D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(_flex["CD_PLANT"]));

                        if (dlg2.ShowDialog() == DialogResult.OK)
                        {
                            string cdCc, nmCc;
                            _flex["NO_SO_ORIGINAL"] = D.GetString(dlg2.원천수주데이터["NO_SO"]);
                            _flex["SEQ_SO_ORIGINAL"] = D.GetDecimal(dlg2.원천수주데이터["SEQ_SO"]);
                            _flex["CD_ITEM"] = D.GetString(dlg2.원천수주데이터["CD_ITEM"]);
                            _flex["NM_ITEM"] = D.GetString(dlg2.원천수주데이터["NM_ITEM"]);
                            _flex["STND_ITEM"] = D.GetString(dlg2.원천수주데이터["STND_ITEM"]);
                            _flex["UNIT_SO"] = D.GetString(dlg2.원천수주데이터["UNIT_SO"]);
                            _flex["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dlg2.원천수주데이터["QT_SO"]));
                            _flex["UNIT_IM"] = D.GetString(dlg2.원천수주데이터["UNIT_IM"]);
                            _flex["TP_ITEM"] = D.GetString(dlg2.원천수주데이터["TP_ITEM"]);
                            _flex["UNIT_SO_FACT"] = D.GetDecimal(dlg2.원천수주데이터["UNIT_SO_FACT"]);
                            _flex["LT_GI"] = D.GetDecimal(dlg2.원천수주데이터["LT_GI"]);
                            _flex["WEIGHT"] = D.GetDecimal(dlg2.원천수주데이터["WEIGHT"]);
                            _flex["UNIT_WEIGHT"] = D.GetString(dlg2.원천수주데이터["UNIT_WEIGHT"]);
                            _flex["YN_ATP"] = D.GetString(dlg2.원천수주데이터["YN_ATP"]);
                            _flex["CUR_ATP_DAY"] = D.GetDecimal(dlg2.원천수주데이터["CUR_ATP_DAY"]);
                            _flex["CD_SL"] = "";
                            _flex["NM_SL"] = "";
                            _flex["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * (D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(_flex["UNIT_SO_FACT"])));
                            CC조회(D.GetString(dlg2.원천수주데이터["GRP_ITEM"]), out cdCc, out nmCc);
                            _flex["CD_CC"] = cdCc;
                            _flex["NM_CC"] = nmCc;
                        }
                        break;
                    case "DC1":
                        if (서버키 == "YWD")
                        {
                            if (D.GetString(_flex["CD_ITEM"]) == string.Empty) break;

                            DataTable dtLot = new DataTable();
                            _biz.dtLot_Schema(dtLot);

                            DataRow dr2 = dtLot.NewRow();
                            dr2["CD_PLANT"] = _flex["CD_PLANT"];
                            dr2["CD_ITEM"] = _flex["CD_ITEM"];      //품목코드
                            dr2["NM_ITEM"] = _flex["NM_ITEM"];      //품목명
                            dr2["STND_ITEM"] = _flex["STND_ITEM"];  //규격
                            dr2["UNIT_IM"] = _flex["UNIT_IM"];      //단위
                            dr2["QT_GOOD_INV"] = _flex["QT_IM"];
                            dr2["CD_SL"] = _flex["CD_SL"];          //창고코드
                            dr2["NM_SL"] = _flex["NM_SL"];          //창고명
                            dr2["CD_QTIOTP"] = _출하형태;
                            dtLot.Rows.Add(dr2.ItemArray);

                            P_PU_LOT_SUB_I dlg3 = new P_PU_LOT_SUB_I(dtLot, new string[] { "N", D.GetString(_flex["NO_PROJECT"]), D.GetString(_flex["NM_PROJECT"]), "P_NO_SO" });
                            if (dlg3.ShowDialog() == DialogResult.OK)
                            {
                                _flex["DC1"] = dlg3.GetLotNo;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flex_StartEdit             

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!추가모드여부 && _flex.RowState(e.Row) != DataRowState.Added)
                {
                    if (서버키 == "HKCOS" || 서버키 == "TOKIMEC")
                    {
                        if (_flex.Cols[e.Col].Name != "QT_SO" && _flex.Cols[e.Col].Name != "UM_SO")
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            if (!_biz.LineCheck(txt수주번호.Text, D.GetDecimal(_flex["SEQ_SO"])))
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                string ColName = _flex.Cols[e.Col].Name;

                switch (ColName)
                {
                    case "UNIT_WEIGHT" :
                    case "WEIGHT" :
                    case "UM_INV":
                    case "AM_PRIFT" :
                        e.Cancel = true;
                        return;
                    case "UM_SO":
                    case "AM_SO":
                        if (Use부가세포함)
                        {
                            e.Cancel = true;
                            return;
                        }
                        //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                        if (so_Price == "Y")
                        {
                            ShowMessage("영업단가통제된 영업그룹입니다.");
                            e.Cancel = true;
                        }
                        break;
                    case "AM_VAT":
                        if (Use부가세포함)
                            e.Cancel = true;
                        break;
                    case "UMVAT_SO":
                        if (!Use부가세포함)
                            e.Cancel = true;
                        if (so_Price == "Y")
                        {
                            ShowMessage("영업단가통제된 영업그룹입니다.");
                            e.Cancel = true;
                        }
                        break;
                    case "CD_ITEM":
                        if (D.GetDecimal(_flex["SEQ_PROJECT"]) != 0)
                        {
                            ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        if (_flexUser.HasNormalRow)
                        {
                            ShowMessage("품목[" + D.GetString(_flex["CD_ITEM"]) + "]에 대한 소요자재가 존재합니다.\n\n소요자재 삭제 후 다시 시도하시기 바랍니다.");
                            e.Cancel = true;
                            return;
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

        #region ▷ _flex_Menu_Click            
        private void Menu_Click(object sender, EventArgs e)
        {
            string cd_item_multi = "";

            for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                cd_item_multi += D.GetString(_flex[i, "CD_ITEM"]) + "|";

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "현재고조회":
                    P_PU_STOCK_SUB m_dlg = new P_PU_STOCK_SUB(D.GetString(_flex["CD_PLANT"]), cd_item_multi);
                    m_dlg.ShowDialog(this);
                    break;

                case "품목수정":
                    H_SA_Z_FAWOO_SO_ITEM_SUB dlg = new H_SA_Z_FAWOO_SO_ITEM_SUB(cd_item_multi);
                    dlg.ShowDialog(this);
                    break;

                case "창고별재고인쇄":
                    창고별현재고인쇄(cd_item_multi);
                    break;
            }
        }

        private void Menu_Click2(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "현재고조회":
                    string cd_item_multi = "";
                    cd_item_multi = Common.MultiString(_flexUser.DataTable, "CD_MATL", "|");

                    P_PU_STOCK_SUB m_dlg = new P_PU_STOCK_SUB(D.GetString(_flex["CD_PLANT"]), cd_item_multi);
                    m_dlg.ShowDialog(this);
                    break;
            }
        }

        #endregion

        #region ▷ _flex_AfterRowChange        

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                decimal Key = D.GetDecimal(_flex[e.NewRange.r1, "SEQ_SO"]);
                string Filter = "SEQ_SO = " + Key + "";

                _flexUser.RowFilter = Filter;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flexUser_BeforeCodeHelp    

        void _flexUser_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!추가모드여부)
                {
                    if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                switch (_flexUser.Cols[e.Col].Name)
                {
                    case "CD_MATL":
                        e.Parameter.P09_CD_PLANT = D.GetString(_flex["CD_PLANT"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flexUser_AfterCodeHelp     

        void _flexUser_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;

                switch (_flexUser.Cols[e.Col].Name)
                {
                    case "CD_MATL":
                        if (e.Result.DialogResult == DialogResult.Cancel) return;

                        _flexUser.Redraw = false;

                        foreach (DataRow row in helpReturn.Rows)
                        {
                            if (e.Result.Rows[0] != row)     //첫번째 행이 아닐 경우만 추가를 해준다.
                                btn소요자재추가_Click(null, null);

                            _flexUser["CD_MATL"] = row["CD_ITEM"];
                            _flexUser["NM_ITEM"] = row["NM_ITEM"];
                            _flexUser["STND_ITEM"] = row["STND_ITEM"];
                            _flexUser["UNIT_IM"] = row["UNIT_IM"];
                            _flexUser["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                            _flexUser["UNIT_GI_FACT"] = D.GetDecimal(row["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_GI_FACT"]);
                            _flexUser["UNIT_GI_FACT_ORI"] = D.GetDecimal(_flexUser["UNIT_GI_FACT"]);
                            _flexUser["QT_NEED"] = 1m;
                            _flexUser["QT_NEED_UNIT"] = 1m / D.GetDecimal(_flex["QT_IM"]);
                            _flexUser["UNIT_GI2"] = D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]);
                        }
                        _flexUser.AddFinished();
                        _flexUser.Col = _flex.Cols.Fixed;
                        _flexUser.Redraw = true;
                        break;
                }

                if (_flexUser.HasNormalRow) btn소요자재삭제.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flexUser_ValidateEdit      

        void _flexUser_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                switch (_flexUser.Cols[e.Col].Name)
                {
                    case "QT_NEED": //소모수량
                        if (D.GetDecimal(newValue) <= 0m)
                        {
                            ShowMessage("소모수량에 0 또는 음수를 입력 할 수 없습니다.");
                            _flexUser["QT_NEED"] = D.GetDecimal(oldValue);
                            e.Cancel = true;
                            return;
                        }
                        _flexUser["QT_NEED_UNIT"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED"]) / D.GetDecimal(_flex["QT_IM"]));
                        _flexUser["UNIT_GI_FACT"] = Unit.수량(DataDictionaryTypes.SA, (D.GetDecimal(newValue) / D.GetDecimal(oldValue)) * D.GetDecimal(_flexUser["UNIT_GI_FACT"]));
                        _flexUser["UNIT_GI2"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        break;
                    case "UNIT_GI_FACT": //출하단위수량/LED 모듈 총길이
                        if (D.GetDecimal(newValue) <= 0m)
                        {
                            ShowMessage("출하단위수량에 0 또는 음수를 입력 할 수 없습니다.");
                            _flexUser["UNIT_GI_FACT"] = D.GetDecimal(oldValue);
                            e.Cancel = true;
                            return;
                        }
                        decimal 변경비율 = D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]) == 0m ? 1m : D.GetDecimal(newValue) / D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]);
                        _flexUser["QT_NEED"] = Unit.수량(DataDictionaryTypes.SA, 변경비율);
                        _flexUser["QT_NEED_UNIT"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED"]) / D.GetDecimal(_flex["QT_IM"]));
                        _flexUser["UNIT_GI2"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        break;
                    case "QT_NEED_UNIT": //단위별소요수량
                        _flexUser["QT_NEED"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flex["QT_IM"]));
                        _flexUser["UNIT_GI_FACT"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        _flexUser["UNIT_GI2"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        break;
                    case "UNIT_GI2": //
                        _flexUser["QT_NEED_UNIT"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["UNIT_GI2"]) / D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        _flexUser["QT_NEED"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED_UNIT"]) * D.GetDecimal(_flex["QT_IM"]));
                        _flexUser["UNIT_GI_FACT"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexUser["QT_NEED"]) * D.GetDecimal(_flexUser["UNIT_GI_FACT_ORI"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _flexUser_StartEdit         

        void _flexUser_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!추가모드여부)
                {
                    if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ▶ 컨트롤 이벤트   

        #region ▷ Control_QueryBefore         

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            switch (e.HelpID)
            {
                case HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                    break;
                case HelpID.P_SA_TPSO_SUB:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;
                case HelpID.P_SA_TPPTR_SUB:         // 매출거래처
                    e.HelpParam.P14_CD_PARTNER = ctx거래처.CodeValue;     //납품처 도움창의 거래처컨트롤에 무조건 거래처가 들어가게 변경(2008.12.16)
                    if (서버키 == "HKCOS") e.HelpParam.P00_CHILD_MODE = 서버키;
                    break;
                case HelpID.P_USER:
                    if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P14_CD_PARTNER = ctx거래처.CodeValue; //거래처코드
                        e.HelpParam.P63_CODE3 = ctx거래처.CodeName;       //거래처명
                        e.HelpParam.P17_CD_SALEGRP = ctx영업그룹.CodeValue;   //영업그룹코드
                        e.HelpParam.P62_CODE2 = ctx영업그룹.CodeName;         //영업그룹명
                    }
                    break;
                case HelpID.P_FI_MNGD_SUB:
                    if (bp_Control.Name == "bp관리내역1")
                        e.HelpParam.P34_CD_MNG = "A21";
                    else if (bp_Control.Name == "bp관리내역2")
                        e.HelpParam.P34_CD_MNG = "A22";
                    else if (bp_Control.Name == "bp관리내역3")
                        e.HelpParam.P34_CD_MNG = "A25";
                    break;
                default :
                    break;
            }
        }

        #endregion

        #region ▷ Control_QueryAfter          

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                BpCodeTextBox bp_Control = sender as BpCodeTextBox;

                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        수주형태변경시셋팅(e.CodeValue);
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB:
                        영업그룹변경시셋팅(e.CodeValue);
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
                        ctx납품처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                        ctx납품처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);

                        if (서버키 == "TAERYUK" && (MA.Login.회사코드 == "2000" || MA.Login.회사코드 == "8000"))
                        {
                            DataRow row납품처정보 = BASIC.GetPartner(ctx납품처.CodeValue);
                            txt비고1.Text = (D.GetString(row납품처정보["DC_ADS1_H"]) + " " + D.GetString(row납품처정보["DC_ADS1_D"])).Trim();
                            _header.CurrentRow["DC_RMK1"] = txt비고1.Text;
                        }

                        if (BASIC.GetMAEXC("수주등록-여신잔액 표시") == "001")
                        {
                            lbl여신잔액.Visible = cur여신잔액.Visible = true;
                            cur여신잔액.DecimalValue = 여신관리.GET_CREDIT_REMAIN(ctx거래처.CodeValue, dtp수주일자.Text);
                        }

                        string 영업그룹설정여부 = BASIC.GetMAEXC("거래처선택-영업그룹적용");
                        string 수주형태설정여부 = BASIC.GetMAEXC("거래처선택-수주형태적용");
                        string 담당자설정여부 = BASIC.GetMAEXC("거래처선택-담당자적용");
                        string 단가유형설정여부 = BASIC.GetMAEXC("거래처선택-단가유형적용");

                        DataRow row거래처 = BASIC.GetPartner(e.CodeValue);
                        string CD_SALEGRP = D.GetString(row거래처["CD_SALEGRP"]);
                        string NM_SALEGRP = D.GetString(row거래처["NM_SALEGRP"]);
                        string TP_SO = D.GetString(row거래처["TP_SO"]);
                        string NM_SO = D.GetString(row거래처["NM_SO"]);
                        string CD_EMP_SALE = D.GetString(row거래처["CD_EMP_SALE"]);
                        string NM_EMP_SALE = D.GetString(row거래처["NM_EMP"]);
                        string fgTaxp = D.GetString(row거래처["FG_TAXP"]);

                        if (수주형태설정여부 == "Y" && TP_SO != ctx수주형태.CodeValue)
                        {
                            ctx수주형태.SetCode(TP_SO, NM_SO);
                            _header.CurrentRow["TP_SO"] = TP_SO;
                            _header.CurrentRow["NM_SO"] = NM_SO;
                            수주형태변경시셋팅(TP_SO);
                        }
                        if (영업그룹설정여부 == "Y" && CD_SALEGRP != ctx영업그룹.CodeValue)
                        {
                            ctx영업그룹.SetCode(CD_SALEGRP, NM_SALEGRP);
                            _header.CurrentRow["CD_SALEGRP"] = CD_SALEGRP;
                            _header.CurrentRow["NM_SALEGRP"] = NM_SALEGRP;
                            영업그룹변경시셋팅(CD_SALEGRP);
                        }

                        if (담당자설정여부 == "Y")
                        {
                            ctx담당자.SetCode(CD_EMP_SALE, NM_EMP_SALE);
                            _header.CurrentRow["NO_EMP"] = CD_EMP_SALE;
                            _header.CurrentRow["NM_KOR"] = NM_EMP_SALE;
                        }
                        if (fgTaxp != D.GetString(_header.CurrentRow["FG_TAXP"]))
                        {
                            if (fgTaxp == "001")
                            {
                                rdo계산서처리일괄.Checked = true;
                                rdo계산서처리건별.Checked = false;
                            }
                            else
                            {
                                rdo계산서처리일괄.Checked = false;
                                rdo계산서처리건별.Checked = true;
                            }
                            _header.CurrentRow["FG_TAXP"] = fgTaxp;
                        }

                        // 한국화장품
                        if (단가유형설정여부 == "Y" || 서버키 == "HKCOS")
                        {
                            if (row거래처.Table.Columns.Contains("FG_UM"))
                            {
                                cbo단가유형.SelectedValue = row거래처["FG_UM"];
                                _header.CurrentRow["TP_PRICE"] = row거래처["FG_UM"];
                            }
                        }

                        if (ConfigSA.SA_EXC.거래처선택_운송방법적용)
                        {
                            _header.CurrentRow["FG_TRANSPORT"] = cbo운송방법.SelectedValue = row거래처["FG_TRANSPORT"];
                        }

                        string CD_SL = string.Empty;
                        string NM_SL = string.Empty;

                        if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
                        {
                            CD_SL = D.GetString(row거래처["CD_SL_ISU"]);
                            if (CD_SL != string.Empty)
                            {
                                DataRow row창고 = BASIC.GetSL(D.GetString(cbo공장.SelectedValue), CD_SL);
                                if (row창고 != null)
                                {
                                    ctx창고.SetCode(CD_SL, D.GetString(row창고["NM_SL"]));
                                }
                            }
                            else
                                ctx창고.Clear();
                        }

                        break;
                    case HelpID.P_USER:
                        if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                        {
                            string strYN = BASIC.GetMAEXC("수주등록 - 프로젝트 거래처/영업그룹 동기화");
                            if (strYN == "Y")
                            {
                                ctx거래처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                                ctx거래처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
                                ctx영업그룹.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);
                                ctx영업그룹.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_SALEGRP"]);
                                _header.CurrentRow["CD_PARTNER"] = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                                _header.CurrentRow["CD_SALEGRP"] = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);

                                영업그룹변경시셋팅(ctx영업그룹.CodeValue);
                            }
                        }
                        break;
                    case HelpID.P_MA_SL_SUB:
                        if (_biz.Get_WH적용 == "100")
                        {
                            bp_WH.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_WH"]);
                            bp_WH.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_WH"]);
                        }
                        break;
                    case HelpID.P_SA_TPPTR_SUB:
                        if (서버키 == "TAERYUK" && (MA.Login.회사코드 == "2000" || MA.Login.회사코드 == "8000"))
                        {
                            DataRow row납품처정보2 = BASIC.GetPartner(D.GetString(e.HelpReturn.Rows[0]["CD_TPPTR"]));
                            txt비고1.Text = (D.GetString(row납품처정보2["DC_ADS1_H"]) + " " + D.GetString(row납품처정보2["DC_ADS1_D"])).Trim();
                            _header.CurrentRow["DC_RMK1"] = txt비고1.Text;
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

        #region ▷ SelectionChangeCommitted    

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "cbo화폐단위":
                        화폐단위셋팅();
                        _biz.Set설정(DefaultSettings.화폐단위, D.GetString(cbo화폐단위.SelectedValue));
                        break;
                    case "cbo부가세구분":
                        VAT구분셋팅();
                        break;
                    case "cbo부가세포함":
                        if (D.GetString(cbo부가세포함.SelectedValue) == "Y")
                        {
                            if (D.GetString(cbo화폐단위.SelectedValue) != "000")
                            {
                                ShowMessage("부가세포함여부는 원화(KRW)인 경우에만 'YES'로 변경 할 수 있습니다.");
                                cbo부가세포함.SelectedValue = "N";
                                _header.CurrentRow["FG_VAT"] = "N";
                            }
                        }
                        _biz.Set설정(DefaultSettings.부가세포함, D.GetString(cbo부가세포함.SelectedValue));
                        break;
                    case "cbo공장":
                        ctx창고.CodeValue = string.Empty;
                        ctx창고.CodeName = string.Empty;
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

        #region ▶ 기타 이벤트     

        #region ▷ Page_DataChanged            

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                {
                    ToolBarSaveButtonEnabled = true;
                    btn_ATP.Enabled = true;
                }

                if (추가모드여부)
                {
                    m_btnAppend.Enabled = true;
                    ToolBarDeleteButtonEnabled = false;
                }
                else
                    ToolBarDeleteButtonEnabled = true;

                if (!_flex.HasNormalRow) dtp수주일자.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _header_ControlValueChanged 

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if ((Control)sender == null) return;

                switch (((Control)sender).Name)
                {
                    case "cur환율":
                        환율변경();
                        break;
                    case "dtp수주일자":
                        수주일자변경();
                        break;
                    default:
                        break;
                }

                if (IsChanged())
                    ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ _header_JobModeChanged      

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    m_pnlBaseInfo.Enabled = true;
                    m_pnlAppendInfo.Enabled = true;
                    pnl해외.Enabled = true;
                    txt멀티비고.ReadOnly = false;

                    txt수주번호.Enabled = true;
                    txt수주번호.Text = string.Empty;
                    _header.CurrentRow["NO_SO"] = string.Empty;
                    _header.CurrentRow["NO_HST"] = 0;
                    ctx수주형태.Enabled = true;
                    cbo화폐단위.Enabled = true;
                    cur환율.Enabled = true;
                    if (_구분 == "복사")
                    {
                        cbo부가세구분.Enabled = false;
                        cbo부가세포함.Enabled = false;
                    }
                    else
                    {
                        cbo부가세구분.Enabled = true;
                        if (수주Config.부가세포함단가사용())
                            cbo부가세포함.Enabled = true;
                    }
                    ToolBarDeleteButtonEnabled = false;
                }
                else
                {
                    if (!_헤더수정여부)
                    {
                        m_pnlBaseInfo.Enabled = false;
                        m_pnlAppendInfo.Enabled = false;
                        txt멀티비고.ReadOnly = true;
                    }
                    else
                    {
                        m_pnlBaseInfo.Enabled = true;
                        m_pnlAppendInfo.Enabled = true;
                        txt멀티비고.ReadOnly = false;
                    }
                    txt수주번호.Enabled = false;
                    ctx수주형태.Enabled = false;
                    cbo부가세구분.Enabled = false;
                    cbo부가세포함.Enabled = false;

                    if(_biz.해외적용건존재여부(txt수주번호.Text))
                        pnl해외.Enabled = false;
                    else
                        pnl해외.Enabled = true;

                    DataRow[] drs = _flex.DataTable.Select("STA_SO1 <> 'O'");

                    if (drs == null || drs.Length == 0)
                    {
                        cbo화폐단위.Enabled = true;
                        cur환율.Enabled = true;
                    }
                    else
                    {
                        cbo화폐단위.Enabled = false;
                        cur환율.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ▷ 영업그룹Default셋팅         
        private void 영업그룹Default셋팅()
        {
            string 영업그룹코드 = string.Empty;
            string 영업그룹명 = string.Empty;

            if (_biz.Get설정(DefaultSettings.회사코드) != MA.Login.회사코드)
            {
                _biz.Set설정(DefaultSettings.회사코드, MA.Login.회사코드);
                _biz.Set설정(DefaultSettings.영업그룹코드, string.Empty);
                _biz.Set설정(DefaultSettings.영업그룹명, string.Empty);
                _biz.Set설정(DefaultSettings.수주형태코드, string.Empty);
                _biz.Set설정(DefaultSettings.수주형태명, string.Empty);
            }

            switch (서버키)
            {
                case "CSIT":    //창성
                case "SHINKI":  //신기인터모빌
                    영업그룹코드 = Global.MainFrame.LoginInfo.SalesGroupCode;
                    영업그룹명 = Global.MainFrame.LoginInfo.SalesGroupName;
                    break;
                default:
                    영업그룹코드 = _biz.Get설정(DefaultSettings.영업그룹코드);
                    영업그룹명 = _biz.Get설정(DefaultSettings.영업그룹명);
                    break;
            }

            if (영업그룹코드 == string.Empty) return;

            ctx영업그룹.SetCode(영업그룹코드, 영업그룹명);
            영업그룹변경시셋팅(ctx영업그룹.CodeValue);
            _header.CurrentRow["CD_SALEGRP"] = ctx영업그룹.CodeValue;
            _header.CurrentRow["NM_SALEGRP"] = ctx영업그룹.CodeName;

            ctx납품처.Clear();
            bp관리내역1.Clear();
            bp관리내역2.Clear();
            bp관리내역3.Clear();
            txt관리내역4.Clear();
        }
        #endregion

        #region ▷ 수주형태Default셋팅         
        void 수주형태Default셋팅()
        {
            string 수주형태코드 = _biz.Get설정(DefaultSettings.수주형태코드);
            string 수주형태명 = _biz.Get설정(DefaultSettings.수주형태명);

            if (수주형태코드 == string.Empty) return;

            ctx수주형태.SetCode(_biz.Get설정(DefaultSettings.수주형태코드), _biz.Get설정(DefaultSettings.수주형태명));
            수주형태변경시셋팅(ctx수주형태.CodeValue);
            _header.CurrentRow["TP_SO"] = ctx수주형태.CodeValue;
            _header.CurrentRow["NM_SO"] = ctx수주형태.CodeName;
        }
        #endregion

        #region ▷ 화폐단위Default셋팅         
        private void 화폐단위Default셋팅()
        {
            string cdExch = _biz.Get설정(DefaultSettings.화폐단위) == string.Empty ? "000" : _biz.Get설정(DefaultSettings.화폐단위);
            cbo화폐단위.SelectedValue = cdExch;
            _header.CurrentRow["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);

            string fgVat = _biz.Get설정(DefaultSettings.부가세포함) == string.Empty ? "N" : _biz.Get설정(DefaultSettings.부가세포함);
            if (fgVat != "N" && 수주Config.부가세포함단가사용())
            {
                cbo부가세포함.SelectedValue = fgVat;
                _header.CurrentRow["FG_VAT"] = D.GetString(cbo부가세포함.SelectedValue);
            }

            화폐단위셋팅();
        }
        #endregion

        #region ▷ 수주형태변경시셋팅          
        void 수주형태변경시셋팅(string 수주형태)
        {
            if (수주형태 == string.Empty) return;

            DataRow row수주형태 = BASIC.GetTPSO(수주형태);
            if (D.GetString(row수주형태["CONF"]) == "Y")
            {
                _수주상태 = "R";
                _헤더수정여부 = false;
            }
            else
            {
                _수주상태 = "O";
                _헤더수정여부 = true;
            }

            for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
            {
                _flex[i, "STA_SO1"] = _수주상태;
            }

            _거래구분 = D.GetString(row수주형태["TP_BUSI"]);
            _출하형태 = D.GetString(row수주형태["TP_GI"]);
            _매출형태 = D.GetString(row수주형태["TP_IV"]);
            _의뢰여부 = D.GetString(row수주형태["GIR"]);
            _출하여부 = D.GetString(row수주형태["GI"]);
            _매출여부 = D.GetString(row수주형태["IV"]);
            _수출여부 = D.GetString(row수주형태["TRADE"]);
            _반품여부 = D.GetString(row수주형태["RET"]);

            string 과세구분 = D.GetString(row수주형태["TP_VAT"]);
            
            //default 과세는 수주유형에 셋팅되어 있는 과세를 바라본다.
            if (과세구분 == string.Empty)
            {
                cbo부가세구분.SelectedValue = "11";
                _header.CurrentRow["TP_VAT"] = "11";
            }
            else
            {
                cbo부가세구분.SelectedValue = 과세구분;
                _header.CurrentRow["TP_VAT"] = 과세구분;
            }

            // 2010.09.13 장은경
            수주관리.Setting chk수주관리 = new 수주관리.Setting();

            try
            {
                chk수주관리.거래구분에따른과세구분(_거래구분, 과세구분);
            }
            catch
            {
                string 과세구분default = chk수주관리.거래구분Default과세구분(_거래구분);
                cbo부가세구분.SelectedValue = _header.CurrentRow["TP_VAT"] = 과세구분default;
            }

            if (_거래구분 == "001")
                cbo부가세구분.Enabled = true;
            else
                cbo부가세구분.Enabled = false;

            VAT구분셋팅();

            if (수주Config.수주라인CC설정유형() == 수주관리.수주라인CC설정.수주유형)
            {
                if (D.GetString(row수주형태["CD_CC"]) == string.Empty)
                    ShowMessage("수주유형에 해당하는 C/C가 설정되지 않았습니다.");
            }

            //반품형태이면 출하적용 버튼을 제어 한당.
            if (D.GetString(row수주형태["RET"]) == "N")
            {
                btn출하적용.Visible = false;
                btn_PRJ_SUB.Enabled = true;
                if (_flex.Cols["NO_IO_MGMT"].Visible)
                    _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = false;
            }
            else
            {
                if (_biz.수주반품사용여부)
                {
                    btn출하적용.Visible = true;
                    btn_PRJ_SUB.Enabled = false;
                    if (!_flex.Cols["NO_IO_MGMT"].Visible)
                        _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = true;
                }
            }

            _biz.Set설정(DefaultSettings.수주형태코드, ctx수주형태.CodeValue);
            _biz.Set설정(DefaultSettings.수주형태명, ctx수주형태.CodeName);
        }
        #endregion

        #region ▷ 영업그룹변경시셋팅          
        void 영업그룹변경시셋팅(string 영업그룹)
        {
            DataRow row영업그룹 = null;
            try
            {
                row영업그룹 = BASIC.GetSaleGrp(영업그룹);
            }
            catch
            {
                return;
            }

            _단가적용형태 = D.GetString(row영업그룹["TP_SALEPRICE"]);
            so_Price = D.GetString(row영업그룹["SO_PRICE"]);

            if (_단가적용형태 == "003")     //001:적용않음, 002:유형별, 003:거래처별
                btn품목전개.Visible = true;
            else
                btn품목전개.Visible = false;

            _biz.Set설정(DefaultSettings.영업그룹코드, ctx영업그룹.CodeValue);
            _biz.Set설정(DefaultSettings.영업그룹명, ctx영업그룹.CodeName);
        }
        #endregion

        #endregion

        #region ▶ 기타 메소드     

        #region ▷ ShowMessage                 

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.이미수주확정되어수정삭제가불가능합니다:
                    return ShowMessage("SA_M000116");
            }

            return DialogResult.None;
        }

        #endregion

        #region ▷ 화폐관련                    

        #region -> 화폐단위셋팅

        private void 화폐단위셋팅()
        {
            if (cbo화폐단위.SelectedValue == null || D.GetString(cbo화폐단위.SelectedValue) != "000")
            {
                if (D.GetString(cbo부가세포함.SelectedValue) == "Y")
                {
                    ShowMessage("부가세포함여부가 'YES' 일 때에는 원화(KRW)만 선택 할 수 있습니다.");
                    cbo화폐단위.SelectedValue = "000";
                    _header.CurrentRow["CD_EXCH"] = "000";
                }

                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    SetExchageApply();

                if (MA.기준환율.Option != MA.기준환율옵션.적용_수정불가)
                    cur환율.Enabled = true;
            }

            if (D.GetString(cbo화폐단위.SelectedValue) == "000") //KRW
            {
                cur환율.DecimalValue = 1m;
                _header.CurrentRow["RT_EXCH"] = 1m;
                cur환율.Enabled = false;
            }

            환율변경();
        }

        #endregion

        #region -> 환율적용    
        private void SetExchageApply()
        {
            decimal rt_exch = 0m;
            rt_exch = Unit.환율(DataDictionaryTypes.SA, MA.기준환율적용(dtp수주일자.Text, D.GetString(cbo화폐단위.SelectedValue)));
            cur환율.DecimalValue = rt_exch == 0m ? 1 : rt_exch;
            _header.CurrentRow["RT_EXCH"] = cur환율.DecimalValue;
        }
        #endregion

        #region -> 환율변경    

        private void 환율변경()
        {
            if (!_flex.HasNormalRow) return;

            _flex.Redraw = false;
            try
            {
                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    _flex[i, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_SO"]) * cur환율.DecimalValue);
                    _flex[i, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_WONAMT"]) * (D.GetDecimal(_flex[i, "RT_VAT"]) / 100));
                    
                    Calc부가세포함(i);

                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        _biz.예상이익(_flex, i);

                    _flex[i, "CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                }
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region -> 수주일자 변경시 환율세팅

        private void 수주일자변경()
        {
            if (D.GetString(cbo화폐단위.SelectedValue) == "000") return;
            if (MA.기준환율.Option == MA.기준환율옵션.적용안함) return;

            decimal oldRtExch = cur환율.DecimalValue;
            SetExchageApply();
            if (oldRtExch != cur환율.DecimalValue) 환율변경();
        }

        #endregion

        #endregion

        #region ▷ VAT구분셋팅                 

        private void VAT구분셋팅()
        {
            if (D.GetString(cbo부가세구분.SelectedValue) == string.Empty || cbo부가세구분.DataSource == null)
            {
                cur부가세율.DecimalValue = 0;
                _header.CurrentRow["RT_VAT"] = 0;
                return;
            }

            DataTable dt = (DataTable)cbo부가세구분.DataSource;
            DataRow row = dt.Rows.Find(cbo부가세구분.SelectedValue);
            
            cur부가세율.DecimalValue = D.GetDecimal(row["CD_FLAG1"]);
            _header.CurrentRow["RT_VAT"] = D.GetDecimal(row["CD_FLAG1"]);

            VAT구분변경();
        }

        #endregion

        #region ▷ IsChanged                   

        protected override bool IsChanged() 
        {
            if (base.IsChanged())   // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;    // 헤더가 변경되었거나
        }

        #endregion

        #region ▷ 채번코드 구하기             
        private string get_Ctrl()
        {
            string cd_Ctrl = string.Empty;

            if (_반품여부 == "N") cd_Ctrl = "02";
            else cd_Ctrl = "16";
            
            return cd_Ctrl;
        }
        #endregion

        #region ▷ 견적관련                    

        #region -> 헤더세팅
        void 견적H셋팅(DataRow row견적H)
        {
            _header.CurrentRow["NO_EST"] = txt통합견적번호.Text = D.GetString(row견적H["NO_EST"]);
            _header.CurrentRow["NO_EST_HST"] = cur통합견적차수.DecimalValue = D.GetDecimal(row견적H["NO_HST"]);
            _header.CurrentRow["CD_EXPORT"] = row견적H["CD_EXPORT"];
            _header.CurrentRow["NM_EXPORT"] = row견적H["NM_EXPORT"];
            ctx수출자.SetCode(D.GetString(row견적H["CD_EXPORT"]), D.GetString(row견적H["NM_EXPORT"]));
            _header.CurrentRow["CD_PRODUCT"] = row견적H["CD_PRODUCT"];
            _header.CurrentRow["NM_PRODUCT"] = row견적H["NM_PRODUCT"];
            ctx제조자.SetCode(D.GetString(row견적H["CD_PRODUCT"]), D.GetString(row견적H["NM_PRODUCT"]));
            _header.CurrentRow["COND_TRANS"] = txt인도조건.Text = D.GetString(row견적H["COND_TRANS"]);
            _header.CurrentRow["COND_PAY"] = cbo결제형태.SelectedValue = D.GetString(row견적H["COND_PAY"]);
            _header.CurrentRow["COND_DAYS"] = cur결제일.DecimalValue = D.GetDecimal(row견적H["COND_DAYS"]);
            _header.CurrentRow["TP_PACKING"] = cbo포장형태.SelectedValue = D.GetString(row견적H["TP_PACKING"]);
            _header.CurrentRow["FG_TRANSPORT"] = cbo운송방법.SelectedValue = D.GetString(row견적H["TP_TRANS"]);
            _header.CurrentRow["TP_TRANSPORT"] = cbo운송형태.SelectedValue = D.GetString(row견적H["TP_TRANSPORT"]);
            _header.CurrentRow["NM_INSPECT"] = txt검사기관.Text = D.GetString(row견적H["NM_INSPECT"]);
            _header.CurrentRow["PORT_LOADING"] = txt선적항.Text = D.GetString(row견적H["PORT_LOADING"]);
            _header.CurrentRow["PORT_ARRIVER"] = txt도착항.Text = D.GetString(row견적H["PORT_ARRIVER"]);
            _header.CurrentRow["CD_ORIGIN"] = cbo원산지.SelectedValue = D.GetString(row견적H["CD_ORIGIN"]);
            _header.CurrentRow["DESTINATION"] = txt목적지.Text = D.GetString(row견적H["DESTINATION"]);
            _header.CurrentRow["DT_EXPIRY"] = dtp유효일자해외.Text = D.GetString(row견적H["DT_EXPIRY"]);
            _header.CurrentRow["COND_PRICE"] = cbo가격조건.SelectedValue = D.GetString(row견적H["COND_PRICE"]);

            //_header.CurrentRow["NO_PROJECT"] = _header.CurrentRow["NM_PROJECT"] = "";

            if (D.GetString(row견적H["DT_DUEDATE"]) != "")
                dtp납기일.Text = D.GetString(row견적H["DT_DUEDATE"]);
            _header.CurrentRow["FG_BILL"] = m_cboFgBill.SelectedValue = D.GetString(row견적H["FG_BILL"]);
            _header.CurrentRow["NO_CONTRACT"] = txt계약번호.Text = D.GetString(row견적H["NO_PO"]);
            ctx프로젝트.Clear();

            _header.CurrentRow["CD_NOTIFY"] = _header.CurrentRow["NM_NOTIFY"] = "";
            ctx착하통지처.Clear();
            _header.CurrentRow["CD_CONSIGNEE"] = _header.CurrentRow["NM_CONSIGNEE"] = "";
            ctx수하인.Clear();

            _header.CurrentRow["DC_RMK_TEXT"] = txt멀티비고.Text = D.GetString(row견적H["DC_RMK_TEXT"]);

            if (서버키 == "FORTIS") //포티스 일 때 견적번호를 수주번호에 세팅해준다.(2011.06.23)
            {
                _header.CurrentRow["NO_SO"] = txt수주번호.Text = D.GetString(row견적H["NO_EST"]);
                _header.CurrentRow["NO_PROJECT"] = D.GetString(row견적H["NO_EST"]);
                ctx프로젝트.SetCode(D.GetString(row견적H["NO_EST"]), D.GetString(row견적H["NO_EST"]));
            }
        }
        #endregion

        #region -> 라인세팅
        void 견적품목셋팅(DataRow[] dr견적D)
        {
            _flex.RemoveViewAll();

            int seq = 1;
            string cdCc, nmCc;

            foreach (DataRow row견적 in dr견적D)
            {
                DataRow newrow = _flex.DataTable.NewRow();

                newrow["SEQ_SO"] = seq++;
                newrow["GI_PARTNER"] = ctx납품처.CodeValue;
                newrow["LN_PARTNER"] = ctx납품처.CodeName;
                newrow["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newrow["UM_SO"] = newrow["AM_SO"] = newrow["AM_VAT"] = newrow["QT_IM"] = 0));

                if (disCount_YN == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
                {
                    newrow["RT_DSCNT"] = 0;  //할인율
                    newrow["UM_BASE"] = 0;   //기준단가
                }

                newrow["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                newrow["RT_VAT"] = cur부가세율.DecimalValue;

                CC조회(D.GetString(row견적["CD_ITEMGRP"]), out cdCc, out nmCc);
                newrow["CD_CC"] = cdCc;
                newrow["NM_CC"] = nmCc;

                if (D.GetString(row견적["DT_DUEDATE"]) != string.Empty)
                {
                    newrow["DT_DUEDATE"] = row견적["DT_DUEDATE"];
                    newrow["DT_REQGI"] = row견적["DT_DUEDATE"];
                }
                else
                {
                    newrow["DT_DUEDATE"] = dtp납기일.Text;
                    newrow["DT_REQGI"] = dtp납기일.Text;
                }
                newrow["DT_REQGI"] = dtp납기일.Text;
                newrow["STA_SO1"] = _수주상태;

                newrow["CD_PLANT"] = row견적["CD_PLANT"];
                newrow["CD_ITEM"] = row견적["CD_ITEM"];
                newrow["NM_ITEM"] = row견적["NM_ITEM"];
                newrow["STND_ITEM"] = row견적["STND_ITEM"];
                newrow["UNIT_SO"] = row견적["UNIT_SO"];
                newrow["TP_ITEM"] = row견적["TP_ITEM"];
                newrow["TP_VAT"] = row견적["TP_VAT"];
                newrow["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row견적["QT_EST"]));
                newrow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row견적["UM_EST"]));
                newrow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row견적["AM_EST"]));
                newrow["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row견적["AM_EST"]) * cur환율.DecimalValue);         //원화금액
                newrow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_WONAMT"]) * (cur부가세율.DecimalValue / 100));  //부가세
                newrow["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_WONAMT"]) + D.GetDecimal(newrow["AM_VAT"]));
                newrow["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newrow["AMVAT_SO"]) / D.GetDecimal(newrow["QT_SO"]));
                newrow["UNIT_SO_FACT"] = D.GetDecimal(row견적["UNIT_SO_FACT"]) == 0 ? 1 : row견적["UNIT_SO_FACT"];
                newrow["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newrow["QT_SO"]) * D.GetDecimal(newrow["UNIT_SO_FACT"]));
                newrow["UNIT_IM"] = row견적["UNIT_IM"];

                newrow["EN_ITEM"] = row견적["EN_ITEM"];
                newrow["STND_DETAIL_ITEM"] = row견적["STND_DETAIL_ITEM"];
                newrow["GRP_MFG"] = row견적["GRP_MFG"];
                newrow["LT_GI"] = D.GetDecimal(row견적["LT_GI"]);
                newrow["WEIGHT"] = row견적["WEIGHT"];
                newrow["UNIT_WEIGHT"] = row견적["UNIT_WEIGHT"];
                newrow["FG_SERNO"] = row견적["FG_SERNO"];
                newrow["YN_ATP"] = row견적["YN_ATP"];
                newrow["CUR_ATP_DAY"] = row견적["CUR_ATP_DAY"];
                newrow["FG_MODEL"] = row견적["FG_MODEL"];
                newrow["UNIT_SO_FACT"] = D.GetDecimal(row견적["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row견적["UNIT_SO_FACT"]);
                newrow["UNIT_GI_FACT"] = D.GetDecimal(row견적["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row견적["UNIT_GI_FACT"]);

                if (서버키 == "FORTIS") //포티스 일 때 견적번호를 수주번호에 세팅해준다.(2011.06.23)
                {
                    newrow["NO_PROJECT"] = D.GetString(row견적["NO_EST"]);
                    newrow["NM_PROJECT"] = D.GetString(row견적["NO_EST"]);
                }

                newrow["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                newrow["CD_MNGD1"] = bp관리내역1.CodeValue;
                newrow["NM_MNGD1"] = bp관리내역1.CodeName;
                newrow["CD_MNGD2"] = bp관리내역2.CodeValue;
                newrow["NM_MNGD2"] = bp관리내역2.CodeName;
                newrow["CD_MNGD3"] = bp관리내역3.CodeValue;
                newrow["NM_MNGD3"] = bp관리내역3.CodeName;
                newrow["CD_MNGD4"] = txt관리내역4.Text;

                _flex.DataTable.Rows.Add(newrow);
            }

            _flex.SumRefresh();
            _flex.Row = _flex.Rows.Count - 1;
            _flex.IsDataChanged = true;
            ToolBarSaveButtonEnabled = true;
            Page_DataChanged(null, null);
        }
        #endregion

        #endregion

        #region ▷ 품목추가                    

        void 품목추가(int idx, DataRow[] dr품목)
        {
            bool 헤더과세일치 = true;
            string 납기요구일 = string.Empty;
            string 공장 = string.Empty;
            string 기준일자 = string.Empty;

            if (_flex.Rows.Count == 3 && D.GetString(_flex[idx, "DT_DUEDATE"]) == string.Empty)
                납기요구일 = dtp납기일.Text;
            else if (_flex.Rows.Count >= 3 && D.GetString(_flex[idx, "DT_DUEDATE"]) != string.Empty)
                납기요구일 = D.GetString(_flex[idx, "DT_DUEDATE"]);

            if (_biz.Get업체별프로세스 == "001") 기준일자 = 납기요구일;
            else 기준일자 = dtp수주일자.Text;

            if (D.GetString(_flex[idx, "CD_PLANT"]) != string.Empty)
                공장 = D.GetString(_flex[idx, "CD_PLANT"]);

            DataTable dt할인율 = null;
            if (disCount_YN == "Y")
            {
                if (_biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
                {
                    dt할인율 = _biz.할인율(공장, ctx거래처.CodeValue, dr품목);
                }
                else if (_biz.Get할인율적용 == 수주관리.할인율적용.한국화장품)
                {
                    한국화장품 hkcos = new 한국화장품();
                    dt할인율 = hkcos.할인율(dr품목, 공장, ctx거래처.CodeValue, 기준일자, D.GetString(cbo단가유형.SelectedValue), ctx수주형태.CodeValue, ctx창고.CodeValue);
                }
            }

            DataTable dt예상이익 = null;
            if (공장 != string.Empty && _biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
            {
                dt예상이익 = _biz.예상이익(공장, 기준일자, dr품목);
            }

            string CD_SL = string.Empty;
            string NM_SL = string.Empty;

            if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
            {
                DataRow row거래처 = BASIC.GetPartner(ctx거래처.CodeValue);
                CD_SL = D.GetString(row거래처["CD_SL_ISU"]);
                if (CD_SL != string.Empty)
                {
                    DataRow row창고 = BASIC.GetSL(공장, CD_SL);

                    if (row창고 == null)
                        CD_SL = NM_SL = string.Empty;
                    else
                        NM_SL = D.GetString(row창고["NM_SL"]);
                }
            }

            bool First = true;
            _flex.Redraw = false;
            _flex.SetDummyColumnAll();

            수주관리.Calc c = new 수주관리.Calc();
            string cdCc, nmCc;

            string multiItem = Common.MultiString(dr품목, "CD_ITEM", "|");
            DataTable dtQtInv = BASIC.GetQtInvMulti(multiItem, dtp수주일자.Text);
            dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_SL"], dtQtInv.Columns["CD_ITEM"] };

            DataTable dtQtMiGi = null;
            if (서버키 == "KOREAF" || 서버키 == "DZSQL")
            {
                dtQtMiGi = _biz.SearchMiGi(공장, string.Empty, multiItem);
                dtQtMiGi.PrimaryKey = new DataColumn[] { dtQtMiGi.Columns["CD_PLANT"], dtQtMiGi.Columns["CD_SL"], dtQtMiGi.Columns["CD_ITEM"] };
            }

            DataTable dtUmFixed = null;
            if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
            {
                dtUmFixed = _biz.SearchUmFixed(ctx거래처.CodeValue, 공장, multiItem);
                dtUmFixed.PrimaryKey = new DataColumn[] { dtUmFixed.Columns["CD_ITEM"] };
            }

            foreach (DataRow row in dr품목)
            {
                if (First)
                {
                    #region ▼
                    _flex[idx, "CD_PLANT"] = 공장;
                    _flex[idx, "CD_ITEM"] = row["CD_ITEM"];
                    _flex[idx, "NM_ITEM"] = row["NM_ITEM"];
                    _flex[idx, "EN_ITEM"] = row["EN_ITEM"];
                    _flex[idx, "STND_ITEM"] = row["STND_ITEM"];
                    _flex[idx, "STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                    _flex[idx, "UNIT_SO"] = row["UNIT_SO"];
                    _flex[idx, "UNIT_IM"] = row["UNIT_IM"];
                    _flex[idx, "TP_ITEM"] = row["TP_ITEM"];
                    _flex[idx, "GRP_MFG"] = row["GRP_MFG"];
                    _flex[idx, "NM_GRP_MFG"] = row["NM_GRP_MFG"];
                    _flex[idx, "LT_GI"] = D.GetDecimal(row["LT_GI"]);
                    _flex[idx, "GI_PARTNER"] = ctx납품처.CodeValue;
                    _flex[idx, "LN_PARTNER"] = ctx납품처.CodeName;
                    _flex[idx, "WEIGHT"] = row["WEIGHT"];
                    _flex[idx, "UNIT_WEIGHT"] = row["UNIT_WEIGHT"];
                    _flex[idx, "NO_PO_PARTNER"] = txt거래처PO.Text;
                    _flex[idx, "FG_SERNO"] = row["FG_SERNO"];
                    _flex[idx, "YN_ATP"] = row["YN_ATP"];
                    _flex[idx, "CUR_ATP_DAY"] = row["CUR_ATP_DAY"];
                    _flex[idx, "FG_MODEL"] = row["FG_MODEL"];
                    _flex[idx, "NM_MANAGER1"] = row["NM_MANAGER1"];
                    _flex[idx, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"]);
                    _flex[idx, "UNIT_GI_FACT"] = D.GetDecimal(row["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_GI_FACT"]);

                    CC조회(D.GetString(row["GRP_ITEM"]), out cdCc, out nmCc);
                    _flex[idx, "CD_CC"] = cdCc;
                    _flex[idx, "NM_CC"] = nmCc;

                    if (_biz.Get업체별프로세스 == "001")
                    {
                        _flex[idx, "QT_WIDTH"] = D.GetDecimal(row["QT_WIDTH"]) * 1000m;
                        _flex[idx, "QT_LENGTH"] = row["QT_LENGTH"];
                        _flex[idx, "AREA"] = D.GetDecimal(row["QT_WIDTH"]) * D.GetDecimal(row["QT_LENGTH"]);
                        _flex[idx, "TOTAL_AREA"] = D.GetDecimal(_flex[idx, "AREA"]) * D.GetDecimal(_flex[idx, "QT_SO"]);
                    }
                    else if (_biz.Get업체별프로세스 == "003")   //아카데미과학(PIMS:D20111115050)
                    {
                        _flex[idx, "PITEM_NUM_USERDEF3"] = row["NUM_USERDEF3"];
                        _flex[idx, "PITEM_NUM_USERDEF4"] = row["NUM_USERDEF4"];
                        _flex[idx, "PITEM_NUM_USERDEF5"] = row["NUM_USERDEF5"];
                        _flex[idx, "PITEM_NUM_USERDEF6"] = row["NUM_USERDEF6"];
                        _flex[idx, "PITEM_NUM_USERDEF7"] = row["NUM_USERDEF7"];
                        _flex[idx, "AM_PACKING"] = D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(row["NUM_USERDEF3"]);
                        _flex[idx, "QT_PACKING"] = D.GetDecimal(_flex[idx, "QT_SO"]) / D.GetDecimal(_flex[idx, "UNIT_GI_FACT"]);
                    }
                    else if (_biz.Get업체별프로세스 == "008")
                    {
                        _flex[idx, "PITEM_NUM_USERDEF1"] = row["NUM_USERDEF1"];
                        _flex[idx, "PITEM_NUM_USERDEF2"] = row["NUM_USERDEF2"];
                    }
                    
                    if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
                    {
                        if (서버키 == "HKCOS")
                        {
                            _flex[idx, "CD_SL"] = ctx창고.CodeValue;
                            _flex[idx, "NM_SL"] = ctx창고.CodeName;
                        }
                        else
                        {
                            _flex[idx, "CD_SL"] = CD_SL;
                            _flex[idx, "NM_SL"] = NM_SL;
                        }
                    }
                    else
                    {
                        _flex[idx, "CD_SL"] = row["CD_GISL"];
                        _flex[idx, "NM_SL"] = row["NM_GISL"];
                    }

                    if (_biz.Get업체별프로세스 == "003" && D.GetString(_flex[idx, "CD_SL"]) == string.Empty)
                    {
                        _flex[idx, "CD_SL"] = ctx창고.CodeValue;
                        _flex[idx, "NM_SL"] = ctx창고.CodeName;
                    }

                    // 현재고 세팅(2011.09.19)
                    DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { D.GetString(_flex[idx, "CD_PLANT"]), D.GetString(_flex[idx, "CD_SL"]), D.GetString(_flex[idx, "CD_ITEM"]) });

                    if (rowQtInv == null)
                        _flex[idx, "SL_QT_INV"] = 0m;
                    else
                        _flex[idx, "SL_QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);

                    if (!ctx프로젝트.IsEmpty())
                    {
                        _flex[idx, "NO_PROJECT"] = ctx프로젝트.CodeValue;
                        _flex[idx, "NM_PROJECT"] = ctx프로젝트.CodeName;
                    }

                    if (_biz.Get_WH적용 == "100")
                    {
                        _flex[idx, "CD_WH"] = row["CD_WH"];
                        _flex[idx, "NM_WH"] = row["NM_WH"];
                    }

                    string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

                    if (_biz.Get과세변경유무 == "N" || 품목과세구분 == string.Empty)
                    {
                        _flex[idx, "TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex[idx, "RT_VAT"] = cur부가세율.DecimalValue;
                    }
                    else
                    {
                        _flex[idx, "TP_VAT"] = 품목과세구분;
                        _flex[idx, "RT_VAT"] = D.GetDecimal(row["RT_TAX_SA"]);

                        if (D.GetString(cbo부가세구분.SelectedValue) != D.GetString(_flex[idx, "TP_VAT"]))
                            헤더과세일치 = false;
                    }

                    _flex[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "QT_SO"]) * (D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"])));

                    if (납기요구일 != string.Empty)
                        _flex[idx, "DT_REQGI"] = c.출하예정일조회(납기요구일, D.GetInt(_flex["LT_GI"]));

                    if (_구분 == "복사")
                        _flex[idx, "STA_SO1"] = _수주상태;

                    if (dt할인율 != null)
                    {
                        DataRow row할인율 = dt할인율.Rows.Find(row["CD_ITEM"]);
                        _flex[idx, "RT_DSCNT"] = row할인율 == null ? 0M : row할인율["DC_RATE"];
                    }

                    if (dr품목[0].Table.Columns.Contains("CD_CITEM"))
                    {
                        _flex[idx, "QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_CALC"]));
                        _flex[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, c.관리수량(D.GetDecimal(_flex["QT_SO"]), D.GetDecimal(_flex["UNIT_SO_FACT"])));
                        _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SET"]));
                    }
                    else
                    {
                        switch (서버키)
                        {
                            case "KLW":     // 로스트왁스
                                _flex[idx, "NUM_USERDEF1"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex[idx, "CD_ITEM"]), ctx거래처.CodeValue, "010", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));
                                _flex[idx, "NUM_USERDEF2"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex[idx, "CD_ITEM"]), ctx거래처.CodeValue, "020", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));
                                _flex[idx, "UM_SO"] = D.GetDecimal(_flex[idx, "NUM_USERDEF1"]) + D.GetDecimal(_flex[idx, "NUM_USERDEF2"]);
                                break;
                            case "HKCOS":   // 한국화장품
                                한국화장품 hkcos = new 한국화장품();
                                if (Use가용재고)
                                    _flex[idx, "QT_USEINV"] = hkcos.Search가용재고(공장, D.GetString(_flex[idx, "CD_SL"]), D.GetString(_flex[idx, "CD_ITEM"]) + "|");
                                decimal 한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, hkcos.단가(공장, D.GetString(_flex[idx, "CD_ITEM"]), 기준일자, D.GetString(cbo단가유형.SelectedValue), ctx수주형태.CodeValue, D.GetString(cbo화폐단위.SelectedValue), ctx창고.CodeValue, ctx거래처.CodeValue));
                                _flex[idx, "UM_BASE"] = 한국화장품단가;
                                한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, 한국화장품단가 - (한국화장품단가 * D.GetDecimal(_flex[idx, "RT_DSCNT"]) * 0.01M));
                                _flex[idx, "UM_SO"] = Decimal.Round(한국화장품단가, MidpointRounding.AwayFromZero);
                                break;
                            default:
                                if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                                    일반단가적용(idx, 기준일자);
                                else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                                {
                                    if ((_단가적용형태 == "002" || _단가적용형태 == "003"))
                                        _flex[idx, "UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex[idx, "CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));

                                    특수단가사용시단가계산(idx);
                                }
                                else if (_biz.Get특수단가적용 == 특수단가적용.조선호텔베이커리단가)
                                {
                                    _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.조선호텔베이커리단가(공장, D.GetString(_flex[idx, "CD_ITEM"]), ctx거래처.CodeValue, 기준일자, D.GetString(cbo화폐단위.SelectedValue))));
                                }
                                else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                                {
                                    DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex[idx, "CD_ITEM"]);

                                    if (rowUmFixed == null)
                                        일반단가적용(idx, 기준일자);
                                    else
                                        _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(rowUmFixed["UM_FIXED"]));
                                }
                                break;
                        }
                    }

                    if (서버키 == "KFL") // 할인율이 있으면 단가 올림 (한국화스너전용). 2011.03.03 SJH
                    {
                        if (D.GetDecimal(_flex[idx, "RT_DSCNT"]) != 0m)
                            _flex[idx, "UM_SO"] = decimal.Ceiling(D.GetDecimal(_flex[idx, "UM_SO"]));
                    }
                    else if (서버키 == "KOREAF" || 서버키 == "DZSQL")   //가용재고(현재고 - 미출하수량) 2012.02.24(PIMS:D20120220022)
                    {
                        DataRow rowQtMiGi = dtQtMiGi.Rows.Find(new object[] { D.GetString(_flex[idx, "CD_PLANT"]), D.GetString(_flex[idx, "CD_SL"]), D.GetString(_flex[idx, "CD_ITEM"]) });

                        if (rowQtMiGi == null)
                            _flex[idx, "QT_USEINV"] = D.GetDecimal(_flex[idx, "SL_QT_INV"]);
                        else
                            _flex[idx, "QT_USEINV"] = D.GetDecimal(_flex[idx, "SL_QT_INV"]) - D.GetDecimal(rowQtMiGi["QT_MI_GI"]);
                    }

                    if (Use부가세포함)
                    {
                        _flex[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(_flex[idx, "UMVAT_SO"]));
                        _flex[idx, "AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[idx, "RT_VAT"]))), MidpointRounding.AwayFromZero);
                        _flex[idx, "AM_VAT"] = D.GetDecimal(_flex[idx, "AMVAT_SO"]) - D.GetDecimal(_flex[idx, "AM_WONAMT"]);
                        _flex[idx, "AM_SO"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex[idx, "UM_SO"] = D.GetDecimal(_flex[idx, "QT_SO"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AM_SO"]) / D.GetDecimal(_flex[idx, "QT_SO"]));
                    }
                    else
                    {
                        Calc금액변경(idx);
                        Calc부가세포함(idx);
                    }

                    if (dt예상이익 != null && dt예상이익.Rows.Count > 0)
                    {
                        DataRow row예상이익 = dt예상이익.Rows.Find(row["CD_ITEM"]);
                        _flex[idx, "UM_INV"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row예상이익 == null ? 0M : row예상이익["UM_INV"]));
                        _biz.예상이익(_flex, idx);
                    }

                    if (_biz.Get업체별프로세스 == "001")
                        _flex[idx, "NUM_USERDEF1"] = D.GetDecimal(_flex[idx, "AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "UM_SO"]) / D.GetDecimal(_flex[idx, "AREA"]));

                    소요자재그리드CLEAR();

                    First = false;
                    #endregion
                }
                else
                {
                    #region ▲
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;
                    _flex["CD_PLANT"] = 공장;
                    _flex["CD_ITEM"] = row["CD_ITEM"];
                    _flex["NM_ITEM"] = row["NM_ITEM"];
                    _flex["EN_ITEM"] = row["EN_ITEM"];
                    _flex["STND_ITEM"] = row["STND_ITEM"];
                    _flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                    _flex["UNIT_SO"] = row["UNIT_SO"];
                    _flex["UNIT_IM"] = row["UNIT_IM"];
                    _flex["TP_ITEM"] = row["TP_ITEM"];
                    _flex["GRP_MFG"] = row["GRP_MFG"];
                    _flex["NM_GRP_MFG"] = row["NM_GRP_MFG"];
                    _flex["LT_GI"] = D.GetDecimal(row["LT_GI"]);
                    _flex["GI_PARTNER"] = ctx납품처.CodeValue;
                    _flex["LN_PARTNER"] = ctx납품처.CodeName;
                    _flex["WEIGHT"] = row["WEIGHT"];
                    _flex["UNIT_WEIGHT"] = row["UNIT_WEIGHT"];
                    _flex["NO_PO_PARTNER"] = txt거래처PO.Text;
                    _flex["FG_SERNO"] = row["FG_SERNO"];
                    _flex["YN_ATP"] = row["YN_ATP"];
                    _flex["CUR_ATP_DAY"] = row["CUR_ATP_DAY"];
                    _flex["FG_MODEL"] = row["FG_MODEL"];
                    _flex["NM_MANAGER1"] = row["NM_MANAGER1"];
                    _flex["FG_USE"] = _flex[_flex.Row - 1, "FG_USE"];
                    _flex["UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"]);
                    _flex["UNIT_GI_FACT"] = D.GetDecimal(row["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_GI_FACT"]);

                    CC조회(D.GetString(row["GRP_ITEM"]), out cdCc, out nmCc);
                    _flex["CD_CC"] = cdCc;
                    _flex["NM_CC"] = nmCc;

                    if (_biz.Get업체별프로세스 == "001")
                    {
                        _flex["QT_WIDTH"] = D.GetDecimal(row["QT_WIDTH"]) * 1000m;
                        _flex["QT_LENGTH"] = row["QT_LENGTH"];
                        _flex["AREA"] = D.GetDecimal(row["QT_WIDTH"]) * D.GetDecimal(row["QT_LENGTH"]);
                        _flex["TOTAL_AREA"] = D.GetDecimal(_flex["AREA"]) * D.GetDecimal(_flex["QT_SO"]);
                    }
                    else if (_biz.Get업체별프로세스 == "003")   //아카데미과학(PIMS:D20111115050)
                    {
                        _flex["PITEM_NUM_USERDEF3"] = row["NUM_USERDEF3"];
                        _flex["PITEM_NUM_USERDEF4"] = row["NUM_USERDEF4"];
                        _flex["PITEM_NUM_USERDEF5"] = row["NUM_USERDEF5"];
                        _flex["PITEM_NUM_USERDEF6"] = row["NUM_USERDEF6"];
                        _flex["PITEM_NUM_USERDEF7"] = row["NUM_USERDEF7"];
                        _flex["AM_PACKING"] = D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(row["NUM_USERDEF3"]);
                        _flex["QT_PACKING"] = D.GetDecimal(_flex["QT_SO"]) / D.GetDecimal(_flex["UNIT_GI_FACT"]);
                    }
                    else if (_biz.Get업체별프로세스 == "008")
                    {
                        _flex["PITEM_NUM_USERDEF1"] = row["NUM_USERDEF1"];
                        _flex["PITEM_NUM_USERDEF2"] = row["NUM_USERDEF2"];
                    }

                    if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
                    {
                        if (서버키 == "HKCOS")
                        {
                            _flex["CD_SL"] = ctx창고.CodeValue;
                            _flex["NM_SL"] = ctx창고.CodeName;
                        }
                        else
                        {
                            _flex["CD_SL"] = CD_SL;
                            _flex["NM_SL"] = NM_SL;
                        }
                    }
                    else
                    {
                        _flex["CD_SL"] = row["CD_GISL"];
                        _flex["NM_SL"] = row["NM_GISL"];
                    }

                    if (_biz.Get업체별프로세스 == "003" && D.GetString(_flex["CD_SL"]) == string.Empty)
                    {
                        _flex["CD_SL"] = ctx창고.CodeValue;
                        _flex["NM_SL"] = ctx창고.CodeName;
                    }

                    // 현재고 세팅(2011.09.19)
                    DataRow rowQtInv = dtQtInv.Rows.Find(new object[] { D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]) });

                    if (rowQtInv == null)
                        _flex["SL_QT_INV"] = 0m;
                    else
                        _flex["SL_QT_INV"] = D.GetDecimal(rowQtInv["QT_INV"]);

                    if (!ctx프로젝트.IsEmpty())
                    {
                        _flex["NO_PROJECT"] = ctx프로젝트.CodeValue;
                        _flex["NM_PROJECT"] = ctx프로젝트.CodeName;
                    }

                    string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

                    if (_biz.Get과세변경유무 == "N" || 품목과세구분 == string.Empty)
                    {
                        _flex["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex["RT_VAT"] = cur부가세율.DecimalValue;
                    }
                    else
                    {
                        _flex["TP_VAT"] = 품목과세구분;
                        _flex["RT_VAT"] = D.GetDecimal(row["RT_TAX_SA"]);

                        if (D.GetString(cbo부가세구분.SelectedValue) != D.GetString(_flex["TP_VAT"]))
                            헤더과세일치 = false;
                    }

                    _flex["SEQ_SO"] = 최대차수 + 1;

                    if (납기요구일 != string.Empty)
                    {
                        _flex["DT_DUEDATE"] = 납기요구일;
                        _flex["DT_REQGI"] = c.출하예정일조회(납기요구일, D.GetInt(_flex["LT_GI"]));
                    }

                    if (_구분 == "복사")
                        _flex["STA_SO1"] = _수주상태;

                    if (dt할인율 != null)
                    {
                        DataRow row할인율 = dt할인율.Rows.Find(row["CD_ITEM"]);
                        _flex["RT_DSCNT"] = row할인율 == null ? 0M : row할인율["DC_RATE"];
                    }

                    if (dr품목[0].Table.Columns.Contains("CD_CITEM"))
                    {
                        _flex["QT_SO"] = D.GetDecimal(row["QT_CALC"]);
                        _flex["QT_IM"] = c.관리수량(D.GetDecimal(_flex["QT_SO"]), D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        _flex["UM_SO"] = D.GetDecimal(row["UM_SET"]);
                        _flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UM_SO"]));
                        _flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) * cur환율.DecimalValue);
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"]) + D.GetDecimal(_flex["AM_VAT"]));

                        if (D.GetDecimal(_flex["QT_SO"]) != 0)
                            _flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AMVAT_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        else
                            _flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AMVAT_SO"]));
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        {
                            _biz.예상이익(_flex, _flex.Row);
                        }
                    }
                    else
                    {
                        switch (서버키)
                        {
                            case "KLW":     // 로스트왁스
                                _flex["NUM_USERDEF1"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex["CD_ITEM"]), ctx거래처.CodeValue, "010", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));
                                _flex["NUM_USERDEF2"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex["CD_ITEM"]), ctx거래처.CodeValue, "020", D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));
                                _flex["UM_SO"] = D.GetDecimal(_flex["NUM_USERDEF1"]) + D.GetDecimal(_flex["NUM_USERDEF2"]);
                                break;
                            case "HKCOS":   // 한국화장품
                                한국화장품 hkcos = new 한국화장품();
                                if (Use가용재고)
                                    _flex["QT_USEINV"] = hkcos.Search가용재고(공장, D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]) + "|");
                                decimal 한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, hkcos.단가(공장, D.GetString(_flex["CD_ITEM"]), 기준일자, D.GetString(cbo단가유형.SelectedValue), ctx수주형태.CodeValue, D.GetString(cbo화폐단위.SelectedValue), ctx창고.CodeValue, ctx거래처.CodeValue));
                                _flex["UM_BASE"] = 한국화장품단가;
                                한국화장품단가 = Unit.외화단가(DataDictionaryTypes.SA, 한국화장품단가 - (한국화장품단가 * D.GetDecimal(_flex["RT_DSCNT"]) * 0.01M));
                                _flex["UM_SO"] = Decimal.Round(한국화장품단가, MidpointRounding.AwayFromZero);
                                break;
                            default:
                                if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                                    일반단가적용(_flex.Row, 기준일자);
                                else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                                {
                                    if ((_단가적용형태 == "002" || _단가적용형태 == "003"))
                                        _flex["UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.UmSearch(D.GetString(_flex["CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자)));

                                    특수단가사용시단가계산(_flex.Row);
                                }
                                else if (_biz.Get특수단가적용 == 특수단가적용.조선호텔베이커리단가)
                                {
                                    _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_biz.조선호텔베이커리단가(공장, D.GetString(_flex["CD_ITEM"]), ctx거래처.CodeValue, 기준일자, D.GetString(cbo화폐단위.SelectedValue))));
                                }
                                else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                                {
                                    DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex["CD_ITEM"]);

                                    if (rowUmFixed == null)
                                        일반단가적용(_flex.Row, 기준일자);
                                    else
                                        _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(rowUmFixed["UM_FIXED"]));
                                }
                                break;
                        }
                    }

                    if (서버키 == "KFL") // 할인율이 있으면 단가 올림 (한국화스너전용). 2011.03.03 SJH
                    {
                        if (D.GetDecimal(_flex["RT_DSCNT"]) != 0m)
                            _flex["UM_SO"] = decimal.Ceiling(D.GetDecimal(_flex["UM_SO"]));
                    }
                    else if (서버키 == "KOREAF" || 서버키 == "DZSQL")   //가용재고(현재고 - 미출하수량) 2012.02.24(PIMS:D20120220022)
                    {
                        DataRow rowQtMiGi = dtQtMiGi.Rows.Find(new object[] { D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]) });

                        if (rowQtMiGi == null)
                            _flex["QT_USEINV"] = D.GetDecimal(_flex["SL_QT_INV"]);
                        else
                            _flex["QT_USEINV"] = D.GetDecimal(_flex["SL_QT_INV"]) - D.GetDecimal(rowQtMiGi["QT_MI_GI"]);
                    }

                    if (dt예상이익 != null && dt예상이익.Rows.Count > 0)
                    {
                        DataRow row예상이익 = dt예상이익.Rows.Find(row["CD_ITEM"]);
                        _flex["UM_INV"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row예상이익 == null ? 0M : row예상이익["UM_INV"]));
                        _biz.예상이익(_flex, _flex.Row);
                    }

                    if (_biz.Get업체별프로세스 == "001")
                        _flex["NUM_USERDEF1"] = D.GetDecimal(_flex["AREA"]) == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SO"]) / D.GetDecimal(_flex["AREA"]));

                    if (_biz.Get_WH적용 == "100")
                    {
                        _flex["CD_WH"] = row["CD_WH"];
                        _flex["NM_WH"] = row["NM_WH"];
                    }

                    _flex["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                    _flex["CD_MNGD1"] = _flex[_flex.Row - 1, "CD_MNGD1"];
                    _flex["NM_MNGD1"] = _flex[_flex.Row - 1, "NM_MNGD1"];
                    _flex["CD_MNGD2"] = _flex[_flex.Row - 1, "CD_MNGD2"];
                    _flex["NM_MNGD2"] = _flex[_flex.Row - 1, "NM_MNGD2"];
                    _flex["CD_MNGD3"] = _flex[_flex.Row - 1, "CD_MNGD3"];
                    _flex["NM_MNGD3"] = _flex[_flex.Row - 1, "NM_MNGD3"];
                    _flex["CD_MNGD4"] = _flex[_flex.Row - 1, "CD_MNGD4"];
                    #endregion
                }
            }

            if (!헤더과세일치)
                ShowMessage("VAT구분값은 공장품목에 등록된 과세구분(매출)값으로 세팅되었습니다.");

            _flex.RemoveDummyColumnAll();
            _flex.AddFinished();
            _flex.Col = _flex.Cols.Fixed;
            _flex.Redraw = true;
        }

        #endregion

        #region ▷ 체크사항                    

        bool 일반추가()
        {
            DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_IO_MGMT, '') = ''", "", DataViewRowState.CurrentRows);
            if (dr != null && dr.Length > 0)
                return true;

            return false;
        }

        bool 출하적용건()
        {
            DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_IO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
            if (dr != null && dr.Length > 0)
                return true;
            return false;
        }

        bool 견적적용건()
        {
            if (txt통합견적번호.Text != string.Empty)
                return true;
            else return false;
        }

        bool Chk적용건(bool ismsg)
        {
            if (!Chk견적적용(ismsg) || !Chk출하적용(ismsg) || !Chk사전프로젝트적용(ismsg)) return false;
            return true;
        }

        bool Chk견적적용(bool ismsg)
        {
            if (txt통합견적번호.Text != string.Empty)
            {
                if (ismsg)
                    ShowMessage("견적적용건이 존재합니다.");
                return false;
            }
            return true;
        }

        bool Chk출하적용(bool ismsg)
        {
            DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_IO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
            if (dr != null && dr.Length > 0)
            {
                if (ismsg)
                    ShowMessage("출하적용건이 존재합니다.");
                return false;
            }
            return true;
        }

        bool Chk사전프로젝트적용(bool ismsg)
        {
            if (D.GetString(_header.CurrentRow["FG_TRACK"]) == "I")
            {
                if (ismsg)
                {
                    ShowMessage("BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.");
                }
                return false;
            }
            return true;
        }

        bool Chk확정여부()
        {
            if (!추가모드여부)
            {
                if (서버키 == "HKCOS" || 서버키 == "TOKIMEC")
                {
                    if (!_biz.LineCheck(txt수주번호.Text, D.GetDecimal(_flex["SEQ_SO"]))) return false;
                }
                else
                {
                    if (D.GetString(_flex[_flex.Row, "STA_SO1"]) != string.Empty && D.GetString(_flex[_flex.Row, "STA_SO1"]) != "O")
                    {
                        ShowMessage(메세지.이미수주확정되어수정삭제가불가능합니다);
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region ▷ 단가계산관련                

        #region -> 일반단가적용

        private void 일반단가적용(int idx, string 기준일자)
        {
            if ((_단가적용형태 != "002" && _단가적용형태 != "003")) return; //유형별단가, 거래처별단가 사용 안할때

            decimal 적용단가 = D.GetDecimal(_biz.UmSearch(D.GetString(_flex[idx, "CD_ITEM"]), ctx거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, 기준일자));

            if (Use부가세포함)    //부가세포함단가 사용 시 할인율은 일단 배제하였음(2011.11.11)
            {
                _flex[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, 적용단가);
            }
            else
            {
                if (disCount_YN == "N")
                    _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, 적용단가);
                else if (disCount_YN == "Y")
                {
                    _flex[idx, "UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, 적용단가);
                    _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "UM_BASE"]) - (D.GetDecimal(_flex[idx, "UM_BASE"]) * D.GetDecimal(_flex[idx, "RT_DSCNT"])) / 100);
                }
            }

            if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
            {
                _flex[idx, "NUM_USERDEF1"] = D.GetDecimal(_flex[idx, "UM_SO"]);
            }
        }

        #endregion

        #region -> 특수단가관련

        void 특수단가사용시단가계산(int idx)
        {
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                중량단가계산(idx);
            }
        }

        void 중량단가계산(int idx)
        {
            decimal 중량 = D.GetDecimal(_flex[idx, "WEIGHT"]);
            decimal 중량단가 = D.GetDecimal(_flex[idx, "UM_OPT"]);
            decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, 중량 * 중량단가);

            if (disCount_YN == "Y")
            {
                _flex[idx, "UM_BASE"] = 단가;
                _flex[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, 단가 - (단가 * D.GetDecimal(_flex[idx, "RT_DSCNT"])) / 100);
            }
            else
                _flex[idx, "UM_SO"] = 단가;
        }

        void 특수단가사용시단가계산(ref DataRow dr)
        {
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                중량단가계산(ref dr);
            }
        }

        void 중량단가계산(ref DataRow dr)
        {
            decimal 중량 = D.GetDecimal(dr["WEIGHT"]);
            decimal 중량단가 = D.GetDecimal(dr["UM_OPT"]);
            decimal 단가 = 중량 * 중량단가;
            dr["UM_SO"] = 단가;

            if (disCount_YN == "Y")
                dr["UM_BASE"] = 단가;
        }

        #endregion

        #endregion

        #region ▷ 기타 계산 및 세팅           

        void 소요자재그리드CLEAR()
        {
            if (_flexUser.HasNormalRow)
            {
                try
                {
                    _flexUser.Redraw = false;
                    for (int i = _flexUser.Rows.Count - (_flexUser.Rows.Fixed - 1); i >= _flexUser.Rows.Fixed; i--)
                    {
                        _flexUser.Rows.Remove(i);
                    }
                }
                finally
                {
                    _flexUser.Redraw = true;
                }
            }
        }

        private void 버튼Enabled(bool check)
        {
            ctx수주형태.Enabled = check;
            cbo화폐단위.Enabled = check;
            cur환율.Enabled = check;
            cbo부가세구분.Enabled = check;
            if (수주Config.부가세포함단가사용())
                cbo부가세포함.Enabled = check;
            ctx프로젝트.Enabled = check;
            ctx거래처.Enabled = check;
        }

        private void Authority(bool check)
        {
            ctx거래처.Enabled = check;
            ctx영업그룹.Enabled = check;
            ctx수주형태.Enabled = check;
            ctx프로젝트.Enabled = check;
            //cbo화폐단위.Enabled = check;
            //cur환율.Enabled = check;
            //m_cboFgVat.Enabled = check;
        }

        private void 디엔씨상품적용계산(ref DataRow row, DataRow dr)
        {
            decimal 외화단가 = 0m;
            decimal 원화단가 = 0m;
            decimal 중량단가 = 0m;
            decimal 부가세포함단가 = 0m;

            row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]));
            row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]));
            row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_VAT"]));

            외화단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) == 0 ? 0 : D.GetDecimal(row["AM_SO"]) / D.GetDecimal(row["QT_SO"]));
            원화단가 = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) == 0 ? 0 : D.GetDecimal(row["AM_WONAMT"]) / D.GetDecimal(row["QT_SO"]));
            중량단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["WEIGHT"]) == 0 ? 0 : 원화단가 / D.GetDecimal(row["WEIGHT"]));
            부가세포함단가 = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) == 0 ? D.GetDecimal(dr["AMVAT_SO"]) : D.GetDecimal(dr["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));
            
            row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, 외화단가);
            row["UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, 중량단가);
            row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]));
            row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, 부가세포함단가);
        }

        void 사용자정의셋팅()
        {
            ColsSetting("NUM_USERDEF", "SA_B000069", 1, 6);
            ColsSetting("TXT_USERDEF", "SA_B000112", 1, 2);

            if (_biz.Get업체별프로세스 == "003")        //아카데미과학(PIMS:D20111205120)
                ColsSetting("PITEM_NUM_USERDEF", "MA_B000093", 4, 7);
            else if (_biz.Get업체별프로세스 == "008")   //(주)아이코닉스엔터테인먼트
                ColsSetting("PITEM_NUM_USERDEF", "MA_B000093", 1, 2);

            DataTable dtUserDefine = MA.GetCode("SA_B000110");
            //헤더사용자정의세팅(날짜컨트롤)
            DataRow[] drsDate = dtUserDefine.Select("CD_FLAG1 = 'DATE'");
            for (int i = 1; i <= drsDate.Length; i++)
            {
                string Name = D.GetString(drsDate[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl날짜사용자정의1.Text = Name;
                        lbl날짜사용자정의1.Visible = dtp사용자정의1.Visible = true;
                        break;
                    case 2:
                        lbl날짜사용자정의2.Text = Name;
                        lbl날짜사용자정의2.Visible = dtp사용자정의2.Visible = true;
                        break;
                }
            }
            //헤더사용자정의세팅(텍스트컨트롤)
            DataRow[] drsText = dtUserDefine.Select("CD_FLAG1 = 'TEXT'");
            for (int i = 1; i <= drsText.Length; i++)
            {
                string Name = D.GetString(drsText[i - 1]["NAME"]);

                switch (i)
                {
                    case 1:
                        lbl텍스트사용자정의1.Text = Name;
                        lbl텍스트사용자정의1.Visible = txt사용자정의1.Visible = true;
                        break;
                    case 2:
                        lbl텍스트사용자정의2.Text = Name;
                        lbl텍스트사용자정의2.Visible = txt사용자정의2.Visible = true;
                        break;
                    case 3:
                        lbl텍스트사용자정의3.Text = Name;
                        lbl텍스트사용자정의3.Visible = txt사용자정의3.Visible = true;
                        break;
                }
            }
            //헤더사용자정의세팅(콤보박스컨트롤)
            SetControl str = new SetControl();
            DataRow[] drsCode = dtUserDefine.Select("CD_FLAG1 = 'CODE'");
            DataTable dtCode = null;
            DataTable dtCodeDtl = null;
            for (int i = 1; i <= drsCode.Length; i++)
            {
                string Name = D.GetString(drsCode[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        dtCode = MA.GetCode("SA_B000111", true);
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '201'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의1, dtCodeDtl);
                        lbl콤보사용자정의1.Text = Name;
                        lbl콤보사용자정의1.Visible = cbo사용자정의1.Visible = true;
                        break;
                    case 2:
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '202'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의2, dtCodeDtl);
                        lbl콤보사용자정의2.Text = Name;
                        lbl콤보사용자정의2.Visible = cbo사용자정의2.Visible = true;
                        break;
                    case 3:
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '203'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의3, dtCodeDtl);
                        lbl콤보사용자정의3.Text = Name;
                        lbl콤보사용자정의3.Visible = cbo사용자정의3.Visible = true;
                        break;
                }
            }
            //헤더사용자정의세팅(숫자컨트롤)
            DataRow[] drsNumber = dtUserDefine.Select("CD_FLAG1 = 'NUMBER'");
            for (int i = 1; i <= drsNumber.Length; i++)
            {
                string Name = D.GetString(drsNumber[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl숫자사용자정의1.Text = Name;
                        lbl숫자사용자정의1.Visible = cur사용자정의1.Visible = true;
                        break;
                    case 2:
                        lbl숫자사용자정의2.Text = Name;
                        lbl숫자사용자정의2.Visible = cur사용자정의2.Visible = true;
                        break;
                    case 3:
                        lbl숫자사용자정의3.Text = Name;
                        lbl숫자사용자정의3.Visible = cur사용자정의3.Visible = true;
                        break;
                    case 4:
                        lbl숫자사용자정의4.Text = Name;
                        lbl숫자사용자정의4.Visible = cur사용자정의4.Visible = true;
                        break;
                    case 5:
                        lbl숫자사용자정의5.Text = Name;
                        lbl숫자사용자정의5.Visible = cur사용자정의5.Visible = true;
                        break;
                }
            }

            if (dtUserDefine == null || dtUserDefine.Rows.Count == 0)
                m_tabSo.TabPages.Remove(tabPage4);
        }

        /// <summary>
        /// 그리드 사용자정의컬럼 세팅
        /// </summary>
        /// <param name="colName">사용자정의컬럼 이름</param>
        /// <param name="cdField">세팅 될 코드관리 CD_FIELD 값</param>
        /// <param name="startIdx">사용자정의컬럼 세팅 시작 번호</param>
        /// <param name="endIdx">사용자정의컬럼 세팅 끝 번호</param>
        private void ColsSetting(string colName, string cdField, int startIdx, int endIdx)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                _flex.Cols[colName + D.GetString(i)].Visible = false;
            }
            DataTable dt = MA.GetCode(cdField);
            for (int i = startIdx; (i <= dt.Rows.Count && i <= endIdx); i++)
            {
                string Name = D.GetString(dt.Rows[i - 1]["NAME"]);
                _flex.Cols[colName + D.GetString(i)].Caption = Name;
                _flex.Cols[colName + D.GetString(i)].Visible = true;
            }
        }

        #endregion

        #region ▷ ATP관련                     

        bool ATP체크로직(bool 자동체크)
        {
            DataTable dt = _flex.DataTable.DefaultView.ToTable(true, new string[] { "CD_PLANT" });

            if (dt.Rows.Count > 1)
            {
                ShowMessage("두개 이상의 공장이 지정되어 ATP체크가 불가합니다.");
                return false;
            }

            Duzon.ERPU.MF.Common.ATP ATP = new Duzon.ERPU.MF.Common.ATP();

            string ATP사용유무 = ATP.ATP환경설정_사용유무(LoginInfo.BizAreaCode, D.GetString(_flex["CD_PLANT"]));
            if (ATP사용유무 == "N") return true;

            string 메뉴별ATP처리 = ATP.ATP자동체크_저장로직(D.GetString(_flex["CD_PLANT"]), "100");
            if (메뉴별ATP처리 != "000" && 메뉴별ATP처리 != "001") return true;

            DataRow[] drs = _flex.DataTable.Select("YN_ATP = 'Y'", "", DataViewRowState.CurrentRows);

            if (drs.Length == 0) return true;

            if (drs.Length != _flex.DataTable.DefaultView.ToTable(true, new string[] { "CD_ITEM", "YN_ATP" }).Select("YN_ATP = 'Y'").Length)
            {
                if (ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다." + Environment.NewLine + "계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
                    return false;
            }
            
            string s_Message = string.Empty;

            ATP.Set메뉴ID = PageID;
            ATP.Set전표번호 = txt수주번호.Text;

            bool ATPGood = ATP.ATP_Check(drs, out s_Message);

            if (!ATPGood)
            {
                if (!자동체크)
                {
                    ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
                    return false;
                }
                else
                {
                    if (메뉴별ATP처리 == "000")
                    {
                        if (ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 저장하시겠습니까?", "", s_Message, "QY2") != DialogResult.Yes)
                            return false;
                        else
                            return true;
                    }
                    else if (메뉴별ATP처리 == "001")
                    {
                        ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
                        return false;
                    }

                    return true;
                }
            }
                
            return true;
        }

        #endregion

        #region ▷ 미수채권일자 통제           

        private bool IsAgingCheck()
        {
            Duzon.ERPU.SA.Common.채권연령관리 aging = new Duzon.ERPU.SA.Common.채권연령관리();
            DataTable dtReturn;
            DataRow[] dr = _header.CurrentRow.Table.Select();

            aging.채권연령체크(dr, Duzon.ERPU.SA.Common.AgingCheckPoint.수주확정, out dtReturn);

            if (dtReturn == null || dtReturn.Rows.Count == 0) return true;

            P_SA_CUST_CREDIT_CHECK_SUB dlg = new P_SA_CUST_CREDIT_CHECK_SUB(dtReturn);

            if (dlg.ShowDialog() != DialogResult.OK) return false;

            return true;
        }

        #endregion

        #region ▷ 창고별현재고 인쇄           

        private void 창고별현재고인쇄(string cd_item_multi)
        {
            if (추가모드여부) return;

            ReportHelper rptHelper = new ReportHelper("R_SA_SO_1", "창고별현재고인쇄");

            rptHelper.SetData("NO_SO", txt수주번호.Text);
            rptHelper.SetData("DT_SO", dtp수주일자.Text);

            DataTable dt = _biz.창고별현재고조회(txt수주번호.Text, cd_item_multi);
            rptHelper.SetDataTable(dt);
            rptHelper.Print();
        }

        #endregion

        #region ▷ 사업장 체크                 
        private bool ChkBizarea()
        {
            if (!base.IsChanged()) return true;     //그리드가 수정된게 아니편 체크 필요 없음

            DataTable dt = _flex.DataTable.DefaultView.ToTable(true, new string[] { "CD_PLANT" });

            if (dt.Rows.Count == 1) return true;    //공장이 하나라면 체크 필요 없음

            string multiCdPlant = string.Empty;

            foreach (DataRow row in dt.Rows)
                multiCdPlant += D.GetString(row["CD_PLANT"]) + "|";

            if (_biz.SearchBizarea(multiCdPlant) > 1)
            {
                ShowMessage("공장의 사업장이 동일하지 않으면 저장 할 수 없습니다.");
                return false;
            }

            return true;
        }
        #endregion

        #region ▷ 수주번호 체크               
        private bool VerifyNoSo()
        {
            DataTable dt = _biz.SearchNoSo(txt수주번호.Text);

            if (dt == null || dt.Rows.Count == 0) return true;

            StringBuilder stb = new StringBuilder();
            stb.AppendLine(" - NO_SO : " + D.GetString(dt.Rows[0]["NO_SO"]) + "");
            stb.AppendLine(" - DT_SO : " + D.GetDecimal(dt.Rows[0]["DT_SO"]).ToString("####/##/##") + "");

            ShowDetailMessage("해당 수주번호는 이미 등록된 건 입니다.", stb.ToString());

            return false;
        }
        #endregion

        #region ▷ C/C조회                     
        void CC조회(string 품목군, out string CD_CC, out string NM_CC)
        {
            string 수주유형 = ctx수주형태.CodeValue;
            string 영업그룹 = ctx영업그룹.CodeValue;
            CD_CC = string.Empty;
            NM_CC = string.Empty;

            switch (수주Config.수주라인CC설정유형())
            {
                case 수주관리.수주라인CC설정.영업그룹:
                    수주Config.수주라인CC설정_영업그룹(영업그룹, out CD_CC, out NM_CC);
                    break;
                case 수주관리.수주라인CC설정.수주유형:
                    수주Config.수주라인CC설정_수주유형(수주유형, out CD_CC, out NM_CC);
                    if (CD_CC == string.Empty)
                        수주Config.수주라인CC설정_영업그룹(영업그룹, out CD_CC, out NM_CC);
                    break;
                case 수주관리.수주라인CC설정.품목군:
                    수주Config.수주라인CC설정_품목군(품목군, out CD_CC, out NM_CC);
                    if (CD_CC == string.Empty)
                        수주Config.수주라인CC설정_영업그룹(영업그룹, out CD_CC, out NM_CC);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ▷ Calc금액                    
        private void Calc금액변경(int idx)
        {
            _flex[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(_flex[idx, "UM_SO"]));
            _flex[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AM_SO"]) * cur환율.DecimalValue);
            _flex[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AM_WONAMT"]) * (D.GetDecimal(_flex[idx, "RT_VAT"]) / 100));
        }
        
        private void Calc부가세포함(int idx)
        {
            _flex[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AM_WONAMT"]) + D.GetDecimal(_flex[idx, "AM_VAT"]));

            if (D.GetDecimal(_flex[idx, "QT_SO"]) != 0)
                _flex[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AMVAT_SO"]) / D.GetDecimal(_flex[idx, "QT_SO"]));
            else
                _flex[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[idx, "AMVAT_SO"]));
        }
        #endregion

        #region ▷ 가용재고 CHECK              
        bool Chk가용재고()
        {
            Duzon.ERPU.SA.Custmize.한국화장품 hkcos = new Duzon.ERPU.SA.Custmize.한국화장품();

            DataTable dt가용재고 = null;
            DataTable dtCheck = _flex.DataTable.DefaultView.ToTable().Copy();
            string errMsg = "";

            dtCheck.Columns.Add("TP_GI", typeof(string));

            foreach (DataRow row in dtCheck.Rows)
                row["TP_GI"] = _출하형태;

            bool isChk = hkcos.가용재고체크(dtCheck, out dt가용재고, out errMsg, 모듈.SALE);

            if (!isChk)
            {
                ShowDetailMessage("가용재고를 초과한 내용이 존재합니다.. \n\n" + "[︾] 버튼을 눌러 목록을 확인하세요!! ", errMsg);
                return false;
            }

            return true;
        }
        #endregion

        #region ▷ 결제조건 CHECK              

        private bool Chk결제조건()
        {
            SA.Check chk결제조건 = new SA.Check();
            return chk결제조건.결제조건별수금체크(ctx거래처.CodeValue, dtp수주일자.Text);
        }

        #endregion

        #region ▷ 기존수주 조회               

        private void SearchSo(string noSo)
        {
            DataSet ds = _biz.Search(noSo);

            if (Use루미시트)
            {
                DataTable dt = _biz.SearchDetail(D.GetString(cbo공장.SelectedValue), noSo);

                _flexUser.DataTable.Rows.Clear();

                if (_구분 == "복사")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow row = _flexUser.DataTable.NewRow();
                        Data.DataCopy(dr, row);
                        _flexUser.DataTable.Rows.Add(row);
                    }
                }
                else
                {
                    _flexUser.Binding = dt;
                }
            }
            else if (_biz.Get사양등록사용여부)
            {
                DataTable dt = _biz.SearchOption(noSo);
                _flexUser.Binding = dt;
            }

            _header.SetDataTable(ds.Tables[0]);

            if (_구분 == "복사")
            {
                _헤더수정여부 = true;

                _flex.DataTable.Rows.Clear();

                try
                {
                    _flex.Redraw = false;
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        DataRow row = _flex.DataTable.NewRow();
                        Data.DataCopy(dr, row);
                        if (Use가용재고)
                        {
                            한국화장품 hkcos = new 한국화장품();
                            row["QT_USEINV"] = hkcos.Search가용재고(D.GetString(row["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]) + "|");
                        }
                        _flex.DataTable.Rows.Add(row);
                    }
                }
                finally
                {
                    _flex.Redraw = true;
                }

                _flex.SumRefresh();
                _flex.Row = _flex.Rows.Count - 1;
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();

                _header.JobMode = JobModeEnum.추가후수정;
                _flex.IsDataChanged = true;
                ToolBarDeleteButtonEnabled = false;
                ToolBarSaveButtonEnabled = m_btnDel.Enabled = true;

                if (_biz.수주반품사용여부)
                {
                    btn출하적용.Enabled = true;
                    if (!_flex.Cols["NO_IO_MGMT"].Visible)
                        _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = true;
                }
            }
            else
            {
                _flex.Binding = ds.Tables[1];
                m_btnAppend.Enabled = true;
            }

            DataRow rowSaleGrp = BASIC.GetSaleGrp(D.GetString(_header.CurrentRow["CD_SALEGRP"]));
            so_Price = D.GetString(rowSaleGrp["SO_PRICE"]);

            if (D.GetDecimal(ds.Tables[0].Rows[0]["NO_HST"]) > 0)
            {
                ShowMessage("수주이력이 존재합니다.수정하실 수 없습니다.");
                this.Enabled = false;
                return;
            }
        }

        #endregion

        #region ▷ 수주관리에서 호출           

        private void CallSo(string noSo)
        {
            bool 헤더수정유무 = false;
            string 단가적용형태 = string.Empty;
            DataRow row = _biz.SearchSo(noSo, out 헤더수정유무, out 단가적용형태);

            _구분 = "적용";
            _헤더수정여부 = 헤더수정유무;
            _단가적용형태 = 단가적용형태;
            _수주상태 = D.GetString(row["STA_SO"]);
            _거래구분 = D.GetString(row["TP_BUSI"]);
            _출하형태 = D.GetString(row["TP_GI"]);
            _매출형태 = D.GetString(row["TP_IV"]);
            _의뢰여부 = D.GetString(row["GIR"]);
            _출하여부 = D.GetString(row["GI"]);
            _매출여부 = D.GetString(row["IV"]);
            _수출여부 = D.GetString(row["TRADE"]);

            SearchSo(noSo);
        }

        #endregion

        #region ▷ 품목중복 체크               
        private void CheckItem(string cdPlant, decimal seqSo, DataRow[] drs, int i)
        {
            DataTable dt = new DataView(_flex.DataTable, "CD_ITEM IS NOT NULL AND CD_ITEM <> '' AND SEQ_SO <> " + seqSo + "", "SEQ_SO", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count == 0)
            {
                품목추가(i, drs);
                return;
            }
            dt = dt.DefaultView.ToTable(true, new string[] { "CD_PLANT", "CD_ITEM" });
            품목추가(i, drs);

            bool isItem = true;
            StringBuilder 검증리스트 = new StringBuilder();
            검증리스트.AppendLine("CD_ITEM".PadRight(25, ' ') + "NM_ITEM");
            검증리스트.AppendLine("-".PadRight(50, '-'));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_PLANT"], dt.Columns["CD_ITEM"] };

            foreach (DataRow row in drs)
            {
                DataRow rowItem = dt.Rows.Find(new object[] { cdPlant, D.GetString(row["CD_ITEM"]) });
                if (rowItem != null)
                {
                    isItem = false;
                    검증리스트.AppendLine(D.GetString(row["CD_ITEM"]).PadRight(25, ' ') + D.GetString(row["NM_ITEM"]));
                }
            }

            if (!isItem)
            {
                ShowDetailMessage("중복된 품목이 존재합니다" + Environment.NewLine + "[더보기] 버튼을 눌러 목록을 확인하세요!! ", 검증리스트.ToString());
                return;
            }
        }
        #endregion

        #region ▷ 컨트롤 Visible 세팅         
        private void ControlVisibleSetting()
        {
            사용자정의셋팅();

            switch (서버키)
            {
                case "KFL":     //한국화스너
                    btn업체전용1.Visible = true;
                    btn업체전용1.Text = DD("SET 전개");
                    btn업체전용1.Click += new EventHandler(btnSET전개_KFL_Click);
                    break;
                case "HKCOS":
                    btn업체전용2.Visible = true;
                    btn업체전용2.Text = DD("WEB");
                    btn업체전용2.Click += new EventHandler(btn업체전용2_Click);
                    break;
                case "FORTIS":  //포티스
                case "YPP":     //와이피피
                case "DYPC":    //동양이화
                case "LEDLITEK"://LED라이텍
                    btn전자결재.Visible = true;
                    break;
                case "MACROGEN"://마크로젠
                    lbl관리내역1.Visible = lbl관리내역2.Visible = lbl관리내역3.Visible = lbl관리내역4.Visible = true;
                    bp관리내역1.Visible = bp관리내역2.Visible = bp관리내역3.Visible = txt관리내역4.Visible = true;
                    break;
                default:
                    if (_biz.Get업체별프로세스 == "008")    //아이코닉스
                    {
                        pnl할인율.Visible = cur할인율.Visible = btn할인율적용.Visible = true;
                        txt비고1.Size = new System.Drawing.Size(329, 21);
                    }
                    break;
            }

            if (Use루미시트)
            {
                btn업체전용1.Visible = true;
                btn업체전용1.Enabled = true;
                btn업체전용1.Text = "신규품목생성";
                btn업체전용1.Click += new EventHandler(btn신규품목생성_FAWOO_Click);

                pnl품목.Visible = true;
                flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(234, 234, 234);

                tableLayoutPanel2.Visible = true;
                _flexUser.Binding = _biz.SearchDetail("#%#%", "#%#%");
            }
            else if (_biz.Get사양등록사용여부)
            {
                btn업체전용1.Visible = true;
                btn업체전용1.Text = DD("사양등록");
                btn업체전용1.Click += new EventHandler(btn사양등록_Click);
                _flexUser.Binding = _biz.SearchOption("#%#%");
            }

            if (BASIC.GetMAEXC("여신한도") == "200") //영우일경우
            {
                엑셀업로드.Visible = m_btnAppend.Visible = false;
            }

            btn_apply.Visible = !ConfigSA.SA_EXC.WH정보사용;

            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                cur환율.Enabled = false;

            if (_biz.GetATP사용여부 == "000")
                btn_ATP.Visible = false;

            if (수주Config.부가세포함단가사용())
                cbo부가세포함.Enabled = true;

            if (수주Config.결제조건도움창사용())
                btn결제조건.Visible = true;

            if (_수주번호 != string.Empty)
                CallSo(_수주번호);
        }
        #endregion

        #region ▷ 부가세구분변경              
        private void VAT구분변경()
        {
            if (서버키 != "KORAVL") return;
            if (!_flex.HasNormalRow) return;

            _flex.Redraw = false;
            try
            {
                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    _flex[i, "TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                    _flex[i, "RT_VAT"] = cur부가세율.DecimalValue;
                    _flex[i, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_WONAMT"]) * (D.GetDecimal(_flex[i, "RT_VAT"]) / 100));

                    Calc부가세포함(i);

                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        _biz.예상이익(_flex, i);
                }
            }
            finally
            {
                _flex.Redraw = true;
            }
        }
        #endregion

        #endregion

        #region ▶ 속성            

        #region ▷ 추가모드여부                
        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;

                return false;
            }
        }
        #endregion

        #region ▷ 헤더변경여부                
        private bool 헤더변경여부
        {
            get
            {
                bool bChange = false;

                bChange = _header.GetChanges() != null ? true : false;

                // 헤더가 변경됬지만 추가모드이고 디테일 그리드가 아무 내용이 없으면 변경안된걸로 본다.
                if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !_flex.HasNormalRow)
                    bChange = false;

                return bChange;
            }
        }
        #endregion

        #region ▷ 최대차수                    
        private decimal 최대차수
        {
            get
            {
                decimal MaxCntAmend = 0;

                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if (D.GetDecimal(_flex[i, "SEQ_SO"]) > MaxCntAmend)
                        MaxCntAmend = D.GetDecimal(_flex[i, "SEQ_SO"]);
                }

                return MaxCntAmend;
            }
        }
        #endregion

        #region ▷ 기타 Property               
        string 서버키    { get { return Global.MainFrame.ServerKeyCommon.ToUpper(); } }
        bool Chk거래처   { get { return !Checker.IsEmpty(ctx거래처, DD("거래처")); } }
        bool Chk공장     { get { return !Checker.IsEmpty(cbo공장, DD("공장")); } }
        bool Chk수주유형 { get { return !Checker.IsEmpty(ctx수주형태, m_lblTpSo.Text); } }
        bool Chk화폐단위 { get { return !Checker.IsEmpty(cbo화폐단위, m_lblCdExch.Text); } }
        bool Chk단가유형 { get { return !Checker.IsEmpty(cbo단가유형, m_lblTpPrice.Text); } }
        bool Chk과세구분 { get { return !Checker.IsEmpty(cbo부가세구분, m_lblTpVat.Text); } }
        bool Chk수주일자 { get { return Checker.IsValid(dtp수주일자, true, m_lblDtSo.Text); } }
        bool Chk영업그룹 { get { return !Checker.IsEmpty(ctx영업그룹, DD("영업그룹")); } }
        bool Chk담당자   { get { return !Checker.IsEmpty(ctx담당자, DD("담당자")); } }
        bool Chk반품사유 { get { return !Checker.IsEmpty(cboReason, lab반품사유.Text); } }
        bool 견적사용    { get { if (txt통합견적번호.Text != string.Empty) return true; return false; } }
        bool Use루미시트 { get { if (PageID == "P_SA_Z_FAWOO_SO") return true; return false; } }
        bool Use가용재고 { get { if (서버키 == "HKCOS" && (MA.Login.회사코드 == "1000" || MA.Login.회사코드 == "8000")) return true; return false; } }
        bool Use부가세포함 { get { if (수주Config.부가세포함단가사용() && D.GetString(cbo부가세포함.SelectedValue) == "Y") return true; return false; } }
        #endregion

        #endregion
    }
}