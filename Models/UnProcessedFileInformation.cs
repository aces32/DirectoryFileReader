using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Models
{
    public class UnProcessedFileInformation
    {
        /// <summary>
        /// Unprocessed File Name
        /// </summary>
        public string UnProcessedFileName { get; set; }

        /// <summary>
        /// Contents of processed File
        /// </summary>
        public string UnProccessedFileError { get; set; }
    }
}
