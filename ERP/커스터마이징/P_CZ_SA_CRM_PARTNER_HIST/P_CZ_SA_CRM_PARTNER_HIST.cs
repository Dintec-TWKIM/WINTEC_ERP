using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_CRM_PARTNER_HIST : PageBase
    {
        P_CZ_SA_CRM_PARTNER_HIST_BIZ _biz = new P_CZ_SA_CRM_PARTNER_HIST_BIZ();

        public P_CZ_SA_CRM_PARTNER_HIST()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("SEQ", "순번", 60);
            this._flex.SetCol("DT_HIST", "등록일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_PARTNER_GRP", "거래처그룹", 100);
            this._flex.SetCol("CD_PARTNER", "거래처코드", 100, true);
            this._flex.SetCol("LN_PARTNER", "거래처명", 500);
            
            this._flex.ExtendLastCol = true;

            this._flex.SetOneGridBinding(null, new IUParentControl[] { this.one기본정보, this.one추가정보 });
            this._flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdo마감후견적제출가능, this.rdo마감후견적제출불가 }, new string[] { "Y", "N" });

            this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            SetControl setControl = new SetControl();

            setControl.SetCombobox(this.cboINQ수신방법, new DataView(MA.GetCode("CZ_SA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboQTN발신방법, new DataView(MA.GetCode("CZ_SA00017", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboPO수신방법, new DataView(MA.GetCode("CZ_PU00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹, new DataView(MA.GetCode("MA_B000065", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            this.cboACK발신방법.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																  		    '' AS NAME
																     UNION ALL
																     SELECT CD_SYSDEF AS CODE,
																  		    NM_SYSDEF AS NAME
																     FROM CZ_MA_CODEDTL WITH(NOLOCK)
																     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                   @"AND CD_FIELD = 'CZ_MA00048'
																     ORDER BY NAME ASC");
            this.cboACK발신방법.ValueMember = "CODE";
            this.cboACK발신방법.DisplayMember = "NAME";
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.bpc거래처.QueryWhereIn_Pipe,
                                                                     this.ctx영업담당자S.CodeValue,
                                                                     this.ctx물류담당자S.CodeValue,
                                                                     this.cbo거래처그룹.SelectedValue });

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                this._flex.Rows.Add();
                this._flex.Row = _flex.Rows.Count - 1;

                this._flex["SEQ"] = this.SeqMax();
                this._flex["DT_HIST"] = Global.MainFrame.GetStringToday;

                this._flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex.HasNormalRow) return;

                this._flex.Rows.Remove(this._flex.Row);
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

        private Decimal SeqMax()
        {
            Decimal num = 1;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM CZ_SA_CRM_PARTNER_HIST WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

            if (num <= this._flex.GetMaxValue("SEQ"))
                num = (this._flex.GetMaxValue("SEQ") + 1);

            return num;
        }
    }
}
