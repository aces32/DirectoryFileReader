using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader.Interfaces.FilesTransfer
{
    internal interface ICopyFiles
    {
        bool CopyFile(string initialfile, string newFileDirectory);
        Task CopyFileAsync(string sourceFile, string destinationFile);
    }
}
