﻿using System;
using System.Drawing;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.View
{
    /// <summary>
    /// Responsible for showing the player the information needed to play the game, 
    /// as well as any visual elements like the screen
    /// </summary>
    internal class Writer : IObserver
    {
        public WriteMode WriteMode { get; set; }
        public Screen ActiveScreen { get; private set; }
        public Point Offset { get; private set; }

        public Writer(Screen screen)
        {
            ActiveScreen = screen;
            //Center the Screen
            int xOffset = (Console.BufferWidth - ActiveScreen.Width) / 2;
            int yOffset = (Console.BufferHeight - ActiveScreen.Height) / 2;
            Offset = new Point(xOffset, yOffset);

            WriteMode = WriteMode.Console;
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

        public void Write(string message)
        {
            switch (WriteMode)
            {
                case WriteMode.Console:
                    Console.WriteLine(message);
                    break;
            }
        }

        public void Draw()
        {
            Console.Clear();
            DrawScreen();
            SetCursorAtPlayerPosition();
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

        private void SetCursorAtPlayerPosition()
        {
            //TODO: move to the input subscreen of the game screen
            Console.SetCursorPosition(Offset.X + 3, Offset.Y + ActiveScreen.Height - 3);
            Console.Write("> ");
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
    }
}
