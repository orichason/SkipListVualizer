using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkipListVualizer
{
    internal class Button : Sprite
    {
        
        ButtonState previousPress;
        public Button(Texture2D texture, Vector2 vector2, float scale, Color color, SpriteFont font, string text, Color textColor) :
             base(texture, vector2, scale, color, font, text, textColor)
        {
        }

        public bool isClicked(MouseState mouseState)
        {
            if (previousPress == ButtonState.Released)
            {
                previousPress = mouseState.LeftButton;
                if(mouseState.LeftButton.Equals(ButtonState.Pressed))
                {
                    return true;
                }
            }

            else previousPress = mouseState.LeftButton;

            return false;
        }
    }
}
