using System;
using System.CodeDom;
using System.Linq;
using Task1.Matrixes;

namespace Task1
{
    public class MatrixVisitor<T> : IVisitor<T>
    {
        public void Visit(SquareMaxrix<T> m)
        {
            Console.WriteLine("Some method of Square");
        }

        public void Visit(DiagonalMatrix<T> m)
        {
            Console.WriteLine("Some method of Diag");
        }

        public void Visit(SymetricMatrix<T> m)
        {
            Console.WriteLine("Some method of symet");
        }

        /// <summary>
        /// The method that provides the addition of matrixes.
        /// </summary>
        public void Visit(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (matrixA.RowsAndColsNumber != matrixB.RowsAndColsNumber)
                throw new ArgumentOutOfRangeException();
            Visit((dynamic) matrixA, (dynamic) matrixB);
        }

        public void Visit(DiagonalMatrix<T> matrixA, SymetricMatrix<T> matrixB)
        {
            Visit(matrixB, matrixA);
        }

        public void Visit(SymetricMatrix<T> matrixA, DiagonalMatrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixB.RowsAndColsNumber; i++)
                {
                    matrixA[i, i] += (dynamic) matrixB[i, i];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }

        public void Visit(SquareMaxrix<T> matrixA, SymetricMatrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixB.RowsAndColsNumber; i++)
                {
                    for (int j = 0; j < matrixB.RowsAndColsNumber; j++)
                    {
                        matrixA[i, j] += (dynamic) matrixB[i, j];
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }

        public void Visit(SymetricMatrix<T> matrixA, SquareMaxrix<T> matrixB)
        {
            Visit(matrixB, matrixA);
        }

        public void Visit(SquareMaxrix<T> matrixA, DiagonalMatrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixB.RowsAndColsNumber; i++)
                {
                    matrixA[i, i] += (dynamic) matrixB[i, i];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }

        public void Visit(DiagonalMatrix<T> matrixA, SquareMaxrix<T> matrixB)
        {
            Visit(matrixB, matrixA);
        }

        public void Visit(SquareMaxrix<T> matrixA, SquareMaxrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixB.RowsAndColsNumber; i++)
                {
                    for (int j = 0; j < matrixB.RowsAndColsNumber; j++)
                    {
                        matrixA[i, j] += (dynamic) matrixB[i, j];
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }

        public void Visit(SymetricMatrix<T> matrixA, SymetricMatrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixA.InnerMatrix.Length; i++)
                    matrixA.InnerMatrix[i] += (dynamic) matrixB.InnerMatrix[i];
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }

        public void Visit(DiagonalMatrix<T> matrixA, DiagonalMatrix<T> matrixB)
        {
            try
            {
                for (int i = 0; i < matrixB.RowsAndColsNumber; i++)
                {
                    matrixA[i, i] += (dynamic) matrixB[i, i];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("bad");
            }
        }
    }
}