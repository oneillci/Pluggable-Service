using System;
using System.ComponentModel.Composition;
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
        private Logger logger;
        private Timer timer;

        public FirstJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            this.timer = new Timer(5000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("Starting first timer");
            logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("Stopping first timer");
            logger.Debug("Stopping timer");
        }

        public void Execute()
        {
            logger.Debug("First job executing");
            Console.WriteLine("First job executing");
        }
    }
}
