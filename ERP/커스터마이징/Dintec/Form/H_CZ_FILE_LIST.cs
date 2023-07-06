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
	public partial class H_CZ_FILE_LIST : Duzon.Common.Forms.CommonDialog
	{
		readonly string _companyCode;

		readonly DataTable FileTable;

		#region ===================================================================================================== Property

		public bool Checkable
		{
			set
			{
				if (value)
					btn발송용저장.Visible = true;
				else
					btn발송용저장.Visible = false;
			}
		}

		public DataTable ChangedItem
		{
			get
			{
				return grd목록.GetChanges();
			}
		}

		public DataTable SelectedItem
		{
			get
			{
				return grd목록.DataTable.Select("CHK = 'Y'").ToDataTable();
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_FILE_LIST(DataTable fileTable)
		{
			InitializeComponent();
			FileTable = fileTable;
		}
		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();
		}
		
		protected override void InitPaint()
		{
			Search();

			if (!btn발송용저장.Visible)
				grd목록.Cols.Remove("CHK");
		}

		#endregion

		#region ==================================================================================================== Grid

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
			grd목록.SetCol("DTS_INSERT"	, "등록일자"		, 140	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd목록.SetCol("DC_RMK"		, "비고"			, 130);

			grd목록.SetDataMap("FILE_TYPE", BASE.Code("CZ_MA00022"), "CODE", "NAME");

			grd목록.Cols["FILE_TYPE"].TextAlign = TextAlignEnum.CenterCenter;
			grd목록.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd목록.Cols["FILE_SIZE"].TextAlign = TextAlignEnum.RightCenter;
			grd목록.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			grd목록.SetDefault("20.09.01.01", SumPositionEnum.None);
		}
		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn발송용저장.Click += Btn발송용저장_Click;
			grd목록.DoubleClick += Grd목록_DoubleClick;			
		}

		private void Btn발송용저장_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
		
		private void Grd목록_DoubleClick(object sender, EventArgs e)
		{
			// 헤더 클릭
			if (grd목록.MouseRow < grd목록.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 첨부파일 열기
			string colName = grd목록.Cols[grd목록.Col].Name;

			if (colName.In("FILE_ICON", "FILE_NAME"))
			{
				string fileType = CT.String(grd목록["FILE_TYPE"]);
				
				if (fileType.In("P", "W"))
					FILE.Download(grd목록["FILE_PATH"] + @"\" + grd목록["FILE_NAME"], true);
				else if (fileType == "S")
					FILE.DownloadBinary(grd목록["FILE_NAME"], grd목록["FILE_DATA"], true);
			}
		}

		private void SetGridStyle()
		{
			for (int i = grd목록.Rows.Fixed; i < grd목록.Rows.Count; i++)
				grd목록.SetCellImage(i, grd목록.Cols["FILE_ICON"].Index, Icons.GetExtension(grd목록[i, "FILE_NAME"].ToString()));
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			// 파일 용량 가져오기
			if (!FileTable.Columns.Contains("FILE_SIZE"))
				FileTable.Columns.Add("FILE_SIZE", typeof(string));
			
			foreach (DataRow row in FileTable.Rows)
			{
				string fileType = row["FILE_TYPE"].ToString();
				string fileName = row["FILE_NAME"].ToString();
				double fileSize;

				if (fileType == "S")
				{
					fileSize = ((byte[])row["FILE_DATA"]).Length;
				}
				else
				{
					WebClient wc = new WebClient();
					wc.OpenRead(Global.MainFrame.HostURL + "/" + row["FILE_PATH"] + "/" + Uri.EscapeDataString(fileName));
					fileSize = wc.ResponseHeaders["Content-Length"].ToInt();
				}

				row["FILE_SIZE"] = Math.Ceiling(fileSize / 1024) + "KB";
				row.AcceptChanges();
			}

			// 바인딩
			grd목록.Binding = FileTable;
			SetGridStyle();
		}

		#endregion
	}
}
