using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace LDG.HopuGoods.Utility
{
    /// <summary>
    /// 需要安装DonetCore.Npoi包
    /// </summary>
    public class NPOIHelper
    {
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="file">导入文件</param>
        /// <returns>List<T></returns>
        public static List<T> InputExcel<T>(IFormFile file) where T : class, new()
        {
            List<T> list = new List<T> { };

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            IWorkbook workbook = new XSSFWorkbook(ms);
            ISheet sheet = workbook.GetSheetAt(0);
            IRow cellNum = sheet.GetRow(0);
            var propertys = typeof(T).GetProperties();
            string value = null;
            int num = cellNum.LastCellNum;

            for (int i = 2; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                var obj = new T();
                for (int j = 2; j < num; j++)
                {
                    value = row.GetCell(j).ToString();
                    string str = (propertys[j].PropertyType).FullName;//反射获取属性类型
                    if (str == "System.String")
                    {
                        propertys[j].SetValue(obj, value, null);
                    }
                    else if (str == "System.DateTime")
                    {
                        DateTime pdt = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                        propertys[j].SetValue(obj, pdt, null);
                    }
                    else if (str == "System.Boolean")
                    {
                        bool pb = Convert.ToBoolean(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else if (str == "System.Int16")
                    {
                        short pi16 = Convert.ToInt16(value);
                        propertys[j].SetValue(obj, pi16, null);
                    }
                    else if (str == "System.Int32")
                    {
                        int pi32 = Convert.ToInt32(value);
                        propertys[j].SetValue(obj, pi32, null);
                    }
                    else if (str == "System.Int64")
                    {
                        long pi64 = Convert.ToInt64(value);
                        propertys[j].SetValue(obj, pi64, null);
                    }
                    else if (str == "System.Byte")
                    {
                        byte pb = Convert.ToByte(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else if (str == "System.Double")
                    {
                        double pb = Convert.ToDouble(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else if (str == "System.Decimal")
                    {
                        decimal pb = Convert.ToDecimal(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else
                    {
                        propertys[j].SetValue(obj, null, null);
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 将文件流读取到DataTable数据表中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadStreamToDataTable(IFormFile file, string sheetName = null, bool isFirstRowColumn = true)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);


            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(ms);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString().Trim();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        ///导出，控制器 return File(buffer, "application/ms-excel", "list.xlsx"); 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableTitle"></param>
        /// <returns></returns>
        public static byte[] Output(DataTable dataTable, string[] tableTitle)
        {
            NPOI.SS.UserModel.IWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow rows = null;
            for (int i = 1; i <= dataTable.Rows.Count; i++)
            {
                //创建表头
                if (i - 1 == 0)
                {
                    Title = sheet.CreateRow(0);
                    for (int k = 1; k < tableTitle.Length + 1; k++)
                    {
                        Title.CreateCell(0).SetCellValue("序号");
                        Title.CreateCell(k).SetCellValue(tableTitle[k - 1]);
                    }
                    continue;
                }
                else
                {
                    rows = sheet.CreateRow(i - 1);
                    for (int j = 1; j <= dataTable.Columns.Count; j++)
                    {
                        rows.CreateCell(0).SetCellValue(i - 1);
                        rows.CreateCell(j).SetCellValue(dataTable.Rows[i - 1][j - 1].ToString());
                    }
                }
            }

            byte[] buffer = new byte[1024 * 5];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }
            return buffer;
        }



        /// <summary>
        /// 导出，控制器 return File(buffer, "application/ms-excel", "list.xlsx"); 
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static byte[] OutputExcel<T>(List<T> entitys, string[] title) where T : class, new()
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow rows = null;
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            for (int i = 0; i <= entitys.Count; i++)
            {
                if (i == 0)
                {
                    Title = sheet.CreateRow(0);
                    for (int k = 1; k < title.Length + 1; k++)
                    {
                        Title.CreateCell(0).SetCellValue("序号");
                        Title.CreateCell(k).SetCellValue(title[k - 1]);
                    }

                    continue;
                }
                else
                {
                    rows = sheet.CreateRow(i);
                    object entity = entitys[i - 1];
                    for (int j = 1; j <= entityProperties.Length; j++)
                    {
                        object[] entityValues = new object[entityProperties.Length];
                        entityValues[j - 1] = entityProperties[j - 1].GetValue(entity);
                        rows.CreateCell(0).SetCellValue(i);
                        rows.CreateCell(j).SetCellValue(entityValues[j - 1] == null ? "" : entityValues[j - 1].ToString());
                    }
                }
            }

            byte[] buffer = new byte[1024 * 2];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }

            return buffer;
        }
    }
}
