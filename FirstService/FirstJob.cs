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
    [ExportMetadata("Description", "The first one")]
    public class FirstJob : IJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;
        private readonly Timer timer;

        public FirstJob()
        {
            EventLog.WriteEntry(LogSource, "FirstJob Ctor");
            //this.logger = LogManager.GetCurrentClassLogger();
            //logger.Debug("Ctor");
            this.timer = new Timer(5000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            EventLog.WriteEntry(LogSource, "FirstJob starting timer");
            Console.WriteLine("Starting first timer");
            //logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            EventLog.WriteEntry(LogSource, "FirstJob stopping timer");
            Console.WriteLine("Stopping first timer");
            //logger.Debug("Stopping timer");
        }

        public void Execute()
        {
            //logger.Debug("First job executing");
            EventLog.WriteEntry(LogSource, "FirstJob executing");
            Console.WriteLine("First job executing");
        }
    }
}
