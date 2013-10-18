using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace ServiceRunner
{
    class Program
    {
        public Program()
        {
            var catalog = new AssemblyCatalog(typeof(Program).Assembly);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        static void Main(string[] args)
        {
            var p = new Program();
        }
    }

  

    public interface IJob
    {
        void Execute();
    }

    [Export(typeof(IJob))]
    public class FirstJob : IJob
    {
        public void Execute()
        {
            Console.WriteLine("First job executing");
        }
    }
    
    [Export(typeof(IJob))]
    public class SecondJob : IJob
    {
        public void Execute()
        {
            Console.WriteLine("Second job executing");
        }
    }
}
