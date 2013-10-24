using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
using Common;
using NLog;
using Quartz;

namespace FirstService
{
    [Export(typeof(IObgJob))]
    [ExportMetadata("Description", "First description")]
    public class FirstJob : IObgJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;

        /// <summary>
        /// "1-59/2 * * * * ?" runs every 2 minutes from minute 1 to 59 on every hour of every day
        /// <para>
        /// "1-59/2 7-19 * * * ?" runs every 2 minutes from minute 1 to 59 between hours of 7:00 and 19:59 of every day
        /// </para>
        /// </summary>
        public string CronSchedule { get { return "1-59/2 * * * * ?"; } }

        public FirstJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void Execute(IJobExecutionContext jobContext)
        {            
            logger.Debug("First job executing");
        }
    }
}
