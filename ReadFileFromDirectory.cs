using DirectoryFileReader.Interfaces;
using DirectoryFileReader.Models;
using DirectoryFileReader.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace DirectoryFileReader
{
    public class ReadFileFromDirectory : IReadFileFromDirectory
    {
        private readonly IUnitOfWork unitOfWork;

        public ReadFileFromDirectory()
        {
            this.unitOfWork = (IUnitOfWork)Activator
                            .CreateInstance(typeof(UnitOfWork));
        }

        public ReadFileResponse ProcessNewFiles(string folderPath)
        {
            try
            {
                var myFiles = Directory.GetFiles(folderPath, "*", SearchOption.TopDirectoryOnly);
                if (myFiles.Length != 0)
                {
                    string fileData = string.Empty;
                    int fileProcessedCount = 0;
                    int fileUnProcessedCount = 0;
                    List<ProcessFileInformation> processFileInformation = new List<ProcessFileInformation>();
                    List<UnProcessedFileInformation> unProcessedFileInformation = new List<UnProcessedFileInformation>();
                    string day = DateTime.Now.ToString("dd-MM-yy", CultureInfo.CreateSpecificCulture("en-GB"));

                    foreach (string file in myFiles)
                    {
                        var fileInfo = unitOfWork.FileInformation.GetFileInformation(new FileInformationRequest
                        {
                            Day = day,
                            File = file,
                            File_path = folderPath
                        });

                        if (unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.Splitedpath) &&
                            unitOfWork.CopyFiles.CopyFile(file, fileInfo.SplitedpathFile)) // Move file to a specific directory for Processing
                        {
                            if (!unitOfWork.ValidateFile.ValidFileType(fileInfo.Extension))
                            {
                                unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
                                unProcessedFileInformation.Add(new UnProcessedFileInformation
                                {
                                    UnProccessedFileError = "File type in the specified directory must be .XLS,.XLSX,.CSV",
                                    UnProcessedFileName = fileInfo.Filename
                                });
                                fileUnProcessedCount++;
                                continue;
                            }


                            var excelOrCsvData = unitOfWork.FileData.ReadFileData(fileInfo);
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
                                    unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.Processedpath);
                                    unitOfWork.MoveFiles.MoveFile(file, fileInfo.ProcessedpathFile);
                                    fileProcessedCount++;
                                }
                                else
                                {
                                    unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                    unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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
                                unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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
                            unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                            unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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


        public async Task<ReadFileResponse> ProcessNewFilesAsync(string FolderPath)
        {
            try
            {
                var myFiles = Directory.GetFiles(FolderPath, "*", SearchOption.TopDirectoryOnly);
                if (myFiles.Length != 0)
                {
                    string fileData = string.Empty;
                    int fileProcessedCount = 0;
                    int fileUnProcessedCount = 0;
                    List<ProcessFileInformation> processFileInformation = new List<ProcessFileInformation>();
                    List<UnProcessedFileInformation> unProcessedFileInformation = new List<UnProcessedFileInformation>();
                    string day = DateTime.Now.ToString("dd-MM-yy", CultureInfo.CreateSpecificCulture("en-GB"));

                    foreach (string file in myFiles)
                    {
                        var fileInfo = unitOfWork.FileInformation.GetFileInformation(new FileInformationRequest
                        {
                            Day = day,
                            File = file,
                            File_path = FolderPath
                        });

                        if (unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.Splitedpath) &&
                            unitOfWork.CopyFiles.CopyFile(file, fileInfo.SplitedpathFile)) // Move file to a specific directory for Processing
                        {
                            if (!unitOfWork.ValidateFile.ValidFileType(fileInfo.Extension))
                            {
                                unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
                                unProcessedFileInformation.Add(new UnProcessedFileInformation
                                {
                                    UnProccessedFileError = "File type in the specified directory must be .XLS,.XLSX,.CSV",
                                    UnProcessedFileName = fileInfo.Filename
                                });
                                fileUnProcessedCount++;
                                continue;
                            }


                            var excelOrCsvData = await unitOfWork.FileData.ReadFileDataAsync(fileInfo); ;
                            if (excelOrCsvData.Tables.Count > 0)
                            {
                                if (excelOrCsvData.Tables[0].Rows.Count > 0)
                                {
                                    // successfully processed files
                                    fileData = await Task.Run(() => { return JsonConvert.SerializeObject(excelOrCsvData.Tables[0]); });
                                    //fileData = JsonConvert.SerializeObject(excelOrCsvData.Tables[0]);
                                    processFileInformation.Add(new ProcessFileInformation
                                    {
                                        ProcessedFileData = fileData,
                                        ProcessedFileName = fileInfo.Filename
                                    });
                                    unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.Processedpath);
                                    unitOfWork.MoveFiles.MoveFile(file, fileInfo.ProcessedpathFile);
                                    fileProcessedCount++;
                                }
                                else
                                {
                                    unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                    unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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
                                unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                                unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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
                            unitOfWork.ValidateFile.DirectoryExistorCreated(fileInfo.UnProcessedpath);
                            unitOfWork.MoveFiles.MoveFile(file, fileInfo.UnProcessedpathFile);
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
