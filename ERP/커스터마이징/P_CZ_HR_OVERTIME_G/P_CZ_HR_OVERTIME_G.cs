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
using Duzon.ERPU;

namespace P_CZ_HR_OVERTIME_G
{
    public partial class P_CZ_HR_OVERTIME_G : PageBase
    {
        public string CD_COMPANY { get; set; }

        public string NO_EMP { get; set; }

        public string NM_EMP { get; set; }

        public string DT_REG { get; set; }

        public int SEQ { get; set; }

        public string NM_TITLE { get; set; }

        public string NO_DOCU
        {
            get
            {
                return flexH.HasNormalRow ? flexH["NO_DOCU"].ToString() : "";
            }
        }

        int NO_LINE = 0;

        public string ST_STAT { get; set; }

        public P_CZ_HR_OVERTIME_G()
        {
            Util.Certify(this);
            CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            NO_EMP = Global.MainFrame.LoginInfo.UserID;
            NM_EMP = Global.MainFrame.LoginInfo.UserName;
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
            //dtp작성일.StartDate. = Util.GetToday(-30);
            //dtp작성To.Text = Util.GetToday();

            tbx사번.Text = NO_EMP;
            tbx성명.Text = NM_EMP;
            tbx부서.Text = Global.MainFrame.LoginInfo.DeptName;

            Util.SetCON_ReadOnly(pnl사번, true);
            Util.SetCON_ReadOnly(pnl성명, true);
            Util.SetCON_ReadOnly(pnl부서, true);

            Util.SetDB_CODE(cbo구분, "CZ_HR00006", true);

            cbo구분.SelectedIndex = 1;
            Util.SetCON_ReadOnly(_pnl인정사유, true);
            tbx사후사유.Text = string.Empty;

            MainGrids = new FlexGrid[] { flexH, flexL };
            flexH.DetailGrids = new FlexGrid[] { flexL };
        }


        private void InitGrid()
        {
            // ================================================== H
            flexH.BeginSetting(1, 1, false);

            flexH.SetCol("SEQ", "SEQ", false);
            flexH.SetCol("CD_COMPANY", "회사", false);
            flexH.SetCol("NO_EMP", "사번", false);
            flexH.SetCol("DT_REG", "작성일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            flexH.SetCol("NM_TITLE", "제목", 250);
            flexH.SetCol("NO_DOCU", "문서번호", false);
            flexH.SetCol("ST_STAT", "결재", 70);

            flexH.Cols["DT_REG"].TextAlign = TextAlignEnum.CenterCenter;
            flexH.Cols["ST_STAT"].TextAlign = TextAlignEnum.CenterCenter;
            flexH.SetDataMap("ST_STAT", Util.GetDB_CODE("FI_J000031"), "CODE", "NAME");
            flexH.VerifyNotNull = new string[] { "SEQ" };
            flexL.SetOneGridBinding(new object[] { }, oneH);

            flexH.SettingVersion = "1.12.26.02";
            flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);



            // ================================================== L
            flexL.BeginSetting(1, 1, false);

            flexL.SetCol("SEQ", "SEQ", false);
            flexL.SetCol("CD_COMPANY", "회사", false);
            flexL.SetCol("NO_EMP", "사번", false);
            flexL.SetCol("DT_REG", "작성일", false);
            flexL.SetCol("ST_STAT", "결재", false);

            flexL.SetCol("NO_LINE", "순번", false);
            flexL.SetCol("CD_OCODE", "구분", 120);
            flexL.SetCol("DT_START", "시작일시", 120);
            flexL.SetCol("DT_END", "종료일시", 120);
            flexL.SetCol("DC_CONTENT", "업무내용", 200);
            flexL.SetCol("DC_REASON", "사유", 200);
            flexL.SetCol("DC_REASON2", "부서장승인사유", 200);
            flexL.SetCol("NO_DOCU", "문서번호", false);


            flexL.Cols["DT_START"].TextAlign = TextAlignEnum.CenterCenter;
            flexL.Cols["DT_END"].TextAlign = TextAlignEnum.CenterCenter;
            flexL.Cols["CD_OCODE"].TextAlign = TextAlignEnum.CenterCenter;
            flexL.SetDataMap("CD_OCODE", Util.GetDB_CODE("CZ_HR00006"), "CODE", "NAME");
            flexL.SetOneGridBinding(new object[] { }, oneL);

            flexL.SettingVersion = "1.12.26.01";
            flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }



