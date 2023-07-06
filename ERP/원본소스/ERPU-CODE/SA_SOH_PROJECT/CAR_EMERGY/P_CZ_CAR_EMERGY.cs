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
using Duzon.Common.Forms.Help;
using Duzon.BizOn.Erpu.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;




namespace cz
{
    public partial class P_CZ_CAR_EMERGY : PageBase
    {

        #region 생성자

        public P_CZ_CAR_EMERGY()
        {
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flex };
        }

        #endregion

        //초기화
        P_CZ_CAR_EMERGY_BIZ _biz = new P_CZ_CAR_EMERGY_BIZ();

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_CZ_CAR_EMERGY_BIZ();
            this.InitGrid();
            this.InitEvent();
            this.InitPaint();
        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);

            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CAR_NO", "차량정보", 150, false);
            _flex.SetCol("NM_CAR", "차랑명", 200, true);
            _flex.SetCol("CD_PARTNER", "상호", 80, true);
            _flex.SetCol("LN_PARTNER", "상호명", 80, true);
            _flex.SetCol("NO_EMP", "성명", 80, true);
            _flex.SetCol("HP", "휴대폰번호", 150);
            _flex.SetCol("PWHP", "파워텔", 150);
            _flex.SetCol("TEL", "전화번호", 150);
            _flex.SetCol("NOTE", "비고", 450);


            _flex.Cols["CAR_NO"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NM_CAR"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["HP"].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols["PWHP"].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols["TEL"].TextAlign = TextAlignEnum.RightCenter;

            


            //팝업함수정보가 필요 
            //this._flex.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });
            this._flex.SetCodeHelpCol("LN_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "LN_PARTNER", "CD_PARTNER" }, new string[] { "LN_PARTNER", "CD_PARTNER" });
            //this._flex.SetCodeHelpCol("NM_COMPANY", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "NM_COMPANY", "CD_COMPANY" }, new string[] { "NM_COMPANY", "CD_COMPANY" });
            //this._flex.SetCodeHelpCol("CAR_NO", "H_CZ_HELP01", ShowHelpEnum.Always, new string[] { "CD_MAN", "NM_MAN" }, new string[] { "CODE", "NAME" });
            //_flex.SetCodeHelpCol("NO_EMP", Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_KOR" }, new string[] { "NO_EMP", "NM_KOR" }, ResultMode.FastMode);

            _flex.VerifyPrimaryKey = new string[] { "CAR_NO", "NO_EMP" };                         // PK로 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyAutoDelete = new string[] { "CAR_NO", "NO_EMP" };                       // 설정한 컬럼에 값이 없을 때 자동으로 행 삭제가 이루어지게 한다. Verify() 메서드 호출에서 체크한다.
            _flex.VerifyNotNull = new string[] { "CAR_NO", "NO_EMP", "HP" };                           // 필수체크 할 컬럼을 설정한다. Verify() 메서드 호출에서 체크한다.

            //_flex.AddRow += new EventHandler(OnToolBarAddButtonClicked);


            _flex.SettingVersion = "0.0.0.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            //gridStyle : 그리드 색상설정
            //allowSorting : 정렬여부(None:소계 표현된 그리드는 반드시 이형식으로 셋팅하여야 한다, 그외:헤더의 컬럼을 더블클릭시 정렬할 수 있다)
            //sumPosition : 상단에 합계행을 설정할지 여부
        }

        private void InitEvent()
        {
            //그리드이벤트
            //_flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

            //화면버튼이벤트
            btn추가.Click += new EventHandler(btn추가_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            //DB 값을 화면 로딩시 가져오는 경우 여기서 셋팅한다.
        }


        #region 값 점검

        bool Chk차량정보 
        { 
            get 
            { 
                return !Checker.IsEmpty(ctx차량정보, DD("차량정보")); 
            } 
        }
        
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

        #region 상단 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;
          
                String carNo = ctx차량정보.Text;
                String name = txt성명.Text;
                

                //MsgControl.ShowMsg();

                //2가지 경우로 가능
                _flex.Binding = _biz.Search(carNo, name);

                /*
                 * Parameter가 많은 경우
                 * List<object> list = new List<object>;
                   list.Add(“carNo”);
                   list.Add(“name”);
                   
                   DataTable dt = _biz.Search(list.ToArray());

                 */

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

                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();
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
                        _biz.Delete(D.GetString(row["CAR_NO"]), D.GetString(row["NO_EMP"]));
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
            DataTable dt = _flex.GetChanges();

            _biz.Save(dt);
            _flex.AcceptChanges();
            return true;
        }

        #endregion

        #region 화면버튼

        void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex["CARNO"] = ctx차량정보.Text;
                _flex["NO_EMP"] = txt성명.Text;
                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();
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
                if (!_flex.HasNormalRow) return;
                _flex.Rows.Remove(_flex.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void btn삭제_Click_1(object sender, EventArgs e)
        {

        }

        #region
        void WinForm호출_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_CAR_EMERGY_SUB sub = new P_CZ_CAR_EMERGY_SUB();//WinForm 생성

                DialogResult rtn = sub.ShowDialog();//WinForm Open

                if (rtn == DialogResult.OK)//WinForm Close시에 DialogResult.OK 이면 추가 설정을 한다
                {
                    //추가설정
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 제어 이벤트

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {


            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    //case "NM_KOR":
                    //    string NO_EMP = D.GetString(_flex["NO_EMP"]);
                    //    string NM_KOR = string.Empty;
                    //    e.Parameter.UserParams = "성명도움창;P_MA_EMP_SUB;" + NO_EMP + ";" + NM_KOR;
                    //    break;

                    //case "NM_COMPANY":
                    //    string CD_COMPANY = D.GetString(_flex["CD_COMPANY"]);
                    //    string NM_COMPANY = "NM_COMPANY";
                    //    e.Parameter.UserParams = "회사도움창;P_MA_COMPANY_SUB;" + CD_COMPANY + ";" + NM_COMPANY;
                    //    break;

                    case "LN_PARTNER":
                        string CD_PARTNERT = D.GetString(_flex["CD_PARTNERT"]);
                        string LN_PARTNERT = "LN_PARTNERT";
                        e.Parameter.UserParams = "공장도움창;P_MA_PARTNERT_SUB;" + CD_PARTNERT + ";" + LN_PARTNERT;
                        break;

                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion

    }
}
