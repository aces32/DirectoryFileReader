using DirectoryFileReader.Interfaces.FilesTransfer;
using System.IO;

namespace DirectoryFileReader.Services.FilesTransfer
{
    internal class MoveFiles : IMoveFiles
    {

        public bool MoveFile(string initialfile, string newFileDirectory)
        {
            if (!(File.Exists(newFileDirectory)))
            {
                File.Move(initialfile, newFileDirectory);
            }

            return true;
        }

    }
}
