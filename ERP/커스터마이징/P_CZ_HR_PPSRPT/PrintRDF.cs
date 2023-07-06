using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using System;
using System.Data;

namespace cz
{
    internal class PrintRDF
    {
        private ReportHelper _Print = (ReportHelper)null;
        private DataTable _dtPrint = (DataTable)null;
        private DataTable _dtParam = (DataTable)null;

        public PrintRDF(
          string systemCode,
          string objectName,
          bool isHorizontal,
          DataTable dtPrint,
          DataTable dtPram)
        {
            this._Print = new ReportHelper(systemCode, objectName, false);
            this._dtPrint = dtPrint;
            this._dtParam = dtPram;
            this.InitEvent();
        }

        private void InitEvent() => this._Print.Printing += new ReportHelper.PrintEventHandler(this._Print_Printing);

        private bool _Print_Printing(object sender, PrintArgs args)
        {
            try
            {
                if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    string upper = args.scriptFile.ToUpper();
                    this.SetParamBinding(upper);
                    this.SetDataTableBindig(upper);
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return false;
            }
        }

        private void SetDataTableBindig(string fileName)
        {
            DataTable dt = new DataTable();
            switch (fileName)
            {
                case "":
                    this._Print.SetDataTable(dt);
                    break;
                default:
                    dt = this._dtPrint;
                    goto case "";
            }
        }

        private void SetParamBinding(string fileName)
        {
            switch (fileName)
            {
                case "":
                    break;
                default:
                    this.SetPackageParamBinding();
                    break;
            }
        }

        private void SetPackageParamBinding()
        {
            if (this._dtParam.Rows.Count <= 0)
                return;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            for (int index = 0; index < this._dtParam.Rows.Count; ++index)
                this._Print.SetData(D.GetString(this._dtParam.Rows[index]["ParamCode"]), D.GetString(this._dtParam.Rows[index]["ParamName"]));
        }

        public void ShowDialog() => this._Print.Print();
    }
}
