using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using Dintec;

namespace cz
{
    public partial class P_CZ_HR_EMP_RPT : PageBase
    {
        P_CZ_HR_EMP_RPT_BIZ _biz =  new P_CZ_HR_EMP_RPT_BIZ();

        public P_CZ_HR_EMP_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("NM_COMPANY", "회사", 100);
            this._flex.SetCol("NM_DUTY_TYPE", "직군", 80);
            this._flex.SetCol("NM_DUTY_RANK", "직위", 60);
            this._flex.SetCol("NM_DUTY_STEP", "직급", 60);
            this._flex.SetCol("CD_PAY_STEP", "호봉", 60);
            this._flex.SetCol("NM_DEPT", "부서", 100);
            this._flex.SetCol("NM_DUTY_RANK1", "직위(발령)", 100);
            this._flex.SetCol("NM_DUTY_STEP1", "직급(발령)", 100);
            this._flex.SetCol("CD_PAY_STEP1", "호봉(발령)", 100);
            this._flex.SetCol("NM_DEPT1", "부서(발령)", 100);
            this._flex.SetCol("NM_CC", "코스트센터", 100);
            this._flex.SetCol("NO_EMP", "사번", 60);
            this._flex.SetCol("NM_KOR", "성명", 60);
            this._flex.SetCol("NO_TEL_EMER", "비상연락", false);
            this._flex.SetCol("NO_EMAIL", "E-MAIL", false);
            this._flex.SetCol("NM_NATION", "내외국인", 80);
            this._flex.SetCol("DT_BIRTH", "생년월일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_GENTER", "그룹입사일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_ENTER", "입사일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_BRETIRE", "퇴직기준일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_RETIRE", "퇴사일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_DUTY_RANK", "현직위승급일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_INCOM", "재직구분", 80);
            this._flex.SetCol("NM_ANCODE_LAST", "최근인사발령", 100);
            this._flex.SetCol("NM_ANCODE_PD", "포상/징계", 100);
            this._flex.SetCol("NM_SCH", "최종출신학교", 150);
            this._flex.SetCol("DC_RMK", "비고", 150, true);

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {

            this.btn단체메일발송.Click += new EventHandler(this.btn단체메일발송_Click);
            this.bpc직군.QueryBefore += new BpQueryHandler(this.bpc직군_QueryBefore);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp기준일자.Text = Global.MainFrame.GetStringToday;

            this.bpc회사.AddItem("K100", "(주)딘텍");
            this.bpc회사.AddItem("K200", "(주)두베코");
            this.bpc회사.AddItem("K300", "글로벌마린앤인더스트리얼서비스 비브이(유)");
            this.bpc회사.AddItem("S100", "DINTEC SINGAPORE PTE.LTD.");

            this.cbo재직구분.DataSource = Global.MainFrame.GetComboDataCombine("S;HR_H000014");
            this.cbo재직구분.ValueMember = "CODE";
            this.cbo재직구분.DisplayMember = "NAME";

            this.cbo내외국인.DataSource = Global.MainFrame.GetComboDataCombine("S;HR_T000003");
            this.cbo내외국인.ValueMember = "CODE";
            this.cbo내외국인.DisplayMember = "NAME";
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { this.dtp기준일자.Text,
                                                                     this.bpc회사.QueryWhereIn_Pipe,
                                                                     this.bpc부서.QueryWhereIn_Pipe,
                                                                     this.bpc직군.QueryWhereIn_Pipe,
                                                                     this.cbo재직구분.SelectedValue,
                                                                     this.cbo내외국인.SelectedValue,
                                                                     this.txt검색.Text,
                                                                     (this.chk전체보기.Checked == true ? "Y" : "N") });

                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            if (!_biz.Save(this._flex.GetChanges())) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void bpc직군_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_CODE_SUB1)
                    e.HelpParam.P41_CD_FIELD1 = "HR_H000004";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn단체메일발송_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_MA_EMAIL_SUB2 dialog = new P_CZ_MA_EMAIL_SUB2();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
