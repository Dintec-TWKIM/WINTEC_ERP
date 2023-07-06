using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_FI_CARD_STATE_MNG : PageBase
	{
		P_CZ_FI_CARD_STATE_MNG_BIZ _biz = new P_CZ_FI_CARD_STATE_MNG_BIZ();

		public P_CZ_FI_CARD_STATE_MNG()
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
			this.MainGrids = new FlexGrid[] { this._flex };

			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("TP_CARD", "카드유형", 100);
			this._flex.SetCol("CD_DEPT", "부서코드", 100);
			this._flex.SetCol("NO_CARD", "카드번호", 200);
			this._flex.SetCol("NM_FILE", "파일이름", 200);

			this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
		}

		private void InitEvent()
		{
			this.btn카드정보업로드.Click += Btn카드정보업로드_Click;
			this.btn명세서업로드BC.Click += Btn명세서업로드_Click;
			this.btn명세서업로드하나.Click += Btn명세서업로드_Click;
			this.btn명세서보기.Click += Btn명세서보기_Click;

			this.bpc카드번호.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);
		}

		private void Btn카드정보업로드_Click(object sender, EventArgs e)
		{
			DataTable dtExcel;
			string fileName;

			try
			{
				if (!Directory.Exists("C:\\BCCARD")) return;

				if (string.IsNullOrEmpty(this.dtp명세년월.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl명세년월.Text);
					return;
				}

				DirectoryInfo dir = new DirectoryInfo("C:\\BCCARD");

				if (dir.GetFiles("*.xls").Length == 0)
					return;

				fileName = dir.GetFiles("*.xls")[0].FullName;

				dtExcel = ExcelReader.Read2(fileName, 1, 2);

				if (this._biz.SaveExcel(this.dtp명세년월.Text, dtExcel))
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn카드정보업로드.Text });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn명세서보기_Click(object sender, EventArgs e)
		{
			string fileName, serverPath;

			try
			{
				if (!this._flex.HasNormalRow) return;

				fileName = this._flex["NM_FILE"].ToString();
				
				if (string.IsNullOrEmpty(fileName))
				{
					this.webBrowser.Navigate(string.Empty);
					return;
				}

				if (this._flex["TP_CARD"].ToString() == "BC카드")
					serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_FI_CARD_STATE_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp명세년월.Text + "/" + this._flex["CD_DEPT"].ToString() + "/";
				else if (this._flex["TP_CARD"].ToString() == "하나카드")
					serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_FI_CARD_STATE_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp명세년월.Text + "/" + this._flex["NO_CARD"].ToString() + "/";
				else
					return;

				this.webBrowser.Navigate(serverPath + fileName);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp명세년월.Text = Global.MainFrame.GetDateTimeToday().ToString("yyyyMM");
		}

		private void BpControl_QueryBefore(object sender, BpQueryArgs e)
		{
			string name;

			try
			{
				name = (sender as Control).Name;

				if (name == bpc카드번호.Name)
					this.bpc카드번호.UserParams = "카드도움창;H_FI_CARD_USER;" + Global.MainFrame.LoginInfo.UserID;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn명세서업로드_Click(object sender, EventArgs e)
		{
			string name, query;

			try
			{
				name = ((Control)sender).Name;

				if (string.IsNullOrEmpty(this.dtp명세년월.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl명세년월.Text);
					return;
				}

				if (string.IsNullOrEmpty(this.txt부서코드.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl부서코드.Text);
					return;
				}

				if (name == this.btn명세서업로드BC.Name)
				{
					#region BC카드
					query = @"SELECT * 
FROM CZ_FI_CARD_STATE CS
WHERE CS.CD_COMPANY = '{0}'
AND CS.DT_MONTH = '{1}'
AND CS.CD_DEPT = '{2}'";
					string key = this.txt부서코드.Text.Split('(')[0];

					DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																			  this.dtp명세년월.Text,
																			  key));

					if (dt == null || dt.Rows.Count == 0)
					{
						this.ShowMessage("부서코드에 해당하는 카드번호가 없습니다.");
						return;
					}

					OpenFileDialog openFileDialog = new OpenFileDialog();

					if (openFileDialog.ShowDialog() != DialogResult.OK)
						return;

					FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

					string 업로드위치 = "Upload/P_CZ_FI_CARD_STATE_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp명세년월.Text;

					FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, key);
					this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, key, fileInfo, 업로드위치, "P_CZ_FI_CARD_STATE_MNG");

					query = @"UPDATE CS
SET CS.NM_FILE = '{3}',
    CS.ID_UPDATE = '{4}',
	CS.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_FI_CARD_STATE CS
WHERE CS.CD_COMPANY = '{0}'
AND CS.DT_MONTH = '{1}'
AND CS.CD_DEPT = '{2}'";

					DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																this.dtp명세년월.Text,
																key,
																fileInfo.Name,
															    Global.MainFrame.LoginInfo.UserID));

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn명세서업로드BC.Text);
					#endregion
				}
				else if (name == this.btn명세서업로드하나.Name)
				{
					#region 하나카드
					if (string.IsNullOrEmpty(this.txt카드번호.Text))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl카드번호.Text);
						return;
					}

					query = @"SELECT NO_CARD 
