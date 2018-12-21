using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
namespace MedicalSys.Framework.Utility
{
    /// <summary>
    /// Class ExcelHelper
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static IMSLogger logger = LogFactory.GetLogger();
        /// <summary>
        /// Export to the excel sheet.
        /// </summary>
        /// <param name="dataTableList">The data table list.</param>
        /// <param name="strFileName">The file name.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool ToExcelSheet(List<DataTable> dataTableList, string strFileName, string templateName)
        {
            //Exsel模板
            string templetFile = AssemblyHelper.GetExecuteAssemblyPath() + @"\Template\" + templateName;
            System.Reflection.Missing miss = System.Reflection.Missing.Value;
            //创建EXCEL对象appExcel,Workbook对象,Worksheet对象,Range对象
            Excel.Application appExcel;
            appExcel = new Excel.Application();
            Excel.Workbook workbookData;
            Excel.Worksheet worksheetData = null;
            Excel.Range rangedata;
            //设置对象不可见
            appExcel.Visible = false;
            /* 在调用Excel应用程序，或创建Excel工作簿之前，记着加上下面的两行代码
             * 这是因为Excel有一个Bug，如果你的操作系统的环境不是英文的，而Excel就会在执行下面的代码时，报异常。
             */
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //以模板的方式
            workbookData = appExcel.Workbooks.Open(templetFile, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss);

            try
            {
                for (int k = 0; k < dataTableList.Count; k++)
                {
                    bool existSheet = CheckWorksheet(workbookData, dataTableList[k].TableName, out worksheetData);

                    if (!existSheet)
                    {

                        worksheetData = (Excel.Worksheet)workbookData.Worksheets.Add(System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value);
                        worksheetData.Name = dataTableList[k].TableName;
                    }
                    if (dataTableList[k] != null)
                    {
                        //表字段（表头）
                        for (int i = 0; i < dataTableList[k].Columns.Count; i++)
                        {
                            worksheetData.Cells[i + 1] = dataTableList[k].Columns[i].ColumnName.ToString();
                        }
                        //先给Range对象一个范围为A2开始，Range对象可以给一个CELL的范围，也可以给例如A1到H10这样的范围
                        //因为第一行已经写了表头，所以所有数据都应该从A2开始
                        rangedata = worksheetData.get_Range("A2", miss);
                        Excel.Range xlRang = null;
                        //总行数
                        int iRowCount = dataTableList[k].Rows.Count;
                        //iEachSize当前每次导出的行数的页面大小，可以自己设置
                        int iEachSize = 2;
                        //已导出的行数     ，当前每次导出的行数大小
                        int iParstedRow = 0, iCurrSize = 0;
                        //列数
                        int iColumnAccount = dataTableList[k].Columns.Count;
                        //在内存中声明一个iEachSize×iColumnAccount的数组，iEachSize是每次最大存储的行数，iColumnAccount就是存储的实际列数
                        object[,] objVal = new object[iEachSize, iColumnAccount];
                        iCurrSize = iEachSize;

                        //Range方法导出表数据（此方法导出很快，建议用此方法）
                        while (iParstedRow < iRowCount)
                        {
                            //总行数-已导出行数；剩余行数是否小于页面大小，小于的话，当前导出行数为余行
                            if ((iRowCount - iParstedRow) < iEachSize)
                            {
                                iCurrSize = iRowCount - iParstedRow;
                            }
                            for (int i = 0; i < iCurrSize; i++)
                            {
                                for (int j = 0; j < iColumnAccount; j++)
                                {
                                    objVal[i, j] = dataTableList[k].Rows[i + iParstedRow][j].ToString();
                                    //objVal[i, j] = gridView[j, i + iParstedRow].Value.ToString();
                                    if (dataTableList[k].Columns[j].ColumnName == "出生日期" &&
                                        dataTableList[k].Rows[i + iParstedRow][j] != DBNull.Value)
                                    {
                                        objVal[i, j] = Convert.ToDateTime(dataTableList[k].Rows[i + iParstedRow][j]).ToShortDateString();
                                    }

                                }
                                System.Windows.Forms.Application.DoEvents();
                            }
                            /*
                                 * 建议使用设置断点研究下哈
                                 * 例如A1到H10的意思是从A到H，第一行到第十行
                                 * 下句很关键，要保证获取workSheet中对应的Range范围
                                 * 下句实际上是得到这样的一个代码语句xlRang = worksheetData.get_Range("A2","H100");
                                 * 注意看实现的过程
                                 * 'A' + iColumnAccount - 1这儿是获取你的最后列，A的数字码为65，大家可以仔细看下是不是得到最后列的字母
                                 * iParstedRow + iCurrSize + 1获取最后行
                                 * 若WHILE第一次循环的话这应该是A2,最后列字母+最后行数字
                                 * iParstedRow + 2要注意，每次循环这个值不一样，他取决于你每次循环RANGE取了多大，也就是iEachSize设置值的大小哦
                                 */
                            xlRang = worksheetData.get_Range("A" + ((int)(iParstedRow + 2)).ToString(), ((char)('A' + iColumnAccount - 1)).ToString() + ((int)(iParstedRow + iCurrSize + 1)).ToString());
                            // 调用Range的Value2属性，把内存中的值赋给Excel
                            xlRang.Value2 = objVal;
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRang);
                            xlRang = null;
                            //已导出的行数=已导出的行数+当前导出行数
                            iParstedRow = iParstedRow + iCurrSize;
                        }
                    }
                    worksheetData.Columns.EntireColumn.AutoFit();
                    workbookData.Saved = true;
                }

                workbookData.SaveAs(strFileName + "", miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                appExcel.Quit();
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(0);

                // 别忘了在结束程序之前恢复你的环境！
                System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI;
                GC.Collect();
                //MessageBox.Show("导出完成！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                appExcel.Quit();

                // 别忘了在结束程序之前恢复你的环境！
                System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI;
                GC.Collect();
                logger.Error(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Checks the worksheet.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="worksheetData">The worksheet data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private static bool CheckWorksheet(Excel.Workbook workbook, string tableName, out Excel.Worksheet worksheetData)
        {
            worksheetData = null;
            bool existSheet = false;
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name == tableName)
                {
                    existSheet = true;
                    worksheetData = sheet;
                    break;
                }
            }
            return existSheet;
        }

        /// <summary>
        /// Gets the data from excel.
        /// </summary>
        /// <param name="strFileName">The file name.</param>
        /// <param name="isHead">if has head.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="strErrorMessage">The error message.</param>
        /// <param name="iRowsIndex">Index of the row.</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetDataFromExcel(string strFileName, bool isHead, string sheetName,
            out string strErrorMessage, int iRowsIndex)
        {
            //检查扩展名
            strErrorMessage = string.Empty;
            if (!strFileName.ToUpper().EndsWith(".XLS"))
            {
                strErrorMessage = "文件类型与系统设定不一致，请核对！";
                return null;
            }

            Excel.Application appExcel = new Excel.Application();
            Excel.Workbook workbookData;
            Excel.Worksheet worksheetData;

            workbookData = appExcel.Workbooks.Open(strFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                     Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //检查sheetname是否存在
            bool IsSheetExist = CheckWorksheet(workbookData, sheetName, out worksheetData);
            if (!IsSheetExist)
            {
                strErrorMessage = "没有找到Sheet " + sheetName;
                return null;
            }

            Excel.Range xlRang = null;
            //总行数
            int iRowCount = worksheetData.UsedRange.Cells.Rows.Count;
            //完成的行数
            int iParstedRow = 0,
                //当前处理行数
                iCurrSize = 0;
            //一次处理个数
            int iEachSize = 1000;
            //列数
            int iColumnAccount = worksheetData.UsedRange.Cells.Columns.Count;
            //数据开始行数
            int iHead = iRowsIndex;

            if (isHead)
                iHead = iRowsIndex + 1;

            DataTable dt = new DataTable();
            //遍历worksheet各列，为DataTable添加列名
            for (int i = 1; i <= iColumnAccount; i++)
            {
                if (isHead)
                    if (worksheetData.Cells[iRowsIndex, i].Value != null)
                    {
                        dt.Columns.Add(worksheetData.Cells[iRowsIndex, i].Value.ToUpper().Trim());
                    }
                    else
                        dt.Columns.Add("Columns" + i.ToString());
            }


            object[,] objVal = new object[iEachSize, iColumnAccount];
            try
            {
                iCurrSize = iEachSize;
                //遍历worksheet每一行数据
                while (iParstedRow < iRowCount)
                {
                    if ((iRowCount - iParstedRow) < iEachSize)
                        iCurrSize = iRowCount - iParstedRow;
                    //获得iCurrSize行的数据
                    xlRang = worksheetData.get_Range("A" + ((int)(iParstedRow + iHead)).ToString(), ((char)('A' + iColumnAccount - 1)).ToString()
                        + (((int)(iParstedRow + iCurrSize + 1)).ToString()));

                    objVal = (object[,])xlRang.Value2;

                    int iLength = objVal.Length / iColumnAccount;
                    //把iCurrSize行的数据添加到DataTable
                    for (int i = 1; i < iLength; i++)
                    {
                        DataRow dr = dt.NewRow();
                        //为每列数据付值
                        for (int j = 1; j <= iColumnAccount; j++)
                        {
                            if (objVal[i, j] != null)
                            {
                                dr[j - 1] = objVal[i, j].ToString();
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    //记录完成的行数
                    iParstedRow = iParstedRow + iCurrSize;

                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRang);
                xlRang = null;

            }
            catch (Exception ex)
            {
                appExcel.Quit();
                strErrorMessage = ex.Message;
                return null;
            }

            appExcel.Quit();

            return dt;

        }


    }
}
