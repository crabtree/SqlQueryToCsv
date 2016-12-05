using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SQC.Application.Infrastructure
{
    public class CsvData
    {
        public IEnumerable<string> Headers { get; private set; }
        public IEnumerable<dynamic> Data { get; private set; }

        public CsvData(string[] headers, IEnumerable<dynamic> data)
        {
            Headers = headers;
            Data = data;
        }

        public void ToFile(string outputFilePath)
        {

            using (var fileStream = File.OpenWrite(outputFilePath))
            using (var streamWriter = new StreamWriter(fileStream))
            using (var csvWriter = new CsvHelper.CsvWriter(streamWriter))
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.Configuration.Encoding = System.Text.Encoding.UTF8;
                csvWriter.Configuration.HasHeaderRecord = true;

                foreach(var header in Headers)
                {
                    csvWriter.WriteField(header);
                }

                csvWriter.NextRecord();

                foreach(var row in Data)
                {
                    var rowAsDict = row as IDictionary<string, object>;
                    foreach(var field in Headers)
                    {
                        object value = null;
                        if(!rowAsDict.TryGetValue(field, out value) || value == null) value = string.Empty;

                        csvWriter.WriteField(value);
                    }

                    csvWriter.NextRecord();
                }
            }
        }

        public static CsvData FromSqlData(IEnumerable<dynamic> sqlData)
        {
            var headers = ProduceHeadersFromSqlData(sqlData);
            return new CsvData(headers, sqlData);
        }

        private static string[] ProduceHeadersFromSqlData(IEnumerable<dynamic> sqlData)
        {
            var row = sqlData.First() as IDictionary<string, object>;
            return row.Keys as string[];
        }
    }
}
