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
	public partial class H_CZ_DEBUG_PRINT : Duzon.Common.Forms.CommonDialog
	{
		string message = "";

		#region ==================================================================================================== Constructor

		public H_CZ_DEBUG_PRINT(string message)
		{
			InitializeComponent();
			this.message = message;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitEvent();
		}

		private void InitEvent()
		{
			//txt비밀번호.KeyDown += new KeyEventHandler(txt비밀번호_KeyDown);
			//btn확인.Click += new EventHandler(btn확인_Click);
		}

		protected override void InitPaint()
		{
			txtDebug.Text = message;
		}

		#endregion
	}
}
