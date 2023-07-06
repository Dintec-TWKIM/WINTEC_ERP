using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;

namespace cz
{
	public partial class P_CZ_MA_WORKFLOW_CHANGE : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_MA_WORKFLOW_CHANGE_BIZ _biz = new P_CZ_MA_WORKFLOW_CHANGE_BIZ();

		public P_CZ_MA_WORKFLOW_CHANGE()
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

			this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			this.cbo담당자변경.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_SA00024");
			this.cbo담당자변경.ValueMember = "CODE";
			this.cbo담당자변경.DisplayMember = "NAME";

            this.cbo워크플로우단계.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_MA00004");
            this.cbo워크플로우단계.ValueMember = "CODE";
            this.cbo워크플로우단계.DisplayMember = "NAME";
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("NM_COMPANY", "회사", 90);
			this._flex.SetCol("NO_KEY", "파일번호", 90);
            this._flex.SetCol("TP_STEP", "단계코드", 90);
			this._flex.SetCol("NM_STEP", "단계", 90);
			this._flex.SetCol("NM_SALES", "영업담당", 90, true);
			this._flex.SetCol("NM_TYPIST", "입력담당", 90, true);
			this._flex.SetCol("NM_PUR", "구매담당", 90, true);
			this._flex.SetCol("NM_LOG", "물류담당", 90, true);
			this._flex.SetCol("YN_DONE", "완료", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("DTS_INSERT", "추가일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("DTS_DONE", "완료일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE", "수정자", 90);
            this._flex.SetCol("DC_RMK", "비고", 90);
            this._flex.SetCol("NM_SUPPLIER", "매입처", 90);

			this._flex.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";
			this._flex.Cols["DTS_DONE"].Format = "####/##/## ##:##:##";
			
			this._flex.SetCodeHelpCol("NM_SALES", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "ID_SALES", "NM_SALES" }, new string[] { "CODE", "NAME" });
			this._flex.SetCodeHelpCol("NM_TYPIST", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "ID_TYPIST", "NM_TYPIST" }, new string[] { "CODE", "NAME" });
			this._flex.SetCodeHelpCol("NM_PUR", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "ID_PUR", "NM_PUR" }, new string[] { "CODE", "NAME" });
			this._flex.SetCodeHelpCol("NM_LOG", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "ID_LOG", "NM_LOG" }, new string[] { "CODE", "NAME" });

            this._flex.EnabledHeaderCheck = false;

			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
		}

		private void InitEvent()
		{
			this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);

			this.btn담당자변경.Click += new EventHandler(this.btn담당자변경_Click);

			this.btn조회.Click += new EventHandler(this.btn조회_Click);
			this.btn저장.Click += new EventHandler(this.btn저장_Click);
			this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			FlexGrid grid;

			try
			{
				grid = ((FlexGrid)sender);

				e.Parameter.P61_CODE1 = @"SU.ID_USER AS CODE,
										  MU.NM_USER AS NAME";
                e.Parameter.P62_CODE2 = @"CZ_SA_USER SU WITH(NOLOCK)
										  LEFT JOIN MA_USER MU WITH(NOLOCK) ON MU.CD_COMPANY = SU.CD_COMPANY AND MU.ID_USER = SU.ID_USER";

				switch (grid.Cols[e.Col].Name)
				{
					case "NM_SALES":
						e.Parameter.P00_CHILD_MODE = "영업담당";
						e.Parameter.P63_CODE3 = "WHERE SU.CD_COMPANY = '" + D.GetString(this._flex["CD_COMPANY"]) + "'" + Environment.NewLine + 
												"AND SU.TP_BIZ = 'SA'";
						break;
					case "NM_TYPIST":
						e.Parameter.P00_CHILD_MODE = "입력지원";
						e.Parameter.P63_CODE3 = "WHERE SU.CD_COMPANY = '" + D.GetString(this._flex["CD_COMPANY"]) + "'" + Environment.NewLine +
												"AND SU.TP_BIZ = 'TP'";
						break;
					case "NM_PUR":
						e.Parameter.P00_CHILD_MODE = "구매담당";
						e.Parameter.P63_CODE3 = "WHERE SU.CD_COMPANY = '" + D.GetString(this._flex["CD_COMPANY"]) + "'" + Environment.NewLine +
												"AND SU.TP_BIZ = 'PU'";
						break;
					case "NM_LOG":
						e.Parameter.P00_CHILD_MODE = "물류담당";
						e.Parameter.P63_CODE3 = "WHERE SU.CD_COMPANY = '" + D.GetString(this._flex["CD_COMPANY"]) + "'" + Environment.NewLine +
												"AND SU.TP_BIZ = 'LO'";
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

        private void btn담당자변경_Click(object sender, EventArgs e)
        {
            int index;
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex.HasNormalRow) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;

                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(this._flex.DataTable.Rows.Count) });

                        if (D.GetString(this.cbo담당자변경.SelectedValue) == "SA")
                        {
                            dr["ID_SALES"] = this.ctx담당자변경.CodeValue;
                            dr["NM_SALES"] = this.ctx담당자변경.CodeName;
                        }
                        else if (D.GetString(this.cbo담당자변경.SelectedValue) == "PU")
                        {
                            dr["ID_PUR"] = this.ctx담당자변경.CodeValue;
                            dr["NM_PUR"] = this.ctx담당자변경.CodeName;
                        }
                        else if (D.GetString(this.cbo담당자변경.SelectedValue) == "TP")
                        {
                            dr["ID_TYPIST"] = this.ctx담당자변경.CodeValue;
                            dr["NM_TYPIST"] = this.ctx담당자변경.CodeName;
                        }
                        else if (D.GetString(this.cbo담당자변경.SelectedValue) == "LO")
                        {
                            dr["ID_LOG"] = this.ctx담당자변경.CodeValue;
                            dr["NM_LOG"] = this.ctx담당자변경.CodeName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
					return;
				}
                else if (string.IsNullOrEmpty(this.txt파일번호.Text) && string.IsNullOrEmpty(D.GetString(this.cbo워크플로우단계.SelectedValue)))
				{
					Global.MainFrame.ShowMessage("파일번호 또는 워크플로우 단계는 필수 입력 항목 입니다.");
					return;
				}

				DataTable dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOW_CHANGE", new object[] { this.ctx회사.CodeValue, 
																								this.txt파일번호.Text,
                                                                                                D.GetString(this.cbo워크플로우단계.SelectedValue),
                                                                                                this.dtp완료일자.StartDateToString,
                                                                                                this.dtp완료일자.EndDateToString,
                                                                                                (this.chk완료제외.Checked == true ? "Y" : "N") });

				this._flex.Binding = dt;

				if (!this._flex.HasNormalRow)
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

				if (this._biz.Save(this._flex.GetChanges()))
				{
					this._flex.AcceptChanges();
					Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				}
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
				this.Close();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                foreach (DataRow dr in this._flex.DataTable.Select("S = 'Y'"))
                {
                    dr.Delete();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
	}
}
