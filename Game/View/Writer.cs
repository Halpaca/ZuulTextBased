using System;
using System.Drawing;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.View
{
    internal class Writer : IObserver
    {
        public WriteMode WriteMode { get; set; }
        public Screen ActiveScreen { get; private set; }
        public Point Offset { get; private set; }

        public Writer(Screen s)
        {
            ActiveScreen = s;
            //Center the Screen
            int xOffset = (Console.BufferWidth - ActiveScreen.Width) / 2;
            int yOffset = (Console.BufferHeight - ActiveScreen.Height) / 2;
            Offset = new Point(xOffset, yOffset);

            WriteMode = WriteMode.Console;
        }

        /// <summary>
        /// The only function that should write out text to the player outside of debug mode
        /// To be refactored to send to a custom window instead of the console
        /// </summary>
        public void Write(string message)
        {
            switch (WriteMode)
            {
                case WriteMode.Console:
                    Console.WriteLine(message);
                    break;
            }
        }

        public void DrawText()
        {
            ClearScreen();
            DrawScreen();
            Console.SetCursorPosition(Offset.X + 3, Offset.Y + ActiveScreen.Height - 3);
            Console.Write("> ");
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        private void DrawScreen()
        {
            StringBuilder output = new StringBuilder();
            Point p = new Point();
            for (int y = -Offset.Y; y < ActiveScreen.Height; y++)
            {
                for (int x = -Offset.X; x < ActiveScreen.Width; x++)
                {
                    p.X = x;
                    p.Y = y;
                    char c = GetScreenChar(ActiveScreen.GetBorderTypeAt(p));
                    output.Append(c);
                }
                output.AppendLine();
            }
            Console.Write(output);
        }

        private char GetScreenChar(BorderType area)
        {
            return area switch
            {
                BorderType.Horizontal => '═',
                BorderType.Vertical => '║',
                BorderType.TopLeft => '╔',
                BorderType.TopRight => '╗',
                BorderType.BottomLeft => '╚',
                BorderType.BottomRight => '╝',
                BorderType.TSplitTop => '╦',
                BorderType.TSplitBottom => '╩',
                BorderType.TSplitLeft => '╠',
                BorderType.TSplitRight => '╣',
                BorderType.CrossSection => '╬',
                _ => ' '
            };
        }

        public void OnNotify(Event state)
        {
            switch (state)
            {
                case WriteEvent:
                    Write((string)state.Data);
                    break;
            }
        }
    }
}
