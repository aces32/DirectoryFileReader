using DirectoryFileReader.Interfaces;
using DirectoryFileReader.Interfaces.CSVReader;
using DirectoryFileReader.Interfaces.Excel;
using DirectoryFileReader.Interfaces.FilesTransfer;
using DirectoryFileReader.Services.CSV;
using DirectoryFileReader.Services.Excel;
using DirectoryFileReader.Services.FilesTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Services
{
    internal class UnitOfWork : IUnitOfWork
    {

        private ICopyFiles copyFiles;
        public ICopyFiles CopyFiles
        {
            get
            {
                if (copyFiles == null)
                {
                    copyFiles = new CopyFiles();
                }

                return copyFiles;
            }
        }

        private ICSVReader cSVReader;
        public ICSVReader CSVReader
        {
            get
            {
                if (cSVReader == null)
                {
                    cSVReader = new CSVReader();
                }

                return cSVReader;
            }
        }

        private IMoveFiles moveFiles;
        public IMoveFiles MoveFiles
        {
            get
            {
                if (moveFiles == null)
                {
                    moveFiles = new MoveFiles();
                }

                return moveFiles;
            }
        }
        private IExcelReader excelReader;
        public IExcelReader ExcelReader
        {
            get
            {
                if (excelReader == null)
                {
                    excelReader = new ExcelReader();
                }

                return excelReader;
            }
        }

        private IFileData fileData;
        public IFileData FileData
        {
            get
            {
                if (fileData == null)
                {
                    fileData = new FileData(ExcelReader, CSVReader);
                }

                return fileData;
            }
        }

        private IFileInformation fileInformation;
        public IFileInformation FileInformation
        {
            get
            {
                if (fileInformation == null)
                {
                    fileInformation = new FileInformation();
                }

                return fileInformation;
            }
        }


        private IValidateFile validateFile;
        public IValidateFile ValidateFile
        {
            get
            {
                if (validateFile == null)
                {
                    validateFile = new ValidateFile();
                }

                return validateFile;
            }
        }

    }
}
