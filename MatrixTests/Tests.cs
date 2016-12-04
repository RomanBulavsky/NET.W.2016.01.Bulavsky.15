using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using NUnit.Framework;
using Task1;
using Task1.Matrixes;


namespace MatrixTests
{
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        public string Author { get; }
        public string Title { get; }
        public string Genre { get; }
        public string Publisher { get; }
        public int NumberOfPages { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Book():this("Smith", "Jhon", "Comedy", "JS", 1233){}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="author">Author name.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="genre">Ganre of the book.</param>
        /// <param name="publisher">Publisher.</param>
        /// <param name="numberOfPages">Number of pages.</param>
        public Book(string author, string title, string genre, string publisher, int numberOfPages)
        {
            if (author == null || title == null || genre == null || publisher == null || numberOfPages < 1)
            {

                throw new ArgumentNullException();
            }
            Author = author;
            Title = title;
            Genre = genre;
            Publisher = publisher;
            NumberOfPages = numberOfPages;
        }

        /// <summary>
        /// Indicates equality between Book type objects.
        /// </summary>
        /// <param name="other"> Book type object for comparing. </param>
        /// <returns> Boolean value indicates equality of the parameters. </returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Author, other.Author) && string.Equals(Title, other.Title)
                   && string.Equals(Genre, other.Genre) && string.Equals(Publisher, other.Publisher)
                   && NumberOfPages == other.NumberOfPages;
        }

