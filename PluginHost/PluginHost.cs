using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using Common;
using NLog;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace PluginHost
{
    class PluginHost
    {
        [ImportMany]
        //private IEnumerable<Lazy<IObgJob, IJobMetadata>> Jobs { get; set; }
        private IEnumerable<IObgJob> Jobs { get; set; }

        Logger logger = LogManager.GetCurrentClassLogger();
        private IContainer _container;

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

            var b = new Bootstrapper();
            _container = b.Configure(catalog);

            //var container = new CompositionContainer(ag);
            //container.ComposeParts(this);

            var scope = _container.BeginLifetimeScope();
            Jobs = scope.Resolve<IEnumerable<IObgJob>>();

            logger.Debug("{0} plugins found in {0}ms", catalog.Parts.Count(), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();            
        }

        public void ExecutePlugins()
        {
            // read http://quartznet.sourceforge.net/tutorial/lesson_3.html
            // jobs should only throw JobExecutionException, so wrap all in try/catch

            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            scheduler.JobFactory = new AutofacJobFactory(_container);
            scheduler.Start();            

            foreach (var job in Jobs)
            {                
                //logger.Debug("Creating Schedule for {0}", job.Metadata.Description);
                logger.Debug("Creating Schedule for {0}", job.GetType().ToString());

                IJobDetail jobDetail = JobBuilder.Create(job.GetType())
                                                //.WithIdentity(job.Metadata.Description + "_JOBDETAIL")
                                                .WithIdentity(job.GetType().ToString() + "_JOBDETAIL")
                                                .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity(job.GetType().ToString() + "_TRIGGER")
                                                 .WithCronSchedule(job.CronExpression)
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
                //logger.Debug("Stopping {0}", job.Metadata.Description);
                logger.Debug("Stopping {0}", job.GetType().ToString());
                //job.Value.Stop();
            }
        }
    }
  
    public class AutofacJobFactory : IJobFactory
    {
        private readonly IContainer _container;

        public AutofacJobFactory(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var lifetimeScope = _container.BeginLifetimeScope();
            return (IJob)lifetimeScope.Resolve(bundle.JobDetail.JobType);
        }


        public void ReturnJob(IJob job)
        {
            // TODO: Implement this method
            //throw new NotImplementedException();
        }
    }
}
