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

                if (param.ToLower().Contains("-e") || param.ToLower().Contains("-easymode"))
                {
                    Program.flagEasyMode = true;
                }

                if (param.ToLower().Contains("-p") || param.ToLower().Contains("-pass"))
                {
                    Program.flagWillPass = true;
                }
            }
        }

        private static void PrintHelpScreen()
        {
            string optionString = "-d debug\tShow extra debugging information in-app.\n" +
                "-e easymode\tGenerates bets that are divisible by 5 only.\n" +
                "-m min [number]\tSets the lower limit for the random number generation.\n" +
                "-p pass\t\tAutomatically pass and generate a new bet on incorrect answers.\n" +
                "-x max [number]\tSets the upper limit for the random number generation.\n" +
                "-? help\t\tPrints this help screen.\n";
            Console.WriteLine("Usage: {0} {1}\n\nOptions:\n{2}", System.AppDomain.CurrentDomain.FriendlyName, "[-options]", optionString);
        }
    }
}
