using System;
using System.Collections;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;

namespace cz
{
    public partial class P_CZ_MA_LOCATION_REG : PageBase
    {
        P_CZ_MA_LOCATION_REG_BIZ _biz = new P_CZ_MA_LOCATION_REG_BIZ();

        public P_CZ_MA_LOCATION_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();
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
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("CD_SL", "S/L코드", 140);
            this._flexH.SetCol("NM_SL", "S/L명", 200);

            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("CD_LOCATION", "LOCATION 코드", 120, true);
            this._flexL.SetCol("NM_LOCATION", "LOCATION 명", 120, true);
            this._flexL.SetCol("NO_SEQ", "순번", 120, true);
            this._flexL.SetCol("YN_USE", "사용여부", 120, true);
            this._flexL.SetCol("DC_RMK", "비고", 120, true);

            this._flexL.VerifyAutoDelete = new string[] { "CD_LOCATION", "NM_LOCATION" };
            this._flexL.VerifyNotNull = new string[] { "CD_LOCATION", "NM_LOCATION" };

            this._flexL.SetDataMap("YN_USE", Global.MainFrame.GetComboDataCombine("N;YESNO"), "CODE", "NAME");

            this._flexL.SetDummyColumn("S");

            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);

            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);

            this.btn라벨인쇄.Click += new EventHandler(btn라벨인쇄_Click);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.splitContainer1.SplitterDistance = 394;

                DataSet comboData = Global.MainFrame.GetComboData(new string[] { "NC;MA_PLANT" });

                this.cbo공장.DataSource = comboData.Tables[0];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string 공장코드, 사업장;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.FieldCheck()) return;

                DataRowView dataRowView = ((DataRowView)this.cbo공장.SelectedItem);

                if (dataRowView == null)
                {
                    공장코드 = null;
                    사업장 = null;
                }
                else
                {
                    공장코드 = D.GetString(dataRowView["CODE"]);
                    사업장 = D.GetString(dataRowView["CD_BIZAREA"]);
                }

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      공장코드,
                                                                      사업장 });

                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt;
            DataRow[] dataRowArray;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexL.HasNormalRow) return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
				else
				{
					dt = this._flexL.DataTable.Clone();

					foreach (DataRow dr in dataRowArray)
					{
						dt.ImportRow(dr);
					}

					reportHelper = new ReportHelper("R_CZ_MA_LOCATION", "창고별 LOCATION 등록(딘텍)");
					reportHelper.SetDataTable(dt, 1);
					reportHelper.Print();
				}

			}
			catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn라벨인쇄_Click(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt;
            DataRow[] dataRowArray;
            DataRow newRow;
            int index;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexL.HasNormalRow) return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("NM_LOCATION");
                    dt.Columns.Add("CD_LOCATION");
                    dt.Columns.Add("NM_LOCATION1");
                    dt.Columns.Add("CD_LOCATION1");

                    newRow = dt.NewRow();
                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        if (index != 0 && index % 2 == 0)
                            newRow = dt.NewRow();

                        if (index % 2 == 0)
                        {
                            newRow["NM_LOCATION"] = dr["NM_LOCATION"].ToString();
                            newRow["CD_LOCATION"] = dr["CD_LOCATION"].ToString();
                        }
                        else
                        {
                            newRow["NM_LOCATION1"] = dr["NM_LOCATION"].ToString();
                            newRow["CD_LOCATION1"] = dr["CD_LOCATION"].ToString();
                        }

                        if (index % 2 != 0 || index == dataRowArray.Length - 1)
                            dt.Rows.Add(newRow);

                        index++;
                    }

                    reportHelper = new ReportHelper("R_CZ_MA_LOCATION_1", "창고별 LOCATION 등록(딘텍)");
                    reportHelper.SetDataTable(dt, 1);
                    reportHelper.Print();

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

                this._flexL.Rows.Add();
                this._flexL.Row = this._flexL.Rows.Count - 1;
                
                this._flexL["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flexL["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                this._flexL["CD_SL"] = D.GetString(this._flexH["CD_SL"]);
                
                this._flexL.AddFinished();

                this._flexL.Col = this._flexL.Cols.Fixed;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this._flexL.HasNormalRow)
                    return;

                this._flexL.Rows.Remove(this._flexL.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

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
            if (!base.SaveData() || !this.Verify())
                return false;

            this._biz.Save(this._flexL.GetChanges());

            return true;
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            string key, filter, 공장코드, 사업장;
            DataTable dt;

            try
            {
                key = D.GetString(this._flexH["CD_SL"]);
                filter = "CD_SL ='" + key + "'";
                dt = null;

                if (this._flexH.DetailQueryNeed)
                {
                    DataRowView dataRowView = ((DataRowView)this.cbo공장.SelectedItem);

                    if (dataRowView == null)
                    {
                        공장코드 = null;
                        사업장 = null;
                    }
                    else
                    {
                        공장코드 = D.GetString(dataRowView["CODE"]); ;
                        사업장 = D.GetString(dataRowView["CD_BIZAREA"]);
                    }

                    dt = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             공장코드,
                                                             사업장,
                                                             key,
                                                             this.txt로케이션.Text });
                }

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string editData = this._flexL.EditData;

            switch (this._flexL.Cols[e.Col].Name)
            {
                case "CD_LOCATION":
                    if (this._flexL.DataTable.Select("CD_LOCATION ='" + editData + "'").Length > 0)
                    {
                        this.ShowMessage(공통메세지._의값이중복되었습니다, new string[] { "LOCATION" });
                        e.Cancel = true;
                    }
                    break;
                case "NO_SEQ":
                    if (string.IsNullOrEmpty(editData))
                        break;

                    if (this._flexL.DataTable.Select("NO_SEQ ='" + D.GetDecimal(editData) + "'").Length > 0)
                    {
                        this.ShowMessage(공통메세지._의값이중복되었습니다, new string[] { this.DD("순번") });
                        e.Cancel = true;
                    }
                    break;
                case null:
                    break;
                default:
                    break;
            }
        }

        private void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexL.Cols[e.Col].Name == "CD_LOCATION")
                {
                    if (D.GetString(this._flexL["CD_LOCATION"]) != string.Empty && this._flexL.RowState() != DataRowState.Added)
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool FieldCheck()
        {
            return ComFunc.NullCheck(new Hashtable() { { this.cbo공장, this.lbl공장 } });
        }

 
    }
}