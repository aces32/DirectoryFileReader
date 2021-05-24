using DirectoryFileReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Interfaces.FilesTransfer
{
    internal interface IFileInformation
    {
        FileInformationResponse GetFileInformation(FileInformationRequest fileInformation);
    }
}