FROM FI_CARD WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND NO_CARD LIKE '{1}'
AND YN_USE = 'Y'";

					object obj = DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																							this.txt카드번호.Text.Replace("****", "%") }));

					string 카드번호 = string.Empty;

					if (obj != null)
						카드번호 = obj.ToString();
					
					if (string.IsNullOrEmpty(카드번호))
					{
						this.ShowMessage("카드번호를 찾을 수 없습니다.");
						return;
					}

					OpenFileDialog openFileDialog = new OpenFileDialog();

					if (openFileDialog.ShowDialog() != DialogResult.OK)
						return;

					FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

					string 업로드위치 = "Upload/P_CZ_FI_CARD_STATE_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp명세년월.Text;

					FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, 카드번호);
					this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, 카드번호, fileInfo, 업로드위치, "P_CZ_FI_CARD_STATE_MNG");

					query = @"IF EXISTS (SELECT 1 
           FROM CZ_FI_CARD_STATE CS WITH(NOLOCK)
		   WHERE CS.CD_COMPANY = '{0}'
		   AND CS.DT_MONTH = '{1}'
		   AND CS.NO_CARD = '{2}')
BEGIN
	UPDATE CS
	SET CS.NM_FILE = '{3}',
	    CS.CD_DEPT = '{4}',
		CS.ID_UPDATE = '{5}',
		CS.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
    FROM CZ_FI_CARD_STATE CS
	WHERE CS.CD_COMPANY = '{0}'
	AND CS.DT_MONTH = '{1}'
	AND CS.NO_CARD = '{2}'
END
ELSE
BEGIN
	INSERT INTO CZ_FI_CARD_STATE
	(
		CD_COMPANY,
		DT_MONTH,
		NO_CARD,
		TP_CARD,
		CD_DEPT,
		NM_FILE,
		ID_INSERT,
		DTS_INSERT
	)
	VALUES
	(
		'{0}',
		'{1}',
		'{2}',
		'하나카드',
		'{3}',
		'{4}',
		'{5}',
		NEOE.SF_SYSDATE(GETDATE())
	)
END";

					DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																this.dtp명세년월.Text,
																카드번호,
																this.txt부서코드.Text,
																fileInfo.Name,
															    Global.MainFrame.LoginInfo.UserID));

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn명세서업로드하나.Text);
					#endregion
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (string.IsNullOrEmpty(this.bpc카드번호.QueryWhereIn_Pipe))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl카드번호.Text);
					return;
				}

				this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																     this.dtp명세년월.Text,
																     this.bpc카드번호.QueryWhereIn_Pipe });

				if (!this._flex.HasNormalRow)
					this.ShowMessage(PageResultMode.SearchNoData);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}
