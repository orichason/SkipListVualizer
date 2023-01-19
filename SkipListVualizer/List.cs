using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipListVualizer
{
    public class SkipList<T> where T : IComparable
    {
        Node<T> head;
        Random random;

        public SkipList(Random random)
        {
            head = new Node<T>(default);
            this.random = random;

        }

        private int GetRandomHeight()
        {
            return random.Next(1, 3);
        }

        public void Insert(T value)
        {
            Node<T> addingNode = new Node<T>(value);
            int height = 1;

            while (GetRandomHeight() != 2)
            {
                Node<T> newNode = new Node<T>(value, ++height);
                newNode.Below = addingNode;
                addingNode = newNode;
                if (addingNode.Height.CompareTo(head.Height) > 0)
                {
                    Node<T> newHead = new Node<T>(default, head.Height + 1);
                    newHead.Below = head;
                    head = newHead;
                    break;
                }
            }

            Node<T> current = head;

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