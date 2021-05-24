using DirectoryFileReader.Interfaces.CSVReader;
using DirectoryFileReader.Interfaces.Excel;
using DirectoryFileReader.Interfaces.FilesTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Interfaces
{
    internal interface IUnitOfWork
    {
        ICopyFiles CopyFiles { get; }
        ICSVReader CSVReader { get; }
        IExcelReader ExcelReader { get; }
        IFileInformation FileInformation { get; }
        IFileData FileData { get; }
        IMoveFiles MoveFiles { get; }      
        IValidateFile ValidateFile { get; }
    }
}
