using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Threading;

public enum State { Blank, X, O }
/**
 * NOTE:
 * X is minimizer
 * O is maximizer
 */

namespace tictactoe {
    class Program {

        private static Game game = new Game(3, int.MaxValue);

        private static void Main (string[] args) {
            Thread gameLoop = new Thread(new ThreadStart(ThreadUpdate));
            game.Display();
            gameLoop.Start();
        }

        private static void ThreadUpdate () {
            while (true) {
                int X = Utility.AskInput("\nInput X value:", "Try again, integer only\n") - 1;
                int Y = Utility.AskInput("\nInput Y value:", "Try again, integer only\n") - 1;
                if (game[Y, X] != State.Blank) {
                    // TODO: Add stuff here that will ask again if player is out of bounds
                }
                game[Y, X] = State.X;
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

            if (game.Winning(State.X)) // Report win for X
                Console.WriteLine("X wins!");
            else if (game.Winning(State.O)) // Report win for O
                Console.WriteLine("O wins!");
            else // Report draw
                Console.WriteLine("Draw!");
        }
    }
}
