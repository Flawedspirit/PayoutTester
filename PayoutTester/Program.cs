using System;
using System.Diagnostics;
using System.Reflection;

namespace PayoutTester
{
    class Program
    {
        //Version info
        private static String asm = Assembly.GetExecutingAssembly().Location;
        private static FileVersionInfo fv = FileVersionInfo.GetVersionInfo(asm);

        public static String VERSION = String.Format("v{0}", fv.FileVersion.ToString());

        //Program Flags
        public static bool flagEasyMode = false;
        public static bool flagWillPass = false;
        public static double flagMin = 2.00;
        public static double flagMax = 100.00;

        //Running Variables
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

            //0 = blackjack; 1 = mixed pair; 2 = coloured pair; 3 = perfect pair
            int payoutType = random.Next(3) + 1; //Offset to ensure the right range is generated

            while (running)
            {
                Console.Clear();

                InterfaceHelper.PrintHeader();

                if(wasCorrect)
                {
                    //If the last round was correct, generate new values
                    // If not, re-use the same until it's right
                    thisBet = GenerateBet();
                    payoutType = random.Next(3) + 1; //Offset to ensure the right range is generated
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

                if(Decimal.Compare(payout, entry) == 0)
                {
                    InterfaceHelper.WriteLine("[\u221a] Correct! Press any key to do another one.", ConsoleColor.Green);

                    wasCorrect = true;
                } else
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


    }
}
