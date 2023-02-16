using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipListVualizer
{
    public class SkipList<T> where T : IComparable
    {
        public Node<T> Head;
        Random random;

        public int Height { get; private set; }
        public SkipList(Random random)
        {
            Head = new Node<T>(default);
            this.random = random;
            
        }

        private int GetRandomHeight()
        {
            return random.Next(1, 3);
        }

        public void Insert(T value)
        {
            Node<T> addingNode = new Node<T>(value);
            Height = 1;

            while (GetRandomHeight() != 2)
            {
                Node<T> newNode = new Node<T>(value, ++Height);
                newNode.Below = addingNode;
                addingNode = newNode;
                if (addingNode.Height.CompareTo(Head.Height) > 0)
                {
                    Node<T> newHead = new Node<T>(default, Head.Height + 1);
                    newHead.Below = Head;
                    Head = newHead;
                    break;
                }
            }

            Node<T> current = Head;

            while (current != null)
            {
                if (current.Right == null)
                {
                    current.Right = addingNode;
                    addingNode = addingNode.Below;
                    current = current.Below;
                }
                else
                {
                    if (current.Right.Value.CompareTo(addingNode.Value) > 0)
                    {
                        current.Right = addingNode;
                        addingNode = addingNode.Below;
                        current = current.Below;
                    }

                    else
                    {
                        current = current.Below;
                    }
                }
            }
        }

    }
}