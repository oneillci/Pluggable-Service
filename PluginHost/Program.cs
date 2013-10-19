using System;
using System.Linq;
using Topshelf;

namespace PluginHost
{
    class Program
    {        
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<PluginRunner>(s =>
                {
                    s.ConstructUsing(() => new PluginRunner());
                    s.WhenStarted(y => y.ExecutePlugins());
                    s.WhenStopped(y => y.StopPlugins());
                });
                
                x.RunAsLocalService();
                x.StartAutomaticallyDelayed();
                x.SetDescription("Ciaran Description:POC Pluggable Service");
                x.SetDisplayName("Ciaran Display Name");
                x.SetServiceName("CiaranServiceName");                
            });
            var p = new PluginRunner();

            p.ExecutePlugins();
            Console.WriteLine("\n\nPress any key to finish");
            Console.ReadKey();
            p.StopPlugins();
        }           
    }   
        
}
