using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Common;
using NLog;

namespace FirstService
{
    [Export(typeof(IJob))]
    [ExportMetadata("Description", "First description")]
    public class FirstJob : IJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;
        private readonly Timer timer;

        public string CronSchedule { get { return "";} }

        public FirstJob()
        {
            Console.WriteLine("FirstJob ctor");
            this.logger = LogManager.GetCurrentClassLogger();
            this.timer = new Timer(5000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("FirstJob Starting first timer");
            logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("FirstJob Stopping first timer");
            logger.Debug("Stopping timer");
        }

        public void Execute()
        {            
            Console.WriteLine("First job executing");
            logger.Debug("First job executing");
        }
    }
}
