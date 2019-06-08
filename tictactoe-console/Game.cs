using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace tictactoe {
    public class Game {

        #region Variable

        #region 2D Array - `board`
        private readonly State[,] board;
        public State this[int y, int x] { // Declare indexer
            get {
                return board[y, x];
            }
            set {
                latestMove = new Vector2(x, y);
                board[y, x] = value;
            }
        }

        public State this[Vector2 index] { // Declare indexer
            get {
                return this[(int)index.Y, (int)index.X];
            }
            set {
                this[(int) index.Y, (int) index.X] = value;
            }
        }

        public int GetLength (int dimension) {
            return board.GetLength(dimension);
        }
        #endregion

        private int maxDepth;
        private Vector2 latestMove;

        public bool Finished {
            get {
                return GetAvailSlots().Count == 0 || Winning(State.O) || Winning(State.X);
            }
        }
        public int nLength;

        #endregion

        #region Constructor

        public Game (int n, int maxDepth) {
            board = new State[n, n];
            board.Populate(State.Blank);

            nLength = n;
            this.maxDepth = maxDepth;
        }

        #endregion

        #region Method

        public void Display () {
            for (int y = 0; y < this.GetLength(0); y++) {
                if (y != 0)
                    Console.Write("--- --- ---\n");
                for (int x = 0; x < this.GetLength(1); x++) {
                    string toWrite = " ";
                    switch (this[y, x]) {
                        case State.O:
                            toWrite = "O";
                            break;
                        case State.X:
                            toWrite = "X";
                            break;
                    }
                    Console.Write(" {0} ", toWrite);
                    if (x != this.GetLength(1) - 1)
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        #region Minimax

        public Vector3 Minimax (bool maximizing) {
            return Minimax(this, maxDepth, maximizing, GetAvailSlots());
        }

        private Vector3 Minimax (Game game, int depth, bool maximizing, List<Vector2> availSlots) {
            /**
             * x = x index
             * y = y index
             * z = score
             */

            if (game.GetScore(depth) != 0 || availSlots.Count == 0)
                return new Vector3(-1, -1, GetScore(depth));

            List<Vector3> moveNodes = new List<Vector3>();
            // Find pausible moves
            foreach (Vector2 slot in availSlots) {
                Vector2[] tmp = new Vector2[availSlots.Count];
                availSlots.CopyTo(tmp);
                List<Vector2> copy = tmp.ToList();
                copy.Remove(slot);
                game[slot] = (maximizing ? State.O : State.X);

                moveNodes.Add(new Vector3(slot, Minimax (game, depth - 1, !maximizing, copy).Z));

                game[slot] = State.Blank; // reset back to blank
                game.latestMove = this.latestMove;
            }

            // Actual minimax
            if (maximizing) { // Bot is playing
                return moveNodes.AsEnumerable().Aggregate((acc, val) => (val.Z > acc.Z ? val : acc));
            } else { // Human is playing
                return moveNodes.AsEnumerable().Aggregate((acc, val) => (val.Z < acc.Z ? val : acc));
            }
        }

        #region Helper method

        #region Winning method

        public bool Winning (State state) {
            return WinCol(state) || WinRow(state) || WinDiag(state) || WinAntiDiag(state);
        }

        private bool WinCol (State state) {
            int winSum = 0;
            for (int i = 0; i < this.nLength; i++) {
                if (winSum == this.nLength)
                    return true;

                if (this[i, (int)latestMove.X] != state) {
                    winSum = 0;
                }else {
                    winSum++;
                }
            }

            return false;
        }

        private bool WinRow (State state) {
            int winSum = 0;
            for (int i = 0; i < this.nLength; i++) {
                if (winSum == this.nLength)
                    return true;

                if (this[(int)latestMove.Y, i] != state) {
                    winSum = 0;
                } else {
                    winSum++;
                }
            }

            return false;
        }

        private bool WinDiag (State state) {
            int winSum = 0;
            for (int i = 0; i < this.nLength; i++) {
                if (winSum == this.nLength)
                    return true;

                if (this[i, i] != state) {
                    winSum = 0;
                } else {
                    winSum++;
                }
            }

            return false;
        }

        private bool WinAntiDiag (State state) {
            int winSum = 0;
            for (int i = 0; i < this.nLength; i++) {
                if (winSum == this.nLength)
                    return true;

                if (this[i, (this.nLength - 1) - i] != state) {
                    winSum = 0;
                } else {
                    winSum++;
                }
            }

            return false;
        }

        #endregion

        private int GetScore (int depth) {
            int toSubtract = (maxDepth - depth);

            if (Winning(State.O)) { // Bot wins
                return int.MaxValue - toSubtract;
            }else if (Winning(State.X)) { // Human wins
                return int.MinValue + toSubtract;
            }else { // Round draw (No available slots) or depth is zero
                return 0;
            }

        }

        private List<Vector2> GetAvailSlots () {
            List<Vector2> dest = new List<Vector2>();

            for (int y = 0; y < this.GetLength(0); y++) {
                for (int x = 0; x < this.GetLength(1); x++) {
                    if (this[y, x] == State.Blank)
                        dest.Add(new Vector2(x, y));
                }
            }

            return dest;
        }

        #endregion

        #endregion

        #endregion

    }

    static class Utility {

        public delegate bool ReturnBool <T> (T value);

        public static void Populate<T> (this T[,] array, T populateValue) {
            for (int y = 0; y < array.GetLength(0); ++y) {
                for (int x = 0; x < array.GetLength(1); ++x) {
                    array[y, x] = populateValue;
                }
            }
        }
    }
}
