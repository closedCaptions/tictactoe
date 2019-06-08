using System;
using System.Collections.Generic;
using System.Text;

public enum State { Blank, X, O }

namespace tictactoe {
    public class Program {

        static void Main (string[] args) {
            Game game = new Game(3, 3, 3, 20, State.Blank);
            game.Display();
        }
    }
}
