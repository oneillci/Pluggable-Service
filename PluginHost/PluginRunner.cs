using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Common;
using NLog;

namespace PluginHost
{
    class PluginRunner
    {
        [ImportMany]
        private IEnumerable<IJob> Jobs { get; set; }

        Logger logger = LogManager.GetCurrentClassLogger();

        public PluginRunner()
        {
            Console.WriteLine("Plugin runner ctor");
            //logger.Debug("plugin runner ctor");
        }

        public void ConfigurePlugins()
        {
            Console.WriteLine("Configuring plugins");
            logger.Debug("Configuring plugins");
            var path = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath) + "\\plugins";
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void ExecutePlugins()
        {
            Console.WriteLine("PluginRunner.ExecutePlugins");
            logger.Debug("ExecutePlugins");
            foreach (var job in Jobs)
            {
                job.Start();
            }
        }

        public void StopPlugins()
        {
            Console.WriteLine("PluginRunner.StopPlugins");
            logger.Debug("StopPlugins");
            foreach (var job in Jobs)
            {
                job.Stop();
            }
        }
    }
}
