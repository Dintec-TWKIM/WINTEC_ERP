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



namespace Dintec
{
	public partial class H_CZ_CHECK_PW : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_CHECK_PW()
		{
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitEvent();
		}

		private void InitEvent()
		{
			txt비밀번호.KeyDown += new KeyEventHandler(txt비밀번호_KeyDown);
			btn확인.Click += new EventHandler(btn확인_Click);
		}

		protected override void InitPaint()
		{
			txt비밀번호.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt비밀번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (txt비밀번호.Text.Trim() == "")
				{
					Global.MainFrame.ShowMessage("비밀번호를 입력하세요");
					return;
				}
				
				btn확인_Click(null, null);
			}
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn확인_Click(object sender, EventArgs e)
		{
			string pw1 = Util.GetDB_EMP_PW(NO_EMP);
			string pw2 = txt비밀번호.Text;

			if (pw1 == pw2) this.DialogResult = DialogResult.OK;
			else
			{
				Global.MainFrame.ShowMessage("비밀번호가 일치하지 않습니다.");
			}			
		}

		#endregion
	}
}
