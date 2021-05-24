using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Interfaces.FilesTransfer
{
    internal interface IMoveFiles
    {
        bool MoveFile(string initialfile, string newFileDirectory);
    }
}
