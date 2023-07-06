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
		#region ===================================================================================================== Property

		public bool LogAddress
		{
			get
			{
				return chkLogAddress.Checked;
			}
		}

		public bool Agency
		{
			get
			{
				return chkAgency.Checked;
			}
		}

		public bool Inquiry
		{
			get
			{
				return chkInquiry.Checked;
			}
		}

		public bool 발주번호인쇄
		{
			get
			{
				return chk발주번호인쇄.Checked;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_PRT_OPTION()
		{
			InitializeComponent();
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

		}

		private void InitEvent()
		{
			btn인쇄.Click += new EventHandler(btn인쇄_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
		}

		#endregion

		#region ==================================================================================================== 버튼

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
