using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Threading;

public enum State { Blank, X, O }

namespace tictactoe {
    public class Program {

        public static Game game = new Game(3, 15);

        private static void Main (string[] args) {
            Thread gameLoop = new Thread(new ThreadStart(MyMethod));
            gameLoop.Start();
        }

        private static void MyMethod () {
            while (true) {
                game.Display();
                var 
                game[game.Minimax(false).X ] = State.O;
                [game.Minimax(true)];
                
                Thread.Sleep(1000 * 5); // 2 seconds

                if (game.Finished)
                    break;
            }
        }
    }
}
