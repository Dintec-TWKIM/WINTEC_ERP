using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms.Help;
using Duzon.Common.Controls;


namespace cz
{
    public partial class P_CZ_HR_PHONE_PAY_REG : PageBase
    {
        #region 생성자
        P_CZ_HR_PHONE_PAY_REG_BIZ _biz = new P_CZ_HR_PHONE_PAY_REG_BIZ();

        public string GUBUN = string.Empty;

        public string YM = string.Empty;

        public string YM2 = string.Empty;

        public string USER_ID = Global.MainFrame.LoginInfo.UserID;

        #endregion 생성자


        #region 초기화
        public P_CZ_HR_PHONE_PAY_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            InitControl();
            InitGrid();
            InitEvent();
        }


        private void InitControl()
        {
            dtp급여반영월.Text = Util.GetToday();
        }


        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { _flex };
            this._flex.BeginSetting(1, 1, true);

            this._flex.SetCol("SEQ", "SEQ", false);
            this._flex.SetCol("YM", "지급반영월", true);
            this._flex.SetCol("NO_EMP", "사번", false);
            this._flex.SetCol("NM_KOR", "이름", 100);
            this._flex.SetCol("GUBUN", "구분", 100, true);
            this._flex.SetCol("NM_CC", "코스트센터", 100, false);
            this._flex.SetCol("NM_DUTY_RANK", "직급", 70, false);
            this._flex.SetCol("DC_RMK", "비고", 100);

            _flex.Cols["YM"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NM_CC"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
            //_flex.Cols["NM_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NM_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;


            //this._flex.SetCodeHelpCol("NM_COMPANY", HelpID.P_MA_COMPANY_SUB, ShowHelpEnum.Always, new string[] { "CD_COMPANY", "NM_COMPANY" }, new string[] { "CD_COMPANY", "NM_COMPANY" });
            this._flex.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NM_KOR", "NO_EMP" }, new string[] { "NM_KOR", "NO_EMP" });


            //MA_CODE, 코드관리 등록 : CZ_HR00005
            this._flex.SetDataMap("GUBUN", this.GetComboDataCombine("S;CZ_HR00005"), "CODE", "NAME");

            this._flex.VerifyPrimaryKey = new string[] { "YM", "NO_EMP", "GUBUN" };

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            btn이월복사.Click += new EventHandler(btn이월복사_Click);
            btn초기화.Click += new EventHandler(btn초기화_Click);
        }
        #endregion 초기화


        #region 버튼
        private void btn이월복사_Click(object sender, EventArgs e)
        {
            try
            {
                YM = dtp급여반영월.Text; //05
                System.DateTime TimeMonth = new System.DateTime(Convert.ToInt16(YM.Substring(0, 4).ToString()), Convert.ToInt16(YM.Substring(4, 2).ToString()), 1);

                YM2 = TimeMonth.AddMonths(-1).ToString("yyyyMM"); //04

                //DataTable dt = this.flex.GetChanges();
                this._biz.CopyPaste(YM, YM2, USER_ID, Global.MainFrame.LoginInfo.CompanyCode);

                Util.ShowMessage("데이터를 복사했습니다.");

                //dtp급여반영월.Text = YM;

                OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        private void btn초기화_Click(object sender, EventArgs e)
        {
            try
            {
                cbx부서.Clear();
                cbx사원.Clear();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 버튼


        #region 조회
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    dtp급여반영월.Text,
                    cbx사원.SelectedValue,
                    cbx부서.SelectedText
                });

                if (!_flex.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
               
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 조회


        #region 추가
        protected override bool BeforeAdd()
        {
            return base.BeforeAdd();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTB = _flex.DataTable;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;

                this._flex["YM"] = dtp급여반영월.Text;
                //this._flex["CD_COMPANY"] = CD_COMPANY;
                //this.flex["GUBUN"] = "N";


                _flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 추가


        #region 삭제

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            return true;
        }


        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;

                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 삭제


        #region 저장

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);

                OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify())
                return false;

            if (this._flex.IsDataChanged == false) return false;

            DataTable dt = this._flex.GetChanges();
            this._biz.Save(dt);

            this._flex.AcceptChanges();

            return true;
        }

        #endregion 저장
    }
}
