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
	public partial class H_CZ_EXTRA_COST : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property
		
		public DataRow SelectedItem
		{
			get
			{
				return grd목록.GetDataRow(grd목록.Row);
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_EXTRA_COST()
		{
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
			
			grd목록.SetCol("CODE"	, "코드"		, 60);
			grd목록.SetCol("NAME"	, "부대비용"	, 230);
			grd목록.SetCol("NO_PART"	, "구분"		, 70);

			grd목록.Cols["CODE"].TextAlign = TextAlignEnum.CenterCenter;
			grd목록.Cols["NO_PART"].TextAlign = TextAlignEnum.CenterCenter;

			grd목록.SetDefault("19.03.21.03", SumPositionEnum.None);
		}

		private void InitEvent()
		{
			grd목록.DoubleClick += new EventHandler(grd목록_DoubleClick);
		}

		protected override void InitPaint()
		{
			grd목록.Binding = GetDb.ExtraCost();
		}

		#endregion


		#region ==================================================================================================== 그리드 이벤트

		private void grd목록_DoubleClick(object sender, EventArgs e)
		{
			if (!grd목록.HasNormalRow || grd목록.MouseCol <= 0)
				return;

			this.DialogResult = DialogResult.OK;
		}

		#endregion
	}
}
