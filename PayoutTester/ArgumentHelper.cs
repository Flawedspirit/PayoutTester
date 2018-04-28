using System;
using System.Linq;

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

                if (param.ToLower().Contains("-h") || param.ToLower().Contains("-help"))
                {
                    PrintHelpScreen();
                    Console.Read();
                    Environment.Exit(0);
                }

                if (param.ToLower().Contains("-d") || param.ToLower().Contains("-debug"))
                {
                    Program.flagDebug = true;
                }

                if (param.ToLower().Contains("-e") || param.ToLower().Contains("-easymode"))

                {
                    Program.flagEasyMode = true;
                }

                if (param.ToLower().Contains("-m") || param.ToLower().Contains("-mode"))
                {
                    int modeParamIndex = Array.IndexOf(args, "-m");
                    int modeToken;

                    if (param.ToLower().Contains("-mode"))
                    {
                        modeParamIndex = Array.IndexOf(args, "-mode");
                    }

                    try
                    {
                        modeToken = Int32.Parse(args.GetValue(modeParamIndex + 1).ToString());

                        if (modeToken < 1 || modeToken > 15)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        Program.flagMode = modeToken;
                    }
                    catch (Exception ex)
                    {
                        if (ex is FormatException || ex is ArgumentNullException || ex is IndexOutOfRangeException)
                        {
                            InterfaceHelper.WriteLine("Mode must be a valid number. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                            Console.ReadKey();
                            Environment.Exit(1);
                        }
                        else if (ex is ArgumentOutOfRangeException || ex is OverflowException)
                        {
                            InterfaceHelper.WriteLine("Mode must be between 1 and 15. Press any key to close the program and correct the issue.", ConsoleColor.Red);
                            Console.ReadKey();
                            Environment.Exit(4);
                        }
                    }
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

        private static void PrintHelpScreen()
        {
            string optionString = "-6t5\t\t\tChanges blackjack payout to 6:5 payout mode.\n" +
                "-d debug\t\tShow extra debugging information in-app.\n" +
                "-e easymode\t\tGenerates bets that are divisible by 5 only. Overrides -min.\n" +
                "-h help\t\t\tPrints this help screen.\n" +
                "-o omit [bj|mp|cp|pp]\tSkip all blackjacks, mixed pairs, coloured pairs, and perfect pairs, respectively.\n" +
                "-p pass\t\t\tAutomatically pass and generate a new bet on incorrect answers.\n" +
                "-s min [number]\t\tSets the lower limit for the random number generation.\n" +
                "-x max [number]\t\tSets the upper limit for the random number generation.\n";
            Console.WriteLine("Usage: {0} {1}\n\nOptions:\n{2}", System.AppDomain.CurrentDomain.FriendlyName, "[-options]", optionString);
        }
    }
}
