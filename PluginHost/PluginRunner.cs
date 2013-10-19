using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
            var path = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath) + "\\plugins";
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void ExecutePlugins()
        {
            foreach (var job in Jobs)
            {
                job.Start();
            }
        }

        public void StopPlugins()
        {
            foreach (var job in Jobs)
            {
                job.Stop();
            }
        }
    }
}
