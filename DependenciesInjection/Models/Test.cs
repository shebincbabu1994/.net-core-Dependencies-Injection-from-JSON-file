using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependenciesInjection.Models
{
    public class Test:ITest
    {
        public string DoSomething(string parameter)
        {
            return $"Message from Test with { parameter }";
        }
    }
}
