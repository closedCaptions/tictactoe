using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace tictactoe {

    public delegate int IntOperation (int value);

    internal class Game {

        #region Variables

        private readonly State[,] board;

        #region Board indexer

        public State this[int y, int x] {
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
                return this[(int) index.Y, (int) index.X];
            }
            set {
                this[(int) index.Y, (int) index.X] = value;
            }
        }

        public State this[Vector3 index] {
            get {
                return this[(int) index.Y, (int) index.X];
            }
            set {
                this[(int) index.Y, (int) index.X] = value;
            }
        }

        #endregion Board indexer

        private int maxDepth;
        private Vector2 latestMove;

        public bool Finished {
            get {
                return GetAvailSlots().Count == 0 || Winning(State.O) || Winning(State.X);
            }
        }

        public int nLength { get; }

        #endregion Variables

        #region Constructor

        public Game (int n, int maxDepth) {
            board = new State[n, n];
            board.Populate(State.Blank);

            nLength = n;
            this.maxDepth = maxDepth;
        }

        #endregion Constructor

        #region Basic Methods

        public void Introduce () {
            string logo = @"████████╗██╗ ██████╗    ████████╗ █████╗  ██████╗    ████████╗ ██████╗ ███████╗" + "\n" +
                          @"╚══██╔══╝██║██╔════╝    ╚══██╔══╝██╔══██╗██╔════╝    ╚══██╔══╝██╔═══██╗██╔════╝" + "\n" +
                          @"   ██║   ██║██║            ██║   ███████║██║            ██║   ██║   ██║█████╗" + "\n" +
                          @"   ██║   ██║██║            ██║   ██╔══██║██║            ██║   ██║   ██║██╔══╝" + "\n" +
                          @"   ██║   ██║╚██████╗       ██║   ██║  ██║╚██████╗       ██║   ╚██████╔╝███████╗" + "\n" +
                          @"   ╚═╝   ╚═╝ ╚═════╝       ╚═╝   ╚═╝  ╚═╝ ╚═════╝       ╚═╝    ╚═════╝ ╚══════╝" + "\n";
            Console.WriteLine(logo);
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
            Console.Clear();
        }

        public void Display () {
            for (int y = 0; y < this.nLength; y++) {
                if (y != 0) {
                    Console.WriteLine("   " + string.Join("--- ", new string[this.nLength + 1]));
                } else {
                    Console.WriteLine("    " + string.Join("   ", Enumerable.Range(1, this.nLength)));
                }

                Console.Write(" {0} ", y + 1);

                for (int x = 0; x < this.nLength; x++) {
                    string toWrite = "  ";
                    switch (this[y, x]) {
                        case State.O:
                            toWrite = "O ";
                            break;

                        case State.X:
                            toWrite = "X ";
                            break;
                    }
                    toWrite += (x != this.nLength - 1 ? "|" : "");

                    Console.Write($" {toWrite}");
                }
                Console.WriteLine();
            }
        }

        #endregion Basic Methods

        #region Minimax

        public Vector2 Minimax (bool maximizing) {
            List<Vector2> availSlots = GetAvailSlots();
            if (availSlots.Count == this.nLength * this.nLength) {  // He is first
                Random rnd = new Random();
                return new Vector2(rnd.Next(0, this.nLength), rnd.Next(0, this.nLength));
            } else {
                Vector3 bestChoice = Minimax(this, this.maxDepth, maximizing, availSlots);
                return new Vector2(bestChoice.X, bestChoice.Y);
            };
        }

        private Vector3 Minimax (Game game, int depth, bool maximizing, List<Vector2> availSlots) {
            if (game.GetScore(depth) != 0 || availSlots.Count == 0)
                return new Vector3(-1, -1, GetScore(depth));

            List<Vector3> moveNodes = new List<Vector3>();
            // Find possible moves
            foreach (Vector2 slot in availSlots) {
                Vector2[] tmp = new Vector2[availSlots.Count];
                availSlots.CopyTo(tmp);
                List<Vector2> copy = tmp.ToList();
                copy.Remove(slot);
                game[slot] = (maximizing ? State.O : State.X);

                moveNodes.Add(new Vector3(slot, Minimax(game, depth - 1, !maximizing, copy).Z));

                game[slot] = State.Blank; // Reset back to blank
            }

            // Actual comparing
            if (maximizing) { // O is playing
                return moveNodes.AsEnumerable().Aggregate((acc, val) => (val.Z > acc.Z ? val : acc));
            } else { // X is playing
                return moveNodes.AsEnumerable().Aggregate((acc, val) => (val.Z < acc.Z ? val : acc));
            }
        }

        #region Helper method

        #region Winning method

        public bool Winning (State state) {
            return WinCol(state) || WinRow(state) || WinDiag(state) || WinAntiDiag(state);
        }

        private bool WinCol (State state) {
            for (int i = 0; i < this.nLength; i++) {
                if (this[i, (int) latestMove.X] != state)
                    break;

                if (i == this.nLength - 1)
                    return true;
            }

            return false;
        }

        private bool WinRow (State state) {
            for (int i = 0; i < this.nLength; i++) {
                if (this[(int) latestMove.Y, i] != state)
                    break;

                if (i == this.nLength - 1)
                    return true;
            }

            return false;
        }

        private bool WinDiag (State state) {
            if (latestMove.X == latestMove.Y) {
                for (int i = 0; i < nLength; i++) {
                    if (this[i, i] != state)
                        break;

                    if (i == nLength - 1)
                        return true;
                }
            }

            return false;
        }

        private bool WinAntiDiag (State state) {
            for (int i = 0; i < nLength; i++) {
                if (this[nLength - 1 - i, i] != state)
                    break; // Will stop the i++ operation from happening

                if (i == nLength - 1)
                    return true;
            }

            return false;
        }

        #endregion Winning method

        private int GetScore (int depth) {
            int toSubtract = (maxDepth - depth);

            if (Winning(State.O)) { // Bot wins
                return int.MaxValue - toSubtract;
            } else if (Winning(State.X)) { // Human wins
                return int.MinValue + toSubtract;
            } else { // Round draw (No available slots) or depth is zero
                return 0;
            }
        }

        public List<Vector2> GetAvailSlots () {
            List<Vector2> dest = new List<Vector2>();

            for (int y = 0; y < this.nLength; y++) {
                for (int x = 0; x < this.nLength; x++) {
                    if (this[y, x] == State.Blank)
                        dest.Add(new Vector2(x, y));
                }
            }

            return dest;
        }

        #endregion Helper method

        #endregion Minimax
    }
}