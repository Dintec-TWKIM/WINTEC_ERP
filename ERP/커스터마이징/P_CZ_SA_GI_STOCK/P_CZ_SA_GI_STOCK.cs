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
using Dintec;
using Duzon.Windows.Print;

namespace cz
{
	public partial class P_CZ_SA_GI_STOCK : PageBase
	{
		P_CZ_SA_GI_STOCK_BIZ _biz;

		public P_CZ_SA_GI_STOCK()
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.MainGrids = new FlexGrid[] { this._flex };
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this._biz = new P_CZ_SA_GI_STOCK_BIZ();

			this.InitGrid();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("TP_GI_STOCK", "처리상태", 80);
            this._flex.SetCol("NM_GI_STOCK", "수정자", 100);
            this._flex.SetCol("DTS_GI_STOCK", "수정일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("NM_COMPANY", "회사", 100);
            this._flex.SetCol("NO_SO", "수주번호", 100);
            this._flex.SetCol("LN_PARTNER", "매출처", 150);
            this._flex.SetCol("NO_HULL", "호선번호", 60);
            this._flex.SetCol("NM_VESSEL", "호선명", 150);
            this._flex.SetCol("NO_DSP", "순번", 40);
            this._flex.SetCol("SEQ_SO", "수주항번", false);
            this._flex.SetCol("CD_ITEM", "재고코드", 80);
            this._flex.SetCol("NM_ITEM", "재고명", 120);
            this._flex.SetCol("STND_ITEM", "규격", 120);
            this._flex.SetCol("NM_SUBJECT", "주제", 120);
            this._flex.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flex.SetCol("CD_ZONE", "저장위치", 100);
            this._flex.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NO_GIR", "의뢰번호", 100);
			this._flex.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("DT_START", "작업시작일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("STA_GIR", "진행상태", 80);
			this._flex.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_GIR_STOCK", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_PACK_BEFORE", "기포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GIR_BEFORE", "기출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_WORK", "작업수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GIR_PACK", "포장의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GIR_STOCK_PACK", "재고포장의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_PACK", "포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_PACK_STOCK", "재고포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_MINUS", "출고후량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex.SetDummyColumn("S");
            this._flex.Cols["DTS_GI_STOCK"].Format = "####/##/##/##:##:##";
            this._flex.SetDataMap("STA_GIR", Global.MainFrame.GetComboDataCombine("N;CZ_SA00030"), "CODE", "NAME");

			this._flex.SettingVersion = "1.0.0.0";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			try
			{
				base.InitPaint();

				this.dtp의뢰일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
				this.dtp의뢰일자.EndDateToString = Global.MainFrame.GetStringToday;

                this.cbo진행상태.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_SA00030");
                this.cbo진행상태.ValueMember = "CODE";
                this.cbo진행상태.DisplayMember = "NAME";

                this.cbo업무구분.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_SA00021");
                this.cbo업무구분.ValueMember = "CODE";
                this.cbo업무구분.DisplayMember = "NAME";

                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                {
                    this.bpc회사.AddItem("K100", "(주)딘텍");
                    this.bpc회사.AddItem("K200", "(주)두베코");
                    
                }
                else
                {
                    this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CompanyName);
                }

                this.bpc회사.SelectedValue = Global.MainFrame.LoginInfo.CompanyCode;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				if (string.IsNullOrEmpty(this.dtp의뢰일자.StartDateToString) || string.IsNullOrEmpty(this.dtp의뢰일자.EndDateToString))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰일자.Text);
					this.dtp의뢰일자.Focus();
					return;
				}

				dt = this._biz.Search(new object[] { this.bpc회사.QueryWhereIn_Pipe,
													 this.dtp의뢰일자.StartDateToString,
													 this.dtp의뢰일자.EndDateToString,
													 (this.chk완료건제외.Checked == true ? "Y" : "N"),
													 (this.chk기포장건제외.Checked == true ? "Y" : "N"),
													 this.txt의뢰번호.Text,
													 this.txt수주번호.Text,
                                                     this.cbo진행상태.SelectedValue,
                                                     this.cbo업무구분.SelectedValue });

				this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                    this.셀병합();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable dt1;
			DataRow[] dataRowArray1;

			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (this._flex.HasNormalRow == false) return;

				dataRowArray1 = this._flex.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					dt1 = this._flex.DataTable.Clone();

					foreach (DataRow dr in dataRowArray1)
					{
						dt1.ImportRow(dr);
					}

					dt1.DefaultView.Sort = "CD_ITEM ASC";
					dt1 = dt1.DefaultView.ToTable();

					reportHelper = Util.SetReportHelper(Util.GetReportFileName("R_CZ_SA_GI_STOCK", this.LoginInfo.CompanyCode), "재고출고관리", this.LoginInfo.CompanyCode);
					reportHelper.SetDataTable(dt1, 1);
					Util.RPT_Print(reportHelper);
				}
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
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			try
			{
				if (!base.SaveData() || !base.Verify()) return false;
				if (this._flex.IsDataChanged == false) return false;

				if (!this._biz.Save(this._flex.GetChanges())) return false;

				this._flex.AcceptChanges();

				return true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return false;
		}

        private void 셀병합()
        {
            CellRange cellRange;
            string key, key1;

            try
            {
                if (this._flex.HasNormalRow == false) return;

                this._flex.Redraw = false;

                for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; row++)
                {
                    key = D.GetString(this._flex.GetData(row, "NO_SO"));
                    key1 = D.GetString(this._flex.GetData(row, "NO_SO")) + "_" + D.GetString(this._flex.GetData(row, "SEQ_SO"));

                    foreach (Column column in this._flex.Cols)
                    {
                        switch (column.Name)
                        {
                            case "S":
                            case "YN_GI_STOCK":
                            case "DTS_GI_STOCK":
                            case "NO_GIR":
                            case "DT_GIR":
                            case "DT_START":
                            case "STA_GIR":
                            case "QT_GIR":
                            case "QT_GIR_STOCK":
                            case "QT_PACK_BEFORE":
                            case "QT_GIR_BEFORE":
                            case "QT_WORK":
                            case "QT_INV":
                            case "QT_MINUS":
                            case "YN_PACK_ORDER":
                            case "YN_PACK_COMPLETE":
                                continue;
                        }

                        cellRange = this._flex.GetCellRange(row, column.Name, row, column.Name);
                        
                        switch(column.Name)
                        {
                            case "NM_COMPANY":
                            case "NO_SO":
                            case "LN_PARTNER":
                            case "NO_HULL":
                            case "NM_VESSEL":
                                cellRange.UserData = key + "_" + column.Name;
                                break;
                            default:
                                cellRange.UserData = key1 + "_" + column.Name;
                                break;
                        }
                    }
                }

                this._flex.DoMerge();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
            }
        }
	}
}
