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
        Vector2 position;
        float scale;
        Color color;
        SpriteFont font;
        Color textColor;

        protected string Text;

        public Sprite(Texture2D texture, Vector2 position, float scale, Color color,SpriteFont font, string text, Color textColor)
        {
            this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.position = position;
            this.scale = scale;
            this.color = color;
            this.font = font;
            this.Text = text;
            this.textColor = textColor; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            
            Vector2 textSize = font.MeasureString(Text);

            Vector2 newCoord = position + new Vector2(texture.Width, texture.Height) * scale / 2 - textSize / 2;

            spriteBatch.DrawString(font, Text, newCoord , textColor);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)this.position.X, (int)this.position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public string GetTextString()
        {
            return Text;
        }

        public int GetTextInt()
        {
            return int.Parse(Text);;
        }

    }
}
