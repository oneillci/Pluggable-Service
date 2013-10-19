using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Common;
using NLog;

namespace PluginHost
{
    class Program
    {
        [ImportMany]
        public IEnumerable<IJob> Jobs { get; set; }

        public Program()
        {
            var path = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath) + "\\plugins";
            var catalog = new DirectoryCatalog(path);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        static void Main(string[] args)
        {
            var p = new Program();

            p.ExecuteJobs();
            Console.WriteLine("\n\nPress any key to finish");
            Console.ReadKey();
        }
  
        private void ExecuteJobs()
        {
            foreach (var job in Jobs)
            {
                job.Execute();
            }
        }
    }   
        
}
