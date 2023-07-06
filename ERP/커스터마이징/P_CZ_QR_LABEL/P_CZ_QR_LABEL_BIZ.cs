using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_QR_LABEL_BIZ
	{
		internal DataTable Search일반품입고(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GR_S", obj);
			return dataTable;
		}
		internal DataTable Search일반품출고(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GI_S", obj);
			return dataTable;
		}

		internal DataTable Search로케이션(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_QR_LABEL_LOC_S", obj);
			return dataTable;
		}

		internal DataTable Search재고품입고(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GR_STOCK_S", obj);
			return dataTable;
		}
		internal DataTable Search재고품출고(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_QR_LABEL_GI_STOCK_S", obj);
			return dataTable;
		}
	}
}
