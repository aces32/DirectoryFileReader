using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Models
{
    public partial class ProcessFileInformation
    {
        /// <summary>
        /// Processed FileName
        /// </summary>
        public string ProcessedFileName { get; set; }

        /// <summary>
        /// Contents of processed File
        /// </summary>
        public string ProcessedFileData { get; set; }
    }
}
