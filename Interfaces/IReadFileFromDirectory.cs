using DirectoryFileReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileReader.Interfaces
{
    public interface IReadFileFromDirectory
    {
        ReadFileResponse ProcessNewFiles(string FolderPath);

        Task<ReadFileResponse> ProcessNewFilesAsync(string FolderPath);
    }
}
