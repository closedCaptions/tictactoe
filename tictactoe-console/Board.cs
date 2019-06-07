using System;
using System.Collections.Generic;
using System.Text;
using tictactoe.Utility;

namespace tictactoe {
    public class Board {
        private State[,] arrRef;

        public State this[int y, int x] {
            get {
                return arrRef[y, x];
            }
            set {
                arrRef[y, x] = value;
            }
        }

        public Board (int height, int width)
            :this(height, width, State.Blank) { }

        public Board (int height, int width, State populateState) {
            arrRef = new State[height, width];
            arrRef.Populate(populateState);
        }

        public void Display () {
            for (int y = 0; y < arrRef.GetLength(0); ++y) {
                if (y != 0)
                    Console.WriteLine("--- --- ---");
                for (int x = 0; x < arrRef.GetLength(1); ++x) {
                    if (x != 0)
                        Console.Write("|");

                    string writeString = "   ";
                    switch (arrRef[y, x]) {
                        case State.O:
                            writeString = " O ";
                            break;
                        case State.X:
                            writeString = " X ";
                            break;
                    }

                    Console.Write(writeString);
                }
                Console.Write("\n");
            }
        }
    }
}
