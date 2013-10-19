using System;
using System.ComponentModel.Composition;
using System.Linq;
using Common;
using NLog;

namespace FirstService
{
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
}
