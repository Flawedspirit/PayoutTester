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
        public static bool flagDebug = false;
        public static bool flagEasyMode = false;
        public static bool flagWillPass = false;
        public static int flagMode = 15;
        public static double flagMin = 2.00;
        public static double flagMax = 100.00;

        //Running State Variables
        public static int headerState = 0;

        //Objects
        static Random random = new Random();

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

                Console.WriteLine(String.Format("${0} bet, paying out a {1}. {2}", thisBet.ToString("N2"), payoutStr, (flagDebug) ? "Debug: " + payout : ""));
                Console.WriteLine("");

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

        static decimal GenerateBet()
        {
            decimal randomBet = (decimal)(random.NextDouble() * (flagMax - flagMin) + flagMin);

            //Return only numbers divisible by 5 when easymode flag is set
            //Otherwise, any number in the range is fair game
            return Math.Ceiling((flagEasyMode) ? Math.Max(5 * Math.Floor(Math.Round(randomBet / 5)), 5) : randomBet);
        }

        static int GeneratePayoutType()
        {
            int genRange = 0;

            if((flagMode & 0x4) >= 0x4)
            {
                genRange = 1;
            }
            if ((flagMode & 0x2) >= 0x2)
            {
                genRange = 2;
            }
            if ((flagMode & 0x1) >= 0x1)
            {
                genRange = 3;
            }

            Console.WriteLine(genRange);
            Console.ReadKey();

            //0 = blackjack; 1 = mixed pair; 2 = coloured pair; 3 = perfect pair
            return random.Next(genRange) + 1;  //The +1 is an offset since Next() is exclusive
        } 
    }
}
