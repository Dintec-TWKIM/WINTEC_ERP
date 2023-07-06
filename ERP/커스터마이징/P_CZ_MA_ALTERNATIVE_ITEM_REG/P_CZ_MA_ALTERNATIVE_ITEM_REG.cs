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

namespace cz
{
	public partial class P_CZ_MA_ALTERNATIVE_ITEM_REG : PageBase
	{
		#region 초기화
		P_CZ_MA_ALTERNATIVE_ITEM_REG_BIZ _biz = new P_CZ_MA_ALTERNATIVE_ITEM_REG_BIZ();

		public P_CZ_MA_ALTERNATIVE_ITEM_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
            this.initEvent();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("NO_PRIME", "대표도면번호", 100, true);
			this._flexH.SetCol("NO_PLATE_PRIME", "대표부품번호", 100);
			this._flexH.SetCol("NM_PLATE_PRIME", "대표부품명", 100);
            this._flexH.SetCol("UCODE_PRIME", "대표U코드", 100);
            this._flexH.SetCol("CD_ITEM_PRIME", "대표재고코드", 100);
            this._flexH.SetCol("CNT_ITEM", "등록자재수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("NM_INSERT", "등록자", 80);
            this._flexH.SetCol("DTS_INSERT", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexH.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flexH.SetDummyColumn(new string[] { "S", "NO_PRIME" });
			this._flexH.VerifyPrimaryKey = new string[] { "NO_PRIME" };

			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexL.SetCol("NO_ALTERNATIVE", "대치도면번호", 100, true);
			this._flexL.SetCol("NO_PLATE_ALTERNATIVE", "대치부품번호", 100);
			this._flexL.SetCol("NM_PLATE_ALTERNATIVE", "대치부품명", 100);
            this._flexL.SetCol("UCODE_ALTERNATIVE", "대치U코드", 100);
            this._flexL.SetCol("CD_ITEM_ALTERNATIVE", "대치재고코드", 100);
            this._flexL.SetCol("NM_INSERT", "등록자", 80);
            this._flexL.SetCol("DTS_INSERT", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexL.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flexL.SetDummyColumn(new string[] { "S", "NO_PRIME", "NO_ALTERNATIVE" });
			this._flexL.VerifyPrimaryKey = new string[] { "NO_PRIME", "NO_ALTERNATIVE" };

			this._flexL.SettingVersion = "0.0.0.1";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

            #region Detail
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("NO_PRIME", "대표도면번호", 100);
            this._flexD.SetCol("NO_PLATE_PRIME", "대표부품번호", 100);
            this._flexD.SetCol("NM_PLATE_PRIME", "대표부품명", 100);
            this._flexD.SetCol("CD_ITEM_PRIME", "대표재고코드", 100);
            this._flexD.SetCol("UCODE_PRIME", "대표U코드", 100);
            
            this._flexD.SetCol("NO_MATERIAL", "대상도면번호", 100);
            this._flexD.SetCol("NO_PLATE_MATERIAL", "대상부품번호", 100);
            this._flexD.SetCol("NM_PLATE_MATERIAL", "대상부품명", 100);
            this._flexD.SetCol("CD_ITEM_MATERIAL", "대상재고코드", 100);
            this._flexD.SetCol("UCODE_MATERIAL", "대상U코드", 100);

            this._flexD.SetCol("NO_ALTERNATIVE", "대치도면번호", 100);
            this._flexD.SetCol("NO_PLATE_ALTERNATIVE", "대치부품번호", 100);
            this._flexD.SetCol("NM_PLATE_ALTERNATIVE", "대치부품명", 100);
            this._flexD.SetCol("CD_ITEM_ALTERNATIVE", "대치재고코드", 100);
            this._flexD.SetCol("UCODE_ALTERNATIVE", "대치U코드", 100);

            this._flexD.SetCol("NM_INSERT", "등록자", 80);
            this._flexD.SetCol("DTS_INSERT", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexD.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flexD.SettingVersion = "0.0.0.1";
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
		}

		private void initEvent()
		{
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.ValidateEdit += this._flexH_ValidateEdit;
            this._flexL.ValidateEdit += _flexL_ValidateEdit;


			this.btn대치자재추가.Click += new EventHandler(this.btn대치자재추가_Click);
			this.btn대치자재삭제.Click += new EventHandler(this.btn대치자재삭제_Click);
		}

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
				
				if (this._flexH.Cols[e.Col].Name == "NO_PRIME" && 
					!string.IsNullOrEmpty(oldValue))
				{
					this.ShowMessage("대표도면번호는 수정이 불가 합니다.");
					this._flexH["NO_PRIME"] = oldValue;
					e.Cancel = true;
					return;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));

				if (this._flexL.Cols[e.Col].Name == "NO_ALTERNATIVE" &&
					!string.IsNullOrEmpty(oldValue))
				{
					this.ShowMessage("대치도면번호는 수정이 불가 합니다.");
					this._flexL["NO_ALTERNATIVE"] = oldValue;
					e.Cancel = true;
					return;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region 메인버튼 이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            DataTable dt;
			object[] objectArray;


			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				objectArray = new object[] { this.txt대표도면번호.Text,
										     this.txt대치도면번호.Text,
											 this.txt대표부품번호.Text,
											 this.txt대치부품번호.Text,
											 this.txt대표U코드.Text,
											 this.txt대치U코드.Text,
											 this.txt대표재고코드.Text,
											 this.txt대치재고코드.Text };

				if (this.tabControl.SelectedIndex == 0)
					dt = this._biz.Search1(objectArray);
				else
					dt = this._biz.Search2(objectArray);

                if (this.tabControl.SelectedIndex == 0)
                {
                    this._flexH.Binding = dt;

                    if (!this._flexH.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this._flexD.Binding = dt;

                    if (!this._flexD.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!BeforeAdd()) return;

                this._flexH.Rows.Add();
                this._flexH.Row = _flexH.Rows.Count - 1;

                this._flexH.AddFinished();
                this._flexH.Col = _flexH.Cols["NO_PRIME"].Index;
                this._flexH.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
						dr.Delete();
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
					ShowMessage(PageResultMode.SaveGood);
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

                if (!this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges())) return false;

                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();

                return true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return false;
		}
		#endregion

		#region 그리드 이벤트
		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string key, filter;

			try
			{
				key = D.GetString(this._flexH["NO_PRIME"]);
                filter = "NO_PRIME = '" + key + "'";

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { key,
                                                               this.txt대치도면번호.Text,
                                                               this.txt대치부품번호.Text,
                                                               this.txt대치U코드.Text,
                                                               this.txt대치재고코드.Text });
				}

				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void btn대치자재추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;
				if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_PRIME"])))
                {
                    this.ShowMessage("대표도면번호가 입력되지 않았습니다.");
                    return;
                }
				
				this._flexH_AfterRowChange(null, null);

				this._flexL.Rows.Add();
				this._flexL.Row = this._flexL.Rows.Count - 1;

                this._flexL["NO_PRIME"] = D.GetString(this._flexH["NO_PRIME"]);

				this._flexL.AddFinished();
				this._flexL.Col = this._flexL.Cols.Fixed;
				this._flexL.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn대치자재삭제_Click(object sender, EventArgs e)
		{
            DataRow[] dataRowArray;

			try
			{
				if (!this._flexL.HasNormalRow) return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                        dr.Delete();

                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion	
	}
}
