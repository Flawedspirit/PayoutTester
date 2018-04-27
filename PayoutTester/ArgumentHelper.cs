using System;

namespace PayoutTester
{
    class ArgumentHelper
    {
        public static void ParseArguments(String[] args)
        {
            foreach(string param in args)
            {
                if (param.ToLower().Contains("-?") || param.ToLower().Contains("-help"))
                {
                    PrintHelpScreen();
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

                if (param.ToLower().Contains("-p") || param.ToLower().Contains("-pass"))
                {
                    Program.flagWillPass = true;
                }

                if (param.ToLower().Contains("-m") || param.ToLower().Contains("-min"))
                {
                    int minParamIndex = Array.IndexOf(args, "-m");
                    double minToken = 2.0;

                    if (param.ToLower().Contains("-min"))
                    {
                        minParamIndex = Array.IndexOf(args, "-min");
                    }

                    try
                    {
                        minToken = Double.Parse(args.GetValue(minParamIndex + 1).ToString());
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

                    Program.flagMin = minToken;
                }

                if (param.ToLower().Contains("-x") || param.ToLower().Contains("-max"))
                {
                    int maxParamIndex = Array.IndexOf(args, "-x");
                    double maxToken = 100.0;

                    if (param.ToLower().Contains("-max"))
                    {
                        maxParamIndex = Array.IndexOf(args, "-max");
                    }

                    try
                    {
                        maxToken = Double.Parse(args.GetValue(maxParamIndex + 1).ToString());
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

                    Program.flagMax = maxToken;
                }
            }
        }

        private static void PrintHelpScreen()
        {
            string optionString = "-d debug\tShow extra debugging information in-app.\n" +
                "-e easymode\tGenerates bets that are divisible by 5 only. Overrides -min.\n" +
                "-m min [number]\tSets the lower limit for the random number generation.\n" +
                "-p pass\t\tAutomatically pass and generate a new bet on incorrect answers.\n" +
                "-x max [number]\tSets the upper limit for the random number generation.\n" +
                "-? help\t\tPrints this help screen.\n";
            Console.WriteLine("Usage: {0} {1}\n\nOptions:\n{2}", System.AppDomain.CurrentDomain.FriendlyName, "[-options]", optionString);
        }
    }
}