        private void InitEvent()
        {
            btn전자결재.Click += new EventHandler(btn전자결재_Click);
            btn추가.Click += new EventHandler(btn추가_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);

            flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
            flexL.AfterRowChange += new RangeEventHandler(flexL_AfterRowChange);

            cbo구분.SelectionChangeCommitted += new EventHandler(cbo구분_SelectionChangeCommitted);
            dtp시작일자.ValueChanged += new EventHandler(dtp시작일자_ValueChanged);
            dtp종료일자.ValueChanged += new EventHandler(dtp종료일자_ValueChanged);
        }



        // 전자결재     ST_STAT 값 : 2:미상신, 0:진행중, 1:종결, -1:반려, 3:취소(삭제)
        private void btn전자결재_Click(object sender, EventArgs e)
        {
            // 결재 상태 체크
            string query = @"
SELECT
	A.NO_DOCU, B.ST_STAT
FROM	  CZ_HR_OVERTIME_GWH	AS A
LEFT JOIN FI_GWDOCU				AS B ON A.NO_DOCU = B.NO_DOCU
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.SEQ = '" + SEQ + @"'
	AND A.NO_EMP = '" + NO_EMP + "'";

            DataTable dt = DBMgr.GetDataTable(query);
            string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

            if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
            if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

            // html 만들기
            string html = @"
<div class='header'>
  ※ 추가연장근로 신청서
</div>
<table>
  <tr>
    <th width='5%'>순번</th>
    <th width='11%'>구분</th>
    <th width='18%'>시작일시</th>
    <th width='18%'>종료일시</th>
    <th width='15%'>업무내용</th>
    <th width='18%'>사유</th>
    <th width='15%'>사후인정사유</th>
  </tr>";

            int idx = 1;
            for (int i = flexL.Rows.Fixed; i < flexL.Rows.Count; i++)
            {
                html += @"
  <tr>
    <td>" + idx++ + @"</td>
    <td>" + ((DataTable)cbo구분.DataSource).Select("CODE = '" + flexL[i, "CD_OCODE"] + "'")[0]["NAME"] + @"</td>
    <td>" + flexL[i, "DT_START"].ToString() + @"</td>
    <td>" + flexL[i, "DT_END"].ToString() + @"</td>
    <td>" + flexL[i, "DC_CONTENT"] + @"</td>
    <td>" + flexL[i, "DC_REASON"] + @"</td>
    <td>" + flexL[i, "DC_REASON2"] + @"</td>
  </tr>";
            }

            html += @"
</table>";

            // html 업데이트 및 전자결재 팝업			
            query = @"
UPDATE CZ_HR_OVERTIME_GWH SET
	NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND SEQ = '" + SEQ + @"'
	AND NO_EMP = '" + NO_EMP + "'";


            query = query + " " + @"
UPDATE CZ_HR_OVERTIME_GWL SET
	NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND SEQ = '" + SEQ + @"'
	AND NO_EMP = '" + NO_EMP + "'";

            GroupWare.Save(flexH["NM_TITLE"].ToString(), html, NO_DOCU, 1019, query, true);
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            flexL.Rows.Add();
            flexL.Row = flexL.Rows.Count - 1;
            NO_LINE = flexL.Rows.Count - 1;

            flexL["CD_COMPANY"] = flexH["CD_COMPANY"];
            flexL["NO_EMP"] = flexH["NO_EMP"];
            flexL["DT_REG"] = flexH["DT_REG"];
            flexL["SEQ"] = flexH["SEQ"];

            cbo구분.SelectedIndex = 1;
            flexL["DT_START"] = dtp시작일자.Text;
            flexL["DT_END"] = dtp종료일자.Text;
            flexL["NO_LINE"] = Convert.ToString(NO_LINE);
            flexL.AddFinished();
        }


        private void btn삭제_Click(object sender, EventArgs e)
        {
            if (flexL.Rows.Count > 0)
                flexL.Rows.Remove(flexL.Row);
        }


