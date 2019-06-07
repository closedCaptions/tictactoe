using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe {
    public class Move {
        public int x { get; }
        public int y { get; }

        public Move (int y, int x) {
            this.y = y;
            this.x = x;
        }
    }
}
