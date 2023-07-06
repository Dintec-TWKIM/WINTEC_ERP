using Duzon.Common.Forms;
using System;
using System.Data;
using System.Windows.Forms;
using Dass.FlexGrid;
using System.IO;

namespace Dintec
{
	public partial class H_CZ_GRID_CONFIG : Duzon.Common.Forms.CommonDialog
	{
		public FlexGrid FlexGrid { get; set; }

		#region ==================================================================================================== Constructor
		public H_CZ_GRID_CONFIG(FlexGrid flexGrid)
		{
			InitializeComponent();
			FlexGrid = flexGrid;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitEvent();
		}

		protected override void InitPaint()
		{
			
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn초기화.Click += Btn초기화_Click;
			btn설정저장.Click += Btn설정저장_Click;
			btn설정적용.Click += Btn설정적용_Click;
		}
		
		private void Btn초기화_Click(object sender, EventArgs e)
		{
			// 그리드 초기화
			if (Global.MainFrame.ShowMessage("해당 그리드를 초기화 하시겠습니까?", "QY2") == DialogResult.Yes)
			{
				string formName = FlexGrid.FindForm().ActiveControl.ToString().Split('.')[1];
				File.Create(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".init");
				Global.MainFrame.ShowMessage("초기화 완료되었습니다. 해당 메뉴 재시작 바랍니다.");

				DialogResult = DialogResult.OK;
			}			
		}

		private void Btn설정저장_Click(object sender, EventArgs e)
		{
			if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 저장 하시겠습니까?", "QY2") == DialogResult.Yes)
			{				
				DataTable dt = new DataTable();
				dt.Columns.Add("SEQ"		, typeof(int));
				dt.Columns.Add("COL_NAME"	, typeof(string));
				dt.Columns.Add("COL_WIDTH"	, typeof(int));
				dt.Columns.Add("VISIBLE"	, typeof(int));

				for (int i = 1; i < FlexGrid.Cols.Count; i++)
					dt.Rows.Add(i, FlexGrid.Cols[i].Name, FlexGrid.Cols[i].Width, FlexGrid.Cols[i].Visible ? 1 : 0);

				SQL sql = new SQL("PX_CZ_MA_USER_CONFIG_GRID", SQLType.Procedure);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP"	, Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID"	, GetFormName() + "." + FlexGrid.Name);
				sql.Parameter.Add2("@XML"		, dt.ToXml());
				sql.ExecuteNonQuery();

				DialogResult = DialogResult.OK;
			}
		}

		private void Btn설정적용_Click(object sender, EventArgs e)
		{
			if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 불러오시겠습니까?", "QY2") == DialogResult.Yes)
			{
				string query = "SELECT * FROM CZ_MA_USER_CONFIG_GRID WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = @NO_EMP AND GRID_UID = @GRID_UID ORDER BY SEQ";

				SQL sql = new SQL(query, SQLType.Text);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP"	, Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID"	, GetFormName() + "." + FlexGrid.Name);
				DataTable dt = sql.GetDataTable();

				foreach (DataRow row in dt.Rows)
				{
					int indexOld = FlexGrid.Cols[row["COL_NAME"].ToString()].Index;
					int indexNew = row["SEQ"].ToInt();
					
					// 순서 설정
					if (indexOld != indexNew)
						FlexGrid.Cols.Move(indexOld, indexNew);

					// 넓이 설정
					if (row["COL_WIDTH"].ToInt() > -1)
						FlexGrid.Cols[indexNew].Width = row["COL_WIDTH"].ToInt();

					// Visible 설정
					FlexGrid.Cols[indexNew].Visible = row["VISIBLE"].ToBoolean();
				}

				DialogResult = DialogResult.OK;
			}
		}

		private string GetFormName()
		{
			Control nowCon = FlexGrid;

			for (int i = 0; i < 15; i++)
			{
				if (nowCon.Parent != null)
				{
					nowCon = nowCon.Parent;

					if (nowCon.Name.IndexOf("P_CZ") >= 0)
						return nowCon.Name;
				}
			}

			return "";
		}

		#endregion
	}
}
