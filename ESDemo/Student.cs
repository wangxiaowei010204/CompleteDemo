using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESDemo
{
    [ElasticsearchType(Name = "student")]
    public class Student
    {
       
        public string ID { get; set; }

       
        public string Name { get; set; }


        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}
