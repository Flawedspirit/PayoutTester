using System;

namespace PayoutTester
{
    class Program
    {
        //Constants
        const String VERSION = "v1.0";
        const double MIN = 2.00;
        const double MAX = 100.00;

        //Running Variables
        static int headerState = 0;

        //Objects
        static Random random = new Random();

        static void Main(string[] args)
        {
            decimal thisBet = GenerateBet();
            decimal payout = 0;
            decimal entry = 0;
            string payoutStr = "";

            bool running = true;
            bool wasCorrect = false;

            //0 = blackjack; 1 = mixed pair; 2 = coloured pair; 3 = perfect pair
            int payoutType = random.Next(4);

            while (running)
            {
                Console.Clear();

                PrintHeader();

                if(wasCorrect)
                {
                    //If the last round was correct, generate new values
                    // If not, re-use the same until it's right
                    thisBet = GenerateBet();
                    payoutType = random.Next(4);
                }
                
                switch (payoutType)
                {
                    case 0:
                        payout = thisBet * 1.5M;
                        payoutStr = "blackjack (3:2)";
                        break;
                    case 1:
                        payout = thisBet * 6M;
                        payoutStr = "mixed pair (6:1)";
                        break;
                    case 2:
                        payout = thisBet * 12M;
                        payoutStr = "coloured pair (12:1)";
                        break;
                    case 3:
                        payout = thisBet * 25M;
                        payoutStr = "perfect pair (25:1)";
                        break;
                    //end case
                }

                Console.WriteLine(String.Format("${0} bet, paying out a {1}.", thisBet.ToString("N2"), payoutStr));
                Console.WriteLine("");

                Console.Write("Enter the expected payout: ");

                try
                {
                    entry = Decimal.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    if(ex is FormatException || ex is ArgumentNullException)
                    {
                        WriteLine("[!] Please enter a valid number. Press any key to try again.", ConsoleColor.Red);
                        wasCorrect = false;
                    }

                    //Wait for user to acknowledge exceptions raised
                    //Needed so user can actually read this message
                    Console.ReadKey();
                    continue;
                }

                if (entry < 0.00M)
                {
                    WriteLine("[!] Please enter a positive number. Press any key to try again.", ConsoleColor.Red);
                    wasCorrect = false;
                }

                if(Decimal.Compare(payout, entry) == 0)
                {
                    WriteLine("[\u221a] Correct! Press any key to do another one.", ConsoleColor.Green);

                    wasCorrect = true;
                } else
                {
                    WriteLine("[x]That was not correct. Press any key to try again.", ConsoleColor.Red);
                    wasCorrect = false;
                }

                Console.ReadKey();
            }
        }

        static void WriteLine(String print, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(print);
            Console.ForegroundColor = prevColor;
        }

        static void Write(String print, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(print);
            Console.ForegroundColor = prevColor;
        }

        static decimal GenerateBet()
        {
            return (decimal)Math.Round((random.NextDouble() * (MAX - MIN) + MIN), 2, MidpointRounding.AwayFromZero);
        }

        static void PrintHeader()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, headerState);
                Console.WriteLine("#");
            }

            headerState++;

            if(headerState < 2)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write("#");

                Console.SetCursorPosition(2, 1);
                Write("Payout Tester by Marc Chiarelli (c) " + DateTime.Now.Year, ConsoleColor.DarkCyan);

                Console.SetCursorPosition(Console.WindowWidth - ((VERSION.Length * 2) -1), 1);
                Write(VERSION, ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(Console.WindowWidth - 1, 1);
                Console.Write("#");

                headerState++;
            } else
            {
                Console.SetCursorPosition(0, headerState + 1);
                return;
            }

            PrintHeader();
            headerState = 0;
        }
    }
}
