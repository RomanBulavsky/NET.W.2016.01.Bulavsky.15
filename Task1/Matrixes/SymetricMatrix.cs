using System;
using System.Text;

namespace Task1.Matrixes
{
    public class SymetricMatrix<T> : Matrix<T>
    {
        #region ctor

        public SymetricMatrix(int rowsAndColsNumber, params T[] args)
        {
            if (rowsAndColsNumber > 0) RowsAndColsNumber = rowsAndColsNumber;
            InnerMatrix = args;
        }

        #endregion

        public override T this[int i, int j]
        {
            get
            {
                if (IndexesValidation(i, j))
                {
                    if (i == j) return InnerMatrix[i];
                    return InnerMatrix[i + RowsAndColsNumber - 1 + j];
                }
                throw new ArgumentException("get error");
            }
            set
            {
                var previousValue = this[i, j]; //IndexesValidation
                var e = new MatrixEventArgs<T>(i, j, previousValue, value);
                if (i == j) InnerMatrix[i] = value;
                else
                    InnerMatrix[i + RowsAndColsNumber - 1 + j] = value;
                //InnerMatrix[i * RowsAndColsNumber + j] = value;
                OnMatrixEvent(e);
            }
        }

        public override bool Validation(T[] matrix) //TODO:ij==ji
        {
            if (matrix == null) throw new ArgumentNullException();
            var rc = RowsAndColsNumber;
            return matrix.Length == (rc * rc - rc) / 2 + rc;
        }

        public override bool IndexesValidation(int i, int j)
            => (i >= 0 && i <= RowsAndColsNumber && j >= 0 && j <= RowsAndColsNumber) ? true : false;

        public override string ToString()
        {
            T[,] mas = new T[RowsAndColsNumber, RowsAndColsNumber];
            int stepper = RowsAndColsNumber;

            for (int i = 0; i < RowsAndColsNumber; i++)
            {
                for (int j = i; j < RowsAndColsNumber; j++)
                {
                    if (i == j) mas[i, j] = InnerMatrix[i];
                    else
                    {
                        mas[i, j] = mas[j, i] = InnerMatrix[stepper];
                        stepper++;
                    }
                }
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < RowsAndColsNumber; i++)
            {
                for (int j = 0; j < RowsAndColsNumber; j++)
                {
                    result.Append(mas[i, j].ToString() + "\t");
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}