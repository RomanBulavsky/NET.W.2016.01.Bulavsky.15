using System;
using System.ComponentModel;
using System.Text;

namespace Task1.Matrixes
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        public DiagonalMatrix(params T[] args)
        {
            if (args == null)
                throw new ArgumentNullException();
            RowsAndColsNumber = args.Length;
            InnerMatrix = args;
        }


        public override T this[int i, int j]
        {
            get
            {
                if (IndexesValidation(i, j))
                    return InnerMatrix[i];
                throw new ArgumentException("get error");
            }
            set
            {
                var previousValue = this[i, j]; //IndexesValidation
                var e = new MatrixEventArgs<T>(i, j, previousValue, value);
                InnerMatrix[i] = value; //InnerMatrix[i * RowsAndColsNumber + j] = value;
                OnMatrixEvent(e);
            }
        }

        public override bool Validation(T[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            return matrix.Length == RowsAndColsNumber;
        }

        public override bool IndexesValidation(int i, int j) => i == j && i >= 0;

        public override string ToString()
        {
            T[,] mas = new T[RowsAndColsNumber, RowsAndColsNumber];

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < RowsAndColsNumber; i++)
            {
                for (int j = 0; j < RowsAndColsNumber; j++)
                {
                    if (i == j) result.Append(InnerMatrix[i].ToString() + "\t");
                    else result.Append("0\t" );
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}