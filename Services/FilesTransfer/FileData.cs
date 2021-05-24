using DirectoryFileReader.Interfaces;
using DirectoryFileReader.Interfaces.CSVReader;
using DirectoryFileReader.Interfaces.Excel;
using DirectoryFileReader.Interfaces.FilesTransfer;
using DirectoryFileReader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader.Services.FilesTransfer
{
    internal class FileData : IFileData
    {
        private readonly IExcelReader excelReader;
        private readonly ICSVReader cSVReader;

        internal FileData(IExcelReader excelReader, ICSVReader cSVReader)
        {
            this.excelReader = excelReader;
            this.cSVReader = cSVReader;
        }
        public DataSet ReadFileData(FileInformationResponse fileInfo)
        {
            DataSet excelOrCsvData = new DataSet();

            if (fileInfo.Extension.ToUpper().CompareTo(".CSV") == 0)
            {
                excelOrCsvData = cSVReader.ConvertCSVtoDataTable(fileInfo.SplitedpathFile);
            }
            else
            {
                //Read Excel File
                excelOrCsvData = excelReader.ReadExcelFile(new ExcelReaderRequest
                {
                    FileExtensiion = fileInfo.Extension,
                    FilePath = fileInfo.SplitedpathFile
                });
            }

            return excelOrCsvData;
        }

        public async Task<DataSet> ReadFileDataAsync(FileInformationResponse fileInfo)
        {
            DataSet excelOrCsvData = new DataSet();

            if (fileInfo.Extension.ToUpper().CompareTo(".CSV") == 0)
            {
                excelOrCsvData = await cSVReader.ConvertCSVtoDataTableAsync(fileInfo.SplitedpathFile);
            }
            else
            {
                //Read Excel File
                excelOrCsvData = await Task.Run(() =>
                {
                    return excelReader.ReadExcelFile(new ExcelReaderRequest
                    {
                        FileExtensiion = fileInfo.Extension,
                        FilePath = fileInfo.SplitedpathFile
                    });
                });
            }

            return excelOrCsvData;
        }
    }
}
