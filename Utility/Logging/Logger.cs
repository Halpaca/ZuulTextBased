using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World;

namespace ZuulTextBased.Utility.Logging
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

        /// <summary>
        /// Used Sparingly for debugging purposes and stubs
        /// </summary>
        public void Debug(string source, string message)
        {
            if(LogLevel.Debug <= LogLevel)
            {
                Write(LogLevel.Debug, source, message);
            }
        }

        /// <summary>
        /// Used for notifying the user for furtherings in the program
        /// </summary>
        public void Info(string source, string message)
        {
            if (LogLevel.Info <= LogLevel)
            {
                Write(LogLevel.Info, source, message);
            }
        }

        /// <summary>
        /// Used to warn the user for mild inconviniences, where the program can still run
        /// </summary>
        public void Warn(string source, string message)
        {
            if (LogLevel.Warn <= LogLevel)
            {
                Write(LogLevel.Warn, source, message);
            }
        }

        /// <summary>
        /// Used for major breaks that will make it difficult for the program to keep running
        /// </summary>
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
