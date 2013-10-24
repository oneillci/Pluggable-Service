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
            var logger = LogManager.GetCurrentClassLogger();

            HostFactory.Run(x =>
            {
                x.Service<PluginHost>(s =>
                {
                    s.ConstructUsing(() =>
                    {
                        Console.WriteLine("Construct using");
                        //logger.Debug("construct using");
                        return new PluginHost();
                    });
                    
                    s.WhenStarted(y =>
                    {
                        Console.WriteLine("WhenStarted");
                        //logger.Debug("WhenStarted");                       
                        y.ConfigurePlugins();
                        y.ExecutePlugins();
                        
                    });
                    s.WhenStopped(y =>
                    {
                        Console.WriteLine("WhenStopped");
                        //logger.Debug("WhenStopped");
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
