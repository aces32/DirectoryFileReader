using DirectoryFileReader.CSV;
using DirectoryFileReader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader
{
    public class MaxAdvanceFile
    {

        public ReadFileResponse ProcessNewFiles(string FolderPath)
        {
            try
            {
                string day = DateTime.Now.ToString("dd-MM-yy", CultureInfo.CreateSpecificCulture("en-GB"));
                var myFiles = Directory.GetFiles(FolderPath, "*", SearchOption.TopDirectoryOnly);
                if (myFiles.Length != 0)
                {
                    string fileData = string.Empty;
                    int fileProcessedCount = 0;
                    int fileUnProcessedCount = 0;
                    List<ProcessFileInformation> processFileInformation = new List<ProcessFileInformation>();
                    List<UnProcessedFileInformation> unProcessedFileInformation = new List<UnProcessedFileInformation>();

                    foreach (string file in myFiles)
                    {
                        var fileInfo = FileInformation.GetFileInformation(new FileInformationRequest
                        {
                            Day = day,
                            File = file,
                            File_path = FolderPath
                        });

                        if (FileInformation.ValidateAndCopyFile(fileInfo.Splitedpath, file , fileInfo.SplitedpathFile)) // Move file to a specific directory for Processing
                        {
                            if (FileInformation.ValidFileType(fileInfo.Extension))
                            {
                                unProcessedFileInformation.Add(new UnProcessedFileInformation
                                {
                                    UnProccessedFileError = "File type in the specified directory must be .XLS,.XLSX,.CSV",
                                    UnProcessedFileName = fileInfo.Filename
                                });
                                fileUnProcessedCount++;
                                continue;
                            }


                            var excelOrCsvData = FileInformation.ReadFileData(fileInfo);
                            if (excelOrCsvData.Tables.Count > 0)
                            {
                                if (excelOrCsvData.Tables[0].Rows.Count > 0)
                                {
                                    // successfully processed files
                                    fileData = JsonConvert.SerializeObject(excelOrCsvData.Tables[0]);
                                    processFileInformation.Add(new ProcessFileInformation
                                    {
                                        ProcessedFileData = fileData,
                                        ProcessedFileName = fileInfo.Filename
                                    });
                                    FileInformation.ValidateAndMoveFile(fileInfo.Processedpath, file, fileInfo.ProcessedpathFile);
                                    fileProcessedCount++;
                                }
                                else
                                {
                                    FileInformation.ValidateAndMoveFile(fileInfo.UnProcessedpath, file, fileInfo.UnProcessedpathFile);
                                    unProcessedFileInformation.Add(new UnProcessedFileInformation 
                                    { 
                                        UnProccessedFileError = "No data exist for the specified file name",
                                        UnProcessedFileName = fileInfo.Filename
                                    });
                                    fileUnProcessedCount++;
                                }
                            }
                            else
                            {
                                FileInformation.ValidateAndMoveFile(fileInfo.UnProcessedpath, file, fileInfo.UnProcessedpathFile);
                                unProcessedFileInformation.Add(new UnProcessedFileInformation
                                {
                                    UnProccessedFileError = "No record exist for the specified file name (File has empty record)",
                                    UnProcessedFileName = fileInfo.Filename
                                });
                                fileUnProcessedCount++;
                            }
                        }
                        else
                        {
                            FileInformation.ValidateAndMoveFile(fileInfo.UnProcessedpath, file, fileInfo.UnProcessedpathFile);
                            unProcessedFileInformation.Add(new UnProcessedFileInformation
                            {
                                UnProccessedFileError = "oops, An error occurred dont tell anyone , Just contact BuariSulaimon@gmail.com",
                                UnProcessedFileName = fileInfo.Filename
                            });
                            fileUnProcessedCount++;
                        }
                    }

                    return new ReadFileResponse
                    {
                        ProcessedFileCount = fileProcessedCount,
                        UnProcessedFileCount = fileUnProcessedCount,
                        ProcessFileInformation = processFileInformation,
                        UnProcessFileInformation = unProcessedFileInformation,
                        AllFileSuccessFullyRead = new AllFileSuccessFullyRead
                        {
                            AllFileWasRead = true,
                            AllFileWasReadResponse = "Success"
                        }
                    };


                }
                else
                {
                    return new ReadFileResponse
                    {
                        AllFileSuccessFullyRead = new AllFileSuccessFullyRead
                        {
                            AllFileWasRead = false,
                            AllFileWasReadResponse = "No file exist in the specified directory"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ReadFileResponse
                {
                    AllFileSuccessFullyRead = new AllFileSuccessFullyRead
                    {
                        AllFileWasRead = false,
                        AllFileWasReadResponse = $"oops, An error occurred dont tell anyone , Just contact BuariSulaimon@gmail.com {ex}"
                    }
                };
            }

        }

    }
}
