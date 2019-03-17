using System;

namespace Neuro.Utils
{
    /// <summary>
    /// reference:
    /// https://github.com/andrewfry/SharpML-Recurrent/blob/master/src/SharpML.Recurrent/Models/Matrix.cs
    /// </summary>
    [Serializable]
    public class Matrix
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public double[] W;
        public double[] Dw;
        public double[] StepCache;

        public Matrix(int dim)
        {
            Rows = dim;
            Cols = 1;
            W = new double[Rows * Cols];
            Dw = new double[Rows * Cols];
            StepCache = new double[Rows * Cols];
        }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            W = new double[Rows * Cols];
            Dw = new double[Rows * Cols];
            StepCache = new double[Rows * Cols];
        }

        public Matrix(double[] vector)
        {
            Rows = vector.Length;
            Cols = 1;
            W = vector;
            Dw = new double[Rows * Cols];
            StepCache = new double[Rows * Cols];
        }

        private int GetIndex(int row, int col)
        {
            return Cols * row + col;
        }

        private double GetW(int row, int col)
        {
            return W[GetIndex(row, col)];
        }

        public override string ToString()
        {
            string result = "";
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    result += string.Format("{0:N5}", GetW(r, c)) + "\t";
                }
                result += "\n";
            }
            return result;
        }


    }
}
