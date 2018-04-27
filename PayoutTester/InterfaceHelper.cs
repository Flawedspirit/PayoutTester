using System;
using System.Linq;

namespace PayoutTester
{
    class InterfaceHelper
    {
        public static void WriteLine(string print, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(print);
            Console.ForegroundColor = prevColor;
        }

        public static void Write(string print, ConsoleColor color = ConsoleColor.White)
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

        public static void PrintFooter()
        {
            string[] flags = new string[] {
                (Program.flagDebug ? "[D]" : null),
                (Program.flagEasyMode ? "[E]" : null),
                (Program.flagWillPass ? "[P]" : null),
                "[M:" + (Program.flagEasyMode ? "5" : Program.flagMin.ToString()) + "]",
                "[X:" + Program.flagMax + "]"
            };

            string flagBuilder = String.Join(" ", flags.Where(s => !String.IsNullOrEmpty(s)));

            Console.SetCursorPosition(0, Console.WindowHeight -2);
            InterfaceHelper.WriteLine(String.Format("Flags: {0}", flagBuilder).TrimEnd(), ConsoleColor.DarkGray);
        }
    }
}
