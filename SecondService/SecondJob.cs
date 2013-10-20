using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Common;
using NLog;

namespace SecondService
{
    [Export(typeof(IJob))]
    [ExportMetadata("Description", "Second description")]
    public class SecondJob : IJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;
        private readonly Timer timer;

        public string CronSchedule { get { return ""; } }

        public SecondJob()
        {
            Console.WriteLine("SecondJob ctor");
            this.logger = LogManager.GetCurrentClassLogger();
            this.timer = new Timer(8000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("SecondJob Starting second timer");
            logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("SecondJob Stopping second timer");
            logger.Debug("Stopping timer");
        }

        public void Execute()
        {
            logger.Debug("Second job executing");
            //logger.Warn("second is warning!");
            Console.WriteLine("Second job executing");
        }
    }
}
