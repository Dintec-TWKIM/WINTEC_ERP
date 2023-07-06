using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
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
    public partial class P_CZ_PR_MATCHING_DEACTIVATE_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PR_MATCHING_DEACTIVATE_SUB_BIZ _biz = new P_CZ_PR_MATCHING_DEACTIVATE_SUB_BIZ();

        public P_CZ_PR_MATCHING_DEACTIVATE_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_ID", "일련번호", 100);
            this._flex.SetCol("STA_DEACTIVATE", "상태", 100);
            this._flex.SetCol("NO_WO", "작업지시번호", 100);
            this._flex.SetCol("CD_PITEM", "품목코드", 100);
            this._flex.SetCol("NM_ITEM", "품목명", 100);
            this._flex.SetCol("STND_ITEM", "규격", 100);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex.SetCol("NUM_P1_OUT", "측정치1(외경)", 100, true);
            this._flex.SetCol("NUM_P2_OUT", "측정치2(외경)", 100, true);
            this._flex.SetCol("NUM_P3_OUT", "측정치3(외경)", 100, true);
            this._flex.SetCol("NUM_P1_IN", "측정치1(내경)", 100, true);
            this._flex.SetCol("NUM_P2_IN", "측정치2(내경)", 100, true);
            this._flex.SetCol("NUM_P3_IN", "측정치3(내경)", 100, true);
            this._flex.SetCol("DC_RMK", "비고", 100, true);
            this._flex.SetCol("NM_INSERT", "등록자", 100);
            this._flex.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE", "수정자", 100);
            this._flex.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flex.SetDataMap("STA_DEACTIVATE", MA.GetCode("CZ_WIN0010", false), "CODE", "NAME");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this.btn수정.Click += new EventHandler(this.btn수정_Click);
            this.btn분실.Click += new EventHandler(this.btn분실_Click);
            this.btn폐기.Click += new EventHandler(this.btn폐기_Click);

            this.ctx품목.QueryBefore += new BpQueryHandler(this.ctx품목_QueryBefore);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("N;MA_PLANT");
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            this.cbo상태.DataSource = MA.GetCode("CZ_WIN0010", true);
            this.cbo상태.DisplayMember = "NAME";
            this.cbo상태.ValueMember = "CODE";
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.cbo공장.SelectedValue,
                                                                     this.cbo상태.SelectedValue,
                                                                     this.ctx품목.CodeValue });

                if (this._flex.DataTable == null || this._flex.DataTable.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.IsDataChanged) return;

                if (!this._biz.Save(this._flex.GetChanges()))
                    return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn수정_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["STA_DEACTIVATE"] = "002";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn분실_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["STA_DEACTIVATE"] = "003";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn폐기_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["STA_DEACTIVATE"] = "004";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx품목_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                {
                    Global.MainFrame.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장.Text });

                    this.cbo공장.Focus();
                    e.QueryCancel = true;
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
