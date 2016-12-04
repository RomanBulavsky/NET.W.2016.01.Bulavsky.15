using System;
using System.Collections;
using System.Collections.Generic;
using MatrixTests;
using NUnit.Framework;
using Task2;

namespace BinaryTreeTests
{
    [TestFixture]
    public class Tests
    {
        struct Point
        {
            public int I { get; private set; }

            public Point(int i)
            {
                I = i;
            }

            public override string ToString()
            {
                return I.ToString();
            }
        }

        #region non-assert

        [Test]
        public void Test1()
        {
            IEnumerable<int> x = new int[] {1, 2, 3, 4, 5, 6, 7};
            var t = new BinaryTree<int>(x);
            BinaryTree<int>.DisplayPostOrder(t.Root);
            Console.WriteLine();
            var s = "abcdefg";
            BinaryTree<char> chartree = new BinaryTree<char>(s.ToCharArray());
            BinaryTree<char>.DisplayPostOrder(chartree.Root);
            Console.WriteLine();
        }

        [Test]
        public void Test2()
        {
            var tree = new BinaryTree<int>((a, b) => b % 3 - a % 2);
            tree.Add(5);
            tree.Add(1);
            tree.Add(0);
            tree.Add(3);
            tree.Add(2);
            tree.Add(4);
            tree.Add(7);
            tree.Add(6);
            tree.Add(8);
            Console.WriteLine($"\n Count is: {tree.Count}");

            #region OldRec

            Console.Write("IN ");
            BinaryTree<int>.DisplayInOrder(tree.Root);
            Console.WriteLine();

            Console.Write("Pre ");
            BinaryTree<int>.DisplayPreOrder(tree.Root);
            Console.WriteLine();

            Console.Write("Post ");
            BinaryTree<int>.DisplayPostOrder(tree.Root);
            Console.WriteLine();

            #endregion

            Console.Write("\nPreOrder: ");
            foreach (var leaf in tree)
            {
                Console.Write(leaf);
            }

            Console.Write("\nInOrder: ");

            foreach (var node in tree.GetInOrderEnumerator() as IEnumerable<int>)
            {
                Console.Write(node);
            }
            Console.Write("\nPostOrder: ");
            foreach (var node in (IEnumerable<int>) tree.GetPostOrderEnumerator())
            {
                Console.Write(node);
            }
            Console.WriteLine();
        }

        [Test]
        public void Test_Book()
        {
            var BookTree = new BinaryTree<Book>((a, b) => b.NumberOfPages - a.NumberOfPages);
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", 1233));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", 100));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", 2222));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", 123));

            BinaryTree<Book>.DisplayPreOrder(BookTree.Root);
            Console.WriteLine();
        }

        [Test]
        public void Test_Point()
        {
            var structTree = new BinaryTree<Point>((a, b) => a.I - b.I);
            structTree.Add(new Point(12));
            structTree.Add(new Point(13));
            structTree.Add(new Point(14));
            //BinaryTree<Point>.DisplayPostOrder(structTree.Root);
            foreach (var node in structTree)
            {
                Console.Write(node + " ");
            }
        }

        #endregion

        [TestCase(new int[]{5,1,0,3,2,4,7,6,8}, new int[]{0,1,2,3,4,5,6,7,8})]
        public void TestBinaryTreeWithInt(int[] args, int[] result)
        {
            var tree = new BinaryTree<int>(args);
            int[] InOrderResult = new int[tree.Count];
            var i = 0;
            foreach (var node in tree.GetInOrderEnumerator() as IEnumerable<int>)
            {
                if (InOrderResult != null) InOrderResult[i++] += node;
            }
            CollectionAssert.AreEquivalent(InOrderResult, result);
        }

        [TestCase(new string[]{"b","a","c","d"}, new string[]{"a","b","c","d"})]
        public void TestBinaryTreeWithString(string[] args, string[] result)
        {
            var tree = new BinaryTree<string>(args);
            string[] InOrderResult = new string[tree.Count];
            var i = 0;
            BinaryTree<string>.DisplayInOrder(tree.Root);
            foreach (var node in tree.GetInOrderEnumerator() as IEnumerable<string>)
            {
                if (InOrderResult != null) InOrderResult[i++] += node;
            }
            foreach (var s in InOrderResult)
            {
                Console.WriteLine(s);
            }
            CollectionAssert.AreEquivalent(InOrderResult, result);
        }

        [TestCase(new int[]{5,1,0,3,2,4,7,6,8}, new int[]{0,1,2,3,4,5,6,7,8})]
        public void TestBinaryTreeWithPoint(int[] args, int[] result)
        {
            var i = 0;

            var tree = new BinaryTree<Point>((a, b) => a.I - b.I);

            foreach (var x in args)
            {
               tree.Add(new Point(x));
            }

            int[] res = new int[tree.Count];
            foreach (var ar in tree.GetInOrderEnumerator() as IEnumerable<Point>)
            {
                res[i++] = ar.I;
            }

            CollectionAssert.AreEqual(res, result);
        }

        [TestCase(new int[]{5,1,11,3}, new int[]{1,3,5,11})]
        public void TestBinaryTreeWithBook(int[] args, int[] result)
        {
            var BookTree = new BinaryTree<Book>((a, b) => a.NumberOfPages - b.NumberOfPages);
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", args[0]));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", args[1]));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", args[2]));
            BookTree.Add(new Book("Smith", "Jhon", "Comedy", "JS", args[3]));


            var books = new int[BookTree.Count];
            var i = 0;
            foreach (var num in BookTree.GetInOrderEnumerator() as IEnumerable<Book>)
            {
                books[i++] = num.NumberOfPages;
            }

            CollectionAssert.AreEqual(books, result);
        }

    }
}