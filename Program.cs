using System;
using ZuulTextBased.Utility;

namespace ZuulTextBased
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.LogLevel = LogLevel.Debug;
            ZuulGame game = new ZuulGame();
            game.Run();
        }
    }
}
