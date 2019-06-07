using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe {
    public class History {
        private List<Move> keeper = new List<Move>();

        public void Reset () {
            keeper.RemoveRange(0, keeper.Count);
        }

        public void Add (Move move) {
            keeper.Add(move);
        }
    }
}
