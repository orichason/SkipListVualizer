using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListVualizer
{
    internal class Button : Sprite
    {
        bool previousPress;
        public Button(Texture2D texture, Vector2 vector2, float scale, Color color) :
             base(texture, vector2, scale, color)
        {
            previousPress = false;
        }

        public bool isClicked(MouseState mouseState)
        {
            bool Pressed = false;

            if (mouseState.LeftButton == ButtonState.Pressed) previousPress = true;



        }
    }
}
