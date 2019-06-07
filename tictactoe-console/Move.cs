using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe_console {
    class Move {
        public int x { get; private set; }
        public int y { get; private set; }

        public Move (int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
