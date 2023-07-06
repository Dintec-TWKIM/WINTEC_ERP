using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
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
    public partial class P_CZ_PR_MATCHING_ID_OLD_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PR_MATCHING_ID_OLD_SUB_BIZ _biz = new P_CZ_PR_MATCHING_ID_OLD_SUB_BIZ();

        public P_CZ_PR_MATCHING_ID_OLD_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(2, 1, true);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_USE", "사용여부", 40, false, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_ID", "가공ID", 100);
            this._flex.SetCol("CD_ITEM", "품목코드", 100);
            this._flex.SetCol("NM_ITEM", "품목명", 100, false);
            this._flex.SetCol("DC_SPEC_OUT", "사양", 100);
            this._flex.SetCol("NO_DATA_OUT", "측정치", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("CD_CLEAR_GRP_OUT", "클리어런스그룹", 100);
            this._flex.SetCol("DC_SPEC_IN", "사양", 100);
            this._flex.SetCol("NO_DATA_IN", "측정치", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("CD_CLEAR_GRP_IN", "클리어런스그룹", 100);
            this._flex.SetCol("NO_HEAT", "소재HEAT번호", 100);
            this._flex.SetCol("NO_LOT", "열처리LOT번호", 100);
            this._flex.SetCol("DC_RMK", "비고", 100);

            this._flex[0, this._flex.Cols["DC_SPEC_OUT"].Index] = "외경";
            this._flex[0, this._flex.Cols["NO_DATA_OUT"].Index] = "외경";
            this._flex[0, this._flex.Cols["CD_CLEAR_GRP_OUT"].Index] = "외경";
            this._flex[0, this._flex.Cols["DC_SPEC_IN"].Index] = "내경";
            this._flex[0, this._flex.Cols["NO_DATA_IN"].Index] = "내경";
            this._flex[0, this._flex.Cols["CD_CLEAR_GRP_IN"].Index] = "내경";

            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex.SetDataMap("CD_CLEAR_GRP_IN", DBHelper.GetDataTable(string.Format(@"SELECT '' AS CODE, '' AS NAME
                                                                                           UNION ALL
                                                                                           SELECT MC.CD_SYSDEF AS CODE,
                                                                                                  MC.NM_SYSDEF AS NAME
                                                                                           FROM MA_CODEDTL MC WITH(NOLOCK)
                                                                                           WHERE MC.CD_COMPANY = '{0}'
                                                                                           AND MC.CD_FIELD = 'CZ_WIN0013'
                                                                                           AND MC.CD_FLAG1 = 'IN'
                                                                                           ORDER BY CODE ASC", Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");
            this._flex.SetDataMap("CD_CLEAR_GRP_OUT", DBHelper.GetDataTable(string.Format(@"SELECT '' AS CODE, '' AS NAME
                                                                                           UNION ALL
                                                                                           SELECT MC.CD_SYSDEF AS CODE,
                                                                                                  MC.NM_SYSDEF AS NAME
                                                                                           FROM MA_CODEDTL MC WITH(NOLOCK)
                                                                                           WHERE MC.CD_COMPANY = '{0}'
                                                                                           AND MC.CD_FIELD = 'CZ_WIN0013'
                                                                                           AND MC.CD_FLAG1 = 'OUT'
                                                                                           ORDER BY CODE ASC", Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

			this.ctx품목코드.QueryBefore += Ctx품목코드_QueryBefore;

			this._flex.BeforeCodeHelp += _flex_BeforeCodeHelp;
			this._flex.ValidateEdit += _flex_ValidateEdit;
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            DataTable dt;
            string columnName, newValue, query;

            try
            {
                columnName = this._flex.Cols[e.Col].Name;

                if (columnName != "NO_ID") return;

                newValue = this._flex.EditData;

                query = @"SELECT WD.NO_ID 
		                  FROM CZ_PR_WO_REQ_D WD
                          WHERE WD.CD_COMPANY = '{0}'
                          AND WD.NO_ID = '{1}'";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, newValue));

                if (dt != null && dt.Rows.Count > 0)
				{
                    Global.MainFrame.ShowMessage("작업진행 중인 가공ID는 등록할 수 없습니다.");
                    e.Cancel = true;
				}
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Ctx품목코드_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo사용여부.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "사용", "미사용" }, true);
            this.cbo사용여부.ValueMember = "CODE";
            this.cbo사용여부.DisplayMember = "NAME";
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Rows.Add();

                this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

                this._flex.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
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
                    return;
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND ISNULL(YN_USE, 'N') = 'Y'").Length > 0)
				{
                    Global.MainFrame.ShowMessage("현합 적용된 데이터는 삭제 할 수 없습니다.");
                    return;
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
                                                                     Global.MainFrame.LoginInfo.CdPlant,
                                                                     this.ctx품목코드.CodeValue,
                                                                     this.txt가공ID.Text,
                                                                     this.cbo사용여부.SelectedValue });

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
    }
}
