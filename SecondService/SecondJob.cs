using System;
using System.ComponentModel.Composition;
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
        private readonly Logger logger;
        private readonly Timer timer;

        public SecondJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            logger.Debug("Ctor");
            this.timer = new Timer(8000);
            timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("Starting second timer");
            logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("Stopping second timer");
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
