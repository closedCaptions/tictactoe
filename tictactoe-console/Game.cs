using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe {
    public class Game {
        

        Board board = new Board(3, 3, State.Blank);

        #region Minimax

        public int Minimax (Game game, int maxDepth, int depth, bool maximizing) {
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
            return 0;
        }

        #endregion
    }
}
