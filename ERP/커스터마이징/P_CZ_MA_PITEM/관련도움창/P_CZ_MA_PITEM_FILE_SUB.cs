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
    public partial class P_CZ_MA_PITEM_FILE_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_PITEM_FILE_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();

            this.btn초기화_Click(null, null);
        }

        private void InitGrid()
        {
            #region 파일
            this._flex파일.BeginSetting(1, 1, false);

            this._flex파일.SetCol("CD_ITEM", "품목코드", 80);
            this._flex파일.SetCol("FILE_NAME", "파일명", 100);
            this._flex파일.SetCol("PATH", "경로", 100);
            this._flex파일.SetCol("RESULT", "결과", 100);

            this._flex파일.SettingVersion = "0.0.0.1";
            this._flex파일.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn초기화.Click += new EventHandler(this.btn초기화_Click);
            this.btn불러오기.Click += new EventHandler(this.btn불러오기_Click);
            this.btn일괄업로드.Click += new EventHandler(this.btn일괄업로드_Click); 
        }

        private void btn초기화_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = new DataTable();

                dt.Columns.Add("CD_ITEM");
                dt.Columns.Add("FILE_NAME");
                dt.Columns.Add("PATH");
                dt.Columns.Add("RESULT");

                this._flex파일.Binding = dt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn불러오기_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo;
            DataRow dr;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                this._flex파일.Redraw = false;

                foreach (string filePath in openFileDialog.FileNames)
                {
                    fileInfo = new FileInfo(filePath);

                    dr = this._flex파일.DataTable.NewRow();

                    dr["CD_ITEM"] = fileInfo.Name.Split('.')[0].Split('_')[0];
                    dr["FILE_NAME"] = fileInfo.Name;
                    dr["PATH"] = filePath;
                    dr["RESULT"] = "대기";

                    this._flex파일.DataTable.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex파일.Redraw = true;
            }
        }

        private void btn일괄업로드_Click(object sender, EventArgs e)
        {
            DataTable tmpDt, tmpDt1;
            DataRow tmpDr;
            string 서버경로, 컬럼명, query, query1;

            try
            {
                foreach(DataRow dr in ComFunc.getGridGroupBy(this._flex파일.DataTable, new string[] { "CD_ITEM" }, true).Rows)
                {
                    query = @"SELECT MI.CD_ITEM,
	                                 MF.IMAGE1,
	                                 MF.IMAGE2,
	                                 MF.IMAGE3,
	                                 MF.IMAGE4,
	                                 MF.IMAGE5,
	                                 MF.IMAGE6,
                                     MF.IMAGE7
                              FROM MA_PITEM MI WITH(NOLOCK)
                              LEFT JOIN CZ_MA_PITEM_FILE MF WITH(NOLOCK) ON MF.CD_COMPANY = MI.CD_COMPANY AND MF.CD_PLANT = MI.CD_PLANT AND MF.CD_ITEM = MI.CD_ITEM
                              WHERE MI.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                             "AND MI.CD_ITEM = '" + dr["CD_ITEM"].ToString() + "'";

                    tmpDt = DBMgr.GetDataTable(query);

                    query1 = string.Empty;

                    foreach (DataRow dr1 in this._flex파일.DataTable.Select("CD_ITEM = '" + dr["CD_ITEM"] + "'"))
                    {
                        if (tmpDt == null || tmpDt.Rows.Count == 0)
                        {
                            dr1["RESULT"] = "품목없음";
                            continue;
                        }

                        if (tmpDt.Select(@"ISNULL(IMAGE1, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR 
                                           ISNULL(IMAGE2, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR 
                                           ISNULL(IMAGE3, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR 
                                           ISNULL(IMAGE4, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR 
                                           ISNULL(IMAGE5, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR 
                                           ISNULL(IMAGE6, '') = '" + dr1["FILE_NAME"].ToString() + @"' OR
                                           ISNULL(IMAGE7, '') = '" + dr1["FILE_NAME"].ToString() + @"'").Length > 0)
                        {
                            dr1["RESULT"] = "동일파일";
                            continue;
                        }

                        if (tmpDt.Select(@"ISNULL(IMAGE1, '') <> '' AND 
                                           ISNULL(IMAGE2, '') <> '' AND 
                                           ISNULL(IMAGE3, '') <> '' AND 
                                           ISNULL(IMAGE4, '') <> '' AND 
                                           ISNULL(IMAGE5, '') <> '' AND 
                                           ISNULL(IMAGE6, '') <> '' AND
                                           ISNULL(IMAGE7, '') <> ''").Length > 0)
                        {
                            dr1["RESULT"] = "공간없음";
                            continue;
                        }

                        서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr1["CD_ITEM"]);

                        if (FileUploader.UploadFile(dr1["FILE_NAME"].ToString(), dr1["PATH"].ToString(), "Upload/P_CZ_MA_PITEM", Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr1["CD_ITEM"])) == "Success")
                        {
                            tmpDr = tmpDt.Rows[0];

                            if (string.IsNullOrEmpty(tmpDr["IMAGE1"].ToString()))
                            {
                                컬럼명 = "IMAGE1";
                                tmpDr["IMAGE1"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE2"].ToString()))
                            {
                                컬럼명 = "IMAGE2";
                                tmpDr["IMAGE2"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE3"].ToString()))
                            {
                                컬럼명 = "IMAGE3";
                                tmpDr["IMAGE3"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE4"].ToString()))
                            {
                                컬럼명 = "IMAGE4";
                                tmpDr["IMAGE4"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE5"].ToString()))
                            {
                                컬럼명 = "IMAGE5";
                                tmpDr["IMAGE5"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE6"].ToString()))
                            {
                                컬럼명 = "IMAGE6";
                                tmpDr["IMAGE6"] = dr1["FILE_NAME"].ToString();
                            }
                            else if (string.IsNullOrEmpty(tmpDr["IMAGE7"].ToString()))
                            {
                                컬럼명 = "IMAGE7";
                                tmpDr["IMAGE7"] = dr1["FILE_NAME"].ToString();
                            }
                            else
                            {
                                dr1["RESULT"] = "업로드실패";
                                continue;
                            }

                            #region CZ_MA_PITEM_FILE
                            query = @"SELECT 1 
                                      FROM CZ_MA_PITEM_FILE WITH(NOLOCK)
                                      WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                     "AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "'";

                            tmpDt1 = DBHelper.GetDataTable(query);

                            if (tmpDt1 != null && tmpDt1.Rows.Count > 0)
                            {
                                query = @"UPDATE CZ_MA_PITEM_FILE
                                          SET " + 컬럼명 + " = '" + dr1["FILE_NAME"].ToString() + "'" + Environment.NewLine +
                                         "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                         "AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "'";
                            }
                            else
                            {
                                query = string.Format(@"INSERT INTO CZ_MA_PITEM_FILE
                                                        (CD_COMPANY, CD_PLANT, CD_ITEM, {0}, ID_INSERT, DTS_INSERT)
                                                        VALUES
                                                        ('{1}', '{2}', '{3}', '{4}', '{5}', NEOE.SF_SYSDATE(GETDATE()))", 컬럼명,
                                                                                                                          Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                          Global.MainFrame.LoginInfo.CdPlant,
                                                                                                                          dr["CD_ITEM"].ToString(),
                                                                                                                          dr1["FILE_NAME"].ToString(),
                                                                                                                          Global.MainFrame.LoginInfo.UserID);
                            }

                            DBHelper.ExecuteScalar(query);
                            #endregion

                            if (컬럼명 != "IMAGE7")
                            {
                                if (string.IsNullOrEmpty(query1))
                                    query1 += 컬럼명 + " = '" + dr1["FILE_NAME"].ToString() + "'";
                                else
                                    query1 += ", " + 컬럼명 + " = '" + dr1["FILE_NAME"].ToString() + "'";
                            }

                            dr1["RESULT"] = "업로드성공";
                        }
                        else
                        {
                            dr1["RESULT"] = "업로드실패";
                            continue;
                        }
                    }

                    #region MA_PITEM
                    if (!string.IsNullOrEmpty(query1))
                    {
                        query = @"UPDATE MA_PITEM
                                  SET " + query1 + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + dr["CD_ITEM"].ToString() + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
