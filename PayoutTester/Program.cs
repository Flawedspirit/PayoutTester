using System;
using System.Diagnostics;
using System.Reflection;

namespace PayoutTester
{
    class Program
    {
        //Version info
        private static string asm = Assembly.GetExecutingAssembly().Location;
        private static FileVersionInfo fv = FileVersionInfo.GetVersionInfo(asm);

        public static string VERSION = String.Format("v{0}", fv.FileVersion.ToString());

        //Program Flags
        public static bool flagSixToFive = false;
        public static bool flagBlackjackOnly = false;
        public static bool flagDebug = false;
        public static bool flagVerboseDebug = false;
        public static bool flagEasyMode = false;
        public static bool flagWillPass = false;
        public static double flagMin = 2.00;
        public static double flagMax = 100.00;

        //Running State Variables
        public static int headerState = 0;

        //Objects
        private static Random random = new Random();

        static void Main(string[] args)
        {
            ArgumentHelper.ParseArguments(args);

            decimal thisBet = GenerateBet();
            decimal payout = 0;
            decimal entry = 0;
            string payoutStr = "";

            bool running = true;
            bool wasCorrect = false;

            int payoutType = GeneratePayoutType();

            while (running)
            {
                Console.Clear();

                InterfaceHelper.PrintHeader();

                if (flagDebug) InterfaceHelper.PrintFooter();

                Console.SetCursorPosition(0, 4);

                if (wasCorrect)
                {
                    thisBet = GenerateBet();
                    payoutType = GeneratePayoutType();
                }

                switch (payoutType)
                {
                    case 0:
                        payout = GenerateBlackjackPayout(thisBet);
                        payoutStr = (flagSixToFive) ? "blackjack (6:5)" : "blackjack (3:2)";
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

                Console.Write(String.Format("${0} bet, paying out a {1}.", thisBet.ToString("N2"), payoutStr));

                if (flagVerboseDebug) InterfaceHelper.Write(" Debug: " + payout, ConsoleColor.DarkGray);

                Console.WriteLine("\n");
                Console.Write("Enter the expected payout: ");

                try
                {
                    entry = Decimal.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is ArgumentNullException)
                    {
                        InterfaceHelper.WriteLine("[!] Please enter a valid number. Press any key to try again.", ConsoleColor.Red);
                        wasCorrect = false;
                    }

                    //Wait for user to acknowledge exceptions raised
                    //Needed so user can actually read this message
                    Console.ReadKey();
                    continue;
                }

                if (entry < 0.00M)
                {
                    InterfaceHelper.WriteLine("[!] Please enter a positive number. Press any key to try again.", ConsoleColor.Red);
                    wasCorrect = false;
                }

                if (Decimal.Compare(payout, entry) == 0)
                {
                    InterfaceHelper.WriteLine("[\u221a] Correct! Press any key to do another one.", ConsoleColor.Green);

                    wasCorrect = true;
                }
                else
                {
                    InterfaceHelper.WriteLine("[x]That was not correct. Press any key to try again.", ConsoleColor.Red);

                    wasCorrect = flagWillPass;
                }

                Console.ReadKey();
            }
        }

        private static decimal GenerateBet()
        {
            decimal randomBet = (decimal)(random.NextDouble() * (flagMax - flagMin) + flagMin);

            //Return only numbers divisible by 5 when easymode flag is set
            //Otherwise, any number in the range is fair game
            return Math.Ceiling((flagEasyMode) ? Math.Max(5 * Math.Floor(Math.Round(randomBet / 5)), 5) : randomBet);
        }

        private static int GeneratePayoutType()
        {
            //0 = blackjack; 1 = mixed pair; 2 = coloured pair; 3 = perfect pair
            return (flagBlackjackOnly) ? 0 : random.Next(3) + 1;  //The +1 is an offset since Next() is max exclusive
        }

        private static decimal GenerateBlackjackPayout(decimal bet)
        {
            if (flagSixToFive)
            {
                decimal remainder = bet % 5;

                if (remainder != 0) bet -= remainder;

                switch (remainder)
                {
                    case 1.0M:
                        remainder = 1.25M;
                        break;
                    case 2.0M:
                        remainder = 2.5M;
                        break;
                    case 3.0M:
                        remainder = 3.75M;
                        break;
                    case 4.0M:
                        remainder = 5.0M;
                        break;
                        //end case
                }

                return (remainder != 0) ? (bet * 1.2M) + remainder : bet * 1.2M;
            }

            return bet * 1.5M;
        }
    }
}
