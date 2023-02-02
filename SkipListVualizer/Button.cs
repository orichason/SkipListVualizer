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
        public Button(Texture2D texture, Vector2 position, float scale, Color color, SpriteFont font, string text, Color textColor) :
             base(texture, position, scale, color, font, text, textColor)
        {
        }

        public bool isClicked(MouseState mouseState)
        {
            Rectangle buttonBounds = GetBounds();
            int x1 = buttonBounds.X;
            int x2 = buttonBounds.X + buttonBounds.Width;
            int y1 = buttonBounds.Y;
            int y2 = buttonBounds.Y + buttonBounds.Height;

            if (previousPress == ButtonState.Released)
            {
                previousPress = mouseState.LeftButton;
                if (mouseState.LeftButton.Equals(ButtonState.Pressed) && mouseState.Position.X > x1 && mouseState.Position.X < x2 && mouseState.Position.Y > y1 && mouseState.Position.Y < y2)
                {
                    return true;
                }
            }
            else previousPress = mouseState.LeftButton;

            return false;
        }
    }
}
