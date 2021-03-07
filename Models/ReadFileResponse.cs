using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryFileReader.Models
{
    public class ReadFileResponse
    {
        /// <summary>
        /// Count of total processed File
        /// </summary>
        public int ProcessedFileCount { get; set; }
        /// <summary>
        /// Count of total Unprocessed File
        /// </summary>
        public int UnProcessedFileCount { get; set; }
        /// <summary>
        /// List of Processed File Information
        /// </summary>
        public List<ProcessFileInformation> ProcessFileInformation { get; set; }
        /// <summary>
        /// List of UnProcessed file information
        /// </summary>
        public  List<UnProcessedFileInformation> UnProcessFileInformation { get; set; }
        /// <summary>
        /// Final check that all files was successfully read
        /// </summary>
        public AllFileSuccessFullyRead  AllFileSuccessFullyRead { get; set; }
    }
}
