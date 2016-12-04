using System;

namespace Task1.Matrixes
{
    public class SquareMaxrix<T> : Matrix<T>
    {
        public override string ToString()
        {
            var result = "";
            var length = (int) Math.Sqrt(InnerMatrix.Length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    result += InnerMatrix[i * length + j].ToString() + " ";
                }
                result += "\n";
            }
            return result;
        }

        public SquareMaxrix(params T[] matrix)
        {
            InnerMatrix = matrix;
        }

        public SquareMaxrix(int rowsAndColsNumber)
        {
            if (rowsAndColsNumber > 0) RowsAndColsNumber = rowsAndColsNumber;
            InnerMatrix = new T[rowsAndColsNumber];
        }

        public override T this[int i, int j]
        {
            get
            {
                if (IndexesValidation(i, j))
                    return InnerMatrix[i * RowsAndColsNumber + j];
                throw new ArgumentException("get error");
            }
            set
            {
                var previousValue = this[i, j]; //IndexesValidation
                var e = new MatrixEventArgs<T>(i, j, previousValue, value);
                InnerMatrix[i * RowsAndColsNumber + j] = value; //InnerMatrix[i * RowsAndColsNumber + j] = value;
                OnMatrixEvent(e);
            }
        }

        public override bool Validation(T[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            int rowsAndColsNumber = (int) Math.Sqrt(matrix.Length);
            if (matrix.Length != rowsAndColsNumber * rowsAndColsNumber) throw new ArgumentException();
            RowsAndColsNumber = rowsAndColsNumber;
            return true;
        }

        public override bool IndexesValidation(int i, int j)
            => (i >= 0 && i <= RowsAndColsNumber && j >= 0 && j <= RowsAndColsNumber) ? true : false;
    }
}