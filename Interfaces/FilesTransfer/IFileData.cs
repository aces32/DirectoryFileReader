using DirectoryFileReader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader.Interfaces.FilesTransfer
{
    internal interface IFileData
    {
        DataSet ReadFileData(FileInformationResponse fileInfo);

        Task<DataSet> ReadFileDataAsync(FileInformationResponse fileInfo);
    }
}
