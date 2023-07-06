using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

namespace cz
{
	public partial class H_CZ_PRT_OPTION : Duzon.Common.Forms.CommonDialog
	{
		string MODE;

		#region ===================================================================================================== Property

		public bool SelItem
		{
			get
			{
				return chkSelItem.Checked;
			}
		}

		public bool AgentLogo
		{
			get
			{
				return chkAgentLogo.Checked;
			}
		}

		public bool Revised
		{
			get
			{
				return chkRevised.Checked;
			}
		}

		public bool SaveDesktop
		{
			get
			{
				return chk바탕화면저장.Checked;
			}
		}

		public string PrintType
		{
			get
			{
				return cbo인쇄형태.GetValue();
			}
		}

		public string PartnerName
		{
			get
			{
				return tbx거래처명.Text.Trim();
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_PRT_OPTION(string MODE)
		{
			InitializeComponent();
			this.MODE = MODE;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitEvent();
		}

		private void InitControl()
		{
			oneGridItem1.Enabled = false;
			oneGridItem2.Enabled = false;

			if (MODE == "PINQ")
			{
				oneGridItem1.Enabled = true;
				oneGridItem2.Enabled = false;
			}
			else if (MODE == "SQTN")
			{
				oneGridItem1.Enabled = false;
				oneGridItem2.Enabled = true;
			}

			DataTable dt = GetDb.Code("CZ_MA00017");
			DataRow row1 = dt.NewRow();
			row1["NAME"] = "인쇄형태";
			DataRow row2 = dt.NewRow();
			row2["NAME"] = "--------";

			dt.Rows.InsertAt(row1, 0);
			dt.Rows.InsertAt(row2, 1);
			cbo인쇄형태.DataBind(dt, false);

			
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn인쇄.Click += new EventHandler(btn인쇄_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
		}		

		private void btn인쇄_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}
