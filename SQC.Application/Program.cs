using SQC.Application.Infrastructure;

namespace SQC.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParsed = ArgumentsParsed.FromArgs(args);
            var sqlQuery = SqlQuery.FromProgramParams(argsParsed.ProgramParams);
            var sqlData = new SqlQueryRunner(sqlQuery.Query, argsParsed.QueryParams).Run();

            CsvData
                .FromSqlData(sqlData)
                .ToFile(argsParsed.GetValue(string.Concat(ArgumentsParsed.PROGRAM_PARAM_PREFIX, "outFilePath")));
        }
    }  
}
