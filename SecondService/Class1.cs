using System;
using System.ComponentModel.Composition;
using System.Linq;
using Common;
using NLog;

namespace SecondService
{
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
