using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace DirectoryFileReader.CSV
{
    public class CSVReader
    {
        public DataSet ConvertCSVtoDataTable(string strFilePath)
        {
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
;
                }
                dataSet.Tables.Add(dt);
            }

            return dataSet;
        }
    }
}
