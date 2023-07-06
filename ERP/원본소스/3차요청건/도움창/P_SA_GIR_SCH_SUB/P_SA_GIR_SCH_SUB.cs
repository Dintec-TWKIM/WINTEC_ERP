using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;

namespace sale
{
    // **************************************
    // 작   성   자 : NJin
    // 재 작  성 일 : 2009-03-19
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하의뢰관리
    // 페 이 지  명 : 납품의뢰 조회 도움창
    // 프로젝트  명 : P_SA_GIR_SCH_SUB
    // **************************************
    public partial class P_SA_GIR_SCH_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region ♣ 생성자 & 변수 선언

        private P_SA_GIR_SCH_SUB_BIZ _biz = new P_SA_GIR_SCH_SUB_BIZ();
        private string _fgIo = string.Empty;

        public P_SA_GIR_SCH_SUB(string YN_RETURN)
        {
            try
            {
                InitializeComponent();
                _flexH.DetailGrids = new FlexGrid[] { _flexL };
                txt_Return.Text = YN_RETURN; //반품여부
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region ♣ InitLoad
        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
        }
        #endregion

        #region ♣ InitGrid : 그리드 초기화
        private void InitGrid()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("NO_GIR", "의뢰번호", 100);
            _flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("LN_PARTNER", "거래처", 100);
            _flexH.SetCol("NM_KOR", "의뢰자", 100);
            _flexH.SetCol("CD_PLANT", "공장", 100);
            _flexH.SetCol("TP_BUSI", "거래구분", 80);
            _flexH.SetCol("DC_RMK", "비고", 160);
            _flexH.ExtendLastCol = true;
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.HelpClick += new EventHandler(_flexH_HelpClick);

            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("CD_ITEM", "품목코드", 120);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 65);
            _flexL.SetCol("QT_GIR", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UNIT", "단위", 65);
            if (_fgIo == string.Empty)
            {
                _flexL.SetCol("UM", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("AM_GIR", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                _flexL.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            }
            _flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("STA_GIR", "의뢰상태", 60);
            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flexL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("NO_SO", "수주번호", 100);
            _flexL.SetCol("NO_PROJECT", "프로젝트", 100, false);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 100, false);
            _flexL.SettingVersion = "1.0.0.2";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (_fgIo == string.Empty)
                _flexL.SetExceptSumCol("UM");

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.LoadUserCache("P_SA_GIR_SCH_SUB_flexH");
            _flexL.LoadUserCache("P_SA_GIR_SCH_SUB_flexL");

        }
        #endregion

        #region ♣ InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet g_dsCombo = Global.MainFrame.GetComboData("S;MA_PLANT", "S;PU_C000016", "S;SA_B000016");

            //공장
            cbo_Plant.DataSource = g_dsCombo.Tables[0];
            cbo_Plant.DisplayMember = "NAME";
            cbo_Plant.ValueMember = "CODE";

            _flexH.SetDataMap("CD_PLANT", g_dsCombo.Tables[0], "CODE", "NAME");     //공장
            _flexH.SetDataMap("TP_BUSI", g_dsCombo.Tables[1], "CODE", "NAME");      //거래구분
            _flexL.SetDataMap("STA_GIR", g_dsCombo.Tables[2], "CODE", "NAME");      //수주상태

            dt_Gir_From.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dt_Gir_To.Text = Global.MainFrame.GetStringToday;
        }
        #endregion

        #endregion

        #region ♣ 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            Hashtable hList = new Hashtable();

            hList.Add(dt_Gir_From, lbl_의뢰일자);
            hList.Add(dt_Gir_To, lbl_의뢰일자);

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region ♣ 조회버튼클릭
        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Check()) return;

                object[] obj = new object[9];

                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                obj[1] = D.GetString(cbo_Plant.SelectedValue);      // 공장
                obj[2] = bp_Partner.CodeValue;                      //거래처
                obj[3] = dt_Gir_From.Text;                          //의뢰일자From
                obj[4] = dt_Gir_To.Text;                            //의뢰일자To
                obj[5] = txt_Return.Text;                           //반품여부
                obj[6] = _fgIo;                                     //수불구분(판매반품정리의뢰등록에서 세팅)
                obj[7] = bp프로젝트.CodeValue;                      //프로젝트
                obj[8] = Global.MainFrame.LoginInfo.EmployeeNo;     //사원번호

                DataTable dt = _biz.Search(obj);

                _flexH.Binding = dt;

                if (!_flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 적용버튼클릭
        private void btn_Append_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                    
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 닫기 이벤트
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_SA_GIR_SCH_SUB_flexH");
            _flexL.SaveUserCache("P_SA_GIR_SCH_SUB_flexL");
        }
        #endregion

        #region ♣ Grid EVENT

        #region ♣ _flexH_AfterRowChange
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string Key = D.GetString(_flexH[e.NewRange.r1, "NO_GIR"]);
                string Filter = "NO_GIR = '" + Key + "'";

                if (_flexH.DetailQueryNeed)
                {
                    object[] obj = new object[2];
                    obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                    obj[1] = Key;                                       //의뢰번호

                    dt = _biz.SearchDetail(obj);
                }

                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flexH_HelpClick
        private void _flexH_HelpClick(object sender, EventArgs e)
        {
            try
            {
                btn_Append_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 결과값 리턴해줄 속성값
        public string[] returnParams
        {
            get
            {
                //나중에 추가로 넘겨줄 속성이 있을지 모르니 배열로 받아서 처리해놨음
                string[] Param = new string[9];

                Param[0] = D.GetString(_flexH["NO_GIR"]);
                Param[1] = D.GetString(_flexH["CD_PLANT"]);

                return Param;
            }
        }
        #endregion 

        public string SetFgIo { set { _fgIo = value; } }
    }
}