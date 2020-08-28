using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility.Logger
{
    internal class Logger
    {
        public static Logger Instance { get; } = new Logger();
        public LogLevel LogLevel { get; set; }
        public WriteTarget WriteTarget { get; set; }

        public void Log(LogLevel level, string source ,string message)
        {
            if(level <= LogLevel)
            {
                Write(level, source, message);
            }
        }

        public void Debug(string source, string message)
        {
            if(LogLevel.Debug <= LogLevel)
            {
                Write(LogLevel.Debug, source, message);
            }
        }

        public void Info(string source, string message)
        {
            if (LogLevel.Info <= LogLevel)
            {
                Write(LogLevel.Info, source, message);
            }
        }

        public void Warn(string source, string message)
        {
            if (LogLevel.Warn <= LogLevel)
            {
                Write(LogLevel.Warn, source, message);
            }
        }

        public void Error(string source, string message)
        {
            if (LogLevel.Error <= LogLevel)
            {
                Write(LogLevel.Error, source, message);
            }
        }

        public void Write(LogLevel level, string source, string message)
        {
            switch(WriteTarget)
            {
                case WriteTarget.Console:
                    Console.WriteLine($"{DateTime.Now:yyyy-mm-dd hh:mm:ss} [{source}] [{level}]: {message}");
                    break;
            }
        }
    }
}
