using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
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

        public Sprite(Texture2D texture, Vector2 vector2, float scale, Color color)
        {
            this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.vector2 = vector2;
            this.scale = scale;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector2, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
