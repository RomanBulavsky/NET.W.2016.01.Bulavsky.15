using System;

/*Создать обобщенные классы для представления квадратной, симметрической и диагональной матриц
 (симметрическая матрица – это квадратная матрица, которая совпадает с транспонированной к ней;
диагональная матрица – это квадратная матрица, у которой элементы вне главной диагонали заведомо
 имеют значения по умолчанию типа элемента). Описать в созданных классах событие, которое происходит
 при изменении элемента матрицы с индексами (i, j).  Предусмотреть возможность расширения функциональности
 для иерархии классов, добавив возможность операции сложения двух матриц любого типа. Разработать unit-тесты.
*/

namespace Task1.Matrixes
{
    public class MatrixEventArgs<T> : EventArgs
    {
        public T PreviousValue { get; }
        public T NewValue { get; }
        private int Row { get; }
        private int Col { get; }

        public MatrixEventArgs(int row, int col, T previousValue, T newValue)
        {
            PreviousValue = previousValue;
            NewValue = newValue;
            Row = row;
            Col = col;
        }
    }

    public abstract class Matrix<T>
    {
        public event EventHandler<MatrixEventArgs<T>> MatrixEvent;
        private T[] _innerMatrix;

        public T[] InnerMatrix
        {
            get { return _innerMatrix; }
            protected set
            {
                if (Validation(value)) _innerMatrix = value;
            } //Validation can throw Exception by itself
        }

        public int RowsAndColsNumber { get; protected set; }

        /// <summary>
        /// Matrix Iterator. That helps us access the matrix as a two-dimensional array.
        /// </summary>
        /// <param name="i">row index</param>
        /// <param name="j">column index</param>
        public abstract T this[int i, int j] { get; set; }

        /// <summary>
        /// Validates the input matrix.
        /// </summary>
        /// <returns> True/False</returns>
        public abstract bool Validation(T[] matrix);

        /// <summary>
        /// Validate indexes for matrix.
        /// </summary>
        /// <param name="i">row index</param>
        /// <param name="j">column index</param>
        /// <returns>True/False</returns>
        public abstract bool IndexesValidation(int i, int j);

        /// <summary>
        /// Event Invoker.
        /// </summary>
        /// <param name="e"> Arguments of event</param>
        protected virtual void OnMatrixEvent(MatrixEventArgs<T> e)
        {
            MatrixEvent?.Invoke(this, e);
        }

        /// <summary>
        /// The method allows to use visitors methods.
        /// </summary>
        /// <param name="visitor">Class that contains method for Matrix.</param>
        /// <param name="matrixB">2nd matrix for method.</param>
        public void Accept(IVisitor<T> visitor, Matrix<T> matrixB)
        {
            if (matrixB == null) throw new ArgumentNullException();
            visitor.Visit(this, matrixB); //TODO: try expression + dynamic was deleted
        }

        public void Accept(IVisitor<T> visitor)
        {
            visitor.Visit((dynamic) this); //TODO: try expression
        }
    }
}