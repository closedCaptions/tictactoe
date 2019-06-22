using System;
using System.Numerics;

public enum State { Blank, X, O }

/**
 * NOTE:
 * X is minimizer
 * O is maximizer
 */

namespace tictactoe {

    internal class Program {

        private static void Main (string[] args) {
            Game game = new Game(3, int.MaxValue);
            game.Introduce();
            game.Display();

            while (!game.Finished) {
                Vector2 gameCoords = GameInput(game, (x) => x - 1);
                game[(int) gameCoords.Y, (int) gameCoords.X] = State.X;
                Console.Clear();
                game.Display();

                if (game.Finished) // Check if finished to avoid out of bounds
                    break;

                game[game.Minimax(true)] = State.O;
                Console.Clear();
                game.Display();

                if (game.Finished) // Check if finished to avoid out of bounds
                    break;
            }

            if (game.Winning(State.X)) { // Report win for X
                Console.WriteLine("X wins!");
            } else if (game.Winning(State.O)) { // Report win for O
                Console.WriteLine("O wins!");
            } else { // Report draw
                Console.WriteLine("Draw!");
            }

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        // Exclusive to the game input in Program.cs : 23
        private static Vector2 GameInput (Game game, IntOperation operation) {
            int X = "Input X value:".AskInput(game.nLength, operation);
            int Y = "Input Y value:".AskInput(game.nLength, operation);

            if (game[Y, X] != State.Blank) {
                Console.WriteLine("\nThat slot is already filled\n");
                return GameInput(game, operation);
            } else {
                return new Vector2(X, Y);
            }
        }
    }
}