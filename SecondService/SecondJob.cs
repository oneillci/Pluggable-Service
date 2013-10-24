using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
using Common;
using NLog;
using Quartz;

namespace SecondService
{
    [Export(typeof(IObgJob))]
    [ExportMetadata("Description", "Second description")]
    [DisallowConcurrentExecution]
    public class SecondJob : IObgJob
    {
        public const string LogSource = "CiaranServiceNameTest1";
        private readonly Logger logger;

        /// <summary>
        /// "1-59/2 * * * * ?" runs every 2 seconds from minute 1 to 59 on every hour of every day
        /// <para>
        /// "1-59/2 * 7-19 * * ?" runs every 2 seconds from minute 1 to 59 between hours of 7:00 and 19:59 of every day
        /// </para>
        /// </summary>
        public string CronExpression { get { return "1-59/5 * 22-23 * * ?"; } }

        public SecondJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            logger.Debug("");
        }

        public void Execute(IJobExecutionContext jobContext)
        {
            logger.Debug("Second job executing");
            //logger.Warn("second is warning!");
        }
    }
}
