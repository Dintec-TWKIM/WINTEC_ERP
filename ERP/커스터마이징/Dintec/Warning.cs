using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Forms;

namespace Dintec
{
	public class Warning
	{
		string companyCode;
		WarningFlag flag;

		string buyerCode;
		string imoNumber;
		string supplierCode;
		DataTable item;

		int count;
		string message;
		bool allowSave;
		
		public string WarningCode
		{
			get; set;
		}

		public string FileCode
		{
			get; set;
		}

		public string BuyerCode
		{
			set
			{
				buyerCode = value;
			}
		}

		public string ImoNumber
		{
			set
			{
				imoNumber = value;
			}
		}

		public string SupplierCode
		{
			set
			{
				supplierCode = value;
			}
		}

		public DataTable Item
		{
			set
			{
				item = value.Copy();
			}
		}


		public int Count
		{
			get
			{
				return count;
			}
		}

		public string Message
		{
			get
			{
				return message;
			}
		}

		public bool AllowSave
		{
			get
			{
				return allowSave;
			}
		}


		public Warning(WarningFlag flag)
		{
			this.companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			this.flag = flag;
			this.buyerCode = "";
			this.imoNumber = "";
			this.supplierCode = "";

			this.count = 0;
			this.message = "";
			this.allowSave = false;
		}

		public void Check()
		{
			// 경고마스터 확인
			if (!item.Columns.Contains("CD_SUPPLIER"))
				item.Columns.Add("CD_SUPPLIER", typeof(string), "'" + supplierCode + "'");

			SQL sql = new SQL("PS_CZ_MA_WARNING_CHK_R5", SQLType.Procedure, SQLDebug.Print);	
			sql.Parameter.Add2("@CD_COMPANY"	, companyCode);
			sql.Parameter.Add2("@WARNING_FLAG"	, flag.ToString());
			sql.Parameter.Add2("@CD_WARNING"	, WarningCode);
			sql.Parameter.Add2("@CD_FILE"		, FileCode);
			sql.Parameter.Add2("@CD_BUYER"		, buyerCode);
			sql.Parameter.Add2("@NO_IMO"		, imoNumber);
			sql.Parameter.Add2("@XML"			, GetTo.Xml(item));
			DataTable dtWarning = sql.GetDataTable();

			count = dtWarning.Rows.Count;
			message = "";

			// 경고가 있는 경우
			if (count > 0)
			{
				foreach (DataRow row in dtWarning.Rows)
					message += "[경고!!] " + row["NM_CODE"] + "(" + row["NO_WARNING"] + ")" + " - " + row["DC_MSG_KO"] + "\r\n\r\n\r\n";

				if (dtWarning.Select("YN_SAVE = 'N'").Length > 0)
					allowSave = false;
				else
					allowSave = true;
			}
		}
	}

	public enum WarningFlag
	{
		  QTN
		, SO 
		, PO 
		, GIR
		, GR 
		, GI 
	}
}
