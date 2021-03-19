using System;
using System.Runtime.InteropServices;
using ZuulTextBased.Game;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased
{
    class Program
    {
        static void Main(string[] args)
        {
            MaximizeConsole();
            InitLogger();
            RunGame();
        }

        private static void MaximizeConsole()
        {
            //DLL Import used to find the pointer to the current console
            [DllImport("kernel32.dll", ExactSpelling = true)]
            static extern IntPtr GetConsoleWindow();
            IntPtr ThisConsole = GetConsoleWindow();

            //DLL Import used to control the current console
            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            //Maximize Console according to the screens dimensions
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, 3); //3 corresponds to SW_MAXIMIZE in the ShowWindow function
        }

        private static void InitLogger()
        {
            Logger logger = Logger.Instance;
            logger.LogLevel = LogLevel.Debug;
            logger.WriteMode = WriteMode.Console;
        }

        private static void RunGame()
        {
            ZuulGame game = new ZuulGame();
            game.Run();
        }
    }
}
