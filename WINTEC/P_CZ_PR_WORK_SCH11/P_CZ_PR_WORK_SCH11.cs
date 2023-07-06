using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.Windows.Print;
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
	public partial class P_CZ_PR_WORK_SCH11 : PageBase
	{
        #region ==================================================================== 초기화 & 전역변수
        private P_CZ_PR_WORK_SCH11_BIZ _biz = new P_CZ_PR_WORK_SCH11_BIZ();
        private DataSet _dsOp = new DataSet();
        private DataSet _dsItem = new DataSet();
        private DataSet _dsEmp = new DataSet();
        private DataSet _dsEquip = new DataSet();
        public P_CZ_PR_WORK_SCH11()
		{
			InitializeComponent();
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
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Dt_Wo.IsNecessaryCondition = true;
            this.bpP_Date.IsNecessaryCondition = true;
            this.bpP_Wc.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            DataSet comboData = this.GetComboData("NC;MA_PLANT");
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            this.dtp작업기간.StartDateToString = this.MainFrameInterface.GetStringToday;
            this.dtp작업기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.dtp조회기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6);
            this.dtp조회기간.EndDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6);
            this.cbo공장.Focus();
        }

        private void InitGrid()
        {

			#region _flex공정별
			this._flex공정별.BeginSetting(1, 1, false);
            this._flex공정별.SetCol("NM_WC", "작업장명", 100, false);
            this._flex공정별.SetCol("CD_OP", "OP", 40, false);
            this._flex공정별.SetCol("NM_OP", "공정명", 100, false);
            this._flex공정별.SetCol("NM_CLS_L", "대분류", 100, false);
            this._flex공정별.SetCol("NM_CLS_M", "중분류", 100, false);
            this._flex공정별.SetCol("NM_CLS_S", "소분류", 100, false);
            this._flex공정별.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex공정별.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex공정별.SetCol("STND_ITEM", "규격", 120, false);
            this._flex공정별.SetCol("UNIT_MO", "단위", 40, false);
            this._flex공정별.SetCol("STND_DETAIL_ITEM", "세부규격", 120, false);
            this._flex공정별.SetCol("MAT_ITEM", "재질", 80, false);
            this._flex공정별.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex공정별.SetCol("DT_WORK", "작업일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex공정별.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("RT_SCRAP", "불량율(%)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("QT_INV", "재고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("WEIGHT_WORK", "중량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex공정별.SetCol("UM_INV", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex공정별.SetCol("AM_WORK", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex공정별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex공정별.SetExceptSumCol("UM_INV");
            this._flex공정별.Cols["RT_SCRAP"].Format = "##0.##";
            this._flex공정별.Rows[1].AllowMerging = true;
			#endregion


			#region _flex품목별
			this._flex품목별.BeginSetting(1, 1, false);
            this._flex품목별.SetCol("NM_CLS_L", "대분류", 100, false);
            this._flex품목별.SetCol("NM_CLS_M", "중분류", 100, false);
            this._flex품목별.SetCol("NM_CLS_S", "소분류", 100, false);
            this._flex품목별.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex품목별.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex품목별.SetCol("STND_ITEM", "규격", 120, false);
            this._flex품목별.SetCol("UNIT_MO", "생산단위", 40, false);
            this._flex품목별.SetCol("STND_DETAIL_ITEM", "세부규격", 120, false);
            this._flex품목별.SetCol("MAT_ITEM", "재질", 80, false);
            this._flex품목별.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex품목별.SetCol("DT_WORK", "작업일", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목별.SetCol("QT_WORK", "작업수량", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("QT_REJECT", "불량수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("RT_SCRAP", "불량율(%)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("QT_MOVE", "이동수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("QT_INV", "입고수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("WEIGHT_WORK", "중량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목별.SetCol("UM_INV", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex품목별.SetCol("AM_WORK", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex품목별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex품목별.SetExceptSumCol("UM_INV");
            this._flex품목별.Cols["RT_SCRAP"].Format = "##0.##";
            this._flex품목별.Rows[1].AllowMerging = true;
            #endregion


            #region _flex작업자별
            this._flex작업자별.BeginSetting(1, 1, false);
            this._flex작업자별.SetCol("NM_WC", "작업장명", 100, false);
            this._flex작업자별.SetCol("NO_EMP", "사번", 100, false);
            this._flex작업자별.SetCol("NM_KOR", "성명", 100, false);
            this._flex작업자별.SetCol("NM_CLS_L", "대분류", 100, false);
            this._flex작업자별.SetCol("NM_CLS_M", "중분류", 100, false);
            this._flex작업자별.SetCol("NM_CLS_S", "소분류", 100, false);
            this._flex작업자별.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex작업자별.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex작업자별.SetCol("STND_ITEM", "규격", 120, false);
            this._flex작업자별.SetCol("UNIT_MO", "생산단위", 40, false);
            this._flex작업자별.SetCol("NO_DESIGN", "도면번호", 80, false);
            this._flex작업자별.SetCol("CD_OP", "OP", 40, false);
            this._flex작업자별.SetCol("NM_OP", "공정명", 100, false);
            this._flex작업자별.SetCol("DT_WORK", "작업일", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex작업자별.SetCol("QT_WORK", "작업수량", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("QT_REJECT", "불량수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("RT_SCRAP", "불량율(%)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("QT_MOVE", "이동수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("QT_INV", "입고수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("WEIGHT_WORK", "중량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex작업자별.SetCol("UM_INV", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex작업자별.SetCol("AM_WORK", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex작업자별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex작업자별.SetExceptSumCol("UM_INV");
            this._flex작업자별.Cols["RT_SCRAP"].Format = "##0.##";
            this._flex작업자별.Rows[1].AllowMerging = true;
            #endregion


            #region _flex장비별
            this._flex장비별.BeginSetting(1, 1, false);
            this._flex장비별.SetCol("NM_WC", "작업장명", 100, false);
            this._flex장비별.SetCol("CD_EQUIP", "장비코드", 100, false);
            this._flex장비별.SetCol("NM_EQUIP", "장비명", 100, false);
            this._flex장비별.SetCol("NM_CLS_L", "대분류", 100, false);
            this._flex장비별.SetCol("NM_CLS_M", "중분류", 100, false);
            this._flex장비별.SetCol("NM_CLS_S", "소분류", 100, false);
            this._flex장비별.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex장비별.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex장비별.SetCol("STND_ITEM", "규격", 120, false);
            this._flex장비별.SetCol("UNIT_MO", "생산단위", 40, false);
            this._flex장비별.SetCol("NO_DESIGN", "도면번호", 80, false);
            this._flex장비별.SetCol("CD_OP", "OP", 40, false);
            this._flex장비별.SetCol("NM_OP", "공정명", 100, false);
            this._flex장비별.SetCol("DT_WORK", "작업일", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex장비별.SetCol("QT_WORK", "작업수량", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("QT_REJECT", "불량수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("RT_SCRAP", "불량율(%)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("QT_MOVE", "이동수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("QT_INV", "입고수량", 0, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("WEIGHT_WORK", "중량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex장비별.SetCol("UM_INV", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex장비별.SetCol("AM_WORK", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex장비별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex장비별.SetExceptSumCol("UM_INV");
            this._flex장비별.Cols["RT_SCRAP"].Format = "##0.##";
            this._flex장비별.Rows[1].AllowMerging = true;
			#endregion

		}

		private void InitEvent()
        {
            this.bpc작업장.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);

            this.chk중분류.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk대분류.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk소분류.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk실적일.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk품목.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk공정.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk작업장.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk사원.Click += new EventHandler(this.OnCheckBoxClick);
            this.chk장비.Click += new EventHandler(this.OnCheckBoxClick);

            this.m_tabWork.Click += new EventHandler(this.m_tabWork_Click);
        }
		#endregion

		#region ==================================================================== 메인버튼 이벤트

		protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo공장.Focus();
                return false;
            }
            if (this.bpc작업장.DataTable == null)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl작업장.Text);
                this.bpc작업장.Focus();
                return false;
            }
            if (this.dtp작업기간.StartDateToString == "" || this.dtp작업기간.EndDateToString == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl작업기간.Text);
                this.dtp작업기간.Focus();
                return false;
            }
            if (!(this.dtp조회기간.StartDateToString == "") && !(this.dtp조회기간.EndDateToString == ""))
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl조회기간.Text);
            this.dtp조회기간.Focus();
            return false;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                object[] objArray = new object[19];
                objArray[0] = "";
                objArray[1] = this.LoginInfo.CompanyCode;
                objArray[2] = this.cbo공장.SelectedValue.ToString();
                objArray[4] = this.dtp작업기간.StartDateToString;
                objArray[5] = this.dtp작업기간.EndDateToString;
                objArray[6] = this.dtp조회기간.StartDateToString;
                objArray[7] = this.dtp조회기간.EndDateToString;
                objArray[8] = this.bpc작업장.QueryWhereIn_Pipe;
                objArray[17] = Global.SystemLanguage.MultiLanguageLpoint;
                DataSet dataSet = this._biz.Search(objArray, this.m_tabWork.SelectedIndex);
                switch (this.m_tabWork.SelectedIndex)
                {
                    case 0:
                        this._dsOp = dataSet.Copy();
                        this._dsOp.Tables[0].TableName = "PR_OP";
                        this._dsOp.Tables[1].TableName = "PR_OP_TOTAL";
                        this._flex공정별.Binding = this._dsOp.Tables["PR_OP"];
                        if (this._dsOp.Tables["PR_OP"] != null && this._dsOp.Tables["PR_OP"].Rows.Count > 0 && this._dsOp.Tables["PR_OP_TOTAL"] != null && this._dsOp.Tables["PR_OP_TOTAL"].Rows.Count > 0)
                            this.setMonTotal(this._dsOp.Tables["PR_OP_TOTAL"], ref this._flex공정별);
                        if (!this._flex공정별.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                            this.ToolBarPrintButtonEnabled = false;
                            break;
                        }
                        this.ToolBarPrintButtonEnabled = true;
                        break;
                    case 1:
                        this._dsItem = dataSet.Copy();
                        this._dsItem.Tables[0].TableName = "PR_ITEM";
                        this._dsItem.Tables[1].TableName = "PR_ITEM_TOTAL";
                        this._flex품목별.Binding = this._dsItem.Tables["PR_ITEM"].DefaultView;
                        if (this._dsItem.Tables["PR_ITEM"] != null && this._dsItem.Tables["PR_ITEM"].Rows.Count > 0 && this._dsItem.Tables["PR_ITEM_TOTAL"] != null && this._dsItem.Tables["PR_ITEM_TOTAL"].Rows.Count > 0)
                            this.setMonTotal(this._dsItem.Tables["PR_ITEM_TOTAL"], ref this._flex품목별);
                        if (!this._flex품목별.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                            this.ToolBarPrintButtonEnabled = false;
                            break;
                        }
                        this.ToolBarPrintButtonEnabled = true;
                        break;
                    case 2:
                        this._dsEmp = dataSet.Copy();
                        this._dsEmp.Tables[0].TableName = "PR_EMP";
                        this._dsEmp.Tables[1].TableName = "PR_EMP_TOTAL";
                        this._flex작업자별.Binding = this._dsEmp.Tables["PR_EMP"].DefaultView;
                        if (this._dsEmp.Tables["PR_EMP"] != null && this._dsEmp.Tables["PR_EMP"].Rows.Count > 0 && this._dsEmp.Tables["PR_EMP_TOTAL"] != null && this._dsEmp.Tables["PR_EMP_TOTAL"].Rows.Count > 0)
                            this.setMonTotal(this._dsEmp.Tables["PR_EMP_TOTAL"], ref this._flex작업자별);
                        if (!this._flex작업자별.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                            this.ToolBarPrintButtonEnabled = false;
                            break;
                        }
                        this.ToolBarPrintButtonEnabled = true;
                        break;
                    case 3:
                        this._dsEquip = dataSet.Copy();
                        this._dsEquip.Tables[0].TableName = "PR_EQUIP";
                        this._dsEquip.Tables[1].TableName = "PR_EQUIP_TOTAL";
                        this._flex장비별.Binding = this._dsEquip.Tables["PR_EQUIP"].DefaultView;
                        if (this._dsEquip.Tables["PR_EQUIP"] != null && this._dsEquip.Tables["PR_EQUIP"].Rows.Count > 0 && this._dsEquip.Tables["PR_EQUIP_TOTAL"] != null && this._dsEquip.Tables["PR_EQUIP_TOTAL"].Rows.Count > 0)
                            this.setMonTotal(this._dsEquip.Tables["PR_EQUIP_TOTAL"], ref this._flex장비별);
                        if (!this._flex장비별.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                            this.ToolBarPrintButtonEnabled = false;
                            break;
                        }
                        this.ToolBarPrintButtonEnabled = true;
                        break;
                }
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
                    return;
                switch (this.m_tabWork.SelectedIndex)
                {
                    case 0:
                        if (!this._flex공정별.HasNormalRow)
                            return;
                        break;
                    case 1:
                        if (!this._flex품목별.HasNormalRow)
                            return;
                        break;
                    case 2:
                        if (!this._flex작업자별.HasNormalRow)
                            return;
                        break;
                    case 3:
                        if (!this._flex장비별.HasNormalRow)
                            return;
                        break;
                }
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                string systemCode;
                string objectName;
                if (Global.MainFrame.ServerKey.ToUpper() == "GEMTECH")
                {
                    if (this.m_tabWork.SelectedIndex == 0)
                    {
                        systemCode = "R_PR_WORK_SCH11_0";
                        objectName = "작업일보-공정별";
                    }
                    else
                    {
                        systemCode = "R_PR_WORK_SCH11_1";
                        objectName = "작업일보(품목별)";
                    }
                }
                else
                {
                    systemCode = "R_PR_WORK_SCH11_0";
                    objectName = "작업일보";
                }
                ReportHelper reportHelper = new ReportHelper(systemCode, objectName);
                reportHelper.가로출력();
                reportHelper.SetData("공장코드", this.cbo공장.SelectedValue.ToString());
                reportHelper.SetData("공장명", this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("(")));
                reportHelper.SetData("작업장코드시작", this.bpc작업장.DataTable.Rows[0]["CD_WC"].ToString());
                reportHelper.SetData("작업장명시작", this.bpc작업장.DataTable.Rows[0]["NM_WC"].ToString());
                int index1 = 0;
                for (int index2 = 0; index2 < this.bpc작업장.DataTable.Rows.Count - 1; ++index2)
                    ++index1;
                reportHelper.SetData("작업장코드끝", this.bpc작업장.DataTable.Rows[index1]["CD_WC"].ToString());
                reportHelper.SetData("작업장명끝", this.bpc작업장.DataTable.Rows[index1]["NM_WC"].ToString());
                reportHelper.SetData("작업기간시작", this.dtp작업기간.StartDateToString);
                reportHelper.SetData("작업기간끝", this.dtp작업기간.EndDateToString);
                reportHelper.SetData("월계기간시작", this.dtp작업기간.StartDateToString);
                reportHelper.SetData("월계기간끝", this.dtp작업기간.EndDateToString);
                reportHelper.SetData("작업장체크여부", this.chk작업장.Checked ? "Check" : "Uncheck");
                reportHelper.SetData("공정체크여부", this.chk공정.Checked ? "Check" : "Uncheck");
                reportHelper.SetData("소분류체크여부", this.chk소분류.Checked ? "Check" : "Uncheck");
                reportHelper.SetData("품목체크여부", this.chk품목.Checked ? "Check" : "Uncheck");
                reportHelper.SetData("실적일체크여부", this.chk실적일.Checked ? "Check" : "Uncheck");
                DataTable dt = null;
                FlexGrid flexGrid = null;
                switch (this.m_tabWork.SelectedIndex)
                {
                    case 0:
                        flexGrid = this._flex공정별;
                        dt = this._flex공정별.DataTable;
                        break;
                    case 1:
                        flexGrid = this._flex품목별;
                        dt = this._flex품목별.DataTable;
                        break;
                }
                this.CaptionMapping(new FlexGrid[] { flexGrid }, ref dt);
                reportHelper.SetDataTable(dt);
                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ==================================================================== 컨트롤 이벤트
        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_WC_SUB1)
                {
                    if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        e.QueryCancel = true;
                        this.cbo공장.Focus();
                    }
                    else
                    {
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P65_CODE5 = this.cbo공장.Text.Replace(" ", "").Remove(0, this.cbo공장.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControlValidated(object sender, EventArgs e)
        {
            try
            {
                string name = ((Control)sender).Name;
                DatePicker datePicker = (DatePicker)sender;
                if (!datePicker.IsValidated)
                {
                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    datePicker.Text = "";
                    datePicker.Focus();
                }
                else
                {
                    switch (name)
                    {
                        case "m_dtFrom1":
                        case "m_dtTo1":
                            if (Convert.ToInt32(this.dtp작업기간.StartDateToString) > Convert.ToInt32(this.dtp작업기간.EndDateToString))
                            {
                                this.ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                                this.dtp작업기간.Focus();
                                break;
                            }
                            break;
                        case "m_dtFrom2":
                        case "m_dtTo2":
                            if (Convert.ToInt32(this.dtp조회기간.StartDateToString) > Convert.ToInt32(this.dtp조회기간.EndDateToString))
                            {
                                this.ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                                this.dtp작업기간.Focus();
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void BpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.ControlName)
                {
                    case "bpc작업장":
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void m_tabWork_Click(object sender, EventArgs e)
        {
            switch (((TabControl)sender).SelectedTab.Name)
            {
                case "m_tabOp":
                    this.chk품목.Checked = false;
                    this.chk대분류.Checked = false;
                    this.chk중분류.Checked = false;
                    this.chk소분류.Checked = false;
                    this.chk사원.Checked = false;
                    this.chk장비.Checked = false;
                    this.chk작업장.Enabled = true;
                    this.chk공정.Enabled = true;
                    this.chk대분류.Enabled = false;
                    this.chk중분류.Enabled = false;
                    this.chk소분류.Enabled = false;
                    this.chk품목.Enabled = true;
                    this.chk실적일.Enabled = false;
                    this.chk사원.Enabled = false;
                    this.chk장비.Enabled = false;
                    if (this._dsOp.Tables["PR_OP"] == null || this._dsOp.Tables["PR_OP"].Rows.Count <= 0 || this._dsOp.Tables["PR_OP_TOTAL"] == null || this._dsOp.Tables["PR_OP_TOTAL"].Rows.Count <= 0)
                        break;
                    this.setMonTotal(this._dsOp.Tables["PR_OP_TOTAL"], ref this._flex공정별);
                    break;
                case "m_tabItem":
                    if (this._flex품목별.HasNormalRow)
                        this.ToolBarPrintButtonEnabled = true;
                    else
                        this.ToolBarPrintButtonEnabled = false;
                    this.chk품목.Checked = true;
                    this.chk대분류.Checked = true;
                    this.chk중분류.Checked = true;
                    this.chk소분류.Checked = true;
                    this.chk사원.Checked = false;
                    this.chk장비.Checked = false;
                    this.chk작업장.Enabled = false;
                    this.chk공정.Enabled = false;
                    this.chk대분류.Enabled = true;
                    this.chk중분류.Enabled = true;
                    this.chk소분류.Enabled = true;
                    this.chk품목.Enabled = true;
                    this.chk실적일.Enabled = true;
                    this.chk사원.Enabled = false;
                    this.chk장비.Enabled = false;
                    if (this._dsItem.Tables["PR_ITEM"] == null || this._dsItem.Tables["PR_ITEM"].Rows.Count <= 0 || this._dsItem.Tables["PR_ITEM_TOTAL"] == null || this._dsItem.Tables["PR_ITEM_TOTAL"].Rows.Count <= 0)
                        break;
                    this.setMonTotal(this._dsItem.Tables["PR_ITEM_TOTAL"], ref this._flex품목별);
                    break;
                case "m_tabEmp":
                    if (this._flex작업자별.HasNormalRow)
                        this.ToolBarPrintButtonEnabled = true;
                    else
                        this.ToolBarPrintButtonEnabled = false;
                    this.chk작업장.Checked = true;
                    this.chk공정.Checked = false;
                    this.chk대분류.Checked = false;
                    this.chk중분류.Checked = false;
                    this.chk소분류.Checked = false;
                    this.chk사원.Checked = true;
                    this.chk장비.Checked = false;
                    this.chk작업장.Enabled = true;
                    this.chk공정.Enabled = false;
                    this.chk대분류.Enabled = false;
                    this.chk중분류.Enabled = false;
                    this.chk소분류.Enabled = false;
                    this.chk품목.Enabled = true;
                    this.chk실적일.Enabled = true;
                    this.chk사원.Enabled = true;
                    this.chk장비.Enabled = false;
                    if (this._dsEmp.Tables["PR_EMP"] == null || this._dsEmp.Tables["PR_EMP"].Rows.Count <= 0 || this._dsEmp.Tables["PR_EMP_TOTAL"] == null || this._dsEmp.Tables["PR_EMP_TOTAL"].Rows.Count <= 0)
                        break;
                    this.setMonTotal(this._dsEmp.Tables["PR_EMP_TOTAL"], ref this._flex작업자별);
                    break;
                case "m_tabEquip":
                    if (this._flex장비별.HasNormalRow)
                        this.ToolBarPrintButtonEnabled = true;
                    else
                        this.ToolBarPrintButtonEnabled = false;
                    this.chk작업장.Checked = true;
                    this.chk공정.Checked = false;
                    this.chk대분류.Checked = false;
                    this.chk중분류.Checked = false;
                    this.chk소분류.Checked = false;
                    this.chk사원.Checked = false;
                    this.chk장비.Checked = true;
                    this.chk작업장.Enabled = true;
                    this.chk공정.Enabled = false;
                    this.chk대분류.Enabled = false;
                    this.chk중분류.Enabled = false;
                    this.chk소분류.Enabled = false;
                    this.chk품목.Enabled = true;
                    this.chk실적일.Enabled = true;
                    this.chk사원.Enabled = false;
                    this.chk장비.Enabled = true;
                    if (this._dsEquip.Tables["PR_EQUIP"] == null || this._dsEquip.Tables["PR_EQUIP"].Rows.Count <= 0 || this._dsEquip.Tables["PR_EQUIP_TOTAL"] == null || this._dsEquip.Tables["PR_EQUIP_TOTAL"].Rows.Count <= 0)
                        break;
                    this.setMonTotal(this._dsEquip.Tables["PR_EQUIP_TOTAL"], ref this._flex장비별);
                    break;
            }
        }

        private void OnCheckBoxClick(object sender, EventArgs e)
        {
            try
            {
                if (this.m_tabWork.SelectedIndex == 0)
                {
                    if (this._dsOp.Tables["PR_OP"] == null || this._dsOp.Tables["PR_OP"].Rows.Count <= 0 || this._dsOp.Tables["PR_OP_TOTAL"] == null || this._dsOp.Tables["PR_OP_TOTAL"].Rows.Count <= 0)
                        return;
                    this.setMonTotal(this._dsOp.Tables["PR_OP_TOTAL"], ref this._flex공정별);
                }
                else if (this.m_tabWork.SelectedIndex == 1)
                {
                    if (this._dsItem.Tables["PR_ITEM"] == null || this._dsItem.Tables["PR_ITEM"].Rows.Count <= 0 || this._dsItem.Tables["PR_ITEM_TOTAL"] == null || this._dsItem.Tables["PR_ITEM_TOTAL"].Rows.Count <= 0)
                        return;
                    this.setMonTotal(this._dsItem.Tables["PR_ITEM_TOTAL"], ref this._flex품목별);
                }
                else if (this.m_tabWork.SelectedIndex == 2)
                {
                    if (this._dsEmp.Tables["PR_EMP"] == null || this._dsEmp.Tables["PR_EMP"].Rows.Count <= 0 || this._dsEmp.Tables["PR_EMP_TOTAL"] == null || this._dsEmp.Tables["PR_EMP_TOTAL"].Rows.Count <= 0)
                        return;
                    this.setMonTotal(this._dsEmp.Tables["PR_EMP_TOTAL"], ref this._flex작업자별);
                }
                else if (this.m_tabWork.SelectedIndex == 3)
                {
                    if (this._dsEquip.Tables["PR_EQUIP"] == null || this._dsEquip.Tables["PR_EQUIP"].Rows.Count <= 0 || this._dsEquip.Tables["PR_EQUIP_TOTAL"] == null || this._dsEquip.Tables["PR_EQUIP_TOTAL"].Rows.Count <= 0)
                        return;
                    this.setMonTotal(this._dsEquip.Tables["PR_EQUIP_TOTAL"], ref this._flex장비별);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
                this.ToolBarDeleteButtonEnabled = false;
                if (this.m_tabWork.SelectedIndex == 0)
                {
                    this.ToolBarPrintButtonEnabled = this._flex공정별.HasNormalRow;
                }
                else
                {
                    if (this.m_tabWork.SelectedIndex != 1)
                        return;
                    this.ToolBarPrintButtonEnabled = this._flex품목별.HasNormalRow;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ==================================================================== 기타 메소드

        private void setMonTotal(DataTable _dtMon, ref FlexGrid _flex)
        {
            _flex.Redraw = false;
            if (_dtMon != null && _dtMon.Rows.Count > 0)
            {
                CellStyle style1 = _flex.Styles[CellStyleEnum.Subtotal0];
                style1.BackColor = Color.Gray;
                style1.ForeColor = Color.White;
                style1.Font = new Font(_flex.Font, FontStyle.Bold);
                CellStyle style2 = _flex.Styles[CellStyleEnum.Subtotal1];
                style2.BackColor = Color.MediumBlue;
                style2.ForeColor = Color.White;
                CellStyle style3 = _flex.Styles[CellStyleEnum.Subtotal2];
                style3.BackColor = Color.MediumSeaGreen;
                style3.ForeColor = Color.Black;
                CellStyle style4 = _flex.Styles[CellStyleEnum.Subtotal3];
                style4.BackColor = Color.MediumAquamarine;
                style4.ForeColor = Color.Black;
                CellStyle style5 = _flex.Styles[CellStyleEnum.Subtotal4];
                style5.BackColor = Color.MediumTurquoise;
                style5.ForeColor = Color.Black;
                CellStyle style6 = _flex.Styles[CellStyleEnum.Subtotal5];
                style6.BackColor = Color.MediumPurple;
                style6.ForeColor = Color.Black;
                _flex.AllowDragging = AllowDraggingEnum.None;
                _flex.AllowEditing = false;
                _flex.Cols[0].WidthDisplay /= 3;
                _flex.Tree.Column = 1;
                _flex.Subtotal(AggregateEnum.Clear);
                string caption = this.lbl작업기간.Text + this.DD("합계") + "(" + this.DD("일계") + ")";
                string str = this.lbl조회기간.Text + this.DD("별합계") + "(" + this.DD("월계") + ")";
                int leftCol = _flex.Cols["DT_WORK"].Index + 1;
                for (int totalOn = leftCol; totalOn < _flex.Cols.Count; ++totalOn)
                {
                    _flex.Subtotal(AggregateEnum.Sum, 0, -1, totalOn, caption);
                    if (_flex.Name == "_flex공정별")
                    {
                        if (this.chk작업장.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 1, 1, totalOn, "Total for {0}");
                        if (this.chk공정.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 2, 3, totalOn, "Total for {0}");
                        if (this.chk품목.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 5, 7, totalOn, "Total for {0}");
                    }
                    else if (_flex.Name == "_flex품목별")
                    {
                        if (this.chk대분류.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 1, 1, totalOn, "Total for {0}");
                        if (this.chk중분류.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 2, 2, totalOn, "Total for {0}");
                        if (this.chk소분류.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 3, 3, totalOn, "Total for {0}");
                        if (this.chk품목.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 4, 4, totalOn, "Total for {0}");
                        if (this.chk실적일.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 5, 10, totalOn, "Total for {0}");
                    }
                    else if (_flex.Name == "_flex작업자별")
                    {
                        if (this.chk작업장.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 1, 1, totalOn, "Total for {0}");
                        if (this.chk사원.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 2, 2, totalOn, "Total for {0}");
                        if (this.chk실적일.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 3, 14, totalOn, "Total for {0}");
                        if (this.chk품목.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 4, 7, totalOn, "Total for {0}");
                    }
                    else if (_flex.Name == "_flex장비별")
                    {
                        if (this.chk작업장.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 1, 1, totalOn, "Total for {0}");
                        if (this.chk장비.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 2, 2, totalOn, "Total for {0}");
                        if (this.chk실적일.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 3, 14, totalOn, "Total for {0}");
                        if (this.chk품목.Checked)
                            _flex.Subtotal(AggregateEnum.Sum, 4, 7, totalOn, "Total for {0}");
                    }
                }
                for (int col = 1; col < leftCol; ++col)
                {
                    _flex[1, col] = str;
                    if (col >= 2)
                        _flex[1, col - 1] = "";
                }
                for (int index = 2; index < _flex.Rows.Count; ++index)
                    _flex.Rows[index]["RT_SCRAP"] = (D.GetDecimal(_flex.Rows[index]["QT_WORK"]) == 0M ? 0M : Math.Round(D.GetDecimal(_flex.Rows[index]["QT_REJECT"]) / D.GetDecimal(_flex.Rows[index]["QT_WORK"]) * 100M, 2));
                _flex[1, "QT_WORK"] = Convert.ToDouble(_dtMon.Rows[0]["QT_WORK"].ToString() == "" ? "0" : _dtMon.Rows[0]["QT_WORK"].ToString());
                _flex[1, "QT_REJECT"] = Convert.ToDouble(_dtMon.Rows[0]["QT_REJECT"].ToString() == "" ? "0" : _dtMon.Rows[0]["QT_REJECT"].ToString());
                _flex[1, "QT_MOVE"] = Convert.ToDouble(_dtMon.Rows[0]["QT_MOVE"].ToString() == "" ? "0" : _dtMon.Rows[0]["QT_MOVE"].ToString());
                _flex[1, "QT_INV"] = Convert.ToDouble(_dtMon.Rows[0]["QT_INV"].ToString() == "" ? "0" : _dtMon.Rows[0]["QT_INV"].ToString());
                _flex[1, "AM_WORK"] = Convert.ToDouble(_dtMon.Rows[0]["AM_WORK"].ToString() == "" ? "0" : _dtMon.Rows[0]["AM_WORK"].ToString());
                _flex[1, "RT_SCRAP"] = _dtMon.Rows[0]["RT_SCRAP"].ToString() == "" ? "0" : D.GetString(Math.Round(D.GetDecimal(_dtMon.Rows[0]["RT_SCRAP"]), 2));
                _flex[1, "WEIGHT_WORK"] = Convert.ToDouble(_dtMon.Rows[0]["WEIGHT_WORK"].ToString() == "" ? "0" : _dtMon.Rows[0]["WEIGHT_WORK"].ToString());
                _flex.AutoSizeCols();
                _flex.Rows[1].AllowMerging = true;
                _flex.GetCellRange(1, leftCol, 1, _flex.Cols.Count - 1).Style.Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.MONEY, FormatFgType.SELECT);
            }
            _flex.Redraw = true;
        }

        private void CaptionMapping(FlexGrid[] _flexArr, ref DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                foreach (FlexGrid flexGrid in _flexArr)
                {
                    if (flexGrid.Cols.Contains(column.ColumnName))
                        column.Caption = flexGrid.Cols[column.ColumnName].Caption;
                }
            }
        }

        #endregion
    }
}
