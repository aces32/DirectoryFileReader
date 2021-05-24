using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader.Interfaces.CSVReader
{
    public interface ICSVReader
    {
        DataSet ConvertCSVtoDataTable(string strFilePath);

        Task<DataSet> ConvertCSVtoDataTableAsync(string strFilePath);
    }
}
