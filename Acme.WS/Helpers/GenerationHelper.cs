using System;
using System.IO;
using Microsoft.Win32.TaskScheduler;

namespace Acme.WS.Helpers
{
    public static class ExecutionHelper
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public  static void ExecuteProgram(string programPath)
        {
            var programName = Path.GetFileNameWithoutExtension(programPath);

            using (var ts = new TaskService())
            {
                var t = ts.Execute(programPath)
                        .Once()
                        .Starting(DateTime.Now.AddSeconds(5))
                        .AsTask(programName);
            }
        }

        public static bool ExecutionTime()
        {
            var timeOfDay = ConfigurationHelper.ExecutionTime();

            var dateTime = DateTime.Now.Date;
            var ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            dateTime += ts;
            DateTime eodTime = Convert.ToDateTime(timeOfDay);

            var value = DateTime.Compare(dateTime, eodTime);

            return value == 0;
        }

        public static bool CheckIfDayIsManday()
        {
            return (DateTime.Now.DayOfWeek != DayOfWeek.Saturday
                     && DateTime.Now.DayOfWeek != DayOfWeek.Sunday);
        }
    }
}
