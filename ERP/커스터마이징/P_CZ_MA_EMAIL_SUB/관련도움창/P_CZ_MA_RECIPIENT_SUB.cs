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
    public partial class P_CZ_MA_RECIPIENT_SUB : Duzon.Common.Forms.CommonDialog
    {
        private string 거래처코드;
        
        private FlexGrid 선택그리드
        {
            get
            {
                if (this.tabControlExt1.SelectedIndex == 0)
                    return this._flex사내담당자;
                else
                    return this._flex거래처;
            }
        }

        public DataRow[] Result
        {
            get
            {
                DataTable dt;

                if (this._flex사내담당자.DataTable == null)
                {
                    if (this._flex거래처.DataTable == null)
                        return null;
                    else
                        dt = this._flex거래처.DataTable;
                }
                else
                {
                    dt = this._flex사내담당자.DataTable;
                    if (this._flex거래처.DataTable != null)
                        dt.Merge(this._flex거래처.DataTable);
                }

                return dt.Select("S = 'Y'");
            }
        }

        public P_CZ_MA_RECIPIENT_SUB(string 거래처코드)
        {
            InitializeComponent();

            this.거래처코드 = 거래처코드;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            #region 사내담당자
            this._flex사내담당자.BeginSetting(1, 1, false);

            this._flex사내담당자.SetCol("S", "S", 35, true, CheckTypeEnum.Y_N);
            this._flex사내담당자.SetCol("NM_KOR", "담당자", 100, false, typeof(string));
            this._flex사내담당자.SetCol("NO_EMAIL", "메일주소", 200, false, typeof(string));

            this._flex사내담당자.ExtendLastCol = true;
            this._flex사내담당자.SettingVersion = "0.0.0.1";
            this._flex사내담당자.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 거래처
            this._flex거래처.BeginSetting(1, 1, false);

            this._flex거래처.SetCol("S", "S", 35, true, CheckTypeEnum.Y_N);
            this._flex거래처.SetCol("NM_KOR", "담당자", 100, false, typeof(string));
            this._flex거래처.SetCol("NO_EMAIL", "메일주소", 200, false, typeof(string));

            this._flex거래처.ExtendLastCol = true;
            this._flex거래처.SettingVersion = "0.0.0.1";
            this._flex거래처.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = DBHelper.GetDataTable("SP_CZ_MA_RECIPIENT_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    Global.MainFrame.LoginInfo.UserID,
                                                                                    this.tabControlExt1.SelectedIndex,
                                                                                    this.거래처코드,
                                                                                    this.txt검색.Text });

                this.선택그리드.Binding = dt;

                if (this.선택그리드.HasNormalRow == false)
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
                if (this.Result == null)
                    this.DialogResult = DialogResult.Cancel;
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
