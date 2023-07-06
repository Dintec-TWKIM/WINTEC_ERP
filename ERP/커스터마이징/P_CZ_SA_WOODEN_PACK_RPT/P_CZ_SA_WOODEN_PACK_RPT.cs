using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_WOODEN_PACK_RPT : PageBase
    {
        P_CZ_SA_WOODEN_PACK_RPT_BIZ _biz = new P_CZ_SA_WOODEN_PACK_RPT_BIZ();

        public P_CZ_SA_WOODEN_PACK_RPT()
        {
            if (Global.MainFrame.LoginInfo.UserID != "A-019")
                StartUp.Certify(this);
            
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

            this.dtp포장일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp포장일자.EndDateToString = MainFrameInterface.GetStringToday;
        }

        private void InitEvent()
        {
            this._flex.DoubleClick += _flex_DoubleClick;

            this.btn회계전표처리.Click += Btn회계전표처리_Click;
            this.btn회계전표취소.Click += Btn회계전표취소_Click;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NM_COMPANY", "회사명", 100);
            this._flex.SetCol("NO_GIR", "협조전번호", 100);
            this._flex.SetCol("NO_FILE", "수주번호", 100);
            this._flex.SetCol("NO_PACK", "포장번호", 100);
            this._flex.SetCol("NM_CC", "코스트센터", 100);
            this._flex.SetCol("DT_PACK", "포장일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("QT_WIDTH", "가로", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_LENGTH", "세로", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_HEIGHT", "높이", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_CBM", "CBM", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("AM", "포장금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_NET_WEIGHT", "무게", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_GROSS_WEIGHT", "총무게", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("DC_FILE", "포장사진", 100);
            this._flex.SetCol("NO_WOODEN", "목공포장번호", false);
            this._flex.SetCol("NO_DOCU", "전표번호", 100);
            this._flex.SetCol("AM_DOCU", "전표금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("AM_BAN", "반제금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetDummyColumn(new string[] { "S" });
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                     this.dtp포장일자.StartDateToString,
                                                                     this.dtp포장일자.EndDateToString,
                                                                     this.txt협조전번호.Text,
                                                                     this.txt수주번호.Text });

                if (!this._flex.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                        return;

                    this._flex.Redraw = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();    
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
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
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
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

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.Cols[this._flex.Col].Name == "DC_FILE" && !string.IsNullOrEmpty(this._flex["DC_FILE"].ToString()))
                {
                    WebClient wc = new WebClient();

                    string 로컬경로 = Application.StartupPath + "/temp/";
                    string 서버경로 = Global.MainFrame.HostURL + "/Upload/CZ_SA_PACKH_FILE/" + this._flex["CD_COMPANY"].ToString() + "/" + this._flex["NO_GIR"].ToString() + "/";

                    Directory.CreateDirectory(로컬경로);
                    wc.DownloadFile(서버경로 + this._flex["NM_FILE"].ToString(), 로컬경로 + this._flex["NM_FILE"].ToString());
                    Process.Start(로컬경로 + this._flex["NM_FILE"].ToString());
                }
                else if (this._flex.ColSel == this._flex.Cols["NO_DOCU"].Index)
                {
                    if (string.IsNullOrEmpty(this._flex["NO_DOCU"].ToString()))
                        return;

                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flex["NO_DOCU"]),
                                                                                                                                     "1",
                                                                                                                                     D.GetString(this._flex["CD_PC"]),
                                                                                                                                     Global.MainFrame.LoginInfo.CompanyCode });
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn회계전표처리_Click(object sender, EventArgs e)
        {
            string query, 목공포장번호;

            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                query = @"UPDATE CZ_SA_PACKH
                          SET NO_WOODEN = '{3}'
                          WHERE CD_COMPANY = '{0}'
                          AND NO_GIR = '{1}'
                          AND NO_PACK = '{2}'";

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND CD_COMPANY <> '" + Global.MainFrame.LoginInfo.CompanyCode + "'").Length > 0)
                {
                    this.ShowMessage("회사코드가 다른 건이 선택되어 있습니다.");
                }
                else
                {
                    this.btn회계전표처리.Enabled = false;

                    목공포장번호 = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "12", Global.MainFrame.GetStringToday.Substring(2, 2));

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(),
                                                                    dr["NO_GIR"].ToString(),
                                                                    dr["NO_PACK"].ToString(),
                                                                    목공포장번호));
                    }

                    this._biz.회계전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          목공포장번호,
                                                          "D07",
                                                          Global.MainFrame.LoginInfo.UserID });

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표처리.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표처리.Enabled = true;
            }
        }

        private void Btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                this.btn회계전표취소.Enabled = false;

                this._biz.회계전표취소("D07", this._flex["NO_WOODEN"].ToString());

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표취소.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표취소.Enabled = true;
            }
        }
    }
}
