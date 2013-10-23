﻿using System;
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
    public class SecondJob : IObgJob
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
        public string CronSchedule { get { return "1-59/3 21-22 * * * ?"; } }

        public SecondJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            this.timer = new Timer(8000);
            //timer.Elapsed += (s, e) => Execute();
        }

        public void Start()
        {
            timer.Start();
            logger.Debug("Starting timer");
        }

        public void Stop()
        {
            timer.Stop();
            logger.Debug("Stopping timer");
        }

        public void Execute(IJobExecutionContext jobContext)
        {
            logger.Debug("Second job executing");
            //logger.Warn("second is warning!");
        }
    }
}
