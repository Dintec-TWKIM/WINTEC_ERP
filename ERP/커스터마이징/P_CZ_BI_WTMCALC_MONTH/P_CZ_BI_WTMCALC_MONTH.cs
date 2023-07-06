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
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Dintec;


namespace cz
{
    public partial class P_CZ_BI_WTMCALC_MONTH : PageBase
    {
        

        public P_CZ_BI_WTMCALC_MONTH()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.IniEvent();

            DateTime dayToday = DateTime.Now.Date;

            dtp일자.StartDate = dayToday.AddDays(1-dayToday.Day);
            dtp일자.EndDate = dayToday;
            lbTitle.Text = dtp일자.StartDate.Year + "년 " + dtp일자.StartDate.Month + "월 근태 현황 보고";
        }

        private void IniEvent()
        {
            btn비고.Click += new EventHandler(btn비고_Click);
        }

        

        private void InitGrid()
        {
            flex.BeginSetting(2, 1, true);

            flex.SetCol("NM_DEPT", "부서", 85);
            flex.SetCol("NO_EMP", "사번", false);
            flex.SetCol("NM_KOR", "성명", 70);
            flex.SetCol("NM_DUTY_RANK", "직급", 90);
            flex.SetCol("CD_DUTY_RANK", "직급코드", false);

            flex.SetCol("BEFORE_EIGHT", "8시이전", 57);
            flex.SetCol("EIGHT_TO_NINE_A", "8-9시", 50);
            flex.SetCol("LATENESS", "지각", 40);
            flex.SetCol("AMOUT", "오전반차", 57);

            flex.SetCol("BEFORE_SIX", "6시이전", 57);
            flex.SetCol("SIX_TO_SEVEN", "6-7시", 50);
            flex.SetCol("SEVEN_TO_EIGHT", "7-8시", 50);
            flex.SetCol("EIGHT_TO_NINE_P", "8-9시", 50);
            flex.SetCol("NINE_TO_TEN", "9-10시", 50);
            flex.SetCol("AFTER_TEN", "10시이후", 60);
            flex.SetCol("PMOUT", "오후반차", 57);

            flex.SetCol("ANNUAL_LEAVE", "연차", 50);
            flex.SetCol("WORK_OUT", "출장/외근", 80);
            flex.SetCol("HOLI", "휴일출근", 70);
            flex.SetCol("RMK", "비고", 150);
            flex.SetCol("WEEK_1", "1주", 95);
            flex.SetCol("WEEK_2", "2주", 95);
            flex.SetCol("WEEK_3", "3주", 95);
            flex.SetCol("WEEK_4", "4주", 95);
            flex.SetCol("WEEK_5", "5주", 95);
            flex.SetCol("MM_TOTAL", "월 누적", 95);

            flex[0, flex.Cols["BEFORE_EIGHT"].Index] = "출근";
            flex[0, flex.Cols["EIGHT_TO_NINE_A"].Index] = "출근";
            flex[0, flex.Cols["LATENESS"].Index] = "출근";
            flex[0, flex.Cols["AMOUT"].Index] = "출근";

            flex[0, flex.Cols["BEFORE_SIX"].Index] = "퇴근";
            flex[0, flex.Cols["SIX_TO_SEVEN"].Index] = "퇴근";
            flex[0, flex.Cols["SEVEN_TO_EIGHT"].Index] = "퇴근";
            flex[0, flex.Cols["EIGHT_TO_NINE_P"].Index] = "퇴근";
            flex[0, flex.Cols["NINE_TO_TEN"].Index] = "퇴근";
            flex[0, flex.Cols["AFTER_TEN"].Index] = "퇴근";
            flex[0, flex.Cols["PMOUT"].Index] = "퇴근";

            flex[0, flex.Cols["WEEK_1"].Index] = "근무시간";
            flex[0, flex.Cols["WEEK_2"].Index] = "근무시간";
            flex[0, flex.Cols["WEEK_3"].Index] = "근무시간";
            flex[0, flex.Cols["WEEK_4"].Index] = "근무시간";
            flex[0, flex.Cols["WEEK_5"].Index] = "근무시간";
            flex[0, flex.Cols["MM_TOTAL"].Index] = "근무시간";


            flex.Cols["NM_DEPT"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["NM_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["CD_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;

            flex.Cols["BEFORE_EIGHT"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["EIGHT_TO_NINE_A"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["LATENESS"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["AMOUT"].TextAlign = TextAlignEnum.CenterCenter;

            flex.Cols["BEFORE_SIX"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["SIX_TO_SEVEN"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["SEVEN_TO_EIGHT"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["EIGHT_TO_NINE_P"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["NINE_TO_TEN"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["AFTER_TEN"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["PMOUT"].TextAlign = TextAlignEnum.CenterCenter;

            flex.Cols["ANNUAL_LEAVE"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["WORK_OUT"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["HOLI"].TextAlign = TextAlignEnum.CenterCenter;
            flex.Cols["RMK"].TextAlign = TextAlignEnum.CenterCenter;

            flex.Cols["WEEK_1"].TextAlign = TextAlignEnum.RightCenter;
            flex.Cols["WEEK_2"].TextAlign = TextAlignEnum.RightCenter;
            flex.Cols["WEEK_3"].TextAlign = TextAlignEnum.RightCenter;
            flex.Cols["WEEK_4"].TextAlign = TextAlignEnum.RightCenter;
            flex.Cols["WEEK_5"].TextAlign = TextAlignEnum.RightCenter;
            flex.Cols["MM_TOTAL"].TextAlign = TextAlignEnum.RightCenter;

            CellStyle style = flex.Styles.Add("HOLI_STYLE");
            style.ForeColor = Color.Blue;
            

            style = flex.Styles.Add("LATE_STYLE");
            style.ForeColor = Color.Red;

            style = flex.Styles.Add("ANNUAL_STYLE");
            style.ForeColor = Color.Green;

            style = flex.Styles.Add("DEFAULT");
            style.ForeColor = Color.Black;

            flex.KeyActionEnter = KeyActionEnum.MoveDown;
            flex.SettingVersion = "18.10.10.28";
            flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            flex.Rows[0].Height = 24;
            flex.Rows[1].Height = 24;
        }


        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string yearStr = dtp일자.StartDate.Year.ToString();
            string monthStr = dtp일자.StartDate.Month.ToString();

            if(monthStr.Length == 1)
            {
                monthStr = "0" + monthStr;
            }

            string dateMon = yearStr + monthStr;


            


            string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            string DT_MONTH = dateMon.Substring(0, 4) + "-" + dateMon.Substring(4, 2);
            string NO_EMP = Global.MainFrame.LoginInfo.UserID;

            lbTitle.Text = dateMon.Substring(0,4).Trim() + "년 " + dateMon.Substring(4,2).Trim() + "월 " +" 근태 현황 보고";

            string DT_F = dtp일자.StartDate.ToString("yyyy-MM-dd");
            string DT_T = dtp일자.EndDate.ToString("yyyy-MM-dd");


            string DT_F_WORK = dtp일자.StartDate.ToString("yyyyMMdd");
            string DT_T_WORK = dtp일자.EndDate.ToString("yyyyMMdd");



            DataTable dt = DBHelper.GetDataTable("PS_CZ_BI_WTMCALC_MONTH", new object[] { CD_COMPANY, DT_F, DT_T, DT_MONTH, NO_EMP, dateMon, DT_F_WORK, DT_T_WORK });
            DataTable dt2 = DBHelper.GetDataTable("PS_CZ_BI_WTMCALC_MONTH_S", new object[] { CD_COMPANY, NO_EMP });

            flex.Redraw = false;

            flex.Binding = dt;
            flex.Row = flex.Rows.Count - 1;

            flex.Redraw = true;

            lbWorkDay.Text = "근무일수 : " + flex["WORK_DAY"].ToString() + " 일";
            lblWeek.Text = "[ 1주차: " + dtp일자.StartDate.ToString("MM월 dd일") + "  /  2주차: " + dtp일자.StartDate.AddDays(7).ToString("MM월 dd일") + "  /  3주차: " + dtp일자.StartDate.AddDays(14).ToString("MM월 dd일") + "  /  4주차: " + dtp일자.StartDate.AddDays(21).ToString("MM월 dd일") + "  /  5주차: " + dtp일자.StartDate.AddDays(28).ToString("MM월 dd일") + " ]";
            lblWeek.ForeColor = Color.Red;

            if (!flex.HasNormalRow)
            {
                ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            else
            {
                for (int i = 1; i < flex.Rows.Count; i++)
                {
                    flex.SetCellStyle(i, flex.Cols["HOLI"].Index, "HOLI_STYLE");

                    if (i > 1)
                    {
                        int lateCount = Convert.ToInt16(flex.Rows[i][8].ToString());
                        int AMOUTCount = Convert.ToInt16(flex.Rows[i][9].ToString());
                        int PMOUTCount = Convert.ToInt16(flex.Rows[i][16].ToString());
                        int WORKOUTCount = Convert.ToInt16(flex.Rows[i][17].ToString());
                        int annualCount = Convert.ToInt16(flex.Rows[i][18].ToString());

                        if (lateCount > 0)
                            flex.SetCellStyle(i, flex.Cols["LATENESS"].Index, "LATE_STYLE");
                        else
                            flex.SetCellStyle(i, flex.Cols["LATENESS"].Index, "DEFAULT");

                        //if (AMOUTCount > 0)
                        //    flex.SetCellStyle(i, flex.Cols["AMOUT"].Index, "ANNUAL_STYLE");
                        //else
                        //    flex.SetCellStyle(i, flex.Cols["AMOUT"].Index, "DEFAULT");

                        //if (PMOUTCount > 0)
                        //    flex.SetCellStyle(i, flex.Cols["PMOUT"].Index, "ANNUAL_STYLE");
                        //else
                        //    flex.SetCellStyle(i, flex.Cols["PMOUT"].Index, "DEFAULT");

                        //if (WORKOUTCount > 0)
                        //    flex.SetCellStyle(i, flex.Cols["WORK_OUT"].Index, "ANNUAL_STYLE");
                        //else
                        //    flex.SetCellStyle(i, flex.Cols["WORK_OUT"].Index, "DEFAULT");

                        //if (annualCount > 0)
                        //    flex.SetCellStyle(i, flex.Cols["ANNUAL_LEAVE"].Index, "ANNUAL_STYLE");
                        //else
                        //    flex.SetCellStyle(i, flex.Cols["ANNUAL_LEAVE"].Index, "DEFAULT");
                    }
                }
            }

            근태현황CellRange();
        }


        private void 근태현황CellRange()
        {
            SubTotals subTotals;
            CellRange cellRange;

            try
            {
                if (!this.flex.HasNormalRow) return;

                this.flex.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this.flex.Rows.Fixed;

                for (int i = this.flex.Rows.Fixed; i < this.flex.Rows.Count; i++)
                {
                    cellRange = this.flex.GetCellRange(i, "NM_DEPT", i, "NM_DEPT");
                    cellRange.UserData = this.flex[i, "NM_DEPT"].ToString();
                }

                this.flex.DoMerge();

                this.flex.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void btn비고_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_BI_WTMCALC_MONTH_RMK rmk = new P_CZ_BI_WTMCALC_MONTH_RMK();
                rmk.ShowDialog(this);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }
    }
}
