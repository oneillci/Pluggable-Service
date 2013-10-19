using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using NLog;

namespace PluginHost
{
    class Program
    {
        [ImportMany]
        public IEnumerable<IJob> Jobs { get; set; }

        public Program()
        {
            var catalog = new AssemblyCatalog(typeof(Program).Assembly);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        static void Main(string[] args)
        {
            var p = new Program();

            p.DoStuff();
            Console.WriteLine("\n\nPress any key to finish");
            Console.ReadKey();
        }
  
        private void DoStuff()
        {
            foreach (var job in Jobs)
            {
                job.Execute();
            }
        }
    }

    public interface IJob
    {
        void Execute();
    }

    [Export(typeof(IJob))]
    [ExportMetadata("Description", "The first one")]
    public class FirstJob : IJob
    {
        private Logger logger;

        public FirstJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void Execute()
        {
            logger.Debug("First job executing");
            Console.WriteLine("First job executing");
        }
    }
    
    [Export(typeof(IJob))]
    [ExportMetadata("Description", "The second one")]
    public class SecondJob : IJob
    {
        private Logger logger;

        public SecondJob()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void Execute()
        {
            logger.Debug("Second job executing");
            logger.Warn("second is warning!");
            Console.WriteLine("Second job executing");
        }
    }
}
