# DirectoryFileReader
Reads Excel or CSV Files from a specified directory and returns a validated json result that can be easily inserted into a datatable  


Take the following step
ReadFileFromDirectory readFileFromDirectory = new ReadFileFromDirectory();

Reads file from directory specified
var read = readFileFromDirectory.ProcessNewFiles("C//local");

Returns a result of read

{
"ProcessedFileCount": 1,
"UnProcessedFileCount": 0,
"ProcessFileInformation": [
{
"ProcessedFileName": "Test",
"ProcessedFileData": "[{"TestNAme":"Kuploader","NewLimit":20000.0}]"
}
],
"UnProcessFileInformation": [],
"AllFileSuccessFullyRead": {
"AllFileWasRead": true,
"AllFileWasReadResponse": "Success"
}
}

Where
processfilecount = total amount of processed file
UnProcessedFileCount = total amount of unprocessed file
ProcessedFileName = name of each processed file on the directory
ProcessedFileData = data of each processed file on the directory
UnProcessedFileName = name of each file on the directory
UnProccessedFileError = reason File was not processed
AllFileWasRead = validate all file was read

Note:- only xls, xlsx and csv files can be currently read

