using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Dintec;
using Duzon.ERPU.Utils;
using Duzon.ERPU;
using Duzon.Common.Util;
using System.Text.RegularExpressions;

namespace cz
{
    public partial class P_CZ_MA_HULL_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_HULL_SUB()
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

	        this._flex파일.SetCol("NO_IMO", "IMO 번호", 80);
			this._flex파일.SetCol("NO_SUPPLIER", "매입처번호", 80);

			this._flex파일.SetCol("URL", "URL", false);
			this._flex파일.SetCol("CD_MODULE", "모듈", false);
			this._flex파일.SetCol("ID_MENU", "페이지명", false);
			this._flex파일.SetCol("CD_FILE", "파일코드", false);
			this._flex파일.SetCol("NO_SEQ", "순번", false);
			this._flex파일.SetCol("FILE_NAME", "관련파일명", 240);
			this._flex파일.SetCol("FILE_PATH", "FILE_PATH", false);
			this._flex파일.SetCol("FILE_EXT", "파일형식", false);
			this._flex파일.SetCol("FILE_DATE", "파일날짜", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex파일.SetCol("FILE_TIME", "시간", 50);
			this._flex파일.SetCol("FILE_SIZE", "용량", 50);
			this._flex파일.SetCol("FILE_STATE", "상태", 80);

			this._flex파일.Cols["FILE_TIME"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex파일.Cols["FILE_SIZE"].TextAlign = TextAlignEnum.RightCenter;
			this._flex파일.Cols["FILE_STATE"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex파일.Cols["FILE_TIME"].Format = this._flex파일.Cols["FILE_TIME"].EditMask = "0#:##";

			this._flex파일.SetStringFormatCol("FILE_TIME");
			this._flex파일.SetNoMaskSaveCol("FILE_TIME");

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

                dt.Columns.Add("NO_IMO");
				dt.Columns.Add("CD_SUPPLIER");
				dt.Columns.Add("NO_TYPE");

				dt.Columns.Add("URL");
				dt.Columns.Add("CD_MODULE");
				dt.Columns.Add("ID_MENU");
				dt.Columns.Add("CD_FILE");
				dt.Columns.Add("NO_SEQ");
				dt.Columns.Add("FILE_NAME");
				dt.Columns.Add("FILE_PATH");
				dt.Columns.Add("FILE_EXT");
				dt.Columns.Add("FILE_DATE");
				dt.Columns.Add("FILE_TIME");
				dt.Columns.Add("FILE_SIZE");
				dt.Columns.Add("FILE_STATE");

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

                    dr["NO_IMO"] = fileInfo.Name.Split('.')[0].Split('_')[0];
					dr["CD_SUPPLIER"] = fileInfo.Name.Split('.')[0].Split('_')[1];
					dr["NO_TYPE"] = fileInfo.Name.Split('.')[0].Split('_')[2];

					dr["URL"] = fileInfo.FullName;
					dr["NO_SEQ"] = 0;
					dr["CD_MODULE"] = "MA";
					dr["ID_MENU"] = "P_CZ_MA_HULL";
					dr["CD_FILE"] = dr["NO_IMO"].ToString() + "_" + dr["CD_SUPPLIER"].ToString() + "_" + dr["NO_TYPE"].ToString();
					dr["FILE_PATH"] = "Upload/P_CZ_MA_HULL/" + dr["NO_IMO"].ToString();
					dr["FILE_EXT"] = fileInfo.Extension.Replace(".", "");
					dr["FILE_NAME"] = fileInfo.Name;
					dr["FILE_DATE"] = fileInfo.LastWriteTime.ToString("yyyyMMdd");
					dr["FILE_TIME"] = fileInfo.LastWriteTime.ToString("hhmm");
					dr["FILE_SIZE"] = string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576)));
					dr["FILE_STATE"] = "대기";

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
			string query;

            try
            {
				query = @"SELECT ISNULL(MAX(NO_SEQ), 0) + 1 
						  FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = 'K100'
						  AND CD_MODULE = 'MA'
                          AND ID_MENU = 'P_CZ_MA_HULL'
                          AND CD_FILE = '{0}'";

				foreach (DataRow dr in this._flex파일.DataTable.Rows)
                {
					dr["NO_SEQ"] = DBHelper.ExecuteScalar(string.Format(query, dr["CD_FILE"].ToString()));

					if (FileUploader.UploadFile(dr["FILE_NAME"].ToString(), dr["URL"].ToString(), dr["FILE_PATH"].ToString(), dr["CD_FILE"].ToString()) == "Success")
					{
						dr["FILE_STATE"] = "업로드성공";

						DBHelper.ExecuteNonQuery("UP_MA_FILEINFO_INSERT", new object[] { "K100",
																					     dr["CD_MODULE"].ToString(),
																					     dr["ID_MENU"].ToString(),
																					     dr["CD_FILE"].ToString(),
																					     dr["NO_SEQ"].ToString(),
																						 dr["FILE_NAME"].ToString(),
																					     dr["FILE_PATH"].ToString(),
																					     dr["FILE_EXT"].ToString(),
																					     dr["FILE_DATE"].ToString(),
																					     dr["FILE_TIME"].ToString(),
																					     dr["FILE_SIZE"].ToString(),
																					     Global.MainFrame.LoginInfo.UserID });
					}
					else
					{
						dr["FILE_STATE"] = "업로드실패";
						continue;
					}
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
	}
}
