using DirectoryFileReader.Interfaces.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace DirectoryFileReader.Services.Excel
{
    /// <summary>
    /// Summary description for ExcelReader
    /// </summary>
    internal class ExcelReader : IExcelReader
    {
        public DataSet ReadExcelFile(ExcelReaderRequest excelReaderRequest)
        {
            try
            {
                String connectionString = string.Empty;

                if (excelReaderRequest.FileExtensiion.ToUpper().CompareTo(".XLS") == 0)
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelReaderRequest.FilePath + @";Extended Properties=""Excel 8.0;HDR=YES;""";
                }
                else if (excelReaderRequest.FileExtensiion.ToUpper().CompareTo(".XLSX") == 0)
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelReaderRequest.FilePath + @";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""";
                }
#pragma warning disable CA1416 // Validate platform compatibility
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand selectCommand = new OleDbCommand();


                connection.Open();

                DataTable worksheets;
                worksheets = connection.GetSchema("Tables");

                DataRow dr1 = worksheets.Rows[0];
                Object[] rowitems = dr1.ItemArray;
                String item1 = "";
                item1 = Convert.ToString(rowitems[2]);

                DataTable columns;
                String[] restrictions = { null, null, item1, null };
                columns = connection.GetSchema("Columns", restrictions);
                //columns = connection.GetSchema("Columns");
                int fieldcount = 0;
                fieldcount = columns.Rows.Count;
                int[] fieldnoarr = new int[fieldcount];
                String[] fieldarr = new String[fieldcount];
                String selectstr = "select ";

                DataRow drc; Object[] temparr;

                for (int jj = 0; jj <= fieldcount - 1; jj++)
                {
                    //DataRow drc = columns.Rows[jj];
                    drc = columns.Rows[jj];
                    //Object[] temparr = drc.ItemArray;
                    temparr = drc.ItemArray;
                    fieldnoarr[jj] = Convert.ToInt32(temparr[6]);
                    fieldarr[fieldnoarr[jj] - 1] = (String)temparr[3];
                }
                for (int jj = 0; jj <= fieldcount - 1; jj++)
                {
                    if (jj == 0)
                        selectstr += "[" + fieldarr[jj] + "]";
                    else
                        selectstr += "," + "[" + fieldarr[jj] + "]";
                }
                selectstr += " FROM [" + item1 + "]";
                selectCommand.CommandText = selectstr;

                selectCommand.Connection = connection;
                adapter.SelectCommand = selectCommand;

                DataSet transrows = new DataSet();

                adapter.Fill(transrows, item1);

                return transrows;

            }
            catch (Exception)
            {
                return null;
            }


        }

#pragma warning restore CA1416 // Validate platform compatibility
    }
}
