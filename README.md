# DirectoryFileReader
Reads Excel or CSV Files from a specified directory and returns a validated json result that can be easily inserted into a datatable  

## Usage
Take the following steps:
1. Initialize the class

```
ReadFileFromDirectory readFileFromDirectory = new ReadFileFromDirectory();
```
2. Read file from specified directory
```
var read = readFileFromDirectory.ProcessNewFiles("C//local");

You can also use the asynchronous method

var read = await readFileFromDirectory.ProcessNewFilesAsync("C//local");

```
3. Returns a result of read
```
{
  "ProcessedFileCount": 2,
  "UnProcessedFileCount": 1,
  "ProcessFileInformation": [
    {
      "ProcessedFileName": "LimitTemplate - Copy",
      "ProcessedFileData": "[{\"User_Name\":\"DFDGR\",\"NewFXLimit\":1000.0}]"
    },
    {
      "ProcessedFileName": "LimitTemplate2",
      "ProcessedFileData": "[{\"User_Name\":\"Kuploader\",\"NewFXLimit\":20000.0},{\"User_Name\":\"DFDG\",\"NewFXLimit\":1000.0}]"
    }
  ],
  "UnProcessFileInformation": [
    {
      "UnProcessedFileName": "LimitTemplate2 - Copy",
      "UnProccessedFileError": "No data exist for the specified file name"
    }
  ],
  "AllFileSuccessFullyRead": {
    "AllFileWasRead": true,
    "AllFileWasReadResponse": "Success"
  }
}
```

Where:
- processfilecount = total amount of processed file
- UnProcessedFileCount = total amount of unprocessed file
- ProcessedFileName = name of each processed file on the directory
- ProcessedFileData = data of each processed file on the directory
- UnProcessedFileName = name of each file on the directory
- UnProccessedFileError = reason File was not processed
- AllFileWasRead = validate all file was read

## Note
Only **xls**, **xlsx**, and **csv** files are currently supported.
```
Also ensure Microsoft Access Database Engine 2010 Redistributable is installed on your container 
URL - https://www.microsoft.com/en-us/download/Confirmation.aspx?ID=13255

