using System;

namespace PayoutTester
{
    class ArgumentHelper
    {
        public static void ParseArguments(String[] args)
        {
            if (args.Length > 0)
            {
                foreach (string param in args)
                {
                    try
                    {
                        if (param.ToLower().Equals("-6t5"))
                        {
                            Program.flagSixToFive = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-bjo"))
                        {
                            Program.flagBlackjackOnly = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-h") || param.ToLower().Equals("-help"))
                        {
                            PrintHelpScreen();
                            Environment.Exit(0);
                        }

                        if (param.ToLower().Equals("-d") || param.ToLower().Equals("-debug"))
                        {
                            Program.flagDebug = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-dd"))
                        {
                            Program.flagVerboseDebug = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-e") || param.ToLower().Equals("-easymode"))

                        {
                            Program.flagMin = 5.00;
                            Program.flagEasyMode = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-p") || param.ToLower().Equals("-pass"))
                        {
                            Program.flagWillPass = true;
                            continue;
                        }

                        if (param.ToLower().Equals("-m") || param.ToLower().Equals("-min"))
                        {
                            int minParamIndex = Array.IndexOf(args, "-m");

                            if (param.ToLower().Equals("-min"))
                            {
                                minParamIndex = Array.IndexOf(args, "-min");
                            }

                            try
                            {
                                Program.flagMin = Double.Parse(args.GetValue(minParamIndex + 1).ToString());

                                if (Program.flagMin >= Program.flagMax)
                                {
                                    throw new ArgumentOutOfRangeException();
                                }

                                continue;
                            }
                            catch (Exception ex)
                            {
                                if (ex is FormatException || ex is ArgumentNullException)
                                {
                                    InterfaceHelper.WriteLine("Minimum bound must be a number. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                                }

                                if (ex is ArgumentOutOfRangeException)
                                {
                                    InterfaceHelper.WriteLine("Minimum bound must be a lower than the maximum bound. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                                }

                                Console.ReadKey();
                                Environment.Exit(1);
                            }
                        }

                        if (param.ToLower().Equals("-x") || param.ToLower().Equals("-max"))
                        {
                            int maxParamIndex = Array.IndexOf(args, "-x");

                            if (param.ToLower().Equals("-max"))
                            {
                                maxParamIndex = Array.IndexOf(args, "-max");
                            }

                            try
                            {
                                Program.flagMax = Double.Parse(args.GetValue(maxParamIndex + 1).ToString());

                                if (Program.flagMax <= Program.flagMin)
                                {
                                    throw new ArgumentOutOfRangeException();
                                }

                                continue;
                            }
                            catch (Exception ex)
                            {
                                if (ex is FormatException || ex is ArgumentNullException)
                                {
                                    InterfaceHelper.WriteLine("Maximum bound must be a number. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                                }

                                if (ex is ArgumentOutOfRangeException)
                                {
                                    InterfaceHelper.WriteLine("Maximum bound must be a greater than the minimum bound. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                                }

                                Console.ReadKey();
                                Environment.Exit(2);
                            }
                        }

                        if (Decimal.TryParse(param.ToString(), out decimal paramVal))
                        {
                            //If an argument is a number, skip over it because it is passed in by another argument and parsed there
                            continue;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    catch
                    {
                        InterfaceHelper.WriteLine("Invalid argument: " + param + ". Type -h or -help to see the help screen.", ConsoleColor.Red);
                        Console.ReadKey();
                        Environment.Exit(4);
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
                "-m min [number]\tSets the lower limit for the random number generation.\n" +
                "-x max [number]\tSets the upper limit for the random number generation.\n";

            string debugHelpString = "If debug (-d) or verbose debug (-dd) mode is activated, a line of flags that are active will be shown\n" +
                "on the bottom of the screen. These flags, from left to right, correspond to:\n\n" +
                "[6t5]\t\tBlackjack payouts are calculated in 6:5 mode instead of 3:2.\n" +
                "[B]\t\tOnly blackjack payouts are generated.\n" +
                "[D]\t\tDebug or verbose debug mode is active.\n" +
                "[E]\t\tEasy mode is on. All bets will be in multiples of 5 only.\n" +
                "[P]\t\tA new bet will be generated on both correct and incorrect guesses.\n" +
                "[M:n]\t\tIndicates the random number generator's lower limit.\n" +
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

            InterfaceHelper.WriteLine(aboutString);
        }
    }
}
