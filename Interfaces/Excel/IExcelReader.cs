using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DirectoryFileReader.Interfaces.Excel
{
    internal interface IExcelReader
    {
        DataSet ReadExcelFile(ExcelReaderRequest excelReaderRequest);
    }
}
