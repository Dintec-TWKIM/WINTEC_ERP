using System;
using System.Collections;
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

namespace sale
{
    // **************************************
    // 작   성   자 : NJin
    // 재 작  성 일 : 2008-08-21
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 수주관리
    // 페 이 지  명 : 일괄수주등록
    // 프로젝트  명 : P_SA_PROJECT
    // **************************************
    public partial class P_SA_SO_BATCH_VAT : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언 

        private enum ExcelType { NONE, 부가세포함단가, 부가세포함금액 };

        CommonFunction _CommFun = new CommonFunction();
        private P_SA_SO_BATCH_VAT_BIZ _biz = new P_SA_SO_BATCH_VAT_BIZ();

        string soStatus = "O";           //수주상태 O(수주등록), R(수주확정) : 조회도움창에서 라인에 있는 수주상태가 하나라도 'O'가 아니면 수주확정
        string tp_Gi = "";               //수주유형 선택시 출하형태를 넣어준다.
        string tp_Busi = "";             //수주유형 선택시 거래구분을 넣어준다.
        string tp_Iv = "";               //수주유형 선택시 매출형태를 넣어준다.
        string _의뢰여부 = string.Empty;        //수주유형 선택시 의뢰여부를 넣어준다.
        string _출하여부 = string.Empty;        //수주유형 선택시 출하여부를 넣어준다.
        string _매출여부 = string.Empty;        //수주유형 선택시 매출여부를 넣어준다.
        string _반품여부 = string.Empty;        //수주유형 선택시 반품여부를 넣어준다.(2011.06.23)
        string trade = "";               //수주유형 선택시 수출여부를 넣어준다.
        string tp_SalePrice = "";        //영업그룹을 선택시 단가적용형태
        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        string ItemHelpName = "P_MA_PITEM_SUB1"; //멀티도움창인지 단일도움창인지를 체크하여 그리드의 도움창에서 FLAG 역할을 한다.

        string Partner = ""; //프로젝트가 셋팅되면 전역변수로 거래처를 가지고 있다가 저장시점에 체크해서 일괄반영또는 error 처리 한다.

        //엑셀로 업로드 하지 않으면 그리드에 바인딩이 일어나지 않아 초기 스키마가 없어 
        //그리드에 값이 매핑이 되지 않는다. 따라서 헤더라인의 전체 스키마를 가지고 있어 
        //행추가로 저장하려고 하는 로직일때 사용한다.
        DataTable defaultSchema = null;

        //영업환경설정  
        /*할인율 적용 여부에 대한 부연 설명*/
        /*할인율 적용은 기존 거래처별 할인이나 유형별 단가와 별개로 중복 할인도 가능하다~
         *이것은 기존 단가 통제 로직 이후에 할인율을 적용 해줘야 한다. 
         *할인율 적용여부가 N 일 경우에는 기존 단가 컬럼에 단가가 들어가서 수량 * 단가 = 공급가액 으로 계산되었으나 
         *할인율 적용여부가 Y 일 경우에는 기준단가(UM_BASE) 에 단가가 들어가서 단가 = 기준단가 - (기준단가*할인율)/100 을 한 단가를 구한뒤에
         *수량 * 계산된 단가(기존단가컬럼) = 공급가액 으로 계산되어야 한다.
         *할인율 적용 여부에 따라 기준단가와 할인율의 그리드 컬럼이 Visible true/false 되어야 하며 
         *표준으로 적용되어야 할 사항이다.
         */
        // 부가세 포함 화면에서는 그냥 저장만 잘 될수 있도록 스키마만 만들어서 프로시져호출시 에러 안나게 까지만 처리한다.
        // 나중에 상세 로직 나오면 부가세 포함은 그때 결정 및 로직 추가하기로한다.
        private string disCount_YN = "N";  //할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)

        // cd_CC 설정 : 시스템 환경설정의 cost center 에 따라서 영업그룹의 c/c 를 설정할지 수주유형의 c/c 를 설정 할지를 결정한다. 
        string cd_CC = string.Empty;     //영업그룹을 선택시 COST CENTER 도 가져온다. 그리고 수주유형을 선택시 COST CENTER 도 가져온다. 
        string nm_CC = string.Empty;     //영업그룹을 선택시 COST CENTER 도 가져온다. 그리고 수주유형을 선택시 COST CENTER 도 가져온다. 

		string _매출자동여부 = string.Empty; //수주유형 선택시 매출자동여부를 넣어준다.

        public P_SA_SO_BATCH_VAT()
        {
            try
            {
                InitializeComponent();

                //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
                DataTable dt = _biz.search_EnvMng();

                if (dt.Rows.Count > 0)
                {
                    // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                    if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                    {
                        disCount_YN = dt.Select("FG_TP = '002'")[0]["CD_TP"].ToString();    //할인율 적용여부 : 002
                    }
                }

                MainGrids = new FlexGrid[] { _flex };
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        } 
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region ♣ InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        #endregion

