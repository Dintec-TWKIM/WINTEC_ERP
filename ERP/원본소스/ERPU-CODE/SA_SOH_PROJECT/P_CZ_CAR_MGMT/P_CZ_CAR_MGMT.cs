/**
 * 
 * 배차관리 시스템 ver1.0 
 * 1. 그룹 : 배차관리
 * 2. 제목 : 비상연락망
 * 3. 상세설명 : 배차관리를 위한 비상연락망
 * 4. 작성일자 : 2013-12-19
 * autor by Duzon
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;



namespace cz
{
    public partial class P_CZ_CAR_MGMT : PageBase
    {
        #region 생성자
        public P_CZ_CAR_MGMT()
        {
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { _flex, _flex2 };
            _flex.DetailGrids = new FlexGrid[] { _flex2 };
        }
        #endregion

        //초기화
        P_CZ_CAR_MGMT_BIZ _biz = new P_CZ_CAR_MGMT_BIZ();

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitGridL();
            this.InitEvent();
            this.InitPaint();
        }

        private void InitGrid()
        {


            _flex.BeginSetting(1, 1, true);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("NO_EMP", "운전자", 80, true);
            _flex.SetCol("CAR_NO", "차량번호", 130, false);
            _flex.SetCol("NM_CAR", "차랑명", 100, true);
            _flex.SetCol("CD_PARTNER", "상호", 150, false);
            _flex.SetCol("LN_PARTNER", "상호명", 150, false);
            _flex.SetCol("NO_HP", "핸드폰", 150);
            _flex.SetCol("NO_TEL", "전화", 150);
            _flex.SetCol("PWHP", "파워텔", 150);
            _flex.SetCol("DTS_INSERT", "생성일자", 100);
            _flex.SetCol("CD_COMPANY", "회사코드", 100);
            _flex.SetCol("ID", "아이디", 100);
            _flex.SetCol("PWD", "비밀번호", 100);

            _flex.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["CAR_NO"].TextAlign = TextAlignEnum.LeftCenter;
            _flex.Cols["NM_CAR"].TextAlign = TextAlignEnum.LeftCenter;
            _flex.Cols["LN_PARTNER"].TextAlign = TextAlignEnum.LeftCenter;

            _flex.SetBinding(pnlDetail, new object[] { txt차량번호 });
            _flex.SetBinding(pnlDetail, new object[] { txt차량명 });
            _flex.SetBinding(pnlDetail, new object[] { txt상호 });
            _flex.SetBinding(pnlDetail, new object[] { txt운전자 });
            _flex.SetBinding(pnlDetail, new object[] { txt핸드폰번호 });
            _flex.SetBinding(pnlDetail, new object[] { txt파워텔 });
            _flex.SetBinding(pnlDetail, new object[] { txt전화번호 });


  

            //// 라디오버튼 바인딩
            //_flex.SetBindningRadioButton(new RadioButtonExt[] { rdo직영구분Y, rdo직영구분N }, new string[] { "Y", "N" });
            //OnBpControl_CodeChanged

            //bpc운전자.BpControlMode = new BpControlMode();


            _flex.VerifyPrimaryKey = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyAutoDelete = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyNotNull = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO", "ID", "PWD" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.



            //팝업함수정보가 필요
            _flex.SetCodeHelpCol("NO_EMP", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "NO_HP", "NO_TEL", "PWHP", "CD_PARTNER", "LN_PARTNER", "CD_COMPANY" }, new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "NO_HP", "NO_TEL", "PWHP", "CD_PARTNER", "LN_PARTNER", "CD_COMPANY" });

            //this._flex.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("NM_COMPANY", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "NM_COMPANY", "CD_COMPANY" }, new string[] { "NM_COMPANY", "CD_COMPANY" });
            this._flex.SetCodeHelpCol("NM_PLANT", HelpID.P_MA_PLANT_SUB, ShowHelpEnum.Always, new string[] { "NM_PLANT", "CD_PLANT" }, new string[] { "NM_PLANT", "CD_PLANT" });

            _flex.SettingVersion = "0.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

        }


        private void InitGridL()
        {
            _flex2.BeginSetting(1, 1, true);

            _flex2.SetCol("DT_PRC_FINAL", "최종수행일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex2.SetCol("CD_PLAN", "관리코드", 80, true);
            //flex2.SetCol("NM_PLAN", "계획명", 100, false);
            _flex2.SetCol("COST", "비용", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex2.SetCol("DIVISION_REQ", "분할청구", 80, true, typeof(string));
            _flex2.SetCol("DC_RMK2", "비고", 150);
            _flex2.SetCol("CD_COMPANY", "회사", 80);
            _flex2.SetCol("CD_PARTNER", "거래처", 80);
            _flex2.SetCol("CAR_NO", "차량번호", 80, false);
            _flex2.SetCol("NM_CAR", "차랑명", 80, false);

            _flex2.Cols["COST"].Format = "0.00";

            _flex2.Cols["DT_PRC_FINAL"].TextAlign = TextAlignEnum.CenterCenter;
            //_flex2.Cols["NM_PLAN"].TextAlign = TextAlignEnum.LeftCenter;
            _flex2.Cols["COST"].TextAlign = TextAlignEnum.RightCenter;
            _flex2.Cols["DIVISION_REQ"].TextAlign = TextAlignEnum.CenterCenter;
            _flex2.Cols["DC_RMK2"].TextAlign = TextAlignEnum.LeftCenter;


            _flex2.SetDataMap("CD_PLAN", MA.GetCode("CZ_Z000003"), "CODE", "NAME");
            //_flex2.SetDataMap("NM_PLAN", MA.GetCode("MA_B000082"), "CODE", "NAME");
            _flex2.SetDataMap("DIVISION_REQ", MA.GetCode("SU_OP00001"), "CODE", "NAME");




            //팝업함수정보가 필요 
            //this._flex2.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });
            //this._flex.SetCodeHelpCol("CAR_NO", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "CD_MAN", "NM_MAN" }, new string[] { "CODE", "NAME" });
            //_flex.SetCodeHelpCol("NO_EMP", Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_KOR" }, new string[] { "NO_EMP", "NM_KOR" }, ResultMode.FastMode);

            _flex2.VerifyPrimaryKey = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flex2.VerifyAutoDelete = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex2.VerifyNotNull = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.

            //_flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);

            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.SettingVersion = "1.0.0.1";
            _flex2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

        }

        private void InitEvent()
        {
            //그리드이벤트
            _flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

            //화면버튼이벤트
            btn추가.Click += new EventHandler(btn추가_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);
        }

        protected override void InitPaint()
        { 
            base.InitPaint();

            SetControl set = new SetControl();

            dtpFrom.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtpFrom.Text = Global.MainFrame.GetStringFirstDayInMonth;

            dtpTo.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtpTo.Text = Global.MainFrame.GetStringToday;

            set.SetCombobox(cbo탱크재질, MA.GetCode("CZ_Z000013", true));
            set.SetCombobox(cbo직영구분, MA.GetCode("MA_B000057", true));
            set.SetCombobox(cbo차량종류, MA.GetCode("CZ_Z000002", true));

            set.SetCombobox(cbo업체구분, MA.GetCode("CZ_Z000004", true));
            set.SetCombobox(cbo운송화물형태, MA.GetCode("CZ_Z000005", true));
            set.SetCombobox(cbo운송품목, MA.GetCode("CZ_Z000006", true));
            set.SetCombobox(cbo차량구분, MA.GetCode("CZ_Z000007", true));
            set.SetCombobox(cbo업태, MA.GetCode("CZ_Z000008", true));
            set.SetCombobox(dropDownComboBox5, MA.GetCode("CZ_Z000009", true));


            //사용여부
            DataSet comboData = base.GetComboData(new string[] { "S;CZ_Z000003" }); //MA_B000082

            this.cbo계획명.DataSource = comboData.Tables[0];
            this.cbo계획명.DisplayMember = "NAME";
            this.cbo계획명.ValueMember = "CODE";

            //사용여부
            DataSet comboData1 = base.GetComboData(new string[] { "S;SU_OP00001" }); //MA_B000082

            this.cbo분할청구.DataSource = comboData1.Tables[0];
            this.cbo분할청구.DisplayMember = "NAME";
            this.cbo분할청구.ValueMember = "CODE";




        }

        #region 값 점검

        //bool Chk회사정보
        //{
        //    get
        //    {
        //        return !Checker.IsEmpty(D.GetString(_flex["CD_COMPANY"]), DD("회사정보"));
        //    }
        //}

        #endregion

        #region 점검 메소드

        protected override bool BeforeSearch()
        {
            //if (!base.BeforeSearch() || !Chk차량정보) return false;   데이터가 많으면 차량정보를 필수 값으로 걸어줘야 함
            return true;
        }

        protected override bool BeforeSave()
        {
            if (!Verify()) return false;    // 그리드 체크

            return true;
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트 ♬

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDetail.Enabled = _flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 상단 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (!BeforeSearch()) return;


                //if (tab2.CanFocus)
                //{
                //    _biz.SearchL(CD_COMPANY, CD_PARTNER, CAR_NO, ctx계획명.Text);
                //    _flex.Binding = _biz.Search(ctx상호.CodeValue, dtpFrom.Text, dtpTo.Text, ctx분할청구.CodeValue, ctx차량번호.Text, ctx계획명.Text, ctx운전자.CodeValue);
                //    _flex2.Binding = _biz.SearchL(D.GetString(_flex["CD_COMPANY"]), D.GetString(_flex["CD_PARTNER"]), D.GetString(_flex["CAR_NO"]), ctx계획명.Text);
                //}
                //else
                //{
                //    _flex.Binding = _biz.Search(ctx상호.CodeValue, dtpFrom.Text, dtpTo.Text, ctx분할청구.CodeValue, ctx차량번호.Text, ctx계획명.Text, ctx운전자.CodeValue);
                //}
                
                _flex.Binding = _biz.Search(ctx상호.CodeValue, ctx차량번호.Text, ctx운전자.Text);

                if (!_flex.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);

                //MsgControl.CloseMsg;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 상단 추가버튼

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;

                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 상단 삭제버튼

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete() || !_flex.HasNormalRow) return;

                DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
                if (result == DialogResult.Yes)
                {
                    foreach (DataRow row in dr)
                    {
                        _biz.Delete(D.GetString(row["CD_COMPANY"]), D.GetString(row["CD_PARTNER"]), D.GetString(row["CAR_NO"]));
                    }

                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 상단 저장버튼

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                //MessageBox.Show(cbo차량종류.SelectedValue.ToString());

                if (MsgAndSave(PageActionMode.Save))
                    //ShowMessage(PageResultMode.SaveGood);
                    ShowMessage("자료가 정상처리되었습니다");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion


        #region ♪ 저장 관련     ♬

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify()) return false;

            _biz.Save(_flex.GetChanges(), _flex2.GetChanges());
            _flex.AcceptChanges();
            _flex2.AcceptChanges();
            return true;
        }

        #endregion

        #region 화면버튼

        void btn추가_Click(object sender, EventArgs e)
        {
            try
            {


                if (!_flex.HasNormalRow) return;

                _flex2.Rows.Add();
                _flex2.Row = _flex2.Rows.Count - _flex2.Rows.Fixed;

                _flex2["CD_COMPANY"] = _flex["CD_COMPANY"];
                _flex2["CD_PARTNER"] = _flex["CD_PARTNER"];
                _flex2["CAR_NO"] = _flex["CAR_NO"];
                _flex2["NM_CAR"] = _flex["NM_CAR"];

                _flex2.AddFinished();
                _flex2.Focus();


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void btn삭제_Click(object sender, EventArgs e)
        {

            try
            {
                if (!_flex2.HasNormalRow) return;
                _flex2.Rows.Remove(_flex2.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion


        #region
        void WinForm호출_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 제어 이벤트

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                string CD_COMPANY = D.GetString(_flex["CD_COMPANY"]);
                string CD_PARTNER = D.GetString(_flex["CD_PARTNER"]);
                string CAR_NO = D.GetString(_flex["CAR_NO"]);

                string Filter = "CD_COMPANY = '" + CD_COMPANY + "'" + "AND CD_PARTNER = '" + CD_PARTNER + "'" + "AND CAR_NO = '" + CAR_NO + "'";

                if (_flex.DetailQueryNeed)
                {
                    _flex2.Redraw = false; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다
                    DataTable dt = _biz.SearchL(CD_COMPANY, CD_PARTNER, CAR_NO, cbo계획명.SelectedValue.ToString(), dtpFrom.Text, dtpTo.Text, cbo분할청구.SelectedValue.ToString());
                    _flex2.BindingAdd(dt, Filter);
                    _flex2.Redraw = true; //종 그리드의 데이터를 화면에 그리는 것을 UnVisible시킨다



                }

                _flex2.RowFilter = Filter;
            }

            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {


            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_PLAN":
                        string CD_PLAN = D.GetString(_flex["CD_PLAN"]);
                        string NM_KOR = string.Empty;
                        e.Parameter.UserParams = "성명도움창;P_MA_EMP_SUB;" + CD_PLAN + ";" + NM_KOR;
                        break;

                    case "NM_COMPANY":
                        string CD_COMPANY = D.GetString(_flex["CD_COMPANY"]);
                        string NM_COMPANY = "NM_COMPANY";
                        e.Parameter.UserParams = "회사도움창;P_MA_COMPANY_SUB;" + CD_COMPANY + ";" + NM_COMPANY;
                        break;

                    //case "NM_KOR":
                    //    string NO_EMP = D.GetString(_flex["NO_EMP"]);
                    //    string NM_KOR2 = "NM_KOR";
                    //    e.Parameter.UserParams = "성명도움창;P_MA_EMP_SUB;" + NO_EMP + ";" + NM_KOR2;
                    //    break;

                    case "NM_PLANT":
                        string CD_PLANT = D.GetString(_flex["CD_PLANT"]);
                        string NM_PLANT = "NM_PLANT";
                        e.Parameter.UserParams = "공장도움창;P_MA_PLANT_SUB;" + CD_PLANT + ";" + NM_PLANT;
                        break;

                    case "NO_EMP":

                        string CAR_NO = D.GetString(_flex["CAR_NO"]);
                        string NO_EMP = D.GetString(_flex["NO_EMP"]);

                        e.Parameter.UserParams = "사용자도움창;H_CZ_CAR_EMERGE;" + CAR_NO + ";" + NO_EMP;
                        break;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion




        //private void OnBpControl_CodeChanged(object sender,Duzon.Common.BpControls.BpQueryArgs e)
        //{
        //    try
        //    {
        //        switch (e.ControlName)
        //        {
        //            case "bpc운전자":
        //                string 첫번째param = string.Empty;
        //                string 두번째param = string.Empty;
        //                bpc운전자.UserParams = "사용자정의도움창;H_CZ_CAR_EMERGE;" + 첫번째param + ";" + 두번째param;
        //                break;
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}


    }
}

