using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Common;

namespace PluginHost
{
    class PluginRunner
    {
        [ImportMany]
        private IEnumerable<IJob> Jobs { get; set; }

        public PluginRunner()
        {
            EventLog.WriteEntry(Program.LogSource, "plugin runner ctor");
        }

        public void ConfigurePlugins()
        {
            EventLog.WriteEntry(Program.LogSource, "ConfigurePlugins start");
            var path = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath) + "\\plugins";
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            EventLog.WriteEntry(Program.LogSource, "ConfigurePlugins end");
        }

        public void ExecutePlugins()
        {
            EventLog.WriteEntry(Program.LogSource, "ExecutePlugins start");
            foreach (var job in Jobs)
            {
                job.Start();
            }
            EventLog.WriteEntry(Program.LogSource, "ExecutePlugins end");
        }

        public void StopPlugins()
        {
            EventLog.WriteEntry(Program.LogSource, "StopPlugins start");
            foreach (var job in Jobs)
            {
                job.Stop();
            }
            EventLog.WriteEntry(Program.LogSource, "StopPlugins end");
        }
    }
}
