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
            DisableConsoleResize();
            InitLogger();
            RunGame();
        }

        private static void MaximizeConsole()
        {
            //DLL Import used to find the pointer to the current console
            [DllImport("kernel32.dll", ExactSpelling = true)]
            static extern IntPtr GetConsoleWindow();

            //DLL Import used to control the current console
            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            //Set Console window and buffer according to the screens dimensions
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), 3); //3 corresponds to SW_MAXIMIZE in the ShowWindow function, maximizes window
        }

        private static void DisableConsoleResize()
        {
            const int MF_BYCOMMAND = 0x00000000;
            const int SC_SIZE = 0xF000;

            //DLL Import used to find the pointer to the current console
            [DllImport("kernel32.dll", ExactSpelling = true)]
            static extern IntPtr GetConsoleWindow();

            //DLL Import user to get menus
            [DllImport("user32.dll")]
            static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

            //DLL Import used to disable menus
            [DllImport("user32.dll")]
            static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
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