        /// <summary>
        /// Indicates equality between objects.
        /// </summary>
        /// <param name="obj"> Object for comparing. </param>
        /// <returns> Boolean value indicates equality of the parameters. </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book) obj);
        }

        /// <summary>
        /// Computes a hash code of the Book type object.
        /// </summary>
        /// <returns> Integer type, hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Author.GetHashCode();
                hashCode = (hashCode * 397) ^ Title.GetHashCode();
                hashCode = (hashCode * 397) ^ Genre.GetHashCode();
                hashCode = (hashCode * 397) ^ Publisher.GetHashCode();
                hashCode = (hashCode * 397) ^ NumberOfPages;
                return hashCode;
            }
        }

        /// <summary>
        /// Works like Equals.
        /// </summary>
        /// <param name="left"> Book type object for comparing. </param>
        /// <param name="right"> Book type object for comparing. </param>
        /// <returns> Boolean value indicates equality of the parameters.</returns>
        public static bool operator ==(Book left, Book right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Works like inverse Equals Method.
        /// </summary>
        /// <param name="left"> Object for comparing.</param>
        /// <param name="right"> Object for comparing.</param>
        /// <returns> Boolean value indicates non-equality of the parameters.</returns>
        public static bool operator !=(Book left, Book right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj"> Object with the Book type. </param>
        /// <returns> Integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object.</returns>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var authorComparison = string.Compare(Author, other.Author, StringComparison.Ordinal);
            if (authorComparison != 0) return authorComparison;
            var titleComparison = string.Compare(Title, other.Title, StringComparison.Ordinal);
            if (titleComparison != 0) return titleComparison;
            var genreComparison = string.Compare(Genre, other.Genre, StringComparison.Ordinal);
            if (genreComparison != 0) return genreComparison;
            var publisherComparison = string.Compare(Publisher, other.Publisher, StringComparison.Ordinal);
            if (publisherComparison != 0) return publisherComparison;
            return NumberOfPages.CompareTo(other.NumberOfPages);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type
        /// and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj"> Object with the object type.</param>
        /// <returns>Integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object.</returns>
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is Book)) throw new ArgumentException($"Object must be of type {nameof(Book)}");
            return CompareTo((Book) obj);
        }

        /// <summary>
        /// Represents Book type like a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{this.NumberOfPages} pages";//{this.Title} by {this.Author} in {this.Genre} genre, published by : {this.Publisher} that contains

        public static bool operator <(Book left, Book right)
        {
            return Comparer<Book>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(Book left, Book right)
        {
            return Comparer<Book>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(Book left, Book right)
        {
            return Comparer<Book>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(Book left, Book right)
        {
            return Comparer<Book>.Default.Compare(left, right) >= 0;
        }

        public static Book operator +(Book lhs, Book rhs)
        {
            return rhs;
        }


    }
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Matrix<int> m = new SquareMaxrix<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            //Matrix<string> m = new SquareMaxrix<string>(4, "zxcvbnmasdfghjkl");

            Console.WriteLine(m);
            m[1, 2] = 25;
            m.MatrixEvent += (o, e) => Console.WriteLine($"Event {e.PreviousValue} -> {e.NewValue}");
            m.MatrixEvent += (o, e) => Console.WriteLine($"Event 2 {e.PreviousValue} -> {e.NewValue}");

            m[3, 3] = 25;
            Console.WriteLine(m);
            //Assert.True(true);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21}, 6)]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, 5)]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 4)]
        public void Test2(int[] smas, int rc)
        {
            SymetricMatrix<int> m = new SymetricMatrix<int>(rc, smas);

            Console.WriteLine(m);
            m.MatrixEvent += (o, e) => Console.WriteLine($"Event cng {e.PreviousValue} to {e.NewValue}");
            m[0, 1] = 131;
            //Func<int,SymetricMatrix<int>,bool> f=(o,x) => o == x[0, 1] && o == x[1, 0];
            //Assert(f);
            m[3, 3] = 12;
            Console.WriteLine(m);
        }

        [TestCase(new int[] {1, 2, 3, 4})]
        [TestCase(new int[] {1, 2, 3, 4, 6, 7})]
        [TestCase(new int[] {1, 2, 3, 4, 5})]
        public void Test3(int[] mas)
        {
            DiagonalMatrix<int> m = new DiagonalMatrix<int>(mas);

            Console.WriteLine(m.ToString());

            m.MatrixEvent += (o, e) => Console.WriteLine($"Chng {e.PreviousValue} -> {e.NewValue} \n");
            //m[0, 1] = 12;
            m[1, 1] = 14;
            m[2, 2] = 11;
            Console.WriteLine(m.ToString());
        }

        [Test]
        public void ExtentionMethodTests()
        {
            DiagonalMatrix<Book> ds = new DiagonalMatrix<Book>(new Book("Roman", "Ditle", "Ganre", "Publisher", 8)
                ,new Book("Roman", "Ditle", "Ganre", "Publisher", 5));
            SymetricMatrix<Book> ss = new SymetricMatrix<Book>(2,new Book("Roman", "Ditle", "Ganre", "Publisher", 4)
                ,new Book("Roman", "Ditle", "Ganre", "Publisher", 5),new Book("ASDASDSAD", "Ditle", "Ganre", "Publisher", 12));

            Console.WriteLine(ss);

            Console.WriteLine(ds);

            ss.Add(ds);
            Console.WriteLine(ss);

            /*DiagonalMatrix<int> m = new DiagonalMatrix<int>(1, 2, 3, 4, 5);
            SymetricMatrix<int> m2 = new SymetricMatrix<int>(5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

            m2.MatrixEvent += (o, e) => Console.WriteLine($"EVENT: {o.ToString()}");
            Console.WriteLine(m2);
            m2.Add(m);
            Console.WriteLine(m2);
            m2.SomeMethod();

            m2[1, 1] = 12;*/
        }
        /// <summary>
        /// Test similar types.
        /// </summary>
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
             new int[] {2, 4, 6, 8, 10, 12, 14, 16, 18})]
        public void AddExtensionsIntTests_Square(int[] masA, int[] masB, int[] result)
        {
            var matrixA = new SquareMaxrix<int>(masA);
            var matrixB = new SquareMaxrix<int>(masB);
            var resultMatrix = new SquareMaxrix<int>(result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }

        [TestCase(3, new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3, 4, 5, 6},
             new int[] {2, 4, 6, 8, 10, 12})]
        public void AddExtensionsIntTests_Symetric(int rc, int[] masA, int[] masB, int[] result)
        {
            var matrixA = new SymetricMatrix<int>(rc, masA);
            var matrixB = new SymetricMatrix<int>(rc, masB);
            var resultMatrix = new SymetricMatrix<int>(rc, result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3, 4, 5, 6},
             new int[] {2, 4, 6, 8, 10, 12})]
        public void AddExtensionsIntTests_Diagonal(int[] masA, int[] masB, int[] result)
        {
            var matrixA = new DiagonalMatrix<int>(masA);
            var matrixB = new DiagonalMatrix<int>(masB);
            var resultMatrix = new DiagonalMatrix<int>(result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }
        /// <summary>
        /// Test different types
        /// </summary>
        /// <param name="dimension"> dimension</param>
        /// <param name="masA"></param>
        /// <param name="masB"></param>
        /// <param name="result"></param>
        [TestCase(3, new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3},
             new int[] {2, 4, 6, 4, 5, 6})]
        public void AddExtensionsIntTests_SymetricDiagonal(int dimension, int[] masA, int[] masB, int[] result)
        {
            var matrixA = new SymetricMatrix<int>(dimension, masA);
            var matrixB = new DiagonalMatrix<int>(masB); //TODO: add big diag  + sym => sym with 0s
            var resultMatrix = new SymetricMatrix<int>(dimension, result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, new int[] {1, 2, 3}, //SquareMatrix main diagonal 1-5-9
             new int[] {2, 2, 3, 4, 7, 6, 7, 8, 12})]
        public void AddExtensionsIntTests_SquareDiagonal(int[] masA, int[] masB, int[] result)
        {
            var matrixA = new SquareMaxrix<int>(masA);
            var matrixB = new DiagonalMatrix<int>(masB);
            var resultMatrix = new SquareMaxrix<int>(result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }

        [TestCase(3, new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, new int[] {1, 2, 3,4,5,6},//SquareMatrix main diagonal 1-5-9
             new int[] {2,6,8,8,7,12,12,14,12})]
        public void AddExtensionsIntTests_SquareSymetric(int dimension, int[] masA, int[] masB, int[] result)
        {
            var matrixA = new SquareMaxrix<int>(masA);
            Console.WriteLine(matrixA);
            var matrixB = new SymetricMatrix<int>(dimension, masB);
            Console.WriteLine(matrixB);
            var resultMatrix = new SquareMaxrix<int>(result);
            matrixA.Add(matrixB);

            Console.WriteLine(matrixA);
            Console.WriteLine(resultMatrix);

            CollectionAssert.AreEquivalent(matrixA.InnerMatrix, resultMatrix.InnerMatrix);
        }
    }
}