        #region ♣ InitGrid : 그리드 초기화
        private void InitGrid()
        { 
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol("NO_SO", "수주번호", 120, false);
            _flex.SetCol("SEQ_SO", "수주항번", 120, false);
            _flex.SetCol("CD_PARTNER", "거래처", 120, true);
            _flex.SetCol("LN_PARTNER", "거래처명", 120, false);
            _flex.SetCol("CD_ITEM", "품목코드", 120, true);
            _flex.SetCol("NM_ITEM", "품목명", 120, false);
            _flex.SetCol("STND_ITEM", "규격", 65, false);
            _flex.SetCol("UNIT_SO", "단위", 65, false);
            _flex.SetCol("CD_PLANT", "공장", 140, true);
            _flex.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("QT_SO", "수량", 60, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            //세전단가 세전금액은 히든 -> 다시 보여줌.
            _flex.SetCol("UM_SO", "세전단가", 100, 15, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("AM_SO", "세전금액", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            //세후단가 세후금액을 오픈해준다.
            _flex.SetCol("UMVAT_SO", "세후단가", 100, 15, true, typeof(decimal), FormatTpType.UNIT_COST);
            _flex.SetCol("AMVAT_SO", "세후금액", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
            //_flex.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_VAT", "부가세", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT_IM", "관리단위", 65, false);
            _flex.SetCol("QT_IM", "관리수량", 65, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("CD_SL", "창고코드", 80, true);
            _flex.SetCol("NM_SL", "창고명", 120, false);
            _flex.SetCol("TP_ITEM", "품목타입", false);
            _flex.SetCol("UNIT_SO_FACT", "수주단위수량", false);
            _flex.SetCol("LT_GI", "출하LT", false);
            _flex.SetCol("GI_PARTNER", "납품처코드", 120, true);
            _flex.SetCol("GN_PARTNER", "납품처명", 200, false);

            _flex.SetCol("DC_RMK", "헤더비고", 200, true);
            _flex.SetCol("DC1", "비고1", 200, true);
            _flex.SetCol("DC2", "비고2", 200, true);

            if (Sa_Global.Sol_TpVat_ModifyYN == "Y")
            {
                _flex.SetCol("TP_VAT", "VAT구분", 80, true);
                _flex.SetCol("RT_VAT", "VAT율", 70, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            if (Sa_Global.SoL_CdCc_ModifyYN == "Y") //영업환경설정의 수주라인-C/C설정수정유무 추가 2010.04.06 NJin (Default Value = "N" 으로 셋팅)
            {
                _flex.SetCol("CD_CC", "코스트센터", 100, true);
                _flex.SetCol("NM_CC", "코스트센터명", 120, false);
            }

            if (disCount_YN == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
            {
                _flex.SetCol("RT_DSCNT", "할인율", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("UM_BASE", "기준단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }

            _flex.SetCol("RMA_REASON", "반품사유", 100, true);

            //그리드에 도움창 띄워주는 부분
            _flex.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always,
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT", "LT_GI", "YN_ATP" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT", "LT_GI", "YN_ATP" }, 
                                  Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flex.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, 
                                  new string[] { "CD_SL", "NM_SL" }, 
                                  new string[] { "CD_SL", "NM_SL" }, 
                                  Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flex.SetCodeHelpCol("CD_PARTNER", Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, 
                                  new string[] { "CD_PARTNER", "LN_PARTNER","GI_PARTNER", "GN_PARTNER"},
                                  new string[] { "CD_PARTNER", "LN_PARTNER", "CD_PARTNER", "LN_PARTNER" });
            _flex.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, 
                                  new string[] { "GI_PARTNER", "GN_PARTNER" }, 
                                  new string[] { "CD_TPPTR", "NM_TPPTR" });
            _flex.SetCodeHelpCol("CD_CC", Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB, ShowHelpEnum.Always,
                                  new string[] { "CD_CC", "NM_CC" },
                                  new string[] { "CD_CC", "NM_CC" });

            //그리드에 Edit 모드가 안되게 막는 부분
            _flex.SetExceptEditCol("NO_SO", "SEQ_SO", "LN_PARTNER", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "UM_SO", "AM_SO", "AM_WONAMT", "NM_SL", "UNIT_SO_FACT", "GN_PARTNER", "RT_VAT", "NM_CC");

            //그리드 필수입력 해야 하는부분 셋팅
            _flex.VerifyNotNull = new string[] { "CD_PARTNER", "CD_PLANT", "CD_ITEM", "DT_DUEDATE", "DT_REQGI", "TP_VAT", "CD_CC" };

            //그리드에 값을 비교하는 부분
            _flex.VerifyCompare(_flex.Cols["QT_SO"], 0, OperatorEnum.Greater);
            _flex.VerifyCompare(_flex.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["UMVAT_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["AMVAT_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            _flex.VerifyCompare(_flex.Cols["QT_IM"], 0, OperatorEnum.Greater);

            _flex.SettingVersion = "1.0.0.4";

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            if (disCount_YN == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
            {
                _flex.SetExceptSumCol("UM_BASE", "RT_DSCNT", "RT_VAT");
            }
            else
                _flex.SetExceptSumCol("RT_VAT");

            // 그리드 이벤트 선언
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flex.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);

            //이렇게 해줘야 그리드 마지막 라인의 마지막 컬럼에서 엔터키 사용시 자동으로 행이 한줄씩 추가된다.
            _flex.AddRow += new EventHandler(btn_AddRow_Click);
        }
        #endregion

        #region ♣ InitPaint : 프리폼 초기화
        protected override void InitPaint()
        {
            DataSet ds = this.GetComboData("N;MA_B000005", "S;SA_B000021", "N;MA_CODEDTL_005;MA_B000040", "N;MA_B000004", "N;MA_PLANT", "S;SA_B000064"); 
                                           
            // 환종
            cbo_CdExch.DataSource = ds.Tables[0];
            cbo_CdExch.DisplayMember = "NAME";
            cbo_CdExch.ValueMember = "CODE";

            txt_RtExch.DecimalValue = 1;

            //단가유형
            cbo_TpPrice.DataSource = ds.Tables[1];
            cbo_TpPrice.DisplayMember = "NAME";
            cbo_TpPrice.ValueMember = "CODE";

            // 부가세
            ds.Tables[2].PrimaryKey = new DataColumn[] { ds.Tables[2].Columns["CODE"] };
            cbo_TpVat.DataSource = ds.Tables[2];
            cbo_TpVat.DisplayMember = "NAME";
            cbo_TpVat.ValueMember = "CODE";

            //부가세
            if (Sa_Global.Sol_TpVat_ModifyYN == "Y")
                _flex.SetDataMap("TP_VAT", ds.Tables[2].Copy(), "CODE", "NAME");

            _flex.SetDataMap("UNIT_SO", ds.Tables[3], "CODE", "NAME");      // 단위
            _flex.SetDataMap("CD_PLANT", ds.Tables[4], "CODE", "NAME");     // 공장
            _flex.SetDataMap("RMA_REASON", ds.Tables[5], "CODE", "NAME");   // 반품사유

            dat_Sodt.Text = Global.MainFrame.GetStringToday;
            //dtp납기일자.Text = Global.MainFrame.GetStringToday;

            bp_SalesGroup.CodeValue = string.Empty;
            bp_SalesGroup.CodeName = string.Empty; 
            bp_SoType.CodeValue = string.Empty;
            bp_SoType.CodeName = string.Empty;
            bp_PJT.CodeValue = string.Empty;
            bp_PJT.CodeName = string.Empty;
            txt_TpVat.DecimalValue = 10;
            bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

            //최초 그리드에 스키마를 만들어 준다.
            defaultSchema = getSchema("DEFAULT_SCHEMA");
            _flex.Binding = defaultSchema;

            if (_biz.GetATP사용여부 == "000")
                btn_ATP.Visible = false;
        }
        #endregion 

        #region ♣ InitEvent

        private void InitEvent()
        {
            btn_ATP.Click += new EventHandler(btn_ATP_Click);
        }
#endregion
        

        #endregion

        #region ♣ 헤더의 활성 비활성을 제어해주는 부분
        private void Authority(bool check)
        {
            dat_Sodt.Enabled = check;
            bp_Emp.Enabled = check;
            bp_SoType.Enabled = check;
            bp_PJT.Enabled = check;
            bp_SalesGroup.Enabled = check;
            cbo_TpVat.Enabled = check;
            cbo_TpPrice.Enabled = check;
            dtp납기일자.Enabled = check;

            //부가세 포함로직은 무조건 막히기때문에 필요없는 부분
            //cbo_CdExch.Enabled = check;
            //txt_RtExch.Enabled = check;

            //부가세 포함로직은 무조건 막히기때문에 필요없는 부분
            //if (check == true && cbo_CdExch.SelectedValue.ToString() == "000")
            //    txt_RtExch.Enabled = false;
            //else
            //    txt_RtExch.Enabled = check; 
        }
        #endregion

        #region ♣ 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool ErrorCheck()
        {
            Hashtable hList = new Hashtable();

            hList.Add(dat_Sodt, lbl_수주일자);
            hList.Add(bp_SalesGroup, lbl_영업그룹);
            hList.Add(bp_Emp, lbl_담당자);
            hList.Add(bp_SoType, lbl_수주형태);
            hList.Add(cbo_CdExch, lbl_화폐단위);
            hList.Add(cbo_TpPrice, lbl_단가유형);
            hList.Add(cbo_TpVat, lbl_VAT구분);

            if (App.SystemEnv.PROJECT사용)
                hList.Add(bp_PJT, lbl_프로젝트);

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region ♣ 조회버튼클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        { 
            try
            {
                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 추가버튼클릭
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();

                //프리폼 초기화
                InitPaint();

                //Authority Setting 헤더 프리폼 고정
                Authority(true);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region ♣ 라인추가버튼클릭
        private void btn_AddRow_Click(object sender, EventArgs e)
        {
            if (!ErrorCheck()) return; //필수입력 체크

            try
            {
                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;

                _flex["CD_PLANT"] = LoginInfo.CdPlant;

                _flex["QT_SO"] = 0;
                _flex["UM_SO"] = 0;
                _flex["AM_SO"] = 0;
                _flex["AM_VAT"] = 0;
                _flex["QT_IM"] = 0;
                _flex["AMVAT_SO"] = 0;
                _flex["UMVAT_SO"] = 0;

                if (disCount_YN == "Y") //영업환경설정의 할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
                {
                    _flex["RT_DSCNT"] = 0;  //할인율
                    _flex["UM_BASE"] = 0;   //기준단가
                }

                _flex["TP_VAT"] = cbo_TpVat.SelectedValue == null ? string.Empty : cbo_TpVat.SelectedValue.ToString();
                _flex["RT_VAT"] = txt_TpVat.DecimalValue;
                _flex["DT_DUEDATE"] = dtp납기일자.Text;
                _flex["DT_REQGI"] = dtp납기일자.Text;

                //수주라인에 cost center 추가 2009.09.18
                _flex["CD_CC"] = cd_CC;
                _flex["NM_CC"] = nm_CC;

                _flex.Col = _flex.Cols["CD_PARTNER"].Index;
                _flex.Focus();

                this.btn_DelRow.Enabled = true;

                //Authority Setting 헤더 프리폼 고정
                Authority(false);

                ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion
        #endregion

        #region ♣ 삭제버튼클릭
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        { 
            try
            {

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region ♣ 라인삭제
        private void btn_DelRow_Click(object sender, EventArgs e)
        {
            //행이 추가되는 라인이 하나도 존재하지 않을때에는 헤더를 풀어주고 라인 삭제버튼을 막아준다.
            if (!_flex.HasNormalRow)
                return;

            try
            {
                // 선택한 라인삭제
                _flex.Rows.Remove(_flex.Row);

                if (!_flex.HasNormalRow)
                {
                    btn_DelRow.Enabled = false; //라인 삭제버튼 비활성
                    Authority(true);            //헤더 프리폼 수정가능 
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            } 
        }
        #endregion
        #endregion

        #region ♣ 저장버튼클릭 : 여신체크 안하고 있음
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //MsgAndSave(PageActionMode.Save) 에서 자동으로 SaveData()함수를 호출한다.
                //if (MsgAndSave(PageActionMode.Save))
                //    this.ShowMessage(PageResultMode.SaveGood);

                if (SaveData())
                    ShowMessage("저장되었습니다. \n\n저장된 데이터는 수주관리에서 조회하실 수 있습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        //MsgAndSave(PageActionMode.Save) 에서 SaveData()호출
        protected override bool SaveData()
        {
            if(!ErrorCheck()) return false; //필수입력 체크
            if(!Verify()) return false;     //그리드에 설정해 놓은 필수입력 체크

            _flex.FinishEditing();          //2011-04-25, 최승애

            #region 과세구분, 과세율, cc 는 필수로 라인에 무조건 저장 되어 져야 한다.
            int cnt_tpVat = 0, cnt_rtVat = 0, cnt_Cdcc = 0;
            //부가세구분 Setting 에 따른 부가세율 Setting
            decimal rtVat = 0;
            object[] obj_Vat = new object[3];

            foreach (DataRow dr in _flex.DataTable.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                    continue;

                if (dr["TP_VAT"].ToString() == string.Empty)
                    cnt_tpVat++;

                obj_Vat[0] = LoginInfo.CompanyCode;     //회사
                obj_Vat[1] = bp_SoType.CodeValue;          //수주형태
                obj_Vat[2] = dr["TP_VAT"].ToString();   //VAT 구분

                rtVat = D.GetDecimal(Sa_ComFunc.GetTpBusi(obj_Vat)[1].ToString());

                //면세나 영세가 아닌 과세 대상이 부가세율이 0 이면 바보다.
                if (D.GetDecimal(dr["RT_VAT"]) != rtVat)
                    cnt_rtVat++;

                if (dr["CD_CC"].ToString() == string.Empty)
                    cnt_Cdcc++;
            }

            if (cnt_tpVat != 0)
            {
                this.ShowMessage("라인과세는 필수입력항목입니다. \n\n 라인과세 입력을 확인하세요.");
                return false;
            }

            if (cnt_rtVat != 0)
            {
                this.ShowMessage("라인부과세율 정보가 잘못되었습니다. \n\n 라인부과세율 정보를 확인하세요.");
                return false;
            }

            if (cnt_Cdcc != 0)
            {
                this.ShowMessage("Cost Center 는 필수입력항목입니다. \n\n Cost Center 입력을 확인하세요.");
                return false;
            }
            #endregion

            DataTable dt = null, dt_SoH = null, dt_SoL = null;

            try
            {
                string cd_bizarea = Global.MainFrame.LoginInfo.BizAreaCode;
                string no_So = string.Empty;
                decimal no_Hst = 0; // 0 차수부터 시작 
                string tp_vat = cbo_TpVat.SelectedValue.ToString();

                dt = GetCalculater(_flex.DataView.ToTable(), ExcelType.NONE);

                if (dt.Rows.Count == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, "저장될 내역");
                    return false;
                }

                if (_biz.GetATP사용여부 == "001")
                {
                    if (!ATP체크로직(true)) return false;
                }

                dt_SoH = getSchema("SOH");
                dt_SoL = getSchema("SOL");

                #region ♣ 헤더에 데이터 채우기
                foreach (DataRow dr in dt.Rows)
                {
                    string filter = "CD_PARTNER = '" + dr["CD_PARTNER"].ToString().Trim() + "' AND ISNULL(DC_RMK, '') = '" + D.GetString(dr["DC_RMK"]) + "' AND ISNULL(RMA_REASON, '') = '" + D.GetString(dr["RMA_REASON"]) + "'";
                    
                    DataRow[] drh = dt_SoH.Select(filter);
                    if (drh.Length == 0)
                    {
                        DataRow dr_SoH = dt_SoH.NewRow();

                        if (_반품여부 == "N")
                            no_So = GetSeq(LoginInfo.CompanyCode, "SA", "02", dat_Sodt.Text.Substring(0, 6)).ToString();
                        else
                            no_So = GetSeq(LoginInfo.CompanyCode, "SA", "16", dat_Sodt.Text.Substring(0, 6)).ToString();

                        //그리드의 dt 에 채번번호를 입혀준다.
                        dr["NO_SO"] = no_So; //그리드의 채번된 첫행
                        //========================================
                        
                        dr_SoH["NO_SO"] = no_So;
                        dr_SoH["NO_HST"] = no_Hst;  
                        dr_SoH["CD_BIZAREA"] = cd_bizarea;
                        dr_SoH["DT_SO"] = dat_Sodt.Text;
                        dr_SoH["CD_PARTNER"] = dr["CD_PARTNER"];
                        dr_SoH["CD_SALEGRP"] = bp_SalesGroup.CodeValue;
                        dr_SoH["NO_EMP"] = bp_Emp.CodeValue;
                        dr_SoH["TP_SO"] = bp_SoType.CodeValue;
                        dr_SoH["CD_EXCH"] = cbo_CdExch.SelectedValue.ToString();
                        dr_SoH["RT_EXCH"] = txt_RtExch.DecimalValue;
                        dr_SoH["TP_PRICE"] = cbo_TpPrice.SelectedValue.ToString(); //단가유형
                        dr_SoH["NO_PROJECT"] = bp_PJT.CodeValue;
                        dr_SoH["TP_VAT"] = tp_vat;
                        dr_SoH["RT_VAT"] = txt_TpVat.DecimalValue;
                        dr_SoH["VATRATE"] = txt_TpVat.DecimalValue;
                        dr_SoH["FG_VAT"] = "Y";     //부가세 사용유무(부가세 포함여부)
                        dr_SoH["FG_TAXP"] = "001";  //계산서처리 : 001 일괄 , 002 건별
                        dr_SoH["DC_RMK"] = dr["DC_RMK"];
                        dr_SoH["FG_BILL"] = string.Empty;
                        dr_SoH["FG_TRANSPORT"] = string.Empty;
                        dr_SoH["NO_CONTRACT"] = string.Empty;

                        dr_SoH["RMA_REASON"] = dr["RMA_REASON"];

                        //수주등록 추적컬럼
                        //'M' 수주등록, 'P' 수주등록(거래처), 'H' 수주이력등록, 'W' 수주웹등록, 
                        //'ME' 일괄 수주등록, 'MEV' 일괄 수주등록(부가세포함), 'YV' 수주등록(용역)
                        dr_SoH["FG_TRACK"] = "MEV";  

                        /*DEFAULT 로 "o"가 되어야 하지 않을까 싶지만...*/
                        //저장시점에 수주유형을 읽어와서 수주상태를 체크한다.
                        string[] tp_Busi = null;                        //거래구분 
                        object[] obj = new object[3];
                        obj[0] = LoginInfo.CompanyCode;                 //회사
                        obj[1] = bp_SoType.CodeValue.ToString();        //수주형태
                        obj[2] = cbo_TpVat.SelectedValue.ToString();    //VAT 구분
                        tp_Busi = _biz.GetTpBusi(obj);

                        if (tp_Busi[2].ToString() == "Y") //자동승인여부 "Y" 자동승인, "N" 자동승인안됨
                            dr_SoH["STA_SO"] = "R"; //수주상태
                        else
                            dr_SoH["STA_SO"] = "O"; //수주상태

                        //그리드의 dt 에 채번번호를 입혀준다.
                        soStatus = dr_SoH["STA_SO"].ToString(); //그리드의 채번된 첫행
                        dr["STA_SO"] = dr_SoH["STA_SO"]; //그리드의 채번된 첫행
                        //========================================

                        dt_SoH.Rows.Add(dr_SoH);
                    }
                    else //그리드의 dt 에 채번번호를 입혀준다.
                    {
                        dr["NO_SO"] = no_So; //그리드의 같은 채번번호로 사용될 행
                        dr["STA_SO"] = soStatus; //그리드의 채번된 첫행
                    }
                }
                #endregion

                #region ♣ 라인에 데이터 채우기
                foreach (DataRow dr in dt_SoH.Rows)
                {
                    decimal seq_No = 1;
                    string filter = "CD_PARTNER = '" + dr["CD_PARTNER"].ToString().Trim() + "' AND ISNULL(DC_RMK, '') = '" + D.GetString(dr["DC_RMK"]) + "' AND ISNULL(RMA_REASON, '') = '" + D.GetString(dr["RMA_REASON"]) + "'";
                    DataRow[] drl = dt.Select(filter);
                    foreach (DataRow dl in drl)
                    {
                        DataRow dr_SoL = dt_SoL.NewRow();

                        dr_SoL["NO_SO"] = dr["NO_SO"];
                        dr_SoL["NO_HST"] = no_Hst;    
                        dr_SoL["SEQ_SO"] = seq_No;
                        dr_SoL["CD_PLANT"] = dl["CD_PLANT"];
                        dr_SoL["CD_ITEM"] = dl["CD_ITEM"];
                        dr_SoL["UNIT_SO"] = dl["UNIT_SO"];
                        dr_SoL["DT_DUEDATE"] = dl["DT_DUEDATE"];
                        dr_SoL["DT_REQGI"] = dl["DT_REQGI"];
                        dr_SoL["QT_SO"] = dl["QT_SO"];
                        dr_SoL["UM_SO"] = dl["UM_SO"];
                        dr_SoL["AM_SO"] = dl["AM_SO"];
                        dr_SoL["AM_WONAMT"] = dl["AM_WONAMT"];
                        dr_SoL["AM_VAT"] = dl["AM_VAT"];
                        dr_SoL["UNIT_IM"] = dl["UNIT_IM"];
                        dr_SoL["QT_IM"] = dl["QT_IM"];
                        dr_SoL["CD_SL"] = dl["CD_SL"];
                        dr_SoL["TP_ITEM"] = string.Empty;
                        dr_SoL["STA_SO"] = soStatus;
                        dr_SoL["TP_BUSI"] = tp_Busi;
                        dr_SoL["TP_GI"] = tp_Gi;
                        dr_SoL["TP_IV"] = tp_Iv;
                        dr_SoL["GIR"] = _의뢰여부;
                        dr_SoL["GI"] = _출하여부;
                        dr_SoL["IV"] = _매출여부;
                        dr_SoL["TRADE"] = trade;
                        dr_SoL["CD_CC"] = dl["CD_CC"];
                        dr_SoL["TP_VAT"] = dl["TP_VAT"];
                        dr_SoL["RT_VAT"] = dl["RT_VAT"];
                        dr_SoL["GI_PARTNER"] = dl["GI_PARTNER"];
                        dr_SoL["NO_PROJECT"] = bp_PJT.CodeValue;
                        dr_SoL["SEQ_PROJECT"] = 0;      //프로젝트 항번은 0으로 때려준다. : 프로젝트 적용 받을때 빼곤 항번은 0 으로 가야 트리거에서 집계작업을 안한다.
                        dr_SoL["UMVAT_SO"] = dl["UMVAT_SO"];
                        dr_SoL["AMVAT_SO"] = dl["AMVAT_SO"];
                        dr_SoL["DC1"] = dl["DC1"];
                        dr_SoL["DC2"] = dl["DC2"];


                        //배송정보 TRACK 기능 => FG_TRACK : SO(수주등록), M(창고이동, 출고요청등록), R(출하반품의뢰등록)
                        dr_SoL["FG_TRACK"] = "SO";

                        dr_SoL["RT_DSCNT"] = 0;
                        dr_SoL["UM_BASE"] = 0;

                        dt_SoL.Rows.Add(dr_SoL);

                        seq_No++;
                    }
                }
                #endregion

				if (MA.ServerKey(true, new string[] { "MCIRCLE" }))
				{
					if (_biz.Get과세변경유무 == "N" && _매출자동여부 == "Y")
					{
						foreach (DataRow row in dt_SoH.Rows)
						{
							row["DT_PROCESS"] = dat_Sodt.Text;
							row["DT_RCP_RSV"] = dat_Sodt.Text;
							row["AM_IV"] = D.GetDecimal(dt_SoL.Compute("SUM(AM_WONAMT)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'"));
							row["AM_IV_EX"] = D.GetDecimal(dt_SoL.Compute("SUM(AM_SO)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'"));
							row["AM_IV_VAT"] = D.GetDecimal(dt_SoL.Compute("SUM(AM_VAT)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'"));
						}
					}
				}

				bool bSuccess = _biz.Save(dt_SoH, dt_SoL, _매출자동여부);

                if (!bSuccess) return false;

                //정상조회되면 초기화를 시켜준다.
                OnToolBarAddButtonClicked(null, null);
            }
            catch(Exception ex)
            {
                MsgEnd(ex);
                return false;
            }

            return true; 
        }
        
        #endregion

        #region ♣ EXCEL

        #region ♣ 엑셀업로드
        private void btn_Excel_Click(object sender, EventArgs e)
        {
            if (!ErrorCheck()) return;  //필수입력 체크

            Duzon.Common.Util.Excel excel = null;
            OpenFileDialog dlg = null;
            DataTable dt_Excel = null, CheckTable = null, ResultTable = null;

            try
            {
                dlg = new OpenFileDialog();

                dlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                Application.DoEvents();

                string excelFileName = dlg.FileName;

                excel = new Duzon.Common.Util.Excel();

                dt_Excel = excel.StartLoadExcel(excelFileName);

                DataTable dt_Excel_Temp = dt_Excel.Clone();

                foreach (DataColumn dc in dt_Excel_Temp.Columns)
                {
                    switch (dc.ColumnName)
                    {
                        case "QT_SO":
                        case "AMVAT_SO":
                        case "UMVAT_SO":
                            dc.DataType = typeof(decimal);
                            break;
                        default:
                            dc.DataType = typeof(string);
                            break;
                    }
                }

                foreach (DataRow dr in dt_Excel.Rows)
                {
                    dt_Excel_Temp.Rows.Add(dr.ItemArray);
                }

                #region -> 날짜 입력형식을 체크한다.

                StringBuilder sbErrorList = new StringBuilder();

                int iRow = 0;

                foreach (DataRow reader in dt_Excel_Temp.Rows)
                {
                    iRow++;

                    if (!Duzon.ERPU.D.StringDate.IsValidDate(reader["DT_DUEDATE"].ToString(), false, ""))
                        sbErrorList.AppendLine(iRow.ToString() + "번행 납기요구일[DT_DUEDATE]컬럼(" + reader["DT_DUEDATE"].ToString() + ")의 입력형식이 올바르지 않습니다.");

                    if (!Duzon.ERPU.D.StringDate.IsValidDate(reader["DT_REQGI"].ToString(), false, ""))
                        sbErrorList.AppendLine(iRow.ToString() + "번행 출하예정일[DT_REQGI]컬럼(" + reader["DT_REQGI"].ToString() + ")의 입력형식이 올바르지 않습니다.");
                }

                if (sbErrorList.Length > 0)
                {
                    ShowDetailMessage("날짜 입력형식이 맞지 않는 목록입니다.", sbErrorList.ToString());
                    return;
                }

                #endregion

                dt_Excel = dt_Excel_Temp;
 
                dt_Excel = getExcelSchema(dt_Excel);

                bool b = false;
                

                //등록한 엑셀데이터 테이블을 마스터 정보들과 비교해서 있는것만 가져와야 한다.
                if(dt_Excel != null && dt_Excel.Rows.Count > 0)
                    CheckTable = SO_PK_Check(dt_Excel, out b);//검사하기

                if (!b) return;

                ExcelType excelTypeEnum = new ExcelType();

                //만약 엑셀에 부가세포함금액, 부가세포함단가 모두 포함되어 있다면 일단 부가세포함금액 양식이라고 생각한다.
                if (CheckTable.Columns.Contains("UMVAT_SO") && CheckTable.Columns.Contains("AMVAT_SO"))
                    excelTypeEnum = ExcelType.부가세포함금액;

                if (!CheckTable.Columns.Contains("UMVAT_SO"))
                {
                    CheckTable.Columns.Add("UMVAT_SO", typeof(decimal));
                    excelTypeEnum = ExcelType.부가세포함금액;
                }

                if (!CheckTable.Columns.Contains("AMVAT_SO"))
                {
                    CheckTable.Columns.Add("AMVAT_SO", typeof(decimal));
                    excelTypeEnum = ExcelType.부가세포함단가;
                }

                //등록한 엑셀데이터 테이블을 마스터 정보들과 비교해서 있는것만 가져와야 한다.
                if (CheckTable != null && CheckTable.Rows.Count > 0)
                    ResultTable = GetCalculater(CheckTable, excelTypeEnum);//계산로직을 구하고 DEFAULT 공장 및 관리수량을 셋팅 한다.
 
                _flex.Binding = ResultTable;

                //Authority Setting 헤더 프리폼 고정
                Authority(false);

                btn_AddRow.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 검사하기
        private DataTable SO_PK_Check(DataTable dt_Excel, out bool b)
        {
            DataSet CheckSet = null;
            DataTable NewDataTable = dt_Excel.Clone();

            object[] obj = new object[1];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;

            try
            {
                DataRow[] CheckDr = null;
                CheckSet = _biz.SO_PK_Check(obj);

                foreach (DataRow excelDr in dt_Excel.Rows)
                {
                    #region -> 품목코드의 존재여부 체크

					CheckDr = CheckSet.Tables[0].Select("CD_PLANT = '" + excelDr["CD_PLANT"].ToString() + "'" + " CD_ITEM = '" + excelDr["CD_ITEM"].ToString() + "'");

                    if (CheckDr.Length == 0 && excelDr["CD_ITEM"].ToString() != String.Empty)
                    {
                        ShowMessage(DD("품목(" + excelDr["CD_ITEM"].ToString() + ")은 존재하지 않는 품목입니다."));
                        b = false;
                        return NewDataTable;
                    }

                    foreach (DataRow drs in CheckDr)
                    {
                        excelDr["NM_ITEM"] = drs["NM_ITEM"].ToString();
                        excelDr["STND_ITEM"] = drs["STND_ITEM"].ToString();
                        excelDr["UNIT_SO"] = drs["UNIT_SO"].ToString();
                        excelDr["UNIT_IM"] = drs["UNIT_IM"].ToString();//관리단위
                        excelDr["UNIT_SO_FACT"] = drs["UNIT_SO_FACT"].ToString();
                    }
                    #endregion
                      
                    #region -> 거래처코드/납품처코드의 존재여부 체크

                    //거래처코드의 존재여부 체크
                    CheckDr = CheckSet.Tables[1].Select("CD_PARTNER = '" + excelDr["CD_PARTNER"].ToString() + "'");

                    if (CheckDr.Length == 0 && excelDr["CD_PARTNER"].ToString() != String.Empty)
                    {
                        ShowMessage(DD("거래처(" + excelDr["CD_PARTNER"].ToString() + ")은 존재하지 않는 거래처입니다."));
                        b = false;
                        return NewDataTable;
                    }

                    foreach (DataRow drs in CheckDr)
                    {
                        excelDr["LN_PARTNER"] = drs["LN_PARTNER"].ToString();
                    }


                    //납품처코드의 존재여부 체크
                    CheckDr = CheckSet.Tables[1].Select("CD_PARTNER = '" + excelDr["GI_PARTNER"].ToString() + "'");

                    if (CheckDr.Length == 0 && excelDr["GI_PARTNER"].ToString() != String.Empty)
                    {
                        ShowMessage(DD("납품처(" + excelDr["GI_PARTNER"].ToString() + ")은 존재하지 않는 납품처입니다."));
                        b = false;
                        return NewDataTable;
                    }

                    foreach (DataRow drs in CheckDr)
                    {
                        excelDr["GN_PARTNER"] = drs["LN_PARTNER"].ToString();
                    }
                    #endregion

                    #region -> 창고코드의 존재여부 체크

                    CheckDr = CheckSet.Tables[2].Select("CD_SL = '" + excelDr["CD_SL"].ToString() + "'");

                    if (CheckDr.Length == 0 && excelDr["CD_SL"].ToString() != String.Empty)
                    {
                        ShowMessage(DD("창고(" + excelDr["CD_SL"].ToString() + ")은 존재하지 않는 창고입니다."));
                        b = false;
                        return NewDataTable;
                    }

                    foreach (DataRow drs in CheckDr)
                    {
                        excelDr["NM_SL"] = drs["NM_SL"].ToString();
                    }
                    #endregion
                } 
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            b = true;
            return dt_Excel;
        }
        #endregion

        #region ♣ 계산로직을 구한다.
        private DataTable GetCalculater(DataTable CheckTable, ExcelType excelTypeEnum)
        {
            DataTable dt = CheckTable.Clone();

            foreach (DataRow dr in CheckTable.Rows)
            {
                if (Global.MainFrame.ServerKey != "NUGA")
                {
                    if (bp_PJT.CodeValue != string.Empty && D.GetString(dr["CD_PARTNER"]) != Partner)
                        continue;
                }

                //엑셀에 공장을 셋팅해주지 않으면 기본적으로 로그인한 공장이 세팅 되도록 해블자.
                if (D.GetString(dr["CD_PLANT"]) == string.Empty)
                    dr["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

                //DEFAULT SETTING
                dr["TP_GI"] = tp_Gi;        //수주유형 선택시 출하형태를 넣어준다.
                dr["TP_BUSI"] = tp_Busi;    //수주유형 선택시 거래구분을 넣어준다.
                dr["TP_IV"] = tp_Iv;        //수주유형 선택시 매출형태를 넣어준다.

                dr["GIR"] = _의뢰여부;        //수주유형 선택시 매출형태를 넣어준다.
                dr["GI"] = _출하여부;        //수주유형 선택시 매출형태를 넣어준다.
                dr["IV"] = _매출여부;        //수주유형 선택시 매출형태를 넣어준다.
                dr["TRADE"] = trade;        //수주유형 선택시 매출형태를 넣어준다. 

                dr["CD_CC"] = cd_CC;
                dr["NM_CC"] = nm_CC;

                dr["TP_VAT"] = D.GetString(cbo_TpVat.SelectedValue);
                dr["RT_VAT"] = txt_TpVat.DecimalValue;

                dr["SEQ_PROJECT"] = 0;      //프로젝트 항번은 0으로 때려준다. : 프로젝트 적용 받을때 빼곤 항번은 0 으로 가야 트리거에서 집계작업을 안한다.

                /*계산로직*/
                //부가세포함여부가 FG_VAT = "Y" 일때 부가세포함 금액계산
                if (excelTypeEnum == ExcelType.부가세포함금액)
                {
                    dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]));
                    dr["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]) / D.GetDecimal(dr["QT_SO"]));
                }
                else if (excelTypeEnum == ExcelType.부가세포함단가)
                {
                    dr["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UMVAT_SO"]));
                    dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["UMVAT_SO"]) * D.GetDecimal(dr["QT_SO"]));
                }

                dr["AM_WONAMT"] = Decimal.Round(D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))), MidpointRounding.AwayFromZero);
                dr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]) - D.GetDecimal(dr["AM_WONAMT"]));
                dr["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                dr["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]) / (D.GetDecimal(dr["QT_SO"]) == 0 ? 1 : D.GetDecimal(dr["QT_SO"])));

                //관리수량
                dr["UNIT_SO_FACT"] = D.GetDecimal(dr["UNIT_SO_FACT"]) == 0m ? 1m : D.GetDecimal(dr["UNIT_SO_FACT"]);
                dr["QT_IM"] = D.GetDecimal(dr["QT_SO"]) * D.GetDecimal(dr["UNIT_SO_FACT"]);

                dt.Rows.Add(dr.ItemArray);
            }

            if (Global.MainFrame.ServerKey != "NUGA")
            {
                if (CheckTable.Rows.Count != dt.Rows.Count)
                {
                    //로우가 삭제가 된것이 있으니까~
                    CheckTable.AcceptChanges();
                    ShowMessage("프로젝트의 거래처와 동일한 거래처만 저장됩니다.");
                }
            }

            return dt;
        }
        #endregion

        #region ♣ 스키마 구하기
        private DataTable getSchema(string param)
        {
            DataTable dt = new DataTable();

            try 
            {
                if (param == "SOH")
                { 
                    dt.Columns.Add("NO_SO", typeof(string));
                    dt.Columns.Add("NO_HST", typeof(decimal));
                    dt.Columns.Add("CD_BIZAREA", typeof(string));
                    dt.Columns.Add("DT_SO", typeof(string));
                    dt.Columns.Add("CD_PARTNER", typeof(string));
                    dt.Columns.Add("CD_SALEGRP", typeof(string));
                    dt.Columns.Add("NO_EMP", typeof(string));
                    dt.Columns.Add("TP_SO", typeof(string));
                    dt.Columns.Add("CD_EXCH", typeof(string));
                    dt.Columns.Add("RT_EXCH", typeof(decimal));
                    dt.Columns.Add("TP_PRICE", typeof(string));
                    dt.Columns.Add("NO_PROJECT", typeof(string));
                    dt.Columns.Add("TP_VAT", typeof(string));
                    dt.Columns.Add("RT_VAT", typeof(decimal));
                    dt.Columns.Add("FG_VAT", typeof(string));
                    dt.Columns.Add("VATRATE", typeof(string));
                    dt.Columns.Add("FG_TAXP", typeof(string));
                    dt.Columns.Add("DC_RMK", typeof(string));
                    dt.Columns.Add("FG_BILL", typeof(string));
                    dt.Columns.Add("FG_TRANSPORT", typeof(string));
                    dt.Columns.Add("NO_CONTRACT", typeof(string));
                    dt.Columns.Add("STA_SO", typeof(string));
                    dt.Columns.Add("FG_TRACK", typeof(string));
                    dt.Columns.Add("NO_PO_PARTNER", typeof(string));

                    dt.Columns.Add("RMA_REASON", typeof(string));   //2011.06.22 반품사유 컬럼 추가

					if (MA.ServerKey(true, new string[] { "MCIRCLE" }))
					{
						if (_biz.Get과세변경유무 == "N" && _매출자동여부 == "Y")
						{
							dt.Columns.Add("DT_PROCESS", typeof(string)); //매출까지 자동프로세스
							dt.Columns.Add("DT_RCP_RSV", typeof(string)); //매출까지 자동프로세스
							dt.Columns.Add("FG_AR_EXC", typeof(string));  //매출까지 자동프로세스
							dt.Columns.Add("AM_IV", typeof(decimal));     //매출까지 자동프로세스
							dt.Columns.Add("AM_IV_EX", typeof(decimal));  //매출까지 자동프로세스
							dt.Columns.Add("AM_IV_VAT", typeof(decimal)); //매출까지 자동프로세스
							dt.Columns.Add("NM_PTR", typeof(string));     //매출까지 자동프로세스
							dt.Columns.Add("EX_EMIL", typeof(string));    //매출까지 자동프로세스
							dt.Columns.Add("EX_HP", typeof(string));      //매출까지 자동프로세스
						}
					}
                }
                else if (param == "SOL")
                { 
                    dt.Columns.Add("NO_SO", typeof(string));
                    dt.Columns.Add("NO_HST", typeof(decimal));
                    dt.Columns.Add("SEQ_SO", typeof(decimal));
                    dt.Columns.Add("CD_PLANT", typeof(string));
                    dt.Columns.Add("CD_ITEM", typeof(string));
                    dt.Columns.Add("UNIT_SO", typeof(string));
                    dt.Columns.Add("DT_DUEDATE", typeof(string));
                    dt.Columns.Add("DT_REQGI", typeof(string));
                    dt.Columns.Add("QT_SO", typeof(decimal));
                    dt.Columns.Add("UM_SO", typeof(decimal));
                    dt.Columns.Add("AM_SO", typeof(decimal));
                    dt.Columns.Add("AM_WONAMT", typeof(decimal));
                    dt.Columns.Add("AM_VAT", typeof(decimal));
                    dt.Columns.Add("NO_PROJECT", typeof(string));
                    dt.Columns.Add("SEQ_PROJECT", typeof(decimal));
                    dt.Columns.Add("UNIT_IM", typeof(string));
                    dt.Columns.Add("QT_IM", typeof(decimal));
                    dt.Columns.Add("CD_SL", typeof(string));
                    dt.Columns.Add("TP_ITEM", typeof(string));
                    dt.Columns.Add("STA_SO", typeof(string));
                    dt.Columns.Add("TP_BUSI", typeof(string));
                    dt.Columns.Add("TP_GI", typeof(string));
                    dt.Columns.Add("TP_IV", typeof(string));
                    dt.Columns.Add("GIR", typeof(string));      //
                    dt.Columns.Add("GI", typeof(string));       //
                    dt.Columns.Add("IV", typeof(string));       // 
                    dt.Columns.Add("TRADE", typeof(string));
                    dt.Columns.Add("TP_VAT", typeof(string));   //
                    dt.Columns.Add("RT_VAT", typeof(decimal));
                    dt.Columns.Add("GI_PARTNER", typeof(string));

                    dt.Columns.Add("CD_ITEM_PARTNER", typeof(string));
                    dt.Columns.Add("NM_ITEM_PARTNER", typeof(string));
                    dt.Columns.Add("DC1", typeof(string));
                    dt.Columns.Add("DC2", typeof(string));
                    dt.Columns.Add("UMVAT_SO", typeof(decimal));
                    dt.Columns.Add("AMVAT_SO", typeof(decimal));
                    dt.Columns.Add("CD_SHOP", typeof(string));
                    dt.Columns.Add("CD_SPITEM", typeof(string));
                    dt.Columns.Add("CD_OPT", typeof(string));
                    dt.Columns.Add("RT_DSCNT", typeof(decimal));
                    dt.Columns.Add("UM_BASE", typeof(decimal));
                    dt.Columns.Add("FG_USE", typeof(string));       //
                    dt.Columns.Add("CD_CC", typeof(string));        //

                    //SA_SOL_DLV 때문에 추가된 컬럼 스키마~ ^^
                    dt.Columns.Add("NM_CUST_DLV", typeof(string));
                    dt.Columns.Add("CD_ZIP", typeof(string));
                    dt.Columns.Add("ADDR1", typeof(string));
                    dt.Columns.Add("ADDR2", typeof(string));
                    dt.Columns.Add("NO_TEL_D1", typeof(string));
                    dt.Columns.Add("NO_TEL_D2", typeof(string));
                    dt.Columns.Add("TP_DLV", typeof(string));
                    dt.Columns.Add("TP_DLV_DUE", typeof(string)); 
                    dt.Columns.Add("DC_REQ", typeof(string));
                    dt.Columns.Add("FG_TRACK", typeof(string));
                }
                else if (param == "DEFAULT_SCHEMA") //헤더라인 상관없이 전체스키마를 가져온다.
                {
                    dt.Columns.Add("NO_SO", typeof(string));
                    dt.Columns.Add("NO_HST", typeof(decimal));
                    dt.Columns.Add("CD_BIZAREA", typeof(string));
                    dt.Columns.Add("DT_SO", typeof(string));
                    dt.Columns.Add("CD_PARTNER", typeof(string));
                    dt.Columns.Add("CD_SALEGRP", typeof(string));
                    dt.Columns.Add("NO_EMP", typeof(string));
                    dt.Columns.Add("TP_SO", typeof(string));
                    dt.Columns.Add("CD_EXCH", typeof(string));
                    dt.Columns.Add("RT_EXCH", typeof(decimal));
                    dt.Columns.Add("TP_PRICE", typeof(string));
                    dt.Columns.Add("NO_PROJECT", typeof(string));
                    dt.Columns.Add("SEQ_PROJECT", typeof(decimal));
                    dt.Columns.Add("TP_VAT", typeof(string));
                    dt.Columns.Add("RT_VAT", typeof(decimal));
                    dt.Columns.Add("FG_VAT", typeof(string));
                    dt.Columns.Add("VATRATE", typeof(string));
                    dt.Columns.Add("FG_TAXP", typeof(string));
                    dt.Columns.Add("DC_RMK", typeof(string));
                    dt.Columns.Add("FG_BILL", typeof(string));
                    dt.Columns.Add("FG_TRANSPORT", typeof(string));
                    dt.Columns.Add("NO_CONTRACT", typeof(string));
                    dt.Columns.Add("STA_SO", typeof(string));
                    dt.Columns.Add("FG_TRACK", typeof(string));
                    dt.Columns.Add("SEQ_SO", typeof(decimal));
                    dt.Columns.Add("CD_PLANT", typeof(string));
                    dt.Columns.Add("CD_ITEM", typeof(string));
                    dt.Columns.Add("UNIT_SO", typeof(string));
                    dt.Columns.Add("DT_DUEDATE", typeof(string));
                    dt.Columns.Add("DT_REQGI", typeof(string));
                    dt.Columns.Add("QT_SO", typeof(decimal));
                    dt.Columns.Add("UM_SO", typeof(decimal));
                    dt.Columns.Add("AM_SO", typeof(decimal));
                    dt.Columns.Add("AM_WONAMT", typeof(decimal));
                    dt.Columns.Add("AM_VAT", typeof(decimal));
                    dt.Columns.Add("UNIT_IM", typeof(string));
                    dt.Columns.Add("QT_IM", typeof(decimal));
                    dt.Columns.Add("CD_SL", typeof(string));
                    dt.Columns.Add("TP_ITEM", typeof(string));
                    dt.Columns.Add("TP_BUSI", typeof(string));
                    dt.Columns.Add("TP_GI", typeof(string));
                    dt.Columns.Add("TP_IV", typeof(string));
                    dt.Columns.Add("GIR", typeof(string));      //
                    dt.Columns.Add("GI", typeof(string));       //
                    dt.Columns.Add("IV", typeof(string));       //
                    dt.Columns.Add("TRADE", typeof(string));
                    dt.Columns.Add("GI_PARTNER", typeof(string));
                    dt.Columns.Add("CD_ITEM_PARTNER", typeof(string));
                    dt.Columns.Add("NM_ITEM_PARTNER", typeof(string));
                    dt.Columns.Add("DC1", typeof(string));
                    dt.Columns.Add("DC2", typeof(string));
                    dt.Columns.Add("UMVAT_SO", typeof(decimal));
                    dt.Columns.Add("AMVAT_SO", typeof(decimal));

                    dt.Columns.Add("LN_PARTNER", typeof(string));
                    dt.Columns.Add("NM_ITEM", typeof(string));
                    dt.Columns.Add("STND_ITEM", typeof(string));
                    dt.Columns.Add("NM_SL", typeof(string));
                    dt.Columns.Add("UNIT_SO_FACT", typeof(decimal));
                    dt.Columns.Add("LT_GI", typeof(decimal));
                    dt.Columns.Add("GN_PARTNER", typeof(string));
                    dt.Columns.Add("NO_PO_PARTNER", typeof(string));
                    dt.Columns.Add("CD_SHOP", typeof(string));
                    dt.Columns.Add("CD_SPITEM", typeof(string));
                    dt.Columns.Add("CD_OPT", typeof(string));
                    dt.Columns.Add("RT_DSCNT", typeof(decimal));
                    dt.Columns.Add("UM_BASE", typeof(decimal));
                    dt.Columns.Add("FG_USE", typeof(string));   //
                    dt.Columns.Add("CD_CC", typeof(string));    //
                    dt.Columns.Add("NM_CC", typeof(string));    //
                    //SA_SOL_DLV 때문에 추가된 컬럼 스키마~ ^^
                    dt.Columns.Add("NM_CUST_DLV", typeof(string));
                    dt.Columns.Add("CD_ZIP", typeof(string));
                    dt.Columns.Add("ADDR1", typeof(string));
                    dt.Columns.Add("ADDR2", typeof(string));
                    dt.Columns.Add("NO_TEL_D1", typeof(string));
                    dt.Columns.Add("NO_TEL_D2", typeof(string));
                    dt.Columns.Add("TP_DLV", typeof(string));
                    dt.Columns.Add("TP_DLV_DUE", typeof(string));
                    dt.Columns.Add("DC_REQ", typeof(string));

                    dt.Columns.Add("RMA_REASON", typeof(string));   //2011.06.22 반품사유 컬럼 추가
                    dt.Columns.Add("YN_ATP", typeof(string));       //2011.07.22 ATP사용여부 컬럼 추가
                }
            }                
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        //엑셀에서 넘겨주지 않는 컬럼들을 추가해서 스키마를 부여하기 위함
        private DataTable getExcelSchema(DataTable dt)
        {
            try
            {
                if(!dt.Columns.Contains("NO_SO"))
                    dt.Columns.Add("NO_SO", typeof(string));
                if (!dt.Columns.Contains("SEQ_SO"))
                    dt.Columns.Add("SEQ_SO", typeof(decimal));
                if (!dt.Columns.Contains("NO_HST"))
                    dt.Columns.Add("NO_HST", typeof(decimal));
                if (!dt.Columns.Contains("LN_PARTNER"))
                    dt.Columns.Add("LN_PARTNER", typeof(string));
                if (!dt.Columns.Contains("NM_ITEM"))
                    dt.Columns.Add("NM_ITEM", typeof(string));
                if (!dt.Columns.Contains("STND_ITEM"))
                    dt.Columns.Add("STND_ITEM", typeof(string));
                if (!dt.Columns.Contains("UNIT_SO"))
                    dt.Columns.Add("UNIT_SO", typeof(string));
                if (!dt.Columns.Contains("UNIT_IM"))
                    dt.Columns.Add("UNIT_IM", typeof(string));
                if (!dt.Columns.Contains("UNIT_SO_FACT"))
                    dt.Columns.Add("UNIT_SO_FACT", typeof(string));
                if (!dt.Columns.Contains("UM_SO"))
                    dt.Columns.Add("UM_SO", typeof(decimal));
                if (!dt.Columns.Contains("AM_SO"))
                    dt.Columns.Add("AM_SO", typeof(decimal));
                if (!dt.Columns.Contains("AM_WONAMT"))
                    dt.Columns.Add("AM_WONAMT", typeof(decimal));
                if (!dt.Columns.Contains("AM_VAT"))
                    dt.Columns.Add("AM_VAT", typeof(decimal));
                if (!dt.Columns.Contains("QT_IM"))
                    dt.Columns.Add("QT_IM", typeof(decimal));
                if (!dt.Columns.Contains("STA_SO"))
                    dt.Columns.Add("STA_SO", typeof(string));
                if (!dt.Columns.Contains("TP_ITEM"))
                    dt.Columns.Add("TP_ITEM", typeof(string));
                if (!dt.Columns.Contains("NM_SL"))
                    dt.Columns.Add("NM_SL", typeof(string));
                if (!dt.Columns.Contains("LT_GI"))
                    dt.Columns.Add("LT_GI", typeof(decimal));
                if (!dt.Columns.Contains("GN_PARTNER"))
                    dt.Columns.Add("GN_PARTNER", typeof(string));
                //비고 컬럼 추가
                if (!dt.Columns.Contains("DC_RMK"))
                    dt.Columns.Add("DC_RMK", typeof(string));
                if (!dt.Columns.Contains("DC1"))
                    dt.Columns.Add("DC1", typeof(string));
                if (!dt.Columns.Contains("DC2"))
                    dt.Columns.Add("DC2", typeof(string));

                if (!dt.Columns.Contains("TP_BUSI"))
                    dt.Columns.Add("TP_BUSI", typeof(string));
                if (!dt.Columns.Contains("TP_GI"))
                    dt.Columns.Add("TP_GI", typeof(string));
                if (!dt.Columns.Contains("TP_IV"))
                    dt.Columns.Add("TP_IV", typeof(string));
                if (!dt.Columns.Contains("GIR"))
                    dt.Columns.Add("GIR", typeof(string));      //
                if (!dt.Columns.Contains("GI"))
                    dt.Columns.Add("GI", typeof(string));       //
                if (!dt.Columns.Contains("IV"))
                    dt.Columns.Add("IV", typeof(string));       //
                if (!dt.Columns.Contains("TRADE"))
                    dt.Columns.Add("TRADE", typeof(string));
                //if (!dt.Columns.Contains("UMVAT_SO"))
                //    dt.Columns.Add("UMVAT_SO", typeof(decimal));
                //if (!dt.Columns.Contains("AMVAT_SO"))
                //    dt.Columns.Add("AMVAT_SO", typeof(decimal));
                if (!dt.Columns.Contains("SEQ_PROJECT"))
                    dt.Columns.Add("SEQ_PROJECT", typeof(decimal));
                if (!dt.Columns.Contains("NO_PO_PARTNER"))
                    dt.Columns.Add("NO_PO_PARTNER", typeof(string));
                if (!dt.Columns.Contains("CD_SHOP"))
                    dt.Columns.Add("CD_SHOP", typeof(string));
                if (!dt.Columns.Contains("CD_SPITEM"))
                    dt.Columns.Add("CD_SPITEM", typeof(string));
                if (!dt.Columns.Contains("CD_OPT"))
                    dt.Columns.Add("CD_OPT", typeof(string));
                if (!dt.Columns.Contains("RT_DSCNT"))
                    dt.Columns.Add("RT_DSCNT", typeof(decimal));
                if (!dt.Columns.Contains("UM_BASE"))
                    dt.Columns.Add("UM_BASE", typeof(decimal));


                if (!dt.Columns.Contains("TP_VAT"))
                    dt.Columns.Add("TP_VAT", typeof(string));   //
                if (!dt.Columns.Contains("RT_VAT"))
                    dt.Columns.Add("RT_VAT", typeof(decimal));
                if (!dt.Columns.Contains("FG_USE"))
                    dt.Columns.Add("FG_USE", typeof(string));   //
                if (!dt.Columns.Contains("CD_CC"))
                    dt.Columns.Add("CD_CC", typeof(string));    //
                if (!dt.Columns.Contains("NM_CC"))
                    dt.Columns.Add("NM_CC", typeof(string));    //

                //SA_SOL_DLV 때문에 추가된 컬럼 스키마~ ^^
                if (!dt.Columns.Contains("NM_CUST_DLV"))
                    dt.Columns.Add("NM_CUST_DLV", typeof(string));
                if (!dt.Columns.Contains("CD_ZIP"))
                    dt.Columns.Add("CD_ZIP", typeof(string));
                if (!dt.Columns.Contains("ADDR1"))
                    dt.Columns.Add("ADDR1", typeof(string));
                if (!dt.Columns.Contains("ADDR2"))
                    dt.Columns.Add("ADDR2", typeof(string));
                if (!dt.Columns.Contains("NO_TEL_D1"))
                    dt.Columns.Add("NO_TEL_D1", typeof(string));
                if (!dt.Columns.Contains("NO_TEL_D2"))
                    dt.Columns.Add("NO_TEL_D2", typeof(string));
                if (!dt.Columns.Contains("TP_DLV"))
                    dt.Columns.Add("TP_DLV", typeof(string));
                if (!dt.Columns.Contains("TP_DLV_DUE"))
                    dt.Columns.Add("TP_DLV_DUE", typeof(string));
                if (!dt.Columns.Contains("DC_REQ"))
                    dt.Columns.Add("DC_REQ", typeof(string));
                if (!dt.Columns.Contains("FG_TRACK"))
                    dt.Columns.Add("FG_TRACK", typeof(string));
                if (!dt.Columns.Contains("RMA_REASON"))
                    dt.Columns.Add("RMA_REASON", typeof(string));   //2011.06.22 반품사유 컬럼 추가
                if (!dt.Columns.Contains("YN_ATP"))
                    dt.Columns.Add("YN_ATP", typeof(string));       //2011.07.22 ATP사용여부 컬럼 추가
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }
        #endregion

        #endregion

        #region ♣ 환종이 바뀌면 환율조정을 해준다. : 부가세 포함 로직은 무조건 막혀서 이 이벤트를 탈 일이 없넹 
                                                  //: 여기는 무조건 환종이 국내이므로 환율을 계산할 필요가 없당.~
        private void cbo_CdExch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbo_CdExch.SelectedValue.ToString() == "000")
            {
                txt_RtExch.DecimalValue = 1;
                txt_RtExch.Enabled = false;
            }
            else
            {
                txt_RtExch.Enabled = true;
            }
        }
        #endregion

        #region ♣ 수주형태 : 수주형태에 따른 해당 거래유형의 코드값에 따라 부가세구분을 Setting
        private void bp_SoType_TextChanged(object sender, EventArgs e)
        {
            string[] tp_Busi = null;        //거래구분 

            try
            {
                object[] obj = new object[3];

                obj[0] = LoginInfo.CompanyCode;                 //회사
                obj[1] = bp_SoType.CodeValue.ToString();        //수주형태
                obj[2] = cbo_TpVat.SelectedValue.ToString();    //VAT 구분

                tp_Busi = _biz.GetTpBusi(obj);

                if (tp_Busi[0] == "001")
                {
                    cbo_TpVat.SelectedValue = "11";
                    cbo_TpVat.Enabled = true;
                }
                else if (tp_Busi[0] == "002" || tp_Busi[0] == "003")
                {
                    cbo_TpVat.SelectedValue = "14";
                    cbo_TpVat.Enabled = false;
                }
                else if (tp_Busi[0] == "004" || tp_Busi[0] == "005")
                {
                    cbo_TpVat.SelectedValue = "15";
                    cbo_TpVat.Enabled = false;
                }
                else
                {
                    ShowMessage("수주유형에 해당하는 해당 거래구분이 존재하지 않습니다.");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ VAT구분
        private void cbo_TpVat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_TpVat.SelectedValue == null || cbo_TpVat.SelectedValue.ToString() == string.Empty || cbo_TpVat.DataSource == null)
            {
                txt_TpVat.DecimalValue = 0;
                return;
            }

            DataTable dt = (DataTable)cbo_TpVat.DataSource;
            DataRow row = dt.Rows.Find(cbo_TpVat.SelectedValue);

            if (row != null)
                txt_TpVat.DecimalValue = D.GetDecimal(row["CD_FLAG1"]);
            else
                txt_TpVat.DecimalValue = 0;
        }
        #endregion

        #region ♣ Control_QueryBefore
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                //수주유형 : P61_CODE1 : 반품여부, P62_CODE2 : 사용유무
                case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB: //수주형태
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break; 
                default:
                    break;
            }
        }
        #endregion

        #region ♣ Control_QueryAfter
        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB: //수주형태
                        if (e.HelpReturn.Rows[0]["CONF"].ToString() == "Y") //자동승인여부 "Y" 자동승인, "N" 자동승인안됨
                            soStatus = "R";
                        else
                            soStatus = "O";

                        //수주상태 "O" : 수주등록,  "R" : 수주확정, 고객남품의뢰
                        //라인에 수주상태값을 넣어준다.
                        //이미라인이 있는경우에 수주형태 도움창은 열어볼 수 없음으로 걱정할거 없다. : 복사일때에도 상관없음
                        if (_flex.DataTable != null)
                        {
                            foreach (DataRow row in _flex.DataTable.Rows)
                            {
                                if (row.RowState != DataRowState.Deleted)
                                    row["STA_SO"] = soStatus;
                            }
                        }

                        tp_Gi = e.HelpReturn.Rows[0]["TP_GI"].ToString();     //출하형태
                        tp_Busi = e.HelpReturn.Rows[0]["TP_BUSI"].ToString(); //거래구분
                        tp_Iv = e.HelpReturn.Rows[0]["TP_IV"].ToString();     //매출형태
                        _의뢰여부 = e.HelpReturn.Rows[0]["GIR"].ToString();
                        _출하여부 = e.HelpReturn.Rows[0]["GI"].ToString();
                        _매출여부 = e.HelpReturn.Rows[0]["IV"].ToString();
                        trade = e.HelpReturn.Rows[0]["TRADE"].ToString();     //수출여부
                        _반품여부 = D.GetString(e.HelpReturn.Rows[0]["RET"]); //반품여부

						DataRow row수주형태 = BASIC.GetTPSO(e.CodeValue);
                        _매출자동여부 = D.GetString(row수주형태["IV_AUTO"]);

                        //수주라인에 cost center 추가 2009.09.18
                        if (Sa_Global.IVL_CdCc == "001")
                        {
                            object[] obj_Tpso = new object[2];
                            obj_Tpso[0] = LoginInfo.CompanyCode;                        //회사
                            obj_Tpso[1] = e.HelpReturn.Rows[0]["TP_SO"].ToString();     //수주형태
                            DataRow dr = Sa_ComFunc.GetTPSO(obj_Tpso);

                            cd_CC = D.GetString(dr["CD_CC"].ToString());    //시스템 통제설정이 수주유형의 C/C를 가져올때 셋팅  
                            nm_CC = D.GetString(dr["NM_CC"].ToString());
                        }

                        break; 

                    case Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB: //영업구매그룹
                        tp_SalePrice = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString(); //단가적용형태

                        object[] obj = new object[2];
                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                        obj[1] = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
                        so_Price = _biz.GetSaleOrgUmCheck(obj);

                        //시스템 통제설정이 영업그룹의 C/C를 가져올때 셋팅
                        if (Sa_Global.IVL_CdCc == "000")
                        {
                            cd_CC = ComFunc.MasterSearch("MA_CC", obj);

                            obj[1] = cd_CC;
                            nm_CC = ComFunc.MasterSearch("MA_CC_NAME", obj);
                        }
                        break;
                    case HelpID.P_USER:
                        if (e.ControlName == "bp_PJT")
                        {
                            bp_SalesGroup.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
                            bp_SalesGroup.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
                            bp_SalesGroup.Enabled = false;
                            Partner = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
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

        #region ♣ Control_CodeChanged
        private void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Name == "bp_PJT")
                {
                    if (bp_PJT.CodeValue == string.Empty && App.SystemEnv.PROJECT사용)
                    {
                        Partner = string.Empty;
                        bp_SalesGroup.CodeValue = string.Empty;
                        bp_SalesGroup.CodeName = string.Empty;
                        bp_SalesGroup.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 EVENT

        #region ♣ _flex_StartEdit
        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                if (so_Price == "Y")
                {
                    if (_flex.Cols[e.Col].Name == "UM_SO" ||
                        _flex.Cols[e.Col].Name == "UMVAT_SO" ||
                        _flex.Cols[e.Col].Name == "AM_SO" ||
                        _flex.Cols[e.Col].Name == "AMVAT_SO")
                    {
                        ShowMessage("영업단가통제된 영업그룹입니다.");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 단가이력 도움창
        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try                                                                                                                                                     
            { 
                //라인행이 존재하지 않으면 FALSE 시킨다.
                if (!_flex.HasNormalRow) return;

                //단가 컬럼에 수정 가능한 진행상태 일때만~
                if ((_flex.Cols[_flex.Col].Name == "UM_SO" || _flex.Cols[_flex.Col].Name == "UMVAT_SO") && 
                    (_flex["STA_SO"].ToString() == "" || _flex["STA_SO"].ToString() == "O"))
                {
                    //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                    if (so_Price == "Y")
                    {
                        //ShowMessage("영업단가통제된 영업그룹입니다."); //여기서 메세지 처리를 해버리면~ _flex_StartEdit 에서 한것까지 2번 메세지 처리가 되삔당.~
                        return;
                    }

                    P_SA_UM_HISTORY_SUB dlg = new P_SA_UM_HISTORY_SUB(D.GetString(_flex["CD_PARTNER"]), 
                                                                      D.GetString(_flex["LN_PARTNER"]), 
                                                                      bp_SoType.CodeValue, 
                                                                      bp_SoType.CodeName,
                                                                      D.GetString(_flex["CD_PLANT"]),
                                                                      D.GetString(_flex["CD_ITEM"]),
                                                                      D.GetString(_flex["NM_ITEM"]),
                                                                      D.GetString(cbo_CdExch.SelectedValue));

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        /*계산로직*/
                        //부가세포함여부가 FG_VAT = "Y" 일때 부가세포함 금액계산
                        //구해지는 단가는 부가세포함(소비자금액)으로 입력하는 곳은 단가적용도 소비자 단가 적용이다.
                        _flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, dlg.단가);
                        _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["UMVAT_SO"]));
                        _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]));
                        _flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                        _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / (D.GetDecimal(_flex["QT_SO"]) == 0 ? 1 : D.GetDecimal(_flex["QT_SO"])));
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 유효성 체크 == OnChange();Onblur();
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (ColName)
                {
                    case "CD_PLANT": //그리드에 공장이 바뀌면 초기화 시켜줘야 하는 것들   
                        _flex[e.Row, "CD_ITEM"] = string.Empty;
                        _flex[e.Row, "NM_ITEM"] = string.Empty;
                        _flex[e.Row, "STND_ITEM"] = string.Empty;
                        _flex[e.Row, "UNIT_SO"] = string.Empty;
                        _flex[e.Row, "UNIT_IM"] = string.Empty;
                        _flex[e.Row, "TP_ITEM"] = string.Empty;
                        _flex[e.Row, "DT_REQGI"] = string.Empty;
                        _flex[e.Row, "QT_IM"] = 0;
                        _flex[e.Row, "CD_SL"] = string.Empty;
                        _flex[e.Row, "NM_SL"] = string.Empty;
                        break;

                    case "DT_DUEDATE":
                    case "DT_REQGI":
                        if (newValue == "") return;

                        if (!_flex.IsDate(ColName))
                        {
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            if (_flex.Editor != null)
                            {
                                _flex.Editor.Text = string.Empty;
                            }
                            e.Cancel = true;
                            return;
                        }
                        //if (newValue.Trim().Length != 8)
                        //{
                        //    ShowMessage(공통메세지.입력형식이올바르지않습니다);
                        //    if (_flex.Editor != null)
                        //    {
                        //        _flex.Editor.Text = string.Empty;
                        //    }
                        //    e.Cancel = true;
                        //    return;
                        //}
                        //else
                        //{
                        //    if (!_flex.IsDate(ColName))
                        //    {
                        //        ShowMessage(공통메세지.입력형식이올바르지않습니다);
                        //        if (_flex.Editor != null)
                        //        {
                        //            _flex.Editor.Text = string.Empty;
                        //        }
                        //        e.Cancel = true;
                        //        return;
                        //    }
                        //}
                        break;

                    case "TP_VAT":
                        //부가세구분 Setting 에 따른 부가세율 Setting
                        object[] obj_Vat = new object[3];
                        obj_Vat[0] = LoginInfo.CompanyCode;     //회사
                        obj_Vat[1] = bp_SoType.CodeValue;          //수주형태
                        obj_Vat[2] = newValue;                  //VAT 구분

                        _flex[e.Row, "RT_VAT"] = D.GetDecimal(Sa_ComFunc.GetTpBusi(obj_Vat)[1].ToString());
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100));
                        _flex[e.Row, "AMVAT_SO"] = D.GetDecimal(_flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);

                        if (D.GetDecimal(newValue) != 0)
                            _flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(_flex[e.Row, "QT_SO"]));
                        else
                            _flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMVAT_SO"]));

                        break;
 
                    case "QT_SO":
                        _flex[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(_flex[e.Row, "UNIT_SO_FACT"]) == 0 ? 1 : _flex[e.Row, "UNIT_SO_FACT"];

                        _flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UMVAT_SO"]) * D.GetDecimal(newValue));
                        _flex[e.Row, "AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[e.Row, "RT_VAT"]))));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flex[e.Row, "AM_WONAMT"]));
                        _flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                        _flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_SO"]) / (D.GetDecimal(newValue) == 0 ? 1 : D.GetDecimal(newValue)));
                         
                        _flex[e.Row, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex[e.Row, "UNIT_SO_FACT"]));

                        break;

                    case "UMVAT_SO":
                        _flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_SO"]) * D.GetDecimal(newValue));
                        _flex[e.Row, "AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[e.Row, "RT_VAT"]))));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flex[e.Row, "AM_WONAMT"]));
                        _flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                        _flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_SO"]) / (D.GetDecimal(_flex[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flex[e.Row, "QT_SO"])));

                        break;

                    case "AMVAT_SO":
                        if (D.GetDecimal(_flex[e.Row, "QT_SO"]) != 0)
                            _flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) / D.GetDecimal(_flex[e.Row, "QT_SO"]));
                        else
                            _flex[e.Row, "UMVAT_SO"] = 0;

                        _flex[e.Row, "AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[e.Row, "RT_VAT"]))));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flex[e.Row, "AM_WONAMT"]));
                        _flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                        _flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_SO"]) / (D.GetDecimal(_flex[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flex[e.Row, "QT_SO"])));

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

        #region ♣ 그리드 도움창 셋팅

        #region ♣ _flex_BeforeCodeHelp
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (_flex["STA_SO"].ToString() != "" && _flex["STA_SO"].ToString() != "O")
                {
                    e.Cancel = true;
                    return;
                }

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                    case "CD_SL":
                        if (_flex[_flex.Row, "CD_PLANT"].ToString().Equals(""))
                            e.Cancel = true;
                        else
                        {
                            //공장품목셋팅
                            e.Parameter.P09_CD_PLANT = _flex["CD_PLANT"].ToString();
                        }
                        break;

                    case "GI_PARTNER":    //거래처 코드를 넘기는 이유는 해당 거래처의 납품처를 반환하기 땜에 그런당.
                        e.Parameter.P14_CD_PARTNER = _flex["CD_PARTNER"].ToString();
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

        #region ♣ _flex_AfterCodeHelp
        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;
                string dueReqDt = "";   //출하예정일 구할거얌~ 

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":

                        if (e.Result.DialogResult == DialogResult.Cancel) return;

                        // 최초 행추가시에 일어나고 차후엔 아래에서 행추가가 일어나므로 제거 해도 된다.
                        if (_flex.Rows.Count > 2 && _flex["DT_DUEDATE"].ToString() != "")
                            dueReqDt = _flex["DT_DUEDATE"].ToString();

                        //단가적용에 사용할 배열변수
                        object[] obj = new object[7];
                        obj[0] = LoginInfo.CompanyCode;                 //회사
                        obj[1] = "";                                    //품목코드
                        obj[2] = _flex["CD_PARTNER"].ToString();        //거래처코드
                        obj[3] = cbo_TpPrice.SelectedValue.ToString();  //단가유형
                        obj[4] = cbo_CdExch.SelectedValue.ToString();   //화폐단위
                        obj[5] = tp_SalePrice;                          //단가적용형태
                        obj[6] = dat_Sodt.Text;                         //수주일자

                        int roopCnt = 0;

                        //작업하는 동안 화면 깜박 거리지 않게 기다리다가 나중에 한방에 처리하도록 하는 부분
                        _flex.Redraw = false;

                        foreach (DataRow row in helpReturn.Rows)
                        {
                            int cnt = _flex.Rows.Count;

                            //멀티도움창일경우 두번째부터는 행을 추가하고 추가된 행에 초기값을 준다.
                            //단일도움창일경우에는 현제 추가된 행에 초기값을 준다.
                            if (ItemHelpName == "P_MA_PITEM_SUB1" && roopCnt > 0)
                            {
                                //_flex.Rows.Add(); 여기서 행 추가하지 말고 행 추가 이벤트를 써서 사용해야 코딩량이 줄어~
                                btn_AddRow_Click(sender, e);

                                _flex.Row = cnt;
                            }

                            _flex[_flex.Row, "CD_ITEM"] = row["CD_ITEM"];
                            _flex[_flex.Row, "NM_ITEM"] = row["NM_ITEM"];
                            _flex[_flex.Row, "STND_ITEM"] = row["STND_ITEM"];
                            _flex[_flex.Row, "UNIT_SO"] = row["UNIT_SO"];
                            _flex[_flex.Row, "UNIT_IM"] = row["UNIT_IM"];
                            _flex[_flex.Row, "TP_ITEM"] = row["TP_ITEM"];
                            _flex[_flex.Row, "CD_SL"] = row["CD_GISL"];
                            _flex[_flex.Row, "NM_SL"] = row["NM_GISL"];
                            _flex[_flex.Row, "LT_GI"] = D.GetDecimal(row["LT_GI"]);
                            _flex[_flex.Row, "YN_ATP"] = row["YN_ATP"];

                            _flex[_flex.Row, "CD_CC"] = cd_CC;
                            _flex[_flex.Row, "NM_CC"] = nm_CC;

                            _flex[_flex.Row, "TP_VAT"] = cbo_TpVat.SelectedValue.ToString();
                            _flex[_flex.Row, "RT_VAT"] = D.GetDecimal(_flex[e.Row, "RT_VAT"]); 

                            _flex[_flex.Row, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];

                            if (dueReqDt != "")
                                _flex[_flex.Row, "DT_REQGI"] = _CommFun.DateAdd(dueReqDt, "D", Convert.ToInt32(_flex[_flex.Row, "LT_GI"]) * -1);

                            if (tp_SalePrice == "002" || tp_SalePrice == "003")
                            {
                                obj[1] = row["CD_ITEM"];

                                _flex[_flex.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, _biz.UmSearch(obj));
                            }

                            //단일도움창일경우와 멀티도움창 첫 행에만 적용
                            if (ItemHelpName != "P_MA_PITEM_SUB1" || (ItemHelpName == "P_MA_PITEM_SUB1" && roopCnt == 0))
                            { 
                                _flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["UMVAT_SO"]) * D.GetDecimal(_flex["QT_SO"]));
                                _flex["AM_WONAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flex[e.Row, "RT_VAT"]))));
                                _flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AMVAT_SO"]) - D.GetDecimal(_flex["AM_WONAMT"]));
                                _flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_WONAMT"])); //무조건 환종이 국내 이므로 자국내 거래가 환률이 발생할 일이 있것냐~ㅋ : 그래서 환율 안 곱했음
                                _flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_SO"]) / (D.GetDecimal(_flex["QT_SO"]) == 0 ? 1 : D.GetDecimal(_flex["QT_SO"])));
 
                                btn_DelRow.Enabled = true;
                            }

                            roopCnt++;
                        }

                        _flex.Redraw = true;

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

        #endregion

        #endregion

        void btn_ATP_Click(object sender, EventArgs e)
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
                MsgEnd(ex);
            }
        }

        #region -> ATP관련

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

            bool ATPGood = ATP.ATP_Check(drs, out s_Message);

            if (ATPGood == false)
            {
                if (자동체크 == false)
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
    }
}