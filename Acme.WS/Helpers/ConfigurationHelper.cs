using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Acme.WS.Helpers
{
    public static class ConfigurationHelper
    {
        public static string ExecutionTime() => ConfigurationManager.AppSettings["ExecutionTime"];

        public static int GetShortTimeOut() => Convert.ToInt32(ConfigurationManager.AppSettings["ShortTimeout"]);

        public static int GetLongTimeOut() => Convert.ToInt32(ConfigurationManager.AppSettings["LongTimeout"]);

        public static int GetWeekendTimeOut() => Convert.ToInt32(ConfigurationManager.AppSettings["LongTimeout"]);

        public static List<string> GetListOfPrograms()
        {
            var listOfPrograms = new List<string>();
            var programList = ConfigurationManager.GetSection("programList") as NameValueCollection;

            if (programList.Count == 0) return listOfPrograms;

            foreach (var key in programList.AllKeys)
            {
                listOfPrograms.Add(programList[key]);
            }

            return listOfPrograms;
        }

    }
}
