using System;
using System.Collections;
using System.Collections.Generic;

namespace PayoutTester {
    class ChipDisplayHelper {
        public static void DrawBetInChips(decimal bet) {
            //In order of $5000, $1000, $500, $100, $25, $5, $2.50, $1 values
            int[] numChips = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            var chips = new List<Chip>
            {
                { new Chip() { Value = 5000M, Color = ConsoleColor.DarkGray, Icon = "\u25cf" } },
                { new Chip() { Value = 1000M, Color = ConsoleColor.DarkYellow, Icon = "\u25cf" } },
                { new Chip() { Value = 500M, Color = ConsoleColor.DarkMagenta, Icon = "\u25cf" } },
                { new Chip() { Value = 100M, Color = ConsoleColor.White, Icon = "\u25cb" } },
                { new Chip() { Value = 25M, Color = ConsoleColor.DarkGreen, Icon = "\u25cf" } },
                { new Chip() { Value = 5M, Color = ConsoleColor.DarkRed, Icon = "\u25cf" } },
                { new Chip() { Value = 2.5M, Color = ConsoleColor.Magenta, Icon = "\u25cf" } },
                { new Chip() { Value = 1M, Color = ConsoleColor.White, Icon = "\u25cf" } }
            };

            decimal remainder = 0;
            int index = 0;

            foreach (Chip chip in chips) {
                numChips[index] = Decimal.ToInt32(bet / chip.Value);
                remainder = (numChips[index] > 0) ? bet %= chip.Value : bet;
                index++;
            }

            index = 0;
            foreach (Chip chip in chips) {
                for (int i = 0; i < numChips[index]; i++) {
                    InterfaceHelper.Write(chip.Icon, chip.Color);
                }

                index++;
            }
        }

        private class Chip : IEnumerable<Chip> {
            public decimal Value { get; set; }
            public ConsoleColor Color { get; set; }
            public string Icon { get; set; }

            public IEnumerator<Chip> GetEnumerator() {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                throw new NotImplementedException();
            }
        }
    }
}
