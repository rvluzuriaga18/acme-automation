using System.Threading;
using Acme.WS.Controllers;

namespace Acme.WS
{
    public class ProgramStarterCaller
    {
        public static bool IsRunning { get; set; }
        private Thread thread;

        public void StartProcess()
        {
            IsRunning = true;

#if !DEBUG
            
            thread = new Thread(ProgramStarterController.RunProgram) { IsBackground = true };
            thread.Start();
#else
            ProgramStarterController.RunProgram();
#endif
        }

        public void StopProcess()
        {
            IsRunning = false;
            if (thread != null) thread.Abort();
        }
    }
}
