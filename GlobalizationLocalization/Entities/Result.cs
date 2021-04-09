using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalizationLocalization.Entities
{
    public class Result
    {
        public bool IsValid { get; set; }
        public string Parameter { get; set; }
        public Person Person { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
