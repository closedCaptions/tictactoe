using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe.Utility {
    public static class ArrayUtil {
        public static int GetLength (this Array arr, int dimension) {
            return arr.GetLength(dimension);
        }

        public static void Populate <T> (this T[,] arr, T value) {
            for (int y = 0; y < arr.GetLength(0); y++) {
                for (int x = 0; x < arr.GetLength(1); x++) {
                    arr[y, x] = value;
                }
            }
        }

        public static void Populate<T> (this T[] arr, T value) {
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = value;
            }
        }
    }
}
