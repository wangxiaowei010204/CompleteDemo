using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteDemo.Common
{
    public interface ILogHelper
    {
        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="msg"></param>
        void Trace(string msg);

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg"></param>
        void Debug(string msg);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);

        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="msg"></param>
        void Info(string msg);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg">错误信息</param>
        void Error(string msg);

        /// <summary>
        /// 致命错误日志
        /// </summary>
        /// <param name="msg">错误信息</param>
        void Fatal(string msg);

    }
}
