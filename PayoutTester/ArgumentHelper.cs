using System;

namespace PayoutTester
{
    class ArgumentHelper
    {
        public static void ParseArguments(String[] args)
        {
            foreach (string param in args)
            {
                if (param.ToLower().Contains("-6t5"))
                {
                    Program.flagSixToFive = true;
                }

                if (param.ToLower().Contains("-bjo"))
                {
                    Program.flagBlackjackOnly = true;
                }

                if (param.ToLower().Contains("-h") || param.ToLower().Contains("-help"))
                {
                    PrintHelpScreen();
                    Environment.Exit(0);
                }

                if (param.ToLower().Contains("-d") || param.ToLower().Contains("-debug"))
                {
                    Program.flagDebug = true;
                }

                if (param.ToLower().Contains("-dd"))
                {
                    Program.flagVerboseDebug = true;
                }

                if (param.ToLower().Contains("-e") || param.ToLower().Contains("-easymode"))

                {
                    Program.flagEasyMode = true;
                }

                if (param.ToLower().Contains("-p") || param.ToLower().Contains("-pass"))
                {
                    Program.flagWillPass = true;
                }

                if (param.ToLower().Contains("-s") || param.ToLower().Contains("-min"))
                {
                    int minParamIndex = Array.IndexOf(args, "-s");

                    if (param.ToLower().Contains("-min"))
                    {
                        minParamIndex = Array.IndexOf(args, "-min");
                    }

                    try
                    {
                        Program.flagMin = Double.Parse(args.GetValue(minParamIndex + 1).ToString());
                    }
                    catch (Exception ex)
                    {
                        if (ex is FormatException || ex is ArgumentNullException)
                        {
                            InterfaceHelper.WriteLine("Minimum bound must be a number. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                            Console.ReadKey();
                            Environment.Exit(1);
                        }
                    }
                }

                if (param.ToLower().Contains("-x") || param.ToLower().Contains("-max"))
                {
                    int maxParamIndex = Array.IndexOf(args, "-x");

                    if (param.ToLower().Contains("-max"))
                    {
                        maxParamIndex = Array.IndexOf(args, "-max");
                    }

                    try
                    {
                        Program.flagMax = Double.Parse(args.GetValue(maxParamIndex + 1).ToString());
                    }
                    catch (Exception ex)
                    {
                        if (ex is FormatException || ex is ArgumentNullException)
                        {
                            InterfaceHelper.WriteLine("Maximum bound must be a number. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                            Console.ReadKey();
                            Environment.Exit(2);
                        }
                    }
                }
            }
        }

        public static void PrintHelpScreen()
        {
            string optionString = "-6t5\t\tChanges blackjack payout to 6:5 payout mode.\n" +
                "-bjo\t\tCauses the program to generate only blackjack payouts.\n" +
                "-d debug\tShow extra debugging information in-app.\n" +
                "-dd\t\tShow more verbose debugging information, including the answer.\n" +
                "-e easymode\tGenerates bets that are divisible by 5 only. Overrides -min.\n" +
                "-h help\t\tPrints this help screen.\n" +
                "-p pass\t\tAutomatically pass and generate a new bet on incorrect answers.\n" +
                "-s min [number]\tSets the lower limit for the random number generation.\n" +
                "-x max [number]\tSets the upper limit for the random number generation.\n";

            string debugHelpString = "If debug (-d) or verbose debug (-dd) mode is activated, a line of flags that are active will be shown\n" +
                "on the bottom of the screen. These flags, from left to right, correspond to:\n\n" +
                "[6t5]\t\tBlackjack payouts are calculated in 6:5 mode instead of 3:2.\n" +
                "[B]\t\tOnly blackjack payouts are generated.\n" +
                "[D]\t\tDebug or verbose debug mode is active.\n" +
                "[E]\t\tEasy mode is on. All bets will be in multiples of 5 only.\n" +
                "[P]\t\tA new bet will be generated on both correct and incorrect guesses.\n" +
                "[S:n]\t\tIndicates the random number generator's lower limit.\n" +
                "[X:n]\t\tIndicates the random number generator's upper limit.\n";

            string aboutString = "This program is licensed under the MIT License. Please see LICENSE in source for details.\n" +
                "Source is available at https://github.com/Flawedspirit/PayoutTester.\n";

            Console.WriteLine("Usage: {0} {1}\n\nOptions:\n{2}\nDebug Flags:\n{3}",
                System.AppDomain.CurrentDomain.FriendlyName,
                "[-options]",
                optionString,
                debugHelpString
            );

            InterfaceHelper.Write("Payout Tester ", ConsoleColor.DarkCyan);
            InterfaceHelper.Write(Program.VERSION + "\n\n");

            Console.WriteLine(aboutString);
        }
    }
}
