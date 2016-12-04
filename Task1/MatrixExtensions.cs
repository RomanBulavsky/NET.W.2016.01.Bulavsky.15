using System;
using Task1.Matrixes;

namespace Task1
{
    public static class MatrixExtensions
    {
        public static void SomeMethod<T>(this Matrix<T> matrix)
        {
            var visitor = new MatrixVisitor<T>();
            matrix.Accept(visitor);
        }

        /// <summary>
        /// The method that provides the addition of matrixes.
        /// </summary>
        public static void Add<T>(this Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (ReferenceEquals(matrixA, null) || ReferenceEquals(matrixB, null)) throw new ArgumentNullException();
            var visitor = new MatrixVisitor<T>();
            matrixA.Accept(visitor, matrixB);
        }
    }
}