        private void cbo구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbo구분.SelectedValue.ToString() == "3")
            {
                Util.SetCON_ReadOnly(_pnl인정사유, false);
            }
            else
            {
                Util.SetCON_ReadOnly(_pnl인정사유, true);
                tbx사후사유.Text = string.Empty;
            }
        }


        private void dtp시작일자_ValueChanged(object sender, EventArgs e)
        {
            timeCal();

            flexL["DT_START"] = dtp시작일자.Text;
        }

        private void dtp종료일자_ValueChanged(object sender, EventArgs e)
        {
            timeCal();

            flexL["DT_END"] = dtp종료일자.Text;
        }

        public void timeCal()
        {
            DateTime startT = new DateTime(dtp시작일자.Value.Year, dtp시작일자.Value.Month, dtp시작일자.Value.Day, dtp시작일자.Value.Hour, dtp시작일자.Value.Minute, 0);
            DateTime endT = new DateTime(dtp종료일자.Value.Year, dtp종료일자.Value.Month, dtp종료일자.Value.Day, dtp종료일자.Value.Hour, dtp종료일자.Value.Minute, 0);

            TimeSpan resultTime = endT - startT;

            lbl시간.Text = resultTime.Hours + "시간 " + resultTime.Minutes + "분";
        }

        private void flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DT_REG = Util.GetToday().Substring(0, 8);

            if (!string.IsNullOrEmpty(flexH["SEQ"].ToString()))
                SEQ = Convert.ToInt16(flexH["SEQ"].ToString());

            DataTable dtL = null;
            if (flexH.DetailQueryNeed) dtL = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_GW_CLICK", new object[] { CD_COMPANY, NO_EMP, SEQ });
            flexL.BindingAdd(dtL, "SEQ = '" + SEQ + "'");

            // L ROW가 있을시, 코드 값에 따라 컨트롤 상태 변화
            if (flexL != null && flexL.Rows.Count > 1)
            {
                if (!string.IsNullOrEmpty(flexL["DT_START"].ToString()))
                    dtp시작일자.Value = Convert.ToDateTime(flexL["DT_START"].ToString());
                if (!string.IsNullOrEmpty(flexL["DT_END"].ToString()))
                    dtp종료일자.Value = Convert.ToDateTime(flexL["DT_END"].ToString());

                timeCal();

                if (flexL["CD_OCODE"].ToString().Equals("3"))
                {
                    Util.SetCON_ReadOnly(_pnl인정사유, false);
                }
                else
                {
                    Util.SetCON_ReadOnly(_pnl인정사유, true);
                    tbx사후사유.Text = string.Empty;
                }
            }

            if (Util.GetTO_String(flexH["ST_STAT"]).Equals("0") || Util.GetTO_String(flexH["ST_STAT"]).Equals("1"))
            {
                btn전자결재.Enabled = false;
                btn추가.Enabled = false;
                btn삭제.Enabled = false;
                //Util.SetCON_ReadOnly(_pnl제목, true);
                Util.SetCON_ReadOnly(pnl구분, true);
                Util.SetCON_ReadOnly(pnl기간, true);
                Util.SetCON_ReadOnly(pnl사유, true);
                Util.SetCON_ReadOnly(_pnl사유, true);

                flexL.AllowEditing = false;
            }
            else
            {
                btn전자결재.Enabled = true;
                btn추가.Enabled = true;
                btn삭제.Enabled = true;
                //Util.SetCON_ReadOnly(pnl제목, false);
                Util.SetCON_ReadOnly(pnl구분, false);
                Util.SetCON_ReadOnly(pnl기간, false);
                Util.SetCON_ReadOnly(pnl사유, false);
                Util.SetCON_ReadOnly(_pnl사유, false);

                flexL.AllowEditing = true;
            }

            if (flexH["NM_TITLE"] != null)
            {
                NM_TITLE = flexH["NM_TITLE"].ToString();
                tbx제목.Text = NM_TITLE.Replace("제목", "");
            }
        }

        private void flexL_AfterRowChange(object sender, RangeEventArgs e)
        {
            if (flexL != null && flexL.Rows.Count > 1)
            {
                if (!string.IsNullOrEmpty(flexL["DT_START"].ToString()))
                    dtp시작일자.Value = Convert.ToDateTime(flexL["DT_START"].ToString());
                if (!string.IsNullOrEmpty(flexL["DT_END"].ToString()))
                    dtp종료일자.Value = Convert.ToDateTime(flexL["DT_END"].ToString());

                timeCal();

                if (flexL["CD_OCODE"].ToString().Equals("3"))
                {
                    Util.SetCON_ReadOnly(_pnl인정사유, false);
                }
                else
                {
                    Util.SetCON_ReadOnly(_pnl인정사유, true);
                    tbx사후사유.Text = string.Empty;
                }

                if (flexH["NM_TITLE"] != null)
                {
                    NM_TITLE = flexH["NM_TITLE"].ToString();
                    tbx제목.Text = NM_TITLE.Replace("제목", "");
                }
            }
        }

        // ============================== 기능 ============================== //

        #region 조회
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_HR_OVERTIME_GW_S", new object[] { CD_COMPANY, NO_EMP, dtp작성일.StartDate.ToString(), dtp작성일.EndDate.ToString() });

            flexH.Redraw = false;
            flexL.Redraw = false;

            flexH.Binding = dt;
            flexH.Row = flexH.Rows.Count - 1;

            if (flexH["NM_TITLE"] != null)
            {
                NM_TITLE = flexH["NM_TITLE"].ToString();
                tbx제목.Text = NM_TITLE.Replace("제목", "");
            }


            flexH.Redraw = true;
            flexL.Redraw = true;

            if (!flexH.HasNormalRow)
            {
                tbx사유.Text = string.Empty;
                tbx사후사유.Text = string.Empty;
                tbx업무내용.Text = string.Empty;
                tbx제목.Text = string.Empty;
                NM_TITLE = string.Empty;

                lbl시간.Text = "";
                cbo구분.SelectedIndex = 1;

                ShowMessage(공통메세지.조건에해당하는내용이없습니다);

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
                DT_REG = Util.GetToday().Substring(0, 8);

                DataTable dtH = flexH.DataTable;
                if (dtH.Select("NO_DOCU = 'NEW'").Length > 0) { ShowMessage("작성중인 문서가 있습니다."); return; }

                if (dtH.Rows.Count >= 1)
                    SEQ = Convert.ToInt16(dtH.Compute("MAX(SEQ)", "")) + 1;

                flexH.Rows.Add();
                flexH.Row = flexH.Rows.Count - 1;
                flexH["DT_REG"] = DT_REG;
                flexH["NO_EMP"] = NO_EMP;
                flexH["NM_TITLE"] = NM_EMP + "_추가연장근로신청서";
                tbx제목.Text = NM_EMP + "_추가연장근로신청서";
                Util.SetCON_ReadOnly(pnl제목, false);
                flexH["NO_DOCU"] = "NEW";
                flexH["CD_COMPANY"] = CD_COMPANY;
                flexH["SEQ"] = SEQ;

                flexH.AddFinished();

                btn추가_Click(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

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
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;

            if (flexL["DT_START"] == null) { ShowMessage("시작일시를 입력하세요"); return false; }
            if (flexL["DT_END"] == null) { ShowMessage("종료일시를 입력하세요"); return false; }
            if (lbl시간.Text.StartsWith("-")) { ShowMessage("시간을 확인하세요"); return false; }

            if (flexH.Rows.Count > 1)
            {
                if (flexH["NO_DOCU"].ToString().Equals("NEW"))
                    flexH["NO_DOCU"] = "";

                flexH["CD_COMPANY"] = CD_COMPANY;
                flexH["DT_REG"] = DateTime.Now.ToString("yyyyMMdd");
                ST_STAT = flexH["ST_STAT"].ToString();
            }

            if (ST_STAT == null)
            {
                ST_STAT = string.Empty;
            }

            if (ST_STAT.Equals("2") || string.IsNullOrEmpty(ST_STAT))
            {
                DataTable dtH = this.flexH.GetChanges();
                DataTable dtL = this.flexL.GetChanges();

                string xmlH = Util.GetTO_Xml(dtH);
                string xmlL = Util.GetTO_Xml(dtL);
                DBMgr.ExecuteNonQuery("SP_CZ_HR_OVERTIME_GW_XML", new object[] { xmlH, xmlL });

                flexH.AcceptChanges();
                flexL.AcceptChanges();

                OnToolBarSearchButtonClicked(null, null);

                return true;
            }
            else
            {
                Util.ShowMessage("결재 진행 중이므로 수정할 수 없습니다.");
                return false;
            }
        }

        #endregion 저장

        #region  삭제

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            if (!base.BeforeDelete() || !flexH.HasNormalRow) return;
            if (!this.flexH.HasNormalRow) return;

            flexH.Rows.Remove(flexH.Row);
        }

        #endregion
    }
}
