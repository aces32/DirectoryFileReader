using DirectoryFileReader.Interfaces.FilesTransfer;
using System.IO;
using System.Threading.Tasks;

namespace DirectoryFileReader.Services.FilesTransfer
{
    internal class CopyFiles : ICopyFiles
    {
        public bool CopyFile(string initialfile, string newFileDirectory)
        {
            if (!(File.Exists(newFileDirectory)))
            {
                File.Copy(initialfile, newFileDirectory);
            }
            return true;
        }

        public async Task CopyFileAsync(string sourceFile, string destinationFile)
        {
            using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
            using (var destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
                await sourceStream.CopyToAsync(destinationStream);
        }
    }
}
