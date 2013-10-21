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

        /// <summary>
        /// "1-59/2 * * * * ?" runs every 2 minutes from minute 1 to 59 on every hour of every day
        /// <para>
        /// "1-59/2 7-19 * * * ?" runs every 2 minutes from minute 1 to 59 between hours of 7:00 and 19:59 of every day
        /// </para>
        /// </summary>
        public string CronSchedule { get { return "1-59/2 * * * * ?"; } }

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
