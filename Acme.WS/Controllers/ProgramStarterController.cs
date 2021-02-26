using System;
using System.Threading;
using Acme.WS.Helpers;

namespace Acme.WS.Controllers
{
    public class ProgramStarterController
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void RunProgram()
        {
            _logger.Info("Acme Program Starter Service has been started...");

            var tempDate = DateTime.Now;
            var isGenerated = false;

            while (ProgramStarterCaller.IsRunning)
            {
                try
                {
                    if (tempDate.DayOfWeek != DateTime.Now.DayOfWeek)
                        isGenerated = false;

                    if (!ExecutionHelper.CheckIfDayIsManday()) 
                        Thread.Sleep(ConfigurationHelper.GetWeekendTimeOut());

                    if ((ExecutionHelper.CheckIfDayIsManday() && ExecutionHelper.ExecutionTime()) 
                        && isGenerated == false)
                    {
                        var listOfPrograms = ConfigurationHelper.GetListOfPrograms();
                        foreach (var program in listOfPrograms)
                        {
                            ExecutionHelper.ExecuteProgram(program);
                            Thread.Sleep(ConfigurationHelper.GetShortTimeOut());
                        }

                        tempDate = DateTime.Now;
                        isGenerated = true;

                        Thread.Sleep(ConfigurationHelper.GetLongTimeOut());
                    }

                    Thread.Sleep(ConfigurationHelper.GetShortTimeOut());
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.ToString());
                    Thread.Sleep(ConfigurationHelper.GetShortTimeOut());
                }
            }
        }
    }
}
