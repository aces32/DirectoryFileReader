using DirectoryFileReader.CSV;
using DirectoryFileReader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader
{
    internal static class FileInformation
    {
        internal static FileInformationResponse GetFileInformation(FileInformationRequest fileInformation)
        {
            var filename = Path.GetFileNameWithoutExtension(fileInformation.File);
            var filenamext = Path.GetFileName(fileInformation.File);
            var ext = Path.GetExtension(fileInformation.File);
            var processedpath = fileInformation.File_path + "\\processed-files\\" + fileInformation.Day;
            var procesedpath1 = fileInformation.File_path + "\\processed-files\\" + fileInformation.Day + "\\" + filenamext;
            var unProcessedpath = fileInformation.File_path + "\\Unprocessed-files\\" + fileInformation.Day;
            var unProcesedpath1 = fileInformation.File_path + "\\Unprocessed-files\\" + fileInformation.Day + "\\" + filenamext;
            var origPath = fileInformation.File_path + "\\" + filenamext;
            var splitedpath = "" + fileInformation.File_path + "\\Splitfiles\\" + fileInformation.Day + ""; //the local directory for splitted files
            var path = "" + splitedpath + "\\" + filename + filenamext;

            return new FileInformationResponse
            {
                Filename = filename,
                Filenamext = filenamext,
                Extension = ext,
                Processedpath = processedpath,
                ProcessedpathFile = procesedpath1,
                UnProcessedpath = unProcessedpath,
                UnProcessedpathFile = unProcesedpath1,
                Splitedpath = splitedpath,
                SplitedpathFile = path,
            };
        }


        internal static bool ValidateAndCopyFile(string directory, string initialfile, string newFileDirectory)
        {
            if (!(Directory.Exists(directory)))
            {
                Directory.CreateDirectory(directory);
                File.Copy(initialfile, newFileDirectory);
            }
            else
            {
                if (!(File.Exists(newFileDirectory)))
                {
                    File.Copy(initialfile, newFileDirectory);
                }
            }

            return true;
        }

        public static async Task CopyFileAsync(string sourceFile, string destinationFile)
        {
            using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
            using (var destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
                await sourceStream.CopyToAsync(destinationStream);
        }

        //public static async Task MoveFileAsync(string sourceFile, string destinationFile)
        //{
        //    using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
        //    using (var destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
        //        await sourceStream.c(destinationStream);
        //}

        internal static bool ValidateAndMoveFile(string directory, string initialfile, string newFileDirectory)
        {
            if (!(Directory.Exists(directory)))
            {
                Directory.CreateDirectory(directory);
                File.Copy(initialfile, newFileDirectory);
            }
            else
            {
                if (!(File.Exists(newFileDirectory)))
                {
                    File.Move(initialfile, newFileDirectory);
                }
            }

            return true;
        }

        internal static T ValidateAndGetExcelRecord<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //internal static List<DataRow> ValidateAndGetExcelRecord(DataTable dt)
        //{
        //    IEnumerable<DataRow> sequence = dt.AsEnumerable();
        //    return sequence.ToList();
        //}

        internal static bool ValidFileType(string fileExtension)
        {
            string availableReadExtensions = ".XLS,.XLSX,.CSV";
            if (availableReadExtensions.Split(',').Contains(fileExtension))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static DataSet ReadFileData(FileInformationResponse fileInfo)
        {
            DataSet excelOrCsvData = new DataSet();

            if (fileInfo.Extension.ToUpper().CompareTo(".CSV") == 0)
            {
                CSVReader cSVReader = new CSVReader();
                excelOrCsvData = cSVReader.ConvertCSVtoDataTable(fileInfo.SplitedpathFile);
            }
            else
            {
                //Read Excel File
                ExcelReader excelReader = new ExcelReader();
                excelOrCsvData = excelReader.ReadExcelFile(new ExcelReaderRequest
                {
                    FileExtensiion = fileInfo.Extension,
                    FilePath = fileInfo.SplitedpathFile
                });
            }

            return excelOrCsvData;
        }


        internal static async Task<DataSet> ReadFileDataAsync(FileInformationResponse fileInfo)
        {
            DataSet excelOrCsvData = new DataSet();

            if (fileInfo.Extension.ToUpper().CompareTo(".CSV") == 0)
            {
                CSVReader cSVReader = new CSVReader();
                excelOrCsvData = await cSVReader.ConvertCSVtoDataTableAsync(fileInfo.SplitedpathFile);
            }
            else
            {
                //Read Excel File
                ExcelReader excelReader = new ExcelReader();
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
