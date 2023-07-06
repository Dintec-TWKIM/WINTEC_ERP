using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_POP_REG_IMAGE_SUB : Duzon.Common.Forms.CommonDialog
	{
		private string _공장코드;
		private string _품목코드;
		private string _공정경로;
		private string _공정번호;
		private string _공정코드;


		public P_CZ_PR_POP_REG_IMAGE_SUB(string 공장코드, string 품목코드, string 공정경로, string 공정번호, string 공정코드)
		{
			InitializeComponent();

			this._공장코드 = 공장코드;
			this._품목코드 = 품목코드;
			this._공정경로 = 공정경로;
			this._공정번호 = 공정번호;
			this._공정코드 = 공정코드;
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

			DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_ROUT_FILE_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					    this._공장코드,
																						this._품목코드,
																						this._공정경로,
																						this._공정번호,
																					    this._공정코드 });

			this._flex작업지침서.Binding = dt;
		}

		private void InitGrid()
		{
			this._flex작업지침서.BeginSetting(1, 1, false);

			this._flex작업지침서.SetCol("NO_SEQ", "순번", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지침서.SetCol("NM_FILE", "파일명", 300);
			this._flex작업지침서.SetCol("NM_INSERT", "등록자", 100);
			this._flex작업지침서.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업지침서.SetCol("DC_FILE", "비고", 100, true);

			this._flex작업지침서.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			this._flex작업지침서.ExtendLastCol = true;

			this._flex작업지침서.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btn열기.Click += Btn열기_Click;
			this.btn미리보기.Click += Btn미리보기_Click;
		}

		private void Btn열기_Click(object sender, EventArgs e)
		{
			string key, key1, key2, key3, serverPath, localPath, fileName;

			try
			{
				if (!this._flex작업지침서.HasNormalRow) return;

				fileName = this._flex작업지침서["NM_FILE"].ToString();

				key = this._flex작업지침서["CD_ITEM"].ToString();
				key1 = this._flex작업지침서["NO_OPPATH"].ToString();
				key2 = this._flex작업지침서["CD_OP"].ToString();
				key3 = this._flex작업지침서["CD_WCOP"].ToString();

				serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PR_ROUT_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3 + "/";
				localPath = Application.StartupPath + "/temp/";

				if (string.IsNullOrEmpty(fileName))
					return;
				else
				{
					WebClient wc = new WebClient();

					Directory.CreateDirectory(localPath);
					wc.DownloadFile(serverPath + fileName, localPath + fileName);
					Process.Start(localPath + fileName);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn미리보기_Click(object sender, EventArgs e)
		{
			string key, key1, key2, key3, serverPath, fileName;

			try
			{
				if (!this._flex작업지침서.HasNormalRow) return;

				fileName = this._flex작업지침서["NM_FILE"].ToString();

				key = this._flex작업지침서["CD_ITEM"].ToString();
				key1 = this._flex작업지침서["NO_OPPATH"].ToString();
				key2 = this._flex작업지침서["CD_OP"].ToString();
				key3 = this._flex작업지침서["CD_WCOP"].ToString();

				serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PR_ROUT_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "_" + key1 + "_" + key2 + "_" + key3 + "/";

				if (string.IsNullOrEmpty(fileName))
				{
					this.web작업지침서.Navigate(string.Empty);
					return;
				}

				this.web작업지침서.Navigate(serverPath + fileName);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}
