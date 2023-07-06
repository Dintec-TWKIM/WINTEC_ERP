using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
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
	public partial class P_CZ_PR_CFG_PLANT : PageBase
	{
        private P_CZ_PR_CFG_PLANT_BIZ _biz = new P_CZ_PR_CFG_PLANT_BIZ();
        private IU_Language L = new IU_Language();
        private string str공장코드 = "";
        public P_CZ_PR_CFG_PLANT() : this(Global.MainFrame.LoginInfo.CdPlant)
		{
		}

        public P_CZ_PR_CFG_PLANT(string 공장코드)
        {
            try
            {
                this.InitializeComponent();
                this.str공장코드 = 공장코드;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

		protected override void InitPaint()
        {
            base.InitPaint();
            this.InitControl();
            this.oneGrid2.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.oneGrid2.InitCustomLayout();
            this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitControl()
        {
            DataTable table1 = this.GetComboData("NC;MA_PLANT").Tables[0];
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo공장, table1);
            if (table1.Select("CODE = '" + this.str공장코드 + "'").Length == 1)
                this.cbo공장.SelectedValue = this.str공장코드;
            else if (table1.Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            this.cbo캘린더코드.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT '' AS CODE
UNION ALL
SELECT NO_CAL AS CODE
FROM CZ_PR_SHIFT
WHERE CD_COMPANY = '{0}'
AND YN_USE = 'Y'
GROUP BY NO_CAL", Global.MainFrame.LoginInfo.CompanyCode));
            this.cbo캘린더코드.ValueMember = "CODE";
            this.cbo캘린더코드.DisplayMember = "CODE";
            this._flexM.SetDataMap("TP_START", MA.GetCode("CZ_WIN0017"), "CODE", "NAME");
            this._flexM.SetDataMap("TP_END", MA.GetCode("CZ_WIN0017"), "CODE", "NAME");
            this._flexM.SetDataMap("YN_USE", MA.GetCode("MA_B000057"), "CODE", "NAME");
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexM };

            this._flexM.BeginSetting(1, 1, true);
            this._flexM.SetCol("NO_CAL", "캘린더코드", 100, false);
            this._flexM.SetCol("TP_START", "시작요일", 80);
            this._flexM.SetCol("TM_START", "시작시간", 80);
            this._flexM.SetCol("TP_END", "종료요일", 80);
            this._flexM.SetCol("TM_END", "종료시간", 80);
            this._flexM.SetCol("TM_STOP", "정지시간", 80);
            this._flexM.SetCol("TM_SFT", "가동시간", 80);
            this._flexM.SetCol("QT_WORKER", "근무인원", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("CD_DEPT", "관리부서코드", 120, 12, true);
            this._flexM.SetCol("NM_DEPT", "관리부서명", 120, false);
            this._flexM.SetCol("YN_USE", "사용유무", 60, true);
            this._flexM.SetCol("DC_RMK", "비고", 200, 100, true);
            this._flexM.Cols["TP_START"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TP_END"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TM_START"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TM_END"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TM_STOP"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TM_SFT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.Cols["TM_START"].EditMask = "99\\:99\\:99";
            this._flexM.Cols["TM_END"].EditMask = "99\\:99\\:99";
            this._flexM.Cols["TM_STOP"].EditMask = "99\\:99\\:99";
            this._flexM.Cols["TM_SFT"].EditMask = "99\\:99\\:99";
            this._flexM.Cols["TM_START"].Format = "##\\:##\\:##";
            this._flexM.Cols["TM_END"].Format = "##\\:##\\:##";
            this._flexM.Cols["TM_STOP"].Format = "##\\:##\\:##";
            this._flexM.Cols["TM_SFT"].Format = "##\\:##\\:##";
            this._flexM.SetStringFormatCol("TM_START");
            this._flexM.SetNoMaskSaveCol("TM_START");
            this._flexM.SetStringFormatCol("TM_END");
            this._flexM.SetNoMaskSaveCol("TM_END");
            this._flexM.SetStringFormatCol("TM_STOP");
            this._flexM.SetNoMaskSaveCol("TM_STOP");
            this._flexM.SetStringFormatCol("TM_SFT");
            this._flexM.SetNoMaskSaveCol("TM_SFT");
            this._flexM.NewRowEditable = true;
            this._flexM.EnterKeyAddRow = true;
            this._flexM.SetCodeHelpCol("CD_DEPT", HelpID.P_MA_DEPT_SUB, ShowHelpEnum.Always, new string[] { "CD_DEPT", "NM_DEPT" }
                                                                                           , new string[] { "CD_DEPT", "NM_DEPT" });
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this._flexM.AddRow += new EventHandler(this.btn추가_Click);
            this._flexM.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (this.공장선택여부)
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
            this.cbo공장.Focus();
            return false;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                this._flexM.Binding = this._biz.Search(this.cbo공장.SelectedValue.ToString(),
                                                       this.cbo캘린더코드.SelectedValue.ToString());

                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd())
                    return;
                if (!this.공장선택여부)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    this.cbo공장.Focus();
                }
                else
                {
                    this._flexM.Rows.Add();
                    this._flexM.Row = this._flexM.Rows.Count - 1;
                    this._flexM[this._flexM.Row, "CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    this._flexM[this._flexM.Row, "TP_START"] = "1";
                    this._flexM[this._flexM.Row, "TM_START"] = "000000";
                    this._flexM[this._flexM.Row, "TP_END"] = "1";
                    this._flexM[this._flexM.Row, "TM_END"] = "000000";
                    this._flexM[this._flexM.Row, "TM_STOP"] = "000000";
                    this._flexM[this._flexM.Row, "TM_SFT"] = "000000";
                    this._flexM[this._flexM.Row, "YN_USE"] = "Y";
                    this._flexM.AddFinished();
                    this._flexM.Col = this._flexM.Cols.Fixed;
                    this._flexM.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flexM.HasNormalRow)
                    return;
                this._flexM.Rows.Remove(this._flexM.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(PageResultMode.SaveGood);
                this.InitPaint();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforePrint())
                    ;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.Verify())
                return false;
            DataTable changes1 = this._flexM.GetChanges();
            if (changes1 == null)
            {
                this.ToolBarSaveButtonEnabled = false;
                return true;
            }
            if (!this._biz.Save(changes1))
                return false;
            this._flexM.AcceptChanges();
            return true;
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!this.onValidateChk(sender))
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool onValidateChk(object sender)
        {
            Dass.FlexGrid.FlexGrid flexGrid = (Dass.FlexGrid.FlexGrid)sender;
            string ls_tp_start = flexGrid[flexGrid.Row, "TP_START"].ToString();
            string ls_tp_end = flexGrid[flexGrid.Row, "TP_END"].ToString();
            string str1 = flexGrid[flexGrid.Row, "TM_START"].ToString();
            string str2 = flexGrid[flexGrid.Row, "TM_END"].ToString();
            string str3 = flexGrid[flexGrid.Row, "TM_STOP"].ToString();
            flexGrid.Editor.Text = flexGrid.Editor.Text.Replace("_", "0");
            switch (flexGrid.Cols[flexGrid.Col].Name)
            {
                case "TM_START":
                case "TM_END":
                case "TM_STOP":
                    if (int.Parse(flexGrid.Editor.Text.Substring(3, 2)) > 60 || int.Parse(flexGrid.Editor.Text.Substring(6, 2)) > 60)
                    {
                        this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                        flexGrid.Editor.Text = "";
                        return false;
                    }
                    break;
            }
            if (flexGrid.Cols[flexGrid.Col].Name == "TP_START")
                ls_tp_start = flexGrid.EditData.ToString();
            else if (flexGrid.Cols[flexGrid.Col].Name == "TP_END")
                ls_tp_end = flexGrid.EditData.ToString();
            else if (flexGrid.Cols[flexGrid.Col].Name == "TM_START")
                str1 = flexGrid.EditData.ToString();
            else if (flexGrid.Cols[flexGrid.Col].Name == "TM_END")
            {
                str2 = flexGrid.EditData.ToString();
            }
            else
            {
                if (!(flexGrid.Cols[flexGrid.Col].Name == "TM_STOP"))
                    return true;
                str3 = flexGrid.EditData.ToString();
            }
            string ls_tm_start = str1.Substring(0, 2) + ":" + str1.Substring(2, 2) + ":" + str1.Substring(4, 2);
            string ls_tm_end = str2.Substring(0, 2) + ":" + str2.Substring(2, 2) + ":" + str2.Substring(4, 2);
            string ls_tm_stop = str3.Substring(0, 2) + ":" + str3.Substring(2, 2) + ":" + str3.Substring(4, 2);
            if (D.GetDecimal(ls_tm_start.Substring(0, 2)) >= 24M || D.GetDecimal(ls_tm_start.Substring(2, 2)) > 59M || D.GetDecimal(ls_tm_start.Substring(4, 2)) > 59M)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                return false;
            }
            if (D.GetDecimal(ls_tm_end.Substring(0, 2)) >= 24M || D.GetDecimal(ls_tm_end.Substring(2, 2)) > 59M || D.GetDecimal(ls_tm_end.Substring(4, 2)) > 59M)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                return false;
            }
            if (D.GetDecimal(ls_tm_stop.Substring(0, 2)) >= 24M || D.GetDecimal(ls_tm_stop.Substring(2, 2)) > 59M || D.GetDecimal(ls_tm_stop.Substring(4, 2)) > 59M)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                return false;
            }
            flexGrid[flexGrid.Row, "TM_SFT"] = this.fnCalTime(ls_tp_start, ls_tm_start, ls_tp_end, ls_tm_end, ls_tm_stop);
            return true;
        }

        private string fnCalTime(
          string ls_tp_start,
          string ls_tm_start,
          string ls_tp_end,
          string ls_tm_end,
          string ls_tm_stop)
        {
            DateTime dateTime1 = DateTime.Parse(ls_tm_start);
            DateTime dateTime2 = DateTime.Parse(ls_tm_end);
            DateTime dateTime3 = DateTime.Parse(ls_tm_stop);
            if (ls_tp_start == "2")
                dateTime1 = dateTime1.AddHours(24.0);
            if (ls_tp_end == "2")
                dateTime2 = dateTime2.AddHours(24.0);
            TimeSpan timeSpan1 = new TimeSpan(dateTime3.Hour, dateTime3.Minute, dateTime3.Second);
            dateTime3 = dateTime1.Add(timeSpan1);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, dateTime2.Second);
            TimeSpan timeSpan3 = dateTime2 - dateTime3;
            timeSpan3.ToString();
            int days = timeSpan3.Days;
            int hours = timeSpan3.Hours;
            int minutes = timeSpan3.Minutes;
            int seconds = timeSpan3.Seconds;
            if (days < 0 || hours < 0 || minutes < 0 || seconds < 0)
                return "000000";
            int num1 = days * 24 + hours + 100;
            int num2 = minutes + 100;
            int num3 = seconds + 100;
            return num1.ToString().Substring(1, 2) + num2.ToString().Substring(1, 2) + num3.ToString().Substring(1, 2);
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
                this.ToolBarDeleteButtonEnabled = false;
                this.ToolBarPrintButtonEnabled = false;
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = this._flexM.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == string.Empty);
    }
}
