using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Scraping;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_AVLFNC_PARTNER_CLOSE : Duzon.Common.Forms.CommonDialog
    {
        private DataTable _dt = (DataTable)null;

        public P_FI_Z_AVLFNC_PARTNER_CLOSE()
        {
            this.InitializeComponent();
        }

        public P_FI_Z_AVLFNC_PARTNER_CLOSE(DataTable dt)
          : this()
		{
            this._dt = dt;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = Application.StartupPath + "\\DSMUpdateAgent.exe";
                process.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\"", (object)"10301", (object)"", (object)"20", (object)"");
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CD_PARTNER", "거래처코드", 100);
            this._flex.SetCol("LN_PARTNER", "거래처명", 160);
            this._flex.SetCol("NO_COMPANY", "사업자등록번호", 100);
            this._flex.SetCol("CD_CON", "휴폐업구분", 70);
            this._flex.SetCol("STATE_GUBN", "상태구분", 70, false);
            this._flex.SetCol("STATE_CODE", "상태코드", 70, false);
            this._flex.SetCol("TAX_TYPE", "과세유형", 70, false);
            this._flex.SetCol("TAX_CODE", "과세유형코드", 70, false);
            this._flex.SetCol("SERACH_RESULT", "조회결과", 100, false);
            this._flex.SetCol("DT_CLOSE", "폐업일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.Cols["DT_CLOSE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["SERACH_RESULT"].Visible = this._flex.Cols["STATE_CODE"].Visible = this._flex.Cols["TAX_CODE"].Visible = false;
            this._flex.SettingVersion = "1.0.4";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flex.HelpClick += new EventHandler(this._flex_HelpClick);
        }

        private void InitEvent() => this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

        protected override void InitPaint()
        {
            base.InitPaint();
            this._flex.SetDataMap("CD_CON", MA.GetCode("MA_B000073", true), "CODE", "NAME");
            if (this._dt.Rows.Count > 0)
            {
                this._flex.Binding = (object)this.휴폐업가져오기(this._dt);
            }
            else
            {
                int num = (int)Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
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

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string detail = D.GetString(this._flex["ERRORMSG"]);
                if (detail != string.Empty)
                {
                    int num = (int)Global.MainFrame.ShowDetailMessage("오류행 정보입니다.\n세부내역은 [더보기] 버튼을 눌러서 확인 하세요", detail);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private DataTable 휴폐업가져오기(DataTable dt)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CD_PARTNER", typeof(string));
            dataTable1.Columns.Add("LN_PARTNER", typeof(string));
            dataTable1.Columns.Add("NO_COMPANY", typeof(string));
            dataTable1.Columns.Add("SERACH_RESULT", typeof(string));
            dataTable1.Columns.Add("STATE_GUBN", typeof(string));
            dataTable1.Columns.Add("STATE_CODE", typeof(string));
            dataTable1.Columns.Add("TAX_TYPE", typeof(string));
            dataTable1.Columns.Add("TAX_CODE", typeof(string));
            dataTable1.Columns.Add("DT_CLOSE", typeof(string));
            dataTable1.Columns.Add("DT_SERACH", typeof(string));
            dataTable1.Columns.Add("CD_CON", typeof(string));
            foreach (DataRow row1 in (InternalDataCollectionBase)dt.Rows)
            {
                DataTable dataTable2 = this.Scraping(row1["S_IDNO"].ToString());
                if (dataTable2 == null)
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다.ToString() + "(null)");
                    return (DataTable)null;
                }
                if (dataTable2.TableName == "ERROR")
                {
                    string code = "거래처코드 : @ 오류 \n@";
                    int num = (int)Global.MainFrame.ShowMessage(code, new object[2]
                    {
            row1["CD_PARTNER"],
            dataTable2.Rows[0][0]
                    });
                }
                else if (dataTable2.TableName == "휴폐업조회 테이블")
                {
                    DataRow row2 = dataTable1.NewRow();
                    row2["CD_PARTNER"] = row1["CD_PARTNER"];
                    row2["LN_PARTNER"] = row1["LN_PARTNER"];
                    row2["NO_COMPANY"] = (object)D.GetString(dataTable2.Rows[0]["사업자번호"]).Replace("-", "");
                    row2["SERACH_RESULT"] = dataTable2.Rows[0][1];
                    row2["STATE_GUBN"] = dataTable2.Rows[0][2];
                    row2["STATE_CODE"] = dataTable2.Rows[0][3];
                    row2["TAX_TYPE"] = dataTable2.Rows[0][4];
                    row2["TAX_CODE"] = dataTable2.Rows[0][5];
                    row2["DT_CLOSE"] = dataTable2.Rows[0][6];
                    row2["DT_SERACH"] = dataTable2.Rows[0][7];
                    string empty = string.Empty;
                    string str;
                    switch (dataTable2.Rows[0][3].ToString())
                    {
                        case "20":
                            str = "002";
                            break;
                        case "30":
                            str = "003";
                            break;
                        default:
                            str = "001";
                            break;
                    }
                    row2["CD_CON"] = (object)str;
                    dataTable1.Rows.Add(row2);
                }
            }
            return dataTable1;
        }

        private DataTable Scraping(string 사업자등록번호)
        {
            object[] objArray = (object[])new string[19];
            objArray[0] = (object)"N";
            objArray[1] = (object)"7";
            objArray[2] = (object)"77";
            objArray[3] = (object)"3";
            objArray[4] = (object)"";
            objArray[5] = (object)"";
            objArray[6] = (object)"";
            objArray[7] = (object)"";
            objArray[8] = (object)"";
            objArray[9] = (object)"";
            objArray[10] = (object)"770";
            objArray[11] = (object)"";
            objArray[12] = (object)"";
            objArray[13] = (object)"";
            objArray[14] = (object)"";
            objArray[15] = (object)사업자등록번호;
            objArray[16] = (object)"";
            objArray[17] = (object)"";
            objArray[18] = (object)"";
            return ServiceFactory.SendRetrieveDataNI(objArray);
        }
    }
}