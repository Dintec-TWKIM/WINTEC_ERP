using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_WORK_SCH11_BIZ
	{
        public DataSet Search(object[] obj, int iSelectedIndex)
        {
            object[] objArray = (object[])obj.Clone();
            SpInfoCollection spCollection = new SpInfoCollection();
            for (int index = 0; index < 2; ++index)
            {
                SpInfo spInfo = new SpInfo();
                if (index == 0)
                {
                    spInfo.SpNameSelect = "UP_CZ_PR_WORK_SELECT";
                    switch (iSelectedIndex)
                    {
                        case 0:
                            obj[0] = "S8";
                            break;
                        case 1:
                            obj[0] = "S10";
                            break;
                        case 2:
                            obj[0] = "S12";
                            break;
                        case 3:
                            obj[0] = "S14";
                            break;
                    }
                    spInfo.SpParamsSelect = obj;
                }
                else
                {
                    spInfo.SpNameSelect = "UP_CZ_PR_WORK_SELECT";
                    switch (iSelectedIndex)
                    {
                        case 0:
                            objArray[0] = "S9";
                            break;
                        case 1:
                            objArray[0] = "S11";
                            break;
                        case 2:
                            objArray[0] = "S13";
                            break;
                        case 3:
                            objArray[0] = "S15";
                            break;
                    }
                    spInfo.SpParamsSelect = objArray;
                }
                spCollection.Add(spInfo);
            }
            return (DataSet)Global.MainFrame.FillDataSet(spCollection);
        }
    }
}