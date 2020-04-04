using NLog;

namespace CompleteDemo.Common
{
    public class Nlogger : ILogHelper
    {
        Logger logger;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Nlogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="msg"></param>
        public void Trace(string msg)
        {
            logger.Trace(msg);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg"></param>
        public void Debug(string msg)
        {
            logger.Debug(msg);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        public void Warn(string msg)
        {
            logger.Warn(msg);
        }

        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            logger.Info(msg);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg">错误信息</param>
        public void Error(string msg)
        {
            logger.Error(msg);
        }

        /// <summary>
        /// 致命错误日志
        /// </summary>
        /// <param name="msg">错误信息</param>
        public void Fatal(string msg)
        {
            logger.Fatal(msg);
        }
    }
}
