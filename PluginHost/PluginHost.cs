using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Common;
using NLog;
using Quartz;
using Quartz.Impl;

namespace PluginHost
{
    class PluginHost
    {
        [ImportMany]
        private IEnumerable<Lazy<IObgJob, IJobMetadata>> Jobs { get; set; }

        Logger logger = LogManager.GetCurrentClassLogger();

        public PluginHost()
        {
            logger.Debug("");
        }

        public void ConfigurePlugins()
        {
            logger.Debug("");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var path = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath) + "\\plugins";
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            stopwatch.Stop();
            logger.Debug("{0} plugins found in {0}ms", Jobs.Count(), stopwatch.ElapsedMilliseconds);
        }

        public void ExecutePlugins()
        {
            // read http://quartznet.sourceforge.net/tutorial/lesson_3.html
            // jobs should only throw JobExecutionException, so wrap all in try/catch

            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            scheduler.Start();            

            foreach (var job in Jobs)
            {                
                logger.Debug("Creating Schedule for {0}", job.Metadata.Description);

                IJobDetail jobDetail = JobBuilder.Create(job.Value.GetType())
                                                .WithIdentity(job.Metadata.Description + "_JOBDETAIL")
                                                .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity(job.Metadata.Description + "_TRIGGER")
                                                 .WithCronSchedule(job.Value.CronSchedule)
                                                 //.WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                                                 .StartAt(DateBuilder.FutureDate(2, IntervalUnit.Second))
                                                 .Build();


                scheduler.ScheduleJob(jobDetail, trigger);

                //job.Value.Start();
            }
        }

        public void StopPlugins()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            factory.AllSchedulers.First().Shutdown();

            foreach (var job in Jobs)
            {
                logger.Debug("Stopping {0}", job.Metadata.Description);
                //job.Value.Stop();
            }
        }
    }
     
}
