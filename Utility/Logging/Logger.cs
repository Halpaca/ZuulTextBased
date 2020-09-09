﻿using System;
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

        public void Log(LogLevel level, Type caller, string message)
        {
            if(level <= LogLevel)
            {
                Write(level, caller.Name, message);
            }
        }

        /// <summary>
        /// Used Sparingly for debugging purposes and stubs
        /// </summary>
        public void Debug(Type caller, string message)
        {
            if(LogLevel.Debug <= LogLevel)
            {
                Write(LogLevel.Debug, caller.Name, message);
            }
        }

        /// <summary>
        /// Used for notifying the user for furtherings in the program
        /// </summary>
        public void Info(Type caller, string message)
        {
            if (LogLevel.Info <= LogLevel)
            {
                Write(LogLevel.Info, caller.Name, message);
            }
        }

        /// <summary>
        /// Used to warn the user for mild inconviniences, where the program can still run
        /// </summary>
        public void Warn(Type caller, string message)
        {
            if (LogLevel.Warn <= LogLevel)
            {
                Write(LogLevel.Warn, caller.Name, message);
            }
        }

        /// <summary>
        /// Used for major breaks that will make it difficult for the program to keep running
        /// </summary>
        public void Error(Type caller, string message)
        {
            if (LogLevel.Error <= LogLevel)
            {
                Write(LogLevel.Error, caller.Name, message);
            }
        }

        public void Write(LogLevel level, string callername, string message)
        {
            switch(WriteTarget)
            {
                case WriteTarget.Console:
                    Console.WriteLine($"{DateTime.Now:yyyy-mm-dd hh:mm:ss} [{callername}] [{level}]: {message}");
                    break;
            }
        }
    }
}
