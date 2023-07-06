using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
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
	public partial class P_CZ_MA_PITEM_LT_SUB : Duzon.Common.Forms.CommonDialog
	{
		private string _품목코드;

		public P_CZ_MA_PITEM_LT_SUB()
		{
			InitializeComponent();
		}

		public P_CZ_MA_PITEM_LT_SUB(string 품목코드)
		{
			InitializeComponent();

			this._품목코드 = 품목코드;

			this.Btn조회_Click(null, null);
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("NO_PO", "발주번호", 100);
			this._flex.SetCol("CD_ITEM", "품목코드", 100);
			this._flex.SetCol("DT_PO", "발주일자", 100);
			this._flex.SetCol("DT_IO", "입고일자", 100);
			this._flex.SetCol("QT_PO", "발주수량", 100);
			this._flex.SetCol("QT_IO", "입고수량", 100);
			this._flex.SetCol("LT", "납기", 100);

			this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn닫기.Click += Btn닫기_Click;
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex.Binding = DBHelper.GetDataTable("SP_CZ_MA_PITEM_LT_SUB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
																								     this._품목코드 });
			}
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
		}

		private void Btn닫기_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
