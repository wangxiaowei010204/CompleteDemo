using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESDemo
{
    public class ESServices
    {
        ElasticClient client;

        public ESServices()
        {
            var node = new Uri("http://myserver:9200");
            var settings = new ConnectionSettings(node).DefaultIndex("defaultindex");
            client = new ElasticClient(settings);
        }

        public bool Insert()
        {
            var list = new List<Student>()
            {
                new Student(){
                    Name="wangxiaowei",
                    ID="112233"
                }
            };

            client.IndexMany<Student>(list);

            return true;
        }

        public IReadOnlyCollection<Student> Search()
        {
            return client.Search<Student>(s => s.Query(q => q.Match(m => m.Field(f => f.Name).Query("wangxiaowei")))).Documents;
        }
       
    }
}
