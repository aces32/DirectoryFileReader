namespace DirectoryFileReader.Models
{
    public class FileInformationResponse
    {
        public string Filename { get; set; }
        public string Filenamext { get; set; }
        public string Extension { get; set; }
        public string Processedpath { get; set; }
        public string ProcessedpathFile { get; set; }
        public string UnProcessedpath { get; set; }
        public string UnProcessedpathFile { get; set; }
        public string Splitedpath { get; set; }
        public string SplitedpathFile { get; set; }
    }
}