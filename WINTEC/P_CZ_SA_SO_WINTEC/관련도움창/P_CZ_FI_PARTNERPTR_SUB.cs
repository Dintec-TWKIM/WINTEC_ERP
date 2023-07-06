using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Windows.Forms.BaseForm;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_FI_PARTNERPTR_SUB : Duzon.Common.Forms.CommonDialog
	{
		private string 거래처코드;
		private object[] _ResultObject;

		public P_CZ_FI_PARTNERPTR_SUB()
		{
			this.InitializeComponent();
		}

		public P_CZ_FI_PARTNERPTR_SUB(string _거래처코드)
		{
			this.InitializeComponent();

			this.거래처코드 = _거래처코드;
		}

		protected override void InitLoad()
		{
			base.InitLoad();
			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, true);

			this._flex.SetCol("USE_YN", "주요사용", 70, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("SEQ", "순번", 60, false);
			this._flex.SetCol("NM_PTR", "거래처담당", 150);
			this._flex.SetCol("NM_EMAIL", "E-Mail", 200);
			this._flex.SetCol("NO_HP", "HP", 120);
			this._flex.SetCol("NO_TEL", "전화번호", 120);
			this._flex.SetCol("NO_FAX", "FAX", 120);
			this._flex.SetCol("CLIENT_NOTE", "발행비고", 250);

			this._flex.StartEdit += new RowColEventHandler(this.Flex_StartEdit);
			this._flex.SettingVersion = "1.0.0.2";
			this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btnAdd.Click += new EventHandler(this.BtnAdd_Click);
			this.btnDel.Click += new EventHandler(this.BtnDel_Click);
			this.btnSave.Click += new EventHandler(this.BtnSave_Click);
			this.btnEnd.Click += new EventHandler(this.BtnEnd_Click);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "PTR_ADD", this.btnAdd, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "PTR_DEL", this.btnDel, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "PTR_SAVE", this.btnSave, true);

			this._flex.Binding = this.Search();
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.추가전확인()) return;

				this._flex.Row = this._flex.Rows.Count - 1;
				this._flex.Rows.Add();

				this._flex[this._flex.Row, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex[this._flex.Row, "CD_PARTNER"] = this.거래처코드;
				this._flex[this._flex.Row, "SEQ"] = D.GetDecimal(this._flex.Rows.Count - 1);
				this._flex[this._flex.Row, "USE_YN"] = "N";

				this._flex.AddFinished();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnDel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex.Row < 0) return;

				this._flex.RemoveItem(this._flex.Row);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSave() || !this.Save()) return;

				this._flex.AcceptChanges();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnEnd_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex.GetChanges() != null)
				{
					Global.MainFrame.ShowMessage("저장 후 적용하시기 바랍니다.");
				}
				else
				{
					DataRow[] dataRowArray = this._flex.DataTable.Select("USE_YN = 'Y'");

					if (dataRowArray.Length > 0)
						this._ResultObject = new object[] { dataRowArray[0]["NM_PTR"],
															dataRowArray[0]["NM_EMAIL"],
															dataRowArray[0]["NO_HP"],
															dataRowArray[0]["NO_TEL"],
															dataRowArray[0]["NO_FAX"] };
					else
						this._ResultObject = null;

					this.DialogResult = DialogResult.OK;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Flex_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (this._flex.Cols[e.Col].Name)
				{
					case "USE_YN":
						for (int index = this._flex.Rows.Fixed; index < this._flex.Rows.Count; ++index)
						{
							if (this._flex.GetCellCheck(index, this._flex.Cols["USE_YN"].Index) == CheckEnum.Checked)
								this._flex.SetCellCheck(index, this._flex.Cols["USE_YN"].Index, CheckEnum.Unchecked);
						}
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private DataTable Search()
		{
			return DBHelper.GetDataTable(@"SELECT CD_COMPANY, 
												  CD_PARTNER, 
												  SEQ, 
												  NM_PTR, 
												  NM_EMAIL, 
												  NO_HP, 
												  NO_TEL, 
												  CLIENT_NOTE, 
												  USE_YN, 
												  NO_FAX 
										   FROM FI_PARTNERPTR WITH(NOLOCK) 
										   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
										  "AND CD_PARTNER = '" + this.거래처코드 + "'");
		}

		private bool Save()
		{
			SpInfo spInfo = new SpInfo();
			spInfo.DataValue = this._flex.GetChanges();
			spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
			spInfo.SpNameInsert = "UP_FI_PARTNERPTR_INSERT";
			spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "SEQ",
												   "NM_PTR",
												   "NM_EMAIL",
												   "NO_HP",
												   "NO_TEL",
												   "CLIENT_NOTE",
												   "USE_YN",
												   "ID_INSERT",
												   "NO_FAX"};
			spInfo.SpNameUpdate = "UP_FI_PARTNERPTR_UPDATE";
			spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "SEQ",
												   "NM_PTR",
												   "NM_EMAIL",
												   "NO_HP",
												   "NO_TEL",
												   "CLIENT_NOTE",
												   "USE_YN",
												   "ID_UPDATE",
												   "NO_FAX" };
			spInfo.SpNameDelete = "UP_FI_PARTNERPTR_DELETE";
			spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "SEQ" };

			DBHelper.Save(spInfo);
			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다, new string[0]);

			return true;
		}

		private bool BeforeSave()
		{
			DataTable changes = this._flex.GetChanges();

			if (changes == null)
			{
				Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다, new string[0]);
				return false;
			}

			if (changes.Select("USE_YN = 'Y'").Length > 1)
			{
				Global.MainFrame.ShowMessage("주요 사용은 하나만 선택가능합니다.");
				return false;
			}

			foreach (DataRow row in changes.Rows)
			{
				if (row.RowState != DataRowState.Deleted && string.IsNullOrEmpty(D.GetString(row["NM_EMAIL"])))
				{
					Global.MainFrame.ShowMessage("E-Mail 은(는) 필수입력항목입니다.");
					return false;
				}
			}

			return true;
		}

		private bool 추가전확인()
		{
			if (this._flex.Rows.Count > 1)
			{
				for (int index = 0; index < this._flex.Rows.Count; ++index)
				{
					if (string.IsNullOrEmpty(D.GetString(this._flex.Rows[index]["NM_EMAIL"])))
					{
						Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "E-Mail" });
						return false;
					}
				}
			}

			return true;
		}

		public object[] ReturnValues
		{
			get
			{
				return this._ResultObject;
			}
		}
	}
}