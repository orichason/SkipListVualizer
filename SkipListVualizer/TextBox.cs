using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SkipListVualizer
{
    internal class TextBox : Sprite
    {
        public TextBox(Texture2D texture, Vector2 position, float scale, Color boxColor, SpriteFont font, string text, Color textColor) :
             base(texture, position, scale, boxColor, font, text, textColor)
        {

        }

        public void SetText(string text)
        {
            this.Text += text;
        }

    }
}
