using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Common
{
    //[Export(typeof(ITest))]
    public class Test : ITest
    {
        public int Number
        {
            get
            {
                return 3;
            }
        }

        public string GetStuff()
        {
            return "Yo";
        }
    }
  
    public interface ITest
    {
        int Number { get; }

        string GetStuff();
    }

    public interface IRepository<T>
    {
        string DoIt();
    }

    public class MyRepository<T> : IRepository<T>
    {
        public string DoIt()
        {
            return typeof(T).ToString();
        }
    }

    public class User { }
    
    public class Team {}
}
