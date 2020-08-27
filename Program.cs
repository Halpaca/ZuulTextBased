using System;
using ZuulTextBased.Utility;

namespace ZuulTextBased
{
    class Program
    {
        static void Main(string[] args)
        {
            ZuulGame game = new ZuulGame();
            game.WriteTarget = WriteTarget.Console;
            game.Run();
        }
    }
}
