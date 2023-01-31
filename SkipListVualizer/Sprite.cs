using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListVualizer
{
    internal class Sprite
    {
        Texture2D texture;
        Vector2 vector2;
        float scale;
        Color color;
        SpriteFont font;
        string text;
        Color textColor;

        public Sprite(Texture2D texture, Vector2 vector2, float scale, Color color,SpriteFont font, string text, Color textColor)
        {
            this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.vector2 = vector2;
            this.scale = scale;
            this.color = color;
            this.font = font;
            this.text = text;
            this.textColor = textColor; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector2, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            
            Vector2 textSize = font.MeasureString(text);

            Vector2 newCoord = vector2 + new Vector2(texture.Width, texture.Height) * scale / 2 - textSize / 2;

            spriteBatch.DrawString(font, text, newCoord , textColor);
        }
    }
}
