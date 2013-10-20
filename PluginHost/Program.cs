using System;
using System.Diagnostics;
using System.Linq;
using NLog;
using Topshelf;

namespace PluginHost
{
    class Program
    {        
        public const string LogSource = "CiaranServiceNameTest1";

        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<PluginRunner>(s =>
                {
                    s.ConstructUsing(() =>
                    {
                        EventLog.WriteEntry(LogSource, "construct using");
                        return new PluginRunner();
                    });
                    
                    s.WhenStarted(y =>
                    {
                        EventLog.WriteEntry(LogSource, "WhenStarted");
                        y.ConfigurePlugins();
                        y.ExecutePlugins();
                        
                    });
                    s.WhenStopped(y =>
                    {
                        EventLog.WriteEntry(Program.LogSource, "WhenStopped");
                        y.StopPlugins();                        
                    });
                });
                
                x.RunAsLocalService();
                x.StartAutomaticallyDelayed();
                x.SetDescription("Ciaran Description:POC Pluggable Service");
                x.SetDisplayName("Ciaran Display Name");
                x.SetServiceName("CiaranServiceName");                
            });
            //var p = new PluginRunner();
            //p.ExecutePlugins();
            //Console.WriteLine("\n\nPress any key to finish");
            //Console.ReadKey();
            //p.StopPlugins();
        }
    }   
        
}
