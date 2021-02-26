using System.ServiceProcess;

namespace Acme.WS
{
    public partial class Service1 : ServiceBase
    {
        private ProgramStarterCaller processCaller = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            processCaller = new ProgramStarterCaller();
            processCaller.StartProcess();
        }

        protected override void OnStop()
        {
            if (processCaller != null)
                processCaller.StopProcess();
        }
    }
}
