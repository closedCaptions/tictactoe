using System;

namespace tictactoe {

    internal static class Utility {

        // Exclusive to the game input in Program.cs : 23 - 24
        public static int AskInput (this string msg, int boardLength, IntOperation operation) {
            Console.WriteLine(msg);
            int result;
            bool successful = int.TryParse(Console.ReadLine(), out result);
            result = operation(result);

            if (!successful)
                return "Integer only please".AskInput(boardLength, operation);
            else if (result < 0 || result >= boardLength)
                return "Integer is out of bounds".AskInput(boardLength, operation);
            else
                return result;
        }

        public static void Populate<T> (this T[,] array, T populateValue) {
            for (int y = 0; y < array.GetLength(0); ++y) {
                for (int x = 0; x < array.GetLength(1); ++x) {
                    array[y, x] = populateValue;
                }
            }
        }
    }
}