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
        static void Main(string[] args)
        {
            var p = new PluginRunner();

            p.ExecutePlugins();
            Console.WriteLine("\n\nPress any key to finish");
            Console.ReadKey();
            p.StopPlugins();
        }           
    }   
        
}
