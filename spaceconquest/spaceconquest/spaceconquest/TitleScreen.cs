using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceconquest
{
    class TitleScreen
    {
        private SpriteBatch batch;
        private SpriteFont font;

        public TitleScreen(SpriteBatch sb,SpriteFont sf)
        {
            batch = sb;
            font = sf;
        }

        public void Draw()
        {
            batch.Draw(new Texture2D(batch.GraphicsDevice, 20, 20), new Rectangle(0, 0, 100, 100), Color.Aquamarine);
            batch.DrawString(font, "Space Conquest", new Vector2(0, 0), Color.Red);

        }

    }
}
