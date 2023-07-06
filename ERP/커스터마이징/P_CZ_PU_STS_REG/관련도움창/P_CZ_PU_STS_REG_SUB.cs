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

namespace cz
{
    public partial class P_CZ_PU_STS_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PU_STS_REG_SUB_BIZ _biz = new P_CZ_PU_STS_REG_SUB_BIZ();
        private string _수불구분;
        private DataTable _lineData;
        
        public DataTable LineData
        {
            get
            {
                return this._lineData;
            }
        }

        public P_CZ_PU_STS_REG_SUB(string 수불구분)
        {
            InitializeComponent();

            this._수불구분 = 수불구분;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            if (this._수불구분 == "001")
            {
                base.TitleText = "입고적용";
                this.lbl수주번호.Text = "발주번호";
            }
            else if (this._수불구분 == "041")
            {
                base.TitleText = "출고반품적용";
                this.lbl수주번호.Text = "수주번호";
            }

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp수불일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd"); ;
            this.dtp수불일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitGrid()
        {
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);

            if (this._수불구분 == "001")
            {
                this._flexH.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("NO_IO", "입고번호", 100);
                this._flexH.SetCol("LN_PARTNER", "매입처", 120);
                this._flexH.SetCol("NM_KOR", "담당자", 120);
            }
            else if (this._수불구분 == "041")
            {
                this._flexH.SetCol("DT_IO", "반품일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("NO_IO", "반품번호", 100);
                this._flexH.SetCol("LN_PARTNER", "매출처", 120);
                this._flexH.SetCol("NM_KOR", "담당자", 120);
            }

            this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.SetDummyColumn(new string[] { "S" });
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);

            if (this._수불구분 == "001")
            {
                this._flexL.SetCol("NO_IOLINE", "순번", 60);
                this._flexL.SetCol("CD_ITEM", "품목코드", 80);
                this._flexL.SetCol("NM_ITEM", "품목명", 100);
                this._flexL.SetCol("UNIT_IM", "단위", 100);
                this._flexL.SetCol("NM_SL", "입고창고", 100);
                this._flexL.SetCol("QT_IO", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_TARGET", "기처리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_GOOD_INV", "적용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("CD_PJT", "프로젝트번호", 100);
                this._flexL.SetCol("NM_PJT", "프로젝트명", 100);
                this._flexL.SetCol("NO_PSO_MGMT", "발주번호", 100);
                this._flexL.SetCol("NO_PSOLINE_MGMT", "발주항번", 100);
            }
            else if (this._수불구분 == "041")
            {
                this._flexL.SetCol("NO_IOLINE", "순번", 60);
                this._flexL.SetCol("CD_ITEM", "품목코드", 80);
                this._flexL.SetCol("NM_ITEM", "품목명", 100);
                this._flexL.SetCol("UNIT_IM", "단위", 100);
                this._flexL.SetCol("NM_SL", "반품창고", 100);
                this._flexL.SetCol("QT_IO", "반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_TARGET", "기처리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_GOOD_INV", "적용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("CD_PJT", "프로젝트번호", 100);
                this._flexL.SetCol("NM_PJT", "프로젝트명", 100);
                this._flexL.SetCol("NO_PSO_MGMT", "수주번호", 100);
                this._flexL.SetCol("NO_PSOLINE_MGMT", "수주항번", 100);
            }
            
            this._flexL.SetDummyColumn(new string[] { "S" });
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = this._flexH["NO_IO"].ToString();
                filter = "NO_IO = '" + key + "'";

                if (this._flexH.DetailQueryNeed == true)
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

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                if (flex.Name == this._flexH.Name)
                {
                    //상단 그리드 Header Click 이벤트

                    if (!this._flexH.HasNormalRow) return;

                    //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                    this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                    if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                    for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                    {
                        this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                        for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        {
                            if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                            this._flexL[i, "S"] = D.GetString(this._flexH["S"]);
                        }
                    }
                }
                else if(flex.Name == this._flexL.Name)
                {
                    if (!this._flexL.HasNormalRow) return;

                    this._flexH["S"] = D.GetString(this._flexL["S"]);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            string name;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;

                if (name == "S")
                {
                    grid["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                    if (grid.Name == this._flexL.Name)
                    {
                        DataRow[] drArr = grid.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                        if (drArr.Length != 0)
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Checked);
                        else
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (this._flexL.Cols[e.Col].Name == "S" && D.GetString(this._flexH["S"]) == "Y")
                {
                    if (D.GetString(this._flexL["S"]) == "Y") //edit 시작점이므로 n으로 변경하려면 기존값은 y
                        this._flexH["S"] = "N";
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
                DataTable dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._수불구분,
                                                               this.dtp수불일자.StartDateToString,
                                                               this.dtp수불일자.EndDateToString,
                                                               this.ctx거래처.CodeValue,
                                                               this.txt프로젝트.Text,
                                                               this.txt수불번호.Text,
                                                               this.txt수주번호.Text });

                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                this._lineData = new DataView(this._flexL.DataTable, "S = 'Y'", string.Empty, DataViewRowState.CurrentRows).ToTable();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn종료_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
