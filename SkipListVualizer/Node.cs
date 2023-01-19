using System;
namespace SkipListVualizer
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Right;
        public Node<T> Below;
        public int Height;

        public Node(T value, Node<T> right, Node<T> below, int height)
        {
            Value = value;
            Right = right;
            Below = below;
            Height = height;
        }

        public Node(T value)
        : this(value, null, null, 1) { }

        public Node(T value, int height)
        : this(value, null, null, height) { }

    }
}