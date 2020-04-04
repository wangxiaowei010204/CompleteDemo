using Castle.DynamicProxy;
using CompleteDemo.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.DataBase
{
    public class LogInterceptor : IInterceptor
    {
        ILogger<LogInterceptor> logger;

        public LogInterceptor(ILogger<LogInterceptor> _logger)
        {
            logger = _logger;
        }

        public void Intercept(IInvocation invocation)
        {
            logger.LogInformation($"你正在调用方法 \"{invocation.Method.Name}\"  参数是 {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}... ");

            invocation.Proceed();
            if (invocation.ReturnValue != null && invocation.ReturnValue is string)
            {
                //在返回接口上拼上LogInterceptor
                invocation.ReturnValue += " LogInterceptor";
            }
            logger.LogInformation($"方法执行完毕，返回结果：{JsonConvert.SerializeObject(invocation.ReturnValue)}");

        }
    }
}
