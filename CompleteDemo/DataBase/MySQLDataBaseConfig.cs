using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.DataBase
{
    public class MySQLDataBaseConfig
    {
        /// <summary>
        /// 默认的MySQL的链接字符串
        /// </summary>
        private const string DefaultMySqlConnectionString = "server=localhost;userid=root;pwd=123456;port=3306;database=wxw;";

        public DemoContext CreateContext(string mySqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(mySqlConnectionString))
            {
                mySqlConnectionString = DefaultMySqlConnectionString;
            }

            var optionBuilder = new DbContextOptionsBuilder<DemoContext>();
            optionBuilder.UseMySQL(mySqlConnectionString);

            var context = new DemoContext(optionBuilder.Options);

            return context;
        }
    }
}
