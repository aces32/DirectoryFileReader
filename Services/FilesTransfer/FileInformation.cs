using DirectoryFileReader.Interfaces.FilesTransfer;
using DirectoryFileReader.Models;
using System.IO;

namespace DirectoryFileReader.Services.FilesTransfer
{
    internal class FileInformation : IFileInformation
    {
        public FileInformationResponse GetFileInformation(FileInformationRequest fileInformation)
        {
            var filename = Path.GetFileNameWithoutExtension(fileInformation.File);
            var filenamext = Path.GetFileName(fileInformation.File);
            var ext = Path.GetExtension(fileInformation.File);
            var processedpath = fileInformation.File_path + "\\processed-files\\" + fileInformation.Day;
            var procesedpath1 = fileInformation.File_path + "\\processed-files\\" + fileInformation.Day + "\\" + filenamext;
            var unProcessedpath = fileInformation.File_path + "\\Unprocessed-files\\" + fileInformation.Day;
            var unProcesedpath1 = fileInformation.File_path + "\\Unprocessed-files\\" + fileInformation.Day + "\\" + filenamext;
            var origPath = fileInformation.File_path + "\\" + filenamext;
            var splitedpath = "" + fileInformation.File_path + "\\Splitfiles\\" + fileInformation.Day + ""; //the local directory for splitted files
            var path = "" + splitedpath + "\\" + filename + filenamext;

            return new FileInformationResponse
            {
                Filename = filename,
                Filenamext = filenamext,
                Extension = ext,
                Processedpath = processedpath,
                ProcessedpathFile = procesedpath1,
                UnProcessedpath = unProcessedpath,
                UnProcessedpathFile = unProcesedpath1,
                Splitedpath = splitedpath,
                SplitedpathFile = path,
            };
        }
    }
}
