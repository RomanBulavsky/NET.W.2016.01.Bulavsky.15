using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

namespace Task2
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        #region Fields&Properties and Ctors

        private Node root;

        public Node Root
        {
            set
            {
                if (value != null) root = value;
                else throw new ArgumentNullException();
            }
            get { return root; }
        }

        public int Count { get; private set; }
        private IComparer<T> customComparer;

        public IComparer<T> CustomComparer
        {
            set
            {
                if (value != null) customComparer = value;
                else throw new ArgumentNullException();
            }
        }

        public BinaryTree(Node root, IComparer<T> customComparer)
        {
            Root = root;
            CustomComparer = customComparer;
        }

        public BinaryTree(Node root, Comparison<T> customComparer)
        {
            Root = root;
            CustomComparer = Comparer<T>.Create(customComparer);
        }

        public BinaryTree(IComparer<T> customComparer)
        {
            CustomComparer = customComparer;
        }

        public BinaryTree(Comparison<T> customComparer)
        {
            CustomComparer = Comparer<T>.Create(customComparer);
        }

        public BinaryTree(Node root)
        {
            Root = root;
        }

        public BinaryTree()
        {
        }

        public BinaryTree(IEnumerable<T> elements)
        {
            if (elements == null) throw new ArgumentNullException();
            foreach (var node in elements)
            {
                Add(node);
            }
        }

        #endregion

        /// <summary>
        /// Class that imitate node of binary tree, has a value and references to Left/Right nodes .
        /// </summary>
        public class Node
        {
            public T Value { get; private set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        /// <summary>
        /// Method that adds leaf to the tree node.
        /// </summary>
        /// <param name="node">The node which will be used as the entry point.</param>
        /// <param name="value">The value that will be stored in the node</param>
        private void Add(Node node, T value)
        {
            var result = customComparer != null
                ? customComparer.Compare(node.Value, value)
                : Comparer.Default.Compare(node.Value, value);

            if (result <= 0)
            {
                if (node.Right == null) node.Right = new Node(value);
                else
                    Add(node.Right, value);
            }
            else
            {
                if (node.Left == null) node.Left = new Node(value);
                else
                    Add(node.Left, value);
            }
        }

        /// <summary>
        /// Adds new node into the tree.
        /// </summary>
        /// <param name="value">The value that will be stored in the node</param>
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException();
            if (Root == null)
            {
                Root = new Node(value);
                Count = 1;
            }
            else
            {
                Add(Root, value);
                Count++;
            }
        }

        #region Recursive static display methods - compare with yield recursion

        /// <summary>
        /// Display all descendants of node in different order.
        /// </summary>
        /// <param name="node">Root node</param>
        public static void DisplayInOrder(Node node)
        {
            if (node == null) return;
            DisplayInOrder(node.Left);
            Console.Write(node.Value);
            DisplayInOrder(node.Right);
        }

        public static void DisplayPreOrder(Node node)
        {
            if (node == null) return;
            Console.Write(node.Value);
            DisplayPreOrder(node.Left);
            DisplayPreOrder(node.Right);
        }

        public static void DisplayPostOrder(Node node)
        {
            if (node == null) return;
            DisplayPostOrder(node.Left);
            DisplayPostOrder(node.Right);
            Console.Write(node.Value);
        }

        #endregion

        /// <summary>
        /// Returns an enumerator that iterates through a tree (in a different order) with inner type T.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator() => PreOrder(root).GetEnumerator();

        public IEnumerator<T> GetInOrderEnumerator() => InOrder(root).GetEnumerator();

        public IEnumerator<T> GetPostOrderEnumerator() => PostOrder(root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns Enumerable of tree elements (in a different order) with inner type T.
        /// </summary>
        /// <param name="node">Node of BinaryTree</param>
        /// <returns>Enumerable</returns>
        private IEnumerable<T> InOrder(Node node)
        {
            if (node == null) yield break;

            foreach (var n in InOrder(node.Left))
                yield return n;

            yield return node.Value;

            foreach (var n in InOrder(node.Right))
                yield return n;
        }

        private IEnumerable<T> PostOrder(Node node)
        {
            if (node == null) yield break;

            foreach (var e in PostOrder(node.Left))
                yield return e;

            foreach (var e in PostOrder(node.Right))
                yield return e;

            yield return node.Value; //<-Post ORDER LOL
        }

        private IEnumerable<T> PreOrder(Node node)
        {
            if (node == null) yield break;

            yield return node.Value;

            foreach (var e in PreOrder(node.Left))
                yield return e;

            foreach (var e in PreOrder(node.Right))
                yield return e;
        }
    }
}