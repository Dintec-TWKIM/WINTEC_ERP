using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Utils;
using Duzon.ERPU.MF;
using System.Collections.Generic;

namespace cz
{
    public partial class P_CZ_MA_PITEM_UNIT_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_PITEM_UNIT_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex단위.BeginSetting(1, 1, false);

            this._flex단위.SetCol("CD_SYSDEF", "단위코드", 80);
            this._flex단위.SetCol("NM_SYSDEF", "단위명", 80);
            this._flex단위.SetCol("CD_FLAG2", "호환단위", 100, true);

            this._flex단위.ExtendLastCol = true;

            this._flex단위.SettingVersion = "0.0.0.1";
            this._flex단위.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this._flex단위.AfterEdit += new RowColEventHandler(this._flex단위_AfterEdit);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex단위.Binding = DBHelper.GetDataTable(@"SELECT CD_SYSDEF,
                                                                        NM_SYSDEF,
                                                                        CD_FLAG2 
                                                                 FROM MA_CODEDTL WITH(NOLOCK)
                                                                 WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                                "AND CD_FIELD = 'MA_B000004'");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _flex단위_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "CD_FLAG2") return;

                DBHelper.ExecuteScalar(@"UPDATE MA_CODEDTL
                                         SET CD_FLAG2 = '" + this._flex단위["CD_FLAG2"].ToString() + "'," +
                                            "ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," +
                                            "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                        "AND CD_FIELD = 'MA_B000004'" +
                                        "AND CD_SYSDEF = '" + this._flex단위["CD_SYSDEF"].ToString() + "'");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
