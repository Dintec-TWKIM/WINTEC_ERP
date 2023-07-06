using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
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
using sale;
using Duzon.ERPU.Grant;

namespace cz
{
    public partial class P_CZ_SA_SO : PageBase
    {
        #region ▶ 멤버필드        
        private P_CZ_SA_SO_BIZ _biz = new P_CZ_SA_SO_BIZ();
        private FreeBinding _header = new FreeBinding();
        private CommonFunction _CommFun = new CommonFunction();
        private 수주관리.Config 수주Config = new 수주관리.Config();

        private string _수주상태 = string.Empty;    //수주상태 O(수주등록), R(수주확정)
        private string _구분 = string.Empty;        //조회도움창에서 적용인지 복사인지 
        private string _거래구분 = string.Empty;    //수주유형 선택시 거래구분을 넣어준다.
        private string _출고형태 = string.Empty;    //수주유형 선택시 출고형태를 넣어준다.
        private string _매출형태 = string.Empty;    //수주유형 선택시 매출형태를 넣어준다.
        private string _의뢰여부 = string.Empty;    //수주유형 선택시 의뢰여부를 넣어준다.
        private string _출고여부 = string.Empty;    //수주유형 선택시 출고여부를 넣어준다.
        private string _매출여부 = string.Empty;    //수주유형 선택시 매출여부를 넣어준다.
        private string _수출여부 = string.Empty;    //수주유형 선택시 수출여부를 넣어준다.
        private string _반품여부 = string.Empty;    //수주유형 선택시 수출여부를 넣어준다.
        private bool _헤더수정여부 = true;              //조회도움창에서 라인에 있는 수주상태가 하나라도 'O'가 아니면 false
        private string _단가적용형태 = string.Empty;    //영업그룹을 선택시 단가정보(TP_SALEPRICE)도 가져온다.
        private string _판매단가통제유무 = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        private string _할인율적용여부 = "N";       //할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
        private string _매출자동여부 = string.Empty; //수주유형 선택시 매출자동여부를 넣어준다.
        private string _배송여부 = string.Empty;    //수주유형 선택시 배송여부 디폴트값을 넣어준다
        private string _자동승인여부 = string.Empty;    //수주유형 선택시 자동승인여부 디폴트값을 넣어준다
        private bool _헤더만복사 = false;    //수주조회 도움창에서 복사버튼 클릭 시 헤더만 복사 할지 라인데이터도 복사 할지 결정.
        private string _공장코드 = string.Empty;
        private string _창고코드 = string.Empty;
        private string _창고이름 = string.Empty;
        #endregion

        #region ▶ 초기화          
        public P_CZ_SA_SO()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flex };
            this.DataChanged += new EventHandler(Page_DataChanged);

            this._header = new FreeBinding();
            this._header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

            //영업환경설정 : 수주수량 초과허용:000, 재고단위EDIT여부(2중단위관리):001, 할인율 적용:002
            this._할인율적용여부 = BASIC.GetSAENV("002");
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            
            InitGrid();
            InitEvent();
        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
 
            _flex.SetCol("CD_PLANT", "공장", false);
            _flex.SetCol("CD_ITEM", "품목코드", 120, true);
            _flex.SetCol("NM_ITEM", "품목명", 120, false);
            _flex.SetCol("DC1", "세부내역", 200, true);
            _flex.SetCol("EN_ITEM", "품목명(영)", false);
            _flex.SetCol("STND_ITEM", "규격", false);
            _flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            _flex.SetCol("UNIT_SO", "단위", 65, false);
            _flex.SetCol("DT_DUEDATE", "납기요구일", false);
            _flex.SetCol("DT_REQGI", "출고예정일", false);
            _flex.SetCol("QT_SO", "수량", 60, 17, true, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("CD_EXCH", "화폐단위", false);     //화폐단위추가 2011.08.04 SJH

            // 장은경 : 2010.07.20
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                _flex.SetCol("UNIT_WEIGHT", "중량단위", false);
                _flex.SetCol("WEIGHT", "중량", false);
                _flex.Cols["WEIGHT"].Format = "#,###,###.####";
                _flex.SetCol("UM_OPT", "중량단가", false);
            }

            _flex.SetCol("NUM_USERDEF1", "사용자정의1", false);
            _flex.SetCol("NUM_USERDEF2", "사용자정의2", false);

            _flex.SetCol("UM_SO", "단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (수주Config.부가세포함단가사용())
                _flex.SetCol("UMVAT_SO", "부가세포함단가", false);

            if (_할인율적용여부 == "Y")
            {
                _flex.SetCol("UM_BASE", "기준단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("RT_DSCNT", "할인율", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }
            
            _flex.SetCol("AM_SO", "금액", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);  
            _flex.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_VAT", "부가세", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("AMVAT_SO", "합계금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_IM", "관리단위", false);

            if(Sa_Global.Two_Unit_Mng)
                _flex.SetCol("QT_IM", "관리수량", false);
            else
                _flex.SetCol("QT_IM", "관리수량", false);

            _flex.SetCol("CD_SL", "창고코드", true);
            _flex.SetCol("NM_SL", "창고명", 120, false);
            _flex.SetCol("TP_ITEM", "품목타입", false);
            _flex.SetCol("UNIT_SO_FACT", "수주단위수량", false);
            _flex.SetCol("LT_GI", "출고LT", false);
            _flex.SetCol("GI_PARTNER", "납품처코드", false);
            _flex.SetCol("LN_PARTNER", "납품처명", false);
            _flex.SetCol("NO_PROJECT", "프로젝트코드", 120, true);
            _flex.SetCol("NM_PROJECT", "프로젝트명", false);
            _flex.SetCol("NO_PO_PARTNER", "거래처PO번호", false);
            _flex.SetCol("NO_POLINE_PARTNER", "거래처PO항번", false);
            _flex.SetCol("DC2", "비고", 100, true);
            _flex.SetCol("FG_MODEL", "도면구분", false);
            _flex.SetCol("FG_USE", "수주용도", false);
            _flex.SetCol("NM_MANAGER1", "품목담당자", false);

            if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
            {
                _flex.SetCol("UM_INV", "재고단가", false);
                _flex.SetCol("AM_PROFIT", "예상이익", false);
            }

            if (_biz.Get과세변경유무 == "Y")
            {
                _flex.SetCol("TP_VAT", "VAT구분", false);
                _flex.SetCol("RT_VAT", "VAT율", false);
            }

            if (Sa_Global.SoL_CdCc_ModifyYN == "Y") //영업환경설정의 수주라인-C/C설정수정유무 추가 2010.04.06 NJin (Default Value = "N" 으로 셋팅)
            {
                _flex.SetCol("CD_CC", "코스트 센터", false);
                _flex.SetCol("NM_CC", "코스트센터명", false);
            }

            _flex.SetCol("NO_IO_MGMT", "관련수불번호", false);
            _flex.SetCol("NO_IOLINE_MGMT", "관련수불라인번호", false);

            if (_biz.Get_WH적용 == "100")
            {
                _flex.SetCol("CD_WH", "W/H코드", false);
                _flex.SetCol("NM_WH", "W/H명", false);
            }

            _flex.SetCol("NO_SO_ORIGINAL", "원천수주번호", false);
            _flex.SetCol("SEQ_SO_ORIGINAL", "원천수주항번", false);

            _flex.SetCol("YN_ATP", "ATP적용여부", false);

            _flex.SetCol("SL_QT_INV", "현재고", false);
            _flex.SetCol("QT_USEINV", "가용재고", false);

            _flex.SetCol("NUM_USERDEF3", "사용자정의3", false);
            _flex.SetCol("NUM_USERDEF4", "사용자정의4", false);
            _flex.SetCol("NUM_USERDEF5", "사용자정의5", false);
            _flex.SetCol("NUM_USERDEF6", "사용자정의6", false);

            _flex.SetCol("TXT_USERDEF1", "TEXT사용자정의1", false);
            _flex.SetCol("TXT_USERDEF2", "TEXT사용자정의2", false);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", "UNIT 항번", false);
                _flex.SetCol("CD_UNIT", "UNIT 코드", false);
                _flex.SetCol("NM_UNIT", "UNIT 명", false);
                _flex.SetCol("STND_UNIT", "UNIT 규격", false);
            }

            if (_biz.Get사양등록사용여부)
                _flex.SetCol("YN_OPTION", "옵션여부", false);

            _flex.SetCol("TP_IV", "매출형태", false);
            _flex.SetCol("MAT_ITEM", "재질", false);

            if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
            {
                _flex.SetCol("CD_ITEM_REF", "SET품목", false);
                _flex.SetCol("NM_ITEM_REF", "SET품명", false);
                _flex.SetCol("STND_ITEM_REF", "SET규격", false);
            }
            
            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                _flex.SetCol("YN_PICKING", "배송여부", false);

            _flex.SetCol("FG_USE2", "수주용도2", false);

            _flex.SetCol("CLS_ITEM", "품목계정", false);
            _flex.SetCol("GRP_ITEM", "품목군", false);
            _flex.SetCol("GRP_ITEMNM", "품목군명", false);
            _flex.SetCol("GRP_MFG", "제품군", false);
            _flex.SetCol("NM_GRP_MFG", "제품군명", false);

            _flex.SetCol("CD_USERDEF1", "사용자정의코드1", false);

            if (_biz.Get_WH적용 == "100")
            {
                _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "CD_WH", "NM_WH", "GRP_MFG", "NM_GRP_MFG", "GRP_ITEM", "GRP_ITEMNM" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "CD_WH", "NM_WH", "GRP_MFG", "NM_GRP_MFG", "GRP_ITEM", "GRP_ITEMNM" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
                _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always,
                                      new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" },
                                      new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            }
            else
            {
                _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "GRP_MFG", "NM_GRP_MFG", "GRP_ITEM", "GRP_ITEMNM" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT", "LT_GI", "WEIGHT", "UNIT_WEIGHT", "YN_ATP", "CUR_ATP_DAY", "GRP_MFG", "NM_GRP_MFG", "GRP_ITEM", "GRP_ITEMNM" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
                _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always,
                                  new string[] { "CD_SL", "NM_SL" },
                                  new string[] { "CD_SL", "NM_SL" }, new string[] { "SL_QT_INV", "QT_USEINV", "QT_EXP", "QT_AVA", "NO_LOT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
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

            if (Config.MA_ENV.YN_UNIT == "Y")
                listNotNullCol.Add("SEQ_PROJECT");
            
            listNotNullCol.Add("TP_IV");

            _flex.VerifyNotNull = listNotNullCol.ToArray();

            _flex.VerifyCompare(_flex.Cols["QT_SO"], 0, OperatorEnum.Greater);
            _flex.VerifyCompare(_flex.Cols["QT_IM"], 0, OperatorEnum.Greater);

            //_flex.VerifyCompare(_flex.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
            //_flex.VerifyCompare(_flex.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
            //_flex.VerifyCompare(_flex.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);

            _flex.KeyActionEnter = KeyActionEnum.MoveDown;

            _flex.SettingVersion = "1.0.3.9";
            _flex.EnterKeyAddRow = true;
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);

            _flex.AddMenuSeperator();
            ToolStripMenuItem parent = _flex.AddPopup("관련 현황");
            _flex.AddMenuItem(parent, "현재고조회", Menu_Click);
            _flex.AddMenuItem(parent, "창고별재고인쇄", Menu_Click);

            List<string> list = new List<string>();
            list.Add("UM_SO");
            list.Add("RT_VAT");
            list.Add("NUM_USERDEF1");
            list.Add("NUM_USERDEF2");
            if (_할인율적용여부 == "Y")
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
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                원그리드적용하기();

                this.dtp청구일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                this.dtp청구일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

                ds = this.GetComboData(new string[] { "N;MA_CODEDTL_005;MA_B000040",
                                                      "N;MA_B000005",
                                                      "S;CZ_SA00025",
                                                      "N;YESNO",
                                                      "S;TR_IM00003",
                                                      "S;CZ_SA00005",
                                                      "S;CZ_SA00013",
                                                      "N;MA_PLANT_AUTH",
                                                      "S;MA_B000004",
                                                      "S;SA_B000057",
                                                      "S;SA_B000063",
                                                      "S;MA_B000005",
                                                      "N;MA_AISPOSTH;100",
                                                      "N;MA_B000010",
                                                      "S;SA_B000125" });
                

                DataTable dt부가세 = ds.Tables[0];
                dt부가세.PrimaryKey = new DataColumn[] { dt부가세.Columns["CODE"] };

                SetControl str = new SetControl();
                str.SetCombobox(this.cbo화폐단위, ds.Tables[1]);
                str.SetCombobox(this.cbo청구구분, ds.Tables[2]);
                str.SetCombobox(this.cbo부가세구분, dt부가세);
                str.SetCombobox(this.cbo부가세포함, ds.Tables[3]);
                str.SetCombobox(this.cbo선적조건, ds.Tables[4]);
                str.SetCombobox(this.cboINCOTERMS지역, ds.Tables[5]);
                str.SetCombobox(this.cbo지불조건, ds.Tables[6]);

                if (this._biz.Get과세변경유무 == "Y")
                    this._flex.SetDataMap("TP_VAT", dt부가세.Copy(), "CODE", "NAME");

                this._flex.SetDataMap("CD_PLANT", ds.Tables[7].Copy(), "CODE", "NAME");
                this._flex.SetDataMap("UNIT_SO", ds.Tables[8].Copy(), "CODE", "NAME");
                this._flex.SetDataMap("UNIT_IM", ds.Tables[8].Copy(), "CODE", "NAME");
                this._flex.SetDataMap("FG_USE", ds.Tables[9], "CODE", "NAME"); //수주용도 
                this._flex.SetDataMap("FG_USE2", ds.Tables[10], "CODE", "NAME"); //수주용도2
                this._flex.SetDataMap("CD_EXCH", ds.Tables[11], "CODE", "NAME");
                this._flex.SetDataMap("TP_IV", ds.Tables[12], "CODE", "NAME");
                this._flex.SetDataMap("CLS_ITEM", ds.Tables[13], "CODE", "NAME");

                this._flex.SetDataMap("CD_USERDEF1", ds.Tables[14], "CODE", "NAME"); // 수주라인사용자정의CODE1

                // 프리폼 초기화
                DataSet dsTemp = _biz.Search("#%#%");
                this._header.SetBinding(dsTemp.Tables[0], this.tabControl);

                this._header.ClearAndNewRow();

                this._flex.Binding = dsTemp.Tables[1];

                this.공장창고설정();
                this.화폐단위셋팅();

                this.계산서처리Default셋팅();

                this.ControlVisibleSetting();

                UGrant ugrant = new UGrant();
                ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "AUTO_DEL", this.btn자동삭제);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.btn자동삭제.Click += new EventHandler(this.btn자동삭제_Click);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            
            this.btn프로젝트적용.Click += new EventHandler(this.btn프로젝트적용_Click);

            this.cbo청구구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo화폐단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo부가세구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo부가세포함.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);

            this.ctx매출처.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx담당자.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx영업그룹.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            
            this.ctx매출처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx수주형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx영업그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx호선번호.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

            this.cur공급가액.Validated += new EventHandler(this.cur공급가액_Validated);
            this.cur부가세액.Validated += new EventHandler(this.cur부가세액_Validated);
            
            // 그리드 이벤트 선언
            this._flex.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.AddRow += new EventHandler(this.btn추가_Click);
            this._flex.KeyDown += new KeyEventHandler(this._flex_KeyDown);

            if (_biz.Get사양등록사용여부)
            {
                this._flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            }
        }
        #endregion

