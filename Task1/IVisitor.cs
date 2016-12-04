using Task1.Matrixes;

namespace Task1
{
    public interface IVisitor<T>
    {
        void Visit(SquareMaxrix<T> m);
        void Visit(DiagonalMatrix<T> m);
        void Visit(SymetricMatrix<T> m);
        void Visit(Matrix<T> matrixA, Matrix<T> matrixB);
    }
}