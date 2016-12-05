using System;
using System.Collections.Generic;
using System.Linq;

namespace SQC.Application.Infrastructure
{
    public class ArgumentsParsed
    {
        public const string PROGRAM_PARAM_PREFIX = "--";
        public const string QUERY_PARAM_PREFIX = "@";
        public const string PROGRAM_PARAM_NAME_QUERY = "query";
        public const string PROGRAM_PARAM_NAME_QUERY_FILE = "queryFile";

        public Dictionary<string, string> ProgramParams { get; private set; }
        public Dictionary<string, string> QueryParams { get; private set; }

        public ArgumentsParsed(Dictionary<string, string> programParams, Dictionary<string, string> queryParams)
        {
            ProgramParams = programParams;
            QueryParams = queryParams;
        }

        public string GetValue(string key, string defaultValue = null)
        {
            if (key.StartsWith(PROGRAM_PARAM_PREFIX))
            {
                return ProgramParams[key];
            }
            else if (key.StartsWith(QUERY_PARAM_PREFIX))
            {
                return QueryParams[key];
            }
            else if(defaultValue != null)
            {
                return defaultValue;
            }
            else
            {
                throw new Exception(string.Format("Parameter with name {0} not found", key));
            }
        }

        public static ArgumentsParsed FromArgs(string[] args)
        {
            var programParamsToParse = args.Where(arg => arg.StartsWith("--")).ToList();
            var queryParamsToParse = args.Where(arg => arg.StartsWith("@")).ToList();

            var programParams = ParseProgramParams(programParamsToParse);
            var queryParams = ParseQueryParams(queryParamsToParse);

            return new ArgumentsParsed(programParams, queryParams);
        }

        private static Dictionary<string, string> ParseProgramParams(List<string> programParamsToParse)
        {
            return SplitParamsStrings(programParamsToParse);
        }

        private static Dictionary<string, string> ParseQueryParams(List<string> queryParamsToParse)
        {
            return SplitParamsStrings(queryParamsToParse);
        }

        private static Dictionary<string, string> SplitParamsStrings(List<string> paramsStringsToSplit)
        {
            return paramsStringsToSplit
                .Where(arg => arg.Contains("="))
                .Select(arg => arg.Split(new[] { '=' }, 2))
                .ToDictionary(splitted => splitted[0], splitted => splitted[1]);
        }
    }
}
