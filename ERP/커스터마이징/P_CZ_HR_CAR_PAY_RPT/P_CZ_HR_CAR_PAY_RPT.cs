using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;

namespace cz
{
	public partial class P_CZ_HR_CAR_PAY_RPT : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string YM
		{
			get
			{
				return flexH.HasNormalRow ? flexH["YM"].ToString() : "";
			}
		}

		public string NO_EMP
		{
			get
			{
				return flexH.HasNormalRow ? flexH["NO_EMP"].ToString() : "";
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_CAR_PAY_RPT()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			//txt포커스.Left = -500;

			dtp급여반영월.Text = Util.GetToday();

			DataTable dt = new DataTable();
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");
			dt.Rows.Add("", "");
			dt.Rows.Add("Y", "처리");
			dt.Rows.Add("N", "미처리");

			cbo처리구분.ValueMember = "CODE";
			cbo처리구분.DisplayMember = "NAME";
			cbo처리구분.DataSource = dt;

			flexH.DetailGrids = new FlexGrid[] { flexL };
		}

		private void InitGrid()
		{
			// ================================================== H
			flexH.BeginSetting(1, 1, false);

			flexH.SetCol("CHK"		, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexH.SetCol("YM"		, "급여반영월"	, 80	, false, typeof(string), FormatTpType.YEAR_MONTH);
			flexH.SetCol("NO_EMP"	, "사번"			, 70);
			flexH.SetCol("NM_EMP"	, "성명"			, 80);
			flexH.SetCol("NM_DEPT"	, "부서"			, 90);
			flexH.SetCol("NM_TITLE"	, "제목"			, 230);
			flexH.SetCol("DT_REG"	, "작성일"		, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("AM"		, "금액"			, 70	, false, typeof(decimal), FormatTpType.MONEY);
            flexH.SetCol("YN_PAY", "처리", 60, false, CheckTypeEnum.Y_N);

			flexH.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["NM_DEPT"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["YN_PAY"].TextAlign = TextAlignEnum.CenterCenter;

			flexH.SettingVersion = "15.11.25.10";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			// ================================================== L
			flexL.BeginSetting(1, 1, false);
	
			flexL.SetCol("YM"		, "급여반영월"	, false);
			flexL.SetCol("NO_EMP"	, "사번"			, false);
			flexL.SetCol("DT_WORK"	, "운행일자"		, 80	, false, typeof(string)	, FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("NM_TRIP"	, "출장지"		, 120);
			flexL.SetCol("DC_TRIP"	, "출장목적"		, 180);			
			flexL.SetCol("DISTANCE"	, "거리"			, 55	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("UM_KM"	, "단가"			, 55	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_OIL"	, "유류비"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_PARK"	, "주차비"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_TOLL"	, "통행료"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM"		, "합계"			, 70	, false, typeof(decimal), FormatTpType.MONEY);
			
			flexL.SettingVersion = "15.11.25.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("UM_KM");
		}

		private void InitEvent()
		{
            this.btn승인.Click += new EventHandler(this.btn승인_Click);
            this.btn승인취소.Click += new EventHandler(this.btn승인_Click);
            this.btn전표처리.Click += new EventHandler(this.btn전표처리_Click);
            this.btn전표취소.Click += new EventHandler(this.btn전표처리_Click);
            this.btn전표이동.Click += new EventHandler(this.btn전표이동_Click);
            
			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
		}

		protected override void InitPaint()
		{
			//txt포커스.Focus();
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr dbm = new DBMgr(DBConn.iU);
			dbm.Procedure = "SP_CZ_HR_CAR_PAYH_RPT_SELECT";
			dbm.AddParameter("CD_COMPANY"	, CD_COMPANY);
			dbm.AddParameter("YM"			, dtp급여반영월.Text);
			//dbm.AddParameter("NO_EMP"		, NO_EMP);
			dbm.AddParameter("YN_PAY"		, cbo처리구분.SelectedValue);
			DataTable dt = dbm.GetDataTable();

			flexH.Binding = dt;
			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn승인_Click(object sender, EventArgs e)
		{
            string name;

            try
            {
                if (!this.flexH.HasNormalRow) return;

                name = ((Control)sender).Name;
                DataTable dtH = flexH.GetCheckedRows("CHK");

                if (dtH == null || dtH.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dtH.Rows)
                    {
                        if (name == this.btn승인.Name)
                            dr["YN_PAY"] = "Y";
                        else
                            dr["YN_PAY"] = "N";
                    }

                    string xmlH = Util.GetTO_Xml(dtH);
                    DBMgr.ExecuteNonQuery("SP_CZ_HR_CAR_PAYH_RPT_XML", new object[] { xmlH });

                    this.flexH.AcceptChanges();
                    this.ShowMessage(공통메세지._작업을완료하였습니다, ((Control)sender).Text);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
		}

        private void btn전표처리_Click(object sender, EventArgs e)
        {
            string name, 참조번호;
            bool result;

            try
            {
                if (!this.flexH.HasNormalRow) return;

                name = ((Control)sender).Name;
                DataTable dtH = flexH.GetCheckedRows("YN_PAY");

                if (dtH == null || dtH.Rows.Count == 0)
                {
                    this.ShowMessage("승인처리된 자료가 존재하지 않습니다.");
                    return;
                }
                else
                {
                    if (name == this.btn전표처리.Name)
                    {
                        result = DBHelper.ExecuteNonQuery("SP_CZ_HR_CAR_PAY_DOCU", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  this.dtp급여반영월.Text,
                                                                                                  "D02",
                                                                                                  Global.MainFrame.LoginInfo.UserID });

                        if (result)
                        {
                            this.OnToolBarSearchButtonClicked(null, null);
                            this.ShowMessage("전표가 처리되었습니다");
                        }
                    }
                    else
                    {
                        참조번호 = "CAR_" + this.dtp급여반영월.Text;

                        result = DBHelper.ExecuteNonQuery("UP_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                               "D02",
                                                                                               참조번호,
                                                                                               Global.MainFrame.LoginInfo.UserID });

                        if (result)
                        {
                            this.OnToolBarSearchButtonClicked(null, null);
                            this.ShowMessage("전표가 취소되었습니다");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표이동_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this.flexH["NO_DOCU"])))
                {
                    this.ShowMessage("생성된 전표가 없습니다.");
                    return;
                }

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this.flexH["NO_DOCU"]),
                                                                                                                                 "1",
                                                                                                                                 Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dtL = null;

			if (flexH.DetailQueryNeed) dtL = DBMgr.GetDataTable("SP_CZ_HR_CAR_PAYL_RPT_SELECT", new object[] { CD_COMPANY, YM, NO_EMP });
			flexL.BindingAdd(dtL, "YM = '" + YM + "' AND NO_EMP = '" + NO_EMP + "'");
		}

		#endregion
	}
}
