using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace tictactoe {
    public class Game {

        private readonly History history = new History();
        private Board board = new Board(3, 3, State.X);

        public State this[int y, int x] {
            get {
                return board[y, x];
            }
            set {
                history.Add(new Move(y, x));
                board[y, x] = value;
            }
        }

        public void DisplayBoard () {
            board.Display();
        }

        #region Minimax

        /* PESUDOCODE - SEBASTIAN LAGUE
         * 
         * function minimax(position, depth, maximizingPlayer)
         *        if depth == 0 or game over in position
         *            return static evaluation of position
         *
         *        if maximizingPlayer
         *            maxEval = -infinity
         *            for each child of position
         *                eval = minimax(child, depth - 1, false)
         *                maxEval = max(maxEval, eval)
         *            return maxEval
         *
         *        else
         *            minEval = +infinity
         *            for each child of position
         *                eval = minimax(child, depth - 1, true)
         *                minEval = min(minEval, eval)
         *            return minEval
         *
         *
         *  // initial call
         *  minimax(currentPosition, 3, true)
         * 
         */

        // Bot is maximizing
        public int Minimax (Game game, int depth, bool maximizing) {

            return 0;
        }

        #region Winning

        public bool Winning (Game game) {
            // Uses latest move
            return WinDiagonal() || WinVertical() || WinHorizontal();
        }

        public bool WinDiagonal () {
            return false;
        }

        public bool WinVertical () {
            return false;
        }

        public bool WinHorizontal () {
            return false;
        }

        #endregion

        #endregion
    }
}
