using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;

namespace cz
{
	public partial class P_CZ_PU_REQR_REG_SUB : Duzon.Common.Forms.CommonDialog
	{
		#region ★ 멤버필드
        P_CZ_PU_REQR_REG_SUB_BIZ _biz = new P_CZ_PU_REQR_REG_SUB_BIZ();
		DataTable _dt = new DataTable();
		#endregion

		#region ★ 초기화
        public P_CZ_PU_REQR_REG_SUB(string 발주번호)
		{
            InitializeComponent();

            this.txt발주번호.Text = 발주번호;
		}

		protected override void InitLoad()
		{
			try
			{
				base.InitLoad();

				this.InitGrid();
                this.InitEvent();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void InitGrid()
		{
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IO", "입고번호", 100);
            this._flexH.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_KOR", "담당자", 100);
            this._flexH.SetCol("DC_RMK", "비고", 185);
            
            this._flexH.ExtendLastCol = true;
            this._flexH.EnabledHeaderCheck = false;
            this._flexH.SettingVersion = "1.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("STND_ITEM", "규격", 100);
            this._flexL.SetCol("UNIT_IM", "단위", 80);
            this._flexL.SetCol("QT_IO", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_EX", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_TOTAL", "총금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 120);
            this._flexL.SetCol("NO_PSO_MGMT", "발주번호", 100);
            this._flexL.SetCol("NM_KOR", "담당자", 80);
            this._flexL.SetCol("NM_TP_UM_TAX", "부가세여부", 140, false, typeof(string));
            this._flexL.SetCol("DC_RMK", "비고", 100);

            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

        }

		private void InitEvent()
		{
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);

            this._flexH.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
		}

		protected override void InitPaint()
		{
			try
			{
				base.InitPaint();

                this.dtp입고일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
                this.dtp입고일자.EndDateToString = Global.MainFrame.GetStringToday;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region ★ 화면내버튼 클릭
		private void btn조회_Click(object sender, System.EventArgs e)
		{
			try
			{
				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp입고일자.StartDateToString,
                                                                      this.dtp입고일자.EndDateToString,
                                                                      this.txt발주번호.Text });

				if (!this._flexH.HasNormalRow)
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn적용_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!this._flexL.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
				}
                else
                {
                    this._dt = this._flexL.DataTable.Clone();

                    foreach (DataRow row in dataRowArray)
                    {
                        this._dt.ImportRow(row);
                    }

                    this.DialogResult = DialogResult.OK;
                }
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region ★ 그리드 이벤트
		private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
		{
			try
			{
                if (!this._flexH.HasNormalRow)
                    this.btn적용.Enabled = false;
				else
                    this.btn적용.Enabled = true;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				DataRow[] dr = this._flexL.DataTable.Select("NO_IO = '" + this._flexH[e.Row, "NO_IO"].ToString() + "'", "", DataViewRowState.CurrentRows);

				if (this._flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
				{
                    for (int i = this._flexL.Rows.Fixed; i <= dr.Length; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
				}
				else
				{
                    for (int i = this._flexL.Rows.Fixed; i <= dr.Length; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				DataTable dt = null;

				string key = this._flexH["NO_IO"].ToString();
				string filter = "NO_IO = '" + key + "'";

                if (this._flexH.DetailQueryNeed)
				{
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                               key });
				}

                this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region ★ 리턴데이타
		public DataTable 입고테이블
		{
            get { return this._dt; }
		}
		#endregion
	}
}