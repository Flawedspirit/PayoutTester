using System;

namespace PayoutTester
{
    class InterfaceHelper
    {
        public static void WriteLine(String print, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(print);
            Console.ForegroundColor = prevColor;
        }

        public static void Write(String print, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(print);
            Console.ForegroundColor = prevColor;
        }

        public static void PrintHeader()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, Program.headerState);
                Console.WriteLine("#");
            }

            Program.headerState++;

            if (Program.headerState < 2)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write("#");

                Console.SetCursorPosition(2, 1);
                Write("Payout Tester by Marc Chiarelli (c) " + DateTime.Now.Year, ConsoleColor.DarkCyan);

                Console.SetCursorPosition(Console.WindowWidth - ((Program.VERSION.Length) + 2), 1);
                Write(Program.VERSION, ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Console.WindowWidth - 1, 1);
                Console.Write("#");

                Program.headerState++;
            }
            else
            {
                Console.SetCursorPosition(0, Program.headerState + 1);
                return;
            }

            PrintHeader();
            Program.headerState = 0;
        }
    }
}
