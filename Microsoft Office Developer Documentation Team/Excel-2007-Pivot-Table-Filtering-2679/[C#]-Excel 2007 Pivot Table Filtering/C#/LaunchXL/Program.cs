using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchXL
{
	class Program
	{
		static void Main(string[] args)
		{
			Microsoft.Office.Interop.Excel.Application m_xlApp = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel.Workbook xlWB; 
			Microsoft.Office.Interop.Excel.Worksheet xlWS;
			Microsoft.Office.Interop.Excel.Range xlRG;
			Microsoft.Office.Interop.Excel.PivotCache xlPC;
			object miss = Type.Missing;

			m_xlApp.Visible = true;
			const string olapConnectionString = "OLEDB;Provider=MSOLAP.2;Integrated Security=SSPI;Persist Security Info=True;Data Source=xlextdat8;Client Cache Size=25;Auto Synch Period=10000;Initial Catalog=FoodMart 2000";
			const string cubeName = "Sales";

			xlWB = m_xlApp.Workbooks.Add(miss);
			xlWS = (Microsoft.Office.Interop.Excel.Worksheet)xlWB.Worksheets[1];
			xlRG = xlWS.get_Range("A1", miss);

			// create PT connected to OLAP data source
			xlPC = (Microsoft.Office.Interop.Excel.PivotCache)((Microsoft.Office.Interop.Excel.PivotCaches)xlWB.PivotCaches()).Add(Microsoft.Office.Interop.Excel.XlPivotTableSourceType.xlExternal, miss, Microsoft.Office.Interop.Excel.XlPivotTableVersionList.xlPivotTableVersionCurrent);
			xlPC.Connection = olapConnectionString;
			xlPC.CommandType = Microsoft.Office.Interop.Excel.XlCmdType.xlCmdCube;
			xlPC.CommandText = cubeName;
			xlPC.MaintainConnection = true;
			xlPC.CreatePivotTable(
				xlRG.get_Address(miss, miss, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlR1C1, miss, miss),
				"Name1",
				miss,
				Microsoft.Office.Interop.Excel.XlPivotTableVersionList.xlPivotTableVersionCurrent);
			
			xlWB.Close(false, miss, miss);
			m_xlApp.Quit();
			m_xlApp = null;
		}
	}
}
