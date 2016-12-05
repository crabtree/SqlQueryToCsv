using Simple.Data;
using Simple.Data.RawSql;
using System.Collections.Generic;
using System.Linq;

namespace SQC.Application.Infrastructure
{
    public class SqlQueryRunner
    {
        public const string DEFAULT_CONNECTION_NAME = "Default";

        public string SqlQuery { get; private set; }
        public Dictionary<string, string> QueryParams { get; private set; }

        public SqlQueryRunner(string sqlQuery, Dictionary<string, string> queryParams)
        {
            SqlQuery = sqlQuery;
            QueryParams = queryParams;
        }

        public IEnumerable<dynamic> Run()
        {
            Database db = Database.OpenNamedConnection(DEFAULT_CONNECTION_NAME);
            var queryParamsDict = QueryParams.ToDictionary(p => p.Key, p => (object)p.Value);

            return db.ToRows(SqlQuery, queryParamsDict);
        }
    }
}
