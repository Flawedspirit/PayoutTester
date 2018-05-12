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
                WriteLine("#");
            }

            Program.headerState++;

            if (Program.headerState < 2)
            {
                Console.SetCursorPosition(0, 1);
                Write("#");

                Console.SetCursorPosition(2, 1);
                Write("Payout Tester by Marc Chiarelli (c) " + DateTime.Now.Year, ConsoleColor.DarkCyan);

                Console.SetCursorPosition(Console.WindowWidth - ((Program.VERSION.Length) + 2), 1);
                Write(Program.VERSION, ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Console.WindowWidth - 1, 1);
                Write("#");

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
            string flagBuilder = "";

            if (Program.flagDebug)
            {
                string[] flags = new string[] {
                    (Program.flagSixToFive ? "[6t5]" : null),
                    (Program.flagBlackjackOnly ? "[B]" : null),
                    (Program.flagDebug ? "[D]" : null),
                    (Program.flagEasyMode ? "[E]" : null),
                    (Program.flagWillPass ? "[P]" : null),
                    "[S:" + (Program.flagEasyMode ? "5*" : Program.flagMin.ToString()) + "]",
                    "[X:" + Program.flagMax + "]"
                };

                flagBuilder = String.Join(" ", flags.Where(s => !String.IsNullOrEmpty(s)));
                flagBuilder = String.Format("Flags: {0} ", flagBuilder);

                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                WriteLine(flagBuilder, ConsoleColor.DarkGray);
            }

            Console.SetCursorPosition(flagBuilder.Length, Console.WindowHeight - 2);
            Write("Correct/Incorrect guesses: ");
            Write(Program.numCorrect.ToString(), ConsoleColor.Green);
            Write(" / ");
            Write(Program.numIncorrect.ToString() + " ", ConsoleColor.Red);
        }
    }
}
