using DirectoryFileReader.Interfaces.FilesTransfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DirectoryFileReader.Services.FilesTransfer
{
    internal class ValidateFile : IValidateFile
    {
        public bool DirectoryExistorCreated(string directory)
        {
            if (!(Directory.Exists(directory)))
            {
                Directory.CreateDirectory(directory);
            }

            return true;
        }

        public bool ValidFileType(string fileExtension)
        {
            string availableReadExtensions = ".XLS,.XLSX,.CSV";
            if (availableReadExtensions.Split(',').Contains(fileExtension.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
