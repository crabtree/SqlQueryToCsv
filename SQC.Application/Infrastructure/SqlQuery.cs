using System;
using System.Collections.Generic;
using System.IO;

namespace SQC.Application.Infrastructure
{
    public class SqlQuery
    {
        public string Query { get; private set; }  

        public SqlQuery(string query)
        {
            Query = query;
        }

        public static SqlQuery FromProgramParams(Dictionary<string, string> programParams)
        {
            var programParamsKey_Query = string.Concat(ArgumentsParsed.PROGRAM_PARAM_PREFIX, ArgumentsParsed.PROGRAM_PARAM_NAME_QUERY);
            var programParamsKey_QueryFile = string.Concat(ArgumentsParsed.PROGRAM_PARAM_PREFIX, ArgumentsParsed.PROGRAM_PARAM_NAME_QUERY_FILE);

            if (programParams.ContainsKey(programParamsKey_Query))
            {
                return new SqlQuery(programParams[programParamsKey_Query]);
            }
            else if(programParams.ContainsKey(programParamsKey_QueryFile))
            {
                return FromSqlFile(programParams[programParamsKey_QueryFile]);
            }
            else
            {
                throw new Exception("Program parameters does not contain query or queryFile parameter");
            }
        }

        public static SqlQuery FromSqlFile(string sqlFilePath)
        {
            if(!File.Exists(sqlFilePath))
            {
                throw new Exception(string.Format("File with path {0} does not exists", sqlFilePath));
            }

            var queryString = File.ReadAllText(sqlFilePath);

            return new SqlQuery(queryString);
        }
    }
}
