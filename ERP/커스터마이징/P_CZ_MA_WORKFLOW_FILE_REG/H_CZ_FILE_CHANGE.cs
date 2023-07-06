using System;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;

namespace cz
{
	public partial class H_CZ_FILE_CHANGE : Duzon.Common.Forms.CommonDialog
	{
		public bool IsChanged
		{
			get; set;
		}

		public string FileNumberNew
		{
			get
			{
				return tbx파일번호New.Text;
			}			
		}

		public string EmpNameNew
		{
			get
			{
				return ctx영업담당자.CodeName;
			}
		}

		#region ==================================================================================================== Constructor
		public H_CZ_FILE_CHANGE(string fileNumber, string empName)
		{
			InitializeComponent();
			ControlExt.SetTextBoxDefault(this);
			tbx파일번호.Text = fileNumber;
			tbx담당자.Text = empName;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			tbx파일번호.Editable(false);
			tbx파일번호New.Editable(false);
			tbx담당자.Editable(false);
			cbo파일유형.Editable(false);
			ctx영업담당자.Editable(false);

			// 검색콤보 
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
				dt.Rows.Add("파일유형", "FB", "FB");
				dt.Rows.Add("파일유형", "NB", "NB");
				dt.Rows.Add("파일유형", "SB", "SB");
				dt.Rows.Add("파일유형", "NS", "NS");
				dt.Rows.Add("파일유형", "DB", "DB");
				dt.Rows.Add("파일유형", "DS", "DS");
				dt.Rows.Add("파일유형", "TE", "TE");
				dt.Rows.Add("파일유형", "BS", "BS");
				dt.Rows.Add("파일유형", "BE", "BE");
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				dt.Rows.Add("파일유형", "A-", "A-");
				dt.Rows.Add("파일유형", "D-", "D-");
				dt.Rows.Add("파일유형", "N-", "N-");
			}

			cbo파일유형.DataSource = dt.Select("TYPE = '파일유형'").CopyToDataTable();
			cbo파일유형.SelectedIndex = 0;

			InitEvent();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			chk파일유형.CheckedChanged += Chk파일유형_CheckedChanged;
			chk영업담당자.CheckedChanged += Chk영업담당자_CheckedChanged;

			btn저장.Click += Btn저장_Click;
			btn확인.Click += Btn확인_Click;
		}

		private void Chk파일유형_CheckedChanged(object sender, EventArgs e)
		{
			cbo파일유형.Editable(chk파일유형.Checked);
		}

		private void Chk영업담당자_CheckedChanged(object sender, EventArgs e)
		{
			ctx영업담당자.Editable(chk영업담당자.Checked);
		}

		private void Btn저장_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// ********** 유효성 검사
			if (!chk파일유형.Checked && !chk영업담당자.Checked)
			{
				Global.MainFrame.ShowMessage("선택된 항목이 없습니다.");
				return;
			}

			if (chk파일유형.Checked && tbx파일번호.GetValue().Left(2) == cbo파일유형.GetValue())
			{
				Global.MainFrame.ShowMessage("변경하고자 하는 파일유형과 동일합니다.");
				return;
			}

			if (chk영업담당자.Checked && tbx담당자.GetValue() == ctx영업담당자.CodeName)
			{
				Global.MainFrame.ShowMessage("변경하고자 하는 영업담당자와 동일합니다.");
				return;
			}

			if (tbx파일번호New.Text != "")
			{
				Global.MainFrame.ShowMessage("이미 처리되었습니다.");
				return;
			}

			// ********** 저장
			string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			
			// 데이터베이스 파일 변경
			SQL sql = new SQL("PX_CZ_MA_WORKFLOW_FILE_CHANGE", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2("@CD_COMPANY", companyCode);
			sql.Parameter.Add2("@NO_FILE"	, tbx파일번호.Text);
			sql.Parameter.Add2("@CD_FILE"	, chk파일유형.Checked ? cbo파일유형.GetValue() : "");
			sql.Parameter.Add2("@NO_EMP"	, chk영업담당자.Checked ? ctx영업담당자.CodeValue : "");
			DataSet ds = sql.GetDataSet();

			// 실제 파일 Rename (로직상 파일별 MOVE만 되고 폴더는 그대로 남아있음, 해결할 방법 없음)
			if (chk파일유형.Checked)
			{
				tbx파일번호New.Text = (string)ds.Tables[0].Rows[0][0];
				DIR.RenameWF(companyCode, tbx파일번호.Text, tbx파일번호New.Text, ds.Tables[1]);
			}

			IsChanged = true;
			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void Btn확인_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	
		#endregion
	}
}
