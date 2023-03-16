using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace SkipListVualizer
{
    internal class TextBox : Sprite
    {
        public TextBox(Texture2D texture, Vector2 position, float scale, Color boxColor, SpriteFont font, string text, Color textColor) :
             base(texture, position, scale, boxColor, font, text, GetTextScale(texture, scale, font, text), textColor)
        {
        }

        public void AddText(string text)
        {
            this.Text += text;
        }

        public void SetText(string text)
        {
            this.Text = text;
        }

        static float GetTextScale(Texture2D texture, float scale, SpriteFont font, string text)
        {
            //Fix this function to scale the text
            float textScale = 1;

            Vector2 textLength = font.MeasureString(text);

            float textureWidth = texture.Width * scale;
            float textureHeight = texture.Height * scale;
            float textWidth = textLength.X;
            float textHeight = textLength.Y;

            if (textureWidth < textWidth || textureHeight < textHeight)
            {
                textScale = Math.Min(textureWidth / textWidth, textureHeight / textHeight);
            }

            return textScale;
        }
    }
}
