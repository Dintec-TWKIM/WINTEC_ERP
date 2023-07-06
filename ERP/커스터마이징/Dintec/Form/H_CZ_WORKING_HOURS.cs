using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dintec
{
	public partial class H_CZ_WORKING_HOURS : System.Windows.Forms.Form
	{
		private int idleTimeSet;	// 분단위 설정
		private DateTime timeStamp;

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;


		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		public H_CZ_WORKING_HOURS()
		{			
			InitializeComponent();
			lbl카운터.Visible = false;
		}

		private void H_CZ_WORKING_HOURS_Load(object sender, EventArgs e)
		{
			string query = "SELECT CD_FLAG1 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_FIELD = 'CZ_HR00007' AND CD_SYSDEF = '001'";
			DataTable dt = DBMgr.GetDataTable(query);
			idleTimeSet = GetTo.Int(dt.Rows[0][0]);
			ShowWorkingHours();
		}
		
		private void H_CZ_WORKING_HOURS_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void Lbl시간_MouseDown(object sender, MouseEventArgs e)
		{
			H_CZ_WORKING_HOURS_MouseDown(this, e);
		}

		private void Pnl닫기_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			int idleTime = idleTimeSet * 60 - GetTo.Int((DateTime.Now - timeStamp).TotalSeconds);    // 15분 * 60초 - 시간간격(초)

			if (idleTime < 0)
			{
				timer1.Stop();
				return;
			}

			int minute = idleTime / 60;
			int second = idleTime % 60;
			lbl카운터.Text = string.Format("({0:00}:{1:00})", minute, second);
			lbl카운터.Visible = true;
			//TopMost = true;
		}

		public void ShowWorkingHours()
		{
			DataTable dt = DBMgr.GetDataTable("PS_CZ_HR_WORKING_HOURS", Global.MainFrame.LoginInfo.CompanyCode, Util.GetToday(), Global.MainFrame.LoginInfo.UserID);
			int workingHours = GetTo.Int(dt.Rows[0]["WORKING_HOURS"]);
			int hour = workingHours / 60;
			int minute = workingHours % 60;

			lbl시간.Text = string.Format("{0:M월 d일} {1:0}시간 {2:0}분", DateTime.Now, hour, minute);
			timeStamp = DateTime.Now;   // 스탬프 리셋
			timer1.Start();
		}
	}
}
