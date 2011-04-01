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
    class TextLine : MenuComponent
    {
        Rectangle area;
        public String text;
        Texture2D texture;
        Vector2 stringvector;
        Color normalcolor;
        Color clickedcolor;
        Color selectedcolor;
        Color currentcolor;
        public bool selected = false;

        public TextLine(Rectangle r, String t)
        {
            
            area = r;
            text = t;
            texture = new Texture2D(MenuManager.batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            stringvector = new Vector2(area.X, area.Center.Y);

            currentcolor = normalcolor = Color.Teal;
            clickedcolor = Color.Blue;
            selectedcolor = Color.Red;
        }
        public override bool Contains(int x, int y)
        {
            return false;
        }

        public override void Draw()
        {
            MenuManager.batch.DrawString(Game1.textFont, text, stringvector, Color.Yellow);
        }
    }
}
