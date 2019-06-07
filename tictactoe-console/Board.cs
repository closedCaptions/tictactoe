using System;
using System.Collections.Generic;
using System.Text;
using tictactoe.Utility;

namespace tictactoe {
    class Board {
        private State[,] arrRef;

        public State this[int y, int x] {
            get {
                return arrRef[y, x];
            }
            set {
                arrRef[y, x] = value;
            }
        }

        public Board (int height, int width, State populateState) {
            arrRef = new State[height, width];
            arrRef.Populate(State.Blank);
        }

        public void Display () {
            for (int y = 0; y < arrRef.GetLength(0); ++y) {
                for (int x = 0; x < arrRef.GetLength(1); ++x) {
                    if (x != 0)
                        Console.Write(" | ");

                    string writeString = arrRef[y, x] == State.Blank ? " " : arrRef[y, x].ToString();
                    Console.Write(writeString);
                }
                Console.Write("\n");
            }
        }
    }
}
