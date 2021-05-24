using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Interfaces.FilesTransfer
{
    internal interface IValidateFile
    {
        bool DirectoryExistorCreated(string directory);
        bool ValidFileType(string fileExtension);
    }
}
