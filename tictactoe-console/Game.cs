namespace tictactoe {
    public class Game {

        #region Variable

        private Board board;
        public Move latestMove { get; private set; } = new Move (-1, -1);
        public int cellsInRow;
        public int maxDepth;

        #region Indexer

        public State this[int y, int x] {
            get {
                return board[y, x];
            }
            set {
                latestMove = new Move(y, x);
                board[y, x] = value;
            }
        }

        public State this[Move move] {
            get {
                return board[move.y, move.x];
            }
            set {
                latestMove = move;
                board[move.y, move.x] = value;
            }
        }

        #endregion

        #endregion

        #region Constructor

        public Game (int height, int length, int cellsInRow, int maxDepth, State state) {
            this.maxDepth = maxDepth;
            this.cellsInRow = cellsInRow;
            board = new Board(height, length, state);
        }

        #endregion

        #region Method

        public void Display () {
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
        // Bot is maximizing = O
        public int Minimax (Game game, int depth, bool maximizing) {
            game.latestMove = this.latestMove;  // At first round will be setting the same

            if (Winning(State.O)) {
                return maxDepth - (maxDepth - depth);
            }

            return 0;
        }

        private bool Winning (State state) {
            // Uses latest move instead of checking each cell for performance
            return WinDiagonal(latestMove, state) || WinVertical(latestMove, state) || WinHorizontal(latestMove, state);
        }

        private bool WinDiagonal (Move latestMove, State state) {
            int trueCount = 0;
            for (int i = -3; i < 4; i++) {
                if (trueCount == cellsInRow)
                    return true;

                try {   // Catch `IndexOutOfRangeException`
                    if (this[latestMove.y + i, latestMove.x + i] == state)
                        trueCount++;
                    else
                        trueCount = 0;
                }
                catch {
                    continue;
                }
            }

            return false;
        }

        private bool WinVertical (Move latestMove, State state) {
            int trueCount = 0;
            for (int i = -3; i < 4; i++) {
                if (trueCount == cellsInRow)
                    return true;

                try {   // Catch `IndexOutOfRangeException`
                    if (this[latestMove.y + i, latestMove.x] == state)
                        trueCount++;
                    else
                        trueCount = 0;
                }
                catch {
                    continue;
                }
            }

            return false;
        }

        private bool WinHorizontal (Move latestMove, State state) {
            int trueCount = 0;
            for (int i = -3; i < 4; i++) {
                if (trueCount == cellsInRow)
                    return true;

                try {   // Catch `IndexOutOfRangeException`
                    if (this[latestMove.y, latestMove.x + i] == state)
                        trueCount++;
                    else
                        trueCount = 0;
                }
                catch {
                    continue;
                }
            }

            return false;
        }

        #endregion

        #endregion
    }
}
