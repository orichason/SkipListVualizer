using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListVualizer
{
    static class Extensions
    {

        public static void PrintList<T>(this SkipList<T> list, Node<T> head, SpriteBatch spriteBatch) where T : IComparable
        {
            head = list.Head;

            
        }
    }
}
