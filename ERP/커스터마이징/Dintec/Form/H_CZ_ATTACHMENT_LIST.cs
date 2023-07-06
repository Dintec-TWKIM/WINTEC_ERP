using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.BpControls;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace Dintec
{
	public partial class H_CZ_ATTACHMENT_LIST : Duzon.Common.Forms.CommonDialog
	{
		readonly string _companyCode;
		readonly private DataTable _dt;
		bool _checkable;

		#region ===================================================================================================== Property

		public bool Checkable
		{
			get
			{
				return _checkable;
			}
			set
			{
				_checkable = value;

				if (_checkable)
					btn발송용저장.Visible = true;
				else
					btn발송용저장.Visible = false;
			}
		}


		public DataTable SelectedItem
		{
			get
			{
				return grd목록.GetCheckedRows("CHK");
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_ATTACHMENT_LIST(DataTable fileList)
		{
			_companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			_dt = fileList;
			_checkable = false;
			InitializeComponent();
		}
		
		#endregion


		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();
		}

		private void InitGrid()
		{
			grd목록.BeginSetting(1, 1, false);

			grd목록.SetCol("CHK"			, "S"			, 30	, true, CheckTypeEnum.Y_N);
			grd목록.SetCol("FILE_TYPE"	, "구분"			, 40);
			grd목록.SetCol("FILE_ICON"	, "유형"			, 40);
			grd목록.SetCol("FILE_PATH"	, "파일경로"		, false);
			grd목록.SetCol("FILE_NAME"	, "파일명"		, 350);
			grd목록.SetCol("FILE_DATA"	, "파일Data"		, false);
			grd목록.SetCol("FILE_SIZE"	, "용량"			, 60);
			grd목록.SetCol("DTS_INSERT"	, "등록일자"		, 130);
			grd목록.SetCol("DC_RMK"		, "비고"			, 130);
			grd목록.SetCol("NO_FILE"		, "파일번호"		, false);
			grd목록.SetCol("NO_LINE"		, "항번"			, false);

			grd목록.SetDataMap("FILE_TYPE", GetDb.Code("CZ_MA00022"), "CODE", "NAME");

			grd목록.Cols["FILE_TYPE"].TextAlign = TextAlignEnum.CenterCenter;
			grd목록.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd목록.Cols["FILE_SIZE"].TextAlign = TextAlignEnum.RightCenter;

			grd목록.SetDefault("19.12.26.01", SumPositionEnum.None);
		}
		
		protected override void InitPaint()
		{
			if (!_dt.Columns.Contains("FILE_SIZE"))
				_dt.Columns.Add("FILE_SIZE", typeof(string));
			if (!_dt.Columns.Contains("CHK"))
				_dt.Columns.Add("CHK", typeof(string));

			foreach (DataRow row in _dt.Rows)
				row["CHK"] = "N";

			grd목록.Binding = _dt;
			SetGridStyle();

			if (!Checkable)
				grd목록.Cols.Remove("CHK");
		}

		#endregion


		#region ==================================================================================================== 그리드 이벤트

		private void InitEvent()
		{
			grd목록.DoubleClick += new EventHandler(grd목록_DoubleClick);
			btn발송용저장.Click += Btn발송용저장_Click;
		}

		private void Btn발송용저장_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void grd목록_DoubleClick(object sender, EventArgs e)
		{
			// 헤더 클릭
			if (grd목록.MouseRow == 0 && grd목록.MouseCol > 0)
			{
				SetGridStyle();
				return;
			}

			// 첨부파일 열기
			string colName = grd목록.Cols[grd목록.Col].Name;

			if (colName == "FILE_ICON" || colName == "FILE_NAME")
			{
				string fileType = grd목록["FILE_TYPE"].ToString();

				if (fileType == "U")		// 일반파일
					FileMgr.Download(grd목록["FILE_PATH"] + @"\" + grd목록["FILE_NAME"], true);
				else if (fileType == "W")	// 워크플로우
					FileMgr.Download_WF(_companyCode, grd목록["NO_FILE"].ToString(), grd목록["FILE_NAME"].ToString(), true);
				else if (fileType == "S")	// SRM
					FileMgr.DownloadBinary(grd목록["FILE_NAME"].ToString(), grd목록["FILE_DATA"]);
			}
		}

		private void SetGridStyle()
		{
			for (int i = grd목록.Rows.Fixed; i < grd목록.Rows.Count; i++)
			{				
				double fileSize = 0;
				string fileType = grd목록[i, "FILE_TYPE"].ToString();
				string fileName = grd목록[i, "FILE_NAME"].ToString();

				// 확장자 아이콘
				Image icon = Icons.GetExtension(fileName.Substring(fileName.LastIndexOf(".") + 1));
				grd목록.SetCellImage(i, grd목록.Cols["FILE_ICON"].Index, icon);

				// 파일 용량
				if (fileType == "U")
				{
					string serverPath = grd목록[i, "FILE_PATH"].ToString();

					WebClient wc = new WebClient();
					wc.OpenRead(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(fileName));

					fileSize = GetTo.Int(wc.ResponseHeaders["Content-Length"]);
				}
				else if (fileType == "W")
				{
					string fileNumber = grd목록[i, "NO_FILE"].ToString();
					string query = @"
SELECT
	DTS_INSERT
FROM CZ_MA_WORKFLOWH WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + _companyCode + @"'
	AND NO_KEY = '" + fileNumber + @"'
ORDER BY TP_STEP";

					DataTable dt = DBMgr.GetDataTable(query);
					string yyyy = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString().Substring(0, 4) : Util.GetToday().Substring(0, 4);
					string serverPath = "WorkFlow/" + _companyCode + "/" + yyyy + "/" + fileNumber;

					WebClient wc = new WebClient();
					wc.OpenRead(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(fileName));

					fileSize = GetTo.Int(wc.ResponseHeaders["Content-Length"]);
				}
				else if (fileType == "S")
				{
					fileSize = ((byte[])grd목록[i, "FILE_DATA"]).Length;
				}

				grd목록[i, "FILE_SIZE"] = Math.Ceiling(fileSize / 1024) + "KB";
			}
		}

		#endregion
	}
}
