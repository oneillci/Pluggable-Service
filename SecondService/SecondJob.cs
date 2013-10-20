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
    [ExportMetadata("Description", "The second one")]
    public class SecondJob : IJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;
        private readonly Timer timer;

        public SecondJob()
        {
            EventLog.WriteEntry(LogSource, "SecondJob Ctor");
            //this.logger = LogManager.GetCurrentClassLogger();
            //logger.Debug("Ctor");
            this.timer = new Timer(8000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            EventLog.WriteEntry(LogSource, "SecondJob Start");
            Console.WriteLine("Starting second timer");
            //logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            EventLog.WriteEntry(LogSource, "SecondJob Stop");
            Console.WriteLine("Stopping second timer");
            //logger.Debug("Stopping timer");
        }

        public void Execute()
        {
            EventLog.WriteEntry(LogSource, "SecondJob executing");
            //logger.Debug("Second job executing");
            //logger.Warn("second is warning!");
            Console.WriteLine("Second job executing");
        }
    }
}