        #region ▶ 메인버튼 클릭   
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
                hList.Add(this.ctx매출처, this.lbl매출처); //거래처
                hList.Add(this.ctx영업그룹, this.lbl영업그룹);   //영업그룹

                if (App.SystemEnv.PROJECT사용)
                    hList.Add(this.ctx프로젝트, this.lbl프로젝트);
            }

            hList.Add(this.cbo청구구분, this.lbl청구번호); //청구번호
            hList.Add(this.ctx수주형태, this.lbl수주형태);      //수주유형
            hList.Add(this.cbo화폐단위, this.lbl화폐단위);    //화폐단위
            hList.Add(this.cbo부가세구분, this.lblVAT구분);   //VAT구분
            hList.Add(this.dtp청구일자, this.lbl청구일자);      //청구일자

            return ComFunc.NullCheck(hList);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                P_CZ_SA_SO_SUB dlg = new P_CZ_SA_SO_SUB();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _수주상태 = dlg.수주상태;
                    _구분 = dlg.구분;
                    _헤더수정여부 = dlg.헤더수정유무;
                    _거래구분 = dlg.거래구분;
                    _출고형태 = dlg.출고형태;
                    _매출형태 = dlg.매출형태;
                    _의뢰여부 = dlg.의뢰여부;
                    _출고여부 = dlg.출고여부;
                    _매출여부 = dlg.매출여부;
                    _수출여부 = dlg.수출여부;
                    _단가적용형태 = dlg.단가적용형태;
                    _헤더만복사 = dlg.헤더만복사;

                    SearchSo(dlg.수주번호);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd())
                return false;

            if (!MsgAndSave(PageActionMode.Search))
                return false;

            tabControl.SelectTab(tpg기본정보);

            return true;
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                this._구분 = string.Empty;
                this._헤더수정여부 = true;

                tabControl.Enabled = true;

                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();

                this.계산서처리Default셋팅();
                this.Authority(true);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

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
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!BeforeDelete()) return;

                if (_biz.Delete(txt청구번호.Text))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    _header.ClearAndNewRow();
                    _flex.AcceptChanges();
                    this.OnToolBarAddButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;
            if (!FieldCheck("") || !Chk청구일자 || !Chk담당자) return false;

            if (!ChkBizarea()) return false;

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
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("창고"));
                        return false;
                    }

                    if (D.GetString(dr["NM_CUST_DLV"]) == string.Empty ||
                        D.GetString(dr["CD_ZIP"]) == string.Empty ||
                        D.GetString(dr["ADDR1"]) == string.Empty ||
                        D.GetString(dr["TP_DLV"]) == string.Empty)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("배송정보"));
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
            }
            #endregion

            if (_biz.GetATP사용여부 == "001")
            {
                if (!ATP체크로직(true)) return false;
            }

            if (수주Config.결제조건도움창사용() && !Chk결제조건()) return false;

            return true;
        }
        
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                _flex.Focus();

                if (MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            this._header.CurrentRow["CD_EXCH"] = this.cbo화폐단위.SelectedValue.ToString();

            this.IsInvAmCalc();

            if (!_flex.HasNormalRow && !추가모드여부)
            {
                this.OnToolBarDeleteButtonClicked(null, null);
                return true;
            }

            string NO_SO = string.Empty;

            if (추가모드여부)
            {
                NO_SO = (string)GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", D.GetString(this.cbo청구구분.SelectedValue), dtp청구일자.Text.Substring(0, 4));//수주채번
                txt청구번호.Text = NO_SO;
                _header.CurrentRow["NO_SO"] = NO_SO;
            }
            else
                NO_SO = txt청구번호.Text;

            DataTable dtH = new DataTable();
            DataTable dtL = new DataTable();
            DataTable dtLL = new DataTable();
            DataTable dtLot = new DataTable();

            dtH = _header.GetChanges();
            dtL = _flex.GetChanges();     

            if (_구분 == "복사")
            {
                //복사일경우 저장시점에 수주유형을 읽어와서 수주상태를 체크한다.
                string[] TP_BUSI = new string[3];
                TP_BUSI = _biz.거래구분(ctx수주형태.CodeValue, D.GetString(cbo부가세구분.SelectedValue));

                if (D.GetString(TP_BUSI[2]) == "Y") //자동승인여부 "Y" 자동승인, "N" 자동승인안됨
                    _수주상태 = "R";
                else
                    _수주상태 = "O";
            }

            //수주등록 추적컬럼 : 'M' 수주등록, 'P' 수주등록(거래처), 'H' 수주이력등록, 'W' 수주웹등록, 'ME' 일괄 수주등록, 'MEV' 일괄 수주등록(부가세포함), 'YV' 수주등록(용역)
            if (dtH != null)
                dtH.Rows[0]["FG_TRACK"] = "M";

            if (dtH == null && dtL == null && dtLL == null)
                return true;
            
            if (dtL != null)
            {
                #region -> 여신 & 미수채권일자 체크
                if (_수주상태 == "R")
                {
                    if (_거래구분 == "001") //여신체크(거래구분이 국내인것만)
                    {
                        decimal 금액 = 0;
                        string 여신구분 = BASIC.GetMAEXC("여신한도");

                        if (_biz.GetExcCredit == "300") //환종별 여신사용
                        {
                            if (D.GetString(cbo화폐단위) == "000")
                                금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", ""));
                            else
                                금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_SO)", ""));

                            if (!_biz.CheckCreditExec(ctx매출처.CodeValue, D.GetString(cbo화폐단위.SelectedValue), this.원화계산(금액)))
                                return false;
                        }
                        else
                        {
                            금액 = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", ""));
                            if (!_biz.CheckCredit(ctx매출처.CodeValue, this.원화계산(금액), _의뢰여부, _출고여부, ref _수주상태))
                                return false;
                        }
                    }

                    //미수채권일자 체크
                    if (!IsAgingCheck()) return false;
                }
                #endregion

                #region -> 기타세팅
                foreach (DataRow df in dtL.Rows)
                {
                    if (df.RowState == DataRowState.Deleted) continue;

                    if (df.RowState == DataRowState.Added)
                    {
                        if (D.GetString(df["NO_PROJECT"]) == string.Empty)
                            df["NO_PROJECT"] = ctx프로젝트.CodeValue;

                        df["FG_TRACK"] = "SO";  //배송정보 TRACK 기능 => FG_TRACK : SO(수주등록), M(창고이동, 출고요청등록), R(출고반품의뢰등록)
                        df["STA_SO1"] = _수주상태;
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
                        if (ShowDetailMessage("수주단가가 최저단가보다 적은 품목이 있습니다. 그래도 저장하시겠습니까?", "", strBuilder.ToString(), "QY2") == DialogResult.No)
                            return false;
                    }
                }

                #endregion
            }

            #region -> 자동프로세스(수주-의뢰-출고) 일 경우 로직

            if (_출고여부 == "Y" && _자동승인여부 == "Y")
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
                        dr2["CD_QTIOTP"] = _출고형태;          //출고형태(출고형태)
                        dr2["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, _출고형태 })["NM_QTIOTP"];  //출고형태(출고형태)
                        dr2["DT_IO"] = dtp청구일자.Text;      //출고일자
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
                        dr2["NO_PSO_MGMT"] = txt청구번호.Text;
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

            string[] strArr = new string[] { NO_SO, _수주상태, _거래구분, _출고형태, _매출형태, _의뢰여부, _출고여부, _매출여부, _수출여부, _구분, _반품여부, _매출자동여부, _자동승인여부};
            bool bSuccess = _biz.Save(dtH, dtL, dtLL, dtLot, strArr);
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

            _구분 = "적용";
            Page_DataChanged(null, null);
            tabControl.SelectTab(tpg기본정보);

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (추가모드여부) return;

                DataRow rowPartnerInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, ctx매출처.CodeValue });
                DataRow rowBizareaInfo = _biz.SerchBizarea(D.GetString(_flex[_flex.Rows.Fixed, "CD_PLANT"]));
                
                DataRow row원산지Info = null;
                DataRow row착하통지처Info = null;
                DataRow row수하인Info = null;
                DataRow row은행Info = null;
                DataRow row거래처Info = null;
                DataRow row수출자Info = null;

                if (ctx매출처.CodeValue != "")
                {
                    DataTable 거래처dt = DBHelper.GetDataTable(string.Format("SELECT NO_TEL1, NO_FAX1, CD_EMP_PARTNER, E_MAIL FROM MA_PARTNER WHERE CD_COMPANY = '{0}' AND CD_PARTNER = '{1}'", MA.Login.회사코드, ctx매출처.CodeValue));
                    if (거래처dt != null && 거래처dt.Rows.Count > 0) row거래처Info = 거래처dt.Rows[0];
                }
                
                ReportHelper rptHelper = rptHelper = new ReportHelper("R_SA_SO_RPT_001", "수주서");

                rptHelper.SetData("NO_SO", txt청구번호.Text);                    
                rptHelper.SetData("DT_SO", dtp청구일자.Text);
                rptHelper.SetData("PARTNER", ctx매출처.CodeValue);   //거래처코드
                rptHelper.SetData("CD_PARTNER", ctx매출처.CodeName); //거래처명
                rptHelper.SetData("CD_SALEGRP", ctx영업그룹.CodeName); 
                rptHelper.SetData("NO_KOR", ctx담당자.CodeName); 
                rptHelper.SetData("TP_SO", ctx수주형태.CodeName);
                rptHelper.SetData("CD_EXCH", D.GetString(cbo화폐단위.SelectedValue));
                rptHelper.SetData("NM_CD_EXCH", cbo화폐단위.Text);
                rptHelper.SetData("RT_EXCH", D.GetString(cur환율.DecimalValue)); 
                rptHelper.SetData("TP_PRICE", string.Empty);
                rptHelper.SetData("NO_PROJECT", ctx프로젝트.CodeName);
                rptHelper.SetData("TP_VAT", cbo부가세구분.Text); 
                rptHelper.SetData("RT_VAT", D.GetString(cur부가세율.DecimalValue));
                rptHelper.SetData("FG_VAT", cbo부가세포함.Text); 
                rptHelper.SetData("FG_TAXP", "일괄");
                rptHelper.SetData("DC_RMK", string.Empty);
                rptHelper.SetData("FG_BILL", string.Empty);
                rptHelper.SetData("FG_TRANSPORT", string.Empty);
                rptHelper.SetData("NO_CONTRACT", string.Empty);
                rptHelper.SetData("NO_PO_PARTNER", string.Empty);
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
                rptHelper.SetData("AM_SO_SUM",D.GetString(_flex[1, "AM_SO"]));
                rptHelper.SetData("QT_SO_SUM", D.GetString(_flex[1, "QT_SO"]));

                //해외정보추가(PIMS:D20120521044, 2012.05.29)
                rptHelper.SetData("NM_EXPORT", string.Empty);
                rptHelper.SetData("NM_PRODUCT", string.Empty);
                rptHelper.SetData("COND_TRANS", string.Empty);
                rptHelper.SetData("NM_INSPECT", string.Empty);
                rptHelper.SetData("DESTINATION", string.Empty);
                rptHelper.SetData("NM_COND_PAY", string.Empty);
                rptHelper.SetData("COND_DAYS", string.Empty);
                rptHelper.SetData("DT_EXPIRY", string.Empty);
                rptHelper.SetData("NM_TP_PACKING", string.Empty);
                rptHelper.SetData("NM_TP_TRANSPORT", string.Empty);
                rptHelper.SetData("NM_CD_ORIGIN", string.Empty);
                rptHelper.SetData("PORT_LOADING", string.Empty);
                rptHelper.SetData("PORT_ARRIVER", string.Empty);
                rptHelper.SetData("NM_COND_PRICE", string.Empty);
                rptHelper.SetData("NM_NOTIFY", string.Empty);
                rptHelper.SetData("NM_CONSIGNEE", string.Empty);
                rptHelper.SetData("DC_RMK_TEXT", string.Empty);
                rptHelper.SetData("DC_RMK_TEXT2", string.Empty);
                rptHelper.SetData("DC_RMK1", string.Empty);

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

                //SD바이오센서 추가요청
                if (row원산지Info != null) rptHelper.SetData("NM_SYSDEF_E", D.GetString(row원산지Info["NM_SYSDEF_E"]));   //원산지 영문명
                if (row착하통지처Info != null)
                {
                    rptHelper.SetData("NOTIFY_DC_ADS1_H", D.GetString(row착하통지처Info["DC_ADS1_H"]));   //착하통지처주소
                    rptHelper.SetData("NOTIFY_DC_ADS1_D", D.GetString(row착하통지처Info["DC_ADS1_D"]));   //착하통지처상세주소
                    rptHelper.SetData("NOTIFY_NO_TEL", D.GetString(row착하통지처Info["NO_TEL"]));         //착하통지처전화번호
                }
                if (row수하인Info != null)
                {
                    rptHelper.SetData("CONSIGNEE_DC_ADS1_H", D.GetString(row수하인Info["DC_ADS1_H"]));   //수하인주소
                    rptHelper.SetData("CONSIGNEE_DC_ADS1_D", D.GetString(row수하인Info["DC_ADS1_D"]));   //수하인상세주소
                }
                if (row은행Info != null)
                {
                    rptHelper.SetData("CD_BANK_SO", string.Empty);   //거래은행
                    rptHelper.SetData("NM_BANK_SO", string.Empty);   //거래은행명
                    rptHelper.SetData("BANK_SO_NM_TEXT", D.GetString(row은행Info["NM_TEXT"]));   //거래은행비고
                }
                if (row거래처Info != null)
                {
                    rptHelper.SetData("NO_TEL1", D.GetString(row거래처Info["NO_TEL1"]));    //본사전화번호
                    rptHelper.SetData("NO_FAX1", D.GetString(row거래처Info["NO_FAX1"]));    //본사FAX번호
                    rptHelper.SetData("CD_EMP_PARTNER", D.GetString(row거래처Info["CD_EMP_PARTNER"]));    //전자세금계산서 담당자
                    rptHelper.SetData("E_MAIL", D.GetString(row거래처Info["E_MAIL"]));    //E_MAIL             
                }
                if (row수출자Info != null)
                {
                    rptHelper.SetData("EXPORT_DC_ADS1_H", D.GetString(row수출자Info["DC_ADS1_H"]));   //수출자주소
                    rptHelper.SetData("EXPORT_DC_ADS1_D", D.GetString(row수출자Info["DC_ADS1_D"]));   //수출자상세주소
                }

                DataTable dt = _biz.Search_Print(txt청구번호.Text);

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

        #region ▶ 화면내버튼 클릭
        private void btn자동삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt청구번호.Text)) return;
                if (!this.BeforeDelete()) return;

                if (this._biz.AutoDelete(this.txt청구번호.Text))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    this._header.ClearAndNewRow();
                    this._flex.AcceptChanges();
                    this.OnToolBarAddButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk확정여부()) return;

                if (출고적용건())
                {
                    ShowMessage("출고적용건이 존재합니다.");
                    return;
                }

                if (!FieldCheck("")) return;

                버튼Enabled(false);

                _flex.Rows.Add();

                _flex.Row = _flex.Rows.Count - 1;
                _flex["SEQ_SO"] = 최대차수 + 1;
                _flex["CD_PLANT"] = this._공장코드;
                _flex["GI_PARTNER"] = this.ctx매출처.CodeValue;
                _flex["LN_PARTNER"] = this.ctx매출처.CodeName;
                _flex["CD_SL"] = this._창고코드;
                _flex["NM_SL"] = this._창고이름;
                _flex["QT_SO"] = 0m;
                _flex["UM_SO"] = 0m;
                _flex["AM_SO"] = 0m;
                _flex["AM_VAT"] = 0m;
                _flex["QT_IM"] = 0m;

                if (!ctx프로젝트.IsEmpty())
                {
                    _flex["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
                    _flex["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                }

                if (_할인율적용여부 == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
                {
                    _flex["RT_DSCNT"] = 0m;  //할인율
                    _flex["UM_BASE"] = 0m;   //기준단가
                }

                _flex["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                _flex["RT_VAT"] = cur부가세율.DecimalValue;

                if (_flex.Rows.Count == 3)
                {
                    _flex["DT_DUEDATE"] = this.dtp청구일자.Text;
                    _flex["DT_REQGI"] = this.dtp청구일자.Text;
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
                _flex["TP_IV"] = _매출형태;

                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                    _flex["YN_PICKING"] = _배송여부;

                _flex.Col = _flex.Cols.Fixed;
                _flex.AddFinished();
                _flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (!Chk확정여부()) return;
                
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
                        ctx영업그룹.Enabled = true;
                        ctx매출처.Enabled = true;
                        if (수주Config.부가세포함단가사용())
                            cbo부가세포함.Enabled = true;
                    }
                }

                this.IsInvAmCalc();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn프로젝트적용_Click(object sender, EventArgs e)
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
                    ShowMessage(공통메세지._이가존재하지않습니다, this.DD("적용대상"));
                    return;
                }

                _flex.Redraw = false;
                foreach (DataRow row in drs)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    row["NO_PROJECT"] = ctx프로젝트.CodeValue;
                    row["NM_PROJECT"] = ctx프로젝트.CodeName;
                }
                _flex.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, this.DD("프로젝트적용"));
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

        #region ▶ 그리드 이벤트
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (D.GetDecimal(_header.CurrentRow["NO_HST"]) > 0)
                {
                    e.Cancel = true;
                    return;
                }

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
                        if (D.GetDecimal(_flex["SEQ_PROJECT"]) != 0)
                        {
                            ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        if (_할인율적용여부 == "Y" && _biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
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
                        e.Parameter.P14_CD_PARTNER = ctx매출처.CodeValue;
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
                        e.Parameter.P14_CD_PARTNER = ctx매출처.CodeValue; //거래처코드
                        e.Parameter.P63_CODE3 = ctx매출처.CodeName;       //거래처명
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

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel) return;
                        품목추가(e.Row, helpReturn.Rows);
                        break;

                    case "CD_SL":
                        _flex["SL_QT_INV"] = BASIC.GetQtInv(D.GetString(_flex["CD_PLANT"]), D.GetString(helpReturn.Rows[0]["CD_SL"]), D.GetString(_flex["CD_ITEM"]), dtp청구일자.Text);
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

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
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
                        //_flexH["CD_SL"] = string.Empty;
                        //_flexH["NM_SL"] = string.Empty;
                        _flex["AM_PROFIT"] = 0m;
                        break;

                    case "DT_DUEDATE":
                        if (!D.StringDate.IsValidDate(newValue, false, _flex.Cols[ColName].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (newValue != string.Empty)
                        {
                            if (D.GetDecimal(dtp청구일자.Text) > D.GetDecimal(newValue))
                            {
                                ShowMessage("CZ_@ 은(는) @ 보다 이전일 수 없습니다.", new string[] { this.DD("납기일자"), this.DD("청구일자") });
                                _flex["DT_DUEDATE"] = oldValue;
                                _flex["DT_REQGI"] = oldValue;
                                e.Cancel = true;
                                return;
                            }

                            수주관리.Calc _calc = new 수주관리.Calc();
                            _flex["DT_REQGI"] = _calc.출하예정일조회(newValue, D.GetInt(_flex["LT_GI"]));
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
                            if (D.GetDecimal(dtp청구일자.Text) > D.GetDecimal(newValue))
                            {
                                ShowMessage("CZ_@ 은(는) @ 보다 이전일 수 없습니다.", new string[] { this.DD("출고예정일"), this.DD("청구일자") });
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

                        this.IsInvAmCalc();
                        break;

                    case "TP_VAT":
                        _flex["RT_VAT"] = BASIC.GetTPVAT(newValue);
                        _flex["AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        Calc부가세포함(e.Row);
                        this.IsInvAmCalc();
                        break;

                    case "QT_SO":
                        _flex["UNIT_SO_FACT"] = D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0 ? 1 : _flex["UNIT_SO_FACT"];
                        _flex["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        
                        if (Use부가세포함)
                        {
                            _flex["AMVAT_SO"] = this.원화계산(D.GetDecimal(newValue) * D.GetDecimal(_flex["UMVAT_SO"]));
                            _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))));
                            _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                            _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                            _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        }
                        else
                        {
                            Calc금액변경(e.Row);
                            Calc부가세포함(e.Row);
                        }
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);

                        this.IsInvAmCalc();
                        break;

                    case "UM_SO":
                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);
                        
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);

                        this.IsInvAmCalc();
                        break;

                    case "AM_SO":
                        if (D.GetDecimal(_flex["QT_SO"]) != 0)
                            _flex["UM_SO"] = this.외화계산(D.GetDecimal(newValue) / D.GetDecimal(_flex["QT_SO"]));
                        else
                            _flex["UM_SO"] = 0;

                        _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(newValue) * cur환율.DecimalValue);
                        _flex["AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));

                        Calc부가세포함(e.Row);
                        
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);

                        this.IsInvAmCalc();
                        break;

                    case "RT_DSCNT": //할인율
                        if (D.GetDecimal(_flex["UM_BASE"]) != 0)
                        {
                            _flex["UM_SO"] = this.외화계산(D.GetDecimal(_flex["UM_BASE"]) - (D.GetDecimal(_flex["UM_BASE"]) * D.GetDecimal(newValue)) / 100);
                        }
                        else
                            _flex["UM_SO"] = 0;

                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);
                        
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);

                        this.IsInvAmCalc();
                        break;

                    case "UM_BASE": //기준단가
                        if (D.GetDecimal(_flex["RT_DSCNT"]) != 0)
                            _flex["UM_SO"] = this.외화계산(D.GetDecimal(newValue) - (D.GetDecimal(newValue) * D.GetDecimal(_flex["RT_DSCNT"])) / 100);
                        else
                            _flex["UM_SO"] = 0;

                        Calc금액변경(e.Row);
                        Calc부가세포함(e.Row);
                        
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                            _biz.예상이익(_flex, e.Row);

                        this.IsInvAmCalc();
                        break;
                    case "AM_VAT":
                        decimal AM_VAT_ORI = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        if (Math.Abs(D.GetDecimal(newValue) - AM_VAT_ORI) > AM_VAT_ORI * 0.3m)
                        {
                            ShowMessage("부가세를 원부가세의 (±)30% 초과 수정 할 수 없습니다.");
                            _flex["AM_VAT"] = D.GetDecimal(oldValue);
                            e.Cancel = true;
                            return;
                        }
                        Calc부가세포함(e.Row);
                        this.IsInvAmCalc();
                        break;

                    case "UMVAT_SO":
                        _flex["AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(newValue));
                        _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))));

                        if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                            _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        else
                            
                        _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                        _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        this.IsInvAmCalc();
                        break;

                    case "AM_WONAMT":   //한국AVL일때만 원화금액 KEY-IN이 허용되어 있음
                        _flex["AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        Calc부가세포함(e.Row);
                        this.IsInvAmCalc();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (D.GetDecimal(_header.CurrentRow["NO_HST"]) > 0) return;
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
                        if (_판매단가통제유무 == "Y") return; //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                        if (Use부가세포함) return;

                        P_SA_UM_HISTORY_SUB dlg = new P_SA_UM_HISTORY_SUB(ctx매출처.CodeValue, ctx매출처.CodeName, ctx수주형태.CodeValue, ctx수주형태.CodeName, D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(cbo화폐단위.SelectedValue));
                        if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                            dlg.Set출하기준단가 = false;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (_할인율적용여부 == "N")
                                _flex["UM_SO"] = this.외화계산(dlg.단가);
                            else if (_할인율적용여부 == "Y")
                            {
                                _flex["UM_BASE"] = this.외화계산(dlg.단가);
                                _flex["UM_SO"] = this.외화계산(D.GetDecimal(_flex["UM_BASE"]) - (D.GetDecimal(_flex["UM_BASE"]) * D.GetDecimal(_flex["RT_DSCNT"])) / 100);
                            }
                            Calc금액변경(_flex.Row);
                            Calc부가세포함(_flex.Row);
                        }
                        break;
                    case "UMVAT_SO":
                        if (_판매단가통제유무 == "Y") return;
                        if (!Use부가세포함) return;

                        P_SA_UM_HISTORY_SUB dlr = new P_SA_UM_HISTORY_SUB(ctx매출처.CodeValue, ctx매출처.CodeName, ctx수주형태.CodeValue, ctx수주형태.CodeName, D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(cbo화폐단위.SelectedValue));
                        dlr.Set출하기준단가 = false;
                        dlr.Set부가세포함단가 = true;

                        if (dlr.ShowDialog() == DialogResult.OK)
                        {
                            _flex["UMVAT_SO"] = this.원화계산(dlr.단가);
                            _flex["AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UMVAT_SO"]));
                            _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))));    
                            _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]);
                            _flex["AM_SO"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_WONAMT"]) / cur환율.DecimalValue);
                            _flex["UM_SO"] = D.GetDecimal(_flex["QT_SO"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flex["AM_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        }
                        break;
                    case "NO_SO_ORIGINAL":
                        P_SA_SO_ORIGINAL_SUB dlg2 = new P_SA_SO_ORIGINAL_SUB(ctx매출처.CodeValue, ctx매출처.CodeName, D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["NM_ITEM"]), D.GetString(_flex["CD_PLANT"]));

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
                            //_flexH["CD_SL"] = string.Empty;
                            //_flexH["NM_SL"] = string.Empty;
                            _flex["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * (D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(_flex["UNIT_SO_FACT"])));
                            CC조회(D.GetString(dlg2.원천수주데이터["GRP_ITEM"]), out cdCc, out nmCc);
                            _flex["CD_CC"] = cdCc;
                            _flex["NM_CC"] = nmCc;
                        }
                        break;
                    case "CD_ITEM_REF":
                        if (BASIC.GetMAEXC("SET품 사용유무") != "Y") return;
                        H_SA_SET_OPEN_SUB dlgSet = new H_SA_SET_OPEN_SUB(this._공장코드, dtp청구일자.Text, D.GetString(cbo화폐단위.SelectedValue));
                        if (dlgSet.ShowDialog() != DialogResult.OK) return;
                        Set품목셋팅(_flex.Rows.Count - 1, dlgSet.GetData);
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

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (D.GetDecimal(_header.CurrentRow["NO_HST"]) > 0)
                {
                    e.Cancel = true;
                    return;
                }                

                if (!추가모드여부 && _flex.RowState(e.Row) != DataRowState.Added)
                {
                    if (D.GetString(_flex["STA_SO1"]) != string.Empty && D.GetString(_flex["STA_SO1"]) != "O")
                    {
                        e.Cancel = true;
                        return;
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
                        if (Use부가세포함)
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (_판매단가통제유무 == "Y") //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                        {
                            ShowMessage("영업단가통제된 영업그룹입니다.");
                            e.Cancel = true;
                        }
                        break;

                    case "AM_SO":
                        if (Use부가세포함)   //한국가구
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (_판매단가통제유무 == "Y") //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
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
                        if (_판매단가통제유무 == "Y")
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
                        break;
                }
            }    
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

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

                case "창고별재고인쇄":
                    창고별현재고인쇄(cd_item_multi);
                    break;
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                decimal Key = D.GetDecimal(_flex[e.NewRange.r1, "SEQ_SO"]);
                string Filter = "SEQ_SO = " + Key + "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                string[,] clipboard = Util.GetClipboardValues();

                for (int i = 0; i < clipboard.GetLength(0); i++)
                {
                    for (int j = 0; j < clipboard.GetLength(1); j++)
                    {
                        int row = this._flex.Row + i;
                        int col = this._flex.Col + j;
                        string val = clipboard[i, j];

                        if (col == this._flex.Cols["DC1"].Index && val != string.Empty)
                            this._flex[row, col] = val;
                    }

                    if (this._flex.Row + i == this._flex.Rows.Count - 1) break;
                }
            }
        }
        #endregion

        #region ▶ 컨트롤 이벤트   
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            switch (e.HelpID)
            {
                case HelpID.P_SA_TPSO_SUB:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;
                case HelpID.P_SA_TPPTR_SUB:         // 매출거래처
                    e.HelpParam.P14_CD_PARTNER = ctx매출처.CodeValue;     //납품처 도움창의 거래처컨트롤에 무조건 거래처가 들어가게 변경(2008.12.16)
                    break;
                case HelpID.P_USER:
                    if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P14_CD_PARTNER = ctx매출처.CodeValue; //거래처코드
                        e.HelpParam.P63_CODE3 = ctx매출처.CodeName;       //거래처명
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
                        this.매출처변경시셋팅(e.CodeValue);
                        break;
                    case HelpID.P_USER:
                        if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                        {
                            P_CZ_SA_SO_SUB dialog = new P_CZ_SA_SO_SUB(e.CodeValue);
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string strYN = BASIC.GetMAEXC("수주등록 - 프로젝트 거래처/영업그룹 동기화");
                                if (strYN == "Y")
                                {
                                    ctx매출처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                                    ctx매출처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
                                    ctx영업그룹.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);
                                    ctx영업그룹.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_SALEGRP"]);
                                    _header.CurrentRow["CD_PARTNER"] = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                                    _header.CurrentRow["CD_SALEGRP"] = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);

                                    영업그룹변경시셋팅(ctx영업그룹.CodeValue);
                                }
                            }
                            else
                            {
                                this.ctx프로젝트.CodeValue = string.Empty;
                                this.ctx프로젝트.CodeName = string.Empty;
                                this._header.CurrentRow["NO_PROJECT"] = string.Empty;
                                this._header.CurrentRow["NM_PROJECT"] = string.Empty;
                            }
                        }
                        else if (e.HelpParam.UserHelpID == "H_CZ_MA_HULL_SUB")
                        {
                            this.txt호선명.Text = D.GetString(e.HelpReturn.Rows[0]["NM_VESSEL"]);
                            this.매출처변경시셋팅(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ctx호선번호.Text))
                {
                    this.txt호선명.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if(name == this.cbo화폐단위.Name)
                {
                    this.화폐단위셋팅();
                }
                else if(name == this.cbo부가세구분.Name)
                {
                    this.VAT구분셋팅();
                }
                else if(name == this.cbo부가세포함.Name)
                {
                    if (D.GetString(this.cbo부가세포함.SelectedValue) == "Y")
                    {
                        if (D.GetString(this.cbo화폐단위.SelectedValue) != "000")
                        {
                            if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                            {
                                this.ShowMessage("부가세포함여부는 원화(KRW)인 경우에만 'YES'로 변경 할 수 있습니다.");
                                this.cbo부가세포함.SelectedValue = "N";
                                this._header.CurrentRow["FG_VAT"] = "N";
                            }
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

        #region ▶ 기타 이벤트     
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                {
                    ToolBarSaveButtonEnabled = true;
                }

                if (추가모드여부)
                {
                    btn추가.Enabled = true;
                    ToolBarDeleteButtonEnabled = false;
                }
                else
                    ToolBarDeleteButtonEnabled = true;

                if (!_flex.HasNormalRow) dtp청구일자.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            string name;

            try
            {
                if ((Control)sender == null) return;
                name = ((Control)sender).Name;

                if (name == this.cur환율.Name)
                    this.환율변경();
                else if (name == this.dtp청구일자.Name)
                    this.청구일자변경();

                if (IsChanged())
                    ToolBarSaveButtonEnabled = true;
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
                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    pnl기본정보.Enabled = true;
                    
                    txt청구번호.Enabled = true;
                    txt청구번호.Text = string.Empty;
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
                        pnl기본정보.Enabled = false;
                    else
                        pnl기본정보.Enabled = true;

                    txt청구번호.Enabled = false;
                    ctx수주형태.Enabled = false;
                    cbo부가세구분.Enabled = false;
                    cbo부가세포함.Enabled = false;

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

        private void 계산서처리Default셋팅()
        {
            _header.CurrentRow["FG_TAXP"] = "001";
        }

        private void 수주형태변경시셋팅(string 수주형태)
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
            _출고형태 = D.GetString(row수주형태["TP_GI"]);
            _매출형태 = D.GetString(row수주형태["TP_IV"]);
            _의뢰여부 = D.GetString(row수주형태["GIR"]);
            _출고여부 = D.GetString(row수주형태["GI"]);
            _매출여부 = D.GetString(row수주형태["IV"]);
            _수출여부 = D.GetString(row수주형태["TRADE"]);
            _반품여부 = D.GetString(row수주형태["RET"]);
            _매출자동여부 = D.GetString(row수주형태["IV_AUTO"]);
            _배송여부 = D.GetString(row수주형태["YN_PICKING"]);
            _자동승인여부 = D.GetString(row수주형태["CONF"]);

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

            //반품형태이면 출고적용 버튼을 제어 한당.
            if (D.GetString(row수주형태["RET"]) == "N")
            {
                if (_flex.Cols["NO_IO_MGMT"].Visible)
                    _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = false;
            }
            else
            {
                if (_biz.수주반품사용여부)
                {
                    if (!_flex.Cols["NO_IO_MGMT"].Visible)
                        _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = true;
                }
            }

            this.IsChkTab5Activate();
        }

        private void 영업그룹변경시셋팅(string 영업그룹)
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
            _판매단가통제유무 = D.GetString(row영업그룹["SO_PRICE"]);
        }

        private void 매출처변경시셋팅(string 매출처)
        {
            string query;
            DataTable dt;
            DataRow dr;

            try
            {
                query = @"SELECT MP.CD_PARTNER,
                                 MP.LN_PARTNER,
                                 MP.NO_COMPANY,
                                 DP.CD_EXCH1
                         FROM MA_PARTNER MP WITH(NOLOCK)
                         LEFT JOIN CZ_MA_PARTNER DP WITH(NOLOCK) ON DP.CD_COMPANY = MP.CD_COMPANY AND DP.CD_PARTNER = MP.CD_PARTNER
                         WHERE MP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                        "AND MP.CD_PARTNER = '" + 매출처 + "'";

                dt = Global.MainFrame.FillDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];

                    if (string.IsNullOrEmpty(D.GetString(dr["NO_COMPANY"])))
                    {
                        this.ShowMessage("CZ_선택된 매출처의 사업자등록번호가 존재하지 않습니다.");
                        this.ctx매출처.CodeValue = string.Empty;
                        this.ctx매출처.CodeName = string.Empty;
                        this._header.CurrentRow["CD_PARTNER"] = string.Empty;
                        this._header.CurrentRow["LN_PARTNER"] = string.Empty;
                        return;
                    }

                    this.ctx매출처.CodeValue = D.GetString(dr["CD_PARTNER"]);
                    this.ctx매출처.CodeName = D.GetString(dr["LN_PARTNER"]);
                    this._header.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                    this._header.CurrentRow["LN_PARTNER"] = this.ctx매출처.CodeName;

                    this.cbo화폐단위.SelectedValue = D.GetString(dr["CD_EXCH1"]);
                    this._header.CurrentRow["CD_EXCH"] = D.GetString(dr["CD_EXCH1"]);

                    this.화폐단위셋팅();
                }
                else
                {
                    this.ctx매출처.CodeValue = string.Empty;
                    this.ctx매출처.CodeName = string.Empty;
                    this._header.CurrentRow["CD_PARTNER"] = string.Empty;
                    this._header.CurrentRow["LN_PARTNER"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ▶ 기타 메소드     
        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.이미수주확정되어수정삭제가불가능합니다:
                    return ShowMessage("SA_M000116");
            }

            return DialogResult.None;
        }

        private void 화폐단위셋팅()
        {
            if (cbo화폐단위.SelectedValue == null || D.GetString(cbo화폐단위.SelectedValue) != "000")
            {
                if (D.GetString(cbo부가세포함.SelectedValue) == "Y")
                {
                    if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                    {
                        ShowMessage("부가세포함여부가 'YES' 일 때에는 원화(KRW)만 선택 할 수 있습니다.");
                        cbo화폐단위.SelectedValue = "000";
                        _header.CurrentRow["CD_EXCH"] = "000";
                    }
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

        private void SetExchageApply()
        {
            decimal rt_exch = 0m;

            rt_exch = MA.기준환율적용(this.dtp청구일자.Text, D.GetString(this.cbo화폐단위.SelectedValue));

            if (D.GetString(this.cbo화폐단위.SelectedValue) != "002")
                rt_exch = Decimal.Round(rt_exch, 2, MidpointRounding.AwayFromZero);

            cur환율.DecimalValue = rt_exch == 0m ? 1 : rt_exch;
            _header.CurrentRow["RT_EXCH"] = cur환율.DecimalValue;
        }

        private void 환율변경()
        {
            if (!_flex.HasNormalRow) return;

            _flex.Redraw = false;
            
            try
            {
                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    _flex[i, "AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex[i, "AM_SO"]) * cur환율.DecimalValue);
                    _flex[i, "AM_VAT"] = this.원화계산(D.GetDecimal(_flex[i, "AM_WONAMT"]) * (D.GetDecimal(_flex[i, "RT_VAT"]) / 100));
                    
                    Calc부가세포함(i);

                    if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        _biz.예상이익(_flex, i);

                    _flex[i, "CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                }

                this.IsInvAmCalc();
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        private void 청구일자변경()
        {
            _header.CurrentRow["DT_PROCESS"] = this.dtp청구일자.Text;
            this.dtp매출일자.Text = this.dtp청구일자.Text;

            if (D.GetString(cbo화폐단위.SelectedValue) == "000") return;
            if (MA.기준환율.Option == MA.기준환율옵션.적용안함) return;

            decimal oldRtExch = cur환율.DecimalValue;
            SetExchageApply();
            if (oldRtExch != cur환율.DecimalValue) 환율변경();
        }

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
        }

        protected override bool IsChanged() 
        {
            if (base.IsChanged())   // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;    // 헤더가 변경되었거나
        }

        private void 품목추가(int idx, DataRow[] dr품목)
        {
            bool 헤더과세일치 = true;
            string 납기요구일 = string.Empty;
            string 공장 = string.Empty;
            string 기준일자 = string.Empty;

            if (_flex.Rows.Count == 3 && D.GetString(_flex[idx, "DT_DUEDATE"]) == string.Empty)
                납기요구일 = this.dtp청구일자.Text;
            else if (_flex.Rows.Count >= 3 && D.GetString(_flex[idx, "DT_DUEDATE"]) != string.Empty)
                납기요구일 = D.GetString(_flex[idx, "DT_DUEDATE"]);

            기준일자 = dtp청구일자.Text;

            if (D.GetString(_flex[idx, "CD_PLANT"]) != string.Empty)
                공장 = D.GetString(_flex[idx, "CD_PLANT"]);

            DataTable dt할인율 = null;
            DataTable dt상보DC = null;

            if (_할인율적용여부 == "Y")
            {
                if (_biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
                {
                    dt할인율 = _biz.할인율(공장, ctx매출처.CodeValue, dr품목);
                }
                else if (_biz.Get할인율적용 == 수주관리.할인율적용.한국화장품)
                {
                    한국화장품 hkcos = new 한국화장품();
                    dt할인율 = hkcos.할인율(dr품목, 공장, ctx매출처.CodeValue, 기준일자, "001", ctx수주형태.CodeValue, "VL02");
                }
            }

            DataTable dt예상이익 = null;
            if (공장 != string.Empty && _biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
            {
                dt예상이익 = _biz.예상이익(공장, 기준일자, dr품목);
            }

            //string CD_SL = string.Empty;
            //string NM_SL = string.Empty;

            //if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
            //{
            //    DataRow row거래처 = BASIC.GetPartner(ctx매출처.CodeValue);
            //    CD_SL = D.GetString(row거래처["CD_SL_ISU"]);
            //    if (CD_SL != string.Empty)
            //    {
            //        DataRow row창고 = BASIC.GetSL(공장, CD_SL);

            //        if (row창고 == null)
            //            CD_SL = NM_SL = string.Empty;
            //        else
            //            NM_SL = D.GetString(row창고["NM_SL"]);
            //    }
            //}

            bool First = true;
            _flex.Redraw = false;
            _flex.SetDummyColumnAll();

            수주관리.Calc c = new 수주관리.Calc();
            string cdCc, nmCc;

            string multiItem = Common.MultiString(dr품목, "CD_ITEM", "|");
            
            DataTable dtQtInv = null;
            dtQtInv = BASIC.GetQtInvMulti(multiItem, dtp청구일자.Text);

            dtQtInv.PrimaryKey = new DataColumn[] { dtQtInv.Columns["CD_PLANT"], dtQtInv.Columns["CD_SL"], dtQtInv.Columns["CD_ITEM"] };

            DataTable dtUmFixed = null;
            if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
            {
                dtUmFixed = _biz.SearchUmFixed(ctx매출처.CodeValue, 공장, multiItem);
                dtUmFixed.PrimaryKey = new DataColumn[] { dtUmFixed.Columns["CD_ITEM"] };
            }

            foreach (DataRow row in dr품목)
            {
                if (row.RowState == DataRowState.Deleted) continue;

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
                    _flex[idx, "GI_PARTNER"] = this.ctx매출처.CodeValue;
                    _flex[idx, "LN_PARTNER"] = this.ctx매출처.CodeName;
                    _flex[idx, "WEIGHT"] = row["WEIGHT"];
                    _flex[idx, "UNIT_WEIGHT"] = row["UNIT_WEIGHT"];
                    _flex[idx, "FG_SERNO"] = row["FG_SERNO"];
                    _flex[idx, "YN_ATP"] = row["YN_ATP"];
                    _flex[idx, "CUR_ATP_DAY"] = row["CUR_ATP_DAY"];
                    _flex[idx, "FG_MODEL"] = row["FG_MODEL"];
                    _flex[idx, "NM_MANAGER1"] = row["NM_MANAGER1"];
                    _flex[idx, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"]);
                    _flex[idx, "UNIT_GI_FACT"] = D.GetDecimal(row["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_GI_FACT"]);
                    _flex[idx, "TP_IV"] = _매출형태;

                    _flex[idx, "MAT_ITEM"] = row["MAT_ITEM"];
                    _flex[idx, "CLS_ITEM"] = row["CLS_ITEM"];
                    _flex[idx, "GRP_ITEM"] = row["GRP_ITEM"];
                    _flex[idx, "GRP_ITEMNM"] = row["GRP_ITEMNM"];
                    _flex[idx, "GRP_MFG"] = row["GRP_MFG"];
                    _flex[idx, "NM_GRP_MFG"] = row["NM_GRP_MFG"];
                    _flex[idx, "CLS_L"] = row["CLS_L"];
                    _flex[idx, "CLS_S"] = row["CLS_S"];
                    
                    CC조회(D.GetString(row["GRP_ITEM"]), out cdCc, out nmCc);
                    _flex[idx, "CD_CC"] = cdCc;
                    _flex[idx, "NM_CC"] = nmCc;

                    //if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
                    //{
                    //    _flexH[idx, "CD_SL"] = CD_SL;
                    //    _flexH[idx, "NM_SL"] = NM_SL;
                    //}
                    //else
                    //{
                    //    _flexH[idx, "CD_SL"] = row["CD_GISL"];
                    //    _flexH[idx, "NM_SL"] = row["NM_GISL"];
                    //}

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
                        _flex[idx, "UM_SO"] = this.외화계산(D.GetDecimal(row["UM_SET"]));
                    }
                    else
                    {
                        if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                        {
                            일반단가적용(idx, 기준일자);
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                        {
                            if ((_단가적용형태 == "002" || _단가적용형태 == "003"))
                                _flex[idx, "UM_OPT"] = this.외화계산(BASIC.GetUM(D.GetString(_flex[idx, "CD_PLANT"]), D.GetString(_flex[idx, "CD_ITEM"]), ctx매출처.CodeValue, ctx영업그룹.CodeValue, dtp청구일자.Text, "001", D.GetString(cbo화폐단위.SelectedValue), "SA"));

                            특수단가사용시단가계산(idx);
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                        {
                            DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex[idx, "CD_ITEM"]);

                            if (rowUmFixed == null)
                                일반단가적용(idx, 기준일자);
                            else
                                _flex[idx, "UM_SO"] = this.외화계산(D.GetDecimal(rowUmFixed["UM_FIXED"]));
                        }
                    }

                    if (Use부가세포함)
                    {
                        _flex[idx, "AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(_flex[idx, "UMVAT_SO"]));
                        if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                            _flex[idx, "AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[idx, "RT_VAT"]))), MidpointRounding.AwayFromZero);
                        else
                            _flex[idx, "AM_WONAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, (D.GetDecimal(_flex[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[idx, "RT_VAT"]))))));
                        _flex[idx, "AM_VAT"] = D.GetDecimal(_flex[idx, "AMVAT_SO"]) - D.GetDecimal(_flex[idx, "AM_WONAMT"]);
                        _flex[idx, "AM_SO"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flex[idx, "AM_WONAMT"]) / cur환율.DecimalValue);
                        _flex[idx, "UM_SO"] = D.GetDecimal(_flex[idx, "QT_SO"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flex[idx, "AM_SO"]) / D.GetDecimal(_flex[idx, "QT_SO"]));
                    }
                    else
                    {
                        Calc금액변경(idx);
                        Calc부가세포함(idx);
                    }

                    if (dt예상이익 != null && dt예상이익.Rows.Count > 0)
                    {
                        DataRow row예상이익 = dt예상이익.Rows.Find(row["CD_ITEM"]);
                        _flex[idx, "UM_INV"] = this.원화계산(D.GetDecimal(row예상이익 == null ? 0M : row예상이익["UM_INV"]));
                        _biz.예상이익(_flex, idx);
                    }

                    if (BASIC.GetMAEXC("SET품 사용유무") == "Y" && dr품목[0].Table.Columns.Contains("CD_SET_ITEM"))
                    {
                        _flex[idx, "CD_ITEM_REF"] = row["CD_SET_ITEM"];
                        _flex[idx, "NM_ITEM_REF"] = row["NM_SET_ITEM"];
                        _flex[idx, "STND_ITEM_REF"] = row["STND_ITEM_SET"];

                    }
                    if (BASIC.GetMAEXC("배차사용유무") == "Y")
                        _flex[idx, "YN_PICKING"] = _배송여부;

                    if (dt상보DC != null)
                    {
                        DataRow row상보 = dt상보DC.Rows.Find(row["CD_ITEM"]);

                        _flex[idx, "UM_BASE"] = row상보 == null ? 0M : row상보["UM_BASE"];
                        _flex[idx, "NUM_USERDEF1"] = row상보 == null ? 0M : row상보["NUM_USERDEF1"];
                        _flex[idx, "RT_DSCNT"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                        _flex[idx, "NUM_USERDEF5"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                        _flex[idx, "NUM_USERDEF2"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                        _flex[idx, "UM_SO"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                        _flex[idx, "NUM_USERDEF6"] = D.GetDecimal(_flex[idx, "NUM_USERDEF2"]) - D.GetDecimal(_flex[idx, "NUM_USERDEF4"]);
                    }

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
                    _flex["GI_PARTNER"] = this.ctx매출처.CodeValue;
                    _flex["LN_PARTNER"] = this.ctx매출처.CodeName;
                    _flex["WEIGHT"] = row["WEIGHT"];
                    _flex["UNIT_WEIGHT"] = row["UNIT_WEIGHT"];
                    _flex["FG_SERNO"] = row["FG_SERNO"];
                    _flex["YN_ATP"] = row["YN_ATP"];
                    _flex["CUR_ATP_DAY"] = row["CUR_ATP_DAY"];
                    _flex["FG_MODEL"] = row["FG_MODEL"];
                    _flex["NM_MANAGER1"] = row["NM_MANAGER1"];
                    _flex["FG_USE"] = _flex[_flex.Row - 1, "FG_USE"];
                    _flex["UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_SO_FACT"]);
                    _flex["UNIT_GI_FACT"] = D.GetDecimal(row["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(row["UNIT_GI_FACT"]);
                    _flex["TP_IV"] = _매출형태;                    

                    _flex["MAT_ITEM"] = row["MAT_ITEM"];
                    _flex["CLS_ITEM"] = row["CLS_ITEM"];
                    _flex["GRP_ITEM"] = row["GRP_ITEM"];
                    _flex["GRP_ITEMNM"] = row["GRP_ITEMNM"];
                    _flex["GRP_MFG"] = row["GRP_MFG"];
                    _flex["NM_GRP_MFG"] = row["NM_GRP_MFG"];
                    _flex["CLS_L"] = row["CLS_L"];
                    _flex["CLS_S"] = row["CLS_S"];

                    CC조회(D.GetString(row["GRP_ITEM"]), out cdCc, out nmCc);
                    _flex["CD_CC"] = cdCc;
                    _flex["NM_CC"] = nmCc;

                    //if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
                    //{
                    //    _flexH["CD_SL"] = CD_SL;
                    //    _flexH["NM_SL"] = NM_SL;
                    //}
                    //else
                    //{
                    //    _flexH["CD_SL"] = row["CD_GISL"];
                    //    _flexH["NM_SL"] = row["NM_GISL"];
                    //}

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
                        _flex["AM_SO"] = this.외화계산(D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UM_SO"]));
                        _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AM_SO"]) * cur환율.DecimalValue);
                        _flex["AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (D.GetDecimal(_flex["RT_VAT"]) / 100));
                        _flex["AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) + D.GetDecimal(_flex["AM_VAT"]));

                        if (D.GetDecimal(_flex["QT_SO"]) != 0)
                            _flex["UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        else
                            _flex["UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]));
                        if (_biz.Get예상이익산출적용 == 예상이익산출.재고단가를원가로산출)
                        {
                            _biz.예상이익(_flex, _flex.Row);
                        }
                    }
                    else
                    {
                        if (_biz.Get특수단가적용 == 특수단가적용.NONE)
                        {
                            일반단가적용(_flex.Row, 기준일자);
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
                        {
                            if ((_단가적용형태 == "002" || _단가적용형태 == "003"))
                                _flex["UM_OPT"] = this.외화계산(BASIC.GetUM(D.GetString(_flex["CD_PLANT"]), D.GetString(_flex["CD_ITEM"]), ctx매출처.CodeValue, ctx영업그룹.CodeValue, dtp청구일자.Text, "001", D.GetString(cbo화폐단위.SelectedValue), "SA"));

                            특수단가사용시단가계산(_flex.Row);
                        }
                        else if (_biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
                        {
                            DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex["CD_ITEM"]);

                            if (rowUmFixed == null)
                                일반단가적용(_flex.Row, 기준일자);
                            else
                                _flex["UM_SO"] = this.외화계산(D.GetDecimal(rowUmFixed["UM_FIXED"]));
                        }
                    }

                    if (dt예상이익 != null && dt예상이익.Rows.Count > 0)
                    {
                        DataRow row예상이익 = dt예상이익.Rows.Find(row["CD_ITEM"]);
                        _flex["UM_INV"] = this.원화계산(D.GetDecimal(row예상이익 == null ? 0M : row예상이익["UM_INV"]));
                        _biz.예상이익(_flex, _flex.Row);
                    }

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

                    if (BASIC.GetMAEXC("SET품 사용유무") == "Y" && dr품목[0].Table.Columns.Contains("CD_SET_ITEM"))
                    {
                        _flex["CD_ITEM_REF"] = row["CD_SET_ITEM"];
                        _flex["NM_ITEM_REF"] = row["NM_SET_ITEM"];
                        _flex["STND_ITEM_REF"] = row["STND_ITEM_SET"];
                    }
                    if (BASIC.GetMAEXC("배차사용유무") == "Y")
                        _flex["YN_PICKING"] = _배송여부;

                    if (dt상보DC != null)
                    {
                        DataRow row상보 = dt상보DC.Rows.Find(row["CD_ITEM"]);

                        _flex["UM_BASE"] = row상보 == null ? 0M : row상보["UM_BASE"];
                        _flex["NUM_USERDEF1"] = row상보 == null ? 0M : row상보["NUM_USERDEF1"];
                        _flex["RT_DSCNT"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                        _flex["NUM_USERDEF5"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                        _flex["NUM_USERDEF2"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                        _flex["UM_SO"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                        _flex["NUM_USERDEF6"] = D.GetDecimal(_flex["NUM_USERDEF2"]) - D.GetDecimal(_flex["NUM_USERDEF4"]);
                    }
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

        private bool 출고적용건()
        {
            DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_IO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
            if (dr != null && dr.Length > 0)
                return true;
            return false;
        }

        private bool Chk확정여부()
        {
            if (!추가모드여부)
            {
                if (D.GetString(_flex[_flex.Row, "STA_SO1"]) != string.Empty && D.GetString(_flex[_flex.Row, "STA_SO1"]) != "O")
                {
                    ShowMessage(메세지.이미수주확정되어수정삭제가불가능합니다);
                    return false;
                }
            }

            return true;
        }

        private void 일반단가적용(int idx, string 기준일자)
        {
            if ((_단가적용형태 != "002" && _단가적용형태 != "003")) return; //유형별단가, 거래처별단가 사용 안할때
            
            decimal 적용단가 = BASIC.GetUM(D.GetString(_flex[idx, "CD_PLANT"]), D.GetString(_flex[idx, "CD_ITEM"]), ctx매출처.CodeValue, ctx영업그룹.CodeValue, dtp청구일자.Text, "001", D.GetString(cbo화폐단위.SelectedValue), "SA");

            if (Use부가세포함)    //부가세포함단가 사용 시 할인율은 일단 배제하였음(2011.11.11)
            {
                _flex[idx, "UMVAT_SO"] = this.원화계산(적용단가);
            }
            else
            {
                if (_할인율적용여부 == "N")
                    _flex[idx, "UM_SO"] = this.외화계산(적용단가);
                else if (_할인율적용여부 == "Y")
                {
                    _flex[idx, "UM_BASE"] = this.외화계산(적용단가);
                    _flex[idx, "UM_SO"] = this.외화계산(D.GetDecimal(_flex[idx, "UM_BASE"]) - (D.GetDecimal(_flex[idx, "UM_BASE"]) * D.GetDecimal(_flex[idx, "RT_DSCNT"])) / 100);
                }
            }

            if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
                _flex[idx, "NUM_USERDEF1"] = D.GetDecimal(_flex[idx, "UM_SO"]);
        }

        private void 특수단가사용시단가계산(int idx)
        {
            if (_biz.Get특수단가적용 == 특수단가적용.중량단가)
            {
                중량단가계산(idx);
            }
        }

        private void 중량단가계산(int idx)
        {
            decimal 중량 = D.GetDecimal(_flex[idx, "WEIGHT"]);
            decimal 중량단가 = D.GetDecimal(_flex[idx, "UM_OPT"]);
            decimal 단가 = this.외화계산(중량 * 중량단가);

            if (_할인율적용여부 == "Y")
            {
                _flex[idx, "UM_BASE"] = 단가;
                _flex[idx, "UM_SO"] = this.외화계산(단가 - (단가 * D.GetDecimal(_flex[idx, "RT_DSCNT"])) / 100);
            }
            else
                _flex[idx, "UM_SO"] = 단가;
        }

        private void 버튼Enabled(bool check)
        {
            ctx수주형태.Enabled = check;
            cbo화폐단위.Enabled = check;
            cur환율.Enabled = check;
            cbo부가세구분.Enabled = check;
            if (수주Config.부가세포함단가사용())
                cbo부가세포함.Enabled = check;
            ctx매출처.Enabled = check;
        }

        private void Authority(bool check)
        {
            ctx매출처.Enabled = check;
            ctx영업그룹.Enabled = check;
            ctx수주형태.Enabled = check;
            //cbo화폐단위.Enabled = check;
            //cur환율.Enabled = check;
            //m_cboFgVat.Enabled = check;
        }

        private void 사용자정의셋팅()
        {            
            ColsSetting("NUM_USERDEF", "SA_B000069", 1, 6);
            ColsSetting("TXT_USERDEF", "SA_B000112", 1, 2);
            ColsSetting("CD_USERDEF", "SA_B000125", 1, 1);
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
            DataTable dt = this.GetComboDataCombine("N;" + cdField);
            for (int i = startIdx; (i <= dt.Rows.Count && i <= endIdx); i++)
            {
                string Name = D.GetString(dt.Rows[i - 1]["NAME"]);
                _flex.Cols[colName + D.GetString(i)].Caption = Name;
                _flex.Cols[colName + D.GetString(i)].Visible = true;
            }
        }

        private bool ATP체크로직(bool 자동체크)
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
            ATP.Set전표번호 = txt청구번호.Text;

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

        private void 창고별현재고인쇄(string cd_item_multi)
        {
            if (추가모드여부) return;

            ReportHelper rptHelper = new ReportHelper("R_SA_SO_1", "창고별현재고인쇄");

            rptHelper.SetData("NO_SO", txt청구번호.Text);
            rptHelper.SetData("DT_SO", dtp청구일자.Text);

            DataTable dt = _biz.창고별현재고조회(txt청구번호.Text, cd_item_multi);
            rptHelper.SetDataTable(dt);
            rptHelper.Print();
        }

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

        private void CC조회(string 품목군, out string CD_CC, out string NM_CC)
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

        private void Calc금액변경(int idx)
        {
            _flex[idx, "AM_SO"] = this.외화계산(D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(_flex[idx, "UM_SO"]));
            _flex[idx, "AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex[idx, "AM_SO"]) * cur환율.DecimalValue);
            _flex[idx, "AM_VAT"] = this.원화계산(D.GetDecimal(_flex[idx, "AM_WONAMT"]) * (D.GetDecimal(_flex[idx, "RT_VAT"]) / 100));
        }
        
        private void Calc부가세포함(int idx)
        {
            _flex[idx, "AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex[idx, "AM_WONAMT"]) + D.GetDecimal(_flex[idx, "AM_VAT"]));

            if (D.GetDecimal(_flex[idx, "QT_SO"]) != 0)
                _flex[idx, "UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex[idx, "AMVAT_SO"]) / D.GetDecimal(_flex[idx, "QT_SO"]));
            else
                _flex[idx, "UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex[idx, "AMVAT_SO"]));
        }

        private bool Chk결제조건()
        {
            SA.Check chk결제조건 = new SA.Check();
            return chk결제조건.결제조건별수금체크(ctx매출처.CodeValue, dtp청구일자.Text);
        }

        private void SearchSo(string noSo)
        {
            DataSet ds = _biz.Search(noSo);

            _header.SetDataTable(ds.Tables[0]);

            if (_구분 == "복사")
            {
                _헤더수정여부 = true;

                _flex.DataTable.Rows.Clear();
                if (!_헤더만복사)
                {
                    try
                    {
                        _flex.Redraw = false;
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            DataRow row = _flex.DataTable.NewRow();
                            Data.DataCopy(dr, row);
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
                    _flex.IsDataChanged = true;

                }
                _header.JobMode = JobModeEnum.추가후수정;
                ToolBarDeleteButtonEnabled = false;
                ToolBarSaveButtonEnabled = btn삭제.Enabled = true;

                if (_biz.수주반품사용여부)
                {
                    if (!_flex.Cols["NO_IO_MGMT"].Visible)
                        _flex.Cols["NO_IO_MGMT"].Visible = _flex.Cols["NO_IOLINE_MGMT"].Visible = true;
                }
            }
            else
            {
                _flex.Binding = ds.Tables[1];
                btn추가.Enabled = true;
            }

            DataRow row수주형태 = BASIC.GetTPSO(D.GetString(ds.Tables[0].Rows[0]["TP_SO"]));
            _매출자동여부 = D.GetString(row수주형태["IV_AUTO"]);
            _자동승인여부 = D.GetString(row수주형태["CONF"]);
            this.IsChkTab5Activate();
            //this.IsInvAmCalc();
            
            DataRow rowSaleGrp = BASIC.GetSaleGrp(D.GetString(_header.CurrentRow["CD_SALEGRP"]));
            _판매단가통제유무 = D.GetString(rowSaleGrp["SO_PRICE"]);

            if (D.GetDecimal(ds.Tables[0].Rows[0]["NO_HST"]) > 0)
            {
                ShowMessage("수주이력이 존재합니다.수정하실 수 없습니다.");
                tabControl.Enabled = false;
                
                return;
            }
        }

        private void ControlVisibleSetting()
        {
            사용자정의셋팅();

            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                cur환율.Enabled = false;

            if (수주Config.부가세포함단가사용())
                cbo부가세포함.Enabled = true;
        }

        private void IsInvAmCalc()
        {
            if (!_flex.HasNormalRow) return;

            if (_biz.Get과세변경유무 == "Y" || _매출자동여부 == "N") return;

            decimal Am_Iv = 0;
            Am_Iv = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT)", ""));
            cur공급가액.DecimalValue = Am_Iv;
            _header.CurrentRow["AM_IV"] = Am_Iv;

            decimal Am_So = 0;
            Am_So = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_SO)", ""));
            cur외화금액.DecimalValue = Am_So;
            _header.CurrentRow["AM_IV_EX"] = Am_So;

            decimal Am_Iv_Vat = 0;
            Am_Iv_Vat = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_VAT)", ""));
            cur부가세액.DecimalValue = Am_Iv_Vat;
            _header.CurrentRow["AM_IV_VAT"] = Am_Iv_Vat;

        }

        private void cur공급가액_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (_biz.Get과세변경유무 == "Y" || _매출자동여부 == "N") return;

                decimal Am_Iv = 0;
                Am_Iv = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_WONAMT)", ""));

                if (cur공급가액.DecimalValue != Am_Iv)
                    _flex[_flex.Rows.Count - 1, "AM_WONAMT"] = D.GetDecimal(_flex[_flex.Rows.Count - 1, "AM_WONAMT"]) + (cur공급가액.DecimalValue - Am_Iv);

                Calc부가세포함(_flex.Rows.Count - 1);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void cur부가세액_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;
                if (_biz.Get과세변경유무 == "Y" || _매출자동여부 == "N") return;

                decimal Am_Vat = 0;
                Am_Vat = D.GetDecimal(_flex.DataTable.Compute("SUM(AM_VAT)", ""));

                if (cur부가세액.DecimalValue != Am_Vat)
                    _flex[_flex.Rows.Count - 1, "AM_VAT"] = D.GetDecimal(_flex[_flex.Rows.Count - 1, "AM_VAT"]) + (cur부가세액.DecimalValue - Am_Vat);

                Calc부가세포함(_flex.Rows.Count - 1);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void IsChkTab5Activate()
        {
            //if (_biz.Get과세변경유무 == "N" && _매출자동여부 == "Y")
            //{
            //    if (!tabControl.TabPages.ContainsKey(this.tpg매출정보.Name))
            //        tabControl.TabPages.Add(this.tpg매출정보);

            //    _flex.Cols["TP_IV"].AllowEditing = true;
            //    _flex.Cols["TP_IV"].Visible = true;
            //}
            //else
            //{
            //    tabControl.TabPages.Remove(this.tpg매출정보);

            //    _flex.Cols["TP_IV"].AllowEditing = false;
            //    _flex.Cols["TP_IV"].Visible = false;
            //}
        }

        private void 원그리드적용하기()
        {
            System.Drawing.Size s1 = pnl기본정보.Size;

            pnl기본정보.UseCustomLayout = true;
            pnl기본정보.IsSearchControl = false;   //입력 전용

            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl5.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl9.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;

            pnl기본정보.InitCustomLayout();
            return;
        }

        private void Set품목셋팅(int idx, DataRow[] drSet품목S)
        {
            try
            {
                bool First = true;
                수주관리.Calc c = new 수주관리.Calc();
                string cdCc, nmCc;

                _flex.Redraw = false;
                _flex.SetDummyColumnAll();

                DataTable dt상보DC = null;

                string 공장 = string.Empty;

                if (D.GetString(_flex[idx, "CD_PLANT"]) != string.Empty)
                    공장 = D.GetString(_flex[idx, "CD_PLANT"]);

                foreach (DataRow rowSet품목 in drSet품목S)
                {
                    if (First)
                    {
                        _flex[idx, "SEQ_SO"] = 최대차수 + 1;
                        _flex[idx, "GI_PARTNER"] = this.ctx매출처.CodeValue;
                        _flex[idx, "LN_PARTNER"] = this.ctx매출처.CodeName;

                        if (_할인율적용여부 == "Y")
                        {
                            _flex[idx, "RT_DSCNT"] = 0;  //할인율
                            _flex[idx, "UM_BASE"] = 0;   //기준단가
                        }

                        _flex[idx, "TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex[idx, "RT_VAT"] = cur부가세율.DecimalValue;

                        CC조회(D.GetString(rowSet품목["GRP_ITEM"]), out cdCc, out nmCc);
                        _flex[idx, "CD_CC"] = cdCc;
                        _flex[idx, "NM_CC"] = nmCc;

                        _flex[idx, "DT_DUEDATE"] = this.dtp청구일자.Text;
                        _flex[idx, "DT_REQGI"] = this.dtp청구일자.Text;
                        _flex[idx, "STA_SO1"] = _수주상태;
                        _flex[idx, "CD_PLANT"] = rowSet품목["CD_PLANT"];
                        _flex[idx, "CD_ITEM"] = rowSet품목["CD_ITEM"];
                        _flex[idx, "NM_ITEM"] = rowSet품목["NM_ITEM"];
                        _flex[idx, "STND_ITEM"] = rowSet품목["STND_ITEM"];
                        _flex[idx, "UNIT_SO"] = rowSet품목["UNIT_SO"];
                        _flex[idx, "TP_ITEM"] = rowSet품목["TP_ITEM"];
                        _flex[idx, "TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex[idx, "RT_VAT"] = cur부가세율.DecimalValue;
                        _flex[idx, "QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(rowSet품목["QT_CALC"]));
                        _flex[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, c.관리수량(D.GetDecimal(_flex["QT_SO"]), D.GetDecimal(_flex["UNIT_SO_FACT"])));
                        _flex[idx, "UM_SO"] = this.외화계산(D.GetDecimal(rowSet품목["UM_SET"]));
                        _flex[idx, "AM_SO"] = this.외화계산(D.GetDecimal(_flex[idx, "QT_SO"]) * D.GetDecimal(_flex[idx, "UM_SO"]));
                        _flex[idx, "AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AM_SO"]) * cur환율.DecimalValue);
                        _flex[idx, "AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (cur부가세율.DecimalValue / 100m));
                        _flex[idx, "AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                        _flex[idx, "UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        _flex[idx, "UNIT_SO_FACT"] = D.GetDecimal(rowSet품목["UNIT_SO_FACT"]) == 0 ? 1 : rowSet품목["UNIT_SO_FACT"];
                        _flex[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        _flex[idx, "UNIT_IM"] = rowSet품목["UNIT_IM"];
                        _flex[idx, "EN_ITEM"] = rowSet품목["EN_ITEM"];
                        _flex[idx, "STND_DETAIL_ITEM"] = rowSet품목["STND_DETAIL_ITEM"];
                        _flex[idx, "GRP_MFG"] = rowSet품목["GRP_MFG"];
                        _flex[idx, "LT_GI"] = D.GetDecimal(rowSet품목["LT_GI"]);
                        _flex[idx, "WEIGHT"] = rowSet품목["WEIGHT"];
                        _flex[idx, "UNIT_WEIGHT"] = rowSet품목["UNIT_WEIGHT"];
                        _flex[idx, "FG_SERNO"] = rowSet품목["FG_SERNO"];
                        _flex[idx, "YN_ATP"] = rowSet품목["YN_ATP"];
                        _flex[idx, "CUR_ATP_DAY"] = rowSet품목["CUR_ATP_DAY"];
                        _flex[idx, "FG_MODEL"] = rowSet품목["FG_MODEL"];
                        _flex[idx, "UNIT_GI_FACT"] = D.GetDecimal(rowSet품목["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(rowSet품목["UNIT_GI_FACT"]);
                        _flex[idx, "CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                        _flex[idx, "TP_IV"] = "002";
                        _flex[idx, "MAT_ITEM"] = rowSet품목["MAT_ITEM"];
                        _flex[idx, "CD_ITEM_REF"] = rowSet품목["CD_SET_ITEM"];
                        _flex[idx, "NM_ITEM_REF"] = rowSet품목["NM_SET_ITEM"];
                        _flex[idx, "STND_ITEM_REF"] = rowSet품목["STND_ITEM_SET"];
                        _flex[idx, "YN_PICKING"] = _배송여부;

                        if (dt상보DC != null)
                        {
                            DataRow row상보 = dt상보DC.Rows.Find(rowSet품목["CD_ITEM"]);

                            _flex[idx, "UM_BASE"] = row상보 == null ? 0M : row상보["UM_BASE"];
                            _flex[idx, "NUM_USERDEF1"] = row상보 == null ? 0M : row상보["NUM_USERDEF1"];
                            _flex[idx, "RT_DSCNT"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                            _flex[idx, "NUM_USERDEF5"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                            _flex[idx, "NUM_USERDEF2"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                            _flex[idx, "UM_SO"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                            _flex[idx, "NUM_USERDEF6"] = D.GetDecimal(_flex[idx, "NUM_USERDEF2"]) - D.GetDecimal(_flex[idx, "NUM_USERDEF4"]);
                            Calc금액변경(idx);
                            Calc부가세포함(idx);
                        }


                        First = false;
                    }
                    else
                    {
                        _flex.Rows.Add();
                        _flex.Row = _flex.Rows.Count - 1;
                        _flex["SEQ_SO"] = 최대차수 + 1;
                        _flex["GI_PARTNER"] = this.ctx매출처.CodeValue;
                        _flex["LN_PARTNER"] = this.ctx매출처.CodeName; if (_할인율적용여부 == "Y")
                        {
                            _flex["RT_DSCNT"] = 0;  //할인율
                            _flex["UM_BASE"] = 0;   //기준단가
                        }
                        _flex["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex["RT_VAT"] = cur부가세율.DecimalValue;

                        CC조회(D.GetString(rowSet품목["GRP_ITEM"]), out cdCc, out nmCc);
                        _flex["CD_CC"] = cdCc;
                        _flex["NM_CC"] = nmCc;

                        _flex["DT_DUEDATE"] = this.dtp청구일자.Text;
                        _flex["DT_REQGI"] = this.dtp청구일자.Text;
                        _flex["STA_SO1"] = _수주상태;
                        _flex["CD_PLANT"] = rowSet품목["CD_PLANT"];
                        _flex["CD_ITEM"] = rowSet품목["CD_ITEM"];
                        _flex["NM_ITEM"] = rowSet품목["NM_ITEM"];
                        _flex["STND_ITEM"] = rowSet품목["STND_ITEM"];
                        _flex["UNIT_SO"] = rowSet품목["UNIT_SO"];
                        _flex["TP_ITEM"] = rowSet품목["TP_ITEM"];
                        _flex["TP_VAT"] = D.GetString(cbo부가세구분.SelectedValue);
                        _flex["RT_VAT"] = cur부가세율.DecimalValue;
                        _flex["QT_SO"] = D.GetDecimal(rowSet품목["QT_CALC"]);
                        _flex["QT_IM"] = c.관리수량(D.GetDecimal(_flex["QT_SO"]), D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        _flex["UM_SO"] = D.GetDecimal(rowSet품목["UM_SET"]);
                        _flex["AM_SO"] = this.외화계산(D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UM_SO"]));
                        _flex["AM_WONAMT"] = this.원화계산(D.GetDecimal(_flex["AM_SO"]) * cur환율.DecimalValue);
                        _flex["AM_VAT"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) * (cur부가세율.DecimalValue / 100m));
                        _flex["AMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AM_WONAMT"]) + D.GetDecimal(_flex["AM_VAT"]));
                        _flex["UMVAT_SO"] = this.원화계산(D.GetDecimal(_flex["AMVAT_SO"]) / D.GetDecimal(_flex["QT_SO"]));
                        _flex["UNIT_SO_FACT"] = D.GetDecimal(rowSet품목["UNIT_SO_FACT"]) == 0 ? 1 : rowSet품목["UNIT_SO_FACT"];
                        _flex["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UNIT_SO_FACT"]));
                        _flex["UNIT_IM"] = rowSet품목["UNIT_IM"];
                        _flex["EN_ITEM"] = rowSet품목["EN_ITEM"];
                        _flex["STND_DETAIL_ITEM"] = rowSet품목["STND_DETAIL_ITEM"];
                        _flex["GRP_MFG"] = rowSet품목["GRP_MFG"];
                        _flex["LT_GI"] = D.GetDecimal(rowSet품목["LT_GI"]);
                        _flex["WEIGHT"] = rowSet품목["WEIGHT"];
                        _flex["UNIT_WEIGHT"] = rowSet품목["UNIT_WEIGHT"];
                        _flex["FG_SERNO"] = rowSet품목["FG_SERNO"];
                        _flex["YN_ATP"] = rowSet품목["YN_ATP"];
                        _flex["CUR_ATP_DAY"] = rowSet품목["CUR_ATP_DAY"];
                        _flex["FG_MODEL"] = rowSet품목["FG_MODEL"];
                        _flex["UNIT_GI_FACT"] = D.GetDecimal(rowSet품목["UNIT_GI_FACT"]) == 0m ? 1m : D.GetDecimal(rowSet품목["UNIT_GI_FACT"]);
                        _flex["CD_EXCH"] = D.GetString(cbo화폐단위.SelectedValue);
                        _flex["TP_IV"] = "002";
                        _flex["MAT_ITEM"] = rowSet품목["MAT_ITEM"];
                        _flex["CD_ITEM_REF"] = rowSet품목["CD_SET_ITEM"];
                        _flex["NM_ITEM_REF"] = rowSet품목["NM_SET_ITEM"];
                        _flex["STND_ITEM_REF"] = rowSet품목["STND_ITEM_SET"];
                        _flex["YN_PICKING"] = _배송여부;

                        if (dt상보DC != null)
                        {
                            DataRow row상보 = dt상보DC.Rows.Find(rowSet품목["CD_ITEM"]);

                            _flex["UM_BASE"] = row상보 == null ? 0M : row상보["UM_BASE"];
                            _flex["NUM_USERDEF1"] = row상보 == null ? 0M : row상보["NUM_USERDEF1"];
                            _flex["RT_DSCNT"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                            _flex["NUM_USERDEF5"] = row상보 == null ? 0M : row상보["RT_DSCNT"];
                            _flex["NUM_USERDEF2"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                            _flex["UM_SO"] = row상보 == null ? 0M : row상보["NUM_USERDEF2"];
                            _flex["NUM_USERDEF6"] = D.GetDecimal(_flex["NUM_USERDEF2"]) - D.GetDecimal(_flex["NUM_USERDEF4"]);
                            Calc금액변경(_flex.Row);
                            Calc부가세포함(_flex.Row);
                        }
                    }
                }
                
                _flex.RemoveDummyColumnAll();
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
                _flex.Redraw = true;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }  

        private void 공장창고설정()
        {
            try
            {
                this._공장코드 = Global.MainFrame.LoginInfo.CdPlant;
                this._창고코드 = "VL01";
                this._창고이름 = D.GetString(BASIC.GetSL(this._공장코드, this._창고코드)["NM_SL"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
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
        #endregion

        #region ▶ 속성
        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;

                return false;
            }
        }

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

        private bool Chk거래처 { get { return !Checker.IsEmpty(ctx매출처, DD("거래처")); } }
        private bool Chk수주유형 { get { return !Checker.IsEmpty(ctx수주형태, lbl수주형태.Text); } }
        private bool Chk화폐단위 { get { return !Checker.IsEmpty(cbo화폐단위, lbl화폐단위.Text); } }
        private bool Chk과세구분 { get { return !Checker.IsEmpty(cbo부가세구분, lblVAT구분.Text); } }
        private bool Chk청구일자 { get { return Checker.IsValid(dtp청구일자, true, lbl청구일자.Text); } }
        private bool Chk영업그룹 { get { return !Checker.IsEmpty(ctx영업그룹, DD("영업그룹")); } }
        private bool Chk담당자 { get { return !Checker.IsEmpty(ctx담당자, DD("담당자")); } }
        private bool Use부가세포함 { get { if (수주Config.부가세포함단가사용() && D.GetString(cbo부가세포함.SelectedValue) == "Y") return true; return false; } }
        #endregion
    }
}